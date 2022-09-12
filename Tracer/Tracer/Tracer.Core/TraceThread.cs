using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace Tracer.Core
{
	public class TraceThread
	{
		public int id;
		public double time;
		public IReadOnlyList <TraceMethod> innerMethods;

		public TraceThread(int id, double time, IReadOnlyList<TraceMethod> innerMethods)
		{
			this.id = id;
			this.time = time;
			this.innerMethods = innerMethods;
		}
	}
}
