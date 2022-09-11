using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Example
{
    public class TracerExample
    {
        public static void MyMethod()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
        }
        public static void Main(string[] argc)
        {
            MyMethod();
        }
    }
}
