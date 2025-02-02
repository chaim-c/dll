using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000233 RID: 563
	public class GenerosityTag : ConversationTag
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x000893BB File Offset: 0x000875BB
		public override string StringId
		{
			get
			{
				return "GenerosityTag";
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000893C2 File Offset: 0x000875C2
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.GetTraitLevel(DefaultTraits.Generosity) > 0;
		}

		// Token: 0x040009BA RID: 2490
		public const string Id = "GenerosityTag";
	}
}
