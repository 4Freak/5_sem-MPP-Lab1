using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Core
{
    internal class Tracer : ITracer
    {
		private ConcurrentDictionary<int, ThreadInfo> _tracerThreads = new ConcurrentDictionary<int, ThreadInfo>();
        
		private struct ThreadInfo
		{
			public Stack<MethodInfo> RunningMethods {get; set; }
			public List<MethodInfo> RootMethod {get; set; }
			public ThreadInfo()
			{
				RunningMethods = new Stack<MethodInfo>();
				RootMethod = new List<MethodInfo>();
			}
		}

		public struct MethodInfo
		{
			public string name {get; set; }
			public string className {get; set; }
			public Stopwatch stopwatch {get; set; }			
			public List<MethodInfo> innerMethods {get; set; }

			public MethodInfo(string Name, string ClassName, Stopwatch Stopwatch)
			{
				name = Name;
				className = ClassName;	
				stopwatch = Stopwatch;
				innerMethods = new List<MethodInfo>();
			}
		}
		
		
		public static void DebugStackTraceOutput(StackFrame[] stackFrames)
        {
            for (int i = 0; i < stackFrames.Length; i++)
            {
                Console.WriteLine("{0} Method: {1}", i, stackFrames[i].GetMethod());
            }
        }
		public void StartTrace()
        {
            var stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            DebugStackTraceOutput(stackFrames);


        }
        public void StopTrace()
        {
			var stackTrace = new StackTrace();
			StackFrame[] stackFrames = stackTrace.GetFrames();

        }

		public TraceResult GetTraceResult()
		{
			return null;
		}    
    }

}
