using Tracer.Core;

namespace Example
{
		public class Foo
		{
			private Bar _bar;
			private ITracer _tracer;

			internal Foo(ITracer tracer)
			{
				_tracer = tracer;
				_bar = new Bar(_tracer);
			}
    
			public void MyMethod()
			{
				_tracer.StartTrace();
				Thread.Sleep(200);
				_bar.InnerMethod();
				Thread.Sleep(200);
				_tracer.StopTrace();
			}
		}

	public class Bar
	{
		private ITracer _tracer;

		internal Bar(ITracer tracer)
		{
			_tracer = tracer;
		}
    
		public void InnerMethod()
		{
			_tracer.StartTrace();
			Thread.Sleep(100);
			_tracer.StopTrace();
		}
	}
}
