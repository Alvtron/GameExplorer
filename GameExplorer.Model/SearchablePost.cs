using System;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class SearchablePost : DatabaseItem
    {
        /// <summary>
        /// The title
        /// </summary>
        private string _title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get => _title;
            set
            {
                if (SetField(ref _title, value))
                {
                    OnPropertyChanged(nameof(_title));
                }
            }
        }

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
        /// The views
        /// </summary>
        private int _views;
        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        public int Views
        {
            get => _views;
            set
            {
                if (SetField(ref _views, value))
                {
                    OnPropertyChanged(nameof(_views));
                }
            }
        }

        /// <summary>
        /// The created
        /// </summary>
        private DateTime? _created = DateTime.Now;
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime? Created
        {
            get => _created;
            set
            {
                if (SetField(ref _created, value))
                {
                    OnPropertyChanged(nameof(_created));
                }
            }
        }

        /// <summary>
        /// The updated
        /// </summary>
        private DateTime? _updated = DateTime.Now;
        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        public DateTime? Updated
        {
            get => _updated;
            set
            {
                if (SetField(ref _updated, value))
                {
                    OnPropertyChanged(nameof(_updated));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchablePost"/> class.
        /// </summary>
        public SearchablePost()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <value>
        /// The name of the object.
        /// </value>
        [IgnoreDataMember]
        public string ObjectName => GetType().Name;
    }
}
