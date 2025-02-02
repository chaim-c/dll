using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B1 RID: 689
	public class MultiplayerTeamSelectComponent : MissionNetwork
	{
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x0600259F RID: 9631 RVA: 0x0008EFF4 File Offset: 0x0008D1F4
		// (remove) Token: 0x060025A0 RID: 9632 RVA: 0x0008F02C File Offset: 0x0008D22C
		public event MultiplayerTeamSelectComponent.OnSelectingTeamDelegate OnSelectingTeam;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060025A1 RID: 9633 RVA: 0x0008F064 File Offset: 0x0008D264
		// (remove) Token: 0x060025A2 RID: 9634 RVA: 0x0008F09C File Offset: 0x0008D29C
		public event Action OnMyTeamChange;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060025A3 RID: 9635 RVA: 0x0008F0D4 File Offset: 0x0008D2D4
		// (remove) Token: 0x060025A4 RID: 9636 RVA: 0x0008F10C File Offset: 0x0008D30C
		public event Action OnUpdateTeams;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060025A5 RID: 9637 RVA: 0x0008F144 File Offset: 0x0008D344
		// (remove) Token: 0x060025A6 RID: 9638 RVA: 0x0008F17C File Offset: 0x0008D37C
		public event Action OnUpdateFriendsPerTeam;

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x0008F1B1 File Offset: 0x0008D3B1
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x0008F1B9 File Offset: 0x0008D3B9
		public bool TeamSelectionEnabled { get; private set; }

		// Token: 0x060025AA RID: 9642 RVA: 0x0008F1CA File Offset: 0x0008D3CA
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionNetworkComponent = base.Mission.GetMissionBehavior<MissionNetworkComponent>();
			this._gameModeServer = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBase>();
			if (BannerlordNetwork.LobbyMissionType == LobbyMissionType.Matchmaker)
			{
				this.TeamSelectionEnabled = false;
				return;
			}
			this.TeamSelectionEnabled = true;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0008F20A File Offset: 0x0008D40A
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<TeamChange>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventTeamChange));
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0008F228 File Offset: 0x0008D428
		private void OnMyClientSynchronized()
		{
			base.Mission.GetMissionBehavior<MissionNetworkComponent>().OnMyClientSynchronized -= this.OnMyClientSynchronized;
			if (Mission.Current.GetMissionBehavior<MissionLobbyComponent>().CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && GameNetwork.MyPeer.GetComponent<MissionPeer>().Team == null)
			{
				this.SelectTeam();
			}
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0008F27C File Offset: 0x0008D47C
		public override void AfterStart()
		{
			this._platformFriends = new HashSet<PlayerId>();
			foreach (PlayerId item in FriendListService.GetAllFriendsInAllPlatforms())
			{
				this._platformFriends.Add(item);
			}
			this._friendsPerTeam = new Dictionary<Team, IEnumerable<VirtualPlayer>>();
			MissionPeer.OnTeamChanged += this.UpdateTeams;
			if (GameNetwork.IsClient)
			{
				MissionNetworkComponent missionBehavior = base.Mission.GetMissionBehavior<MissionNetworkComponent>();
				if (this.TeamSelectionEnabled)
				{
					missionBehavior.OnMyClientSynchronized += this.OnMyClientSynchronized;
				}
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0008F324 File Offset: 0x0008D524
		public override void OnRemoveBehavior()
		{
			MissionPeer.OnTeamChanged -= this.UpdateTeams;
			this.OnMyTeamChange = null;
			base.OnRemoveBehavior();
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x0008F344 File Offset: 0x0008D544
		private bool HandleClientEventTeamChange(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			TeamChange teamChange = (TeamChange)baseMessage;
			if (this.TeamSelectionEnabled)
			{
				if (teamChange.AutoAssign)
				{
					this.AutoAssignTeam(peer);
				}
				else
				{
					Team teamFromTeamIndex = Mission.MissionNetworkHelper.GetTeamFromTeamIndex(teamChange.TeamIndex);
					this.ChangeTeamServer(peer, teamFromTeamIndex);
				}
			}
			return true;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x0008F388 File Offset: 0x0008D588
		public void SelectTeam()
		{
			if (this.OnSelectingTeam != null)
			{
				List<Team> disabledTeams = this.GetDisabledTeams();
				this.OnSelectingTeam(disabledTeams);
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x0008F3B0 File Offset: 0x0008D5B0
		public void UpdateTeams(NetworkCommunicator peer, Team oldTeam, Team newTeam)
		{
			if (this.OnUpdateTeams != null)
			{
				this.OnUpdateTeams();
			}
			if (GameNetwork.IsMyPeerReady)
			{
				this.CacheFriendsForTeams();
			}
			if (newTeam.Side != BattleSideEnum.None)
			{
				MissionPeer component = peer.GetComponent<MissionPeer>();
				component.SelectedTroopIndex = 0;
				component.NextSelectedTroopIndex = 0;
				component.OverrideCultureWithTeamCulture();
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0008F400 File Offset: 0x0008D600
		public List<Team> GetDisabledTeams()
		{
			List<Team> list = new List<Team>();
			if (MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) == 0)
			{
				return list;
			}
			Team myTeam = GameNetwork.IsMyPeerReady ? GameNetwork.MyPeer.GetComponent<MissionPeer>().Team : null;
			Team[] array = (from q in base.Mission.Teams
			where q != this.Mission.SpectatorTeam
			select q).OrderBy(delegate(Team q)
			{
				if (myTeam == null)
				{
					return this.GetPlayerCountForTeam(q);
				}
				if (q != myTeam)
				{
					return this.GetPlayerCountForTeam(q);
				}
				return this.GetPlayerCountForTeam(q) - 1;
			}).ToArray<Team>();
			foreach (Team team in array)
			{
				int num = this.GetPlayerCountForTeam(team);
				int num2 = this.GetPlayerCountForTeam(array[0]);
				if (myTeam == team)
				{
					num--;
				}
				if (myTeam == array[0])
				{
					num2--;
				}
				if (num - num2 >= MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
				{
					list.Add(team);
				}
			}
			return list;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0008F4E8 File Offset: 0x0008D6E8
		public void ChangeTeamServer(NetworkCommunicator networkPeer, Team team)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			Team team2 = component.Team;
			if (team2 != null && team2 != base.Mission.SpectatorTeam && team2 != team && component.ControlledAgent != null)
			{
				Blow b = new Blow(component.ControlledAgent.Index);
				b.DamageType = DamageTypes.Invalid;
				b.BaseMagnitude = 10000f;
				b.GlobalPosition = component.ControlledAgent.Position;
				b.DamagedPercentage = 1f;
				component.ControlledAgent.Die(b, Agent.KillInfo.TeamSwitch);
			}
			component.Team = team;
			BasicCultureObject culture = component.Team.IsAttacker ? MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)) : MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			component.Culture = culture;
			if (team != team2)
			{
				if (component.HasSpawnedAgentVisuals)
				{
					component.HasSpawnedAgentVisuals = false;
					MBDebug.Print("HasSpawnedAgentVisuals = false for peer: " + component.Name + " because he just changed his team", 0, Debug.DebugColor.White, 17592186044416UL);
					component.SpawnCountThisRound = 0;
					Mission.Current.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, true);
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(component.GetNetworkPeer()));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
					}
					component.HasSpawnedAgentVisuals = false;
				}
				if (!this._gameModeServer.IsGameModeHidingAllAgentVisuals && !networkPeer.IsServerPeer)
				{
					MissionNetworkComponent missionNetworkComponent = this._missionNetworkComponent;
					if (missionNetworkComponent != null)
					{
						missionNetworkComponent.OnPeerSelectedTeam(component);
					}
				}
				this._gameModeServer.OnPeerChangedTeam(networkPeer, team2, team);
				component.SpawnTimer.Reset(Mission.Current.CurrentTime, 0.1f);
				component.WantsToSpawnAsBot = false;
				component.HasSpawnTimerExpired = false;
			}
			this.UpdateTeams(networkPeer, team2, team);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0008F698 File Offset: 0x0008D898
		public void ChangeTeam(Team team)
		{
			if (team != GameNetwork.MyPeer.GetComponent<MissionPeer>().Team)
			{
				if (GameNetwork.IsServer)
				{
					Mission.Current.PlayerTeam = team;
					this.ChangeTeamServer(GameNetwork.MyPeer, team);
				}
				else
				{
					foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
					{
						MissionPeer component = networkPeer.GetComponent<MissionPeer>();
						if (component != null)
						{
							component.ClearAllVisuals(false);
						}
					}
					GameNetwork.BeginModuleEventAsClient();
					GameNetwork.WriteMessage(new TeamChange(false, team.TeamIndex));
					GameNetwork.EndModuleEventAsClient();
				}
				if (this.OnMyTeamChange != null)
				{
					this.OnMyTeamChange();
				}
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0008F758 File Offset: 0x0008D958
		public int GetPlayerCountForTeam(Team team)
		{
			int num = 0;
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (((component != null) ? component.Team : null) != null && component.Team == team)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0008F7C8 File Offset: 0x0008D9C8
		private void CacheFriendsForTeams()
		{
			this._friendsPerTeam.Clear();
			if (this._platformFriends.Count > 0)
			{
				List<MissionPeer> list = new List<MissionPeer>();
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && this._platformFriends.Contains(networkCommunicator.VirtualPlayer.Id))
					{
						list.Add(component);
					}
				}
				using (List<Team>.Enumerator enumerator2 = base.Mission.Teams.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Team team = enumerator2.Current;
						if (team != null)
						{
							this._friendsPerTeam.Add(team, from x in list
							where x.Team == team
							select x.Peer);
						}
					}
				}
				if (this.OnUpdateFriendsPerTeam != null)
				{
					this.OnUpdateFriendsPerTeam();
				}
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0008F914 File Offset: 0x0008DB14
		public IEnumerable<VirtualPlayer> GetFriendsForTeam(Team team)
		{
			if (this._friendsPerTeam.ContainsKey(team))
			{
				return this._friendsPerTeam[team];
			}
			return new List<VirtualPlayer>();
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0008F938 File Offset: 0x0008DB38
		public void BalanceTeams()
		{
			if (MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) != 0)
			{
				int i = this.GetPlayerCountForTeam(Mission.Current.AttackerTeam);
				int j = this.GetPlayerCountForTeam(Mission.Current.DefenderTeam);
				while (i > j + MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
				{
					MissionPeer missionPeer = null;
					foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
					{
						if (networkCommunicator.IsSynchronized)
						{
							MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
							if (((component != null) ? component.Team : null) != null && component.Team == base.Mission.AttackerTeam && (missionPeer == null || component.JoinTime >= missionPeer.JoinTime))
							{
								missionPeer = component;
							}
						}
					}
					this.ChangeTeamServer(missionPeer.GetNetworkPeer(), Mission.Current.DefenderTeam);
					i--;
					j++;
				}
				while (j > i + MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))
				{
					MissionPeer missionPeer2 = null;
					foreach (NetworkCommunicator networkCommunicator2 in GameNetwork.NetworkPeers)
					{
						if (networkCommunicator2.IsSynchronized)
						{
							MissionPeer component2 = networkCommunicator2.GetComponent<MissionPeer>();
							if (((component2 != null) ? component2.Team : null) != null && component2.Team == base.Mission.DefenderTeam && (missionPeer2 == null || component2.JoinTime >= missionPeer2.JoinTime))
							{
								missionPeer2 = component2;
							}
						}
					}
					this.ChangeTeamServer(missionPeer2.GetNetworkPeer(), Mission.Current.AttackerTeam);
					i++;
					j--;
				}
			}
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0008FB00 File Offset: 0x0008DD00
		public void AutoAssignTeam(NetworkCommunicator peer)
		{
			if (!GameNetwork.IsServer)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new TeamChange(true, -1));
				GameNetwork.EndModuleEventAsClient();
				if (this.OnMyTeamChange != null)
				{
					this.OnMyTeamChange();
				}
				return;
			}
			List<Team> disabledTeams = this.GetDisabledTeams();
			List<Team> list = (from x in base.Mission.Teams
			where !disabledTeams.Contains(x) && x.Side != BattleSideEnum.None
			select x).ToList<Team>();
			Team team;
			if (list.Count > 1)
			{
				int[] array = new int[list.Count];
				foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkPeer.GetComponent<MissionPeer>();
					if (((component != null) ? component.Team : null) != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							if (component.Team == list[i])
							{
								array[i]++;
							}
						}
					}
				}
				int num = -1;
				int num2 = -1;
				for (int j = 0; j < array.Length; j++)
				{
					if (num2 < 0 || array[j] < num)
					{
						num2 = j;
						num = array[j];
					}
				}
				team = list[num2];
			}
			else
			{
				team = list[0];
			}
			if (!peer.IsMine)
			{
				this.ChangeTeamServer(peer, team);
				return;
			}
			this.ChangeTeam(team);
		}

		// Token: 0x04000DFE RID: 3582
		private MissionNetworkComponent _missionNetworkComponent;

		// Token: 0x04000DFF RID: 3583
		private MissionMultiplayerGameModeBase _gameModeServer;

		// Token: 0x04000E00 RID: 3584
		private HashSet<PlayerId> _platformFriends;

		// Token: 0x04000E01 RID: 3585
		private Dictionary<Team, IEnumerable<VirtualPlayer>> _friendsPerTeam;

		// Token: 0x02000575 RID: 1397
		// (Invoke) Token: 0x060039E3 RID: 14819
		public delegate void OnSelectingTeamDelegate(List<Team> disableTeams);
	}
}
