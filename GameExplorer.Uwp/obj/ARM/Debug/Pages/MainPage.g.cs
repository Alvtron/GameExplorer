﻿#pragma checksum "C:\Users\thoma\source\repos\QC\QuestCompanion\QC.UI\Pages\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "955E99EB9782A6CAABC6BEB7A63D1362"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QC.UI
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Pages\MainPage.xaml line 11
                {
                    this.NavView = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.NavView).ItemInvoked += this.NavView_ItemInvoked;
                }
                break;
            case 2: // Pages\MainPage.xaml line 38
                {
                    global::Windows.UI.Xaml.Controls.AutoSuggestBox element2 = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element2).Loaded += this.ASB_Loaded;
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element2).TextChanged += this.ASB_TextChanged;
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)element2).QuerySubmitted += this.ASB_QuerySubmitted;
                }
                break;
            case 3: // Pages\MainPage.xaml line 66
                {
                    global::Windows.UI.Xaml.Controls.PersonPicture element3 = (global::Windows.UI.Xaml.Controls.PersonPicture)(target);
                    ((global::Windows.UI.Xaml.Controls.PersonPicture)element3).Loaded += this.CurrentUser_Loaded;
                }
                break;
            case 4: // Pages\MainPage.xaml line 69
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element4 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element4).Click += this.SignUser_Click;
                }
                break;
            case 5: // Pages\MainPage.xaml line 70
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element5 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element5).Click += this.SignUser_Click;
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element5).Loaded += this.SignIn_Loaded;
                }
                break;
            case 6: // Pages\MainPage.xaml line 55
                {
                    global::Windows.UI.Xaml.Controls.TextBlock element6 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)element6).Loaded += this.CurrentGame_Loaded;
                }
                break;
            case 7: // Pages\MainPage.xaml line 83
                {
                    this.ContentFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

