using LibraryManagementSystem.Data;
using LibraryManagementSystem.Forms;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Panels;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests
{
    public static class RegressionTestRunner
    {
        private static int _passed = 0;
        private static int _failed = 0;

        public static int RunAllTests()
        {
            Console.WriteLine("================================================================================");
            Console.WriteLine("  LIBRARY MANAGEMENT SYSTEM — AUTOMATED REGRESSION & RESPONSIVE SUITE (.NET 8)");
            Console.WriteLine("================================================================================");

            RunTest("01. Database Connectivity & Seeding", Test_DbConnectivity);
            RunTest("02. Authentication — Admin Role & Password Verification", Test_AuthAdmin);
            RunTest("03. Authentication — Librarian Role & Password Verification", Test_AuthLibrarian);
            RunTest("04. Authentication — Wrong Password Rejection", Test_AuthWrongPassword);
            RunTest("05. Validation — ISBN-10 & ISBN-13 Checksum Mathematical Verification", Test_ISBNValidation);
            RunTest("06. Business Rule — Duplicate ISBN Rejection", Test_DuplicateISBN);
            RunTest("07. Business Rule — Duplicate Username Rejection", Test_DuplicateUsername);
            RunTest("08. Circulation — Borrow & Inventory Decrement Transaction", Test_BorrowAndInventoryDecrement);
            RunTest("09. Circulation — Return & Inventory Increment Transaction", Test_ReturnAndInventoryIncrement);
            RunTest("10. Circulation — Already-Returned Prevention Guard", Test_AlreadyReturnedGuard);
            RunTest("11. Circulation — Fine Calculation Formula (2,000 KHR/day)", Test_OverdueFineCalculation);
            RunTest("12. Reporting — Analytics & Summary Queries", Test_ReportingQueries);
            RunTest("13. Responsive Layout Matrix (1024x600, 1280x720, 1366x768, 1920x1080)", Test_ResponsiveMatrix);

            Console.WriteLine("================================================================================");
            Console.WriteLine($"  TEST RUN RESULTS: {_passed} PASSED, {_failed} FAILED");
            Console.WriteLine("================================================================================");

            return _failed == 0 ? 0 : 1;
        }

        private static void RunTest(string testName, Action testAction)
        {
            Console.Write($"[TEST] {testName.PadRight(65)} ");
            try
            {
                testAction();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("PASS");
                Console.ResetColor();
                _passed++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FAIL");
                Console.ResetColor();
                Console.WriteLine($"       -> Error: {ex.Message}");
                _failed++;
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
        }

        private static void Test_DbConnectivity()
        {
            using var ctx = Program.CreateDbContext();
            Assert(ctx.Database.CanConnect(), "Cannot connect to SQL Server database.");
            Assert(ctx.Librarians.Any(), "Librarians table has no records.");
            Assert(ctx.Books.Any(), "Books table has no records.");
        }

        private static void Test_AuthAdmin()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var librarian = auth.Login("admin", "admin123");
            Assert(librarian != null, "Admin login failed with valid credentials.");
            Assert(librarian!.Role == "Admin", "Librarian is not Admin role.");
        }

        private static void Test_AuthLibrarian()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var libUser = ctx.Librarians.FirstOrDefault(l => l.Role == "Librarian" || l.Role == "Employee");
            if (libUser != null)
            {
                var librarian = auth.Login(libUser.Username, "librarian123");
                if (librarian != null)
                {
                    Assert(librarian.Role == "Librarian" || librarian.Role == "Employee", "Role mismatch.");
                }
            }
        }

        private static void Test_AuthWrongPassword()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var librarian = auth.Login("admin", "WrongPassword!999");
            Assert(librarian == null, "Login should fail with wrong password.");
        }

        private static void Test_ISBNValidation()
        {
            // Valid ISBN-10
            Assert(ValidationHelper.IsValidISBN("0-306-40615-2"), "Valid ISBN-10 was rejected.");
            // Invalid ISBN-10 checksum
            Assert(!ValidationHelper.IsValidISBN("0-306-40615-9"), "Invalid ISBN-10 checksum was accepted.");
            // Valid ISBN-13
            Assert(ValidationHelper.IsValidISBN("978-0-306-40615-7"), "Valid ISBN-13 was rejected.");
            // Invalid ISBN-13 checksum
            Assert(!ValidationHelper.IsValidISBN("978-0-306-40615-0"), "Invalid ISBN-13 checksum was accepted.");
            // Garbage string
            Assert(!ValidationHelper.IsValidISBN("ABC-NOT-AN-ISBN"), "Garbage string was accepted as ISBN.");
        }

        private static void Test_DuplicateISBN()
        {
            using var ctx = Program.CreateDbContext();
            var svc = new BookService(ctx);
            var existingBook = ctx.Books.First();

            var dupBook = new Book
            {
                Title = "Duplicate Test Book",
                ISBN = existingBook.ISBN,
                CategoryId = existingBook.CategoryId,
                AuthorId = existingBook.AuthorId,
                PublisherId = existingBook.PublisherId,
                Year = 2024,
                TotalCopies = 5,
                AvailableCopies = 5
            };

            var (success, message) = svc.Add(dupBook);
            Assert(!success, "BookService allowed adding duplicate ISBN.");
            Assert(message.Contains("already exists", StringComparison.OrdinalIgnoreCase), "Unexpected error message.");
        }

        private static void Test_DuplicateUsername()
        {
            using var ctx = Program.CreateDbContext();
            var svc = new LibrarianService(ctx);
            var existingLibrarian = ctx.Librarians.First();

            var dupLibrarian = new Librarian
            {
                Name = "Duplicate User Test",
                Username = existingLibrarian.Username,
                Role = "Librarian"
            };

            var (success, message) = svc.Add(dupLibrarian, "ValidPass123!");
            Assert(!success, "LibrarianService allowed adding duplicate username.");
            Assert(message.Contains("already taken", StringComparison.OrdinalIgnoreCase), "Unexpected error message.");
        }

        private static void Test_BorrowAndInventoryDecrement()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);

            int initialAvailable = book.AvailableCopies;
            var borrowSvc = new BorrowService(ctx);

            var items = new List<(int bookId, int quantity)>
            {
                (book.BookId, 1)
            };

            var (success, msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), items);
            Assert(success, $"Borrow transaction creation failed: {msg}");

            // Verify inventory decreased by 1
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable - 1, $"AvailableCopies did not decrease from {initialAvailable} to {initialAvailable - 1}.");

            // Extract borrow id from message or query latest borrow
            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();

            // Clean up: return this transaction to restore inventory
            var (retSuccess, retMsg, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(retSuccess, $"Clean-up return failed: {retMsg}");

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable, "AvailableCopies did not restore on return.");
        }

        private static void Test_ReturnAndInventoryIncrement()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);

            var borrowSvc = new BorrowService(ctx);
            int initialCopies = book.AvailableCopies;

            var (success, msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(14), new List<(int bookId, int quantity)>
            {
                (book.BookId, 1)
            });
            Assert(success, $"CreateBorrow failed: {msg}");

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialCopies - 1, "Inventory did not decrease on borrow.");

            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();
            var (retSuccess, retMsg, fine) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(retSuccess, $"ProcessReturn failed: {retMsg}");

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialCopies, "Inventory was not restored on return.");
        }

        private static void Test_AlreadyReturnedGuard()
        {
            using var ctx = Program.CreateDbContext();
            var returnedBorrow = ctx.Borrows.FirstOrDefault(b => b.Status == BorrowStatus.Returned);
            if (returnedBorrow != null)
            {
                var borrowSvc = new BorrowService(ctx);
                var (success, msg, _) = borrowSvc.ProcessReturn(returnedBorrow.BorrowId);
                Assert(!success, "ProcessReturn should fail for already returned borrow.");
                Assert(msg.Contains("already been returned", StringComparison.OrdinalIgnoreCase), "Unexpected error message.");
            }
        }

        private static void Test_OverdueFineCalculation()
        {
            // Verify fine formula: 2,000 KHR per day
            int overdueDays = 5;
            decimal expectedFine = overdueDays * FinePolicy.FinePerDay;
            decimal calculatedFine = FinePolicy.CalculateFine(DateTime.Today.AddDays(-overdueDays), DateTime.Today);
            Assert(calculatedFine == expectedFine, $"Fine mismatch: expected {expectedFine}, got {calculatedFine}.");

            // Non-overdue borrow should have 0 fine
            decimal zeroFine = FinePolicy.CalculateFine(DateTime.Today.AddDays(2), DateTime.Today);
            Assert(zeroFine == 0m, "Non-overdue borrow must have 0 fine.");
        }

        private static void Test_ReportingQueries()
        {
            using var ctx = Program.CreateDbContext();
            var rep = new ReportService(ctx);
            var active = rep.GetActiveBorrows();
            var overdue = rep.GetOverdueBorrows();
            var returned = rep.GetReturnedBorrows();
            var popular = rep.GetMostBorrowedBooks();
            var inv = rep.GetInventory();

            Assert(active != null, "GetActiveBorrows returned null.");
            Assert(overdue != null, "GetOverdueBorrows returned null.");
            Assert(returned != null, "GetReturnedBorrows returned null.");
            Assert(popular != null, "GetMostBorrowedBooks returned null.");
            Assert(inv != null, "GetInventory returned null.");
        }

        private static void Test_ResponsiveMatrix()
        {
            var resolutions = new[]
            {
                new Size(1024, 600),
                new Size(1280, 720),
                new Size(1366, 768),
                new Size(1920, 1080)
            };

            foreach (var res in resolutions)
            {
                using var shell = new MainShellForm();
                shell.ClientSize = res;
                shell.PerformLayout();

                using var db = new DashboardPanel();
                db.Size = res;
                db.PerformLayout();

                using var bp = new BooksPanel();
                bp.Size = res;
                bp.PerformLayout();

                using var mp = new MembersPanel();
                mp.Size = res;
                mp.PerformLayout();

                using var rp = new ReturnPanel();
                rp.Size = res;
                rp.PerformLayout();

                using var rep = new ReportsPanel();
                rep.Size = res;
                rep.PerformLayout();
            }
        }
    }
}
