using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Core
{
    internal class Tracer : ITracer
    {
        private ConcurrentDictionary<int, TraceThread> TracerThreads = new ConcurrentDictionary<int, TraceThread>();
        public static void DebugStackTraceOutput(StackFrame[] stackFrames)
        {
            for (int i = 0; i < stackFrames.Length; i++)
            {
                Console.WriteLine("{0} Method: {1}", i, stackFrames[i].GetMethod());
            }
        }

        private ConcurrentDictionary<int, TraceMethod> FindMethod(MethodBase methodBase, ConcurrentDictionary<int, TraceMethod> methods)
        {
			return null;
        }

        private void AddTracer(StackFrame[] stackFrames, ConcurrentDictionary<int, TraceThread> TracerThreads)
        {

        }

		private void StopTracer(StackFrame[] stackFrames, ConcurrentDictionary<int, TraceThread> TracerThreads)
		{
			
		}
        public void StartTrace()
        {
            var stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            DebugStackTraceOutput(stackFrames);
            AddTracer(stackFrames, TracerThreads);


        }
        public void StopTrace()
        {
			var stackTrace = new StackTrace();
			StackFrame[] stackFrames = stackTrace.GetFrames();
			StopTracer(stackFrames, TracerThreads);

        }

		public TraceResult GetTraceResult()
		{
			return null;
		}
    }

}
