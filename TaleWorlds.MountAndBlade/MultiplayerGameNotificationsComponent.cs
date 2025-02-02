using System;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A9 RID: 681
	public class MultiplayerGameNotificationsComponent : MissionNetwork
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x0008C99B File Offset: 0x0008AB9B
		public static int NotificationCount
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0008C99F File Offset: 0x0008AB9F
		public void WarmupEnding()
		{
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.BattleWarmupEnding, 30, -1, null, null);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0008C9B0 File Offset: 0x0008ABB0
		public void GameOver(Team winnerTeam)
		{
			if (winnerTeam == null)
			{
				this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GameOverDraw, -1, -1, null, null);
				return;
			}
			Team syncToTeam = (winnerTeam.Side == BattleSideEnum.Attacker) ? base.Mission.Teams.Defender : base.Mission.Teams.Attacker;
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GameOverVictory, -1, -1, winnerTeam, null);
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GameOverDefeat, -1, -1, syncToTeam, null);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0008CA0E File Offset: 0x0008AC0E
		public void PreparationStarted()
		{
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.BattlePreparationStart, -1, -1, null, null);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0008CA1C File Offset: 0x0008AC1C
		public void FlagsXRemoved(FlagCapturePoint removedFlag)
		{
			int flagChar = removedFlag.FlagChar;
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXRemoved, flagChar, -1, null, null);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x0008CA3C File Offset: 0x0008AC3C
		public void FlagXRemaining(FlagCapturePoint remainingFlag)
		{
			int flagChar = remainingFlag.FlagChar;
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXRemaining, flagChar, -1, null, null);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x0008CA5B File Offset: 0x0008AC5B
		public void FlagsWillBeRemovedInXSeconds(int timeLeft)
		{
			this.ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagsWillBeRemoved, new int[]
			{
				timeLeft
			});
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0008CA70 File Offset: 0x0008AC70
		public void FlagXCapturedByTeamX(SynchedMissionObject flag, Team capturingTeam)
		{
			FlagCapturePoint flagCapturePoint = flag as FlagCapturePoint;
			int param = (flagCapturePoint != null) ? flagCapturePoint.FlagChar : 65;
			Team syncToTeam = (capturingTeam.Side == BattleSideEnum.Attacker) ? base.Mission.Teams.Defender : base.Mission.Teams.Attacker;
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXCapturedByYourTeam, param, -1, capturingTeam, null);
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXCapturedByOtherTeam, param, -1, syncToTeam, null);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0008CAD8 File Offset: 0x0008ACD8
		public void GoldCarriedFromPreviousRound(int carriedGoldAmount, NetworkCommunicator syncToPeer)
		{
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GoldCarriedFromPreviousRound, carriedGoldAmount, -1, null, syncToPeer);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0008CAF3 File Offset: 0x0008ACF3
		public void PlayerIsInactive(NetworkCommunicator peer)
		{
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.PlayerIsInactive, -1, -1, null, peer);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0008CB01 File Offset: 0x0008AD01
		public void FormationAutoFollowEnforced(NetworkCommunicator peer)
		{
			this.HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FormationAutoFollowEnforced, -1, -1, null, peer);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0008CB10 File Offset: 0x0008AD10
		public void PollRejected(MultiplayerPollRejectReason reason)
		{
			if (reason == MultiplayerPollRejectReason.TooManyPollRequests)
			{
				this.ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.TooManyPollRequests, Array.Empty<int>());
				return;
			}
			if (reason == MultiplayerPollRejectReason.HasOngoingPoll)
			{
				this.ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.HasOngoingPoll, Array.Empty<int>());
				return;
			}
			if (reason == MultiplayerPollRejectReason.NotEnoughPlayersToOpenPoll)
			{
				this.ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.NotEnoughPlayersToOpenPoll, new int[]
				{
					3
				});
				return;
			}
			if (reason == MultiplayerPollRejectReason.KickPollTargetNotSynced)
			{
				this.ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.KickPollTargetNotSynced, Array.Empty<int>());
				return;
			}
			Debug.FailedAssert("Notification of a PollRejectReason is missing (" + reason + ")", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameNotificationsComponent.cs", "PollRejected", 153);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0008CB92 File Offset: 0x0008AD92
		public void PlayerKicked(NetworkCommunicator kickedPeer)
		{
			this.ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.PlayerIsKicked, new int[]
			{
				kickedPeer.Index
			});
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x0008CBAB File Offset: 0x0008ADAB
		private void HandleNewNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum notification, int param1 = -1, int param2 = -1, Team syncToTeam = null, NetworkCommunicator syncToPeer = null)
		{
			if (syncToPeer != null)
			{
				this.SendNotificationToPeer(syncToPeer, notification, param1, param2);
				return;
			}
			if (syncToTeam != null)
			{
				this.SendNotificationToTeam(syncToTeam, notification, param1, param2);
				return;
			}
			this.SendNotificationToEveryone(notification, param1, param2);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0008CBD8 File Offset: 0x0008ADD8
		private void ShowNotification(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum notification, params int[] parameters)
		{
			if (!GameNetwork.IsDedicatedServer)
			{
				NotificationProperty notificationProperty = (NotificationProperty)notification.GetType().GetField(notification.ToString()).GetCustomAttributesSafe(typeof(NotificationProperty), false).Single<object>();
				if (notificationProperty != null)
				{
					int[] parameters2 = (from x in parameters
					where x != -1
					select x).ToArray<int>();
					TextObject message = this.ToNotificationString(notification, notificationProperty, parameters2);
					string soundEventPath = this.ToSoundString(notification, notificationProperty, parameters2);
					MBInformationManager.AddQuickInformation(message, 0, null, soundEventPath);
				}
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0008CC70 File Offset: 0x0008AE70
		private void SendNotificationToEveryone(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum message, int param1 = -1, int param2 = -1)
		{
			this.ShowNotification(message, new int[]
			{
				param1,
				param2
			});
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new NotificationMessage((int)message, param1, param2));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0008CCA0 File Offset: 0x0008AEA0
		private void SendNotificationToPeer(NetworkCommunicator peer, MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum message, int param1 = -1, int param2 = -1)
		{
			if (peer.IsServerPeer)
			{
				this.ShowNotification(message, new int[]
				{
					param1,
					param2
				});
				return;
			}
			GameNetwork.BeginModuleEventAsServer(peer);
			GameNetwork.WriteMessage(new NotificationMessage((int)message, param1, param2));
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0008CCDC File Offset: 0x0008AEDC
		private void SendNotificationToTeam(Team team, MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum message, int param1 = -1, int param2 = -1)
		{
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
			if (!GameNetwork.IsDedicatedServer && ((missionPeer != null) ? missionPeer.Team : null) != null && missionPeer.Team.IsEnemyOf(team))
			{
				this.ShowNotification(message, new int[]
				{
					param1,
					param2
				});
			}
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (((component != null) ? component.Team : null) != null && !component.IsMine && !component.Team.IsEnemyOf(team))
				{
					GameNetwork.BeginModuleEventAsServer(component.Peer);
					GameNetwork.WriteMessage(new NotificationMessage((int)message, param1, param2));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0008CDBC File Offset: 0x0008AFBC
		private string ToSoundString(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum value, NotificationProperty attribute, params int[] parameters)
		{
			string result = string.Empty;
			if (string.IsNullOrEmpty(attribute.SoundIdTwo))
			{
				result = attribute.SoundIdOne;
			}
			else if (value != MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.BattleYouHaveXTheRound)
			{
				if (value != MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXCapturedByYourTeam)
				{
					if (value == MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXCapturedByOtherTeam)
					{
						result = attribute.SoundIdTwo;
					}
				}
				else
				{
					result = attribute.SoundIdOne;
				}
			}
			else
			{
				Team team = (parameters[0] == 0) ? Mission.Current.AttackerTeam : Mission.Current.DefenderTeam;
				Team team2 = GameNetwork.IsMyPeerReady ? GameNetwork.MyPeer.GetComponent<MissionPeer>().Team : null;
				result = attribute.SoundIdOne;
				if (team2 != null && team2 != team)
				{
					result = attribute.SoundIdTwo;
				}
			}
			return result;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0008CE53 File Offset: 0x0008B053
		private TextObject ToNotificationString(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum value, NotificationProperty attribute, params int[] parameters)
		{
			if (parameters.Length != 0)
			{
				this.SetGameTextVariables(value, parameters);
			}
			return GameTexts.FindText(attribute.StringId, null);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0008CE70 File Offset: 0x0008B070
		private void SetGameTextVariables(MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum message, params int[] parameters)
		{
			if (parameters.Length == 0)
			{
				return;
			}
			switch (message)
			{
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.BattleWarmupEnding:
				GameTexts.SetVariable("SECONDS_LEFT", parameters[0]);
				return;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.BattlePreparationStart:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GameOverDraw:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GameOverVictory:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GameOverDefeat:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.PlayerIsInactive:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.HasOngoingPoll:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.TooManyPollRequests:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.KickPollTargetNotSynced:
				break;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.BattleYouHaveXTheRound:
			{
				Team team = (parameters[0] == 0) ? Mission.Current.AttackerTeam : Mission.Current.DefenderTeam;
				Team team2 = GameNetwork.IsMyPeerReady ? GameNetwork.MyPeer.GetComponent<MissionPeer>().Team : null;
				if (team2 != null)
				{
					GameTexts.SetVariable("IS_WINNER", (team2 == team) ? 1 : 0);
					return;
				}
				break;
			}
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXRemoved:
				GameTexts.SetVariable("PARAM1", ((char)parameters[0]).ToString());
				return;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXRemaining:
				GameTexts.SetVariable("PARAM1", ((char)parameters[0]).ToString());
				return;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagsWillBeRemoved:
				GameTexts.SetVariable("PARAM1", parameters[0]);
				return;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXCapturedByYourTeam:
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.FlagXCapturedByOtherTeam:
				GameTexts.SetVariable("PARAM1", ((char)parameters[0]).ToString());
				return;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.GoldCarriedFromPreviousRound:
				GameTexts.SetVariable("PARAM1", parameters[0].ToString());
				return;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.NotEnoughPlayersToOpenPoll:
				GameTexts.SetVariable("MIN_PARTICIPANT_COUNT", parameters[0]);
				break;
			case MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum.PlayerIsKicked:
				GameTexts.SetVariable("PLAYER_NAME", GameNetwork.FindNetworkPeer(parameters[0]).UserName);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0008CFBD File Offset: 0x0008B1BD
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<NotificationMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventServerMessage));
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0008CFD8 File Offset: 0x0008B1D8
		private void HandleServerEventServerMessage(GameNetworkMessage baseMessage)
		{
			NotificationMessage notificationMessage = (NotificationMessage)baseMessage;
			this.ShowNotification((MultiplayerGameNotificationsComponent.MultiplayerNotificationEnum)notificationMessage.Message, new int[]
			{
				notificationMessage.ParameterOne,
				notificationMessage.ParameterTwo
			});
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0008D010 File Offset: 0x0008B210
		protected override void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
			bool isServerPeer = clientConnectionInfo.NetworkPeer.IsServerPeer;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0008D01E File Offset: 0x0008B21E
		protected override void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
			bool isServer = GameNetwork.IsServer;
		}

		// Token: 0x0200056D RID: 1389
		private enum MultiplayerNotificationEnum
		{
			// Token: 0x04001D11 RID: 7441
			[NotificationProperty("str_battle_warmup_ending_in_x_seconds", "event:/ui/mission/multiplayer/lastmanstanding", "")]
			BattleWarmupEnding,
			// Token: 0x04001D12 RID: 7442
			[NotificationProperty("str_battle_preparation_start", "event:/ui/mission/multiplayer/roundstart", "")]
			BattlePreparationStart,
			// Token: 0x04001D13 RID: 7443
			[NotificationProperty("str_round_result_win_lose", "event:/ui/mission/multiplayer/victory", "event:/ui/mission/multiplayer/defeat")]
			BattleYouHaveXTheRound,
			// Token: 0x04001D14 RID: 7444
			[NotificationProperty("str_mp_mission_game_over_draw", "", "")]
			GameOverDraw,
			// Token: 0x04001D15 RID: 7445
			[NotificationProperty("str_mp_mission_game_over_victory", "", "")]
			GameOverVictory,
			// Token: 0x04001D16 RID: 7446
			[NotificationProperty("str_mp_mission_game_over_defeat", "", "")]
			GameOverDefeat,
			// Token: 0x04001D17 RID: 7447
			[NotificationProperty("str_mp_flag_removed", "event:/ui/mission/multiplayer/pointsremoved", "")]
			FlagXRemoved,
			// Token: 0x04001D18 RID: 7448
			[NotificationProperty("str_sergeant_a_one_flag_remaining", "event:/ui/mission/multiplayer/pointsremoved", "")]
			FlagXRemaining,
			// Token: 0x04001D19 RID: 7449
			[NotificationProperty("str_sergeant_a_flags_will_be_removed", "event:/ui/mission/multiplayer/pointwarning", "")]
			FlagsWillBeRemoved,
			// Token: 0x04001D1A RID: 7450
			[NotificationProperty("str_sergeant_a_flag_captured_by_your_team", "event:/ui/mission/multiplayer/pointcapture", "event:/ui/mission/multiplayer/pointlost")]
			FlagXCapturedByYourTeam,
			// Token: 0x04001D1B RID: 7451
			[NotificationProperty("str_sergeant_a_flag_captured_by_other_team", "event:/ui/mission/multiplayer/pointcapture", "event:/ui/mission/multiplayer/pointlost")]
			FlagXCapturedByOtherTeam,
			// Token: 0x04001D1C RID: 7452
			[NotificationProperty("str_gold_carried_from_previous_round", "", "")]
			GoldCarriedFromPreviousRound,
			// Token: 0x04001D1D RID: 7453
			[NotificationProperty("str_player_is_inactive", "", "")]
			PlayerIsInactive,
			// Token: 0x04001D1E RID: 7454
			[NotificationProperty("str_has_ongoing_poll", "", "")]
			HasOngoingPoll,
			// Token: 0x04001D1F RID: 7455
			[NotificationProperty("str_too_many_poll_requests", "", "")]
			TooManyPollRequests,
			// Token: 0x04001D20 RID: 7456
			[NotificationProperty("str_kick_poll_target_not_synced", "", "")]
			KickPollTargetNotSynced,
			// Token: 0x04001D21 RID: 7457
			[NotificationProperty("str_not_enough_players_to_open_poll", "", "")]
			NotEnoughPlayersToOpenPoll,
			// Token: 0x04001D22 RID: 7458
			[NotificationProperty("str_player_is_kicked", "", "")]
			PlayerIsKicked,
			// Token: 0x04001D23 RID: 7459
			[NotificationProperty("str_formation_autofollow_enforced", "", "")]
			FormationAutoFollowEnforced,
			// Token: 0x04001D24 RID: 7460
			Count
		}
	}
}
