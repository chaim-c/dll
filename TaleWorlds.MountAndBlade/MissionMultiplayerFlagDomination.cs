using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade.Objects;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A5 RID: 677
	public class MissionMultiplayerFlagDomination : MissionMultiplayerGameModeBase, IAnalyticsFlagInfo, IMissionBehavior
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x00087DD5 File Offset: 0x00085FD5
		public override bool IsGameModeHidingAllAgentVisuals
		{
			get
			{
				return this._gameType == MultiplayerGameType.Captain || this._gameType == MultiplayerGameType.Battle;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x00087DEB File Offset: 0x00085FEB
		public override bool IsGameModeUsingOpposingTeams
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00087DEE File Offset: 0x00085FEE
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x00087DF6 File Offset: 0x00085FF6
		public MBReadOnlyList<FlagCapturePoint> AllCapturePoints { get; private set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00087DFF File Offset: 0x00085FFF
		public float MoraleRounded
		{
			get
			{
				return (float)((int)(this._morale / 0.01f)) * 0.01f;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x00087E15 File Offset: 0x00086015
		public bool GameModeUsesSingleSpawning
		{
			get
			{
				return this.GetMissionType() == MultiplayerGameType.Captain || this.GetMissionType() == MultiplayerGameType.Battle;
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00087E2B File Offset: 0x0008602B
		public bool UseGold()
		{
			return this._gameModeFlagDominationClient.IsGameModeUsingGold;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00087E38 File Offset: 0x00086038
		public override bool AllowCustomPlayerBanners()
		{
			return false;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00087E3B File Offset: 0x0008603B
		public override bool UseRoundController()
		{
			return true;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x00087E40 File Offset: 0x00086040
		public MissionMultiplayerFlagDomination(MultiplayerGameType gameType)
		{
			this._gameType = gameType;
			switch (this._gameType)
			{
			case MultiplayerGameType.Battle:
				this._moraleMultiplierForEachFlag = 0.75f;
				this._pointRemovalTimeInSeconds = 210f;
				this._moraleMultiplierOnLastFlag = 3.5f;
				MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection = 120;
				MissionMultiplayerFlagDomination._maxGoldAmountToCarryOver = 110;
				MissionMultiplayerFlagDomination._maxGoldAmountToCarryOverForSurvival = 20;
				return;
			case MultiplayerGameType.Captain:
				this._moraleMultiplierForEachFlag = 1f;
				this._pointRemovalTimeInSeconds = 180f;
				this._moraleMultiplierOnLastFlag = 2f;
				return;
			case MultiplayerGameType.Skirmish:
				this._moraleMultiplierForEachFlag = 2f;
				this._pointRemovalTimeInSeconds = 120f;
				this._moraleMultiplierOnLastFlag = 2f;
				MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection = 300;
				MissionMultiplayerFlagDomination._maxGoldAmountToCarryOver = 80;
				MissionMultiplayerFlagDomination._maxGoldAmountToCarryOverForSurvival = 30;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x00087F4C File Offset: 0x0008614C
		public override MultiplayerGameType GetMissionType()
		{
			return this._gameType;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x00087F54 File Offset: 0x00086154
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._gameModeFlagDominationClient = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeFlagDominationClient>();
			this._morale = 0f;
			this._capturePointOwners = new Team[3];
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>().ToMBList<FlagCapturePoint>();
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				flagCapturePoint.SetTeamColorsWithAllSynched(4284111450U, uint.MaxValue);
				this._capturePointOwners[flagCapturePoint.FlagIndex] = null;
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00088004 File Offset: 0x00086204
		public override void AfterStart()
		{
			base.AfterStart();
			this.RoundController.OnRoundStarted += this.OnPreparationStart;
			MissionPeer.OnPreTeamChanged += this.OnPreTeamChanged;
			this.RoundController.OnPreparationEnded += this.OnPreparationEnded;
			if (this.WarmupComponent != null)
			{
				this.WarmupComponent.OnWarmupEnding += this.OnWarmupEnding;
			}
			this.RoundController.OnPreRoundEnding += this.OnRoundEnd;
			this.RoundController.OnPostRoundEnded += this.OnPostRoundEnd;
			BasicCultureObject basicCultureObject = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			BasicCultureObject basicCultureObject2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			if (basicCultureObject == null)
			{
				basicCultureObject = MBObjectManager.Instance.GetFirstObject<BasicCultureObject>();
			}
			if (basicCultureObject2 == null)
			{
				basicCultureObject2 = MBObjectManager.Instance.GetFirstObject<BasicCultureObject>();
			}
			Banner banner = new Banner(basicCultureObject.BannerKey, basicCultureObject.BackgroundColor1, basicCultureObject.ForegroundColor1);
			Banner banner2 = new Banner(basicCultureObject2.BannerKey, basicCultureObject2.BackgroundColor2, basicCultureObject2.ForegroundColor2);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, basicCultureObject.BackgroundColor1, basicCultureObject.ForegroundColor1, banner, false, true, true);
			base.Mission.Teams.Add(BattleSideEnum.Defender, basicCultureObject2.BackgroundColor2, basicCultureObject2.ForegroundColor2, banner2, false, true, true);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00088159 File Offset: 0x00086359
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			registerer.RegisterBaseHandler<RequestForfeitSpawn>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestForfeitSpawn));
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00088170 File Offset: 0x00086370
		public override void OnRemoveBehavior()
		{
			this.RoundController.OnRoundStarted -= this.OnPreparationStart;
			MissionPeer.OnPreTeamChanged -= this.OnPreTeamChanged;
			this.RoundController.OnPreparationEnded -= this.OnPreparationEnded;
			if (this.WarmupComponent != null)
			{
				this.WarmupComponent.OnWarmupEnding -= this.OnWarmupEnding;
			}
			this.RoundController.OnPreRoundEnding -= this.OnRoundEnd;
			this.RoundController.OnPostRoundEnded -= this.OnPostRoundEnd;
			base.OnRemoveBehavior();
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0008820F File Offset: 0x0008640F
		public override void OnPeerChangedTeam(NetworkCommunicator peer, Team oldTeam, Team newTeam)
		{
			if (oldTeam != null && oldTeam != newTeam && this.UseGold() && (this.WarmupComponent == null || !this.WarmupComponent.IsInWarmup))
			{
				base.ChangeCurrentGoldForPeer(peer.GetComponent<MissionPeer>(), 0);
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00088242 File Offset: 0x00086442
		private void OnPreparationStart()
		{
			this.NotificationsComponent.PreparationStarted();
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00088250 File Offset: 0x00086450
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this.MissionLobbyComponent.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing)
			{
				if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
				{
					this.CheckForPlayersSpawningAsBots();
					this.CheckPlayerBeingDetached();
				}
				if (this.RoundController.IsRoundInProgress && base.CanGameModeSystemsTickThisFrame)
				{
					if (!this._flagRemovalOccured)
					{
						this.CheckRemovingOfPoints();
					}
					this.CheckMorales();
					this.TickFlags();
				}
			}
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000882B8 File Offset: 0x000864B8
		private void CheckForPlayersSpawningAsBots()
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.IsSynchronized)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (component != null && component.ControlledAgent == null && component.Team != null && component.ControlledFormation != null && component.SpawnCountThisRound > 0)
					{
						if (!component.HasSpawnTimerExpired && component.SpawnTimer.Check(base.Mission.CurrentTime))
						{
							component.HasSpawnTimerExpired = true;
						}
						if (component.HasSpawnTimerExpired && component.WantsToSpawnAsBot)
						{
							if (component.ControlledFormation.HasUnitsWithCondition((Agent agent) => agent.IsActive() && agent.IsAIControlled))
							{
								Agent newAgent = null;
								Agent followingAgent = component.FollowedAgent;
								if (followingAgent != null && followingAgent.IsActive() && followingAgent.IsAIControlled && component.ControlledFormation.HasUnitsWithCondition((Agent agent) => agent == followingAgent))
								{
									newAgent = followingAgent;
								}
								else
								{
									float maxHealth = 0f;
									component.ControlledFormation.ApplyActionOnEachUnit(delegate(Agent agent)
									{
										if (agent.Health > maxHealth)
										{
											maxHealth = agent.Health;
											newAgent = agent;
										}
									}, null);
								}
								Mission.Current.ReplaceBotWithPlayer(newAgent, component);
								component.WantsToSpawnAsBot = false;
								component.HasSpawnTimerExpired = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0008847C File Offset: 0x0008667C
		private bool GetMoraleGain(out float moraleGain)
		{
			List<FlagCapturePoint> source = (from flag in this.AllCapturePoints
			where !flag.IsDeactivated && this.GetFlagOwnerTeam(flag) != null && flag.IsFullyRaised
			select flag).ToList<FlagCapturePoint>();
			int f = source.Count((FlagCapturePoint flag) => this.GetFlagOwnerTeam(flag).Side == BattleSideEnum.Attacker) - source.Count((FlagCapturePoint flag) => this.GetFlagOwnerTeam(flag).Side == BattleSideEnum.Defender);
			int num = MathF.Sign(f);
			moraleGain = 0f;
			if (num != 0)
			{
				float num2 = 0.000625f * this._moraleMultiplierForEachFlag * (float)MathF.Abs(f);
				if (num > 0)
				{
					moraleGain = MBMath.ClampFloat((float)num - this._morale, 1f, 2f) * num2;
				}
				else
				{
					moraleGain = MBMath.ClampFloat((float)num - this._morale, -2f, -1f) * num2;
				}
				if (this._flagRemovalOccured)
				{
					moraleGain *= this._moraleMultiplierOnLastFlag;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00088548 File Offset: 0x00086748
		public float GetTimeUntilBattleSideVictory(BattleSideEnum side)
		{
			float a = float.MaxValue;
			if ((side == BattleSideEnum.Attacker && this._morale > 0f) || (side == BattleSideEnum.Defender && this._morale < 0f))
			{
				a = this.RoundController.RemainingRoundTime;
			}
			float b = float.MaxValue;
			float num;
			this.GetMoraleGain(out num);
			if (side == BattleSideEnum.Attacker && num > 0f)
			{
				b = (1f - this._morale) / num;
			}
			else if (side == BattleSideEnum.Defender && num < 0f)
			{
				b = (-1f - this._morale) / (num / 0.25f);
			}
			return MathF.Min(a, b);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000885DC File Offset: 0x000867DC
		private void CheckMorales()
		{
			float num;
			if (this.GetMoraleGain(out num))
			{
				this._morale += num;
				this._morale = MBMath.ClampFloat(this._morale, -1f, 1f);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new FlagDominationMoraleChangeMessage(this.MoraleRounded));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				MissionMultiplayerGameModeFlagDominationClient gameModeFlagDominationClient = this._gameModeFlagDominationClient;
				if (gameModeFlagDominationClient != null)
				{
					gameModeFlagDominationClient.OnMoraleChanged(this.MoraleRounded);
				}
				MPPerkObject.RaiseEventForAllPeers(MPPerkCondition.PerkEventFlags.MoraleChange);
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00088658 File Offset: 0x00086858
		private void CheckRemovingOfPoints()
		{
			if (this._nextTimeToCheckForPointRemoval < 0f)
			{
				this._nextTimeToCheckForPointRemoval = base.Mission.CurrentTime + this._pointRemovalTimeInSeconds;
			}
			if (base.Mission.CurrentTime >= this._nextTimeToCheckForPointRemoval)
			{
				this._nextTimeToCheckForPointRemoval += this._pointRemovalTimeInSeconds;
				List<BattleSideEnum> list = new List<BattleSideEnum>();
				using (List<Team>.Enumerator enumerator = base.Mission.Teams.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Team team = enumerator.Current;
						if (team.Side != BattleSideEnum.None)
						{
							int num = team.Side * BattleSideEnum.NumSides - BattleSideEnum.Attacker;
							if (this.AllCapturePoints.All((FlagCapturePoint cp) => this.GetFlagOwnerTeam(cp) != team))
							{
								if (this.AllCapturePoints.FirstOrDefault((FlagCapturePoint cp) => this.GetFlagOwnerTeam(cp) == null) != null)
								{
									this._morale -= 0.1f * (float)num;
									list.Add(BattleSideEnum.None);
								}
								else
								{
									this._morale -= 0.1f * (float)num * 2f;
									list.Add(team.Side.GetOppositeSide());
								}
								this._morale = MBMath.ClampFloat(this._morale, -1f, 1f);
							}
							else
							{
								list.Add(team.Side);
							}
						}
					}
				}
				List<int> removedCapIndexList = new List<int>();
				MBList<FlagCapturePoint> mblist = this.AllCapturePoints.ToMBList<FlagCapturePoint>();
				using (List<BattleSideEnum>.Enumerator enumerator2 = list.GetEnumerator())
				{
					Func<FlagCapturePoint, bool> <>9__4;
					while (enumerator2.MoveNext())
					{
						BattleSideEnum side = enumerator2.Current;
						if (side == BattleSideEnum.None)
						{
							removedCapIndexList.Add(this.RemoveCapturePoint(mblist.GetRandomElementWithPredicate((FlagCapturePoint cp) => this.GetFlagOwnerTeam(cp) == null)));
						}
						else
						{
							List<FlagCapturePoint> list2 = (from cp in mblist
							where this.GetFlagOwnerTeam(cp) != null && this.GetFlagOwnerTeam(cp).Side == side
							select cp).ToList<FlagCapturePoint>();
							MBList<FlagCapturePoint> mblist2 = (from cp in list2
							where this.GetNumberOfAttackersAroundFlag(cp) == 0
							select cp).ToMBList<FlagCapturePoint>();
							if (mblist2.Count > 0)
							{
								removedCapIndexList.Add(this.RemoveCapturePoint(mblist2.GetRandomElement<FlagCapturePoint>()));
							}
							else
							{
								MBList<KeyValuePair<FlagCapturePoint, int>> mblist3 = new MBList<KeyValuePair<FlagCapturePoint, int>>();
								foreach (FlagCapturePoint flagCapturePoint in list2)
								{
									if (mblist3.Count == 0)
									{
										mblist3.Add(new KeyValuePair<FlagCapturePoint, int>(flagCapturePoint, this.GetNumberOfAttackersAroundFlag(flagCapturePoint)));
									}
									else
									{
										int count = this.GetNumberOfAttackersAroundFlag(flagCapturePoint);
										if (mblist3.Any((KeyValuePair<FlagCapturePoint, int> cc) => cc.Value > count))
										{
											mblist3.Clear();
											mblist3.Add(new KeyValuePair<FlagCapturePoint, int>(flagCapturePoint, count));
										}
										else if (mblist3.Any((KeyValuePair<FlagCapturePoint, int> cc) => cc.Value == count))
										{
											mblist3.Add(new KeyValuePair<FlagCapturePoint, int>(flagCapturePoint, count));
										}
									}
								}
								removedCapIndexList.Add(this.RemoveCapturePoint(mblist3.GetRandomElement<KeyValuePair<FlagCapturePoint, int>>().Key));
							}
						}
						IEnumerable<FlagCapturePoint> source = mblist;
						Func<FlagCapturePoint, bool> predicate;
						if ((predicate = <>9__4) == null)
						{
							predicate = (<>9__4 = ((FlagCapturePoint fl) => fl.FlagIndex == removedCapIndexList[removedCapIndexList.Count - 1]));
						}
						FlagCapturePoint item = source.First(predicate);
						mblist.Remove(item);
					}
				}
				removedCapIndexList.Sort();
				int first = removedCapIndexList[0];
				int second = removedCapIndexList[1];
				FlagCapturePoint flagCapturePoint2 = this.AllCapturePoints.First((FlagCapturePoint cp) => cp.FlagIndex != first && cp.FlagIndex != second);
				this.NotificationsComponent.FlagXRemaining(flagCapturePoint2);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new FlagDominationMoraleChangeMessage(this.MoraleRounded));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new FlagDominationFlagsRemovedMessage());
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				this._flagRemovalOccured = true;
				MissionMultiplayerGameModeFlagDominationClient gameModeFlagDominationClient = this._gameModeFlagDominationClient;
				if (gameModeFlagDominationClient != null)
				{
					gameModeFlagDominationClient.OnNumberOfFlagsChanged();
				}
				foreach (MissionBehavior missionBehavior in base.Mission.MissionBehaviors)
				{
					IFlagRemoved flagRemoved = missionBehavior as IFlagRemoved;
					if (flagRemoved != null)
					{
						flagRemoved.OnFlagsRemoved(flagCapturePoint2.FlagIndex);
					}
				}
				MPPerkObject.RaiseEventForAllPeers(MPPerkCondition.PerkEventFlags.FlagRemoval);
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00088B74 File Offset: 0x00086D74
		private int RemoveCapturePoint(FlagCapturePoint capToRemove)
		{
			int flagIndex = capToRemove.FlagIndex;
			capToRemove.RemovePointAsServer();
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new FlagDominationCapturePointMessage(flagIndex, -1));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
			return flagIndex;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00088B9C File Offset: 0x00086D9C
		public override void OnClearScene()
		{
			base.OnClearScene();
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>().ToMBList<FlagCapturePoint>();
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				flagCapturePoint.ResetPointAsServer(4284111450U, uint.MaxValue);
				this._capturePointOwners[flagCapturePoint.FlagIndex] = null;
			}
			this._morale = 0f;
			this._nextTimeToCheckForPointRemoval = float.MinValue;
			this._flagRemovalOccured = false;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00088C40 File Offset: 0x00086E40
		public override bool CheckIfOvertime()
		{
			if (!this._flagRemovalOccured)
			{
				return false;
			}
			FlagCapturePoint flagCapturePoint = this.AllCapturePoints.FirstOrDefault((FlagCapturePoint flag) => !flag.IsDeactivated);
			Team flagOwnerTeam = this.GetFlagOwnerTeam(flagCapturePoint);
			return flagOwnerTeam != null && ((float)(flagOwnerTeam.Side * BattleSideEnum.NumSides - BattleSideEnum.Attacker) * this._morale < 0f || this.GetNumberOfAttackersAroundFlag(flagCapturePoint) > 0);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00088CB8 File Offset: 0x00086EB8
		public override bool CheckForWarmupEnd()
		{
			int[] array = new int[2];
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
				if (networkCommunicator.IsSynchronized && ((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None)
				{
					array[(int)component.Team.Side]++;
				}
			}
			return array.Sum() >= MultiplayerOptions.OptionType.MaxNumberOfPlayers.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00088D5C File Offset: 0x00086F5C
		public override bool CheckForRoundEnd()
		{
			if (base.CanGameModeSystemsTickThisFrame)
			{
				if (MathF.Abs(this._morale) >= 1f)
				{
					if (!this._flagRemovalOccured)
					{
						return true;
					}
					FlagCapturePoint flagCapturePoint = this.AllCapturePoints.FirstOrDefault((FlagCapturePoint flag) => !flag.IsDeactivated);
					Team flagOwnerTeam = this.GetFlagOwnerTeam(flagCapturePoint);
					if (flagOwnerTeam == null)
					{
						return true;
					}
					BattleSideEnum battleSideEnum = (this._morale > 0f) ? BattleSideEnum.Attacker : BattleSideEnum.Defender;
					return flagOwnerTeam.Side == battleSideEnum && flagCapturePoint.IsFullyRaised && this.GetNumberOfAttackersAroundFlag(flagCapturePoint) == 0;
				}
				else
				{
					bool flag3 = base.Mission.AttackerTeam.ActiveAgents.Count > 0;
					bool flag2 = base.Mission.DefenderTeam.ActiveAgents.Count > 0;
					if (flag3 && flag2)
					{
						return false;
					}
					if (!base.SpawnComponent.AreAgentsSpawning())
					{
						return true;
					}
					bool[] array = new bool[2];
					if (this.UseGold())
					{
						foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
						{
							MissionPeer component = networkPeer.GetComponent<MissionPeer>();
							if (((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None && !array[(int)component.Team.Side])
							{
								string strValue = MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
								if (component.Team.Side != BattleSideEnum.Attacker)
								{
									strValue = MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
								}
								if (base.GetCurrentGoldForPeer(component) >= MultiplayerClassDivisions.GetMinimumTroopCost(MBObjectManager.Instance.GetObject<BasicCultureObject>(strValue)))
								{
									array[(int)component.Team.Side] = true;
								}
							}
						}
					}
					if ((!flag3 && !array[1]) || (!flag2 && !array[0]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00088F34 File Offset: 0x00087134
		public override bool UseCultureSelection()
		{
			return false;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00088F37 File Offset: 0x00087137
		private void OnWarmupEnding()
		{
			this.NotificationsComponent.WarmupEnding();
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00088F44 File Offset: 0x00087144
		private void OnRoundEnd()
		{
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				if (!flagCapturePoint.IsDeactivated)
				{
					flagCapturePoint.SetMoveNone();
				}
			}
			RoundEndReason roundEndReason = RoundEndReason.Invalid;
			bool flag = this.RoundController.RemainingRoundTime <= 0f && !this.CheckIfOvertime();
			int num = -1;
			for (int i = 0; i < 2; i++)
			{
				int num2 = i * 2 - 1;
				if ((flag && (float)num2 * this._morale > 0f) || (!flag && (float)num2 * this._morale >= 1f))
				{
					num = i;
					break;
				}
			}
			CaptureTheFlagCaptureResultEnum captureTheFlagCaptureResultEnum = CaptureTheFlagCaptureResultEnum.NotCaptured;
			if (num >= 0)
			{
				captureTheFlagCaptureResultEnum = ((num == 0) ? CaptureTheFlagCaptureResultEnum.DefendersWin : CaptureTheFlagCaptureResultEnum.AttackersWin);
				this.RoundController.RoundWinner = ((num == 0) ? BattleSideEnum.Defender : BattleSideEnum.Attacker);
				roundEndReason = (flag ? RoundEndReason.RoundTimeEnded : RoundEndReason.GameModeSpecificEnded);
			}
			else
			{
				bool flag2 = base.Mission.AttackerTeam.ActiveAgents.Count > 0;
				bool flag3 = base.Mission.DefenderTeam.ActiveAgents.Count > 0;
				if (flag2 && flag3)
				{
					if (this._morale > 0f)
					{
						captureTheFlagCaptureResultEnum = CaptureTheFlagCaptureResultEnum.AttackersWin;
						this.RoundController.RoundWinner = BattleSideEnum.Attacker;
					}
					else if (this._morale < 0f)
					{
						captureTheFlagCaptureResultEnum = CaptureTheFlagCaptureResultEnum.DefendersWin;
						this.RoundController.RoundWinner = BattleSideEnum.Defender;
					}
					else
					{
						captureTheFlagCaptureResultEnum = CaptureTheFlagCaptureResultEnum.Draw;
						this.RoundController.RoundWinner = BattleSideEnum.None;
					}
					roundEndReason = RoundEndReason.RoundTimeEnded;
				}
				else if (flag2)
				{
					captureTheFlagCaptureResultEnum = CaptureTheFlagCaptureResultEnum.AttackersWin;
					this.RoundController.RoundWinner = BattleSideEnum.Attacker;
					roundEndReason = RoundEndReason.SideDepleted;
				}
				else if (flag3)
				{
					captureTheFlagCaptureResultEnum = CaptureTheFlagCaptureResultEnum.DefendersWin;
					this.RoundController.RoundWinner = BattleSideEnum.Defender;
					roundEndReason = RoundEndReason.SideDepleted;
				}
				else
				{
					foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
					{
						MissionPeer component = networkPeer.GetComponent<MissionPeer>();
						if (((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None)
						{
							string strValue = MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
							if (component.Team.Side != BattleSideEnum.Attacker)
							{
								strValue = MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
							}
							if (base.GetCurrentGoldForPeer(component) >= MultiplayerClassDivisions.GetMinimumTroopCost(MBObjectManager.Instance.GetObject<BasicCultureObject>(strValue)))
							{
								this.RoundController.RoundWinner = component.Team.Side;
								roundEndReason = RoundEndReason.SideDepleted;
								captureTheFlagCaptureResultEnum = ((component.Team.Side == BattleSideEnum.Attacker) ? CaptureTheFlagCaptureResultEnum.AttackersWin : CaptureTheFlagCaptureResultEnum.DefendersWin);
								break;
							}
						}
					}
				}
			}
			if (captureTheFlagCaptureResultEnum != CaptureTheFlagCaptureResultEnum.NotCaptured)
			{
				this.RoundController.RoundEndReason = roundEndReason;
				this.HandleRoundEnd(captureTheFlagCaptureResultEnum);
			}
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000891E0 File Offset: 0x000873E0
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			agent.UpdateSyncHealthToAllClients(true);
			if (agent.IsPlayerControlled)
			{
				agent.MissionPeer.GetComponent<FlagDominationMissionRepresentative>().UpdateSelectedClassServer(agent);
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00089204 File Offset: 0x00087404
		private void HandleRoundEnd(CaptureTheFlagCaptureResultEnum roundResult)
		{
			AgentVictoryLogic missionBehavior = base.Mission.GetMissionBehavior<AgentVictoryLogic>();
			if (missionBehavior == null)
			{
				Debug.FailedAssert("Agent victory logic should not be null after someone just won/lost!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\Multiplayer\\MissionNetworkLogics\\MultiplayerGameModeLogics\\ServerGameModeLogics\\MissionMultiplayerFlagDomination.cs", "HandleRoundEnd", 780);
				return;
			}
			if (roundResult == CaptureTheFlagCaptureResultEnum.AttackersWin)
			{
				missionBehavior.SetTimersOfVictoryReactionsOnBattleEnd(BattleSideEnum.Attacker);
				return;
			}
			if (roundResult != CaptureTheFlagCaptureResultEnum.DefendersWin)
			{
				return;
			}
			missionBehavior.SetTimersOfVictoryReactionsOnBattleEnd(BattleSideEnum.Defender);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00089254 File Offset: 0x00087454
		private void OnPostRoundEnd()
		{
			if (this.UseGold() && !this.RoundController.IsMatchEnding)
			{
				foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkPeer.GetComponent<MissionPeer>();
					if (component != null && this.RoundController.RoundCount > 0)
					{
						int num = MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection;
						int num2 = base.GetCurrentGoldForPeer(component);
						if (num2 < 0)
						{
							num2 = MissionMultiplayerFlagDomination._maxGoldAmountToCarryOver;
						}
						else if (component.Team != null && component.Team.Side != BattleSideEnum.None && this.RoundController.RoundWinner == component.Team.Side && component.GetComponent<FlagDominationMissionRepresentative>().CheckIfSurvivedLastRoundAndReset())
						{
							num2 += MissionMultiplayerFlagDomination._maxGoldAmountToCarryOverForSurvival;
						}
						num += MBMath.ClampInt(num2, 0, MissionMultiplayerFlagDomination._maxGoldAmountToCarryOver);
						if (num > MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection)
						{
							int carriedGoldAmount = num - MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection;
							this.NotificationsComponent.GoldCarriedFromPreviousRound(carriedGoldAmount, component.GetNetworkPeer());
						}
						base.ChangeCurrentGoldForPeer(component, num);
					}
				}
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00089374 File Offset: 0x00087574
		protected override void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
			if (this.RoundController.IsRoundInProgress && MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
			{
				this.MakePlayerFormationCharge(networkPeer);
			}
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00089395 File Offset: 0x00087595
		private void OnPreTeamChanged(NetworkCommunicator peer, Team currentTeam, Team newTeam)
		{
			if (peer.IsSynchronized && peer.GetComponent<MissionPeer>().ControlledAgent != null)
			{
				this.MakePlayerFormationCharge(peer);
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000893B4 File Offset: 0x000875B4
		private void OnPreparationEnded()
		{
			if (this.UseGold())
			{
				List<MissionPeer>[] array = new List<MissionPeer>[2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new List<MissionPeer>();
				}
				foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkPeer.GetComponent<MissionPeer>();
					if (component != null && component.Team != null && component.Team.Side != BattleSideEnum.None)
					{
						array[(int)component.Team.Side].Add(component);
					}
				}
				int num = array[1].Count - array[0].Count;
				BattleSideEnum battleSideEnum = (num == 0) ? BattleSideEnum.None : ((num < 0) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
				if (battleSideEnum != BattleSideEnum.None)
				{
					num = MathF.Abs(num);
					int count = array[(int)battleSideEnum].Count;
					if (count > 0)
					{
						int num2 = MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection * num / 10 / count * 10;
						foreach (MissionPeer peer in array[(int)battleSideEnum])
						{
							base.ChangeCurrentGoldForPeer(peer, base.GetCurrentGoldForPeer(peer) + num2);
						}
					}
				}
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000894FC File Offset: 0x000876FC
		private void CheckPlayerBeingDetached()
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.IsSynchronized)
				{
					MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
					if (this.PlayerDistanceToFormation(component) >= component.CaptainBeingDetachedThreshold)
					{
						this.MakePlayerFormationFollowPlayer(component.GetNetworkPeer());
					}
				}
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00089574 File Offset: 0x00087774
		private int PlayerDistanceToFormation(MissionPeer missionPeer)
		{
			float num = 0f;
			if (missionPeer != null && missionPeer.ControlledAgent != null && missionPeer.ControlledFormation != null)
			{
				float num2 = missionPeer.ControlledFormation.GetAveragePositionOfUnits(true, true).Distance(missionPeer.ControlledAgent.Position.AsVec2);
				float num3 = missionPeer.ControlledFormation.OrderPosition.Distance(missionPeer.ControlledAgent.Position.AsVec2);
				num += num2 + num3;
				if (missionPeer.ControlledFormation.PhysicalClass.IsMounted())
				{
					num *= 0.8f;
				}
			}
			return (int)num;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00089614 File Offset: 0x00087814
		private void MakePlayerFormationFollowPlayer(NetworkCommunicator peer)
		{
			if (peer.IsSynchronized)
			{
				MissionPeer component = peer.GetComponent<MissionPeer>();
				if (component.ControlledFormation != null)
				{
					component.ControlledFormation.SetMovementOrder(MovementOrder.MovementOrderFollow(component.ControlledAgent));
					this.NotificationsComponent.FormationAutoFollowEnforced(peer);
				}
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0008965C File Offset: 0x0008785C
		private void MakePlayerFormationCharge(NetworkCommunicator peer)
		{
			if (peer.IsSynchronized)
			{
				MissionPeer component = peer.GetComponent<MissionPeer>();
				if (component.ControlledFormation != null)
				{
					component.ControlledFormation.SetMovementOrder(MovementOrder.MovementOrderCharge);
				}
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00089690 File Offset: 0x00087890
		protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			networkPeer.AddComponent<FlagDominationMissionRepresentative>();
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0008969C File Offset: 0x0008789C
		protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			if (this.UseGold())
			{
				int num = (this._gameType == MultiplayerGameType.Battle) ? 200 : MissionMultiplayerFlagDomination._defaultGoldAmountForTroopSelection;
				int num2 = (!this.RoundController.IsRoundInProgress) ? num : 0;
				base.ChangeCurrentGoldForPeer(networkPeer.GetComponent<MissionPeer>(), num2);
				MissionMultiplayerGameModeFlagDominationClient gameModeFlagDominationClient = this._gameModeFlagDominationClient;
				if (gameModeFlagDominationClient != null)
				{
					gameModeFlagDominationClient.OnGoldAmountChangedForRepresentative(networkPeer.GetComponent<FlagDominationMissionRepresentative>(), num2);
				}
			}
			if (this.AllCapturePoints != null && !networkPeer.IsServerPeer)
			{
				foreach (FlagCapturePoint flagCapturePoint in from cp in this.AllCapturePoints
				where !cp.IsDeactivated
				select cp)
				{
					GameNetwork.BeginModuleEventAsServer(networkPeer);
					int flagIndex = flagCapturePoint.FlagIndex;
					Team team = this._capturePointOwners[flagCapturePoint.FlagIndex];
					GameNetwork.WriteMessage(new FlagDominationCapturePointMessage(flagIndex, (team != null) ? team.TeamIndex : -1));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000897A8 File Offset: 0x000879A8
		private bool HandleClientEventRequestForfeitSpawn(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			this.ForfeitSpawning(peer);
			return true;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000897B4 File Offset: 0x000879B4
		public void ForfeitSpawning(NetworkCommunicator peer)
		{
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component != null && component.HasSpawnedAgentVisuals && this.UseGold() && this.RoundController.IsRoundInProgress)
			{
				Mission.Current.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>().RemoveAgentVisuals(component, true);
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new RemoveAgentVisualsForPeer(component.GetNetworkPeer()));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				}
				component.HasSpawnedAgentVisuals = false;
				base.ChangeCurrentGoldForPeer(component, -1);
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0008982C File Offset: 0x00087A2C
		public static void SetWinnerTeam(int winnerTeamNo)
		{
			Mission mission = Mission.Current;
			MissionMultiplayerFlagDomination missionBehavior = mission.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			if (missionBehavior != null)
			{
				foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkPeer.GetComponent<MissionPeer>();
					missionBehavior.ChangeCurrentGoldForPeer(component, 0);
				}
				for (int i = mission.Agents.Count - 1; i >= 0; i--)
				{
					Agent agent = mission.Agents[i];
					if (agent.IsHuman && agent.Team.MBTeam.Index != winnerTeamNo + 1)
					{
						Mission.Current.KillAgentCheat(agent);
					}
				}
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000898F0 File Offset: 0x00087AF0
		private void TickFlags()
		{
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				if (!flagCapturePoint.IsDeactivated)
				{
					for (int i = 0; i < 2; i++)
					{
						this._agentCountsOnSide[i] = 0;
					}
					Team team = this._capturePointOwners[flagCapturePoint.FlagIndex];
					Agent agent = null;
					float num = 16f;
					AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, flagCapturePoint.Position.AsVec2, 6f, false);
					while (proximityMapSearchStruct.LastFoundAgent != null)
					{
						Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
						if (lastFoundAgent.IsHuman && lastFoundAgent.IsActive())
						{
							this._agentCountsOnSide[(int)lastFoundAgent.Team.Side]++;
							float num2 = lastFoundAgent.Position.DistanceSquared(flagCapturePoint.Position);
							if (num2 <= num)
							{
								agent = lastFoundAgent;
								num = num2;
							}
						}
						AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
					}
					ValueTuple<int, int> valueTuple = ValueTuple.Create<int, int>(this._agentCountsOnSide[0], this._agentCountsOnSide[1]);
					bool flag = valueTuple.Item1 != this._defenderAttackerCountsInFlagArea[flagCapturePoint.FlagIndex].Item1 || valueTuple.Item2 != this._defenderAttackerCountsInFlagArea[flagCapturePoint.FlagIndex].Item2;
					this._defenderAttackerCountsInFlagArea[flagCapturePoint.FlagIndex] = valueTuple;
					bool isContested = flagCapturePoint.IsContested;
					float speedMultiplier = 1f;
					if (agent != null)
					{
						BattleSideEnum side = agent.Team.Side;
						BattleSideEnum oppositeSide = side.GetOppositeSide();
						if (this._agentCountsOnSide[(int)oppositeSide] != 0)
						{
							float num3 = (float)Math.Min(this._agentCountsOnSide[(int)side], 200);
							int num4 = Math.Min(this._agentCountsOnSide[(int)oppositeSide], 200);
							float val = (MathF.Log10(num3) + 1f) / (2f * (MathF.Log10((float)num4) + 1f)) - 0.09f;
							speedMultiplier = Math.Min(1f, val);
						}
					}
					if (team == null)
					{
						if (!isContested && agent != null)
						{
							flagCapturePoint.SetMoveFlag(CaptureTheFlagFlagDirection.Down, speedMultiplier);
						}
						else if (agent == null && isContested)
						{
							flagCapturePoint.SetMoveFlag(CaptureTheFlagFlagDirection.Up, speedMultiplier);
						}
						else if (flag)
						{
							flagCapturePoint.ChangeMovementSpeed(speedMultiplier);
						}
					}
					else if (agent != null)
					{
						if (agent.Team != team && !isContested)
						{
							flagCapturePoint.SetMoveFlag(CaptureTheFlagFlagDirection.Down, speedMultiplier);
						}
						else if (agent.Team == team && isContested)
						{
							flagCapturePoint.SetMoveFlag(CaptureTheFlagFlagDirection.Up, speedMultiplier);
						}
						else if (flag)
						{
							flagCapturePoint.ChangeMovementSpeed(speedMultiplier);
						}
					}
					else if (isContested)
					{
						flagCapturePoint.SetMoveFlag(CaptureTheFlagFlagDirection.Up, speedMultiplier);
					}
					else if (flag)
					{
						flagCapturePoint.ChangeMovementSpeed(speedMultiplier);
					}
					bool flag2;
					flagCapturePoint.OnAfterTick(agent != null, out flag2);
					if (flag2)
					{
						Team team2 = agent.Team;
						uint color = (team2 != null) ? team2.Color : 4284111450U;
						uint color2 = (team2 != null) ? team2.Color2 : uint.MaxValue;
						flagCapturePoint.SetTeamColorsWithAllSynched(color, color2);
						this._capturePointOwners[flagCapturePoint.FlagIndex] = team2;
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new FlagDominationCapturePointMessage(flagCapturePoint.FlagIndex, (team2 != null) ? team2.TeamIndex : -1));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
						MissionMultiplayerGameModeFlagDominationClient gameModeFlagDominationClient = this._gameModeFlagDominationClient;
						if (gameModeFlagDominationClient != null)
						{
							gameModeFlagDominationClient.OnCapturePointOwnerChanged(flagCapturePoint, team2);
						}
						this.NotificationsComponent.FlagXCapturedByTeamX(flagCapturePoint, agent.Team);
						MPPerkObject.RaiseEventForAllPeers(MPPerkCondition.PerkEventFlags.FlagCapture);
					}
				}
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x00089C68 File Offset: 0x00087E68
		public int GetNumberOfAttackersAroundFlag(FlagCapturePoint capturePoint)
		{
			Team flagOwnerTeam = this.GetFlagOwnerTeam(capturePoint);
			if (flagOwnerTeam == null)
			{
				return 0;
			}
			int num = 0;
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, capturePoint.Position.AsVec2, 6f, false);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				if (lastFoundAgent.IsHuman && lastFoundAgent.IsActive() && lastFoundAgent.Position.DistanceSquared(capturePoint.Position) <= 36f && lastFoundAgent.Team.Side != flagOwnerTeam.Side)
				{
					num++;
				}
				AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
			}
			return num;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00089D0A File Offset: 0x00087F0A
		public Team GetFlagOwnerTeam(FlagCapturePoint flag)
		{
			if (flag == null)
			{
				return null;
			}
			return this._capturePointOwners[flag.FlagIndex];
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00089D20 File Offset: 0x00087F20
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
			if (this.UseGold() && affectorAgent != null && affectedAgent != null && affectedAgent.IsHuman && blow.DamageType != DamageTypes.Invalid && (agentState == AgentState.Unconscious || agentState == AgentState.Killed))
			{
				bool flag = affectorAgent.Team != null && affectedAgent.Team != null && affectorAgent.Team.Side == affectedAgent.Team.Side;
				Agent.Hitter assistingHitter = affectedAgent.GetAssistingHitter(affectorAgent.MissionPeer);
				MultiplayerClassDivisions.MPHeroClass mpheroClassForCharacter = MultiplayerClassDivisions.GetMPHeroClassForCharacter(affectedAgent.Character);
				FlagDominationMissionRepresentative flagDominationMissionRepresentative;
				if (affectorAgent.MissionPeer != null && (flagDominationMissionRepresentative = (affectorAgent.MissionPeer.Representative as FlagDominationMissionRepresentative)) != null)
				{
					int goldGainsFromKillData = flagDominationMissionRepresentative.GetGoldGainsFromKillData(MPPerkObject.GetPerkHandler(affectorAgent.MissionPeer), MPPerkObject.GetPerkHandler((assistingHitter != null) ? assistingHitter.HitterPeer : null), mpheroClassForCharacter, false, flag);
					if (goldGainsFromKillData > 0)
					{
						base.ChangeCurrentGoldForPeer(affectorAgent.MissionPeer, flagDominationMissionRepresentative.Gold + goldGainsFromKillData);
					}
				}
				FlagDominationMissionRepresentative flagDominationMissionRepresentative2;
				if (((assistingHitter != null) ? assistingHitter.HitterPeer : null) != null && assistingHitter.HitterPeer.Peer.Communicator.IsConnectionActive && !assistingHitter.IsFriendlyHit && (flagDominationMissionRepresentative2 = (assistingHitter.HitterPeer.Representative as FlagDominationMissionRepresentative)) != null)
				{
					int goldGainsFromKillData2 = flagDominationMissionRepresentative2.GetGoldGainsFromKillData(MPPerkObject.GetPerkHandler(affectorAgent.MissionPeer), MPPerkObject.GetPerkHandler(assistingHitter.HitterPeer), mpheroClassForCharacter, true, flag);
					if (goldGainsFromKillData2 > 0)
					{
						base.ChangeCurrentGoldForPeer(assistingHitter.HitterPeer, flagDominationMissionRepresentative2.Gold + goldGainsFromKillData2);
					}
				}
				MissionPeer missionPeer = affectedAgent.MissionPeer;
				if (((missionPeer != null) ? missionPeer.Team : null) != null && !flag)
				{
					MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(affectedAgent.MissionPeer);
					IEnumerable<ValueTuple<MissionPeer, int>> enumerable = (perkHandler != null) ? perkHandler.GetTeamGoldRewardsOnDeath() : null;
					if (enumerable != null)
					{
						foreach (ValueTuple<MissionPeer, int> valueTuple in enumerable)
						{
							MissionPeer item = valueTuple.Item1;
							int item2 = valueTuple.Item2;
							FlagDominationMissionRepresentative flagDominationMissionRepresentative3;
							if (item2 > 0 && (flagDominationMissionRepresentative3 = (((item != null) ? item.Representative : null) as FlagDominationMissionRepresentative)) != null)
							{
								int goldGainsFromAllyDeathReward = flagDominationMissionRepresentative3.GetGoldGainsFromAllyDeathReward(item2);
								if (goldGainsFromAllyDeathReward > 0)
								{
									base.ChangeCurrentGoldForPeer(item, flagDominationMissionRepresentative3.Gold + goldGainsFromAllyDeathReward);
								}
							}
						}
					}
				}
			}
			if (affectedAgent.IsPlayerControlled)
			{
				affectedAgent.MissionPeer.GetComponent<FlagDominationMissionRepresentative>().UpdateSelectedClassServer(null);
			}
			else if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0 && (this.WarmupComponent == null || !this.WarmupComponent.IsInWarmup) && !affectedAgent.IsMount && affectedAgent.OwningAgentMissionPeer != null && affectedAgent.Formation != null && affectedAgent.Formation.CountOfUnits == 1)
			{
				if (!GameNetwork.IsDedicatedServer)
				{
					MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
					Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/squad_wiped"), position);
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new FormationWipedMessage());
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, affectedAgent.OwningAgentMissionPeer.GetNetworkPeer());
			}
			if (this._gameType == MultiplayerGameType.Battle && affectedAgent.IsHuman && this.RoundController.IsRoundInProgress && blow.DamageType != DamageTypes.Invalid && (agentState == AgentState.Unconscious || agentState == AgentState.Killed))
			{
				MultiplayerClassDivisions.MPHeroClass mpheroClassForCharacter2 = MultiplayerClassDivisions.GetMPHeroClassForCharacter(affectedAgent.Character);
				if (((affectorAgent != null) ? affectorAgent.MissionPeer : null) != null && affectorAgent.Team != affectedAgent.Team)
				{
					FlagDominationMissionRepresentative flagDominationMissionRepresentative4 = affectorAgent.MissionPeer.Representative as FlagDominationMissionRepresentative;
					int goldGainFromKillDataAndUpdateFlags = flagDominationMissionRepresentative4.GetGoldGainFromKillDataAndUpdateFlags(mpheroClassForCharacter2, false);
					base.ChangeCurrentGoldForPeer(affectorAgent.MissionPeer, flagDominationMissionRepresentative4.Gold + goldGainFromKillDataAndUpdateFlags);
				}
				Agent.Hitter assistingHitter2 = affectedAgent.GetAssistingHitter((affectorAgent != null) ? affectorAgent.MissionPeer : null);
				if (((assistingHitter2 != null) ? assistingHitter2.HitterPeer : null) != null && !assistingHitter2.IsFriendlyHit)
				{
					FlagDominationMissionRepresentative flagDominationMissionRepresentative5 = assistingHitter2.HitterPeer.Representative as FlagDominationMissionRepresentative;
					int goldGainFromKillDataAndUpdateFlags2 = flagDominationMissionRepresentative5.GetGoldGainFromKillDataAndUpdateFlags(mpheroClassForCharacter2, true);
					base.ChangeCurrentGoldForPeer(assistingHitter2.HitterPeer, flagDominationMissionRepresentative5.Gold + goldGainFromKillDataAndUpdateFlags2);
				}
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0008A120 File Offset: 0x00088320
		public override float GetTroopNumberMultiplierForMissingPlayer(MissionPeer spawningPeer)
		{
			if (this._gameType == MultiplayerGameType.Captain)
			{
				List<MissionPeer>[] array = new List<MissionPeer>[2];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new List<MissionPeer>();
				}
				foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
				{
					MissionPeer component = networkPeer.GetComponent<MissionPeer>();
					if (component != null && component.Team != null && component.Team.Side != BattleSideEnum.None)
					{
						array[(int)component.Team.Side].Add(component);
					}
				}
				int[] array2 = new int[]
				{
					0,
					MultiplayerOptions.OptionType.NumberOfBotsTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
				};
				array2[0] = MultiplayerOptions.OptionType.NumberOfBotsTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				int num = array[1].Count + array2[1] - (array[0].Count + array2[0]);
				BattleSideEnum battleSideEnum = (num == 0) ? BattleSideEnum.None : ((num < 0) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
				if (battleSideEnum == spawningPeer.Team.Side)
				{
					num = MathF.Abs(num);
					int num2 = array[(int)battleSideEnum].Count + array2[(int)battleSideEnum];
					return 1f + (float)num / (float)num2;
				}
			}
			return 1f;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0008A24C File Offset: 0x0008844C
		protected override void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			if (!networkPeer.IsServerPeer)
			{
				GameNetwork.BeginModuleEventAsServer(networkPeer);
				GameNetwork.WriteMessage(new FlagDominationMoraleChangeMessage(this.MoraleRounded));
				GameNetwork.EndModuleEventAsServer();
			}
		}

		// Token: 0x04000D61 RID: 3425
		public const int NumberOfFlagsInGame = 3;

		// Token: 0x04000D62 RID: 3426
		public const float MoraleRoundPrecision = 0.01f;

		// Token: 0x04000D63 RID: 3427
		public const int DefaultGoldAmountForTroopSelectionForSkirmish = 300;

		// Token: 0x04000D64 RID: 3428
		public const int MaxGoldAmountToCarryOverForSkirmish = 80;

		// Token: 0x04000D65 RID: 3429
		private const int MaxGoldAmountToCarryOverForSurvivalForSkirmish = 30;

		// Token: 0x04000D66 RID: 3430
		public const int InitialGoldAmountForTroopSelectionForBattle = 200;

		// Token: 0x04000D67 RID: 3431
		public const int DefaultGoldAmountForTroopSelectionForBattle = 120;

		// Token: 0x04000D68 RID: 3432
		public const int MaxGoldAmountToCarryOverForBattle = 110;

		// Token: 0x04000D69 RID: 3433
		private const int MaxGoldAmountToCarryOverForSurvivalForBattle = 20;

		// Token: 0x04000D6A RID: 3434
		private const float MoraleGainOnTick = 0.000625f;

		// Token: 0x04000D6B RID: 3435
		private const float MoralePenaltyPercentageIfNoPointsCaptured = 0.1f;

		// Token: 0x04000D6C RID: 3436
		private const float MoraleTickTimeInSeconds = 0.25f;

		// Token: 0x04000D6D RID: 3437
		public const float TimeTillFlagRemovalForPriorInfoInSeconds = 30f;

		// Token: 0x04000D6E RID: 3438
		public const float PointRemovalTimeInSecondsForBattle = 210f;

		// Token: 0x04000D6F RID: 3439
		public const float PointRemovalTimeInSecondsForCaptain = 180f;

		// Token: 0x04000D70 RID: 3440
		public const float PointRemovalTimeInSecondsForSkirmish = 120f;

		// Token: 0x04000D71 RID: 3441
		public const float MoraleMultiplierForEachFlagForBattle = 0.75f;

		// Token: 0x04000D72 RID: 3442
		public const float MoraleMultiplierForEachFlagForCaptain = 1f;

		// Token: 0x04000D73 RID: 3443
		private const float MoraleMultiplierOnLastFlagForBattle = 3.5f;

		// Token: 0x04000D74 RID: 3444
		private static int _defaultGoldAmountForTroopSelection = -1;

		// Token: 0x04000D75 RID: 3445
		private static int _maxGoldAmountToCarryOver = -1;

		// Token: 0x04000D76 RID: 3446
		private static int _maxGoldAmountToCarryOverForSurvival = -1;

		// Token: 0x04000D77 RID: 3447
		private const float MoraleMultiplierOnLastFlagForCaptainSkirmish = 2f;

		// Token: 0x04000D78 RID: 3448
		public const float MoraleMultiplierForEachFlagForSkirmish = 2f;

		// Token: 0x04000D79 RID: 3449
		private readonly float _pointRemovalTimeInSeconds = -1f;

		// Token: 0x04000D7A RID: 3450
		private readonly float _moraleMultiplierForEachFlag = -1f;

		// Token: 0x04000D7B RID: 3451
		private readonly float _moraleMultiplierOnLastFlag = -1f;

		// Token: 0x04000D7C RID: 3452
		private Team[] _capturePointOwners;

		// Token: 0x04000D7E RID: 3454
		private bool _flagRemovalOccured;

		// Token: 0x04000D7F RID: 3455
		private float _nextTimeToCheckForPointRemoval = float.MinValue;

		// Token: 0x04000D80 RID: 3456
		private MissionMultiplayerGameModeFlagDominationClient _gameModeFlagDominationClient;

		// Token: 0x04000D81 RID: 3457
		private float _morale;

		// Token: 0x04000D82 RID: 3458
		private readonly MultiplayerGameType _gameType;

		// Token: 0x04000D83 RID: 3459
		private int[] _agentCountsOnSide = new int[2];

		// Token: 0x04000D84 RID: 3460
		private ValueTuple<int, int>[] _defenderAttackerCountsInFlagArea = new ValueTuple<int, int>[3];
	}
}
