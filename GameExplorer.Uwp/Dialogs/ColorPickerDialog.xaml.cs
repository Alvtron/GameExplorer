using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public sealed partial class ColorPickerDialog : ContentDialog, INotifyPropertyChanged
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
        private void OnPropertyChanged(string propertyName)
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
        private bool SetField<T>(ref T field, T value,
        [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ColorPickerDialog"/> is alpha.
        /// </summary>
        /// <value>
        ///   <c>true</c> if alpha; otherwise, <c>false</c>.
        /// </value>
        public bool Alpha { get; }
        /// <summary>
        /// The color
        /// </summary>
        private Color _color;
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get => _color;
            set
            {
                if (SetField(ref _color, value))
                {
                    OnPropertyChanged(nameof(_color));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerDialog"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="alpha">if set to <c>true</c> [alpha].</param>
        public ColorPickerDialog(Model.Color color, bool alpha = false)
        {
            InitializeComponent();

            Color = color == null ? ((SolidColorBrush)Application.Current.Resources["GameExplorerColor"]).Color : Color.FromArgb(color.A, color.R, color.G, color.B);

            Alpha = alpha;
        }

        /// <summary>
        /// Contents the dialog primary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Contents the dialog secondary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
