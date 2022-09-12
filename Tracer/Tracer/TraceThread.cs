namespace Tracer.Core
{
	public class TraceThread
	{
		public int Id;
		public double Time;
		public IReadOnlyList <TraceMethod> InnerMethods;

		public TraceThread(int id, IReadOnlyList<TraceMethod> innerMethods)
		{
			this.Id = id;
			this.InnerMethods = innerMethods;
			this.Time = this.InnerMethods.Sum(method => method.Time.TotalMilliseconds);
		}
	}
}
