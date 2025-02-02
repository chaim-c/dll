using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000394 RID: 916
	public class MultiplayerFireTrebuchetSpawner : TrebuchetSpawner
	{
		// Token: 0x060031B4 RID: 12724 RVA: 0x000CD085 File Offset: 0x000CB285
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}
	}
}
