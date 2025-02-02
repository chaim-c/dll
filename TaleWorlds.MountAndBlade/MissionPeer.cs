using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F5 RID: 757
	public class MissionPeer : PeerComponent
	{
		// Token: 0x14000083 RID: 131
		// (add) Token: 0x060028F3 RID: 10483 RVA: 0x0009D888 File Offset: 0x0009BA88
		// (remove) Token: 0x060028F4 RID: 10484 RVA: 0x0009D8BC File Offset: 0x0009BABC
		public static event MissionPeer.OnUpdateEquipmentSetIndexEventDelegate OnEquipmentIndexRefreshed;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x060028F5 RID: 10485 RVA: 0x0009D8F0 File Offset: 0x0009BAF0
		// (remove) Token: 0x060028F6 RID: 10486 RVA: 0x0009D924 File Offset: 0x0009BB24
		public static event MissionPeer.OnPerkUpdateEventDelegate OnPerkSelectionUpdated;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x060028F7 RID: 10487 RVA: 0x0009D958 File Offset: 0x0009BB58
		// (remove) Token: 0x060028F8 RID: 10488 RVA: 0x0009D98C File Offset: 0x0009BB8C
		public static event MissionPeer.OnTeamChangedDelegate OnPreTeamChanged;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x060028F9 RID: 10489 RVA: 0x0009D9C0 File Offset: 0x0009BBC0
		// (remove) Token: 0x060028FA RID: 10490 RVA: 0x0009D9F4 File Offset: 0x0009BBF4
		public static event MissionPeer.OnTeamChangedDelegate OnTeamChanged;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060028FB RID: 10491 RVA: 0x0009DA28 File Offset: 0x0009BC28
		// (remove) Token: 0x060028FC RID: 10492 RVA: 0x0009DA60 File Offset: 0x0009BC60
		private event MissionPeer.OnCultureChangedDelegate OnCultureChanged;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060028FD RID: 10493 RVA: 0x0009DA98 File Offset: 0x0009BC98
		// (remove) Token: 0x060028FE RID: 10494 RVA: 0x0009DACC File Offset: 0x0009BCCC
		public static event MissionPeer.OnPlayerKilledDelegate OnPlayerKilled;

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x0009DAFF File Offset: 0x0009BCFF
		// (set) Token: 0x06002900 RID: 10496 RVA: 0x0009DB07 File Offset: 0x0009BD07
		public DateTime JoinTime { get; internal set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x0009DB10 File Offset: 0x0009BD10
		// (set) Token: 0x06002902 RID: 10498 RVA: 0x0009DB18 File Offset: 0x0009BD18
		public bool EquipmentUpdatingExpired { get; set; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x0009DB21 File Offset: 0x0009BD21
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x0009DB29 File Offset: 0x0009BD29
		public bool TeamInitialPerkInfoReady { get; private set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x0009DB32 File Offset: 0x0009BD32
		// (set) Token: 0x06002906 RID: 10502 RVA: 0x0009DB3A File Offset: 0x0009BD3A
		public bool HasSpawnedAgentVisuals { get; set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x0009DB43 File Offset: 0x0009BD43
		// (set) Token: 0x06002908 RID: 10504 RVA: 0x0009DB4B File Offset: 0x0009BD4B
		public int SelectedTroopIndex
		{
			get
			{
				return this._selectedTroopIndex;
			}
			set
			{
				if (this._selectedTroopIndex != value)
				{
					this._selectedTroopIndex = value;
					this.ResetSelectedPerks();
					MissionPeer.OnUpdateEquipmentSetIndexEventDelegate onEquipmentIndexRefreshed = MissionPeer.OnEquipmentIndexRefreshed;
					if (onEquipmentIndexRefreshed == null)
					{
						return;
					}
					onEquipmentIndexRefreshed(this, value);
				}
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x0009DB74 File Offset: 0x0009BD74
		// (set) Token: 0x0600290A RID: 10506 RVA: 0x0009DB7C File Offset: 0x0009BD7C
		public int NextSelectedTroopIndex { get; set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x0009DB85 File Offset: 0x0009BD85
		public MissionRepresentativeBase Representative
		{
			get
			{
				if (this._representative == null)
				{
					this._representative = base.Peer.GetComponent<MissionRepresentativeBase>();
				}
				return this._representative;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x0009DBA6 File Offset: 0x0009BDA6
		public MBReadOnlyList<int[]> Perks
		{
			get
			{
				return this._perks;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x0009DBB0 File Offset: 0x0009BDB0
		public string DisplayedName
		{
			get
			{
				if (GameNetwork.IsDedicatedServer)
				{
					return base.Name;
				}
				if (NetworkMain.CommunityClient.IsInGame)
				{
					return base.Name;
				}
				if (NetworkMain.GameClient.HasUserGeneratedContentPrivilege && (NetworkMain.GameClient.IsKnownPlayer(base.Peer.Id) || !BannerlordConfig.EnableGenericNames))
				{
					VirtualPlayer peer = base.Peer;
					return ((peer != null) ? peer.UserName : null) ?? "";
				}
				if (this.Culture == null || MultiplayerClassDivisions.GetMPHeroClassForPeer(this, false) == null)
				{
					return new TextObject("{=RN6zHak0}Player", null).ToString();
				}
				return MultiplayerClassDivisions.GetMPHeroClassForPeer(this, false).TroopName.ToString();
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x0009DC58 File Offset: 0x0009BE58
		public MBReadOnlyList<MPPerkObject> SelectedPerks
		{
			get
			{
				if (this.SelectedTroopIndex < 0 || this.Team == null || this.Team.Side == BattleSideEnum.None)
				{
					return new MBList<MPPerkObject>();
				}
				if ((this._selectedPerks.Item2 == null || this.SelectedTroopIndex != this._selectedPerks.Item1 || this._selectedPerks.Item2.Count < 3) && !this.RefreshSelectedPerks())
				{
					return new MBReadOnlyList<MPPerkObject>();
				}
				return this._selectedPerks.Item2;
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0009DCD8 File Offset: 0x0009BED8
		public MissionPeer()
		{
			this.SpawnTimer = new Timer(Mission.Current.CurrentTime, 3f, false);
			this._selectedPerks = new ValueTuple<int, MBList<MPPerkObject>>(0, null);
			this._perks = new MBList<int[]>();
			for (int i = 0; i < 16; i++)
			{
				int[] item = new int[3];
				this._perks.Add(item);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x0009DD67 File Offset: 0x0009BF67
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x0009DD6F File Offset: 0x0009BF6F
		public Timer SpawnTimer { get; internal set; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x0009DD78 File Offset: 0x0009BF78
		// (set) Token: 0x06002913 RID: 10515 RVA: 0x0009DD80 File Offset: 0x0009BF80
		public bool HasSpawnTimerExpired { get; set; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x0009DD89 File Offset: 0x0009BF89
		// (set) Token: 0x06002915 RID: 10517 RVA: 0x0009DD91 File Offset: 0x0009BF91
		public BasicCultureObject VotedForBan { get; private set; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x0009DD9A File Offset: 0x0009BF9A
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x0009DDA2 File Offset: 0x0009BFA2
		public BasicCultureObject VotedForSelection { get; private set; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x0009DDAB File Offset: 0x0009BFAB
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x0009DDB3 File Offset: 0x0009BFB3
		public bool WantsToSpawnAsBot { get; set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x0009DDBC File Offset: 0x0009BFBC
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x0009DDC4 File Offset: 0x0009BFC4
		public int SpawnCountThisRound { get; set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x0009DDCD File Offset: 0x0009BFCD
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x0009DDD5 File Offset: 0x0009BFD5
		public int RequestedKickPollCount { get; private set; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x0009DDDE File Offset: 0x0009BFDE
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x0009DDE6 File Offset: 0x0009BFE6
		public int KillCount
		{
			get
			{
				return this._killCount;
			}
			internal set
			{
				this._killCount = MBMath.ClampInt(value, -1000, 100000);
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x0009DDFE File Offset: 0x0009BFFE
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x0009DE06 File Offset: 0x0009C006
		public int AssistCount
		{
			get
			{
				return this._assistCount;
			}
			internal set
			{
				this._assistCount = MBMath.ClampInt(value, -1000, 100000);
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x0009DE1E File Offset: 0x0009C01E
		// (set) Token: 0x06002923 RID: 10531 RVA: 0x0009DE26 File Offset: 0x0009C026
		public int DeathCount
		{
			get
			{
				return this._deathCount;
			}
			internal set
			{
				this._deathCount = MBMath.ClampInt(value, -1000, 100000);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x0009DE3E File Offset: 0x0009C03E
		// (set) Token: 0x06002925 RID: 10533 RVA: 0x0009DE46 File Offset: 0x0009C046
		public int Score
		{
			get
			{
				return this._score;
			}
			internal set
			{
				this._score = MBMath.ClampInt(value, -1000000, 1000000);
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x0009DE5E File Offset: 0x0009C05E
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x0009DE66 File Offset: 0x0009C066
		public int BotsUnderControlAlive
		{
			get
			{
				return this._botsUnderControlAlive;
			}
			set
			{
				if (this._botsUnderControlAlive != value)
				{
					this._botsUnderControlAlive = value;
					MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this);
					if (perkHandler == null)
					{
						return;
					}
					perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.AliveBotCountChange);
				}
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x0009DE8A File Offset: 0x0009C08A
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x0009DE92 File Offset: 0x0009C092
		public int BotsUnderControlTotal { get; internal set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x0009DE9B File Offset: 0x0009C09B
		public bool IsControlledAgentActive
		{
			get
			{
				return this.ControlledAgent != null && this.ControlledAgent.IsActive();
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x0009DEB2 File Offset: 0x0009C0B2
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x0009DEC0 File Offset: 0x0009C0C0
		public Agent ControlledAgent
		{
			get
			{
				return this.GetNetworkPeer().ControlledAgent;
			}
			set
			{
				NetworkCommunicator networkPeer = this.GetNetworkPeer();
				if (networkPeer.ControlledAgent != value)
				{
					this.ResetSelectedPerks();
					Agent controlledAgent = networkPeer.ControlledAgent;
					networkPeer.ControlledAgent = value;
					if (controlledAgent != null && controlledAgent.MissionPeer == this && controlledAgent.IsActive())
					{
						controlledAgent.MissionPeer = null;
					}
					if (networkPeer.ControlledAgent != null && networkPeer.ControlledAgent.MissionPeer != this)
					{
						networkPeer.ControlledAgent.MissionPeer = this;
					}
					MissionRepresentativeBase component = networkPeer.VirtualPlayer.GetComponent<MissionRepresentativeBase>();
					if (component != null)
					{
						component.SetAgent(value);
					}
					if (value != null)
					{
						MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this);
						if (perkHandler == null)
						{
							return;
						}
						perkHandler.OnEvent(value, MPPerkCondition.PerkEventFlags.PeerControlledAgentChange);
					}
				}
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x0009DF5A File Offset: 0x0009C15A
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x0009DF62 File Offset: 0x0009C162
		public Agent FollowedAgent
		{
			get
			{
				return this._followedAgent;
			}
			set
			{
				if (this._followedAgent != value)
				{
					this._followedAgent = value;
					if (GameNetwork.IsClient)
					{
						GameNetwork.BeginModuleEventAsClient();
						Agent followedAgent = this._followedAgent;
						GameNetwork.WriteMessage(new SetFollowedAgent((followedAgent != null) ? followedAgent.Index : -1));
						GameNetwork.EndModuleEventAsClient();
					}
				}
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x0009DFA1 File Offset: 0x0009C1A1
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x0009DFAC File Offset: 0x0009C1AC
		public Team Team
		{
			get
			{
				return this._team;
			}
			set
			{
				if (this._team != value)
				{
					if (MissionPeer.OnPreTeamChanged != null)
					{
						MissionPeer.OnPreTeamChanged(this.GetNetworkPeer(), this._team, value);
					}
					Team team = this._team;
					this._team = value;
					string str = "Set the team to: ";
					Team team2 = this._team;
					Debug.Print(str + (((team2 != null) ? team2.Side.ToString() : null) ?? "null") + ", for peer: " + base.Name, 0, Debug.DebugColor.White, 17592186044416UL);
					this._controlledFormation = null;
					if (this._team != null)
					{
						if (GameNetwork.IsServer)
						{
							MBAPI.IMBPeer.SetTeam(base.Peer.Index, this._team.MBTeam.Index);
							GameNetwork.BeginBroadcastModuleEvent();
							GameNetwork.WriteMessage(new SetPeerTeam(this.GetNetworkPeer(), this._team.TeamIndex));
							GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
						}
						if (MissionPeer.OnTeamChanged != null)
						{
							MissionPeer.OnTeamChanged(this.GetNetworkPeer(), team, this._team);
							return;
						}
					}
					else if (GameNetwork.IsServer)
					{
						MBAPI.IMBPeer.SetTeam(base.Peer.Index, -1);
					}
				}
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x0009E0DF File Offset: 0x0009C2DF
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x0009E0E8 File Offset: 0x0009C2E8
		public BasicCultureObject Culture
		{
			get
			{
				return this._culture;
			}
			set
			{
				BasicCultureObject culture = this._culture;
				this._culture = value;
				if (GameNetwork.IsServerOrRecorder)
				{
					this.TeamInitialPerkInfoReady = base.Peer.IsMine;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new ChangeCulture(this, this._culture));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				if (this.OnCultureChanged != null)
				{
					this.OnCultureChanged(this._culture);
				}
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x0009E152 File Offset: 0x0009C352
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x0009E15A File Offset: 0x0009C35A
		public Formation ControlledFormation
		{
			get
			{
				return this._controlledFormation;
			}
			set
			{
				if (this._controlledFormation != value)
				{
					this._controlledFormation = value;
				}
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x0009E16C File Offset: 0x0009C36C
		public bool IsAgentAliveForChatting
		{
			get
			{
				MissionPeer component = base.GetComponent<MissionPeer>();
				return component != null && (this.IsControlledAgentActive || component.HasSpawnedAgentVisuals);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x0009E195 File Offset: 0x0009C395
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x0009E19D File Offset: 0x0009C39D
		public bool IsMutedFromPlatform { get; private set; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x0009E1A6 File Offset: 0x0009C3A6
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x0009E1AE File Offset: 0x0009C3AE
		public bool IsMuted { get; private set; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x0009E1B7 File Offset: 0x0009C3B7
		public bool IsMutedFromGameOrPlatform
		{
			get
			{
				return this.IsMutedFromPlatform || this.IsMuted;
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0009E1C9 File Offset: 0x0009C3C9
		public void SetMutedFromPlatform(bool isMuted)
		{
			this.IsMutedFromPlatform = isMuted;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x0009E1D2 File Offset: 0x0009C3D2
		public void SetMuted(bool isMuted)
		{
			this.IsMuted = isMuted;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0009E1DB File Offset: 0x0009C3DB
		public void ResetRequestedKickPollCount()
		{
			this.RequestedKickPollCount = 0;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0009E1E4 File Offset: 0x0009C3E4
		public void IncrementRequestedKickPollCount()
		{
			int requestedKickPollCount = this.RequestedKickPollCount;
			this.RequestedKickPollCount = requestedKickPollCount + 1;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x0009E201 File Offset: 0x0009C401
		public int GetSelectedPerkIndexWithPerkListIndex(int troopIndex, int perkListIndex)
		{
			return this._perks[troopIndex][perkListIndex];
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0009E214 File Offset: 0x0009C414
		public bool SelectPerk(int perkListIndex, int perkIndex, int enforcedSelectedTroopIndex = -1)
		{
			if (this.SelectedTroopIndex >= 0 && enforcedSelectedTroopIndex >= 0 && this.SelectedTroopIndex != enforcedSelectedTroopIndex)
			{
				Debug.Print("SelectedTroopIndex < 0 || enforcedSelectedTroopIndex < 0 || SelectedTroopIndex == enforcedSelectedTroopIndex", 0, Debug.DebugColor.White, 17179869184UL);
				Debug.Print(string.Format("SelectedTroopIndex: {0} enforcedSelectedTroopIndex: {1}", this.SelectedTroopIndex, enforcedSelectedTroopIndex), 0, Debug.DebugColor.White, 17179869184UL);
			}
			int num = (enforcedSelectedTroopIndex >= 0) ? enforcedSelectedTroopIndex : this.SelectedTroopIndex;
			if (perkIndex != this._perks[num][perkListIndex])
			{
				this._perks[num][perkListIndex] = perkIndex;
				if (this.GetNetworkPeer().IsMine)
				{
					List<MultiplayerClassDivisions.MPHeroClass> list = MultiplayerClassDivisions.GetMPHeroClasses(this.Culture).ToList<MultiplayerClassDivisions.MPHeroClass>();
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						if (num == i)
						{
							MultiplayerClassDivisions.MPHeroClass currentHeroClass = list[i];
							List<MPPerkSelectionManager.MPPerkSelection> list2 = new List<MPPerkSelectionManager.MPPerkSelection>();
							for (int j = 0; j < 3; j++)
							{
								list2.Add(new MPPerkSelectionManager.MPPerkSelection(this._perks[i][j], j));
							}
							MPPerkSelectionManager.Instance.SetSelectionsForHeroClassTemporarily(currentHeroClass, list2);
							break;
						}
					}
				}
				if (num == this.SelectedTroopIndex)
				{
					this.ResetSelectedPerks();
				}
				MissionPeer.OnPerkUpdateEventDelegate onPerkSelectionUpdated = MissionPeer.OnPerkSelectionUpdated;
				if (onPerkSelectionUpdated != null)
				{
					onPerkSelectionUpdated(this);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0009E350 File Offset: 0x0009C550
		public void HandleVoteChange(CultureVoteTypes voteType, BasicCultureObject culture)
		{
			if (voteType != CultureVoteTypes.Ban)
			{
				if (voteType == CultureVoteTypes.Select)
				{
					this.VotedForSelection = culture;
				}
			}
			else
			{
				this.VotedForBan = culture;
			}
			if (GameNetwork.IsServer)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new CultureVoteServer(this.GetNetworkPeer(), voteType, (voteType == CultureVoteTypes.Ban) ? this.VotedForBan : this.VotedForSelection));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0009E3AC File Offset: 0x0009C5AC
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (base.IsMine)
			{
				MPPerkSelectionManager.Instance.TryToApplyAndSavePendingChanges();
			}
			this.ResetKillRegistry();
			if (this.HasSpawnedAgentVisuals && Mission.Current != null)
			{
				MultiplayerMissionAgentVisualSpawnComponent missionBehavior = Mission.Current.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
				if (missionBehavior != null)
				{
					missionBehavior.RemoveAgentVisuals(this, false);
				}
				this.HasSpawnedAgentVisuals = false;
				this.OnCultureChanged -= this.CultureChanged;
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0009E416 File Offset: 0x0009C616
		public override void OnInitialize()
		{
			base.OnInitialize();
			this.OnCultureChanged += this.CultureChanged;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0009E430 File Offset: 0x0009C630
		public int GetAmountOfAgentVisualsForPeer()
		{
			return this._visuals.Count((PeerVisualsHolder v) => v != null);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0009E45C File Offset: 0x0009C65C
		public PeerVisualsHolder GetVisuals(int visualIndex)
		{
			if (this._visuals.Count <= 0)
			{
				return null;
			}
			return this._visuals[visualIndex];
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0009E47C File Offset: 0x0009C67C
		public void ClearVisuals(int visualIndex)
		{
			if (visualIndex < this._visuals.Count && this._visuals[visualIndex] != null)
			{
				if (!GameNetwork.IsDedicatedServer)
				{
					MBAgentVisuals visuals = this._visuals[visualIndex].AgentVisuals.GetVisuals();
					visuals.ClearVisualComponents(true);
					visuals.ClearAllWeaponMeshes();
					visuals.Reset();
					if (this._visuals[visualIndex].MountAgentVisuals != null)
					{
						MBAgentVisuals visuals2 = this._visuals[visualIndex].MountAgentVisuals.GetVisuals();
						visuals2.ClearVisualComponents(true);
						visuals2.ClearAllWeaponMeshes();
						visuals2.Reset();
					}
				}
				this._visuals[visualIndex] = null;
			}
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x0009E520 File Offset: 0x0009C720
		public void ClearAllVisuals(bool freeResources = false)
		{
			if (this._visuals != null)
			{
				for (int i = this._visuals.Count - 1; i >= 0; i--)
				{
					if (this._visuals[i] != null)
					{
						this.ClearVisuals(i);
					}
				}
				if (freeResources)
				{
					this._visuals = null;
				}
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0009E56C File Offset: 0x0009C76C
		public void OnVisualsSpawned(PeerVisualsHolder visualsHolder, int visualIndex)
		{
			if (visualIndex >= this._visuals.Count)
			{
				int num = visualIndex - this._visuals.Count;
				for (int i = 0; i < num + 1; i++)
				{
					this._visuals.Add(null);
				}
			}
			this._visuals[visualIndex] = visualsHolder;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0009E5BC File Offset: 0x0009C7BC
		public IEnumerable<IAgentVisual> GetAllAgentVisualsForPeer()
		{
			int count = this.GetAmountOfAgentVisualsForPeer();
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return this.GetVisuals(i).AgentVisuals;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x0009E5CC File Offset: 0x0009C7CC
		public IAgentVisual GetAgentVisualForPeer(int visualsIndex)
		{
			IAgentVisual agentVisual;
			return this.GetAgentVisualForPeer(visualsIndex, out agentVisual);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0009E5E4 File Offset: 0x0009C7E4
		public IAgentVisual GetAgentVisualForPeer(int visualsIndex, out IAgentVisual mountAgentVisuals)
		{
			PeerVisualsHolder visuals = this.GetVisuals(visualsIndex);
			mountAgentVisuals = ((visuals != null) ? visuals.MountAgentVisuals : null);
			if (visuals == null)
			{
				return null;
			}
			return visuals.AgentVisuals;
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0009E614 File Offset: 0x0009C814
		public void TickInactivityStatus()
		{
			NetworkCommunicator networkPeer = this.GetNetworkPeer();
			if (!networkPeer.IsMine)
			{
				if (this.ControlledAgent != null && this.ControlledAgent.IsActive())
				{
					if (this._lastActiveTime == MissionTime.Zero)
					{
						this._lastActiveTime = MissionTime.Now;
						this._previousActivityStatus = ValueTuple.Create<Agent.MovementControlFlag, Vec2, Vec3>(this.ControlledAgent.MovementFlags, this.ControlledAgent.MovementInputVector, this.ControlledAgent.LookDirection);
						this._inactiveWarningGiven = false;
						return;
					}
					ValueTuple<Agent.MovementControlFlag, Vec2, Vec3> valueTuple = ValueTuple.Create<Agent.MovementControlFlag, Vec2, Vec3>(this.ControlledAgent.MovementFlags, this.ControlledAgent.MovementInputVector, this.ControlledAgent.LookDirection);
					if (this._previousActivityStatus.Item1 != valueTuple.Item1 || this._previousActivityStatus.Item2.DistanceSquared(valueTuple.Item2) > 1E-05f || this._previousActivityStatus.Item3.DistanceSquared(valueTuple.Item3) > 1E-05f)
					{
						this._lastActiveTime = MissionTime.Now;
						this._previousActivityStatus = valueTuple;
						this._inactiveWarningGiven = false;
					}
					if (this._lastActiveTime.ElapsedSeconds > 180f)
					{
						DisconnectInfo disconnectInfo = networkPeer.PlayerConnectionInfo.GetParameter<DisconnectInfo>("DisconnectInfo") ?? new DisconnectInfo();
						disconnectInfo.Type = DisconnectType.Inactivity;
						networkPeer.PlayerConnectionInfo.AddParameter("DisconnectInfo", disconnectInfo);
						GameNetwork.AddNetworkPeerToDisconnectAsServer(networkPeer);
						return;
					}
					if (this._lastActiveTime.ElapsedSeconds > 120f && !this._inactiveWarningGiven)
					{
						MultiplayerGameNotificationsComponent missionBehavior = Mission.Current.GetMissionBehavior<MultiplayerGameNotificationsComponent>();
						if (missionBehavior != null)
						{
							missionBehavior.PlayerIsInactive(this.GetNetworkPeer());
						}
						this._inactiveWarningGiven = true;
						return;
					}
				}
				else
				{
					this._lastActiveTime = MissionTime.Now;
					this._inactiveWarningGiven = false;
				}
			}
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		public void OnKillAnotherPeer(MissionPeer victimPeer)
		{
			if (victimPeer != null)
			{
				if (!this._numberOfTimesPeerKilledPerPeer.ContainsKey(victimPeer))
				{
					this._numberOfTimesPeerKilledPerPeer.Add(victimPeer, 1);
				}
				else
				{
					Dictionary<MissionPeer, int> numberOfTimesPeerKilledPerPeer = this._numberOfTimesPeerKilledPerPeer;
					int num = numberOfTimesPeerKilledPerPeer[victimPeer];
					numberOfTimesPeerKilledPerPeer[victimPeer] = num + 1;
				}
				MissionPeer.OnPlayerKilledDelegate onPlayerKilled = MissionPeer.OnPlayerKilled;
				if (onPlayerKilled == null)
				{
					return;
				}
				onPlayerKilled(this, victimPeer);
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x0009E820 File Offset: 0x0009CA20
		public void OverrideCultureWithTeamCulture()
		{
			MultiplayerOptions.OptionType optionType = (this.Team.Side == BattleSideEnum.Attacker) ? MultiplayerOptions.OptionType.CultureTeam1 : MultiplayerOptions.OptionType.CultureTeam2;
			this.Culture = MBObjectManager.Instance.GetObject<BasicCultureObject>(optionType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0009E859 File Offset: 0x0009CA59
		public int GetNumberOfTimesPeerKilledPeer(MissionPeer killedPeer)
		{
			if (this._numberOfTimesPeerKilledPerPeer.ContainsKey(killedPeer))
			{
				return this._numberOfTimesPeerKilledPerPeer[killedPeer];
			}
			return 0;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x0009E877 File Offset: 0x0009CA77
		public void ResetKillRegistry()
		{
			this._numberOfTimesPeerKilledPerPeer.Clear();
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0009E884 File Offset: 0x0009CA84
		public bool RefreshSelectedPerks()
		{
			MBList<MPPerkObject> mblist = new MBList<MPPerkObject>();
			List<List<IReadOnlyPerkObject>> availablePerksForPeer = MultiplayerClassDivisions.GetAvailablePerksForPeer(this);
			if (availablePerksForPeer.Count == 3)
			{
				for (int i = 0; i < 3; i++)
				{
					int num = this._perks[this.SelectedTroopIndex][i];
					if (availablePerksForPeer[i].Count > 0)
					{
						mblist.Add(availablePerksForPeer[i][(num >= 0 && num < availablePerksForPeer[i].Count) ? num : 0].Clone(this));
					}
				}
				this._selectedPerks = new ValueTuple<int, MBList<MPPerkObject>>(this.SelectedTroopIndex, mblist);
				return true;
			}
			return false;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x0009E91C File Offset: 0x0009CB1C
		private void ResetSelectedPerks()
		{
			if (this._selectedPerks.Item2 != null)
			{
				foreach (MPPerkObject mpperkObject in this._selectedPerks.Item2)
				{
					mpperkObject.Reset();
				}
			}
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0009E980 File Offset: 0x0009CB80
		private void CultureChanged(BasicCultureObject newCulture)
		{
			List<MultiplayerClassDivisions.MPHeroClass> list = MultiplayerClassDivisions.GetMPHeroClasses(newCulture).ToList<MultiplayerClassDivisions.MPHeroClass>();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				MultiplayerClassDivisions.MPHeroClass currentHeroClass = list[i];
				List<MPPerkSelectionManager.MPPerkSelection> selectionsForHeroClass = MPPerkSelectionManager.Instance.GetSelectionsForHeroClass(currentHeroClass);
				if (selectionsForHeroClass != null)
				{
					int count2 = selectionsForHeroClass.Count;
					for (int j = 0; j < count2; j++)
					{
						MPPerkSelectionManager.MPPerkSelection mpperkSelection = selectionsForHeroClass[j];
						this._perks[i][mpperkSelection.ListIndex] = mpperkSelection.Index;
					}
				}
				else
				{
					for (int k = 0; k < 3; k++)
					{
						this._perks[i][k] = 0;
					}
				}
			}
			if (base.IsMine && GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new TeamInitialPerkInfoMessage(this._perks[this.SelectedTroopIndex]));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0009EA60 File Offset: 0x0009CC60
		public void OnTeamInitialPerkInfoReceived(int[] perks)
		{
			for (int i = 0; i < 3; i++)
			{
				this.SelectPerk(i, perks[i], -1);
			}
			this.TeamInitialPerkInfoReady = true;
		}

		// Token: 0x04000FC4 RID: 4036
		public const int NumberOfPerkLists = 3;

		// Token: 0x04000FC5 RID: 4037
		public const int MaxNumberOfTroopTypesPerCulture = 16;

		// Token: 0x04000FC6 RID: 4038
		private const float InactivityKickInSeconds = 180f;

		// Token: 0x04000FC7 RID: 4039
		private const float InactivityWarnInSeconds = 120f;

		// Token: 0x04000FC8 RID: 4040
		public const int MinKDACount = -1000;

		// Token: 0x04000FC9 RID: 4041
		public const int MaxKDACount = 100000;

		// Token: 0x04000FCA RID: 4042
		public const int MinScore = -1000000;

		// Token: 0x04000FCB RID: 4043
		public const int MaxScore = 1000000;

		// Token: 0x04000FCC RID: 4044
		public const int MinSpawnTimer = 3;

		// Token: 0x04000FCD RID: 4045
		public int CaptainBeingDetachedThreshold = 125;

		// Token: 0x04000FD4 RID: 4052
		private List<PeerVisualsHolder> _visuals = new List<PeerVisualsHolder>();

		// Token: 0x04000FD5 RID: 4053
		private Dictionary<MissionPeer, int> _numberOfTimesPeerKilledPerPeer = new Dictionary<MissionPeer, int>();

		// Token: 0x04000FD6 RID: 4054
		private MissionTime _lastActiveTime = MissionTime.Zero;

		// Token: 0x04000FD7 RID: 4055
		private ValueTuple<Agent.MovementControlFlag, Vec2, Vec3> _previousActivityStatus;

		// Token: 0x04000FD8 RID: 4056
		private bool _inactiveWarningGiven;

		// Token: 0x04000FDD RID: 4061
		private int _selectedTroopIndex;

		// Token: 0x04000FDF RID: 4063
		private Agent _followedAgent;

		// Token: 0x04000FE0 RID: 4064
		private Team _team;

		// Token: 0x04000FE1 RID: 4065
		private BasicCultureObject _culture;

		// Token: 0x04000FE2 RID: 4066
		private Formation _controlledFormation;

		// Token: 0x04000FE3 RID: 4067
		private MissionRepresentativeBase _representative;

		// Token: 0x04000FE4 RID: 4068
		private readonly MBList<int[]> _perks;

		// Token: 0x04000FE5 RID: 4069
		private int _killCount;

		// Token: 0x04000FE6 RID: 4070
		private int _assistCount;

		// Token: 0x04000FE7 RID: 4071
		private int _deathCount;

		// Token: 0x04000FE8 RID: 4072
		private int _score;

		// Token: 0x04000FE9 RID: 4073
		private ValueTuple<int, MBList<MPPerkObject>> _selectedPerks;

		// Token: 0x04000FF1 RID: 4081
		private int _botsUnderControlAlive;

		// Token: 0x020005AA RID: 1450
		// (Invoke) Token: 0x06003A8A RID: 14986
		public delegate void OnUpdateEquipmentSetIndexEventDelegate(MissionPeer lobbyPeer, int equipmentSetIndex);

		// Token: 0x020005AB RID: 1451
		// (Invoke) Token: 0x06003A8E RID: 14990
		public delegate void OnPerkUpdateEventDelegate(MissionPeer peer);

		// Token: 0x020005AC RID: 1452
		// (Invoke) Token: 0x06003A92 RID: 14994
		public delegate void OnTeamChangedDelegate(NetworkCommunicator peer, Team previousTeam, Team newTeam);

		// Token: 0x020005AD RID: 1453
		// (Invoke) Token: 0x06003A96 RID: 14998
		public delegate void OnCultureChangedDelegate(BasicCultureObject newCulture);

		// Token: 0x020005AE RID: 1454
		// (Invoke) Token: 0x06003A9A RID: 15002
		public delegate void OnPlayerKilledDelegate(MissionPeer killerPeer, MissionPeer killedPeer);
	}
}
