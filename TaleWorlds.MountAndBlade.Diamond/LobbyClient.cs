using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Messages.FromClient.ToLobbyServer;
using Messages.FromLobbyServer.ToClient;
using TaleWorlds.Core;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.ChatSystem.Library;
using TaleWorlds.Diamond.ClientApplication;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges;
using TaleWorlds.MountAndBlade.Diamond.Ranked;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012C RID: 300
	public class LobbyClient : Client<LobbyClient>, IChatClientHandler
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00008DCC File Offset: 0x00006FCC
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00008DD3 File Offset: 0x00006FD3
		private static int FriendListCheckDelay
		{
			get
			{
				return LobbyClient._friendListCheckDelay;
			}
			set
			{
				if (value != LobbyClient._friendListCheckDelay)
				{
					LobbyClient._friendListCheckDelay = value;
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00008DE3 File Offset: 0x00006FE3
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00008DEB File Offset: 0x00006FEB
		public PlayerData PlayerData { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00008DF4 File Offset: 0x00006FF4
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00008DFC File Offset: 0x00006FFC
		public SupportedFeatures SupportedFeatures { get; private set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00008E05 File Offset: 0x00007005
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00008E0D File Offset: 0x0000700D
		public ClanInfo ClanInfo { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00008E16 File Offset: 0x00007016
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x00008E1E File Offset: 0x0000701E
		public ClanHomeInfo ClanHomeInfo { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00008E27 File Offset: 0x00007027
		public IReadOnlyList<string> OwnedCosmetics
		{
			get
			{
				return this._ownedCosmetics;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00008E2F File Offset: 0x0000702F
		public IReadOnlyDictionary<string, List<string>> UsedCosmetics
		{
			get
			{
				return this._usedCosmetics;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00008E37 File Offset: 0x00007037
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00008E3F File Offset: 0x0000703F
		public AvailableScenes AvailableScenes { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00008E48 File Offset: 0x00007048
		public PlayerId PlayerID
		{
			get
			{
				return this._playerId;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00008E50 File Offset: 0x00007050
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00008E58 File Offset: 0x00007058
		public bool IsRefreshingPlayerData { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00008E61 File Offset: 0x00007061
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00008E6C File Offset: 0x0000706C
		public LobbyClient.State CurrentState
		{
			get
			{
				return this._state;
			}
			private set
			{
				if (this._state != value)
				{
					LobbyClient.State state = this._state;
					this._state = value;
					if (this._handler != null)
					{
						this._handler.OnGameClientStateChange(state);
					}
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00008EA4 File Offset: 0x000070A4
		public override long AliveCheckTimeInMiliSeconds
		{
			get
			{
				switch (this.CurrentState)
				{
				case LobbyClient.State.Idle:
				case LobbyClient.State.Working:
				case LobbyClient.State.Connected:
				case LobbyClient.State.SessionRequested:
				case LobbyClient.State.AtLobby:
					return 6000L;
				case LobbyClient.State.SearchingToRejoinBattle:
				case LobbyClient.State.RequestingToSearchBattle:
				case LobbyClient.State.RequestingToCancelSearchBattle:
				case LobbyClient.State.SearchingBattle:
				case LobbyClient.State.QuittingFromBattle:
				case LobbyClient.State.WaitingToRegisterCustomGame:
				case LobbyClient.State.HostingCustomGame:
				case LobbyClient.State.WaitingToJoinCustomGame:
					return 2000L;
				case LobbyClient.State.AtBattle:
				case LobbyClient.State.InCustomGame:
					return 60000L;
				}
				return 1000L;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00008F1F File Offset: 0x0000711F
		public bool AtLobby
		{
			get
			{
				return this.CurrentState == LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00008F2A File Offset: 0x0000712A
		public bool CanPerformLobbyActions
		{
			get
			{
				return this.CurrentState == LobbyClient.State.AtLobby || this.CurrentState == LobbyClient.State.RequestingToSearchBattle || this.CurrentState == LobbyClient.State.SearchingBattle || this.CurrentState == LobbyClient.State.WaitingToJoinCustomGame;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00008F53 File Offset: 0x00007153
		public string Name
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00008F5B File Offset: 0x0000715B
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00008F63 File Offset: 0x00007163
		public string LastBattleServerAddressForClient { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00008F6C File Offset: 0x0000716C
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00008F74 File Offset: 0x00007174
		public ushort LastBattleServerPortForClient { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00008F7D File Offset: 0x0000717D
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00008F85 File Offset: 0x00007185
		public bool LastBattleIsOfficial { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00008F8E File Offset: 0x0000718E
		public bool Connected
		{
			get
			{
				return this.CurrentState != LobbyClient.State.Working && this.CurrentState > LobbyClient.State.Idle;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00008FA4 File Offset: 0x000071A4
		public bool IsIdle
		{
			get
			{
				return this.CurrentState == LobbyClient.State.Idle;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00008FAF File Offset: 0x000071AF
		public void Logout(TextObject logOutReason)
		{
			base.BeginDisconnect();
			this.ChatManager.Cleanup();
			this._logOutReason = logOutReason;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00008FC9 File Offset: 0x000071C9
		public bool LoggedIn
		{
			get
			{
				return this.CurrentState != LobbyClient.State.Idle && this.CurrentState != LobbyClient.State.Working && this.CurrentState != LobbyClient.State.Connected && this.CurrentState != LobbyClient.State.SessionRequested;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00008FF3 File Offset: 0x000071F3
		public bool IsInGame
		{
			get
			{
				return this.CurrentState == LobbyClient.State.AtBattle || this.CurrentState == LobbyClient.State.HostingCustomGame || this.CurrentState == LobbyClient.State.InCustomGame;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00009015 File Offset: 0x00007215
		public bool IsHostingCustomGame
		{
			get
			{
				return this._state == LobbyClient.State.HostingCustomGame;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00009021 File Offset: 0x00007221
		public bool IsMatchmakingAvailable
		{
			get
			{
				ServerStatus serverStatus = this._serverStatus;
				return serverStatus != null && serverStatus.IsMatchmakingEnabled;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00009034 File Offset: 0x00007234
		public bool IsAbleToSearchForGame
		{
			get
			{
				return this.IsMatchmakingAvailable && this._matchmakerBlockedTime <= DateTime.Now;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00009050 File Offset: 0x00007250
		public bool PartySystemAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00009053 File Offset: 0x00007253
		public bool IsCustomBattleAvailable
		{
			get
			{
				ServerStatus serverStatus = this._serverStatus;
				return serverStatus != null && serverStatus.IsCustomBattleEnabled;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00009066 File Offset: 0x00007266
		public IReadOnlyList<ModuleInfoModel> LoadedUnofficialModules
		{
			get
			{
				return this._loadedUnofficialModules;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000906E File Offset: 0x0000726E
		public bool HasUnofficialModulesLoaded
		{
			get
			{
				return this.LoadedUnofficialModules.Count > 0;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000907E File Offset: 0x0000727E
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00009086 File Offset: 0x00007286
		public bool HasUserGeneratedContentPrivilege { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00009090 File Offset: 0x00007290
		public bool IsPartyLeader
		{
			get
			{
				if (this.Connected)
				{
					object objA = true;
					PartyPlayerInLobbyClient partyPlayerInLobbyClient = this.PlayersInParty.Find((PartyPlayerInLobbyClient p) => p.PlayerId == this._playerId);
					return object.Equals(objA, (partyPlayerInLobbyClient != null) ? new bool?(partyPlayerInLobbyClient.IsPartyLeader) : null);
				}
				return false;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000090E7 File Offset: 0x000072E7
		public bool IsClanLeader
		{
			get
			{
				ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == this._playerId);
				return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Leader;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000910E File Offset: 0x0000730E
		public bool IsClanOfficer
		{
			get
			{
				ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == this._playerId);
				return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Officer;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00009135 File Offset: 0x00007335
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0000913D File Offset: 0x0000733D
		public bool IsEligibleToCreatePremadeGame { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00009146 File Offset: 0x00007346
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0000914E File Offset: 0x0000734E
		public CustomBattleId CustomBattleId { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00009157 File Offset: 0x00007357
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0000915F File Offset: 0x0000735F
		public string CustomGameType { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00009168 File Offset: 0x00007368
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00009170 File Offset: 0x00007370
		public string CustomGameScene { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00009179 File Offset: 0x00007379
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00009181 File Offset: 0x00007381
		public AvailableCustomGames AvailableCustomGames { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000918A File Offset: 0x0000738A
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00009192 File Offset: 0x00007392
		public PremadeGameList AvailablePremadeGames { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000919B File Offset: 0x0000739B
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x000091A3 File Offset: 0x000073A3
		public List<PartyPlayerInLobbyClient> PlayersInParty { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000091AC File Offset: 0x000073AC
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x000091B4 File Offset: 0x000073B4
		public List<ClanPlayer> PlayersInClan { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000091BD File Offset: 0x000073BD
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x000091C5 File Offset: 0x000073C5
		public List<ClanPlayerInfo> PlayerInfosInClan { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000091CE File Offset: 0x000073CE
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x000091D6 File Offset: 0x000073D6
		public FriendInfo[] FriendInfos { get; private set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000091DF File Offset: 0x000073DF
		public bool IsInParty
		{
			get
			{
				return this.Connected && this.PlayersInParty.Count > 0;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000091F9 File Offset: 0x000073F9
		public bool IsPartyFull
		{
			get
			{
				return this.PlayersInParty.Count == Parameters.MaxPlayerCountInParty;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000920D File Offset: 0x0000740D
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00009215 File Offset: 0x00007415
		public string CurrentMatchId { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000921E File Offset: 0x0000741E
		public bool IsInClan
		{
			get
			{
				return this.PlayersInClan.Count > 0;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000922E File Offset: 0x0000742E
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00009236 File Offset: 0x00007436
		public bool IsPartyInvitationPopupActive { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000923F File Offset: 0x0000743F
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00009247 File Offset: 0x00007447
		public bool IsPartyJoinRequestPopupActive { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00009250 File Offset: 0x00007450
		public bool CanInvitePlayers
		{
			get
			{
				SupportedFeatures supportedFeatures = this.SupportedFeatures;
				return supportedFeatures != null && supportedFeatures.SupportsFeatures(Features.Party) && (!this.IsInParty || this.IsPartyLeader);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00009279 File Offset: 0x00007479
		public bool CanSuggestPlayers
		{
			get
			{
				SupportedFeatures supportedFeatures = this.SupportedFeatures;
				return supportedFeatures != null && supportedFeatures.SupportsFeatures(Features.Party) && this.IsInParty && !this.IsPartyLeader;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x000092A3 File Offset: 0x000074A3
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x000092AB File Offset: 0x000074AB
		public Guid ClanID { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x000092B4 File Offset: 0x000074B4
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x000092BC File Offset: 0x000074BC
		public List<PlayerId> FriendIDs { get; private set; }

		// Token: 0x060006FE RID: 1790 RVA: 0x000092C8 File Offset: 0x000074C8
		public LobbyClient(DiamondClientApplication diamondClientApplication, IClientSessionProvider<LobbyClient> sessionProvider) : base(diamondClientApplication, sessionProvider, false)
		{
			this._serverStatusTimer = new Stopwatch();
			this._serverStatusTimer.Start();
			this._matchmakerBlockedTime = DateTime.MinValue;
			this._friendListTimer = new Stopwatch();
			this._friendListTimer.Start();
			this.PlayersInParty = new List<PartyPlayerInLobbyClient>();
			this.PlayersInClan = new List<ClanPlayer>();
			this.PlayerInfosInClan = new List<ClanPlayerInfo>();
			this.FriendInfos = new FriendInfo[0];
			this.ChatManager = new ChatManager(this);
			this.ClanID = Guid.Empty;
			this.FriendIDs = new List<PlayerId>();
			this.SupportedFeatures = new SupportedFeatures();
			this._ownedCosmetics = new List<string>();
			this._usedCosmetics = new Dictionary<string, List<string>>();
			this._cachedRankInfos = new TimedDictionaryCache<PlayerId, GameTypeRankInfo[]>(TimeSpan.FromSeconds(10.0));
			this._cachedPlayerStats = new TimedDictionaryCache<PlayerId, PlayerStatsBase[]>(TimeSpan.FromSeconds(10.0));
			this._cachedPlayerDatas = new TimedDictionaryCache<PlayerId, PlayerData>(TimeSpan.FromSeconds(10.0));
			this._cachedPlayerBannerlordIDs = new TimedDictionaryCache<PlayerId, string>(TimeSpan.FromSeconds(30.0));
			this._pendingPlayerRequests = new Dictionary<ValueTuple<LobbyClient.PendingRequest, PlayerId>, Task>();
			base.AddMessageHandler<FindGameAnswerMessage>(new ClientMessageHandler<FindGameAnswerMessage>(this.OnFindGameAnswerMessage));
			base.AddMessageHandler<JoinBattleMessage>(new ClientMessageHandler<JoinBattleMessage>(this.OnJoinBattleMessage));
			base.AddMessageHandler<BattleResultMessage>(new ClientMessageHandler<BattleResultMessage>(this.OnBattleResultMessage));
			base.AddMessageHandler<BattleServerLostMessage>(new ClientMessageHandler<BattleServerLostMessage>(this.OnBattleServerLostMessage));
			base.AddMessageHandler<BattleOverMessage>(new ClientMessageHandler<BattleOverMessage>(this.OnBattleOverMessage));
			base.AddMessageHandler<CancelBattleResponseMessage>(new ClientMessageHandler<CancelBattleResponseMessage>(this.OnCancelBattleResponseMessage));
			base.AddMessageHandler<RejoinRequestRejectedMessage>(new ClientMessageHandler<RejoinRequestRejectedMessage>(this.OnRejoinRequestRejectedMessage));
			base.AddMessageHandler<CancelFindGameMessage>(new ClientMessageHandler<CancelFindGameMessage>(this.OnCancelFindGameMessage));
			base.AddMessageHandler<RequestJoinPartyMessage>(new ClientMessageHandler<RequestJoinPartyMessage>(this.OnRequestJoinPartyMessage));
			base.AddMessageHandler<WhisperReceivedMessage>(new ClientMessageHandler<WhisperReceivedMessage>(this.OnWhisperMessageReceivedMessage));
			base.AddMessageHandler<ClanMessageReceivedMessage>(new ClientMessageHandler<ClanMessageReceivedMessage>(this.OnClanMessageReceivedMessage));
			base.AddMessageHandler<ChannelMessageReceivedMessage>(new ClientMessageHandler<ChannelMessageReceivedMessage>(this.OnChannelMessageReceivedMessage));
			base.AddMessageHandler<PartyMessageReceivedMessage>(new ClientMessageHandler<PartyMessageReceivedMessage>(this.OnPartyMessageReceivedMessage));
			base.AddMessageHandler<SystemMessage>(new ClientMessageHandler<SystemMessage>(this.OnSystemMessage));
			base.AddMessageHandler<InvitationToPartyMessage>(new ClientMessageHandler<InvitationToPartyMessage>(this.OnInvitationToPartyMessage));
			base.AddMessageHandler<PartyInvitationInvalidMessage>(new ClientMessageHandler<PartyInvitationInvalidMessage>(this.OnPartyInvitationInvalidMessage));
			base.AddMessageHandler<UpdatePlayerDataMessage>(new ClientMessageHandler<UpdatePlayerDataMessage>(this.OnUpdatePlayerDataMessage));
			base.AddMessageHandler<RecentPlayerStatusesMessage>(new ClientMessageHandler<RecentPlayerStatusesMessage>(this.OnRecentPlayerStatusesMessage));
			base.AddMessageHandler<PlayerQuitFromMatchmakerGameResult>(new ClientMessageHandler<PlayerQuitFromMatchmakerGameResult>(this.OnPlayerQuitFromMatchmakerGameResult));
			base.AddMessageHandler<PlayerRemovedFromMatchmakerGame>(new ClientMessageHandler<PlayerRemovedFromMatchmakerGame>(this.OnPlayerRemovedFromMatchmakerGameMessage));
			base.AddMessageHandler<EnterBattleWithPartyAnswer>(new ClientMessageHandler<EnterBattleWithPartyAnswer>(this.OnEnterBattleWithPartyAnswerMessage));
			base.AddMessageHandler<JoinCustomGameResultMessage>(new ClientMessageHandler<JoinCustomGameResultMessage>(this.OnJoinCustomGameResultMessage));
			base.AddMessageHandler<ClientWantsToConnectCustomGameMessage>(new ClientMessageHandler<ClientWantsToConnectCustomGameMessage>(this.OnClientWantsToConnectCustomGameMessage));
			base.AddMessageHandler<ClientQuitFromCustomGameMessage>(new ClientMessageHandler<ClientQuitFromCustomGameMessage>(this.OnClientQuitFromCustomGameMessage));
			base.AddMessageHandler<PlayerRemovedFromCustomGame>(new ClientMessageHandler<PlayerRemovedFromCustomGame>(this.OnPlayerRemovedFromCustomGame));
			base.AddMessageHandler<EnterCustomBattleWithPartyAnswer>(new ClientMessageHandler<EnterCustomBattleWithPartyAnswer>(this.OnEnterCustomBattleWithPartyAnswerMessage));
			base.AddMessageHandler<PlayerInvitedToPartyMessage>(new ClientMessageHandler<PlayerInvitedToPartyMessage>(this.OnPlayerInvitedToPartyMessage));
			base.AddMessageHandler<PlayersAddedToPartyMessage>(new ClientMessageHandler<PlayersAddedToPartyMessage>(this.OnPlayerAddedToPartyMessage));
			base.AddMessageHandler<PlayerRemovedFromPartyMessage>(new ClientMessageHandler<PlayerRemovedFromPartyMessage>(this.OnPlayerRemovedFromPartyMessage));
			base.AddMessageHandler<PlayerAssignedPartyLeaderMessage>(new ClientMessageHandler<PlayerAssignedPartyLeaderMessage>(this.OnPlayerAssignedPartyLeaderMessage));
			base.AddMessageHandler<PlayerSuggestedToPartyMessage>(new ClientMessageHandler<PlayerSuggestedToPartyMessage>(this.OnPlayerSuggestedToPartyMessage));
			base.AddMessageHandler<ServerStatusMessage>(new ClientMessageHandler<ServerStatusMessage>(this.OnServerStatusMessage));
			base.AddMessageHandler<MatchmakerDisabledMessage>(new ClientMessageHandler<MatchmakerDisabledMessage>(this.OnMatchmakerDisabledMessage));
			base.AddMessageHandler<FriendListMessage>(new ClientMessageHandler<FriendListMessage>(this.OnFriendListMessage));
			base.AddMessageHandler<AdminMessage>(new ClientMessageHandler<AdminMessage>(this.OnAdminMessage));
			base.AddMessageHandler<CreateClanAnswerMessage>(new ClientMessageHandler<CreateClanAnswerMessage>(this.OnCreateClanAnswerMessage));
			base.AddMessageHandler<ClanCreationRequestMessage>(new ClientMessageHandler<ClanCreationRequestMessage>(this.OnClanCreationRequestMessage));
			base.AddMessageHandler<ClanCreationRequestAnsweredMessage>(new ClientMessageHandler<ClanCreationRequestAnsweredMessage>(this.OnClanCreationRequestAnsweredMessage));
			base.AddMessageHandler<ClanCreationFailedMessage>(new ClientMessageHandler<ClanCreationFailedMessage>(this.OnClanCreationFailedMessage));
			base.AddMessageHandler<ClanCreationSuccessfulMessage>(new ClientMessageHandler<ClanCreationSuccessfulMessage>(this.OnClanCreationSuccessfulMessage));
			base.AddMessageHandler<ClanInfoChangedMessage>(new ClientMessageHandler<ClanInfoChangedMessage>(this.OnClanInfoChangedMessage));
			base.AddMessageHandler<InvitationToClanMessage>(new ClientMessageHandler<InvitationToClanMessage>(this.OnInvitationToClanMessage));
			base.AddMessageHandler<ClanDisbandedMessage>(new ClientMessageHandler<ClanDisbandedMessage>(this.OnClanDisbandedMessage));
			base.AddMessageHandler<KickedFromClanMessage>(new ClientMessageHandler<KickedFromClanMessage>(this.OnKickedFromClan));
			base.AddMessageHandler<JoinPremadeGameAnswerMessage>(new ClientMessageHandler<JoinPremadeGameAnswerMessage>(this.OnJoinPremadeGameAnswerMessage));
			base.AddMessageHandler<PremadeGameEligibilityStatusMessage>(new ClientMessageHandler<PremadeGameEligibilityStatusMessage>(this.OnPremadeGameEligibilityStatusMessage));
			base.AddMessageHandler<CreatePremadeGameAnswerMessage>(new ClientMessageHandler<CreatePremadeGameAnswerMessage>(this.OnCreatePremadeGameAnswerMessage));
			base.AddMessageHandler<JoinPremadeGameRequestMessage>(new ClientMessageHandler<JoinPremadeGameRequestMessage>(this.OnJoinPremadeGameRequestMessage));
			base.AddMessageHandler<JoinPremadeGameRequestResultMessage>(new ClientMessageHandler<JoinPremadeGameRequestResultMessage>(this.OnJoinPremadeGameRequestResultMessage));
			base.AddMessageHandler<ClanGameCreationCancelledMessage>(new ClientMessageHandler<ClanGameCreationCancelledMessage>(this.OnClanGameCreationCancelledMessage));
			base.AddMessageHandler<JoinChatRoomMessage>(new ClientMessageHandler<JoinChatRoomMessage>(this.OnJoinChatRoomMessage));
			base.AddMessageHandler<ChatRoomClosedMessage>(new ClientMessageHandler<ChatRoomClosedMessage>(this.OnChatRoomClosedMessage));
			base.AddMessageHandler<SigilChangeAnswerMessage>(new ClientMessageHandler<SigilChangeAnswerMessage>(this.OnSigilChangeAnswerMessage));
			base.AddMessageHandler<LobbyNotificationsMessage>(new ClientMessageHandler<LobbyNotificationsMessage>(this.OnLobbyNotificationsMessage));
			base.AddMessageHandler<CustomBattleOverMessage>(new ClientMessageHandler<CustomBattleOverMessage>(this.OnCustomBattleOverMessage));
			base.AddMessageHandler<RejoinBattleRequestAnswerMessage>(new ClientMessageHandler<RejoinBattleRequestAnswerMessage>(this.OnRejoinBattleRequestAnswerMessage));
			base.AddMessageHandler<PendingBattleRejoinMessage>(new ClientMessageHandler<PendingBattleRejoinMessage>(this.OnPendingBattleRejoinMessage));
			base.AddMessageHandler<ShowAnnouncementMessage>(new ClientMessageHandler<ShowAnnouncementMessage>(this.OnShowAnnouncementMessage));
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00009808 File Offset: 0x00007A08
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00009810 File Offset: 0x00007A10
		public ChatManager ChatManager { get; private set; }

		// Token: 0x06000701 RID: 1793 RVA: 0x0000981C File Offset: 0x00007A1C
		public void SetLoadedModules(string[] moduleIDs)
		{
			if (this._loadedUnofficialModules == null)
			{
				this._loadedUnofficialModules = new List<ModuleInfoModel>();
				using (List<ModuleInfo>.Enumerator enumerator = ModuleHelper.GetSortedModules(moduleIDs).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ModuleInfoModel item;
						if (ModuleInfoModel.TryCreateForSession(enumerator.Current, out item))
						{
							this._loadedUnofficialModules.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00009890 File Offset: 0x00007A90
		public async Task<AvailableCustomGames> GetCustomGameServerList()
		{
			this.AssertCanPerformLobbyActions();
			CustomGameServerListResponse customGameServerListResponse = await base.CallFunction<CustomGameServerListResponse>(new RequestCustomGameServerListMessage());
			Debug.Print("Custom game server list received", 0, Debug.DebugColor.White, 17592186044416UL);
			AvailableCustomGames result;
			if (customGameServerListResponse != null)
			{
				this.AvailableCustomGames = customGameServerListResponse.AvailableCustomGames;
				if (this._handler != null)
				{
					this._handler.OnCustomGameServerListReceived(this.AvailableCustomGames);
				}
				result = this.AvailableCustomGames;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000098D5 File Offset: 0x00007AD5
		public void QuitFromCustomGame()
		{
			base.SendMessage(new QuitFromCustomGameMessage());
			this.CurrentState = LobbyClient.State.AtLobby;
			if (this._handler != null)
			{
				this._handler.OnQuitFromCustomGame();
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000098FC File Offset: 0x00007AFC
		public void QuitFromMatchmakerGame()
		{
			if (this.CurrentState == LobbyClient.State.AtBattle)
			{
				this.CheckAndSendMessage(new QuitFromMatchmakerGameMessage());
				this.CurrentState = LobbyClient.State.QuittingFromBattle;
				if (this._handler != null)
				{
					this._handler.OnQuitFromMatchmakerGame();
				}
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00009930 File Offset: 0x00007B30
		public async Task<bool> RequestJoinCustomGame(CustomBattleId serverId, string password, bool isJoinAsAdmin = false)
		{
			this.CurrentState = LobbyClient.State.WaitingToJoinCustomGame;
			this.CustomBattleId = serverId;
			string password2 = (!string.IsNullOrEmpty(password)) ? Common.CalculateMD5Hash(password) : null;
			base.SendMessage(new RequestJoinCustomGameMessage(serverId, password2, isJoinAsAdmin));
			while (this.CurrentState == LobbyClient.State.WaitingToJoinCustomGame)
			{
				await Task.Yield();
			}
			bool result;
			if (this.CurrentState == LobbyClient.State.InCustomGame)
			{
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00009990 File Offset: 0x00007B90
		public async Task<bool> RequestJoinPlayerParty(PlayerId targetPlayer, bool inviteRequest)
		{
			this.AssertCanPerformLobbyActions();
			RequestJoinPlayerPartyMessageResult requestJoinPlayerPartyMessageResult = await base.CallFunction<RequestJoinPlayerPartyMessageResult>(new RequestJoinPlayerPartyMessage(targetPlayer, inviteRequest));
			bool result;
			if (requestJoinPlayerPartyMessageResult != null)
			{
				result = requestJoinPlayerPartyMessageResult.Success;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000099E5 File Offset: 0x00007BE5
		public void CancelFindGame()
		{
			this.CurrentState = LobbyClient.State.RequestingToCancelSearchBattle;
			this.CheckAndSendMessage(new CancelBattleRequestMessage());
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000099F9 File Offset: 0x00007BF9
		public void FindGame()
		{
			this.CurrentState = LobbyClient.State.RequestingToSearchBattle;
			this.CheckAndSendMessage(new FindGameMessage());
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00009A10 File Offset: 0x00007C10
		public async Task<bool> FindCustomGame(string[] selectedCustomGameTypes, bool? hasCrossplayPrivilege, string region)
		{
			this.CurrentState = LobbyClient.State.WaitingToJoinCustomGame;
			int i = 0;
			while (i < LobbyClient.CheckForCustomGamesCount)
			{
				CustomGameServerListResponse customGameServerListResponse = await base.CallFunction<CustomGameServerListResponse>(new RequestCustomGameServerListMessage());
				if (customGameServerListResponse != null && customGameServerListResponse.AvailableCustomGames.CustomGameServerInfos.Count > 0)
				{
					List<GameServerEntry> list = (from c in customGameServerListResponse.AvailableCustomGames.CustomGameServerInfos
					orderby c.PlayerCount descending
					select c).ToList<GameServerEntry>();
					bool? flag = hasCrossplayPrivilege;
					bool flag2 = true;
					GameServerEntry.FilterGameServerEntriesBasedOnCrossplay(ref list, flag.GetValueOrDefault() == flag2 & flag != null);
					foreach (string b in selectedCustomGameTypes)
					{
						foreach (GameServerEntry gameServerEntry in list)
						{
							if (gameServerEntry.IsOfficial && gameServerEntry.GameType == b && gameServerEntry.Region == region && !gameServerEntry.PasswordProtected && gameServerEntry.MaxPlayerCount >= gameServerEntry.PlayerCount + this.PlayersInParty.Count)
							{
								base.SendMessage(new RequestJoinCustomGameMessage(gameServerEntry.Id, "", false));
								while (this.CurrentState == LobbyClient.State.WaitingToJoinCustomGame)
								{
									await Task.Yield();
								}
								if (this.CurrentState == LobbyClient.State.InCustomGame)
								{
									return true;
								}
								return false;
							}
						}
						List<GameServerEntry>.Enumerator enumerator = default(List<GameServerEntry>.Enumerator);
					}
					await Task.Delay(LobbyClient.CheckForCustomGamesDelay);
				}
				int j = i++;
			}
			this.CurrentState = LobbyClient.State.AtLobby;
			return false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00009A70 File Offset: 0x00007C70
		public async Task<LobbyClientConnectResult> Connect(ILobbyClientSessionHandler lobbyClientSessionHandler, ILoginAccessProvider lobbyClientLoginAccessProvider, string overridenUserName, bool hasUserGeneratedContentPrivilege, PlatformInitParams initParams)
		{
			base.AccessProvider = lobbyClientLoginAccessProvider;
			base.AccessProvider.Initialize(overridenUserName, initParams);
			this._handler = lobbyClientSessionHandler;
			this.CurrentState = LobbyClient.State.Working;
			this.HasUserGeneratedContentPrivilege = hasUserGeneratedContentPrivilege;
			base.BeginConnect();
			while (this.CurrentState == LobbyClient.State.Working)
			{
				await Task.Yield();
			}
			LobbyClientConnectResult result;
			if (this.CurrentState == LobbyClient.State.Connected)
			{
				AccessObjectResult accessObjectResult = AccessObjectResult.CreateFailed(new TextObject("{=gAeQdLU5}Failed to acquire access data from platform", null));
				Task getAccessObjectTask = Task.Run(delegate()
				{
					accessObjectResult = this.AccessProvider.CreateAccessObject();
				});
				while (!getAccessObjectTask.IsCompleted)
				{
					await Task.Yield();
				}
				if (getAccessObjectTask.IsFaulted)
				{
					throw getAccessObjectTask.Exception ?? new Exception("Get access object task faulted without exception");
				}
				if (getAccessObjectTask.IsCanceled)
				{
					throw new Exception("Get access object task canceled");
				}
				if (accessObjectResult.Success)
				{
					this._userName = base.AccessProvider.GetUserName();
					this._playerId = base.AccessProvider.GetPlayerId();
					this.CurrentState = LobbyClient.State.SessionRequested;
					string environmentVariable = Environment.GetEnvironmentVariable("Bannerlord.ConnectionPassword");
					LoginResult loginResult = await base.Login(new InitializeSession(this._playerId, this._userName, accessObjectResult.AccessObject, base.Application.ApplicationVersion, environmentVariable, this._loadedUnofficialModules.ToArray()));
					if (loginResult != null)
					{
						if (loginResult.Successful)
						{
							InitializeSessionResponse initializeSessionResponse = (InitializeSessionResponse)loginResult.LoginResultObject;
							this.PlayerData = initializeSessionResponse.PlayerData;
							this._serverStatus = initializeSessionResponse.ServerStatus;
							this.SupportedFeatures = initializeSessionResponse.SupportedFeatures;
							this.AvailableScenes = initializeSessionResponse.AvailableScenes;
							this._logOutReason = new TextObject("{=i4MNr0bo}Disconnected from the Lobby.", null);
							await PermaMuteList.LoadMutedPlayers(this.PlayerData.PlayerId);
							this._ownedCosmetics.Clear();
							this._usedCosmetics.Clear();
							this._handler.OnPlayerDataReceived(this.PlayerData);
							this._handler.OnServerStatusReceived(initializeSessionResponse.ServerStatus);
							LobbyClient.FriendListCheckDelay = this._serverStatus.FriendListUpdatePeriod * 1000;
							if (initializeSessionResponse.HasPendingRejoin)
							{
								this._handler.OnPendingRejoin();
							}
							this.CurrentState = LobbyClient.State.AtLobby;
							result = new LobbyClientConnectResult(true, null);
						}
						else
						{
							base.BeginDisconnect();
							result = LobbyClientConnectResult.FromServerConnectResult(loginResult.ErrorCode, loginResult.ErrorParameters);
						}
					}
					else
					{
						base.BeginDisconnect();
						result = new LobbyClientConnectResult(false, new TextObject("{=63X8LERm}Couldn't receive login result from server.", null));
					}
				}
				else
				{
					base.BeginDisconnect();
					result = new LobbyClientConnectResult(false, accessObjectResult.FailReason ?? new TextObject("{=JO37PkfW}Your platform service is not initialized.", null));
				}
			}
			else
			{
				result = new LobbyClientConnectResult(false, new TextObject("{=3cWg0cWt}Could not connect to server.", null));
			}
			return result;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00009ADF File Offset: 0x00007CDF
		public void KickPlayer(PlayerId id, bool banPlayer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00009AE6 File Offset: 0x00007CE6
		public void ChangeRegion(string region)
		{
			if (this.PlayerData == null || this.PlayerData.LastRegion != region)
			{
				this.CheckAndSendMessage(new ChangeRegionMessage(region));
			}
			if (this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.PlayerData.LastRegion = region;
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00009B24 File Offset: 0x00007D24
		public void ChangeGameTypes(string[] gameTypes)
		{
			bool flag = this.PlayerData == null || this.PlayerData.LastGameTypes.Length != gameTypes.Length;
			if (!flag)
			{
				foreach (string value in gameTypes)
				{
					if (!this.PlayerData.LastGameTypes.Contains(value))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.CheckAndSendMessage(new ChangeGameTypesMessage(gameTypes));
			}
			if (this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.PlayerData.LastGameTypes = gameTypes;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00009BA4 File Offset: 0x00007DA4
		private void CheckAndSendMessage(Message message)
		{
			base.SendMessage(message);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00009BAD File Offset: 0x00007DAD
		public override void OnConnected()
		{
			base.OnConnected();
			this.CurrentState = LobbyClient.State.Connected;
			if (this._handler != null)
			{
				this._handler.OnConnected();
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00009BCF File Offset: 0x00007DCF
		public override void OnCantConnect()
		{
			base.OnCantConnect();
			this.CurrentState = LobbyClient.State.Idle;
			if (this._handler != null)
			{
				this._handler.OnCantConnect();
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			bool loggedIn = this.LoggedIn;
			this.CurrentState = LobbyClient.State.Idle;
			this.PlayerData = null;
			this.PlayersInParty.Clear();
			this.PlayersInClan.Clear();
			this._matchmakerBlockedTime = DateTime.MinValue;
			this.FriendInfos = new FriendInfo[0];
			PermaMuteList.SaveMutedPlayers();
			this._ownedCosmetics.Clear();
			this._usedCosmetics.Clear();
			if (this._handler != null)
			{
				this._handler.OnDisconnected(loggedIn ? this._logOutReason : null);
			}
			this._handler = null;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00009C8A File Offset: 0x00007E8A
		private void OnFindGameAnswerMessage(FindGameAnswerMessage message)
		{
			if (!message.Successful)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
			else
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
			this._handler.OnFindGameAnswer(message.Successful, message.SelectedAndEnabledGameTypes, false);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00009CBC File Offset: 0x00007EBC
		private void OnJoinBattleMessage(JoinBattleMessage message)
		{
			BattleServerInformationForClient battleServerInformation = message.BattleServerInformation;
			string serverAddress;
			if (base.Application.ProxyAddressMap.TryGetValue(battleServerInformation.ServerAddress, out serverAddress))
			{
				battleServerInformation.ServerAddress = serverAddress;
			}
			this.LastBattleServerAddressForClient = battleServerInformation.ServerAddress;
			this.LastBattleServerPortForClient = battleServerInformation.ServerPort;
			this.CurrentMatchId = battleServerInformation.MatchId;
			this.LastBattleIsOfficial = true;
			string text = "Successful matchmaker game join response\n";
			text = text + "Address: " + this.LastBattleServerAddressForClient + "\n";
			text = string.Concat(new object[]
			{
				text,
				"Port: ",
				this.LastBattleServerPortForClient,
				"\n"
			});
			text = text + "Match Id: " + this.CurrentMatchId + "\n";
			Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
			this._handler.OnBattleServerInformationReceived(battleServerInformation);
			this.CurrentState = LobbyClient.State.AtBattle;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00009DAC File Offset: 0x00007FAC
		private void OnBattleOverMessage(BattleOverMessage message)
		{
			if (this.CurrentState == LobbyClient.State.AtBattle || this.CurrentState == LobbyClient.State.QuittingFromBattle || this.CurrentState == LobbyClient.State.AtLobby)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
				this._handler.OnMatchmakerGameOver(message.OldExperience, message.NewExperience, message.EarnedBadges, message.GoldGained, message.OldInfo, message.NewInfo, message.BattleCancelReason);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00009E12 File Offset: 0x00008012
		private void OnJoinChatRoomMessage(JoinChatRoomMessage message)
		{
			this.ChatManager.OnJoinChatRoom(message.ChatRoomInformaton, this.PlayerData.PlayerId, this.PlayerData.LastPlayerName);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00009E3B File Offset: 0x0000803B
		private void OnChatRoomClosedMessage(ChatRoomClosedMessage message)
		{
			this.ChatManager.OnChatRoomClosed(message.ChatRoomId);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00009E4E File Offset: 0x0000804E
		private void OnBattleResultMessage(BattleResultMessage message)
		{
			this._handler.OnBattleResultReceived();
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00009E5B File Offset: 0x0000805B
		private void OnBattleServerLostMessage(BattleServerLostMessage message)
		{
			if (this.CurrentState == LobbyClient.State.AtBattle || this.CurrentState == LobbyClient.State.SearchingToRejoinBattle)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
			this._handler.OnBattleServerLost();
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00009E82 File Offset: 0x00008082
		private void OnCancelBattleResponseMessage(CancelBattleResponseMessage message)
		{
			if (message.Successful)
			{
				this._handler.OnCancelJoiningBattle();
				this.CurrentState = LobbyClient.State.AtLobby;
				return;
			}
			if (this.CurrentState == LobbyClient.State.RequestingToCancelSearchBattle)
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00009EAF File Offset: 0x000080AF
		private void OnRejoinRequestRejectedMessage(RejoinRequestRejectedMessage message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			this._handler.OnRejoinRequestRejected();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00009EC3 File Offset: 0x000080C3
		private void OnCancelFindGameMessage(CancelFindGameMessage message)
		{
			if (this.CurrentState == LobbyClient.State.SearchingBattle)
			{
				this.CancelFindGame();
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00009ED4 File Offset: 0x000080D4
		private void OnWhisperMessageReceivedMessage(WhisperReceivedMessage message)
		{
			this._handler.OnWhisperMessageReceived(message.FromPlayer, message.ToPlayer, message.Message);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00009EF3 File Offset: 0x000080F3
		private void OnClanMessageReceivedMessage(ClanMessageReceivedMessage message)
		{
			this._handler.OnClanMessageReceived(message.PlayerName, message.Message);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00009F0C File Offset: 0x0000810C
		private void OnChannelMessageReceivedMessage(ChannelMessageReceivedMessage message)
		{
			this._handler.OnChannelMessageReceived(message.Channel, message.PlayerName, message.Message);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00009F2B File Offset: 0x0000812B
		private void OnPartyMessageReceivedMessage(PartyMessageReceivedMessage message)
		{
			this._handler.OnPartyMessageReceived(message.PlayerName, message.Message);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00009F44 File Offset: 0x00008144
		private void OnPlayerQuitFromMatchmakerGameResult(PlayerQuitFromMatchmakerGameResult message)
		{
			if (this.CurrentState == LobbyClient.State.QuittingFromBattle)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00009F58 File Offset: 0x00008158
		private void OnEnterBattleWithPartyAnswerMessage(EnterBattleWithPartyAnswer message)
		{
			if (message.Successful)
			{
				if (this.CurrentState == LobbyClient.State.AtLobby || this.CurrentState == LobbyClient.State.RequestingToSearchBattle)
				{
					this.CurrentState = LobbyClient.State.SearchingBattle;
				}
				else if (this.CurrentState != LobbyClient.State.SearchingBattle)
				{
					LobbyClient.State currentState = this.CurrentState;
				}
				this._handler.OnEnterBattleWithPartyAnswer(message.SelectedAndEnabledGameTypes);
				return;
			}
			this.CurrentState = LobbyClient.State.AtLobby;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00009FB4 File Offset: 0x000081B4
		private void OnJoinCustomGameResultMessage(JoinCustomGameResultMessage message)
		{
			if (!message.Success && message.Response == CustomGameJoinResponse.AlreadyRequestedWaitingForServerResponse)
			{
				if (this._handler != null)
				{
					this._handler.OnSystemMessageReceived(new TextObject("{=ivKntfNA}Already requested to join, waiting for server response", null).ToString());
					return;
				}
			}
			else if (message.Success)
			{
				message.JoinGameData.GameServerProperties.CheckAndReplaceProxyAddress(base.Application.ProxyAddressMap);
				this.CurrentState = LobbyClient.State.InCustomGame;
				this.LastBattleServerAddressForClient = message.JoinGameData.GameServerProperties.Address;
				this.LastBattleServerPortForClient = (ushort)message.JoinGameData.GameServerProperties.Port;
				this.LastBattleIsOfficial = message.JoinGameData.GameServerProperties.IsOfficial;
				this.CurrentMatchId = message.MatchId;
				string text = "Successful custom game join response\n";
				text = text + "Server Name: " + message.JoinGameData.GameServerProperties.Name + "\n";
				text = text + "Host Name: " + message.JoinGameData.GameServerProperties.HostName + "\n";
				text = text + "Address: " + this.LastBattleServerAddressForClient + "\n";
				text = string.Concat(new object[]
				{
					text,
					"Port: ",
					this.LastBattleServerPortForClient,
					"\n"
				});
				text = text + "Match Id: " + this.CurrentMatchId + "\n";
				text = text + "Is Official: " + message.JoinGameData.GameServerProperties.IsOfficial.ToString() + "\n";
				Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
				if (this._handler != null)
				{
					this._handler.OnJoinCustomGameResponse(message.Success, message.JoinGameData, message.Response, message.IsAdmin);
					return;
				}
			}
			else
			{
				this.CurrentState = LobbyClient.State.AtLobby;
				if (this._handler != null)
				{
					this._handler.OnJoinCustomGameFailureResponse(message.Response);
				}
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000A1A4 File Offset: 0x000083A4
		private void OnClientWantsToConnectCustomGameMessage(ClientWantsToConnectCustomGameMessage message)
		{
			this.AssertCanPerformLobbyActions();
			List<PlayerJoinGameResponseDataFromHost> list = new List<PlayerJoinGameResponseDataFromHost>();
			PlayerJoinGameData[] playerJoinGameData = message.PlayerJoinGameData;
			for (int i = 0; i < playerJoinGameData.Length; i++)
			{
				if (playerJoinGameData[i] != null)
				{
					List<PlayerJoinGameData> list2 = new List<PlayerJoinGameData>();
					PlayerJoinGameData playerJoinGameData2 = playerJoinGameData[i];
					Guid? partyId = playerJoinGameData2.PartyId;
					if (partyId == null)
					{
						list2.Add(playerJoinGameData2);
					}
					else
					{
						for (int j = i; j < playerJoinGameData.Length; j++)
						{
							PlayerJoinGameData playerJoinGameData3 = playerJoinGameData[j];
							partyId = playerJoinGameData2.PartyId;
							if (partyId.Equals((playerJoinGameData3 != null) ? playerJoinGameData3.PartyId : null))
							{
								list2.Add(playerJoinGameData3);
								playerJoinGameData[j] = null;
							}
						}
					}
					if (this._handler != null)
					{
						PlayerJoinGameResponseDataFromHost[] collection = this._handler.OnClientWantsToConnectCustomGame(list2.ToArray());
						list.AddRange(collection);
					}
				}
			}
			this.ResponseCustomGameClientConnection(list.ToArray());
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000A28F File Offset: 0x0000848F
		private void OnClientQuitFromCustomGameMessage(ClientQuitFromCustomGameMessage message)
		{
			if (this._handler != null)
			{
				this._handler.OnClientQuitFromCustomGame(message.PlayerId);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000A2AA File Offset: 0x000084AA
		private void OnEnterCustomBattleWithPartyAnswerMessage(EnterCustomBattleWithPartyAnswer message)
		{
			if (message.Successful)
			{
				if (this.CurrentState == LobbyClient.State.AtLobby)
				{
					this.CurrentState = LobbyClient.State.WaitingToJoinCustomGame;
				}
				this._handler.OnEnterCustomBattleWithPartyAnswer();
				return;
			}
			this.CurrentState = LobbyClient.State.AtLobby;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000A2D8 File Offset: 0x000084D8
		private void OnPlayerRemovedFromMatchmakerGameMessage(PlayerRemovedFromMatchmakerGame message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			if (this._handler != null)
			{
				this._handler.OnRemovedFromMatchmakerGame(message.DisconnectType);
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000A2FA File Offset: 0x000084FA
		private void OnPlayerRemovedFromCustomGame(PlayerRemovedFromCustomGame message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			if (this._handler != null)
			{
				this._handler.OnRemovedFromCustomGame(message.DisconnectType);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000A31C File Offset: 0x0000851C
		private void OnSystemMessage(SystemMessage message)
		{
			this._handler.OnSystemMessageReceived(message.GetDescription().ToString());
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000A334 File Offset: 0x00008534
		private void OnAdminMessage(AdminMessage message)
		{
			this._handler.OnAdminMessageReceived(message.Message);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000A347 File Offset: 0x00008547
		private void OnInvitationToPartyMessage(InvitationToPartyMessage message)
		{
			this.IsPartyInvitationPopupActive = true;
			this._handler.OnPartyInvitationReceived(message.InviterPlayerName, message.InviterPlayerId);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000A367 File Offset: 0x00008567
		private void OnPartyInvitationInvalidMessage(PartyInvitationInvalidMessage message)
		{
			this.IsPartyInvitationPopupActive = false;
			this._handler.OnPartyInvitationInvalidated();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000A37B File Offset: 0x0000857B
		private void OnRequestJoinPartyMessage(RequestJoinPartyMessage message)
		{
			this.IsPartyJoinRequestPopupActive = true;
			this._handler.OnPartyJoinRequestReceived(message.PlayerId, message.ViaPlayerId, message.ViaPlayerName);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000A3A1 File Offset: 0x000085A1
		private void OnPlayerInvitedToPartyMessage(PlayerInvitedToPartyMessage message)
		{
			this.PlayersInParty.Add(new PartyPlayerInLobbyClient(message.PlayerId, message.PlayerName, false));
			this._handler.OnPlayerInvitedToParty(message.PlayerId);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000A3D4 File Offset: 0x000085D4
		private void OnPlayerAddedToPartyMessage(PlayersAddedToPartyMessage message)
		{
			foreach (ValueTuple<PlayerId, string, bool> valueTuple in message.Players)
			{
				PlayerId playerId = valueTuple.Item1;
				string item = valueTuple.Item2;
				bool item2 = valueTuple.Item3;
				PartyPlayerInLobbyClient partyPlayerInLobbyClient = this.PlayersInParty.Find((PartyPlayerInLobbyClient p) => p.PlayerId == playerId);
				if (partyPlayerInLobbyClient != null)
				{
					partyPlayerInLobbyClient.SetAtParty();
				}
				else
				{
					partyPlayerInLobbyClient = new PartyPlayerInLobbyClient(playerId, item, item2);
					this.PlayersInParty.Add(partyPlayerInLobbyClient);
					partyPlayerInLobbyClient.SetAtParty();
				}
				if (playerId != this.PlayerID)
				{
					RecentPlayersManager.AddOrUpdatePlayerEntry(playerId, item, InteractionType.InPartyTogether, -1);
				}
			}
			foreach (ValueTuple<PlayerId, string> valueTuple2 in message.InvitedPlayers)
			{
				PlayerId item3 = valueTuple2.Item1;
				string item4 = valueTuple2.Item2;
				this.PlayersInParty.Add(new PartyPlayerInLobbyClient(item3, item4, false));
			}
			this._handler.OnPlayersAddedToParty(message.Players, message.InvitedPlayers);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000A52C File Offset: 0x0000872C
		private void OnPlayerRemovedFromPartyMessage(PlayerRemovedFromPartyMessage message)
		{
			if (message.PlayerId == this._playerId)
			{
				this.PlayersInParty.Clear();
			}
			else
			{
				this.PlayersInParty.RemoveAll((PartyPlayerInLobbyClient partyPlayer) => partyPlayer.PlayerId == message.PlayerId);
			}
			this._handler.OnPlayerRemovedFromParty(message.PlayerId, message.Reason);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000A5A4 File Offset: 0x000087A4
		private void OnPlayerAssignedPartyLeaderMessage(PlayerAssignedPartyLeaderMessage message)
		{
			PartyPlayerInLobbyClient partyPlayerInLobbyClient = this.PlayersInParty.FirstOrDefault((PartyPlayerInLobbyClient p) => p.IsPartyLeader);
			if (partyPlayerInLobbyClient != null)
			{
				partyPlayerInLobbyClient.SetMember();
			}
			PartyPlayerInLobbyClient partyPlayerInLobbyClient2 = this.PlayersInParty.FirstOrDefault((PartyPlayerInLobbyClient partyPlayer) => partyPlayer.PlayerId == message.PartyLeaderId);
			if (partyPlayerInLobbyClient2 != null)
			{
				partyPlayerInLobbyClient2.SetLeader();
			}
			else
			{
				this.KickPlayerFromParty(this.PlayerID);
			}
			this._handler.OnPlayerAssignedPartyLeader(message.PartyLeaderId);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000A638 File Offset: 0x00008838
		private void OnPlayerSuggestedToPartyMessage(PlayerSuggestedToPartyMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerSuggestedToParty(message.PlayerId, message.PlayerName, message.SuggestingPlayerId, message.SuggestingPlayerName);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000A662 File Offset: 0x00008862
		private void OnUpdatePlayerDataMessage(UpdatePlayerDataMessage updatePlayerDataMessage)
		{
			this.PlayerData = updatePlayerDataMessage.PlayerData;
			if (this._handler != null)
			{
				this._handler.OnPlayerDataReceived(this.PlayerData);
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000A68C File Offset: 0x0000888C
		private void OnServerStatusMessage(ServerStatusMessage serverStatusMessage)
		{
			this._serverStatusTimer.Restart();
			this._serverStatus = serverStatusMessage.ServerStatus;
			if (!this.IsAbleToSearchForGame && this.CurrentState == LobbyClient.State.SearchingBattle)
			{
				this.CancelFindGame();
			}
			if (this._handler != null)
			{
				this._handler.OnServerStatusReceived(this._serverStatus);
				LobbyClient.FriendListCheckDelay = this._serverStatus.FriendListUpdatePeriod * 1000;
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000A6F6 File Offset: 0x000088F6
		private void OnFriendListMessage(FriendListMessage friendListMessage)
		{
			this._friendListTimer.Restart();
			this.FriendInfos = friendListMessage.Friends;
			if (this._handler != null)
			{
				this._handler.OnFriendListReceived(friendListMessage.Friends);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000A728 File Offset: 0x00008928
		private void OnMatchmakerDisabledMessage(MatchmakerDisabledMessage matchmakerDisabledMessage)
		{
			if (matchmakerDisabledMessage.RemainingTime > 0)
			{
				this._matchmakerBlockedTime = DateTime.Now.AddSeconds((double)matchmakerDisabledMessage.RemainingTime);
				return;
			}
			this._matchmakerBlockedTime = DateTime.MinValue;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000A764 File Offset: 0x00008964
		private void OnClanCreationRequestMessage(ClanCreationRequestMessage clanCreationRequestMessage)
		{
			if (this._handler != null)
			{
				this._handler.OnClanInvitationReceived(clanCreationRequestMessage.ClanName, clanCreationRequestMessage.ClanTag, true);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000A786 File Offset: 0x00008986
		private void OnClanCreationRequestAnsweredMessage(ClanCreationRequestAnsweredMessage clanCreationRequestAnsweredMessage)
		{
			if (this._handler != null)
			{
				this._handler.OnClanInvitationAnswered(clanCreationRequestAnsweredMessage.PlayerId, clanCreationRequestAnsweredMessage.ClanCreationAnswer);
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000A7A7 File Offset: 0x000089A7
		private void OnClanCreationSuccessfulMessage(ClanCreationSuccessfulMessage clanCreationSuccessfulMessage)
		{
			if (this._handler != null)
			{
				this._handler.OnClanCreationSuccessful();
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000A7BC File Offset: 0x000089BC
		private void OnClanCreationFailedMessage(ClanCreationFailedMessage clanCreationFailedMessage)
		{
			if (this._handler != null)
			{
				this._handler.OnClanCreationFailed();
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000A7D1 File Offset: 0x000089D1
		private void OnCreateClanAnswerMessage(CreateClanAnswerMessage createClanAnswerMessage)
		{
			if (createClanAnswerMessage.Successful)
			{
				this._handler.OnClanCreationStarted();
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000A7E6 File Offset: 0x000089E6
		public void SendWhisper(string playerName, string message)
		{
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000A7E8 File Offset: 0x000089E8
		private void OnRecentPlayerStatusesMessage(RecentPlayerStatusesMessage message)
		{
			if (this._handler != null)
			{
				this._handler.OnRecentPlayerStatusesReceived(message.Friends);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000A803 File Offset: 0x00008A03
		public void FleeBattle()
		{
			this.CheckAndSendMessage(new RejoinBattleRequestMessage(false));
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000A811 File Offset: 0x00008A11
		public void SendPartyMessage(string message)
		{
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000A813 File Offset: 0x00008A13
		public void SendChannelMessage(Guid roomId, string message)
		{
			this.ChatManager.SendMessage(roomId, message);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000A822 File Offset: 0x00008A22
		private void OnClanInfoChangedMessage(ClanInfoChangedMessage clanInfoChangedMessage)
		{
			this.UpdateClanInfo(clanInfoChangedMessage.ClanHomeInfo);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000A830 File Offset: 0x00008A30
		protected override void OnTick()
		{
			if (this.LoggedIn && !this.IsInGame)
			{
				if (this._serverStatusTimer != null && this._serverStatusTimer.ElapsedMilliseconds > (long)LobbyClient.ServerStatusCheckDelay)
				{
					this._serverStatusTimer.Restart();
					this.CheckAndSendMessage(new GetServerStatusMessage());
				}
				if (this._friendListTimer != null && this._friendListTimer.ElapsedMilliseconds > (long)LobbyClient.FriendListCheckDelay)
				{
					this._friendListTimer.Restart();
					this.CheckAndSendMessage(new GetFriendListMessage());
					PlayerId[] recentPlayerIds = RecentPlayersManager.GetRecentPlayerIds();
					if (recentPlayerIds.Length != 0)
					{
						this.CheckAndSendMessage(new GetRecentPlayersStatusMessage(recentPlayerIds));
					}
				}
			}
			this.ChatManager.OnTick();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000A8D3 File Offset: 0x00008AD3
		private void OnInvitationToClanMessage(InvitationToClanMessage invitationToClanMessage)
		{
			if (this._handler != null)
			{
				this._handler.OnClanInvitationReceived(invitationToClanMessage.ClanName, invitationToClanMessage.ClanTag, false);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000A8F5 File Offset: 0x00008AF5
		public void RejoinBattle()
		{
			this.CheckAndSendMessage(new RejoinBattleRequestMessage(true));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000A903 File Offset: 0x00008B03
		private void OnJoinPremadeGameAnswerMessage(JoinPremadeGameAnswerMessage joinPremadeGameAnswerMessage)
		{
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000A905 File Offset: 0x00008B05
		public void OnBattleResultsSeen()
		{
			this.AssertCanPerformLobbyActions();
			this.CheckAndSendMessage(new BattleResultSeenMessage());
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000A918 File Offset: 0x00008B18
		private void OnCreatePremadeGameAnswerMessage(CreatePremadeGameAnswerMessage createPremadeGameAnswerMessage)
		{
			if (createPremadeGameAnswerMessage.Successful)
			{
				this._handler.OnPremadeGameCreated();
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000A92D File Offset: 0x00008B2D
		private void OnJoinPremadeGameRequestMessage(JoinPremadeGameRequestMessage joinPremadeGameRequestMessage)
		{
			this._handler.OnJoinPremadeGameRequested(joinPremadeGameRequestMessage.ClanName, joinPremadeGameRequestMessage.Sigil, joinPremadeGameRequestMessage.ChallengerPartyId, joinPremadeGameRequestMessage.ChallengerPlayers, joinPremadeGameRequestMessage.ChallengerPartyLeaderId, joinPremadeGameRequestMessage.PremadeGameType);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000A95E File Offset: 0x00008B5E
		private void OnJoinPremadeGameRequestResultMessage(JoinPremadeGameRequestResultMessage joinPremadeGameRequestResultMessage)
		{
			if (joinPremadeGameRequestResultMessage.Successful)
			{
				this._handler.OnJoinPremadeGameRequestSuccessful();
				this.CurrentState = LobbyClient.State.WaitingToJoinPremadeGame;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000A97C File Offset: 0x00008B7C
		private async void OnClanDisbandedMessage(ClanDisbandedMessage clanDisbandedMessage)
		{
			ClanHomeInfo clanHomeInfo = await this.GetClanHomeInfo();
			this.UpdateClanInfo(clanHomeInfo);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000A9B5 File Offset: 0x00008BB5
		private void OnClanGameCreationCancelledMessage(ClanGameCreationCancelledMessage clanGameCreationCancelledMessage)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			this._handler.OnPremadeGameCreationCancelled();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000A9C9 File Offset: 0x00008BC9
		private void OnPremadeGameEligibilityStatusMessage(PremadeGameEligibilityStatusMessage premadeGameEligibilityStatusMessage)
		{
			this._handler.OnPremadeGameEligibilityStatusReceived(premadeGameEligibilityStatusMessage.EligibleGameTypes.Length != 0);
			this.IsEligibleToCreatePremadeGame = (premadeGameEligibilityStatusMessage.EligibleGameTypes.Length != 0);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		private async void OnKickedFromClan(KickedFromClanMessage kickedFromClanMessage)
		{
			ClanHomeInfo clanHomeInfo = await this.GetClanHomeInfo();
			this.UpdateClanInfo(clanHomeInfo);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000AA29 File Offset: 0x00008C29
		private void OnCustomBattleOverMessage(CustomBattleOverMessage message)
		{
			this.CurrentState = LobbyClient.State.AtLobby;
			this._handler.OnMatchmakerGameOver(message.OldExperience, message.NewExperience, new List<string>(), message.GoldGain, null, null, BattleCancelReason.None);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000AA57 File Offset: 0x00008C57
		public void AcceptClanInvitation()
		{
			this.CheckAndSendMessage(new AcceptClanInvitationMessage());
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000AA64 File Offset: 0x00008C64
		public void DeclineClanInvitation()
		{
			this.CheckAndSendMessage(new DeclineClanInvitationMessage());
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000AA71 File Offset: 0x00008C71
		private void OnShowAnnouncementMessage(ShowAnnouncementMessage message)
		{
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnAnnouncementReceived(message.Announcement);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public void MarkNotificationAsRead(int notificationID)
		{
			UpdateNotificationsMessage message = new UpdateNotificationsMessage(new int[]
			{
				notificationID
			});
			this.CheckAndSendMessage(message);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		private void OnRejoinBattleRequestAnswerMessage(RejoinBattleRequestAnswerMessage rejoinBattleRequestAnswerMessage)
		{
			this._handler.OnRejoinBattleRequestAnswered(rejoinBattleRequestAnswerMessage.IsSuccessful);
			if (rejoinBattleRequestAnswerMessage.IsSuccessful && rejoinBattleRequestAnswerMessage.IsRejoinAccepted)
			{
				this.CurrentState = LobbyClient.State.SearchingBattle;
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000AADA File Offset: 0x00008CDA
		public void AcceptClanCreationRequest()
		{
			this.CheckAndSendMessage(new AcceptClanCreationRequestMessage());
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000AAE7 File Offset: 0x00008CE7
		private void OnPendingBattleRejoinMessage(PendingBattleRejoinMessage pendingBattleRejoinMessage)
		{
			this._handler.OnPendingRejoin();
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		private void OnSigilChangeAnswerMessage(SigilChangeAnswerMessage message)
		{
			if (message.Successful)
			{
				ILobbyClientSessionHandler handler = this._handler;
				if (handler == null)
				{
					return;
				}
				handler.OnSigilChanged();
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000AB0E File Offset: 0x00008D0E
		public void DeclineClanCreationRequest()
		{
			this.CheckAndSendMessage(new DeclineClanCreationRequestMessage());
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000AB1B File Offset: 0x00008D1B
		public void PromoteToClanLeader(PlayerId playerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new PromoteToClanLeaderMessage(playerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000AB2A File Offset: 0x00008D2A
		private void OnLobbyNotificationsMessage(LobbyNotificationsMessage message)
		{
			this._handler.OnNotificationsReceived(message.Notifications);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public void KickFromClan(PlayerId playerId)
		{
			this.CheckAndSendMessage(new KickFromClanMessage(playerId));
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public async Task<CheckClanParameterValidResult> ClanNameExists(string clanName)
		{
			return await base.CallFunction<CheckClanParameterValidResult>(new CheckClanNameValidMessage(clanName));
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public async Task<CheckClanParameterValidResult> ClanTagExists(string clanTag)
		{
			return await base.CallFunction<CheckClanParameterValidResult>(new CheckClanTagValidMessage(clanTag));
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public async Task<ClanHomeInfo> GetClanHomeInfo()
		{
			GetClanHomeInfoResult getClanHomeInfoResult = await base.CallFunction<GetClanHomeInfoResult>(new GetClanHomeInfoMessage());
			ClanHomeInfo result;
			if (getClanHomeInfoResult != null)
			{
				this.UpdateClanInfo(getClanHomeInfoResult.ClanHomeInfo);
				result = getClanHomeInfoResult.ClanHomeInfo;
			}
			else
			{
				this.UpdateClanInfo(null);
				result = null;
			}
			return result;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000AC31 File Offset: 0x00008E31
		public void SendChatMessage(Guid roomId, string message)
		{
			this.ChatManager.SendMessage(roomId, message);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000AC40 File Offset: 0x00008E40
		public void JoinChannel(ChatChannelType channel)
		{
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000AC42 File Offset: 0x00008E42
		public void AssignAsClanOfficer(PlayerId playerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new AssignAsClanOfficerMessage(playerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000AC51 File Offset: 0x00008E51
		public void RemoveClanOfficerRoleForPlayer(PlayerId playerId)
		{
			this.CheckAndSendMessage(new RemoveClanOfficerRoleForPlayerMessage(playerId));
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000AC5F File Offset: 0x00008E5F
		public void LeaveChannel(ChatChannelType channel)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000AC64 File Offset: 0x00008E64
		private void UpdateClanInfo(ClanHomeInfo clanHomeInfo)
		{
			this.PlayersInClan.Clear();
			this.PlayerInfosInClan.Clear();
			this.ClanID = Guid.Empty;
			this.ClanInfo = null;
			this.ClanHomeInfo = clanHomeInfo;
			if (clanHomeInfo != null)
			{
				if (clanHomeInfo.IsInClan)
				{
					foreach (ClanPlayer item in clanHomeInfo.ClanInfo.Players)
					{
						this.PlayersInClan.Add(item);
					}
					foreach (ClanPlayerInfo item2 in clanHomeInfo.ClanPlayerInfos)
					{
						this.PlayerInfosInClan.Add(item2);
					}
					this.ClanID = clanHomeInfo.ClanInfo.ClanId;
				}
				this.ClanInfo = clanHomeInfo.ClanInfo;
			}
			if (this._handler != null)
			{
				this._handler.OnClanInfoChanged();
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public async Task<ClanLeaderboardInfo> GetClanLeaderboardInfo()
		{
			GetClanLeaderboardResult getClanLeaderboardResult = await base.CallFunction<GetClanLeaderboardResult>(new GetClanLeaderboardMessage());
			ClanLeaderboardInfo result;
			if (getClanLeaderboardResult != null)
			{
				result = getClanLeaderboardResult.ClanLeaderboardInfo;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000AD74 File Offset: 0x00008F74
		public async Task<ClanInfo> GetPlayerClanInfo(PlayerId playerId)
		{
			GetPlayerClanInfoResult getPlayerClanInfoResult = await base.CallFunction<GetPlayerClanInfoResult>(new GetPlayerClanInfo(playerId));
			ClanInfo result;
			if (((getPlayerClanInfoResult != null) ? getPlayerClanInfoResult.ClanInfo : null) != null)
			{
				result = getPlayerClanInfoResult.ClanInfo;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000ADC1 File Offset: 0x00008FC1
		public void SendClanMessage(string message)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		public async Task<PremadeGameList> GetPremadeGameList()
		{
			GetPremadeGameListResult getPremadeGameListResult = await base.CallFunction<GetPremadeGameListResult>(new GetPremadeGameListMessage());
			PremadeGameList result;
			if (getPremadeGameListResult != null)
			{
				this.AvailablePremadeGames = getPremadeGameListResult.GameList;
				if (this._handler != null)
				{
					this._handler.OnPremadeGameListReceived();
				}
				result = getPremadeGameListResult.GameList;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000AE0C File Offset: 0x0000900C
		public async Task<AvailableScenes> GetAvailableScenes()
		{
			GetAvailableScenesResult getAvailableScenesResult = await base.CallFunction<GetAvailableScenesResult>(new GetAvailableScenesMessage());
			AvailableScenes result;
			if (getAvailableScenesResult != null)
			{
				result = getAvailableScenesResult.AvailableScenes;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000AE54 File Offset: 0x00009054
		public async Task<PublishedLobbyNewsArticle[]> GetLobbyNews()
		{
			GetPublishedLobbyNewsMessageResult getPublishedLobbyNewsMessageResult = await base.CallFunction<GetPublishedLobbyNewsMessageResult>(new GetPublishedLobbyNewsMessage());
			PublishedLobbyNewsArticle[] result;
			if (getPublishedLobbyNewsMessageResult != null)
			{
				result = getPublishedLobbyNewsMessageResult.Content;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000AE99 File Offset: 0x00009099
		public void SetClanInformationText(string informationText)
		{
			this.CheckAndSendMessage(new SetClanInformationMessage(informationText));
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000AEA7 File Offset: 0x000090A7
		public void AddClanAnnouncement(string announcement)
		{
			this.CheckAndSendMessage(new AddClanAnnouncementMessage(announcement));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000AEB5 File Offset: 0x000090B5
		public void EditClanAnnouncement(int announcementId, string text)
		{
			this.CheckAndSendMessage(new EditClanAnnouncementMessage(announcementId, text));
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public void RemoveClanAnnouncement(int announcementId)
		{
			this.CheckAndSendMessage(new RemoveClanAnnouncementMessage(announcementId));
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000AED2 File Offset: 0x000090D2
		public void ChangeClanFaction(string faction)
		{
			this.CheckAndSendMessage(new ChangeClanFactionMessage(faction));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public void ChangeClanSigil(string sigil)
		{
			this.CheckAndSendMessage(new ChangeClanSigilMessage(sigil));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000AEEE File Offset: 0x000090EE
		public void DestroyClan()
		{
			this.CheckAndSendMessage(new DestroyClanMessage());
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000AEFB File Offset: 0x000090FB
		public void InviteToClan(PlayerId invitedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new InviteToClanMessage(invitedPlayerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000AF0C File Offset: 0x0000910C
		public async void CreatePremadeGame(string name, string gameType, string mapName, string factionA, string factionB, string password, PremadeGameType premadeGameType)
		{
			this.CurrentState = LobbyClient.State.WaitingToCreatePremadeGame;
			string password2 = (!string.IsNullOrEmpty(password)) ? Common.CalculateMD5Hash(password) : null;
			CreatePremadeGameMessageResult createPremadeGameMessageResult = await base.CallFunction<CreatePremadeGameMessageResult>(new CreatePremadeGameMessage(name, gameType, mapName, factionA, factionB, password2, premadeGameType));
			if (createPremadeGameMessageResult == null || !createPremadeGameMessageResult.Successful)
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000AF81 File Offset: 0x00009181
		public void CancelCreatingPremadeGame()
		{
			this.CheckAndSendMessage(new CancelCreatingPremadeGameMessage());
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000AF90 File Offset: 0x00009190
		public void RequestToJoinPremadeGame(Guid gameId, string password)
		{
			string password2 = Common.CalculateMD5Hash(password);
			this.CheckAndSendMessage(new RequestToJoinPremadeGameMessage(gameId, password2));
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000AFB1 File Offset: 0x000091B1
		public void AcceptJoinPremadeGameRequest(Guid partyId)
		{
			this.CheckAndSendMessage(new AcceptJoinPremadeGameRequestMessage(partyId));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000AFBF File Offset: 0x000091BF
		public void DeclineJoinPremadeGameRequest(Guid partyId)
		{
			this.CheckAndSendMessage(new DeclineJoinPremadeGameRequestMessage(partyId));
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000AFCD File Offset: 0x000091CD
		public void InviteToParty(PlayerId playerId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new InviteToPartyMessage(playerId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000AFDC File Offset: 0x000091DC
		public void DisbandParty()
		{
			this.CheckAndSendMessage(new DisbandPartyMessage());
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000AFE9 File Offset: 0x000091E9
		public void Test_CreateChatRoom(string name)
		{
			this.CheckAndSendMessage(new Test_CreateChatRoomMessage(name));
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public void Test_AddChatRoomUser(string name)
		{
			this.CheckAndSendMessage(new Test_AddChatRoomUser(name));
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000B005 File Offset: 0x00009205
		public void Test_RemoveChatRoomUser(string name)
		{
			this.CheckAndSendMessage(new Test_RemoveChatRoomUser(name));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000B013 File Offset: 0x00009213
		public void Test_DeleteChatRoom(Guid id)
		{
			this.CheckAndSendMessage(new Test_DeleteChatRoomMessage(id));
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000B021 File Offset: 0x00009221
		public IEnumerable<string> Test_ListChatRoomIds()
		{
			return from room in this.ChatManager.Rooms
			select room.RoomId.ToString();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000B052 File Offset: 0x00009252
		public void KickPlayerFromParty(PlayerId playerId)
		{
			this.CheckAndSendMessage(new KickPlayerFromPartyMessage(playerId));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000B060 File Offset: 0x00009260
		public void OnPlayerNameUpdated(string name)
		{
			this._userName = name;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000B069 File Offset: 0x00009269
		public void ToggleUseClanSigil(bool isUsed)
		{
			this.CheckAndSendMessage(new UpdateUsingClanSigil(isUsed));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000B077 File Offset: 0x00009277
		public void PromotePlayerToPartyLeader(PlayerId playerId)
		{
			this.CheckAndSendMessage(new PromotePlayerToPartyLeaderMessage(playerId));
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000B085 File Offset: 0x00009285
		public void ChangeSigil(string sigilId)
		{
			this.CheckAndSendMessage(new ChangePlayerSigilMessage(sigilId));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000B094 File Offset: 0x00009294
		public async Task<bool> InviteToPlatformSession(PlayerId playerId)
		{
			bool result = false;
			if (this._handler != null)
			{
				result = await this._handler.OnInviteToPlatformSession(playerId);
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000B0E4 File Offset: 0x000092E4
		public async void EndCustomGame()
		{
			await base.CallFunction<EndHostingCustomGameResult>(new EndHostingCustomGameMessage());
			if (this._handler != null)
			{
				this._handler.OnCustomGameEnd();
			}
			this.CurrentState = LobbyClient.State.AtLobby;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000B120 File Offset: 0x00009320
		public async void RegisterCustomGame(string gameModule, string gameType, string serverName, int maxPlayerCount, string map, string uniqueMapId, string gamePassword, string adminPassword, int port)
		{
			this.CustomGameType = gameType;
			this.CustomGameScene = map;
			this.CurrentState = LobbyClient.State.WaitingToRegisterCustomGame;
			RegisterCustomGameResult registerCustomGameResult = await base.CallFunction<RegisterCustomGameResult>(new RegisterCustomGameMessage(gameModule, gameType, serverName, maxPlayerCount, map, uniqueMapId, gamePassword, adminPassword, port));
			Debug.Print("Register custom game server response received", 0, Debug.DebugColor.White, 17592186044416UL);
			if (registerCustomGameResult.Success)
			{
				this.CurrentState = LobbyClient.State.HostingCustomGame;
				if (this._handler != null)
				{
					this._handler.OnRegisterCustomGameServerResponse();
				}
			}
			else
			{
				this.CurrentState = LobbyClient.State.AtLobby;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000B1A7 File Offset: 0x000093A7
		public void UpdateCustomGameData(string newGameType, string newMap, int newCount)
		{
			base.SendMessage(new UpdateCustomGameData(newGameType, newMap, newCount));
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000B1B7 File Offset: 0x000093B7
		public void ResponseCustomGameClientConnection(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			base.SendMessage(new ResponseCustomGameClientConnectionMessage(playerJoinData));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000B1C5 File Offset: 0x000093C5
		public void AcceptPartyInvitation()
		{
			this.IsPartyInvitationPopupActive = false;
			this.CheckAndSendMessage(new AcceptPartyInvitationMessage());
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000B1D9 File Offset: 0x000093D9
		public void DeclinePartyInvitation()
		{
			this.IsPartyInvitationPopupActive = false;
			this.CheckAndSendMessage(new DeclinePartyInvitationMessage());
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000B1ED File Offset: 0x000093ED
		public void AcceptPartyJoinRequest(PlayerId playerId)
		{
			this.IsPartyJoinRequestPopupActive = false;
			this.CheckAndSendMessage(new AcceptPartyJoinRequestMessage(playerId));
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000B202 File Offset: 0x00009402
		public void DeclinePartyJoinRequest(PlayerId playerId, PartyJoinDeclineReason reason)
		{
			this.IsPartyJoinRequestPopupActive = false;
			this.CheckAndSendMessage(new DeclinePartyJoinRequestMessage(playerId, reason));
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000B218 File Offset: 0x00009418
		public void UpdateCharacter(BodyProperties bodyProperties, bool isFemale)
		{
			this.AssertCanPerformLobbyActions();
			base.SendMessage(new UpdateCharacterMessage(bodyProperties, isFemale));
			if (this.CanPerformLobbyActions)
			{
				this.PlayerData.BodyProperties = bodyProperties;
				this.PlayerData.IsFemale = isFemale;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000B250 File Offset: 0x00009450
		public async Task<bool> UpdateShownBadgeId(string shownBadgeId)
		{
			this.AssertCanPerformLobbyActions();
			UpdateShownBadgeIdMessageResult updateShownBadgeIdMessageResult = await base.CallFunction<UpdateShownBadgeIdMessageResult>(new UpdateShownBadgeIdMessage(shownBadgeId));
			if (updateShownBadgeIdMessageResult != null && updateShownBadgeIdMessageResult.Successful)
			{
				this.PlayerData.ShownBadgeId = shownBadgeId;
			}
			return updateShownBadgeIdMessageResult != null && updateShownBadgeIdMessageResult.Successful;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public async Task<AnotherPlayerData> GetAnotherPlayerState(PlayerId playerId)
		{
			this.AssertCanPerformLobbyActions();
			GetAnotherPlayerStateMessageResult getAnotherPlayerStateMessageResult = await base.CallFunction<GetAnotherPlayerStateMessageResult>(new GetAnotherPlayerStateMessage(playerId));
			AnotherPlayerData result;
			if (getAnotherPlayerStateMessageResult != null)
			{
				result = getAnotherPlayerStateMessageResult.AnotherPlayerData;
			}
			else
			{
				result = new AnotherPlayerData(AnotherPlayerState.NoAnswer, 0);
			}
			return result;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public async Task<PlayerData> GetAnotherPlayerData(PlayerId playerID)
		{
			this.AssertCanPerformLobbyActions();
			await this.WaitForPendingRequestCompletion(LobbyClient.PendingRequest.PlayerData, playerID);
			PlayerData playerData;
			PlayerData result;
			if (this._cachedPlayerDatas.TryGetValue(playerID, out playerData))
			{
				result = playerData;
			}
			else
			{
				GetAnotherPlayerDataMessageResult getAnotherPlayerDataMessageResult = await this.CreatePendingRequest<GetAnotherPlayerDataMessageResult>(LobbyClient.PendingRequest.PlayerData, playerID, base.CallFunction<GetAnotherPlayerDataMessageResult>(new GetAnotherPlayerDataMessage(playerID)));
				if (((getAnotherPlayerDataMessageResult != null) ? getAnotherPlayerDataMessageResult.AnotherPlayerData : null) != null)
				{
					this._cachedPlayerDatas[playerID] = getAnotherPlayerDataMessageResult.AnotherPlayerData;
				}
				result = ((getAnotherPlayerDataMessageResult != null) ? getAnotherPlayerDataMessageResult.AnotherPlayerData : null);
			}
			return result;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000B340 File Offset: 0x00009540
		public async Task<MatchmakingQueueStats> GetPlayerCountInQueue()
		{
			GetPlayerCountInQueueResult getPlayerCountInQueueResult = await base.CallFunction<GetPlayerCountInQueueResult>(new GetPlayerCountInQueue());
			MatchmakingQueueStats result;
			if (getPlayerCountInQueueResult != null)
			{
				result = getPlayerCountInQueueResult.MatchmakingQueueStats;
			}
			else
			{
				result = MatchmakingQueueStats.Empty;
			}
			return result;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000B388 File Offset: 0x00009588
		public async Task<List<ValueTuple<PlayerId, AnotherPlayerData>>> GetOtherPlayersState(List<PlayerId> players)
		{
			this.AssertCanPerformLobbyActions();
			GetOtherPlayersStateMessageResult getOtherPlayersStateMessageResult = await base.CallFunction<GetOtherPlayersStateMessageResult>(new GetOtherPlayersStateMessage(players));
			return (getOtherPlayersStateMessageResult != null) ? getOtherPlayersStateMessageResult.States : null;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public async Task<MatchmakingWaitTimeStats> GetMatchmakingWaitTimes()
		{
			GetAverageMatchmakingWaitTimesResult getAverageMatchmakingWaitTimesResult = await base.CallFunction<GetAverageMatchmakingWaitTimesResult>(new GetAverageMatchmakingWaitTimesMessage());
			MatchmakingWaitTimeStats result;
			if (getAverageMatchmakingWaitTimesResult != null)
			{
				result = getAverageMatchmakingWaitTimesResult.MatchmakingWaitTimeStats;
			}
			else
			{
				result = MatchmakingWaitTimeStats.Empty;
			}
			return result;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000B420 File Offset: 0x00009620
		public async Task<Badge[]> GetPlayerBadges()
		{
			GetPlayerBadgesMessageResult getPlayerBadgesMessageResult = await base.CallFunction<GetPlayerBadgesMessageResult>(new GetPlayerBadgesMessage());
			List<Badge> list = new List<Badge>();
			if (getPlayerBadgesMessageResult != null)
			{
				string[] badges = getPlayerBadgesMessageResult.Badges;
				for (int i = 0; i < badges.Length; i++)
				{
					Badge byId = BadgeManager.GetById(badges[i]);
					if (byId != null)
					{
						list.Add(byId);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000B468 File Offset: 0x00009668
		public async Task<PlayerStatsBase[]> GetPlayerStats(PlayerId playerID)
		{
			PlayerStatsBase[] array;
			PlayerStatsBase[] result;
			if (this._cachedPlayerStats.TryGetValue(playerID, out array))
			{
				result = array;
			}
			else
			{
				GetPlayerStatsMessageResult getPlayerStatsMessageResult = await base.CallFunction<GetPlayerStatsMessageResult>(new GetPlayerStatsMessage(playerID));
				if (((getPlayerStatsMessageResult != null) ? getPlayerStatsMessageResult.PlayerStats : null) != null)
				{
					this._cachedPlayerStats[playerID] = getPlayerStatsMessageResult.PlayerStats;
				}
				result = ((getPlayerStatsMessageResult != null) ? getPlayerStatsMessageResult.PlayerStats : null);
			}
			return result;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public async Task<GameTypeRankInfo[]> GetGameTypeRankInfo(PlayerId playerID)
		{
			await this.WaitForPendingRequestCompletion(LobbyClient.PendingRequest.RankInfo, playerID);
			GameTypeRankInfo[] array;
			GameTypeRankInfo[] result;
			if (this._cachedRankInfos.TryGetValue(playerID, out array))
			{
				result = array;
			}
			else
			{
				GetPlayerGameTypeRankInfoMessageResult getPlayerGameTypeRankInfoMessageResult = await this.CreatePendingRequest<GetPlayerGameTypeRankInfoMessageResult>(LobbyClient.PendingRequest.RankInfo, playerID, base.CallFunction<GetPlayerGameTypeRankInfoMessageResult>(new GetPlayerGameTypeRankInfoMessage(playerID)));
				if (((getPlayerGameTypeRankInfoMessageResult != null) ? getPlayerGameTypeRankInfoMessageResult.GameTypeRankInfo : null) != null)
				{
					this._cachedRankInfos[playerID] = getPlayerGameTypeRankInfoMessageResult.GameTypeRankInfo;
				}
				result = ((getPlayerGameTypeRankInfoMessageResult != null) ? getPlayerGameTypeRankInfoMessageResult.GameTypeRankInfo : null);
			}
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000B508 File Offset: 0x00009708
		public async Task<int> GetRankedLeaderboardCount(string gameType)
		{
			GetRankedLeaderboardCountMessageResult getRankedLeaderboardCountMessageResult = await base.CallFunction<GetRankedLeaderboardCountMessageResult>(new GetRankedLeaderboardCountMessage(gameType));
			return (getRankedLeaderboardCountMessageResult != null) ? getRankedLeaderboardCountMessageResult.Count : 0;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000B558 File Offset: 0x00009758
		public async Task<PlayerLeaderboardData[]> GetRankedLeaderboard(string gameType, int startIndex, int count)
		{
			GetRankedLeaderboardMessageResult getRankedLeaderboardMessageResult = await base.CallFunction<GetRankedLeaderboardMessageResult>(new GetRankedLeaderboardMessage(gameType, startIndex, count));
			return (getRankedLeaderboardMessageResult != null) ? getRankedLeaderboardMessageResult.LeaderboardPlayers : null;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000B5B5 File Offset: 0x000097B5
		public void SendCreateClanMessage(string clanName, string clanTag, string clanFaction, string clanSigil)
		{
			this.AssertCanPerformLobbyActions();
			base.SendMessage(new CreateClanMessage(clanName, clanTag, clanFaction, clanSigil));
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000B5D0 File Offset: 0x000097D0
		public async Task<bool> CanLogin()
		{
			this.CurrentState = LobbyClient.State.Working;
			TaskAwaiter<bool> taskAwaiter = Gatekeeper.IsGenerous().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			bool result;
			if (taskAwaiter.GetResult())
			{
				this.CurrentState = LobbyClient.State.Idle;
				result = true;
			}
			else
			{
				await Task.Delay(new Random().Next() % 3000 + 1000);
				this.CurrentState = LobbyClient.State.Idle;
				result = false;
			}
			return result;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000B615 File Offset: 0x00009815
		public void GetFriendList()
		{
			this.CheckAndSendMessage(new GetFriendListMessage());
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000B622 File Offset: 0x00009822
		public void AddFriend(PlayerId friendId, bool dontUseNameForUnknownPlayer)
		{
			this.CheckAndSendMessage(new AddFriendMessage(friendId, dontUseNameForUnknownPlayer));
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000B631 File Offset: 0x00009831
		public void RemoveFriend(PlayerId friendId)
		{
			this.CheckAndSendMessage(new RemoveFriendMessage(friendId));
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000B63F File Offset: 0x0000983F
		public void RespondToFriendRequest(PlayerId playerId, bool dontUseNameForUnknownPlayer, bool isAccepted, bool isBlocked = false)
		{
			this.CheckAndSendMessage(new FriendRequestResponseMessage(playerId, dontUseNameForUnknownPlayer, isAccepted, isBlocked));
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000B654 File Offset: 0x00009854
		public void ReportPlayer(string gameId, PlayerId player, string playerName, PlayerReportType type, string message)
		{
			Guid gameId2;
			if (Guid.TryParse(gameId, out gameId2))
			{
				this.CheckAndSendMessage(new ReportPlayerMessage(gameId2, player, playerName, type, message));
				return;
			}
			ILobbyClientSessionHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSystemMessageReceived(new TextObject("{=dnKQbXIZ}Could not report player: Game does not exist.", null).ToString());
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public void ChangeUsername(string username)
		{
			if ((this.PlayerData == null || this.PlayerData.Username != username) && username != null && username.Length >= Parameters.UsernameMinLength && username.Length <= Parameters.UsernameMaxLength && Common.IsAllLetters(username))
			{
				this.CheckAndSendMessage(new ChangeUsernameMessage(username));
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000B6F9 File Offset: 0x000098F9
		void IChatClientHandler.OnChatMessageReceived(Guid roomId, string roomName, string playerName, string textMessage, string textColor, MessageType type)
		{
			if (this._handler != null)
			{
				this._handler.OnChatMessageReceived(roomId, roomName, playerName, textMessage, textColor, type);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000B718 File Offset: 0x00009918
		public void AddFriendByUsernameAndId(string username, int userId, bool dontUseNameForUnknownPlayer)
		{
			if (username != null && username.Length >= Parameters.UsernameMinLength && username.Length <= Parameters.UsernameMaxLength && Common.IsAllLetters(username) && userId >= 0 && userId <= Parameters.UserIdMax)
			{
				this.CheckAndSendMessage(new AddFriendByUsernameAndIdMessage(username, userId, dontUseNameForUnknownPlayer));
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000B764 File Offset: 0x00009964
		public async Task<bool> DoesPlayerWithUsernameAndIdExist(string username, int userId)
		{
			bool result;
			if (username != null && username.Length >= Parameters.UsernameMinLength && username.Length <= Parameters.UsernameMaxLength && Common.IsAllLetters(username) && userId >= 0 && userId <= Parameters.UserIdMax)
			{
				GetPlayerByUsernameAndIdMessageResult getPlayerByUsernameAndIdMessageResult = await base.CallFunction<GetPlayerByUsernameAndIdMessageResult>(new GetPlayerByUsernameAndIdMessage(username, userId));
				result = (getPlayerByUsernameAndIdMessageResult != null && getPlayerByUsernameAndIdMessageResult.PlayerId.IsValid);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000B7BC File Offset: 0x000099BC
		public bool IsPlayerClanLeader(PlayerId playerID)
		{
			ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == playerID);
			return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Leader;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000B7FC File Offset: 0x000099FC
		public bool IsPlayerClanOfficer(PlayerId playerID)
		{
			ClanPlayer clanPlayer = this.PlayersInClan.Find((ClanPlayer p) => p.PlayerId == playerID);
			return clanPlayer != null && clanPlayer.Role == ClanPlayerRole.Officer;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000B83C File Offset: 0x00009A3C
		public async Task<bool> UpdateUsedCosmeticItems([TupleElementNames(new string[]
		{
			"cosmeticId",
			"isEquipped"
		})] Dictionary<string, List<ValueTuple<string, bool>>> usedCosmetics)
		{
			List<CosmeticItemInfo> list = new List<CosmeticItemInfo>();
			foreach (string text in usedCosmetics.Keys)
			{
				foreach (ValueTuple<string, bool> valueTuple in usedCosmetics[text])
				{
					CosmeticItemInfo item = new CosmeticItemInfo(text, valueTuple.Item1, valueTuple.Item2);
					list.Add(item);
				}
			}
			UpdateUsedCosmeticItemsMessageResult updateUsedCosmeticItemsMessageResult = await base.CallFunction<UpdateUsedCosmeticItemsMessageResult>(new UpdateUsedCosmeticItemsMessage(list));
			if (updateUsedCosmeticItemsMessageResult != null && updateUsedCosmeticItemsMessageResult.Successful)
			{
				foreach (KeyValuePair<string, List<ValueTuple<string, bool>>> keyValuePair in usedCosmetics)
				{
					if (!string.IsNullOrWhiteSpace(keyValuePair.Key))
					{
						List<string> list2;
						if (!this.UsedCosmetics.TryGetValue(keyValuePair.Key, out list2))
						{
							list2 = new List<string>();
							this._usedCosmetics.Add(keyValuePair.Key, list2);
						}
						foreach (ValueTuple<string, bool> valueTuple2 in keyValuePair.Value)
						{
							string item2 = valueTuple2.Item1;
							if (valueTuple2.Item2)
							{
								list2.Add(item2);
							}
							else
							{
								list2.Remove(item2);
							}
						}
					}
				}
			}
			return updateUsedCosmeticItemsMessageResult != null && updateUsedCosmeticItemsMessageResult.Successful;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000B88C File Offset: 0x00009A8C
		[return: TupleElementNames(new string[]
		{
			"isSuccessful",
			"finalGold"
		})]
		public async Task<ValueTuple<bool, int>> BuyCosmetic(string cosmeticId)
		{
			BuyCosmeticMessageResult buyCosmeticMessageResult = await base.CallFunction<BuyCosmeticMessageResult>(new BuyCosmeticMessage(cosmeticId));
			if (buyCosmeticMessageResult != null && buyCosmeticMessageResult.Successful)
			{
				this._ownedCosmetics.Add(cosmeticId);
			}
			return new ValueTuple<bool, int>(buyCosmeticMessageResult != null && buyCosmeticMessageResult.Successful, (buyCosmeticMessageResult != null) ? buyCosmeticMessageResult.Gold : 0);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000B8DC File Offset: 0x00009ADC
		[return: TupleElementNames(new string[]
		{
			"isSuccessful",
			"ownedCosmetics",
			"usedCosmetics"
		})]
		public async Task<ValueTuple<bool, List<string>, Dictionary<string, List<string>>>> GetCosmeticsInfo()
		{
			GetUserCosmeticsInfoMessageResult getUserCosmeticsInfoMessageResult = await base.CallFunction<GetUserCosmeticsInfoMessageResult>(new GetUserCosmeticsInfoMessage());
			if (getUserCosmeticsInfoMessageResult != null)
			{
				this._usedCosmetics = (getUserCosmeticsInfoMessageResult.UsedCosmetics ?? new Dictionary<string, List<string>>());
				this._ownedCosmetics = (getUserCosmeticsInfoMessageResult.OwnedCosmetics ?? new List<string>());
			}
			return new ValueTuple<bool, List<string>, Dictionary<string, List<string>>>(getUserCosmeticsInfoMessageResult != null && getUserCosmeticsInfoMessageResult.Successful, (getUserCosmeticsInfoMessageResult != null) ? getUserCosmeticsInfoMessageResult.OwnedCosmetics : null, (getUserCosmeticsInfoMessageResult != null) ? getUserCosmeticsInfoMessageResult.UsedCosmetics : null);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000B924 File Offset: 0x00009B24
		public async Task<string> GetDedicatedCustomServerAuthToken()
		{
			GetDedicatedCustomServerAuthTokenMessageResult getDedicatedCustomServerAuthTokenMessageResult = await base.CallFunction<GetDedicatedCustomServerAuthTokenMessageResult>(new GetDedicatedCustomServerAuthTokenMessage());
			return (getDedicatedCustomServerAuthTokenMessageResult != null) ? getDedicatedCustomServerAuthTokenMessageResult.AuthToken : null;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000B96C File Offset: 0x00009B6C
		public async Task<string> GetOfficialServerProviderName()
		{
			GetOfficialServerProviderNameResult getOfficialServerProviderNameResult = await base.CallFunction<GetOfficialServerProviderNameResult>(new GetOfficialServerProviderNameMessage());
			return ((getOfficialServerProviderNameResult != null) ? getOfficialServerProviderNameResult.Name : null) ?? string.Empty;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public async Task<string> GetPlayerBannerlordID(PlayerId playerId)
		{
			await this.WaitForPendingRequestCompletion(LobbyClient.PendingRequest.BannerlordID, playerId);
			string text;
			string result;
			if (this._cachedPlayerBannerlordIDs.TryGetValue(playerId, out text))
			{
				result = text;
			}
			else
			{
				GetBannerlordIDMessageResult getBannerlordIDMessageResult = await this.CreatePendingRequest<GetBannerlordIDMessageResult>(LobbyClient.PendingRequest.BannerlordID, playerId, base.CallFunction<GetBannerlordIDMessageResult>(new GetBannerlordIDMessage(playerId)));
				if (getBannerlordIDMessageResult != null && getBannerlordIDMessageResult.BannerlordID != null)
				{
					this._cachedPlayerBannerlordIDs[playerId] = getBannerlordIDMessageResult.BannerlordID;
				}
				result = (((getBannerlordIDMessageResult != null) ? getBannerlordIDMessageResult.BannerlordID : null) ?? string.Empty);
			}
			return result;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000BA04 File Offset: 0x00009C04
		public bool IsKnownPlayer(PlayerId playerID)
		{
			bool flag = playerID == this._playerId;
			bool flag2 = this.FriendIDs.Contains(playerID);
			bool flag3 = this.IsInParty && this.PlayersInParty.Any((PartyPlayerInLobbyClient p) => p.PlayerId.Equals(playerID));
			bool flag4 = this.IsInClan && this.PlayersInClan.Any((ClanPlayer p) => p.PlayerId.Equals(playerID));
			return flag || flag2 || flag3 || flag4;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000BA90 File Offset: 0x00009C90
		public async Task<long> GetPingToServer(string IpAddress)
		{
			long result;
			try
			{
				using (Ping ping = new Ping())
				{
					PingReply pingReply = await ping.SendPingAsync(IpAddress, (int)TimeSpan.FromSeconds(15.0).TotalMilliseconds);
					result = ((pingReply.Status != IPStatus.Success) ? -1L : pingReply.RoundtripTime);
				}
			}
			catch (Exception)
			{
				result = -1L;
			}
			return result;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000BAD5 File Offset: 0x00009CD5
		private void AssertCanPerformLobbyActions()
		{
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public async Task<bool> SendPSPlayerJoinedToPlayerSessionMessage(ulong inviterAccountId)
		{
			PSPlayerJoinedToPlayerSessionMessage message = new PSPlayerJoinedToPlayerSessionMessage(inviterAccountId);
			return (await base.CallFunction<PSPlayerJoinedToPlayerSessionMessageResult>(message)).Successful;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000BB28 File Offset: 0x00009D28
		private Task WaitForPendingRequestCompletion(LobbyClient.PendingRequest requestType, PlayerId playerId)
		{
			Task result;
			if (this._pendingPlayerRequests.TryGetValue(new ValueTuple<LobbyClient.PendingRequest, PlayerId>(requestType, playerId), out result))
			{
				return result;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000BB54 File Offset: 0x00009D54
		private async Task<T> CreatePendingRequest<T>(LobbyClient.PendingRequest requestType, PlayerId playerId, Task<T> requestTask)
		{
			ValueTuple<LobbyClient.PendingRequest, PlayerId> key = new ValueTuple<LobbyClient.PendingRequest, PlayerId>(requestType, playerId);
			T result;
			try
			{
				this._pendingPlayerRequests[key] = requestTask;
				result = await requestTask;
			}
			finally
			{
				this._pendingPlayerRequests.Remove(key);
			}
			return result;
		}

		// Token: 0x040002D9 RID: 729
		public const string TestRegionCode = "Test";

		// Token: 0x040002DA RID: 730
		private static readonly int ServerStatusCheckDelay = 30000;

		// Token: 0x040002DB RID: 731
		private static int _friendListCheckDelay;

		// Token: 0x040002DC RID: 732
		private static readonly int CheckForCustomGamesCount = 5;

		// Token: 0x040002DD RID: 733
		private static readonly int CheckForCustomGamesDelay = 5000;

		// Token: 0x040002DE RID: 734
		private ILobbyClientSessionHandler _handler;

		// Token: 0x040002DF RID: 735
		private readonly Stopwatch _serverStatusTimer;

		// Token: 0x040002E0 RID: 736
		private readonly Stopwatch _friendListTimer;

		// Token: 0x040002E5 RID: 741
		private List<string> _ownedCosmetics;

		// Token: 0x040002E6 RID: 742
		private Dictionary<string, List<string>> _usedCosmetics;

		// Token: 0x040002E9 RID: 745
		private ServerStatus _serverStatus;

		// Token: 0x040002EA RID: 746
		private DateTime _matchmakerBlockedTime;

		// Token: 0x040002EB RID: 747
		private TextObject _logOutReason;

		// Token: 0x040002EC RID: 748
		private LobbyClient.State _state;

		// Token: 0x040002ED RID: 749
		private string _userName;

		// Token: 0x040002EE RID: 750
		private PlayerId _playerId;

		// Token: 0x040002F2 RID: 754
		private List<ModuleInfoModel> _loadedUnofficialModules;

		// Token: 0x04000303 RID: 771
		private TimedDictionaryCache<PlayerId, GameTypeRankInfo[]> _cachedRankInfos;

		// Token: 0x04000304 RID: 772
		private TimedDictionaryCache<PlayerId, PlayerStatsBase[]> _cachedPlayerStats;

		// Token: 0x04000305 RID: 773
		private TimedDictionaryCache<PlayerId, PlayerData> _cachedPlayerDatas;

		// Token: 0x04000306 RID: 774
		private TimedDictionaryCache<PlayerId, string> _cachedPlayerBannerlordIDs;

		// Token: 0x04000307 RID: 775
		private Dictionary<ValueTuple<LobbyClient.PendingRequest, PlayerId>, Task> _pendingPlayerRequests;

		// Token: 0x020001A5 RID: 421
		public enum State
		{
			// Token: 0x040005AB RID: 1451
			Idle,
			// Token: 0x040005AC RID: 1452
			Working,
			// Token: 0x040005AD RID: 1453
			Connected,
			// Token: 0x040005AE RID: 1454
			SessionRequested,
			// Token: 0x040005AF RID: 1455
			AtLobby,
			// Token: 0x040005B0 RID: 1456
			SearchingToRejoinBattle,
			// Token: 0x040005B1 RID: 1457
			RequestingToSearchBattle,
			// Token: 0x040005B2 RID: 1458
			RequestingToCancelSearchBattle,
			// Token: 0x040005B3 RID: 1459
			SearchingBattle,
			// Token: 0x040005B4 RID: 1460
			AtBattle,
			// Token: 0x040005B5 RID: 1461
			QuittingFromBattle,
			// Token: 0x040005B6 RID: 1462
			WaitingToCreatePremadeGame,
			// Token: 0x040005B7 RID: 1463
			WaitingToJoinPremadeGame,
			// Token: 0x040005B8 RID: 1464
			WaitingToRegisterCustomGame,
			// Token: 0x040005B9 RID: 1465
			HostingCustomGame,
			// Token: 0x040005BA RID: 1466
			WaitingToJoinCustomGame,
			// Token: 0x040005BB RID: 1467
			InCustomGame
		}

		// Token: 0x020001A6 RID: 422
		private enum PendingRequest
		{
			// Token: 0x040005BD RID: 1469
			RankInfo,
			// Token: 0x040005BE RID: 1470
			PlayerData,
			// Token: 0x040005BF RID: 1471
			BannerlordID
		}
	}
}
