using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E7 RID: 743
	public class ChatBox : GameHandler
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x0009C413 File Offset: 0x0009A613
		// (set) Token: 0x06002887 RID: 10375 RVA: 0x0009C41B File Offset: 0x0009A61B
		public bool IsContentRestricted { get; private set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x0009C424 File Offset: 0x0009A624
		public bool NetworkReady
		{
			get
			{
				return GameNetwork.IsClient || GameNetwork.IsServer || (NetworkMain.GameClient != null && NetworkMain.GameClient.Connected);
			}
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0009C449 File Offset: 0x0009A649
		protected override void OnGameStart()
		{
			ChatBox._chatBox = this;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0009C451 File Offset: 0x0009A651
		public override void OnBeforeSave()
		{
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0009C453 File Offset: 0x0009A653
		public override void OnAfterSave()
		{
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0009C455 File Offset: 0x0009A655
		protected override void OnGameEnd()
		{
			ChatBox._chatBox = null;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0009C45D File Offset: 0x0009A65D
		public void SendMessageToAll(string message)
		{
			this.SendMessageToAll(message, null);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x0009C467 File Offset: 0x0009A667
		public void SendMessageToAll(string message, List<VirtualPlayer> receiverList)
		{
			if (GameNetwork.IsClient && !this.IsContentRestricted)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new NetworkMessages.FromClient.PlayerMessageAll(message));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			if (GameNetwork.IsServer)
			{
				this.ServerPrepareAndSendMessage(GameNetwork.MyPeer, false, message, receiverList);
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0009C4A4 File Offset: 0x0009A6A4
		public void SendMessageToTeam(string message)
		{
			this.SendMessageToTeam(message, null);
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0009C4AE File Offset: 0x0009A6AE
		public void SendMessageToTeam(string message, List<VirtualPlayer> receiverList)
		{
			if (GameNetwork.IsClient && !this.IsContentRestricted)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new NetworkMessages.FromClient.PlayerMessageTeam(message));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			if (GameNetwork.IsServer)
			{
				this.ServerPrepareAndSendMessage(GameNetwork.MyPeer, true, message, receiverList);
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0009C4EB File Offset: 0x0009A6EB
		public void SendMessageToWhisperTarget(string message, string platformName, string whisperTarget)
		{
			if (NetworkMain.GameClient != null && NetworkMain.GameClient.Connected)
			{
				NetworkMain.GameClient.SendWhisper(whisperTarget, message);
				if (this.WhisperMessageSent != null)
				{
					this.WhisperMessageSent(message, whisperTarget);
				}
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0009C521 File Offset: 0x0009A721
		private void OnServerMessage(string message)
		{
			if (this.ServerMessage != null)
			{
				this.ServerMessage(message);
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0009C537 File Offset: 0x0009A737
		protected override void OnGameNetworkBegin()
		{
			ChatBox._queuedTeamMessages = new List<ChatBox.QueuedMessageInfo>();
			ChatBox._queuedEveryoneMessages = new List<ChatBox.QueuedMessageInfo>();
			this._isNetworkInitialized = true;
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0009C55C File Offset: 0x0009A75C
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromServer.PlayerMessageTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<NetworkMessages.FromServer.PlayerMessageTeam>(this.HandleServerEventPlayerMessageTeam));
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromServer.PlayerMessageAll>(new GameNetworkMessage.ServerMessageHandlerDelegate<NetworkMessages.FromServer.PlayerMessageAll>(this.HandleServerEventPlayerMessageAll));
				networkMessageHandlerRegisterer.Register<ServerMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<ServerMessage>(this.HandleServerEventServerMessage));
				networkMessageHandlerRegisterer.Register<ServerAdminMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<ServerAdminMessage>(this.HandleServerEventServerAdminMessage));
				return;
			}
			if (GameNetwork.IsServer)
			{
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromClient.PlayerMessageAll>(new GameNetworkMessage.ClientMessageHandlerDelegate<NetworkMessages.FromClient.PlayerMessageAll>(this.HandleClientEventPlayerMessageAll));
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromClient.PlayerMessageTeam>(new GameNetworkMessage.ClientMessageHandlerDelegate<NetworkMessages.FromClient.PlayerMessageTeam>(this.HandleClientEventPlayerMessageTeam));
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0009C5EB File Offset: 0x0009A7EB
		protected override void OnGameNetworkEnd()
		{
			base.OnGameNetworkEnd();
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0009C5FC File Offset: 0x0009A7FC
		private void HandleServerEventPlayerMessageAll(NetworkMessages.FromServer.PlayerMessageAll message)
		{
			if (!this.IsContentRestricted)
			{
				this.ShouldShowPlayersMessage(message.Player.VirtualPlayer.Id, delegate(bool result)
				{
					if (result)
					{
						this.OnPlayerMessageReceived(message.Player, message.Message, false);
					}
				});
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x0009C64C File Offset: 0x0009A84C
		private void HandleServerEventPlayerMessageTeam(NetworkMessages.FromServer.PlayerMessageTeam message)
		{
			if (!this.IsContentRestricted)
			{
				this.ShouldShowPlayersMessage(message.Player.VirtualPlayer.Id, delegate(bool result)
				{
					if (result)
					{
						this.OnPlayerMessageReceived(message.Player, message.Message, true);
					}
				});
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0009C69C File Offset: 0x0009A89C
		private void HandleServerEventServerMessage(ServerMessage message)
		{
			this.OnServerMessage(message.IsMessageTextId ? GameTexts.FindText(message.Message, null).ToString() : message.Message);
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0009C6C8 File Offset: 0x0009A8C8
		private void HandleServerEventServerAdminMessage(ServerAdminMessage message)
		{
			if (message.IsAdminBroadcast)
			{
				TextObject textObject = new TextObject("{=!}{ADMIN_TEXT}", null);
				textObject.SetTextVariable("ADMIN_TEXT", message.Message);
				MBInformationManager.AddQuickInformation(textObject, 5000, null, "");
				SoundEvent.PlaySound2D("event:/ui/notification/alert");
			}
			ServerAdminMessageDelegate serverAdminMessage = this.ServerAdminMessage;
			if (serverAdminMessage == null)
			{
				return;
			}
			serverAdminMessage(message.Message);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0009C72B File Offset: 0x0009A92B
		private bool HandleClientEventPlayerMessageAll(NetworkCommunicator networkPeer, NetworkMessages.FromClient.PlayerMessageAll message)
		{
			return this.ServerPrepareAndSendMessage(networkPeer, false, message.Message, message.ReceiverList);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0009C741 File Offset: 0x0009A941
		private bool HandleClientEventPlayerMessageTeam(NetworkCommunicator networkPeer, NetworkMessages.FromClient.PlayerMessageTeam message)
		{
			return this.ServerPrepareAndSendMessage(networkPeer, true, message.Message, message.ReceiverList);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0009C757 File Offset: 0x0009A957
		public static void ServerSendServerMessageToEveryone(string message)
		{
			ChatBox._chatBox.OnServerMessage(message);
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new ServerMessage(message, false, false));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0009C780 File Offset: 0x0009A980
		private bool ServerPrepareAndSendMessage(NetworkCommunicator fromPeer, bool toTeamOnly, string message, List<VirtualPlayer> receiverList)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				Action<NetworkCommunicator, string> onMessageReceivedAtDedicatedServer = this.OnMessageReceivedAtDedicatedServer;
				if (onMessageReceivedAtDedicatedServer != null)
				{
					onMessageReceivedAtDedicatedServer(fromPeer, message);
				}
			}
			if (fromPeer.IsMuted || CustomGameMutedPlayerManager.IsUserMuted(fromPeer.VirtualPlayer.Id))
			{
				GameNetwork.BeginModuleEventAsServer(fromPeer);
				GameNetwork.WriteMessage(new ServerMessage("str_multiplayer_muted_message", true, false));
				GameNetwork.EndModuleEventAsServer();
				return true;
			}
			if (this._profanityChecker != null)
			{
				message = this._profanityChecker.CensorText(message);
			}
			if (!GameNetwork.IsDedicatedServer && fromPeer != GameNetwork.MyPeer && !this._mutedPlayers.Contains(fromPeer.VirtualPlayer.Id) && !PermaMuteList.IsPlayerMuted(fromPeer.VirtualPlayer.Id))
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component == null)
				{
					return false;
				}
				bool flag;
				if (toTeamOnly)
				{
					if (component == null)
					{
						return false;
					}
					MissionPeer component2 = fromPeer.GetComponent<MissionPeer>();
					if (component2 == null)
					{
						return false;
					}
					flag = (component.Team == component2.Team);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					this.OnPlayerMessageReceived(fromPeer, message, toTeamOnly);
				}
			}
			if (toTeamOnly)
			{
				ChatBox.ServerSendMessageToTeam(fromPeer, message, receiverList);
			}
			else
			{
				ChatBox.ServerSendMessageToEveryone(fromPeer, message, receiverList);
			}
			return true;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0009C88C File Offset: 0x0009AA8C
		private static void ServerSendMessageToTeam(NetworkCommunicator networkPeer, string message, List<VirtualPlayer> receiverList)
		{
			if (!networkPeer.IsSynchronized)
			{
				ChatBox._queuedTeamMessages.Add(new ChatBox.QueuedMessageInfo(networkPeer, message, receiverList));
				return;
			}
			MissionPeer missionPeer = networkPeer.GetComponent<MissionPeer>();
			MissionPeer missionPeer2 = missionPeer;
			if (((missionPeer2 != null) ? missionPeer2.Team : null) != null)
			{
				using (IEnumerator<NetworkCommunicator> enumerator = (from x in GameNetwork.NetworkPeers
				where !x.IsServerPeer && x.IsSynchronized && x.GetComponent<MissionPeer>().Team == missionPeer.Team
				select x).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NetworkCommunicator networkCommunicator = enumerator.Current;
						if (receiverList == null || receiverList.Contains(networkCommunicator.VirtualPlayer))
						{
							GameNetwork.BeginModuleEventAsServer(networkCommunicator);
							GameNetwork.WriteMessage(new NetworkMessages.FromServer.PlayerMessageTeam(networkPeer, message));
							GameNetwork.EndModuleEventAsServer();
						}
					}
					return;
				}
			}
			ChatBox.ServerSendMessageToEveryone(networkPeer, message, receiverList);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0009C954 File Offset: 0x0009AB54
		private static void ServerSendMessageToEveryone(NetworkCommunicator networkPeer, string message, List<VirtualPlayer> receiverList)
		{
			if (!networkPeer.IsSynchronized)
			{
				ChatBox._queuedEveryoneMessages.Add(new ChatBox.QueuedMessageInfo(networkPeer, message, receiverList));
				return;
			}
			foreach (NetworkCommunicator networkCommunicator in from x in GameNetwork.NetworkPeers
			where !x.IsServerPeer && x.IsSynchronized
			select x)
			{
				if (receiverList == null || receiverList.Contains(networkCommunicator.VirtualPlayer))
				{
					GameNetwork.BeginModuleEventAsServer(networkCommunicator);
					GameNetwork.WriteMessage(new NetworkMessages.FromServer.PlayerMessageAll(networkPeer, message));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0009CA00 File Offset: 0x0009AC00
		public void ResetMuteList()
		{
			this._mutedPlayers.Clear();
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0009CA0D File Offset: 0x0009AC0D
		public static void AddWhisperMessage(string fromUserName, string messageBody)
		{
			ChatBox._chatBox.OnWhisperMessageReceived(fromUserName, messageBody);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0009CA1B File Offset: 0x0009AC1B
		public static void AddErrorWhisperMessage(string toUserName)
		{
			ChatBox._chatBox.OnErrorWhisperMessageReceived(toUserName);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0009CA28 File Offset: 0x0009AC28
		private void OnWhisperMessageReceived(string fromUserName, string messageBody)
		{
			if (this.WhisperMessageReceived != null)
			{
				this.WhisperMessageReceived(fromUserName, messageBody);
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0009CA3F File Offset: 0x0009AC3F
		private void OnErrorWhisperMessageReceived(string toUserName)
		{
			if (this.ErrorWhisperMessageReceived != null)
			{
				this.ErrorWhisperMessageReceived(toUserName);
			}
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0009CA55 File Offset: 0x0009AC55
		private void OnPlayerMessageReceived(NetworkCommunicator networkPeer, string message, bool toTeamOnly)
		{
			if (this.PlayerMessageReceived != null)
			{
				this.PlayerMessageReceived(networkPeer, message, toTeamOnly);
			}
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0009CA6D File Offset: 0x0009AC6D
		public void SetPlayerMuted(PlayerId playerID, bool isMuted)
		{
			if (isMuted)
			{
				this.OnPlayerMuted(playerID);
				return;
			}
			this.OnPlayerUnmuted(playerID);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x0009CA81 File Offset: 0x0009AC81
		public void SetPlayerMutedFromPlatform(PlayerId playerID, bool isMuted)
		{
			if (isMuted && !this._platformMutedPlayers.Contains(playerID))
			{
				this._platformMutedPlayers.Add(playerID);
				return;
			}
			if (!isMuted && this._platformMutedPlayers.Contains(playerID))
			{
				this._platformMutedPlayers.Remove(playerID);
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x0009CABF File Offset: 0x0009ACBF
		private void OnPlayerMuted(PlayerId mutedPlayer)
		{
			if (!this._mutedPlayers.Contains(mutedPlayer))
			{
				this._mutedPlayers.Add(mutedPlayer);
				PlayerMutedDelegate onPlayerMuteChanged = this.OnPlayerMuteChanged;
				if (onPlayerMuteChanged == null)
				{
					return;
				}
				onPlayerMuteChanged(mutedPlayer, true);
			}
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0009CAED File Offset: 0x0009ACED
		private void OnPlayerUnmuted(PlayerId unmutedPlayer)
		{
			if (this._mutedPlayers.Contains(unmutedPlayer))
			{
				this._mutedPlayers.Remove(unmutedPlayer);
				PlayerMutedDelegate onPlayerMuteChanged = this.OnPlayerMuteChanged;
				if (onPlayerMuteChanged == null)
				{
					return;
				}
				onPlayerMuteChanged(unmutedPlayer, false);
			}
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x0009CB1C File Offset: 0x0009AD1C
		public bool IsPlayerMuted(PlayerId player)
		{
			return this.IsPlayerMutedFromGame(player) || this.IsPlayerMutedFromPlatform(player);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0009CB30 File Offset: 0x0009AD30
		public bool IsPlayerMutedFromPlatform(PlayerId player)
		{
			return this._platformMutedPlayers.Contains(player);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0009CB40 File Offset: 0x0009AD40
		public bool IsPlayerMutedFromGame(PlayerId player)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				return this._mutedPlayers.Contains(player);
			}
			PlatformServices.Instance.CheckPrivilege(Privilege.Chat, false, delegate(bool result)
			{
				this.IsContentRestricted = !result;
			});
			return this._mutedPlayers.Contains(player) || PermaMuteList.IsPlayerMuted(player);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x0009CB90 File Offset: 0x0009AD90
		private void ShouldShowPlayersMessage(PlayerId player, Action<bool> result)
		{
			if (this.IsPlayerMuted(player) || !NetworkMain.GameClient.SupportedFeatures.SupportsFeatures(Features.TextChat))
			{
				result(false);
				return;
			}
			PlayerIdProvidedTypes providedType = player.ProvidedType;
			LobbyClient gameClient = NetworkMain.GameClient;
			PlayerIdProvidedTypes? playerIdProvidedTypes = (gameClient != null) ? new PlayerIdProvidedTypes?(gameClient.PlayerID.ProvidedType) : null;
			if (!(providedType == playerIdProvidedTypes.GetValueOrDefault() & playerIdProvidedTypes != null))
			{
				result(true);
				return;
			}
			PlatformServices.Instance.CheckPermissionWithUser(Permission.CommunicateUsingText, player, delegate(bool res)
			{
				result(res);
			});
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x0009CC39 File Offset: 0x0009AE39
		public void SetChatFilterLists(string[] profanityList, string[] allowList)
		{
			this._profanityChecker = new ProfanityChecker(profanityList, allowList);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0009CC48 File Offset: 0x0009AE48
		public void InitializeForMultiplayer()
		{
			PlatformServices.Instance.CheckPrivilege(Privilege.Chat, true, delegate(bool result)
			{
				this.IsContentRestricted = !result;
			});
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0009CC62 File Offset: 0x0009AE62
		public void InitializeForSinglePlayer()
		{
			this.IsContentRestricted = false;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x0009CC6B File Offset: 0x0009AE6B
		public void OnLogin()
		{
			PlatformServices.Instance.CheckPrivilege(Privilege.Chat, false, delegate(bool chatPrivilegeResult)
			{
				this.IsContentRestricted = !chatPrivilegeResult;
			});
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060028B2 RID: 10418 RVA: 0x0009CC88 File Offset: 0x0009AE88
		// (remove) Token: 0x060028B3 RID: 10419 RVA: 0x0009CCC0 File Offset: 0x0009AEC0
		public event PlayerMessageReceivedDelegate PlayerMessageReceived;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060028B4 RID: 10420 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
		// (remove) Token: 0x060028B5 RID: 10421 RVA: 0x0009CD30 File Offset: 0x0009AF30
		public event WhisperMessageSentDelegate WhisperMessageSent;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060028B6 RID: 10422 RVA: 0x0009CD68 File Offset: 0x0009AF68
		// (remove) Token: 0x060028B7 RID: 10423 RVA: 0x0009CDA0 File Offset: 0x0009AFA0
		public event WhisperMessageReceivedDelegate WhisperMessageReceived;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060028B8 RID: 10424 RVA: 0x0009CDD8 File Offset: 0x0009AFD8
		// (remove) Token: 0x060028B9 RID: 10425 RVA: 0x0009CE10 File Offset: 0x0009B010
		public event ErrorWhisperMessageReceivedDelegate ErrorWhisperMessageReceived;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x060028BA RID: 10426 RVA: 0x0009CE48 File Offset: 0x0009B048
		// (remove) Token: 0x060028BB RID: 10427 RVA: 0x0009CE80 File Offset: 0x0009B080
		public event ServerMessageDelegate ServerMessage;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060028BC RID: 10428 RVA: 0x0009CEB8 File Offset: 0x0009B0B8
		// (remove) Token: 0x060028BD RID: 10429 RVA: 0x0009CEF0 File Offset: 0x0009B0F0
		public event ServerAdminMessageDelegate ServerAdminMessage;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060028BE RID: 10430 RVA: 0x0009CF28 File Offset: 0x0009B128
		// (remove) Token: 0x060028BF RID: 10431 RVA: 0x0009CF60 File Offset: 0x0009B160
		public event PlayerMutedDelegate OnPlayerMuteChanged;

		// Token: 0x060028C0 RID: 10432 RVA: 0x0009CF98 File Offset: 0x0009B198
		protected override void OnTick(float dt)
		{
			if (GameNetwork.IsServer && this._isNetworkInitialized)
			{
				for (int i = ChatBox._queuedTeamMessages.Count - 1; i >= 0; i--)
				{
					ChatBox.QueuedMessageInfo queuedMessageInfo = ChatBox._queuedTeamMessages[i];
					if (queuedMessageInfo.SourcePeer.IsSynchronized)
					{
						ChatBox.ServerSendMessageToTeam(queuedMessageInfo.SourcePeer, queuedMessageInfo.Message, queuedMessageInfo.ReceiverList);
						ChatBox._queuedTeamMessages.RemoveAt(i);
					}
					else if (queuedMessageInfo.IsExpired)
					{
						ChatBox._queuedTeamMessages.RemoveAt(i);
					}
				}
				for (int j = ChatBox._queuedEveryoneMessages.Count - 1; j >= 0; j--)
				{
					ChatBox.QueuedMessageInfo queuedMessageInfo2 = ChatBox._queuedEveryoneMessages[j];
					if (queuedMessageInfo2.SourcePeer.IsSynchronized)
					{
						ChatBox.ServerSendMessageToEveryone(queuedMessageInfo2.SourcePeer, queuedMessageInfo2.Message, queuedMessageInfo2.ReceiverList);
						ChatBox._queuedEveryoneMessages.RemoveAt(j);
					}
					else if (queuedMessageInfo2.IsExpired)
					{
						ChatBox._queuedEveryoneMessages.RemoveAt(j);
					}
				}
			}
		}

		// Token: 0x04000F5D RID: 3933
		private static ChatBox _chatBox;

		// Token: 0x04000F5F RID: 3935
		private bool _isNetworkInitialized;

		// Token: 0x04000F60 RID: 3936
		public const string AdminMessageSoundEvent = "event:/ui/notification/alert";

		// Token: 0x04000F61 RID: 3937
		private List<PlayerId> _mutedPlayers = new List<PlayerId>();

		// Token: 0x04000F62 RID: 3938
		private List<PlayerId> _platformMutedPlayers = new List<PlayerId>();

		// Token: 0x04000F63 RID: 3939
		private ProfanityChecker _profanityChecker;

		// Token: 0x04000F64 RID: 3940
		private static List<ChatBox.QueuedMessageInfo> _queuedTeamMessages;

		// Token: 0x04000F65 RID: 3941
		private static List<ChatBox.QueuedMessageInfo> _queuedEveryoneMessages;

		// Token: 0x04000F66 RID: 3942
		public Action<NetworkCommunicator, string> OnMessageReceivedAtDedicatedServer;

		// Token: 0x020005A2 RID: 1442
		private class QueuedMessageInfo
		{
			// Token: 0x170009B6 RID: 2486
			// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000E7130 File Offset: 0x000E5330
			public bool IsExpired
			{
				get
				{
					return (DateTime.Now - this._creationTime).TotalSeconds >= 3.0;
				}
			}

			// Token: 0x06003A79 RID: 14969 RVA: 0x000E7163 File Offset: 0x000E5363
			public QueuedMessageInfo(NetworkCommunicator sourcePeer, string message, List<VirtualPlayer> receiverList)
			{
				this.SourcePeer = sourcePeer;
				this.Message = message;
				this._creationTime = DateTime.Now;
				this.ReceiverList = receiverList;
			}

			// Token: 0x04001DF0 RID: 7664
			public readonly NetworkCommunicator SourcePeer;

			// Token: 0x04001DF1 RID: 7665
			public readonly string Message;

			// Token: 0x04001DF2 RID: 7666
			public readonly List<VirtualPlayer> ReceiverList;

			// Token: 0x04001DF3 RID: 7667
			private const float _timeOutDuration = 3f;

			// Token: 0x04001DF4 RID: 7668
			private DateTime _creationTime;
		}
	}
}
