using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x020001F9 RID: 505
	public class TribalRegisterTag : ConversationTag
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0008865C File Offset: 0x0008685C
		public override string StringId
		{
			get
			{
				return "TribalRegisterTag";
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00088663 File Offset: 0x00086863
		public override bool IsApplicableTo(CharacterObject character)
		{
			return !ConversationTagHelper.UsesHighRegister(character) && !ConversationTagHelper.UsesLowRegister(character);
		}

		// Token: 0x0400097F RID: 2431
		public const string Id = "TribalRegisterTag";
	}
}
