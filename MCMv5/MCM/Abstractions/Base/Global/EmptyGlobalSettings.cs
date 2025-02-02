using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000BE RID: 190
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public sealed class EmptyGlobalSettings : GlobalSettings<EmptyGlobalSettings>
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000BD96 File Offset: 0x00009F96
		public override string Id
		{
			get
			{
				return "empty_v1";
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000BD9D File Offset: 0x00009F9D
		public override string DisplayName
		{
			get
			{
				return "Empty Global Settings";
			}
		}
	}
}
