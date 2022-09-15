using Abstractions;
using System.Reflection;
using Serialization;

namespace Example
{
	internal class Example
	{
		public static void Main(string[] argv)
		{
			var example = new Example();
			
			var tracer = new Tracer.Core.Tracer();

			var foo = new Foo(tracer);
			var bar = new Bar(tracer);

			var thread1 = new Thread(() => 
			{
				foo.MyMethod(); 
			});
			thread1.Start();

			var thread2 = new Thread(() =>
			{
				bar.InnerMethod();
			});
			thread2.Start();

			thread1.Join();
			thread2.Join();

			var traceResult = tracer.GetTraceResult();

			var serializers = PluginLoader.GetSerializers();
			TraceResultSerializer.SerializeToFiles(serializers, traceResult, "notRes");
		}
	}
}
