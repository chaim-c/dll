using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000236 RID: 566
	public class DeviousTag : ConversationTag
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00089445 File Offset: 0x00087645
		public override string StringId
		{
			get
			{
				return "DeviousTag";
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0008944C File Offset: 0x0008764C
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.GetTraitLevel(DefaultTraits.Honor) < 0;
		}

		// Token: 0x040009BD RID: 2493
		public const string Id = "DeviousTag";
	}
}
