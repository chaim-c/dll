using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000224 RID: 548
	public class GangLeaderNotableTypeTag : ConversationTag
	{
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0008911C File Offset: 0x0008731C
		public override string StringId
		{
			get
			{
				return "GangLeaderNotableTypeTag";
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00089123 File Offset: 0x00087323
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.Occupation == Occupation.GangLeader;
		}

		// Token: 0x040009AB RID: 2475
		public const string Id = "GangLeaderNotableTypeTag";
	}
}
