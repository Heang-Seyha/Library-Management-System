using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    /// <summary>
    /// Service for Member account and profile management.
    /// </summary>
    public class MemberService : CrudService<Member>
    {
        private static readonly MemberValidator _validator = new();

        public MemberService(LibraryDbContext context) : base(context) { }

        public override List<Member> GetAll() =>
            _context.Members.OrderBy(m => m.Name).ToList();

        public List<Member> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return GetAll();

            var q = query.Trim();
            return _context.Members
                .Where(m => m.Name.Contains(q)
                         || (m.Gender != null && m.Gender.Contains(q))
                         || m.Phone.Contains(q)
                         || (m.Email != null && m.Email.Contains(q))
                         || (m.Address != null && m.Address.Contains(q)))
                .OrderBy(m => m.Name)
                .ToList();
        }

        public (bool success, string message) Add(Member member)
        {
            var result = _validator.Validate(member);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var trimmedEmail = member.Email.Trim();
            if (!string.IsNullOrEmpty(trimmedEmail) && _context.Members.Any(m => m.Email == trimmedEmail))
                return (false, $"A member with email '{member.Email}' already exists.");

            var trimmedPhone = member.Phone.Trim();
            if (!string.IsNullOrEmpty(trimmedPhone) && _context.Members.Any(m => m.Phone == trimmedPhone))
                return (false, $"A member with phone '{member.Phone}' already exists.");

            member.Name = member.Name.Trim();
            member.Gender = string.IsNullOrWhiteSpace(member.Gender) ? "Male" : member.Gender.Trim();
            member.Phone = trimmedPhone;
            member.Email = trimmedEmail;
            member.Address = member.Address?.Trim() ?? "";
            _context.Members.Add(member);
            _context.SaveChanges();
            return (true, "Member added successfully.");
        }

        public (bool success, string message) Update(Member member)
        {
            var existing = _context.Members.Find(member.MemberId);
            if (existing == null) return (false, "Member not found.");

            var result = _validator.Validate(member);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            var trimmedEmail = member.Email.Trim();
            if (!string.IsNullOrEmpty(trimmedEmail) && _context.Members.Any(m => m.Email == trimmedEmail && m.MemberId != member.MemberId))
                return (false, $"A member with email '{member.Email}' already exists.");

            var trimmedPhone = member.Phone.Trim();
            if (!string.IsNullOrEmpty(trimmedPhone) && _context.Members.Any(m => m.Phone == trimmedPhone && m.MemberId != member.MemberId))
                return (false, $"A member with phone '{member.Phone}' already exists.");

            existing.Name = member.Name.Trim();
            existing.Gender = string.IsNullOrWhiteSpace(member.Gender) ? "Male" : member.Gender.Trim();
            existing.DateOfBirth = member.DateOfBirth;
            existing.Phone = trimmedPhone;
            existing.Email = trimmedEmail;
            existing.Address = member.Address?.Trim() ?? "";
            existing.JoinDate = member.JoinDate;
            _context.SaveChanges();
            return (true, "Member updated successfully.");
        }

        public (bool success, string message) Delete(int memberId)
        {
            if (_context.Borrows.Any(b => b.MemberId == memberId))
                return (false, "Cannot delete: this member has borrowing records.");
            var member = _context.Members.Find(memberId);
            if (member == null) return (false, "Member not found.");

            try
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
                return (true, "Member deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return (false, "Cannot delete this member because they are referenced by existing library transactions.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[MemberService.Delete] {ex}");
                return (false, "An error occurred while deleting the member. Please try again.");
            }
        }
    }
}
