using System;

namespace SandBox.View.Missions.SandBox
{
	// Token: 0x02000025 RID: 37
	public class SpawnPointUnits
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000CF52 File Offset: 0x0000B152
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000CF5A File Offset: 0x0000B15A
		public string SpName { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000CF63 File Offset: 0x0000B163
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000CF6B File Offset: 0x0000B16B
		public SpawnPointUnits.SceneType Place { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000CF74 File Offset: 0x0000B174
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public int MinCount { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000CF85 File Offset: 0x0000B185
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000CF8D File Offset: 0x0000B18D
		public int MaxCount { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000CF96 File Offset: 0x0000B196
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000CF9E File Offset: 0x0000B19E
		public string Type { get; private set; }

		// Token: 0x0600010E RID: 270 RVA: 0x0000CFA7 File Offset: 0x0000B1A7
		public SpawnPointUnits(string sp_name, SpawnPointUnits.SceneType place, int minCount, int maxCount)
		{
			this.SpName = sp_name;
			this.Place = place;
			this.MinCount = minCount;
			this.MaxCount = maxCount;
			this.CurrentCount = 0;
			this.SpawnedAgentCount = 0;
			this.Type = "other";
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000CFE5 File Offset: 0x0000B1E5
		public SpawnPointUnits(string sp_name, SpawnPointUnits.SceneType place, string type, int minCount, int maxCount)
		{
			this.SpName = sp_name;
			this.Place = place;
			this.Type = type;
			this.MinCount = minCount;
			this.MaxCount = maxCount;
			this.CurrentCount = 0;
			this.SpawnedAgentCount = 0;
		}

		// Token: 0x04000083 RID: 131
		public int CurrentCount;

		// Token: 0x04000085 RID: 133
		public int SpawnedAgentCount;

		// Token: 0x0200006D RID: 109
		public enum SceneType
		{
			// Token: 0x04000292 RID: 658
			Center,
			// Token: 0x04000293 RID: 659
			Tavern,
			// Token: 0x04000294 RID: 660
			VillageCenter,
			// Token: 0x04000295 RID: 661
			Arena,
			// Token: 0x04000296 RID: 662
			LordsHall,
			// Token: 0x04000297 RID: 663
			Castle,
			// Token: 0x04000298 RID: 664
			Dungeon,
			// Token: 0x04000299 RID: 665
			EmptyShop,
			// Token: 0x0400029A RID: 666
			All,
			// Token: 0x0400029B RID: 667
			NotDetermined
		}
	}
}
