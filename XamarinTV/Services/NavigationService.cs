using System;
using System.Collections.Generic;
using XamarinTV.ViewModels;
using XamarinTV.Views;
using Xamarin.Forms;

namespace XamarinTV.Services
{
    public class NavigationService
    {
        static readonly Lazy<NavigationService> _instance = new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Instance => _instance.Value;

        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }
        
        public View CreateAndBind(object viewModel)
        {
            var pageType = GetPageTypeForViewModel(viewModel.GetType());
            var page = Activator.CreateInstance(pageType) as View;
            page.BindingContext = viewModel;

            return page;
        }

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(TopVideosViewModel), typeof(TopVideosView));
            _mappings.Add(typeof(BrowseVideosViewModel), typeof(BrowseVideosView));
            _mappings.Add(typeof(SettingsViewModel), typeof(SettingsView));
            _mappings.Add(typeof(SearchVideosViewModel), typeof(SearchVideosView));
            _mappings.Add(typeof(VideoDetailViewModel), typeof(VideoDetailView));
            _mappings.Add(typeof(VideoPlayerViewModel), typeof(VideoPlayerView));
        }
    }
}