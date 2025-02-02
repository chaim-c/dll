using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200002B RID: 43
	[OverrideView(typeof(MissionAgentLockVisualizerView))]
	public class MissionGauntletAgentLockVisualizerView : MissionGauntletBattleUIBase
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000BA50 File Offset: 0x00009C50
		protected override void OnCreateView()
		{
			this._missionMainAgentController = base.Mission.GetMissionBehavior<MissionMainAgentController>();
			this._missionMainAgentController.OnLockedAgentChanged += this.OnLockedAgentChanged;
			this._missionMainAgentController.OnPotentialLockedAgentChanged += this.OnPotentialLockedAgentChanged;
			this._dataSource = new MissionAgentLockVisualizerVM();
			this._layer = new GauntletLayer(10, "GauntletLayer", false);
			this._layer.LoadMovie("AgentLockTargets", this._dataSource);
			base.MissionScreen.AddLayer(this._layer);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000BAE2 File Offset: 0x00009CE2
		protected override void OnDestroyView()
		{
			base.MissionScreen.RemoveLayer(this._layer);
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._layer = null;
			this._missionMainAgentController = null;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000BB15 File Offset: 0x00009D15
		private void OnPotentialLockedAgentChanged(Agent newPotentialAgent)
		{
			MissionAgentLockVisualizerVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.IsEnabled)
			{
				this._dataSource.OnPossibleLockAgentChange(this._latestPotentialLockedAgent, newPotentialAgent);
				this._latestPotentialLockedAgent = newPotentialAgent;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000BB44 File Offset: 0x00009D44
		private void OnLockedAgentChanged(Agent newAgent)
		{
			MissionAgentLockVisualizerVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.IsEnabled)
			{
				this._dataSource.OnActiveLockAgentChange(this._latestLockedAgent, newAgent);
				this._latestLockedAgent = newAgent;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000BB74 File Offset: 0x00009D74
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.IsViewActive && this._dataSource != null)
			{
				this._dataSource.IsEnabled = this.IsMainAgentAvailable();
				if (this._dataSource.IsEnabled)
				{
					for (int i = 0; i < this._dataSource.AllTrackedAgents.Count; i++)
					{
						MissionAgentLockItemVM missionAgentLockItemVM = this._dataSource.AllTrackedAgents[i];
						float a = 0f;
						float b = 0f;
						float num = 0f;
						MBWindowManager.WorldToScreenInsideUsableArea(base.MissionScreen.CombatCamera, missionAgentLockItemVM.TrackedAgent.GetChestGlobalPosition(), ref a, ref b, ref num);
						missionAgentLockItemVM.Position = new Vec2(a, b);
					}
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000BC2B File Offset: 0x00009E2B
		private bool IsMainAgentAvailable()
		{
			Agent main = Agent.Main;
			return main != null && main.IsActive();
		}

		// Token: 0x04000113 RID: 275
		private GauntletLayer _layer;

		// Token: 0x04000114 RID: 276
		private MissionAgentLockVisualizerVM _dataSource;

		// Token: 0x04000115 RID: 277
		private MissionMainAgentController _missionMainAgentController;

		// Token: 0x04000116 RID: 278
		private Agent _latestLockedAgent;

		// Token: 0x04000117 RID: 279
		private Agent _latestPotentialLockedAgent;
	}
}
