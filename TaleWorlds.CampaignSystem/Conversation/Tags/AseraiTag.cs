using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200022F RID: 559
	public class AseraiTag : ConversationTag
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00089313 File Offset: 0x00087513
		public override string StringId
		{
			get
			{
				return "AseraiTag";
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0008931A File Offset: 0x0008751A
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.Culture.StringId == "aserai";
		}

		// Token: 0x040009B6 RID: 2486
		public const string Id = "AseraiTag";
	}
}
