namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    public static class Experience
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Action
        {
            /// <summary>
            /// The add game
            /// </summary>
            AddGame = 1000,
            /// <summary>
            /// The add wiki
            /// </summary>
            AddWiki = 200,
            /// <summary>
            /// The add quest
            /// </summary>
            AddQuest = 200,
            /// <summary>
            /// The add comment
            /// </summary>
            AddComment = 20,
            /// <summary>
            /// The add profile photo
            /// </summary>
            AddProfilePhoto = 100,
            /// <summary>
            /// The edit game
            /// </summary>
            EditGame = 10,
            /// <summary>
            /// The edit wiki
            /// </summary>
            EditWiki = 10,
            /// <summary>
            /// The edit quest
            /// </summary>
            EditQuest = 10,
            /// <summary>
            /// The upload image
            /// </summary>
            UploadImage = 10,
            /// <summary>
            /// The upload video
            /// </summary>
            UploadVideo = 10
        };

        /// <summary>
        /// The level up exp
        /// </summary>
        public const int LevelUpExp = 5000;

        /// <summary>
        /// Exps to level.
        /// </summary>
        /// <param name="exp">The exp.</param>
        /// <returns></returns>
        public static int ExpToLevel(int exp)
        {
            return exp / LevelUpExp;
        }

        /// <summary>
        /// Levels to exp.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public static int LevelToExp(int level)
        {
            return level * LevelUpExp;
        }

        /// <summary>
        /// Progresses the exp.
        /// </summary>
        /// <param name="exp">The exp.</param>
        /// <returns></returns>
        public static int ProgressExp(int exp)
        {
            return exp % LevelUpExp;
        }

        /// <summary>
        /// Progresses the exp in percentage.
        /// </summary>
        /// <param name="exp">The exp.</param>
        /// <returns></returns>
        public static double ProgressExpInPercentage(int exp)
        {
            return (exp % LevelUpExp / (double)LevelUpExp) * 100.00;
        }
    }
}
