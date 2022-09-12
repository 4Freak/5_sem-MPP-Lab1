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
		public string name {get; private set;}
		public string className {get; private set;}
		public TimeSpan time {get; private set;}
		public IReadOnlyList<TraceMethod> innerMethods {get;}

		public TraceMethod(string name, string className, TimeSpan time, IReadOnlyList<TraceMethod> innerMethods)
		{
			this.name = name;
			this.className = className;
			this.time = time;
			this.innerMethods = innerMethods;
		}
	}
}
