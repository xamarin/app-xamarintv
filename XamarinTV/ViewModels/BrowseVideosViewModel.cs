using System.Windows.Input;
using XamarinTV.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinTV.ViewModels
{
    public class BrowseVideosViewModel : BaseViewModel
    {
        int _selectedViewModelIndex;
        BaseViewModel _topViewModel;

        public BrowseVideosViewModel()
        {
            TopViewModel = new TopVideosViewModel();
        }

        public BaseViewModel TopViewModel
        {
            get => _topViewModel;
            set { SetProperty(ref _topViewModel, value); }
        }

        public int SelectedViewModelIndex
        {
            get { return _selectedViewModelIndex; }
            set { SetProperty(ref _selectedViewModelIndex, value); }
        }

        public ICommand SettingsCommand => new Command(OpenSettings);

        void OpenSettings()
        {
            MainViewModel.Instance.OpenSettingWindow();
        }
    }
}