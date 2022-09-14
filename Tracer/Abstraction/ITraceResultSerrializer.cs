using Tracer.Core;
using System.IO;

namespace Abstractions
{
	public interface ITraceResultSerializer
	{
		string Format {get; }
		void Serialize(TraceResult traceResult, Stream dest);		
	}
}