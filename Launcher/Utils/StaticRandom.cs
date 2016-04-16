using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class StaticRandom
{
	static int seed = Environment.TickCount;

	static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

	public static Random Rand
	{
		get
		{
			return random.Value;
		}
	}
}