using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem
{
    internal static class Program
    {
        private static readonly string[] FallbackInstances =
        {
            @".\MSSQLSERVER2022",
            @".\SQLEXPRESS",
            "localhost",
            ".",
            @"(localdb)\mssqllocaldb"
        };

        /// <summary>
        /// The EF Core DbContext factory — used throughout the application
        /// to create new DbContext instances when needed.
        /// </summary>
        public static DbContextOptions<LibraryDbContext> DbOptions { get; private set; } = null!;

        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            // ── Load configuration from appsettings.json ──────────────────────
            string? configuredConnectionString = null;
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .Build();

                configuredConnectionString = config.GetConnectionString("DefaultConnection");
            }
            catch
            {
                // If appsettings.json cannot be read, fall back to auto-discovery
            }

            // ── Fast Database Auto-Discovery and Fallback ─────────────────────
            string? validConnectionString = DiscoverValidConnectionString(configuredConnectionString);

            if (string.IsNullOrEmpty(validConnectionString))
            {
                MessageBox.Show(
                    "Failed to connect to the database.\n\n" +
                    "Please ensure SQL Server is running and reachable on this machine.\n\n" +
                    "Error: Could not connect to any local SQL Server instance (tested existing configuration, " +
                    @".\MSSQLSERVER2022, .\SQLEXPRESS, localhost, '.', and (localdb)\mssqllocaldb).",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // ── Configure EF Core ─────────────────────────────────────────────
            DbOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer(validConnectionString)
                .Options;

            // ── Ensure database is created and apply migrations ───────────────
            try
            {
                using var context = new LibraryDbContext(DbOptions);
                context.Database.Migrate();   // Creates DB + applies migrations

                // Seed demo data on first run
                DbInitializer.Seed(context);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to connect to the database.\n\n" +
                    $"Please ensure SQL Server is running and the connection string is correct.\n\n" +
                    $"Error: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // ── Automated Test Suite Mode ─────────────────────────────────────
            if (args.Length > 0 && args[0].Equals("--test", StringComparison.OrdinalIgnoreCase))
            {
                int exitCode = Tests.RegressionTestRunner.RunAllTests();
                Environment.ExitCode = exitCode;
                return;
            }

            // ── Launch the application with MainShellForm (which presents modal login) ──
            Application.Run(new MainShellForm());
        }

        /// <summary>
        /// Discovers a working SQL Server connection string by testing the configured
        /// connection string first, followed by standard local fallback instances.
        /// </summary>
        private static string? DiscoverValidConnectionString(string? configuredConnectionString)
        {
            // 1. Fast Connectivity Check: Attempt connecting using the existing ConnectionString first
            if (!string.IsNullOrWhiteSpace(configuredConnectionString))
            {
                var testExistingConnStr = BuildCandidateConnectionString(configuredConnectionString, null, timeoutSeconds: 2);
                if (CanConnect(testExistingConnStr))
                {
                    return configuredConnectionString;
                }
            }

            // Identify the already tested instance so we avoid redundant checks
            string? failedInstance = null;
            if (!string.IsNullOrWhiteSpace(configuredConnectionString))
            {
                try
                {
                    failedInstance = new SqlConnectionStringBuilder(configuredConnectionString).DataSource;
                }
                catch
                {
                    // Ignore parsing error on invalid existing connection string
                }
            }

            // 2. Fallback Candidates: Test common local SQL Server instances with 2s timeout
            foreach (var candidate in FallbackInstances)
            {
                if (string.Equals(candidate, failedInstance, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var candidateTestConnStr = BuildCandidateConnectionString(configuredConnectionString, candidate, timeoutSeconds: 2);
                if (CanConnect(candidateTestConnStr))
                {
                    // 3. Auto-Save Configuration: Construct full production ConnectionString (with standard 30s timeout)
                    var prodConnStr = BuildCandidateConnectionString(configuredConnectionString, candidate, timeoutSeconds: 30);

                    // Automatically update and save the new connection string back into appsettings.json
                    SaveConnectionStringToAppSettings(prodConnStr);

                    return prodConnStr;
                }
            }

            return null;
        }

        /// <summary>
        /// Tests if a connection can be established using the provided connection string (within a 2-second timeout).
        /// If the database does not exist yet (e.g., initial install), verifies instance reachability via 'master'.
        /// </summary>
        private static bool CanConnect(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString)
                {
                    ConnectTimeout = 2
                };

                var options = new DbContextOptionsBuilder<LibraryDbContext>()
                    .UseSqlServer(builder.ConnectionString)
                    .Options;

                var sw = Stopwatch.StartNew();
                using (var context = new LibraryDbContext(options))
                {
                    if (context.Database.CanConnect())
                    {
                        return true;
                    }
                }
                sw.Stop();

                // If connection took >= 1900ms, it failed due to timeout (server unreachable).
                // Avoid redundant timeout penalty against 'master'.
                if (sw.ElapsedMilliseconds >= 1900)
                {
                    return false;
                }

                // Server responded quickly, but target database might not exist yet before Migrate().
                // Verify server instance reachability via 'master'.
                builder.InitialCatalog = "master";
                var masterOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                    .UseSqlServer(builder.ConnectionString)
                    .Options;

                using (var masterContext = new LibraryDbContext(masterOptions))
                {
                    return masterContext.Database.CanConnect();
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Builds a candidate connection string with specified server instance and timeout.
        /// Preserves database name, credentials, and settings where available.
        /// </summary>
        private static string BuildCandidateConnectionString(string? templateConnectionString, string? targetInstance, int timeoutSeconds)
        {
            SqlConnectionStringBuilder builder;
            if (!string.IsNullOrWhiteSpace(templateConnectionString))
            {
                try
                {
                    builder = new SqlConnectionStringBuilder(templateConnectionString);
                }
                catch
                {
                    builder = new SqlConnectionStringBuilder();
                }
            }
            else
            {
                builder = new SqlConnectionStringBuilder();
            }

            if (!string.IsNullOrWhiteSpace(targetInstance))
            {
                builder.DataSource = targetInstance;
            }

            if (string.IsNullOrWhiteSpace(builder.InitialCatalog))
            {
                builder.InitialCatalog = "LibraryManagementDB";
            }

            if (string.IsNullOrWhiteSpace(builder.UserID))
            {
                builder.IntegratedSecurity = true;
            }

            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = timeoutSeconds;

            return builder.ConnectionString;
        }

        /// <summary>
        /// Updates appsettings.json with the new connection string using System.Text.Json.
        /// </summary>
        private static void SaveConnectionStringToAppSettings(string newConnectionString)
        {
            var candidatePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"),
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\appsettings.json"))
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

            foreach (var path in candidatePaths)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        var jsonText = File.ReadAllText(path);
                        var root = JsonNode.Parse(jsonText) as JsonObject ?? new JsonObject();

                        if (root["ConnectionStrings"] is not JsonObject connStrings)
                        {
                            connStrings = new JsonObject();
                            root["ConnectionStrings"] = connStrings;
                        }

                        connStrings["DefaultConnection"] = newConnectionString;
                        File.WriteAllText(path, root.ToJsonString(jsonOptions) + Environment.NewLine);
                    }
                }
                catch
                {
                    // Ignore non-fatal file write errors
                }
            }

            try
            {
                var baseDirFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                if (!File.Exists(baseDirFile))
                {
                    var root = new JsonObject
                    {
                        ["ConnectionStrings"] = new JsonObject
                        {
                            ["DefaultConnection"] = newConnectionString
                        }
                    };
                    File.WriteAllText(baseDirFile, root.ToJsonString(jsonOptions) + Environment.NewLine);
                }
            }
            catch
            {
                // Ignore non-fatal file write errors
            }
        }

        /// <summary>
        /// Creates a new DbContext instance.
        /// Call this in services and forms that need database access.
        /// </summary>
        public static LibraryDbContext CreateDbContext()
        {
            return new LibraryDbContext(DbOptions);
        }
    }
}