using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029B RID: 667
	public class MissionScoreboardComponent : MissionNetwork
	{
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600236F RID: 9071 RVA: 0x00083628 File Offset: 0x00081828
		// (remove) Token: 0x06002370 RID: 9072 RVA: 0x00083660 File Offset: 0x00081860
		public event Action OnRoundPropertiesChanged;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06002371 RID: 9073 RVA: 0x00083698 File Offset: 0x00081898
		// (remove) Token: 0x06002372 RID: 9074 RVA: 0x000836D0 File Offset: 0x000818D0
		public event Action<BattleSideEnum> OnBotPropertiesChanged;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06002373 RID: 9075 RVA: 0x00083708 File Offset: 0x00081908
		// (remove) Token: 0x06002374 RID: 9076 RVA: 0x00083740 File Offset: 0x00081940
		public event Action<Team, Team, MissionPeer> OnPlayerSideChanged;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06002375 RID: 9077 RVA: 0x00083778 File Offset: 0x00081978
		// (remove) Token: 0x06002376 RID: 9078 RVA: 0x000837B0 File Offset: 0x000819B0
		public event Action<BattleSideEnum, MissionPeer> OnPlayerPropertiesChanged;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002377 RID: 9079 RVA: 0x000837E8 File Offset: 0x000819E8
		// (remove) Token: 0x06002378 RID: 9080 RVA: 0x00083820 File Offset: 0x00081A20
		public event Action<MissionPeer, int> OnMVPSelected;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002379 RID: 9081 RVA: 0x00083858 File Offset: 0x00081A58
		// (remove) Token: 0x0600237A RID: 9082 RVA: 0x00083890 File Offset: 0x00081A90
		public event Action OnScoreboardInitialized;

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000838C5 File Offset: 0x00081AC5
		public bool IsOneSided
		{
			get
			{
				return this._scoreboardSides == MissionScoreboardComponent.ScoreboardSides.OneSide;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000838D0 File Offset: 0x00081AD0
		public BattleSideEnum RoundWinner
		{
			get
			{
				IRoundComponent roundComponent = this._mpGameModeBase.RoundComponent;
				if (roundComponent == null)
				{
					return BattleSideEnum.None;
				}
				return roundComponent.RoundWinner;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000838E8 File Offset: 0x00081AE8
		public MissionScoreboardComponent.ScoreboardHeader[] Headers
		{
			get
			{
				return this._scoreboardData.GetScoreboardHeaders();
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000838F5 File Offset: 0x00081AF5
		public MissionScoreboardComponent(IScoreboardData scoreboardData)
		{
			this._scoreboardData = scoreboardData;
			this._spectators = new List<MissionPeer>();
			this._sides = new MissionScoreboardComponent.MissionScoreboardSide[2];
			this._roundWinnerList = new List<BattleSideEnum>();
			this._mvpCountPerPeer = new List<ValueTuple<MissionPeer, int>>();
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00083931 File Offset: 0x00081B31
		public IEnumerable<BattleSideEnum> RoundWinnerList
		{
			get
			{
				return this._roundWinnerList.AsReadOnly();
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x0008393E File Offset: 0x00081B3E
		public MissionScoreboardComponent.MissionScoreboardSide[] Sides
		{
			get
			{
				return this._sides;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00083946 File Offset: 0x00081B46
		public List<MissionPeer> Spectators
		{
			get
			{
				return this._spectators;
			}
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x00083950 File Offset: 0x00081B50
		public override void AfterStart()
		{
			this._spectators.Clear();
			this._missionLobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
			this._missionNetworkComponent = base.Mission.GetMissionBehavior<MissionNetworkComponent>();
			this._mpGameModeBase = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
			if (this._missionLobbyComponent.MissionType == MultiplayerGameType.FreeForAll || this._missionLobbyComponent.MissionType == MultiplayerGameType.Duel)
			{
				this._scoreboardSides = MissionScoreboardComponent.ScoreboardSides.OneSide;
			}
			else
			{
				this._scoreboardSides = MissionScoreboardComponent.ScoreboardSides.TwoSides;
			}
			MissionPeer.OnTeamChanged += this.TeamChange;
			this._missionNetworkComponent.OnMyClientSynchronized += this.OnMyClientSynchronized;
			if (GameNetwork.IsServerOrRecorder && this._mpGameModeBase.RoundComponent != null)
			{
				this._mpGameModeBase.RoundComponent.OnRoundEnding += this.OnRoundEnding;
				this._mpGameModeBase.RoundComponent.OnPreRoundEnding += this.OnPreRoundEnding;
			}
			this.LateInitScoreboard();
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00083A40 File Offset: 0x00081C40
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClient)
			{
				registerer.RegisterBaseHandler<UpdateRoundScores>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerUpdateRoundScoresMessage));
				registerer.RegisterBaseHandler<SetRoundMVP>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerSetRoundMVP));
				registerer.RegisterBaseHandler<BotData>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventBotDataMessage));
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00083A80 File Offset: 0x00081C80
		public override void OnRemoveBehavior()
		{
			this._spectators.Clear();
			for (int i = 0; i < 2; i++)
			{
				if (this._sides[i] != null)
				{
					this._sides[i].Clear();
				}
			}
			MissionPeer.OnTeamChanged -= this.TeamChange;
			if (this._missionNetworkComponent != null)
			{
				this._missionNetworkComponent.OnMyClientSynchronized -= this.OnMyClientSynchronized;
			}
			if (GameNetwork.IsServerOrRecorder && this._mpGameModeBase.RoundComponent != null)
			{
				this._mpGameModeBase.RoundComponent.OnRoundEnding -= this.OnRoundEnding;
			}
			base.OnRemoveBehavior();
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00083B24 File Offset: 0x00081D24
		public void ResetBotScores()
		{
			foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this._sides)
			{
				if (((missionScoreboardSide != null) ? missionScoreboardSide.BotScores : null) != null)
				{
					missionScoreboardSide.BotScores.ResetKillDeathAssist();
				}
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00083B64 File Offset: 0x00081D64
		public void ChangeTeamScore(Team team, int scoreChange)
		{
			MissionScoreboardComponent.MissionScoreboardSide sideSafe = this.GetSideSafe(team.Side);
			sideSafe.SideScore += scoreChange;
			sideSafe.SideScore = MBMath.ClampInt(sideSafe.SideScore, -1023000, 1023000);
			if (GameNetwork.IsServer)
			{
				int defenderTeamScore = (this._scoreboardSides != MissionScoreboardComponent.ScoreboardSides.OneSide) ? this._sides[0].SideScore : 0;
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new UpdateRoundScores(this._sides[1].SideScore, defenderTeamScore));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
			if (this.OnRoundPropertiesChanged != null)
			{
				this.OnRoundPropertiesChanged();
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00083BFC File Offset: 0x00081DFC
		private void UpdateRoundScores()
		{
			foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this._sides)
			{
				if (missionScoreboardSide != null && missionScoreboardSide.Side == this.RoundWinner)
				{
					this._roundWinnerList.Add(this.RoundWinner);
					if (this.RoundWinner != BattleSideEnum.None)
					{
						this._sides[(int)this.RoundWinner].SideScore++;
					}
				}
			}
			if (this.OnRoundPropertiesChanged != null)
			{
				this.OnRoundPropertiesChanged();
			}
			if (GameNetwork.IsServer)
			{
				int defenderTeamScore = (this._scoreboardSides != MissionScoreboardComponent.ScoreboardSides.OneSide) ? this._sides[0].SideScore : 0;
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new UpdateRoundScores(this._sides[1].SideScore, defenderTeamScore));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00083CBE File Offset: 0x00081EBE
		public MissionScoreboardComponent.MissionScoreboardSide GetSideSafe(BattleSideEnum battleSide)
		{
			if (this._scoreboardSides == MissionScoreboardComponent.ScoreboardSides.OneSide)
			{
				return this._sides[1];
			}
			return this._sides[(int)battleSide];
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00083CD9 File Offset: 0x00081ED9
		public int GetRoundScore(BattleSideEnum side)
		{
			if (side > (BattleSideEnum)this._sides.Length || side < BattleSideEnum.Defender)
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionScoreboardComponent.cs", "GetRoundScore", 463);
				return 0;
			}
			return this.GetSideSafe(side).SideScore;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00083D14 File Offset: 0x00081F14
		public void HandleServerUpdateRoundScoresMessage(GameNetworkMessage baseMessage)
		{
			UpdateRoundScores updateRoundScores = (UpdateRoundScores)baseMessage;
			this._sides[1].SideScore = updateRoundScores.AttackerTeamScore;
			if (this._scoreboardSides != MissionScoreboardComponent.ScoreboardSides.OneSide)
			{
				this._sides[0].SideScore = updateRoundScores.DefenderTeamScore;
			}
			if (this.OnRoundPropertiesChanged != null)
			{
				this.OnRoundPropertiesChanged();
			}
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00083D6C File Offset: 0x00081F6C
		public void HandleServerSetRoundMVP(GameNetworkMessage baseMessage)
		{
			SetRoundMVP setRoundMVP = (SetRoundMVP)baseMessage;
			Action<MissionPeer, int> onMVPSelected = this.OnMVPSelected;
			if (onMVPSelected != null)
			{
				onMVPSelected(setRoundMVP.MVPPeer.GetComponent<MissionPeer>(), setRoundMVP.MVPCount);
			}
			this.PlayerPropertiesChanged(setRoundMVP.MVPPeer);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00083DB0 File Offset: 0x00081FB0
		public void CalculateTotalNumbers()
		{
			foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this._sides)
			{
				if (missionScoreboardSide != null)
				{
					int num = missionScoreboardSide.BotScores.DeathCount;
					int num2 = missionScoreboardSide.BotScores.AssistCount;
					int num3 = missionScoreboardSide.BotScores.KillCount;
					foreach (MissionPeer missionPeer in missionScoreboardSide.Players)
					{
						num2 += missionPeer.AssistCount;
						num += missionPeer.DeathCount;
						num3 += missionPeer.KillCount;
					}
				}
			}
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00083E68 File Offset: 0x00082068
		private void TeamChange(NetworkCommunicator player, Team oldTeam, Team nextTeam)
		{
			if (oldTeam == null && GameNetwork.VirtualPlayers[player.VirtualPlayer.Index] != player.VirtualPlayer)
			{
				Debug.Print("Ignoring team change call for {}, dced peer.", 0, Debug.DebugColor.White, 17179869184UL);
				return;
			}
			MissionPeer component = player.GetComponent<MissionPeer>();
			if (oldTeam != null)
			{
				if (oldTeam == base.Mission.SpectatorTeam)
				{
					this._spectators.Remove(component);
				}
				else
				{
					this.GetSideSafe(oldTeam.Side).RemovePlayer(component);
				}
			}
			if (nextTeam != null)
			{
				if (nextTeam == base.Mission.SpectatorTeam)
				{
					this._spectators.Add(component);
				}
				else
				{
					Debug.Print(string.Format(">SBC => {0} is switching from {1} to {2}. Adding to scoreboard side {3}.", new object[]
					{
						player.UserName,
						(oldTeam == null) ? "NULL" : oldTeam.Side.ToString(),
						nextTeam.Side.ToString(),
						nextTeam.Side
					}), 0, Debug.DebugColor.Blue, 17179869184UL);
					this.GetSideSafe(nextTeam.Side).AddPlayer(component);
				}
			}
			if (this.OnPlayerSideChanged != null)
			{
				this.OnPlayerSideChanged(oldTeam, nextTeam, component);
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00083FA0 File Offset: 0x000821A0
		public override void OnClearScene()
		{
			if (this._mpGameModeBase.RoundComponent == null && GameNetwork.IsServer)
			{
				this.ClearSideScores();
			}
			foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this.Sides)
			{
				if (missionScoreboardSide != null)
				{
					missionScoreboardSide.BotScores.AliveCount = 0;
				}
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00083FF0 File Offset: 0x000821F0
		public override void OnPlayerConnectedToServer(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			if (component != null && component.Team != null)
			{
				this.TeamChange(networkPeer, null, component.Team);
			}
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00084020 File Offset: 0x00082220
		public override void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
			MissionPeer missionPeer = networkPeer.GetComponent<MissionPeer>();
			if (missionPeer != null)
			{
				bool flag = this._spectators.Contains(missionPeer);
				bool flag2 = this._sides.Any((MissionScoreboardComponent.MissionScoreboardSide x) => x != null && x.Players.Contains(missionPeer));
				if (flag)
				{
					this._spectators.Remove(missionPeer);
					return;
				}
				if (flag2)
				{
					this.GetSideSafe(missionPeer.Team.Side).RemovePlayer(missionPeer);
					Formation controlledFormation = missionPeer.ControlledFormation;
					if (controlledFormation != null)
					{
						Team team = missionPeer.Team;
						BotData botScores = this.Sides[(int)team.Side].BotScores;
						botScores.AliveCount += controlledFormation.GetCountOfUnitsWithCondition((Agent agent) => agent.IsActive());
						this.BotPropertiesChanged(team.Side);
					}
					Action<Team, Team, MissionPeer> onPlayerSideChanged = this.OnPlayerSideChanged;
					if (onPlayerSideChanged == null)
					{
						return;
					}
					onPlayerSideChanged(missionPeer.Team, null, missionPeer);
				}
			}
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0008413B File Offset: 0x0008233B
		private void BotsControlledChanged(NetworkCommunicator peer)
		{
			this.PlayerPropertiesChanged(peer);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x00084144 File Offset: 0x00082344
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.IsActive() && !agent.IsMount)
			{
				if (agent.MissionPeer == null)
				{
					this.BotPropertiesChanged(agent.Team.Side);
					return;
				}
				if (agent.MissionPeer != null)
				{
					this.PlayerPropertiesChanged(agent.MissionPeer.GetNetworkPeer());
				}
			}
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00084194 File Offset: 0x00082394
		public override void OnAssignPlayerAsSergeantOfFormation(Agent agent)
		{
			if (agent.MissionPeer != null)
			{
				this.PlayerPropertiesChanged(agent.MissionPeer.GetNetworkPeer());
			}
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000841AF File Offset: 0x000823AF
		public void BotPropertiesChanged(BattleSideEnum side)
		{
			if (this.OnBotPropertiesChanged != null)
			{
				this.OnBotPropertiesChanged(side);
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000841C8 File Offset: 0x000823C8
		public void PlayerPropertiesChanged(NetworkCommunicator player)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				return;
			}
			MissionPeer component = player.GetComponent<MissionPeer>();
			if (component != null)
			{
				this.PlayerPropertiesChanged(component);
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000841F0 File Offset: 0x000823F0
		public void PlayerPropertiesChanged(MissionPeer player)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				return;
			}
			this.CalculateTotalNumbers();
			if (this.OnPlayerPropertiesChanged != null && player.Team != null && player.Team != Mission.Current.SpectatorTeam)
			{
				BattleSideEnum side = player.Team.Side;
				this.OnPlayerPropertiesChanged(side, player);
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00084248 File Offset: 0x00082448
		protected override void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			networkPeer.GetComponent<MissionPeer>();
			foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this._sides)
			{
				if (missionScoreboardSide != null && !networkPeer.IsServerPeer)
				{
					if (missionScoreboardSide.BotScores.IsAnyValid)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new BotData(missionScoreboardSide.Side, missionScoreboardSide.BotScores.KillCount, missionScoreboardSide.BotScores.AssistCount, missionScoreboardSide.BotScores.DeathCount, missionScoreboardSide.BotScores.AliveCount));
						GameNetwork.EndModuleEventAsServer();
					}
					if (this._mpGameModeBase != null)
					{
						int defenderTeamScore = (this._scoreboardSides != MissionScoreboardComponent.ScoreboardSides.OneSide) ? this._sides[0].SideScore : 0;
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new UpdateRoundScores(this._sides[1].SideScore, defenderTeamScore));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			if (!networkPeer.IsServerPeer && this._mvpCountPerPeer != null)
			{
				foreach (ValueTuple<MissionPeer, int> valueTuple in this._mvpCountPerPeer)
				{
					GameNetwork.BeginModuleEventAsServer(networkPeer);
					GameNetwork.WriteMessage(new SetRoundMVP(valueTuple.Item1.GetNetworkPeer(), valueTuple.Item2));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000843A0 File Offset: 0x000825A0
		public void HandleServerEventBotDataMessage(GameNetworkMessage baseMessage)
		{
			BotData botData = (BotData)baseMessage;
			MissionScoreboardComponent.MissionScoreboardSide sideSafe = this.GetSideSafe(botData.Side);
			sideSafe.BotScores.KillCount = botData.KillCount;
			sideSafe.BotScores.AssistCount = botData.AssistCount;
			sideSafe.BotScores.DeathCount = botData.DeathCount;
			sideSafe.BotScores.AliveCount = botData.AliveBotCount;
			this.BotPropertiesChanged(botData.Side);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00084410 File Offset: 0x00082610
		private void ClearSideScores()
		{
			this._sides[1].SideScore = 0;
			if (this._scoreboardSides == MissionScoreboardComponent.ScoreboardSides.TwoSides)
			{
				this._sides[0].SideScore = 0;
			}
			if (GameNetwork.IsServer)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new UpdateRoundScores(0, 0));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			}
			if (this.OnRoundPropertiesChanged != null)
			{
				this.OnRoundPropertiesChanged();
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00084474 File Offset: 0x00082674
		public void OnRoundEnding()
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				this.UpdateRoundScores();
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00084483 File Offset: 0x00082683
		private void OnMyClientSynchronized()
		{
			this.LateInitializeHeaders();
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0008448C File Offset: 0x0008268C
		private void LateInitScoreboard()
		{
			MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide = new MissionScoreboardComponent.MissionScoreboardSide(BattleSideEnum.Attacker);
			this._sides[1] = missionScoreboardSide;
			this._sides[1].BotScores = new BotData();
			if (this._scoreboardSides == MissionScoreboardComponent.ScoreboardSides.TwoSides)
			{
				MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide2 = new MissionScoreboardComponent.MissionScoreboardSide(BattleSideEnum.Defender);
				this._sides[0] = missionScoreboardSide2;
				this._sides[0].BotScores = new BotData();
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000844E8 File Offset: 0x000826E8
		private void LateInitializeHeaders()
		{
			if (this._isInitialized)
			{
				return;
			}
			this._isInitialized = true;
			foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this._sides)
			{
				if (missionScoreboardSide != null)
				{
					missionScoreboardSide.UpdateHeader(this.Headers);
				}
			}
			if (this.OnScoreboardInitialized != null)
			{
				this.OnScoreboardInitialized();
			}
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00084540 File Offset: 0x00082740
		public void OnMultiplayerGameClientBehaviorInitialized(ref Action<NetworkCommunicator> onBotsControlledChanged)
		{
			onBotsControlledChanged = (Action<NetworkCommunicator>)Delegate.Combine(onBotsControlledChanged, new Action<NetworkCommunicator>(this.BotsControlledChanged));
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0008455C File Offset: 0x0008275C
		public BattleSideEnum GetMatchWinnerSide()
		{
			List<int> scores = new List<int>();
			KeyValuePair<BattleSideEnum, int> keyValuePair = new KeyValuePair<BattleSideEnum, int>(BattleSideEnum.None, -1);
			for (int i = 0; i < 2; i++)
			{
				BattleSideEnum battleSideEnum = (BattleSideEnum)i;
				MissionScoreboardComponent.MissionScoreboardSide sideSafe = this.GetSideSafe(battleSideEnum);
				if (sideSafe.SideScore > keyValuePair.Value && sideSafe.CurrentPlayerCount > 0)
				{
					keyValuePair = new KeyValuePair<BattleSideEnum, int>(battleSideEnum, sideSafe.SideScore);
				}
				scores.Add(sideSafe.SideScore);
			}
			if (!scores.IsEmpty<int>() && scores.All((int s) => s == scores[0]))
			{
				return BattleSideEnum.None;
			}
			return keyValuePair.Key;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00084604 File Offset: 0x00082804
		private void OnPreRoundEnding()
		{
			if (GameNetwork.IsServer)
			{
				KeyValuePair<MissionPeer, int> keyValuePair2;
				KeyValuePair<MissionPeer, int> keyValuePair4;
				foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in this.Sides)
				{
					if (missionScoreboardSide.Side == BattleSideEnum.Attacker)
					{
						KeyValuePair<MissionPeer, int> keyValuePair = missionScoreboardSide.CalculateAndGetMVPScoreWithPeer();
						if (keyValuePair2.Key == null || keyValuePair2.Value < keyValuePair.Value)
						{
							keyValuePair2 = keyValuePair;
						}
					}
					else if (missionScoreboardSide.Side == BattleSideEnum.Defender)
					{
						KeyValuePair<MissionPeer, int> keyValuePair3 = missionScoreboardSide.CalculateAndGetMVPScoreWithPeer();
						if (keyValuePair4.Key == null || keyValuePair4.Value < keyValuePair3.Value)
						{
							keyValuePair4 = keyValuePair3;
						}
					}
				}
				if (keyValuePair2.Key != null)
				{
					this.SetPeerAsMVP(keyValuePair2.Key);
				}
				if (keyValuePair4.Key != null)
				{
					this.SetPeerAsMVP(keyValuePair4.Key);
				}
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000846C0 File Offset: 0x000828C0
		private void SetPeerAsMVP(MissionPeer peer)
		{
			int num = -1;
			for (int i = 0; i < this._mvpCountPerPeer.Count; i++)
			{
				if (peer == this._mvpCountPerPeer[i].Item1)
				{
					num = i;
					break;
				}
			}
			int num2 = 1;
			if (num != -1)
			{
				num2 = this._mvpCountPerPeer[num].Item2 + 1;
				this._mvpCountPerPeer.RemoveAt(num);
			}
			this._mvpCountPerPeer.Add(new ValueTuple<MissionPeer, int>(peer, num2));
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new SetRoundMVP(peer.GetNetworkPeer(), num2));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			Action<MissionPeer, int> onMVPSelected = this.OnMVPSelected;
			if (onMVPSelected == null)
			{
				return;
			}
			onMVPSelected(peer, num2);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00084764 File Offset: 0x00082964
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent != null && GameNetwork.IsServer && !isBlocked && damagedHp > 0f)
			{
				if (affectorAgent.IsMount)
				{
					affectorAgent = affectorAgent.RiderAgent;
				}
				if (affectorAgent != null)
				{
					MissionPeer missionPeer = affectorAgent.MissionPeer ?? ((affectorAgent.IsAIControlled && affectorAgent.OwningAgentMissionPeer != null) ? affectorAgent.OwningAgentMissionPeer : null);
					if (missionPeer != null)
					{
						int num = (int)damagedHp;
						if (affectedAgent.IsMount)
						{
							num = (int)(damagedHp * 0.35f);
							affectedAgent = affectedAgent.RiderAgent;
						}
						if (affectedAgent != null && affectorAgent != affectedAgent)
						{
							if (!affectorAgent.IsFriendOf(affectedAgent))
							{
								missionPeer.Score += num;
							}
							else
							{
								missionPeer.Score -= (int)((float)num * 1.5f);
							}
							GameNetwork.BeginBroadcastModuleEvent();
							GameNetwork.WriteMessage(new KillDeathCountChange(missionPeer.GetNetworkPeer(), null, missionPeer.KillCount, missionPeer.AssistCount, missionPeer.DeathCount, missionPeer.Score));
							GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
						}
					}
				}
			}
		}

		// Token: 0x04000D00 RID: 3328
		private const int TotalSideCount = 2;

		// Token: 0x04000D01 RID: 3329
		private MissionLobbyComponent _missionLobbyComponent;

		// Token: 0x04000D02 RID: 3330
		private MissionNetworkComponent _missionNetworkComponent;

		// Token: 0x04000D03 RID: 3331
		private MissionMultiplayerGameModeBaseClient _mpGameModeBase;

		// Token: 0x04000D04 RID: 3332
		private IScoreboardData _scoreboardData;

		// Token: 0x04000D0B RID: 3339
		private List<MissionPeer> _spectators;

		// Token: 0x04000D0C RID: 3340
		private MissionScoreboardComponent.MissionScoreboardSide[] _sides;

		// Token: 0x04000D0D RID: 3341
		private bool _isInitialized;

		// Token: 0x04000D0E RID: 3342
		private List<BattleSideEnum> _roundWinnerList;

		// Token: 0x04000D0F RID: 3343
		private MissionScoreboardComponent.ScoreboardSides _scoreboardSides;

		// Token: 0x04000D10 RID: 3344
		private List<ValueTuple<MissionPeer, int>> _mvpCountPerPeer;

		// Token: 0x02000553 RID: 1363
		private enum ScoreboardSides
		{
			// Token: 0x04001CD2 RID: 7378
			OneSide,
			// Token: 0x04001CD3 RID: 7379
			TwoSides
		}

		// Token: 0x02000554 RID: 1364
		public struct ScoreboardHeader
		{
			// Token: 0x0600394B RID: 14667 RVA: 0x000E4793 File Offset: 0x000E2993
			public ScoreboardHeader(string id, Func<MissionPeer, string> playerGetterFunc, Func<BotData, string> botGetterFunc)
			{
				this.Id = id;
				this.Name = GameTexts.FindText("str_scoreboard_header", id);
				this._playerGetterFunc = playerGetterFunc;
				this._botGetterFunc = botGetterFunc;
			}

			// Token: 0x0600394C RID: 14668 RVA: 0x000E47BC File Offset: 0x000E29BC
			public string GetValueOf(MissionPeer missionPeer)
			{
				if (missionPeer == null || this._playerGetterFunc == null)
				{
					string str = "Scoreboard header values are invalid: Peer: ";
					string str2 = ((missionPeer != null) ? missionPeer.ToString() : null) ?? "NULL";
					string str3 = " Getter: ";
					Func<MissionPeer, string> playerGetterFunc = this._playerGetterFunc;
					Debug.FailedAssert(str + str2 + str3 + (((playerGetterFunc != null) ? playerGetterFunc.ToString() : null) ?? "NULL"), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionScoreboardComponent.cs", "GetValueOf", 43);
					return string.Empty;
				}
				string result;
				try
				{
					result = this._playerGetterFunc(missionPeer);
				}
				catch (Exception ex)
				{
					Debug.FailedAssert(string.Format("An error occured while trying to get scoreboard value ({0}) for peer: {1}. Exception: {2}", this.Id, missionPeer.Name, ex.InnerException), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionScoreboardComponent.cs", "GetValueOf", 53);
					result = string.Empty;
				}
				return result;
			}

			// Token: 0x0600394D RID: 14669 RVA: 0x000E4884 File Offset: 0x000E2A84
			public string GetValueOf(BotData botData)
			{
				if (botData == null || this._botGetterFunc == null)
				{
					string str = "Scoreboard header values are invalid: Bot Data: ";
					string str2 = ((botData != null) ? botData.ToString() : null) ?? "NULL";
					string str3 = " Getter: ";
					Func<BotData, string> botGetterFunc = this._botGetterFunc;
					Debug.FailedAssert(str + str2 + str3 + (((botGetterFunc != null) ? botGetterFunc.ToString() : null) ?? "NULL"), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionScoreboardComponent.cs", "GetValueOf", 62);
					return string.Empty;
				}
				string result;
				try
				{
					result = this._botGetterFunc(botData);
				}
				catch (Exception ex)
				{
					Debug.FailedAssert(string.Format("An error occured while trying to get scoreboard value ({0}) for a bot. Exception: {1}", this.Id, ex.InnerException), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MissionScoreboardComponent.cs", "GetValueOf", 72);
					result = string.Empty;
				}
				return result;
			}

			// Token: 0x04001CD4 RID: 7380
			private readonly Func<MissionPeer, string> _playerGetterFunc;

			// Token: 0x04001CD5 RID: 7381
			private readonly Func<BotData, string> _botGetterFunc;

			// Token: 0x04001CD6 RID: 7382
			public readonly string Id;

			// Token: 0x04001CD7 RID: 7383
			public readonly TextObject Name;
		}

		// Token: 0x02000555 RID: 1365
		public class MissionScoreboardSide
		{
			// Token: 0x17000999 RID: 2457
			// (get) Token: 0x0600394E RID: 14670 RVA: 0x000E4944 File Offset: 0x000E2B44
			public int CurrentPlayerCount
			{
				get
				{
					return this._players.Count;
				}
			}

			// Token: 0x1700099A RID: 2458
			// (get) Token: 0x0600394F RID: 14671 RVA: 0x000E4951 File Offset: 0x000E2B51
			public IEnumerable<MissionPeer> Players
			{
				get
				{
					return this._players;
				}
			}

			// Token: 0x06003950 RID: 14672 RVA: 0x000E4959 File Offset: 0x000E2B59
			public MissionScoreboardSide(BattleSideEnum side)
			{
				this.Side = side;
				this._players = new List<MissionPeer>();
				this._playerLastRoundScoreMap = new List<int>();
			}

			// Token: 0x06003951 RID: 14673 RVA: 0x000E497E File Offset: 0x000E2B7E
			public void AddPlayer(MissionPeer peer)
			{
				if (!this._players.Contains(peer))
				{
					this._players.Add(peer);
					this._playerLastRoundScoreMap.Add(0);
				}
			}

			// Token: 0x06003952 RID: 14674 RVA: 0x000E49A8 File Offset: 0x000E2BA8
			public void RemovePlayer(MissionPeer peer)
			{
				for (int i = 0; i < this._players.Count; i++)
				{
					if (this._players[i] == peer)
					{
						this._players.RemoveAt(i);
						this._playerLastRoundScoreMap.RemoveAt(i);
						return;
					}
				}
			}

			// Token: 0x06003953 RID: 14675 RVA: 0x000E49F4 File Offset: 0x000E2BF4
			public string[] GetValuesOf(MissionPeer peer)
			{
				if (this._properties == null)
				{
					return new string[0];
				}
				string[] array = new string[this._properties.Length];
				if (peer == null)
				{
					for (int i = 0; i < this._properties.Length; i++)
					{
						array[i] = this._properties[i].GetValueOf(this.BotScores);
					}
					return array;
				}
				for (int j = 0; j < this._properties.Length; j++)
				{
					array[j] = this._properties[j].GetValueOf(peer);
				}
				return array;
			}

			// Token: 0x06003954 RID: 14676 RVA: 0x000E4A7C File Offset: 0x000E2C7C
			public string[] GetHeaderNames()
			{
				if (this._properties == null)
				{
					return new string[0];
				}
				string[] array = new string[this._properties.Length];
				for (int i = 0; i < this._properties.Length; i++)
				{
					array[i] = this._properties[i].Name.ToString();
				}
				return array;
			}

			// Token: 0x06003955 RID: 14677 RVA: 0x000E4AD4 File Offset: 0x000E2CD4
			public string[] GetHeaderIds()
			{
				if (this._properties == null)
				{
					return new string[0];
				}
				string[] array = new string[this._properties.Length];
				for (int i = 0; i < this._properties.Length; i++)
				{
					array[i] = this._properties[i].Id;
				}
				return array;
			}

			// Token: 0x06003956 RID: 14678 RVA: 0x000E4B28 File Offset: 0x000E2D28
			public int GetScore(MissionPeer peer)
			{
				if (this._properties == null)
				{
					return 0;
				}
				string s;
				if (peer == null)
				{
					if (this._properties.Any((MissionScoreboardComponent.ScoreboardHeader p) => p.Id == "score"))
					{
						s = this._properties.FirstOrDefault((MissionScoreboardComponent.ScoreboardHeader x) => x.Id == "score").GetValueOf(this.BotScores);
					}
					else
					{
						s = string.Empty;
					}
				}
				else if (this._properties.Any((MissionScoreboardComponent.ScoreboardHeader p) => p.Id == "score"))
				{
					s = this._properties.Single((MissionScoreboardComponent.ScoreboardHeader x) => x.Id == "score").GetValueOf(peer);
				}
				else
				{
					s = string.Empty;
				}
				int result = 0;
				int.TryParse(s, out result);
				return result;
			}

			// Token: 0x06003957 RID: 14679 RVA: 0x000E4C25 File Offset: 0x000E2E25
			public void UpdateHeader(MissionScoreboardComponent.ScoreboardHeader[] headers)
			{
				this._properties = headers;
			}

			// Token: 0x06003958 RID: 14680 RVA: 0x000E4C2E File Offset: 0x000E2E2E
			public void Clear()
			{
				this._players.Clear();
			}

			// Token: 0x06003959 RID: 14681 RVA: 0x000E4C3C File Offset: 0x000E2E3C
			public KeyValuePair<MissionPeer, int> CalculateAndGetMVPScoreWithPeer()
			{
				KeyValuePair<MissionPeer, int> result = default(KeyValuePair<MissionPeer, int>);
				for (int i = 0; i < this._players.Count; i++)
				{
					int num = this._players[i].Score - this._playerLastRoundScoreMap[i];
					this._playerLastRoundScoreMap[i] = this._players[i].Score;
					if (result.Key == null || result.Value < num)
					{
						result = new KeyValuePair<MissionPeer, int>(this._players[i], num);
					}
				}
				return result;
			}

			// Token: 0x04001CD8 RID: 7384
			public readonly BattleSideEnum Side;

			// Token: 0x04001CD9 RID: 7385
			private MissionScoreboardComponent.ScoreboardHeader[] _properties;

			// Token: 0x04001CDA RID: 7386
			public BotData BotScores;

			// Token: 0x04001CDB RID: 7387
			public int SideScore;

			// Token: 0x04001CDC RID: 7388
			private List<MissionPeer> _players;

			// Token: 0x04001CDD RID: 7389
			private List<int> _playerLastRoundScoreMap;
		}
	}
}
