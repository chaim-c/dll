using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000229 RID: 553
	public class InHomeSettlementTag : ConversationTag
	{
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x000891D7 File Offset: 0x000873D7
		public override string StringId
		{
			get
			{
				return "InHomeSettlementTag";
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000891E0 File Offset: 0x000873E0
		public override bool IsApplicableTo(CharacterObject character)
		{
			return (character.IsHero && Settlement.CurrentSettlement != null && character.HeroObject.HomeSettlement == Settlement.CurrentSettlement) || (character.IsHero && Settlement.CurrentSettlement != null && Settlement.CurrentSettlement.OwnerClan.Leader == character.HeroObject);
		}

		// Token: 0x040009B0 RID: 2480
		public const string Id = "InHomeSettlementTag";
	}
}
