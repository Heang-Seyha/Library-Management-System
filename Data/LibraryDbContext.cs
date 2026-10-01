using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    /// <summary>
    /// EF Core Database Context — the single entry point for all database operations.
    /// Configures relationships, constraints, and table mappings.
    /// </summary>
    public class LibraryDbContext : DbContext
    {
        // ── DbSets (Tables) ──────────────────────────────────────────────────
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BorrowDetail> BorrowDetails { get; set; }

        // ── Constructor ──────────────────────────────────────────────────────
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        // ── Model Configuration ──────────────────────────────────────────────
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Category ─────────────────────────────────────────────────────
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Description).HasMaxLength(500);
            });

            // ── Author ────────────────────────────────────────────────────────
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.AuthorId);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(150);
                entity.Property(a => a.Gender).IsRequired().HasMaxLength(10).HasDefaultValue("Male");
                entity.Property(a => a.DateOfBirth);
                entity.Property(a => a.Bio).HasMaxLength(1000);
                entity.Ignore(a => a.Phone);
            });

            // ── Publisher ─────────────────────────────────────────────────────
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(p => p.PublisherId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
                entity.Property(p => p.Address).HasMaxLength(500);
                entity.Property(p => p.Phone).HasMaxLength(20);
            });

            // ── Book ──────────────────────────────────────────────────────────
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books", t =>
                {
                    t.HasCheckConstraint("CK_Books_AvailableCopies_NonNegative", "[AvailableCopies] >= 0");
                    t.HasCheckConstraint("CK_Books_AvailableCopies_Lte_TotalCopies", "[AvailableCopies] <= [TotalCopies]");
                    t.HasCheckConstraint("CK_Books_TotalCopies_Positive", "[TotalCopies] >= 1");
                });

                entity.HasKey(b => b.BookId);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(300);
                entity.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
                entity.HasIndex(b => b.ISBN).IsUnique();  // ISBN must be unique
                entity.Property(b => b.RowVersion).IsRowVersion();

                if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
                {
                    entity.Property(b => b.RowVersion).HasDefaultValueSql("randomblob(8)");
                }

                // Book → Category (restrict delete to protect book history)
                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Books)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Book → Author
                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Book → Publisher
                entity.HasOne(b => b.Publisher)
                      .WithMany(p => p.Books)
                      .HasForeignKey(b => b.PublisherId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Member ────────────────────────────────────────────────────────
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.MemberId);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(150);
                entity.Property(m => m.Gender).IsRequired().HasMaxLength(10).HasDefaultValue("Male");
                entity.Property(m => m.DateOfBirth);
                entity.Property(m => m.Phone).HasMaxLength(20);
                entity.Property(m => m.Email).HasMaxLength(200);
                entity.Property(m => m.Address).HasMaxLength(500);
            });

            // ── Librarian ─────────────────────────────────────────────────────
            modelBuilder.Entity<Librarian>(entity =>
            {
                entity.ToTable("Librarians");
                entity.HasKey(e => e.LibrarianId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(10).HasDefaultValue("Male");
                entity.Property(e => e.DateOfBirth);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20).HasDefaultValue("");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200).HasDefaultValue("");
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique();  // Username must be unique
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20).HasDefaultValue("Librarian");
            });

            // ── Borrow ────────────────────────────────────────────────────────
            modelBuilder.Entity<Borrow>(entity =>
            {
                entity.HasKey(b => b.BorrowId);
                entity.Property(b => b.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Active");
                entity.Property(b => b.FineAmount).HasPrecision(18, 2);
                entity.Property(b => b.RowVersion).IsRowVersion();

                if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
                {
                    entity.Property(b => b.RowVersion).HasDefaultValueSql("randomblob(8)");
                }

                // Borrow → Member (restrict: don't delete members with borrow history)
                entity.HasOne(b => b.Member)
                      .WithMany(m => m.Borrows)
                      .HasForeignKey(b => b.MemberId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Borrow → Librarian (restrict: don't delete librarians with borrow history)
                entity.HasOne(b => b.Librarian)
                      .WithMany(e => e.Borrows)
                      .HasForeignKey(b => b.LibrarianId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ── BorrowDetail ──────────────────────────────────────────────────
            modelBuilder.Entity<BorrowDetail>(entity =>
            {
                entity.ToTable("BorrowDetails", t =>
                {
                    t.HasCheckConstraint("CK_BorrowDetails_Quantity_Positive", "[Quantity] >= 1");
                });

                entity.HasKey(bd => bd.BorrowDetailId);

                // BorrowDetail → Borrow (cascade: delete details when borrow is deleted)
                entity.HasOne(bd => bd.Borrow)
                      .WithMany(b => b.BorrowDetails)
                      .HasForeignKey(bd => bd.BorrowId)
                      .OnDelete(DeleteBehavior.Cascade);

                // BorrowDetail → Book (restrict: don't delete books with borrow history)
                entity.HasOne(bd => bd.Book)
                      .WithMany(b => b.BorrowDetails)
                      .HasForeignKey(bd => bd.BookId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
