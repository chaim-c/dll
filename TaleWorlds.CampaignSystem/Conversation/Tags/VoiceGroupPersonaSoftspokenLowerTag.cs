using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200024B RID: 587
	public class VoiceGroupPersonaSoftspokenLowerTag : ConversationTag
	{
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0008979B File Offset: 0x0008799B
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaSoftspokenLowerTag";
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000897A2 File Offset: 0x000879A2
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaSoftspoken && ConversationTagHelper.UsesLowRegister(character);
		}

		// Token: 0x040009D2 RID: 2514
		public const string Id = "VoiceGroupPersonaSoftspokenLowerTag";
	}
}
