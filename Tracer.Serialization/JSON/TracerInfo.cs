using System.Text.Json.Serialization;
using Tracer.Core;

namespace JSON
{
	public class ThreadInfo
	{
		[JsonInclude, JsonPropertyName("id")]
		public int Id;

		[JsonInclude, JsonPropertyName("time")]
		public string Time;

		[JsonInclude, JsonPropertyName("methods")]
		public List<MethodInfo> Methods;

		public ThreadInfo(int id, string time, List<MethodInfo> methods)
		{
			Id = id;
			Time = time;
			Methods = methods;
		}
	}

	public class MethodInfo
	{
		[JsonInclude, JsonPropertyName("name")]
		public string Name;

		[JsonInclude, JsonPropertyName("class")]
		public string ClassName;
		

		[JsonInclude, JsonPropertyName("time")]
		public string Time;

		[JsonInclude, JsonPropertyName("methods")]
		public List<MethodInfo> Methods;

		public MethodInfo(string name, string className, TimeSpan time, List <MethodInfo> methods)
		{
			Name = name;
			ClassName = className;
			Time = String.Format("{0:f} ms", time.TotalMilliseconds);
			Methods = methods;
		}

		public static List<MethodInfo> GetInnerMethods (TraceMethod traceMethod)
		{
			List<MethodInfo> innerMethods = new List<MethodInfo>();
			foreach (TraceMethod method in traceMethod.InnerMethods)
			{
				innerMethods.Add(new MethodInfo(method.Name, method.ClassName, method.Time, GetInnerMethods(method)));
			}
			return innerMethods;
		}
	}

	public class SerializableTraceResult
	{
		[JsonInclude, JsonPropertyName("threads")]
		public List<ThreadInfo> Threads = new List<ThreadInfo>();
		
		public SerializableTraceResult(List<ThreadInfo> threads)
		{
			Threads = threads;
		}
	}
}
