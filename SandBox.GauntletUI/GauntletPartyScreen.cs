using System;
using SandBox.View;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
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
	// Token: 0x0200000C RID: 12
	[GameStateScreen(typeof(PartyState))]
	public class GauntletPartyScreen : ScreenBase, IGameStateListener, IChangeableScreen
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000608F File Offset: 0x0000428F
		public GauntletPartyScreen(PartyState partyState)
		{
			this._partyState = partyState;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000060A0 File Offset: 0x000042A0
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			LoadingWindow.DisableGlobalLoadingWindow();
			this._dataSource.IsFiveStackModifierActive = this._gauntletLayer.Input.IsHotKeyDown("FiveStackModifier");
			this._dataSource.IsEntireStackModifierActive = this._gauntletLayer.Input.IsHotKeyDown("EntireStackModifier");
			if (!this._partyState.IsActive || this._gauntletLayer.Input.IsHotKeyDownAndReleased("Exit") || (!this._gauntletLayer.Input.IsControlDown() && this._gauntletLayer.Input.IsGameKeyDownAndReleased(43)))
			{
				this.HandleCancelInput();
				return;
			}
			if (this._gauntletLayer.Input.IsHotKeyDownAndReleased("Confirm"))
			{
				this.HandleDoneInput();
				return;
			}
			if (this._gauntletLayer.Input.IsHotKeyDownAndReleased("Reset"))
			{
				this.HandleResetInput();
				return;
			}
			if (!this._dataSource.IsAnyPopUpOpen)
			{
				if (this._gauntletLayer.Input.IsHotKeyPressed("TakeAllTroops"))
				{
					if (this._dataSource.IsOtherTroopsHaveTransferableTroops)
					{
						UISoundsHelper.PlayUISound("event:/ui/inventory/take_all");
						this._dataSource.ExecuteTransferAllOtherTroops();
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("GiveAllTroops"))
				{
					if (this._dataSource.IsMainTroopsHaveTransferableTroops)
					{
						UISoundsHelper.PlayUISound("event:/ui/inventory/take_all");
						this._dataSource.ExecuteTransferAllMainTroops();
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("TakeAllPrisoners"))
				{
					if (this._dataSource.CurrentFocusedCharacter != null && Input.IsGamepadActive)
					{
						if (this._dataSource.CurrentFocusedCharacter.IsTroopTransferrable && this._dataSource.CurrentFocusedCharacter.Side == PartyScreenLogic.PartyRosterSide.Left)
						{
							this._dataSource.CurrentFocusedCharacter.ExecuteTransferSingle();
							UISoundsHelper.PlayUISound("event:/ui/transfer");
							return;
						}
					}
					else if (this._dataSource.IsOtherPrisonersHaveTransferableTroops)
					{
						UISoundsHelper.PlayUISound("event:/ui/inventory/take_all");
						this._dataSource.ExecuteTransferAllOtherPrisoners();
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("GiveAllPrisoners"))
				{
					if (this._dataSource.CurrentFocusedCharacter != null && Input.IsGamepadActive)
					{
						if (this._dataSource.CurrentFocusedCharacter.IsTroopTransferrable && this._dataSource.CurrentFocusedCharacter.Side == PartyScreenLogic.PartyRosterSide.Right)
						{
							this._dataSource.CurrentFocusedCharacter.ExecuteTransferSingle();
							UISoundsHelper.PlayUISound("event:/ui/transfer");
							return;
						}
					}
					else if (this._dataSource.IsMainPrisonersHaveTransferableTroops)
					{
						UISoundsHelper.PlayUISound("event:/ui/inventory/take_all");
						this._dataSource.ExecuteTransferAllMainPrisoners();
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("OpenUpgradePopup"))
				{
					if (!this._dataSource.IsUpgradePopUpDisabled)
					{
						this._dataSource.ExecuteOpenUpgradePopUp();
						UISoundsHelper.PlayUISound("event:/ui/default");
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsHotKeyPressed("OpenRecruitPopup"))
				{
					if (!this._dataSource.IsRecruitPopUpDisabled)
					{
						this._dataSource.ExecuteOpenRecruitPopUp();
						UISoundsHelper.PlayUISound("event:/ui/default");
						return;
					}
				}
				else if (this._gauntletLayer.Input.IsGameKeyReleased(39) && this._dataSource.CurrentFocusedCharacter != null && Input.IsGamepadActive)
				{
					this._dataSource.CurrentFocusedCharacter.ExecuteOpenTroopEncyclopedia();
					return;
				}
			}
			else if (this._gauntletLayer.Input.IsHotKeyPressed("PopupItemPrimaryAction"))
			{
				PartyUpgradeTroopVM upgradePopUp = this._dataSource.UpgradePopUp;
				if (upgradePopUp != null && upgradePopUp.IsOpen)
				{
					if (this._dataSource.UpgradePopUp.IsFocusedOnACharacter && this._dataSource.UpgradePopUp.FocusedTroop.PartyCharacter.Upgrades.Count > 0 && this._dataSource.UpgradePopUp.FocusedTroop.PartyCharacter.Upgrades[0].IsAvailable)
					{
						UISoundsHelper.PlayUISound("event:/ui/party/upgrade");
					}
					this._dataSource.UpgradePopUp.ExecuteItemPrimaryAction();
					return;
				}
				PartyRecruitTroopVM recruitPopUp = this._dataSource.RecruitPopUp;
				if (recruitPopUp != null && recruitPopUp.IsOpen)
				{
					this._dataSource.RecruitPopUp.ExecuteItemPrimaryAction();
					return;
				}
			}
			else if (this._gauntletLayer.Input.IsHotKeyDownAndReleased("PopupItemSecondaryAction"))
			{
				PartyUpgradeTroopVM upgradePopUp2 = this._dataSource.UpgradePopUp;
				if (upgradePopUp2 != null && upgradePopUp2.IsOpen)
				{
					if (this._dataSource.UpgradePopUp.IsFocusedOnACharacter && this._dataSource.UpgradePopUp.FocusedTroop.PartyCharacter.Upgrades.Count > 1 && this._dataSource.UpgradePopUp.FocusedTroop.PartyCharacter.Upgrades[1].IsAvailable)
					{
						UISoundsHelper.PlayUISound("event:/ui/party/upgrade");
					}
					this._dataSource.UpgradePopUp.ExecuteItemSecondaryAction();
					return;
				}
				PartyRecruitTroopVM recruitPopUp2 = this._dataSource.RecruitPopUp;
				if (recruitPopUp2 != null && recruitPopUp2.IsOpen)
				{
					PartyTroopManagerItemVM focusedTroop = this._dataSource.RecruitPopUp.FocusedTroop;
					if (focusedTroop != null && focusedTroop.PartyCharacter.IsTroopRecruitable)
					{
						UISoundsHelper.PlayUISound("event:/ui/party/recruit_prisoner");
					}
					this._dataSource.RecruitPopUp.ExecuteItemSecondaryAction();
					return;
				}
			}
			else if (Input.IsGamepadActive && this._gauntletLayer.Input.IsGameKeyReleased(39))
			{
				PartyRecruitTroopVM recruitPopUp3 = this._dataSource.RecruitPopUp;
				if (recruitPopUp3 != null && recruitPopUp3.IsOpen && this._dataSource.RecruitPopUp.FocusedTroop != null)
				{
					this._dataSource.RecruitPopUp.FocusedTroop.PartyCharacter.ExecuteOpenTroopEncyclopedia();
					return;
				}
				PartyUpgradeTroopVM upgradePopUp3 = this._dataSource.UpgradePopUp;
				if (upgradePopUp3 != null && upgradePopUp3.IsOpen)
				{
					if (this._dataSource.UpgradePopUp.FocusedTroop != null)
					{
						this._dataSource.UpgradePopUp.FocusedTroop.ExecuteOpenTroopEncyclopedia();
						return;
					}
					if (this._dataSource.CurrentFocusedUpgrade != null)
					{
						this._dataSource.CurrentFocusedUpgrade.ExecuteUpgradeEncyclopediaLink();
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000669C File Offset: 0x0000489C
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._partyscreenCategory = spriteData.SpriteCategories["ui_partyscreen"];
			this._partyscreenCategory.Load(resourceContext, uiresourceDepot);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", true);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("PartyHotKeyCategory"));
			this._dataSource = new PartyVM(this._partyState.PartyScreenLogic);
			this._dataSource.SetGetKeyTextFromKeyIDFunc(new Func<string, TextObject>(Game.Current.GameTextManager.GetHotKeyGameTextFromKeyID));
			this._dataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetTakeAllTroopsInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("TakeAllTroops"));
			this._dataSource.SetDismissAllTroopsInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("GiveAllTroops"));
			this._dataSource.SetTakeAllPrisonersInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("TakeAllPrisoners"));
			this._dataSource.SetDismissAllPrisonersInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("GiveAllPrisoners"));
			this._dataSource.SetOpenUpgradePanelInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("OpenUpgradePopup"));
			this._dataSource.SetOpenRecruitPanelInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("OpenRecruitPopup"));
			this._dataSource.UpgradePopUp.SetPrimaryActionInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("PopupItemPrimaryAction"));
			this._dataSource.UpgradePopUp.SetSecondaryActionInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("PopupItemSecondaryAction"));
			this._dataSource.RecruitPopUp.SetPrimaryActionInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("PopupItemPrimaryAction"));
			this._dataSource.RecruitPopUp.SetSecondaryActionInputKey(HotKeyManager.GetCategory("PartyHotKeyCategory").GetHotKey("PopupItemSecondaryAction"));
			string fiveStackShortcutkeyText = this.GetFiveStackShortcutkeyText();
			this._dataSource.SetFiveStackShortcutKeyText(fiveStackShortcutkeyText);
			string entireStackShortcutkeyText = this.GetEntireStackShortcutkeyText();
			this._dataSource.SetEntireStackShortcutKeyText(entireStackShortcutkeyText);
			this._partyState.Handler = this._dataSource;
			this._gauntletLayer.LoadMovie("PartyScreen", this._dataSource);
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.PartyScreen));
			UISoundsHelper.PlayUISound("event:/ui/panels/panel_party_open");
			this._gauntletLayer.GamepadNavigationContext.GainNavigationAfterFrames(2, null);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000069D8 File Offset: 0x00004BD8
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
			PartyBase.MainParty.SetVisualAsDirty();
			this._gauntletLayer.IsFocusLayer = false;
			this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
			base.RemoveLayer(this._gauntletLayer);
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
			if (Campaign.Current.ConversationManager.IsConversationInProgress && !Campaign.Current.ConversationManager.IsConversationFlowActive)
			{
				Campaign.Current.ConversationManager.OnConversationActivate();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006A6E File Offset: 0x00004C6E
		void IGameStateListener.OnInitialize()
		{
			CampaignEvents.CompanionRemoved.AddNonSerializedListener(this, new Action<Hero, RemoveCompanionAction.RemoveCompanionDetail>(this.OnCompanionRemoved));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006A87 File Offset: 0x00004C87
		void IGameStateListener.OnFinalize()
		{
			CampaignEvents.CompanionRemoved.ClearListeners(this);
			this._dataSource.OnFinalize();
			this._partyscreenCategory.Unload();
			this._dataSource = null;
			this._gauntletLayer = null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006AB8 File Offset: 0x00004CB8
		protected override void OnResume()
		{
			base.OnResume();
			PartyVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.IsInConversation)
			{
				this._dataSource.IsInConversation = false;
				if (this._dataSource.PartyScreenLogic.IsDoneActive())
				{
					this._dataSource.PartyScreenLogic.DoneLogic(false);
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006B0F File Offset: 0x00004D0F
		private void HandleResetInput()
		{
			if (!this._dataSource.IsAnyPopUpOpen)
			{
				this._dataSource.ExecuteReset();
				UISoundsHelper.PlayUISound("event:/ui/default");
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006B34 File Offset: 0x00004D34
		private void HandleCancelInput()
		{
			PartyUpgradeTroopVM upgradePopUp = this._dataSource.UpgradePopUp;
			if (upgradePopUp != null && upgradePopUp.IsOpen)
			{
				this._dataSource.UpgradePopUp.ExecuteCancel();
			}
			else
			{
				PartyRecruitTroopVM recruitPopUp = this._dataSource.RecruitPopUp;
				if (recruitPopUp != null && recruitPopUp.IsOpen)
				{
					this._dataSource.RecruitPopUp.ExecuteCancel();
				}
				else
				{
					this._dataSource.ExecuteCancel();
				}
			}
			UISoundsHelper.PlayUISound("event:/ui/default");
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006BAC File Offset: 0x00004DAC
		private void HandleDoneInput()
		{
			PartyUpgradeTroopVM upgradePopUp = this._dataSource.UpgradePopUp;
			if (upgradePopUp != null && upgradePopUp.IsOpen)
			{
				this._dataSource.UpgradePopUp.ExecuteDone();
			}
			else
			{
				PartyRecruitTroopVM recruitPopUp = this._dataSource.RecruitPopUp;
				if (recruitPopUp != null && recruitPopUp.IsOpen)
				{
					this._dataSource.RecruitPopUp.ExecuteDone();
				}
				else
				{
					this._dataSource.ExecuteDone();
				}
			}
			UISoundsHelper.PlayUISound("event:/ui/default");
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006C24 File Offset: 0x00004E24
		private void OnCompanionRemoved(Hero arg1, RemoveCompanionAction.RemoveCompanionDetail arg2)
		{
			((IChangeableScreen)this).ApplyChanges();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006C2C File Offset: 0x00004E2C
		private string GetFiveStackShortcutkeyText()
		{
			if (!Input.IsControllerConnected || Input.IsMouseActive)
			{
				return Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", "anyshift").ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006C66 File Offset: 0x00004E66
		private string GetEntireStackShortcutkeyText()
		{
			if (!Input.IsControllerConnected || Input.IsMouseActive)
			{
				return Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", "anycontrol").ToString();
			}
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006C9C File Offset: 0x00004E9C
		bool IChangeableScreen.AnyUnsavedChanges()
		{
			return this._partyState.PartyScreenLogic.IsThereAnyChanges();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006CAE File Offset: 0x00004EAE
		bool IChangeableScreen.CanChangesBeApplied()
		{
			return this._partyState.PartyScreenLogic.IsDoneActive();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006CC0 File Offset: 0x00004EC0
		void IChangeableScreen.ApplyChanges()
		{
			this._partyState.PartyScreenLogic.DoneLogic(true);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006CD4 File Offset: 0x00004ED4
		void IChangeableScreen.ResetChanges()
		{
			this._partyState.PartyScreenLogic.Reset(true);
		}

		// Token: 0x04000040 RID: 64
		private PartyVM _dataSource;

		// Token: 0x04000041 RID: 65
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000042 RID: 66
		private SpriteCategory _partyscreenCategory;

		// Token: 0x04000043 RID: 67
		private readonly PartyState _partyState;
	}
}
