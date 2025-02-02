using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000244 RID: 580
	public class VoiceGroupPersonaCurtUpperTag : ConversationTag
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00089691 File Offset: 0x00087891
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaCurtUpperTag";
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00089698 File Offset: 0x00087898
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaCurt && ConversationTagHelper.UsesHighRegister(character);
		}

		// Token: 0x040009CB RID: 2507
		public const string Id = "VoiceGroupPersonaCurtUpperTag";
	}
}
