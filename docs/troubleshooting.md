# Troubleshooting

Please see below for a list of possible problems and potential solutions.

* Building a project results in the following error:
    > Missing partial modifier on declaration of type 'T'; another partial declaration of this type exists (CS0260)
    * The specified module is not marked as a partial class.  Add `partial` to its class declaration.
        ```
        public partial class MyModule
        ```
* Building a project results in the following warning:
    > Generator 'ReactGenerator' failed to generate source. It will not contribute to the output and compilation errors may occur as a result. Exception was of type 'NotSupportedException' with message 'Type T is not supported' (CS8785)
    * One or more of the methods marked with the `ReactMethod` attribute contains an unsupported parameter or return type. Modify the method to use only the types indicated [here](supported-types.md).
* The app crashes at runtime and the following message is found in Application Output:
    > Terminating app due to uncaught exception 'NSInternalInconsistencyException', reason: '(null) does not conform to the RCTBridgeModule protocol'
    * One or more modules does not inherit from a supported base class. Modify the class declaration to inherit from either `RCTBridgeModule` or `ReactEventEmitterBase`.
        ```
        public partial class MyModule : RCTBridgeModule
        ```
* When debugging the app, the following alert is displayed:
    > '\<event\>' is not a supported event type for \<module\>. Supported events are:
    * A listener was added for an unsupported event in the JavaScript module. Either change the name to an event that is supported:
        ```
        const subscription = eventEmitter.addListener("doSomething", (message) =>
            console.log(message)
        );
        ```
        OR add the event to the array of SupportedEvents in the C# module:
        ```
        public override string[] SupportedEvents => new string[] { "doSomething" };
        ```
* When debugging the app, the app is blank and the Metro console contains the following error:
    > Invariant Violation: Native module cannot be null.
    * The module name referenced in JavaScript either does not exist or has not been registered. Ensure that the name used in JavaScript matches the one defined in C#. If those match, ensure that the module has been registered. By default, all modules in the application project (entry assembly) are registered. If the module exists in another project, it will need to be registered in your `AppDelegate` by overriding the `RegisterModules` method:
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
* When debugging the app, the app is blank and the Metro console contains the following error:
    > TypeError: _reactNative.NativeModules.\<module\>.\<function\> is not a function.
    * The function name referenced in JavaScript does not exist in your C# module. Either change the name to a method that does exist or add the method to the module.
* When debugging the app, the app is blank and the Metro console contains the following error:
    > Invariant Violation: "\<module\>" has not been registered.
    * The name provided for your JavaScript app does not match the one specified in your app.json file. Modify the value returned by the JsModuleName property in your `ViewController`:
        ```
        protected override string JsModuleName => "MyApp";
        ```
* When debugging the app, the app is blank and the Metro console contains the following error:
    > Error: EISDIR: illegal operation on a directory
    * Your JavaScript app likely does not contain the default entry point of index.js. In your `AppDelegate`, override the `BundleRoot` property to return the correct value:
        ```
        protected override string BundleRoot => "entry";
        ```
* The React packager (Metro) runs every time I build my project.
    * This is expected behavior. However, if you would like to disable this behavior, you may add the `EnableReactPackager` property to your project file.
        ```
        <PropertyGroup>
            <EnableReactPackager>false</EnableReactPackager>
        </PropertyGroup>
        ```
* My app is trying to connect to the React packager (Metro) when I want it to use the local bundle.
    * By default, the local bundle is only used for configurations named "Release". You can specify that another configuration should use the local bundle, by adding the `IsReactRelease` property to your project file.
        ```
        <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
            <IsReactRelease>true</IsReactRelease>
        </PropertyGroup>
        ```
