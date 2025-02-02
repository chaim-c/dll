﻿using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003B1 RID: 945
	public class NotablePowerManagementBehavior : CampaignBehaviorBase
	{
		// Token: 0x060039F7 RID: 14839 RVA: 0x001107C8 File Offset: 0x0010E9C8
		public override void RegisterEvents()
		{
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.DailyTickHero));
			CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, RaidEventComponent>(this.OnRaidCompleted));
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0011081A File Offset: 0x0010EA1A
		private void OnHeroCreated(Hero hero, bool isMaternal)
		{
			if (hero.IsNotable)
			{
				hero.AddPower((float)Campaign.Current.Models.NotablePowerModel.GetInitialPower());
			}
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x00110840 File Offset: 0x0010EA40
		private void DailyTickHero(Hero hero)
		{
			if (hero.IsAlive && hero.IsNotable)
			{
				hero.AddPower(Campaign.Current.Models.NotablePowerModel.CalculateDailyPowerChangeForHero(hero, false).ResultNumber);
			}
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x00110884 File Offset: 0x0010EA84
		private void OnRaidCompleted(BattleSideEnum winnerSide, RaidEventComponent mapEvent)
		{
			foreach (Hero hero in mapEvent.MapEventSettlement.Notables)
			{
				hero.AddPower(-5f);
			}
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x001108E0 File Offset: 0x0010EAE0
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
