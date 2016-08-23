using SharpScript.Sandboxing;
using System;

namespace SharpScript.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunMain();
        }

        private static async void RunMain()
        {
            var engine = new ScriptingEngine();

            Console.Write("Basic Arithmetic...");
            var addResult = await engine.EvaluateAsync<int>("1 + 2");
            TestUtil.WriteResult(TestUtil.Match(3, addResult));

            Console.Write("Testing variables...");
            engine.Globals.X = 1;
            engine.Globals.Y = 2;
            var variableAddResult = await engine.EvaluateAsync<int>("Globals.X + Globals.Y");
            TestUtil.WriteResult(TestUtil.Match(engine.Globals.X + engine.Globals.Y, variableAddResult));

            Console.Write("Testing precompiled script basic arithmetic...");
            var precompiledAddScript = engine.CompileScript("1 + 2");
            var precompiledAddResult = await precompiledAddScript.RunAsync<int>();
            TestUtil.WriteResult(TestUtil.Match(3, precompiledAddResult));

            Console.WriteLine("Testing passing objects to script...");
            engine.Globals.Creature = new Dog();
            await engine.RunAsync("var creature = Globals.Creature;\n//creature.Bark()");

            Console.WriteLine("Completed Scripting Engine tests");

            Console.Write("Testing Sandboxing...");

            var securityParams = new SandboxSecurityParameters();

            //securityParams.UseZoneSecurity = true; //This is very weak

            securityParams.AllowScripting();

            var scriptSandbox = new ScriptSandbox(securityParams, Guid.NewGuid().ToString("N"));
            Console.WriteLine("Sandbox Created.");

            Console.Write("Basic arithmetic precompiled script in sandbox...");
            var sandboxedPrecompileAddScript = scriptSandbox.SandboxedEngine.CompileScript("1 + 2");
            var sandboxedPrecompileAddScriptResult = sandboxedPrecompileAddScript.RunSync<int>();
            TestUtil.WriteResult(TestUtil.Match(3, sandboxedPrecompileAddScriptResult));

            /*
            Console.Write("Basic arithmetic in sandbox...");
            var sandboxedAddResult = scriptSandbox.SandboxedEngine.EvaluateSync<int>("1 + 2");
            TestUtil.WriteResult(TestUtil.Match(3, sandboxedAddResult));
            */

            Console.WriteLine("All tests completed");
            Console.ReadLine();
        }
    }
}