using Example;
using Tracer.Core;

namespace Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void SingleThreadInnerMethods()
		{
			// Arrange
			var tracer = new Tracer.Core.Tracer();

			var foo = new Foo(tracer);
			var bar = new Bar(tracer);

			// Act
			foo.MyMethod();
			bar.InnerMethod();

			var traceResult = tracer.GetTraceResult();

			// Assert
			Assert.That(traceResult.Threads.Count, Is.EqualTo(1));
			Assert.That(traceResult.Threads[0].Time, Is.InRange(500,650));
			Assert.That(traceResult.Threads[0].InnerMethods.Count, Is.EqualTo(2));

			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[0].InnerMethods[0].Name, Is.EqualTo("MyMethod"));
				Assert.That(traceResult.Threads[0].InnerMethods[0].ClassName, Is.EqualTo("Foo"));
				Assert.That(traceResult.Threads[0].InnerMethods[0].Time.TotalMilliseconds, Is.InRange(500, 650));
				Assert.That(traceResult.Threads[0].InnerMethods[0].InnerMethods.Count, Is.EqualTo(1));
			});
			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[0].InnerMethods[1].Name, Is.EqualTo("InnerMethod"));
				Assert.That(traceResult.Threads[0].InnerMethods[1].ClassName, Is.EqualTo("Bar"));
				Assert.That(traceResult.Threads[0].InnerMethods[1].Time.TotalMilliseconds, Is.InRange(100, 250));
				Assert.That(traceResult.Threads[0].InnerMethods[1].InnerMethods.Count, Is.EqualTo(0));
			});

			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[0].InnerMethods[0].InnerMethods[0].Name, Is.EqualTo("InnerMethod"));
				Assert.That(traceResult.Threads[0].InnerMethods[0].InnerMethods[0].ClassName, Is.EqualTo("Bar"));
				Assert.That(traceResult.Threads[0].InnerMethods[0].InnerMethods[0].Time.TotalMilliseconds, Is.InRange(100, 250));
				Assert.That(traceResult.Threads[0].InnerMethods[0].InnerMethods[0].InnerMethods.Count, Is.EqualTo(0));
			});

		}

		[Test]
		public void TwoThreadInnerMethods()
		{
			// Arrange		
			var tracer = new Tracer.Core.Tracer();

			var foo = new Foo(tracer);
			var bar = new Bar(tracer);

			// Act
			var thread1 = new Thread(() => 
			{
				bar.InnerMethod(); 
			});
			thread1.Start();

			Thread.Sleep(00);

			var thread2 = new Thread(() =>
			{
				foo.MyMethod();
				bar.InnerMethod();
			});
			thread2.Start();

			thread1.Join();
			thread2.Join();

			var traceResult = tracer.GetTraceResult();

			// Assert
			Assert.That(traceResult.Threads.Count, Is.EqualTo(2));
			Assert.That(traceResult.Threads[0].Time, Is.InRange(100,250));
			Assert.That(traceResult.Threads[0].InnerMethods.Count, Is.EqualTo(1));

			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[0].InnerMethods[0].Name, Is.EqualTo("InnerMethod"));
				Assert.That(traceResult.Threads[0].InnerMethods[0].ClassName, Is.EqualTo("Bar"));
				Assert.That(traceResult.Threads[0].InnerMethods[0].Time.TotalMilliseconds, Is.InRange(100, 250));
				Assert.That(traceResult.Threads[0].InnerMethods[0].InnerMethods.Count, Is.EqualTo(0));
			});
			
			Assert.That(traceResult.Threads[1].Time, Is.InRange(500,650));
			Assert.That(traceResult.Threads[1].InnerMethods.Count, Is.EqualTo(2));

			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[1].InnerMethods[0].Name, Is.EqualTo("MyMethod"));
				Assert.That(traceResult.Threads[1].InnerMethods[0].ClassName, Is.EqualTo("Foo"));
				Assert.That(traceResult.Threads[1].InnerMethods[0].Time.TotalMilliseconds, Is.InRange(500, 650));
				Assert.That(traceResult.Threads[1].InnerMethods[0].InnerMethods.Count, Is.EqualTo(1));
			});
			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[1].InnerMethods[1].Name, Is.EqualTo("InnerMethod"));
				Assert.That(traceResult.Threads[1].InnerMethods[1].ClassName, Is.EqualTo("Bar"));
				Assert.That(traceResult.Threads[1].InnerMethods[1].Time.TotalMilliseconds, Is.InRange(100, 250));
				Assert.That(traceResult.Threads[1].InnerMethods[1].InnerMethods.Count, Is.EqualTo(0));
			});

			Assert.Multiple(() =>
			{
				Assert.That(traceResult.Threads[1].InnerMethods[0].InnerMethods[0].Name, Is.EqualTo("InnerMethod"));
				Assert.That(traceResult.Threads[1].InnerMethods[0].InnerMethods[0].ClassName, Is.EqualTo("Bar"));
				Assert.That(traceResult.Threads[1].InnerMethods[0].InnerMethods[0].Time.TotalMilliseconds, Is.InRange(100, 250));
				Assert.That(traceResult.Threads[1].InnerMethods[0].InnerMethods[0].InnerMethods.Count, Is.EqualTo(0));
			});
		}
	}
}