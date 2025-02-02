using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000020 RID: 32
	[OverrideView(typeof(MissionAgentStatusUIHandler))]
	public class MissionGauntletAgentStatus : MissionGauntletBattleUIBase
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00008584 File Offset: 0x00006784
		public override void OnMissionStateActivated()
		{
			base.OnMissionStateActivated();
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnMainAgentWeaponChange();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000859C File Offset: 0x0000679C
		public override void EarlyStart()
		{
			base.EarlyStart();
			this._dataSource = new MissionAgentStatusVM(base.Mission, base.MissionScreen.CombatCamera, new Func<float>(base.MissionScreen.GetCameraToggleProgress));
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("MainAgentHUD", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			this._dataSource.TakenDamageController.SetIsEnabled(BannerlordConfig.EnableDamageTakenVisuals);
			this.RegisterInteractionEvents();
			CombatLogManager.OnGenerateCombatLog += this.OnGenerateCombatLog;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008667 File Offset: 0x00006867
		protected override void OnCreateView()
		{
			this._dataSource.IsAgentStatusAvailable = true;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008675 File Offset: 0x00006875
		protected override void OnDestroyView()
		{
			this._dataSource.IsAgentStatusAvailable = false;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008683 File Offset: 0x00006883
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.EnableDamageTakenVisuals)
			{
				MissionAgentStatusVM dataSource = this._dataSource;
				if (dataSource == null)
				{
					return;
				}
				dataSource.TakenDamageController.SetIsEnabled(BannerlordConfig.EnableDamageTakenVisuals);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000086A4 File Offset: 0x000068A4
		public override void AfterStart()
		{
			base.AfterStart();
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.InitializeMainAgentPropterties();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000086BC File Offset: 0x000068BC
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._isInDeployement = (base.Mission.GetMissionBehavior<BattleDeploymentHandler>() != null);
			if (this._isInDeployement)
			{
				this._deploymentMissionView = base.Mission.GetMissionBehavior<DeploymentMissionView>();
				if (this._deploymentMissionView != null)
				{
					DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
					deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Combine(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
				}
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000872B File Offset: 0x0000692B
		private void OnDeploymentFinish()
		{
			this._isInDeployement = false;
			DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
			deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Remove(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000875C File Offset: 0x0000695C
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			this.UnregisterInteractionEvents();
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			CombatLogManager.OnGenerateCombatLog -= this.OnGenerateCombatLog;
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this._missionMainAgentController = null;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000087DD File Offset: 0x000069DD
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			this._dataSource.IsInDeployement = this._isInDeployement;
			this._dataSource.Tick(dt);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008803 File Offset: 0x00006A03
		public override void OnFocusGained(Agent mainAgent, IFocusable focusableObject, bool isInteractable)
		{
			base.OnFocusGained(mainAgent, focusableObject, isInteractable);
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnFocusGained(mainAgent, focusableObject, isInteractable);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008821 File Offset: 0x00006A21
		public override void OnAgentInteraction(Agent userAgent, Agent agent)
		{
			base.OnAgentInteraction(userAgent, agent);
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnAgentInteraction(userAgent, agent);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000883D File Offset: 0x00006A3D
		public override void OnFocusLost(Agent agent, IFocusable focusableObject)
		{
			base.OnFocusLost(agent, focusableObject);
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnFocusLost(agent, focusableObject);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008859 File Offset: 0x00006A59
		public override void OnAgentDeleted(Agent affectedAgent)
		{
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnAgentDeleted(affectedAgent);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000886C File Offset: 0x00006A6C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			MissionAgentStatusVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnAgentRemoved(affectedAgent);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008880 File Offset: 0x00006A80
		private void OnGenerateCombatLog(CombatLogData logData)
		{
			if (logData.IsVictimAgentMine && logData.TotalDamage > 0 && logData.BodyPartHit != BoneBodyPartType.None)
			{
				MissionAgentStatusVM dataSource = this._dataSource;
				if (dataSource == null)
				{
					return;
				}
				dataSource.OnMainAgentHit(logData.TotalDamage, (float)(logData.IsRangedAttack ? 1 : 0));
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000088CC File Offset: 0x00006ACC
		private void RegisterInteractionEvents()
		{
			this._missionMainAgentController = base.Mission.GetMissionBehavior<MissionMainAgentController>();
			if (this._missionMainAgentController != null)
			{
				this._missionMainAgentController.InteractionComponent.OnFocusGained += this._dataSource.OnSecondaryFocusGained;
				this._missionMainAgentController.InteractionComponent.OnFocusLost += this._dataSource.OnSecondaryFocusLost;
				this._missionMainAgentController.InteractionComponent.OnFocusHealthChanged += this._dataSource.InteractionInterface.OnFocusedHealthChanged;
			}
			this._missionMainAgentEquipmentControllerView = base.Mission.GetMissionBehavior<MissionGauntletMainAgentEquipmentControllerView>();
			if (this._missionMainAgentEquipmentControllerView != null)
			{
				this._missionMainAgentEquipmentControllerView.OnEquipmentDropInteractionViewToggled += this._dataSource.OnEquipmentInteractionViewToggled;
				this._missionMainAgentEquipmentControllerView.OnEquipmentEquipInteractionViewToggled += this._dataSource.OnEquipmentInteractionViewToggled;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000089AC File Offset: 0x00006BAC
		private void UnregisterInteractionEvents()
		{
			if (this._missionMainAgentController != null)
			{
				this._missionMainAgentController.InteractionComponent.OnFocusGained -= this._dataSource.OnSecondaryFocusGained;
				this._missionMainAgentController.InteractionComponent.OnFocusLost -= this._dataSource.OnSecondaryFocusLost;
				this._missionMainAgentController.InteractionComponent.OnFocusHealthChanged -= this._dataSource.InteractionInterface.OnFocusedHealthChanged;
			}
			if (this._missionMainAgentEquipmentControllerView != null)
			{
				this._missionMainAgentEquipmentControllerView.OnEquipmentDropInteractionViewToggled -= this._dataSource.OnEquipmentInteractionViewToggled;
				this._missionMainAgentEquipmentControllerView.OnEquipmentEquipInteractionViewToggled -= this._dataSource.OnEquipmentInteractionViewToggled;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008A69 File Offset: 0x00006C69
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008A86 File Offset: 0x00006C86
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x040000C2 RID: 194
		private GauntletLayer _gauntletLayer;

		// Token: 0x040000C3 RID: 195
		private MissionAgentStatusVM _dataSource;

		// Token: 0x040000C4 RID: 196
		private MissionMainAgentController _missionMainAgentController;

		// Token: 0x040000C5 RID: 197
		private MissionGauntletMainAgentEquipmentControllerView _missionMainAgentEquipmentControllerView;

		// Token: 0x040000C6 RID: 198
		private DeploymentMissionView _deploymentMissionView;

		// Token: 0x040000C7 RID: 199
		private bool _isInDeployement;
	}
}
