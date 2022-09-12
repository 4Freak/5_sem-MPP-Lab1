using Tracer.Core;
using System.IO;

namespace Abstractions
{
	internal interface ITraceResultSerializer
	{
		void Serialize(TraceResult traceResult, Stream dest);		
	}
}