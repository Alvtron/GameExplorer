using GameExplorer.Uwp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameExplorer.Model;

namespace GameExplorer.Uwp.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class NavigationService
    {
        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public static bool CanGoBack => MainViewReference.MainView != null && MainViewReference.MainView.NavigationFrame.CanGoBack;

        /// <summary>
        /// Gets a value indicating whether this instance can go forward.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public static bool CanGoForward => MainViewReference.MainView != null && MainViewReference.MainView.NavigationFrame.CanGoForward;

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="parameter">The parameter.</param>
        public static void NavigateTo(Type viewType, object parameter = null)
        {
            if (MainViewReference.MainView?.ViewModel == null) return;

            MainViewReference.MainView.ViewModel.HeaderTitle = "";
            MainViewReference.MainView.NavigationFrame.Navigate(viewType, parameter);
            MainViewReference.MainView.ViewModel.UpdateBackButtonVisibillity();
        }

        /// <summary>
        /// Navigates the back.
        /// </summary>
        public static void NavigateBack()
        {
            if (!CanGoBack) return;

            MainViewReference.MainView.ViewModel.HeaderTitle = "";
            MainViewReference.MainView.NavigationFrame.GoBack();
            MainViewReference.MainView.ViewModel.UpdateBackButtonVisibillity();
        }

        /// <summary>
        /// Navigates the forward.
        /// </summary>
        public static void NavigateForward()
        {
            if (!CanGoForward) return;

            MainViewReference.MainView.ViewModel.HeaderTitle = "";
            MainViewReference.MainView.NavigationFrame.GoForward();
            MainViewReference.MainView.ViewModel.UpdateBackButtonVisibillity();
        }

        /// <summary>
        /// Sets the header title.
        /// </summary>
        /// <param name="title">The title.</param>
        public static void SetHeaderTitle(string title)
        {
            if (MainViewReference.MainView?.ViewModel == null || title == null) return;

            MainViewReference.MainView.ViewModel.HeaderTitle = title;
        }

        /// <summary>
        /// Updates the current user.
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateCurrentUser()
        {
            if (MainViewReference.MainView?.ViewModel == null) return;
            await MainViewReference.MainView.ViewModel.UpdateCurrentUser();
        }

        /// <summary>
        /// Updates the back button.
        /// </summary>
        public static void UpdateBackButton()
        {
            MainViewReference.MainView?.ViewModel?.UpdateBackButtonVisibillity();
        }

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        public static void ClearBackStack()
        {
            if (MainViewReference.MainView?.ViewModel == null) return;
            MainViewReference.MainView.NavigationFrame.BackStack.Clear();
            MainViewReference.MainView.ViewModel.BackButtonVisible = false;
        }
    }
}
