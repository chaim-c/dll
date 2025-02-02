using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000028 RID: 40
	public class NodeComparer : IComparer<ThumbnailCacheNode>
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public int Compare(ThumbnailCacheNode x, ThumbnailCacheNode y)
		{
			return x.FrameNo.CompareTo(y.FrameNo);
		}
	}
}
