using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.UI.Adapter.MCMv5.Base;

namespace MCM.UI.Adapter.MCMv5.Presets
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class MCMv5FluentSettingsPresetWrapper : SettingsPresetWrapper<MCMv5FluentSettingsWrapper>
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002B0A File Offset: 0x00000D0A
		[NullableContext(2)]
		public MCMv5FluentSettingsPresetWrapper(object @object) : base(@object)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B15 File Offset: 0x00000D15
		protected override MCMv5FluentSettingsWrapper Create([Nullable(2)] object @object)
		{
			return new MCMv5FluentSettingsWrapper(@object);
		}
	}
}
