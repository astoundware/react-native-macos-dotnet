# React Native for macOS on .NET
A framework for building native macOS apps with React and .NET. Use React Native to build your application as you normally would, but write your custom native code in .NET. The primary goal of this project is to make consuming [react-native-macos](https://github.com/microsoft/react-native-macos) in .NET as simple as possible.

## System Requirements
* Those identified by [react-native-macos](https://microsoft.github.io/react-native-windows/docs/rnm-dependencies)
* [.NET 6+ SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Xamarin.Mac](https://docs.microsoft.com/en-us/xamarin/mac/get-started/)

## Getting Started
### Creating an App
1. Follow the steps in [this guide](https://microsoft.github.io/react-native-windows/docs/0.64/rnm-getting-started) to create a React Native for macOS app
1. Empty (or recreate) the macos folder
    > Note: You may also remove the android and/or ios folders if you are not developing for those platforms
1. Create a new Mac Cocoa App in the macos folder
    1. Target macOS Mojave 10.14 or higher
1. Install the Astound.ReactNative.macOS.Extensions NuGet package
1. Edit AppDelegate.cs
    1. Add `using Astound.ReactNative.macOS.Extensions;`
    1. Change the class declaration to inherit from `ReactAppDelegate` instead of `NSApplicationDelegate`
        ```
        public class AppDelegate : ReactAppDelegate
        ```
1. Edit ViewController.cs
    1. Add `using Astound.ReactNative.macOS.Extensions;`
    1. Change the class declaration to inherit from `ReactViewControllerBase` instead of `NSViewController`
        ```
        public partial class ViewController : ReactViewControllerBase
        ```
    1. Implement the abstract `JsModuleName` property by returning the name specified for your project in the `npx react-native init` command
        ```
        protected override string JsModuleName => "MyReactNativeApp";
        ```
1. Build and run the app!

### Creating a Native Module
Follow the steps below to create a native module that can be called from JavaScript.
1. Add a class to your Cocoa App project
1. Add using statements for the following namespaces:
    * `Astound.ReactNative.macOS.Bindings`
    * `Astound.ReactNative.Modules`
1. Modify the class declaration to be a `partial` class and inherit `RCTBridgeModule`
    ```
    public partial class MyModule : RCTBridgeModule
    ```
1. Add the `ReactModule` attribute above the class declaration
    ```
    [ReactModule]
	public partial class MyModule
    ```
    > Note: If you want to call the class something else from JavaScript, specify a name in the attribute constructor
    > ```
    > [ReactModule("DotnetModule")]
    > ```
1. For every method that you want to call from JavaScript, add a `ReactMethod` attribute
    ```
    [ReactMethod("doSomething")]
	public string DoSomething(int value)
    ```
    > Note: The following types are supported for parameters and return values:
    > * bool
    > * Most numeric types (double, float, int, uint, nint, nuint, long, ulong, short, ushort)
    > * string
    > * Single-dimensional arrays of the above types
    > * Dictionaries of the above types
    1. If you decide to create this or other modules in a separate project, be sure to register them in your `AppDelegate` class by overriding the `RegisterModules` method:
        ```
        public override void RegisterModules(Astound.ReactNative.Modules.IReactModuleRegistry registry)
        {
            base.RegisterModules(registry);
            // register a single module
			registry.Register(typeof(MyOtherModule));
            // OR register all modules in an assembly
            registry.Register(typeof(MyOtherModule).Assembly);
        }
        ```
1. Follow the steps [here](https://reactnative.dev/docs/native-modules-ios#test-what-you-have-built) to call the module from JavaScript

Follow these additional steps to call a JavaScript function from your native module.
1. Add a `using` statement for the `Astound.ReactNative.macOS.Extensions` namespace
1. Modify the class declaration to inherit `ReactEventEmitterBase` instead
    ```
    public partial class MyModule : ReactEventEmitterBase
    ```
1. Override the `SupportedEvents` method by returning the event names that your JavaScript module will support
    ```
    public override string[] SupportedEvents => new string[] { "doSomethingElse" };
    ```
1. Call your event somewhere
    ```
    base.EmitEvent("doSomethingElse", "It works!");
    ```
    > Note: While `EmitEvent` takes an `object` for the `body` parameter, only the following types are supported:
    > * bool
    > * Most numeric types (double, float, int, uint, nint, nuint, long, ulong, short, ushort)
    > * string
    > * Single-dimensional arrays of the above types
    > * Dictionaries of the above types
1. Add a listener for the event to your Javascript module
    ```
    import {NativeEventEmitter, NativeModules} from 'react-native';

    const eventEmitter = new NativeEventEmitter(NativeModules.MyModule);
    const subscription = eventEmitter.addListener("doSomethingElse", (message) =>
        console.log(message)
    );
    ```
    > Note: Be sure to call `subscription.remove()` when the listener is no longer needed to prevent memory leaks

## Contributing
Most contributions are welcome, but those not meeting the project's goals or standards may be rejected.

This project makes us of [Git LFS](https://git-lfs.github.com/) for storing binary files.  Please ensure that this is installed before cloning.

To begin, create a branch from `main` for the issue you are working on.  Please use the following naming convention.
> \<feature|bugfix\>/\<issue-number\>-\<short-description\>

If an issue does not exist for the improvement you would like to make, please create one.  Once work is complete, create a pull request to have the branch merged into `main`.

### Requirements
* [Git LFS](https://git-lfs.github.com/)
* [Node.js](https://nodejs.org/)
* [Yarn](https://yarnpkg.com/)
* [CocoaPods](https://cocoapods.org/)
* [Xamarin.Mac](https://docs.microsoft.com/en-us/xamarin/mac/)

### Getting Started
1. Run `yarn install` in the root of the repository.
1. Run `pod install` in the xcode directory.
1. Open the [Astound.ReactNative.macOS solution](dotnet/Astound.ReactNative.macOS.sln) in the dotnet directory.

### Building and Packaging
* Use MSBuild or Visual Studio to build the solution.
    * I have not had success building it with `dotnet build`.
* Run `make` or `make pack` in the Astound.ReactNative.macOS.Bindings directory to build and package the bindings library.

## License
This .NET extension, the underlying React Native for macOS extension, including modifications to the original Facebook source code, and all newly contributed code is provided under the [MIT License](LICENSE). The React Native for macOS extension is copyright Microsoft. Portions of the React Native for macOS extension derived from React Native are copyright Facebook.
