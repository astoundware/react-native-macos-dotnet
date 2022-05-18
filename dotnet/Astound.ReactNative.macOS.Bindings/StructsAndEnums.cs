using System;
using System.Runtime.InteropServices;

using Foundation;

namespace Astound.ReactNative.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RCTMethodInfo
    {
        public string jsName;
        public string objcName;
        public bool isSync;
    }

    public static class RCTFunctions
    {
        [DllImport("__Internal")]
        public static extern void RCTRegisterModule(IntPtr module);
    }
}
