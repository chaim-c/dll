using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x02000094 RID: 148
	public static class TWParallel
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00010E9A File Offset: 0x0000F09A
		public static void InitializeAndSetImplementation(IParallelDriver parallelDriver)
		{
			TWParallel._parallelDriver = parallelDriver;
			TWParallel._mainThreadId = TWParallel.GetMainThreadId();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00010EAC File Offset: 0x0000F0AC
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
		{
			return Parallel.ForEach<TSource>(Partitioner.Create<TSource>(source), Common.ParallelOptions, body);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00010EBF File Offset: 0x0000F0BF
		[Obsolete("Please use For() not ForEach() for better Parallel Performance.", true)]
		public static void ForEach<TSource>(IList<TSource> source, Action<TSource> body)
		{
			Parallel.ForEach<TSource>(Partitioner.Create<TSource>(source), Common.ParallelOptions, body);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010ED3 File Offset: 0x0000F0D3
		public static void For(int fromInclusive, int toExclusive, TWParallel.ParallelForAuxPredicate body, int grainSize = 16)
		{
			TWParallel.IsInParallelFor = true;
			if (toExclusive - fromInclusive < grainSize)
			{
				body(fromInclusive, toExclusive);
			}
			else
			{
				TWParallel._parallelDriver.For(fromInclusive, toExclusive, body, grainSize);
			}
			TWParallel.IsInParallelFor = false;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00010EFF File Offset: 0x0000F0FF
		public static void For(int fromInclusive, int toExclusive, float deltaTime, TWParallel.ParallelForWithDtAuxPredicate body, int grainSize = 16)
		{
			TWParallel.IsInParallelFor = true;
			if (toExclusive - fromInclusive < grainSize)
			{
				body(fromInclusive, toExclusive, deltaTime);
			}
			else
			{
				TWParallel._parallelDriver.For(fromInclusive, toExclusive, deltaTime, body, grainSize);
			}
			TWParallel.IsInParallelFor = false;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00010F2F File Offset: 0x0000F12F
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void AssertIsMainThread()
		{
			TWParallel.GetCurrentThreadId();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010F37 File Offset: 0x0000F137
		public static bool IsMainThread()
		{
			return TWParallel._mainThreadId == TWParallel.GetCurrentThreadId();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00010F45 File Offset: 0x0000F145
		private static ulong GetMainThreadId()
		{
			return TWParallel._parallelDriver.GetMainThreadId();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00010F51 File Offset: 0x0000F151
		private static ulong GetCurrentThreadId()
		{
			return TWParallel._parallelDriver.GetCurrentThreadId();
		}

		// Token: 0x0400017F RID: 383
		private static IParallelDriver _parallelDriver = new DefaultParallelDriver();

		// Token: 0x04000180 RID: 384
		private static ulong _mainThreadId;

		// Token: 0x04000181 RID: 385
		public static bool IsInParallelFor = false;

		// Token: 0x020000DC RID: 220
		public class SingleThreadTestData
		{
			// Token: 0x040002B3 RID: 691
			public static TWParallel.SingleThreadTestData GlobalData = new TWParallel.SingleThreadTestData();

			// Token: 0x040002B4 RID: 692
			public int InsideThreadCount;
		}

		// Token: 0x020000DD RID: 221
		public struct SingleThreadTestBlock : IDisposable
		{
			// Token: 0x0600072D RID: 1837 RVA: 0x0001698E File Offset: 0x00014B8E
			public SingleThreadTestBlock(TWParallel.SingleThreadTestData data)
			{
				TWParallel.SingleThreadTestBlock.SingleThreadTestAssert(Interlocked.Increment(ref data.InsideThreadCount) == 1);
				this._data = data;
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x000169AA File Offset: 0x00014BAA
			public void Dispose()
			{
				TWParallel.SingleThreadTestBlock.SingleThreadTestAssert(Interlocked.Decrement(ref this._data.InsideThreadCount) == 0);
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x000169C4 File Offset: 0x00014BC4
			private static void SingleThreadTestAssert(bool b)
			{
				if (!b)
				{
					Debugger.Break();
					Debug.FailedAssert("Single thread test have failed!", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\TWParallel.cs", "SingleThreadTestAssert", 89);
				}
			}

			// Token: 0x040002B5 RID: 693
			private readonly TWParallel.SingleThreadTestData _data;
		}

		// Token: 0x020000DE RID: 222
		public class RecursiveSingleThreadTestData
		{
			// Token: 0x040002B6 RID: 694
			public static TWParallel.RecursiveSingleThreadTestData GlobalData = new TWParallel.RecursiveSingleThreadTestData();

			// Token: 0x040002B7 RID: 695
			public static TWParallel.RecursiveSingleThreadTestData ScriptComponentAddRemove = new TWParallel.RecursiveSingleThreadTestData();

			// Token: 0x040002B8 RID: 696
			public int InsideCallCount;

			// Token: 0x040002B9 RID: 697
			public int InsideThreadId = -1;
		}

		// Token: 0x020000DF RID: 223
		public struct RecursiveSingleThreadTestBlock : IDisposable
		{
			// Token: 0x06000732 RID: 1842 RVA: 0x00016A0C File Offset: 0x00014C0C
			public RecursiveSingleThreadTestBlock(TWParallel.RecursiveSingleThreadTestData data)
			{
				this._data = data;
				int threadId = this.GetThreadId();
				TWParallel.RecursiveSingleThreadTestData data2 = this._data;
				lock (data2)
				{
					if (Interlocked.Increment(ref data.InsideCallCount) == 1)
					{
						data.InsideThreadId = threadId;
					}
				}
				TWParallel.RecursiveSingleThreadTestBlock.SingleThreadTestAssert(data.InsideThreadId == threadId);
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x00016A78 File Offset: 0x00014C78
			public void Dispose()
			{
				int threadId = this.GetThreadId();
				TWParallel.RecursiveSingleThreadTestBlock.SingleThreadTestAssert(this._data.InsideThreadId == threadId);
				TWParallel.RecursiveSingleThreadTestData data = this._data;
				lock (data)
				{
					if (Interlocked.Decrement(ref this._data.InsideCallCount) == 0)
					{
						this._data.InsideThreadId = -1;
					}
				}
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x00016AEC File Offset: 0x00014CEC
			private static void SingleThreadTestAssert(bool b)
			{
				if (!b)
				{
					Debugger.Break();
					Debug.FailedAssert("Single thread test have failed!", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\TWParallel.cs", "SingleThreadTestAssert", 149);
				}
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x00016B0F File Offset: 0x00014D0F
			private int GetThreadId()
			{
				return Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x040002BA RID: 698
			private readonly TWParallel.RecursiveSingleThreadTestData _data;
		}

		// Token: 0x020000E0 RID: 224
		// (Invoke) Token: 0x06000737 RID: 1847
		public delegate void ParallelForAuxPredicate(int localStartIndex, int localEndIndex);

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x0600073B RID: 1851
		public delegate void ParallelForWithDtAuxPredicate(int localStartIndex, int localEndIndex, float dt);
	}
}
