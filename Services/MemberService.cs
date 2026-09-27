using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Validators;

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
            var q = query.Trim().ToLower();
            return _context.Members
                .Where(m => m.Name.ToLower().Contains(q)
                         || m.Phone.ToLower().Contains(q)
                         || m.Email.ToLower().Contains(q))
                .OrderBy(m => m.Name)
                .ToList();
        }

        public (bool success, string message) Add(Member member)
        {
            var result = _validator.Validate(member);
            if (!result.IsValid)
                return (false, result.Errors.First().ErrorMessage);

            member.Name = member.Name.Trim();
            member.Phone = member.Phone.Trim();
            member.Email = member.Email.Trim();
            member.Address = member.Address.Trim();
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

            existing.Name = member.Name.Trim();
            existing.Phone = member.Phone.Trim();
            existing.Email = member.Email.Trim();
            existing.Address = member.Address.Trim();
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
            _context.Members.Remove(member);
            _context.SaveChanges();
            return (true, "Member deleted successfully.");
        }
    }
}
