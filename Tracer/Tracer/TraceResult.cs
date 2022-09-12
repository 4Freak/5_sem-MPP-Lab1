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
