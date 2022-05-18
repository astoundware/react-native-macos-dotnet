# Getting Started
## Creating an App
As suggested by its name, the `Astound.ReactNative.macOS.Extensions` package is really just an extension of the great work done by Facebook/Meta and Microsoft. Because of this, the very first step is to use their tools to create a basic app. You can do so by following [Microsoft's instructions](https://microsoft.github.io/react-native-windows/docs/0.64/rnm-getting-started).

Once that is completed, you should now have folders for each platform along with your app's JavaScript code. Since this project enables connectivity with .NET on _macOS_, the next set of steps replace the existing macOS-specific code with .NET code.
1. Empty (or recreate) the macos folder
    > Note: You may also remove the android and/or ios folders if you are not developing for those platforms
1. Use _Visual Studio for Mac_ to create a new Mac Cocoa App in the macos folder
    ![Cocoa App Project](images/cocoa-app-project.png)
    1. Target macOS Mojave 10.14 or higher
        ![Configure Mac App](images/configure-mac-app.png)

Now that we have a basic Xamarin macOS app, we're going to modify it to call React Native. This is where the `Astound.ReactNative.macOS.Extensions` package comes in. Select your project and use _Project>Manage NuGet Packages..._ to install it.
![Manage NuGet Packages](images/manage-nuget-packages.png)

The `Astound.ReactNative.macOS.Extensions` package provides a pre-configured `AppDelegate` class called `ReactAppDelegate`. This class creates a React bridge and tells it where to access the JavaScript. Open your `AppDelegate.cs` file and perform the following steps to use `ReactAppDelegate`.
1. Add `using Astound.ReactNative.macOS.Extensions;`
1. Change the class declaration to inherit from `ReactAppDelegate` instead of `NSApplicationDelegate`
    ```
    public class AppDelegate : ReactAppDelegate
    ```
By default, `ReactAppDelegate` expects a JavaScript file called `index.js`. If this does not match your app's configuration, you can modify it by overriding the `BundleRoot` property.
```
protected override string BundleRoot => "entry";
```
`ReactAppDelegate` also registers your C# modules for use in JavaScript. We'll go into the details of that later. At some point, you'll need to decide where to place that code. By default, `ReactAppDelegate` finds and registers all modules in the main Cocoa App project. If you decide to place them in a different project, you will want to override the `RegisterModules` method.
```
public override void RegisterModules(Astound.ReactNative.Modules.IReactModuleRegistry registry)
{
    base.RegisterModules(registry);
    registry.Register(typeof(MyOtherModule).Assembly);
}
```
The above code will register all types in the same assembly/project as the specified type. If you only want to register a specific type, you can change it to the following.
```
registry.Register(typeof(MyOtherModule));
```

Now that we have a working `AppDelegate`, we want to modify the `ViewController`. `Astound.ReactNative.macOS.Extensions` makes this easy through the use of base class called `ReactViewControllerBase`. Open `ViewController.cs` and perform the following steps to use it.
1. Add `using Astound.ReactNative.macOS.Extensions;`
1. Change the class declaration to inherit from `ReactViewControllerBase` instead of `NSViewController`
    ```
    public partial class ViewController : ReactViewControllerBase
    ```
`ReactViewControllerBase` uses the React bridge created by `ReactAppDelegate` to create a root React view. You just need to tell it the name that you used when you executed the `npx react-native init` command.  To do so, implement the abstract `JsModuleName` property.
```
protected override string JsModuleName => "MyReactNativeApp";
```

Congratulations! At this point, you should have a working React Native app. Build and run it to enjoy the fruits of your labor. In the next section, we'll detail the steps required to create a native (C#) module.

## Creating a Native Module
You probably wouldn't be here if you didn't want to call some C# code in your React app. This section will walk you through the steps to create a native module that can be called from JavaScript.

First, you will want to add a new class to whichever project you want to keep your modules in. Open that class and add the following using statements.
```
using Astound.ReactNative.macOS.Bindings;
using Astound.ReactNative.Modules;
```
The `Bindings` namespace contains the actual React Native code that we need to interface with. The `Modules` namespace contains the basic framework for declaring and registering your modules in .NET.

Second, you will want to make your class a `partial` class. This is done so that a source generator can add boilerplate code that is required by React Native to the class. You also need to inherit one of two classes. If you only want to call C# code from JavaScript, you would inherit `RCTBridgeModule`.
```
public partial class MyModule : RCTBridgeModule
```

_If you also want to call JavaScript code from C#_, you would instead inherit `ReactEventEmitterBase`.
```
public partial class MyModule : ReactEventEmitterBase
```
Doing that will require one additonal change to get your code to compile. `ReactEventEmitterBase` is an `abstract` class that requires a `SupportedEvents` property to be implemented. Later, you will add listeners to these events in your JavaScript code. The `SupportedEvents` property should return an array containing all events you plan to emit, or send.
```
public override string[] SupportedEvents => new string[] { "doSomethingInJs" };
```

Next, you need to add an attribute to your class declaration. This attribute tells the source generator to add the boilerplate we discussed earlier and allows the class to be found when registering a whole assembly of modules in the previous steps.
```
[ReactModule]
public partial class MyModule
```
This code will register your module as "MyModule". That is the name you will use in JavaScript. If you want to use a different name, you can specify it in the `ReactModule` constructor.
```
[ReactModule("CustomModuleName")]
```

Your module won't be complete without some methods to call! For every method that you want to call from JavaScript, add a `ReactMethod` attribute.
```
[ReactMethod]
public string DoSomething(int value)
```
This attribute behaves similarly to `ReactModule`. If you want to use the C# method name directly in JavaScript, don't pass any arguments. If you want to give it a different name, say in camel case, specify it in the constructor.
```
[ReactMethod("doSomethingInCSharp")]
```

Calling a C# module from JavaScript is done the same way you normally would with React Native. Follow the steps [here](https://reactnative.dev/docs/native-modules-ios#test-what-you-have-built) to do that.

Now, if you also want to call JavaScript code from C#, you will need to add some code for that. On the C# side, you will want to call the `EmitEvent` method provided by `ReactNativeEventEmitterBase`.
```
base.EmitEvent("doSomethingInJs", "It works!");
```
On the JavaScript side, you will add a listener for that event that calls a function of your choice.
```
import {NativeEventEmitter, NativeModules} from 'react-native';

const eventEmitter = new NativeEventEmitter(NativeModules.MyModule);
const subscription = eventEmitter.addListener("doSomethingInJs", (message) =>
    console.log(message)
);
```
You will likely want to call the above code in a `useEffect` hook. Don't forget to also remove the listener when it is no longer needed to prevent memory leaks. You will likely want to place the below code in the cleanup function that you pass to `useEffect`. Information on both can be found [here](https://reactjs.org/docs/hooks-effect.html).
```
subscription.remove()
```

You can now build and run your code again to communicate between C# and JavaScript!
