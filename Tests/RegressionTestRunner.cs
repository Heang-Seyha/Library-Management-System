using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dialogs;
using LibraryManagementSystem.Forms;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Panels;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Validators;
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
            RunTest("31. Grid Header Sorting — Dashboard & Reports Columns Automatic Sort", Test_GridHeaderSorting_DashboardAndReports);
            RunTest("32. Live Search — Categories, Authors, Publishers, Librarians, Reports Panels", Test_SearchOnFivePanels);
            RunTest("33. Dashboard Stat Cards — Icon Optical Centering Verification", Test_DashboardStatCardIconsCentering);
            RunTest("34. Action Buttons Layout — Books and Members Refresh Button at Far Right", Test_RefreshButtonPositionOnBooksAndMembersPanels);
            RunTest("35. Person & Member Gender — Base Class Encapsulation, UI & DB Persistence", Test_MemberGenderFieldAndPersistence);
            RunTest("36. Gender & Date of Birth — All Entities (Member, Author, Admin, Librarian)", Test_GenderAndDateOfBirthAcrossAllEntities);

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
                var target = new Librarian { Name = "Unauthorized Test", Username = "unauth_lib_test", Role = "Librarian", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1) };
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
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Role = "Librarian",
                    Phone = "012000111",
                    Email = "admin_auth_test@library.gov.kh"
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
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Username = existingLibrarian.Username,
                    Phone = "012345678",
                    Email = "dup_test@library.gov.kh",
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
                    Email = "sec_admin@library.gov.kh"
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

        // ── 31. GRID HEADER SORTING ────────────────────────────────────────────────
        private static void Test_GridHeaderSorting_DashboardAndReports()
        {
            // 1. Test SortableBindingList directly
            var testList = new List<BorrowReportRow>
            {
                new() { BorrowId = 2, MemberName = "Charlie", EstimatedFine = 4000m, DueDate = DateTime.Today.AddDays(5) },
                new() { BorrowId = 1, MemberName = "Alice", EstimatedFine = 2000m, DueDate = DateTime.Today.AddDays(1) },
                new() { BorrowId = 3, MemberName = "Bob", EstimatedFine = 6000m, DueDate = DateTime.Today.AddDays(10) }
            };

            var sortable = new SortableBindingList<BorrowReportRow>(testList);
            var dgvTest = new DataGridView();
            dgvTest.BindingContext = new BindingContext();
            dgvTest.AutoGenerateColumns = false;
            var colId = new DataGridViewTextBoxColumn { Name = "BorrowId", DataPropertyName = "BorrowId", SortMode = DataGridViewColumnSortMode.Automatic };
            var colMember = new DataGridViewTextBoxColumn { Name = "MemberName", DataPropertyName = "MemberName", SortMode = DataGridViewColumnSortMode.Automatic };
            var colFine = new DataGridViewTextBoxColumn { Name = "EstimatedFine", DataPropertyName = "EstimatedFine", SortMode = DataGridViewColumnSortMode.Automatic };
            dgvTest.Columns.AddRange(colId, colMember, colFine);
            dgvTest.DataSource = sortable;

            // Sort by BorrowId Ascending
            dgvTest.Sort(colId, System.ComponentModel.ListSortDirection.Ascending);
            Assert(sortable[0].BorrowId == 1 && sortable[1].BorrowId == 2 && sortable[2].BorrowId == 3, "SortableBindingList failed to sort BorrowId ascending.");

            // Sort by BorrowId Descending
            dgvTest.Sort(colId, System.ComponentModel.ListSortDirection.Descending);
            Assert(sortable[0].BorrowId == 3 && sortable[1].BorrowId == 2 && sortable[2].BorrowId == 1, "SortableBindingList failed to sort BorrowId descending.");

            // Sort by MemberName Ascending
            dgvTest.Sort(colMember, System.ComponentModel.ListSortDirection.Ascending);
            Assert(sortable[0].MemberName == "Alice" && sortable[2].MemberName == "Charlie", "SortableBindingList failed to sort MemberName ascending.");

            // Sort by EstimatedFine Descending
            dgvTest.Sort(colFine, System.ComponentModel.ListSortDirection.Descending);
            Assert(sortable[0].EstimatedFine == 6000m && sortable[2].EstimatedFine == 2000m, "SortableBindingList failed to sort EstimatedFine descending.");

            // 2. Test DashboardPanel grid columns sort mode
            using var db = new DashboardPanel();
            var overdueGrid = db.Controls.Find("dgvOverdue", true).OfType<DataGridView>().FirstOrDefault();
            var recentGrid = db.Controls.Find("dgvRecent", true).OfType<DataGridView>().FirstOrDefault();

            Assert(overdueGrid != null, "DashboardPanel dgvOverdue grid not found.");
            Assert(recentGrid != null, "DashboardPanel dgvRecent grid not found.");

            foreach (DataGridViewColumn col in overdueGrid!.Columns)
            {
                Assert(col.SortMode == DataGridViewColumnSortMode.Automatic, $"Dashboard dgvOverdue column '{col.HeaderText}' SortMode is not Automatic.");
            }

            foreach (DataGridViewColumn col in recentGrid!.Columns)
            {
                Assert(col.SortMode == DataGridViewColumnSortMode.Automatic, $"Dashboard dgvRecent column '{col.HeaderText}' SortMode is not Automatic.");
            }

            // 3. Test ReportsPanel grid columns sort mode
            using var rep = new ReportsPanel();
            rep.LoadAllReports();

            var activeGrid = rep.Controls.Find("dgvActive", true).OfType<DataGridView>().FirstOrDefault();
            var invGrid = rep.Controls.Find("dgvInventory", true).OfType<DataGridView>().FirstOrDefault();

            Assert(activeGrid != null, "ReportsPanel dgvActive grid not found.");
            Assert(invGrid != null, "ReportsPanel dgvInventory grid not found.");

            foreach (DataGridViewColumn col in activeGrid!.Columns)
            {
                Assert(col.SortMode == DataGridViewColumnSortMode.Automatic, $"Reports dgvActive column '{col.HeaderText}' SortMode is not Automatic.");
            }

            foreach (DataGridViewColumn col in invGrid!.Columns)
            {
                Assert(col.SortMode == DataGridViewColumnSortMode.Automatic, $"Reports dgvInventory column '{col.HeaderText}' SortMode is not Automatic.");
            }
        }

        private static void Test_SearchOnFivePanels()
        {
            // 1. CategoriesPanel
            using (var catPanel = new CategoriesPanel())
            {
                catPanel.LoadData();
                var txtSearch = catPanel.Controls.Find("txtSearch", true).OfType<TextBox>().FirstOrDefault();
                var dgv = catPanel.Controls.Find("dgv", true).OfType<DataGridView>().FirstOrDefault();
                Assert(txtSearch != null, "CategoriesPanel txtSearch not found.");
                Assert(dgv != null, "CategoriesPanel dgv not found.");

                txtSearch!.Text = "NonExistentCategoryQuery999";
                Assert(dgv!.Rows.Count == 0, "CategoriesPanel should filter to 0 rows for non-matching query.");

                txtSearch.Text = "";
                Assert(dgv.Rows.Count > 0, "CategoriesPanel should restore rows when search text is cleared.");
            }

            // 2. AuthorsPanel
            using (var authPanel = new AuthorsPanel())
            {
                authPanel.LoadData();
                var txtSearch = authPanel.Controls.Find("txtSearch", true).OfType<TextBox>().FirstOrDefault();
                var dgv = authPanel.Controls.Find("dgv", true).OfType<DataGridView>().FirstOrDefault();
                Assert(txtSearch != null, "AuthorsPanel txtSearch not found.");
                Assert(dgv != null, "AuthorsPanel dgv not found.");

                txtSearch!.Text = "NonExistentAuthorQuery999";
                Assert(dgv!.Rows.Count == 0, "AuthorsPanel should filter to 0 rows for non-matching query.");

                txtSearch.Text = "";
                Assert(dgv.Rows.Count > 0, "AuthorsPanel should restore rows when search text is cleared.");
            }

            // 3. PublishersPanel
            using (var pubPanel = new PublishersPanel())
            {
                pubPanel.LoadData();
                var txtSearch = pubPanel.Controls.Find("txtSearch", true).OfType<TextBox>().FirstOrDefault();
                var dgv = pubPanel.Controls.Find("dgv", true).OfType<DataGridView>().FirstOrDefault();
                Assert(txtSearch != null, "PublishersPanel txtSearch not found.");
                Assert(dgv != null, "PublishersPanel dgv not found.");

                txtSearch!.Text = "NonExistentPublisherQuery999";
                Assert(dgv!.Rows.Count == 0, "PublishersPanel should filter to 0 rows for non-matching query.");

                txtSearch.Text = "";
                Assert(dgv.Rows.Count > 0, "PublishersPanel should restore rows when search text is cleared.");
            }

            // 4. LibrariansPanel
            using (var libPanel = new LibrariansPanel())
            {
                libPanel.LoadData();
                var txtSearch = libPanel.Controls.Find("txtSearch", true).OfType<TextBox>().FirstOrDefault();
                var dgv = libPanel.Controls.Find("dgv", true).OfType<DataGridView>().FirstOrDefault();
                Assert(txtSearch != null, "LibrariansPanel txtSearch not found.");
                Assert(dgv != null, "LibrariansPanel dgv not found.");

                txtSearch!.Text = "NonExistentLibrarianQuery999";
                Assert(dgv!.Rows.Count == 0, "LibrariansPanel should filter to 0 rows for non-matching query.");

                txtSearch.Text = "";
                Assert(dgv.Rows.Count > 0, "LibrariansPanel should restore rows when search text is cleared.");
            }

            // 5. ReportsPanel
            using (var repPanel = new ReportsPanel())
            {
                // Force load reports
                repPanel.LoadAllReports();
                var txtSearch = repPanel.Controls.Find("txtSearch", true).OfType<TextBox>().FirstOrDefault();
                var dgvInv = repPanel.Controls.Find("dgvInventory", true).OfType<DataGridView>().FirstOrDefault();
                Assert(txtSearch != null, "ReportsPanel txtSearch not found.");
                Assert(dgvInv != null, "ReportsPanel dgvInventory not found.");

                txtSearch!.Text = "NonExistentReportQuery999";
                Assert(dgvInv!.Rows.Count == 0, "ReportsPanel inventory should filter to 0 rows for non-matching query.");

                txtSearch.Text = "";
                Assert(dgvInv.Rows.Count > 0, "ReportsPanel inventory should restore rows when search text is cleared.");
            }
        }

        private static void Test_DashboardStatCardIconsCentering()
        {
            using var db = new DashboardPanel();
            var iconNames = new[] { "lblBooksIcon", "lblMembersIcon", "lblBorrowIcon", "lblOverdueIcon" };

            foreach (var name in iconNames)
            {
                var lbl = db.Controls.Find(name, true).OfType<Label>().FirstOrDefault();
                Assert(lbl != null, $"Dashboard icon badge '{name}' not found.");
                Assert(lbl!.Image != null, $"Dashboard icon badge '{name}' must have an Image assigned.");

                var bmp = (Bitmap)lbl.Image!;
                int minX = bmp.Width, maxX = -1;
                int minY = bmp.Height, maxY = -1;

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        var px = bmp.GetPixel(x, y);
                        if (px.A > 20)
                        {
                            if (x < minX) minX = x;
                            if (x > maxX) maxX = x;
                            if (y < minY) minY = y;
                            if (y > maxY) maxY = y;
                        }
                    }
                }

                Assert(maxX >= minX, $"Dashboard icon '{name}' does not contain rendered pixels.");

                int leftGap = minX;
                int rightGap = (bmp.Width - 1) - maxX;
                int diffX = Math.Abs(leftGap - rightGap);

                Assert(diffX <= 1, $"Dashboard icon '{name}' is not centered horizontally (leftGap={leftGap}, rightGap={rightGap}, diff={diffX}).");
            }
        }

        private static void Test_RefreshButtonPositionOnBooksAndMembersPanels()
        {
            using (var booksPanel = new BooksPanel())
            {
                var flp = booksPanel.Controls.Find("flpActions", true).OfType<FlowLayoutPanel>().FirstOrDefault();
                Assert(flp != null, "BooksPanel flpActions not found.");
                var lastControl = flp!.Controls[flp.Controls.Count - 1];
                Assert(lastControl.Name == "btnRefresh", $"BooksPanel last action button should be btnRefresh, but was '{lastControl.Name}'.");
            }

            using (var membersPanel = new MembersPanel())
            {
                var flp = membersPanel.Controls.Find("flpActions", true).OfType<FlowLayoutPanel>().FirstOrDefault();
                Assert(flp != null, "MembersPanel flpActions not found.");
                var lastControl = flp!.Controls[flp.Controls.Count - 1];
                Assert(lastControl.Name == "btnRefresh", $"MembersPanel last action button should be btnRefresh, but was '{lastControl.Name}'.");
            }
        }

        private static void Test_MemberGenderFieldAndPersistence()
        {
            // 1. Person OOP Base Class Encapsulation
            var testMember = new Member
            {
                Name = "Test Person",
                Gender = "Female",
                Phone = "012-333-444",
                Email = "test.person@test.com",
                Address = "Phnom Penh"
            };
            Assert(testMember.Gender == "Female", "Member Gender property encapsulation failed.");
            string info = testMember.GetInfo();
            Assert(info.Contains("Gender: Female"), $"GetInfo() polymorphic override should contain Gender, but returned '{info}'.");

            // 2. MemberEditDialog Gender Controls & Defaults
            using (var dialog = new MemberEditDialog())
            {
                var cmbGender = dialog.Controls.Find("cmbGender", true).OfType<ComboBox>().FirstOrDefault();
                Assert(cmbGender != null, "MemberEditDialog cmbGender control not found.");
                Assert(cmbGender!.Items.Contains("Male"), "cmbGender should contain 'Male'.");
                Assert(cmbGender.Items.Contains("Female"), "cmbGender should contain 'Female'.");
                Assert(cmbGender.DropDownStyle == ComboBoxStyle.DropDownList, "cmbGender should be DropDownList.");
            }

            // 3. MembersPanel DataGridView colGender
            using (var membersPanel = new MembersPanel())
            {
                var dgv = membersPanel.Controls.Find("dgv", true).OfType<DataGridView>().FirstOrDefault();
                Assert(dgv != null, "MembersPanel dgv not found.");
                Assert(dgv!.Columns.Contains("colGender"), "MembersPanel dgv does not contain colGender.");
                var colGender = dgv.Columns["colGender"]!;
                Assert(colGender.HeaderText == "Gender", $"colGender HeaderText should be 'Gender', got '{colGender.HeaderText}'.");
            }

            // 4. Database Persistence & Querying with Gender
            using (var ctx = Program.CreateDbContext())
            {
                var svc = new MemberService(ctx);

                // Add test member with Female gender
                string uniqueEmail = $"gender.test.{Guid.NewGuid():N}@test.com";
                var newMember = new Member
                {
                    Name = "Gender Test User",
                    Gender = "Female",
                    DateOfBirth = new DateTime(1995, 5, 20),
                    Phone = "012-999-888",
                    Email = uniqueEmail,
                    Address = "Siem Reap",
                    JoinDate = DateTime.Today
                };

                var (addOk, addMsg) = svc.Add(newMember);
                Assert(addOk, $"Adding member with Gender failed: {addMsg}");
                Assert(newMember.MemberId > 0, "Saved member should have generated MemberId.");

                // Re-fetch from fresh context
                using (var queryCtx = Program.CreateDbContext())
                {
                    var fetched = queryCtx.Members.Find(newMember.MemberId);
                    Assert(fetched != null, "Could not find newly added member.");
                    Assert(fetched!.Gender == "Female", $"Saved member Gender should be 'Female', but was '{fetched.Gender}'.");

                    // Test search by Gender
                    var querySvc = new MemberService(queryCtx);
                    var femaleMembers = querySvc.Search("Female");
                    Assert(femaleMembers.Any(m => m.MemberId == newMember.MemberId), "MemberService.Search should match by Gender.");

                    // Clean up test record
                    querySvc.Delete(newMember.MemberId);
                }
            }
        }

        private static void Test_GenderAndDateOfBirthAcrossAllEntities()
        {
            // 1. OOP Polymorphism & Inheritance: Person Base Class
            Person authorPerson = new Author { Name = "Polymorphic Author", Gender = "Female", DateOfBirth = new DateTime(1985, 4, 12), Bio = "Author bio" };
            Person memberPerson = new Member { Name = "Polymorphic Member", Gender = "Male", DateOfBirth = new DateTime(1998, 7, 24), Phone = "012345678" };
            Person librarianPerson = new Librarian { Name = "Polymorphic Librarian", Gender = "Female", DateOfBirth = new DateTime(1990, 11, 3), Username = "poly_lib", Role = "Librarian" };
            Person adminPerson = new Librarian { Name = "Polymorphic Admin", Gender = "Male", DateOfBirth = new DateTime(1988, 2, 14), Username = "poly_admin", Role = "Admin" };

            Assert(authorPerson.Gender == "Female" && authorPerson.DateOfBirth == new DateTime(1985, 4, 12), "Author Person inheritance failed.");
            Assert(memberPerson.Gender == "Male" && memberPerson.DateOfBirth == new DateTime(1998, 7, 24), "Member Person inheritance failed.");
            Assert(librarianPerson.Gender == "Female" && librarianPerson.DateOfBirth == new DateTime(1990, 11, 3), "Librarian Person inheritance failed.");
            Assert(adminPerson.Gender == "Male" && adminPerson.DateOfBirth == new DateTime(1988, 2, 14), "Admin Person inheritance failed.");

            Assert(authorPerson.GetInfo().Contains("Gender: Female") && authorPerson.GetInfo().Contains("04/12/1985"), "Author GetInfo() override failed.");
            Assert(memberPerson.GetInfo().Contains("Gender: Male") && memberPerson.GetInfo().Contains("07/24/1998") && memberPerson.GetInfo().Contains("Join Date:"), "Member GetInfo() override failed.");
            Assert(librarianPerson.GetInfo().Contains("Gender: Female") && librarianPerson.GetInfo().Contains("11/03/1990"), "Librarian GetInfo() override failed.");
            Assert(adminPerson.GetInfo().Contains("Gender: Male") && adminPerson.GetInfo().Contains("02/14/1988"), "Admin GetInfo() override failed.");

            // 2. UI Dialogs Verification (Defaults must be blank/unselected & required)
            using (var dlgMember = new MemberEditDialog())
            {
                var cmb = dlgMember.Controls.Find("cmbGender", true).OfType<ComboBox>().FirstOrDefault();
                var dtp = dlgMember.Controls.Find("dtpDob", true).OfType<DateTimePicker>().FirstOrDefault();
                var dtpJoin = dlgMember.Controls.Find("dtpJoin", true).OfType<DateTimePicker>().FirstOrDefault();
                Assert(cmb != null && cmb.SelectedIndex == -1, "MemberEditDialog cmbGender should default to unselected (-1).");
                Assert(dtp != null && dtp.CustomFormat == " ", "MemberEditDialog dtpDob should default to blank (' ').");
                Assert(dtpJoin != null && dtpJoin.CustomFormat == "MM/dd/yyyy", "MemberEditDialog dtpJoin should format as MM/dd/yyyy with leading zero.");
            }
            using (var dlgAuthor = new AuthorEditDialog())
            {
                var cmb = dlgAuthor.Controls.Find("cmbGender", true).OfType<ComboBox>().FirstOrDefault();
                var dtp = dlgAuthor.Controls.Find("dtpDob", true).OfType<DateTimePicker>().FirstOrDefault();
                Assert(cmb != null && cmb.SelectedIndex == -1, "AuthorEditDialog cmbGender should default to unselected (-1).");
                Assert(dtp != null && dtp.CustomFormat == " ", "AuthorEditDialog dtpDob should default to blank (' ').");
            }
            using (var dlgLibrarian = new LibrarianEditDialog())
            {
                var cmb = dlgLibrarian.Controls.Find("cmbGender", true).OfType<ComboBox>().FirstOrDefault();
                var dtp = dlgLibrarian.Controls.Find("dtpDob", true).OfType<DateTimePicker>().FirstOrDefault();
                var txtEmail = dlgLibrarian.Controls.Find("txtEmail", true).OfType<TextBox>().FirstOrDefault();
                var lblEmail = dlgLibrarian.Controls.Find("lblEmailLabel", true).OfType<Label>().FirstOrDefault();
                var lblPhone = dlgLibrarian.Controls.Find("lblPhoneLabel", true).OfType<Label>().FirstOrDefault();
                Assert(cmb != null && cmb.SelectedIndex == -1, "LibrarianEditDialog cmbGender should default to unselected (-1).");
                Assert(dtp != null && dtp.CustomFormat == " ", "LibrarianEditDialog dtpDob should default to blank (' ').");
                Assert(txtEmail != null, "LibrarianEditDialog txtEmail not found.");
                Assert(lblEmail != null && lblEmail.Text.Contains("*"), "LibrarianEditDialog Email must be marked required (*).");
                Assert(lblPhone != null && lblPhone.Text.Contains("*"), "LibrarianEditDialog Phone must be marked required (*).");
            }

            // 2b. Strict Validation Rules: Gender, Date of Birth, Phone, and Email are REQUIRED
            Assert(!new MemberValidator().Validate(new Member { Name = "Test", Phone = "012345678", Email = "m@t.com" }).IsValid,
                "MemberValidator must reject missing Gender and DateOfBirth.");
            Assert(!new AuthorValidator().Validate(new Author { Name = "Test" }).IsValid,
                "AuthorValidator must reject missing Gender and DateOfBirth.");
            Assert(!new LibrarianValidator().Validate(new Librarian { Name = "Test", Username = "user", Role = "Librarian", Phone = "012345678", Email = "test@library.gov.kh" }).IsValid,
                "LibrarianValidator must reject missing Gender and DateOfBirth.");
            Assert(!new LibrarianValidator().Validate(new Librarian { Name = "Test", Username = "user", Role = "Librarian", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Phone = "", Email = "test@library.gov.kh" }).IsValid,
                "LibrarianValidator must reject missing Phone.");
            Assert(!new LibrarianValidator().Validate(new Librarian { Name = "Test", Username = "user", Role = "Librarian", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Phone = "012345678", Email = "" }).IsValid,
                "LibrarianValidator must reject missing Email.");
            Assert(!new LibrarianValidator().Validate(new Librarian { Name = "Test", Username = "user", Role = "Librarian", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Phone = "invalid-phone", Email = "test@library.gov.kh" }).IsValid,
                "LibrarianValidator must reject invalid Phone format.");
            Assert(!new LibrarianValidator().Validate(new Librarian { Name = "Test", Username = "user", Role = "Librarian", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Phone = "012345678", Email = "invalid-email" }).IsValid,
                "LibrarianValidator must reject invalid Email format.");
            Assert(new LibrarianValidator().Validate(new Librarian { Name = "Test", Username = "valid_user", Role = "Librarian", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), Phone = "012345678", Email = "test@library.gov.kh" }).IsValid,
                "LibrarianValidator must accept valid Librarian with Phone and Email.");

            // 3. UI Panels DataGridView Columns Verification
            using (var membersPanel = new MembersPanel())
            {
                var dgv = membersPanel.Controls.Find("dgv", true).OfType<DataGridView>().First();
                Assert(dgv.Columns.Contains("colGender") && dgv.Columns.Contains("colDob") && dgv.Columns.Contains("colJoinDate"),
                    "MembersPanel missing colGender, colDob, or colJoinDate.");
                membersPanel.LoadData();
                if (dgv.Rows.Count > 0)
                {
                    string joinVal = dgv.Rows[0].Cells["colJoinDate"].Value?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(joinVal))
                    {
                        Assert(System.Text.RegularExpressions.Regex.IsMatch(joinVal, @"^\d{2}/\d{2}/\d{4}$"),
                            $"MembersPanel Join Date '{joinVal}' must have leading zeros in MM/dd/yyyy format.");
                    }
                }
            }
            using (var authorsPanel = new AuthorsPanel())
            {
                var dgv = authorsPanel.Controls.Find("dgv", true).OfType<DataGridView>().First();
                Assert(dgv.Columns.Contains("colGender") && dgv.Columns.Contains("colDob"), "AuthorsPanel missing colGender or colDob.");
            }
            using (var librariansPanel = new LibrariansPanel())
            {
                var dgv = librariansPanel.Controls.Find("dgv", true).OfType<DataGridView>().First();
                Assert(dgv.Columns.Contains("colGender") && dgv.Columns.Contains("colDob"), "LibrariansPanel missing colGender or colDob.");
                Assert(dgv.Columns.Contains("colEmail") && !dgv.Columns.Contains("colPosition"), "LibrariansPanel must have colEmail and not colPosition.");
            }

            // 4. Database Persistence & Services Verification
            var prevUser = SessionManager.CurrentLibrarian;
            try
            {
                using var ctx = Program.CreateDbContext();

                // D. Admin persistence verification from seeded accounts
                var seededAdmin = ctx.Librarians.FirstOrDefault(l => l.Role == "Admin");
                Assert(seededAdmin != null, "Seeded Admin not found.");
                Assert(!string.IsNullOrEmpty(seededAdmin!.Gender), "Admin Gender should not be empty.");
                Assert(seededAdmin.DateOfBirth.HasValue, "Admin DateOfBirth should not be null.");

                SessionManager.Login(seededAdmin);

                // A. Member persistence
                var memberSvc = new MemberService(ctx);
                var testMember = new Member
                {
                    Name = "Test Member DOB",
                    Gender = "Female",
                    DateOfBirth = new DateTime(1996, 6, 18),
                    Phone = "012888777",
                    Email = $"testdob.{Guid.NewGuid():N}@test.com",
                    Address = "Phnom Penh",
                    JoinDate = DateTime.Today
                };
                var (mOk, mMsg) = memberSvc.Add(testMember);
                Assert(mOk, $"Add Member with DOB failed: {mMsg}");

                // B. Author persistence
                var authorSvc = new AuthorService(ctx);
                var testAuthor = new Author
                {
                    Name = $"Test Author DOB {Guid.NewGuid():N}",
                    Gender = "Female",
                    DateOfBirth = new DateTime(1975, 9, 25),
                    Bio = "Author biography with DOB."
                };
                var (aOk, aMsg) = authorSvc.Add(testAuthor);
                Assert(aOk, $"Add Author with DOB failed: {aMsg}");

                // C. Librarian persistence
                var libSvc = new LibrarianService(ctx);
                string uniqueLibUser = $"lib_{Guid.NewGuid():N}".Substring(0, 15);
                var testLibrarian = new Librarian
                {
                    Name = "Test Librarian Staff",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1993, 12, 5),
                    Username = uniqueLibUser,
                    PasswordHash = "hashedpassword",
                    Role = "Librarian",
                    Email = $"{uniqueLibUser}@library.gov.kh",
                    Phone = "012777666"
                };
                var (lOk, lMsg) = libSvc.Add(testLibrarian, "password123");
                Assert(lOk, $"Add Librarian with DOB failed: {lMsg}");

                // Re-fetch everything from a new DbContext to guarantee database roundtrip
                using (var queryCtx = Program.CreateDbContext())
                {
                    var fetchedMember = queryCtx.Members.Find(testMember.MemberId);
                    Assert(fetchedMember != null && fetchedMember.Gender == "Female" && fetchedMember.DateOfBirth == new DateTime(1996, 6, 18),
                        "Persisted Member Gender/DOB verification failed.");

                    var fetchedAuthor = queryCtx.Authors.Find(testAuthor.AuthorId);
                    Assert(fetchedAuthor != null && fetchedAuthor.Gender == "Female" && fetchedAuthor.DateOfBirth == new DateTime(1975, 9, 25),
                        "Persisted Author Gender/DOB verification failed.");

                    var fetchedLib = queryCtx.Librarians.Find(testLibrarian.LibrarianId);
                    Assert(fetchedLib != null && fetchedLib.Gender == "Male" && fetchedLib.DateOfBirth == new DateTime(1993, 12, 5) && fetchedLib.Email == $"{uniqueLibUser}@library.gov.kh",
                        "Persisted Librarian Gender/DOB/Email verification failed.");

                    // Clean up test data
                    new MemberService(queryCtx).Delete(testMember.MemberId);
                    new AuthorService(queryCtx).Delete(testAuthor.AuthorId);
                    new LibrarianService(queryCtx).Delete(testLibrarian.LibrarianId);
                }
            }
            finally
            {
                if (prevUser != null) SessionManager.Login(prevUser);
                else SessionManager.Clear();
            }
        }
    }
}

