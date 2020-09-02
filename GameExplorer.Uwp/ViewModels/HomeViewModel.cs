using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.BaseViewModel" />
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        /// The featured
        /// </summary>
        private ObservableCollection<SearchablePost> _featured = new ObservableCollection<SearchablePost>();
        /// <summary>
        /// Gets or sets the featured.
        /// </summary>
        /// <value>
        /// The featured.
        /// </value>
        public ObservableCollection<SearchablePost> Featured
        {
            get => _featured;
            set
            {
                if (SetField(ref _featured, value))
                {
                    OnPropertyChanged(nameof(_featured));
                }
            }
        }

        /// <summary>
        /// The new quests
        /// </summary>
        private ObservableCollection<SearchablePost> _newQuests = new ObservableCollection<SearchablePost>();
        /// <summary>
        /// Gets or sets the new quests.
        /// </summary>
        /// <value>
        /// The new quests.
        /// </value>
        public ObservableCollection<SearchablePost> NewQuests
        {
            get => _newQuests;
            set
            {
                if (SetField(ref _newQuests, value))
                {
                    OnPropertyChanged(nameof(_newQuests));
                }
            }
        }

        /// <summary>
        /// The new wikis
        /// </summary>
        private ObservableCollection<SearchablePost> _newWikis = new ObservableCollection<SearchablePost>();
        /// <summary>
        /// Gets or sets the new wikis.
        /// </summary>
        /// <value>
        /// The new wikis.
        /// </value>
        public ObservableCollection<SearchablePost> NewWikis
        {
            get => _newWikis;
            set
            {
                if (SetField(ref _newWikis, value))
                {
                    OnPropertyChanged(nameof(_newWikis));
                }
            }
        }

        /// <summary>
        /// The featured users
        /// </summary>
        private ObservableCollection<User> _featuredUsers = new ObservableCollection<User>();
        /// <summary>
        /// Gets or sets the featured users.
        /// </summary>
        /// <value>
        /// The featured users.
        /// </value>
        public ObservableCollection<User> FeaturedUsers
        {
            get => _featuredUsers;
            set
            {
                if (SetField(ref _featuredUsers, value))
                {
                    OnPropertyChanged(nameof(_featuredUsers));
                }
            }
        }

        /// <summary>
        /// The current user contributions
        /// </summary>
        private ObservableCollection<Log> _currentUserContributions = new ObservableCollection<Log>();
        /// <summary>
        /// Gets or sets the current user contributions.
        /// </summary>
        /// <value>
        /// The current user contributions.
        /// </value>
        public ObservableCollection<Log> CurrentUserContributions
        {
            get => _currentUserContributions;
            set
            {
                if (SetField(ref _currentUserContributions, value))
                {
                    OnPropertyChanged(nameof(_currentUserContributions));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        public HomeViewModel()
        {
            RefreshFeatured();
            RefreshNewQuests();
            RefreshNewWikis();
            RefreshFeaturedUsers();
            RefreshCurrentUserContributions();
        }

        /// <summary>
        /// Refreshes the featured.
        /// </summary>
        public void RefreshFeatured()
        {
            Featured.Clear();
            MainViewReference.Database.CombineAll()
                .Where(o => o is Quest || o is Wiki)
                .OrderBy(o => o.Views)
                .ToList().ForEach(Featured.Add);
        }

        /// <summary>
        /// Refreshes the new quests.
        /// </summary>
        public void RefreshNewQuests()
        {
            NewQuests.Clear();
            MainViewReference.Database.CombineAll()
                .Where(o => o is Quest)
                .OrderBy(o => o.Created)
                .ToList().ForEach(NewQuests.Add);
        }

        /// <summary>
        /// Refreshes the new wikis.
        /// </summary>
        public void RefreshNewWikis()
        {
            NewWikis.Clear();
            MainViewReference.Database.CombineAll()
                .Where(o => o is Wiki)
                .OrderBy(o => o.Created)
                .ToList().ForEach(NewWikis.Add);
        }

        /// <summary>
        /// Refreshes the featured users.
        /// </summary>
        public void RefreshFeaturedUsers()
        {
            FeaturedUsers.Clear();
            var featuredUsers = MainViewReference.Database.Users
                .OrderBy(o => o.Experience)
                .ToList();

            for (var i = 0; i < featuredUsers.Count && i < 9; i++)
            {
                FeaturedUsers.Add(featuredUsers.ElementAt(i));
            }
        }

        /// <summary>
        /// Refreshes the current user contributions.
        /// </summary>
        public void RefreshCurrentUserContributions()
        {
            if (MainViewReference.CurrentUser == null) return;

            CurrentUserContributions.Clear();

            MainViewReference.CurrentUser.Logs
                .OrderBy(o => o.Date)
                .ToList().ForEach(CurrentUserContributions.Add);
        }

        /// <summary>
        /// The add quest command
        /// </summary>
        private RelayCommand _addQuestCommand;
        /// <summary>
        /// Gets the add quest command.
        /// </summary>
        /// <value>
        /// The add quest command.
        /// </value>
        public ICommand AddQuestCommand => _addQuestCommand = _addQuestCommand ?? new RelayCommand(param => AddQuest());
        /// <summary>
        /// Adds the quest.
        /// </summary>
        public void AddQuest()
        {
            NavigationService.NavigateTo(typeof(AddQuestPage));
        }

        /// <summary>
        /// The add wiki command
        /// </summary>
        private RelayCommand _addWikiCommand;
        /// <summary>
        /// Gets the add wiki command.
        /// </summary>
        /// <value>
        /// The add wiki command.
        /// </value>
        public ICommand AddWikiCommand => _addWikiCommand = _addWikiCommand ?? new RelayCommand(param => AddWiki());
        /// <summary>
        /// Adds the wiki.
        /// </summary>
        public void AddWiki()
        {
            NavigationService.NavigateTo(typeof(AddWikiPage));
        }
    }
}
