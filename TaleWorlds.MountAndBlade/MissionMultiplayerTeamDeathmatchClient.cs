using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A2 RID: 674
	public class MissionMultiplayerTeamDeathmatchClient : MissionMultiplayerGameModeBaseClient
	{
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x0600243D RID: 9277 RVA: 0x00086A40 File Offset: 0x00084C40
		// (remove) Token: 0x0600243E RID: 9278 RVA: 0x00086A78 File Offset: 0x00084C78
		public event Action<GoldGain> OnGoldGainEvent;

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x00086AAD File Offset: 0x00084CAD
		public override bool IsGameModeUsingGold
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x00086AB0 File Offset: 0x00084CB0
		public override bool IsGameModeTactical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x00086AB3 File Offset: 0x00084CB3
		public override bool IsGameModeUsingRoundCountdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x00086AB6 File Offset: 0x00084CB6
		public override MultiplayerGameType GameType
		{
			get
			{
				return MultiplayerGameType.TeamDeathmatch;
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00086AB9 File Offset: 0x00084CB9
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.MissionNetworkComponent.OnMyClientSynchronized += this.OnMyClientSynchronized;
			base.ScoreboardComponent.OnRoundPropertiesChanged += this.OnTeamScoresChanged;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00086AEF File Offset: 0x00084CEF
		public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
		{
			if (representative != null && base.MissionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending)
			{
				representative.UpdateGold(goldAmount);
				base.ScoreboardComponent.PlayerPropertiesChanged(representative.MissionPeer);
			}
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00086B1A File Offset: 0x00084D1A
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00086B29 File Offset: 0x00084D29
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<SyncGoldsForSkirmish>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventUpdateGold));
				registerer.RegisterBaseHandler<GoldGain>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventTDMGoldGain));
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00086B56 File Offset: 0x00084D56
		private void OnMyClientSynchronized()
		{
			this._myRepresentative = GameNetwork.MyPeer.GetComponent<TeamDeathmatchMissionRepresentative>();
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00086B68 File Offset: 0x00084D68
		private void HandleServerEventUpdateGold(GameNetworkMessage baseMessage)
		{
			SyncGoldsForSkirmish syncGoldsForSkirmish = (SyncGoldsForSkirmish)baseMessage;
			MissionRepresentativeBase component = syncGoldsForSkirmish.VirtualPlayer.GetComponent<MissionRepresentativeBase>();
			this.OnGoldAmountChangedForRepresentative(component, syncGoldsForSkirmish.GoldAmount);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00086B98 File Offset: 0x00084D98
		private void HandleServerEventTDMGoldGain(GameNetworkMessage baseMessage)
		{
			GoldGain obj = (GoldGain)baseMessage;
			Action<GoldGain> onGoldGainEvent = this.OnGoldGainEvent;
			if (onGoldGainEvent == null)
			{
				return;
			}
			onGoldGainEvent(obj);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00086BBD File Offset: 0x00084DBD
		public override int GetGoldAmount()
		{
			return this._myRepresentative.Gold;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00086BCA File Offset: 0x00084DCA
		public override void OnRemoveBehavior()
		{
			base.MissionNetworkComponent.OnMyClientSynchronized -= this.OnMyClientSynchronized;
			base.ScoreboardComponent.OnRoundPropertiesChanged -= this.OnTeamScoresChanged;
			base.OnRemoveBehavior();
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00086C00 File Offset: 0x00084E00
		private void OnTeamScoresChanged()
		{
			if (!GameNetwork.IsDedicatedServer && !this._battleEndingNotificationGiven && this._myRepresentative.MissionPeer.Team != null && this._myRepresentative.MissionPeer.Team.Side != BattleSideEnum.None)
			{
				int intValue = MultiplayerOptions.OptionType.MinScoreToWinMatch.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				float num = (float)(intValue - base.ScoreboardComponent.GetRoundScore(this._myRepresentative.MissionPeer.Team.Side)) / (float)intValue;
				float num2 = (float)(intValue - base.ScoreboardComponent.GetRoundScore(this._myRepresentative.MissionPeer.Team.Side.GetOppositeSide())) / (float)intValue;
				MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
				Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
				if (num <= 0.1f && num2 > 0.1f)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/battle_winning"), position);
					this._battleEndingNotificationGiven = true;
				}
				if (num2 <= 0.1f && num > 0.1f)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/battle_losing"), position);
					this._battleEndingNotificationGiven = true;
				}
			}
		}

		// Token: 0x04000D47 RID: 3399
		private const string BattleWinningSoundEventString = "event:/alerts/report/battle_winning";

		// Token: 0x04000D48 RID: 3400
		private const string BattleLosingSoundEventString = "event:/alerts/report/battle_losing";

		// Token: 0x04000D49 RID: 3401
		private const float BattleWinLoseAlertThreshold = 0.1f;

		// Token: 0x04000D4B RID: 3403
		private TeamDeathmatchMissionRepresentative _myRepresentative;

		// Token: 0x04000D4C RID: 3404
		private bool _battleEndingNotificationGiven;
	}
}
