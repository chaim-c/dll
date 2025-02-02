using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000036 RID: 54
	internal struct Row<T1, T2>
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00007E5A File Offset: 0x0000605A
		public Row(T1 col1, T2 col2)
		{
			this.Col1 = col1;
			this.Col2 = col2;
		}

		// Token: 0x040002BB RID: 699
		internal T1 Col1;

		// Token: 0x040002BC RID: 700
		internal T2 Col2;
	}
}
