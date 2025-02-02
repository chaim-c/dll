using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000080 RID: 128
	public struct SingleplayerBattleSceneData
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0004A511 File Offset: 0x00048711
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0004A519 File Offset: 0x00048719
		public string SceneID { get; private set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0004A522 File Offset: 0x00048722
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0004A52A File Offset: 0x0004872A
		public TerrainType Terrain { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0004A533 File Offset: 0x00048733
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x0004A53B File Offset: 0x0004873B
		public List<TerrainType> TerrainTypes { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0004A544 File Offset: 0x00048744
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0004A54C File Offset: 0x0004874C
		public ForestDensity ForestDensity { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0004A555 File Offset: 0x00048755
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0004A55D File Offset: 0x0004875D
		public List<int> MapIndices { get; private set; }

		// Token: 0x06001050 RID: 4176 RVA: 0x0004A566 File Offset: 0x00048766
		public SingleplayerBattleSceneData(string sceneID, TerrainType terrain, List<TerrainType> terrainTypes, ForestDensity forestDensity, List<int> mapIndices)
		{
			this.SceneID = sceneID;
			this.Terrain = terrain;
			this.TerrainTypes = terrainTypes;
			this.ForestDensity = forestDensity;
			this.MapIndices = mapIndices;
		}
	}
}
