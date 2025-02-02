using System;
using SandBox.View;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x02000005 RID: 5
	[GameStateScreen(typeof(CharacterDeveloperState))]
	public class GauntletCharacterDeveloperScreen : ScreenBase, IGameStateListener, IChangeableScreen, ICharacterDeveloperStateHandler
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000218F File Offset: 0x0000038F
		public GauntletCharacterDeveloperScreen(CharacterDeveloperState clanState)
		{
			this._characterDeveloperState = clanState;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021A0 File Offset: 0x000003A0
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			LoadingWindow.DisableGlobalLoadingWindow();
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit") || this._gauntletLayer.Input.IsGameKeyPressed(37))
			{
				if (this._dataSource.CurrentCharacter.IsInspectingAnAttribute)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.CurrentCharacter.ExecuteStopInspectingCurrentAttribute();
					return;
				}
				if (this._dataSource.CurrentCharacter.PerkSelection.IsActive)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.CurrentCharacter.PerkSelection.ExecuteDeactivate();
					return;
				}
				this.CloseCharacterDeveloperScreen();
				return;
			}
			else
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
				{
					this.ExecuteConfirm();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyReleased("Reset"))
				{
					this.ExecuteReset();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyPressed("SwitchToPreviousTab"))
				{
					this.ExecuteSwitchToPreviousTab();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyPressed("SwitchToNextTab"))
				{
					this.ExecuteSwitchToNextTab();
				}
				return;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C8 File Offset: 0x000004C8
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._characterdeveloper = spriteData.SpriteCategories["ui_characterdeveloper"];
			this._characterdeveloper.Load(resourceContext, uiresourceDepot);
			this._dataSource = new CharacterDeveloperVM(new Action(this.CloseCharacterDeveloperScreen));
			this._dataSource.SetGetKeyTextFromKeyIDFunc(new Func<string, TextObject>(Game.Current.GameTextManager.GetHotKeyGameTextFromKeyID));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			this._dataSource.SetPreviousCharacterInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
			this._dataSource.SetNextCharacterInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
			if (this._characterDeveloperState.InitialSelectedHero != null)
			{
				this._dataSource.SelectHero(this._characterDeveloperState.InitialSelectedHero);
			}
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", true);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._gauntletLayer.LoadMovie("CharacterDeveloper", this._dataSource);
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.CharacterScreen));
			UISoundsHelper.PlayUISound("event:/ui/panels/panel_character_open");
			this._gauntletLayer.GamepadNavigationContext.GainNavigationAfterFrames(2, null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024C8 File Offset: 0x000006C8
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
			base.RemoveLayer(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002508 File Offset: 0x00000708
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000250A File Offset: 0x0000070A
		void IGameStateListener.OnFinalize()
		{
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer = null;
			this._characterdeveloper.Unload();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002530 File Offset: 0x00000730
		private void CloseCharacterDeveloperScreen()
		{
			UISoundsHelper.PlayUISound("event:/ui/default");
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000254C File Offset: 0x0000074C
		private void ExecuteConfirm()
		{
			UISoundsHelper.PlayUISound("event:/ui/default");
			this._dataSource.ExecuteDone();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002563 File Offset: 0x00000763
		private void ExecuteReset()
		{
			UISoundsHelper.PlayUISound("event:/ui/default");
			this._dataSource.ExecuteReset();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000257A File Offset: 0x0000077A
		private void ExecuteSwitchToPreviousTab()
		{
			MBBindingList<SelectorItemVM> itemList = this._dataSource.CharacterList.ItemList;
			if (itemList != null && itemList.Count > 1)
			{
				UISoundsHelper.PlayUISound("event:/ui/checkbox");
			}
			this._dataSource.CharacterList.ExecuteSelectPreviousItem();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025B7 File Offset: 0x000007B7
		private void ExecuteSwitchToNextTab()
		{
			MBBindingList<SelectorItemVM> itemList = this._dataSource.CharacterList.ItemList;
			if (itemList != null && itemList.Count > 1)
			{
				UISoundsHelper.PlayUISound("event:/ui/checkbox");
			}
			this._dataSource.CharacterList.ExecuteSelectNextItem();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025F4 File Offset: 0x000007F4
		bool IChangeableScreen.AnyUnsavedChanges()
		{
			return this._dataSource.IsThereAnyChanges();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002601 File Offset: 0x00000801
		bool IChangeableScreen.CanChangesBeApplied()
		{
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002604 File Offset: 0x00000804
		void IChangeableScreen.ApplyChanges()
		{
			this._dataSource.ApplyAllChanges();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002611 File Offset: 0x00000811
		void IChangeableScreen.ResetChanges()
		{
			this._dataSource.ExecuteReset();
		}

		// Token: 0x04000002 RID: 2
		private CharacterDeveloperVM _dataSource;

		// Token: 0x04000003 RID: 3
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000004 RID: 4
		private SpriteCategory _characterdeveloper;

		// Token: 0x04000005 RID: 5
		private readonly CharacterDeveloperState _characterDeveloperState;
	}
}
