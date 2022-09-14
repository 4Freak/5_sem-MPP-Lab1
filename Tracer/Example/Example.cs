using Abstractions;
using System.Reflection;

namespace Example
{
	internal class Example
	{
		public string PluginsPath = "Plugins";
		private List<ITraceResultSerializer> _serializers = new();

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

			var serializers = example.GetSerializers();
			foreach (var serializer in serializers)
			{
				using var filestream = new FileStream($"res.{serializer.Format}", FileMode.Create);
				serializer.Serialize(traceResult, filestream);
			}
		}

		public List<ITraceResultSerializer> GetSerializers()
		{
			_serializers.Clear();
			var pluginDirectory = new DirectoryInfo(PluginsPath);
			if (!pluginDirectory.Exists)
			{
				pluginDirectory.Create();
			}

			var pluginLib = Directory.GetFiles(PluginsPath, "*.dll");
			foreach(var file in pluginLib)
			{
				Assembly assembly = Assembly.LoadFrom(file);
				var types = assembly.GetTypes().
					Where(t => t.GetInterfaces().
					Where(i => i.FullName == typeof(ITraceResultSerializer).FullName).Any());

				foreach (var type in types)
				{
					if (type.FullName != null)
					{
						var plugin = assembly.CreateInstance(type.FullName) as ITraceResultSerializer;
						if (plugin != null)
						{
							_serializers.Add(plugin);
						}
					}
				}
			}
			return _serializers;
		}
	}
}
