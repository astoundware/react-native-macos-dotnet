﻿using System;
using System.Diagnostics;
using AppKit;
using Astound.ReactNative.macOS.Bindings;

namespace Astound.ReactNative.macOS.Extensions
{
    public abstract class ReactViewControllerBase : NSViewController
    {
        protected RCTRootView _rootView = null!;

        protected abstract string JsModuleName { get; }

        public ReactViewControllerBase(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (NSApplication.SharedApplication.Delegate is not IReactAppDelegate appDelegate)
            {
                Trace.WriteLine("Cannot initialize root view because app delegate is not an IReactAppDelegate");
                return;
            }

            _rootView = new RCTRootView(appDelegate.Bridge, JsModuleName, null);

            View.AddSubview(_rootView);

            _rootView.SetBackgroundColor(NSColor.WindowBackground);
            _rootView.Frame = View.Bounds;
            _rootView.AutoresizingMask =
                NSViewResizingMask.MinXMargin |
                NSViewResizingMask.MaxXMargin |
                NSViewResizingMask.MinYMargin |
                NSViewResizingMask.MaxYMargin |
                NSViewResizingMask.WidthSizable |
                NSViewResizingMask.HeightSizable;
        }
    }
}
