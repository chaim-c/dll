using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000038 RID: 56
	internal struct Row<T1, T2, T3, T4>
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00007E81 File Offset: 0x00006081
		public Row(T1 col1, T2 col2, T3 col3, T4 col4)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
		}

		// Token: 0x040002C0 RID: 704
		internal T1 Col1;

		// Token: 0x040002C1 RID: 705
		internal T2 Col2;

		// Token: 0x040002C2 RID: 706
		internal T3 Col3;

		// Token: 0x040002C3 RID: 707
		internal T4 Col4;
	}
}
