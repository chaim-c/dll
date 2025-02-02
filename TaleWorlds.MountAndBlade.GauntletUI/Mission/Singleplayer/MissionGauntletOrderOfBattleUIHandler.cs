using System;
using System.Collections.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Order;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x02000030 RID: 48
	[OverrideView(typeof(MissionOrderOfBattleUIHandler))]
	public class MissionGauntletOrderOfBattleUIHandler : MissionView
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public MissionGauntletOrderOfBattleUIHandler(OrderOfBattleVM dataSource)
		{
			this._dataSource = dataSource;
			this.ViewOrderPriority = 13;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._deploymentMissionView = base.Mission.GetMissionBehavior<DeploymentMissionView>();
			DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
			deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Combine(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
			this._playerRoleMissionController = base.Mission.GetMissionBehavior<AssignPlayerRoleInTeamMissionController>();
			this._playerRoleMissionController.OnPlayerTurnToChooseFormationToLead += this.OnPlayerTurnToChooseFormationToLead;
			this._playerRoleMissionController.OnAllFormationsAssignedSergeants += this.OnAllFormationsAssignedSergeants;
			this._orderUIHandler = base.Mission.GetMissionBehavior<MissionGauntletSingleplayerOrderUIHandler>();
			this._orderUIHandler.OnCameraControlsToggled += this.OnCameraControlsToggled;
			this._orderTroopPlacer = base.Mission.GetMissionBehavior<OrderTroopPlacer>();
			OrderTroopPlacer orderTroopPlacer = this._orderTroopPlacer;
			orderTroopPlacer.OnUnitDeployed = (Action)Delegate.Combine(orderTroopPlacer.OnUnitDeployed, new Action(this.OnUnitDeployed));
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._movie = this._gauntletLayer.LoadMovie("OrderOfBattle", this._dataSource);
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._orderOfBattleCategory = spriteData.SpriteCategories["ui_order_of_battle"];
			this._orderOfBattleCategory.Load(resourceContext, uiresourceDepot);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		public override bool IsReady()
		{
			return this._isDeploymentFinished || this._orderOfBattleCategory.IsLoaded;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._isActive)
			{
				this.TickInput();
				this._dataSource.Tick();
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		private void TickInput()
		{
			bool flag;
			bool flag2;
			bool flag3;
			this.HandleLayerFocus(out flag, out flag2, out flag3);
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				if (flag2)
				{
					this._dataSource.ExecuteDisableAllClassSelections();
				}
				else if (flag3)
				{
					this._dataSource.ExecuteDisableAllFilterSelections();
				}
				else if (flag)
				{
					this._dataSource.ExecuteClearHeroSelection();
				}
			}
			if (base.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton) || base.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.ControllerLTrigger))
			{
				this._gauntletLayer.InputRestrictions.SetMouseVisibility(false);
				return;
			}
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		private void HandleLayerFocus(out bool isAnyHeroSelected, out bool isClassSelectionEnabled, out bool isFilterSelectionEnabled)
		{
			isAnyHeroSelected = this._dataSource.HasSelectedHeroes;
			isClassSelectionEnabled = this._dataSource.IsAnyClassSelectionEnabled();
			isFilterSelectionEnabled = this._dataSource.IsAnyFilterSelectionEnabled();
			bool flag = isAnyHeroSelected | isClassSelectionEnabled | isFilterSelectionEnabled;
			if (this._gauntletLayer.IsFocusLayer && !flag)
			{
				base.MissionScreen.SetDisplayDialog(false);
				this._gauntletLayer.IsFocusLayer = false;
				ScreenManager.TryLoseFocus(this._gauntletLayer);
				return;
			}
			if (!this._gauntletLayer.IsFocusLayer && flag)
			{
				base.MissionScreen.SetDisplayDialog(true);
				this._gauntletLayer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(this._gauntletLayer);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public override void OnMissionScreenFinalize()
		{
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer.ReleaseMovie(this._movie);
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._orderOfBattleCategory.Unload();
			base.OnMissionScreenFinalize();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		public override bool OnEscape()
		{
			bool flag = false;
			if (this._orderUIHandler != null && this._orderUIHandler.IsOrderMenuActive)
			{
				flag = this._orderUIHandler.OnEscape();
			}
			if (!flag)
			{
				flag = this._dataSource.OnEscape();
			}
			return flag;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000CE2C File Offset: 0x0000B02C
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000CE49 File Offset: 0x0000B049
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000CE66 File Offset: 0x0000B066
		public override bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return !this._isActive;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000CE74 File Offset: 0x0000B074
		private void OnPlayerTurnToChooseFormationToLead(Dictionary<int, Agent> lockedFormationIndicesAndSergeants, List<int> remainingFormationIndices)
		{
			if (base.Mission.PlayerTeam == null)
			{
				Debug.FailedAssert("Player team must be initialized before OOB", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\Mission\\Singleplayer\\MissionGauntletOrderOfBattleUIHandler.cs", "OnPlayerTurnToChooseFormationToLead", 199);
			}
			this._cachedOrderTypeSetting = ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.OrderType);
			ManagedOptions.SetConfig(ManagedOptions.ManagedOptionsType.OrderType, 1f);
			this._dataSource.Initialize(base.Mission, base.MissionScreen.CombatCamera, new Action<int>(this.SelectFormationAtIndex), new Action<int>(this.DeselectFormationAtIndex), new Action(this.ClearFormationSelection), new Action(this.OnAutoDeploy), new Action(this.OnBeginMission), lockedFormationIndicesAndSergeants);
			this._orderUIHandler.SetIsOrderPreconfigured(this._dataSource.IsOrderPreconfigured);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._isActive = true;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CF48 File Offset: 0x0000B148
		private void OnAllFormationsAssignedSergeants(Dictionary<int, Agent> formationsWithLooselyAssignedSergeants)
		{
			this._dataSource.OnAllFormationsAssignedSergeants(formationsWithLooselyAssignedSergeants);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CF58 File Offset: 0x0000B158
		private void OnDeploymentFinish()
		{
			bool playerDeployed = MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle();
			this._dataSource.OnDeploymentFinalized(playerDeployed);
			if (this._isActive)
			{
				ManagedOptions.SetConfig(ManagedOptions.ManagedOptionsType.OrderType, this._cachedOrderTypeSetting);
				this._isActive = false;
				this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
			}
			this._isDeploymentFinished = true;
			DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
			deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Remove(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
			this._playerRoleMissionController.OnPlayerTurnToChooseFormationToLead -= this.OnPlayerTurnToChooseFormationToLead;
			this._playerRoleMissionController.OnAllFormationsAssignedSergeants -= this.OnAllFormationsAssignedSergeants;
			OrderTroopPlacer orderTroopPlacer = this._orderTroopPlacer;
			orderTroopPlacer.OnUnitDeployed = (Action)Delegate.Remove(orderTroopPlacer.OnUnitDeployed, new Action(this.OnUnitDeployed));
			this._orderUIHandler.OnCameraControlsToggled -= this.OnCameraControlsToggled;
			this._orderOfBattleCategory.Unload();
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = false;
			base.MissionScreen.SetDisplayDialog(false);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000D076 File Offset: 0x0000B276
		private void OnCameraControlsToggled(bool isEnabled)
		{
			this._dataSource.AreCameraControlsEnabled = isEnabled;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D084 File Offset: 0x0000B284
		private void SelectFormationAtIndex(int index)
		{
			MissionGauntletSingleplayerOrderUIHandler orderUIHandler = this._orderUIHandler;
			if (orderUIHandler == null)
			{
				return;
			}
			orderUIHandler.SelectFormationAtIndex(index);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D097 File Offset: 0x0000B297
		private void DeselectFormationAtIndex(int index)
		{
			MissionGauntletSingleplayerOrderUIHandler orderUIHandler = this._orderUIHandler;
			if (orderUIHandler == null)
			{
				return;
			}
			orderUIHandler.DeselectFormationAtIndex(index);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D0AA File Offset: 0x0000B2AA
		private void ClearFormationSelection()
		{
			MissionGauntletSingleplayerOrderUIHandler orderUIHandler = this._orderUIHandler;
			if (orderUIHandler == null)
			{
				return;
			}
			orderUIHandler.ClearFormationSelection();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		private void OnAutoDeploy()
		{
			this._orderUIHandler.OnAutoDeploy();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D0C9 File Offset: 0x0000B2C9
		private void OnBeginMission()
		{
			this._orderUIHandler.OnBeginMission();
			this._orderUIHandler.OnFiltersSet(this._dataSource.CurrentConfiguration);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		private void OnUnitDeployed()
		{
			this._dataSource.OnUnitDeployed();
		}

		// Token: 0x0400012B RID: 299
		private OrderOfBattleVM _dataSource;

		// Token: 0x0400012C RID: 300
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400012D RID: 301
		private IGauntletMovie _movie;

		// Token: 0x0400012E RID: 302
		private SpriteCategory _orderOfBattleCategory;

		// Token: 0x0400012F RID: 303
		private DeploymentMissionView _deploymentMissionView;

		// Token: 0x04000130 RID: 304
		private MissionGauntletSingleplayerOrderUIHandler _orderUIHandler;

		// Token: 0x04000131 RID: 305
		private AssignPlayerRoleInTeamMissionController _playerRoleMissionController;

		// Token: 0x04000132 RID: 306
		private OrderTroopPlacer _orderTroopPlacer;

		// Token: 0x04000133 RID: 307
		private bool _isActive;

		// Token: 0x04000134 RID: 308
		private bool _isDeploymentFinished;

		// Token: 0x04000135 RID: 309
		private float _cachedOrderTypeSetting;
	}
}
