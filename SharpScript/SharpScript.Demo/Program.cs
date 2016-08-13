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

            Console.WriteLine("Completed Scripting Engine tests");

            Console.Write("Testing Sandboxing...");

            var securityParams = new SandboxSecurityParameters();

            
            securityParams.UseZoneSecurity = true; //This is relatively weak

            securityParams.AllowScripting();

            var scriptSandbox = new ScriptSandbox(securityParams, Guid.NewGuid().ToString("N"));
            Console.WriteLine("Sandbox Created.");

            Console.Write("Basic arithmetic in sandbox...");
            var sandboxedAddResult = scriptSandbox.SandboxedEngine.EvaluateSync<int>("1 + 2");
            TestUtil.WriteResult(TestUtil.Match(3, variableAddResult));

            Console.WriteLine("All tests completed");
            Console.ReadLine();
        }
    }
}