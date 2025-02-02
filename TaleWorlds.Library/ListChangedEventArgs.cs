using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000057 RID: 87
	public class ListChangedEventArgs : EventArgs
	{
		// Token: 0x0600028F RID: 655 RVA: 0x000078DF File Offset: 0x00005ADF
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex)
		{
			this.ListChangedType = listChangedType;
			this.NewIndex = newIndex;
			this.OldIndex = -1;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000078FC File Offset: 0x00005AFC
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.ListChangedType = listChangedType;
			this.NewIndex = newIndex;
			this.OldIndex = oldIndex;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007919 File Offset: 0x00005B19
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00007921 File Offset: 0x00005B21
		public ListChangedType ListChangedType { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000792A File Offset: 0x00005B2A
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00007932 File Offset: 0x00005B32
		public int NewIndex { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000793B File Offset: 0x00005B3B
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00007943 File Offset: 0x00005B43
		public int OldIndex { get; private set; }
	}
}
