using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000243 RID: 579
	public class VoiceGroupPersonaCurtTribalTag : ConversationTag
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0008966B File Offset: 0x0008786B
		public override string StringId
		{
			get
			{
				return "VoiceGroupPersonaCurtTribalTag";
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00089672 File Offset: 0x00087872
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetPersona() == DefaultTraits.PersonaCurt && ConversationTagHelper.TribalVoiceGroup(character);
		}

		// Token: 0x040009CA RID: 2506
		public const string Id = "VoiceGroupPersonaCurtTribalTag";
	}
}
