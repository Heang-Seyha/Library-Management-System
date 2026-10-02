using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Service for Publisher master data management.
    /// </summary>
    public class PublisherService : CrudService<Publisher>
    {
        private static readonly PublisherValidator _validator = new();

        public PublisherService(LibraryDbContext context) : base(context) { }

        public override List<Publisher> GetAll() =>
            _context.Publishers.OrderBy(p => p.PublisherId).ToList();

        public (bool success, string message) Add(Publisher publisher)
        {
            var result = _validator.Validate(publisher);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            publisher.Name = publisher.Name.Trim();
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return (true, "Publisher added successfully.");
        }

        public (bool success, string message) Update(Publisher publisher)
        {
            var result = _validator.Validate(publisher);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var existing = _context.Publishers.Find(publisher.PublisherId);
            if (existing == null) return (false, "Publisher not found.");
            existing.Name = publisher.Name.Trim();
            existing.Address = publisher.Address?.Trim() ?? "";
            existing.Phone = publisher.Phone?.Trim() ?? "";
            _context.SaveChanges();
            return (true, "Publisher updated successfully.");
        }

        public (bool success, string message) Delete(int publisherId)
        {
            if (_context.Books.Any(b => b.PublisherId == publisherId))
                return (false, "Cannot delete: books are assigned to this publisher.");
            var pub = _context.Publishers.Find(publisherId);
            if (pub == null) return (false, "Publisher not found.");
            try
            {
                _context.Publishers.Remove(pub);
                _context.SaveChanges();
                return (true, "Publisher deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return (false, "Cannot delete this publisher because books are assigned to them.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PublisherService.Delete] {ex}");
                return (false, "An error occurred while deleting the publisher. Please try again.");
            }
        }
    }
}
