using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x02000035 RID: 53
	[OverrideView(typeof(MissionSpectatorControlView))]
	public class MissionGauntletSpectatorControl : MissionView
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000F460 File Offset: 0x0000D660
		public override void EarlyStart()
		{
			base.EarlyStart();
			this.ViewOrderPriority = 14;
			this._dataSource = new MissionSpectatorControlVM(base.Mission);
			this._dataSource.SetPrevCharacterInputKey(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(10));
			this._dataSource.SetNextCharacterInputKey(HotKeyManager.GetCategory("CombatHotKeyCategory").GetGameKey(9));
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("SpectatorControl", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			base.MissionScreen.OnSpectateAgentFocusIn += this._dataSource.OnSpectatedAgentFocusIn;
			base.MissionScreen.OnSpectateAgentFocusOut += this._dataSource.OnSpectatedAgentFocusOut;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000F53C File Offset: 0x0000D73C
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._dataSource != null)
			{
				Mission.SpectatorData spectatingData = base.MissionScreen.GetSpectatingData(base.MissionScreen.CombatCamera.Frame.origin);
				bool flag = spectatingData.CameraType == SpectatorCameraTypes.LockToMainPlayer || spectatingData.CameraType == SpectatorCameraTypes.LockToPosition;
				MissionSpectatorControlVM dataSource = this._dataSource;
				bool isEnabled;
				if ((!flag && base.Mission.Mode != MissionMode.Deployment) || (base.MissionScreen.IsCheatGhostMode && !base.Mission.IsOrderMenuOpen))
				{
					MissionMultiplayerGameModeBaseClient missionBehavior = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
					if ((missionBehavior == null || missionBehavior.IsRoundInProgress) && !base.MissionScreen.LockCameraMovement)
					{
						isEnabled = (base.MissionScreen.CustomCamera == null);
						goto IL_B6;
					}
				}
				isEnabled = false;
				IL_B6:
				dataSource.IsEnabled = isEnabled;
				bool mainAgentStatus = base.Mission.PlayerTeam != null && base.Mission.MainAgent == null;
				this._dataSource.SetMainAgentStatus(mainAgentStatus);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000F630 File Offset: 0x0000D830
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			base.MissionScreen.OnSpectateAgentFocusIn -= this._dataSource.OnSpectatedAgentFocusIn;
			base.MissionScreen.OnSpectateAgentFocusOut -= this._dataSource.OnSpectatedAgentFocusOut;
			this._dataSource.OnFinalize();
		}

		// Token: 0x04000167 RID: 359
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000168 RID: 360
		private MissionSpectatorControlVM _dataSource;
	}
}
