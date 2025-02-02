using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200020C RID: 524
	public class PlayerIsMaleTag : ConversationTag
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00088A86 File Offset: 0x00086C86
		public override string StringId
		{
			get
			{
				return "PlayerIsMaleTag";
			}
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00088A8D File Offset: 0x00086C8D
		public override bool IsApplicableTo(CharacterObject character)
		{
			return !Hero.MainHero.IsFemale;
		}

		// Token: 0x04000992 RID: 2450
		public const string Id = "PlayerIsMaleTag";
	}
}
