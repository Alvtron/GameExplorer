using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameExplorer.Uwp
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.RichEditBox" />
    public class RichEditBoxExtended : RichEditBox
    {
        /// <summary>
        /// The RTF text property
        /// </summary>
        public static readonly DependencyProperty RtfTextProperty = DependencyProperty.Register("RtfText", typeof(string), typeof(RichEditBoxExtended), new PropertyMetadata(default(string), RtfTextPropertyChanged));
        /// <summary>
        /// The editable property
        /// </summary>
        public static readonly DependencyProperty EditableProperty = DependencyProperty.Register("Editable", typeof(bool), typeof(RichEditBoxExtended), new PropertyMetadata(default(bool), EditablePropertyChanged));


        /// <summary>
        /// The lock change execution
        /// </summary>
        private bool _lockChangeExecution;

        /// <summary>
        /// Initializes a new instance of the <see cref="RichEditBoxExtended"/> class.
        /// </summary>
        public RichEditBoxExtended()
        {
            TextChanged += RichEditBoxExtended_TextChanged;
        }

        /// <summary>
        /// Gets or sets the RTF text.
        /// </summary>
        /// <value>
        /// The RTF text.
        /// </value>
        public string RtfText
        {
            get => (string)GetValue(RtfTextProperty);
            set => SetValue(RtfTextProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RichEditBoxExtended"/> is editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if editable; otherwise, <c>false</c>.
        /// </value>
        public bool Editable
        {
            get => (bool)GetValue(EditableProperty);
            set => SetValue(EditableProperty, value);
        }

        /// <summary>
        /// Handles the TextChanged event of the RichEditBoxExtended control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RichEditBoxExtended_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!_lockChangeExecution)
            {
                _lockChangeExecution = true;
                Document.GetText(TextGetOptions.None, out var text);
                if (string.IsNullOrWhiteSpace(text))
                {
                    RtfText = "";
                }
                else
                {
                    Document.GetText(TextGetOptions.FormatRtf, out text);
                    RtfText = text;
                }
                _lockChangeExecution = false;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            IsReadOnly = false;
            Document.SetText(TextSetOptions.None, "");
            IsReadOnly = true;
        }

        /// <summary>
        /// To the RTF.
        /// </summary>
        /// <returns></returns>
        public string ToRtf()
        {
            var value = string.Empty;
            Document.GetText(TextGetOptions.FormatRtf, out value);

            return value;
        }

        /// <summary>
        /// RTFs the text property changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void RtfTextPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is RichEditBoxExtended rtb)) return;
            if (!rtb._lockChangeExecution)
            {
                rtb.IsReadOnly = false;
                rtb.Document.SetText(TextSetOptions.FormatRtf, rtb.RtfText);
                rtb.IsReadOnly = !rtb.Editable;
            }
        }

        /// <summary>
        /// Editables the property changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void EditablePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is RichEditBoxExtended rtb)) return;
            if (!rtb._lockChangeExecution)
            {
                rtb.IsReadOnly = !rtb.Editable;
            }
        }

        /// <summary>
        /// Bolds the specified editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public static void Bold(RichEditBoxExtended editor)
        {
            editor.IsReadOnly = false;
            var selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                var charFormatting = selectedText.CharacterFormat;
                charFormatting.Bold = FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
            editor.IsReadOnly = !editor.Editable;
        }

        /// <summary>
        /// Italics the specified editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public static void Italic(RichEditBoxExtended editor)
        {
            editor.IsReadOnly = false;
            var selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                var charFormatting = selectedText.CharacterFormat;
                charFormatting.Italic = FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
            editor.IsReadOnly = !editor.Editable;
        }

        /// <summary>
        /// Underlines the specified editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        public static void Underline(RichEditBoxExtended editor)
        {
            editor.IsReadOnly = false;
            var selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                var charFormatting = selectedText.CharacterFormat;
                if (charFormatting.Underline == UnderlineType.None)
                {
                    charFormatting.Underline = UnderlineType.Single;
                }
                else
                {
                    charFormatting.Underline = UnderlineType.None;
                }
                selectedText.CharacterFormat = charFormatting;
            }
            editor.IsReadOnly = !editor.Editable;
        }
    }
}
