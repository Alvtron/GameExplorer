using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Image : DatabaseItem
    {
        /// <summary>
        /// The image in bytes
        /// </summary>
        private byte[] _imageInBytes;
        /// <summary>
        /// Gets or sets the image in bytes.
        /// </summary>
        /// <value>
        /// The image in bytes.
        /// </value>
        public byte[] ImageInBytes
        {
            get => _imageInBytes;
            set
            {
                if (SetField(ref _imageInBytes, value))
                {
                    OnPropertyChanged(nameof(_imageInBytes));
                }
            }
        }

        /// <summary>
        /// The width
        /// </summary>
        private int _width = -1;
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get => _width;
            set
            {
                if (SetField(ref _width, value))
                {
                    OnPropertyChanged(nameof(_width));
                }
            }
        }

        /// <summary>
        /// The height
        /// </summary>
        private int _height = -1;
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get => _height;
            set
            {
                if (SetField(ref _height, value))
                {
                    OnPropertyChanged(nameof(_height));
                }
            }
        }

        /// <summary>
        /// The type
        /// </summary>
        private string _type;
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
        {
            get => _type;
            set
            {
                if (SetField(ref _type, value))
                {
                    OnPropertyChanged(nameof(_type));
                }
            }
        }

        /// <summary>
        /// The description
        /// </summary>
        private string _description;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get => _description;
            set
            {
                if (SetField(ref _description, value))
                {
                    OnPropertyChanged(nameof(_description));
                }
            }
        }

        /// <summary>
        /// The uploaded
        /// </summary>
        private DateTime? _uploaded = DateTime.Now;
        /// <summary>
        /// Gets or sets the uploaded.
        /// </summary>
        /// <value>
        /// The uploaded.
        /// </value>
        public DateTime? Uploaded
        {
            get => _uploaded;
            set
            {
                if (SetField(ref _uploaded, value))
                {
                    OnPropertyChanged(nameof(_uploaded));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Image"/> is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if empty; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Empty => (ImageInBytes == null) || ImageInBytes.Length == 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            Uid = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="imageInBytes">The image in bytes.</param>
        /// <param name="description">The description.</param>
        public Image(byte[] imageInBytes, string description = null)
        {
            Uid = Guid.NewGuid();
            ImageInBytes = imageInBytes;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="imageInBytes">The image in bytes.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="description">The description.</param>
        public Image(byte[] imageInBytes, int width, int height, string description = null)
        {
            Uid = Guid.NewGuid();
            ImageInBytes = imageInBytes;
            Width = width;
            Height = height;
            Description = description;
        }

        /// <summary>
        /// Sets the image.
        /// </summary>
        /// <param name="imageInBytes">The image in bytes.</param>
        public void SetImage(byte[] imageInBytes)
        {
            ImageInBytes = imageInBytes;
            Uploaded = DateTime.Now;
        }
    }
}