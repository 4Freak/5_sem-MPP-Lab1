using Abstractions;
using System.Xml;
using System.Xml.Serialization;
using Tracer.Core;

namespace XML
{
	public class XMLTraceResultSerializer: ITraceResultSerializer
	{
		public string Format { get => "XML"; }

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

			using var xmlWriter = XmlWriter.Create(dest, new XmlWriterSettings { Indent = true });
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializableTraceResult));
			xmlSerializer.Serialize(xmlWriter, serializableTraceResult);

		}
	}
}
