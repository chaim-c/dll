using System;
using SandBox.View;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x0200000A RID: 10
	[GameStateScreen(typeof(InventoryState))]
	public class GauntletInventoryScreen : ScreenBase, IInventoryStateHandler, IGameStateListener, IChangeableScreen
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004FD7 File Offset: 0x000031D7
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00004FDF File Offset: 0x000031DF
		public InventoryState InventoryState { get; private set; }

		// Token: 0x06000060 RID: 96 RVA: 0x00004FE8 File Offset: 0x000031E8
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (!this._closed)
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			this._dataSource.IsFiveStackModifierActive = this._gauntletLayer.Input.IsHotKeyDown("FiveStackModifier");
			this._dataSource.IsEntireStackModifierActive = this._gauntletLayer.Input.IsHotKeyDown("EntireStackModifier");
			if (!this._dataSource.IsSearchAvailable || !this._gauntletLayer.IsFocusedOnInput())
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("SwitchAlternative") && this._dataSource != null)
				{
					this._dataSource.CompareNextItem();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyDownAndReleased("Exit") || this._gauntletLayer.Input.IsGameKeyDownAndReleased(38))
				{
					this.ExecuteCancel();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyDownAndReleased("Confirm"))
				{
					this.ExecuteConfirm();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyDownAndReleased("Reset"))
				{
					this.HandleResetInput();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyPressed("SwitchToPreviousTab"))
				{
					if (!this._dataSource.IsFocusedOnItemList || !Input.IsGamepadActive)
					{
						this.ExecuteSwitchToPreviousTab();
						return;
					}
					if (this._dataSource.CurrentFocusedItem != null && this._dataSource.CurrentFocusedItem.IsTransferable && this._dataSource.CurrentFocusedItem.InventorySide == InventoryLogic.InventorySide.OtherInventory)
					{
						this.ExecuteBuySingle();
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("SwitchToNextTab"))
				{
					if (!this._dataSource.IsFocusedOnItemList || !Input.IsGamepadActive)
					{
						this.ExecuteSwitchToNextTab();
						return;
					}
					if (this._dataSource.CurrentFocusedItem != null && this._dataSource.CurrentFocusedItem.IsTransferable && this._dataSource.CurrentFocusedItem.InventorySide == InventoryLogic.InventorySide.PlayerInventory)
					{
						this.ExecuteSellSingle();
						return;
					}
				}
				else
				{
					if (this._gauntletLayer.Input.IsHotKeyPressed("TakeAll"))
					{
						this.ExecuteTakeAll();
						return;
					}
					if (this._gauntletLayer.Input.IsHotKeyPressed("GiveAll"))
					{
						this.ExecuteGiveAll();
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005218 File Offset: 0x00003418
		public GauntletInventoryScreen(InventoryState inventoryState)
		{
			this.InventoryState = inventoryState;
			this.InventoryState.Handler = this;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005234 File Offset: 0x00003434
		protected override void OnInitialize()
		{
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._inventoryCategory = spriteData.SpriteCategories["ui_inventory"];
			this._inventoryCategory.Load(resourceContext, uiresourceDepot);
			InventoryLogic inventoryLogic = this.InventoryState.InventoryLogic;
			Mission mission = Mission.Current;
			this._dataSource = new SPInventoryVM(inventoryLogic, mission != null && mission.DoesMissionRequireCivilianEquipment, new Func<WeaponComponentData, ItemObject.ItemUsageSetFlags>(this.GetItemUsageSetFlag), this.GetFiveStackShortcutkeyText(), this.GetEntireStackShortcutkeyText());
			this._dataSource.SetGetKeyTextFromKeyIDFunc(new Func<string, TextObject>(Game.Current.GameTextManager.GetHotKeyGameTextFromKeyID));
			this._dataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetPreviousCharacterInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
			this._dataSource.SetNextCharacterInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
			this._dataSource.SetBuyAllInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("TakeAll"));
			this._dataSource.SetSellAllInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("GiveAll"));
			this._gauntletLayer = new GauntletLayer(15, "GauntletLayer", true)
			{
				IsFocusLayer = true
			};
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.AddLayer(this._gauntletLayer);
			ScreenManager.TrySetFocus(this._gauntletLayer);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("InventoryHotKeyCategory"));
			this._gauntletMovie = this._gauntletLayer.LoadMovie("Inventory", this._dataSource);
			this._openedFromMission = (this.InventoryState.Predecessor is MissionState);
			InformationManager.ClearAllMessages();
			UISoundsHelper.PlayUISound("event:/ui/panels/panel_inventory_open");
			this._gauntletLayer.GamepadNavigationContext.GainNavigationAfterFrames(2, null);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005495 File Offset: 0x00003695
		private string GetFiveStackShortcutkeyText()
		{
			if (!Input.IsControllerConnected || Input.IsMouseActive)
			{
				return Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", "anyshift").ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000054CF File Offset: 0x000036CF
		private string GetEntireStackShortcutkeyText()
		{
			if (!Input.IsControllerConnected || Input.IsMouseActive)
			{
				return Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", "anycontrol").ToString();
			}
			return null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005505 File Offset: 0x00003705
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._closed = true;
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005519 File Offset: 0x00003719
		protected override void OnActivate()
		{
			base.OnActivate();
			SPInventoryVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.RefreshCallbacks();
			}
			if (this._gauntletLayer != null)
			{
				ScreenManager.TrySetFocus(this._gauntletLayer);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005545 File Offset: 0x00003745
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._gauntletMovie = null;
			this._inventoryCategory.Unload();
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer = null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005578 File Offset: 0x00003778
		void IGameStateListener.OnActivate()
		{
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.InventoryScreen));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000558F File Offset: 0x0000378F
		void IGameStateListener.OnDeactivate()
		{
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000055A6 File Offset: 0x000037A6
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000055A8 File Offset: 0x000037A8
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000055AC File Offset: 0x000037AC
		void IInventoryStateHandler.FilterInventoryAtOpening(InventoryManager.InventoryCategoryType inventoryCategoryType)
		{
			if (this._dataSource == null)
			{
				Debug.FailedAssert("Data source is not initialized when filtering inventory", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.GauntletUI\\GauntletInventoryScreen.cs", "FilterInventoryAtOpening", 234);
				return;
			}
			switch (inventoryCategoryType)
			{
			case InventoryManager.InventoryCategoryType.Armors:
				this._dataSource.ExecuteFilterArmors();
				return;
			case InventoryManager.InventoryCategoryType.Weapon:
				this._dataSource.ExecuteFilterWeapons();
				return;
			case InventoryManager.InventoryCategoryType.Shield:
				break;
			case InventoryManager.InventoryCategoryType.HorseCategory:
				this._dataSource.ExecuteFilterMounts();
				return;
			case InventoryManager.InventoryCategoryType.Goods:
				this._dataSource.ExecuteFilterMisc();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005627 File Offset: 0x00003827
		public void ExecuteLootingScript()
		{
			this._dataSource.ExecuteBuyAllItems();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005634 File Offset: 0x00003834
		public void ExecuteSellAllLoot()
		{
			this._dataSource.ExecuteSellAllItems();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005641 File Offset: 0x00003841
		private void HandleResetInput()
		{
			if (!this._dataSource.ItemPreview.IsSelected)
			{
				this._dataSource.ExecuteResetTranstactions();
				UISoundsHelper.PlayUISound("event:/ui/default");
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000566C File Offset: 0x0000386C
		public void ExecuteCancel()
		{
			if (this._dataSource.ItemPreview.IsSelected)
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._dataSource.ClosePreview();
				return;
			}
			if (this._dataSource.IsExtendedEquipmentControlsEnabled)
			{
				this._dataSource.IsExtendedEquipmentControlsEnabled = false;
				return;
			}
			UISoundsHelper.PlayUISound("event:/ui/default");
			this._dataSource.ExecuteResetAndCompleteTranstactions();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000056D0 File Offset: 0x000038D0
		public void ExecuteConfirm()
		{
			if (!this._dataSource.ItemPreview.IsSelected && !this._dataSource.IsDoneDisabled)
			{
				this._dataSource.ExecuteCompleteTranstactions();
				UISoundsHelper.PlayUISound("event:/ui/default");
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005708 File Offset: 0x00003908
		public void ExecuteSwitchToPreviousTab()
		{
			if (!this._dataSource.ItemPreview.IsSelected)
			{
				MBBindingList<InventoryCharacterSelectorItemVM> itemList = this._dataSource.CharacterList.ItemList;
				if (itemList != null && itemList.Count > 1)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
				}
				this._dataSource.CharacterList.ExecuteSelectPreviousItem();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005764 File Offset: 0x00003964
		public void ExecuteSwitchToNextTab()
		{
			if (!this._dataSource.ItemPreview.IsSelected)
			{
				MBBindingList<InventoryCharacterSelectorItemVM> itemList = this._dataSource.CharacterList.ItemList;
				if (itemList != null && itemList.Count > 1)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
				}
				this._dataSource.CharacterList.ExecuteSelectNextItem();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000057BE File Offset: 0x000039BE
		public void ExecuteBuySingle()
		{
			this._dataSource.CurrentFocusedItem.ExecuteBuySingle();
			UISoundsHelper.PlayUISound("event:/ui/transfer");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000057DA File Offset: 0x000039DA
		public void ExecuteSellSingle()
		{
			this._dataSource.CurrentFocusedItem.ExecuteSellSingle();
			UISoundsHelper.PlayUISound("event:/ui/transfer");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000057F6 File Offset: 0x000039F6
		public void ExecuteTakeAll()
		{
			if (!this._dataSource.ItemPreview.IsSelected)
			{
				this._dataSource.ExecuteBuyAllItems();
				UISoundsHelper.PlayUISound("event:/ui/inventory/take_all");
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000581F File Offset: 0x00003A1F
		public void ExecuteGiveAll()
		{
			if (!this._dataSource.ItemPreview.IsSelected)
			{
				this._dataSource.ExecuteSellAllItems();
				UISoundsHelper.PlayUISound("event:/ui/inventory/take_all");
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005848 File Offset: 0x00003A48
		public void ExecuteBuyConsumableItem()
		{
			this._dataSource.ExecuteBuyItemTest();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005855 File Offset: 0x00003A55
		private ItemObject.ItemUsageSetFlags GetItemUsageSetFlag(WeaponComponentData item)
		{
			if (!string.IsNullOrEmpty(item.ItemUsage))
			{
				return MBItem.GetItemUsageSetFlags(item.ItemUsage);
			}
			return (ItemObject.ItemUsageSetFlags)0;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005871 File Offset: 0x00003A71
		private void CloseInventoryScreen()
		{
			InventoryManager.Instance.CloseInventoryPresentation(false);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000587E File Offset: 0x00003A7E
		bool IChangeableScreen.AnyUnsavedChanges()
		{
			return this.InventoryState.InventoryLogic.IsThereAnyChanges();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005890 File Offset: 0x00003A90
		bool IChangeableScreen.CanChangesBeApplied()
		{
			return this.InventoryState.InventoryLogic.CanPlayerCompleteTransaction();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000058A2 File Offset: 0x00003AA2
		void IChangeableScreen.ApplyChanges()
		{
			this._dataSource.ItemPreview.Close();
			this.InventoryState.InventoryLogic.DoneLogic();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000058C5 File Offset: 0x00003AC5
		void IChangeableScreen.ResetChanges()
		{
			this.InventoryState.InventoryLogic.Reset(true);
		}

		// Token: 0x04000033 RID: 51
		private IGauntletMovie _gauntletMovie;

		// Token: 0x04000034 RID: 52
		private SPInventoryVM _dataSource;

		// Token: 0x04000035 RID: 53
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000036 RID: 54
		private bool _closed;

		// Token: 0x04000037 RID: 55
		private bool _openedFromMission;

		// Token: 0x04000038 RID: 56
		private SpriteCategory _inventoryCategory;
	}
}
