using System.Collections.Generic;
using System.Reflection;

namespace SharpScript
{
    public class SharpScriptOptions
    {
        public List<Assembly> ReferencedAssemblies { get; set; } = new List<Assembly>() {
            typeof(object).Assembly, // mscorlib
            typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException).Assembly, // Microsoft.CSharp
            typeof(Microsoft.CSharp.CSharpCodeProvider).Assembly, // System
            Assembly.GetAssembly(typeof(System.Dynamic.DynamicObject)),  // System.Core
        };

        public List<string> Imports { get; set; } = new List<string>();

        /// <summary>
        /// If this is true, the script will be able to access the engine in a variable called HostEngine. Be aware that this undermines any security measures, as the script can now access the engine or load arbitrary code.
        /// </summary>
        public bool PassEngineReference { get; set; }
    }
}