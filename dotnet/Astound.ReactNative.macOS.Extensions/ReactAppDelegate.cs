using AppKit;
using Astound.ReactNative.macOS.Bindings;
using Foundation;

namespace Astound.ReactNative.macOS.Extensions
{
    public class ReactAppDelegate : NSApplicationDelegate, IRCTBridgeDelegate, IReactAppDelegate
    {
        public RCTBridge Bridge { get; private set; }

        protected virtual string BundleRoot => "index";
        protected virtual string FallbackResourceName => "main";

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Bridge = new RCTBridge(this, null);
        }

        public virtual NSUrl SourceURLForBridge(RCTBridge bridge)
        {
            var settings = RCTBundleURLProvider.SharedSettings();

            return settings.JsBundleURLForBundleRoot(BundleRoot, FallbackResourceName);
        }
    }
}
