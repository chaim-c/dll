using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000092 RID: 146
	public interface IParallelDriver
	{
		// Token: 0x0600050B RID: 1291
		void For(int fromInclusive, int toExclusive, TWParallel.ParallelForAuxPredicate body, int grainSize);

		// Token: 0x0600050C RID: 1292
		void For(int fromInclusive, int toExclusive, float deltaTime, TWParallel.ParallelForWithDtAuxPredicate body, int grainSize);

		// Token: 0x0600050D RID: 1293
		ulong GetMainThreadId();

		// Token: 0x0600050E RID: 1294
		ulong GetCurrentThreadId();
	}
}
