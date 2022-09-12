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
		public int Id;
		public double Time;
		public IReadOnlyList <TraceMethod> InnerMethods;

		public TraceThread(int id, double time, IReadOnlyList<TraceMethod> innerMethods)
		{
			this.Id = id;
			this.Time = time;
			this.InnerMethods = innerMethods;
		}
	}
}
