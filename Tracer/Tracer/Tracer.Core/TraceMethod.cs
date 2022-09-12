namespace Tracer.Core
{
	public class TraceMethod
	{
		public string Name {get; private set;}
		public string ClassName {get; private set;}
		public TimeSpan Time {get; private set;}
		public IReadOnlyList<TraceMethod> InnerMethods {get;}

		public TraceMethod(string name, string className, TimeSpan time, IReadOnlyList<TraceMethod> innerMethods)
		{
			this.Name = name;
			this.ClassName = className;
			this.Time = time;
			this.InnerMethods = innerMethods;
		}
	}
}
