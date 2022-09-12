using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

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
			public string Name {get; set; }
			public string ClassName {get; set; }
			public Stopwatch Stopwatch {get; set; }			
			public List<MethodInfo> InnerMethods {get; set; }

			public MethodInfo(string name, string className)
			{
				Name = name;
				ClassName = className;	
				Stopwatch = new Stopwatch();
				InnerMethods = new List<MethodInfo>();
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
			MethodBase ? method = null;
			StackFrame ? frame = stackTrace.GetFrame(1);

			if (frame != null)
			{
				method = frame.GetMethod();
				
				if (method != null)
				{
					string ? className = method.DeclaringType.Name;
					if (className == null)
					{
						className = string.Empty;
					}
					var methodInfo = new MethodInfo(method.Name, className);

					int threadId = Thread.CurrentThread.ManagedThreadId;
					var threadInfo = _tracerThreads.GetOrAdd(threadId, new ThreadInfo());

					if (threadInfo.RunningMethods.Count > 0)
					{
						var parentMethod = threadInfo.RunningMethods.Peek();
						parentMethod.InnerMethods.Add(methodInfo);
					}
					else
					{
						threadInfo.RootMethod.Add(methodInfo);
					}

					threadInfo.RunningMethods.Push(methodInfo);
					methodInfo.Stopwatch.Start();
				}
			}

        }
        public void StopTrace()
        {
			int threadId = Thread.CurrentThread.ManagedThreadId;
			try
			{
				MethodInfo methodInfo = _tracerThreads[threadId].RunningMethods.Pop();
				methodInfo.Stopwatch.Stop();
			}
			catch (InvalidOperationException){ }
        }

		public TraceResult GetTraceResult()
		{
			return null;
		}    
    }

}
