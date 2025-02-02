using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000393 RID: 915
	public class MultiplayerFireMangonelSpawner : MangonelSpawner
	{
		// Token: 0x060031B2 RID: 12722 RVA: 0x000CD06E File Offset: 0x000CB26E
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}
	}
}
