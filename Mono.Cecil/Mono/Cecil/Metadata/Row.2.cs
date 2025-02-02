using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000037 RID: 55
	internal struct Row<T1, T2, T3>
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00007E6A File Offset: 0x0000606A
		public Row(T1 col1, T2 col2, T3 col3)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
		}

		// Token: 0x040002BD RID: 701
		internal T1 Col1;

		// Token: 0x040002BE RID: 702
		internal T2 Col2;

		// Token: 0x040002BF RID: 703
		internal T3 Col3;
	}
}
