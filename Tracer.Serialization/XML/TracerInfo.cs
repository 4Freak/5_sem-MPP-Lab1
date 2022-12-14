using System.Xml;
using System.Xml.Serialization;
using Tracer.Core;

namespace XML
{
	public class ThreadInfo
	{
		[XmlAttribute("id")]
		public int Id;

		[XmlAttribute("time")]
		public string Time;

		[XmlElement("methods")]
		public List<MethodInfo> Methods;

		public ThreadInfo(int id, string time, List<MethodInfo> methods)
		{
			Id = id;
			Time = time;
			Methods = methods;
		}

		public ThreadInfo()
		{
			Id = 0;
			Time = String.Empty;
			Methods	= new List<MethodInfo>();
		}
	}

	public class MethodInfo
	{
		[XmlAttribute("name")]
		public string Name;

		[XmlAttribute("class")]
		public string ClassName;
		

		[XmlAttribute("time")]
		public string Time;

		[XmlElement("methods")]
		public List<MethodInfo> Methods;

		public MethodInfo(string name, string className, TimeSpan time, List <MethodInfo> methods)
		{
			Name = name;
			ClassName = className;
			Time = String.Format("{0:f} ms", time.TotalMilliseconds);
			Methods = methods;
		}

		public MethodInfo()
		{
			Name = String.Empty;
			ClassName = String.Empty;
			Time = String.Empty;
			Methods = new List<MethodInfo>();
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

	[XmlRoot("root")]
	public class SerializableTraceResult
	{
		[XmlElement("threads")]
		public List<ThreadInfo> Threads = new List<ThreadInfo>();

		public SerializableTraceResult(List<ThreadInfo> threads)
		{
			Threads = threads;
		}

		public SerializableTraceResult(){}
	}
}

