using GameExplorer.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GameExplorer.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <inheritdoc />
    /// <seealso cref="T:System.Data.Entity.DbContext" />
    public class DataContext : DbContext
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }
        /// <summary>
        /// Gets or sets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public virtual DbSet<Game> Games { get; set; }
        /// <summary>
        /// Gets or sets the quests.
        /// </summary>
        /// <value>
        /// The quests.
        /// </value>
        public virtual DbSet<Quest> Quests { get; set; }
        /// <summary>
        /// Gets or sets the questlines.
        /// </summary>
        /// <value>
        /// The questlines.
        /// </value>
        public virtual DbSet<Questline> Questlines { get; set; }
        /// <summary>
        /// Gets or sets the wikis.
        /// </summary>
        /// <value>
        /// The wikis.
        /// </value>
        public virtual DbSet<Wiki> Wikis { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public virtual DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public virtual DbSet<Image> Images { get; set; }
        /// <summary>
        /// Gets or sets the videos.
        /// </summary>
        /// <value>
        /// The videos.
        /// </value>
        public virtual DbSet<Video> Videos { get; set; }
        /// <summary>
        /// Gets or sets the rewards.
        /// </summary>
        /// <value>
        /// The rewards.
        /// </value>
        public virtual DbSet<Reward> Rewards { get; set; }
        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        public virtual DbSet<Step> Steps { get; set; }
        /// <summary>
        /// Gets or sets the maps.
        /// </summary>
        /// <value>
        /// The maps.
        /// </value>
        public virtual DbSet<Map> Maps { get; set; }
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public virtual DbSet<Coordinate> Coordinates { get; set; }
        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public virtual DbSet<Log> Logs { get; set; }
        /// <summary>
        /// Gets or sets the reports.
        /// </summary>
        /// <value>
        /// The reports.
        /// </value>
        public virtual DbSet<Report> Reports { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:GameExplorer.DataAccess.DataContext" /> class.
        /// </summary>
        /// <inheritdoc />
        public DataContext() : base("name=DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DataInitializer());
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Handle cycles by not traversing relations
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // User relations
            modelBuilder.Entity<User>()
                .Map(mc =>
                {
                    mc.ToTable("Users");
                });
            modelBuilder.Entity<User>()
                .HasKey(u => u.Uid);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Games);
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Photo);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Logs)
                .WithRequired(l => l.User);
            modelBuilder.Entity<User>()
                .HasMany(p => p.Friends)
                .WithMany()
                .Map(mc =>
                {
                    mc.ToTable("Friendships").MapLeftKey("Friend_A").MapRightKey("Friend_B");
                });

            // User color value object
            modelBuilder.ComplexType<Color>()
                .Property(e => e.R).HasColumnName("Color_R");
            modelBuilder.ComplexType<Color>()
                .Property(e => e.G).HasColumnName("Color_G");
            modelBuilder.ComplexType<Color>()
                .Property(e => e.B).HasColumnName("Color_B");
            modelBuilder.ComplexType<Color>()
                .Property(e => e.A).HasColumnName("Color_A");

            // User password value object
            modelBuilder.ComplexType<Password>()
                .Property(e => e.Iterations).HasColumnName("Iterations");
            modelBuilder.ComplexType<Password>()
                .Property(e => e.Salt).HasColumnName("Salt");
            modelBuilder.ComplexType<Password>()
                .Property(e => e.Hash).HasColumnName("Hash");

            // Game relations
            modelBuilder.Entity<Game>()
                .Map(mc =>
                {
                    mc.ToTable("Games");
                });
            modelBuilder.Entity<Game>()
                .HasKey(g => g.Uid);
            modelBuilder.Entity<Game>()
                .HasOptional(u => u.Photo);
            modelBuilder.Entity<Game>()
                .HasOptional(u => u.Banner);
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Logs);
            modelBuilder.Entity<Game>()
                .HasMany(a => a.Screenshots);
            modelBuilder.Entity<Game>()
                .HasMany(a => a.Videos);
            modelBuilder.Entity<Game>()
                .HasMany(a => a.Comments);
            modelBuilder.Entity<Game>()
                .HasMany(a => a.Quests);
            modelBuilder.Entity<Game>()
                .HasMany(a => a.Wikis);

            // Quest relations
            modelBuilder.Entity<Quest>()
                .Map(mc =>
                {
                    mc.ToTable("Quests");
                });
            modelBuilder.Entity<Quest>()
                .HasKey(q => q.Uid);
            modelBuilder.Entity<Quest>()
                .HasOptional(q => q.Game);
            modelBuilder.Entity<Quest>()
                .HasOptional(q => q.Map);
            modelBuilder.Entity<Quest>()
                .HasOptional(u => u.Photo);
            modelBuilder.Entity<Quest>()
                .HasOptional(u => u.Banner);
            modelBuilder.Entity<Quest>()
                .HasOptional(q => q.Questline);
            modelBuilder.Entity<Quest>()
                .HasMany(q => q.Logs);
            modelBuilder.Entity<Quest>()
                .HasMany(a => a.Comments);
            modelBuilder.Entity<Quest>()
                .HasMany(a => a.Screenshots);
            modelBuilder.Entity<Quest>()
                .HasMany(a => a.Videos);
            modelBuilder.Entity<Quest>()
                .HasMany(q => q.Rewards);
            modelBuilder.Entity<Quest>()
                .HasMany(q => q.Steps);

            // Questline relations
            modelBuilder.Entity<Questline>()
                .Map(mc =>
                {
                    mc.ToTable("Questlines");
                });
            modelBuilder.Entity<Questline>()
                .HasKey(q => q.Uid);

            // Wiki relations
            modelBuilder.Entity<Wiki>()
                .Map(mc =>
                {
                    mc.ToTable("Wikis");
                });
            modelBuilder.Entity<Wiki>()
                .HasKey(w => w.Uid);
            modelBuilder.Entity<Wiki>()
                .HasOptional(w => w.Game);
            modelBuilder.Entity<Wiki>()
                .HasOptional(w => w.Photo);
            modelBuilder.Entity<Wiki>()
                .HasOptional(w => w.Banner);
            modelBuilder.Entity<Wiki>()
                .HasOptional(w => w.Map);
            modelBuilder.Entity<Wiki>()
                .HasMany(w => w.Logs);
            modelBuilder.Entity<Wiki>()
                .HasMany(a => a.Comments);
            modelBuilder.Entity<Wiki>()
                .HasMany(a => a.Screenshots);
            modelBuilder.Entity<Wiki>()
                .HasMany(a => a.Videos);

            // Log relations
            modelBuilder.Entity<Log>()
                .Map(mc =>
                {
                    mc.ToTable("Logs");
                });
            modelBuilder.Entity<Log>()
                .HasKey(l => l.Uid);

            // Image relations
            modelBuilder.Entity<Image>()
                .Map(mc =>
                {
                    mc.ToTable("Images");
                });
            modelBuilder.Entity<Image>()
                .HasKey(i => i.Uid);

            // Video relations
            modelBuilder.Entity<Video>()
                .Map(mc =>
                {
                    mc.ToTable("Videos");
                });
            modelBuilder.Entity<Video>()
                .HasKey(v => v.Uid);

            // Step relations
            modelBuilder.Entity<Step>()
                .Map(mc =>
                {
                    mc.ToTable("Steps");
                });
            modelBuilder.Entity<Step>()
                .HasKey(s => s.Uid);

            // Reward relations
            modelBuilder.Entity<Reward>()
                .Map(mc =>
                {
                    mc.ToTable("Rewards");
                });
            modelBuilder.Entity<Reward>()
                .HasKey(r => r.Uid);

            // Coordinate relations
            modelBuilder.Entity<Coordinate>()
                .Map(mc =>
                {
                    mc.ToTable("Coordinates");
                });
            modelBuilder.Entity<Coordinate>()
                .HasKey(c => c.Uid);

            // Rating relations
            modelBuilder.Entity<Rating>()
                .Map(mc =>
                {
                    mc.ToTable("Ratings");
                });
            modelBuilder.Entity<Rating>()
                .HasKey(r => r.Uid);

            // Comment relations
            modelBuilder.Entity<Comment>()
                .Map(mc =>
                {
                    mc.ToTable("Comments");
                });
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Uid);
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User);

            // Map relations
            modelBuilder.Entity<Map>()
                .Map(mc =>
                {
                    mc.ToTable("Maps");
                });
            modelBuilder.Entity<Map>()
                .HasKey(m => m.Uid);
            modelBuilder.Entity<Map>()
                .HasMany(m => m.Coordinates);

            // Report relations
            modelBuilder.Entity<Report>()
                .Map(mc =>
                {
                    mc.ToTable("Reports");
                });
            modelBuilder.Entity<Report>()
                .HasKey(r => r.Uid);
            modelBuilder.Entity<Report>()
                .HasRequired(r => r.User);
        }
    }
}
 