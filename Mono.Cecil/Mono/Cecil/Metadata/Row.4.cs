using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000039 RID: 57
	internal struct Row<T1, T2, T3, T4, T5>
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00007EA0 File Offset: 0x000060A0
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
		}

		// Token: 0x040002C4 RID: 708
		internal T1 Col1;

		// Token: 0x040002C5 RID: 709
		internal T2 Col2;

		// Token: 0x040002C6 RID: 710
		internal T3 Col3;

		// Token: 0x040002C7 RID: 711
		internal T4 Col4;

		// Token: 0x040002C8 RID: 712
		internal T5 Col5;
	}
}
