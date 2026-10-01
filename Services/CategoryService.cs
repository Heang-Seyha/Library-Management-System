using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Service for Category master data management.
    /// </summary>
    public class CategoryService : CrudService<Category>
    {
        private static readonly CategoryValidator _validator = new();

        public CategoryService(LibraryDbContext context) : base(context) { }

        public override List<Category> GetAll() =>
            _context.Categories.OrderBy(c => c.Name).ToList();

        public (bool success, string message) Add(Category category)
        {
            var result = _validator.Validate(category);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var name = category.Name.Trim();
            if (_context.Categories.Any(c => c.Name == name))
                return (false, "A category with this name already exists.");

            category.Name = name;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return (true, "Category added successfully.");
        }

        public (bool success, string message) Update(Category category)
        {
            var result = _validator.Validate(category);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var name = category.Name.Trim();
            if (_context.Categories.Any(c => c.Name == name && c.CategoryId != category.CategoryId))
                return (false, "A category with this name already exists.");

            var existing = _context.Categories.Find(category.CategoryId);
            if (existing == null) return (false, "Category not found.");
            existing.Name = name;
            existing.Description = category.Description?.Trim() ?? "";
            _context.SaveChanges();
            return (true, "Category updated successfully.");
        }

        public (bool success, string message) Delete(int categoryId)
        {
            if (_context.Books.Any(b => b.CategoryId == categoryId))
                return (false, "Cannot delete: books are assigned to this category.");
            var cat = _context.Categories.Find(categoryId);
            if (cat == null) return (false, "Category not found.");
            try
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
                return (true, "Category deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return (false, "Cannot delete this category because it is referenced by existing books.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CategoryService.Delete] {ex}");
                return (false, "An error occurred while deleting the category. Please try again.");
            }
        }
    }
}
