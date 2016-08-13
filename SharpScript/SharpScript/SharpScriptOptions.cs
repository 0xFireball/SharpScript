using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpScript
{
    public class SharpScriptOptions
    {
        public List<Assembly> ReferencedAssemblies { get; set; } = new List<Assembly>();
        public List<string> Imports { get; set; } = new List<string>();
    }
}
