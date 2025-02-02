using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x0200001A RID: 26
	public class SettlementNameplatePartyMarkersVM : ViewModel
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000BD09 File Offset: 0x00009F09
		public SettlementNameplatePartyMarkersVM(Settlement settlement)
		{
			this._settlement = settlement;
			this.PartiesInSettlement = new MBBindingList<SettlementNameplatePartyMarkerItemVM>();
			this._itemComparer = new SettlementNameplatePartyMarkersVM.PartyMarkerItemComparer();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BD30 File Offset: 0x00009F30
		private void PopulatePartyList()
		{
			this.PartiesInSettlement.Clear();
			foreach (MobileParty mobileParty in from p in this._settlement.Parties
			where this.IsMobilePartyValid(p)
			select p)
			{
				this.PartiesInSettlement.Add(new SettlementNameplatePartyMarkerItemVM(mobileParty));
			}
			this.PartiesInSettlement.Sort(this._itemComparer);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000BDBC File Offset: 0x00009FBC
		private bool IsMobilePartyValid(MobileParty party)
		{
			if (party.IsGarrison || party.IsMilitia)
			{
				return false;
			}
			if (party.IsMainParty && (!party.IsMainParty || Campaign.Current.IsMainHeroDisguised))
			{
				return false;
			}
			if (party.Army != null)
			{
				Army army = party.Army;
				return army != null && army.LeaderParty.IsMainParty && !Campaign.Current.IsMainHeroDisguised;
			}
			return true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BE2C File Offset: 0x0000A02C
		private void OnSettlementLeft(MobileParty party, Settlement settlement)
		{
			if (settlement == this._settlement)
			{
				SettlementNameplatePartyMarkerItemVM settlementNameplatePartyMarkerItemVM = this.PartiesInSettlement.SingleOrDefault((SettlementNameplatePartyMarkerItemVM p) => p.Party == party);
				if (settlementNameplatePartyMarkerItemVM != null)
				{
					this.PartiesInSettlement.Remove(settlementNameplatePartyMarkerItemVM);
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BE78 File Offset: 0x0000A078
		private void OnSettlementEntered(MobileParty partyEnteredSettlement, Settlement settlement, Hero leader)
		{
			if (settlement == this._settlement && partyEnteredSettlement != null && this.PartiesInSettlement.SingleOrDefault((SettlementNameplatePartyMarkerItemVM p) => p.Party == partyEnteredSettlement) == null && this.IsMobilePartyValid(partyEnteredSettlement))
			{
				this.PartiesInSettlement.Add(new SettlementNameplatePartyMarkerItemVM(partyEnteredSettlement));
				this.PartiesInSettlement.Sort(this._itemComparer);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BEF1 File Offset: 0x0000A0F1
		private void OnMapEventEnded(MapEvent obj)
		{
			if (obj.MapEventSettlement != null && obj.MapEventSettlement == this._settlement)
			{
				this.PopulatePartyList();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BF10 File Offset: 0x0000A110
		public void RegisterEvents()
		{
			if (!this._eventsRegistered)
			{
				this.PopulatePartyList();
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
				this._eventsRegistered = true;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BF77 File Offset: 0x0000A177
		public void UnloadEvents()
		{
			if (this._eventsRegistered)
			{
				CampaignEvents.SettlementEntered.ClearListeners(this);
				CampaignEvents.OnSettlementLeftEvent.ClearListeners(this);
				CampaignEvents.MapEventEnded.ClearListeners(this);
				this.PartiesInSettlement.Clear();
				this._eventsRegistered = false;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public MBBindingList<SettlementNameplatePartyMarkerItemVM> PartiesInSettlement
		{
			get
			{
				return this._partiesInSettlement;
			}
			set
			{
				if (value != this._partiesInSettlement)
				{
					this._partiesInSettlement = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNameplatePartyMarkerItemVM>>(value, "PartiesInSettlement");
				}
			}
		}

		// Token: 0x04000128 RID: 296
		private Settlement _settlement;

		// Token: 0x04000129 RID: 297
		private bool _eventsRegistered;

		// Token: 0x0400012A RID: 298
		private SettlementNameplatePartyMarkersVM.PartyMarkerItemComparer _itemComparer;

		// Token: 0x0400012B RID: 299
		private MBBindingList<SettlementNameplatePartyMarkerItemVM> _partiesInSettlement;

		// Token: 0x0200006B RID: 107
		public class PartyMarkerItemComparer : IComparer<SettlementNameplatePartyMarkerItemVM>
		{
			// Token: 0x0600051F RID: 1311 RVA: 0x000148C4 File Offset: 0x00012AC4
			public int Compare(SettlementNameplatePartyMarkerItemVM x, SettlementNameplatePartyMarkerItemVM y)
			{
				return x.SortIndex.CompareTo(y.SortIndex);
			}
		}
	}
}
