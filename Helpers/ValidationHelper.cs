using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Helpers
{
    /// <summary>
    /// Professional validation utilities for user input across dialogs and forms.
    /// Supports inline visual error states, error label management, and standardized formats.
    /// </summary>
    public static class ValidationHelper
    {
        internal static readonly Regex EmailRegex = new(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Cambodian phone: starts with 0, 9-10 digits (allowing hyphens/spaces)
        internal static readonly Regex PhoneRegex = new(
            @"^0\d{1,2}[-\s]?\d{3}[-\s]?\d{3,4}$",
            RegexOptions.Compiled);

        /// <summary>
        /// Validates ISBN (supports standard ISBN-10 and ISBN-13 with checksum verification).
        /// Rejects invalid length, characters, and failing checksums.
        /// </summary>
        public static bool IsValidISBN(string? isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;
            var clean = isbn.Replace("-", "").Replace(" ", "").Trim();

            // ISBN-13 check
            if (clean.Length == 13 && clean.All(char.IsDigit))
            {
                // Must start with standard book prefix (978 or 979)
                if (!clean.StartsWith("978") && !clean.StartsWith("979"))
                    return false;

                // Validate ISBN-13 checksum (mod 10)
                int sum = 0;
                for (int i = 0; i < 12; i++)
                {
                    int digit = clean[i] - '0';
                    sum += (i % 2 == 0) ? digit : digit * 3;
                }
                int checkDigit = (10 - (sum % 10)) % 10;
                return (clean[12] - '0') == checkDigit;
            }

            // ISBN-10 check
            if (clean.Length == 10)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (!char.IsDigit(clean[i])) return false;
                }
                char last = clean[9];
                if (!char.IsDigit(last) && last != 'X' && last != 'x')
                    return false;

                // Validate ISBN-10 checksum (mod 11)
                int sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    sum += (clean[i] - '0') * (10 - i);
                }
                int lastVal = (last == 'X' || last == 'x') ? 10 : (last - '0');
                sum += lastVal;
                return (sum % 11) == 0;
            }

            return false;
        }

        /// <summary>
        /// Validates phone number (Cambodian format: starts with 0, 9-10 digits, allows hyphens).
        /// </summary>
        public static bool IsValidPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var trimmed = phone.Trim();
            var digitsOnly = new string(trimmed.Where(char.IsDigit).ToArray());

            if (!digitsOnly.StartsWith("0")) return false;
            if (digitsOnly.Length < 9 || digitsOnly.Length > 10) return false;

            return PhoneRegex.IsMatch(trimmed) || digitsOnly.Length is 9 or 10;
        }

        /// <summary>
        /// Validates email address with standard pattern.
        /// </summary>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return EmailRegex.IsMatch(email.Trim());
        }

        /// <summary>
        /// Validates publication year (between 1000 and current calendar year).
        /// </summary>
        public static bool IsValidYear(int year)
        {
            return year >= 1000 && year <= DateTime.Now.Year;
        }

        /// <summary>
        /// Validates required text and maximum character length.
        /// </summary>
        public static bool IsRequired(string? value, int maxLength = int.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.Trim().Length <= maxLength;
        }

        /// <summary>
        /// Validates username format: alphanumeric + underscore only, min 3 chars.
        /// </summary>
        public static bool IsValidUsername(string? username, int minLength = 3, int maxLength = 50)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            var trimmed = username.Trim();
            if (trimmed.Length < minLength || trimmed.Length > maxLength) return false;
            return trimmed.All(c => char.IsLetterOrDigit(c) || c == '_');
        }

        /// <summary>
        /// Validates whether string is a positive integer (> 0).
        /// </summary>
        public static bool IsPositiveInt(string? value, out int result)
        {
            if (int.TryParse(value?.Trim(), out result) && result > 0)
            {
                return true;
            }
            result = 0;
            return false;
        }

        /// <summary>
        /// Validates whether string is a non-negative integer (>= 0).
        /// </summary>
        public static bool IsNonNegativeInt(string? value, out int result)
        {
            if (int.TryParse(value?.Trim(), out result) && result >= 0)
            {
                return true;
            }
            result = 0;
            return false;
        }

        /// <summary>
        /// Validates password strength: minimum 8 chars, at least 1 letter and 1 number.
        /// </summary>
        public static bool IsValidPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < 8) return false;
            return password.Any(char.IsLetter) && password.Any(char.IsDigit);
        }

        /// <summary>
        /// Visual helper for inline real-time error highlighting.
        /// Soft red tint on error, clean white on normal.
        /// </summary>
        public static void HighlightError(TextBox tb, bool hasError)
        {
            tb.BackColor = hasError ? Color.FromArgb(254, 242, 242) : Color.White;
        }

        /// <summary>
        /// Visual helper for ComboBox error highlighting.
        /// </summary>
        public static void HighlightError(ComboBox cb, bool hasError)
        {
            cb.BackColor = hasError ? Color.FromArgb(254, 242, 242) : Color.White;
        }

        /// <summary>
        /// Sets or clears an inline field error for a TextBox in a single reusable call.
        /// </summary>
        public static void SetFieldError(TextBox tb, Label errLabel, string? message)
        {
            bool hasError = !string.IsNullOrEmpty(message);
            errLabel.UseMnemonic = false;
            errLabel.Text = message ?? string.Empty;
            errLabel.Visible = hasError;
            HighlightError(tb, hasError);
        }

        /// <summary>
        /// Sets or clears an inline field error for a ComboBox in a single reusable call.
        /// </summary>
        public static void SetFieldError(ComboBox cb, Label errLabel, string? message)
        {
            bool hasError = !string.IsNullOrEmpty(message);
            errLabel.UseMnemonic = false;
            errLabel.Text = message ?? string.Empty;
            errLabel.Visible = hasError;
            HighlightError(cb, hasError);
        }

        /// <summary>
        /// Sets or clears an inline field error for a DateTimePicker in a single reusable call.
        /// </summary>
        public static void SetFieldError(DateTimePicker dtp, Label errLabel, string? message)
        {
            bool hasError = !string.IsNullOrEmpty(message);
            errLabel.UseMnemonic = false;
            errLabel.Text = message ?? string.Empty;
            errLabel.Visible = hasError;
        }

        /// <summary>
        /// Clears field error state for a TextBox.
        /// </summary>
        public static void ClearFieldError(TextBox tb, Label errLabel)
        {
            SetFieldError(tb, errLabel, null);
        }

        /// <summary>
        /// Clears field error state for a ComboBox.
        /// </summary>
        public static void ClearFieldError(ComboBox cb, Label errLabel)
        {
            SetFieldError(cb, errLabel, null);
        }

        /// <summary>
        /// Clears field error state for a DateTimePicker.
        /// </summary>
        public static void ClearFieldError(DateTimePicker dtp, Label errLabel)
        {
            SetFieldError(dtp, errLabel, null);
        }
    }
}
