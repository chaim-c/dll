using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x02000093 RID: 147
	public sealed class DefaultParallelDriver : IParallelDriver
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00010E0C File Offset: 0x0000F00C
		public void For(int fromInclusive, int toExclusive, TWParallel.ParallelForAuxPredicate body, int grainSize)
		{
			Parallel.ForEach<Tuple<int, int>>(Partitioner.Create(fromInclusive, toExclusive, grainSize), Common.ParallelOptions, delegate(Tuple<int, int> range, ParallelLoopState loopState)
			{
				body(range.Item1, range.Item2);
			});
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00010E48 File Offset: 0x0000F048
		public void For(int fromInclusive, int toExclusive, float deltaTime, TWParallel.ParallelForWithDtAuxPredicate body, int grainSize)
		{
			Parallel.ForEach<Tuple<int, int>>(Partitioner.Create(fromInclusive, toExclusive, grainSize), Common.ParallelOptions, delegate(Tuple<int, int> range, ParallelLoopState loopState)
			{
				body(range.Item1, range.Item2, deltaTime);
			});
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00010E8A File Offset: 0x0000F08A
		public ulong GetMainThreadId()
		{
			return 0UL;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00010E8E File Offset: 0x0000F08E
		public ulong GetCurrentThreadId()
		{
			return 0UL;
		}
	}
}
