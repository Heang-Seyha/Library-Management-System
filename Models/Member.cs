namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Represents a library member.
    /// Demonstrates: Inheritance (extends Person), Polymorphism (overrides GetInfo)
    /// </summary>
    public class Member : Person
    {
        public int MemberId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.Today;

        // Navigation property — Association: Member has many Borrows
        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

        // Demonstrates Polymorphism — overrides Person.GetInfo()
        public override string GetInfo()
        {
            return $"Member: {Name} | Phone: {Phone} | Email: {Email}";
        }

        public override string ToString() => Name;
    }
}
