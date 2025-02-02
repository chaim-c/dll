using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200022C RID: 556
	public class BattanianTag : ConversationTag
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000892A1 File Offset: 0x000874A1
		public override string StringId
		{
			get
			{
				return "BattanianTag";
			}
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000892A8 File Offset: 0x000874A8
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.Culture.StringId == "battania";
		}

		// Token: 0x040009B3 RID: 2483
		public const string Id = "BattanianTag";
	}
}
