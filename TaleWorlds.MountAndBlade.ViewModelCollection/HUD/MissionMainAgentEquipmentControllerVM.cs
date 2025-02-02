using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000048 RID: 72
	public class MissionMainAgentEquipmentControllerVM : ViewModel
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00018CCC File Offset: 0x00016ECC
		public MissionMainAgentEquipmentControllerVM(Action<EquipmentIndex> onDropEquipment, Action<SpawnedItemEntity, EquipmentIndex> onEquipItem)
		{
			this._onDropEquipment = onDropEquipment;
			this._onEquipItem = onEquipItem;
			this.DropActions = new MBBindingList<EquipmentActionItemVM>();
			this.EquipActions = new MBBindingList<EquipmentActionItemVM>();
			this.RefreshValues();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00018D1A File Offset: 0x00016F1A
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._dropLocalizedText = GameTexts.FindText("str_inventory_drop", null);
			this._replaceWithLocalizedText = GameTexts.FindText("str_replace_with", null);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018D44 File Offset: 0x00016F44
		public void OnDropControllerToggle(bool isActive)
		{
			this.SelectedItemText = "";
			if (isActive && Agent.Main != null)
			{
				this.DropActions.Clear();
				this.DropActions.Add(new EquipmentActionItemVM(GameTexts.FindText("str_cancel", null).ToString(), "None", null, new Action<EquipmentActionItemVM>(this.OnItemSelected), false));
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					MissionWeapon weapon = Agent.Main.Equipment[equipmentIndex];
					if (!weapon.IsEmpty)
					{
						string itemTypeAsString = MissionMainAgentEquipmentControllerVM.GetItemTypeAsString(weapon.Item);
						bool isCurrentlyWielded = this.IsWieldedWeaponAtIndex(equipmentIndex);
						string weaponName = this.GetWeaponName(weapon);
						this.DropActions.Add(new EquipmentActionItemVM(weaponName, itemTypeAsString, equipmentIndex, new Action<EquipmentActionItemVM>(this.OnItemSelected), isCurrentlyWielded));
					}
				}
			}
			else
			{
				EquipmentActionItemVM equipmentActionItemVM = this.DropActions.SingleOrDefault((EquipmentActionItemVM a) => a.IsSelected);
				if (equipmentActionItemVM != null)
				{
					this.HandleDropItemActionSelection(equipmentActionItemVM.Identifier);
				}
			}
			this.IsDropControllerActive = isActive;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00018E5C File Offset: 0x0001705C
		private void HandleDropItemActionSelection(object selectedItem)
		{
			if (selectedItem is EquipmentIndex)
			{
				EquipmentIndex obj = (EquipmentIndex)selectedItem;
				this._onDropEquipment(obj);
				return;
			}
			if (selectedItem != null)
			{
				Debug.FailedAssert("Unidentified action on drop wheel", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentEquipmentControllerVM.cs", "HandleDropItemActionSelection", 106);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018EA0 File Offset: 0x000170A0
		public void SetCurrentFocusedWeaponEntity(SpawnedItemEntity weaponEntity)
		{
			this._focusedWeaponEntity = weaponEntity;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018EAC File Offset: 0x000170AC
		public void OnEquipControllerToggle(bool isActive)
		{
			this.SelectedItemText = "";
			this.FocusedItemText = "";
			if (isActive && Agent.Main != null)
			{
				this.EquipActions.Clear();
				this.EquipActions.Add(new EquipmentActionItemVM(GameTexts.FindText("str_cancel", null).ToString(), "None", null, new Action<EquipmentActionItemVM>(this.OnItemSelected), false));
				if (this._focusedWeaponEntity.WeaponCopy.Item.Type == ItemObject.ItemTypeEnum.Shield && this.DoesPlayerHaveAtLeastOneShield())
				{
					this._pickText.SetTextVariable("ITEM_NAME", this._focusedWeaponEntity.WeaponCopy.Item.Name.ToString());
					this.FocusedItemText = this._pickText.ToString();
					for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
					{
						MissionWeapon weapon = Agent.Main.Equipment[equipmentIndex];
						if (!weapon.IsEmpty && weapon.Item.Type == ItemObject.ItemTypeEnum.Shield)
						{
							string itemTypeAsString = MissionMainAgentEquipmentControllerVM.GetItemTypeAsString(weapon.Item);
							bool isCurrentlyWielded = this.IsWieldedWeaponAtIndex(equipmentIndex);
							string weaponName = this.GetWeaponName(weapon);
							this.EquipActions.Add(new EquipmentActionItemVM(weaponName, itemTypeAsString, equipmentIndex, new Action<EquipmentActionItemVM>(this.OnItemSelected), isCurrentlyWielded));
						}
					}
				}
				else
				{
					Agent main = Agent.Main;
					if (main != null && main.CanInteractableWeaponBePickedUp(this._focusedWeaponEntity))
					{
						this._pickText.SetTextVariable("ITEM_NAME", this._focusedWeaponEntity.WeaponCopy.Item.Name.ToString());
						this.FocusedItemText = this._pickText.ToString();
						bool flag = Agent.Main.WillDropWieldedShield(this._focusedWeaponEntity);
						for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex2++)
						{
							MissionWeapon weapon2 = Mission.Current.MainAgent.Equipment[equipmentIndex2];
							if (!weapon2.IsEmpty && (!flag || weapon2.IsShield()))
							{
								string itemTypeAsString2 = MissionMainAgentEquipmentControllerVM.GetItemTypeAsString(weapon2.Item);
								bool isCurrentlyWielded2 = this.IsWieldedWeaponAtIndex(equipmentIndex2);
								string weaponName2 = this.GetWeaponName(weapon2);
								this.EquipActions.Add(new EquipmentActionItemVM(weaponName2, itemTypeAsString2, equipmentIndex2, new Action<EquipmentActionItemVM>(this.OnItemSelected), isCurrentlyWielded2));
							}
						}
					}
					else
					{
						this.FocusedItemText = this._focusedWeaponEntity.WeaponCopy.Item.Name.ToString();
						EquipmentActionItemVM item = new EquipmentActionItemVM(GameTexts.FindText("str_pickup_to_equip", null).ToString(), "PickUp", this._focusedWeaponEntity, new Action<EquipmentActionItemVM>(this.OnItemSelected), false)
						{
							IsSelected = true
						};
						this.EquipActions.Add(item);
					}
				}
				EquipmentIndex itemIndexThatQuickPickUpWouldReplace = MissionEquipment.SelectWeaponPickUpSlot(Agent.Main, this._focusedWeaponEntity.WeaponCopy, this._focusedWeaponEntity.IsStuckMissile());
				EquipmentActionItemVM equipmentActionItemVM = this.EquipActions.SingleOrDefault(delegate(EquipmentActionItemVM a)
				{
					object identifier;
					if ((identifier = a.Identifier) is EquipmentIndex)
					{
						EquipmentIndex equipmentIndex3 = (EquipmentIndex)identifier;
						return equipmentIndex3 == itemIndexThatQuickPickUpWouldReplace;
					}
					return false;
				});
				if (equipmentActionItemVM != null)
				{
					equipmentActionItemVM.IsSelected = true;
				}
			}
			else
			{
				EquipmentActionItemVM equipmentActionItemVM2 = this.EquipActions.SingleOrDefault((EquipmentActionItemVM a) => a.IsSelected);
				if (equipmentActionItemVM2 != null)
				{
					this.HandleEquipItemActionSelection(equipmentActionItemVM2.Identifier);
				}
			}
			this.IsEquipControllerActive = isActive;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000191FE File Offset: 0x000173FE
		public void OnCancelEquipController()
		{
			this.IsEquipControllerActive = false;
			this.EquipActions.Clear();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00019212 File Offset: 0x00017412
		public void OnCancelDropController()
		{
			this.IsDropControllerActive = false;
			this.DropActions.Clear();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019228 File Offset: 0x00017428
		private void HandleEquipItemActionSelection(object selectedItem)
		{
			if (selectedItem is EquipmentIndex)
			{
				EquipmentIndex arg = (EquipmentIndex)selectedItem;
				if (this._focusedWeaponEntity != null)
				{
					this._onEquipItem(this._focusedWeaponEntity, arg);
					return;
				}
			}
			SpawnedItemEntity arg2;
			if ((arg2 = (selectedItem as SpawnedItemEntity)) != null)
			{
				this._onEquipItem(arg2, EquipmentIndex.None);
				return;
			}
			if (selectedItem != null)
			{
				Debug.FailedAssert("Unidentified action on drop wheel", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentEquipmentControllerVM.cs", "HandleEquipItemActionSelection", 223);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00019298 File Offset: 0x00017498
		private void OnItemSelected(EquipmentActionItemVM item)
		{
			if (this.IsEquipControllerActive)
			{
				if (item.Identifier == null || item.Identifier is SpawnedItemEntity)
				{
					this.EquipText = "";
				}
				else
				{
					this.EquipText = this._replaceWithLocalizedText.ToString();
				}
			}
			else if (item.Identifier == null)
			{
				this.DropText = "";
			}
			else
			{
				this.DropText = this._dropLocalizedText.ToString();
			}
			this.SelectedItemText = item.ActionText;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019314 File Offset: 0x00017514
		private string GetWeaponName(MissionWeapon weapon)
		{
			string text = weapon.Item.Name.ToString();
			WeaponComponentData currentUsageItem = weapon.CurrentUsageItem;
			if (currentUsageItem != null && currentUsageItem.IsShield)
			{
				text = string.Concat(new object[]
				{
					text,
					" (",
					weapon.HitPoints,
					" / ",
					weapon.ModifiedMaxHitPoints,
					")"
				});
			}
			else
			{
				WeaponComponentData currentUsageItem2 = weapon.CurrentUsageItem;
				if (currentUsageItem2 != null && currentUsageItem2.IsConsumable && weapon.ModifiedMaxAmount > 1)
				{
					text = string.Concat(new object[]
					{
						text,
						" (",
						weapon.Amount,
						" / ",
						weapon.ModifiedMaxAmount,
						")"
					});
				}
			}
			return text;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000193F6 File Offset: 0x000175F6
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000193FE File Offset: 0x000175FE
		[DataSourceProperty]
		public bool IsDropControllerActive
		{
			get
			{
				return this._isDropControllerActive;
			}
			set
			{
				if (value != this._isDropControllerActive)
				{
					this._isDropControllerActive = value;
					base.OnPropertyChangedWithValue(value, "IsDropControllerActive");
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001941C File Offset: 0x0001761C
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00019424 File Offset: 0x00017624
		[DataSourceProperty]
		public bool IsEquipControllerActive
		{
			get
			{
				return this._isEquipControllerActive;
			}
			set
			{
				if (value != this._isEquipControllerActive)
				{
					this._isEquipControllerActive = value;
					base.OnPropertyChangedWithValue(value, "IsEquipControllerActive");
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00019442 File Offset: 0x00017642
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0001944A File Offset: 0x0001764A
		[DataSourceProperty]
		public string DropText
		{
			get
			{
				return this._dropText;
			}
			set
			{
				if (value != this._dropText)
				{
					this._dropText = value;
					base.OnPropertyChangedWithValue<string>(value, "DropText");
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001946D File Offset: 0x0001766D
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00019475 File Offset: 0x00017675
		[DataSourceProperty]
		public string EquipText
		{
			get
			{
				return this._equipText;
			}
			set
			{
				if (value != this._equipText)
				{
					this._equipText = value;
					base.OnPropertyChangedWithValue<string>(value, "EquipText");
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00019498 File Offset: 0x00017698
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x000194A0 File Offset: 0x000176A0
		[DataSourceProperty]
		public string FocusedItemText
		{
			get
			{
				return this._focusedItemText;
			}
			set
			{
				if (value != this._focusedItemText)
				{
					this._focusedItemText = value;
					base.OnPropertyChangedWithValue<string>(value, "FocusedItemText");
				}
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000194C3 File Offset: 0x000176C3
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x000194CB File Offset: 0x000176CB
		[DataSourceProperty]
		public string SelectedItemText
		{
			get
			{
				return this._selectedItemText;
			}
			set
			{
				if (value != this._selectedItemText)
				{
					this._selectedItemText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectedItemText");
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000194EE File Offset: 0x000176EE
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x000194F6 File Offset: 0x000176F6
		[DataSourceProperty]
		public MBBindingList<EquipmentActionItemVM> DropActions
		{
			get
			{
				return this._dropActions;
			}
			set
			{
				if (value != this._dropActions)
				{
					this._dropActions = value;
					base.OnPropertyChangedWithValue<MBBindingList<EquipmentActionItemVM>>(value, "DropActions");
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00019514 File Offset: 0x00017714
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0001951C File Offset: 0x0001771C
		[DataSourceProperty]
		public MBBindingList<EquipmentActionItemVM> EquipActions
		{
			get
			{
				return this._equipActions;
			}
			set
			{
				if (value != this._equipActions)
				{
					this._equipActions = value;
					base.OnPropertyChangedWithValue<MBBindingList<EquipmentActionItemVM>>(value, "EquipActions");
				}
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001953C File Offset: 0x0001773C
		public static string GetItemTypeAsString(ItemObject item)
		{
			if (item.ItemComponent is WeaponComponent)
			{
				switch ((item.ItemComponent as WeaponComponent).PrimaryWeapon.WeaponClass)
				{
				case WeaponClass.Dagger:
				case WeaponClass.OneHandedSword:
				case WeaponClass.TwoHandedSword:
					return "Sword";
				case WeaponClass.OneHandedAxe:
				case WeaponClass.TwoHandedAxe:
					return "Axe";
				case WeaponClass.Mace:
				case WeaponClass.TwoHandedMace:
					return "Mace";
				case WeaponClass.OneHandedPolearm:
				case WeaponClass.TwoHandedPolearm:
				case WeaponClass.LowGripPolearm:
					return "Spear";
				case WeaponClass.Arrow:
				case WeaponClass.Bolt:
				case WeaponClass.Cartridge:
				case WeaponClass.Musket:
					return "Ammo";
				case WeaponClass.Bow:
					return "Bow";
				case WeaponClass.Crossbow:
					return "Crossbow";
				case WeaponClass.Stone:
					return "Stone";
				case WeaponClass.ThrowingAxe:
					return "ThrowingAxe";
				case WeaponClass.ThrowingKnife:
					return "ThrowingKnife";
				case WeaponClass.Javelin:
					return "Javelin";
				case WeaponClass.SmallShield:
				case WeaponClass.LargeShield:
					return "Shield";
				case WeaponClass.Banner:
					return "Banner";
				}
				return "None";
			}
			if (item.ItemComponent is HorseComponent)
			{
				return "Mount";
			}
			return "None";
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00019650 File Offset: 0x00017850
		private bool DoesPlayerHaveAtLeastOneShield()
		{
			EquipmentIndex wieldedItemIndex = Agent.Main.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				if (equipmentIndex != wieldedItemIndex && !Agent.Main.Equipment[equipmentIndex].IsEmpty && Mission.Current.MainAgent.Equipment[equipmentIndex].Item.Type == ItemObject.ItemTypeEnum.Shield)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000196BB File Offset: 0x000178BB
		private bool IsWieldedWeaponAtIndex(EquipmentIndex index)
		{
			return index == Agent.Main.GetWieldedItemIndex(Agent.HandIndex.MainHand) || index == Agent.Main.GetWieldedItemIndex(Agent.HandIndex.OffHand);
		}

		// Token: 0x040002DC RID: 732
		private TextObject _replaceWithLocalizedText;

		// Token: 0x040002DD RID: 733
		private TextObject _dropLocalizedText;

		// Token: 0x040002DE RID: 734
		private SpawnedItemEntity _focusedWeaponEntity;

		// Token: 0x040002DF RID: 735
		private readonly Action<EquipmentIndex> _onDropEquipment;

		// Token: 0x040002E0 RID: 736
		private readonly Action<SpawnedItemEntity, EquipmentIndex> _onEquipItem;

		// Token: 0x040002E1 RID: 737
		private readonly TextObject _pickText = new TextObject("{=d5SNB0HV}Pick {ITEM_NAME}", null);

		// Token: 0x040002E2 RID: 738
		private bool _isDropControllerActive;

		// Token: 0x040002E3 RID: 739
		private bool _isEquipControllerActive;

		// Token: 0x040002E4 RID: 740
		private string _selectedItemText;

		// Token: 0x040002E5 RID: 741
		private string _dropText;

		// Token: 0x040002E6 RID: 742
		private string _equipText;

		// Token: 0x040002E7 RID: 743
		private string _focusedItemText;

		// Token: 0x040002E8 RID: 744
		private MBBindingList<EquipmentActionItemVM> _dropActions;

		// Token: 0x040002E9 RID: 745
		private MBBindingList<EquipmentActionItemVM> _equipActions;

		// Token: 0x020000CE RID: 206
		public enum ItemGroup
		{
			// Token: 0x040005F0 RID: 1520
			None,
			// Token: 0x040005F1 RID: 1521
			Spear,
			// Token: 0x040005F2 RID: 1522
			Javelin,
			// Token: 0x040005F3 RID: 1523
			Bow,
			// Token: 0x040005F4 RID: 1524
			Crossbow,
			// Token: 0x040005F5 RID: 1525
			Sword,
			// Token: 0x040005F6 RID: 1526
			Axe,
			// Token: 0x040005F7 RID: 1527
			Mace,
			// Token: 0x040005F8 RID: 1528
			ThrowingAxe,
			// Token: 0x040005F9 RID: 1529
			ThrowingKnife,
			// Token: 0x040005FA RID: 1530
			Ammo,
			// Token: 0x040005FB RID: 1531
			Shield,
			// Token: 0x040005FC RID: 1532
			Mount,
			// Token: 0x040005FD RID: 1533
			Banner,
			// Token: 0x040005FE RID: 1534
			Stone
		}
	}
}
