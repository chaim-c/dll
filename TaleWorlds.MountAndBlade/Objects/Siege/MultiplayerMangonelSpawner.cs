using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000395 RID: 917
	public class MultiplayerMangonelSpawner : MangonelSpawner
	{
		// Token: 0x060031B6 RID: 12726 RVA: 0x000CD09C File Offset: 0x000CB29C
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}
	}
}
