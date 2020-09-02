using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.BaseViewModel" />
    public abstract class EditorViewModel : BaseViewModel
    {
        /// <summary>
        /// The changed
        /// </summary>
        private bool _changed;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EditorViewModel"/> is changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if changed; otherwise, <c>false</c>.
        /// </value>
        public bool Changed
        {
            get => _changed;
            set
            {
                if (SetField(ref _changed, value))
                {
                    OnPropertyChanged(nameof(_changed));
                }
            }
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public abstract Task Create();
        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> SaveChangesAsync();
        /// <summary>
        /// Saves the changes and navigate back.
        /// </summary>
        /// <returns></returns>
        public abstract Task SaveChangesAndNavigateBack();
        /// <summary>
        /// Uploads the photo asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task UploadPhotoAsync();
        /// <summary>
        /// Edits this instance.
        /// </summary>
        public abstract void Edit();
        /// <summary>
        /// Resets this instance.
        /// </summary>
        public abstract void Reset();
        /// <summary>
        /// Reports the asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task ReportAsync();

        /// <summary>
        /// The upload command
        /// </summary>
        private RelayCommand<bool> _uploadCommand;
        /// <summary>
        /// Gets the upload command.
        /// </summary>
        /// <value>
        /// The upload command.
        /// </value>
        public ICommand UploadCommand => _uploadCommand = _uploadCommand ?? new RelayCommand<bool>(async (param) => await SaveChangesAsync());

        /// <summary>
        /// The upload and navigate command
        /// </summary>
        private RelayCommand _uploadAndNavigateCommand;
        /// <summary>
        /// Gets the upload and navigate command.
        /// </summary>
        /// <value>
        /// The upload and navigate command.
        /// </value>
        public ICommand UploadAndNavigateCommand => _uploadAndNavigateCommand = _uploadAndNavigateCommand ?? new RelayCommand(async (param) => await SaveChangesAndNavigateBack());

        /// <summary>
        /// The upload photo command
        /// </summary>
        private RelayCommand _uploadPhotoCommand;
        /// <summary>
        /// Gets the upload photo command.
        /// </summary>
        /// <value>
        /// The upload photo command.
        /// </value>
        public ICommand UploadPhotoCommand => _uploadPhotoCommand = _uploadPhotoCommand ?? new RelayCommand(async param => await UploadPhotoAsync());

        /// <summary>
        /// The reset command
        /// </summary>
        private RelayCommand _resetCommand;
        /// <summary>
        /// Gets the reset command.
        /// </summary>
        /// <value>
        /// The reset command.
        /// </value>
        public ICommand ResetCommand => _resetCommand = _resetCommand ?? new RelayCommand(param => Reset());

        /// <summary>
        /// The edit command
        /// </summary>
        private RelayCommand _editCommand;
        /// <summary>
        /// Gets the edit command.
        /// </summary>
        /// <value>
        /// The edit command.
        /// </value>
        public ICommand EditCommand => _editCommand = _editCommand ?? new RelayCommand((param) => Edit());

        /// <summary>
        /// The report command
        /// </summary>
        private RelayCommand _reportCommand;
        /// <summary>
        /// Gets the report command.
        /// </summary>
        /// <value>
        /// The report command.
        /// </value>
        public ICommand ReportCommand => _reportCommand = _reportCommand ?? new RelayCommand(async param => await ReportAsync());

        /// <summary>
        /// The bold command
        /// </summary>
        private RelayCommand<RichEditBoxExtended> _boldCommand;
        /// <summary>
        /// Gets the bold command.
        /// </summary>
        /// <value>
        /// The bold command.
        /// </value>
        public ICommand BoldCommand => _boldCommand = _boldCommand ?? new RelayCommand<RichEditBoxExtended>((editor) => Bold(ref editor));

        /// <summary>
        /// The italic command
        /// </summary>
        private RelayCommand<RichEditBoxExtended> _italicCommand;
        /// <summary>
        /// Gets the italic command.
        /// </summary>
        /// <value>
        /// The italic command.
        /// </value>
        public ICommand ItalicCommand => _italicCommand = _italicCommand ?? new RelayCommand<RichEditBoxExtended>((editor) => Italic(ref editor));

        /// <summary>
        /// The underline command
        /// </summary>
        private RelayCommand<RichEditBoxExtended> _underlineCommand;
        /// <summary>
        /// Gets the underline command.
        /// </summary>
        /// <value>
        /// The underline command.
        /// </value>
        public ICommand UnderlineCommand => _underlineCommand = _underlineCommand ?? new RelayCommand<RichEditBoxExtended>((editor) => Underline(ref editor));

        /// <summary>
        /// Bolds the specified editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public void Bold(ref RichEditBoxExtended editor)
        {
            if (editor != null)
            {
                RichEditBoxExtended.Bold(editor);
            }
        }

        /// <summary>
        /// Italics the specified editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public void Italic(ref RichEditBoxExtended editor)
        {
            if (editor != null)
            {

                RichEditBoxExtended.Italic(editor);
            }
        }

        /// <summary>
        /// Underlines the specified editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public void Underline(ref RichEditBoxExtended editor)
        {
            if (editor != null)
            {
                RichEditBoxExtended.Underline(editor);
            }
        }
    }
}
