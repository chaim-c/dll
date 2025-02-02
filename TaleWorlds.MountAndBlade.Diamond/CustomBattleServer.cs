using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Messages.FromCustomBattleServer.ToCustomBattleServerManager;
using Messages.FromCustomBattleServerManager.ToCustomBattleServer;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.ClientApplication;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000116 RID: 278
	public class CustomBattleServer : Client<CustomBattleServer>
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00007A0D File Offset: 0x00005C0D
		public bool Finished
		{
			get
			{
				return this._state == CustomBattleServer.State.Finished;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00007A18 File Offset: 0x00005C18
		public bool IsRegistered
		{
			get
			{
				return this._state == CustomBattleServer.State.RegisteredGame || this._state == CustomBattleServer.State.RegisteredServer;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00007A2E File Offset: 0x00005C2E
		public bool IsPlaying
		{
			get
			{
				return this._state == CustomBattleServer.State.RegisteredGame;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00007A39 File Offset: 0x00005C39
		public bool Connected
		{
			get
			{
				return this.CurrentState != CustomBattleServer.State.Working && this.CurrentState > CustomBattleServer.State.Idle;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00007A4F File Offset: 0x00005C4F
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00007A58 File Offset: 0x00005C58
		public CustomBattleServer.State CurrentState
		{
			get
			{
				return this._state;
			}
			private set
			{
				if (this._state != value)
				{
					CustomBattleServer.State state = this._state;
					this._state = value;
					if (this._handler != null)
					{
						this._handler.OnStateChanged(state);
					}
				}
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00007A90 File Offset: 0x00005C90
		public bool IsIdle
		{
			get
			{
				return this._state == CustomBattleServer.State.RegisteredGame && this._customBattlePlayers.Count == 0 && this._useTimeoutTimer && this._timeoutTimer.ElapsedMilliseconds > (long)this._timeoutDuration;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00007AC8 File Offset: 0x00005CC8
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public string CustomGameType { get; private set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00007AD9 File Offset: 0x00005CD9
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00007AE1 File Offset: 0x00005CE1
		public string CustomGameScene { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00007AEA File Offset: 0x00005CEA
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00007AF2 File Offset: 0x00005CF2
		public int Port { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00007AFB File Offset: 0x00005CFB
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00007B03 File Offset: 0x00005D03
		public MultipleBattleResult BattleResult { get; private set; }

		// Token: 0x0600060A RID: 1546 RVA: 0x00007B0C File Offset: 0x00005D0C
		public CustomBattleServer(DiamondClientApplication diamondClientApplication, IClientSessionProvider<CustomBattleServer> provider) : base(diamondClientApplication, provider, false)
		{
			this._peerId = new PeerId(Guid.NewGuid());
			this._customBattlePlayers = new List<PlayerId>();
			this._requestedPlayers = new List<PlayerId>();
			this._timeoutTimer = new Stopwatch();
			this._terminationTime = null;
			this._state = CustomBattleServer.State.Idle;
			this._timer = new Stopwatch();
			this._timer.Start();
			if (!base.Application.Parameters.TryGetParameterAsInt("CustomBattleServer.TimeoutDuration", out this._timeoutDuration))
			{
				this._timeoutDuration = this._defaultServerTimeoutDuration;
			}
			this._badgeComponent = null;
			this._badgeComponentPlayers = new List<PlayerData>();
			this.BattleResult = new MultipleBattleResult();
			base.AddMessageHandler<ClientWantsToConnectCustomGameMessage>(new ClientMessageHandler<ClientWantsToConnectCustomGameMessage>(this.OnClientWantsToConnectCustomGameMessage));
			base.AddMessageHandler<ClientQuitFromCustomGameMessage>(new ClientMessageHandler<ClientQuitFromCustomGameMessage>(this.OnClientQuitFromCustomGameMessage));
			base.AddMessageHandler<TerminateOperationCustomMessage>(new ClientMessageHandler<TerminateOperationCustomMessage>(this.OnTerminateOperationCustomMessage));
			base.AddMessageHandler<SetChatFilterListsMessage>(new ClientMessageHandler<SetChatFilterListsMessage>(this.OnSetChatFilterListsMessage));
			base.AddMessageHandler<PlayerDisconnectedFromLobbyMessage>(new ClientMessageHandler<PlayerDisconnectedFromLobbyMessage>(this.OnPlayerDisconnectedFromLobbyMessage));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00007C28 File Offset: 0x00005E28
		public void SetBadgeComponent(IBadgeComponent badgeComponent)
		{
			this._badgeComponent = badgeComponent;
			if (this._badgeComponent != null)
			{
				foreach (PlayerData playerData in this._badgeComponentPlayers)
				{
					this._badgeComponent.OnPlayerJoin(playerData);
				}
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00007C90 File Offset: 0x00005E90
		public void Connect(ICustomBattleServerSessionHandler handler, string authToken, bool isSinglePlatformServer, string[] loadedModuleIDs, bool allowsOptionalModules, bool isPlayerHosted)
		{
			this._handler = handler;
			this._authToken = authToken;
			this._allowsOptionalModules = allowsOptionalModules;
			this._useTimeoutTimer = !isPlayerHosted;
			this._isSinglePlatformServer = isSinglePlatformServer;
			this._loadedModules = new List<ModuleInfoModel>();
			foreach (ModuleInfo moduleInfo in ModuleHelper.GetSortedModules(loadedModuleIDs))
			{
				if (!allowsOptionalModules && moduleInfo.Category == ModuleCategory.MultiplayerOptional)
				{
					throw new InvalidOperationException("Optional modules are explicitly disallowed, yet an optional module (" + moduleInfo.Id + ") was loaded! You must use category 'Server' instead of 'MultiplayerOptional'.");
				}
				ModuleInfoModel item;
				if (ModuleInfoModel.TryCreateForSession(moduleInfo, out item))
				{
					this._loadedModules.Add(item);
				}
			}
			this.CurrentState = CustomBattleServer.State.Working;
			base.BeginConnect();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00007D5C File Offset: 0x00005F5C
		public override void OnConnected()
		{
			base.OnConnected();
			this.CurrentState = CustomBattleServer.State.Connected;
			if (this._handler != null)
			{
				this._handler.OnConnected();
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00007D7E File Offset: 0x00005F7E
		public override void OnCantConnect()
		{
			base.OnCantConnect();
			this.CurrentState = CustomBattleServer.State.Idle;
			if (this._handler != null)
			{
				this._handler.OnCantConnect();
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.CurrentState = CustomBattleServer.State.Idle;
			if (this._handler != null)
			{
				this._handler.OnDisconnected();
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00007DC4 File Offset: 0x00005FC4
		protected override void OnTick()
		{
			if (this._terminationTime != null && this._terminationTime < DateTime.UtcNow)
			{
				throw new Exception("Now I am become Death, the destroyer of worlds");
			}
			long elapsedMilliseconds = this._timer.ElapsedMilliseconds;
			float num = (float)(elapsedMilliseconds - this._previousTimeInMS);
			this._previousTimeInMS = elapsedMilliseconds;
			float num2 = num / 1000f;
			this._battleResultUpdateTimeElapsed += num2;
			if (this._battleResultUpdateTimeElapsed >= 5f)
			{
				if (this._latestQueuedBattleResult != null && this._latestQueuedTeamScores != null && this._latestQueuedPlayerScores != null)
				{
					base.SendMessage(new CustomBattleServerStatsUpdateMessage(this._latestQueuedBattleResult, this._latestQueuedTeamScores, this._latestQueuedPlayerScores));
					this._latestQueuedBattleResult = null;
					this._latestQueuedTeamScores = null;
					this._latestQueuedPlayerScores = null;
				}
				this._battleResultUpdateTimeElapsed = 0f;
			}
			CustomBattleServer.State state = this._state;
			if (state == CustomBattleServer.State.Connected)
			{
				this.DoLogin();
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00007EB8 File Offset: 0x000060B8
		private async void DoLogin()
		{
			this._state = CustomBattleServer.State.SessionRequested;
			LoginResult loginResult = await base.Login(new CustomBattleServerReadyMessage(this._peerId, base.ApplicationVersion, this._authToken, this._loadedModules.ToArray(), this._allowsOptionalModules));
			if (loginResult != null && loginResult.Successful)
			{
				this._state = CustomBattleServer.State.RegisteredServer;
			}
			else
			{
				Console.WriteLine("Login Failed! Server is shutting down.");
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00007EF1 File Offset: 0x000060F1
		private void OnClientWantsToConnectCustomGameMessage(ClientWantsToConnectCustomGameMessage message)
		{
			this.HandleOnClientWantsToConnectCustomGameMessage(message);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00007EFC File Offset: 0x000060FC
		private async void HandleOnClientWantsToConnectCustomGameMessage(ClientWantsToConnectCustomGameMessage message)
		{
			List<PlayerJoinGameResponseDataFromHost> responses = new List<PlayerJoinGameResponseDataFromHost>();
			if (this.CurrentState == CustomBattleServer.State.Finished)
			{
				foreach (PlayerJoinGameData playerJoinGameData2 in message.PlayerJoinGameData)
				{
					responses.Add(new PlayerJoinGameResponseDataFromHost
					{
						PlayerId = playerJoinGameData2.PlayerId,
						PeerIndex = -1,
						SessionKey = -1,
						CustomGameJoinResponse = CustomGameJoinResponse.CustomGameServerFinishing
					});
				}
			}
			else
			{
				PlayerJoinGameData[] requestedPlayers = message.PlayerJoinGameData;
				for (int k = 0; k < requestedPlayers.Length; k++)
				{
					if (requestedPlayers[k] != null)
					{
						PlayerJoinGameData playerJoinGameData3 = requestedPlayers[k];
						Debug.Print(string.Concat(new object[]
						{
							"Player ",
							playerJoinGameData3.Name,
							" - ",
							playerJoinGameData3.PlayerId,
							" with IP address ",
							playerJoinGameData3.IpAddress,
							" wants to join the game"
						}), 0, Debug.DebugColor.White, 17592186044416UL);
					}
				}
				int j;
				for (int i = 0; i < requestedPlayers.Length; i = j + 1)
				{
					if (requestedPlayers[i] != null)
					{
						List<PlayerJoinGameData> requestedGroup = new List<PlayerJoinGameData>();
						PlayerJoinGameData playerJoinGameData4 = requestedPlayers[i];
						Guid? partyId = playerJoinGameData4.PartyId;
						if (partyId == null)
						{
							requestedGroup.Add(playerJoinGameData4);
						}
						else
						{
							for (int l = i; l < requestedPlayers.Length; l++)
							{
								PlayerJoinGameData playerJoinGameData5 = requestedPlayers[l];
								partyId = playerJoinGameData4.PartyId;
								if (partyId.Equals((playerJoinGameData5 != null) ? playerJoinGameData5.PartyId : null))
								{
									requestedGroup.Add(playerJoinGameData5);
									requestedPlayers[l] = null;
								}
							}
						}
						bool flag = true;
						foreach (PlayerJoinGameData playerJoinGameData6 in requestedGroup)
						{
							if (this._requestedPlayers.Contains(playerJoinGameData6.PlayerId) || this._customBattlePlayers.Contains(playerJoinGameData6.PlayerId))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							this._timeoutTimer.Restart();
							foreach (PlayerJoinGameData playerJoinGameData7 in requestedGroup)
							{
								this._requestedPlayers.Add(playerJoinGameData7.PlayerId);
							}
							if (this._handler != null)
							{
								PlayerJoinGameResponseDataFromHost[] array = await this._handler.OnClientWantsToConnectCustomGame(requestedGroup.ToArray());
								if (this._badgeComponent != null)
								{
									foreach (PlayerJoinGameResponseDataFromHost playerJoinGameResponseDataFromHost in array)
									{
										if (playerJoinGameResponseDataFromHost.CustomGameJoinResponse == CustomGameJoinResponse.Success)
										{
											foreach (PlayerJoinGameData playerJoinGameData8 in requestedGroup)
											{
												if (playerJoinGameData8.PlayerId.Equals(playerJoinGameResponseDataFromHost.PlayerId))
												{
													this._badgeComponent.OnPlayerJoin(playerJoinGameData8.PlayerData);
													this._badgeComponentPlayers.Add(playerJoinGameData8.PlayerData);
												}
											}
										}
									}
								}
								responses.AddRange(array);
							}
						}
						else
						{
							foreach (PlayerJoinGameData playerJoinGameData9 in requestedGroup)
							{
								responses.Add(new PlayerJoinGameResponseDataFromHost
								{
									PlayerId = playerJoinGameData9.PlayerId,
									PeerIndex = -1,
									SessionKey = -1,
									CustomGameJoinResponse = CustomGameJoinResponse.NotAllPlayersReady
								});
							}
						}
						requestedGroup = null;
					}
					j = i;
				}
				requestedPlayers = null;
			}
			this.ResponseCustomGameClientConnection(responses.ToArray());
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00007F40 File Offset: 0x00006140
		private void OnClientQuitFromCustomGameMessage(ClientQuitFromCustomGameMessage message)
		{
			if (this.CurrentState == CustomBattleServer.State.RegisteredGame && this._customBattlePlayers.Contains(message.PlayerId))
			{
				if (this._handler != null)
				{
					this._handler.OnClientQuitFromCustomGame(message.PlayerId);
				}
				this._customBattlePlayers.Remove(message.PlayerId);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00007F94 File Offset: 0x00006194
		public void OnPlayerDisconnectedFromLobbyMessage(PlayerDisconnectedFromLobbyMessage message)
		{
			this.HandlePlayerDisconnect(message.PlayerId, DisconnectType.DisconnectedFromLobby);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00007FA4 File Offset: 0x000061A4
		private void OnTerminateOperationCustomMessage(TerminateOperationCustomMessage message)
		{
			Random random = new Random();
			this._terminationTime = new DateTime?(DateTime.UtcNow.AddMilliseconds((double)random.Next(3000, 10000)));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00007FE0 File Offset: 0x000061E0
		private void OnSetChatFilterListsMessage(SetChatFilterListsMessage message)
		{
			if (this._handler != null)
			{
				this._handler.OnChatFilterListsReceived(message.ProfanityList, message.AllowList);
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00008004 File Offset: 0x00006204
		public void ResponseCustomGameClientConnection(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			if (this.CurrentState == CustomBattleServer.State.RegisteredGame)
			{
				foreach (PlayerJoinGameResponseDataFromHost playerJoinGameResponseDataFromHost in playerJoinData)
				{
					this._requestedPlayers.Remove(playerJoinGameResponseDataFromHost.PlayerId);
					if (playerJoinGameResponseDataFromHost.CustomGameJoinResponse == CustomGameJoinResponse.Success)
					{
						this._customBattlePlayers.Add(playerJoinGameResponseDataFromHost.PlayerId);
					}
				}
				base.SendMessage(new ResponseCustomGameClientConnectionMessage(playerJoinData));
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00008068 File Offset: 0x00006268
		public async Task RegisterGame(string gameModule, string gameType, string serverName, int maxPlayerCount, string scene, string uniqueSceneId, int port, string region, string gamePassword, string adminPassword, int permission)
		{
			await this.RegisterGame(0, gameModule, gameType, serverName, maxPlayerCount, scene, uniqueSceneId, port, region, gamePassword, adminPassword, permission, string.Empty);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00008110 File Offset: 0x00006310
		public async Task RegisterGame(int gameDefinitionId, string gameModule, string gameType, string serverName, int maxPlayerCount, string scene, string uniqueSceneId, int port, string region, string gamePassword, string adminPassword, int permission, string overriddenIP)
		{
			this.Port = port;
			this.CustomGameType = gameType;
			this.CustomGameScene = scene;
			string serverAddress;
			if (!base.Application.Parameters.TryGetParameter("CustomBattleServer.Host.Address", out serverAddress))
			{
				serverAddress = null;
			}
			bool isOverridingIP = false;
			if (overriddenIP != string.Empty)
			{
				isOverridingIP = true;
				serverAddress = overriddenIP;
			}
			RegisterCustomGameMessageResponseMessage registerCustomGameMessageResponseMessage = await base.CallFunction<RegisterCustomGameMessageResponseMessage>(new RegisterCustomGameMessage(gameDefinitionId, gameModule, gameType, serverName, serverAddress, maxPlayerCount, scene, uniqueSceneId, gamePassword, adminPassword, port, region, permission, !this._isSinglePlatformServer, isOverridingIP));
			this._shouldReportActivities = registerCustomGameMessageResponseMessage.ShouldReportActivities;
			this.CurrentState = CustomBattleServer.State.RegisteredGame;
			this._timeoutTimer.Start();
			if (this._handler != null)
			{
				this._handler.OnSuccessfulGameRegister();
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000081C7 File Offset: 0x000063C7
		public void UpdateCustomGameData(string newGameType, string newMap, int newCount)
		{
			base.SendMessage(new UpdateCustomGameData(newGameType, newMap, newCount));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000081D7 File Offset: 0x000063D7
		public void KickPlayer(PlayerId id, bool banPlayer)
		{
			ICustomBattleServerSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerKickRequested(id, banPlayer);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000081EB File Offset: 0x000063EB
		public void HandlePlayerDisconnect(PlayerId playerId, DisconnectType disconnectType)
		{
			this._timeoutTimer.Restart();
			this._customBattlePlayers.Remove(playerId);
			base.SendMessage(new PlayerDisconnectedMessage(playerId, disconnectType));
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00008212 File Offset: 0x00006412
		public void FinishAsIdle(GameLog[] gameLogs)
		{
			this.FinishGame(gameLogs);
			base.BeginDisconnect();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00008221 File Offset: 0x00006421
		public void FinishGame(GameLog[] gameLogs)
		{
			this.CurrentState = CustomBattleServer.State.Finished;
			if (this._handler != null)
			{
				this._handler.OnGameFinished();
			}
			IBadgeComponent badgeComponent = this._badgeComponent;
			base.SendMessage(new CustomBattleServerFinishingMessage(gameLogs, (badgeComponent != null) ? badgeComponent.DataDictionary : null, this.BattleResult));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00008261 File Offset: 0x00006461
		public void UpdateGameProperties(string gameType, string scene, string uniqueSceneId)
		{
			this.CustomGameType = gameType;
			this.CustomGameScene = scene;
			base.SendMessage(new UpdateGamePropertiesMessage(gameType, scene, uniqueSceneId));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000827F File Offset: 0x0000647F
		public void BeforeStartingNextBattle(GameLog[] gameLogs)
		{
			IBadgeComponent badgeComponent = this._badgeComponent;
			if (badgeComponent != null)
			{
				badgeComponent.OnStartingNextBattle();
			}
			if (gameLogs != null && gameLogs.Length != 0)
			{
				base.SendMessage(new AddGameLogsMessage(gameLogs));
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000082A5 File Offset: 0x000064A5
		public void BattleStarted(Dictionary<PlayerId, int> playerTeams, string cultureTeam1, string cultureTeam2)
		{
			if (this._shouldReportActivities)
			{
				base.SendMessage(new CustomBattleStartedMessage(this.CustomGameType, playerTeams, new List<string>
				{
					cultureTeam2,
					cultureTeam1
				}));
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000082D4 File Offset: 0x000064D4
		public void BattleFinished(BattleResult battleResult, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			if (this._shouldReportActivities)
			{
				base.SendMessage(new CustomBattleFinishedMessage(battleResult, teamScores, playerScores));
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000082EC File Offset: 0x000064EC
		public void UpdateBattleStats(BattleResult battleResult, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			if (this._shouldReportActivities)
			{
				this._latestQueuedBattleResult = battleResult;
				this._latestQueuedTeamScores = teamScores;
				this._latestQueuedPlayerScores = playerScores;
			}
		}

		// Token: 0x0400023E RID: 574
		private CustomBattleServer.State _state;

		// Token: 0x0400023F RID: 575
		private string _authToken;

		// Token: 0x04000240 RID: 576
		private List<ModuleInfoModel> _loadedModules;

		// Token: 0x04000241 RID: 577
		private bool _allowsOptionalModules;

		// Token: 0x04000242 RID: 578
		private bool _isSinglePlatformServer;

		// Token: 0x04000243 RID: 579
		private Stopwatch _timer;

		// Token: 0x04000244 RID: 580
		private long _previousTimeInMS;

		// Token: 0x04000249 RID: 585
		private ICustomBattleServerSessionHandler _handler;

		// Token: 0x0400024A RID: 586
		private PeerId _peerId;

		// Token: 0x0400024B RID: 587
		private List<PlayerId> _customBattlePlayers;

		// Token: 0x0400024C RID: 588
		private List<PlayerId> _requestedPlayers;

		// Token: 0x0400024D RID: 589
		private int _defaultServerTimeoutDuration = 600000;

		// Token: 0x0400024E RID: 590
		private int _timeoutDuration;

		// Token: 0x0400024F RID: 591
		private Stopwatch _timeoutTimer;

		// Token: 0x04000250 RID: 592
		private DateTime? _terminationTime;

		// Token: 0x04000251 RID: 593
		private bool _useTimeoutTimer;

		// Token: 0x04000252 RID: 594
		private IBadgeComponent _badgeComponent;

		// Token: 0x04000253 RID: 595
		private readonly List<PlayerData> _badgeComponentPlayers;

		// Token: 0x04000254 RID: 596
		private bool _shouldReportActivities;

		// Token: 0x04000255 RID: 597
		private const float BattleResultUpdatePeriod = 5f;

		// Token: 0x04000256 RID: 598
		private float _battleResultUpdateTimeElapsed;

		// Token: 0x04000257 RID: 599
		private BattleResult _latestQueuedBattleResult;

		// Token: 0x04000258 RID: 600
		private Dictionary<int, int> _latestQueuedTeamScores;

		// Token: 0x04000259 RID: 601
		private Dictionary<PlayerId, int> _latestQueuedPlayerScores;

		// Token: 0x0200019C RID: 412
		public enum State
		{
			// Token: 0x0400056C RID: 1388
			Idle,
			// Token: 0x0400056D RID: 1389
			Working,
			// Token: 0x0400056E RID: 1390
			Connected,
			// Token: 0x0400056F RID: 1391
			SessionRequested,
			// Token: 0x04000570 RID: 1392
			RegisteredServer,
			// Token: 0x04000571 RID: 1393
			RegisteredGame,
			// Token: 0x04000572 RID: 1394
			Finished
		}
	}
}
