using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Map
{
	// Token: 0x0200002C RID: 44
	public class MapMobilePartyTrackerVM : ViewModel
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00010271 File Offset: 0x0000E471
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00010279 File Offset: 0x0000E479
		public MBBindingList<MobilePartyTrackItemVM> Trackers
		{
			get
			{
				return this._trackers;
			}
			set
			{
				if (value != this._trackers)
				{
					this._trackers = value;
					base.OnPropertyChangedWithValue<MBBindingList<MobilePartyTrackItemVM>>(value, "Trackers");
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010298 File Offset: 0x0000E498
		public MapMobilePartyTrackerVM(Camera mapCamera, Action<Vec2> fastMoveCameraToPosition)
		{
			this._mapCamera = mapCamera;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			this.UpdateTrackerPropertiesAuxPredicate = new TWParallel.ParallelForAuxPredicate(this.UpdateTrackerPropertiesAux);
			this.Trackers = new MBBindingList<MobilePartyTrackItemVM>();
			this.InitList();
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnArmyCreated));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
			CampaignEvents.MobilePartyCreated.AddNonSerializedListener(this, new Action<MobileParty>(this.OnMobilePartyCreated));
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnPartyDestroyed));
			CampaignEvents.MobilePartyQuestStatusChanged.AddNonSerializedListener(this, new Action<MobileParty, bool>(this.OnPartyQuestStatusChanged));
			CampaignEvents.OnPartyDisbandedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnPartyDisbanded));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.OnCompanionClanCreatedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnCompanionClanCreated));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010394 File Offset: 0x0000E594
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEventDispatcher.Instance.RemoveListeners(this);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000103A8 File Offset: 0x0000E5A8
		private void InitList()
		{
			this.Trackers.Clear();
			foreach (WarPartyComponent warPartyComponent in Clan.PlayerClan.WarPartyComponents)
			{
				if (this.CanAddParty(warPartyComponent.MobileParty))
				{
					this.Trackers.Add(new MobilePartyTrackItemVM(warPartyComponent.MobileParty, this._mapCamera, this._fastMoveCameraToPosition));
				}
			}
			foreach (CaravanPartyComponent caravanPartyComponent in Clan.PlayerClan.Heroes.SelectMany((Hero h) => h.OwnedCaravans))
			{
				if (this.CanAddParty(caravanPartyComponent.MobileParty))
				{
					this.Trackers.Add(new MobilePartyTrackItemVM(caravanPartyComponent.MobileParty, this._mapCamera, this._fastMoveCameraToPosition));
				}
			}
			if (Clan.PlayerClan.Kingdom != null)
			{
				foreach (Army trackedArmy in Clan.PlayerClan.Kingdom.Armies)
				{
					this.Trackers.Add(new MobilePartyTrackItemVM(trackedArmy, this._mapCamera, this._fastMoveCameraToPosition));
				}
			}
			using (List<TrackedObject>.Enumerator enumerator4 = Campaign.Current.VisualTrackerManager.TrackedObjects.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					MobileParty mobileParty;
					if ((mobileParty = (enumerator4.Current.Object as MobileParty)) != null && mobileParty.LeaderHero == null && mobileParty.IsCurrentlyUsedByAQuest)
					{
						this.Trackers.Add(new MobilePartyTrackItemVM(mobileParty, this._mapCamera, this._fastMoveCameraToPosition));
					}
				}
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000105B4 File Offset: 0x0000E7B4
		private void UpdateTrackerPropertiesAux(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this.Trackers[i].UpdateProperties();
				this.Trackers[i].UpdatePosition();
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000105F0 File Offset: 0x0000E7F0
		public void Update()
		{
			TWParallel.For(0, this.Trackers.Count, this.UpdateTrackerPropertiesAuxPredicate, 16);
			this.Trackers.ApplyActionOnAllItems(delegate(MobilePartyTrackItemVM t)
			{
				t.RefreshBinding();
			});
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010640 File Offset: 0x0000E840
		public void UpdateProperties()
		{
			this.Trackers.ApplyActionOnAllItems(delegate(MobilePartyTrackItemVM t)
			{
				t.UpdateProperties();
			});
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001066C File Offset: 0x0000E86C
		private bool CanAddParty(MobileParty party)
		{
			return party != null && !party.IsMainParty && !party.IsMilitia && !party.IsGarrison && !party.IsVillager && !party.IsBandit && !party.IsBanditBossParty && !party.IsCurrentlyUsedByAQuest && (!party.IsCaravan || party.CaravanPartyComponent.Owner == Hero.MainHero);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000106D4 File Offset: 0x0000E8D4
		private void AddIfNotAdded(Army army)
		{
			if (this.Trackers.FirstOrDefault((MobilePartyTrackItemVM t) => t.TrackedArmy == army) == null)
			{
				this.Trackers.Add(new MobilePartyTrackItemVM(army, this._mapCamera, this._fastMoveCameraToPosition));
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001072C File Offset: 0x0000E92C
		private void AddIfNotAdded(MobileParty party)
		{
			for (int i = 0; i < this.Trackers.Count; i++)
			{
				if (this.Trackers[i].TrackedParty == party)
				{
					return;
				}
			}
			this.Trackers.Add(new MobilePartyTrackItemVM(party, this._mapCamera, this._fastMoveCameraToPosition));
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00010784 File Offset: 0x0000E984
		private void RemoveIfExists(Army army)
		{
			MobilePartyTrackItemVM mobilePartyTrackItemVM = this.Trackers.FirstOrDefault((MobilePartyTrackItemVM t) => t.TrackedArmy == army);
			if (mobilePartyTrackItemVM != null)
			{
				this.Trackers.Remove(mobilePartyTrackItemVM);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000107C8 File Offset: 0x0000E9C8
		private void RemoveIfExists(MobileParty party)
		{
			for (int i = 0; i < this.Trackers.Count; i++)
			{
				if (this.Trackers[i].TrackedParty == party)
				{
					this.Trackers.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001080C File Offset: 0x0000EA0C
		private void OnPartyDestroyed(MobileParty mobileParty, PartyBase arg2)
		{
			this.RemoveIfExists(mobileParty);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00010815 File Offset: 0x0000EA15
		private void OnPartyQuestStatusChanged(MobileParty mobileParty, bool isUsedByQuest)
		{
			if (isUsedByQuest)
			{
				this.RemoveIfExists(mobileParty);
				return;
			}
			if (this.CanAddParty(mobileParty))
			{
				this.AddIfNotAdded(mobileParty);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010832 File Offset: 0x0000EA32
		private void OnPartyDisbanded(MobileParty disbandedParty, Settlement relatedSettlement)
		{
			this.RemoveIfExists(disbandedParty);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001083B File Offset: 0x0000EA3B
		private void OnMobilePartyCreated(MobileParty party)
		{
			if (party.IsLordParty)
			{
				if (Clan.PlayerClan.WarPartyComponents.Contains(party.WarPartyComponent))
				{
					this.AddIfNotAdded(party);
					return;
				}
			}
			else if (this.CanAddParty(party))
			{
				this.AddIfNotAdded(party);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00010874 File Offset: 0x0000EA74
		private void OnArmyDispersed(Army army, Army.ArmyDispersionReason arg2, bool arg3)
		{
			if (army.Kingdom == Hero.MainHero.MapFaction)
			{
				this.RemoveIfExists(army);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001088F File Offset: 0x0000EA8F
		private void OnArmyCreated(Army army)
		{
			if (army.Kingdom == Hero.MainHero.MapFaction)
			{
				this.AddIfNotAdded(army);
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000108AA File Offset: 0x0000EAAA
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			if (clan == Clan.PlayerClan)
			{
				this.InitList();
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000108BA File Offset: 0x0000EABA
		private void OnCompanionClanCreated(Clan clan)
		{
			this.RemoveIfExists(clan.Leader.PartyBelongedTo);
		}

		// Token: 0x040001C5 RID: 453
		private readonly Camera _mapCamera;

		// Token: 0x040001C6 RID: 454
		private readonly Action<Vec2> _fastMoveCameraToPosition;

		// Token: 0x040001C7 RID: 455
		private readonly TWParallel.ParallelForAuxPredicate UpdateTrackerPropertiesAuxPredicate;

		// Token: 0x040001C8 RID: 456
		private MBBindingList<MobilePartyTrackItemVM> _trackers;
	}
}
