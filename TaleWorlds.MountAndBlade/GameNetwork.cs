using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E2 RID: 738
	public static class GameNetwork
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x0009A43D File Offset: 0x0009863D
		public static bool IsServer
		{
			get
			{
				return MBCommon.CurrentGameType == MBCommon.GameType.MultiServer || MBCommon.CurrentGameType == MBCommon.GameType.MultiClientServer;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x0009A451 File Offset: 0x00098651
		public static bool IsServerOrRecorder
		{
			get
			{
				return GameNetwork.IsServer || MBCommon.CurrentGameType == MBCommon.GameType.SingleRecord;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x0009A464 File Offset: 0x00098664
		public static bool IsClient
		{
			get
			{
				return MBCommon.CurrentGameType == MBCommon.GameType.MultiClient;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x0009A46E File Offset: 0x0009866E
		public static bool IsReplay
		{
			get
			{
				return MBCommon.CurrentGameType == MBCommon.GameType.SingleReplay;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x0009A478 File Offset: 0x00098678
		public static bool IsClientOrReplay
		{
			get
			{
				return GameNetwork.IsClient || GameNetwork.IsReplay;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x0009A488 File Offset: 0x00098688
		public static bool IsDedicatedServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x0009A48B File Offset: 0x0009868B
		public static bool MultiplayerDisabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x0009A48E File Offset: 0x0009868E
		public static bool IsMultiplayer
		{
			get
			{
				return GameNetwork.IsServer || GameNetwork.IsClient;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x0009A49E File Offset: 0x0009869E
		public static bool IsMultiplayerOrReplay
		{
			get
			{
				return GameNetwork.IsMultiplayer || GameNetwork.IsReplay;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x0009A4AE File Offset: 0x000986AE
		public static bool IsSessionActive
		{
			get
			{
				return GameNetwork.IsServerOrRecorder || GameNetwork.IsClientOrReplay;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x0009A4BE File Offset: 0x000986BE
		public static IEnumerable<NetworkCommunicator> NetworkPeersIncludingDisconnectedPeers
		{
			get
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					yield return networkCommunicator;
				}
				List<NetworkCommunicator>.Enumerator enumerator = default(List<NetworkCommunicator>.Enumerator);
				int num;
				for (int i = 0; i < GameNetwork.DisconnectedNetworkPeers.Count; i = num + 1)
				{
					yield return GameNetwork.DisconnectedNetworkPeers[i];
					num = i;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0009A4C7 File Offset: 0x000986C7
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x0009A4CE File Offset: 0x000986CE
		public static VirtualPlayer[] VirtualPlayers { get; private set; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x0009A4D6 File Offset: 0x000986D6
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x0009A4DD File Offset: 0x000986DD
		public static List<NetworkCommunicator> NetworkPeers { get; private set; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x0009A4E5 File Offset: 0x000986E5
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x0009A4EC File Offset: 0x000986EC
		public static List<NetworkCommunicator> DisconnectedNetworkPeers { get; private set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x0009A4F4 File Offset: 0x000986F4
		public static int NetworkPeerCount
		{
			get
			{
				return GameNetwork.NetworkPeers.Count;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x0009A500 File Offset: 0x00098700
		public static bool NetworkPeersValid
		{
			get
			{
				return GameNetwork.NetworkPeers != null;
			}
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0009A50A File Offset: 0x0009870A
		private static void AddNetworkPeer(NetworkCommunicator networkPeer)
		{
			GameNetwork.NetworkPeers.Add(networkPeer);
			Debug.Print("AddNetworkPeer: " + networkPeer.UserName, 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0009A538 File Offset: 0x00098738
		private static void RemoveNetworkPeer(NetworkCommunicator networkPeer)
		{
			Debug.Print("RemoveNetworkPeer: " + networkPeer.UserName, 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.NetworkPeers.Remove(networkPeer);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0009A567 File Offset: 0x00098767
		private static void AddToDisconnectedPeers(NetworkCommunicator networkPeer)
		{
			Debug.Print("AddToDisconnectedPeers: " + networkPeer.UserName, 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.DisconnectedNetworkPeers.Add(networkPeer);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0009A598 File Offset: 0x00098798
		public static void ClearAllPeers()
		{
			if (GameNetwork.VirtualPlayers != null)
			{
				for (int i = 0; i < GameNetwork.VirtualPlayers.Length; i++)
				{
					GameNetwork.VirtualPlayers[i] = null;
				}
				GameNetwork.NetworkPeers.Clear();
				GameNetwork.DisconnectedNetworkPeers.Clear();
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0009A5DC File Offset: 0x000987DC
		public static NetworkCommunicator FindNetworkPeer(int index)
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.Index == index)
				{
					return networkCommunicator;
				}
			}
			return null;
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0009A638 File Offset: 0x00098838
		public static void Initialize(IGameNetworkHandler handler)
		{
			GameNetwork._handler = handler;
			GameNetwork.VirtualPlayers = new VirtualPlayer[1023];
			GameNetwork.NetworkPeers = new List<NetworkCommunicator>();
			GameNetwork.DisconnectedNetworkPeers = new List<NetworkCommunicator>();
			MBNetwork.Initialize(new NetworkCommunication());
			GameNetwork.NetworkComponents = new List<UdpNetworkComponent>();
			GameNetwork.NetworkHandlers = new List<IUdpNetworkHandler>();
			GameNetwork._handler.OnInitialize();
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0009A698 File Offset: 0x00098898
		internal static void Tick(float dt)
		{
			int i = 0;
			try
			{
				for (i = 0; i < GameNetwork.NetworkHandlers.Count; i++)
				{
					GameNetwork.NetworkHandlers[i].OnUdpNetworkHandlerTick(dt);
				}
			}
			catch (Exception ex)
			{
				if (GameNetwork.NetworkHandlers.Count > 0 && i < GameNetwork.NetworkHandlers.Count && GameNetwork.NetworkHandlers[i] != null)
				{
					string str = GameNetwork.NetworkHandlers[i].ToString();
					Debug.Print("Exception On Network Component: " + str, 0, Debug.DebugColor.White, 17592186044416UL);
				}
				Debug.Print(ex.StackTrace, 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0009A768 File Offset: 0x00098968
		private static void StartMultiplayer()
		{
			VirtualPlayer.Reset();
			GameNetwork._handler.OnStartMultiplayer();
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0009A77C File Offset: 0x0009897C
		public static void EndMultiplayer()
		{
			GameNetwork._handler.OnEndMultiplayer();
			for (int i = GameNetwork.NetworkComponents.Count - 1; i >= 0; i--)
			{
				GameNetwork.DestroyComponent(GameNetwork.NetworkComponents[i]);
			}
			for (int j = GameNetwork.NetworkHandlers.Count - 1; j >= 0; j--)
			{
				GameNetwork.RemoveNetworkHandler(GameNetwork.NetworkHandlers[j]);
			}
			if (GameNetwork.IsServer)
			{
				GameNetwork.TerminateServerSide();
			}
			if (GameNetwork.IsClientOrReplay)
			{
				GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
			}
			if (GameNetwork.IsClient)
			{
				GameNetwork.TerminateClientSide();
			}
			Debug.Print("Clearing peers list with count " + GameNetwork.NetworkPeerCount, 0, Debug.DebugColor.White, 17592186044416UL);
			GameNetwork.ClearAllPeers();
			VirtualPlayer.Reset();
			GameNetwork.MyPeer = null;
			Debug.Print("NetworkManager::HandleMultiplayerEnd", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0009A854 File Offset: 0x00098A54
		[MBCallback]
		internal static void HandleRemovePlayer(MBNetworkPeer peer, bool isTimedOut)
		{
			DisconnectInfo disconnectInfo;
			if ((disconnectInfo = peer.NetworkPeer.PlayerConnectionInfo.GetParameter<DisconnectInfo>("DisconnectInfo")) == null)
			{
				(disconnectInfo = new DisconnectInfo()).Type = DisconnectType.QuitFromGame;
			}
			DisconnectInfo disconnectInfo2 = disconnectInfo;
			disconnectInfo2.Type = (isTimedOut ? DisconnectType.TimedOut : disconnectInfo2.Type);
			peer.NetworkPeer.PlayerConnectionInfo.AddParameter("DisconnectInfo", disconnectInfo2);
			GameNetwork.HandleRemovePlayerInternal(peer.NetworkPeer, peer.NetworkPeer.IsSynchronized && MultiplayerIntermissionVotingManager.Instance.CurrentVoteState == MultiplayerIntermissionState.Idle);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0009A8D8 File Offset: 0x00098AD8
		internal static void HandleRemovePlayerInternal(NetworkCommunicator networkPeer, bool isDisconnected)
		{
			if (GameNetwork.IsClient && networkPeer.IsMine)
			{
				GameNetwork.HandleDisconnect();
				return;
			}
			GameNetwork._handler.OnPlayerDisconnectedFromServer(networkPeer);
			if (GameNetwork.IsServer)
			{
				foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
				{
					udpNetworkHandler.HandleEarlyPlayerDisconnect(networkPeer);
				}
				foreach (IUdpNetworkHandler udpNetworkHandler2 in GameNetwork.NetworkHandlers)
				{
					udpNetworkHandler2.HandlePlayerDisconnect(networkPeer);
				}
			}
			foreach (IUdpNetworkHandler udpNetworkHandler3 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler3.OnPlayerDisconnectedFromServer(networkPeer);
			}
			GameNetwork.RemoveNetworkPeer(networkPeer);
			if (isDisconnected)
			{
				GameNetwork.AddToDisconnectedPeers(networkPeer);
			}
			GameNetwork.VirtualPlayers[networkPeer.VirtualPlayer.Index] = null;
			if (GameNetwork.IsServer)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					if (!networkCommunicator.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkCommunicator);
						GameNetwork.WriteMessage(new DeletePlayer(networkPeer.Index, isDisconnected));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0009AA54 File Offset: 0x00098C54
		[MBCallback]
		internal static void HandleDisconnect()
		{
			GameNetwork._handler.OnDisconnectedFromServer();
			foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler.OnDisconnectedFromServer();
			}
			GameNetwork.MyPeer = null;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0009AAB4 File Offset: 0x00098CB4
		public static void StartReplay()
		{
			GameNetwork._handler.OnStartReplay();
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0009AAC0 File Offset: 0x00098CC0
		public static void EndReplay()
		{
			GameNetwork._handler.OnEndReplay();
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0009AACC File Offset: 0x00098CCC
		public static void PreStartMultiplayerOnServer()
		{
			MBCommon.CurrentGameType = (GameNetwork.IsDedicatedServer ? MBCommon.GameType.MultiServer : MBCommon.GameType.MultiClientServer);
			GameNetwork.ClientPeerIndex = -1;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0009AAE4 File Offset: 0x00098CE4
		public static void StartMultiplayerOnServer(int port)
		{
			Debug.Print("StartMultiplayerOnServer", 0, Debug.DebugColor.White, 17592186044416UL);
			GameNetwork.PreStartMultiplayerOnServer();
			GameNetwork.InitializeServerSide(port);
			GameNetwork.StartMultiplayer();
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0009AB0C File Offset: 0x00098D0C
		[MBCallback]
		internal static bool HandleNetworkPacketAsServer(MBNetworkPeer networkPeer)
		{
			return GameNetwork.HandleNetworkPacketAsServer(networkPeer.NetworkPeer);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0009AB1C File Offset: 0x00098D1C
		internal static bool HandleNetworkPacketAsServer(NetworkCommunicator networkPeer)
		{
			if (networkPeer == null)
			{
				Debug.Print("networkPeer == null", 0, Debug.DebugColor.White, 17592186044416UL);
				return false;
			}
			bool flag = true;
			try
			{
				int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.NetworkComponentEventTypeFromClientCompressionInfo, ref flag);
				if (flag)
				{
					if (num >= 0 && num < GameNetwork._gameNetworkMessageIdsFromClient.Count)
					{
						GameNetworkMessage gameNetworkMessage = Activator.CreateInstance(GameNetwork._gameNetworkMessageIdsFromClient[num]) as GameNetworkMessage;
						gameNetworkMessage.MessageId = num;
						flag = gameNetworkMessage.Read();
						if (flag)
						{
							bool flag2 = false;
							bool flag3 = true;
							List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>> list;
							if (GameNetwork._fromClientBaseMessageHandlers.TryGetValue(num, out list))
							{
								foreach (GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> clientMessageHandlerDelegate in list)
								{
									flag = (flag && clientMessageHandlerDelegate(networkPeer, gameNetworkMessage));
									if (!flag)
									{
										break;
									}
								}
								flag3 = false;
								flag2 = (list.Count != 0);
							}
							List<object> list2;
							if (GameNetwork._fromClientMessageHandlers.TryGetValue(num, out list2))
							{
								foreach (object obj in list2)
								{
									Delegate method = obj as Delegate;
									flag = (flag && (bool)method.DynamicInvokeWithLog(new object[]
									{
										networkPeer,
										gameNetworkMessage
									}));
									if (!flag)
									{
										break;
									}
								}
								flag3 = false;
								flag2 = (flag2 || list2.Count != 0);
							}
							if (flag3)
							{
								Debug.FailedAssert("Unknown network messageId " + gameNetworkMessage, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\GameNetwork.cs", "HandleNetworkPacketAsServer", 765);
								flag = false;
							}
							else if (!flag2)
							{
								Debug.Print("Handler not found for network message " + gameNetworkMessage, 0, Debug.DebugColor.White, 17179869184UL);
							}
						}
					}
					else
					{
						Debug.Print("Handler not found for network message " + num.ToString(), 0, Debug.DebugColor.White, 17179869184UL);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print("error " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				return false;
			}
			return flag;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0009AD5C File Offset: 0x00098F5C
		[MBCallback]
		public static void HandleConsoleCommand(string command)
		{
			if (GameNetwork._handler != null)
			{
				GameNetwork._handler.OnHandleConsoleCommand(command);
			}
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0009AD70 File Offset: 0x00098F70
		private static void InitializeServerSide(int port)
		{
			MBAPI.IMBNetwork.InitializeServerSide(port);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0009AD7D File Offset: 0x00098F7D
		private static void TerminateServerSide()
		{
			MBAPI.IMBNetwork.TerminateServerSide();
			if (!GameNetwork.IsDedicatedServer)
			{
				MBCommon.CurrentGameType = MBCommon.GameType.Single;
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0009AD96 File Offset: 0x00098F96
		private static void PrepareNewUdpSession(int peerIndex, int sessionKey)
		{
			MBAPI.IMBNetwork.PrepareNewUdpSession(peerIndex, sessionKey);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0009ADA4 File Offset: 0x00098FA4
		public static string GetActiveUdpSessionsIpAddress()
		{
			return MBAPI.IMBNetwork.GetActiveUdpSessionsIpAddress();
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0009ADB0 File Offset: 0x00098FB0
		public static ICommunicator AddNewPlayerOnServer(PlayerConnectionInfo playerConnectionInfo, bool serverPeer, bool isAdmin)
		{
			bool flag = playerConnectionInfo == null;
			int num = flag ? MBAPI.IMBNetwork.AddNewBotOnServer() : MBAPI.IMBNetwork.AddNewPlayerOnServer(serverPeer);
			Debug.Print(string.Concat(new object[]
			{
				"AddNewPlayerOnServer: ",
				playerConnectionInfo.Name,
				" index: ",
				num
			}), 0, Debug.DebugColor.White, 17179869184UL);
			if (num >= 0)
			{
				int sessionKey = 0;
				if (!serverPeer)
				{
					sessionKey = GameNetwork.GetSessionKeyForPlayer();
				}
				int num2 = -1;
				ICommunicator communicator = null;
				if (flag)
				{
					communicator = DummyCommunicator.CreateAsServer(num, "");
				}
				else
				{
					for (int i = 0; i < GameNetwork.DisconnectedNetworkPeers.Count; i++)
					{
						PlayerData parameter = playerConnectionInfo.GetParameter<PlayerData>("PlayerData");
						if (parameter != null && GameNetwork.DisconnectedNetworkPeers[i].VirtualPlayer.Id == parameter.PlayerId)
						{
							num2 = i;
							communicator = GameNetwork.DisconnectedNetworkPeers[i];
							NetworkCommunicator networkCommunicator = communicator as NetworkCommunicator;
							networkCommunicator.UpdateIndexForReconnectingPlayer(num);
							networkCommunicator.UpdateConnectionInfoForReconnect(playerConnectionInfo, isAdmin);
							MBAPI.IMBPeer.SetUserData(num, new MBNetworkPeer(networkCommunicator));
							Debug.Print("RemoveFromDisconnectedPeers: " + networkCommunicator.UserName, 0, Debug.DebugColor.White, 17179869184UL);
							GameNetwork.DisconnectedNetworkPeers.RemoveAt(i);
							break;
						}
					}
					if (communicator == null)
					{
						communicator = NetworkCommunicator.CreateAsServer(playerConnectionInfo, num, isAdmin);
					}
				}
				GameNetwork.VirtualPlayers[communicator.VirtualPlayer.Index] = communicator.VirtualPlayer;
				if (!flag)
				{
					NetworkCommunicator networkCommunicator2 = communicator as NetworkCommunicator;
					if (serverPeer && GameNetwork.IsServer)
					{
						GameNetwork.ClientPeerIndex = num;
						GameNetwork.MyPeer = networkCommunicator2;
					}
					networkCommunicator2.SessionKey = sessionKey;
					networkCommunicator2.SetServerPeer(serverPeer);
					GameNetwork.AddNetworkPeer(networkCommunicator2);
					playerConnectionInfo.NetworkPeer = networkCommunicator2;
					if (!serverPeer)
					{
						GameNetwork.PrepareNewUdpSession(num, sessionKey);
					}
					if (num2 < 0)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator2.Index, playerConnectionInfo.Name, num2, false, false));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord | GameNetwork.EventBroadcastFlags.DontSendToPeers, null);
					}
					foreach (NetworkCommunicator networkCommunicator3 in GameNetwork.NetworkPeers)
					{
						if (networkCommunicator3 != networkCommunicator2 && networkCommunicator3 != GameNetwork.MyPeer)
						{
							GameNetwork.BeginModuleEventAsServer(networkCommunicator3);
							GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator2.Index, playerConnectionInfo.Name, num2, false, false));
							GameNetwork.EndModuleEventAsServer();
						}
						if (!serverPeer)
						{
							bool isReceiverPeer = networkCommunicator3 == networkCommunicator2;
							GameNetwork.BeginModuleEventAsServer(networkCommunicator2);
							GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator3.Index, networkCommunicator3.UserName, -1, false, isReceiverPeer));
							GameNetwork.EndModuleEventAsServer();
						}
					}
					for (int j = 0; j < GameNetwork.DisconnectedNetworkPeers.Count; j++)
					{
						NetworkCommunicator networkCommunicator4 = GameNetwork.DisconnectedNetworkPeers[j];
						GameNetwork.BeginModuleEventAsServer(networkCommunicator2);
						GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator4.Index, networkCommunicator4.UserName, j, true, false));
						GameNetwork.EndModuleEventAsServer();
					}
					foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
					{
						udpNetworkHandler.HandleNewClientConnect(playerConnectionInfo);
					}
					GameNetwork._handler.OnPlayerConnectedToServer(networkCommunicator2);
				}
				return communicator;
			}
			return null;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0009B0F8 File Offset: 0x000992F8
		public static GameNetwork.AddPlayersResult AddNewPlayersOnServer(PlayerConnectionInfo[] playerConnectionInfos, bool serverPeer)
		{
			bool flag = MBAPI.IMBNetwork.CanAddNewPlayersOnServer(playerConnectionInfos.Length);
			NetworkCommunicator[] array = new NetworkCommunicator[playerConnectionInfos.Length];
			if (flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					object parameter = playerConnectionInfos[i].GetParameter<object>("IsAdmin");
					bool isAdmin = parameter != null && (bool)parameter;
					ICommunicator communicator = GameNetwork.AddNewPlayerOnServer(playerConnectionInfos[i], serverPeer, isAdmin);
					array[i] = (communicator as NetworkCommunicator);
				}
			}
			return new GameNetwork.AddPlayersResult
			{
				NetworkPeers = array,
				Success = flag
			};
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0009B17C File Offset: 0x0009937C
		public static void ClientFinishedLoading(NetworkCommunicator networkPeer)
		{
			foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler.HandleEarlyNewClientAfterLoadingFinished(networkPeer);
			}
			foreach (IUdpNetworkHandler udpNetworkHandler2 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler2.HandleNewClientAfterLoadingFinished(networkPeer);
			}
			foreach (IUdpNetworkHandler udpNetworkHandler3 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler3.HandleLateNewClientAfterLoadingFinished(networkPeer);
			}
			networkPeer.IsSynchronized = true;
			foreach (IUdpNetworkHandler udpNetworkHandler4 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler4.HandleNewClientAfterSynchronized(networkPeer);
			}
			foreach (IUdpNetworkHandler udpNetworkHandler5 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler5.HandleLateNewClientAfterSynchronized(networkPeer);
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0009B2D0 File Offset: 0x000994D0
		public static void BeginModuleEventAsClient()
		{
			MBAPI.IMBNetwork.BeginModuleEventAsClient(true);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0009B2DD File Offset: 0x000994DD
		public static void EndModuleEventAsClient()
		{
			MBAPI.IMBNetwork.EndModuleEventAsClient(true);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0009B2EA File Offset: 0x000994EA
		public static void BeginModuleEventAsClientUnreliable()
		{
			MBAPI.IMBNetwork.BeginModuleEventAsClient(false);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0009B2F7 File Offset: 0x000994F7
		public static void EndModuleEventAsClientUnreliable()
		{
			MBAPI.IMBNetwork.EndModuleEventAsClient(false);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x0009B304 File Offset: 0x00099504
		public static void BeginModuleEventAsServer(NetworkCommunicator communicator)
		{
			GameNetwork.BeginModuleEventAsServer(communicator.VirtualPlayer);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0009B311 File Offset: 0x00099511
		public static void BeginModuleEventAsServerUnreliable(NetworkCommunicator communicator)
		{
			GameNetwork.BeginModuleEventAsServerUnreliable(communicator.VirtualPlayer);
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0009B31E File Offset: 0x0009951E
		public static void BeginModuleEventAsServer(VirtualPlayer peer)
		{
			MBAPI.IMBPeer.BeginModuleEvent(peer.Index, true);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0009B331 File Offset: 0x00099531
		public static void EndModuleEventAsServer()
		{
			MBAPI.IMBPeer.EndModuleEvent(true);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0009B33E File Offset: 0x0009953E
		public static void BeginModuleEventAsServerUnreliable(VirtualPlayer peer)
		{
			MBAPI.IMBPeer.BeginModuleEvent(peer.Index, false);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0009B351 File Offset: 0x00099551
		public static void EndModuleEventAsServerUnreliable()
		{
			MBAPI.IMBPeer.EndModuleEvent(false);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0009B35E File Offset: 0x0009955E
		public static void BeginBroadcastModuleEvent()
		{
			MBAPI.IMBNetwork.BeginBroadcastModuleEvent();
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0009B36C File Offset: 0x0009956C
		public static void EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags broadcastFlags, NetworkCommunicator targetPlayer = null)
		{
			int targetPlayer2 = (targetPlayer != null) ? targetPlayer.Index : -1;
			MBAPI.IMBNetwork.EndBroadcastModuleEvent((int)broadcastFlags, targetPlayer2, true);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0009B393 File Offset: 0x00099593
		public static double ElapsedTimeSinceLastUdpPacketArrived()
		{
			return MBAPI.IMBNetwork.ElapsedTimeSinceLastUdpPacketArrived();
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0009B3A0 File Offset: 0x000995A0
		public static void EndBroadcastModuleEventUnreliable(GameNetwork.EventBroadcastFlags broadcastFlags, NetworkCommunicator targetPlayer = null)
		{
			int targetPlayer2 = (targetPlayer != null) ? targetPlayer.Index : -1;
			MBAPI.IMBNetwork.EndBroadcastModuleEvent((int)broadcastFlags, targetPlayer2, false);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0009B3C8 File Offset: 0x000995C8
		public static void UnSynchronizeEveryone()
		{
			Debug.Print("UnSynchronizeEveryone is called!", 0, Debug.DebugColor.White, 17179869184UL);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				networkCommunicator.IsSynchronized = false;
			}
			foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler.OnEveryoneUnSynchronized();
			}
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0009B46C File Offset: 0x0009966C
		public static void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			networkMessageHandlerRegisterer.Register<CreatePlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<CreatePlayer>(GameNetwork.HandleServerEventCreatePlayer));
			networkMessageHandlerRegisterer.Register<DeletePlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<DeletePlayer>(GameNetwork.HandleServerEventDeletePlayer));
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0009B497 File Offset: 0x00099697
		public static void StartMultiplayerOnClient(string serverAddress, int port, int sessionKey, int playerIndex)
		{
			Debug.Print("StartMultiplayerOnClient", 0, Debug.DebugColor.White, 17592186044416UL);
			MBCommon.CurrentGameType = MBCommon.GameType.MultiClient;
			GameNetwork.ClientPeerIndex = playerIndex;
			GameNetwork.InitializeClientSide(serverAddress, port, sessionKey, playerIndex);
			GameNetwork.StartMultiplayer();
			GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x0009B4D0 File Offset: 0x000996D0
		[MBCallback]
		internal static bool HandleNetworkPacketAsClient()
		{
			bool flag = true;
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo, ref flag);
			if (flag && num >= 0 && num < GameNetwork._gameNetworkMessageIdsFromServer.Count)
			{
				GameNetworkMessage gameNetworkMessage = Activator.CreateInstance(GameNetwork._gameNetworkMessageIdsFromServer[num]) as GameNetworkMessage;
				gameNetworkMessage.MessageId = num;
				Debug.Print("Reading message: " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
				flag = gameNetworkMessage.Read();
				if (flag)
				{
					if (!NetworkMain.GameClient.IsInGame && !GameNetwork.IsReplay && !NetworkMain.CommunityClient.IsInGame)
					{
						Debug.Print("ignoring post mission message: " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
					}
					else
					{
						bool flag2 = false;
						bool flag3 = true;
						if ((gameNetworkMessage.GetLogFilter() & (MultiplayerMessageFilter)(-1)) != MultiplayerMessageFilter.None)
						{
							if (GameNetworkMessage.IsClientMissionOver)
							{
								Debug.Print("WARNING: Entering message processing while client mission is over", 0, Debug.DebugColor.White, 17592186044416UL);
							}
							Debug.Print("Processing message: " + gameNetworkMessage.GetType().Name + ": " + gameNetworkMessage.GetLogFormat(), 0, Debug.DebugColor.White, 17179869184UL);
						}
						List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>> list;
						if (GameNetwork._fromServerBaseMessageHandlers.TryGetValue(num, out list))
						{
							foreach (GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> serverMessageHandlerDelegate in list)
							{
								try
								{
									serverMessageHandlerDelegate(gameNetworkMessage);
								}
								catch
								{
									Debug.Print("Exception in handler of " + num.ToString(), 0, Debug.DebugColor.White, 17179869184UL);
									Debug.Print("Exception in handler of " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.Red, 17179869184UL);
								}
							}
							flag3 = false;
							flag2 = (list.Count != 0);
						}
						List<object> list2;
						if (GameNetwork._fromServerMessageHandlers.TryGetValue(num, out list2))
						{
							foreach (object obj in list2)
							{
								(obj as Delegate).DynamicInvokeWithLog(new object[]
								{
									gameNetworkMessage
								});
							}
							flag3 = false;
							flag2 = (flag2 || list2.Count != 0);
						}
						if (flag3)
						{
							Debug.Print("Invalid messageId " + num.ToString(), 0, Debug.DebugColor.White, 17179869184UL);
							Debug.Print("Invalid messageId " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
						}
						else if (!flag2)
						{
							Debug.Print("No message handler found for " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.Red, 17179869184UL);
						}
					}
				}
				else
				{
					Debug.Print("Invalid message read for: " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
				}
			}
			else
			{
				Debug.Print("Invalid message id read: " + num, 0, Debug.DebugColor.White, 17179869184UL);
			}
			return flag;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0009B7F0 File Offset: 0x000999F0
		private static int GetSessionKeyForPlayer()
		{
			return new Random(DateTime.Now.Millisecond).Next(1, 4001);
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x0009B81C File Offset: 0x00099A1C
		public static NetworkCommunicator HandleNewClientConnect(PlayerConnectionInfo playerConnectionInfo, bool isAdmin)
		{
			NetworkCommunicator networkCommunicator = GameNetwork.AddNewPlayerOnServer(playerConnectionInfo, false, isAdmin) as NetworkCommunicator;
			GameNetwork._handler.OnNewPlayerConnect(playerConnectionInfo, networkCommunicator);
			return networkCommunicator;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0009B844 File Offset: 0x00099A44
		public static GameNetwork.AddPlayersResult HandleNewClientsConnect(PlayerConnectionInfo[] playerConnectionInfos, bool isAdmin)
		{
			GameNetwork.AddPlayersResult addPlayersResult = GameNetwork.AddNewPlayersOnServer(playerConnectionInfos, isAdmin);
			if (addPlayersResult.Success)
			{
				for (int i = 0; i < playerConnectionInfos.Length; i++)
				{
					GameNetwork._handler.OnNewPlayerConnect(playerConnectionInfos[i], addPlayersResult.NetworkPeers[i]);
				}
			}
			return addPlayersResult;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0009B888 File Offset: 0x00099A88
		public static void AddNetworkPeerToDisconnectAsServer(NetworkCommunicator networkPeer)
		{
			Debug.Print("adding peer to disconnect index:" + networkPeer.Index, 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.AddPeerToDisconnect(networkPeer);
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new DeletePlayer(networkPeer.Index, false));
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0009B8E0 File Offset: 0x00099AE0
		private static void HandleServerEventCreatePlayer(CreatePlayer message)
		{
			int playerIndex = message.PlayerIndex;
			string playerName = message.PlayerName;
			bool isReceiverPeer = message.IsReceiverPeer;
			NetworkCommunicator networkCommunicator;
			if (isReceiverPeer || message.IsNonExistingDisconnectedPeer || message.DisconnectedPeerIndex < 0)
			{
				networkCommunicator = NetworkCommunicator.CreateAsClient(playerName, playerIndex);
			}
			else
			{
				networkCommunicator = GameNetwork.DisconnectedNetworkPeers[message.DisconnectedPeerIndex];
				networkCommunicator.UpdateIndexForReconnectingPlayer(message.PlayerIndex);
				Debug.Print("RemoveFromDisconnectedPeers: " + networkCommunicator.UserName, 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.DisconnectedNetworkPeers.RemoveAt(message.DisconnectedPeerIndex);
			}
			if (isReceiverPeer)
			{
				GameNetwork.MyPeer = networkCommunicator;
			}
			if (message.IsNonExistingDisconnectedPeer)
			{
				GameNetwork.AddToDisconnectedPeers(networkCommunicator);
			}
			else
			{
				GameNetwork.VirtualPlayers[networkCommunicator.VirtualPlayer.Index] = networkCommunicator.VirtualPlayer;
				GameNetwork.AddNetworkPeer(networkCommunicator);
			}
			GameNetwork._handler.OnPlayerConnectedToServer(networkCommunicator);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0009B9B0 File Offset: 0x00099BB0
		private static void HandleServerEventDeletePlayer(DeletePlayer message)
		{
			NetworkCommunicator networkCommunicator = GameNetwork.NetworkPeers.FirstOrDefault((NetworkCommunicator networkPeer) => networkPeer.Index == message.PlayerIndex);
			if (networkCommunicator != null)
			{
				GameNetwork.HandleRemovePlayerInternal(networkCommunicator, message.AddToDisconnectList);
			}
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0009B9F5 File Offset: 0x00099BF5
		public static void InitializeClientSide(string serverAddress, int port, int sessionKey, int playerIndex)
		{
			MBAPI.IMBNetwork.InitializeClientSide(serverAddress, port, sessionKey, playerIndex);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0009BA05 File Offset: 0x00099C05
		public static void TerminateClientSide()
		{
			MBAPI.IMBNetwork.TerminateClientSide();
			MBCommon.CurrentGameType = MBCommon.GameType.Single;
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0009BA17 File Offset: 0x00099C17
		public static Type GetSynchedMissionObjectReadableRecordTypeFromIndex(int typeIndex)
		{
			return GameNetwork._synchedMissionObjectClassTypes[typeIndex];
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0009BA24 File Offset: 0x00099C24
		public static int GetSynchedMissionObjectReadableRecordIndexFromType(Type type)
		{
			for (int i = 0; i < GameNetwork._synchedMissionObjectClassTypes.Count; i++)
			{
				Type element = GameNetwork._synchedMissionObjectClassTypes[i];
				DefineSynchedMissionObjectType customAttribute = element.GetCustomAttribute<DefineSynchedMissionObjectType>();
				DefineSynchedMissionObjectTypeForMod customAttribute2 = element.GetCustomAttribute<DefineSynchedMissionObjectTypeForMod>();
				Type right = ((customAttribute != null) ? customAttribute.Type : null) ?? ((customAttribute2 != null) ? customAttribute2.Type : null);
				Type type2 = type;
				while (type2 != null)
				{
					if (type2 == right)
					{
						return i;
					}
					type2 = type2.BaseType;
				}
			}
			return -1;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0009BAA0 File Offset: 0x00099CA0
		public static void DestroyComponent(UdpNetworkComponent udpNetworkComponent)
		{
			GameNetwork.RemoveNetworkHandler(udpNetworkComponent);
			GameNetwork.NetworkComponents.Remove(udpNetworkComponent);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x0009BAB4 File Offset: 0x00099CB4
		public static T AddNetworkComponent<T>() where T : UdpNetworkComponent
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T), new object[0]));
			GameNetwork.NetworkComponents.Add(t);
			GameNetwork.NetworkHandlers.Add(t);
			return t;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0009BAFD File Offset: 0x00099CFD
		public static void AddNetworkHandler(IUdpNetworkHandler handler)
		{
			GameNetwork.NetworkHandlers.Add(handler);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x0009BB0A File Offset: 0x00099D0A
		public static void RemoveNetworkHandler(IUdpNetworkHandler handler)
		{
			handler.OnUdpNetworkHandlerClose();
			GameNetwork.NetworkHandlers.Remove(handler);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0009BB20 File Offset: 0x00099D20
		public static T GetNetworkComponent<T>() where T : UdpNetworkComponent
		{
			using (List<UdpNetworkComponent>.Enumerator enumerator = GameNetwork.NetworkComponents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T result;
					if ((result = (enumerator.Current as T)) != null)
					{
						return result;
					}
				}
			}
			return default(T);
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0009BB8C File Offset: 0x00099D8C
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x0009BB93 File Offset: 0x00099D93
		public static List<UdpNetworkComponent> NetworkComponents { get; private set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0009BB9B File Offset: 0x00099D9B
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x0009BBA2 File Offset: 0x00099DA2
		public static List<IUdpNetworkHandler> NetworkHandlers { get; private set; }

		// Token: 0x06002859 RID: 10329 RVA: 0x0009BBAC File Offset: 0x00099DAC
		public static void WriteMessage(GameNetworkMessage message)
		{
			Type type = message.GetType();
			message.MessageId = GameNetwork._gameNetworkMessageTypesAll[type];
			message.Write();
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0009BBD8 File Offset: 0x00099DD8
		private static void AddServerMessageHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int key = GameNetwork._gameNetworkMessageTypesFromServer[typeof(T)];
			GameNetwork._fromServerMessageHandlers[key].Add(handler);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0009BC0C File Offset: 0x00099E0C
		private static void AddServerBaseMessageHandler(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int key = GameNetwork._gameNetworkMessageTypesFromServer[messageType];
			GameNetwork._fromServerBaseMessageHandlers[key].Add(handler);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0009BC38 File Offset: 0x00099E38
		private static void AddClientMessageHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int key = GameNetwork._gameNetworkMessageTypesFromClient[typeof(T)];
			GameNetwork._fromClientMessageHandlers[key].Add(handler);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0009BC6C File Offset: 0x00099E6C
		private static void AddClientBaseMessageHandler(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int key = GameNetwork._gameNetworkMessageTypesFromClient[messageType];
			GameNetwork._fromClientBaseMessageHandlers[key].Add(handler);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0009BC98 File Offset: 0x00099E98
		private static void RemoveServerMessageHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int key = GameNetwork._gameNetworkMessageTypesFromServer[typeof(T)];
			GameNetwork._fromServerMessageHandlers[key].Remove(handler);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0009BCCC File Offset: 0x00099ECC
		private static void RemoveServerBaseMessageHandler(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int key = GameNetwork._gameNetworkMessageTypesFromServer[messageType];
			GameNetwork._fromServerBaseMessageHandlers[key].Remove(handler);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0009BCF8 File Offset: 0x00099EF8
		private static void RemoveClientMessageHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int key = GameNetwork._gameNetworkMessageTypesFromClient[typeof(T)];
			GameNetwork._fromClientMessageHandlers[key].Remove(handler);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x0009BD2C File Offset: 0x00099F2C
		private static void RemoveClientBaseMessageHandler(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int key = GameNetwork._gameNetworkMessageTypesFromClient[messageType];
			GameNetwork._fromClientBaseMessageHandlers[key].Remove(handler);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0009BD58 File Offset: 0x00099F58
		internal static void FindGameNetworkMessages()
		{
			Debug.Print("Searching Game NetworkMessages Methods", 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork._fromClientMessageHandlers = new Dictionary<int, List<object>>();
			GameNetwork._fromServerMessageHandlers = new Dictionary<int, List<object>>();
			GameNetwork._fromClientBaseMessageHandlers = new Dictionary<int, List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>>>();
			GameNetwork._fromServerBaseMessageHandlers = new Dictionary<int, List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>>>();
			GameNetwork._gameNetworkMessageTypesAll = new Dictionary<Type, int>();
			GameNetwork._gameNetworkMessageTypesFromClient = new Dictionary<Type, int>();
			GameNetwork._gameNetworkMessageTypesFromServer = new Dictionary<Type, int>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> list = new List<Type>();
			List<Type> list2 = new List<Type>();
			foreach (Assembly assembly in assemblies)
			{
				if (GameNetwork.CheckAssemblyForNetworkMessage(assembly))
				{
					GameNetwork.CollectGameNetworkMessagesFromAssembly(assembly, list, list2);
				}
			}
			list.Sort((Type s1, Type s2) => s1.FullName.CompareTo(s2.FullName));
			list2.Sort((Type s1, Type s2) => s1.FullName.CompareTo(s2.FullName));
			GameNetwork._gameNetworkMessageIdsFromClient = new List<Type>(list.Count);
			for (int j = 0; j < list.Count; j++)
			{
				Type type = list[j];
				GameNetwork._gameNetworkMessageIdsFromClient.Add(type);
				GameNetwork._gameNetworkMessageTypesFromClient.Add(type, j);
				GameNetwork._gameNetworkMessageTypesAll.Add(type, j);
				GameNetwork._fromClientMessageHandlers.Add(j, new List<object>());
				GameNetwork._fromClientBaseMessageHandlers.Add(j, new List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>>());
			}
			GameNetwork._gameNetworkMessageIdsFromServer = new List<Type>(list2.Count);
			for (int k = 0; k < list2.Count; k++)
			{
				Type type2 = list2[k];
				GameNetwork._gameNetworkMessageIdsFromServer.Add(type2);
				GameNetwork._gameNetworkMessageTypesFromServer.Add(type2, k);
				GameNetwork._gameNetworkMessageTypesAll.Add(type2, k);
				GameNetwork._fromServerMessageHandlers.Add(k, new List<object>());
				GameNetwork._fromServerBaseMessageHandlers.Add(k, new List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>>());
			}
			CompressionBasic.NetworkComponentEventTypeFromClientCompressionInfo = new CompressionInfo.Integer(0, list.Count - 1, true);
			CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo = new CompressionInfo.Integer(0, list2.Count - 1, true);
			Debug.Print("Found " + list.Count + " Client Game Network Messages", 0, Debug.DebugColor.White, 17179869184UL);
			Debug.Print("Found " + list2.Count + " Server Game Network Messages", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0009BFB8 File Offset: 0x0009A1B8
		internal static void FindSynchedMissionObjectTypes()
		{
			Debug.Print("Searching Game SynchedMissionObjects", 0, Debug.DebugColor.White, 17179869184UL);
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			GameNetwork._synchedMissionObjectClassTypes = new List<Type>();
			foreach (Assembly assembly in assemblies)
			{
				if (GameNetwork.CheckAssemblyForNetworkMessage(assembly))
				{
					GameNetwork.CollectSynchedMissionObjectTypesFromAssembly(assembly, GameNetwork._synchedMissionObjectClassTypes);
				}
			}
			GameNetwork._synchedMissionObjectClassTypes.Sort((Type s1, Type s2) => s1.FullName.CompareTo(s2.FullName));
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0009C040 File Offset: 0x0009A240
		private static bool CheckAssemblyForNetworkMessage(Assembly assembly)
		{
			Assembly assembly2 = Assembly.GetAssembly(typeof(GameNetworkMessage));
			if (assembly == assembly2)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].FullName == assembly2.FullName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0009C095 File Offset: 0x0009A295
		public static void SetServerBandwidthLimitInMbps(double value)
		{
			MBAPI.IMBNetwork.SetServerBandwidthLimitInMbps(value);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0009C0A2 File Offset: 0x0009A2A2
		public static void SetServerTickRate(double value)
		{
			MBAPI.IMBNetwork.SetServerTickRate(value);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0009C0AF File Offset: 0x0009A2AF
		public static void SetServerFrameRate(double value)
		{
			MBAPI.IMBNetwork.SetServerFrameRate(value);
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		public static void ResetDebugVariables()
		{
			MBAPI.IMBNetwork.ResetDebugVariables();
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0009C0C8 File Offset: 0x0009A2C8
		public static void PrintDebugStats()
		{
			MBAPI.IMBNetwork.PrintDebugStats();
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0009C0D4 File Offset: 0x0009A2D4
		public static float GetAveragePacketLossRatio()
		{
			return MBAPI.IMBNetwork.GetAveragePacketLossRatio();
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0009C0E0 File Offset: 0x0009A2E0
		public static void GetDebugUploadsInBits(ref GameNetwork.DebugNetworkPacketStatisticsStruct networkStatisticsStruct, ref GameNetwork.DebugNetworkPositionCompressionStatisticsStruct posStatisticsStruct)
		{
			MBAPI.IMBNetwork.GetDebugUploadsInBits(ref networkStatisticsStruct, ref posStatisticsStruct);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0009C0EE File Offset: 0x0009A2EE
		public static void PrintReplicationTableStatistics()
		{
			MBAPI.IMBNetwork.PrintReplicationTableStatistics();
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0009C0FA File Offset: 0x0009A2FA
		public static void ClearReplicationTableStatistics()
		{
			MBAPI.IMBNetwork.ClearReplicationTableStatistics();
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0009C106 File Offset: 0x0009A306
		public static void ResetDebugUploads()
		{
			MBAPI.IMBNetwork.ResetDebugUploads();
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0009C112 File Offset: 0x0009A312
		public static void ResetMissionData()
		{
			MBAPI.IMBNetwork.ResetMissionData();
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0009C11E File Offset: 0x0009A31E
		private static void AddPeerToDisconnect(NetworkCommunicator networkPeer)
		{
			MBAPI.IMBNetwork.AddPeerToDisconnect(networkPeer.Index);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x0009C130 File Offset: 0x0009A330
		public static void InitializeCompressionInfos()
		{
			CompressionBasic.ActionCodeCompressionInfo = new CompressionInfo.Integer(ActionIndexCache.act_none.Index, MBAnimation.GetNumActionCodes() - 1, true);
			CompressionBasic.AnimationIndexCompressionInfo = new CompressionInfo.Integer(0, MBAnimation.GetNumAnimations() - 1, true);
			CompressionBasic.CultureIndexCompressionInfo = new CompressionInfo.Integer(-1, MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>().Count - 1, true);
			CompressionBasic.SoundEventsCompressionInfo = new CompressionInfo.Integer(0, SoundEvent.GetTotalEventCount() - 1, true);
			CompressionMission.ActionSetCompressionInfo = new CompressionInfo.Integer(0, MBActionSet.GetNumberOfActionSets() - 1, true);
			CompressionMission.MonsterUsageSetCompressionInfo = new CompressionInfo.Integer(0, MBActionSet.GetNumberOfMonsterUsageSets() - 1, true);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0009C1C2 File Offset: 0x0009A3C2
		[MBCallback]
		internal static void SyncRelevantGameOptionsToServer()
		{
			SyncRelevantGameOptionsToServer syncRelevantGameOptionsToServer = new SyncRelevantGameOptionsToServer();
			syncRelevantGameOptionsToServer.InitializeOptions();
			GameNetwork.BeginModuleEventAsClient();
			GameNetwork.WriteMessage(syncRelevantGameOptionsToServer);
			GameNetwork.EndModuleEventAsClient();
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x0009C1E0 File Offset: 0x0009A3E0
		private static void CollectGameNetworkMessagesFromAssembly(Assembly assembly, List<Type> gameNetworkMessagesFromClient, List<Type> gameNetworkMessagesFromServer)
		{
			Type typeFromHandle = typeof(GameNetworkMessage);
			bool? flag = null;
			List<Type> typesSafe = assembly.GetTypesSafe(null);
			for (int i = 0; i < typesSafe.Count; i++)
			{
				Type type = typesSafe[i];
				if (typeFromHandle.IsAssignableFrom(type) && type != typeFromHandle && type.IsSealed && !(type.GetConstructor(Type.EmptyTypes) == null))
				{
					DefineGameNetworkMessageType customAttribute = type.GetCustomAttribute<DefineGameNetworkMessageType>();
					if (customAttribute != null)
					{
						if (flag == null || !flag.Value)
						{
							flag = new bool?(false);
							GameNetworkMessageSendType sendType = customAttribute.SendType;
							if (sendType != GameNetworkMessageSendType.FromClient)
							{
								if (sendType - GameNetworkMessageSendType.FromServer <= 1)
								{
									gameNetworkMessagesFromServer.Add(type);
								}
							}
							else
							{
								gameNetworkMessagesFromClient.Add(type);
							}
						}
					}
					else
					{
						DefineGameNetworkMessageTypeForMod customAttribute2 = type.GetCustomAttribute<DefineGameNetworkMessageTypeForMod>();
						if (customAttribute2 != null && (flag == null || flag.Value))
						{
							flag = new bool?(true);
							GameNetworkMessageSendType sendType2 = customAttribute2.SendType;
							if (sendType2 != GameNetworkMessageSendType.FromClient)
							{
								if (sendType2 - GameNetworkMessageSendType.FromServer <= 1)
								{
									gameNetworkMessagesFromServer.Add(type);
								}
							}
							else
							{
								gameNetworkMessagesFromClient.Add(type);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x0009C310 File Offset: 0x0009A510
		private static void CollectSynchedMissionObjectTypesFromAssembly(Assembly assembly, List<Type> synchedMissionObjectClassTypes)
		{
			Type typeFromHandle = typeof(ISynchedMissionObjectReadableRecord);
			bool? flag = null;
			List<Type> typesSafe = assembly.GetTypesSafe(null);
			for (int i = 0; i < typesSafe.Count; i++)
			{
				Type type = typesSafe[i];
				if (typeFromHandle.IsAssignableFrom(type) && type != typeFromHandle)
				{
					if (type.GetCustomAttribute<DefineSynchedMissionObjectType>() != null)
					{
						if (flag == null || !flag.Value)
						{
							flag = new bool?(false);
							synchedMissionObjectClassTypes.Add(type);
						}
					}
					else if (type.GetCustomAttribute<DefineSynchedMissionObjectTypeForMod>() != null && (flag == null || flag.Value))
					{
						flag = new bool?(true);
						synchedMissionObjectClassTypes.Add(type);
					}
				}
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x0009C3CD File Offset: 0x0009A5CD
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x0009C3D4 File Offset: 0x0009A5D4
		public static NetworkCommunicator MyPeer { get; private set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x0009C3DC File Offset: 0x0009A5DC
		public static bool IsMyPeerReady
		{
			get
			{
				return GameNetwork.MyPeer != null && GameNetwork.MyPeer.IsSynchronized;
			}
		}

		// Token: 0x04000F3A RID: 3898
		public const int MaxAutomatedBattleIndex = 10;

		// Token: 0x04000F3B RID: 3899
		public const int MaxPlayerCount = 1023;

		// Token: 0x04000F3C RID: 3900
		private static IGameNetworkHandler _handler;

		// Token: 0x04000F40 RID: 3904
		public static int ClientPeerIndex;

		// Token: 0x04000F41 RID: 3905
		private const MultiplayerMessageFilter MultiplayerLogging = MultiplayerMessageFilter.All;

		// Token: 0x04000F44 RID: 3908
		private static Dictionary<Type, int> _gameNetworkMessageTypesAll;

		// Token: 0x04000F45 RID: 3909
		private static Dictionary<Type, int> _gameNetworkMessageTypesFromClient;

		// Token: 0x04000F46 RID: 3910
		private static List<Type> _gameNetworkMessageIdsFromClient;

		// Token: 0x04000F47 RID: 3911
		private static Dictionary<Type, int> _gameNetworkMessageTypesFromServer;

		// Token: 0x04000F48 RID: 3912
		private static List<Type> _gameNetworkMessageIdsFromServer;

		// Token: 0x04000F49 RID: 3913
		private static Dictionary<int, List<object>> _fromClientMessageHandlers;

		// Token: 0x04000F4A RID: 3914
		private static Dictionary<int, List<object>> _fromServerMessageHandlers;

		// Token: 0x04000F4B RID: 3915
		private static Dictionary<int, List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>>> _fromClientBaseMessageHandlers;

		// Token: 0x04000F4C RID: 3916
		private static Dictionary<int, List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>>> _fromServerBaseMessageHandlers;

		// Token: 0x04000F4D RID: 3917
		private static List<Type> _synchedMissionObjectClassTypes;

		// Token: 0x02000599 RID: 1433
		public class NetworkMessageHandlerRegisterer
		{
			// Token: 0x06003A5C RID: 14940 RVA: 0x000E6A23 File Offset: 0x000E4C23
			public NetworkMessageHandlerRegisterer(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode definitionMode)
			{
				this._registerMode = definitionMode;
			}

			// Token: 0x06003A5D RID: 14941 RVA: 0x000E6A32 File Offset: 0x000E4C32
			public void Register<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddServerMessageHandler<T>(handler);
					return;
				}
				GameNetwork.RemoveServerMessageHandler<T>(handler);
			}

			// Token: 0x06003A5E RID: 14942 RVA: 0x000E6A49 File Offset: 0x000E4C49
			public void RegisterBaseHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddServerBaseMessageHandler(handler, typeof(T));
					return;
				}
				GameNetwork.RemoveServerBaseMessageHandler(handler, typeof(T));
			}

			// Token: 0x06003A5F RID: 14943 RVA: 0x000E6A74 File Offset: 0x000E4C74
			public void Register<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddClientMessageHandler<T>(handler);
					return;
				}
				GameNetwork.RemoveClientMessageHandler<T>(handler);
			}

			// Token: 0x06003A60 RID: 14944 RVA: 0x000E6A8B File Offset: 0x000E4C8B
			public void RegisterBaseHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddClientBaseMessageHandler(handler, typeof(T));
					return;
				}
				GameNetwork.RemoveClientBaseMessageHandler(handler, typeof(T));
			}

			// Token: 0x04001DB2 RID: 7602
			private readonly GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode _registerMode;

			// Token: 0x02000688 RID: 1672
			public enum RegisterMode
			{
				// Token: 0x04002193 RID: 8595
				Add,
				// Token: 0x04002194 RID: 8596
				Remove
			}
		}

		// Token: 0x0200059A RID: 1434
		public class NetworkMessageHandlerRegistererContainer
		{
			// Token: 0x06003A61 RID: 14945 RVA: 0x000E6AB6 File Offset: 0x000E4CB6
			public NetworkMessageHandlerRegistererContainer()
			{
				this._fromClientHandlers = new List<Delegate>();
				this._fromServerHandlers = new List<Delegate>();
				this._fromServerBaseHandlers = new List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>>();
				this._fromClientBaseHandlers = new List<Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type>>();
			}

			// Token: 0x06003A62 RID: 14946 RVA: 0x000E6AEA File Offset: 0x000E4CEA
			public void RegisterBaseHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler) where T : GameNetworkMessage
			{
				this._fromServerBaseHandlers.Add(new Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>(handler, typeof(T)));
			}

			// Token: 0x06003A63 RID: 14947 RVA: 0x000E6B07 File Offset: 0x000E4D07
			public void Register<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				this._fromServerHandlers.Add(handler);
			}

			// Token: 0x06003A64 RID: 14948 RVA: 0x000E6B15 File Offset: 0x000E4D15
			public void RegisterBaseHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler)
			{
				this._fromClientBaseHandlers.Add(new Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type>(handler, typeof(T)));
			}

			// Token: 0x06003A65 RID: 14949 RVA: 0x000E6B32 File Offset: 0x000E4D32
			public void Register<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				this._fromClientHandlers.Add(handler);
			}

			// Token: 0x06003A66 RID: 14950 RVA: 0x000E6B40 File Offset: 0x000E4D40
			public void RegisterMessages()
			{
				if (this._fromServerHandlers.Count > 0 || this._fromServerBaseHandlers.Count > 0)
				{
					foreach (Delegate @delegate in this._fromServerHandlers)
					{
						Type key = @delegate.GetType().GenericTypeArguments[0];
						int key2 = GameNetwork._gameNetworkMessageTypesFromServer[key];
						GameNetwork._fromServerMessageHandlers[key2].Add(@delegate);
					}
					using (List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>>.Enumerator enumerator2 = this._fromServerBaseHandlers.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type> tuple = enumerator2.Current;
							int key3 = GameNetwork._gameNetworkMessageTypesFromServer[tuple.Item2];
							GameNetwork._fromServerBaseMessageHandlers[key3].Add(tuple.Item1);
						}
						return;
					}
				}
				foreach (Delegate delegate2 in this._fromClientHandlers)
				{
					Type key4 = delegate2.GetType().GenericTypeArguments[0];
					int key5 = GameNetwork._gameNetworkMessageTypesFromClient[key4];
					GameNetwork._fromClientMessageHandlers[key5].Add(delegate2);
				}
				foreach (Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type> tuple2 in this._fromClientBaseHandlers)
				{
					int key6 = GameNetwork._gameNetworkMessageTypesFromClient[tuple2.Item2];
					GameNetwork._fromClientBaseMessageHandlers[key6].Add(tuple2.Item1);
				}
			}

			// Token: 0x06003A67 RID: 14951 RVA: 0x000E6D18 File Offset: 0x000E4F18
			public void UnregisterMessages()
			{
				if (this._fromServerHandlers.Count > 0 || this._fromServerBaseHandlers.Count > 0)
				{
					foreach (Delegate @delegate in this._fromServerHandlers)
					{
						Type key = @delegate.GetType().GenericTypeArguments[0];
						int key2 = GameNetwork._gameNetworkMessageTypesFromServer[key];
						GameNetwork._fromServerMessageHandlers[key2].Remove(@delegate);
					}
					using (List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>>.Enumerator enumerator2 = this._fromServerBaseHandlers.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type> tuple = enumerator2.Current;
							int key3 = GameNetwork._gameNetworkMessageTypesFromServer[tuple.Item2];
							GameNetwork._fromServerBaseMessageHandlers[key3].Remove(tuple.Item1);
						}
						return;
					}
				}
				foreach (Delegate delegate2 in this._fromClientHandlers)
				{
					Type key4 = delegate2.GetType().GenericTypeArguments[0];
					int key5 = GameNetwork._gameNetworkMessageTypesFromClient[key4];
					GameNetwork._fromClientMessageHandlers[key5].Remove(delegate2);
				}
				foreach (Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type> tuple2 in this._fromClientBaseHandlers)
				{
					int key6 = GameNetwork._gameNetworkMessageTypesFromClient[tuple2.Item2];
					GameNetwork._fromClientBaseMessageHandlers[key6].Remove(tuple2.Item1);
				}
			}

			// Token: 0x04001DB3 RID: 7603
			private List<Delegate> _fromClientHandlers;

			// Token: 0x04001DB4 RID: 7604
			private List<Delegate> _fromServerHandlers;

			// Token: 0x04001DB5 RID: 7605
			private List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>> _fromServerBaseHandlers;

			// Token: 0x04001DB6 RID: 7606
			private List<Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type>> _fromClientBaseHandlers;
		}

		// Token: 0x0200059B RID: 1435
		[Flags]
		public enum EventBroadcastFlags
		{
			// Token: 0x04001DB8 RID: 7608
			None = 0,
			// Token: 0x04001DB9 RID: 7609
			ExcludeTargetPlayer = 1,
			// Token: 0x04001DBA RID: 7610
			ExcludeNoBloodStainsOption = 2,
			// Token: 0x04001DBB RID: 7611
			ExcludeNoParticlesOption = 4,
			// Token: 0x04001DBC RID: 7612
			ExcludeNoSoundOption = 8,
			// Token: 0x04001DBD RID: 7613
			AddToMissionRecord = 16,
			// Token: 0x04001DBE RID: 7614
			IncludeUnsynchronizedClients = 32,
			// Token: 0x04001DBF RID: 7615
			ExcludeOtherTeamPlayers = 64,
			// Token: 0x04001DC0 RID: 7616
			ExcludePeerTeamPlayers = 128,
			// Token: 0x04001DC1 RID: 7617
			DontSendToPeers = 256
		}

		// Token: 0x0200059C RID: 1436
		[EngineStruct("Debug_network_position_compression_statistics_struct", false)]
		public struct DebugNetworkPositionCompressionStatisticsStruct
		{
			// Token: 0x04001DC2 RID: 7618
			public int totalPositionUpload;

			// Token: 0x04001DC3 RID: 7619
			public int totalPositionPrecisionBitCount;

			// Token: 0x04001DC4 RID: 7620
			public int totalPositionCoarseBitCountX;

			// Token: 0x04001DC5 RID: 7621
			public int totalPositionCoarseBitCountY;

			// Token: 0x04001DC6 RID: 7622
			public int totalPositionCoarseBitCountZ;
		}

		// Token: 0x0200059D RID: 1437
		[EngineStruct("Debug_network_packet_statistics_struct", false)]
		public struct DebugNetworkPacketStatisticsStruct
		{
			// Token: 0x04001DC7 RID: 7623
			public int TotalPackets;

			// Token: 0x04001DC8 RID: 7624
			public int TotalUpload;

			// Token: 0x04001DC9 RID: 7625
			public int TotalConstantsUpload;

			// Token: 0x04001DCA RID: 7626
			public int TotalReliableEventUpload;

			// Token: 0x04001DCB RID: 7627
			public int TotalReplicationUpload;

			// Token: 0x04001DCC RID: 7628
			public int TotalUnreliableEventUpload;

			// Token: 0x04001DCD RID: 7629
			public int TotalReplicationTableAdderCount;

			// Token: 0x04001DCE RID: 7630
			public int TotalReplicationTableAdderBitCount;

			// Token: 0x04001DCF RID: 7631
			public int TotalReplicationTableAdder;

			// Token: 0x04001DD0 RID: 7632
			public double TotalCellPriority;

			// Token: 0x04001DD1 RID: 7633
			public double TotalCellAgentPriority;

			// Token: 0x04001DD2 RID: 7634
			public double TotalCellCellPriority;

			// Token: 0x04001DD3 RID: 7635
			public int TotalCellPriorityChecks;

			// Token: 0x04001DD4 RID: 7636
			public int TotalSentCellCount;

			// Token: 0x04001DD5 RID: 7637
			public int TotalNotSentCellCount;

			// Token: 0x04001DD6 RID: 7638
			public int TotalReplicationWriteCount;

			// Token: 0x04001DD7 RID: 7639
			public int CurMaxPacketSizeInBytes;

			// Token: 0x04001DD8 RID: 7640
			public double AveragePingTime;

			// Token: 0x04001DD9 RID: 7641
			public double AverageDtToSendPacket;

			// Token: 0x04001DDA RID: 7642
			public double TimeOutPeriod;

			// Token: 0x04001DDB RID: 7643
			public double PacingRate;

			// Token: 0x04001DDC RID: 7644
			public double DeliveryRate;

			// Token: 0x04001DDD RID: 7645
			public double RoundTripTime;

			// Token: 0x04001DDE RID: 7646
			public int InflightBitCount;

			// Token: 0x04001DDF RID: 7647
			public int IsCongested;

			// Token: 0x04001DE0 RID: 7648
			public int ProbeBwPhaseIndex;

			// Token: 0x04001DE1 RID: 7649
			public double LostPercent;

			// Token: 0x04001DE2 RID: 7650
			public int LostCount;

			// Token: 0x04001DE3 RID: 7651
			public int TotalCountOnLostCheck;
		}

		// Token: 0x0200059E RID: 1438
		public struct AddPlayersResult
		{
			// Token: 0x04001DE4 RID: 7652
			public bool Success;

			// Token: 0x04001DE5 RID: 7653
			public NetworkCommunicator[] NetworkPeers;
		}
	}
}
