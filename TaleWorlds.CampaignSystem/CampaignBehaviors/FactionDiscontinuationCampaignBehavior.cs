using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200038E RID: 910
	public class FactionDiscontinuationCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x000F50E0 File Offset: 0x000F32E0
		public override void RegisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.DailyTickClanEvent.AddNonSerializedListener(this, new Action<Clan>(this.DailyTickClan));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000F514C File Offset: 0x000F334C
		public void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (this._independentClans.ContainsKey(newOwner.Clan))
			{
				this._independentClans.Remove(newOwner.Clan);
			}
			if (this.CanClanBeDiscontinued(oldOwner.Clan))
			{
				this.AddIndependentClan(oldOwner.Clan);
			}
			Kingdom kingdom = oldOwner.Clan.Kingdom;
			if (kingdom != null && this.CanKingdomBeDiscontinued(kingdom))
			{
				this.DiscontinueKingdom(kingdom);
			}
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000F51BC File Offset: 0x000F33BC
		public void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			if (newKingdom == null)
			{
				if (this.CanClanBeDiscontinued(clan))
				{
					this.AddIndependentClan(clan);
				}
			}
			else if (this._independentClans.ContainsKey(clan))
			{
				this._independentClans.Remove(clan);
			}
			if (clan == Clan.PlayerClan && oldKingdom != null && this.CanKingdomBeDiscontinued(oldKingdom))
			{
				this.DiscontinueKingdom(oldKingdom);
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000F5214 File Offset: 0x000F3414
		private void DailyTickClan(Clan clan)
		{
			if (this._independentClans.ContainsKey(clan) && this._independentClans[clan].IsPast)
			{
				this.DiscontinueClan(clan);
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000F524C File Offset: 0x000F344C
		private bool CanKingdomBeDiscontinued(Kingdom kingdom)
		{
			bool flag = !kingdom.IsEliminated && kingdom != Clan.PlayerClan.Kingdom && kingdom.Settlements.IsEmpty<Settlement>();
			if (flag)
			{
				CampaignEventDispatcher.Instance.CanKingdomBeDiscontinued(kingdom, ref flag);
			}
			return flag;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000F5290 File Offset: 0x000F3490
		private void DiscontinueKingdom(Kingdom kingdom)
		{
			foreach (Clan clan in new List<Clan>(kingdom.Clans))
			{
				this.FinalizeMapEvents(clan);
				ChangeKingdomAction.ApplyByLeaveByKingdomDestruction(clan, true);
			}
			kingdom.RulingClan = null;
			DestroyKingdomAction.Apply(kingdom);
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000F52FC File Offset: 0x000F34FC
		private void FinalizeMapEvents(Clan clan)
		{
			foreach (WarPartyComponent warPartyComponent in clan.WarPartyComponents)
			{
				if (warPartyComponent.MobileParty.MapEvent != null)
				{
					warPartyComponent.MobileParty.MapEvent.FinalizeEvent();
				}
				if (warPartyComponent.MobileParty.SiegeEvent != null)
				{
					warPartyComponent.MobileParty.SiegeEvent.FinalizeSiegeEvent();
				}
			}
			foreach (Settlement settlement in clan.Settlements)
			{
				if (settlement.Party.MapEvent != null)
				{
					settlement.Party.MapEvent.FinalizeEvent();
				}
				if (settlement.Party.SiegeEvent != null)
				{
					settlement.Party.SiegeEvent.FinalizeSiegeEvent();
				}
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000F53F8 File Offset: 0x000F35F8
		private bool CanClanBeDiscontinued(Clan clan)
		{
			return clan.Kingdom == null && !clan.IsRebelClan && !clan.IsBanditFaction && !clan.IsMinorFaction && clan != Clan.PlayerClan && clan.Settlements.IsEmpty<Settlement>();
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000F542F File Offset: 0x000F362F
		private void DiscontinueClan(Clan clan)
		{
			DestroyClanAction.Apply(clan);
			this._independentClans.Remove(clan);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000F5444 File Offset: 0x000F3644
		private void AddIndependentClan(Clan clan)
		{
			if (!this._independentClans.ContainsKey(clan))
			{
				this._independentClans.Add(clan, CampaignTime.WeeksFromNow(4f));
			}
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000F546A File Offset: 0x000F366A
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Clan, CampaignTime>>("_independentClans", ref this._independentClans);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000F5480 File Offset: 0x000F3680
		private void OnGameLoadFinished()
		{
			if (MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.2", 45697))
			{
				foreach (Kingdom kingdom in Kingdom.All)
				{
					if (!kingdom.IsEliminated && this.CanKingdomBeDiscontinued(kingdom))
					{
						this.DiscontinueKingdom(kingdom);
					}
				}
			}
		}

		// Token: 0x04001165 RID: 4453
		private const float SurvivalDurationForIndependentClanInWeeks = 4f;

		// Token: 0x04001166 RID: 4454
		private Dictionary<Clan, CampaignTime> _independentClans = new Dictionary<Clan, CampaignTime>();
	}
}
