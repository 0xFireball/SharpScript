using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace SharpScript
{
    public class ScriptingEngine
    {
        public ScriptingEngine()
        {

        }

        public async Task<T> EvaluateAsync<T>(string expression, ScriptOptions options = null, object globals = null, Type globalsType = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            return await CSharpScript.EvaluateAsync<T>(expression, options, globals, globalsType, cancellationToken);
        }

        public async Task<T> EvaluateAsync<T>(string expression)
        {
            return await CSharpScript.EvaluateAsync<T>(expression);
        }

        public ExpandoObject Globals;
    }
}