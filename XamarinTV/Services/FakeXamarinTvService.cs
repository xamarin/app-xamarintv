using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinTV.Models;

namespace XamarinTV.Services
{
    public class FakeXamarinTvService
    {
        static readonly Lazy<FakeXamarinTvService> _instance = new Lazy<FakeXamarinTvService>(() => new FakeXamarinTvService());

        public static FakeXamarinTvService Instance => _instance.Value;

        public List<Video> Videos
        {
            get
            {
                return new List<Video>
                {
                    // XAMARIN COMMUNITY STANDUP
                    new Video { VideoId = 40, Title = "August 1st, 2019 - XAML Hot Reload with Alex Corrado", Image = "v40.png", ListName = "Xamarin Community Standup", Duration = 65, ViewCount = 1679, ReleaseDate = new DateTime(2019, 8, 1), Description = "Join the Mobile .NET Teams for our community standup covering great community contributions for Mobile .NET, Xamarin, Xamarin.Forms, Components, and more. <br><br>Suggest an idea for a standup: <a href='https://www.xamarin.com'>xamarin.com</a><br><br><b>Links:</b> <a href='https://www.xamarin.com'>xamarin.com</a> <br><br><b>Category:</b> Science &amp; Technology"  },
                    new Video { VideoId = 39, Title = "Sept. 5th, 2019 - Android Perf & Startup Challenge + App Bundles", Image = "v39.png", ListName = "Xamarin Community Standup", Duration = 61, ViewCount = 1412, ReleaseDate = new DateTime(2019, 9, 5), Description = "Join the Mobile .NET Teams for our community standup covering great community contributions for Mobile .NET, Xamarin, Xamarin.Forms, Components, and more. <br><br>Suggest an idea for a standup: <a href='https://www.xamarin.com'>xamarin.com</a><br><br><b>Links:</b> <a href='https://www.xamarin.com'>xamarin.com</a> <br><br><b>Category:</b> Science &amp; Technology"  },
                    new Video { VideoId = 38, Title = "Oct. 3, 2019 - CarouselView to the Max", Image = "v38.png", ListName = "Xamarin Community Standup", Duration = 65, ViewCount = 1943, ReleaseDate = new DateTime(2019, 9, 3)  , Description = "Join the Mobile .NET Teams for our community standup covering great community contributions for Mobile .NET, Xamarin, Xamarin.Forms, Components, and more. <br><br>Suggest an idea for a standup: <a href='https://www.xamarin.com'>xamarin.com</a><br><br><b>Links:</b> <a href='https://www.xamarin.com'>xamarin.com</a> <br><br><b>Category:</b> Science &amp; Technology"},
                    // THE XAMARIN SHOW
                    new Video { VideoId = 37, Title = "4 Awesome Things In Xamarin.Forms 4.0", ListName = "The Xamarin Show", Image = "v37.png", Duration = 23, ViewCount = 11966, ReleaseDate = new DateTime(2019, 7, 27)  },
                    new Video { VideoId = 36, Title = "Introduction to AndroidX", ListName = "The Xamarin Show", Image = "v36.png", Duration = 18, ViewCount = 6171, ReleaseDate = new DateTime(2019, 7, 25)  },
                    new Video { VideoId = 35, Title = "Editor Improvements in Visual Studio for Mac", Image = "v35.png",  ListName = "The Xamarin Show", Duration = 10, ViewCount = 3188, ReleaseDate = new DateTime(2019, 8, 15)  },
                    new Video { VideoId = 34, Title = "Android App Bundles 101", Image = "v34.png", ListName = "The Xamarin Show", Duration = 11, ViewCount = 3329, ReleaseDate = new DateTime(2019, 9, 19)  },
                    new Video { VideoId = 33, Title = "XAML Hot Reload for Xamarin.Forms In-Depth", Image = "v33.png", ListName = "The Xamarin Show", Duration = 18, ViewCount = 5936, ReleaseDate = new DateTime(2019, 9, 26)  },
                    new Video { VideoId = 32, Title = "Best Practices - Memory Management & Profiling", Image = "v32.png", ListName = "The Xamarin Show", Duration = 31, ViewCount = 2853, ReleaseDate = new DateTime(2020, 1, 2)  },
                    new Video { VideoId = 31, Title = "Best Practices - Advanced Async / Await", Image = "v31.png", ListName = "The Xamarin Show", Duration = 6, ViewCount = 2494, ReleaseDate = new DateTime(2020, 1, 16)  },
                    // XAMARIN 101
                    new Video { VideoId = 30, Title = "Xamarin.Forms 101: Control Templates", Image = "v30.png", ListName = "Xamarin 101", Duration = 5, ViewCount = 3589, ReleaseDate = new DateTime(2019, 12, 10)  },
                    new Video { VideoId = 29, Title = "Xamarin.Forms 101: Compiled Bindings", Image = "v29.png", ListName = "Xamarin 101", Duration = 5, ViewCount = 2679, ReleaseDate = new DateTime(2019, 11, 26)  },
                    new Video { VideoId = 28, Title = "Xamarin.Forms 101: Styles", Image = "v28.png", ListName = "Xamarin 101", Duration = 5, ViewCount = 3145, ReleaseDate = new DateTime(2019, 10, 29)  },
                    new Video { VideoId = 27, Title = "Xamarin.Forms 101: OnPlatform (Adjusting UI Based on Operating System)", Image = "v27.png", ListName = "Xamarin 101", Duration = 2, ViewCount = 3491, ReleaseDate = new DateTime(2019, 7, 23)  },
                    new Video { VideoId = 26, Title = "Xamarin.Forms 101: Command Parameters", Image = "v26.png", ListName = "Xamarin 101", Duration = 5, ViewCount = 5258, ReleaseDate = new DateTime(2019, 2, 14)  },
                    new Video { VideoId = 25, Title = "Xamarin.Forms 101: Commands", Image = "v25.png", ListName = "Xamarin 101", Duration = 8, ViewCount = 6816, ReleaseDate = new DateTime(2019, 1, 24)  },
                    new Video { VideoId = 24, Title = "Xamarin.Forms 101: Data Binding", Image = "v24.png", ListName = "Xamarin 101", Duration = 10, ViewCount = 14934, ReleaseDate = new DateTime(2019, 1, 10)  },
                    // XAMARIN DEVELOPER SUMMIT
                    new Video { VideoId = 23, Title = "Using Xamarin.Forms Shell to easily create a consistent, dynamic, customized, and feature filled UI", Image = "v23.png", ListName = "Xamarin Developer Summit", Duration = 47, ViewCount = 6106, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 22, Title = "App Center and the Future of Azure Mobile Apps", Image = "v22.png", ListName = "Xamarin Developer Summit", Duration = 46, ViewCount = 1932, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 21, Title = "Xamarin + GraphQL", ListName = "Xamarin Developer Summit", Image = "v21.png", Duration = 30, ViewCount = 1880, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 20, Title = "Let’s Make Crazy Beautiful UI With Xamarin.Forms", Image = "v20.png", ListName = "Xamarin Developer Summit", Duration = 56, ViewCount = 11031, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 19, Title = "Create Mixed Reality Experiences with Azure Spatial Anchors and Xamarin", Image = "v19.png", ListName = "Xamarin Developer Summit", Duration = 30, ViewCount = 504, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 18, Title = "Crafting Real-Time Mobile Apps with SignalR", Image = "v18.png", ListName = "Xamarin Developer Summit", Duration = 47, ViewCount = 4593, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 17, Title = "Streamline & Simplify Events with Reactive Extensions", Image = "v17.png",ListName = "Xamarin Developer Summit", Duration = 44, ViewCount = 950, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 16, Title = "Page Object Pattern and UITest Best Practices", Image = "v16.png", ListName = "Xamarin Developer Summit", Duration = 48, ViewCount = 1436, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 15, Title = "Speed Up Android Build Times & Shrink APK Sizes", Image = "v15.png", ListName = "Xamarin Developer Summit", Duration = 34, ViewCount = 2936, ReleaseDate = new DateTime(2019, 7, 20)  },
                    new Video { VideoId = 14, Title = "A Glimpse Into the Future of Xamarin", Image = "v14.png", ListName = "Xamarin Developer Summit", Duration = 61, ViewCount = 8959, ReleaseDate = new DateTime(2019, 7, 20)  },
                    // XAMARIN UNIVERSITY
                    new Video { VideoId = 13, Title = "Customizing Xamarin.Forms UI", Image = "v13.png", ListName = "Xamarin University", Duration = 63, ViewCount = 22014, ReleaseDate = new DateTime(2017, 6, 22)  },
                    new Video { VideoId = 12, Title = "SkiaSharp Graphics for Xamarin.Forms", Image = "v12.png", ListName = "Xamarin University", Duration = 41, ViewCount = 23392, ReleaseDate = new DateTime(2017, 6, 15)  },
                    new Video { VideoId = 11, Title = "Connected Mobile Apps with Microsoft Azure", Image = "v11.png", ListName = "Xamarin University", Duration = 65, ViewCount = 18954, ReleaseDate = new DateTime(2017, 3, 30)  },
                    new Video { VideoId = 10, Title = "Building Your First iOS App with Xamarin for Visual Studio", Image = "v10.png", ListName = "Xamarin University", Duration = 49, ViewCount = 64030, ReleaseDate = new DateTime(2017, 3, 23)  },
                    new Video { VideoId = 9, Title = "Building Your First Android App with Xamarin for Visual Studio", Image = "v9.png", ListName = "Xamarin University", Duration = 44, ViewCount = 81015, ReleaseDate = new DateTime(2017, 3, 16)  },
                    new Video { VideoId = 8, Title = "Building Your First Xamarin.Forms App with Xamarin for Visual Studio", Image = "v9.png", ListName = "Xamarin University", Duration = 41, ViewCount = 109944, ReleaseDate = new DateTime(2017, 3, 9)  },
                    new Video { VideoId = 7, Title = "Intro to Xamarin for Visual Studio: Native iOS, Android, and Windows Apps in C#", Image = "v7.png", ListName = "Xamarin University", Duration = 52, ViewCount = 34618, ReleaseDate = new DateTime(2017, 3, 2)  },
                    // EVOLVE 2016
                    new Video { VideoId = 6, Title = "Mobile Search: Making your mobile apps stand out – James Montemagno", Image = "v6.png", ListName = "Evolve 2016", Duration = 44, ViewCount = 2808, ReleaseDate = new DateTime(2016, 6, 4)  },
                    new Video { VideoId = 5, Title = "Async and Await, All the Things Your Mother Never Told You – James Clancey", Image = "v5.png", ListName = "Evolve 2016", Duration = 45, ViewCount = 14070, ReleaseDate = new DateTime(2016, 5, 4)  },
                    new Video { VideoId = 4, Title = "Creating Custom Layouts in Xamarin.Forms – Jason Smith", Image = "v4.png", ListName = "Evolve 2016", Duration = 45, ViewCount = 19944, ReleaseDate = new DateTime(2016, 4, 30)  },
                    new Video { VideoId = 3, Title = "Xamarin Evolve 2016: Becoming a XAML Master – Charles Petzold", Image = "v3.png", ListName = "Evolve 2016", Duration = 48, ViewCount = 34822, ReleaseDate = new DateTime(2016, 4, 30)  },
                    new Video { VideoId = 2, Title = "Optimizing App Performance with Xamarin.Forms – Jason Smith", Image = "v2.png", ListName = "Evolve 2016", Duration = 48, ViewCount = 16381, ReleaseDate = new DateTime(2016, 4, 28)  },
                    new Video { VideoId = 1, Title = "Xamarin Evolve 2016: Keynote", Image = "v1.png", ListName = "Evolve 2016", Duration = 72, ViewCount = 58327, ReleaseDate = new DateTime(2016, 4, 28)  }
                };
            }
        }

