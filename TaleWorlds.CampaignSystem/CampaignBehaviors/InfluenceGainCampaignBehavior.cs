﻿using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200039F RID: 927
	public class InfluenceGainCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600379A RID: 14234 RVA: 0x000FB126 File Offset: 0x000F9326
		public override void RegisterEvents()
		{
			CampaignEvents.OnPrisonerDonatedToSettlementEvent.AddNonSerializedListener(this, new Action<MobileParty, FlattenedTroopRoster, Settlement>(this.OnPrisonerDonatedToSettlement));
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000FB13F File Offset: 0x000F933F
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000FB144 File Offset: 0x000F9344
		private void OnPrisonerDonatedToSettlement(MobileParty donatingParty, FlattenedTroopRoster donatedPrisoners, Settlement donatedSettlement)
		{
			if (donatedSettlement.OwnerClan != Clan.PlayerClan || donatingParty.ActualClan != Clan.PlayerClan)
			{
				float num = 0f;
				foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in donatedPrisoners)
				{
					num += Campaign.Current.Models.PrisonerDonationModel.CalculateInfluenceGainAfterPrisonerDonation(donatingParty.Party, flattenedTroopRosterElement.Troop, donatedSettlement);
				}
				GainKingdomInfluenceAction.ApplyForDonatePrisoners(donatingParty, num);
			}
		}
	}
}
