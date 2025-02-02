﻿using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x020001FA RID: 506
	public class PlayerIsEnemyTag : ConversationTag
	{
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00088680 File Offset: 0x00086880
		public override string StringId
		{
			get
			{
				return "PlayerIsEnemyTag";
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00088687 File Offset: 0x00086887
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && FactionManager.IsAtWarAgainstFaction(character.HeroObject.MapFaction, Hero.MainHero.MapFaction);
		}

		// Token: 0x04000980 RID: 2432
		public const string Id = "PlayerIsEnemyTag";
	}
}
