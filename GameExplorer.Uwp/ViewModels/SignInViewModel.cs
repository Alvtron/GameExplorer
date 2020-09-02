using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Security.Credentials;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.BaseViewModel" />
    public class SignInViewModel : BaseViewModel
    {
        /// <summary>
        /// The can close
        /// </summary>
        private bool _canClose;
        /// <summary>
        /// Gets or sets a value indicating whether this instance can close.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can close; otherwise, <c>false</c>.
        /// </value>
        public bool CanClose
        {
            get => _canClose;
            set
            {
                if (SetField(ref _canClose, value))
                {
                    OnPropertyChanged(nameof(_canClose));
                }
            }
        }

        /// <summary>
        /// The log in command
        /// </summary>
        private RelayCommand<PasswordCredential> _logInCommand;
        /// <summary>
        /// Gets the log in command.
        /// </summary>
        /// <value>
        /// The log in command.
        /// </value>
        public ICommand LogInCommand => _logInCommand = _logInCommand ?? new RelayCommand<PasswordCredential>(async param => await LogIn(param));

        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns></returns>
        public async Task LogIn(PasswordCredential credential)
        {
            credential.RetrievePassword();

            if (string.IsNullOrWhiteSpace(credential.Password))
            {
                AllUsersVisibility = false;
                SignInUserName = credential.UserName;
                SignInVisibility = true;
                return;
            }

            if (!await MainViewReference.Database.SignIn(credential.UserName, credential.Password))
            {
                ErrorText = "wrong username or password";
                return;
            }

            CanClose = true;
        }

        /// <summary>
        /// The delete users command
        /// </summary>
        private RelayCommand _deleteUsersCommand;
        /// <summary>
        /// Gets the delete users command.
        /// </summary>
        /// <value>
        /// The delete users command.
        /// </value>
        public ICommand DeleteUsersCommand => _deleteUsersCommand = _deleteUsersCommand ?? new RelayCommand(param => DeleteUsers());

        /// <summary>
        /// Deletes the users.
        /// </summary>
        private void DeleteUsers()
        {
            CredentialService.DeleteAll();
            Users.Clear();
            Refresh();
        }

        /// <summary>
        /// The sign in command
        /// </summary>
        private RelayCommand _signInCommand;
        /// <summary>
        /// Gets the sign in command.
        /// </summary>
        /// <value>
        /// The sign in command.
        /// </value>
        public ICommand SignInCommand => _signInCommand = _signInCommand ?? new RelayCommand(async param => await SignIn());

        /// <summary>
        /// Signs the in.
        /// </summary>
        /// <returns></returns>
        private async Task SignIn()
        {
            if (string.IsNullOrWhiteSpace(SignInUserName) || string.IsNullOrWhiteSpace(SignInPassword))
            {
                ErrorText = "Username or password are empty!";
                return;
            }

            // Attempt to sign in
            if (!await MainViewReference.Database.SignIn(SignInUserName, SignInPassword))
            {
                ErrorText = "Wrong username or password!";
                return;
            }

            CanClose = true;
        }

        /// <summary>
        /// The sign up command
        /// </summary>
        private RelayCommand _signUpCommand;
        /// <summary>
        /// Gets the sign up command.
        /// </summary>
        /// <value>
        /// The sign up command.
        /// </value>
        public ICommand SignUpCommand => _signUpCommand = _signUpCommand ?? new RelayCommand(async param => await SignUp());

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <returns></returns>
        private async Task SignUp()
        {
            if (!ValidSignUp())
                return;

            // Attempt to add user to database
            if (!await MainViewReference.Database.Add(SignUpEmail, SignUpUserName, SignUpPassword))
            {
                ErrorText = "User is not available.";
                return;
            }

            CanClose = true;
        }

        /// <summary>
        /// The sign in hint command
        /// </summary>
        private RelayCommand _signInHintCommand;
        /// <summary>
        /// Gets the sign in hint command.
        /// </summary>
        /// <value>
        /// The sign in hint command.
        /// </value>
        public ICommand SignInHintCommand => _signInHintCommand = _signInHintCommand ?? new RelayCommand(param => SignInHint());

        /// <summary>
        /// Signs the in hint.
        /// </summary>
        private void SignInHint()
        {
            SignInVisibility = true;
            SignUpVisibility = false;
        }

        /// <summary>
        /// The sign up hint command
        /// </summary>
        private RelayCommand _signUpHintCommand;
        /// <summary>
        /// Gets the sign up hint command.
        /// </summary>
        /// <value>
        /// The sign up hint command.
        /// </value>
        public ICommand SignUpHintCommand => _signUpHintCommand = _signUpHintCommand ?? new RelayCommand(param => SignUpHint());

        /// <summary>
        /// Signs up hint.
        /// </summary>
        private void SignUpHint()
        {
            SignInVisibility = false;
            SignUpVisibility = true;
            AllUsersVisibility = false;
        }

        /// <summary>
        /// The add user hint command
        /// </summary>
        private RelayCommand _addUserHintCommand;
        /// <summary>
        /// Gets the add user hint command.
        /// </summary>
        /// <value>
        /// The add user hint command.
        /// </value>
        public ICommand AddUserHintCommand => _addUserHintCommand = _addUserHintCommand ?? new RelayCommand(param => AddUserHint());


        /// <summary>
        /// Adds the user hint.
        /// </summary>
        private void AddUserHint()
        {
            AllUsersVisibility = false;
            SignUpVisibility = true;
        }

        /// <summary>
        /// The sign in visibility
        /// </summary>
        private bool _signInVisibility;
        /// <summary>
        /// Gets or sets a value indicating whether [sign in visibility].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sign in visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool SignInVisibility
        {
            get => _signInVisibility;
            set
            {
                if (SetField(ref _signInVisibility, value))
                {
                    OnPropertyChanged(nameof(_signInVisibility));
                }
            }
        }

        /// <summary>
        /// The sign up visibility
        /// </summary>
        private bool _signUpVisibility;
        /// <summary>
        /// Gets or sets a value indicating whether [sign up visibility].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sign up visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool SignUpVisibility
        {
            get => _signUpVisibility;
            set
            {
                if (SetField(ref _signUpVisibility, value))
                {
                    OnPropertyChanged(nameof(_signUpVisibility));
                }
            }
        }

        /// <summary>
        /// All users visibility
        /// </summary>
        private bool _allUsersVisibility = true;
        /// <summary>
        /// Gets or sets a value indicating whether [all users visibility].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [all users visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool AllUsersVisibility
        {
            get => _allUsersVisibility;
            set
            {
                if (SetField(ref _allUsersVisibility, value))
                {
                    OnPropertyChanged(nameof(_allUsersVisibility));
                }
            }
        }

        /// <summary>
        /// The error text
        /// </summary>
        private string _errorText;
        /// <summary>
        /// Gets or sets the error text.
        /// </summary>
        /// <value>
        /// The error text.
        /// </value>
        public string ErrorText
        {
            get => _errorText;
            set
            {
                if (SetField(ref _errorText, value))
                {
                    OnPropertyChanged(nameof(_errorText));
                }
            }
        }

        /// <summary>
        /// The sign in user name
        /// </summary>
        private string _signInUserName;
        /// <summary>
        /// Gets or sets the name of the sign in user.
        /// </summary>
        /// <value>
        /// The name of the sign in user.
        /// </value>
        public string SignInUserName
        {
            get => _signInUserName;
            set
            {
                if (SetField(ref _signInUserName, value))
                {
                    OnPropertyChanged(nameof(_signInUserName));
                }
            }
        }

        /// <summary>
        /// The sign in password
        /// </summary>
        private string _signInPassword;
        /// <summary>
        /// Gets or sets the sign in password.
        /// </summary>
        /// <value>
        /// The sign in password.
        /// </value>
        public string SignInPassword
        {
            get => _signInPassword;
            set
            {
                if (SetField(ref _signInPassword, value))
                {
                    OnPropertyChanged(nameof(_signInPassword));
                }
            }
        }

        /// <summary>
        /// The sign up email
        /// </summary>
        private string _signUpEmail;
        /// <summary>
        /// Gets or sets the sign up email.
        /// </summary>
        /// <value>
        /// The sign up email.
        /// </value>
        public string SignUpEmail
        {
            get => _signUpEmail;
            set
            {
                if (SetField(ref _signUpEmail, value))
                {
                    OnPropertyChanged(nameof(_signUpEmail));
                    ValidateEmail();
                }
            }
        }

        /// <summary>
        /// The sign up user name
        /// </summary>
        private string _signUpUserName;
        /// <summary>
        /// Gets or sets the name of the sign up user.
        /// </summary>
        /// <value>
        /// The name of the sign up user.
        /// </value>
        public string SignUpUserName
        {
            get => _signUpUserName;
            set
            {
                if (SetField(ref _signUpUserName, value))
                {
                    OnPropertyChanged(nameof(_signUpUserName));
                    ValidateUserName();
                }
            }
        }

        /// <summary>
        /// The sign up password
        /// </summary>
        private string _signUpPassword;
        /// <summary>
        /// Gets or sets the sign up password.
        /// </summary>
        /// <value>
        /// The sign up password.
        /// </value>
        public string SignUpPassword
        {
            get => _signUpPassword;
            set
            {
                if (SetField(ref _signUpPassword, value))
                {
                    OnPropertyChanged(nameof(_signUpPassword));
                    ValidatePassword();
                    ValidateReEnteredPasword();
                }
            }
        }

        /// <summary>
        /// The sign up re enter password
        /// </summary>
        private string _signUpReEnterPassword;
        /// <summary>
        /// Gets or sets the sign up re enter password.
        /// </summary>
        /// <value>
        /// The sign up re enter password.
        /// </value>
        public string SignUpReEnterPassword
        {
            get => _signUpReEnterPassword;
            set
            {
                if (SetField(ref _signUpReEnterPassword, value))
                {
                    OnPropertyChanged(nameof(_signUpReEnterPassword));
                    ValidatePassword();
                    ValidateReEnteredPasword();
                }
            }
        }

        /// <summary>
        /// The sign up email border
        /// </summary>
        private SolidColorBrush _signUpEmailBorder = new SolidColorBrush(Colors.Gray);
        /// <summary>
        /// Gets or sets the sign up email border.
        /// </summary>
        /// <value>
        /// The sign up email border.
        /// </value>
        public SolidColorBrush SignUpEmailBorder
        {
            get => _signUpEmailBorder;
            set
            {
                if (SetField(ref _signUpEmailBorder, value))
                {
                    OnPropertyChanged(nameof(_signUpEmailBorder));
                }
            }
        }

        /// <summary>
        /// The sign up user name border
        /// </summary>
        private SolidColorBrush _signUpUserNameBorder = new SolidColorBrush(Colors.Gray);
        /// <summary>
        /// Gets or sets the sign up user name border.
        /// </summary>
        /// <value>
        /// The sign up user name border.
        /// </value>
        public SolidColorBrush SignUpUserNameBorder
        {
            get => _signUpUserNameBorder;
            set
            {
                if (SetField(ref _signUpUserNameBorder, value))
                {
                    OnPropertyChanged(nameof(_signUpUserNameBorder));
                }
            }
        }

        /// <summary>
        /// The sign up password border
        /// </summary>
        private SolidColorBrush _signUpPasswordBorder = new SolidColorBrush(Colors.Gray);
        /// <summary>
        /// Gets or sets the sign up password border.
        /// </summary>
        /// <value>
        /// The sign up password border.
        /// </value>
        public SolidColorBrush SignUpPasswordBorder
        {
            get => _signUpPasswordBorder;
            set
            {
                if (SetField(ref _signUpPasswordBorder, value))
                {
                    OnPropertyChanged(nameof(_signUpPasswordBorder));
                }
            }
        }

        /// <summary>
        /// The sign up re enter password border
        /// </summary>
        private SolidColorBrush _signUpReEnterPasswordBorder = new SolidColorBrush(Colors.Gray);
        /// <summary>
        /// Gets or sets the sign up re enter password border.
        /// </summary>
        /// <value>
        /// The sign up re enter password border.
        /// </value>
        public SolidColorBrush SignUpReEnterPasswordBorder
        {
            get => _signUpReEnterPasswordBorder;
            set
            {
                if (SetField(ref _signUpReEnterPasswordBorder, value))
                {
                    OnPropertyChanged(nameof(_signUpReEnterPasswordBorder));
                }
            }
        }

        /// <summary>
        /// The users
        /// </summary>
        private ObservableCollection<PasswordCredential> _users = new ObservableCollection<PasswordCredential>();
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollection<PasswordCredential> Users
        {
            get => _users;
            set
            {
                if (SetField(ref _users, value))
                {
                    OnPropertyChanged(nameof(_users));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInViewModel"/> class.
        /// </summary>
        public SignInViewModel()
        {
            Refresh();
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            CredentialService.GetAll().ForEach(x => Users.Add(x));
        }

        /// <summary>
        /// Valids the sign up.
        /// </summary>
        /// <returns></returns>
        private bool ValidSignUp()
        {
            return (ValidateEmail() && ValidateUserName() && ValidatePassword() && ValidateReEnteredPasword());
        }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <returns></returns>
        private bool ValidateEmail()
        {
            if (Validation.ValidateEmail(SignUpEmail) != Validation.Email.Valid)
            {
                SignUpEmailBorder = new SolidColorBrush(Colors.Red);
                return false;
            }
            SignUpEmailBorder = new SolidColorBrush(Colors.Green);
            return true;
        }

        /// <summary>
        /// Validates the name of the user.
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserName()
        {
            if (Validation.ValidateUsername(SignUpUserName) != Validation.Username.Valid)
            {
                SignUpUserNameBorder = new SolidColorBrush(Colors.Red);
                return false;
            }
            SignUpUserNameBorder = new SolidColorBrush(Colors.Green);
            return true;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <returns></returns>
        private bool ValidatePassword()
        {
            if (Validation.ValidatePassword(SignUpPassword) != Validation.Password.Valid)
            {
                SignUpPasswordBorder = new SolidColorBrush(Colors.Red);
                return false;
            }
            SignUpPasswordBorder = new SolidColorBrush(Colors.Green);
            return true;
        }

        /// <summary>
        /// Validates the re entered pasword.
        /// </summary>
        /// <returns></returns>
        private bool ValidateReEnteredPasword()
        {
            if (SignUpPassword != SignUpReEnterPassword)
            {
                SignUpReEnterPasswordBorder = new SolidColorBrush(Colors.Red);
                return false;
            }
            SignUpReEnterPasswordBorder = new SolidColorBrush(Colors.Green);
            return true;
        }
    }
}
