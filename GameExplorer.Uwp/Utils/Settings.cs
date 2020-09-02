using GameExplorer.Model;

namespace GameExplorer.Uwp.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// The local settings
        /// </summary>
        private static readonly Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        /* DEFAULT  USER */

        /// <summary>
        /// Gets the default name of the user.
        /// </summary>
        /// <value>
        /// The default name of the user.
        /// </value>
        public static string DefaultUserName => (string)LocalSettings.Values["defaultUserName"];

        /// <summary>
        /// Gets a value indicating whether [default user exist].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [default user exist]; otherwise, <c>false</c>.
        /// </value>
        public static bool DefaultUserExist => !string.IsNullOrWhiteSpace(DefaultUserName);

        /// <summary>
        /// Sets the default user.
        /// </summary>
        /// <param name="newDefaultUser">The new default user.</param>
        public static void SetDefaultUser(User newDefaultUser)
        {
            SetDefaultUser(newDefaultUser.Username);
        }

        /// <summary>
        /// Sets the default user.
        /// </summary>
        /// <param name="username">The username.</param>
        public static void SetDefaultUser(string username)
        {
            LocalSettings.Values["defaultUserName"] = username;
        }

        /// <summary>
        /// Removes the default user.
        /// </summary>
        public static void RemoveDefaultUser()
        {
            LocalSettings.Values["defaultUserName"] = null;
        }


        /* DEFAULT  GAME */

        /// <summary>
        /// Gets the default game uid.
        /// </summary>
        /// <value>
        /// The default game uid.
        /// </value>
        public static string DefaultGameUid => (string)LocalSettings.Values["defaultGameUID"];
        /// <summary>
        /// Gets the default game title.
        /// </summary>
        /// <value>
        /// The default game title.
        /// </value>
        public static string DefaultGameTitle => (string)LocalSettings.Values["defaultGameTitle"];

        /// <summary>
        /// Gets a value indicating whether [default game exist].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [default game exist]; otherwise, <c>false</c>.
        /// </value>
        public static bool DefaultGameExist => !string.IsNullOrWhiteSpace(DefaultGameUid) && !string.IsNullOrWhiteSpace(DefaultGameTitle);

        /// <summary>
        /// Sets the default game.
        /// </summary>
        /// <param name="newDefaultGame">The new default game.</param>
        public static void SetDefaultGame(Game newDefaultGame)
        {
            SetDefaultGame(newDefaultGame.Uid.ToString(), newDefaultGame.Title);
        }

        /// <summary>
        /// Sets the default game.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <param name="title">The title.</param>
        public static void SetDefaultGame(string uid, string title)
        {
            LocalSettings.Values["defaultGameUID"] = uid;
            LocalSettings.Values["defaultGameTitle"] = title;
        }

        /// <summary>
        /// Removes the default game.
        /// </summary>
        public static void RemoveDefaultGame()
        {
            LocalSettings.Values["defaultGame"] = null;
            LocalSettings.Values["defaultGameTitle"] = null;
        }
    }
}
