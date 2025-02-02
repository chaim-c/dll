using System;
using System.Threading;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006C RID: 108
	public sealed class NativeParallelDriver : IParallelDriver
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x000087B8 File Offset: 0x000069B8
		public void For(int fromInclusive, int toExclusive, TWParallel.ParallelForAuxPredicate loopBody, int grainSize)
		{
			long num = Interlocked.Increment(ref NativeParallelDriver.LoopBodyHolder.UniqueLoopBodyKeySeed) % 256L;
			checked
			{
				NativeParallelDriver._loopBodyCache[(int)((IntPtr)num)].LoopBody = loopBody;
				Utilities.ParallelFor(fromInclusive, toExclusive, num, grainSize);
				NativeParallelDriver._loopBodyCache[(int)((IntPtr)num)].LoopBody = null;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00008805 File Offset: 0x00006A05
		[EngineCallback]
		internal static void ParalelForLoopBodyCaller(long loopBodyKey, int localStartIndex, int localEndIndex)
		{
			NativeParallelDriver._loopBodyCache[(int)(checked((IntPtr)loopBodyKey))].LoopBody(localStartIndex, localEndIndex);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00008820 File Offset: 0x00006A20
		public void For(int fromInclusive, int toExclusive, float deltaTime, TWParallel.ParallelForWithDtAuxPredicate loopBody, int grainSize)
		{
			long num = Interlocked.Increment(ref NativeParallelDriver.LoopBodyWithDtHolder.UniqueLoopBodyKeySeed) % 256L;
			checked
			{
				NativeParallelDriver._loopBodyWithDtCache[(int)((IntPtr)num)].LoopBody = loopBody;
				NativeParallelDriver._loopBodyWithDtCache[(int)((IntPtr)num)].DeltaTime = deltaTime;
				Utilities.ParallelForWithDt(fromInclusive, toExclusive, num, grainSize);
				NativeParallelDriver._loopBodyWithDtCache[(int)((IntPtr)num)].LoopBody = null;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00008880 File Offset: 0x00006A80
		[EngineCallback]
		internal static void ParalelForLoopBodyWithDtCaller(long loopBodyKey, int localStartIndex, int localEndIndex)
		{
			checked
			{
				NativeParallelDriver._loopBodyWithDtCache[(int)((IntPtr)loopBodyKey)].LoopBody(localStartIndex, localEndIndex, NativeParallelDriver._loopBodyWithDtCache[(int)((IntPtr)loopBodyKey)].DeltaTime);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000088AB File Offset: 0x00006AAB
		public ulong GetMainThreadId()
		{
			return Utilities.GetMainThreadId();
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000088B2 File Offset: 0x00006AB2
		public ulong GetCurrentThreadId()
		{
			return Utilities.GetCurrentThreadId();
		}

		// Token: 0x04000148 RID: 328
		private const int K = 256;

		// Token: 0x04000149 RID: 329
		private static NativeParallelDriver.LoopBodyHolder[] _loopBodyCache = new NativeParallelDriver.LoopBodyHolder[256];

		// Token: 0x0400014A RID: 330
		private static NativeParallelDriver.LoopBodyWithDtHolder[] _loopBodyWithDtCache = new NativeParallelDriver.LoopBodyWithDtHolder[256];

		// Token: 0x020000C1 RID: 193
		private struct LoopBodyHolder
		{
			// Token: 0x04000402 RID: 1026
			public static long UniqueLoopBodyKeySeed;

			// Token: 0x04000403 RID: 1027
			public TWParallel.ParallelForAuxPredicate LoopBody;
		}

		// Token: 0x020000C2 RID: 194
		private struct LoopBodyWithDtHolder
		{
			// Token: 0x04000404 RID: 1028
			public static long UniqueLoopBodyKeySeed;

			// Token: 0x04000405 RID: 1029
			public TWParallel.ParallelForWithDtAuxPredicate LoopBody;

			// Token: 0x04000406 RID: 1030
			public float DeltaTime;
		}
	}
}
