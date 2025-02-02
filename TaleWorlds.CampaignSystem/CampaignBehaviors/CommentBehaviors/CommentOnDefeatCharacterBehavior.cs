using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F0 RID: 1008
	public class CommentOnDefeatCharacterBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E9A RID: 16026 RVA: 0x001339B1 File Offset: 0x00131BB1
		public override void RegisterEvents()
		{
			CampaignEvents.CharacterDefeated.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnCharacterDefeated));
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x001339CA File Offset: 0x00131BCA
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x001339CC File Offset: 0x00131BCC
		private void OnCharacterDefeated(Hero winner, Hero loser)
		{
			LogEntry.AddLogEntry(new DefeatCharacterLogEntry(winner, loser));
		}
	}
}
