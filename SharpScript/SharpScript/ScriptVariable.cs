using System;
using System.Collections.Immutable;
using System.Linq;

namespace SharpScript
{
    /// <summary>
    /// A variable in the script
    /// </summary>
    public sealed class ScriptVariable
    {
        internal ScriptVariable(Microsoft.CodeAnalysis.Scripting.ScriptVariable roslynVariable)
        {
            _roslynVariable = roslynVariable;
        }

        /// <summary>
        /// Convert a Roslyn script variable collection to a SharpScript variable collection
        /// </summary>
        /// <returns></returns>
        internal static ImmutableArray<ScriptVariable> FromRoslynVariables(ImmutableArray<Microsoft.CodeAnalysis.Scripting.ScriptVariable> roslynVariables)
        {
            return roslynVariables.Select(roslynVariable => new ScriptVariable(roslynVariable)).ToImmutableArray();
        }

        private Microsoft.CodeAnalysis.Scripting.ScriptVariable _roslynVariable;

        //
        // Summary:
        //     True if the variable can't be written to (it's declared as readonly or a constant).
        public bool IsReadOnly { get; }

        //
        // Summary:
        //     The name of the variable.
        public string Name { get; }

        //
        // Summary:
        //     The type of the variable.
        public Type Type { get; }

        //
        // Summary:
        //     The value of the variable after running the script.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     Variable is read-only or a constant.
        //
        //   T:System.ArgumentException:
        //     The type of the specified value isn't assignable to the type of the variable.
        public object Value { get; set; }
    }
}