using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using MCM.UI.Adapter.MCMv5.Presets;

namespace MCM.UI.Adapter.MCMv5.Base
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MCMv5AttributeSettingsWrapper : SettingsWrapper
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002B1D File Offset: 0x00000D1D
		public override string DiscoveryType
		{
			get
			{
				return "mcm_v5_attributes";
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B24 File Offset: 0x00000D24
		[NullableContext(2)]
		public MCMv5AttributeSettingsWrapper(object @object) : base(@object)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B2F File Offset: 0x00000D2F
		protected override BaseSettings Create([Nullable(2)] object @object)
		{
			return new MCMv5AttributeSettingsWrapper(@object);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B37 File Offset: 0x00000D37
		protected override ISettingsPreset CreatePreset([Nullable(2)] object @object)
		{
			return new MCMv5AttributeSettingsPresetWrapper(@object);
		}
	}
}
