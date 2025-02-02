using System;
using System.Collections;

namespace TaleWorlds.Library
{
	// Token: 0x02000035 RID: 53
	public interface IMBBindingList : IList, ICollection, IEnumerable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001B1 RID: 433
		// (remove) Token: 0x060001B2 RID: 434
		event ListChangedEventHandler ListChanged;
	}
}
