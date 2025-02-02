using System;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x0200006A RID: 106
	public class MBReadOnlyList<T> : List<T>
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000C410 File Offset: 0x0000A610
		public MBReadOnlyList()
		{
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C418 File Offset: 0x0000A618
		public MBReadOnlyList(int capacity) : base(capacity)
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C421 File Offset: 0x0000A621
		public MBReadOnlyList(IEnumerable<T> collection) : base(collection)
		{
		}
	}
}
