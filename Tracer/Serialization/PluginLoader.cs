using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
	public class PluginLoader
	{
		private const string PluginsPath = "Plugins";
		private List<ITraceResultSerializer> _serializers = new();
		
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
					Where(t => t.IsAssignableTo(typeof(ITraceResultSerializer)));
					
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
