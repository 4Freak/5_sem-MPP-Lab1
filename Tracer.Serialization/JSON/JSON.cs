using Abstractions;
using System.Text.Json;
using Tracer.Core;

namespace JSON
{
	public class JSONTraceResultSerializer: ITraceResultSerializer
	{
		public string Format { get => "JSON"; }

		public void Serialize(TraceResult traceResult, Stream dest)
		{
			JsonSerializerOptions options = new JsonSerializerOptions (){ WriteIndented = true };

			var threadsInfo = new List<ThreadInfo>();
			foreach(TraceThread thread in traceResult.Threads)
			{
				var rootMethods = new List<MethodInfo>();
				foreach(TraceMethod method in thread.InnerMethods)
				{
					rootMethods.Add(new MethodInfo(method.Name, method.ClassName, method.Time, MethodInfo.GetInnerMethods(method)));
				}
				threadsInfo.Add(new ThreadInfo(thread.Id, thread.Time.ToString(), rootMethods));
			}
			var serializableTraceResult = new SerializableTraceResult(threadsInfo);

			JsonSerializer.Serialize(dest, serializableTraceResult, options);
		}
	}
}
