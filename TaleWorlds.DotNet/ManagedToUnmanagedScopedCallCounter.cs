using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000026 RID: 38
	public class ManagedToUnmanagedScopedCallCounter : IDisposable
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00004A78 File Offset: 0x00002C78
		public ManagedToUnmanagedScopedCallCounter()
		{
			if (!ManagedToUnmanagedScopedCallCounter._table.IsValueCreated)
			{
				ManagedToUnmanagedScopedCallCounter._table.Value = new Dictionary<int, List<StackTrace>>();
			}
			ThreadLocal<int> depth = ManagedToUnmanagedScopedCallCounter._depth;
			int value = depth.Value;
			depth.Value = value + 1;
			if (ManagedToUnmanagedScopedCallCounter._depth.Value < ManagedToUnmanagedScopedCallCounter._depthThreshold)
			{
				return;
			}
			this._st = new StackTrace(true);
			List<StackTrace> list;
			if (ManagedToUnmanagedScopedCallCounter._table.Value.TryGetValue(ManagedToUnmanagedScopedCallCounter._depth.Value, out list))
			{
				list.Add(this._st);
				return;
			}
			ManagedToUnmanagedScopedCallCounter._table.Value.Add(ManagedToUnmanagedScopedCallCounter._depth.Value, new List<StackTrace>
			{
				this._st
			});
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004B2C File Offset: 0x00002D2C
		public void Dispose()
		{
			ThreadLocal<int> depth = ManagedToUnmanagedScopedCallCounter._depth;
			int value = depth.Value;
			depth.Value = value - 1;
		}

		// Token: 0x0400005B RID: 91
		private static ThreadLocal<Dictionary<int, List<StackTrace>>> _table = new ThreadLocal<Dictionary<int, List<StackTrace>>>();

		// Token: 0x0400005C RID: 92
		private static ThreadLocal<int> _depth = new ThreadLocal<int>();

		// Token: 0x0400005D RID: 93
		private static int _depthThreshold = 4;

		// Token: 0x0400005E RID: 94
		private StackTrace _st;
	}
}
