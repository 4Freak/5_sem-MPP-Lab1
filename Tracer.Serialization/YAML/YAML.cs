using Abstractions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Tracer.Core;

namespace YAML
{
	public class YAMLTraceResultSerializer: ITraceResultSerializer
	{
		public string Format { get => "YAML"; }

		public void Serialize(TraceResult traceResult, Stream dest)
		{
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

			var serializer = new SerializerBuilder()
				.WithNamingConvention(CamelCaseNamingConvention.Instance)
				.Build();

			var YAMLString = serializer.Serialize(serializableTraceResult);
			
			using var streamWriter = new StreamWriter(dest);
			streamWriter.WriteLine(YAMLString);
			streamWriter.Flush();

		}
	}
}
