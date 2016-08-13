using System;

namespace SharpScript.Sandboxing
{
    public sealed class SandboxedCommand<T> : IDisposable where T : MarshalByRefObject
    {
        private AppDomain appDomain;
        private T value;

        public SandboxedCommand(AppDomain appDomain)
        {
            this.appDomain = appDomain;
            Type type = typeof(T);
            var handle = Activator.CreateInstanceFrom(
                appDomain,
                type.Assembly.ManifestModule.FullyQualifiedName,
                type.FullName);
            this.value = (T)handle.Unwrap();
        }

        public T Value
        {
            get { return this.value; }
        }

        public void Dispose()
        {
            if (this.appDomain != null)
            {
                AppDomain.Unload(this.appDomain);
                this.appDomain = null;
            }
        }
    }
}