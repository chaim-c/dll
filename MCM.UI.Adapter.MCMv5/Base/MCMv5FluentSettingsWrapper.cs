using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Base.Global;
using MCM.UI.Adapter.MCMv5.Presets;

namespace MCM.UI.Adapter.MCMv5.Base
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MCMv5FluentSettingsWrapper : SettingsWrapper
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002B3F File Offset: 0x00000D3F
		public override string DiscoveryType
		{
			get
			{
				return "mcm_v5_fluent";
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B46 File Offset: 0x00000D46
		[NullableContext(2)]
		public MCMv5FluentSettingsWrapper(object @object) : base(@object)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B51 File Offset: 0x00000D51
		protected override BaseSettings Create([Nullable(2)] object @object)
		{
			return new MCMv5FluentSettingsWrapper(@object);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B59 File Offset: 0x00000D59
		protected override ISettingsPreset CreatePreset([Nullable(2)] object @object)
		{
			return new MCMv5FluentSettingsPresetWrapper(@object);
		}
	}
}
