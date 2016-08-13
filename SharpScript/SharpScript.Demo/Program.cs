using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpScript.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunMain();
        }

        static async void RunMain()
        {
            var engine = new ScriptingEngine();

            Console.Write("Basic Arithmetic...");
            var addResult = await engine.EvaluateAsync<int>("1 + 2");
            TestUtil.WriteResult(TestUtil.Match(3, addResult));

            Console.Write("Testing variables...");
            engine.Globals["X"] = 1;
            engine.Globals["Y"] = 2;
            var variableAddResult = await engine.EvaluateAsync<int>("Globals[\"X\"] + Globals[\"Y\"]");
            TestUtil.WriteResult(TestUtil.Match(3, variableAddResult));

            Console.WriteLine("All tests completed");
            Console.ReadLine();
        }
    }
}
