using System;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003B3 RID: 947
	public class NPCEquipmentsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003A11 RID: 14865 RVA: 0x00111560 File Offset: 0x0010F760
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedPartialFollowUpEndEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreatedPartialFollowUpEnd));
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x00111579 File Offset: 0x0010F779
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x0011157C File Offset: 0x0010F77C
		private void OnNewGameCreatedPartialFollowUpEnd(CampaignGameStarter starter)
		{
			foreach (CharacterObject characterObject in CharacterObject.All)
			{
				bool isTemplate = characterObject.IsTemplate;
			}
		}
	}
}
