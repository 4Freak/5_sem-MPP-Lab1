using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Core
{
	public class TraceResult
	{
		public IReadOnlyList<TraceThread> Threads {get;}

		public TraceResult(IReadOnlyList<TraceThread> threads)
		{
			this.Threads = threads;
		}
	}
}
