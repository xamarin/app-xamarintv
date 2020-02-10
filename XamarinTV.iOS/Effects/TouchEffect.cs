using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using XamarinTV.Events;
using XamarinTV.Models;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamarinTV")]
[assembly: ExportEffect(typeof(XamarinTV.iOS.Effects.TouchEffect), "TouchEffect")]
namespace XamarinTV.iOS.Effects
{
    public class TouchEffect : PlatformEffect
    {
        UIView _view;
        TouchRecognizer _touchRecognizer;

        protected override void OnAttached()
        {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            _view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            XamarinTV.Effects.TouchEffect effect = (XamarinTV.Effects.TouchEffect)Element.Effects.FirstOrDefault(e => e is XamarinTV.Effects.TouchEffect);

            if (effect != null && _view != null)
            {
                // Create a TouchRecognizer for this UIView
                _touchRecognizer = new TouchRecognizer(Element, _view, effect);
                _view.AddGestureRecognizer(_touchRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (_touchRecognizer != null)
            {
                // Clean up the TouchRecognizer object
                _touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                _view.RemoveGestureRecognizer(_touchRecognizer);
            }
        }

        class TouchRecognizer : UIGestureRecognizer
        {
            readonly Element _element;        // Forms element for firing events
            readonly UIView _view;            // iOS UIView 
            XamarinTV.Effects.TouchEffect _touchEffect;
            bool _capture;

            static Dictionary<UIView, TouchRecognizer> viewDictionary =
                new Dictionary<UIView, TouchRecognizer>();

            static Dictionary<long, TouchRecognizer> idToTouchDictionary =
                new Dictionary<long, TouchRecognizer>();

            public TouchRecognizer(Element element, UIView view, XamarinTV.Effects.TouchEffect touchEffect)
            {
                _element = element;
                _view = view;
                _touchEffect = touchEffect;

                viewDictionary.Add(view, this);
            }

            public void Detach()
            {
                viewDictionary.Remove(_view);
            }

            // touches = touches of interest; evt = all touches of type UITouch
            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                foreach (UITouch touch in touches.Cast<UITouch>())
                {
                    long id = touch.Handle.ToInt64();
                    FireEvent(this, id, TouchActionType.Pressed, touch, true);

                    if (!idToTouchDictionary.ContainsKey(id))
                    {
                        idToTouchDictionary.Add(id, this);
                    }
                }

                // Save the setting of the Capture property
                _capture = _touchEffect.Capture;
            }

            public override void TouchesMoved(NSSet touches, UIEvent evt)
            {
                base.TouchesMoved(touches, evt);

                foreach (UITouch touch in touches.Cast<UITouch>())
                {
                    long id = touch.Handle.ToInt64();

                    if (_capture)
                    {
                        FireEvent(this, id, TouchActionType.Moved, touch, true);
                    }
                    else
                    {
                        CheckForBoundaryHop(touch);

                        if (idToTouchDictionary[id] != null)
                        {
                            FireEvent(idToTouchDictionary[id], id, TouchActionType.Moved, touch, true);
                        }
                    }
                }
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                foreach (UITouch touch in touches.Cast<UITouch>())
                {
                    long id = touch.Handle.ToInt64();

                    if (_capture)
                    {
                        FireEvent(this, id, TouchActionType.Released, touch, false);
                    }
                    else
                    {
                        CheckForBoundaryHop(touch);

                        if (idToTouchDictionary[id] != null)
                        {
                            FireEvent(idToTouchDictionary[id], id, TouchActionType.Released, touch, false);
                        }
                    }
                    idToTouchDictionary.Remove(id);
                }
            }

            public override void TouchesCancelled(NSSet touches, UIEvent evt)
            {
                base.TouchesCancelled(touches, evt);

                foreach (UITouch touch in touches.Cast<UITouch>())
                {
                    long id = touch.Handle.ToInt64();

                    if (_capture)
                    {
                        FireEvent(this, id, TouchActionType.Cancelled, touch, false);
                    }
                    else if (idToTouchDictionary[id] != null)
                    {
                        FireEvent(idToTouchDictionary[id], id, TouchActionType.Cancelled, touch, false);
                    }
                    idToTouchDictionary.Remove(id);
                }
            }

            void CheckForBoundaryHop(UITouch touch)
            {
                long id = touch.Handle.ToInt64();

                // TODO: Might require converting to a List for multiple hits
                TouchRecognizer recognizerHit = null;

                foreach (UIView view in viewDictionary.Keys)
                {
                    CGPoint location = touch.LocationInView(view);

                    if (new CGRect(new CGPoint(), view.Frame.Size).Contains(location))
                    {
                        recognizerHit = viewDictionary[view];
                    }
                }
                if (recognizerHit != idToTouchDictionary[id])
                {
                    if (idToTouchDictionary[id] != null)
                    {
                        FireEvent(idToTouchDictionary[id], id, TouchActionType.Exited, touch, true);
                    }
                    if (recognizerHit != null)
                    {
                        FireEvent(recognizerHit, id, TouchActionType.Entered, touch, true);
                    }
                    idToTouchDictionary[id] = recognizerHit;
                }
            }

            void FireEvent(TouchRecognizer recognizer, long id, TouchActionType actionType, UITouch touch, bool isInContact)
            {
                // Convert touch location to Xamarin.Forms Point value
                CGPoint cgPoint = touch.LocationInView(recognizer.View);
                Point xfPoint = new Point(cgPoint.X, cgPoint.Y);

                // Get the method to call for firing events
                Action<Element, TouchActionEventArgs> onTouchAction = recognizer._touchEffect.OnTouchAction;

                // Call that method
                onTouchAction(recognizer._element,
                    new TouchActionEventArgs(id, actionType, xfPoint, isInContact));
            }
        }
    }
}