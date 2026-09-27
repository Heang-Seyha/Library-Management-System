using LibraryManagementSystem.Data;
using LibraryManagementSystem.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem
{
    internal static class Program
    {
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
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");

            // ── Configure EF Core ─────────────────────────────────────────────
            DbOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer(connectionString)
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

            // ── Launch the application with the Login form ────────────────────
            Application.Run(new LoginForm());
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