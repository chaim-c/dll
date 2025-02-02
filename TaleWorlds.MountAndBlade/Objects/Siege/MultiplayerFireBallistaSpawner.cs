using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000392 RID: 914
	public class MultiplayerFireBallistaSpawner : BallistaSpawner
	{
		// Token: 0x060031B0 RID: 12720 RVA: 0x000CD057 File Offset: 0x000CB257
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}
	}
}
