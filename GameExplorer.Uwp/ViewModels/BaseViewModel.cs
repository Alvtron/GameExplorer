using GameExplorer.Model;
using GameExplorer.Uwp.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Views;

namespace GameExplorer.Uwp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value,
        [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool _lock;
        public bool Lock
        {
            get => _lock;
            set
            {
                if (SetField(ref _lock, value))
                {
                    OnPropertyChanged(nameof(_lock));
                    OnPropertyChanged(nameof(Enabled));
                }
            }
        }
        public bool Enabled => !Lock;

        private RelayCommand<object> _goToCommand;
        public ICommand GoToCommand => _goToCommand = _goToCommand ?? new RelayCommand<object>(param => GoTo(param));
        private static void GoTo(object target)
        {
            switch (target)
            {
                case Game game:
                    NavigationService.NavigateTo(typeof(GamePage), game);
                    return;
                case Quest quest:
                    NavigationService.NavigateTo(typeof(QuestPage), quest);
                    return;
                case Wiki wiki:
                    NavigationService.NavigateTo(typeof(WikiPage), wiki);
                    return;
                case User user:
                    NavigationService.NavigateTo(typeof(UserPage), user);
                    return;
            }
        }
    }
}
