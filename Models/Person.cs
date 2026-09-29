namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Abstract base class representing any person in the system.
    /// Demonstrates: Abstraction, Encapsulation, Inheritance
    /// </summary>
    public abstract class Person
    {
        // Private backing fields — Encapsulation
        private string _name = string.Empty;
        private string _phone = string.Empty;
        private string _gender = string.Empty;
        private DateTime? _dateOfBirth;

        public string Name
        {
            get => _name;
            set => _name = value?.Trim() ?? string.Empty;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value?.Trim() ?? string.Empty;
        }

        public string Gender
        {
            get => _gender;
            set => _gender = value?.Trim() ?? string.Empty;
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }

        // Virtual method — Polymorphism (overridden in subclasses)
        public virtual string GetInfo()
        {
            string dobStr = DateOfBirth.HasValue ? DateOfBirth.Value.ToString("MM/dd/yyyy") : "N/A";
            return $"{Name} | {Gender} | DOB: {dobStr} | {Phone}";
        }
    }
}
