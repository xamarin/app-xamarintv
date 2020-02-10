using System;
using System.Collections.Generic;
using System.Linq;
using XamarinTV.Events;
using XamarinTV.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace XamarinTV.Views
{
    public class InProgressPaint
    {
        public InProgressPaint(long paintId, SKPath path, SKColor color)
        {
            PaintId = paintId;
            Path = path;
            Color = color;
        }

        public long PaintId { get; set; }
        public SKPath Path { get; set; }
        public SKColor Color { get; set; }
    }

    public class CompletedPaint
    {
        public CompletedPaint(SKPath path, SKColor color)
        {
            Path = path;
            Color = color;
        }

        public SKPath Path { get; set; }
        public SKColor Color { get; set; }
    }

    public partial class NotesView : ContentView
    {
        readonly List<string> _colors;
        readonly List<InProgressPaint> _inProgressPaths;
        readonly List<CompletedPaint> _completedPaths;

        readonly SKPaint _paint;
        SKColor _color;
        private bool _clear;

        public NotesView()
        {
            InitializeComponent();

            _inProgressPaths = new List<InProgressPaint>();
            _completedPaths = new List<CompletedPaint>();

            _colors = new List<string>
             {
                "#000000", "#ff0000", "#ff8000",
                "#ffbf00", "#ffff00", "#80ff00",
                "#00ff40", "#00ffff", "#00bfff",
                "#0080ff", "#0000ff", "#4000ff",
                "#8000ff", "#bf00ff", "#ff00ff",
                "#ff00bf", "#ff0080", "#ff0040",
                "#ff0000"
             };

            _color = SKColors.Black;
            _paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = _color,
                StrokeWidth = 10,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round
            };

            BindableLayout.SetItemsSource(ColorsLayout, _colors.Select(x => new ColorSource(Color.FromHex(x))));
        }

        public ColorSource SelectedColorSource { get; set; }

        void OnColorSourceTapped(object sender, EventArgs e)
        {
            if (!(sender is BindableObject bindable) || !(bindable.BindingContext is ColorSource selectedColorSource))
                return;

            if (SelectedColorSource == selectedColorSource)
                return;

            selectedColorSource.IsSelected = true;

            if (SelectedColorSource != null)
                SelectedColorSource.IsSelected = false;

            SelectedColorSource = selectedColorSource;
            
            SKColor.TryParse(SelectedColorSource.Color.ToHex(), out SKColor sKColor);

            _color = sKColor;
            _paint.Color = _color;
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!_inProgressPaths.Any(p => p.PaintId == args.Id))
                    {
                        SKPath path = new SKPath();
                        path.MoveTo(ConvertToPixel(args.Location));
                        _inProgressPaths.Add(new InProgressPaint(args.Id, path, _color));
                        CanvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Moved:
                    if (_inProgressPaths.Any(p => p.PaintId == args.Id))
                    {
                        var inProgressPath = _inProgressPaths.FirstOrDefault(p => p.PaintId == args.Id);

                        if (inProgressPath != null)
                        {
                            inProgressPath.Path.LineTo(ConvertToPixel(args.Location));
                            CanvasView.InvalidateSurface();
                        }
                    }
                    break;

                case TouchActionType.Released:
                    if (_inProgressPaths.Any(p => p.PaintId == args.Id))
                    {
                        var inProgressPath = _inProgressPaths.FirstOrDefault(p => p.PaintId == args.Id);

                        if (inProgressPath != null)
                        {
                            _completedPaths.Add(new CompletedPaint(inProgressPath.Path, _color));
                            _inProgressPaths.Remove(inProgressPath);
                            CanvasView.InvalidateSurface();
                        }
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (_inProgressPaths.Any(p => p.PaintId == args.Id))
                    {
                        var inProgressPath = _inProgressPaths.FirstOrDefault(p => p.PaintId == args.Id);
                        _inProgressPaths.Remove(inProgressPath);
                        CanvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        void ClearButton_Clicked(object sender, EventArgs e)
        {
            _clear = true;
            CanvasView.InvalidateSurface();
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            if (_clear == true)
            {
                _clear = false;
                _inProgressPaths.Clear();
                _completedPaths.Clear();
                return;
            }

            foreach (var completedPath in _completedPaths)
            {
                _paint.Color = completedPath.Color;
                canvas.DrawPath(completedPath.Path, _paint);
            }

            foreach (var inProgressPath in _inProgressPaths)
            {
                _paint.Color = inProgressPath.Color;
                canvas.DrawPath(inProgressPath.Path, _paint);
            }
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(CanvasView.CanvasSize.Width * pt.X / CanvasView.Width),
                               (float)(CanvasView.CanvasSize.Height * pt.Y / CanvasView.Height));
        }
    }
}
