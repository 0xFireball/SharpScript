using Microsoft.CodeAnalysis.Scripting;

namespace SharpScript
{
    /// <summary>
    /// A compiled script. Use this when running code repeatedly to reduce compile overhead.
    /// </summary>
    public class CompiledScript
    {
        private Script<object> _compiledRoslynScript;
        public ScriptingEngine Engine { get; }

        public CompiledScript(Script<object> compiledScript, ScriptingEngine hostEngine)
        {
            this._compiledRoslynScript = compiledScript;
            Engine = hostEngine;
        }

        //
        // Summary:
        //     The source code of the script.
        public string Code => _compiledRoslynScript.Code;
    }
}