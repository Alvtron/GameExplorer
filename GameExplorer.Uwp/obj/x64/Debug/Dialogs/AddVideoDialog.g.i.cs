﻿#pragma checksum "C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Dialogs\AddVideoDialog.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6FBA4A526054E4C8383B03E1A7576B9F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameExplorer.Uwp.Dialogs
{
    partial class AddVideoDialog : global::Windows.UI.Xaml.Controls.ContentDialog
    {


        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.WebView VideoPlayer; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.TextBox URLInput; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.HyperlinkButton ViewVideoButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Dialogs/AddVideoDialog.xaml");
            global::Windows.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
        }

        partial void UnloadObject(global::Windows.UI.Xaml.DependencyObject unloadableObject);

        private interface IAddVideoDialog_Bindings
        {
            void Initialize();
            void Update();
            void StopTracking();
            void DisconnectUnloadedObject(int connectionId);
        }
#pragma warning disable 0169    //  Proactively suppress unused field warning in case Bindings is not used.
        private IAddVideoDialog_Bindings Bindings;
#pragma warning restore 0169
    }
}


