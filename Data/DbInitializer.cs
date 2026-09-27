using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    /// <summary>
    /// Seeds the database with realistic demo data on first run.
    /// This ensures the system is immediately demonstrable without manual data entry.
    /// </summary>
    public static class DbInitializer
    {
        public static void Seed(LibraryDbContext context)
        {
            // Only seed if the database is empty
            if (context.Librarians.Any()) return;

            // ── Categories ────────────────────────────────────────────────────
            var categories = new[]
            {
                new Category { Name = "Computer Science", Description = "Programming, algorithms, and software engineering" },
                new Category { Name = "Mathematics",      Description = "Algebra, calculus, statistics, and discrete math" },
                new Category { Name = "Physics",          Description = "Classical mechanics, quantum physics, and thermodynamics" },
                new Category { Name = "Literature",       Description = "Fiction, poetry, and classical literature" },
                new Category { Name = "History",          Description = "World history, civilization, and biography" },
            };
            context.Categories.AddRange(categories);

            // ── Authors ───────────────────────────────────────────────────────
            var authors = new[]
            {
                new Author { Name = "Robert C. Martin",  Bio = "Software engineer and author, known for Clean Code and SOLID principles." },
                new Author { Name = "Donald E. Knuth",   Bio = "Computer scientist and author of The Art of Computer Programming." },
                new Author { Name = "Thomas H. Cormen",  Bio = "Professor and co-author of Introduction to Algorithms." },
                new Author { Name = "Andrew Hunt",       Bio = "Co-author of The Pragmatic Programmer." },
                new Author { Name = "Martin Fowler",     Bio = "Software architect and author known for Refactoring." },
                new Author { Name = "Jon Kleinberg",     Bio = "Professor at Cornell and co-author of Algorithm Design." },
                new Author { Name = "Brian W. Kernighan", Bio = "Professor at Princeton and co-author of The C Programming Language." },
                new Author { Name = "James Stewart",     Bio = "Mathematician and author of widely used calculus textbooks." },
                new Author { Name = "George Orwell",     Bio = "English novelist and essayist, author of 1984 and Animal Farm." },
                new Author { Name = "Walter Isaacson",   Bio = "Biographer known for Steve Jobs, Einstein, and Da Vinci." },
            };
            context.Authors.AddRange(authors);

            // ── Publishers ────────────────────────────────────────────────────
            var publishers = new[]
            {
                new Publisher { Name = "Pearson Education",    Address = "Hoboken, NJ, USA",    Phone = "+1-800-922-0579" },
                new Publisher { Name = "O'Reilly Media",       Address = "Sebastopol, CA, USA", Phone = "+1-800-998-9938" },
                new Publisher { Name = "MIT Press",            Address = "Cambridge, MA, USA",  Phone = "+1-617-253-5646" },
                new Publisher { Name = "Addison-Wesley",       Address = "Boston, MA, USA",     Phone = "+1-617-944-3700" },
                new Publisher { Name = "Cengage Learning",     Address = "Boston, MA, USA",     Phone = "+1-800-354-9706" },
            };
            context.Publishers.AddRange(publishers);

            context.SaveChanges(); // Save to get IDs assigned

            // ── Books ─────────────────────────────────────────────────────────
            var books = new[]
            {
                new Book
                {
                    Title = "Clean Code",
                    ISBN = "978-0132350884",
                    Year = 2008,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[0].AuthorId,
                    PublisherId = publishers[3].PublisherId
                },
                new Book
                {
                    Title = "Introduction to Algorithms",
                    ISBN = "978-0262046305",
                    Year = 2022,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[2].AuthorId,
                    PublisherId = publishers[2].PublisherId
                },
                new Book
                {
                    Title = "The Pragmatic Programmer",
                    ISBN = "978-0135957059",
                    Year = 2019,
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[3].AuthorId,
                    PublisherId = publishers[3].PublisherId
                },
                new Book
                {
                    Title = "The C Programming Language",
                    ISBN = "978-0131103627",
                    Year = 1988,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[6].AuthorId,
                    PublisherId = publishers[0].PublisherId
                },
                new Book
                {
                    Title = "Refactoring",
                    ISBN = "978-0134757599",
                    Year = 2018,
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[4].AuthorId,
                    PublisherId = publishers[3].PublisherId
                },
                new Book
                {
                    Title = "Algorithm Design",
                    ISBN = "978-0321295354",
                    Year = 2005,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[5].AuthorId,
                    PublisherId = publishers[0].PublisherId
                },
                new Book
                {
                    Title = "Calculus: Early Transcendentals",
                    ISBN = "978-1285741550",
                    Year = 2015,
                    TotalCopies = 8,
                    AvailableCopies = 8,
                    CategoryId = categories[1].CategoryId,
                    AuthorId = authors[7].AuthorId,
                    PublisherId = publishers[4].PublisherId
                },
                new Book
                {
                    Title = "The Art of Computer Programming Vol.1",
                    ISBN = "978-0201896831",
                    Year = 1997,
                    TotalCopies = 2,
                    AvailableCopies = 2,
                    CategoryId = categories[0].CategoryId,
                    AuthorId = authors[1].AuthorId,
                    PublisherId = publishers[3].PublisherId
                },
                new Book
                {
                    Title = "1984",
                    ISBN = "978-0451524935",
                    Year = 1949,
                    TotalCopies = 10,
                    AvailableCopies = 10,
                    CategoryId = categories[3].CategoryId,
                    AuthorId = authors[8].AuthorId,
                    PublisherId = publishers[0].PublisherId
                },
                new Book
                {
                    Title = "Steve Jobs",
                    ISBN = "978-1451648539",
                    Year = 2011,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[4].CategoryId,
                    AuthorId = authors[9].AuthorId,
                    PublisherId = publishers[1].PublisherId
                },
            };
            context.Books.AddRange(books);

            // ── Members ───────────────────────────────────────────────────────
            var members = new[]
            {
                new Member { Name = "Sophea Chan",   Phone = "012-345-678", Email = "sophea.chan@email.com",   Address = "Phnom Penh, Cambodia",  JoinDate = new DateTime(2024, 1, 15) },
                new Member { Name = "Dara Kem",      Phone = "011-234-567", Email = "dara.kem@email.com",      Address = "Siem Reap, Cambodia",   JoinDate = new DateTime(2024, 2, 20) },
                new Member { Name = "Bopha Pich",    Phone = "015-678-901", Email = "bopha.pich@email.com",    Address = "Battambang, Cambodia",  JoinDate = new DateTime(2024, 3, 10) },
                new Member { Name = "Virak Seng",    Phone = "078-901-234", Email = "virak.seng@email.com",    Address = "Kampong Cham, Cambodia", JoinDate = new DateTime(2024, 4, 5)  },
                new Member { Name = "Sreymom Heng",  Phone = "096-345-678", Email = "sreymom.heng@email.com",  Address = "Phnom Penh, Cambodia",  JoinDate = new DateTime(2024, 5, 12) },
                new Member { Name = "Pisach Ly",     Phone = "085-456-789", Email = "pisach.ly@email.com",     Address = "Kandal, Cambodia",      JoinDate = new DateTime(2024, 6, 18) },
                new Member { Name = "Rachana Sok",   Phone = "077-567-890", Email = "rachana.sok@email.com",   Address = "Takeo, Cambodia",       JoinDate = new DateTime(2024, 7, 22) },
            };
            context.Members.AddRange(members);

            // ── Librarians ───────────────────────────────────────────────────
            // Passwords are hashed using BCrypt — never stored as plain text
            var librarians = new[]
            {
                new Librarian
                {
                    Name = "Admin User",
                    Phone = "023-456-789",
                    Position = "Library Administrator",
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                },
                new Librarian
                {
                    Name = "Sokha Meas",
                    Phone = "023-111-222",
                    Position = "Librarian",
                    Username = "sokha",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("sokha123"),
                    Role = "Librarian"
                },
                new Librarian
                {
                    Name = "Dyna Khun",
                    Phone = "023-333-444",
                    Position = "Librarian",
                    Username = "dyna",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("dyna123"),
                    Role = "Librarian"
                },
            };
            context.Librarians.AddRange(librarians);
            context.SaveChanges();

            // ── Demo Borrow Records ────────────────────────────────────────────
            // Borrow 1: Active borrow (Sophea Chan — currently has 2 books)
            var borrow1 = new Borrow
            {
                MemberId = members[0].MemberId,
                LibrarianId = librarians[1].LibrarianId,
                BorrowDate = DateTime.Today.AddDays(-5),
                DueDate = DateTime.Today.AddDays(9),  // Due in 9 days — not overdue
                Status = BorrowStatus.Active
            };
            context.Borrows.Add(borrow1);
            context.SaveChanges();

            context.BorrowDetails.AddRange(
                new BorrowDetail { BorrowId = borrow1.BorrowId, BookId = books[0].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = borrow1.BorrowId, BookId = books[2].BookId, Quantity = 1 }
            );
            // Reduce available copies
            books[0].AvailableCopies -= 1;
            books[2].AvailableCopies -= 1;

            // Borrow 2: Overdue borrow (Dara Kem — overdue by 7 days for demo)
            var borrow2 = new Borrow
            {
                MemberId = members[1].MemberId,
                LibrarianId = librarians[1].LibrarianId,
                BorrowDate = DateTime.Today.AddDays(-21),
                DueDate = DateTime.Today.AddDays(-7),  // 7 days overdue!
                Status = BorrowStatus.Overdue
            };
            context.Borrows.Add(borrow2);
            context.SaveChanges();

            context.BorrowDetails.Add(
                new BorrowDetail { BorrowId = borrow2.BorrowId, BookId = books[8].BookId, Quantity = 2 }
            );
            // Reduce available copies
            books[8].AvailableCopies -= 2;

            // Borrow 3: Already returned (Bopha Pich)
            var returnDate3 = DateTime.Today.AddDays(-2);
            var dueDate3 = DateTime.Today.AddDays(-5);
            var fine3 = FinePolicy.CalculateFine(dueDate3, returnDate3); // 3 days × 2000 = 6000 KHR
            var borrow3 = new Borrow
            {
                MemberId = members[2].MemberId,
                LibrarianId = librarians[2].LibrarianId,
                BorrowDate = DateTime.Today.AddDays(-15),
                DueDate = dueDate3,
                ReturnDate = returnDate3,
                FineAmount = fine3,
                Status = BorrowStatus.Returned
            };
            context.Borrows.Add(borrow3);
            context.SaveChanges();

            context.BorrowDetails.Add(
                new BorrowDetail { BorrowId = borrow3.BorrowId, BookId = books[6].BookId, Quantity = 1 }
            );
            // Book 7 (index 6) is already returned — copies not reduced

            context.SaveChanges();
        }
    }
}
