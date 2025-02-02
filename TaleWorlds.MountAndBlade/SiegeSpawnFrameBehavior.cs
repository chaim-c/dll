using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BF RID: 703
	public class SiegeSpawnFrameBehavior : SpawnFrameBehaviorBase
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x00091F84 File Offset: 0x00090184
		public override void Initialize()
		{
			base.Initialize();
			this._spawnPointsByTeam = new List<GameEntity>[2];
			this._spawnZonesByTeam = new List<GameEntity>[2];
			this._spawnPointsByTeam[1] = (from x in this.SpawnPoints
			where x.HasTag("attacker")
			select x).ToList<GameEntity>();
			this._spawnPointsByTeam[0] = (from x in this.SpawnPoints
			where x.HasTag("defender")
			select x).ToList<GameEntity>();
			this._spawnZonesByTeam[1] = (from sz in (from sp in this._spawnPointsByTeam[1]
			select sp.Parent).Distinct<GameEntity>()
			where sz != null
			select sz).ToList<GameEntity>();
			this._spawnZonesByTeam[0] = (from sz in (from sp in this._spawnPointsByTeam[0]
			select sp.Parent).Distinct<GameEntity>()
			where sz != null
			select sz).ToList<GameEntity>();
			this._activeSpawnZoneIndex = 0;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000920E8 File Offset: 0x000902E8
		public override MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn)
		{
			List<GameEntity> list = new List<GameEntity>();
			GameEntity gameEntity = this._spawnZonesByTeam[(int)team.Side].First((GameEntity sz) => sz.HasTag(string.Format("{0}{1}", "sp_zone_", this._activeSpawnZoneIndex)));
			list.AddRange(from sp in gameEntity.GetChildren()
			where sp.HasTag("spawnpoint")
			select sp);
			return base.GetSpawnFrameFromSpawnPoints(list, team, hasMount);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00092153 File Offset: 0x00090353
		public void OnFlagDeactivated(FlagCapturePoint flag)
		{
			this._activeSpawnZoneIndex++;
		}

		// Token: 0x04000E5B RID: 3675
		public const string SpawnZoneTagAffix = "sp_zone_";

		// Token: 0x04000E5C RID: 3676
		public const string SpawnZoneEnableTagAffix = "enable_";

		// Token: 0x04000E5D RID: 3677
		public const string SpawnZoneDisableTagAffix = "disable_";

		// Token: 0x04000E5E RID: 3678
		public const int StartingActiveSpawnZoneIndex = 0;

		// Token: 0x04000E5F RID: 3679
		private List<GameEntity>[] _spawnPointsByTeam;

		// Token: 0x04000E60 RID: 3680
		private List<GameEntity>[] _spawnZonesByTeam;

		// Token: 0x04000E61 RID: 3681
		private int _activeSpawnZoneIndex;
	}
}
