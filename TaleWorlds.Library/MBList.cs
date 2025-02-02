using System;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x02000065 RID: 101
	public class MBList<T> : MBReadOnlyList<T>
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000AFB3 File Offset: 0x000091B3
		public MBList()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000AFBB File Offset: 0x000091BB
		public MBList(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000AFC4 File Offset: 0x000091C4
		public MBList(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000AFCD File Offset: 0x000091CD
		public MBList(List<T> collection) : base(collection)
		{
		}
	}
}
