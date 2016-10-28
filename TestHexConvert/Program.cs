using System;
using System.Collections.Generic;

namespace TestHexConvert
{
	class Program
	{
		static Benchmark.Benchmarker bench = new Benchmark.Benchmarker ();
		static Random rnd = new Random ();
		static byte[] bytes = new byte[1024 * 1024];

		public static void Main (string[] args)
		{
			rnd.NextBytes (bytes);

			Console.WriteLine ("Testing first run");
			Console.WriteLine ("------------------------------------------------------------------------");
			Test ();

			Console.WriteLine ("\nTesting second run");
			Console.WriteLine ("------------------------------------------------------------------------");
			Test ();

			Console.ReadKey (true);
		}

		static void Test ()
		{
			bench.Restart ();
			var s1 = BitConverter.ToString (bytes).Replace ("-", "");
			bench.Stop ();
			Console.WriteLine ("{0,20} {1}", "Bitconverter", bench);

			bench.Restart ();
			var s2 = DRDigit.Fast.ToHexString (bytes);
			bench.Stop ();
			Console.WriteLine ("{0,20} {1}", "Fast.ToHexString", bench);

			bench.Restart ();
			s2 = Hex.ToHexString (bytes);
			bench.Stop ();
			Console.WriteLine ("{0,20} {1}", "Hex.ToHexString", bench);

			bench.Restart ();
			var b1 = DRDigit.Fast.FromHexString (s2);
			bench.Stop ();
			Console.WriteLine ("{0,20} {1}", "Fast.FromHexString", bench);

			bench.Restart ();
			var b2 = Hex.FromHexString (s2);
			bench.Stop ();
			Console.WriteLine ("{0,20} {1}", "Hex.FromHexString", bench);
		}
	}
}