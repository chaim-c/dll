using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000AF RID: 175
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public sealed class EmptyPerSaveSettings : PerSaveSettings<EmptyPerSaveSettings>
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000B360 File Offset: 0x00009560
		public override string Id
		{
			get
			{
				return "empty_persave_v1";
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000B367 File Offset: 0x00009567
		public override string DisplayName
		{
			get
			{
				return "Empty PerSave Settings";
			}
		}
	}
}
