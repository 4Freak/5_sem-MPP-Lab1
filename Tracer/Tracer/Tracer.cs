using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Tracer.Core
{
    public class Tracer : ITracer
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
					string ? className = method.DeclaringType?.Name;
					if (className == null)
					{
						className = string.Empty;
					}
					var methodInfo = new MethodInfo(method.Name, className);

					int threadId = Thread.CurrentThread.ManagedThreadId;

					// '_ => new ThreadInfo()' allow you to not create 'ThreadInfo' if GetOrAdd -> Get
					var threadInfo = _tracerThreads.GetOrAdd(threadId, _ => new ThreadInfo());

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
			//Try get
				MethodInfo methodInfo = _tracerThreads[threadId].RunningMethods.Pop();
				methodInfo.Stopwatch.Stop();
			}
			catch (InvalidOperationException){ }
        }

		public TraceResult GetTraceResult()
		{
			List<TraceThread> threads = new List<TraceThread>();
			foreach(var thread in _tracerThreads)
			{
				List<TraceMethod> methods = new List<TraceMethod>();
				foreach(var method in thread.Value.RootMethod)
				{
					methods.Add(new TraceMethod(method.Name, method.ClassName, method.Stopwatch.Elapsed, GetInnerMethod(method)));
				}
				threads.Add(new TraceThread(thread.Key, methods));
			}
			return new TraceResult(threads);
		}    

		private List<TraceMethod> GetInnerMethod (MethodInfo traceMethod)
		{
			List<TraceMethod> innerMethods = new List<TraceMethod>();
			foreach (MethodInfo method in traceMethod.InnerMethods)
			{
				innerMethods.Add(new TraceMethod(method.Name, method.ClassName, method.Stopwatch.Elapsed, GetInnerMethod(method)));
			}
			return innerMethods;
		}
    }

}
