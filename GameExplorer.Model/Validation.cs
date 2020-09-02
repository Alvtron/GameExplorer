using System.Text.RegularExpressions;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// 
        /// </summary>
        public enum General
        {
            /// <summary>
            /// The empty
            /// </summary>
            Empty,
            /// <summary>
            /// The already exist
            /// </summary>
            AlreadyExist,
            /// <summary>
            /// The dont exist
            /// </summary>
            DontExist
        };

        /// <summary>
        /// 
        /// </summary>
        public enum User
        {
            /// <summary>
            /// The invalid username
            /// </summary>
            InvalidUsername,
            /// <summary>
            /// The invalid email
            /// </summary>
            InvalidEmail,
            /// <summary>
            /// The invalid password
            /// </summary>
            InvalidPassword,
            /// <summary>
            /// The user created
            /// </summary>
            UserCreated
        };

        /// <summary>
        /// 
        /// </summary>
        public enum Username
        {
            /// <summary>
            /// The empty
            /// </summary>
            Empty,
            /// <summary>
            /// The too short
            /// </summary>
            TooShort,
            /// <summary>
            /// The too long
            /// </summary>
            TooLong,
            /// <summary>
            /// The contains illegal characters
            /// </summary>
            ContainsIllegalCharacters,
            /// <summary>
            /// The already exist
            /// </summary>
            AlreadyExist,
            /// <summary>
            /// The unavailable
            /// </summary>
            Unavailable,
            /// <summary>
            /// The valid
            /// </summary>
            Valid
        };

        /// <summary>
        /// 
        /// </summary>
        public enum Password
        {
            /// <summary>
            /// The empty
            /// </summary>
            Empty,
            /// <summary>
            /// The too short
            /// </summary>
            TooShort,
            /// <summary>
            /// The too long
            /// </summary>
            TooLong,
            /// <summary>
            /// The no symbol
            /// </summary>
            NoSymbol,
            /// <summary>
            /// The no number
            /// </summary>
            NoNumber,
            /// <summary>
            /// The no lower case
            /// </summary>
            NoLowerCase,
            /// <summary>
            /// The no upper case
            /// </summary>
            NoUpperCase,
            /// <summary>
            /// The valid
            /// </summary>
            Valid
        };

        /// <summary>
        /// 
        /// </summary>
        public enum Email
        {
            /// <summary>
            /// The empty
            /// </summary>
            Empty,
            /// <summary>
            /// The invalid
            /// </summary>
            Invalid,
            /// <summary>
            /// The valid
            /// </summary>
            Valid
        };

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public static Email ValidateEmail(string email)
        {
            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrWhiteSpace(email))
                return Email.Empty;

            if (!emailRegex.IsMatch(email))
                return Email.Invalid;

            return Email.Valid;
        }

        /// <summary>
        /// Validates the username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public static Username ValidateUsername(string username)
        {
            const int minLength = 3, maxLength = 30;
            var validCharacters = new Regex(@"[a-zA-Z0-9¨_]+$");

            if (string.IsNullOrWhiteSpace(username))
                return Username.Empty;

            if (username.Length <= minLength)
                return Username.TooShort;

            if (username.Length >= maxLength)
                return Username.TooLong;

            if (!validCharacters.IsMatch(username))
                return Username.ContainsIllegalCharacters;

            return Username.Valid;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static Password ValidatePassword(string password)
        {
            const int minLength = 6, maxLength = 100;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (string.IsNullOrWhiteSpace(password))
                return Password.Empty;

            if (password.Length <= minLength)
                return Password.TooShort;

            if (password.Length >= maxLength)
                return Password.TooLong;

            if (!hasLowerChar.IsMatch(password))
                return Password.NoLowerCase;

            if (!hasUpperChar.IsMatch(password))
                return Password.NoUpperCase;

            if (!hasNumber.IsMatch(password))
                return Password.NoNumber;

            if (!hasSymbols.IsMatch(password))
                return Password.NoSymbol;

            return Password.Valid;
        }
    }
}
