using System;
using System.Diagnostics;

namespace Benchmark
{
	/// <summary>
	/// Description of Benchmarker.
	/// </summary>
	public class Benchmarker : Stopwatch
	{
		long mem1 = 0, mem2 = 0;

		public long BytesUsed { get { return mem2 - mem1; } }

		public new void Start ()
		{
			mem1 = mem2 = GC.GetTotalMemory (true);
			base.Start ();
		}

		public new void Restart ()
		{
			mem1 = mem2 = GC.GetTotalMemory (true);
			base.Restart ();
		}

		public new void Reset ()
		{
			mem1 = mem2 = GC.GetTotalMemory (true);
			base.Reset ();
		}

		public new void Stop ()
		{
			base.Stop ();
			mem2 = GC.GetTotalMemory (false);
		}

		public override string ToString ()
		{
			return string.Format ("took {0,10:F3}ms & used {1,10} bytes", Elapsed.TotalMilliseconds, BytesUsed);
		}
	}
}
