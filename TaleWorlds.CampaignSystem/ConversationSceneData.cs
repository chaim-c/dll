using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000081 RID: 129
	public struct ConversationSceneData
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0004A58D File Offset: 0x0004878D
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x0004A595 File Offset: 0x00048795
		public string SceneID { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0004A59E File Offset: 0x0004879E
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0004A5A6 File Offset: 0x000487A6
		public TerrainType Terrain { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0004A5AF File Offset: 0x000487AF
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x0004A5B7 File Offset: 0x000487B7
		public List<TerrainType> TerrainTypes { get; private set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0004A5C0 File Offset: 0x000487C0
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x0004A5C8 File Offset: 0x000487C8
		public ForestDensity ForestDensity { get; private set; }

		// Token: 0x06001059 RID: 4185 RVA: 0x0004A5D1 File Offset: 0x000487D1
		public ConversationSceneData(string sceneID, TerrainType terrain, List<TerrainType> terrainTypes, ForestDensity forestDensity)
		{
			this.SceneID = sceneID;
			this.Terrain = terrain;
			this.TerrainTypes = terrainTypes;
			this.ForestDensity = forestDensity;
		}
	}
}
