using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200003A RID: 58
	internal struct Row<T1, T2, T3, T4, T5, T6>
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00007EC7 File Offset: 0x000060C7
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
			this.Col6 = col6;
		}

		// Token: 0x040002C9 RID: 713
		internal T1 Col1;

		// Token: 0x040002CA RID: 714
		internal T2 Col2;

		// Token: 0x040002CB RID: 715
		internal T3 Col3;

		// Token: 0x040002CC RID: 716
		internal T4 Col4;

		// Token: 0x040002CD RID: 717
		internal T5 Col5;

		// Token: 0x040002CE RID: 718
		internal T6 Col6;
	}
}
