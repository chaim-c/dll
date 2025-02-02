using System;

namespace Mono.Cecil
{
	// Token: 0x02000067 RID: 103
	internal abstract class OneRowTable<TRow> : MetadataTable where TRow : struct
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000110AD File Offset: 0x0000F2AD
		public sealed override int Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000110B0 File Offset: 0x0000F2B0
		public sealed override void Sort()
		{
		}

		// Token: 0x040003AD RID: 941
		internal TRow row;
	}
}
