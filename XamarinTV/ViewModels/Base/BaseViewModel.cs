using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms.StateSquid;

namespace XamarinTV.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool _isBusy;
        bool _hasAppearedFirst = false;
        bool _hasAppeared = false;
        State _currentState = State.None;

        public bool IsBusy
        {
            get { return _isBusy; }
            set {
                SetProperty(ref _isBusy, value);
                CurrentState = _isBusy ? State.Loading : State.None;
            }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                SetProperty(ref _currentState, value);
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnAppearing()
        {
            if (_hasAppeared)
                return;

            _hasAppeared = true;
            if (!_hasAppearedFirst)
            {
                _hasAppearedFirst = true;
                OnFirstAppearing();
            }
        }

        public virtual void OnDisappearing()
        {
            if (!_hasAppeared)
                return;

            _hasAppeared = false;
        }

        public virtual void OnFirstAppearing()
        {
        }
    }
}