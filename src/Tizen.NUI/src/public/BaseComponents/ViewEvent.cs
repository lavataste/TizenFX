﻿/*
 * Copyright(c) 2021 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Tizen.NUI.BaseComponents
{
    /// <summary>
    /// View is the base class for all views.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public partial class View
    {
        private EventHandler offWindowEventHandler;
        private OffWindowEventCallbackType offWindowEventCallback;
        private EventHandlerWithReturnType<object, WheelEventArgs, bool> wheelEventHandler;
        private WheelEventCallbackType wheelEventCallback;
        private EventHandlerWithReturnType<object, KeyEventArgs, bool> keyEventHandler;
        private KeyCallbackType keyCallback;
        private EventHandlerWithReturnType<object, TouchEventArgs, bool> interceptTouchDataEventHandler;
        private TouchDataCallbackType interceptTouchDataCallback;
        private EventHandlerWithReturnType<object, TouchEventArgs, bool> touchDataEventHandler;
        private TouchDataCallbackType touchDataCallback;
        private EventHandlerWithReturnType<object, HoverEventArgs, bool> hoverEventHandler;
        private HoverEventCallbackType hoverEventCallback;
        private EventHandler<VisibilityChangedEventArgs> visibilityChangedEventHandler;
        private VisibilityChangedEventCallbackType visibilityChangedEventCallback;
        private EventHandler keyInputFocusGainedEventHandler;

        private KeyInputFocusGainedCallbackType keyInputFocusGainedCallback;
        private EventHandler keyInputFocusLostEventHandler;

        private KeyInputFocusLostCallbackType keyInputFocusLostCallback;
        private EventHandler onRelayoutEventHandler;
        private OnRelayoutEventCallbackType onRelayoutEventCallback;
        private EventHandler onWindowEventHandler;
        private OnWindowEventCallbackType onWindowEventCallback;
        private EventHandler<LayoutDirectionChangedEventArgs> layoutDirectionChangedEventHandler;
        private LayoutDirectionChangedEventCallbackType layoutDirectionChangedEventCallback;
        // Resource Ready Signal
        private EventHandler resourcesLoadedEventHandler;
        private ResourcesLoadedCallbackType ResourcesLoadedCallback;
        private EventHandler<BackgroundResourceLoadedEventArgs> backgroundResourceLoadedEventHandler;
        private _backgroundResourceLoadedCallbackType backgroundResourceLoadedCallback;
        private OnWindowEventCallbackType onWindowSendEventCallback;
        private void SendViewAddedEventToWindow(IntPtr data)
        {
            // Unused parameter
            _ = data;
            NUIApplication.GetDefaultWindow()?.SendViewAdded(this);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void OffWindowEventCallbackType(IntPtr control);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool WheelEventCallbackType(IntPtr view, IntPtr wheelEvent);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool KeyCallbackType(IntPtr control, IntPtr keyEvent);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool TouchDataCallbackType(IntPtr view, IntPtr touchData);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool HoverEventCallbackType(IntPtr view, IntPtr hoverEvent);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void VisibilityChangedEventCallbackType(IntPtr data, bool visibility, VisibilityChangeType type);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ResourcesLoadedCallbackType(IntPtr control);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void _backgroundResourceLoadedCallbackType(IntPtr view);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void KeyInputFocusGainedCallbackType(IntPtr control);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void KeyInputFocusLostCallbackType(IntPtr control);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void OnRelayoutEventCallbackType(IntPtr control);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void OnWindowEventCallbackType(IntPtr control);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void LayoutDirectionChangedEventCallbackType(IntPtr data, ViewLayoutDirectionType type);

        /// <summary>
        /// Event when a child is removed.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public new event EventHandler<ChildRemovedEventArgs> ChildRemoved;
        /// <summary>
        /// Event when a child is added.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public new event EventHandler<ChildAddedEventArgs> ChildAdded;

        /// <summary>
        /// An event for the KeyInputFocusGained signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The KeyInputFocusGained signal is emitted when the control gets the key input focus.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler FocusGained
        {
            add
            {
                if (keyInputFocusGainedEventHandler == null)
                {
                    keyInputFocusGainedCallback = OnKeyInputFocusGained;
                    using KeyInputFocusSignal signal = new KeyInputFocusSignal(Interop.ViewSignal.KeyInputFocusGainedSignal(SwigCPtr), false);
                    signal?.Connect(keyInputFocusGainedCallback);
                }
                keyInputFocusGainedEventHandler += value;
            }

            remove
            {
                keyInputFocusGainedEventHandler -= value;
                if (keyInputFocusGainedEventHandler == null)
                {
                    using KeyInputFocusSignal signal = new KeyInputFocusSignal(Interop.ViewSignal.KeyInputFocusGainedSignal(SwigCPtr), false);

                    if (keyInputFocusGainedCallback != null)
                    {
                        signal?.Disconnect(keyInputFocusGainedCallback);
                        if (signal?.Empty() == true)
                        {
                            keyInputFocusGainedCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the KeyInputFocusLost signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The KeyInputFocusLost signal is emitted when the control loses the key input focus.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler FocusLost
        {
            add
            {
                if (keyInputFocusLostEventHandler == null)
                {
                    keyInputFocusLostCallback = OnKeyInputFocusLost;
                    using KeyInputFocusSignal signal = new KeyInputFocusSignal(Interop.ViewSignal.KeyInputFocusLostSignal(SwigCPtr), false);
                    signal?.Connect(keyInputFocusLostCallback);
                }
                keyInputFocusLostEventHandler += value;
            }

            remove
            {
                keyInputFocusLostEventHandler -= value;
                if (keyInputFocusLostEventHandler == null)
                {
                    using KeyInputFocusSignal signal = new KeyInputFocusSignal(Interop.ViewSignal.KeyInputFocusLostSignal(SwigCPtr), false);

                    if (keyInputFocusLostCallback != null)
                    {
                        signal?.Disconnect(keyInputFocusLostCallback);
                        if (signal?.Empty() == true)
                        {
                            keyInputFocusLostCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the KeyPressed signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The KeyPressed signal is emitted when the key event is received.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandlerWithReturnType<object, KeyEventArgs, bool> KeyEvent
        {
            add
            {
                if (keyEventHandler == null)
                {
                    keyCallback = OnKeyEvent;
                    using ControlKeySignal signal = new ControlKeySignal(Interop.ViewSignal.KeyEventSignal(SwigCPtr), false);
                    signal?.Connect(keyCallback);
                }
                keyEventHandler += value;
            }

            remove
            {
                keyEventHandler -= value;
                if (keyEventHandler == null)
                {
                    using ControlKeySignal signal = new ControlKeySignal(Interop.ViewSignal.KeyEventSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(keyCallback);
                        if (signal?.Empty() == true)
                        {
                            keyCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the OnRelayout signal which can be used to subscribe or unsubscribe the event handler.<br />
        /// The OnRelayout signal is emitted after the size has been set on the view during relayout.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler Relayout
        {
            add
            {
                if (onRelayoutEventHandler == null)
                {
                    onRelayoutEventCallback = OnRelayout;
                    using ViewSignal signal = new ViewSignal(Interop.ActorSignal.ActorOnRelayoutSignal(SwigCPtr), false);
                    signal?.Connect(onRelayoutEventCallback);
                }
                onRelayoutEventHandler += value;
            }

            remove
            {
                onRelayoutEventHandler -= value;
                if (onRelayoutEventHandler == null)
                {
                    using ViewSignal signal = new ViewSignal(Interop.ActorSignal.ActorOnRelayoutSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(onRelayoutEventCallback);
                        if (signal?.Empty() == true)
                        {
                            onRelayoutEventCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the touched signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The touched signal is emitted when the touch input is received.<br />
        /// This can receive touch events before child. <br />
        /// If it returns false, the child can receive the touch event. If it returns true, the touch event is intercepted. So child cannot receive touch event.<br />
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandlerWithReturnType<object, TouchEventArgs, bool> InterceptTouchEvent
        {
            add
            {
                if (interceptTouchDataEventHandler == null)
                {
                    interceptTouchDataCallback = OnInterceptTouch;
                    using TouchDataSignal signal = new TouchDataSignal(Interop.ActorSignal.ActorInterceptTouchSignal(SwigCPtr), false);
                    signal?.Connect(interceptTouchDataCallback);
                }
                interceptTouchDataEventHandler += value;
            }

            remove
            {
                interceptTouchDataEventHandler -= value;
                if (interceptTouchDataEventHandler == null)
                {
                    using TouchDataSignal signal = new TouchDataSignal(Interop.ActorSignal.ActorInterceptTouchSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(interceptTouchDataCallback);
                        if (signal?.Empty() == true)
                        {
                            interceptTouchDataCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// If child view doesn't want the parent's view to intercept the touch, you can set it to true.
        /// for example :
        ///    parent.Add(child);
        ///    parent.InterceptTouchEvent += OnInterceptTouchEvent;
        ///    View view = child.GetParent() as View;
        ///    view.DisallowInterceptTouchEvent = true;
        ///  This prevents the parent from intercepting touch.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DisallowInterceptTouchEvent { get; set; }


        /// <summary>
        /// An event for the touched signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The touched signal is emitted when the touch input is received.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandlerWithReturnType<object, TouchEventArgs, bool> TouchEvent
        {
            add
            {
                if (touchDataEventHandler == null)
                {
                    touchDataCallback = OnTouch;
                    using TouchDataSignal signal = new TouchDataSignal(Interop.ActorSignal.ActorTouchSignal(SwigCPtr), false);
                    signal.Connect(touchDataCallback);
                }
                touchDataEventHandler += value;
            }

            remove
            {
                touchDataEventHandler -= value;
                if (touchDataEventHandler == null)
                {
                    using TouchDataSignal signal = new TouchDataSignal(Interop.ActorSignal.ActorTouchSignal(SwigCPtr), false);
                    if (signal.Empty() == false)
                    {
                        signal.Disconnect(touchDataCallback);
                        if (signal?.Empty() == true)
                        {
                            touchDataCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the hovered signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The hovered signal is emitted when the hover input is received.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandlerWithReturnType<object, HoverEventArgs, bool> HoverEvent
        {
            add
            {
                if (hoverEventHandler == null)
                {
                    hoverEventCallback = OnHoverEvent;
                    using HoverSignal signal = new HoverSignal(Interop.ActorSignal.ActorHoveredSignal(SwigCPtr), false);
                    signal?.Connect(hoverEventCallback);
                }
                hoverEventHandler += value;
            }

            remove
            {
                hoverEventHandler -= value;
                if (hoverEventHandler == null)
                {
                    using HoverSignal signal = new HoverSignal(Interop.ActorSignal.ActorHoveredSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(hoverEventCallback);
                        if (signal?.Empty() == true)
                        {
                            hoverEventCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the WheelMoved signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The WheelMoved signal is emitted when the wheel event is received.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandlerWithReturnType<object, WheelEventArgs, bool> WheelEvent
        {
            add
            {
                if (wheelEventHandler == null)
                {
                    wheelEventCallback = OnWheelEvent;
                    using WheelSignal signal = new WheelSignal(Interop.ActorSignal.ActorWheelEventSignal(SwigCPtr), false);
                    signal?.Connect(wheelEventCallback);
                }
                wheelEventHandler += value;

                if (WindowWheelEventHandler == null)
                {
                    NUIApplication.GetDefaultWindow().WheelEvent += OnWindowWheelEvent;
                }
                WindowWheelEventHandler += value;
            }

            remove
            {
                wheelEventHandler -= value;
                if (wheelEventHandler == null)
                {
                    using WheelSignal signal = new WheelSignal(Interop.ActorSignal.ActorWheelEventSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(wheelEventCallback);
                        if (signal?.Empty() == true)
                        {
                            wheelEventCallback = null;
                        }
                    }
                }

                WindowWheelEventHandler -= value;
                if (WindowWheelEventHandler == null)
                {
                    NUIApplication.GetDefaultWindow().WheelEvent -= OnWindowWheelEvent;
                }
            }
        }

        /// <summary>
        /// An event for the OnWindow signal which can be used to subscribe or unsubscribe the event handler.<br />
        /// The OnWindow signal is emitted after the view has been connected to the window.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler AddedToWindow
        {
            add
            {
                if (onWindowEventHandler == null)
                {
                    onWindowEventCallback = OnWindow;
                    using ViewSignal signal = new ViewSignal(Interop.ActorSignal.ActorOnSceneSignal(SwigCPtr), false);
                    signal?.Connect(onWindowEventCallback);
                }
                onWindowEventHandler += value;
            }

            remove
            {
                onWindowEventHandler -= value;
                if (onWindowEventHandler == null)
                {
                    using ViewSignal signal = new ViewSignal(Interop.ActorSignal.ActorOnSceneSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(onWindowEventCallback);
                        if (signal?.Empty() == true)
                        {
                            onWindowEventCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for the OffWindow signal, which can be used to subscribe or unsubscribe the event handler.<br />
        /// OffWindow signal is emitted after the view has been disconnected from the window.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler RemovedFromWindow
        {
            add
            {
                if (offWindowEventHandler == null)
                {
                    offWindowEventCallback = OffWindow;
                    using ViewSignal signal = new ViewSignal(Interop.ActorSignal.ActorOffSceneSignal(SwigCPtr), false);
                    signal?.Connect(offWindowEventCallback);
                }
                offWindowEventHandler += value;
            }

            remove
            {
                offWindowEventHandler -= value;
                if (offWindowEventHandler == null)
                {
                    using ViewSignal signal = new ViewSignal(Interop.ActorSignal.ActorOffSceneSignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(offWindowEventCallback);
                        if (signal?.Empty() == true)
                        {
                            offWindowEventCallback = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// An event for visibility change which can be used to subscribe or unsubscribe the event handler.<br />
        /// This signal is emitted when the visible property of this or a parent view is changed.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged
        {
            add
            {
                if (visibilityChangedEventHandler == null)
                {
                    visibilityChangedEventCallback = OnVisibilityChanged;
                    VisibilityChangedSignal(this).Connect(visibilityChangedEventCallback);
                }

                visibilityChangedEventHandler += value;
            }

            remove
            {
                visibilityChangedEventHandler -= value;

                if (visibilityChangedEventHandler == null && VisibilityChangedSignal(this).Empty() == false)
                {
                    VisibilityChangedSignal(this).Disconnect(visibilityChangedEventCallback);
                    if (VisibilityChangedSignal(this).Empty() == true)
                    {
                        visibilityChangedEventCallback = null;
                    }
                }
            }
        }

        /// <summary>
        /// Event for layout direction change which can be used to subscribe/unsubscribe the event handler.<br />
        /// This signal is emitted when the layout direction property of this or a parent view is changed.<br />
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler<LayoutDirectionChangedEventArgs> LayoutDirectionChanged
        {
            add
            {
                if (layoutDirectionChangedEventHandler == null)
                {
                    layoutDirectionChangedEventCallback = OnLayoutDirectionChanged;
                    LayoutDirectionChangedSignal(this).Connect(layoutDirectionChangedEventCallback);
                }

                layoutDirectionChangedEventHandler += value;
            }

            remove
            {
                layoutDirectionChangedEventHandler -= value;

                if (layoutDirectionChangedEventHandler == null && LayoutDirectionChangedSignal(this).Empty() == false)
                {
                    LayoutDirectionChangedSignal(this).Disconnect(layoutDirectionChangedEventCallback);
                    if (LayoutDirectionChangedSignal(this).Empty() == true)
                    {
                        layoutDirectionChangedEventCallback = null;
                    }
                }
            }
        }

        /// <summary>
        /// An event for the ResourcesLoadedSignal signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// This signal is emitted after all resources required by a view are loaded and ready.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler ResourcesLoaded
        {
            add
            {
                if (resourcesLoadedEventHandler == null)
                {
                    ResourcesLoadedCallback = OnResourcesLoaded;
                    using ViewSignal signal = new ViewSignal(Interop.View.ResourceReadySignal(SwigCPtr), false);
                    signal?.Connect(ResourcesLoadedCallback);
                }
                resourcesLoadedEventHandler += value;
            }

            remove
            {
                resourcesLoadedEventHandler -= value;
                if (resourcesLoadedEventHandler == null)
                {
                    using ViewSignal signal = new ViewSignal(Interop.View.ResourceReadySignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(ResourcesLoadedCallback);
                        if (signal?.Empty() == true)
                        {
                            ResourcesLoadedCallback = null;
                        }
                    }
                }
            }
        }

        private EventHandler _backKeyPressed;

        /// <summary>
        /// An event for getting notice when physical back key is pressed.<br />
        /// This event is emitted BackKey is up.<br />
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler BackKeyPressed
        {
            add
            {
                _backKeyPressed += value;
                BackKeyManager.Instance.Subscriber.Add(this);
            }

            remove
            {
                BackKeyManager.Instance.Subscriber.Remove(this);
                _backKeyPressed -= value;
            }
        }

        /// <summary>
        /// Function for emitting BackKeyPressed event outside of View instance
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void EmitBackKeyPressed()
        {
            _backKeyPressed.Invoke(this, null);
        }


        internal event EventHandler<BackgroundResourceLoadedEventArgs> BackgroundResourceLoaded
        {
            add
            {
                if (backgroundResourceLoadedEventHandler == null)
                {
                    backgroundResourceLoadedCallback = OnBackgroundResourceLoaded;
                    using ViewSignal signal = new ViewSignal(Interop.View.ResourceReadySignal(SwigCPtr), false);
                    signal?.Connect(backgroundResourceLoadedCallback);
                }
                backgroundResourceLoadedEventHandler += value;
            }

            remove
            {
                backgroundResourceLoadedEventHandler -= value;
                if (backgroundResourceLoadedEventHandler == null)
                {
                    using ViewSignal signal = new ViewSignal(Interop.View.ResourceReadySignal(SwigCPtr), false);
                    if (signal?.Empty() == false)
                    {
                        signal?.Disconnect(backgroundResourceLoadedCallback);
                        if (signal?.Empty() == true)
                        {
                            backgroundResourceLoadedCallback = null;
                        }
                    }
                }
            }
        }

        internal TouchDataSignal InterceptTouchSignal()
        {
            TouchDataSignal ret = new TouchDataSignal(Interop.ActorSignal.ActorInterceptTouchSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal TouchDataSignal TouchSignal()
        {
            TouchDataSignal ret = new TouchDataSignal(Interop.ActorSignal.ActorTouchSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal HoverSignal HoveredSignal()
        {
            HoverSignal ret = new HoverSignal(Interop.ActorSignal.ActorHoveredSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal WheelSignal WheelEventSignal()
        {
            WheelSignal ret = new WheelSignal(Interop.ActorSignal.ActorWheelEventSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ViewSignal OnWindowSignal()
        {
            ViewSignal ret = new ViewSignal(Interop.ActorSignal.ActorOnSceneSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ViewSignal OffWindowSignal()
        {
            ViewSignal ret = new ViewSignal(Interop.ActorSignal.ActorOffSceneSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ViewSignal OnRelayoutSignal()
        {
            ViewSignal ret = new ViewSignal(Interop.ActorSignal.ActorOnRelayoutSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ViewVisibilityChangedSignal VisibilityChangedSignal(View view)
        {
            ViewVisibilityChangedSignal ret = new ViewVisibilityChangedSignal(Interop.NDalic.VisibilityChangedSignal(View.getCPtr(view)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ViewLayoutDirectionChangedSignal LayoutDirectionChangedSignal(View view)
        {
            ViewLayoutDirectionChangedSignal ret = new ViewLayoutDirectionChangedSignal(Interop.Layout.LayoutDirectionChangedSignal(View.getCPtr(view)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ViewSignal ResourcesLoadedSignal()
        {
            ViewSignal ret = new ViewSignal(Interop.View.ResourceReadySignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal ControlKeySignal KeyEventSignal()
        {
            ControlKeySignal ret = new ControlKeySignal(Interop.ViewSignal.KeyEventSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal KeyInputFocusSignal KeyInputFocusGainedSignal()
        {
            KeyInputFocusSignal ret = new KeyInputFocusSignal(Interop.ViewSignal.KeyInputFocusGainedSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal KeyInputFocusSignal KeyInputFocusLostSignal()
        {
            KeyInputFocusSignal ret = new KeyInputFocusSignal(Interop.ViewSignal.KeyInputFocusLostSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        private void OnColorChanged(float r, float g, float b, float a)
        {
            Color = new Color(r, g, b, a);
        }

        private void OnMinimumSizeChanged(int width, int height)
        {
            MinimumSize = new Size2D(width, height);
        }

        private void OnMaximumSizeChanged(int width, int height)
        {
            MaximumSize = new Size2D(width, height);
        }

        private void OnPosition2DChanged(int x, int y)
        {
            SetPosition((float)x, (float)y, 0);
        }

        private void OnPositionChanged(float x, float y, float z)
        {
            SetPosition(x, y, z);
        }

        private void OnSize2DChanged(int width, int height)
        {
            SetSize((float)width, (float)height, 0);
        }

        private void OnSizeChanged(float width, float height, float depth)
        {
            SetSize(width, height, depth);
        }

        private void OnParentOriginChanged(float x, float y, float z)
        {
            ParentOrigin = new Position(x, y, z);
        }

        private void OnPivotPointChanged(float x, float y, float z)
        {
            PivotPoint = new Position(x, y, z);
        }

        private void OnImageShadowChanged(ShadowBase instance)
        {
            ImageShadow = (ImageShadow)instance;
        }

        private void OnBoxShadowChanged(ShadowBase instance)
        {
            BoxShadow = (Shadow)instance;
        }

        private void OnBackgroundImageBorderChanged(int left, int right, int bottom, int top)
        {
            BackgroundImageBorder = new Rectangle(left, right, bottom, top);
        }

        private void OnKeyInputFocusGained(IntPtr view)
        {
            if (IsNativeHandleInvalid())
            {
                if (this.Disposed)
                {
                    if (keyInputFocusGainedEventHandler != null)
                    {
                        var process = global::System.Diagnostics.Process.GetCurrentProcess().Id;
                        var thread = global::System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var me = this.GetType().FullName;

                        throw new ObjectDisposedException(nameof(SwigCPtr), $"Error! NUI's native dali object is already disposed. " +
                            $"OR the native dali object handle of NUI becomes null! \n" +
                            $" process:{process} thread:{thread}, isDisposed:{this.Disposed}, isDisposeQueued:{this.IsDisposeQueued}, me:{me}\n");
                    }
                }
                else
                {
                    if (this.IsDisposeQueued)
                    {
                        var process = global::System.Diagnostics.Process.GetCurrentProcess().Id;
                        var thread = global::System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var me = this.GetType().FullName;

                        //in this case, the View object is ready to be disposed waiting on DisposeQueue, so event callback should not be invoked!
                        Tizen.Log.Error("NUI", "in this case, the View object is ready to be disposed waiting on DisposeQueue, so event callback should not be invoked! just return here! \n" +
                            $"process:{process} thread:{thread}, isDisposed:{this.Disposed}, isDisposeQueued:{this.IsDisposeQueued}, me:{me}\n");
                        return;
                    }
                }
            }

            keyInputFocusGainedEventHandler?.Invoke(this, null);
        }

        private void OnKeyInputFocusLost(IntPtr view)
        {
            if (IsNativeHandleInvalid())
            {
                if (this.Disposed)
                {
                    if (keyInputFocusLostEventHandler != null)
                    {
                        var process = global::System.Diagnostics.Process.GetCurrentProcess().Id;
                        var thread = global::System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var me = this.GetType().FullName;

                        throw new ObjectDisposedException(nameof(SwigCPtr), $"Error! NUI's native dali object is already disposed. " +
                            $"OR the native dali object handle of NUI becomes null! \n" +
                            $" process:{process} thread:{thread}, isDisposed:{this.Disposed}, isDisposeQueued:{this.IsDisposeQueued}, me:{me}\n");
                    }
                }
                else
                {
                    if (this.IsDisposeQueued)
                    {
                        var process = global::System.Diagnostics.Process.GetCurrentProcess().Id;
                        var thread = global::System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var me = this.GetType().FullName;

                        //in this case, the View object is ready to be disposed waiting on DisposeQueue, so event callback should not be invoked!
                        Tizen.Log.Error("NUI", "in this case, the View object is ready to be disposed waiting on DisposeQueue, so event callback should not be invoked! just return here! \n" +
                            $"process:{process} thread:{thread}, isDisposed:{this.Disposed}, isDisposeQueued:{this.IsDisposeQueued}, me:{me}\n");
                        return;
                    }
                }
            }

            keyInputFocusLostEventHandler?.Invoke(this, null);
        }

        private bool OnKeyEvent(IntPtr view, IntPtr keyEvent)
        {
            if (keyEvent == global::System.IntPtr.Zero)
            {
                NUILog.Error("keyEvent should not be null!");
                return true;
            }

            KeyEventArgs e = new KeyEventArgs();

            bool result = false;

            e.Key = Tizen.NUI.Key.GetKeyFromPtr(keyEvent);

            if (keyEventHandler != null)
            {
                Delegate[] delegateList = keyEventHandler.GetInvocationList();

                // Oring the result of each callback.
                foreach (EventHandlerWithReturnType<object, KeyEventArgs, bool> del in delegateList)
                {
                    result |= del(this, e);
                }
            }

            return result;
        }

        // Callback for View OnRelayout signal
        private void OnRelayout(IntPtr data)
        {
            if (onRelayoutEventHandler != null)
            {
                onRelayoutEventHandler(this, null);
            }
        }

        // Callback for View TouchSignal
        private bool OnInterceptTouch(IntPtr view, IntPtr touchData)
        {
            if (touchData == global::System.IntPtr.Zero)
            {
                NUILog.Error("touchData should not be null!");
                return true;
            }

            // DisallowInterceptTouchEvent prevents the parent from intercepting touch.
            if (DisallowInterceptTouchEvent)
            {
                return false;
            }

            TouchEventArgs e = new TouchEventArgs();

            e.Touch = Tizen.NUI.Touch.GetTouchFromPtr(touchData);

            bool consumed = false;

            if (interceptTouchDataEventHandler != null)
            {
                consumed = interceptTouchDataEventHandler(this, e);
            }

            return consumed;
        }

        // Callback for View TouchSignal
        private bool OnTouch(IntPtr view, IntPtr touchData)
        {
            if (touchData == global::System.IntPtr.Zero)
            {
                NUILog.Error("touchData should not be null!");
                return true;
            }

            TouchEventArgs e = new TouchEventArgs();

            e.Touch = Tizen.NUI.Touch.GetTouchFromPtr(touchData);

            bool consumed = false;

            if (touchDataEventHandler != null)
            {
                consumed = touchDataEventHandler(this, e);
            }

            if (enableControlState && !consumed)
            {
                consumed = HandleControlStateOnTouch(e.Touch);
            }

            return consumed;
        }

        // Callback for View Hover signal
        private bool OnHoverEvent(IntPtr view, IntPtr hoverEvent)
        {
            if (hoverEvent == global::System.IntPtr.Zero)
            {
                NUILog.Error("hoverEvent should not be null!");
                return true;
            }

            HoverEventArgs e = new HoverEventArgs();

            e.Hover = Tizen.NUI.Hover.GetHoverFromPtr(hoverEvent);

            if (hoverEventHandler != null)
            {
                return hoverEventHandler(this, e);
            }
            return false;
        }

        // Callback for View Wheel signal
        private bool OnWheelEvent(IntPtr view, IntPtr wheelEvent)
        {
            if (wheelEvent == global::System.IntPtr.Zero)
            {
                NUILog.Error("wheelEvent should not be null!");
                return true;
            }

            WheelEventArgs e = new WheelEventArgs();

            e.Wheel = Tizen.NUI.Wheel.GetWheelFromPtr(wheelEvent);

            if (wheelEventHandler != null)
            {
                return wheelEventHandler(this, e);
            }
            return false;
        }

        // Callback for View OnWindow signal
        private void OnWindow(IntPtr data)
        {
            if (onWindowEventHandler != null)
            {
                onWindowEventHandler(this, null);
            }
        }

        // Callback for View OffWindow signal
        private void OffWindow(IntPtr data)
        {
            if (offWindowEventHandler != null)
            {
                offWindowEventHandler(this, null);
            }
        }

        // Callback for View visibility change signal
        private void OnVisibilityChanged(IntPtr data, bool visibility, VisibilityChangeType type)
        {
            VisibilityChangedEventArgs e = new VisibilityChangedEventArgs();
            if (data != null)
            {
                e.View = Registry.GetManagedBaseHandleFromNativePtr(data) as View;
            }
            e.Visibility = visibility;
            e.Type = type;

            if (visibilityChangedEventHandler != null)
            {
                visibilityChangedEventHandler(this, e);
            }
        }

        // Callback for View layout direction change signal
        private void OnLayoutDirectionChanged(IntPtr data, ViewLayoutDirectionType type)
        {
            LayoutDirectionChangedEventArgs e = new LayoutDirectionChangedEventArgs();
            if (data != null)
            {
                e.View = Registry.GetManagedBaseHandleFromNativePtr(data) as View;
            }
            e.Type = type;

            if (layoutDirectionChangedEventHandler != null)
            {
                layoutDirectionChangedEventHandler(this, e);
            }
        }

        private void OnResourcesLoaded(IntPtr view)
        {
            if (resourcesLoadedEventHandler != null)
            {
                resourcesLoadedEventHandler(this, null);
            }
        }

        private void OnBackgroundResourceLoaded(IntPtr view)
        {
            BackgroundResourceLoadedEventArgs e = new BackgroundResourceLoadedEventArgs();
            e.Status = (ResourceLoadingStatusType)Interop.View.GetVisualResourceStatus(this.SwigCPtr, Property.BACKGROUND);

            if (backgroundResourceLoadedEventHandler != null)
            {
                backgroundResourceLoadedEventHandler(this, e);
            }
        }

        /// <summary>
        /// Event argument passed through the ChildAdded event.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public class ChildAddedEventArgs : EventArgs
        {
            /// <summary>
            /// Added child view at moment.
            /// </summary>
            /// <since_tizen> 5 </since_tizen>
            public View Added { get; set; }
        }

        /// <summary>
        /// Event argument passed through the ChildRemoved event.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public class ChildRemovedEventArgs : EventArgs
        {
            /// <summary>
            /// Removed child view at moment.
            /// </summary>
            /// <since_tizen> 5 </since_tizen>
            public View Removed { get; set; }
        }

        /// <summary>
        /// Event arguments that passed via the KeyEvent signal.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public class KeyEventArgs : EventArgs
        {
            private Key _key;

            /// <summary>
            /// Key - is the key sent to the view.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public Key Key
            {
                get
                {
                    return _key;
                }
                set
                {
                    _key = value;
                }
            }
        }

        /// <summary>
        /// Event arguments that passed via the touch signal.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public class TouchEventArgs : EventArgs
        {
            private Touch _touch;

            /// <summary>
            /// Touch - contains the information of touch points.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public Touch Touch
            {
                get
                {
                    return _touch;
                }
                set
                {
                    _touch = value;
                }
            }
        }

        /// <summary>
        /// Event arguments that passed via the hover signal.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public class HoverEventArgs : EventArgs
        {
            private Hover _hover;

            /// <summary>
            /// Hover - contains touch points that represent the points that are currently being hovered or the points where a hover has stopped.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public Hover Hover
            {
                get
                {
                    return _hover;
                }
                set
                {
                    _hover = value;
                }
            }
        }

        /// <summary>
        /// Event arguments that passed via the wheel signal.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public class WheelEventArgs : EventArgs
        {
            private Wheel _wheel;

            /// <summary>
            /// WheelEvent - store a wheel rolling type: MOUSE_WHEEL or CUSTOM_WHEEL.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public Wheel Wheel
            {
                get
                {
                    return _wheel;
                }
                set
                {
                    _wheel = value;
                }
            }
        }

        /// <summary>
        /// Event arguments of visibility changed.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public class VisibilityChangedEventArgs : EventArgs
        {
            private View _view;
            private bool _visibility;
            private VisibilityChangeType _type;

            /// <summary>
            /// The view, or child of view, whose visibility has changed.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public View View
            {
                get
                {
                    return _view;
                }
                set
                {
                    _view = value;
                }
            }

            /// <summary>
            /// Whether the view is now visible or not.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public bool Visibility
            {
                get
                {
                    return _visibility;
                }
                set
                {
                    _visibility = value;
                }
            }

            /// <summary>
            /// Whether the view's visible property has changed or a parent's.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public VisibilityChangeType Type
            {
                get
                {
                    return _type;
                }
                set
                {
                    _type = value;
                }
            }
        }

        /// <summary>
        /// Event arguments of layout direction changed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public class LayoutDirectionChangedEventArgs : EventArgs
        {
            private View _view;
            private ViewLayoutDirectionType _type;

            /// <summary>
            /// The view, or child of view, whose layout direction has changed.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public View View
            {
                get
                {
                    return _view;
                }
                set
                {
                    _view = value;
                }
            }

            /// <summary>
            /// Whether the view's layout direction property has changed or a parent's.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public ViewLayoutDirectionType Type
            {
                get
                {
                    return _type;
                }
                set
                {
                    _type = value;
                }
            }
        }

        internal class BackgroundResourceLoadedEventArgs : EventArgs
        {
            private ResourceLoadingStatusType status = ResourceLoadingStatusType.Invalid;
            public ResourceLoadingStatusType Status
            {
                get
                {
                    return status;
                }
                set
                {
                    status = value;
                }
            }
        }

        /// <summary>
        /// The class represents the information of the situation where the View's control state changes.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public class ControlStateChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Create an instance with mandatory fields.
            /// </summary>
            /// <param name="previousState">The previous control state.</param>
            /// <param name="currentState">The current control state.</param>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public ControlStateChangedEventArgs(ControlState previousState, ControlState currentState)
            {
                PreviousState = previousState;
                CurrentState = currentState;
            }

            /// <summary>
            /// The previous control state.
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public ControlState PreviousState { get; }

            /// <summary>
            /// The current control state.
            /// </summary>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public ControlState CurrentState { get; }
        }

        private EventHandlerWithReturnType<object, WheelEventArgs, bool> WindowWheelEventHandler;
        private void OnWindowWheelEvent(object sender, Window.WheelEventArgs e)
        {
            if (e != null)
            {
                if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
                {
                    var arg = new WheelEventArgs()
                    {
                        Wheel = e.Wheel,
                    };
                    WindowWheelEventHandler?.Invoke(this, arg);
                }
            }
        }

        /// <summary>
        /// The expanded touch area.
        /// TouchArea can expand the view's touchable area.<br/>
        /// If you set the TouchAreaOffset on an view, when you touch the view, the touch area is used rather than the size of the view.<br/>
        /// </summary>
        /// <remarks>
        /// This is based on the top left x, y coordinates.<br/>
        /// For example) <br/>
        /// <code>
        ///  view.Size = new Size(100, 100);
        ///  view.TouchAreaOffset = new Offset(-10, 20, 30, -40); // left, right, bottom, top
        /// </code>
        /// Then, touch area is 130x170.<br/>
        /// This is view.width + TouchAreaOffset.right - TouchAreaOffset.left and view.height + TouchAreaOffset.bottom - TouchAreaOffset.top <br/>
        /// +---------------------+ <br/>
        /// |         ^           | <br/>
        /// |         |           | <br/>
        /// |         | -40       | <br/>
        /// |         |           | <br/>
        /// |         |           | <br/>
        /// |    +----+----+      | <br/>
        /// |    |         |      | <br/>
        /// | -10|         | 20   | <br/>
        /// |&lt;---+         +-----&gt;| <br/>
        /// |    |         |      | <br/>
        /// |    |         |      | <br/>
        /// |    +----+----+      | <br/>
        /// |         |           | <br/>
        /// |         | 30        | <br/>
        /// |         |           | <br/>
        /// |         v           | <br/>
        /// +---------------------+ <br/>
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Offset TouchAreaOffset
        {
            get
            {
                return (Offset)GetValue(TouchAreaOffsetProperty);
            }
            set
            {
                SetValue(TouchAreaOffsetProperty, value);
                NotifyPropertyChanged();
            }
        }

        private Offset InternalTouchAreaOffset
        {
            get
            {
                Interop.ActorInternal.GetTouchAreaOffset(SwigCPtr, out int left, out int right, out int bottom, out int top);
                if (NDalicPINVOKE.SWIGPendingException.Pending)
                    throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return new Offset(left, right, bottom, top);
            }
            set
            {
                Interop.ActorInternal.SetTouchAreaOffset(SwigCPtr, value.Left, value.Right, value.Bottom, value.Top);
                if (NDalicPINVOKE.SWIGPendingException.Pending)
                    throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

    }
}
