using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using GameExplorer.Uwp.DataSource;

namespace GameExplorer.Uwp.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class DatabaseService : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum Response
        {
            /// <summary>
            /// The success
            /// </summary>
            Success,
            /// <summary>
            /// The failed
            /// </summary>
            Failed
        }

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();
        /// <summary>
        /// Gets the quests.
        /// </summary>
        /// <value>
        /// The quests.
        /// </value>
        public ObservableCollection<Quest> Quests { get; } = new ObservableCollection<Quest>();

        /// <summary>
        /// Gets the wikis.
        /// </summary>
        /// <value>
        /// The wikis.
        /// </value>
        public ObservableCollection<Wiki> Wikis { get; } = new ObservableCollection<Wiki>();

        /// <summary>
        /// Combines all.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SearchablePost> CombineAll()
        {
            var collection = new List<SearchablePost>();

            foreach (var game in Games)
            {
                collection.Add(game);
            }
            foreach (var quest in Quests)
            {
                collection.Add(quest);
            }
            foreach (var wiki in Wikis)
            {
                collection.Add(wiki);
            }

            return new ObservableCollection<SearchablePost>(collection);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        /// <summary>
        /// The current user
        /// </summary>
        private User _currentUser;
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                if (!SetField(ref _currentUser, value)) return;

                OnPropertyChanged(nameof(_currentUser));
                OnPropertyChanged(nameof(HasUser));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has user; otherwise, <c>false</c>.
        /// </value>
        public bool HasUser => CurrentUser != null;

        /// <summary>
        /// The current game
        /// </summary>
        private Game _currentGame;

        /// <summary>
        /// Gets or sets the current game.
        /// </summary>
        /// <value>
        /// The current game.
        /// </value>
        public Game CurrentGame
        {
            get => _currentGame;
            set
            {
                if (!SetField(ref _currentGame, value)) return;

                OnPropertyChanged(nameof(_currentGame));
                OnPropertyChanged(nameof(HasGame));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has game; otherwise, <c>false</c>.
        /// </value>
        public bool HasGame => CurrentGame != null;

        /// <summary>
        /// Loads the database.
        /// </summary>
        /// <returns></returns>
        public async Task LoadDatabase()
        {
            await LoadCurrentUser();
            LoadCurrentGame();
            await UpdateGames();
            await UpdateQuests();
            await UpdateWikis();
            await UpdateUsers();

            Debug.WriteLine("Database information:");
            Debug.WriteLine($"Current User: {CurrentUser?.Username}");
            Debug.WriteLine($"Current Game: {CurrentUser?.Username}");
            Debug.WriteLine($"Games: {Games?.Count}");
            Debug.WriteLine($"Quests: {Quests?.Count}");
            Debug.WriteLine($"Wikis: {Wikis?.Count}");
            Debug.WriteLine($"Users: {Users?.Count}");
        }

        /// <summary>
        /// Updates the games.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateGames()
        {
            Games.Clear();
            var games = await GamesDataSource.Get();
            games?.ToList().ForEach(Games.Add);
        }

        /// <summary>
        /// Updates the quests.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateQuests()
        {
            Quests.Clear();
            var quests = await QuestsDataSource.Get();
            quests?.ToList().ForEach(Quests.Add);
        }

        /// <summary>
        /// Updates the wikis.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateWikis()
        {
            Wikis.Clear();
            var wikis = await WikisDataSource.Get();
            wikis?.ToList().ForEach(Wikis.Add);
        }

        /// <summary>
        /// Updates the users.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateUsers()
        {
            Users.Clear();
            var users = await UsersDataSource.Get();
            users?.ToList().ForEach(Users.Add);
        }

        #region User

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public async Task<User> GetUser(Guid guid)
        {
            try
            {
                return await UsersDataSource.Get(guid);
            }
            catch (HttpRequestException exception)
            {
                Debug.WriteLine($"DatabaseService: {exception.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns></returns>
        public async Task<User> GetUser(PasswordCredential credential)
        {
            credential.RetrievePassword();
            return await GetUser(credential.UserName, credential.Password);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<User> GetUser(string username)
        {
            await UpdateUsers();
            return Users.FirstOrDefault(u => u.Username == username);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<User> GetUser(string username, string password)
        {
            await UpdateUsers();
            return Users.FirstOrDefault(u => u.Username == username && u.ValidatePassword(password));
        }

        /// <summary>
        /// Loads the current user.
        /// </summary>
        /// <returns></returns>
        public async Task<User> LoadCurrentUser()
        {
            var currentCredential = CredentialService.GetCurrent();
            currentCredential?.RetrievePassword();

            if (string.IsNullOrWhiteSpace(currentCredential?.Password)) return CurrentUser = null;

            return CurrentUser = await GetUser(currentCredential?.UserName, currentCredential?.Password);
        }

        /// <summary>
        /// Loads the current game.
        /// </summary>
        /// <returns></returns>
        public Game LoadCurrentGame()
        {
            if (string.IsNullOrWhiteSpace(Settings.DefaultGameUid)) return null;

            return CurrentGame = CurrentUser?.Games?.FirstOrDefault(i => i.Uid.ToString() == Settings.DefaultGameUid);
        }

        /// <summary>
        /// Signs the in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<bool> SignIn(string username, string password)
        {
            CurrentUser = await GetUser(username, password);

            if (CurrentUser == null) return false;

            CurrentUser.SignIn();
            CredentialService.AddToVault(username, password);
            return true;
        }

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        /// <returns></returns>
        public async Task SignOut()
        {
            CurrentUser.SignOut();
            await Upload(CurrentUser);
            CurrentUser.Photo = null;
            CurrentUser = null;
        }

        #endregion

        #region Get

        /// <summary>
        /// Gets the reward types.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<string>> GetRewardTypes()
        {
            var DistinctTypes = (await RewardsDataSource.Get()).SelectMany(e => e.Type).Distinct().ToList();
            return DistinctTypes.Select(c => c.ToString()).ToList();
        }

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <returns></returns>
        public static async Task<ObservableCollection<SearchablePost>> GetGames()
        {
            return new ObservableCollection<SearchablePost>(await WikisDataSource.Get());
        }

        /// <summary>
        /// Gets the questlines.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public static async Task<ObservableCollection<Questline>> GetQuestlines(Game game)
        {
            return new ObservableCollection<Questline>(await QuestlinesDataSource.Get());
        }

        /// <summary>
        /// Gets the rewards.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public static async Task<ObservableCollection<Reward>> GetRewards(Game game)
        {
            return new ObservableCollection<Reward>(await RewardsDataSource.Get());
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public async Task<Game> GetGame(Guid guid)
        {
            await UpdateGames();
            return Games.FirstOrDefault(i => i.Uid == guid);
        }

        /// <summary>
        /// Gets the quest.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public async Task<Quest> GetQuest(Guid guid)
        {
            await UpdateQuests();
            return Quests.FirstOrDefault(i => i.Uid == guid);
        }

        /// <summary>
        /// Gets the wiki.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public async Task<Wiki> GetWiki(Guid guid)
        {
            await UpdateWikis();
            return Wikis.FirstOrDefault(i => i.Uid == guid);
        }

        #endregion

        #region Update

        /// <summary>
        /// Uploads the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<Response> Upload(User user)
        {
            Debug.WriteLine("DatabaseService: Upload(User): Attempting to upload.");

            if (Users.Any(u => u.Uid == user.Uid) && !await UsersDataSource.Update(user))
                {
                    Debug.WriteLine("DatabaseService: Upload(User): Upload failed!");
                    return Response.Failed;
                }
            if (!await UsersDataSource.Add(user))
            {
                Debug.WriteLine("DatabaseService: Upload(User): Upload failed!");
                return Response.Failed;
            }

            Debug.WriteLine("DatabaseService: Upload(User): Upload was successful!");
            return Response.Success;
        }

        /// <summary>
        /// Uploads the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public async Task<Response> Upload(Game game)
        {
            Debug.WriteLine("DatabaseService: Upload(Game): Attempting to upload.");

            if (Users.Any(u => u.Uid == game.Uid) && !await GamesDataSource.Update(game))
            {
                Debug.WriteLine("DatabaseService: Upload(Game): Upload failed!");
                return Response.Failed;
            }
            if (!await GamesDataSource.Add(game))
            {
                Debug.WriteLine("DatabaseService: Upload(Game): Upload failed!");
                return Response.Failed;
            }

            Debug.WriteLine("DatabaseService: Upload(Game): Upload was successful!");
            return Response.Success;
        }

        /// <summary>
        /// Uploads the specified quest.
        /// </summary>
        /// <param name="quest">The quest.</param>
        /// <returns></returns>
        public async Task<Response> Upload(Quest quest)
        {
            Debug.WriteLine("DatabaseService: Upload(Quest): Attempting to upload.");

            if (Users.Any(u => u.Uid == quest.Uid) && !await QuestsDataSource.Update(quest))
            {
                Debug.WriteLine("DatabaseService: Upload(Quest): Upload failed!");
                return Response.Failed;
            }
            if (!await QuestsDataSource.Add(quest))
            {
                Debug.WriteLine("DatabaseService: Upload(Quest): Upload failed!");
                return Response.Failed;
            }

            Debug.WriteLine("DatabaseService: Upload(Quest): Upload was successful!");
            return Response.Success;
        }

        /// <summary>
        /// Uploads the specified wiki.
        /// </summary>
        /// <param name="wiki">The wiki.</param>
        /// <returns></returns>
        public async Task<Response> Upload(Wiki wiki)
        {
            Debug.WriteLine("DatabaseService: Upload(Wiki): Attempting to upload.");

            if (Users.Any(u => u.Uid == wiki.Uid) && !await WikisDataSource.Update(wiki))
            {
                Debug.WriteLine("DatabaseService: Upload(Wiki): Upload failed!");
                return Response.Failed;
            }
            if (!await WikisDataSource.Add(wiki))
            {
                Debug.WriteLine("DatabaseService: Upload(Wiki): Upload failed!");
                return Response.Failed;
            }

            Debug.WriteLine("DatabaseService: Upload(Wiki): Upload was successful!");
            return Response.Success;
        }

        /// <summary>
        /// Uploads the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        public async Task<Response> Upload(Report report)
        {
            Debug.WriteLine("DatabaseService: Upload(Report): Attempting to upload user");

            if (Users.Any(u => u.Uid == report.Uid) && !await ReportsDataSource.Update(report))
            {
                Debug.WriteLine("DatabaseService: Upload(Report): Upload failed!");
                return Response.Failed;
            }
            if (!await ReportsDataSource.Add(report))
            {
                Debug.WriteLine("DatabaseService: Upload(Report): Upload failed!");
                return Response.Failed;
            }

            Debug.WriteLine("DatabaseService: Upload(Report): Upload was successful!");
            return Response.Success;
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<bool> Add(string email, string userName, string password)
        {
            Debug.WriteLine("DatabaseService: Add(User): Attempting to create new user");
            var newUser = new User(userName, email, password);

            if (!newUser.Valid)
            {
                Debug.WriteLine("DatabaseService: Add(User): User was not valid");
                return false;
            }

            // Attempt to upload
            if (!await UsersDataSource.Add(newUser))
            {
                Debug.WriteLine("DatabaseService: Add(User): Upload was unsuccessful");
                return false;
            }

            Debug.WriteLine("DatabaseService: Add(User): User was successfully added");
            CurrentUser = newUser;
            Users.Add(newUser);
            // Add user to vault.
            CredentialService.AddToVault(userName, password);
            return true;
        }

        /// <summary>
        /// Availables the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<bool> Available(User user)
        {
            await UpdateUsers();
            return Users.All(u => !u.Match(user));
        }

        /// <summary>
        /// Adds the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public async Task<bool> Add(Game game)
        {
            if (game == null)
            {
                Debug.WriteLine("DatabaseService: Add(Game): Game was null");
                return false;
            }

            if (!game.Valid)
            {
                Debug.WriteLine("DatabaseService: Add(Game): Game was not valid");
                return false;
            }

            // Attempt to upload
            if (!await GamesDataSource.Add(game))
            {
                Debug.WriteLine("DatabaseService: Add(Game): Upload was unsuccessful");
                return false;
            }

            Debug.WriteLine("DatabaseService: Add(Game): Game was successfully added");
            Games.Add(game);
            return true;
        }

        /// <summary>
        /// Availables the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public async Task<bool> Available(Game game)
        {
            await UpdateGames();
            return Games.All(g => g.Uid != game.Uid);
        }

        /// <summary>
        /// Adds the specified quest.
        /// </summary>
        /// <param name="quest">The quest.</param>
        /// <returns></returns>
        public async Task<bool> Add(Quest quest)
        {
            if (quest == null)
            {
                Debug.WriteLine("DatabaseService: Add(quest): Quest was null");
                return false;
            }

            if (!quest.Valid)
            {
                Debug.WriteLine("DatabaseService: Add(quest): Quest was not valid");
                return false;
            }

            // Attempt to upload
            if (!await QuestsDataSource.Add(quest))
            {
                Debug.WriteLine("DatabaseService: Add(quest): Upload was unsuccessful");
                return false;
            }

            Debug.WriteLine("DatabaseService: Add(quest): Quest was successfully added");
            Quests.Add(quest);
            return true;
        }

        /// <summary>
        /// Availables the specified quest.
        /// </summary>
        /// <param name="quest">The quest.</param>
        /// <returns></returns>
        public async Task<bool> Available(Quest quest)
        {
            await UpdateQuests();
            return Quests.All(g => g.Uid != quest.Uid);
        }

        /// <summary>
        /// Adds the specified wiki.
        /// </summary>
        /// <param name="wiki">The wiki.</param>
        /// <returns></returns>
        public async Task<bool> Add(Wiki wiki)
        {
            if (wiki == null)
            {
                Debug.WriteLine("DatabaseService: Add(Wiki): Wiki was null");
                return false;
            }

            if (!wiki.Valid)
            {
                Debug.WriteLine("DatabaseService: Add(Wiki): Wiki was not valid");
                return false;
            }

            // Attempt to upload
            if (!await WikisDataSource.Add(wiki))
            {
                Debug.WriteLine("DatabaseService: Add(Wiki): Wiki was unsuccessful");
                return false;
            }

            Debug.WriteLine("DatabaseService: Add(Wiki): Wiki was successfully added");
            Wikis.Add(wiki);
            return true;
        }

        /// <summary>
        /// Availables the specified wiki.
        /// </summary>
        /// <param name="wiki">The wiki.</param>
        /// <returns></returns>
        public async Task<bool> Available(Wiki wiki)
        {
            await UpdateWikis();
            return Wikis.All(g => g.Uid != wiki.Uid);
        }

        /// <summary>
        /// Adds the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        public async Task<bool> Add(Report report)
        {
            if (report == null)
            {
                Debug.WriteLine("DatabaseService: Add(Report): Report was null");
                return false;
            }

            if (!report.Valid)
            {
                Debug.WriteLine("DatabaseService: Add(Report): Report was not valid");
                return false;
            }

            // Attempt to upload
            if (!await ReportsDataSource.Add(report))
            {
                Debug.WriteLine("DatabaseService: Add(Report): Report was unsuccessful");
                return false;
            }

            Debug.WriteLine("DatabaseService: Add(Report): Report was successfully added");
            return true;
        }

        #endregion
    }
}