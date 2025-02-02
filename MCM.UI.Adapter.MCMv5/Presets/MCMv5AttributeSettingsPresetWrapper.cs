using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.UI.Adapter.MCMv5.Base;

namespace MCM.UI.Adapter.MCMv5.Presets
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class MCMv5AttributeSettingsPresetWrapper : SettingsPresetWrapper<MCMv5AttributeSettingsWrapper>
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002AF7 File Offset: 0x00000CF7
		[NullableContext(2)]
		public MCMv5AttributeSettingsPresetWrapper(object @object) : base(@object)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B02 File Offset: 0x00000D02
		protected override MCMv5AttributeSettingsWrapper Create([Nullable(2)] object @object)
		{
			return new MCMv5AttributeSettingsWrapper(@object);
		}
	}
}
