using System;
using System.Diagnostics;
using Astound.ReactNative.macOS.Bindings;

namespace Astound.ReactNative.macOS.Extensions
{
    public abstract class ReactEventEmitterBase : RCTEventEmitter
	{
		public virtual bool HasListeners { get; protected set; }

        public abstract override string[] SupportedEvents { get; }

        public override void StartObserving()
        {
            HasListeners = true;
        }

        public override void StopObserving()
        {
            HasListeners = false;
        }

        public virtual void EmitEvent(string name, object body = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!HasListeners)
            {
                Trace.WriteLine($"Event '{name}' was not emitted because the emitter has no listeners. " +
                    "Confirm that addListener was called from JavaScript prior to emitting the event.");

                return;
            }

            SendEventWithName(name, body != null ? FromObject(body) : null);
        }
    }
}

