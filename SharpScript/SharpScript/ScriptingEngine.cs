using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Immutable;
using System.Dynamic;
using System.Reflection;
using System.Threading.Tasks;

namespace SharpScript
{
    public class ScriptingEngine : MarshalByRefObject
    {
        public ScriptingEngine() : this(new SharpScriptOptions())
        {
        }

        public ScriptingEngine(SharpScriptOptions scriptOptions)
        {
            InitializeEngine(scriptOptions);
        }

        private async void InitializeEngine(SharpScriptOptions scriptOptions)
        {
            EngineOptions = scriptOptions;
            EngineState = await CSharpScript.RunAsync<object>("", _roslynScriptOptions, _roslynScriptGlobals);
            _roslynScriptOptions = ScriptOptions.Default
                .AddReferences(EngineOptions.ReferencedAssemblies)
                .AddImports(EngineOptions.Imports);

            if (EngineOptions.PassEngineReference)
            {
                Globals.HostEngine = this;
            }
        }

        /// <summary>
        /// Adds the assembly to the referenced assemblies available to the script.
        /// </summary>
        /// <param name="asm"></param>
        public void AddAssemblyReference(Assembly asm)
        {
            _roslynScriptOptions.AddReferences(asm);
        }

        /// <summary>
        /// Run the script, preserving the state of the engine after execution
        /// </summary>
        /// <param name="scriptCode"></param>
        /// <returns></returns>
        public async Task<bool> RunAsync<T>(string scriptCode)
        {
            var afterExecutionState = await EngineState.ContinueWithAsync<T>(scriptCode, _roslynScriptOptions);
            EngineState = afterExecutionState;
            return true;
        }

        /// <summary>
        /// Statelessly (state is not changed) evaluates the code and returns a result. To preserve state, use RunAsync instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> EvaluateAsync<T>(string expression)
        {
            return await CSharpScript.EvaluateAsync<T>(expression, _roslynScriptOptions, _roslynScriptGlobals);
        }

        public CompiledScript CompileScript(string scriptCode)
        {
            var compiledScript = CSharpScript.Create<object>(scriptCode, _roslynScriptOptions);
            return new CompiledScript(compiledScript, this);
        }

        /*
        public async Task<T> EvaluateAsync<T>(string expression, ScriptOptions options = null, object globals = null, Type globalsType = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return await CSharpScript.EvaluateAsync<T>(expression, options, globals, globalsType, cancellationToken);
        }

        public async Task<T> EvaluateAsync<T>(string expression, SharpScriptOptions scriptOptions, ExpandoObject globals)
        {
            return await CSharpScript.EvaluateAsync<T>(expression, options, globals, globalsType, cancellationToken);
        }

        public async Task<T> EvaluateAsync<T>(string expression)
        {
            return await CSharpScript.EvaluateAsync<T>(expression);
        }
        */
        public dynamic Globals { get; set; } = new ExpandoObject();
        public SharpScriptOptions EngineOptions { get; set; }
        public ScriptState EngineState { get; set; }
        public ImmutableArray<Microsoft.CodeAnalysis.Scripting.ScriptVariable> Variables => EngineState.Variables;

        private ScriptOptions _roslynScriptOptions;
        private ScriptGlobals _roslynScriptGlobals => new ScriptGlobals() { Globals = Globals };
    }
}