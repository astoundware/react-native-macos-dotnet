using System.Reflection;
using AppKit;
using Astound.ReactNative.macOS.Bindings;
using Astound.ReactNative.Modules;
using Astound.ReactNative.Modules.ObjC;
using Foundation;

namespace Astound.ReactNative.macOS.Extensions
{
    public class ReactAppDelegate : NSApplicationDelegate, IRCTBridgeDelegate, IReactAppDelegate
    {
        public RCTBridge Bridge { get; private set; } = null!;

        protected virtual string BundleRoot => "index";
        protected virtual string FallbackExtension => "jsbundle";

        protected virtual IReactModuleRegistry ModuleRegistry { get; } =
            new ReactModuleRegistry(ReactFunctionsWrapper.Self);

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            RegisterModules(ModuleRegistry);

            Bridge = new RCTBridge(this, null);
        }

        public virtual void RegisterModules(IReactModuleRegistry registry)
        {
            registry.Register(Assembly.GetEntryAssembly()!);
        }

        public virtual NSUrl SourceURLForBridge(RCTBridge bridge)
        {
            var settings = RCTBundleURLProvider.SharedSettings();

            return settings.JsBundleURLForBundleRoot(BundleRoot, FallbackExtension);
        }
    }
}
