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
            var addResult = await engine.EvaluateStatelessAsync<int>("1 + 2");
            TestUtil.WriteResult(TestUtil.Match(3, addResult));

            Console.Write("DnsHostname...");
            var hostnameResult = await engine.EvaluateStatelessAsync<int>("1 + 2");
        }
    }
}
