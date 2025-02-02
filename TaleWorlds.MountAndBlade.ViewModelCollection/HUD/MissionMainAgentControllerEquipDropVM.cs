using System;
using System.ComponentModel;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000046 RID: 70
	public class MissionMainAgentControllerEquipDropVM : ViewModel
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001864C File Offset: 0x0001684C
		public MissionMainAgentControllerEquipDropVM(Action<EquipmentIndex> toggleItem)
		{
			this._toggleItem = toggleItem;
			this.EquippedWeapons = new MBBindingList<ControllerEquippedItemVM>();
			this.RefreshValues();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001867D File Offset: 0x0001687D
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PressToEquipText = new TextObject("{=HEEZhL90}Press to Equip", null).ToString();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001869B File Offset: 0x0001689B
		public void InitializeMainAgentPropterties()
		{
			Mission.Current.OnMainAgentChanged += this.OnMainAgentChanged;
			this.OnMainAgentChanged(null, null);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000186BB File Offset: 0x000168BB
		private void OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (Agent.Main != null)
			{
				Agent main = Agent.Main;
				main.OnMainAgentWieldedItemChange = (Agent.OnMainAgentWieldedItemChangeDelegate)Delegate.Combine(main.OnMainAgentWieldedItemChange, new Agent.OnMainAgentWieldedItemChangeDelegate(this.OnMainAgentWeaponChange));
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000186EA File Offset: 0x000168EA
		private void OnMainAgentWeaponChange()
		{
			this.UpdateItemsWieldStatus();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000186F4 File Offset: 0x000168F4
		public void OnToggle(bool isEnabled)
		{
			this.EquippedWeapons.ApplyActionOnAllItems(delegate(ControllerEquippedItemVM o)
			{
				o.OnFinalize();
			});
			this.EquippedWeapons.Clear();
			if (isEnabled)
			{
				this.EquippedWeapons.Add(new ControllerEquippedItemVM(GameTexts.FindText("str_cancel", null).ToString(), null, "None", null, new Action<EquipmentActionItemVM>(this.OnItemSelected)));
				int num = 0;
				int totalNumberOfWeaponsOnMainAgent = this.GetTotalNumberOfWeaponsOnMainAgent();
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
				{
					MissionWeapon weapon = Agent.Main.Equipment[equipmentIndex];
					if (!weapon.IsEmpty)
					{
						string itemTypeAsString = MissionMainAgentEquipmentControllerVM.GetItemTypeAsString(weapon.Item);
						string weaponName = this.GetWeaponName(weapon);
						this.EquippedWeapons.Add(new ControllerEquippedItemVM(weaponName, itemTypeAsString, equipmentIndex, MissionMainAgentControllerEquipDropVM.GetWeaponHotKey(num, totalNumberOfWeaponsOnMainAgent), new Action<EquipmentActionItemVM>(this.OnItemSelected)));
						num++;
					}
				}
				this.UpdateItemsWieldStatus();
			}
			else
			{
				if (this._lastSelectedItem != null && this._lastSelectedItem.Identifier is EquipmentIndex)
				{
					Action<EquipmentIndex> toggleItem = this._toggleItem;
					if (toggleItem != null)
					{
						toggleItem((EquipmentIndex)this._lastSelectedItem.Identifier);
					}
				}
				this._lastSelectedItem = null;
			}
			this.IsActive = isEnabled;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00018837 File Offset: 0x00016A37
		private void OnItemSelected(EquipmentActionItemVM selectedItem)
		{
			if (this._lastSelectedItem != selectedItem)
			{
				this._lastSelectedItem = selectedItem;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018849 File Offset: 0x00016A49
		public void OnCancelHoldController()
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001884B File Offset: 0x00016A4B
		public void OnWeaponDroppedAtIndex(int droppedWeaponIndex)
		{
			this.OnToggle(true);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00018854 File Offset: 0x00016A54
		private bool IsWieldedWeaponAtIndex(EquipmentIndex index)
		{
			return index == Agent.Main.GetWieldedItemIndex(Agent.HandIndex.MainHand) || index == Agent.Main.GetWieldedItemIndex(Agent.HandIndex.OffHand);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00018874 File Offset: 0x00016A74
		public void OnWeaponEquippedAtIndex(int equippedWeaponIndex)
		{
			this.UpdateItemsWieldStatus();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001887C File Offset: 0x00016A7C
		public void SetDropProgressForIndex(EquipmentIndex eqIndex, float progress)
		{
			int i = 0;
			while (i < this.EquippedWeapons.Count)
			{
				object identifier;
				if (!((identifier = this.EquippedWeapons[i].Identifier) is EquipmentIndex))
				{
					goto IL_31;
				}
				EquipmentIndex equipmentIndex = (EquipmentIndex)identifier;
				if (equipmentIndex != eqIndex || progress <= 0.2f)
				{
					goto IL_31;
				}
				float num = progress;
				IL_39:
				float dropProgress = num;
				this.EquippedWeapons[i].DropProgress = dropProgress;
				i++;
				continue;
				IL_31:
				num = 0f;
				goto IL_39;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000188E8 File Offset: 0x00016AE8
		private void UpdateItemsWieldStatus()
		{
			for (int i = 0; i < this.EquippedWeapons.Count; i++)
			{
				object identifier;
				if ((identifier = this.EquippedWeapons[i].Identifier) is EquipmentIndex)
				{
					EquipmentIndex index = (EquipmentIndex)identifier;
					this.EquippedWeapons[i].IsWielded = this.IsWieldedWeaponAtIndex(index);
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00018944 File Offset: 0x00016B44
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

		// Token: 0x060005F1 RID: 1521 RVA: 0x00018A26 File Offset: 0x00016C26
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.EquippedWeapons.ApplyActionOnAllItems(delegate(ControllerEquippedItemVM o)
			{
				o.OnFinalize();
			});
			this.EquippedWeapons.Clear();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00018A64 File Offset: 0x00016C64
		private static HotKey GetWeaponHotKey(int currentIndexOfWeapon, int totalNumOfWeapons)
		{
			if (currentIndexOfWeapon == 0)
			{
				if (totalNumOfWeapons == 1)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon4");
				}
				if (totalNumOfWeapons > 1)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon1");
				}
				Debug.FailedAssert("Wrong number of total weapons!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentControllerEquipDropVM.cs", "GetWeaponHotKey", 182);
			}
			else if (currentIndexOfWeapon == 1)
			{
				if (totalNumOfWeapons == 2)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon3");
				}
				if (totalNumOfWeapons > 2)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon4");
				}
			}
			else
			{
				if (currentIndexOfWeapon == 2)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon3");
				}
				if (currentIndexOfWeapon == 3)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon2");
				}
				Debug.FailedAssert("Wrong index of current weapon. Cannot be higher than 3", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentControllerEquipDropVM.cs", "GetWeaponHotKey", 206);
			}
			return null;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00018B46 File Offset: 0x00016D46
		public void OnGamepadActiveChanged(bool isActive)
		{
			this.HoldToDropText = (isActive ? this._dropTextObject.ToString() : string.Empty);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00018B64 File Offset: 0x00016D64
		private int GetTotalNumberOfWeaponsOnMainAgent()
		{
			int num = 0;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (!Agent.Main.Equipment[equipmentIndex].IsEmpty)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00018B9E File Offset: 0x00016D9E
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00018BA6 File Offset: 0x00016DA6
		[DataSourceProperty]
		public MBBindingList<ControllerEquippedItemVM> EquippedWeapons
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
					base.OnPropertyChangedWithValue<MBBindingList<ControllerEquippedItemVM>>(value, "EquippedWeapons");
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00018BC4 File Offset: 0x00016DC4
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00018BCC File Offset: 0x00016DCC
		[DataSourceProperty]
		public string HoldToDropText
		{
			get
			{
				return this._holdToDropText;
			}
			set
			{
				if (value != this._holdToDropText)
				{
					this._holdToDropText = value;
					base.OnPropertyChangedWithValue<string>(value, "HoldToDropText");
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00018BEF File Offset: 0x00016DEF
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00018BF7 File Offset: 0x00016DF7
		[DataSourceProperty]
		public string PressToEquipText
		{
			get
			{
				return this._pressToEquipText;
			}
			set
			{
				if (value != this._pressToEquipText)
				{
					this._pressToEquipText = value;
					base.OnPropertyChangedWithValue<string>(value, "PressToEquipText");
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00018C1A File Offset: 0x00016E1A
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00018C22 File Offset: 0x00016E22
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x040002D3 RID: 723
		private EquipmentActionItemVM _lastSelectedItem;

		// Token: 0x040002D4 RID: 724
		private Action<EquipmentIndex> _toggleItem;

		// Token: 0x040002D5 RID: 725
		private TextObject _dropTextObject = new TextObject("{=d1tCz15N}Hold to Drop", null);

		// Token: 0x040002D6 RID: 726
		private MBBindingList<ControllerEquippedItemVM> _equipActions;

		// Token: 0x040002D7 RID: 727
		private bool _isActive;

		// Token: 0x040002D8 RID: 728
		private string _holdToDropText;

		// Token: 0x040002D9 RID: 729
		private string _pressToEquipText;
	}
}