        public List<VideoComment> Comments
        {
            get
            {
                return new List<VideoComment>
                {
                    new VideoComment { Username = "Melissa", Avatar = "u1.jpg", Comment = "Will concepts like x:Bind come to Xamarin.Forms?", Date = "1 hour ago" },
                    new VideoComment { Username = "John", Avatar = "u2.jpg", Comment = "What is the difference with compiled bindings?", Date = "2 hours ago" },
                    new VideoComment { Username = "Robert", Avatar = "u3.jpg", Comment = "Mnn, I don't understand the Converters concept. Can we focus on that topic in another video?", Date = "Yesterday" },
                    new VideoComment { Username = "Thomas", Avatar = "u4.jpg", Comment = "Go Xamarin Team, Go!", Date = "Yesterday" },
                    new VideoComment { Username = "Tamara", Avatar = "u5.jpg", Comment = "Can we see the same with more examples in C# ?. Is there any kind of change related to performance? I would be interested to know this.", Date = "5 days ago" },
                    new VideoComment { Username = "Aysha", Avatar = "u6.jpg", Comment = "It would be nice to see this in a live on Twitch answering questions.", Date = "2 weeks ago" },
                    new VideoComment { Username = "James", Avatar = "u7.jpg", Comment = "I understand the concept but it is not clear to me the use of the modes. What is the difference between OneWay and OneTime?.", Date = "1 month ago" },
                    new VideoComment { Username = "Marie", Avatar = "u8.jpg", Comment = "What is the difference when using compiled bindings?.", Date = "1 month ago" },
                    new VideoComment { Username = "William", Avatar = "u9.jpg", Comment = "The topic is interesting, can you continue creating related videos?.", Date = "1 month ago" },
                    new VideoComment { Username = "Madeleine", Avatar = "u10.jpg", Comment = "I love this kind of short videos!.", Date = "2 months ago" }
                };
            }
        }

