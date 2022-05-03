# React Native for macOS on .NET
A framework for building native macOS apps with React and .NET. Use React Native to build your application as you normally would, but write your custom native code in .NET. The primary goal of this project is to make consuming [react-native-macos](https://github.com/microsoft/react-native-macos) in .NET as simple as possible.

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
