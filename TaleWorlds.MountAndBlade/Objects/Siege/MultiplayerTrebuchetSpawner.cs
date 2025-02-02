using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000397 RID: 919
	public class MultiplayerTrebuchetSpawner : TrebuchetSpawner
	{
		// Token: 0x060031BA RID: 12730 RVA: 0x000CD0F9 File Offset: 0x000CB2F9
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}
	}
}
