using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000216 RID: 534
	public class ImpoliteTag : ConversationTag
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x00088DA2 File Offset: 0x00086FA2
		public override string StringId
		{
			get
			{
				return "ImpoliteTag";
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00088DAC File Offset: 0x00086FAC
		public override bool IsApplicableTo(CharacterObject character)
		{
			if (!character.IsHero)
			{
				return false;
			}
			int heroRelation = CharacterRelationManager.GetHeroRelation(character.HeroObject, Hero.MainHero);
			return (character.HeroObject.IsLord || character.HeroObject.IsMerchant || character.HeroObject.IsGangLeader) && Clan.PlayerClan.Renown < 100f && heroRelation < 1 && character.GetTraitLevel(DefaultTraits.Mercy) + character.GetTraitLevel(DefaultTraits.Generosity) < 0;
		}

		// Token: 0x0400099C RID: 2460
		public const string Id = "ImpoliteTag";
	}
}
