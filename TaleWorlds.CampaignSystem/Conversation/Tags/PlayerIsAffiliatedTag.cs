using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x020001FC RID: 508
	public class PlayerIsAffiliatedTag : ConversationTag
	{
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000886EA File Offset: 0x000868EA
		public override string StringId
		{
			get
			{
				return "PlayerIsAffiliatedTag";
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000886F1 File Offset: 0x000868F1
		public override bool IsApplicableTo(CharacterObject character)
		{
			return Hero.MainHero.MapFaction.IsKingdomFaction;
		}

		// Token: 0x04000982 RID: 2434
		public const string Id = "PlayerIsAffiliatedTag";
	}
}
