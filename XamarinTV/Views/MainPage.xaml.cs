using XamarinTV.ViewModels;
using XamarinTV.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;

namespace XamarinTV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = MainViewModel.Instance;
            twoPaneView.LayoutChanged += OnTwoPaneViewLayoutChanged;

        }

        void OnTwoPaneViewLayoutChanged(object sender, System.EventArgs e)
        {
           MainViewModel.Instance.UpdateLayouts();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            MainViewModel.Instance.UpdateLayouts();
            base.LayoutChildren(x, y, width, height);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((BaseViewModel)BindingContext).OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((BaseViewModel)BindingContext).OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return ((BaseViewModel)BindingContext).OnBackButtonPressed();
        }
    }
}