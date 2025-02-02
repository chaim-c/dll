using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000221 RID: 545
	public class FirstMeetingTag : ConversationTag
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x000890B2 File Offset: 0x000872B2
		public override string StringId
		{
			get
			{
				return "FirstMeetingTag";
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000890B9 File Offset: 0x000872B9
		public override bool IsApplicableTo(CharacterObject character)
		{
			return Campaign.Current.ConversationManager.CurrentConversationIsFirst;
		}

		// Token: 0x040009A8 RID: 2472
		public const string Id = "FirstMeetingTag";
	}
}
