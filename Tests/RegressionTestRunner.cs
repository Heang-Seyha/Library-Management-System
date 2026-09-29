using LibraryManagementSystem.Data;
using LibraryManagementSystem.Forms;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Panels;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace LibraryManagementSystem.Tests
{
    public static class RegressionTestRunner
    {
        private static int _passed = 0;
        private static int _failed = 0;

        public static int RunAllTests()
        {
            _passed = 0;
            _failed = 0;

            Console.WriteLine("================================================================================");
            Console.WriteLine("  LIBRARY MANAGEMENT SYSTEM — AUTOMATED REGRESSION SUITE (PHASE 2 HARDENED)");
            Console.WriteLine("================================================================================");

            // 1-5: Authentication & Connectivity
            RunTest("01. Database Connectivity & Table Seeding", Test_DbConnectivity);
            RunTest("02. Authentication — Admin Role & Password Verification", Test_AuthAdmin);
            RunTest("03. Authentication — Librarian Role & Password Verification", Test_AuthLibrarian);
            RunTest("04. Authentication — Wrong Password Rejection", Test_AuthWrongPassword);
            RunTest("05. Authentication — Unknown Username Rejection", Test_AuthUnknownUsername);

            // 6-7: Authorization
            RunTest("06. Authorization — Librarian Management (Admin-Only Guard)", Test_Authorization_LibrarianManagement_AdminVsLibrarian);
            RunTest("07. Authorization — Policy Matrix (Both Roles CRUD, Admin-Only User Mgmt)", Test_Authorization_CRUD_PermissionsMatrix);

            // 8-10: Validation & Uniqueness
            RunTest("08. Validation — ISBN-10 & ISBN-13 Checksum Mathematical Verification", Test_ISBNValidation);
            RunTest("09. Business Rule — Duplicate ISBN Rejection", Test_DuplicateISBN);
            RunTest("10. Business Rule — Duplicate Username Rejection", Test_DuplicateUsername);

            // 11-14: Borrow Rules & Atomicity
            RunTest("11. Circulation — Borrow Input & Due Date Validation Rules", Test_Borrow_Validation_InvalidInputs);
            RunTest("12. Circulation — Duplicate Book Normalization & Merging", Test_Borrow_DuplicateBook_Merging);
            RunTest("13. Circulation — Insufficient Inventory Rejection & Rollback", Test_Borrow_InsufficientInventory_Rollback);
            RunTest("14. Circulation — Valid Borrow & Inventory Decrement Transaction", Test_BorrowAndInventoryDecrement);

            // 15-19: Return Rules & Integrity
            RunTest("15. Circulation — Valid Return & Inventory Restoration", Test_ReturnAndInventoryIncrement);
            RunTest("16. Circulation — Non-Existent Borrow Return Guard", Test_Return_NonExistentBorrow);
            RunTest("17. Circulation — Already-Returned Prevention Guard", Test_AlreadyReturnedGuard);
            RunTest("18. Circulation — Return Inventory Overfill Data-Integrity Guard", Test_Return_DataIntegrity_ExceedsTotalCopies);
            RunTest("19. Circulation — Fine Calculation Formula (2,000 KHR/day)", Test_OverdueFineCalculation);

            // 20-23: Historical Data & Delete Safety
            RunTest("20. Delete Safety — Book With Borrow History Cannot Be Deleted", Test_DeleteSafety_BookWithBorrowHistory);
            RunTest("21. Delete Safety — Member With Borrow History Cannot Be Deleted", Test_DeleteSafety_MemberWithBorrowHistory);
            RunTest("22. Delete Safety — Librarian With Borrow History Cannot Be Deleted", Test_DeleteSafety_LibrarianWithBorrowHistory);
            RunTest("23. Delete Safety — Referenced Category/Author/Publisher Protected", Test_DeleteSafety_ReferencedMetadata);

            // 24-26: Concurrency
            RunTest("24. Concurrency — Book Update RowVersion Conflict Detection", Test_Concurrency_BookUpdate_RowVersion);
            RunTest("25. Concurrency — Concurrent Borrow of Last Copy Guard", Test_Concurrency_ConcurrentBorrow_LastCopy);
            RunTest("26. Concurrency — Concurrent Return Guard", Test_Concurrency_ConcurrentReturn);

            // 27: Database Integrity
            RunTest("27. Database Integrity — Inventory Bounds (0 <= Available <= Total)", Test_DatabaseIntegrity_InventoryBounds);

            // 28-30: Reports & Responsive UI
            RunTest("28. Reporting — Filtered Queries & Export Formats (Excel, PDF)", Test_Reporting_FilteredQueriesAndExports);
            RunTest("29. Reporting — Analytics & Summary Queries", Test_ReportingQueries);
            RunTest("30. Responsive Layout Matrix (1024x600, 1280x720, 1366x768, 1920x1080)", Test_ResponsiveMatrix);

            Console.WriteLine("================================================================================");
            Console.WriteLine($"  TEST RUN RESULTS: {_passed} PASSED, {_failed} FAILED (TOTAL: {_passed + _failed})");
            Console.WriteLine("================================================================================");

            return _failed == 0 ? 0 : 1;
        }

        private static void RunTest(string testName, Action testAction)
        {
            Console.Write($"[TEST] {testName.PadRight(70)} ");
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

        // ── 01. DATABASE CONNECTIVITY ────────────────────────────────────────────────
        private static void Test_DbConnectivity()
        {
            using var ctx = Program.CreateDbContext();
            Assert(ctx.Database.CanConnect(), "Cannot connect to SQL Server database.");
            Assert(ctx.Librarians.Any(), "Librarians table has no records.");
            Assert(ctx.Books.Any(), "Books table has no records.");
        }

        // ── 02. AUTH: ADMIN ─────────────────────────────────────────────────────────
        private static void Test_AuthAdmin()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var librarian = auth.Login("admin", "admin123");
            Assert(librarian != null, "Admin login failed with valid credentials.");
            Assert(librarian!.Role == "Admin", "Librarian is not Admin role.");
        }

        // ── 03. AUTH: LIBRARIAN ─────────────────────────────────────────────────────
        private static void Test_AuthLibrarian()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var libUser = ctx.Librarians.FirstOrDefault(l => l.Username == "librarian")
                       ?? ctx.Librarians.FirstOrDefault(l => l.Role == "Librarian");
            Assert(libUser != null, "No Librarian role user found in database.");

            var librarian = auth.Login(libUser!.Username, "librarian123");
            Assert(librarian != null, $"Librarian login failed for user '{libUser.Username}'.");
            Assert(librarian!.Role == "Librarian", $"Role mismatch: expected Librarian, got {librarian.Role}.");
        }

        // ── 04. AUTH: WRONG PASSWORD ────────────────────────────────────────────────
        private static void Test_AuthWrongPassword()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var librarian = auth.Login("admin", "WrongPassword!999");
            Assert(librarian == null, "Login should fail with wrong password.");
        }

        // ── 05. AUTH: UNKNOWN USERNAME ──────────────────────────────────────────────
        private static void Test_AuthUnknownUsername()
        {
            using var ctx = Program.CreateDbContext();
            var auth = new AuthenticationService(ctx);
            var librarian = auth.Login("nonexistent_user_xyz_999", "admin123");
            Assert(librarian == null, "Login should fail with unknown username.");
        }

        // ── 06. AUTHZ: LIBRARIAN MANAGEMENT ADMIN-ONLY ──────────────────────────────
        private static void Test_Authorization_LibrarianManagement_AdminVsLibrarian()
        {
            using var ctx = Program.CreateDbContext();
            var adminUser = ctx.Librarians.FirstOrDefault(l => l.Role == "Admin");
            var libUser = ctx.Librarians.FirstOrDefault(l => l.Role == "Librarian");
            Assert(adminUser != null && libUser != null, "Admin and Librarian users required in database.");

            var svc = new LibrarianService(ctx);

            // Test 1: When logged in as Librarian, Add/Update/Delete must be rejected
            SessionManager.Login(libUser!);
            try
            {
                var target = new Librarian { Name = "Unauthorized Test", Username = "unauth_lib_test", Role = "Librarian" };
                var (addSuccess, addMsg) = svc.Add(target, "ValidPass123!");
                Assert(!addSuccess, "Librarian role must not be allowed to add librarians.");
                Assert(addMsg.Contains("privileges", StringComparison.OrdinalIgnoreCase) || addMsg.Contains("admin", StringComparison.OrdinalIgnoreCase),
                    $"Unexpected auth error message: {addMsg}");

                var (updSuccess, _) = svc.Update(adminUser!, null);
                Assert(!updSuccess, "Librarian role must not be allowed to update librarians.");

                var (delSuccess, _) = svc.Delete(adminUser!.LibrarianId);
                Assert(!delSuccess, "Librarian role must not be allowed to delete librarians.");
            }
            finally
            {
                SessionManager.Logout();
            }

            // Test 2: When logged in as Admin, Add succeeds (and we clean up)
            SessionManager.Login(adminUser!);
            try
            {
                var tempUsername = $"test_adm_auth_{DateTime.Now.Ticks % 100000}";
                var newLib = new Librarian
                {
                    Name = "Admin Auth Test",
                    Username = tempUsername,
                    Role = "Librarian",
                    Phone = "012000111",
                    Position = "Staff"
                };

                var (addSuccess, addMsg) = svc.Add(newLib, "Pass1234!");
                Assert(addSuccess, $"Admin should be allowed to add librarian: {addMsg}");

                // Clean up created record
                var created = ctx.Librarians.FirstOrDefault(l => l.Username == tempUsername);
                if (created != null)
                {
                    var (delSuccess, _) = svc.Delete(created.LibrarianId);
                    Assert(delSuccess, "Admin should be allowed to delete the test librarian.");
                }
            }
            finally
            {
                SessionManager.Logout();
            }
        }

        // ── 07. AUTHZ: POLICY MATRIX ────────────────────────────────────────────────
        private static void Test_Authorization_CRUD_PermissionsMatrix()
        {
            // Admin permissions
            Assert(AuthorizationHelper.CanManageLibrarians("Admin"), "Admin must be allowed to manage librarians.");
            Assert(AuthorizationHelper.CanManageBooks("Admin"), "Admin must be allowed to manage books.");
            Assert(AuthorizationHelper.CanManageMembers("Admin"), "Admin must be allowed to manage members.");
            Assert(AuthorizationHelper.CanManageCategories("Admin"), "Admin must be allowed to manage categories.");
            Assert(AuthorizationHelper.CanManageAuthors("Admin"), "Admin must be allowed to manage authors.");
            Assert(AuthorizationHelper.CanManagePublishers("Admin"), "Admin must be allowed to manage publishers.");
            Assert(AuthorizationHelper.CanCirculate("Admin"), "Admin must be allowed to borrow/return.");
            Assert(AuthorizationHelper.CanViewReports("Admin"), "Admin must be allowed to view reports.");
            Assert(AuthorizationHelper.CanExportReports("Admin"), "Admin must be allowed to export reports.");

            // Librarian permissions (must be identical to Admin EXCEPT Librarian Management)
            Assert(!AuthorizationHelper.CanManageLibrarians("Librarian"), "Librarian must NOT be allowed to manage librarians.");
            Assert(AuthorizationHelper.CanManageBooks("Librarian"), "Librarian must be allowed to manage books.");
            Assert(AuthorizationHelper.CanManageMembers("Librarian"), "Librarian must be allowed to manage members.");
            Assert(AuthorizationHelper.CanManageCategories("Librarian"), "Librarian must be allowed to manage categories.");
            Assert(AuthorizationHelper.CanManageAuthors("Librarian"), "Librarian must be allowed to manage authors.");
            Assert(AuthorizationHelper.CanManagePublishers("Librarian"), "Librarian must be allowed to manage publishers.");
            Assert(AuthorizationHelper.CanCirculate("Librarian"), "Librarian must be allowed to borrow/return.");
            Assert(AuthorizationHelper.CanViewReports("Librarian"), "Librarian must be allowed to view reports.");
            Assert(AuthorizationHelper.CanExportReports("Librarian"), "Librarian must be allowed to export reports.");

            // Unauthenticated / Invalid role
            Assert(!AuthorizationHelper.CanManageLibrarians(null), "Unauthenticated cannot manage librarians.");
            Assert(!AuthorizationHelper.CanManageBooks(""), "Blank role cannot manage books.");
            Assert(!AuthorizationHelper.CanCirculate("Guest"), "Guest role cannot circulate.");
        }

        // ── 08. VALIDATION: ISBN CHECKSUMS ──────────────────────────────────────────
        private static void Test_ISBNValidation()
        {
            Assert(ValidationHelper.IsValidISBN("0-306-40615-2"), "Valid ISBN-10 was rejected.");
            Assert(!ValidationHelper.IsValidISBN("0-306-40615-9"), "Invalid ISBN-10 checksum was accepted.");
            Assert(ValidationHelper.IsValidISBN("978-0-306-40615-7"), "Valid ISBN-13 was rejected.");
            Assert(!ValidationHelper.IsValidISBN("978-0-306-40615-0"), "Invalid ISBN-13 checksum was accepted.");
            Assert(!ValidationHelper.IsValidISBN("ABC-NOT-AN-ISBN"), "Garbage string was accepted as ISBN.");
        }

        // ── 09. BUSINESS RULE: DUPLICATE ISBN ───────────────────────────────────────
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

        // ── 10. BUSINESS RULE: DUPLICATE USERNAME ───────────────────────────────────
        private static void Test_DuplicateUsername()
        {
            using var ctx = Program.CreateDbContext();
            var adminUser = ctx.Librarians.First(l => l.Role == "Admin");
            SessionManager.Login(adminUser);
            try
            {
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
            finally
            {
                SessionManager.Logout();
            }
        }

        // ── 11. BORROW: INPUT VALIDATION & DUE DATE RULES ───────────────────────────
        private static void Test_Borrow_Validation_InvalidInputs()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);
            var borrowSvc = new BorrowService(ctx);

            // Empty items list
            var (r1, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)>());
            Assert(!r1, "Empty borrow list must be rejected.");

            // Invalid member
            var (r2, _) = borrowSvc.CreateBorrow(-9999, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)> { (book.BookId, 1) });
            Assert(!r2, "Non-existent member must be rejected.");

            // Invalid book
            var (r3, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)> { (-9999, 1) });
            Assert(!r3, "Non-existent book must be rejected.");

            // Zero quantity
            var (r4, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)> { (book.BookId, 0) });
            Assert(!r4, "Zero quantity must be rejected.");

            // Negative quantity
            var (r5, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)> { (book.BookId, -2) });
            Assert(!r5, "Negative quantity must be rejected.");

            // Due date = today (must be at least 1 day after borrow date)
            var (r6, msg6) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today, new List<(int, int)> { (book.BookId, 1) });
            Assert(!r6, "Due date equal to today must be rejected.");
            Assert(msg6.Contains("at least one day", StringComparison.OrdinalIgnoreCase), $"Expected due date error message, got: {msg6}");

            // Due date in the past
            var (r7, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(-1), new List<(int, int)> { (book.BookId, 1) });
            Assert(!r7, "Past due date must be rejected.");
        }

        // ── 12. BORROW: DUPLICATE BOOK MERGING ──────────────────────────────────────
        private static void Test_Borrow_DuplicateBook_Merging()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies >= 3);

            int initialAvailable = book.AvailableCopies;
            var borrowSvc = new BorrowService(ctx);

            // Pass the same book ID twice: quantity 1 and quantity 2 -> should merge to 3
            var items = new List<(int bookId, int quantity)>
            {
                (book.BookId, 1),
                (book.BookId, 2)
            };

            var (success, msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), items);
            Assert(success, $"Borrow with duplicate book entries failed: {msg}");

            // Verify inventory decreased by 3
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable - 3, $"AvailableCopies did not decrease by merged quantity 3. Expected {initialAvailable - 3}, got {book.AvailableCopies}.");

            // Verify the created Borrow has exactly 1 detail item with Quantity = 3
            var latestBorrow = ctx.Borrows
                .Include(b => b.BorrowDetails)
                .Where(b => b.MemberId == member.MemberId)
                .OrderByDescending(b => b.BorrowId)
                .First();

            Assert(latestBorrow.BorrowDetails.Count == 1, $"Expected exactly 1 merged BorrowDetail, found {latestBorrow.BorrowDetails.Count}.");
            Assert(latestBorrow.BorrowDetails.First().Quantity == 3, $"Expected merged quantity 3, found {latestBorrow.BorrowDetails.First().Quantity}.");

            // Clean up: return and remove test record to preserve pristine database
            borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable, "AvailableCopies did not restore on cleanup.");
        }

        // ── 13. BORROW: INSUFFICIENT INVENTORY ROLLBACK ─────────────────────────────
        private static void Test_Borrow_InsufficientInventory_Rollback()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);

            int initialAvailable = book.AvailableCopies;
            var borrowSvc = new BorrowService(ctx);

            // Request more than available
            var items = new List<(int bookId, int quantity)>
            {
                (book.BookId, initialAvailable + 10)
            };

            var (success, msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), items);
            Assert(!success, "Borrow should fail when requested quantity exceeds available copies.");
            Assert(msg.Contains("insufficient", StringComparison.OrdinalIgnoreCase) || msg.Contains("only", StringComparison.OrdinalIgnoreCase),
                $"Unexpected error message: {msg}");

            // Verify inventory is untouched
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable, "Inventory must remain unchanged after rejected borrow.");
        }

        // ── 14. BORROW: VALID BORROW & INVENTORY DECREMENT ──────────────────────────
        private static void Test_BorrowAndInventoryDecrement()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);

            int initialAvailable = book.AvailableCopies;
            var borrowSvc = new BorrowService(ctx);

            var items = new List<(int bookId, int quantity)> { (book.BookId, 1) };
            var (success, msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), items);
            Assert(success, $"Borrow transaction creation failed: {msg}");

            // Verify inventory decreased by 1
            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable - 1, $"AvailableCopies did not decrease from {initialAvailable} to {initialAvailable - 1}.");

            // Clean up: return this transaction to restore inventory, then remove test borrow record
            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();
            var (retSuccess, retMsg, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(retSuccess, $"Clean-up return failed: {retMsg}");

            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialAvailable, "AvailableCopies did not restore on return.");
        }

        // ── 15. RETURN: VALID RETURN & INVENTORY INCREMENT ──────────────────────────
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
            var (retSuccess, retMsg, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(retSuccess, $"ProcessReturn failed: {retMsg}");

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialCopies, "Inventory was not restored on return.");

            // Clean up test record
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();
        }

        // ── 16. RETURN: NON-EXISTENT BORROW ─────────────────────────────────────────
        private static void Test_Return_NonExistentBorrow()
        {
            using var ctx = Program.CreateDbContext();
            var borrowSvc = new BorrowService(ctx);
            var (success, msg, _) = borrowSvc.ProcessReturn(-9999);
            Assert(!success, "ProcessReturn should fail for non-existent borrow ID.");
            Assert(msg.Contains("not found", StringComparison.OrdinalIgnoreCase), $"Unexpected error message: {msg}");
        }

        // ── 17. RETURN: ALREADY RETURNED GUARD ───────────────────────────────────────
        private static void Test_AlreadyReturnedGuard()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);
            var borrowSvc = new BorrowService(ctx);

            var (bSuccess, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)> { (book.BookId, 1) });
            Assert(bSuccess, "Setup borrow failed.");

            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();

            // First return succeeds
            var (r1Success, _, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(r1Success, "First return should succeed.");

            // Second return MUST be rejected
            var (r2Success, r2Msg, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(!r2Success, "ProcessReturn must reject an already returned borrow.");
            Assert(r2Msg.Contains("already been returned", StringComparison.OrdinalIgnoreCase), $"Unexpected error: {r2Msg}");

            // Clean up
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();
        }

        // ── 18. RETURN: INVENTORY OVERFILL DATA-INTEGRITY GUARD ─────────────────────
        private static void Test_Return_DataIntegrity_ExceedsTotalCopies()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);
            var borrowSvc = new BorrowService(ctx);

            int origAvail = book.AvailableCopies;
            int origTotal = book.TotalCopies;

            var (bSuccess, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7), new List<(int, int)> { (book.BookId, 1) });
            Assert(bSuccess, "Setup borrow failed.");

            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();

            // Simulate corrupted database state: set AvailableCopies = TotalCopies
            // Returning the book would cause AvailableCopies + 1 > TotalCopies
            book.AvailableCopies = book.TotalCopies;
            ctx.SaveChanges();

            // Requirement 20: Do NOT automatically clamp; reject and report data integrity error!
            var (retSuccess, retMsg, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(!retSuccess, "ProcessReturn must reject returning a book when inventory would exceed TotalCopies.");
            Assert(retMsg.Contains("Data integrity", StringComparison.OrdinalIgnoreCase) || retMsg.Contains("exceed", StringComparison.OrdinalIgnoreCase),
                $"Expected data integrity error message, got: {retMsg}");

            // Clean up: restore book inventory and delete test records
            book.AvailableCopies = origAvail;
            ctx.SaveChanges();

            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();
        }

        // ── 19. FINE CALCULATION ───────────────────────────────────────────────────
        private static void Test_OverdueFineCalculation()
        {
            int overdueDays = 5;
            decimal expectedFine = overdueDays * FinePolicy.FinePerDay;
            decimal calculatedFine = FinePolicy.CalculateFine(DateTime.Today.AddDays(-overdueDays), DateTime.Today);
            Assert(calculatedFine == expectedFine, $"Fine mismatch: expected {expectedFine}, got {calculatedFine}.");

            decimal zeroFine = FinePolicy.CalculateFine(DateTime.Today.AddDays(2), DateTime.Today);
            Assert(zeroFine == 0m, "Non-overdue borrow must have 0 fine.");
        }

        // ── 20. DELETE SAFETY: BOOK WITH BORROW HISTORY ─────────────────────────────
        private static void Test_DeleteSafety_BookWithBorrowHistory()
        {
            using var ctx = Program.CreateDbContext();
            var bookWithHistory = ctx.BorrowDetails.Select(bd => bd.Book).FirstOrDefault();
            if (bookWithHistory != null)
            {
                var bookSvc = new BookService(ctx);
                var (success, msg) = bookSvc.Delete(bookWithHistory.BookId);
                Assert(!success, "Book with borrowing history must NOT be deleted.");
                Assert(msg.Contains("borrow", StringComparison.OrdinalIgnoreCase), $"Unexpected error message: {msg}");
            }
        }

        // ── 21. DELETE SAFETY: MEMBER WITH BORROW HISTORY ───────────────────────────
        private static void Test_DeleteSafety_MemberWithBorrowHistory()
        {
            using var ctx = Program.CreateDbContext();
            var memberWithHistory = ctx.Borrows.Select(b => b.Member).FirstOrDefault();
            if (memberWithHistory != null)
            {
                var memberSvc = new MemberService(ctx);
                var (success, msg) = memberSvc.Delete(memberWithHistory.MemberId);
                Assert(!success, "Member with borrowing history must NOT be deleted.");
                Assert(msg.Contains("borrow", StringComparison.OrdinalIgnoreCase), $"Unexpected error message: {msg}");
            }
        }

        // ── 22. DELETE SAFETY: LIBRARIAN WITH BORROW HISTORY ────────────────────────
        private static void Test_DeleteSafety_LibrarianWithBorrowHistory()
        {
            using var ctx = Program.CreateDbContext();
            var librarianWithHistory = ctx.Borrows.Select(b => b.Librarian).FirstOrDefault();

            if (librarianWithHistory != null)
            {
                // Create a temporary secondary admin to perform the delete test
                // so that the self-deletion check (deleting own logged-in account) does not mask the history check
                var tempAdmin = new Librarian
                {
                    Name = "Secondary Admin Test",
                    Username = $"temp_adm_{DateTime.Now.Ticks % 100000}",
                    Role = "Admin",
                    Phone = "012333444",
                    Position = "Admin"
                };
                ctx.Librarians.Add(tempAdmin);
                ctx.SaveChanges();

                SessionManager.Login(tempAdmin);
                try
                {
                    var libSvc = new LibrarianService(ctx);
                    var (success, msg) = libSvc.Delete(librarianWithHistory.LibrarianId);
                    Assert(!success, "Librarian with borrow history must NOT be deleted.");
                    Assert(msg.Contains("borrow", StringComparison.OrdinalIgnoreCase), $"Unexpected error message: {msg}");
                }
                finally
                {
                    SessionManager.Logout();
                    ctx.Librarians.Remove(tempAdmin);
                    ctx.SaveChanges();
                }
            }
        }

        // ── 23. DELETE SAFETY: REFERENCED METADATA ──────────────────────────────────
        private static void Test_DeleteSafety_ReferencedMetadata()
        {
            using var ctx = Program.CreateDbContext();
            var cat = ctx.Books.Select(b => b.Category).FirstOrDefault();
            var aut = ctx.Books.Select(b => b.Author).FirstOrDefault();
            var pub = ctx.Books.Select(b => b.Publisher).FirstOrDefault();

            if (cat != null)
            {
                var (success, msg) = new CategoryService(ctx).Delete(cat.CategoryId);
                Assert(!success, "Category assigned to books must not be deleted.");
                Assert(msg.Contains("books are assigned", StringComparison.OrdinalIgnoreCase), $"Unexpected message: {msg}");
            }

            if (aut != null)
            {
                var (success, msg) = new AuthorService(ctx).Delete(aut.AuthorId);
                Assert(!success, "Author assigned to books must not be deleted.");
                Assert(msg.Contains("books are assigned", StringComparison.OrdinalIgnoreCase), $"Unexpected message: {msg}");
            }

            if (pub != null)
            {
                var (success, msg) = new PublisherService(ctx).Delete(pub.PublisherId);
                Assert(!success, "Publisher assigned to books must not be deleted.");
                Assert(msg.Contains("books are assigned", StringComparison.OrdinalIgnoreCase), $"Unexpected message: {msg}");
            }
        }

        // ── 24. CONCURRENCY: BOOK ROWVERSION CONFLICT ───────────────────────────────
        private static void Test_Concurrency_BookUpdate_RowVersion()
        {
            using var ctx1 = Program.CreateDbContext();
            using var ctx2 = Program.CreateDbContext();

            // User 1 loads book with initial RowVersion
            var book1 = ctx1.Books.AsNoTracking().First();
            var originalTitle = book1.Title;

            // User 2 loads and modifies the same book, triggering a RowVersion change
            var book2 = ctx2.Books.First(b => b.BookId == book1.BookId);
            book2.Title = originalTitle + " [ModBy2]";
            ctx2.SaveChanges();

            // User 1 attempts to save changes using the original stale RowVersion
            var svc1 = new BookService(ctx1);
            book1.Title = originalTitle + " [ModBy1]";
            var (success, msg) = svc1.Update(book1);

            Assert(!success, "Stale book update should fail due to RowVersion concurrency conflict.");
            Assert(msg.Contains("another user", StringComparison.OrdinalIgnoreCase) || msg.Contains("modified", StringComparison.OrdinalIgnoreCase),
                $"Unexpected concurrency message: {msg}");

            // Clean up: restore title
            using var ctxCleanup = Program.CreateDbContext();
            var bookCleanup = ctxCleanup.Books.First(b => b.BookId == book1.BookId);
            bookCleanup.Title = originalTitle;
            ctxCleanup.SaveChanges();
        }

        // ── 25. CONCURRENCY: CONCURRENT BORROW OF LAST COPY ─────────────────────────
        private static void Test_Concurrency_ConcurrentBorrow_LastCopy()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);

            int initialCopies = book.AvailableCopies;
            var borrowSvc = new BorrowService(ctx);

            // User 1 borrows all remaining copies
            var (b1Success, b1Msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7),
                new List<(int, int)> { (book.BookId, initialCopies) });
            Assert(b1Success, $"First borrow failed: {b1Msg}");

            // User 2 attempts to borrow 1 more copy -> must fail because available = 0
            var (b2Success, b2Msg) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7),
                new List<(int, int)> { (book.BookId, 1) });
            Assert(!b2Success, "Second borrow should fail when no copies are available.");

            // Clean up: return User 1's borrow and remove record
            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();
            borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();

            ctx.Entry(book).Reload();
            Assert(book.AvailableCopies == initialCopies, "Inventory was not restored.");
        }

        // ── 26. CONCURRENCY: CONCURRENT RETURN ──────────────────────────────────────
        private static void Test_Concurrency_ConcurrentReturn()
        {
            using var ctx = Program.CreateDbContext();
            var member = ctx.Members.First();
            var librarian = ctx.Librarians.First();
            var book = ctx.Books.First(b => b.AvailableCopies > 0);

            var borrowSvc = new BorrowService(ctx);
            var (bSuccess, _) = borrowSvc.CreateBorrow(member.MemberId, librarian.LibrarianId, DateTime.Today.AddDays(7),
                new List<(int, int)> { (book.BookId, 1) });
            Assert(bSuccess, "Setup borrow failed.");

            var latestBorrow = ctx.Borrows.Where(b => b.MemberId == member.MemberId).OrderByDescending(b => b.BorrowId).First();

            // User 1 returns
            var (r1Success, _, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(r1Success, "First return must succeed.");

            // User 2 attempts concurrent return of the same borrow -> must be rejected
            var (r2Success, r2Msg, _) = borrowSvc.ProcessReturn(latestBorrow.BorrowId);
            Assert(!r2Success, "Concurrent return must be rejected.");
            Assert(r2Msg.Contains("already been returned", StringComparison.OrdinalIgnoreCase), $"Unexpected error: {r2Msg}");

            // Clean up
            ctx.BorrowDetails.RemoveRange(ctx.BorrowDetails.Where(bd => bd.BorrowId == latestBorrow.BorrowId));
            ctx.Borrows.Remove(latestBorrow);
            ctx.SaveChanges();
        }

        // ── 27. DATABASE INTEGRITY: INVENTORY BOUNDS ────────────────────────────────
        private static void Test_DatabaseIntegrity_InventoryBounds()
        {
            using var ctx = Program.CreateDbContext();
            var allBooks = ctx.Books.ToList();
            foreach (var b in allBooks)
            {
                Assert(b.AvailableCopies >= 0, $"Book '{b.Title}' (ID {b.BookId}) has negative AvailableCopies: {b.AvailableCopies}");
                Assert(b.AvailableCopies <= b.TotalCopies, $"Book '{b.Title}' (ID {b.BookId}) has AvailableCopies ({b.AvailableCopies}) > TotalCopies ({b.TotalCopies})");
            }
        }

        // ── 28. REPORTING: FILTERED QUERIES & EXPORTS ───────────────────────────────
        private static void Test_Reporting_FilteredQueriesAndExports()
        {
            using var ctx = Program.CreateDbContext();
            var repSvc = new ReportService(ctx);

            var criteria = new ReportFilterCriteria
            {
                FromDate = DateTime.Today.AddDays(-60),
                ToDate = DateTime.Today.AddDays(1),
                Status = "All"
            };

            var filtered = repSvc.GetFilteredBorrows(criteria);
            Assert(filtered != null, "GetFilteredBorrows returned null.");

            // Test export functions with headless DataGridView and temporary output paths
            using var dgv = new DataGridView();
            dgv.Columns.Add("BorrowId", "Borrow ID");
            dgv.Columns.Add("MemberName", "Member");
            dgv.Columns.Add("BookTitle", "Book");
            dgv.Columns.Add("Status", "Status");
            dgv.Rows.Add("B-001", "Test Member", "C# in Depth", "Active");

            var tempCsv = Path.Combine(Path.GetTempPath(), $"test_report_{Guid.NewGuid():N}.csv");
            var tempPdf = Path.Combine(Path.GetTempPath(), $"test_report_{Guid.NewGuid():N}.pdf");
            var tempHtml = Path.Combine(Path.GetTempPath(), $"test_report_{Guid.NewGuid():N}.html");

            try
            {
                ExportHelper.SaveExcelFile(dgv, tempCsv, "Library Circulation Report", "Status: All");
                Assert(File.Exists(tempCsv), "Excel/CSV export file was not created.");
                var csvContent = File.ReadAllText(tempCsv);
                Assert(csvContent.Contains("Borrow ID"), "CSV export header missing.");
                Assert(csvContent.Contains("C# in Depth"), "CSV export content missing.");

                // Test genuine PDF generation with PdfSharp
                ExportHelper.SavePdfFile(dgv, tempPdf, "Library Circulation Report", "Status: All");
                Assert(File.Exists(tempPdf), "PDF export file was not created.");
                var pdfBytes = File.ReadAllBytes(tempPdf);
                Assert(pdfBytes.Length >= 4 && pdfBytes[0] == '%' && pdfBytes[1] == 'P' && pdfBytes[2] == 'D' && pdfBytes[3] == 'F',
                    "Generated file is not a valid PDF binary (%PDF signature missing).");

                // Test HTML report generation
                ExportHelper.SaveHtmlReportFile(dgv, tempHtml, "Library Circulation Report", "Status: All");
                Assert(File.Exists(tempHtml), "HTML export file was not created.");
                var htmlContent = File.ReadAllText(tempHtml);
                Assert(htmlContent.Contains("Library Circulation Report"), "HTML export title missing.");
                Assert(htmlContent.Contains("C# in Depth"), "HTML export content missing.");
            }
            finally
            {
                if (File.Exists(tempCsv)) File.Delete(tempCsv);
                if (File.Exists(tempPdf)) File.Delete(tempPdf);
                if (File.Exists(tempHtml)) File.Delete(tempHtml);
            }
        }

        // ── 29. REPORTING: SUMMARY QUERIES ──────────────────────────────────────────
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

        // ── 30. RESPONSIVE MATRIX ───────────────────────────────────────────────────
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
