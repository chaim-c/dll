using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200023A RID: 570
	public class CautiousTag : ConversationTag
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000894FD File Offset: 0x000876FD
		public override string StringId
		{
			get
			{
				return "CautiousTag";
			}
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00089504 File Offset: 0x00087704
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.GetTraitLevel(DefaultTraits.Valor) < 0;
		}

		// Token: 0x040009C1 RID: 2497
		public const string Id = "CautiousTag";
	}
}
