using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000390 RID: 912
	public class MultiplayerBallistaSpawner : BallistaSpawner
	{
		// Token: 0x060031AC RID: 12716 RVA: 0x000CCFE0 File Offset: 0x000CB1E0
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}
	}
}
