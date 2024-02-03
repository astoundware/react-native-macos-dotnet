using Astound.ReactNative.Bindings;
using Astound.ReactNative.Modules.ObjC;
using ObjCRuntime;

namespace Astound.ReactNative.macOS.Extensions
{
	class ReactFunctionsWrapper : IReactFunctions
	{
		public static ReactFunctionsWrapper Self { get; } = new ReactFunctionsWrapper();

		private ReactFunctionsWrapper()
		{

		}

		public void RegisterModule(string moduleName) => RCTFunctions.RCTRegisterModule(Class.GetHandle(moduleName));
	}
}
