using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpScript.Demo
{
    class TestUtil
    {
        public static bool Match<T>(T expectedValue, T inputValue)
        {
            return expectedValue.Equals(inputValue);
        }

        internal static void WriteResult(bool v)
        {
            Console.WriteLine(v ? "Success" : "Failure");
        }
    }
}
