using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x0200001B RID: 27
	public class SettlementNameplatesVM : ViewModel
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public SettlementNameplatesVM(Camera mapCamera, Action<Vec2> fastMoveCameraToPosition)
		{
			this.Nameplates = new MBBindingList<SettlementNameplateVM>();
			this._mapCamera = mapCamera;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			CampaignEvents.PartyVisibilityChangedEvent.AddNonSerializedListener(this, new Action<PartyBase>(this.OnPartyBaseVisibilityChange));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnPeaceDeclared));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangeKingdom));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventStartedOnSettlement));
			CampaignEvents.OnSiegeEventEndedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeEventEndedOnSettlement));
			CampaignEvents.RebelliousClanDisbandedAtSettlement.AddNonSerializedListener(this, new Action<Settlement, Clan>(this.OnRebelliousClanDisbandedAtSettlement));
			this.UpdateNameplateAuxMTPredicate = new TWParallel.ParallelForAuxPredicate(this.UpdateNameplateAuxMT);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Nameplates.ApplyActionOnAllItems(delegate(SettlementNameplateVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C10C File Offset: 0x0000A30C
		public void Initialize(IEnumerable<Tuple<Settlement, GameEntity>> settlements)
		{
			IEnumerable<Tuple<Settlement, GameEntity>> enumerable = from x in settlements
			where !x.Item1.IsHideout && !(x.Item1.SettlementComponent is RetirementSettlementComponent)
			select x;
			this._allHideouts = from x in settlements
			where x.Item1.IsHideout && !(x.Item1.SettlementComponent is RetirementSettlementComponent)
			select x;
			this._allRetreats = from x in settlements
			where !x.Item1.IsHideout && x.Item1.SettlementComponent is RetirementSettlementComponent
			select x;
			foreach (Tuple<Settlement, GameEntity> tuple in enumerable)
			{
				SettlementNameplateVM item = new SettlementNameplateVM(tuple.Item1, tuple.Item2, this._mapCamera, this._fastMoveCameraToPosition);
				this.Nameplates.Add(item);
			}
			foreach (Tuple<Settlement, GameEntity> tuple2 in this._allHideouts)
			{
				if (tuple2.Item1.Hideout.IsSpotted)
				{
					SettlementNameplateVM item2 = new SettlementNameplateVM(tuple2.Item1, tuple2.Item2, this._mapCamera, this._fastMoveCameraToPosition);
					this.Nameplates.Add(item2);
				}
			}
			foreach (Tuple<Settlement, GameEntity> tuple3 in this._allRetreats)
			{
				RetirementSettlementComponent retirementSettlementComponent;
				if ((retirementSettlementComponent = (tuple3.Item1.SettlementComponent as RetirementSettlementComponent)) != null)
				{
					if (retirementSettlementComponent.IsSpotted)
					{
						SettlementNameplateVM item3 = new SettlementNameplateVM(tuple3.Item1, tuple3.Item2, this._mapCamera, this._fastMoveCameraToPosition);
						this.Nameplates.Add(item3);
					}
				}
				else
				{
					Debug.FailedAssert("A seetlement which is IsRetreat doesn't have a retirement component.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Nameplate\\SettlementNameplatesVM.cs", "Initialize", 83);
				}
			}
			foreach (SettlementNameplateVM settlementNameplateVM in this.Nameplates)
			{
				Settlement settlement = settlementNameplateVM.Settlement;
				if (((settlement != null) ? settlement.SiegeEvent : null) != null)
				{
					SettlementNameplateVM settlementNameplateVM2 = settlementNameplateVM;
					Settlement settlement2 = settlementNameplateVM.Settlement;
					settlementNameplateVM2.OnSiegeEventStartedOnSettlement((settlement2 != null) ? settlement2.SiegeEvent : null);
				}
				else if (settlementNameplateVM.Settlement.IsTown || settlementNameplateVM.Settlement.IsCastle)
				{
					Clan ownerClan = settlementNameplateVM.Settlement.OwnerClan;
					if (ownerClan != null && ownerClan.IsRebelClan)
					{
						settlementNameplateVM.OnRebelliousClanFormed(settlementNameplateVM.Settlement.OwnerClan);
					}
				}
			}
			this.RefreshRelationsOfNameplates();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		private void UpdateNameplateAuxMT(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this.Nameplates[i].UpdateNameplateMT(this._cachedCameraPosition);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public void Update()
		{
			this._cachedCameraPosition = this._mapCamera.Position;
			TWParallel.For(0, this.Nameplates.Count, this.UpdateNameplateAuxMTPredicate, 16);
			for (int i = 0; i < this.Nameplates.Count; i++)
			{
				this.Nameplates[i].RefreshBindValues();
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C450 File Offset: 0x0000A650
		private void OnSiegeEventStartedOnSettlement(SiegeEvent siegeEvent)
		{
			SettlementNameplateVM settlementNameplateVM = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == siegeEvent.BesiegedSettlement);
			if (settlementNameplateVM == null)
			{
				return;
			}
			settlementNameplateVM.OnSiegeEventStartedOnSettlement(siegeEvent);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C494 File Offset: 0x0000A694
		private void OnSiegeEventEndedOnSettlement(SiegeEvent siegeEvent)
		{
			SettlementNameplateVM settlementNameplateVM = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == siegeEvent.BesiegedSettlement);
			if (settlementNameplateVM == null)
			{
				return;
			}
			settlementNameplateVM.OnSiegeEventEndedOnSettlement(siegeEvent);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		private void OnMapEventStartedOnSettlement(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			SettlementNameplateVM settlementNameplateVM = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == mapEvent.MapEventSettlement);
			if (settlementNameplateVM == null)
			{
				return;
			}
			settlementNameplateVM.OnMapEventStartedOnSettlement(mapEvent);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C51C File Offset: 0x0000A71C
		private void OnMapEventEndedOnSettlement(MapEvent mapEvent)
		{
			SettlementNameplateVM settlementNameplateVM = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == mapEvent.MapEventSettlement);
			if (settlementNameplateVM == null)
			{
				return;
			}
			settlementNameplateVM.OnMapEventEndedOnSettlement();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C558 File Offset: 0x0000A758
		private void OnPartyBaseVisibilityChange(PartyBase party)
		{
			if (party.IsSettlement)
			{
				Tuple<Settlement, GameEntity> desiredSettlementTuple = null;
				if (party.Settlement.IsHideout)
				{
					desiredSettlementTuple = this._allHideouts.SingleOrDefault((Tuple<Settlement, GameEntity> h) => h.Item1.Hideout == party.Settlement.Hideout);
				}
				else if (party.Settlement.SettlementComponent is RetirementSettlementComponent)
				{
					desiredSettlementTuple = this._allRetreats.SingleOrDefault((Tuple<Settlement, GameEntity> h) => h.Item1.SettlementComponent as RetirementSettlementComponent == party.Settlement.SettlementComponent as RetirementSettlementComponent);
				}
				else
				{
					Debug.FailedAssert("We don't support hiding non retreat or non hideout settlements.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Nameplate\\SettlementNameplatesVM.cs", "OnPartyBaseVisibilityChange", 176);
				}
				if (desiredSettlementTuple != null)
				{
					SettlementNameplateVM settlementNameplateVM = this.Nameplates.SingleOrDefault((SettlementNameplateVM n) => n.Settlement == desiredSettlementTuple.Item1);
					if (party.IsVisible && settlementNameplateVM == null)
					{
						SettlementNameplateVM settlementNameplateVM2 = new SettlementNameplateVM(desiredSettlementTuple.Item1, desiredSettlementTuple.Item2, this._mapCamera, this._fastMoveCameraToPosition);
						this.Nameplates.Add(settlementNameplateVM2);
						settlementNameplateVM2.RefreshRelationStatus();
						return;
					}
					if (!party.IsVisible && settlementNameplateVM != null)
					{
						this.Nameplates.Remove(settlementNameplateVM);
					}
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C69A File Offset: 0x0000A89A
		private void OnPeaceDeclared(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			this.OnPeaceOrWarDeclared(faction1, faction2);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail arg3)
		{
			this.OnPeaceOrWarDeclared(faction1, faction2);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C6AE File Offset: 0x0000A8AE
		private void OnPeaceOrWarDeclared(IFaction faction1, IFaction faction2)
		{
			if (faction1 == Hero.MainHero.MapFaction || faction1 == Hero.MainHero.Clan || faction2 == Hero.MainHero.MapFaction || faction2 == Hero.MainHero.Clan)
			{
				this.RefreshRelationsOfNameplates();
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C6EA File Offset: 0x0000A8EA
		private void OnClanChangeKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			this.RefreshRelationsOfNameplates();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero previousOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			SettlementNameplateVM settlementNameplateVM = this.Nameplates.SingleOrDefault((SettlementNameplateVM n) => n.Settlement == settlement);
			if (settlementNameplateVM != null)
			{
				settlementNameplateVM.RefreshDynamicProperties(true);
			}
			if (settlementNameplateVM != null)
			{
				settlementNameplateVM.RefreshRelationStatus();
			}
			using (List<Village>.Enumerator enumerator = settlement.BoundVillages.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Village village = enumerator.Current;
					SettlementNameplateVM settlementNameplateVM2 = this.Nameplates.SingleOrDefault((SettlementNameplateVM n) => n.Settlement.IsVillage && n.Settlement.Village == village);
					if (settlementNameplateVM2 != null)
					{
						settlementNameplateVM2.RefreshDynamicProperties(true);
					}
					if (settlementNameplateVM2 != null)
					{
						settlementNameplateVM2.RefreshRelationStatus();
					}
				}
			}
			if (detail != ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.ByRebellion)
			{
				if (previousOwner != null && previousOwner.IsRebel)
				{
					SettlementNameplateVM settlementNameplateVM3 = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == settlement);
					if (settlementNameplateVM3 == null)
					{
						return;
					}
					settlementNameplateVM3.OnRebelliousClanDisbanded(previousOwner.Clan);
				}
				return;
			}
			SettlementNameplateVM settlementNameplateVM4 = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == settlement);
			if (settlementNameplateVM4 == null)
			{
				return;
			}
			settlementNameplateVM4.OnRebelliousClanFormed(newOwner.Clan);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C820 File Offset: 0x0000AA20
		private void OnRebelliousClanDisbandedAtSettlement(Settlement settlement, Clan clan)
		{
			SettlementNameplateVM settlementNameplateVM = this.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == settlement);
			if (settlementNameplateVM == null)
			{
				return;
			}
			settlementNameplateVM.OnRebelliousClanDisbanded(clan);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C85C File Offset: 0x0000AA5C
		private void RefreshRelationsOfNameplates()
		{
			foreach (SettlementNameplateVM settlementNameplateVM in this.Nameplates)
			{
				settlementNameplateVM.RefreshRelationStatus();
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		private void RefreshDynamicPropertiesOfNameplates()
		{
			foreach (SettlementNameplateVM settlementNameplateVM in this.Nameplates)
			{
				settlementNameplateVM.RefreshDynamicProperties(false);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.PartyVisibilityChangedEvent.ClearListeners(this);
			CampaignEvents.WarDeclared.ClearListeners(this);
			CampaignEvents.MakePeace.ClearListeners(this);
			CampaignEvents.OnClanChangedKingdomEvent.ClearListeners(this);
			CampaignEvents.OnSettlementOwnerChangedEvent.ClearListeners(this);
			CampaignEvents.OnSiegeEventStartedEvent.ClearListeners(this);
			CampaignEvents.OnSiegeEventEndedEvent.ClearListeners(this);
			CampaignEvents.RebelliousClanDisbandedAtSettlement.ClearListeners(this);
			this.Nameplates.ApplyActionOnAllItems(delegate(SettlementNameplateVM n)
			{
				n.OnFinalize();
			});
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000C989 File Offset: 0x0000AB89
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000C991 File Offset: 0x0000AB91
		[DataSourceProperty]
		public MBBindingList<SettlementNameplateVM> Nameplates
		{
			get
			{
				return this._nameplates;
			}
			set
			{
				if (this._nameplates != value)
				{
					this._nameplates = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNameplateVM>>(value, "Nameplates");
				}
			}
		}

		// Token: 0x0400012C RID: 300
		private readonly Camera _mapCamera;

		// Token: 0x0400012D RID: 301
		private Vec3 _cachedCameraPosition;

		// Token: 0x0400012E RID: 302
		private readonly TWParallel.ParallelForAuxPredicate UpdateNameplateAuxMTPredicate;

		// Token: 0x0400012F RID: 303
		private readonly Action<Vec2> _fastMoveCameraToPosition;

		// Token: 0x04000130 RID: 304
		private IEnumerable<Tuple<Settlement, GameEntity>> _allHideouts;

		// Token: 0x04000131 RID: 305
		private IEnumerable<Tuple<Settlement, GameEntity>> _allRetreats;

		// Token: 0x04000132 RID: 306
		private MBBindingList<SettlementNameplateVM> _nameplates;
	}
}
