using System;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace SandBox.View.Map
{
	// Token: 0x0200003C RID: 60
	public class MapConversationTableauData
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000151CC File Offset: 0x000133CC
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000151D4 File Offset: 0x000133D4
		public ConversationCharacterData PlayerCharacterData { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000151DD File Offset: 0x000133DD
		// (set) Token: 0x0600020F RID: 527 RVA: 0x000151E5 File Offset: 0x000133E5
		public ConversationCharacterData ConversationPartnerData { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000151EE File Offset: 0x000133EE
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000151F6 File Offset: 0x000133F6
		public TerrainType ConversationTerrainType { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000151FF File Offset: 0x000133FF
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00015207 File Offset: 0x00013407
		public bool IsCurrentTerrainUnderSnow { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00015210 File Offset: 0x00013410
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00015218 File Offset: 0x00013418
		public bool IsInside { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00015221 File Offset: 0x00013421
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00015229 File Offset: 0x00013429
		public bool IsSnowing { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00015232 File Offset: 0x00013432
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0001523A File Offset: 0x0001343A
		public bool IsRaining { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00015243 File Offset: 0x00013443
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0001524B File Offset: 0x0001344B
		public float TimeOfDay { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00015254 File Offset: 0x00013454
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0001525C File Offset: 0x0001345C
		public Settlement Settlement { get; private set; }

		// Token: 0x0600021E RID: 542 RVA: 0x00015265 File Offset: 0x00013465
		private MapConversationTableauData()
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00015270 File Offset: 0x00013470
		public static MapConversationTableauData CreateFrom(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData, TerrainType terrainType, float timeOfDay, bool isCurrentTerrainUnderSnow, Settlement settlement, bool isInside, bool isRaining, bool isSnowing)
		{
			return new MapConversationTableauData
			{
				PlayerCharacterData = playerCharacterData,
				ConversationPartnerData = conversationPartnerData,
				ConversationTerrainType = terrainType,
				TimeOfDay = timeOfDay,
				IsCurrentTerrainUnderSnow = isCurrentTerrainUnderSnow,
				Settlement = settlement,
				IsInside = isInside,
				IsRaining = isRaining,
				IsSnowing = isSnowing
			};
		}
	}
}
