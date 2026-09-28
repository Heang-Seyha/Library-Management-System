using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    /// <summary>
    /// Seeds the database with a comprehensive, realistic, and production-ready mock dataset
    /// reflecting a Cambodian University Library.
    /// Seeds only on the first run when the database is empty to preserve user-created records across restarts.
    /// </summary>
    public static class DbInitializer
    {
        public static void Seed(LibraryDbContext context)
        {
            // ── Step 0: Connectivity Guard ──────────────────────────────────────
            if (!context.Database.CanConnect())
            {
                return;
            }

            // Check if the database has already been seeded
            if (context.Librarians.Any())
            {
                return; // Database already contains data; skip seeding to preserve manual changes
            }

            // ── Step 1: Seed Default Administrator Account ─────────────────────
            // Seed ONLY ONE default Administrator account as specified
            var admin = new Librarian
            {
                Name = "System Administrator",
                Phone = "012-888-999",
                Position = "Chief Librarian / Administrator",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            };
            context.Librarians.Add(admin);

            // ── Step 3: Seed Categories (7 Categories) ─────────────────────────
            var categories = new[]
            {
                new Category
                {
                    Name = "Computer Science & IT",
                    Description = "Programming languages, algorithms, cloud computing, cybersecurity, and computational systems."
                },
                new Category
                {
                    Name = "Software Engineering",
                    Description = "Software architecture, design patterns, clean code practices, testing, and agile workflows."
                },
                new Category
                {
                    Name = "Business & Economics",
                    Description = "Entrepreneurship, microeconomics, Southeast Asian markets, public finance, and trade policy."
                },
                new Category
                {
                    Name = "Cambodian History & Culture",
                    Description = "Khmer civilization, Angkorian heritage, traditional arts, post-war history, and national identity."
                },
                new Category
                {
                    Name = "Khmer Literature",
                    Description = "Classical Khmer prose, modernist novels, contemporary poetry, folktales, and literary anthologies."
                },
                new Category
                {
                    Name = "Science & Mathematics",
                    Description = "Calculus, discrete mathematics, linear algebra, applied physics, and statistics."
                },
                new Category
                {
                    Name = "Personal Development",
                    Description = "Leadership, effective communication, emotional intelligence, productivity, and career development."
                }
            };
            context.Categories.AddRange(categories);

            // ── Step 4: Seed Authors (14 Authors: 50% Cambodian, 50% International) ──
            var authors = new[]
            {
                // Cambodian Authors (7)
                new Author
                {
                    Name = "Rim Kin",
                    Bio = "Pioneering modern Khmer author and novelist, best known for Sophat (1938), widely recognized as the first modern Khmer novel."
                },
                new Author
                {
                    Name = "Nou Hach",
                    Bio = "Distinguished Cambodian diplomat and modernist writer, celebrated for the classic romance novel Phka Srapoun (Wilted Flower)."
                },
                new Author
                {
                    Name = "Soth Polin",
                    Bio = "Influential Cambodian existentialist novelist and philosopher, author of A Meaningless Life and The Dead Heart."
                },
                new Author
                {
                    Name = "Chuth Khay",
                    Bio = "Cambodian scholar, writer, and jurist, acclaimed for nostalgic memoirs of youth and poignant post-war literature."
                },
                new Author
                {
                    Name = "Pich Tum Kravel",
                    Bio = "Cambodian performing arts scholar, playwright, and director who dedicated his career to revitalizing Khmer traditional theatre and shadow puppetry."
                },
                new Author
                {
                    Name = "Dr. Hang Chuon Naron",
                    Bio = "Cambodian scholar, economist, and Minister of Education, Youth and Sport; author of authoritative treatises on Cambodian economy and fiscal history."
                },
                new Author
                {
                    Name = "Vann Molyvann",
                    Bio = "Visionary Cambodian architect and urban planner, pioneer of New Khmer Architecture, and author of Modern Khmer Cities."
                },

                // International Authors (7)
                new Author
                {
                    Name = "Robert C. Martin",
                    Bio = "Software craftsman and author, globally recognized for Clean Code and formulating the SOLID object-oriented principles."
                },
                new Author
                {
                    Name = "Martin Fowler",
                    Bio = "Chief Scientist at Thoughtworks, author of groundbreaking works on refactoring, enterprise application architecture, and microservices."
                },
                new Author
                {
                    Name = "Donald E. Knuth",
                    Bio = "Turing Award laureate, Stanford Professor Emeritus, and author of the landmark multi-volume work The Art of Computer Programming."
                },
                new Author
                {
                    Name = "Thomas H. Cormen",
                    Bio = "Dartmouth Computer Science professor and primary co-author of the premier textbook Introduction to Algorithms (CLRS)."
                },
                new Author
                {
                    Name = "Walter Isaacson",
                    Bio = "Acclaimed biographer, historian, and former CEO of the Aspen Institute; biographer of Steve Jobs, Albert Einstein, and Leonardo da Vinci."
                },
                new Author
                {
                    Name = "Peter Thiel",
                    Bio = "Venture capitalist, co-founder of PayPal and Palantir Technologies, and author of Zero to One: Notes on Startups."
                },
                new Author
                {
                    Name = "Andrew Hunt",
                    Bio = "Pioneer of the Agile software movement, co-author of The Pragmatic Programmer, and co-founder of The Pragmatic Bookshelf."
                }
            };
            context.Authors.AddRange(authors);

            // ── Step 5: Seed Publishers (7 Publishers: Majority Cambodian) ─────
            var publishers = new[]
            {
                // Cambodian Publishers (5)
                new Publisher
                {
                    Name = "Publishing and Distribution Department",
                    Address = "Corner of Norodom Blvd & St. 106, Khan Daun Penh, Phnom Penh, Cambodia",
                    Phone = "023-217-024"
                },
                new Publisher
                {
                    Name = "Bophana Center",
                    Address = "No. 64 St. 200, Khan Daun Penh, Phnom Penh, Cambodia",
                    Phone = "023-992-174"
                },
                new Publisher
                {
                    Name = "Sipar Books",
                    Address = "No. 9 St. 334, Khan Chamkarmon, Phnom Penh, Cambodia",
                    Phone = "023-212-407"
                },
                new Publisher
                {
                    Name = "Angkor Books Phnom Penh",
                    Address = "No. 222 Preah Sihanouk Blvd, Khan 7 Makara, Phnom Penh, Cambodia",
                    Phone = "023-880-991"
                },
                new Publisher
                {
                    Name = "Cambodia Book Center",
                    Address = "Building 32, St. 271, Khan Toul Kork, Phnom Penh, Cambodia",
                    Phone = "023-219-556"
                },

                // International Publishers (2)
                new Publisher
                {
                    Name = "O'Reilly Media",
                    Address = "Sebastopol, CA, USA / Regional Distribution, Phnom Penh",
                    Phone = "023-998-877"
                },
                new Publisher
                {
                    Name = "Pearson Education",
                    Address = "London, UK / Higher Education Services Cambodia",
                    Phone = "023-722-101"
                }
            };
            context.Publishers.AddRange(publishers);

            // Persist parents so their generated primary keys are available
            context.SaveChanges();

            // ── Step 6: Seed Books (26 Books: 50% Khmer Context, 50% International) ──
            var books = new[]
            {
                // ── Khmer Context & Literature (13 Books) ──
                new Book
                {
                    Title = "Sophat",
                    ISBN = CreateIsbn13("978-99950-0-101"),
                    Year = 1950,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[4].CategoryId, // Khmer Literature
                    AuthorId = authors[0].AuthorId,       // Rim Kin
                    PublisherId = publishers[0].PublisherId // Publishing and Distribution Dept
                },
                new Book
                {
                    Title = "Phka Srapoun",
                    ISBN = CreateIsbn13("978-99950-0-102"),
                    Year = 1953,
                    TotalCopies = 8,
                    AvailableCopies = 8,
                    CategoryId = categories[4].CategoryId, // Khmer Literature
                    AuthorId = authors[1].AuthorId,       // Nou Hach
                    PublisherId = publishers[0].PublisherId
                },
                new Book
                {
                    Title = "Mealea Duong Chett",
                    ISBN = CreateIsbn13("978-99950-0-103"),
                    Year = 1972,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[4].CategoryId, // Khmer Literature
                    AuthorId = authors[1].AuthorId,       // Nou Hach
                    PublisherId = publishers[3].PublisherId // Angkor Books
                },
                new Book
                {
                    Title = "A Meaningless Life",
                    ISBN = CreateIsbn13("978-99950-0-104"),
                    Year = 1965,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[4].CategoryId, // Khmer Literature
                    AuthorId = authors[2].AuthorId,       // Soth Polin
                    PublisherId = publishers[1].PublisherId // Bophana Center
                },
                new Book
                {
                    Title = "The Dead Heart",
                    ISBN = CreateIsbn13("978-99950-0-105"),
                    Year = 1973,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[4].CategoryId, // Khmer Literature
                    AuthorId = authors[2].AuthorId,       // Soth Polin
                    PublisherId = publishers[1].PublisherId
                },
                new Book
                {
                    Title = "The Pagoda Boy",
                    ISBN = CreateIsbn13("978-99950-0-106"),
                    Year = 2002,
                    TotalCopies = 7,
                    AvailableCopies = 7,
                    CategoryId = categories[3].CategoryId, // Cambodian History & Culture
                    AuthorId = authors[3].AuthorId,       // Chuth Khay
                    PublisherId = publishers[2].PublisherId // Sipar Books
                },
                new Book
                {
                    Title = "Shadow Theatre of Cambodia",
                    ISBN = CreateIsbn13("978-99950-0-107"),
                    Year = 2001,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[3].CategoryId, // Cambodian History & Culture
                    AuthorId = authors[4].AuthorId,       // Pich Tum Kravel
                    PublisherId = publishers[2].PublisherId
                },
                new Book
                {
                    Title = "Cambodian Economy: Charting the Course",
                    ISBN = CreateIsbn13("978-99950-0-108"),
                    Year = 2012,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[2].CategoryId, // Business & Economics
                    AuthorId = authors[5].AuthorId,       // Dr. Hang Chuon Naron
                    PublisherId = publishers[4].PublisherId // Cambodia Book Center
                },
                new Book
                {
                    Title = "Public Finance in Cambodia",
                    ISBN = CreateIsbn13("978-99950-0-109"),
                    Year = 2015,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[2].CategoryId, // Business & Economics
                    AuthorId = authors[5].AuthorId,       // Dr. Hang Chuon Naron
                    PublisherId = publishers[4].PublisherId
                },
                new Book
                {
                    Title = "Modern Khmer Architecture: 1953-1970",
                    ISBN = CreateIsbn13("978-99950-0-110"),
                    Year = 2008,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[3].CategoryId, // Cambodian History & Culture
                    AuthorId = authors[6].AuthorId,       // Vann Molyvann
                    PublisherId = publishers[1].PublisherId // Bophana Center
                },
                new Book
                {
                    Title = "Angkor and the Khmer Civilization",
                    ISBN = CreateIsbn13("978-99950-0-111"),
                    Year = 2003,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[3].CategoryId, // Cambodian History & Culture
                    AuthorId = authors[6].AuthorId,       // Vann Molyvann
                    PublisherId = publishers[3].PublisherId // Angkor Books
                },
                new Book
                {
                    Title = "Treasures of Khmer Folktales",
                    ISBN = CreateIsbn13("978-99950-0-112"),
                    Year = 1968,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[4].CategoryId, // Khmer Literature
                    AuthorId = authors[0].AuthorId,       // Rim Kin
                    PublisherId = publishers[2].PublisherId // Sipar Books
                },
                new Book
                {
                    Title = "Education Reform in Cambodia",
                    ISBN = CreateIsbn13("978-99950-0-113"),
                    Year = 2019,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[6].CategoryId, // Personal Development
                    AuthorId = authors[5].AuthorId,       // Dr. Hang Chuon Naron
                    PublisherId = publishers[4].PublisherId // Cambodia Book Center
                },

                // ── International Software, Science & Business (13 Books) ──
                new Book
                {
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                    ISBN = CreateIsbn13("978-0-132-35088"),
                    Year = 2008,
                    TotalCopies = 8,
                    AvailableCopies = 8,
                    CategoryId = categories[1].CategoryId, // Software Engineering
                    AuthorId = authors[7].AuthorId,       // Robert C. Martin
                    PublisherId = publishers[6].PublisherId // Pearson Education
                },
                new Book
                {
                    Title = "Clean Architecture: A Craftsman's Guide to Software Structure",
                    ISBN = CreateIsbn13("978-0-134-49416"),
                    Year = 2017,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[1].CategoryId, // Software Engineering
                    AuthorId = authors[7].AuthorId,       // Robert C. Martin
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "Refactoring: Improving the Design of Existing Code",
                    ISBN = CreateIsbn13("978-0-134-75759"),
                    Year = 2018,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[1].CategoryId, // Software Engineering
                    AuthorId = authors[8].AuthorId,       // Martin Fowler
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "Patterns of Enterprise Application Architecture",
                    ISBN = CreateIsbn13("978-0-321-12742"),
                    Year = 2002,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[1].CategoryId, // Software Engineering
                    AuthorId = authors[8].AuthorId,       // Martin Fowler
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "The Art of Computer Programming, Vol. 1: Fundamental Algorithms",
                    ISBN = CreateIsbn13("978-0-201-89683"),
                    Year = 1997,
                    TotalCopies = 4,
                    AvailableCopies = 4,
                    CategoryId = categories[0].CategoryId, // Computer Science & IT
                    AuthorId = authors[9].AuthorId,       // Donald E. Knuth
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "The Art of Computer Programming, Vol. 2: Seminumerical Algorithms",
                    ISBN = CreateIsbn13("978-0-201-89684"),
                    Year = 1998,
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    CategoryId = categories[0].CategoryId, // Computer Science & IT
                    AuthorId = authors[9].AuthorId,       // Donald E. Knuth
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "Introduction to Algorithms, Fourth Edition",
                    ISBN = CreateIsbn13("978-0-262-04630"),
                    Year = 2022,
                    TotalCopies = 7,
                    AvailableCopies = 7,
                    CategoryId = categories[0].CategoryId, // Computer Science & IT
                    AuthorId = authors[10].AuthorId,      // Thomas H. Cormen
                    PublisherId = publishers[5].PublisherId // O'Reilly Media
                },
                new Book
                {
                    Title = "Algorithms Unlocked",
                    ISBN = CreateIsbn13("978-0-262-53305"),
                    Year = 2013,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[0].CategoryId, // Computer Science & IT
                    AuthorId = authors[10].AuthorId,      // Thomas H. Cormen
                    PublisherId = publishers[5].PublisherId
                },
                new Book
                {
                    Title = "Steve Jobs",
                    ISBN = CreateIsbn13("978-1-451-64853"),
                    Year = 2011,
                    TotalCopies = 7,
                    AvailableCopies = 7,
                    CategoryId = categories[6].CategoryId, // Personal Development
                    AuthorId = authors[11].AuthorId,      // Walter Isaacson
                    PublisherId = publishers[5].PublisherId
                },
                new Book
                {
                    Title = "Leonardo da Vinci",
                    ISBN = CreateIsbn13("978-1-501-13915"),
                    Year = 2017,
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    CategoryId = categories[6].CategoryId, // Personal Development
                    AuthorId = authors[11].AuthorId,      // Walter Isaacson
                    PublisherId = publishers[5].PublisherId
                },
                new Book
                {
                    Title = "Zero to One: Notes on Startups, or How to Build the Future",
                    ISBN = CreateIsbn13("978-0-804-13929"),
                    Year = 2014,
                    TotalCopies = 8,
                    AvailableCopies = 8,
                    CategoryId = categories[2].CategoryId, // Business & Economics
                    AuthorId = authors[12].AuthorId,      // Peter Thiel
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "The Pragmatic Programmer: 20th Anniversary Edition",
                    ISBN = CreateIsbn13("978-0-135-95705"),
                    Year = 2019,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[1].CategoryId, // Software Engineering
                    AuthorId = authors[13].AuthorId,      // Andrew Hunt
                    PublisherId = publishers[6].PublisherId
                },
                new Book
                {
                    Title = "Discrete Mathematics and Its Applications",
                    ISBN = CreateIsbn13("978-0-073-38309"),
                    Year = 2019,
                    TotalCopies = 6,
                    AvailableCopies = 6,
                    CategoryId = categories[5].CategoryId, // Science & Mathematics
                    AuthorId = authors[9].AuthorId,       // Donald E. Knuth
                    PublisherId = publishers[6].PublisherId
                }
            };
            context.Books.AddRange(books);

            // ── Step 7: Seed Members (18 Members: All Cambodian Names & Cities) ─
            var members = new[]
            {
                new Member
                {
                    Name = "Chan Vicheka",
                    Phone = "012-345-678",
                    Email = "chan.vicheka@cambodia-edu.kh",
                    Address = "Khan Toul Kork, Phnom Penh",
                    JoinDate = new DateTime(2023, 3, 15)
                },
                new Member
                {
                    Name = "Keo Sarath",
                    Phone = "011-234-567",
                    Email = "keo.sarath@cambodia-edu.kh",
                    Address = "Khan Daun Penh, Phnom Penh",
                    JoinDate = new DateTime(2023, 5, 20)
                },
                new Member
                {
                    Name = "Seng Dara",
                    Phone = "015-678-901",
                    Email = "seng.dara@cambodia-edu.kh",
                    Address = "Khan Sen Sok, Phnom Penh",
                    JoinDate = new DateTime(2023, 8, 11)
                },
                new Member
                {
                    Name = "Meas Bopha",
                    Phone = "078-901-234",
                    Email = "meas.bopha@cambodia-edu.kh",
                    Address = "Krong Siem Reap, Siem Reap",
                    JoinDate = new DateTime(2023, 11, 4)
                },
                new Member
                {
                    Name = "Kim Heang",
                    Phone = "096-345-678",
                    Email = "kim.heang@cambodia-edu.kh",
                    Address = "Krong Battambang, Battambang",
                    JoinDate = new DateTime(2024, 1, 18)
                },
                new Member
                {
                    Name = "Sok Chenda",
                    Phone = "085-456-789",
                    Email = "sok.chenda@cambodia-edu.kh",
                    Address = "Khan Chamkarmon, Phnom Penh",
                    JoinDate = new DateTime(2024, 2, 22)
                },
                new Member
                {
                    Name = "Tep Vanna",
                    Phone = "077-567-890",
                    Email = "tep.vanna@cambodia-edu.kh",
                    Address = "Krong Ta Khmau, Kandal",
                    JoinDate = new DateTime(2024, 3, 30)
                },
                new Member
                {
                    Name = "Ouk Panha",
                    Phone = "093-456-789",
                    Email = "ouk.panha@cambodia-edu.kh",
                    Address = "Khan Chroy Changvar, Phnom Penh",
                    JoinDate = new DateTime(2024, 5, 14)
                },
                new Member
                {
                    Name = "Rath Sovann",
                    Phone = "010-889-911",
                    Email = "rath.sovann@cambodia-edu.kh",
                    Address = "Khan Boeng Keng Kang, Phnom Penh",
                    JoinDate = new DateTime(2024, 7, 9)
                },
                new Member
                {
                    Name = "Chhorn Piseth",
                    Phone = "012-771-122",
                    Email = "chhorn.piseth@cambodia-edu.kh",
                    Address = "Khan Russey Keo, Phnom Penh",
                    JoinDate = new DateTime(2024, 9, 12)
                },
                new Member
                {
                    Name = "Ly Sreynoch",
                    Phone = "016-554-433",
                    Email = "ly.sreynoch@cambodia-edu.kh",
                    Address = "Krong Kampong Cham, Kampong Cham",
                    JoinDate = new DateTime(2024, 11, 5)
                },
                new Member
                {
                    Name = "Heng Samnang",
                    Phone = "069-443-322",
                    Email = "heng.samnang@cambodia-edu.kh",
                    Address = "Khan Pur Senchey, Phnom Penh",
                    JoinDate = new DateTime(2025, 1, 10)
                },
                new Member
                {
                    Name = "Prak Kolab",
                    Phone = "089-332-211",
                    Email = "prak.kolab@cambodia-edu.kh",
                    Address = "Krong Kampot, Kampot",
                    JoinDate = new DateTime(2025, 3, 15)
                },
                new Member
                {
                    Name = "Nget Makara",
                    Phone = "070-221-199",
                    Email = "nget.makara@cambodia-edu.kh",
                    Address = "Khan Meanchey, Phnom Penh",
                    JoinDate = new DateTime(2025, 5, 20)
                },
                new Member
                {
                    Name = "Ros Chantrea",
                    Phone = "098-112-233",
                    Email = "ros.chantrea@cambodia-edu.kh",
                    Address = "Krong Sihanoukville, Preah Sihanouk",
                    JoinDate = new DateTime(2025, 8, 1)
                },
                new Member
                {
                    Name = "Khuon Visal",
                    Phone = "092-667-788",
                    Email = "khuon.visal@cambodia-edu.kh",
                    Address = "Khan Prek Pnov, Phnom Penh",
                    JoinDate = new DateTime(2025, 10, 18)
                },
                new Member
                {
                    Name = "Chea Sreymao",
                    Phone = "095-778-899",
                    Email = "chea.sreymao@cambodia-edu.kh",
                    Address = "Krong Chbar Mon, Kampong Speu",
                    JoinDate = new DateTime(2026, 1, 12)
                },
                new Member
                {
                    Name = "Vannak Boramey",
                    Phone = "087-889-900",
                    Email = "vannak.boramey@cambodia-edu.kh",
                    Address = "Khan Toul Kork, Phnom Penh",
                    JoinDate = new DateTime(2026, 2, 25)
                }
            };
            context.Members.AddRange(members);

            // Persist books and members so their IDs are generated
            context.SaveChanges();

            // ── Step 8: Seed Borrows & BorrowDetails (15 Realistic Transactions) ─
            // Processed exclusively by the single Admin librarian
            var today = DateTime.Today;

            // ── Active Borrows (~5 records, due in the future, fine = 0, status = Active) ──
            var activeBorrow1 = new Borrow
            {
                MemberId = members[0].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-4),
                DueDate = today.AddDays(10),
                ReturnDate = null,
                FineAmount = 0m,
                Status = BorrowStatus.Active
            };
            var activeBorrow2 = new Borrow
            {
                MemberId = members[1].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-6),
                DueDate = today.AddDays(8),
                ReturnDate = null,
                FineAmount = 0m,
                Status = BorrowStatus.Active
            };
            var activeBorrow3 = new Borrow
            {
                MemberId = members[2].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-2),
                DueDate = today.AddDays(12),
                ReturnDate = null,
                FineAmount = 0m,
                Status = BorrowStatus.Active
            };
            var activeBorrow4 = new Borrow
            {
                MemberId = members[3].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-5),
                DueDate = today.AddDays(9),
                ReturnDate = null,
                FineAmount = 0m,
                Status = BorrowStatus.Active
            };
            var activeBorrow5 = new Borrow
            {
                MemberId = members[4].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-1),
                DueDate = today.AddDays(13),
                ReturnDate = null,
                FineAmount = 0m,
                Status = BorrowStatus.Active
            };

            // ── Overdue Borrows (~5 records, borrowed 20-30 days ago, due 5-15 days ago, status = Overdue) ──
            var overdueBorrow1 = new Borrow
            {
                MemberId = members[5].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-22),
                DueDate = today.AddDays(-8),
                ReturnDate = null,
                FineAmount = FinePolicy.CalculateFine(today.AddDays(-8), today),
                Status = BorrowStatus.Overdue
            };
            var overdueBorrow2 = new Borrow
            {
                MemberId = members[6].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-25),
                DueDate = today.AddDays(-11),
                ReturnDate = null,
                FineAmount = FinePolicy.CalculateFine(today.AddDays(-11), today),
                Status = BorrowStatus.Overdue
            };
            var overdueBorrow3 = new Borrow
            {
                MemberId = members[7].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-28),
                DueDate = today.AddDays(-14),
                ReturnDate = null,
                FineAmount = FinePolicy.CalculateFine(today.AddDays(-14), today),
                Status = BorrowStatus.Overdue
            };
            var overdueBorrow4 = new Borrow
            {
                MemberId = members[8].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-20),
                DueDate = today.AddDays(-6),
                ReturnDate = null,
                FineAmount = FinePolicy.CalculateFine(today.AddDays(-6), today),
                Status = BorrowStatus.Overdue
            };
            var overdueBorrow5 = new Borrow
            {
                MemberId = members[9].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-26),
                DueDate = today.AddDays(-12),
                ReturnDate = null,
                FineAmount = FinePolicy.CalculateFine(today.AddDays(-12), today),
                Status = BorrowStatus.Overdue
            };

            // ── Returned Borrows (~5 records, historical dates, ReturnDate set, status = Returned) ──
            var returnDueDate1 = today.AddDays(-31);
            var returnActualDate1 = today.AddDays(-33); // On time -> 0 fine
            var returnedBorrow1 = new Borrow
            {
                MemberId = members[10].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-45),
                DueDate = returnDueDate1,
                ReturnDate = returnActualDate1,
                FineAmount = FinePolicy.CalculateFine(returnDueDate1, returnActualDate1),
                Status = BorrowStatus.Returned
            };

            var returnDueDate2 = today.AddDays(-46);
            var returnActualDate2 = today.AddDays(-42); // 4 days late -> 8,000 KHR fine
            var returnedBorrow2 = new Borrow
            {
                MemberId = members[11].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-60),
                DueDate = returnDueDate2,
                ReturnDate = returnActualDate2,
                FineAmount = FinePolicy.CalculateFine(returnDueDate2, returnActualDate2),
                Status = BorrowStatus.Returned
            };

            var returnDueDate3 = today.AddDays(-76);
            var returnActualDate3 = today.AddDays(-78); // On time -> 0 fine
            var returnedBorrow3 = new Borrow
            {
                MemberId = members[12].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-90),
                DueDate = returnDueDate3,
                ReturnDate = returnActualDate3,
                FineAmount = FinePolicy.CalculateFine(returnDueDate3, returnActualDate3),
                Status = BorrowStatus.Returned
            };

            var returnDueDate4 = today.AddDays(-96);
            var returnActualDate4 = today.AddDays(-90); // 6 days late -> 12,000 KHR fine
            var returnedBorrow4 = new Borrow
            {
                MemberId = members[13].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-110),
                DueDate = returnDueDate4,
                ReturnDate = returnActualDate4,
                FineAmount = FinePolicy.CalculateFine(returnDueDate4, returnActualDate4),
                Status = BorrowStatus.Returned
            };

            var returnDueDate5 = today.AddDays(-126);
            var returnActualDate5 = today.AddDays(-128); // On time -> 0 fine
            var returnedBorrow5 = new Borrow
            {
                MemberId = members[14].MemberId,
                LibrarianId = admin.LibrarianId,
                BorrowDate = today.AddDays(-140),
                DueDate = returnDueDate5,
                ReturnDate = returnActualDate5,
                FineAmount = FinePolicy.CalculateFine(returnDueDate5, returnActualDate5),
                Status = BorrowStatus.Returned
            };

            var allBorrows = new[]
            {
                activeBorrow1, activeBorrow2, activeBorrow3, activeBorrow4, activeBorrow5,
                overdueBorrow1, overdueBorrow2, overdueBorrow3, overdueBorrow4, overdueBorrow5,
                returnedBorrow1, returnedBorrow2, returnedBorrow3, returnedBorrow4, returnedBorrow5
            };
            context.Borrows.AddRange(allBorrows);
            context.SaveChanges();

            // ── Line Items (BorrowDetails) & AvailableCopies Decrement ───────────
            var borrowDetails = new List<BorrowDetail>
            {
                // Active Borrows (decrease available copies)
                new BorrowDetail { BorrowId = activeBorrow1.BorrowId, BookId = books[0].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = activeBorrow2.BorrowId, BookId = books[13].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = activeBorrow3.BorrowId, BookId = books[19].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = activeBorrow4.BorrowId, BookId = books[1].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = activeBorrow4.BorrowId, BookId = books[21].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = activeBorrow5.BorrowId, BookId = books[23].BookId, Quantity = 1 },

                // Overdue Borrows (decrease available copies)
                new BorrowDetail { BorrowId = overdueBorrow1.BorrowId, BookId = books[5].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = overdueBorrow2.BorrowId, BookId = books[14].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = overdueBorrow3.BorrowId, BookId = books[7].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = overdueBorrow4.BorrowId, BookId = books[17].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = overdueBorrow5.BorrowId, BookId = books[24].BookId, Quantity = 1 },

                // Returned Borrows (already returned to shelves — do NOT reduce available copies)
                new BorrowDetail { BorrowId = returnedBorrow1.BorrowId, BookId = books[2].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = returnedBorrow2.BorrowId, BookId = books[15].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = returnedBorrow3.BorrowId, BookId = books[6].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = returnedBorrow4.BorrowId, BookId = books[9].BookId, Quantity = 1 },
                new BorrowDetail { BorrowId = returnedBorrow5.BorrowId, BookId = books[22].BookId, Quantity = 1 }
            };
            context.BorrowDetails.AddRange(borrowDetails);

            // Deduct AvailableCopies for currently unreturned (Active & Overdue) books
            books[0].AvailableCopies -= 1;  // Sophat
            books[13].AvailableCopies -= 1; // Clean Code
            books[19].AvailableCopies -= 1; // Introduction to Algorithms
            books[1].AvailableCopies -= 1;  // Phka Srapoun
            books[21].AvailableCopies -= 1; // Steve Jobs
            books[23].AvailableCopies -= 1; // Zero to One

            books[5].AvailableCopies -= 1;  // The Pagoda Boy
            books[14].AvailableCopies -= 1; // Clean Architecture
            books[7].AvailableCopies -= 1;  // Cambodian Economy
            books[17].AvailableCopies -= 1; // TAOCP Vol 1
            books[24].AvailableCopies -= 1; // The Pragmatic Programmer

            context.SaveChanges();
        }

        /// <summary>
        /// Generates a valid ISBN-13 string with mathematically correct Modulo-10 check digit.
        /// Guaranteed to pass checksum validation in ValidationHelper.IsValidISBN.
        /// </summary>
        private static string CreateIsbn13(string prefix12)
        {
            var clean = prefix12.Replace("-", "").Replace(" ", "").Trim();
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = clean[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }
            int checkDigit = (10 - (sum % 10)) % 10;
            return $"{prefix12}-{checkDigit}";
        }
    }
}
