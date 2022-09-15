using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Core;

namespace Serialization
{
	public class TraceResultSerializer
	{
		public static void SerializeToFiles(List<ITraceResultSerializer> serializers, TraceResult traceResult, string fileName)
		{
			foreach (var serializer in serializers)
			{
				using var filestream = new FileStream($"{fileName}.{serializer.Format}", FileMode.Create);
				serializer.Serialize(traceResult, filestream);
			}
		}
	}
}
