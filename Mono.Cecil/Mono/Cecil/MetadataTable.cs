using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000066 RID: 102
	internal abstract class MetadataTable
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000405 RID: 1029
		public abstract int Length { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00011096 File Offset: 0x0000F296
		public bool IsLarge
		{
			get
			{
				return this.Length > 65535;
			}
		}

		// Token: 0x06000407 RID: 1031
		public abstract void Write(TableHeapBuffer buffer);

		// Token: 0x06000408 RID: 1032
		public abstract void Sort();
	}
}
