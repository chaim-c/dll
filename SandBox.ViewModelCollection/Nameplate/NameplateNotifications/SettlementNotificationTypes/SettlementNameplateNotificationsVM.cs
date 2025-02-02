using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes
{
	// Token: 0x02000022 RID: 34
	public class SettlementNameplateNotificationsVM : ViewModel
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000DE24 File Offset: 0x0000C024
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public bool IsEventsRegistered { get; private set; }

		// Token: 0x060002D5 RID: 725 RVA: 0x0000DE35 File Offset: 0x0000C035
		public SettlementNameplateNotificationsVM(Settlement settlement)
		{
			this._settlement = settlement;
			this.Notifications = new MBBindingList<SettlementNotificationItemBaseVM>();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000DE4F File Offset: 0x0000C04F
		public void Tick()
		{
			this._tickSinceEnabled++;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000DE60 File Offset: 0x0000C060
		private void OnTroopRecruited(Hero recruiterHero, Settlement settlement, Hero troopSource, CharacterObject troop, int amount)
		{
			if (amount > 0 && settlement == this._settlement && this._settlement.IsInspected && recruiterHero != null && (recruiterHero.CurrentSettlement == this._settlement || (recruiterHero.PartyBelongedTo != null && recruiterHero.PartyBelongedTo.LastVisitedSettlement == this._settlement)))
			{
				TroopRecruitmentNotificationItemVM updatableNotificationByPredicate = this.GetUpdatableNotificationByPredicate<TroopRecruitmentNotificationItemVM>((TroopRecruitmentNotificationItemVM n) => n.RecruiterHero == recruiterHero);
				if (updatableNotificationByPredicate != null)
				{
					updatableNotificationByPredicate.AddNewAction(amount);
					return;
				}
				this.Notifications.Add(new TroopRecruitmentNotificationItemVM(new Action<SettlementNotificationItemBaseVM>(this.RemoveItem), recruiterHero, amount, this._tickSinceEnabled));
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000DF2C File Offset: 0x0000C12C
		private void OnCaravanTransactionCompleted(MobileParty caravanParty, Town town, List<ValueTuple<EquipmentElement, int>> items)
		{
			if (this._settlement != town.Owner.Settlement)
			{
				return;
			}
			CaravanTransactionNotificationItemVM updatableNotificationByPredicate = this.GetUpdatableNotificationByPredicate<CaravanTransactionNotificationItemVM>((CaravanTransactionNotificationItemVM n) => n.CaravanParty == caravanParty);
			if (updatableNotificationByPredicate != null)
			{
				updatableNotificationByPredicate.AddNewItems(items);
				return;
			}
			this.Notifications.Add(new CaravanTransactionNotificationItemVM(new Action<SettlementNotificationItemBaseVM>(this.RemoveItem), caravanParty, items, this._tickSinceEnabled));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		private void OnPrisonerSold(PartyBase sellerParty, PartyBase buyerParty, TroopRoster prisoners)
		{
			if (sellerParty.IsMobile && buyerParty != null && buyerParty.IsSettlement && buyerParty.Settlement == this._settlement && this._settlement.IsInspected && prisoners.Count > 0 && sellerParty.LeaderHero != null)
			{
				MobileParty sellerMobileParty = sellerParty.MobileParty;
				PrisonerSoldNotificationItemVM updatableNotificationByPredicate = this.GetUpdatableNotificationByPredicate<PrisonerSoldNotificationItemVM>((PrisonerSoldNotificationItemVM n) => n.Party == sellerMobileParty);
				if (updatableNotificationByPredicate != null)
				{
					updatableNotificationByPredicate.AddNewPrisoners(prisoners);
					return;
				}
				this.Notifications.Add(new PrisonerSoldNotificationItemVM(new Action<SettlementNotificationItemBaseVM>(this.RemoveItem), sellerMobileParty, prisoners, this._tickSinceEnabled));
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E054 File Offset: 0x0000C254
		private void OnTroopGivenToSettlement(Hero giverHero, Settlement givenSettlement, TroopRoster givenTroops)
		{
			if (this._settlement.IsInspected && givenTroops.TotalManCount > 0 && giverHero != null && givenSettlement == this._settlement)
			{
				TroopGivenToSettlementNotificationItemVM updatableNotificationByPredicate = this.GetUpdatableNotificationByPredicate<TroopGivenToSettlementNotificationItemVM>((TroopGivenToSettlementNotificationItemVM n) => n.GiverHero == giverHero);
				if (updatableNotificationByPredicate != null)
				{
					updatableNotificationByPredicate.AddNewAction(givenTroops);
					return;
				}
				this.Notifications.Add(new TroopGivenToSettlementNotificationItemVM(new Action<SettlementNotificationItemBaseVM>(this.RemoveItem), giverHero, givenTroops, this._tickSinceEnabled));
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		private void OnItemSold(PartyBase receiverParty, PartyBase payerParty, ItemRosterElement item, int number, Settlement currentSettlement)
		{
			if (this._settlement.IsInspected && number > 0 && currentSettlement == this._settlement)
			{
				int num = receiverParty.IsSettlement ? -1 : 1;
				ItemSoldNotificationItemVM updatableNotificationByPredicate = this.GetUpdatableNotificationByPredicate<ItemSoldNotificationItemVM>((ItemSoldNotificationItemVM n) => n.Item.EquipmentElement.Item == item.EquipmentElement.Item && (n.PayerParty == receiverParty || n.PayerParty == payerParty));
				if (updatableNotificationByPredicate != null)
				{
					updatableNotificationByPredicate.AddNewTransaction(number * num);
					return;
				}
				this.Notifications.Add(new ItemSoldNotificationItemVM(new Action<SettlementNotificationItemBaseVM>(this.RemoveItem), receiverParty, payerParty, item, number * num, this._tickSinceEnabled));
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E18B File Offset: 0x0000C38B
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails updateType, Hero relatedHero)
		{
			if (updateType == IssueBase.IssueUpdateDetails.IssueFinishedByAILord && relatedHero != null && relatedHero.CurrentSettlement == this._settlement)
			{
				this.Notifications.Add(new IssueSolvedByLordNotificationItemVM(new Action<SettlementNotificationItemBaseVM>(this.RemoveItem), relatedHero, this._tickSinceEnabled));
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E1C5 File Offset: 0x0000C3C5
		private void RemoveItem(SettlementNotificationItemBaseVM item)
		{
			this.Notifications.Remove(item);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		public void RegisterEvents()
		{
			if (!this.IsEventsRegistered)
			{
				CampaignEvents.OnTroopRecruitedEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, Hero, CharacterObject, int>(this.OnTroopRecruited));
				CampaignEvents.OnPrisonerSoldEvent.AddNonSerializedListener(this, new Action<PartyBase, PartyBase, TroopRoster>(this.OnPrisonerSold));
				CampaignEvents.OnCaravanTransactionCompletedEvent.AddNonSerializedListener(this, new Action<MobileParty, Town, List<ValueTuple<EquipmentElement, int>>>(this.OnCaravanTransactionCompleted));
				CampaignEvents.OnTroopGivenToSettlementEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, TroopRoster>(this.OnTroopGivenToSettlement));
				CampaignEvents.OnItemSoldEvent.AddNonSerializedListener(this, new Action<PartyBase, PartyBase, ItemRosterElement, int, Settlement>(this.OnItemSold));
				CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
				this._tickSinceEnabled = 0;
				this.IsEventsRegistered = true;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E284 File Offset: 0x0000C484
		public void UnloadEvents()
		{
			if (this.IsEventsRegistered)
			{
				CampaignEvents.OnTroopRecruitedEvent.ClearListeners(this);
				CampaignEvents.OnItemSoldEvent.ClearListeners(this);
				CampaignEvents.OnPrisonerSoldEvent.ClearListeners(this);
				CampaignEvents.OnCaravanTransactionCompletedEvent.ClearListeners(this);
				CampaignEvents.OnTroopGivenToSettlementEvent.ClearListeners(this);
				CampaignEvents.OnIssueUpdatedEvent.ClearListeners(this);
				this._tickSinceEnabled = 0;
				this.IsEventsRegistered = false;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public bool IsValidItemForNotification(ItemRosterElement item)
		{
			switch (item.EquipmentElement.Item.Type)
			{
			case ItemObject.ItemTypeEnum.Horse:
			case ItemObject.ItemTypeEnum.OneHandedWeapon:
			case ItemObject.ItemTypeEnum.TwoHandedWeapon:
			case ItemObject.ItemTypeEnum.Polearm:
			case ItemObject.ItemTypeEnum.Arrows:
			case ItemObject.ItemTypeEnum.Bolts:
			case ItemObject.ItemTypeEnum.Shield:
			case ItemObject.ItemTypeEnum.Bow:
			case ItemObject.ItemTypeEnum.Crossbow:
			case ItemObject.ItemTypeEnum.Thrown:
			case ItemObject.ItemTypeEnum.Goods:
			case ItemObject.ItemTypeEnum.HeadArmor:
			case ItemObject.ItemTypeEnum.BodyArmor:
			case ItemObject.ItemTypeEnum.LegArmor:
			case ItemObject.ItemTypeEnum.HandArmor:
			case ItemObject.ItemTypeEnum.Pistol:
			case ItemObject.ItemTypeEnum.Musket:
			case ItemObject.ItemTypeEnum.Bullets:
			case ItemObject.ItemTypeEnum.Animal:
			case ItemObject.ItemTypeEnum.ChestArmor:
			case ItemObject.ItemTypeEnum.Cape:
			case ItemObject.ItemTypeEnum.HorseHarness:
				return true;
			}
			return false;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E380 File Offset: 0x0000C580
		private T GetUpdatableNotificationByPredicate<T>(Func<T, bool> predicate) where T : SettlementNotificationItemBaseVM
		{
			for (int i = 0; i < this.Notifications.Count; i++)
			{
				SettlementNotificationItemBaseVM settlementNotificationItemBaseVM = this.Notifications[i];
				T t;
				if (this._tickSinceEnabled - settlementNotificationItemBaseVM.CreatedTick < 10 && (t = (settlementNotificationItemBaseVM as T)) != null && predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000E3EB File Offset: 0x0000C5EB
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		public MBBindingList<SettlementNotificationItemBaseVM> Notifications
		{
			get
			{
				return this._notifications;
			}
			set
			{
				if (value != this._notifications)
				{
					this._notifications = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementNotificationItemBaseVM>>(value, "Notifications");
				}
			}
		}

		// Token: 0x04000170 RID: 368
		private readonly Settlement _settlement;

		// Token: 0x04000172 RID: 370
		private int _tickSinceEnabled;

		// Token: 0x04000173 RID: 371
		private const int _maxTickDeltaToCongregate = 10;

		// Token: 0x04000174 RID: 372
		private MBBindingList<SettlementNotificationItemBaseVM> _notifications;
	}
}
