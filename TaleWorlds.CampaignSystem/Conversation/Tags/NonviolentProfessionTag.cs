using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200022A RID: 554
	public class NonviolentProfessionTag : ConversationTag
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00089240 File Offset: 0x00087440
		public override string StringId
		{
			get
			{
				return "NonviolentProfessionTag";
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00089247 File Offset: 0x00087447
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && (character.Occupation == Occupation.Artisan || character.Occupation == Occupation.Merchant || character.Occupation == Occupation.Headman);
		}

		// Token: 0x040009B1 RID: 2481
		public const string Id = "NonviolentProfessionTag";
	}
}
