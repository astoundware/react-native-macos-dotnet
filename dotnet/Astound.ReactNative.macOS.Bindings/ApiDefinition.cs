using System;

using AppKit;
using Foundation;
using ObjCRuntime;

namespace Astound.ReactNative.macOS.Bindings
{
    // @interface RCTBridge : NSObject <RCTInvalidating>
    [BaseType(typeof(NSObject))]
    interface RCTBridge
    {
        // -(instancetype)initWithDelegate:(id<RCTBridgeDelegate>)delegate launchOptions:(NSDictionary *)launchOptions;
        [Export("initWithDelegate:launchOptions:")]
        IntPtr Constructor(NSObject @delegate, [NullAllowed] NSDictionary launchOptions);
    }

    // @interface RCTBundleURLProvider : NSObject
    [BaseType(typeof(NSObject))]
    interface RCTBundleURLProvider
    {
        // +(instancetype)sharedSettings;
        [Static]
        [Export("sharedSettings")]
        RCTBundleURLProvider SharedSettings();
        
        // -(NSURL *)jsBundleURLForSplitBundleRoot:(NSString *)bundleRoot;
        [Export("jsBundleURLForBundleRoot:")]
        NSUrl JsBundleURLForBundleRoot(string bundleRoot);

        // -(NSURL *)jsBundleURLForBundleRoot:(NSString *)bundleRoot fallbackExtension:(NSString *)extension;
        [Export("jsBundleURLForBundleRoot:fallbackExtension:")]
        NSUrl JsBundleURLForBundleRoot(string bundleRoot, [NullAllowed] string fallbackExtension);
    }

    // @interface RCTRootView : RCTUIView
    [BaseType(typeof(RCTUIView))]
    interface RCTRootView
    {
        // -(instancetype)initWithBundleURL:(NSURL *)bundleURL moduleName:(NSString *)moduleName initialProperties:(NSDictionary *)initialProperties launchOptions:(NSDictionary *)launchOptions;
        [Export("initWithBundleURL:moduleName:initialProperties:launchOptions:")]
        IntPtr Constructor(NSUrl bundleURL, string moduleName, [NullAllowed] NSDictionary initialProperties, [NullAllowed] NSDictionary launchOptions);

        // -(instancetype)initWithBridge:(RCTBridge *)bridge moduleName:(NSString*) moduleName initialProperties:(NSDictionary*) initialProperties
        [Export("initWithBridge:moduleName:initialProperties:")]
        IntPtr Constructor(RCTBridge bridge, string moduleName, [NullAllowed] NSDictionary initialProperties);
    }

    // @interface RCTUIView : NSView
    [BaseType(typeof(NSView))]
    interface RCTUIView
    {
        // -(void)setBackgroundColor:(NSColor *)backgroundColor
        [Export("setBackgroundColor:")]
        void SetBackgroundColor(NSColor backgroundColor);
    }

    // @protocol RCTBridgeModule <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCTBridgeModule
    {

    }

    // @protocol RCTBridgeDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface RCTBridgeDelegate
    {
        // -(NSURL *)sourceURLForBridge:(RCTBridge *)bridge;
        [Export("sourceURLForBridge:")]
        [Abstract]
        NSUrl SourceURLForBridge(RCTBridge bridge);
    }

    // @interface RCTEventEmitter : NSObject <RCTBridgeModule, RCTJSInvokerModule, RCTInvalidating>
    [BaseType(typeof(RCTBridgeModule))]
    interface RCTEventEmitter
    {
        // -(id)supportedEvents;
        [Export("supportedEvents")]
        [Abstract]
        string[] SupportedEvents { get; }

        // -(void)sendEventWithName:(NSString *_Nullable)name body:(id _Nullable )body;
        [Export("sendEventWithName:body:")]
        void SendEventWithName(string name, [NullAllowed] NSObject body);

        // -(void)startObserving;
        [Export("startObserving")]
        void StartObserving();

        // -(void)stopObserving;
        [Export("stopObserving")]
        void StopObserving();
    }
}

