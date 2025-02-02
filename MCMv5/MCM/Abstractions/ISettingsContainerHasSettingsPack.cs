using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	public interface ISettingsContainerHasSettingsPack
	{
		// Token: 0x0600019C RID: 412
		IEnumerable<SettingSnapshot> SaveAvailableSnapshots();

		// Token: 0x0600019D RID: 413
		IEnumerable<BaseSettings> LoadAvailableSnapshots(IEnumerable<SettingSnapshot> snapshots);
	}
}
