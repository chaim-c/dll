using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200003B RID: 59
	internal struct Row<T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00007EF8 File Offset: 0x000060F8
		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6, T7 col7, T8 col8, T9 col9)
		{
			this.Col1 = col1;
			this.Col2 = col2;
			this.Col3 = col3;
			this.Col4 = col4;
			this.Col5 = col5;
			this.Col6 = col6;
			this.Col7 = col7;
			this.Col8 = col8;
			this.Col9 = col9;
		}

		// Token: 0x040002CF RID: 719
		internal T1 Col1;

		// Token: 0x040002D0 RID: 720
		internal T2 Col2;

		// Token: 0x040002D1 RID: 721
		internal T3 Col3;

		// Token: 0x040002D2 RID: 722
		internal T4 Col4;

		// Token: 0x040002D3 RID: 723
		internal T5 Col5;

		// Token: 0x040002D4 RID: 724
		internal T6 Col6;

		// Token: 0x040002D5 RID: 725
		internal T7 Col7;

		// Token: 0x040002D6 RID: 726
		internal T8 Col8;

		// Token: 0x040002D7 RID: 727
		internal T9 Col9;
	}
}
