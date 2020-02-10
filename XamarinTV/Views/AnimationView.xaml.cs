using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimationView : ContentView
    {
        const int AnimationDuration = 500;

        public AnimationView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ActiveViewProperty =
            BindableProperty.Create("ActiveView", typeof(View), typeof(AnimationView), propertyChanged: OnActiveViewPropertyChanged);

        public static readonly BindableProperty ViewToAnimateInProperty =
            BindableProperty.Create("ViewToAnimateIn", typeof(View), typeof(AnimationView), propertyChanged: OnViewToAnimateInPropertyChanged);

        static async void OnViewToAnimateInPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AnimationView animationView = bindable as AnimationView;
            View newView = (View)newValue;
            View oldView = (View)oldValue;

            if(oldView == null && newView != null)
            {
                animationView.GridView.Children.Add(newView);
                animationView.ActiveView = newView;
                return;
            }

            if (newView != null)
            {
                animationView.GridView.Children.Insert(0, newView);
            }

            if (oldView != null)
            {
                oldView.InputTransparent = true;
                await oldView.FadeTo(0, AnimationDuration, Easing.CubicIn);
            }

            if(newView != null)
                newView.InputTransparent = false;

            animationView.ActiveView = newView;

            if (oldView != null)
            {
                animationView.GridView.Children.Remove(oldView);
            }

        }

        static void OnActiveViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public View ActiveView
        {
            get { return (View)GetValue(ActiveViewProperty); }
            set { SetValue(ActiveViewProperty, value); }
        }

        public View ViewToAnimateIn
        {
            get { return (View)GetValue(ViewToAnimateInProperty); }
            set { SetValue(ViewToAnimateInProperty, value); }
        }
    }
}