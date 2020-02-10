using XamarinTV.Events;
using XamarinTV.Models;
using System;
using System.Linq;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("XamarinTV")]
[assembly: ExportEffect(typeof(XamarinTV.UWP.Effects.TouchEffect), "TouchEffect")]
namespace XamarinTV.UWP.Effects
{
    public class TouchEffect : PlatformEffect
    {
        FrameworkElement _frameworkElement;
        XamarinTV.Effects.TouchEffect _effect;
        Action<Element, TouchActionEventArgs> _onTouchAction;

        protected override void OnAttached()
        {
            // Get the Windows FrameworkElement corresponding to the Element that the effect is attached to
            _frameworkElement = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            _effect = (XamarinTV.Effects.TouchEffect)Element.Effects.
                FirstOrDefault(e => e is XamarinTV.Effects.TouchEffect);

            if (_effect != null && _frameworkElement != null)
            {
                // Save the method to call on touch events
                _onTouchAction = _effect.OnTouchAction;

                // Set event handlers on FrameworkElement
                _frameworkElement.PointerEntered += OnPointerEntered;
                _frameworkElement.PointerPressed += OnPointerPressed;
                _frameworkElement.PointerMoved += OnPointerMoved;
                _frameworkElement.PointerReleased += OnPointerReleased;
                _frameworkElement.PointerExited += OnPointerExited;
                _frameworkElement.PointerCanceled += OnPointerCancelled;
            }
        }

        protected override void OnDetached()
        {
            if (_onTouchAction != null)
            {
                // Release event handlers on FrameworkElement
                _frameworkElement.PointerEntered -= OnPointerEntered;
                _frameworkElement.PointerPressed -= OnPointerPressed;
                _frameworkElement.PointerMoved -= OnPointerMoved;
                _frameworkElement.PointerReleased -= OnPointerReleased;
                _frameworkElement.PointerExited -= OnPointerEntered;
                _frameworkElement.PointerCanceled -= OnPointerCancelled;
            }
        }

        void OnPointerEntered(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, TouchActionType.Entered, args);
        }

        void OnPointerPressed(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, TouchActionType.Pressed, args);

            // Check setting of Capture property
            if (_effect.Capture)
            {
                (sender as FrameworkElement).CapturePointer(args.Pointer);
            }
        }

        void OnPointerMoved(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, TouchActionType.Moved, args);
        }

        void OnPointerReleased(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, TouchActionType.Released, args);
        }

        void OnPointerExited(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, TouchActionType.Exited, args);
        }

        void OnPointerCancelled(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, TouchActionType.Cancelled, args);
        }

        void CommonHandler(object sender, TouchActionType touchActionType, PointerRoutedEventArgs args)
        {
            PointerPoint pointerPoint = args.GetCurrentPoint(sender as UIElement);
            Windows.Foundation.Point windowsPoint = pointerPoint.Position;

            _onTouchAction(Element, new TouchActionEventArgs(
                args.Pointer.PointerId,                     
                touchActionType,
                new Point(windowsPoint.X, windowsPoint.Y),
                args.Pointer.IsInContact));
        }
    }
}
