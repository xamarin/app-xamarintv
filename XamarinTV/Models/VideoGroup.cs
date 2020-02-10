using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinTV.Models
{
    public class VideoGroup : ObservableCollection<Video>
    {
        public VideoGroup(IEnumerable<Video> items) : base(items)
        {
        }

        public string Title { get; set; }
    }
}