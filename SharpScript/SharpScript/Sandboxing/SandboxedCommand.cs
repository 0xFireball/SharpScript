using System;

namespace SharpScript.Sandboxing
{
    public sealed class SandboxedCommand<T> : IDisposable where T : MarshalByRefObject
    {
        private AppDomain _sandboxedAppDomain;
        private T _value;

        public SandboxedCommand(AppDomain sandboxedAppDomain)
        {
            _sandboxedAppDomain = sandboxedAppDomain;

            Type type = typeof(T);

            //_value = (T)sandboxedAppDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);

            _value = (T)Activator.CreateInstance(sandboxedAppDomain, type.Assembly.FullName, type.FullName).Unwrap();
            
            /*
            var handle = Activator.CreateInstanceFrom(
                _sandboxedAppDomain,
                type.Assembly.ManifestModule.FullyQualifiedName,
                type.FullName);
            _value = (T)handle.Unwrap();
            */            
        }

        public T Value
        {
            get { return _value; }
        }

        public void Dispose()
        {
            if (_sandboxedAppDomain != null)
            {
                AppDomain.Unload(_sandboxedAppDomain);
                _sandboxedAppDomain = null;
            }
        }
    }
}