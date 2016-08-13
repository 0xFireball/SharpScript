using Microsoft.CodeAnalysis.Scripting;
using System.Threading.Tasks;

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

        public void RunSync()
        {
            var task = RunAsync();
            task.Wait();
        }

        public T RunSync<T>()
        {
            var task = RunAsync<T>();
            task.Wait();
            return task.Result;
        }

        public async Task RunAsync()
        {
            var result = await _compiledRoslynScript.RunAsync(Engine.Globals);
        }

        public async Task<T> RunAsync<T>()
        {
            var result = await _compiledRoslynScript.RunAsync(Engine.Globals);
            return (T)result.ReturnValue;
        }

        //
        // Summary:
        //     The source code of the script.
        public string Code => _compiledRoslynScript.Code;
    }
}