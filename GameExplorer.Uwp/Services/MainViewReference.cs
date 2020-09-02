using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GameExplorer.Model;
using GameExplorer.Uwp.Views;

namespace GameExplorer.Uwp.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class MainViewReference
    {
        /// <summary>
        /// Gets the main view.
        /// </summary>
        /// <value>
        /// The main view.
        /// </value>
        public static MainPage MainView => Window.Current?.Content as MainPage;
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public static DatabaseService Database => MainView?.Database;
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public static User CurrentUser => Database?.CurrentUser;
        /// <summary>
        /// Gets the current game.
        /// </summary>
        /// <value>
        /// The current game.
        /// </value>
        public static Game CurrentGame => Database?.CurrentGame;
    }
}
