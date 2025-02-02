using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000226 RID: 550
	public class ArtisanNotableTypeTag : ConversationTag
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x00089166 File Offset: 0x00087366
		public override string StringId
		{
			get
			{
				return "ArtisanNotableTypeTag";
			}
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0008916D File Offset: 0x0008736D
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.Occupation == Occupation.Artisan;
		}

		// Token: 0x040009AD RID: 2477
		public const string Id = "ArtisanNotableTypeTag";
	}
}
