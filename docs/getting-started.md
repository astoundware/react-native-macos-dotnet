# Getting Started
## Creating an App
As suggested by its name, the `Astound.ReactNative.macOS.Extensions` package is really just an extension of the great work done by Facebook/Meta and Microsoft. Because of this, the very first step is to use their tools to create a basic app. You can do so by following [Microsoft's instructions](https://microsoft.github.io/react-native-windows/docs/0.71/rnm-getting-started).

### Creating a Project
Once that has been completed, you should have folders for each platform along with your app's JavaScript code. Since **React Native for macOS + .NET** enables connectivity with .NET on _macOS_, the next set of steps replace the existing macOS-specific Xcode project with a .NET project.
1. Empty (or recreate) the macos folder.
    > Note: You may also remove the android and/or ios folders if you are not developing for those platforms.
1. Navigate to the macos folder.
    ```
    cd macos
    ```
1. Install the macos workload.
    ```
    sudo dotnet workload install macos
    ```
1. Create a new project.
   ```
   dotnet new macos -n <projectName>
   ```
1. Navigate to the new project folder.
   ```
   cd <projectName>
   ```

### Installing the NuGet Package
Now that we have a basic macOS app, we're going to modify it to call React Native. This is where the `Astound.ReactNative.macOS.Extensions` package comes in. Use the `dotnet` CLI to install it.
```
dotnet add package Astound.ReactNative.macOS.Extensions -v 0.71
```

### Modifying the AppDelegate
The `Astound.ReactNative.macOS.Extensions` package provides a pre-configured `AppDelegate` class called `ReactAppDelegate`. This class creates a React bridge and tells it where to access the JavaScript. Open your `AppDelegate.cs` file and perform the following steps to use `ReactAppDelegate`.
1. Add `using Astound.ReactNative.macOS.Extensions;`.
1. Change the class declaration to inherit `ReactAppDelegate` instead of `NSApplicationDelegate`.
    ```
    public class AppDelegate : ReactAppDelegate
    ```
By default, `ReactAppDelegate` expects a JavaScript file called `index.js`. If this does not match your app's configuration, you can modify it by overriding the `BundleRoot` property.
```
protected override string BundleRoot => "entry";
```
`ReactAppDelegate` also registers your C# modules for use in JavaScript. We'll go into the details of creating a C# module later. At some point, you'll need to decide where to place that code. By default, `ReactAppDelegate` finds and registers all modules in the main Application project. If you decide to place them in a different project, you will want to override the `RegisterModules` method.
```
public override void RegisterModules(Astound.ReactNative.Modules.IReactModuleRegistry registry)
{
    base.RegisterModules(registry);
    registry.Register(typeof(MyOtherModule).Assembly);
}
```
The above code will register all modules in the same assembly/project as the specified type. If you only want to register a specific module, you can change it to the following.
```
registry.Register(typeof(MyOtherModule));
```

### Modiyfing the ViewController
Now that we have a working `AppDelegate`, we want to modify the `ViewController`. `Astound.ReactNative.macOS.Extensions` makes this easy through the use of a base class called `ReactViewControllerBase`. Open `ViewController.cs` and perform the following steps to use it.
1. Add `using Astound.ReactNative.macOS.Extensions;`.
1. Change the class declaration to inherit `ReactViewControllerBase` instead of `NSViewController`.
    ```
    public partial class ViewController : ReactViewControllerBase
    ```
`ReactViewControllerBase` uses the React bridge created by `ReactAppDelegate` to create a root React view. You just need to tell it the name that you used when you executed the `npx react-native init` command. This name can also be found in a file called `app.json`. To tell `ReactViewControllerBase` the name, implement the abstract `JsModuleName` property.
```
protected override string JsModuleName => "MyReactNativeApp";
```

### Running the App
Congratulations! At this point, you should have a working React Native app. Build and run it to enjoy the fruits of your labor. If you use the Debug configuration, you should see Metro, the React packager, launch and your app connect to it. By default, Metro will reload your app when you make changes to the JavaScript. If you use the Release configuration instead, a JS bundle file should be created and placed in your macOS app bundle. In the next section, we'll detail the steps required to create a native (C#) module.
```
dotnet run -c <Configuration>
```

## Creating a Native Module
You probably wouldn't be here if you didn't want to call some C# code in your React app. This section will walk you through the steps to create a native module that can be called from JavaScript.

### Creating a New Class
First, you will want to add a new class to whichever project you want to keep your modules in. This class will need to be made a `partial` class so that a source generator can come along later and add boilerplate code that is required by React Native. It will also need to inherit from one of two classes. If you only want to call C# code from JavaScript, you would inherit `RCTBridgeModule`, a class provided by React Native itself. If you also want to call JavaScript code from C#, you would inherit from `ReactEventEmitterBase`, a class provided by `Astound.ReactNative.macOS.Extensions`, instead. Perform the following steps to inherit from `RCTBrideModule`.
1. Add `using Astound.ReactNative.macOS.Bindings;`.
1. Add `partial` to the class declaration and inherit `RCTBridgeModule`.
    ```
    public class MyModule : RCTBridgeModule
    ```
Perform the following steps to inherit from `ReactEventEmitterBase` instead. Since `ReactEventEmitterBase` is an abstract class, you will need to also implement an abstract property called `SupportedEvents`. This property should return an array containing all events you plan to emit, or send. Later, you will add listeners to these events in your JavaScript code. _Again, this is only necessary if you want to call JavaScript code from C#._
1. Add `using Astound.ReactNative.macOS.Extensions;`.
1. Add `partial` to the class declaration and inherit `ReactEventEmitterBase`.
    ```
    public class MyModule : ReactEventEmitterBase
    ```
1. Implement the `SupportedEvents` property.
    ```
    public override string[] SupportedEvents => new string[] { "doSomethingInJs" };
    ```

### Using the ReactModule Attribute
Next, you need to add an attribute to your class declaration. This attribute tells the source generator to add the boilerplate we discussed earlier and allows the class to be found when registering a whole assembly of modules in the previous steps.
```
[ReactModule]
public partial class MyModule
```
This code will register your module as "MyModule". That is the name you will use in JavaScript. If you want to use a different name, you can specify it in the `ReactModule` constructor.
```
[ReactModule("CustomModuleName")]
```

### Using the ReactMethod Attribute
Your module won't be complete without some methods to call! For that, you would add methods as you normally would to any C# class. For every method that you want to call from JavaScript, you will add a `ReactMethod` attribute.
```
[ReactMethod]
public string DoSomething(int value)
```
This attribute behaves similarly to `ReactModule`. If you want to use the C# method name directly in JavaScript, don't pass any arguments. If you want to give it a different name, say in camel case, specify it in the constructor.
```
[ReactMethod("doSomethingInCSharp")]
```

### Calling a C# Module from JavaScript
Calling a C# module from JavaScript is done the same way you normally would with React Native. Follow the steps [here](https://reactnative.dev/docs/native-modules-ios#test-what-you-have-built) to do that.

### Calling JavaScript from a C# Module
Now, if you also want to call JavaScript code from C#, you will need to add some code for that. On the C# side, you will call the `EmitEvent` method provided by `ReactNativeEventEmitterBase`. This method takes two parameters: (1) the event name and (2) a body or payload to send to the listener.
```
base.EmitEvent("doSomethingInJs", "It works!");
```
On the JavaScript side, you will add a listener for the event that calls a function of your choice. First, import `NativeEventEmitter` and `NativeModules` if you haven't already.
```
import {NativeEventEmitter, NativeModules} from 'react-native';
```
Next, instantiate a `NativeEventEmitter` and add a listener to it.
```
const eventEmitter = new NativeEventEmitter(NativeModules.MyModule);
const listener = eventEmitter.addListener("doSomethingInJs", (message) =>
    console.log(message)
);
```
You will likely want to call the above code in a `useEffect` hook. Information on that can be found [here](https://reactjs.org/docs/hooks-effect.html). Also, don't forget to remove the listener when it is no longer needed to prevent memory leaks. You will likely want to place that code in the cleanup function that you pass to `useEffect`.
```
listener.remove()
```
That's it! You can now build and run your code again to communicate between C# and JavaScript!
