using LibraryManagementSystem.Data;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Tests
{
    /// <summary>
    /// Core regression suite — approximately 11 high-value business tests.
    ///
    /// Isolation: All tests run against a dedicated temporary SQL Server database
    /// (LibraryManagementDB_RegressionTest) that is created fresh, seeded, and
    /// dropped automatically. The user's normal application database is never touched.
    ///
    /// Run: dotnet run -- --test
    /// </summary>
    public static class RegressionTestRunner
    {
        // ── Test database name — isolated from the production DB ──────────────
        private const string TestDatabaseName = "LibraryManagementDB_RegressionTest";

        private static int _passed = 0;
        private static int _failed = 0;

        // ── Seeded test entity IDs — set once during setup ────────────────────
        private static int _adminId;
        private static int _librarianId;
        private static int _memberId;
        private static int _bookId;        // book with 5 available copies
        private static int _categoryId;
        private static int _authorId;
        private static int _publisherId;

        public static int RunAllTests()
        {
            _passed = 0;
            _failed = 0;

            Console.WriteLine("Running regression tests...");
            Console.WriteLine();

            DbContextOptions<LibraryDbContext> testOptions;
            try
            {
                testOptions = BuildTestDbOptions();
                SetupTestDatabase(testOptions);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FATAL] Could not create test database: {ex.Message}");
                Console.ResetColor();
                return 1;
            }

            try
            {
                RunTest("Authentication",          () => Test_Authentication(testOptions));
                RunTest("Authorization",           () => Test_Authorization(testOptions));
                RunTest("Book Validation",         () => Test_BookValidation(testOptions));
                RunTest("Member Validation",       () => Test_MemberValidation(testOptions));
                RunTest("Borrow",                  () => Test_Borrow(testOptions));
                RunTest("Inventory Protection",    () => Test_InventoryProtection(testOptions));
                RunTest("Return",                  () => Test_Return(testOptions));
                RunTest("Fine Calculation",        () => Test_FineCalculation());
                RunTest("Delete Safety",           () => Test_DeleteSafety(testOptions));
                RunTest("Role Rules",              () => Test_RoleRules(testOptions));
                RunTest("Concurrency (Borrow)",    () => Test_ConcurrentBorrowLastCopy(testOptions));
                RunTest("Concurrency (Return)",    () => Test_ConcurrentDoubleReturn(testOptions));
            }
            finally
            {
                TeardownTestDatabase(testOptions);
                SessionManager.Logout();
            }

            Console.WriteLine();
            Console.WriteLine($"{_passed + _failed}/{_passed + _failed} tests completed.");

            if (_failed == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{_passed}/{_passed + _failed} tests passed.");
                Console.WriteLine("Regression suite completed successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{_passed}/{_passed + _failed} tests passed. {_failed} FAILED.");
            }
            Console.ResetColor();

            return _failed == 0 ? 0 : 1;
        }

        // ── Test runner ───────────────────────────────────────────────────────

        private static void RunTest(string name, Action test)
        {
            try
            {
                SessionManager.Logout(); // Ensure clean session state before each test
                test();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] {name}");
                Console.ResetColor();
                _passed++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] {name}");
                Console.ResetColor();
                Console.WriteLine($"       Expected: see assertion below");
                Console.WriteLine($"       Error:    {ex.Message}");
                _failed++;
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
        }

        // ── Test database lifecycle ───────────────────────────────────────────

        private static DbContextOptions<LibraryDbContext> BuildTestDbOptions()
        {
            // Read the production connection string from appsettings.json and replace
            // the database name with the isolated test database name.
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var prodConnStr = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection not found in appsettings.json.");

            var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(prodConnStr)
            {
                InitialCatalog = TestDatabaseName
            };

            return new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer(builder.ConnectionString)
                .Options;
        }

        private static void SetupTestDatabase(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);

            // Create/reset the test database schema
            ctx.Database.EnsureDeleted();
            ctx.Database.Migrate();

            // ── Seed minimal deterministic test data ──────────────────────────

            var admin = new Librarian
            {
                Name = "Test Admin",
                Gender = "Male",
                DateOfBirth = new DateTime(1985, 1, 1),
                Username = "test_admin",
                PasswordHash = PasswordHasher.Hash("admin123"),
                Role = "Admin",
                Phone = "012000001",
                Email = "admin@test.lib"
            };
            var lib = new Librarian
            {
                Name = "Test Librarian",
                Gender = "Female",
                DateOfBirth = new DateTime(1990, 6, 15),
                Username = "test_librarian",
                PasswordHash = PasswordHasher.Hash("librarian123"),
                Role = "Librarian",
                Phone = "012000002",
                Email = "librarian@test.lib"
            };
            ctx.Librarians.AddRange(admin, lib);

            var member = new Member
            {
                Name = "Test Member",
                Gender = "Male",
                DateOfBirth = new DateTime(1995, 3, 20),
                Phone = "012000003",
                Email = "member@test.lib",
                Address = "Phnom Penh",
                JoinDate = DateTime.Today
            };
            ctx.Members.Add(member);

            var category = new Category { Name = "Test Category", Description = "For regression tests" };
            var author = new Author { Name = "Test Author", Gender = "Male", DateOfBirth = new DateTime(1970, 1, 1), Bio = "" };
            var publisher = new Publisher { Name = "Test Publisher", Address = "Test City", Phone = "012000004" };
            ctx.Categories.Add(category);
            ctx.Authors.Add(author);
            ctx.Publishers.Add(publisher);
            ctx.SaveChanges();

            // Book with 5 copies
            var book = new Book
            {
                Title = "Test Book Alpha",
                ISBN = "978-0-306-40615-7",
                Year = 2020,
                TotalCopies = 5,
                AvailableCopies = 5,
                CategoryId = category.CategoryId,
                AuthorId = author.AuthorId,
                PublisherId = publisher.PublisherId
            };
            ctx.Books.Add(book);
            ctx.SaveChanges();

            // Store IDs for use by all tests
            _adminId = admin.LibrarianId;
            _librarianId = lib.LibrarianId;
            _memberId = member.MemberId;
            _bookId = book.BookId;
            _categoryId = category.CategoryId;
            _authorId = author.AuthorId;
            _publisherId = publisher.PublisherId;
        }

        private static void TeardownTestDatabase(DbContextOptions<LibraryDbContext> options)
        {
            try
            {
                using var ctx = new LibraryDbContext(options);
                ctx.Database.EnsureDeleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARN] Could not drop test database: {ex.Message}");
            }
        }

        // ── 01. AUTHENTICATION ────────────────────────────────────────────────

        private static void Test_Authentication(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var auth = new AuthenticationService(ctx);

            // Valid admin login
            var admin = auth.Login("test_admin", "admin123");
            Assert(admin != null, "Admin login failed with valid credentials.");
            Assert(admin!.Role == "Admin", $"Expected Admin role, got '{admin.Role}'.");

            // Valid librarian login
            var lib = auth.Login("test_librarian", "librarian123");
            Assert(lib != null, "Librarian login failed with valid credentials.");
            Assert(lib!.Role == "Librarian", $"Expected Librarian role, got '{lib.Role}'.");

            // Wrong password
            var bad = auth.Login("test_admin", "WrongPassword!999");
            Assert(bad == null, "Login must fail with incorrect password.");

            // Unknown username
            var unknown = auth.Login("nobody_xyz_999", "admin123");
            Assert(unknown == null, "Login must fail with unknown username.");

            // Empty credentials
            Assert(auth.Login("", "admin123") == null, "Empty username must be rejected.");
            Assert(auth.Login("test_admin", "") == null, "Empty password must be rejected.");
        }

        // ── 02. AUTHORIZATION ─────────────────────────────────────────────────

        private static void Test_Authorization(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var admin = ctx.Librarians.Find(_adminId)!;
            var lib = ctx.Librarians.Find(_librarianId)!;
            var svc = new LibrarianService(ctx);

            // Policy matrix assertions
            Assert(AuthorizationHelper.CanManageLibrarians("Admin"), "Admin must manage librarians.");
            Assert(!AuthorizationHelper.CanManageLibrarians("Librarian"), "Librarian must NOT manage librarians.");
            Assert(AuthorizationHelper.CanManageBooks("Librarian"), "Librarian must manage books.");
            Assert(AuthorizationHelper.CanCirculate("Librarian"), "Librarian must circulate.");
            Assert(!AuthorizationHelper.CanManageLibrarians(null), "No session = no librarian management.");

            // Service enforces authorization even if caller bypasses UI
            SessionManager.Login(lib);
            var newLib = new Librarian
            {
                Name = "Unauthorized Test",
                Username = "unauth_xyz_test",
                Gender = "Male",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = "Librarian",
                Phone = "012111222",
                Email = "unauth@test.lib"
            };
            var (addOk, addMsg) = svc.Add(newLib, "Pass1234!");
            Assert(!addOk, "Librarian role must not be allowed to add librarians.");
            Assert(addMsg.Contains("privileges", StringComparison.OrdinalIgnoreCase) ||
                   addMsg.Contains("admin", StringComparison.OrdinalIgnoreCase),
                $"Expected auth error message, got: '{addMsg}'.");

            // Unauthenticated borrow must be rejected at service layer
            SessionManager.Logout();
            var borrowSvc = new BorrowService(ctx);
            var (rAuth, msgAuth) = borrowSvc.CreateBorrow(_memberId, DateTime.Today.AddDays(7),
                new List<(int, int)> { (_bookId, 1) });
            Assert(!rAuth, "Borrow without authenticated session must be rejected.");
            Assert(msgAuth.Contains("Authentication", StringComparison.OrdinalIgnoreCase) ||
                   msgAuth.Contains("logged in", StringComparison.OrdinalIgnoreCase),
                $"Expected authentication error, got: '{msgAuth}'.");
        }

        // ── 03. BOOK VALIDATION ───────────────────────────────────────────────

        private static void Test_BookValidation(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var svc = new BookService(ctx);

            // Valid ISBN-10 and ISBN-13 checksums
            Assert(ValidationHelper.IsValidISBN("0-306-40615-2"), "Valid ISBN-10 rejected.");
            Assert(!ValidationHelper.IsValidISBN("0-306-40615-9"), "Invalid ISBN-10 checksum accepted.");
            Assert(ValidationHelper.IsValidISBN("978-0-306-40615-7"), "Valid ISBN-13 rejected.");
            Assert(!ValidationHelper.IsValidISBN("978-0-306-40615-0"), "Invalid ISBN-13 checksum accepted.");
            Assert(!ValidationHelper.IsValidISBN("NOT-AN-ISBN"), "Garbage string accepted as ISBN.");

            // Duplicate ISBN rejected at service layer
            var dup = new Book
            {
                Title = "Duplicate ISBN Book",
                ISBN = "978-0-306-40615-7",  // Same as seeded book
                Year = 2021,
                TotalCopies = 1,
                AvailableCopies = 1,
                CategoryId = _categoryId,
                AuthorId = _authorId,
                PublisherId = _publisherId
            };
            var (dupOk, dupMsg) = svc.Add(dup);
            Assert(!dupOk, "BookService must reject duplicate ISBN.");
            Assert(dupMsg.Contains("already exists", StringComparison.OrdinalIgnoreCase),
                $"Expected 'already exists', got: '{dupMsg}'.");

            // Invalid copies: AvailableCopies > TotalCopies
            var validator = new BookValidator();
            var invalid = new Book
            {
                Title = "Bad Copies Book",
                ISBN = "978-3-16-148410-0",
                Year = 2020,
                TotalCopies = 2,
                AvailableCopies = 5,  // Exceeds TotalCopies
                CategoryId = _categoryId,
                AuthorId = _authorId,
                PublisherId = _publisherId
            };
            var valResult = validator.Validate(invalid);
            Assert(!valResult.IsValid, "Validator must reject AvailableCopies > TotalCopies.");

            // Non-existent foreign key rejected
            var badFkBook = new Book
            {
                Title = "Bad Category FK Book",
                ISBN = "978-1-491-95035-7",
                Year = 2021,
                TotalCopies = 1,
                AvailableCopies = 1,
                CategoryId = 999999, // non-existent category ID
                AuthorId = _authorId,
                PublisherId = _publisherId
            };
            var (fkOk, fkMsg) = svc.Add(badFkBook);
            Assert(!fkOk, "Book with non-existent CategoryId must be rejected.");
            Assert(fkMsg.Contains("category", StringComparison.OrdinalIgnoreCase),
                $"Expected category error message, got: '{fkMsg}'.");
        }

        // ── 04. MEMBER VALIDATION ─────────────────────────────────────────────

        private static void Test_MemberValidation(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var svc = new MemberService(ctx);
            var validator = new MemberValidator();

            // Missing required fields
            Assert(!validator.Validate(new Member { Name = "X", Phone = "012345678", Email = "x@x.com" }).IsValid,
                "Member without Gender and DateOfBirth must be invalid.");
            Assert(!validator.Validate(new Member { Name = "X", Gender = "Male", DateOfBirth = DateTime.Today, Phone = "bad-phone", Email = "x@x.com" }).IsValid,
                "Member with invalid phone must be invalid.");
            Assert(!validator.Validate(new Member { Name = "X", Gender = "Male", DateOfBirth = DateTime.Today, Phone = "012345678", Email = "not-an-email" }).IsValid,
                "Member with invalid email must be invalid.");

            // Add a valid member, verify it persists
            string uniqueEmail = $"valtest_{Guid.NewGuid():N}@test.lib";
            string uniquePhone = "012999888";
            var validMember = new Member
            {
                Name = "Validation Test Member",
                Gender = "Female",
                DateOfBirth = new DateTime(1997, 4, 10),
                Phone = uniquePhone,
                Email = uniqueEmail,
                Address = "Test City",
                JoinDate = DateTime.Today
            };
            var (ok, msg) = svc.Add(validMember);
            Assert(ok, $"Valid member rejected: {msg}");
            Assert(validMember.MemberId > 0, "Saved member must have a generated ID.");

            // Duplicate email rejected
            var dupMember = new Member
            {
                Name = "Duplicate Email Member",
                Gender = "Female",
                DateOfBirth = new DateTime(1998, 1, 1),
                Phone = "012999777",
                Email = uniqueEmail,
                Address = "Test City",
                JoinDate = DateTime.Today
            };
            var (dupOk, dupMsg) = svc.Add(dupMember);
            Assert(!dupOk, "MemberService must reject duplicate member email.");
            Assert(dupMsg.Contains("already exists", StringComparison.OrdinalIgnoreCase),
                $"Expected 'already exists', got: '{dupMsg}'.");

            // Cleanup
            ctx.Members.Remove(ctx.Members.Find(validMember.MemberId)!);
            ctx.SaveChanges();
        }

        // ── 05. BORROW ────────────────────────────────────────────────────────

        private static void Test_Borrow(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var svc = new BorrowService(ctx);

            // Reload fresh book state
            var book = ctx.Books.Find(_bookId)!;
            ctx.Entry(book).Reload();
            int initial = book.AvailableCopies;

            // Successful borrow via authenticated session
            SessionManager.Login(ctx.Librarians.Find(_adminId)!);
            var (ok, msg) = svc.CreateBorrow(_memberId, DateTime.Today.AddDays(7),
                new List<(int, int)> { (_bookId, 1) });
            Assert(ok, $"Valid borrow failed: {msg}");

            // Inventory decremented
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initial - 1,
                $"Expected AvailableCopies={initial - 1}, got {book.AvailableCopies}.");

            // Borrow record references the session librarian
            var created = ctx.Borrows
                .Where(b => b.MemberId == _memberId)
                .OrderByDescending(b => b.BorrowId)
                .First();
            Assert(created.LibrarianId == _adminId,
                "Borrow must reference the authenticated session librarian.");

            // Return to restore inventory for subsequent tests
            svc.ProcessReturn(created.BorrowId);
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initial, "AvailableCopies must restore after return.");

            // Cleanup
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == created.BorrowId));
            ctx.Borrows.Remove(created);
            ctx.SaveChanges();
        }

        // ── 06. INVENTORY PROTECTION ──────────────────────────────────────────

        private static void Test_InventoryProtection(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var svc = new BorrowService(ctx);

            var book = ctx.Books.Find(_bookId)!;
            ctx.Entry(book).Reload();
            int initial = book.AvailableCopies;

            // Request more than available
            var (ok, msg) = svc.CreateBorrow(_memberId, _librarianId,
                DateTime.Today.AddDays(7),
                new List<(int, int)> { (_bookId, initial + 10) });
            Assert(!ok, "Borrow exceeding available copies must be rejected.");
            Assert(msg.Contains("only", StringComparison.OrdinalIgnoreCase) ||
                   msg.Contains("insufficient", StringComparison.OrdinalIgnoreCase),
                $"Expected inventory error, got: '{msg}'.");

            // Inventory unchanged after rejection
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initial,
                $"Inventory must remain unchanged after rejected borrow. Expected {initial}, got {book.AvailableCopies}.");

            // Zero-quantity rejected
            var (zOk, _) = svc.CreateBorrow(_memberId, _librarianId,
                DateTime.Today.AddDays(7),
                new List<(int, int)> { (_bookId, 0) });
            Assert(!zOk, "Zero quantity must be rejected.");

            // Past due date rejected
            var (pOk, _) = svc.CreateBorrow(_memberId, _librarianId,
                DateTime.Today.AddDays(-1),
                new List<(int, int)> { (_bookId, 1) });
            Assert(!pOk, "Past due date must be rejected.");

            // Due date = today (must be at least tomorrow)
            var (tOk, tMsg) = svc.CreateBorrow(_memberId, _librarianId,
                DateTime.Today,
                new List<(int, int)> { (_bookId, 1) });
            Assert(!tOk, "Due date equal to today must be rejected.");
            Assert(tMsg.Contains("at least one day", StringComparison.OrdinalIgnoreCase),
                $"Expected due date message, got: '{tMsg}'.");

            // Non-existent member
            var (mOk, _) = svc.CreateBorrow(-9999, _librarianId,
                DateTime.Today.AddDays(7),
                new List<(int, int)> { (_bookId, 1) });
            Assert(!mOk, "Non-existent member must be rejected.");
        }

        // ── 07. RETURN ────────────────────────────────────────────────────────

        private static void Test_Return(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var svc = new BorrowService(ctx);

            var book = ctx.Books.Find(_bookId)!;
            ctx.Entry(book).Reload();
            int initial = book.AvailableCopies;

            // Setup: borrow 1 copy
            var (bOk, _) = svc.CreateBorrow(_memberId, _librarianId,
                DateTime.Today.AddDays(14),
                new List<(int, int)> { (_bookId, 1) });
            Assert(bOk, "Setup borrow for return test failed.");

            var borrow = ctx.Borrows
                .Where(b => b.MemberId == _memberId)
                .OrderByDescending(b => b.BorrowId)
                .First();

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initial - 1, "Inventory must decrease after borrow.");

            // Valid return
            var (rOk, rMsg, _) = svc.ProcessReturn(borrow.BorrowId);
            Assert(rOk, $"Valid return failed: {rMsg}");

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initial, "Inventory must be restored after return.");

            ctx.Entry(borrow).Reload();
            Assert(borrow.Status == BorrowStatus.Returned, "Borrow status must be 'Returned'.");
            Assert(borrow.ReturnDate.HasValue, "ReturnDate must be set.");

            // Double return rejected
            var (r2Ok, r2Msg, _) = svc.ProcessReturn(borrow.BorrowId);
            Assert(!r2Ok, "Second return of same borrow must be rejected.");
            Assert(r2Msg.Contains("already been returned", StringComparison.OrdinalIgnoreCase),
                $"Expected 'already been returned', got: '{r2Msg}'.");

            // Non-existent borrow rejected
            var (nOk, nMsg, _) = svc.ProcessReturn(-9999);
            Assert(!nOk, "Return of non-existent borrow must be rejected.");
            Assert(nMsg.Contains("not found", StringComparison.OrdinalIgnoreCase),
                $"Expected 'not found', got: '{nMsg}'.");

            // Cleanup
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == borrow.BorrowId));
            ctx.Borrows.Remove(borrow);
            ctx.SaveChanges();
        }

        // ── 08. FINE CALCULATION ──────────────────────────────────────────────

        private static void Test_FineCalculation()
        {
            // On-time return: no fine
            decimal zeroFine = FinePolicy.CalculateFine(DateTime.Today.AddDays(2), DateTime.Today);
            Assert(zeroFine == 0m, $"On-time return must have 0 fine, got {zeroFine}.");

            // 1-day overdue
            decimal oneDay = FinePolicy.CalculateFine(DateTime.Today.AddDays(-1), DateTime.Today);
            Assert(oneDay == FinePolicy.FinePerDay,
                $"1-day overdue must be {FinePolicy.FinePerDay} KHR, got {oneDay}.");

            // 5-day overdue
            decimal fiveDays = FinePolicy.CalculateFine(DateTime.Today.AddDays(-5), DateTime.Today);
            Assert(fiveDays == 5 * FinePolicy.FinePerDay,
                $"5-day overdue must be {5 * FinePolicy.FinePerDay} KHR, got {fiveDays}.");

            // FinePerDay must be a positive amount
            Assert(FinePolicy.FinePerDay > 0, "FinePerDay must be a positive amount.");
        }

        // ── 09. DELETE SAFETY ─────────────────────────────────────────────────

        private static void Test_DeleteSafety(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var borrowSvc = new BorrowService(ctx);

            // Create a borrow record so the book/member/librarian have history
            var (bOk, _) = borrowSvc.CreateBorrow(_memberId, _librarianId,
                DateTime.Today.AddDays(7),
                new List<(int, int)> { (_bookId, 1) });
            Assert(bOk, "Setup borrow for delete safety test failed.");

            var borrow = ctx.Borrows
                .Where(b => b.MemberId == _memberId)
                .OrderByDescending(b => b.BorrowId)
                .First();

            // Book with history cannot be deleted
            var bookSvc = new BookService(ctx);
            var (bdOk, bdMsg) = bookSvc.Delete(_bookId);
            Assert(!bdOk, "Book with borrow history must NOT be deletable.");
            Assert(bdMsg.Contains("borrow", StringComparison.OrdinalIgnoreCase),
                $"Expected borrow-history error, got: '{bdMsg}'.");

            // Member with history cannot be deleted
            var memberSvc = new MemberService(ctx);
            var (mdOk, mdMsg) = memberSvc.Delete(_memberId);
            Assert(!mdOk, "Member with borrow history must NOT be deletable.");
            Assert(mdMsg.Contains("borrow", StringComparison.OrdinalIgnoreCase),
                $"Expected borrow-history error, got: '{mdMsg}'.");

            // Librarian with borrow history cannot be deleted (preserves audit trail)
            SessionManager.Login(ctx.Librarians.Find(_adminId)!);
            var libSvc = new LibrarianService(ctx);
            var (ldOk, ldMsg) = libSvc.Delete(_librarianId);
            Assert(!ldOk, "Librarian with borrow history must NOT be deletable.");
            Assert(ldMsg.Contains("borrowing records", StringComparison.OrdinalIgnoreCase) ||
                   ldMsg.Contains("history", StringComparison.OrdinalIgnoreCase),
                $"Expected preservation error, got: '{ldMsg}'.");

            // Administrator account cannot be deleted (exactly one Admin invariant)
            var (adOk, adMsg) = libSvc.Delete(_adminId);
            Assert(!adOk, "Administrator account must NOT be deletable.");
            Assert(adMsg.Contains("Administrator", StringComparison.OrdinalIgnoreCase) ||
                   adMsg.Contains("one Admin", StringComparison.OrdinalIgnoreCase),
                $"Expected Admin preservation error, got: '{adMsg}'.");

            // Category referenced by a book cannot be deleted
            var catSvc = new CategoryService(ctx);
            var (cdOk, cdMsg) = catSvc.Delete(_categoryId);
            Assert(!cdOk, "Category referenced by books must NOT be deletable.");
            Assert(cdMsg.Contains("books are assigned", StringComparison.OrdinalIgnoreCase),
                $"Expected 'books are assigned', got: '{cdMsg}'.");

            // Cleanup borrow
            borrowSvc.ProcessReturn(borrow.BorrowId);
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == borrow.BorrowId));
            ctx.Borrows.Remove(borrow);
            ctx.SaveChanges();
        }

        // ── 10. ROLE RULES ────────────────────────────────────────────────────

        private static void Test_RoleRules(DbContextOptions<LibraryDbContext> options)
        {
            using var ctx = new LibraryDbContext(options);
            var admin = ctx.Librarians.Find(_adminId)!;
            var lib = ctx.Librarians.Find(_librarianId)!;

            SessionManager.Login(admin);
            var svc = new LibrarianService(ctx);

            // Verify Add() forces Librarian role regardless of caller input (no trust from UI)
            var firstLib = new Librarian
            {
                Name = "Role Force Test",
                Username = "role_force_check",
                Role = "Admin",  // Caller attempts Admin — must be silently overridden to Librarian
                Gender = "Male",
                DateOfBirth = new DateTime(1988, 1, 1),
                Phone = "012888000",
                Email = "role_force@test.lib"
            };
            var (rfOk, rfMsg) = svc.Add(firstLib, "Pass1234!");
            Assert(rfOk, $"Add with Admin role input must succeed (service overrides to Librarian): {rfMsg}");

            var forcedLib = ctx.Librarians.FirstOrDefault(l => l.Username == "role_force_check");
            Assert(forcedLib != null, "Force-test librarian not found in database.");
            Assert(forcedLib!.Role == "Librarian",
                $"Add() must force role to 'Librarian', got '{forcedLib.Role}'.");

            // Cleanup this intermediate test record via Admin delete (has no history)
            var (dfOk, _) = svc.Delete(forcedLib.LibrarianId);
            Assert(dfOk, "Admin must be able to delete librarian with no history.");

            // New librarian is created with Librarian role (role input ignored)
            var tempUsername = $"role_test_{Guid.NewGuid():N}".Substring(0, 20);
            var newLib = new Librarian
            {
                Name = "Role Test Librarian",
                Username = tempUsername,
                Gender = "Male",
                DateOfBirth = new DateTime(1992, 5, 10),
                Role = "Admin",  // Caller attempts Admin — must be overridden to Librarian
                Phone = "012000099",
                Email = $"{tempUsername}@test.lib"
            };
            var (nlOk, nlMsg) = svc.Add(newLib, "Pass1234!");
            Assert(nlOk, $"Admin must be able to add a new librarian: {nlMsg}");

            var created = ctx.Librarians.FirstOrDefault(l => l.Username == tempUsername);
            Assert(created != null, "Created librarian not found in database.");
            Assert(created!.Role == "Librarian",
                $"New librarian must be forced to Librarian role, got '{created.Role}'.");

            // Cannot promote Librarian to Admin
            var attemptPromo = new Librarian
            {
                LibrarianId = created.LibrarianId,
                Name = created.Name,
                Username = created.Username,
                Gender = created.Gender,
                DateOfBirth = created.DateOfBirth,
                Phone = created.Phone,
                Email = created.Email,
                Role = "Admin"  // Attempt to promote
            };
            var (promoOk, promoMsg) = svc.Update(attemptPromo, null);
            Assert(!promoOk, "System must prevent promoting Librarian to Admin.");
            Assert(promoMsg.Contains("promote", StringComparison.OrdinalIgnoreCase) ||
                   promoMsg.Contains("Administrator", StringComparison.OrdinalIgnoreCase),
                $"Expected promotion-blocked error, got: '{promoMsg}'.");

            // Cannot demote the only Admin
            var adminCopy = new Librarian
            {
                LibrarianId = admin.LibrarianId,
                Name = admin.Name,
                Username = admin.Username,
                Gender = admin.Gender,
                DateOfBirth = admin.DateOfBirth,
                Phone = admin.Phone,
                Email = admin.Email,
                Role = "Librarian"  // Attempt to demote
            };
            var (demoteOk, demoteMsg) = svc.Update(adminCopy, null);
            Assert(!demoteOk, "System must prevent demoting the only Admin.");

            // Librarian role cannot delete another librarian
            SessionManager.Login(lib);
            var (delLibOk, delLibMsg) = svc.Delete(created.LibrarianId);
            Assert(!delLibOk, "Librarian role must NOT be permitted to delete accounts.");
            Assert(delLibMsg.Contains("Administrator", StringComparison.OrdinalIgnoreCase) ||
                   delLibMsg.Contains("privileges", StringComparison.OrdinalIgnoreCase),
                $"Expected authorization error, got: '{delLibMsg}'.");

            // Admin MAY delete a librarian who has NO borrowing history
            SessionManager.Login(admin);
            var (delOk, delMsg) = svc.Delete(created.LibrarianId);
            Assert(delOk, $"Admin must be able to delete librarian with no history: {delMsg}");
            Assert(ctx.Librarians.Find(created.LibrarianId) == null,
                "Deleted librarian must no longer exist in database.");
        }

        // ── 11. CONCURRENCY — CONCURRENT BORROW OF LAST COPY ─────────────────

        private static void Test_ConcurrentBorrowLastCopy(DbContextOptions<LibraryDbContext> options)
        {
            // Create an isolated book with exactly 1 available copy
            int isolatedBookId;
            using (var setupCtx = new LibraryDbContext(options))
            {
                var isolatedBook = new Book
                {
                    Title = "Concurrency Test Book",
                    ISBN = "978-0-13-468599-1",
                    Year = 2023,
                    TotalCopies = 1,
                    AvailableCopies = 1,
                    CategoryId = _categoryId,
                    AuthorId = _authorId,
                    PublisherId = _publisherId
                };
                setupCtx.Books.Add(isolatedBook);
                setupCtx.SaveChanges();
                isolatedBookId = isolatedBook.BookId;
            }

            // Two threads compete to borrow the single copy simultaneously
            using var barrier = new Barrier(2);
            (bool success, string message) resultA = (false, "");
            (bool success, string message) resultB = (false, "");

            var taskA = Task.Run(() =>
            {
                using var ctxA = new LibraryDbContext(options);
                var svcA = new BorrowService(ctxA);
                barrier.SignalAndWait();
                resultA = svcA.CreateBorrow(_memberId, _librarianId, DateTime.Today.AddDays(7),
                    new List<(int, int)> { (isolatedBookId, 1) });
            });

            var taskB = Task.Run(() =>
            {
                using var ctxB = new LibraryDbContext(options);
                var svcB = new BorrowService(ctxB);
                barrier.SignalAndWait();
                resultB = svcB.CreateBorrow(_memberId, _librarianId, DateTime.Today.AddDays(7),
                    new List<(int, int)> { (isolatedBookId, 1) });
            });

            Task.WaitAll(taskA, taskB);

            // Exactly one transaction must succeed; the other must fail safely
            bool exactlyOne = (resultA.success && !resultB.success) || (!resultA.success && resultB.success);
            Assert(exactlyOne,
                $"Expected exactly one concurrent borrow to succeed. " +
                $"A=[{resultA.success}, '{resultA.message}'] B=[{resultB.success}, '{resultB.message}']");

            // Final state: AvailableCopies must be exactly 0
            using var verifyCtx = new LibraryDbContext(options);
            var freshBook = verifyCtx.Books.Find(isolatedBookId)!;
            Assert(freshBook.AvailableCopies == 0,
                $"AvailableCopies must be 0 after concurrent borrow, got {freshBook.AvailableCopies}.");
            Assert(freshBook.AvailableCopies >= 0,
                "AvailableCopies must never be negative.");

            // Cleanup
            var successBorrowId = verifyCtx.BorrowDetails
                .Where(bd => bd.BookId == isolatedBookId)
                .Select(bd => bd.BorrowId)
                .FirstOrDefault();

            if (successBorrowId > 0)
            {
                new BorrowService(verifyCtx).ProcessReturn(successBorrowId);
                verifyCtx.BorrowDetails.RemoveRange(verifyCtx.BorrowDetails.Where(bd => bd.BookId == isolatedBookId));
                verifyCtx.Borrows.RemoveRange(verifyCtx.Borrows.Where(b => b.BorrowId == successBorrowId));
            }
            verifyCtx.Books.Remove(freshBook);
            verifyCtx.SaveChanges();
        }

        // ── 12. CONCURRENCY — CONCURRENT DOUBLE RETURN ───────────────────────

        private static void Test_ConcurrentDoubleReturn(DbContextOptions<LibraryDbContext> options)
        {
            // Setup: Create an isolated book with 1 copy and borrow it
            int isolatedBookId;
            int isolatedBorrowId;
            using (var setupCtx = new LibraryDbContext(options))
            {
                var book = new Book
                {
                    Title = "Double Return Concurrency Book",
                    ISBN = "978-0-201-63361-0",
                    Year = 2022,
                    TotalCopies = 1,
                    AvailableCopies = 1,
                    CategoryId = _categoryId,
                    AuthorId = _authorId,
                    PublisherId = _publisherId
                };
                setupCtx.Books.Add(book);
                setupCtx.SaveChanges();
                isolatedBookId = book.BookId;

                var borrowSvc = new BorrowService(setupCtx);
                var (bOk, _) = borrowSvc.CreateBorrow(_memberId, _librarianId, DateTime.Today.AddDays(7),
                    new List<(int, int)> { (isolatedBookId, 1) });
                Assert(bOk, "Setup borrow for double return concurrency test failed.");

                isolatedBorrowId = setupCtx.BorrowDetails
                    .Where(bd => bd.BookId == isolatedBookId)
                    .Select(bd => bd.BorrowId)
                    .First();
            }

            // Two threads attempt to return the same borrow concurrently
            using var barrier = new Barrier(2);
            (bool success, string message, decimal fine) resultA = (false, "", 0m);
            (bool success, string message, decimal fine) resultB = (false, "", 0m);

            var taskA = Task.Run(() =>
            {
                using var ctxA = new LibraryDbContext(options);
                var svcA = new BorrowService(ctxA);
                barrier.SignalAndWait();
                resultA = svcA.ProcessReturn(isolatedBorrowId);
            });

            var taskB = Task.Run(() =>
            {
                using var ctxB = new LibraryDbContext(options);
                var svcB = new BorrowService(ctxB);
                barrier.SignalAndWait();
                resultB = svcB.ProcessReturn(isolatedBorrowId);
            });

            Task.WaitAll(taskA, taskB);

            // Exactly one return must succeed; the other must fail safely
            bool exactlyOne = (resultA.success && !resultB.success) || (!resultA.success && resultB.success);
            Assert(exactlyOne,
                $"Expected exactly one concurrent return to succeed. " +
                $"A=[{resultA.success}, '{resultA.message}'] B=[{resultB.success}, '{resultB.message}']");

            // Final state verification: Inventory must be restored exactly once (never exceeds TotalCopies)
            using (var verifyCtx = new LibraryDbContext(options))
            {
                var book = verifyCtx.Books.Find(isolatedBookId)!;
                Assert(book.AvailableCopies == 1,
                    $"AvailableCopies must be restored to 1, got {book.AvailableCopies}.");
                Assert(book.AvailableCopies <= book.TotalCopies,
                    $"AvailableCopies ({book.AvailableCopies}) must not exceed TotalCopies ({book.TotalCopies}).");

                var borrow = verifyCtx.Borrows.Find(isolatedBorrowId)!;
                Assert(borrow.Status == BorrowStatus.Returned,
                    $"Borrow status must be Returned, got '{borrow.Status}'.");

                // Cleanup
                verifyCtx.BorrowDetails.RemoveRange(verifyCtx.BorrowDetails.Where(bd => bd.BorrowId == isolatedBorrowId));
                verifyCtx.Borrows.Remove(borrow);
                verifyCtx.Books.Remove(book);
                verifyCtx.SaveChanges();
            }
        }
    }
}
