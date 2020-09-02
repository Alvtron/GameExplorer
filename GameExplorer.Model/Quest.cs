using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.Post" />
    public class Quest : Post
    {
        /// <summary>
        /// Gets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        public string ModelType => GetType().Name;

        /// <summary>
        /// The quest identifier
        /// </summary>
        private string _questId;
        /// <summary>
        /// Gets or sets the quest identifier.
        /// </summary>
        /// <value>
        /// The quest identifier.
        /// </value>
        public string QuestId
        {
            get => _questId;
            set
            {
                if (SetField(ref _questId, value))
                {
                    OnPropertyChanged(nameof(_questId));
                }
            }
        }

        /// <summary>
        /// The game
        /// </summary>
        private Game _game;
        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public Game Game
        {
            get => _game;
            set
            {
                if (SetField(ref _game, value))
                {
                    OnPropertyChanged(nameof(_game));
                }
            }
        }

        /// <summary>
        /// The questline
        /// </summary>
        private Questline _questline;
        /// <summary>
        /// Gets or sets the questline.
        /// </summary>
        /// <value>
        /// The questline.
        /// </value>
        public Questline Questline
        {
            get => _questline;
            set
            {
                if (SetField(ref _questline, value))
                {
                    OnPropertyChanged(nameof(_questline));
                }
            }
        }

        /// <summary>
        /// The map
        /// </summary>
        private Map _map = new Map();
        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <value>
        /// The map.
        /// </value>
        public Map Map
        {
            get => _map;
            set
            {
                if (SetField(ref _map, value))
                {
                    OnPropertyChanged(nameof(_map));
                }
            }
        }

        /// <summary>
        /// The rewards
        /// </summary>
        private ObservableCollection<Reward> _rewards = new ObservableCollection<Reward>();
        /// <summary>
        /// Gets or sets the rewards.
        /// </summary>
        /// <value>
        /// The rewards.
        /// </value>
        public ObservableCollection<Reward> Rewards
        {
            get => _rewards;
            set
            {
                if (SetField(ref _rewards, value))
                {
                    OnPropertyChanged(nameof(_rewards));
                }
            }
        }

        /// <summary>
        /// The steps
        /// </summary>
        private ObservableCollection<Step> _steps = new ObservableCollection<Step>();
        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        public ObservableCollection<Step> Steps
        {
            get => _steps;
            set
            {
                if (SetField(ref _steps, value))
                {
                    OnPropertyChanged(nameof(_steps));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quest"/> class.
        /// </summary>
        public Quest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quest"/> class.
        /// </summary>
        /// <param name="creator">The creator.</param>
        public Quest(User creator)
        {
            Logs.Add(new Log(creator, "created this"));
        }

        /// <summary>
        /// Gets the rewards string.
        /// </summary>
        /// <value>
        /// The rewards string.
        /// </value>
        [IgnoreDataMember]
        public string RewardsString => _RewardsString();

        /// <summary>
        /// Gets a value indicating whether this <see cref="Quest"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Valid => !string.IsNullOrWhiteSpace(Title);

        /// <summary>
        /// Rewardses the string.
        /// </summary>
        /// <returns></returns>
        private string _RewardsString()
        {
            var rewardString = "";
            foreach (var reward in Rewards)
                rewardString += reward + "\n";
            return rewardString;
        }

        /// <summary>
        /// Adds the step.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="step">The step.</param>
        /// <returns></returns>
        public bool AddStep(User user, Step step)
        {
            if (user == null || step == null)
            {
                return false;
            }

            Steps.Add(step);
            Logs.Add(new Log(user, "added a new step"));
            return true;
        }

        /// <summary>
        /// Adds the reward.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="reward">The reward.</param>
        /// <returns></returns>
        public bool AddReward(User user, Reward reward)
        {
            if (user == null || reward == null || !reward.Valid)
            {
                return false;
            }

            Rewards.Add(reward);
            Logs.Add(new Log(user, "added a new reward"));
            return true;
        }

        /// <summary>
        /// Sets the questline.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="questline">The questline.</param>
        /// <returns></returns>
        public bool SetQuestline(User user, Questline questline)
        {
            if (user == null || questline == null || !questline.Valid)
            {
                return false;
            }

            Questline = questline;
            Logs.Add(new Log(user, "set new questline"));
            return true;
        }
    }
}
