using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200023F RID: 575
	public class PersonaSoftspokenTag : ConversationTag
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x000895D1 File Offset: 0x000877D1
		public override string StringId
		{
			get
			{
				return "PersonaSoftspokenTag";
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000895D8 File Offset: 0x000877D8
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.GetPersona() == DefaultTraits.PersonaSoftspoken;
		}

		// Token: 0x040009C6 RID: 2502
		public const string Id = "PersonaSoftspokenTag";
	}
}
