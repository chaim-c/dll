using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000201 RID: 513
	public class PlayerIsMotherTag : ConversationTag
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x000887FE File Offset: 0x000869FE
		public override string StringId
		{
			get
			{
				return "PlayerIsMotherTag";
			}
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00088805 File Offset: 0x00086A05
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.Mother == Hero.MainHero;
		}

		// Token: 0x04000987 RID: 2439
		public const string Id = "PlayerIsMotherTag";
	}
}
