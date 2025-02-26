﻿using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x02000006 RID: 6
	[GameStateScreen(typeof(ClanState))]
	public class GauntletClanScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000261E File Offset: 0x0000081E
		public GauntletClanScreen(ClanState clanState)
		{
			this._clanState = clanState;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002630 File Offset: 0x00000830
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			LoadingWindow.DisableGlobalLoadingWindow();
			this._dataSource.CanSwitchTabs = (!Input.IsGamepadActive || (!InformationManager.GetIsAnyTooltipActiveAndExtended() && this._gauntletLayer.IsHitThisFrame));
			ClanManagementVM dataSource = this._dataSource;
			bool flag;
			if (dataSource == null)
			{
				flag = false;
			}
			else
			{
				ClanCardSelectionPopupVM cardSelectionPopup = dataSource.CardSelectionPopup;
				bool? flag2 = (cardSelectionPopup != null) ? new bool?(cardSelectionPopup.IsVisible) : null;
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
			}
			if (flag)
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.CardSelectionPopup.ExecuteDone();
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.CardSelectionPopup.ExecuteCancel();
					return;
				}
			}
			else
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("Exit") || this._gauntletLayer.Input.IsGameKeyPressed(41) || this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
				{
					this.CloseClanScreen();
					return;
				}
				if (this._dataSource.CanSwitchTabs)
				{
					if (this._gauntletLayer.Input.IsHotKeyReleased("SwitchToPreviousTab"))
					{
						UISoundsHelper.PlayUISound("event:/ui/tab");
						this._dataSource.SelectPreviousCategory();
						return;
					}
					if (this._gauntletLayer.Input.IsHotKeyReleased("SwitchToNextTab"))
					{
						UISoundsHelper.PlayUISound("event:/ui/tab");
						this._dataSource.SelectNextCategory();
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027CB File Offset: 0x000009CB
		private void OpenPartyScreenForNewClanParty(Hero hero)
		{
			this._isCreatingPartyWithMembers = true;
			PartyScreenManager.OpenScreenAsCreateClanPartyForHero(hero, null, null);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027DC File Offset: 0x000009DC
		private void OpenBannerEditorWithPlayerClan()
		{
			Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<BannerEditorState>(), 0);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002800 File Offset: 0x00000A00
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._clanCategory = spriteData.SpriteCategories["ui_clan"];
			this._clanCategory.Load(resourceContext, uiresourceDepot);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", true);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			base.AddLayer(this._gauntletLayer);
			this._dataSource = new ClanManagementVM(new Action(this.CloseClanScreen), new Action<Hero>(this.ShowHeroOnMap), new Action<Hero>(this.OpenPartyScreenForNewClanParty), new Action(this.OpenBannerEditorWithPlayerClan));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetPreviousTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToPreviousTab"));
			this._dataSource.SetNextTabInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("SwitchToNextTab"));
			if (this._isCreatingPartyWithMembers)
			{
				this._dataSource.SelectParty(PartyBase.MainParty);
				this._isCreatingPartyWithMembers = false;
			}
			else if (this._clanState.InitialSelectedHero != null)
			{
				this._dataSource.SelectHero(this._clanState.InitialSelectedHero);
			}
			else if (this._clanState.InitialSelectedParty != null)
			{
				this._dataSource.SelectParty(this._clanState.InitialSelectedParty);
				if (this._clanState.InitialSelectedParty.LeaderHero == null)
				{
					ClanPartiesVM clanParties = this._dataSource.ClanParties;
					bool flag;
					if (clanParties == null)
					{
						flag = false;
					}
					else
					{
						ClanPartyItemVM currentSelectedParty = clanParties.CurrentSelectedParty;
						bool? flag2 = (currentSelectedParty != null) ? new bool?(currentSelectedParty.IsChangeLeaderEnabled) : null;
						bool flag3 = true;
						flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
					}
					if (flag)
					{
						this._dataSource.ClanParties.OnShowChangeLeaderPopup();
					}
				}
			}
			else if (this._clanState.InitialSelectedSettlement != null)
			{
				this._dataSource.SelectSettlement(this._clanState.InitialSelectedSettlement);
			}
			else if (this._clanState.InitialSelectedWorkshop != null)
			{
				this._dataSource.SelectWorkshop(this._clanState.InitialSelectedWorkshop);
			}
			else if (this._clanState.InitialSelectedAlley != null)
			{
				this._dataSource.SelectAlley(this._clanState.InitialSelectedAlley);
			}
			this._gauntletLayer.LoadMovie("ClanScreen", this._dataSource);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.ClanScreen));
			UISoundsHelper.PlayUISound("event:/ui/panels/panel_clan_open");
			this._gauntletLayer.GamepadNavigationContext.GainNavigationAfterFrames(2, null);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private void ShowHeroOnMap(Hero hero)
		{
			Vec2 asVec = hero.GetPosition().AsVec2;
			this.CloseClanScreen();
			MapScreen.Instance.FastMoveCameraToPosition(asVec);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B25 File Offset: 0x00000D25
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
			base.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B65 File Offset: 0x00000D65
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B67 File Offset: 0x00000D67
		void IGameStateListener.OnFinalize()
		{
			this._clanCategory.Unload();
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer = null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B8D File Offset: 0x00000D8D
		protected override void OnActivate()
		{
			base.OnActivate();
			ClanManagementVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.UpdateBannerVisuals();
			}
			ClanManagementVM dataSource2 = this._dataSource;
			if (dataSource2 == null)
			{
				return;
			}
			dataSource2.RefreshCategoryValues();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002BB6 File Offset: 0x00000DB6
		private void CloseClanScreen()
		{
			Game.Current.GameStateManager.PopState(0);
			UISoundsHelper.PlayUISound("event:/ui/default");
		}

		// Token: 0x04000006 RID: 6
		private ClanManagementVM _dataSource;

		// Token: 0x04000007 RID: 7
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000008 RID: 8
		private SpriteCategory _clanCategory;

		// Token: 0x04000009 RID: 9
		private readonly ClanState _clanState;

		// Token: 0x0400000A RID: 10
		private bool _isCreatingPartyWithMembers;
	}
}
