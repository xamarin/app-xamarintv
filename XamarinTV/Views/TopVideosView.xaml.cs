using System.Linq;
using Xamarin.Forms;
using XamarinTV.Models;

namespace XamarinTV.Views
{
    public partial class TopVideosView : ContentView
    {
        public TopVideosView()
        {
            InitializeComponent();
        }

        void OnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var carousel = (CarouselView)sender;
            if (!carousel.IsPlatformEnabled)
                return;

            var carouselItems = carousel.ItemsSource.Cast<object>().ToList();
            var firstIndex = e.FirstVisibleItemIndex;
            var currentIndex = e.CenterItemIndex;
            var lastIndex = e.LastVisibleItemIndex;

            if (firstIndex != currentIndex)
            {
                var firstItem = carouselItems[firstIndex] as VideoCarouselItem;
                firstItem.Scale = 0.8;
            }

            var currentItem = carouselItems[currentIndex] as VideoCarouselItem;
            currentItem.Scale = 1;

            var lastItem = carouselItems[lastIndex] as VideoCarouselItem;
            lastItem.Scale = 0.8;
        }
    }
}
