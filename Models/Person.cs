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

        // Virtual method — Polymorphism (overridden in subclasses)
        public virtual string GetInfo()
        {
            return $"{Name} | {Phone}";
        }
    }
}
