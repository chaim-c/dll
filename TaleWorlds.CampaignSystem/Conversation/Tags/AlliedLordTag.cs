using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x020001FB RID: 507
	public class AlliedLordTag : ConversationTag
	{
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x000886B5 File Offset: 0x000868B5
		public override string StringId
		{
			get
			{
				return "PlayerIsAlliedTag";
			}
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000886BC File Offset: 0x000868BC
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && FactionManager.IsAlliedWithFaction(character.HeroObject.MapFaction, Hero.MainHero.MapFaction);
		}

		// Token: 0x04000981 RID: 2433
		public const string Id = "PlayerIsAlliedTag";
	}
}
