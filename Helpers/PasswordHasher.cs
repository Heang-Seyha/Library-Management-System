namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// Provides password hashing and verification using BCrypt.
    /// BCrypt is a one-way hash — we NEVER store the original password.
    /// 
    /// How it works:
    ///   Hash: plainTextPassword → $2a$12$... (stored in DB)
    ///   Verify: plainTextPassword + storedHash → true/false
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Hashes a plain-text password using BCrypt.
        /// Call this when creating or updating a password.
        /// </summary>
        public static string Hash(string plainTextPassword)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword))
                throw new ArgumentException("Password cannot be empty.", nameof(plainTextPassword));

            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }

        /// <summary>
        /// Verifies a plain-text password against a stored BCrypt hash.
        /// Returns true if the password matches.
        /// </summary>
        public static bool Verify(string plainTextPassword, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword)) return false;
            if (string.IsNullOrWhiteSpace(storedHash)) return false;

            return BCrypt.Net.BCrypt.Verify(plainTextPassword, storedHash);
        }
    }
}
