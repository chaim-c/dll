﻿using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements.Buildings;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x020000F2 RID: 242
	public class DefaultBuildingEffectModel : BuildingEffectModel
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x0005DA08 File Offset: 0x0005BC08
		public override float GetBuildingEffectAmount(Building building, BuildingEffectEnum effect)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(building.BuildingType.GetBaseBuildingEffectAmount(effect, building.CurrentLevel), false, null);
			if (effect == BuildingEffectEnum.Foodstock && building.Town.Governor != null && building.Town.Governor.GetPerkValue(DefaultPerks.Engineering.Battlements) && (building.BuildingType == DefaultBuildingTypes.CastleGranary || building.BuildingType == DefaultBuildingTypes.SettlementGranary))
			{
				explainedNumber.Add(DefaultPerks.Engineering.Battlements.SecondaryBonus, DefaultPerks.Engineering.Battlements.Name, null);
			}
			if (building.Town.IsTown)
			{
				PerkHelper.AddPerkBonusForTown(DefaultPerks.Steward.Contractors, building.Town, ref explainedNumber);
			}
			if (building.Town.Governor != null && building.Town.Governor.GetPerkValue(DefaultPerks.Steward.MasterOfPlanning))
			{
				explainedNumber.AddFactor(DefaultPerks.Steward.MasterOfPlanning.SecondaryBonus, DefaultPerks.Steward.MasterOfPlanning.Name);
			}
			Hero governor = building.Town.Governor;
			if (governor != null && governor.GetPerkValue(DefaultPerks.Charm.PublicSpeaker) && (building.BuildingType == DefaultBuildingTypes.SettlementMarketplace || building.BuildingType == DefaultBuildingTypes.FestivalsAndGamesDaily || building.BuildingType == DefaultBuildingTypes.SettlementForum))
			{
				explainedNumber.AddFactor(DefaultPerks.Charm.PublicSpeaker.SecondaryBonus, DefaultPerks.Charm.PublicSpeaker.Name);
			}
			return explainedNumber.ResultNumber;
		}
	}
}
