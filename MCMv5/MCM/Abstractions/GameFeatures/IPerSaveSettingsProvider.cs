using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base.PerSave;

namespace MCM.Abstractions.GameFeatures
{
	// Token: 0x02000074 RID: 116
	[NullableContext(1)]
	public interface IPerSaveSettingsProvider
	{
		// Token: 0x060002A4 RID: 676
		bool SaveSettings(PerSaveSettings perSaveSettings);

		// Token: 0x060002A5 RID: 677
		void LoadSettings(PerSaveSettings perSaveSettings);
	}
}
