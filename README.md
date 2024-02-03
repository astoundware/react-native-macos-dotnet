# React Native for macOS + .NET
A framework for building native macOS apps with React and .NET. Use React Native to build your application as you normally would, but write your custom native code in .NET. The primary goal of this project is to make consuming [react-native-macos](https://github.com/microsoft/react-native-macos) in .NET as simple as possible.

## System Requirements
* Those identified by [react-native-macos](https://microsoft.github.io/react-native-windows/docs/rnm-dependencies)
* [.NET 8+ SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Getting Started
Follow the steps in [this guide](docs/getting-started.md) to create your first app using **React Native for macOS + .NET**.

## Troubleshooting
Should you run into any problems, please refer to [this guide](docs/troubleshooting.md) for common pitfalls.

## Third-Party Components
In addition to the built-in components provided by React Native, this extension includes some third-party components that you may find useful. Please refer to the documentation provided by each component for instructions on how to use them. Please note that any instructions related to CocoaPods or linking can be skipped.
* [@react-native-picker/picker](https://github.com/react-native-picker/picker/tree/v2.6.1)

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

### Setting Up
1. Run `yarn install` in the root of the repository.
1. Run `pod install` in the xcode directory.
1. Open the [Astound.ReactNative.macOS solution](dotnet/Astound.ReactNative.macOS.sln) in the dotnet directory.

### Building and Packaging
* Use `dotnet` CLI or your IDE to build the solution.
* Run `make` or `make pack` in the Astound.ReactNative.macOS.Bindings directory to build and package the bindings library.
* Run `make` or `make pack` in the Astound.ReactNative.macOS.Extensions directory to build and package the extensions library.

### Updating Dependencies
Follow these steps to target a newer version of React Native for macOS.
1. Identify the latest stable version on [this page](https://microsoft.github.io/react-native-windows/docs/rnm-getting-started).
1. Run `yarn add react-native@<version> react-native-macos@<version>`
1. Run `pod install` in the xcode directory.
1. Open [ReactNative-macOS.xcworkspace](xcode/ReactNative-macOS.xcworkspace) in the xcode directory.
1. Remove all Frameworks except JavaScriptCore from the ReactNative-macOS project.
1. Under _Build Phases > Link Binary With Libraries_, add all targets of the Pods project to the ReactNative-macOS target.

## License
This .NET extension, the underlying React Native for macOS extension, including modifications to the original Facebook source code, and all newly contributed code is provided under the [MIT License](LICENSE). The React Native for macOS extension is copyright Microsoft. Portions of the React Native for macOS extension derived from React Native are copyright Facebook.
