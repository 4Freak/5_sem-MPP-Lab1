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
		public Stopwatch time;
		public ConcurrentDictionary<int, TraceMethod> innerMethods;
	}
}
