using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000015 RID: 21
	public class PartyNameplatesVM : ViewModel
	{
		// Token: 0x060001EC RID: 492 RVA: 0x000098C1 File Offset: 0x00007AC1
		public PartyNameplatesVM(Camera mapCamera, Action resetCamera, Func<bool> isShowPartyNamesEnabled)
		{
			this.Nameplates = new MBBindingList<PartyNameplateVM>();
			this._nameplateComparer = new PartyNameplatesVM.NameplateDistanceComparer();
			this._mapCamera = mapCamera;
			this._resetCamera = resetCamera;
			this._isShowPartyNamesEnabled = isShowPartyNamesEnabled;
			this.RegisterEvents();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000098FA File Offset: 0x00007AFA
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Nameplates.ApplyActionOnAllItems(delegate(PartyNameplateVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000992C File Offset: 0x00007B2C
		public void Initialize()
		{
			foreach (MobileParty party in from p in MobileParty.All
			where p.IsSpotted() && p.CurrentSettlement == null
			select p)
			{
				PartyNameplateVM item = new PartyNameplateVM(party, this._mapCamera, this._resetCamera, this._isShowPartyNamesEnabled);
				this.Nameplates.Add(item);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000099B8 File Offset: 0x00007BB8
		private void OnClanChangeKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			foreach (PartyNameplateVM partyNameplateVM in from p in this.Nameplates
			where p.Party.LeaderHero != null && p.Party.LeaderHero.Clan == clan
			select p)
			{
				partyNameplateVM.RefreshDynamicProperties(true);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009A24 File Offset: 0x00007C24
		private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			for (int i = 0; i < this.Nameplates.Count; i++)
			{
				PartyNameplateVM partyNameplateVM = this.Nameplates[i];
				if (partyNameplateVM.Party == party)
				{
					partyNameplateVM.OnFinalize();
					this.Nameplates.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009A70 File Offset: 0x00007C70
		private void OnSettlementLeft(MobileParty party, Settlement settlement)
		{
			if (party.Army != null && party.Army.LeaderParty == party)
			{
				using (List<MobileParty>.Enumerator enumerator = party.Army.Parties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MobileParty armyParty = enumerator.Current;
						if (armyParty.IsSpotted() && this.Nameplates.All((PartyNameplateVM p) => p.Party != armyParty))
						{
							this.Nameplates.Add(new PartyNameplateVM(armyParty, this._mapCamera, this._resetCamera, this._isShowPartyNamesEnabled));
						}
					}
				}
				return;
			}
			if (party.IsSpotted() && this.Nameplates.All((PartyNameplateVM p) => p.Party != party))
			{
				this.Nameplates.Add(new PartyNameplateVM(party, this._mapCamera, this._resetCamera, this._isShowPartyNamesEnabled));
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009BA8 File Offset: 0x00007DA8
		private void OnPartyVisibilityChanged(PartyBase party)
		{
			if (party.IsMobile)
			{
				if (party.MobileParty.IsSpotted() && party.MobileParty.CurrentSettlement == null && this.Nameplates.All((PartyNameplateVM p) => p.Party != party.MobileParty))
				{
					this.Nameplates.Add(new PartyNameplateVM(party.MobileParty, this._mapCamera, this._resetCamera, this._isShowPartyNamesEnabled));
					return;
				}
				PartyNameplateVM partyNameplateVM;
				if ((!party.MobileParty.IsSpotted() || party.MobileParty.CurrentSettlement != null) && (partyNameplateVM = this.Nameplates.FirstOrDefault((PartyNameplateVM p) => p.Party == party.MobileParty)) != null && !partyNameplateVM.IsMainParty)
				{
					partyNameplateVM.OnFinalize();
					this.Nameplates.Remove(partyNameplateVM);
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009C98 File Offset: 0x00007E98
		public void Update()
		{
			for (int i = 0; i < this.Nameplates.Count; i++)
			{
				PartyNameplateVM partyNameplateVM = this.Nameplates[i];
				partyNameplateVM.RefreshPosition();
				partyNameplateVM.DetermineIsVisibleOnMap();
				partyNameplateVM.RefreshDynamicProperties(false);
			}
			for (int j = 0; j < this.Nameplates.Count; j++)
			{
				this.Nameplates[j].RefreshBinding();
			}
			this.Nameplates.Sort(this._nameplateComparer);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009D14 File Offset: 0x00007F14
		private void OnPlayerCharacterChangedEvent(Hero oldPlayer, Hero newPlayer, MobileParty newMainParty, bool isMainPartyChanged)
		{
			PartyNameplateVM partyNameplateVM = this.Nameplates.FirstOrDefault((PartyNameplateVM n) => n.GetIsMainParty);
			if (partyNameplateVM != null)
			{
				partyNameplateVM.OnFinalize();
				this.Nameplates.Remove(partyNameplateVM);
			}
			if (this.Nameplates.AllQ((PartyNameplateVM p) => p.Party.LeaderHero != newPlayer))
			{
				this.Nameplates.Add(new PartyNameplateVM(newMainParty, this._mapCamera, this._resetCamera, this._isShowPartyNamesEnabled));
			}
			foreach (PartyNameplateVM partyNameplateVM2 in this.Nameplates)
			{
				partyNameplateVM2.OnPlayerCharacterChanged(newPlayer);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009DF0 File Offset: 0x00007FF0
		private void OnGameOver()
		{
			PartyNameplateVM partyNameplateVM = this.Nameplates.FirstOrDefault((PartyNameplateVM n) => n.IsMainParty);
			if (partyNameplateVM != null)
			{
				partyNameplateVM.OnFinalize();
				this.Nameplates.Remove(partyNameplateVM);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009E3E File Offset: 0x0000803E
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Nameplates.Clear();
			this.UnregisterEvents();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009E58 File Offset: 0x00008058
		private void RegisterEvents()
		{
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
			CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
			CampaignEvents.PartyVisibilityChangedEvent.AddNonSerializedListener(this, new Action<PartyBase>(this.OnPartyVisibilityChanged));
			CampaignEvents.OnPlayerCharacterChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero, MobileParty, bool>(this.OnPlayerCharacterChangedEvent));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangeKingdom));
			CampaignEvents.OnGameOverEvent.AddNonSerializedListener(this, new Action(this.OnGameOver));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009EF0 File Offset: 0x000080F0
		private void UnregisterEvents()
		{
			CampaignEvents.SettlementEntered.ClearListeners(this);
			CampaignEvents.OnSettlementLeftEvent.ClearListeners(this);
			CampaignEvents.PartyVisibilityChangedEvent.ClearListeners(this);
			CampaignEvents.OnPlayerCharacterChangedEvent.ClearListeners(this);
			CampaignEvents.OnClanChangedKingdomEvent.ClearListeners(this);
			CampaignEvents.OnGameOverEvent.ClearListeners(this);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009F3F File Offset: 0x0000813F
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00009F47 File Offset: 0x00008147
		[DataSourceProperty]
		public MBBindingList<PartyNameplateVM> Nameplates
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
					base.OnPropertyChangedWithValue<MBBindingList<PartyNameplateVM>>(value, "Nameplates");
				}
			}
		}

		// Token: 0x040000D5 RID: 213
		private readonly Camera _mapCamera;

		// Token: 0x040000D6 RID: 214
		private readonly Action _resetCamera;

		// Token: 0x040000D7 RID: 215
		private readonly Func<bool> _isShowPartyNamesEnabled;

		// Token: 0x040000D8 RID: 216
		private readonly PartyNameplatesVM.NameplateDistanceComparer _nameplateComparer;

		// Token: 0x040000D9 RID: 217
		private MBBindingList<PartyNameplateVM> _nameplates;

		// Token: 0x02000061 RID: 97
		public class NameplateDistanceComparer : IComparer<PartyNameplateVM>
		{
			// Token: 0x06000505 RID: 1285 RVA: 0x00014750 File Offset: 0x00012950
			public int Compare(PartyNameplateVM x, PartyNameplateVM y)
			{
				return y.DistanceToCamera.CompareTo(x.DistanceToCamera);
			}
		}
	}
}
