using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x020001F3 RID: 499
	public class CombatantTag : ConversationTag
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00088492 File Offset: 0x00086692
		public override string StringId
		{
			get
			{
				return "CombatantTag";
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00088499 File Offset: 0x00086699
		public override bool IsApplicableTo(CharacterObject character)
		{
			return !character.IsHero || !character.HeroObject.IsNoncombatant;
		}

		// Token: 0x04000979 RID: 2425
		public const string Id = "CombatantTag";
	}
}
