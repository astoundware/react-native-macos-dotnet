using System;
using ObjCRuntime;

[assembly: LinkWith("libReactNative-macOS.a", LinkTarget = LinkTarget.Arm64 | LinkTarget.x86_64, LinkerFlags = "-lstdc++", SmartLink = true, ForceLoad = true)] 