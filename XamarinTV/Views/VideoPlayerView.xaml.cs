using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;

namespace XamarinTV.Views
{
    public partial class VideoPlayerView : ContentView
    {
        Timer _inactivityTimer;
        Timer _playbackTimer;

        public VideoPlayerView()
        {
            InitializeComponent();
            _inactivityTimer = new Timer(TimeSpan.FromSeconds(3).TotalMilliseconds);
            _playbackTimer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            
            if (Parent == null)
            {
                _playbackTimer.Elapsed -= OnPlaybackTimerElapsed;
                _playbackTimer.Stop();

                _inactivityTimer.Elapsed -= OnInactivityTimerElapsed;
                _inactivityTimer.Stop();
                DualScreenInfo.Current.PropertyChanged -= OnDualScreenInfoChanged;
            }
            else
            {
                _playbackTimer.Elapsed += OnPlaybackTimerElapsed;
                _playbackTimer.Start();

                _inactivityTimer.Elapsed += OnInactivityTimerElapsed;
                _inactivityTimer.Start();
                DualScreenInfo.Current.PropertyChanged += OnDualScreenInfoChanged;
                UpdateAspectRatio();
            }
        }

        void UpdateAspectRatio()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (DualScreenInfo.Current.IsLandscape)
                {
                    VideoPlayer.Aspect = Aspect.AspectFill;
                    VideoPlayer.HeightRequest = -1;
                }
                else
                {
                    VideoPlayer.Aspect = Aspect.AspectFit;
                    VideoPlayer.HeightRequest = 300;
                }

            });
        }

        void OnDualScreenInfoChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAspectRatio();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            UpdateAspectRatio();
        }

        private void OnPlaybackTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateTimeDisplay();
        }

        private async void OnInactivityTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await Task.WhenAny<bool>
            (
                PlayerHUD.FadeTo(0)
            );

            _inactivityTimer.Stop();

            if (Parent != null)
                _inactivityTimer.Start();
        }

        private void MediaElement_StateRequested(object sender, StateRequested e)
        {
            VisualStateManager.GoToState(PlayPauseToggle,
                (e.State == MediaElementState.Playing)
                ? "playing"
                : "paused");

            if (e.State == MediaElementState.Playing)
            {
                _playbackTimer.Stop();
                if (Parent != null)
                    _playbackTimer.Start();

            }
            else if (e.State == MediaElementState.Paused || e.State == MediaElementState.Stopped)
            {
                _playbackTimer.Stop();
            }
        }

        void PlayPauseToggle_Clicked(object sender, EventArgs e)
        {
            if (VideoPlayer.CurrentState == MediaElementState.Playing)
            {
                VideoPlayer.Pause();
            }
            else
            {
                VideoPlayer.Play();
            }
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            if (PlayerHUD.Opacity == 1)
            {
                await Task.WhenAny<bool>
                (
                    PlayerHUD.FadeTo(0)
                );
            }
            else
            {
                await Task.WhenAny<bool>
                (
                    PlayerHUD.FadeTo(1, 100)
                );
            }

            _inactivityTimer.Stop();
            if (Parent != null)
                _inactivityTimer.Start();
        }

        void VideoPlayer_MediaOpened(object sender, EventArgs e)
        {
            UpdateTimeDisplay();
        }

        void UpdateTimeDisplay()
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                TimeAndDuration.Text = $"{VideoPlayer.Position.ToString(@"hh\:mm\:ss")} / {VideoPlayer.Duration?.ToString(@"hh\:mm\:ss")}";
            });
        }
    }
}