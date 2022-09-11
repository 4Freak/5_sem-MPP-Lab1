using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Core
{
	public class TraceMethod
	{
		public string name;
		public string className;
		public TimeSpan time;
		public ConcurrentDictionary<int, TraceMethod> innerMethods;
	}
}
