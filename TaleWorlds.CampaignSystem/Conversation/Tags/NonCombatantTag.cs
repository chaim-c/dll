﻿using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x020001F2 RID: 498
	public class NonCombatantTag : ConversationTag
	{
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0008846C File Offset: 0x0008666C
		public override string StringId
		{
			get
			{
				return "NonCombatantTag";
			}
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00088473 File Offset: 0x00086673
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.HeroObject.IsNoncombatant;
		}

		// Token: 0x04000978 RID: 2424
		public const string Id = "NonCombatantTag";
	}
}
