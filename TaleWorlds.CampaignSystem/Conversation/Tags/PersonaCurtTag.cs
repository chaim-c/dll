using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200023D RID: 573
	public class PersonaCurtTag : ConversationTag
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x00089581 File Offset: 0x00087781
		public override string StringId
		{
			get
			{
				return "PersonaCurtTag";
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00089588 File Offset: 0x00087788
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.GetPersona() == DefaultTraits.PersonaCurt;
		}

		// Token: 0x040009C4 RID: 2500
		public const string Id = "PersonaCurtTag";
	}
}
