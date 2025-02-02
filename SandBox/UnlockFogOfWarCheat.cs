﻿using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace SandBox
{
	// Token: 0x02000018 RID: 24
	public class UnlockFogOfWarCheat : GameplayCheatItem
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003E7C File Offset: 0x0000207C
		public override void ExecuteCheat()
		{
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				hero.IsKnownToPlayer = true;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003ECC File Offset: 0x000020CC
		public override TextObject GetName()
		{
			return new TextObject("{=jPtG0Pu1}Unlock Fog of War", null);
		}
	}
}
