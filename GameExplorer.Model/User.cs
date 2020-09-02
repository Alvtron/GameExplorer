using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class User : DatabaseItem
    {
        /// <summary>
        /// The username
        /// </summary>
        private string _username;
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get => _username;
            set
            {
                if (SetField(ref _username, value))
                {
                    OnPropertyChanged(nameof(_username));
                }
            }
        }

        /// <summary>
        /// The email
        /// </summary>
        private string _email;
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email
        {
            get => _email;
            set
            {
                if (SetField(ref _email, value))
                {
                    OnPropertyChanged(nameof(_email));
                }
            }
        }

        /// <summary>
        /// The password
        /// </summary>
        private Password _password = new Password();
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public Password Password
        {
            get => _password;
            set
            {
                if (SetField(ref _password, value))
                {
                    OnPropertyChanged(nameof(_password));
                }
            }
        }

        /// <summary>
        /// The first name
        /// </summary>
        private string _firstName;
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (SetField(ref _firstName, value))
                {
                    OnPropertyChanged(nameof(_firstName));
                }
            }
        }

        /// <summary>
        /// The last name
        /// </summary>
        private string _lastName;
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (SetField(ref _lastName, value))
                {
                    OnPropertyChanged(nameof(_lastName));
                }
            }
        }

        /// <summary>
        /// Gets the initials.
        /// </summary>
        /// <value>
        /// The initials.
        /// </value>
        [IgnoreDataMember]
        public string Initials => (FirstName?.Length > 0 && LastName?.Length > 0) ? FirstName?.Substring(0, 1) + LastName?.Substring(0, 1) : "";

        /// <summary>
        /// The birthday
        /// </summary>
        private DateTime? _birthday = DateTime.Now;
        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>
        /// The birthday.
        /// </value>
        public DateTime? Birthday
        {
            get => _birthday;
            set
            {
                if (SetField(ref _birthday, value))
                {
                    OnPropertyChanged(nameof(_birthday));
                }
            }
        }

        /// <summary>
        /// The gender
        /// </summary>
        private string _gender;
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender
        {
            get => _gender;
            set
            {
                if (SetField(ref _gender, value))
                {
                    OnPropertyChanged(nameof(_gender));
                }
            }
        }

        /// <summary>
        /// The country
        /// </summary>
        private string _country;
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country
        {
            get => _country;
            set
            {
                if (SetField(ref _country, value))
                {
                    OnPropertyChanged(nameof(_country));
                }
            }
        }

        /// <summary>
        /// The bio
        /// </summary>
        private string _bio;
        /// <summary>
        /// Gets or sets the bio.
        /// </summary>
        /// <value>
        /// The bio.
        /// </value>
        public string Bio
        {
            get => _bio;
            set
            {
                if (SetField(ref _bio, value))
                {
                    OnPropertyChanged(nameof(_bio));
                }
            }
        }

        /// <summary>
        /// The website
        /// </summary>
        private string _website;
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website
        {
            get => _website;
            set
            {
                if (SetField(ref _website, value))
                {
                    OnPropertyChanged(nameof(_website));
                }
            }
        }

        /// <summary>
        /// The experience
        /// </summary>
        private int _experience;
        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        /// <value>
        /// The experience.
        /// </value>
        public int Experience
        {
            get => _experience;
            set
            {
                if (SetField(ref _experience, value))
                {
                    OnPropertyChanged(nameof(_experience));
                }
            }
        }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        /// <value>
        /// The current level.
        /// </value>
        [IgnoreDataMember]
        public int CurrentLevel => Model.Experience.ExpToLevel(Experience);
        /// <summary>
        /// Gets the next level.
        /// </summary>
        /// <value>
        /// The next level.
        /// </value>
        [IgnoreDataMember]
        public int NextLevel => CurrentLevel + 1;
        /// <summary>
        /// Gets the next exp.
        /// </summary>
        /// <value>
        /// The next exp.
        /// </value>
        [IgnoreDataMember]
        public int NextExp => NextLevel * Model.Experience.LevelUpExp;

        /// <summary>
        /// The photo
        /// </summary>
        private Image _photo;
        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public Image Photo
        {
            get => _photo;
            set
            {
                if (SetField(ref _photo, value))
                {
                    OnPropertyChanged(nameof(_photo));
                }
            }
        }

        /// <summary>
        /// The signed up
        /// </summary>
        private DateTime? _signedUp = DateTime.Now;
        /// <summary>
        /// Gets or sets the signed up.
        /// </summary>
        /// <value>
        /// The signed up.
        /// </value>
        public DateTime? SignedUp
        {
            get => _signedUp;
            set
            {
                if (SetField(ref _signedUp, value))
                {
                    OnPropertyChanged(nameof(_signedUp));
                }
            }
        }

        /// <summary>
        /// The signed in
        /// </summary>
        private DateTime? _signedIn = DateTime.Now;
        /// <summary>
        /// Gets or sets the signed in.
        /// </summary>
        /// <value>
        /// The signed in.
        /// </value>
        public DateTime? SignedIn
        {
            get => _signedIn;
            set
            {
                if (SetField(ref _signedIn, value))
                {
                    OnPropertyChanged(nameof(_signedIn));
                }
            }
        }

        /// <summary>
        /// The signed out
        /// </summary>
        private DateTime? _signedOut = DateTime.Now;
        /// <summary>
        /// Gets or sets the signed out.
        /// </summary>
        /// <value>
        /// The signed out.
        /// </value>
        public DateTime? SignedOut
        {
            get => _signedOut;
            set
            {
                if (SetField(ref _signedOut, value))
                {
                    OnPropertyChanged(nameof(_signedOut));
                }
            }
        }

        /// <summary>
        /// The subscribed
        /// </summary>
        private bool _subscribed;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is subscribed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if subscribed; otherwise, <c>false</c>.
        /// </value>
        public bool Subscribed
        {
            get => _subscribed;
            set
            {
                if (SetField(ref _subscribed, value))
                {
                    OnPropertyChanged(nameof(_subscribed));
                }
            }
        }

        /// <summary>
        /// The color
        /// </summary>
        private Color _color = new Color();
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get => _color;
            set
            {
                if (SetField(ref _color, value))
                {
                    OnPropertyChanged(nameof(_color));
                }
            }
        }

        /// <summary>
        /// Gets or sets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();
        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>
        /// The friends.
        /// </value>
        public ObservableCollection<User> Friends { get; set; } = new ObservableCollection<User>();
        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public ObservableCollection<Log> Logs { get; set; } = new ObservableCollection<Log>();

        /* METHODS */

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public User(string username, string email, string password)
        {
            if (Validation.ValidateUsername(username) != Validation.Username.Valid) return;
            if (Validation.ValidateEmail(email) != Validation.Email.Valid) return;
            if (Validation.ValidatePassword(password) != Validation.Password.Valid) return;

            Username = username;
            Email = email;
            CreateNewPassword(password);
            SignedUp = DateTime.Now;
            Logs.Add(new Log(this, "signed up"));
        }

        /// <summary>
        /// Creates the new password.
        /// </summary>
        /// <param name="password">The password.</param>
        public void CreateNewPassword(string password)
        {
            const int saltSize = 24;
            const int hashSize = 20;
            const int pbkdf2Iterations = 1000;

            // Create salt 24 bytes large
            var cryptoProvider = new RNGCryptoServiceProvider();
            var saltSizeByte = new byte[saltSize];
            cryptoProvider.GetBytes(saltSizeByte);

            // Create hash with salt and iterations, 20 bytes large
            Password = new Password(pbkdf2Iterations, saltSizeByte, CreateHash(password, saltSizeByte, pbkdf2Iterations, hashSize));

            // Release resources
            cryptoProvider.Dispose();
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            var newHash = CreateHash(password, Password.Salt, Password.Iterations, Convert.FromBase64String(Password.Hash).Length);

            return newHash.Equals(Password.Hash);
        }

        /// <summary>
        /// Creates the hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The iterations.</param>
        /// <param name="outputBytes">The output bytes.</param>
        /// <returns></returns>
        private static string CreateHash(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) { IterationCount = iterations };

            var newHash = pbkdf2.GetBytes(outputBytes);

            pbkdf2.Dispose();

            return Convert.ToBase64String(newHash);
        }

        /// <summary>
        /// Matches the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public bool Match(User user)
        {
            Debug.WriteLine($"Uid == user?.Uid -> {Uid == user?.Uid} || Username == user?.Username -> {Username == user?.Username} || Email == user?.Email -> {Email == user?.Email}");
            return Uid == user?.Uid || Username == user?.Username || Email == user?.Email;
        }

        /// <summary>
        /// Increases the experience.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public int IncreaseExperience(Experience.Action action)
        {
            return Experience += (int)action;
        }

        /// <summary>
        /// Decreases the experience.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void DecreaseExperience(int amount)
        {
            if (amount < 0)
                return;

            Experience -= amount;
        }

        /// <summary>
        /// Adds the game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void AddGame(Game game)
        {
            if (!Games.Contains(game))
            {
                Games.Add(game);
                Logs.Add(new Log(this, "added " + game.Title));
            }
        }

        /// <summary>
        /// Removes the game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void RemoveGame(Game game)
        {
            if (Games.Contains(game))
            {
                Games.Remove(game);
                Logs.Add(new Log(this, "removed " + game.Title));
            }
        }

        /// <summary>
        /// Toggles the add game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void ToggleAddGame(Game game)
        {
            if (Games.Contains(game))
                RemoveGame(game);
            else
                AddGame(game);

        }

        /// <summary>
        /// Sets the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        public void SetPhoto(Image photo)
        {
            if (photo == null)
                return;
            if (photo.Empty)
                return;

            Photo = photo;
            Logs.Add(new Log(this, "changed " + ((Gender == "Male") ? " his" : " her") + " photo"));
        }

        /// <summary>
        /// Adds the friend.
        /// </summary>
        /// <param name="friend">The friend.</param>
        public void AddFriend(User friend)
        {
            if (friend == null || Uid == friend.Uid || HasFriend(friend))
                return;

            Friends.Add(friend);
            Logs.Add(new Log(this, " and " + friend.Username + " are now friends"));
        }

        /// <summary>
        /// Determines whether the specified friend has friend.
        /// </summary>
        /// <param name="friend">The friend.</param>
        /// <returns>
        ///   <c>true</c> if the specified friend has friend; otherwise, <c>false</c>.
        /// </returns>
        public bool HasFriend(User friend)
        {
            return Friends.Any(e => e.Uid == friend.Uid);
        }

        /// <summary>
        /// Signs the in.
        /// </summary>
        public void SignIn()
        {
            SignedIn = DateTime.Now;
            Logs.Add(new Log(this, "signed in"));
        }

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        public void SignOut()
        {
            SignedOut = DateTime.Now;
            Logs.Add(new Log(this, "signed out"));
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void SetColor(Color color)
        {
            if (color == null) return;

            Color = color;
            Logs.Add(new Log(this, "changed " + ((Gender == "Male") ? " his" : " her") + " color"));
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a">a.</param>
        public void SetColor(byte r, byte g, byte b, byte a)
        {
            SetColor(new Color(r, g, b, a));
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Username;
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [IgnoreDataMember]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets the birthday string.
        /// </summary>
        /// <value>
        /// The birthday string.
        /// </value>
        [IgnoreDataMember]
        public string BirthdayString => Birthday?.ToLongDateString();

        /// <summary>
        /// Gets the signed up string.
        /// </summary>
        /// <value>
        /// The signed up string.
        /// </value>
        [IgnoreDataMember]
        public string SignedUpString => SignedUp?.ToShortDateString();

        /// <summary>
        /// Gets the signed in string.
        /// </summary>
        /// <value>
        /// The signed in string.
        /// </value>
        [IgnoreDataMember]
        public string SignedInString => SignedIn?.ToShortDateString();

        /// <summary>
        /// Gets a value indicating whether this <see cref="User"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Valid => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Email) && Password != null;
    }
}