        public List<string> LastSearch
        {
            get
            {
                return new List<string>
                {
                    "Surface Duo",
                    "Surface Neo",
                    "Xamarin.Forms",
                    "XAML",
                    "Binding",
                    "Maddy Leger",
                    "Font Icons",
                    "James Montemagno",
                    "David Ortinau"
                };
            }
        }

        public async Task<IEnumerable<Video>> GetLatestAsync()
        {
            await Task.Delay(GetRandomLoadingTime());

            return Videos.OrderBy(v => v.ReleaseDate).Take(10).ToList();
        }

        public async Task<IEnumerable<Video>> GetPopularAsync()
        {
            await Task.Delay(GetRandomLoadingTime());

            return Videos.OrderBy(v => v.ViewCount).Take(10).ToList();
        }

        public async Task<IEnumerable<Video>> GetVideosByListNameAsync(string listName)
        {
            await Task.Delay(GetRandomLoadingTime());

            return Videos.Where(v => v.ListName == listName).ToList();
        }

        public async Task<IEnumerable<Video>> GetVideosAsync()
        {
            await Task.Delay(GetRandomLoadingTime());

            return Videos;
        }

        public IEnumerable<VideoGroup> GetVideoGroups()
        {
            IEnumerable<VideoGroup> groups =
                from item in Videos
                group item by item.ListName into videoGroup
                select new VideoGroup(videoGroup)
                {
                    Title = videoGroup.Key
                };

            return groups;
        }

        public async Task<IEnumerable<VideoComment>> GetVideoCommentsAsync(int videoId)
        {
            await Task.Delay(GetRandomLoadingTime());

            if (videoId != 0)
                return Comments;

            return new List<VideoComment>();
        }

        public async Task<IEnumerable<string>> GetRecentSearchesAsync()
        {
            await Task.Delay(GetRandomLoadingTime());

            return LastSearch;
        }

        int GetRandomLoadingTime()
        {
            var random = new Random();
            return random.Next(0, 500);
        }
    }
}