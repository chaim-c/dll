using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200005A RID: 90
	public class MissionCrimeHandler : MissionLogic
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00019C28 File Offset: 0x00017E28
		protected override void OnEndMission()
		{
			if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement.IsFortification)
			{
				IFaction mapFaction = Settlement.CurrentSettlement.MapFaction;
				if (!Hero.MainHero.IsPrisoner && !Campaign.Current.IsMainHeroDisguised && !mapFaction.IsBanditFaction && Campaign.Current.Models.CrimeModel.IsPlayerCrimeRatingSevere(mapFaction.MapFaction))
				{
					Campaign.Current.GameMenuManager.SetNextMenu("fortification_crime_rating");
				}
			}
		}
	}
}
