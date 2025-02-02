using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200002E RID: 46
	[OverrideView(typeof(MissionSingleplayerKillNotificationUIHandler))]
	public class MissionGauntletKillNotificationSingleplayerUIHandler : MissionGauntletBattleUIBase
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000C67C File Offset: 0x0000A87C
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this.ViewOrderPriority = 17;
			this._isGeneralFeedEnabled = (BannerlordConfig.ReportCasualtiesType < 2);
			this._isPersonalFeedEnabled = BannerlordConfig.ReportPersonalDamage;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnOptionChange));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnOptionChange));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		protected override void OnCreateView()
		{
			this._dataSource = new SPKillFeedVM();
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("SingleplayerKillfeed", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			CombatLogManager.OnGenerateCombatLog += this.OnCombatLogManagerOnPrintCombatLog;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000C760 File Offset: 0x0000A960
		protected override void OnDestroyView()
		{
			CombatLogManager.OnGenerateCombatLog -= this.OnCombatLogManagerOnPrintCombatLog;
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000C79D File Offset: 0x0000A99D
		private void OnOptionChange(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.ReportCasualtiesType)
			{
				this._isGeneralFeedEnabled = (BannerlordConfig.ReportCasualtiesType < 2);
				return;
			}
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.ReportPersonalDamage)
			{
				this._isPersonalFeedEnabled = BannerlordConfig.ReportPersonalDamage;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
			if (!base.IsViewActive || affectorAgent == null || (agentState != AgentState.Killed && agentState != AgentState.Unconscious))
			{
				return;
			}
			bool flag = killingBlow.IsHeadShot();
			if (this._isPersonalFeedEnabled && affectorAgent == Agent.Main && (affectedAgent.IsHuman || affectedAgent.IsMount))
			{
				bool flag2 = affectedAgent.Team == affectorAgent.Team || affectedAgent.IsFriendOf(affectorAgent);
				SPKillFeedVM dataSource = this._dataSource;
				int inflictedDamage = killingBlow.InflictedDamage;
				bool isMount = affectedAgent.IsMount;
				bool isFriendlyFire = flag2;
				bool isHeadshot = flag;
				BasicCharacterObject character = affectedAgent.Character;
				dataSource.OnPersonalKill(inflictedDamage, isMount, isFriendlyFire, isHeadshot, (character != null) ? character.Name.ToString() : null, agentState == AgentState.Unconscious);
			}
			if (this._isGeneralFeedEnabled && affectedAgent.IsHuman)
			{
				this._dataSource.OnAgentRemoved(affectedAgent, affectorAgent, flag);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000C884 File Offset: 0x0000AA84
		private void OnCombatLogManagerOnPrintCombatLog(CombatLogData logData)
		{
			if (this._isPersonalFeedEnabled && !logData.IsVictimAgentMine && (logData.IsAttackerAgentMine || logData.IsAttackerAgentRiderAgentMine) && logData.TotalDamage > 0 && !logData.IsFatalDamage)
			{
				this._dataSource.OnPersonalDamage(logData.TotalDamage, logData.IsVictimAgentMount, logData.IsFriendlyFire, logData.VictimAgentName);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000C8E7 File Offset: 0x0000AAE7
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C90C File Offset: 0x0000AB0C
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x04000125 RID: 293
		private SPKillFeedVM _dataSource;

		// Token: 0x04000126 RID: 294
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000127 RID: 295
		private bool _isGeneralFeedEnabled = true;

		// Token: 0x04000128 RID: 296
		private bool _isPersonalFeedEnabled = true;
	}
}
