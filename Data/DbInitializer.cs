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
                bool changed = false;

                // Normalize any legacy "Employee" roles to "Librarian"
                var legacyEmployees = context.Librarians.Where(l => l.Role == "Employee").ToList();
                foreach (var emp in legacyEmployees)
                {
                    emp.Role = "Librarian";
                    changed = true;
                }

                // Authoritative Business Rule: Exactly ONE Admin account exists (username 'admin').
                // Demote any stray or secondary accounts with Role == "Admin" to "Librarian".
                var extraAdmins = context.Librarians
                    .Where(l => l.Role == "Admin" && l.Username.ToLower() != "admin")
                    .ToList();
                foreach (var extra in extraAdmins)
                {
                    extra.Role = "Librarian";
                    changed = true;
                }

                // Normalize/assign realistic Gender, DateOfBirth, Email & Phone for existing librarians/admins
                var existingLibrarians = context.Librarians.ToList();
                foreach (var l in existingLibrarians)
                {
                    if (string.IsNullOrEmpty(l.Gender))
                    {
                        l.Gender = "Male";
                        changed = true;
                    }
                    if (l.DateOfBirth == null)
                    {
                        l.DateOfBirth = l.Role == "Admin" ? new DateTime(1985, 5, 12) : new DateTime(1992, 8, 20);
                        changed = true;
                    }
                    if (string.IsNullOrEmpty(l.Email))
                    {
                        l.Email = l.Role == "Admin" ? "admin@library.gov.kh" : $"{l.Username.ToLower()}@library.gov.kh";
                        changed = true;
                    }
                    if (string.IsNullOrEmpty(l.Phone))
                    {
                        l.Phone = l.Role == "Admin" ? "012-888-999" : "012-777-666";
                        changed = true;
                    }
                }

                // Normalize/assign realistic Gender & DateOfBirth for existing members
                var existingMembers = context.Members.ToList();
                for (int i = 0; i < existingMembers.Count; i++)
                {
                    var m = existingMembers[i];
                    if (string.IsNullOrEmpty(m.Gender) || m.Gender == "Male")
                    {
                        if (m.Name.Contains("Bopha") || m.Name.Contains("Chenda") || m.Name.Contains("Sreynoch") ||
                            m.Name.Contains("Kolab") || m.Name.Contains("Chantrea") || m.Name.Contains("Sreymao") ||
                            m.Name.Contains("Boramey") || m.Name.Contains("Kalyan") || m.Name.Contains("Dany"))
                        {
                            m.Gender = "Female";
                            changed = true;
                        }
                        else if (string.IsNullOrEmpty(m.Gender))
                        {
                            m.Gender = "Male";
                            changed = true;
                        }
                    }
                    if (m.DateOfBirth == null)
                    {
                        m.DateOfBirth = new DateTime(1998 + (i % 8), 1 + (i % 12), 1 + (i % 25));
                        changed = true;
                    }
                }

                // Normalize/assign realistic Gender & DateOfBirth for existing authors
                var existingAuthors = context.Authors.ToList();
                foreach (var a in existingAuthors)
                {
                    if (string.IsNullOrEmpty(a.Gender))
                    {
                        a.Gender = "Male";
                        changed = true;
                    }
                    if (a.DateOfBirth == null)
                    {
                        if (a.Name.Contains("Knuth")) a.DateOfBirth = new DateTime(1938, 1, 10);
                        else if (a.Name.Contains("Martin")) a.DateOfBirth = new DateTime(1952, 12, 5);
                        else if (a.Name.Contains("Fowler")) a.DateOfBirth = new DateTime(1963, 12, 18);
                        else if (a.Name.Contains("Rim Kin")) a.DateOfBirth = new DateTime(1911, 1, 1);
                        else if (a.Name.Contains("Nou Hach")) a.DateOfBirth = new DateTime(1916, 6, 26);
                        else a.DateOfBirth = new DateTime(1955, 6, 15);
                        changed = true;
                    }
                }

                if (changed)
                {
                    context.SaveChanges();
                }

                return; // Database already contains data; skip full seeding to preserve manual changes
            }

            // ── Step 1: Seed Default Accounts ──────────────────────────────────
            var admin = new Librarian
            {
                Name = "System Administrator",
                Gender = "Male",
                DateOfBirth = new DateTime(1985, 5, 12),
                Phone = "012-888-999",
                Email = "admin@library.gov.kh",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            };
            context.Librarians.Add(admin);

            // ── Step 2: Seed New Librarians ────────────────────────────────────
            var librarians = new[]
            {
                new Librarian
                {
                    Name = "Heang Seyha",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1999, 4, 12),
                    Phone = "010-234-567",
                    Email = "seyha@gmail.com",
                    Username = "seyha",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("librarian123"),
                    Role = "Librarian"
                },
                new Librarian
                {
                    Name = "Heng Daravathanak",
                    Gender = "Male",
                    DateOfBirth = new DateTime(2001, 8, 25),
                    Phone = "015-345-678",
                    Email = "dara@gmail.com",
                    Username = "dara",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("librarian123"),
                    Role = "Librarian"
                },
                new Librarian
                {
                    Name = "Lay Sali",
                    Gender = "Male",
                    DateOfBirth = new DateTime(2003, 11, 3),
                    Phone = "016-456-789",
                    Email = "sali@gmail.com",
                    Username = "sali",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("librarian123"),
                    Role = "Librarian"
                }
            };
            context.Librarians.AddRange(librarians);
            context.SaveChanges();

            // ── Step 3: Seed Categories (10 Categories) ────────────────────────
            var categories = new[]
            {
                new Category { Name = "Khmer Literature", Description = "Classic and modern Khmer prose, novels, poetry, and folktales." },
                new Category { Name = "Khmer History",    Description = "Ancient Angkor civilization, post-colonial era, and heritage." },
                new Category { Name = "Computer Science", Description = "Theoretical computing, algorithms, architecture, and systems." },
                new Category { Name = "Software Eng.",    Description = "System design, clean code, agile practices, and architecture." },
                new Category { Name = "Business",         Description = "Leadership, corporate finance, market strategy, and economics." },
                new Category { Name = "Mathematics",      Description = "Calculus, discrete mathematics, linear algebra, and logic." },
                new Category { Name = "Science",          Description = "Applied physics, chemistry, biology, and environmental study." },
                new Category { Name = "Self-Development", Description = "Habit formation, communication skills, mindset, and focus." },
                new Category { Name = "Biography",        Description = "Memoirs and biographies of historic leaders and visionaries." },
                new Category { Name = "Fiction",          Description = "Classic literature, science fiction, and modern short stories." }
            };
            context.Categories.AddRange(categories);

            // ── Step 4: Seed Authors (22 Authors: 50% Cambodian, 50% International) ──
            var authors = new[]
            {
                // Cambodian Authors (11)
                new Author { Name = "Rim Kin",          Gender = "Male",   DateOfBirth = new DateTime(1911, 1, 1),   Bio = "Pioneering novelist behind Sophat." },
                new Author { Name = "Nou Hach",          Gender = "Male",   DateOfBirth = new DateTime(1916, 6, 26),  Bio = "Modernist writer of Phka Srapoun." },
                new Author { Name = "Pich Tum Krovil",  Gender = "Male",   DateOfBirth = new DateTime(1943, 6, 2),   Bio = "Scholar of Khmer shadow theatre." },
                new Author { Name = "Kong Bunchhoeun",   Gender = "Male",   DateOfBirth = new DateTime(1939, 11, 23), Bio = "Prolific Battambang novelist." },
                new Author { Name = "Vann Molyvann",     Gender = "Male",   DateOfBirth = new DateTime(1926, 11, 23), Bio = "Legendary architect and urbanist." },
                new Author { Name = "Chuth Khay",        Gender = "Male",   DateOfBirth = new DateTime(1940, 4, 5),   Bio = "Renowned author of pagoda memoirs." },
                new Author { Name = "Soth Polin",        Gender = "Male",   DateOfBirth = new DateTime(1943, 2, 9),   Bio = "Philosopher and existential writer." },
                new Author { Name = "Hang Chuon Naron",  Gender = "Male",   DateOfBirth = new DateTime(1962, 1, 16),  Bio = "Cambodian scholar and economist." },
                new Author { Name = "Ouch Kalyan",       Gender = "Female", DateOfBirth = new DateTime(1974, 5, 18),  Bio = "Contemporary Cambodian essayist." },
                new Author { Name = "Pal Vannarirak",    Gender = "Female", DateOfBirth = new DateTime(1954, 7, 21),  Bio = "Prize-winning Cambodian novelist." },
                new Author { Name = "You Bo",            Gender = "Male",   DateOfBirth = new DateTime(1946, 9, 14),  Bio = "President of Khmer Writers Assoc." },

                // International Authors (11)
                new Author { Name = "Robert C. Martin",  Gender = "Male",   DateOfBirth = new DateTime(1952, 12, 5),  Bio = "Co-author of Agile Manifesto." },
                new Author { Name = "Martin Fowler",     Gender = "Male",   DateOfBirth = new DateTime(1963, 12, 18), Bio = "Pioneer in software refactoring." },
                new Author { Name = "Donald Knuth",      Gender = "Male",   DateOfBirth = new DateTime(1938, 1, 10),  Bio = "Author of The Art of Programming." },
                new Author { Name = "Thomas Cormen",     Gender = "Male",   DateOfBirth = new DateTime(1956, 3, 30),  Bio = "Primary author of CLRS textbook." },
                new Author { Name = "Walter Isaacson",   Gender = "Male",   DateOfBirth = new DateTime(1952, 5, 20),  Bio = "Biographer of Jobs and Da Vinci." },
                new Author { Name = "Dale Carnegie",     Gender = "Male",   DateOfBirth = new DateTime(1888, 11, 24), Bio = "Pioneer in corporate public speaking." },
                new Author { Name = "George Orwell",     Gender = "Male",   DateOfBirth = new DateTime(1903, 6, 25),  Bio = "Novelist known for dystopian satire." },
                new Author { Name = "Paulo Coelho",      Gender = "Male",   DateOfBirth = new DateTime(1947, 8, 24),  Bio = "Brazilian author of The Alchemist." },
                new Author { Name = "Ada Lovelace",      Gender = "Female", DateOfBirth = new DateTime(1815, 12, 10), Bio = "World's first computer programmer." },
                new Author { Name = "Marie Curie",       Gender = "Female", DateOfBirth = new DateTime(1867, 11, 7),  Bio = "Nobel laureate in physics & chemistry." },
                new Author { Name = "Frank Herbert",     Gender = "Male",   DateOfBirth = new DateTime(1920, 10, 8),  Bio = "Science fiction creator of Dune." }
            };
            context.Authors.AddRange(authors);

            // ── Step 5: Seed Publishers (9 Publishers: Majority Cambodian) ─────
            var publishers = new[]
            {
                // Cambodian Publishers (7)
                new Publisher { Name = "Sipar Books",    Address = "No. 9 St. 334, Chamkarmon, Phnom Penh",         Phone = "023-212-407" },
                new Publisher { Name = "Angkor Books",   Address = "No. 222 Sihanouk Blvd, 7 Makara, Phnom Penh",   Phone = "023-880-991" },
                new Publisher { Name = "Bophana Media",  Address = "No. 64 St. 200, Daun Penh, Phnom Penh",         Phone = "023-992-174" },
                new Publisher { Name = "CBC Books",      Address = "Building 32, St. 271, Toul Kork, Phnom Penh",   Phone = "023-219-556" },
                new Publisher { Name = "SR Publishing",  Address = "Wat Bo Village, Sala Kamreuk, Siem Reap",       Phone = "063-964-123" },
                new Publisher { Name = "BTB Books",      Address = "St. 1.5, Svay Por, Battambang",                 Phone = "053-952-456" },
                new Publisher { Name = "MoEYS Publish",  Address = "Corner Norodom & St. 106, Daun Penh, Phnom Penh", Phone = "023-217-024" },

                // International Publishers (2)
                new Publisher { Name = "O'Reilly",       Address = "1005 Gravenstein Hwy N, Sebastopol, CA, USA",   Phone = "023-998-877" },
                new Publisher { Name = "Pearson",        Address = "80 Strand, London, WC2R 0RL, United Kingdom",   Phone = "023-722-101" }
            };
            context.Publishers.AddRange(publishers);
            context.SaveChanges();

            // ── Step 6: Seed Books (50 Books: 24 Cambodian Context, 26 International) ──
            var books = new[]
            {
                // ── Category 0: Khmer Literature (6 books) ──
                new Book { Title = "Sophat",             ISBN = CreateIsbn13("978-1-01-100001"), Year = 1950, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[0].CategoryId, AuthorId = authors[0].AuthorId, PublisherId = publishers[0].PublisherId },
                new Book { Title = "Phka Srapoun",       ISBN = CreateIsbn13("978-1-01-100002"), Year = 1953, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[0].CategoryId, AuthorId = authors[1].AuthorId, PublisherId = publishers[0].PublisherId },
                new Book { Title = "Mealea Duong Chett", ISBN = CreateIsbn13("978-1-01-100003"), Year = 1972, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[0].CategoryId, AuthorId = authors[1].AuthorId, PublisherId = publishers[1].PublisherId },
                new Book { Title = "Kolab Pailin",       ISBN = CreateIsbn13("978-1-01-100004"), Year = 1943, TotalCopies = 6, AvailableCopies = 6, CategoryId = categories[0].CategoryId, AuthorId = authors[0].AuthorId, PublisherId = publishers[0].PublisherId },
                new Book { Title = "Tum Teav",           ISBN = CreateIsbn13("978-1-01-100005"), Year = 1965, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[0].CategoryId, AuthorId = authors[2].AuthorId, PublisherId = publishers[1].PublisherId },
                new Book { Title = "Kakey",              ISBN = CreateIsbn13("978-1-01-100006"), Year = 1960, TotalCopies = 1, AvailableCopies = 1, CategoryId = categories[0].CategoryId, AuthorId = authors[3].AuthorId, PublisherId = publishers[5].PublisherId },

                // ── Category 1: Khmer History (7 books) ──
                new Book { Title = "Shadow Theatre",     ISBN = CreateIsbn13("978-1-01-100007"), Year = 2001, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[1].CategoryId, AuthorId = authors[2].AuthorId, PublisherId = publishers[2].PublisherId },
                new Book { Title = "Angkor Civilization",ISBN = CreateIsbn13("978-1-01-100008"), Year = 2003, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[1].CategoryId, AuthorId = authors[4].AuthorId, PublisherId = publishers[1].PublisherId },
                new Book { Title = "Modern Architecture",ISBN = CreateIsbn13("978-1-01-100009"), Year = 2008, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[1].CategoryId, AuthorId = authors[4].AuthorId, PublisherId = publishers[2].PublisherId },
                new Book { Title = "The Pagoda Boy",     ISBN = CreateIsbn13("978-1-01-100010"), Year = 2002, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[1].CategoryId, AuthorId = authors[5].AuthorId, PublisherId = publishers[0].PublisherId },
                new Book { Title = "The Lost Heritage",  ISBN = CreateIsbn13("978-1-01-100011"), Year = 2010, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[1].CategoryId, AuthorId = authors[5].AuthorId, PublisherId = publishers[2].PublisherId },
                new Book { Title = "Monsoon Rain",       ISBN = CreateIsbn13("978-1-01-100012"), Year = 2016, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[1].CategoryId, AuthorId = authors[8].AuthorId, PublisherId = publishers[3].PublisherId },
                new Book { Title = "City Lights",        ISBN = CreateIsbn13("978-1-01-100013"), Year = 2019, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[1].CategoryId, AuthorId = authors[8].AuthorId, PublisherId = publishers[3].PublisherId },

                // ── Category 4: Business (3 books) ──
                new Book { Title = "Cambodian Economy",  ISBN = CreateIsbn13("978-1-01-100014"), Year = 2012, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[4].CategoryId, AuthorId = authors[7].AuthorId, PublisherId = publishers[3].PublisherId },
                new Book { Title = "Public Finance",     ISBN = CreateIsbn13("978-1-01-100015"), Year = 2015, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[4].CategoryId, AuthorId = authors[7].AuthorId, PublisherId = publishers[3].PublisherId },
                new Book { Title = "Win Friends",        ISBN = CreateIsbn13("978-1-01-100016"), Year = 1936, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[4].CategoryId, AuthorId = authors[16].AuthorId,PublisherId = publishers[6].PublisherId },

                // ── Category 8: Biography (4 books) ──
                new Book { Title = "A Meaningless Life", ISBN = CreateIsbn13("978-1-01-100017"), Year = 1965, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[8].CategoryId, AuthorId = authors[6].AuthorId, PublisherId = publishers[2].PublisherId },
                new Book { Title = "The Dead Heart",     ISBN = CreateIsbn13("978-1-01-100018"), Year = 1973, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[8].CategoryId, AuthorId = authors[6].AuthorId, PublisherId = publishers[2].PublisherId },
                new Book { Title = "Steve Jobs",         ISBN = CreateIsbn13("978-1-01-100019"), Year = 2011, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[8].CategoryId, AuthorId = authors[15].AuthorId,PublisherId = publishers[7].PublisherId },
                new Book { Title = "Leonardo da Vinci",  ISBN = CreateIsbn13("978-1-01-100020"), Year = 2017, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[8].CategoryId, AuthorId = authors[15].AuthorId,PublisherId = publishers[7].PublisherId },

                // ── Category 9: Fiction (11 books) ──
                new Book { Title = "The Moonlit Lake",   ISBN = CreateIsbn13("978-1-01-100021"), Year = 1970, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[9].CategoryId, AuthorId = authors[3].AuthorId, PublisherId = publishers[5].PublisherId },
                new Book { Title = "Champa Sak",         ISBN = CreateIsbn13("978-1-01-100022"), Year = 1974, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[9].CategoryId, AuthorId = authors[3].AuthorId, PublisherId = publishers[5].PublisherId },
                new Book { Title = "River of Secrets",   ISBN = CreateIsbn13("978-1-01-100023"), Year = 1993, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[9].CategoryId, AuthorId = authors[3].AuthorId, PublisherId = publishers[5].PublisherId },
                new Book { Title = "Dark Evening",       ISBN = CreateIsbn13("978-1-01-100024"), Year = 1989, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[9].CategoryId, AuthorId = authors[9].AuthorId, PublisherId = publishers[4].PublisherId },
                new Book { Title = "Tearful Smile",      ISBN = CreateIsbn13("978-1-01-100025"), Year = 1995, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[9].CategoryId, AuthorId = authors[9].AuthorId, PublisherId = publishers[4].PublisherId },
                new Book { Title = "Flower of Hope",     ISBN = CreateIsbn13("978-1-01-100026"), Year = 2005, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[9].CategoryId, AuthorId = authors[9].AuthorId, PublisherId = publishers[4].PublisherId },
                new Book { Title = "Battambang Memoir",  ISBN = CreateIsbn13("978-1-01-100027"), Year = 1998, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[9].CategoryId, AuthorId = authors[9].AuthorId, PublisherId = publishers[5].PublisherId },
                new Book { Title = "1984",               ISBN = CreateIsbn13("978-1-01-100028"), Year = 1949, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[9].CategoryId, AuthorId = authors[17].AuthorId,PublisherId = publishers[0].PublisherId },
                new Book { Title = "Animal Farm",        ISBN = CreateIsbn13("978-1-01-100029"), Year = 1945, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[9].CategoryId, AuthorId = authors[17].AuthorId,PublisherId = publishers[0].PublisherId },
                new Book { Title = "Dune",               ISBN = CreateIsbn13("978-1-01-100030"), Year = 1965, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[9].CategoryId, AuthorId = authors[21].AuthorId,PublisherId = publishers[7].PublisherId },
                new Book { Title = "Dune Messiah",       ISBN = CreateIsbn13("978-1-01-100031"), Year = 1969, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[9].CategoryId, AuthorId = authors[21].AuthorId,PublisherId = publishers[7].PublisherId },

                // ── Category 2: Computer Science (3 books) ──
                new Book { Title = "TAOCP Vol. 1",       ISBN = CreateIsbn13("978-1-01-100032"), Year = 1997, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[2].CategoryId, AuthorId = authors[13].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Intro to Algorithms",ISBN = CreateIsbn13("978-1-01-100033"), Year = 2022, TotalCopies = 6, AvailableCopies = 6, CategoryId = categories[2].CategoryId, AuthorId = authors[14].AuthorId,PublisherId = publishers[7].PublisherId },
                new Book { Title = "Algorithms Unlocked",ISBN = CreateIsbn13("978-1-01-100034"), Year = 2013, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[2].CategoryId, AuthorId = authors[14].AuthorId,PublisherId = publishers[7].PublisherId },

                // ── Category 3: Software Eng. (6 books) ──
                new Book { Title = "Clean Code",         ISBN = CreateIsbn13("978-1-01-100035"), Year = 2008, TotalCopies = 2, AvailableCopies = 2, CategoryId = categories[3].CategoryId, AuthorId = authors[11].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Clean Architecture", ISBN = CreateIsbn13("978-1-01-100036"), Year = 2017, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[3].CategoryId, AuthorId = authors[11].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "The Clean Coder",    ISBN = CreateIsbn13("978-1-01-100037"), Year = 2011, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[3].CategoryId, AuthorId = authors[11].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Agile Principles",   ISBN = CreateIsbn13("978-1-01-100038"), Year = 2006, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[3].CategoryId, AuthorId = authors[11].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Refactoring",        ISBN = CreateIsbn13("978-1-01-100039"), Year = 2018, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[3].CategoryId, AuthorId = authors[12].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Enterprise Patterns",ISBN = CreateIsbn13("978-1-01-100040"), Year = 2002, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[3].CategoryId, AuthorId = authors[12].AuthorId,PublisherId = publishers[8].PublisherId },

                // ── Category 5: Mathematics (3 books) ──
                new Book { Title = "Discrete Math",      ISBN = CreateIsbn13("978-1-01-100041"), Year = 2019, TotalCopies = 5, AvailableCopies = 5, CategoryId = categories[5].CategoryId, AuthorId = authors[13].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Concrete Math",      ISBN = CreateIsbn13("978-1-01-100042"), Year = 1994, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[5].CategoryId, AuthorId = authors[13].AuthorId,PublisherId = publishers[8].PublisherId },
                new Book { Title = "Calculus Essentials",ISBN = CreateIsbn13("978-1-01-100043"), Year = 2015, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[5].CategoryId, AuthorId = authors[13].AuthorId,PublisherId = publishers[8].PublisherId },

                // ── Category 6: Science (4 books) ──
                new Book { Title = "Radioactivity",      ISBN = CreateIsbn13("978-1-01-100044"), Year = 1910, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[6].CategoryId, AuthorId = authors[20].AuthorId,PublisherId = publishers[4].PublisherId },
                new Book { Title = "Modern Physics",     ISBN = CreateIsbn13("978-1-01-100045"), Year = 1935, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[6].CategoryId, AuthorId = authors[20].AuthorId,PublisherId = publishers[4].PublisherId },
                new Book { Title = "Radium Research",    ISBN = CreateIsbn13("978-1-01-100046"), Year = 1921, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[6].CategoryId, AuthorId = authors[20].AuthorId,PublisherId = publishers[4].PublisherId },
                new Book { Title = "Science Treatise",   ISBN = CreateIsbn13("978-1-01-100047"), Year = 1930, TotalCopies = 3, AvailableCopies = 3, CategoryId = categories[6].CategoryId, AuthorId = authors[20].AuthorId,PublisherId = publishers[6].PublisherId },

                // ── Category 7: Self-Development (3 books) ──
                new Book { Title = "Stop Worrying",      ISBN = CreateIsbn13("978-1-01-100048"), Year = 1948, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[7].CategoryId, AuthorId = authors[16].AuthorId,PublisherId = publishers[6].PublisherId },
                new Book { Title = "The Alchemist",      ISBN = CreateIsbn13("978-1-01-100049"), Year = 1988, TotalCopies = 6, AvailableCopies = 6, CategoryId = categories[7].CategoryId, AuthorId = authors[18].AuthorId,PublisherId = publishers[6].PublisherId },
                new Book { Title = "The Zahir",          ISBN = CreateIsbn13("978-1-01-100050"), Year = 2005, TotalCopies = 4, AvailableCopies = 4, CategoryId = categories[7].CategoryId, AuthorId = authors[18].AuthorId,PublisherId = publishers[6].PublisherId }
            };
            context.Books.AddRange(books);

            // ── Step 7: Seed Members (Exactly 40 Members: All Cambodian Names & Cities) ─
            var today = DateTime.Today;

            var members = new[]
            {
                new Member { Name = "Chan Vicheka",     Gender = "Male",   DateOfBirth = new DateTime(2000, 3, 15),  Phone = "011-201-101", Email = "vicheka.c@gmail.com", Address = "7 Makara, Phnom Penh",   JoinDate = today.AddDays(-700) },
                new Member { Name = "Keo Sarath",       Gender = "Male",   DateOfBirth = new DateTime(2001, 5, 20),  Phone = "012-202-102", Email = "sarath.k@gmail.com",  Address = "Sen Sok, Phnom Penh",    JoinDate = today.AddDays(-685) },
                new Member { Name = "Seng Dara",        Gender = "Male",   DateOfBirth = new DateTime(1999, 8, 11),  Phone = "017-203-103", Email = "dara.seng@gmail.com", Address = "Dangkao, Phnom Penh",   JoinDate = today.AddDays(-670) },
                new Member { Name = "Meas Bopha",       Gender = "Female", DateOfBirth = new DateTime(2002, 11, 4),  Phone = "069-204-104", Email = "bopha.m@gmail.com",   Address = "Wat Bo, Siem Reap",      JoinDate = today.AddDays(-655) },
                new Member { Name = "Kim Heang",        Gender = "Male",   DateOfBirth = new DateTime(2000, 1, 18),  Phone = "070-205-105", Email = "heang.kim@gmail.com", Address = "Svay Por, Battambang",  JoinDate = today.AddDays(-640) },
                new Member { Name = "Sok Chenda",       Gender = "Female", DateOfBirth = new DateTime(2003, 2, 22),  Phone = "077-206-106", Email = "chenda.s@gmail.com",  Address = "Kamboul, Phnom Penh",   JoinDate = today.AddDays(-625) },
                new Member { Name = "Tep Vanna",        Gender = "Male",   DateOfBirth = new DateTime(1998, 3, 30),  Phone = "078-207-107", Email = "vanna.tep@gmail.com", Address = "Ta Khmau, Kandal",      JoinDate = today.AddDays(-610) },
                new Member { Name = "Ouk Panha",        Gender = "Male",   DateOfBirth = new DateTime(2001, 5, 14),  Phone = "085-208-108", Email = "panha.ouk@gmail.com", Address = "Puok, Siem Reap",       JoinDate = today.AddDays(-595) },
                new Member { Name = "Rath Sovann",      Gender = "Male",   DateOfBirth = new DateTime(1997, 7, 9),   Phone = "086-209-109", Email = "sovann.r@gmail.com",  Address = "Ratanak, Battambang",   JoinDate = today.AddDays(-580) },
                new Member { Name = "Chhorn Piseth",    Gender = "Male",   DateOfBirth = new DateTime(2002, 9, 12),  Phone = "092-210-110", Email = "piseth.c@gmail.com",  Address = "Saang, Kandal",         JoinDate = today.AddDays(-565) },
                new Member { Name = "Ly Sreynoch",      Gender = "Female", DateOfBirth = new DateTime(2004, 11, 5),  Phone = "095-211-111", Email = "sreynoch.l@gmail.com",Address = "Chhouk, Kampot",        JoinDate = today.AddDays(-550) },
                new Member { Name = "Heng Samnang",     Gender = "Male",   DateOfBirth = new DateTime(1999, 1, 10),  Phone = "096-212-112", Email = "samnang.h@gmail.com", Address = "7 Makara, Phnom Penh",   JoinDate = today.AddDays(-535) },
                new Member { Name = "Prak Kolab",       Gender = "Female", DateOfBirth = new DateTime(2003, 3, 15),  Phone = "098-213-113", Email = "kolab.p@gmail.com",   Address = "Tuek Chhou, Kampot",     JoinDate = today.AddDays(-520) },
                new Member { Name = "Nget Makara",      Gender = "Male",   DateOfBirth = new DateTime(2001, 5, 20),  Phone = "099-214-114", Email = "makara.n@gmail.com",  Address = "Sen Sok, Phnom Penh",    JoinDate = today.AddDays(-505) },
                new Member { Name = "Ros Chantrea",     Gender = "Female", DateOfBirth = new DateTime(2000, 8, 1),   Phone = "010-315-115", Email = "chantrea.r@gmail.com",Address = "Prey Nob, Sihanouk",    JoinDate = today.AddDays(-490) },
                new Member { Name = "Khuon Visal",      Gender = "Male",   DateOfBirth = new DateTime(2002, 10, 18), Phone = "011-316-116", Email = "visal.k@gmail.com",   Address = "Bavel, Battambang",     JoinDate = today.AddDays(-475) },
                new Member { Name = "Chea Sreymao",     Gender = "Female", DateOfBirth = new DateTime(2003, 1, 12),  Phone = "012-317-117", Email = "sreymao.c@gmail.com", Address = "Doun Keo, Takeo",      JoinDate = today.AddDays(-460) },
                new Member { Name = "Vannak Boramey",   Gender = "Female", DateOfBirth = new DateTime(2004, 2, 25),  Phone = "015-318-118", Email = "boramey.v@gmail.com", Address = "Dangkao, Phnom Penh",   JoinDate = today.AddDays(-445) },
                new Member { Name = "Em Sovannarith",   Gender = "Male",   DateOfBirth = new DateTime(1988, 6, 14),  Phone = "016-319-119", Email = "rith.em@gmail.com",   Address = "Kien Svay, Kandal",     JoinDate = today.AddDays(-430) },
                new Member { Name = "Long Sovatha",     Gender = "Male",   DateOfBirth = new DateTime(1996, 4, 18),  Phone = "017-320-120", Email = "sovatha.l@gmail.com", Address = "Ang Snuol, Kandal",     JoinDate = today.AddDays(-415) },
                new Member { Name = "Orn Sothea",       Gender = "Male",   DateOfBirth = new DateTime(1982, 12, 5),  Phone = "069-321-121", Email = "sothea.o@gmail.com",  Address = "Kralanh, Siem Reap",    JoinDate = today.AddDays(-400) },
                new Member { Name = "Som Rathana",      Gender = "Female", DateOfBirth = new DateTime(2002, 7, 22),  Phone = "070-322-122", Email = "rathana.s@gmail.com", Address = "Kampong Bay, Kampot",   JoinDate = today.AddDays(-385) },
                new Member { Name = "Sin Chantha",      Gender = "Female", DateOfBirth = new DateTime(2001, 9, 17),  Phone = "077-323-123", Email = "chantha.s@gmail.com", Address = "Bati, Takeo",          JoinDate = today.AddDays(-370) },
                new Member { Name = "Hun Sophal",       Gender = "Male",   DateOfBirth = new DateTime(1997, 11, 28), Phone = "078-324-124", Email = "sophal.h@gmail.com",  Address = "Moung, Battambang",     JoinDate = today.AddDays(-355) },
                new Member { Name = "Lim Sreypov",      Gender = "Female", DateOfBirth = new DateTime(2003, 4, 19),  Phone = "085-325-125", Email = "sreypov.l@gmail.com", Address = "Tram Kak, Takeo",      JoinDate = today.AddDays(-340) },
                new Member { Name = "Khorn Chamroeun",  Gender = "Male",   DateOfBirth = new DateTime(2000, 8, 30),  Phone = "086-326-126", Email = "chamroeun@gmail.com", Address = "Mittapheap, Sihanouk",  JoinDate = today.AddDays(-325) },
                new Member { Name = "Mao Pich",         Gender = "Male",   DateOfBirth = new DateTime(1998, 2, 14),  Phone = "092-327-127", Email = "pich.mao@gmail.com",  Address = "Samraong, Takeo",       JoinDate = today.AddDays(-310) },
                new Member { Name = "Nouv Pisey",       Gender = "Female", DateOfBirth = new DateTime(2002, 12, 8),  Phone = "095-328-128", Email = "pisey.n@gmail.com",   Address = "Angkor Chey, Kampot",   JoinDate = today.AddDays(-295) },
                new Member { Name = "Pen Vutha",        Gender = "Male",   DateOfBirth = new DateTime(1978, 5, 25),  Phone = "096-329-129", Email = "vutha.pen@gmail.com", Address = "Sen Sok, Phnom Penh",    JoinDate = today.AddDays(-280) },
                new Member { Name = "Bun Theara",       Gender = "Male",   DateOfBirth = new DateTime(2001, 10, 10), Phone = "098-330-130", Email = "theara.b@gmail.com",  Address = "Wat Bo, Siem Reap",      JoinDate = today.AddDays(-265) },
                new Member { Name = "Suy Mengleang",    Gender = "Male",   DateOfBirth = new DateTime(2004, 3, 3),   Phone = "099-331-131", Email = "mengleang@gmail.com", Address = "7 Makara, Phnom Penh",   JoinDate = today.AddDays(-250) },
                new Member { Name = "Phorn Rithy",      Gender = "Male",   DateOfBirth = new DateTime(1999, 6, 20),  Phone = "010-432-132", Email = "rithy.p@gmail.com",   Address = "Dangkao, Phnom Penh",   JoinDate = today.AddDays(-235) },
                new Member { Name = "Sarun Bunna",      Gender = "Male",   DateOfBirth = new DateTime(2000, 11, 15), Phone = "011-433-133", Email = "bunna.s@gmail.com",   Address = "Kamboul, Phnom Penh",   JoinDate = today.AddDays(-220) },
                new Member { Name = "Roeun Kanha",      Gender = "Female", DateOfBirth = new DateTime(2003, 8, 8),   Phone = "012-434-134", Email = "kanha.r@gmail.com",   Address = "Ta Khmau, Kandal",      JoinDate = today.AddDays(-205) },

                // ── Members who have never borrowed (6 Members) ──
                new Member { Name = "Someth Phearith",  Gender = "Male",   DateOfBirth = new DateTime(2002, 1, 5),   Phone = "015-435-135", Email = "phearith.s@gmail.com",Address = "Puok, Siem Reap",       JoinDate = today.AddDays(-120) },
                new Member { Name = "Try Sokunthea",    Gender = "Female", DateOfBirth = new DateTime(2004, 5, 12),  Phone = "016-436-136", Email = "sokunthea@gmail.com", Address = "Svay Por, Battambang",  JoinDate = today.AddDays(-102) },
                new Member { Name = "Voeun Chandara",   Gender = "Male",   DateOfBirth = new DateTime(2001, 7, 29),  Phone = "017-437-137", Email = "chandara.v@gmail.com",Address = "Sen Sok, Phnom Penh",    JoinDate = today.AddDays(-84) },
                new Member { Name = "Yan Navy",         Gender = "Female", DateOfBirth = new DateTime(2003, 10, 2),  Phone = "069-438-138", Email = "navy.yan@gmail.com",  Address = "Chhouk, Kampot",        JoinDate = today.AddDays(-66) },
                new Member { Name = "Yim Vibol",        Gender = "Male",   DateOfBirth = new DateTime(1995, 12, 14), Phone = "070-439-139", Email = "vibol.yim@gmail.com",  Address = "Bati, Takeo",          JoinDate = today.AddDays(-48) },
                new Member { Name = "Yun Kosal",        Gender = "Male",   DateOfBirth = new DateTime(2000, 4, 25),  Phone = "077-440-140", Email = "kosal.yun@gmail.com", Address = "Mittapheap, Sihanouk",  JoinDate = today.AddDays(-30) }
            };
            context.Members.AddRange(members);
            context.SaveChanges();

            // ── Step 8: Seed Borrows (40 Transactions: 10 per Librarian) ───────
            // Evenly handled across: Admin (0..9), Seyha (10..19), Dara (20..29), Sali (30..39)
            var allLibrarians = new[] { admin, librarians[0], librarians[1], librarians[2] };

            var borrows = new[]
            {
                // ── Active Borrows (11 records: BorrowDate 1-12 days ago, DueDate in future) ──
                new Borrow { MemberId = members[0].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-1),  DueDate = today.AddDays(13), ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[1].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-2),  DueDate = today.AddDays(12), ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[2].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-3),  DueDate = today.AddDays(11), ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[3].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-4),  DueDate = today.AddDays(10), ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[4].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-5),  DueDate = today.AddDays(9),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[5].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-6),  DueDate = today.AddDays(8),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[6].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-7),  DueDate = today.AddDays(7),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[7].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-8),  DueDate = today.AddDays(6),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[8].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-9),  DueDate = today.AddDays(5),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[9].MemberId,  LibrarianId = allLibrarians[0].LibrarianId, BorrowDate = today.AddDays(-10), DueDate = today.AddDays(4),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },
                new Borrow { MemberId = members[10].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-12), DueDate = today.AddDays(2),  ReturnDate = null, FineAmount = 0m, Status = BorrowStatus.Active },

                // ── Overdue Borrows (9 records: DueDate 3-20 days ago, fine computed to today) ──
                new Borrow { MemberId = members[0].MemberId,  LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-17), DueDate = today.AddDays(-3),  ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-3), today),  Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[11].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-19), DueDate = today.AddDays(-5),  ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-5), today),  Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[12].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-21), DueDate = today.AddDays(-7),  ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-7), today),  Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[13].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-23), DueDate = today.AddDays(-9),  ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-9), today),  Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[14].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-25), DueDate = today.AddDays(-11), ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-11), today), Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[15].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-28), DueDate = today.AddDays(-14), ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-14), today), Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[16].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-30), DueDate = today.AddDays(-16), ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-16), today), Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[17].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-32), DueDate = today.AddDays(-18), ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-18), today), Status = BorrowStatus.Overdue },
                new Borrow { MemberId = members[18].MemberId, LibrarianId = allLibrarians[1].LibrarianId, BorrowDate = today.AddDays(-34), DueDate = today.AddDays(-20), ReturnDate = null, FineAmount = FinePolicy.CalculateFine(today.AddDays(-20), today), Status = BorrowStatus.Overdue },

                // ── Returned on time (11 records: ReturnDate <= DueDate, fine = 0) ──
                new Borrow { MemberId = members[0].MemberId,  LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-30), DueDate = today.AddDays(-16), ReturnDate = today.AddDays(-18), FineAmount = FinePolicy.CalculateFine(today.AddDays(-16), today.AddDays(-18)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[1].MemberId,  LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-35), DueDate = today.AddDays(-21), ReturnDate = today.AddDays(-21), FineAmount = FinePolicy.CalculateFine(today.AddDays(-21), today.AddDays(-21)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[2].MemberId,  LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-40), DueDate = today.AddDays(-26), ReturnDate = today.AddDays(-28), FineAmount = FinePolicy.CalculateFine(today.AddDays(-26), today.AddDays(-28)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[19].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-45), DueDate = today.AddDays(-31), ReturnDate = today.AddDays(-33), FineAmount = FinePolicy.CalculateFine(today.AddDays(-31), today.AddDays(-33)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[20].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-50), DueDate = today.AddDays(-36), ReturnDate = today.AddDays(-37), FineAmount = FinePolicy.CalculateFine(today.AddDays(-36), today.AddDays(-37)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[21].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-55), DueDate = today.AddDays(-41), ReturnDate = today.AddDays(-43), FineAmount = FinePolicy.CalculateFine(today.AddDays(-41), today.AddDays(-43)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[22].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-60), DueDate = today.AddDays(-46), ReturnDate = today.AddDays(-47), FineAmount = FinePolicy.CalculateFine(today.AddDays(-46), today.AddDays(-47)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[23].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-65), DueDate = today.AddDays(-51), ReturnDate = today.AddDays(-53), FineAmount = FinePolicy.CalculateFine(today.AddDays(-51), today.AddDays(-53)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[24].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-70), DueDate = today.AddDays(-56), ReturnDate = today.AddDays(-58), FineAmount = FinePolicy.CalculateFine(today.AddDays(-56), today.AddDays(-58)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[25].MemberId, LibrarianId = allLibrarians[2].LibrarianId, BorrowDate = today.AddDays(-75), DueDate = today.AddDays(-61), ReturnDate = today.AddDays(-63), FineAmount = FinePolicy.CalculateFine(today.AddDays(-61), today.AddDays(-63)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[26].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-80), DueDate = today.AddDays(-66), ReturnDate = today.AddDays(-67), FineAmount = FinePolicy.CalculateFine(today.AddDays(-66), today.AddDays(-67)), Status = BorrowStatus.Returned },

                // ── Returned late (9 records: ReturnDate > DueDate, fine computed via FinePolicy) ──
                new Borrow { MemberId = members[1].MemberId,  LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-40),  DueDate = today.AddDays(-26), ReturnDate = today.AddDays(-23), FineAmount = FinePolicy.CalculateFine(today.AddDays(-26), today.AddDays(-23)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[2].MemberId,  LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-45),  DueDate = today.AddDays(-31), ReturnDate = today.AddDays(-27), FineAmount = FinePolicy.CalculateFine(today.AddDays(-31), today.AddDays(-27)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[27].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-50),  DueDate = today.AddDays(-36), ReturnDate = today.AddDays(-33), FineAmount = FinePolicy.CalculateFine(today.AddDays(-36), today.AddDays(-33)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[28].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-60),  DueDate = today.AddDays(-46), ReturnDate = today.AddDays(-41), FineAmount = FinePolicy.CalculateFine(today.AddDays(-46), today.AddDays(-41)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[29].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-70),  DueDate = today.AddDays(-56), ReturnDate = today.AddDays(-52), FineAmount = FinePolicy.CalculateFine(today.AddDays(-56), today.AddDays(-52)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[30].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-80),  DueDate = today.AddDays(-66), ReturnDate = today.AddDays(-60), FineAmount = FinePolicy.CalculateFine(today.AddDays(-66), today.AddDays(-60)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[31].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-90),  DueDate = today.AddDays(-76), ReturnDate = today.AddDays(-73), FineAmount = FinePolicy.CalculateFine(today.AddDays(-76), today.AddDays(-73)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[32].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-100), DueDate = today.AddDays(-86), ReturnDate = today.AddDays(-81), FineAmount = FinePolicy.CalculateFine(today.AddDays(-86), today.AddDays(-81)), Status = BorrowStatus.Returned },
                new Borrow { MemberId = members[33].MemberId, LibrarianId = allLibrarians[3].LibrarianId, BorrowDate = today.AddDays(-115), DueDate = today.AddDays(-101),ReturnDate = today.AddDays(-94), FineAmount = FinePolicy.CalculateFine(today.AddDays(-101),today.AddDays(-94)), Status = BorrowStatus.Returned }
            };
            context.Borrows.AddRange(borrows);
            context.SaveChanges();

            // ── Step 9: Seed BorrowDetails (47 Line Items) ──────────────────────
            var borrowDetails = new List<BorrowDetail>
            {
                // Active Borrows (indices 0..10)
                new BorrowDetail { BorrowId = borrows[0].BorrowId,  BookId = books[0].BookId,  Quantity = 1 }, // Sophat
                new BorrowDetail { BorrowId = borrows[0].BorrowId,  BookId = books[29].BookId, Quantity = 1 }, // Dune
                new BorrowDetail { BorrowId = borrows[1].BorrowId,  BookId = books[0].BookId,  Quantity = 2 }, // Sophat (x2)
                new BorrowDetail { BorrowId = borrows[1].BorrowId,  BookId = books[27].BookId, Quantity = 1 }, // 1984
                new BorrowDetail { BorrowId = borrows[2].BorrowId,  BookId = books[29].BookId, Quantity = 1 }, // Dune
                new BorrowDetail { BorrowId = borrows[2].BorrowId,  BookId = books[5].BookId,  Quantity = 1 }, // Kakey
                new BorrowDetail { BorrowId = borrows[3].BorrowId,  BookId = books[34].BookId, Quantity = 1 }, // Clean Code
                new BorrowDetail { BorrowId = borrows[4].BorrowId,  BookId = books[1].BookId,  Quantity = 1 }, // Phka Srapoun
                new BorrowDetail { BorrowId = borrows[5].BorrowId,  BookId = books[2].BookId,  Quantity = 1 }, // Mealea Duong Chett
                new BorrowDetail { BorrowId = borrows[6].BorrowId,  BookId = books[7].BookId,  Quantity = 1 }, // Angkor Civilization
                new BorrowDetail { BorrowId = borrows[7].BorrowId,  BookId = books[9].BookId,  Quantity = 1 }, // The Pagoda Boy
                new BorrowDetail { BorrowId = borrows[8].BorrowId,  BookId = books[35].BookId, Quantity = 1 }, // Clean Architecture
                new BorrowDetail { BorrowId = borrows[9].BorrowId,  BookId = books[31].BookId, Quantity = 1 }, // TAOCP Vol. 1
                new BorrowDetail { BorrowId = borrows[10].BorrowId, BookId = books[32].BookId, Quantity = 1 }, // Intro to Algorithms

                // Overdue Borrows (indices 11..19)
                new BorrowDetail { BorrowId = borrows[11].BorrowId, BookId = books[29].BookId, Quantity = 1 }, // Dune
                new BorrowDetail { BorrowId = borrows[11].BorrowId, BookId = books[4].BookId,  Quantity = 1 }, // Tum Teav
                new BorrowDetail { BorrowId = borrows[12].BorrowId, BookId = books[34].BookId, Quantity = 1 }, // Clean Code
                new BorrowDetail { BorrowId = borrows[13].BorrowId, BookId = books[27].BookId, Quantity = 2 }, // 1984 (x2)
                new BorrowDetail { BorrowId = borrows[14].BorrowId, BookId = books[6].BookId,  Quantity = 1 }, // Shadow Theatre
                new BorrowDetail { BorrowId = borrows[15].BorrowId, BookId = books[13].BookId, Quantity = 1 }, // Cambodian Economy
                new BorrowDetail { BorrowId = borrows[16].BorrowId, BookId = books[38].BookId, Quantity = 1 }, // Refactoring
                new BorrowDetail { BorrowId = borrows[17].BorrowId, BookId = books[18].BookId, Quantity = 1 }, // Steve Jobs
                new BorrowDetail { BorrowId = borrows[18].BorrowId, BookId = books[48].BookId, Quantity = 1 }, // The Alchemist
                new BorrowDetail { BorrowId = borrows[19].BorrowId, BookId = books[15].BookId, Quantity = 1 }, // Win Friends

                // Returned on time (indices 20..30)
                new BorrowDetail { BorrowId = borrows[20].BorrowId, BookId = books[0].BookId,  Quantity = 1 }, // Sophat
                new BorrowDetail { BorrowId = borrows[20].BorrowId, BookId = books[4].BookId,  Quantity = 1 }, // Tum Teav
                new BorrowDetail { BorrowId = borrows[21].BorrowId, BookId = books[29].BookId, Quantity = 1 }, // Dune
                new BorrowDetail { BorrowId = borrows[21].BorrowId, BookId = books[27].BookId, Quantity = 1 }, // 1984
                new BorrowDetail { BorrowId = borrows[21].BorrowId, BookId = books[5].BookId,  Quantity = 1 }, // Kakey
                new BorrowDetail { BorrowId = borrows[22].BorrowId, BookId = books[1].BookId,  Quantity = 1 }, // Phka Srapoun
                new BorrowDetail { BorrowId = borrows[23].BorrowId, BookId = books[2].BookId,  Quantity = 1 }, // Mealea Duong Chett
                new BorrowDetail { BorrowId = borrows[24].BorrowId, BookId = books[3].BookId,  Quantity = 1 }, // Kolab Pailin
                new BorrowDetail { BorrowId = borrows[25].BorrowId, BookId = books[20].BookId, Quantity = 1 }, // The Moonlit Lake
                new BorrowDetail { BorrowId = borrows[26].BorrowId, BookId = books[7].BookId,  Quantity = 1 }, // Angkor Civilization
                new BorrowDetail { BorrowId = borrows[27].BorrowId, BookId = books[8].BookId,  Quantity = 1 }, // Modern Architecture
                new BorrowDetail { BorrowId = borrows[28].BorrowId, BookId = books[10].BookId, Quantity = 1 }, // The Lost Heritage
                new BorrowDetail { BorrowId = borrows[29].BorrowId, BookId = books[16].BookId, Quantity = 1 }, // A Meaningless Life
                new BorrowDetail { BorrowId = borrows[30].BorrowId, BookId = books[17].BookId, Quantity = 1 }, // The Dead Heart

                // Returned late (indices 31..39)
                new BorrowDetail { BorrowId = borrows[31].BorrowId, BookId = books[34].BookId, Quantity = 1 }, // Clean Code
                new BorrowDetail { BorrowId = borrows[32].BorrowId, BookId = books[35].BookId, Quantity = 1 }, // Clean Architecture
                new BorrowDetail { BorrowId = borrows[33].BorrowId, BookId = books[36].BookId, Quantity = 1 }, // The Clean Coder
                new BorrowDetail { BorrowId = borrows[34].BorrowId, BookId = books[38].BookId, Quantity = 1 }, // Refactoring
                new BorrowDetail { BorrowId = borrows[35].BorrowId, BookId = books[31].BookId, Quantity = 1 }, // TAOCP Vol. 1
                new BorrowDetail { BorrowId = borrows[36].BorrowId, BookId = books[32].BookId, Quantity = 1 }, // Intro to Algorithms
                new BorrowDetail { BorrowId = borrows[37].BorrowId, BookId = books[18].BookId, Quantity = 1 }, // Steve Jobs
                new BorrowDetail { BorrowId = borrows[38].BorrowId, BookId = books[28].BookId, Quantity = 1 }, // Animal Farm
                new BorrowDetail { BorrowId = borrows[39].BorrowId, BookId = books[48].BookId, Quantity = 1 }  // The Alchemist
            };
            context.BorrowDetails.AddRange(borrowDetails);

            // Deduct AvailableCopies for currently unreturned (Active & Overdue) books
            books[0].AvailableCopies  -= 3; // Sophat (1 from Borrow 0, 2 from Borrow 1) -> 0 copies left
            books[29].AvailableCopies -= 3; // Dune (1 from Borrow 0, 1 from Borrow 2, 1 from Borrow 11) -> 0 copies left
            books[27].AvailableCopies -= 3; // 1984 (1 from Borrow 1, 2 from Borrow 13) -> 0 copies left
            books[5].AvailableCopies  -= 1; // Kakey (1 from Borrow 2) -> 0 copies left
            books[34].AvailableCopies -= 2; // Clean Code (1 from Borrow 3, 1 from Borrow 12) -> 0 copies left

            books[1].AvailableCopies  -= 1; // Phka Srapoun (Borrow 4) -> 4 copies left
            books[2].AvailableCopies  -= 1; // Mealea Duong Chett (Borrow 5) -> 3 copies left
            books[7].AvailableCopies  -= 1; // Angkor Civilization (Borrow 6) -> 4 copies left
            books[9].AvailableCopies  -= 1; // The Pagoda Boy (Borrow 7) -> 4 copies left
            books[35].AvailableCopies -= 1; // Clean Architecture (Borrow 8) -> 4 copies left
            books[31].AvailableCopies -= 1; // TAOCP Vol. 1 (Borrow 9) -> 3 copies left
            books[32].AvailableCopies -= 1; // Intro to Algorithms (Borrow 10) -> 5 copies left

            books[4].AvailableCopies  -= 1; // Tum Teav (Borrow 11) -> 3 copies left
            books[6].AvailableCopies  -= 1; // Shadow Theatre (Borrow 14) -> 3 copies left
            books[13].AvailableCopies -= 1; // Cambodian Economy (Borrow 15) -> 4 copies left
            books[38].AvailableCopies -= 1; // Refactoring (Borrow 16) -> 3 copies left
            books[18].AvailableCopies -= 1; // Steve Jobs (Borrow 17) -> 4 copies left
            books[48].AvailableCopies -= 1; // The Alchemist (Borrow 18) -> 5 copies left
            books[15].AvailableCopies -= 1; // Win Friends (Borrow 19) -> 4 copies left

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
