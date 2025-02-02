using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Objects;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A7 RID: 679
	public class MissionMultiplayerSiege : MissionMultiplayerGameModeBase, IAnalyticsFlagInfo, IMissionBehavior
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x0008AA69 File Offset: 0x00088C69
		public override bool IsGameModeHidingAllAgentVisuals
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0008AA6C File Offset: 0x00088C6C
		public override bool IsGameModeUsingOpposingTeams
		{
			get
			{
				return true;
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060024DD RID: 9437 RVA: 0x0008AA70 File Offset: 0x00088C70
		// (remove) Token: 0x060024DE RID: 9438 RVA: 0x0008AAA8 File Offset: 0x00088CA8
		public event MissionMultiplayerSiege.OnDestructableComponentDestroyedDelegate OnDestructableComponentDestroyed;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060024DF RID: 9439 RVA: 0x0008AAE0 File Offset: 0x00088CE0
		// (remove) Token: 0x060024E0 RID: 9440 RVA: 0x0008AB18 File Offset: 0x00088D18
		public event MissionMultiplayerSiege.OnObjectiveGoldGainedDelegate OnObjectiveGoldGained;

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0008AB4D File Offset: 0x00088D4D
		// (set) Token: 0x060024E2 RID: 9442 RVA: 0x0008AB55 File Offset: 0x00088D55
		public MBReadOnlyList<FlagCapturePoint> AllCapturePoints { get; private set; }

		// Token: 0x060024E3 RID: 9443 RVA: 0x0008AB60 File Offset: 0x00088D60
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._objectiveSystem = new MissionMultiplayerSiege.ObjectiveSystem();
			this._childDestructableComponents = new Dictionary<GameEntity, List<DestructableComponent>>();
			this._gameModeSiegeClient = Mission.Current.GetMissionBehavior<MissionMultiplayerSiegeClient>();
			this._warmupComponent = Mission.Current.GetMissionBehavior<MultiplayerWarmupComponent>();
			this._capturePointOwners = new Team[7];
			this._capturePointRemainingMoraleGains = new int[7];
			this._morales = new int[2];
			this._morales[1] = 360;
			this._morales[0] = 360;
			this.AllCapturePoints = Mission.Current.MissionObjects.FindAllWithType<FlagCapturePoint>().ToMBList<FlagCapturePoint>();
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				flagCapturePoint.SetTeamColorsSynched(4284111450U, uint.MaxValue);
				this._capturePointOwners[flagCapturePoint.FlagIndex] = null;
				this._capturePointRemainingMoraleGains[flagCapturePoint.FlagIndex] = 90;
				if (flagCapturePoint.GameEntity.HasTag("keep_capture_point"))
				{
					this._masterFlag = flagCapturePoint;
				}
			}
			foreach (DestructableComponent destructableComponent in Mission.Current.MissionObjects.FindAllWithType<DestructableComponent>())
			{
				if (destructableComponent.BattleSide != BattleSideEnum.None)
				{
					GameEntity root = destructableComponent.GameEntity.Root;
					if (this._objectiveSystem.RegisterObjective(root))
					{
						this._childDestructableComponents.Add(root, new List<DestructableComponent>());
						MissionMultiplayerSiege.GetDestructableCompoenentClosestToTheRoot(root).OnDestroyed += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.DestructableComponentOnDestroyed);
					}
					this._childDestructableComponents[root].Add(destructableComponent);
					destructableComponent.OnHitTaken += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.DestructableComponentOnHitTaken);
				}
			}
			List<RangedSiegeWeapon> list = new List<RangedSiegeWeapon>();
			List<IMoveableSiegeWeapon> list2 = new List<IMoveableSiegeWeapon>();
			foreach (UsableMachine usableMachine in Mission.Current.MissionObjects.FindAllWithType<UsableMachine>())
			{
				RangedSiegeWeapon rangedSiegeWeapon;
				IMoveableSiegeWeapon item;
				if ((rangedSiegeWeapon = (usableMachine as RangedSiegeWeapon)) != null)
				{
					list.Add(rangedSiegeWeapon);
					rangedSiegeWeapon.OnAgentLoadsMachine += this.RangedSiegeMachineOnAgentLoadsMachine;
				}
				else if ((item = (usableMachine as IMoveableSiegeWeapon)) != null)
				{
					list2.Add(item);
					this._objectiveSystem.RegisterObjective(usableMachine.GameEntity.Root);
				}
			}
			this._lastReloadingAgentPerRangedSiegeMachine = new ValueTuple<RangedSiegeWeapon, Agent>[list.Count];
			for (int i = 0; i < this._lastReloadingAgentPerRangedSiegeMachine.Length; i++)
			{
				this._lastReloadingAgentPerRangedSiegeMachine[i] = ValueTuple.Create<RangedSiegeWeapon, Agent>(list[i], null);
			}
			this._movingObjectives = new ValueTuple<IMoveableSiegeWeapon, Vec3>[list2.Count];
			for (int j = 0; j < this._movingObjectives.Length; j++)
			{
				SiegeWeapon siegeWeapon = list2[j] as SiegeWeapon;
				this._movingObjectives[j] = ValueTuple.Create<IMoveableSiegeWeapon, Vec3>(list2[j], siegeWeapon.GameEntity.GlobalPosition);
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0008AE8C File Offset: 0x0008908C
		private static DestructableComponent GetDestructableCompoenentClosestToTheRoot(GameEntity entity)
		{
			DestructableComponent destructableComponent = entity.GetFirstScriptOfType<DestructableComponent>();
			while (destructableComponent == null && entity.ChildCount != 0)
			{
				for (int i = 0; i < entity.ChildCount; i++)
				{
					destructableComponent = MissionMultiplayerSiege.GetDestructableCompoenentClosestToTheRoot(entity.GetChild(i));
					if (destructableComponent != null)
					{
						break;
					}
				}
			}
			return destructableComponent;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0008AED0 File Offset: 0x000890D0
		private void RangedSiegeMachineOnAgentLoadsMachine(RangedSiegeWeapon siegeWeapon, Agent reloadingAgent)
		{
			for (int i = 0; i < this._lastReloadingAgentPerRangedSiegeMachine.Length; i++)
			{
				if (this._lastReloadingAgentPerRangedSiegeMachine[i].Item1 == siegeWeapon)
				{
					this._lastReloadingAgentPerRangedSiegeMachine[i].Item2 = reloadingAgent;
				}
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0008AF18 File Offset: 0x00089118
		private void DestructableComponentOnHitTaken(DestructableComponent destructableComponent, Agent attackerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			if (!this.WarmupComponent.IsInWarmup)
			{
				GameEntity root = destructableComponent.GameEntity.Root;
				BatteringRam batteringRam;
				if ((batteringRam = (attackerScriptComponentBehavior as BatteringRam)) != null)
				{
					int userCountNotInStruckAction = batteringRam.UserCountNotInStruckAction;
					if (userCountNotInStruckAction <= 0)
					{
						goto IL_227;
					}
					float contribution = (float)inflictedDamage / (float)userCountNotInStruckAction;
					using (List<StandingPoint>.Enumerator enumerator = batteringRam.StandingPoints.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StandingPoint standingPoint = enumerator.Current;
							Agent userAgent = standingPoint.UserAgent;
							if (((userAgent != null) ? userAgent.MissionPeer : null) != null && !userAgent.IsInBeingStruckAction && userAgent.MissionPeer.Team.Side == destructableComponent.BattleSide.GetOppositeSide())
							{
								this._objectiveSystem.AddContributionForObjective(root, userAgent.MissionPeer, contribution);
							}
						}
						goto IL_227;
					}
				}
				bool flag;
				if (attackerAgent == null)
				{
					flag = (null != null);
				}
				else
				{
					MissionPeer missionPeer = attackerAgent.MissionPeer;
					flag = (((missionPeer != null) ? missionPeer.Team : null) != null);
				}
				if (flag && attackerAgent.MissionPeer.Team.Side == destructableComponent.BattleSide.GetOppositeSide())
				{
					StandingPoint standingPoint2;
					if (attackerAgent.CurrentlyUsedGameObject != null && (standingPoint2 = (attackerAgent.CurrentlyUsedGameObject as StandingPoint)) != null)
					{
						RangedSiegeWeapon firstScriptOfTypeInFamily = standingPoint2.GameEntity.GetFirstScriptOfTypeInFamily<RangedSiegeWeapon>();
						if (firstScriptOfTypeInFamily != null)
						{
							for (int i = 0; i < this._lastReloadingAgentPerRangedSiegeMachine.Length; i++)
							{
								if (this._lastReloadingAgentPerRangedSiegeMachine[i].Item1 == firstScriptOfTypeInFamily)
								{
									Agent item = this._lastReloadingAgentPerRangedSiegeMachine[i].Item2;
									if (((item != null) ? item.MissionPeer : null) != null)
									{
										Agent item2 = this._lastReloadingAgentPerRangedSiegeMachine[i].Item2;
										BattleSideEnum? battleSideEnum = (item2 != null) ? new BattleSideEnum?(item2.MissionPeer.Team.Side) : null;
										BattleSideEnum oppositeSide = destructableComponent.BattleSide.GetOppositeSide();
										if (battleSideEnum.GetValueOrDefault() == oppositeSide & battleSideEnum != null)
										{
											this._objectiveSystem.AddContributionForObjective(root, this._lastReloadingAgentPerRangedSiegeMachine[i].Item2.MissionPeer, (float)inflictedDamage * 0.33f);
										}
									}
								}
							}
						}
					}
					this._objectiveSystem.AddContributionForObjective(root, attackerAgent.MissionPeer, (float)inflictedDamage);
				}
				IL_227:
				if (destructableComponent.IsDestroyed)
				{
					destructableComponent.OnHitTaken -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.DestructableComponentOnHitTaken);
					this._childDestructableComponents[root].Remove(destructableComponent);
				}
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0008B18C File Offset: 0x0008938C
		private void DestructableComponentOnDestroyed(DestructableComponent destructableComponent, Agent attackerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			GameEntity root = destructableComponent.GameEntity.Root;
			List<KeyValuePair<MissionPeer, float>> allContributorsForSideAndClear = this._objectiveSystem.GetAllContributorsForSideAndClear(root, destructableComponent.BattleSide.GetOppositeSide());
			float num = allContributorsForSideAndClear.Sum((KeyValuePair<MissionPeer, float> ac) => ac.Value);
			List<MissionPeer> list = new List<MissionPeer>();
			foreach (KeyValuePair<MissionPeer, float> keyValuePair in allContributorsForSideAndClear)
			{
				int goldGainsFromObjectiveAssist = (keyValuePair.Key.Representative as SiegeMissionRepresentative).GetGoldGainsFromObjectiveAssist(root, keyValuePair.Value / num, false);
				if (goldGainsFromObjectiveAssist > 0)
				{
					base.ChangeCurrentGoldForPeer(keyValuePair.Key, keyValuePair.Key.Representative.Gold + goldGainsFromObjectiveAssist);
					list.Add(keyValuePair.Key);
					MissionMultiplayerSiege.OnObjectiveGoldGainedDelegate onObjectiveGoldGained = this.OnObjectiveGoldGained;
					if (onObjectiveGoldGained != null)
					{
						onObjectiveGoldGained(keyValuePair.Key, goldGainsFromObjectiveAssist);
					}
				}
			}
			destructableComponent.OnDestroyed -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.DestructableComponentOnDestroyed);
			foreach (DestructableComponent destructableComponent2 in this._childDestructableComponents[root])
			{
				destructableComponent2.OnHitTaken -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.DestructableComponentOnHitTaken);
			}
			this._childDestructableComponents.Remove(root);
			MissionMultiplayerSiege.OnDestructableComponentDestroyedDelegate onDestructableComponentDestroyed = this.OnDestructableComponentDestroyed;
			if (onDestructableComponentDestroyed == null)
			{
				return;
			}
			onDestructableComponentDestroyed(destructableComponent, attackerScriptComponentBehavior, list.ToArray());
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0008B320 File Offset: 0x00089520
		public override MultiplayerGameType GetMissionType()
		{
			return MultiplayerGameType.Siege;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0008B323 File Offset: 0x00089523
		public override bool UseRoundController()
		{
			return false;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0008B328 File Offset: 0x00089528
		public override void AfterStart()
		{
			BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			BasicCultureObject object2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			Banner banner = new Banner(@object.BannerKey, @object.BackgroundColor1, @object.ForegroundColor1);
			Banner banner2 = new Banner(object2.BannerKey, object2.BackgroundColor2, object2.ForegroundColor2);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, @object.BackgroundColor1, @object.ForegroundColor1, banner, true, false, true);
			base.Mission.Teams.Add(BattleSideEnum.Defender, object2.BackgroundColor2, object2.ForegroundColor2, banner2, true, false, true);
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				this._capturePointOwners[flagCapturePoint.FlagIndex] = base.Mission.Teams.Defender;
				flagCapturePoint.SetTeamColors(base.Mission.Teams.Defender.Color, base.Mission.Teams.Defender.Color2);
				MissionMultiplayerSiegeClient gameModeSiegeClient = this._gameModeSiegeClient;
				if (gameModeSiegeClient != null)
				{
					gameModeSiegeClient.OnCapturePointOwnerChanged(flagCapturePoint, base.Mission.Teams.Defender);
				}
			}
			if (this._warmupComponent != null)
			{
				this._warmupComponent.OnWarmupEnding += this.OnWarmupEnding;
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0008B4A4 File Offset: 0x000896A4
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this._firstTickDone)
			{
				foreach (CastleGate castleGate in Mission.Current.MissionObjects.FindAllWithType<CastleGate>())
				{
					castleGate.OpenDoor();
					foreach (StandingPoint standingPoint in castleGate.StandingPoints)
					{
						standingPoint.SetIsDeactivatedSynched(true);
					}
				}
				this._firstTickDone = true;
			}
			if (this.MissionLobbyComponent.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing && (this.WarmupComponent == null || !this.WarmupComponent.IsInWarmup))
			{
				this.CheckMorales(dt);
				if (this.CheckObjectives(dt))
				{
					this.TickFlags(dt);
					this.TickObjectives(dt);
				}
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0008B590 File Offset: 0x00089790
		private void CheckMorales(float dt)
		{
			this._dtSumCheckMorales += dt;
			if (this._dtSumCheckMorales >= 1f)
			{
				this._dtSumCheckMorales -= 1f;
				int num = MathF.Max(this._morales[1] + this.GetMoraleGain(BattleSideEnum.Attacker), 0);
				int num2 = MBMath.ClampInt(this._morales[0] + this.GetMoraleGain(BattleSideEnum.Defender), 0, 360);
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SiegeMoraleChangeMessage(num, num2, this._capturePointRemainingMoraleGains));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
				MissionMultiplayerSiegeClient gameModeSiegeClient = this._gameModeSiegeClient;
				if (gameModeSiegeClient != null)
				{
					gameModeSiegeClient.OnMoraleChanged(num, num2, this._capturePointRemainingMoraleGains);
				}
				this._morales[1] = num;
				this._morales[0] = num2;
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0008B649 File Offset: 0x00089849
		public override bool CheckForMatchEnd()
		{
			return this._morales.Any((int morale) => morale == 0);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0008B678 File Offset: 0x00089878
		public override Team GetWinnerTeam()
		{
			Team team = null;
			if (this._morales[1] <= 0 && this._morales[0] > 0)
			{
				team = base.Mission.Teams.Defender;
			}
			if (this._morales[0] <= 0 && this._morales[1] > 0)
			{
				team = base.Mission.Teams.Attacker;
			}
			team = (team ?? base.Mission.Teams.Defender);
			base.Mission.GetMissionBehavior<MissionScoreboardComponent>().ChangeTeamScore(team, 1);
			return team;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0008B700 File Offset: 0x00089900
		private int GetMoraleGain(BattleSideEnum side)
		{
			int num = 0;
			bool flag2 = this._masterFlagBestAgent != null && this._masterFlagBestAgent.Team.Side == side;
			if (side == BattleSideEnum.Attacker)
			{
				if (!flag2)
				{
					num += -1;
				}
				using (IEnumerator<FlagCapturePoint> enumerator = (from flag in this.AllCapturePoints
				where flag != this._masterFlag && !flag.IsDeactivated && flag.IsFullyRaised && this.GetFlagOwnerTeam(flag).Side == BattleSideEnum.Attacker
				select flag).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FlagCapturePoint flagCapturePoint = enumerator.Current;
						this._capturePointRemainingMoraleGains[flagCapturePoint.FlagIndex]--;
						num++;
						if (this._capturePointRemainingMoraleGains[flagCapturePoint.FlagIndex] == 0)
						{
							num += 90;
							foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
							{
								MissionPeer component = networkPeer.GetComponent<MissionPeer>();
								if (component != null)
								{
									Team team = component.Team;
									BattleSideEnum? battleSideEnum = (team != null) ? new BattleSideEnum?(team.Side) : null;
									if (battleSideEnum.GetValueOrDefault() == side & battleSideEnum != null)
									{
										base.ChangeCurrentGoldForPeer(component, base.GetCurrentGoldForPeer(component) + 35);
									}
								}
							}
							flagCapturePoint.RemovePointAsServer();
							(base.SpawnComponent.SpawnFrameBehavior as SiegeSpawnFrameBehavior).OnFlagDeactivated(flagCapturePoint);
							this._gameModeSiegeClient.OnNumberOfFlagsChanged();
							GameNetwork.BeginBroadcastModuleEvent();
							GameNetwork.WriteMessage(new FlagDominationFlagsRemovedMessage());
							GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
							this.NotificationsComponent.FlagsXRemoved(flagCapturePoint);
						}
					}
					return num;
				}
			}
			if (this._masterFlag.IsFullyRaised)
			{
				if (this.GetFlagOwnerTeam(this._masterFlag).Side == BattleSideEnum.Attacker)
				{
					if (!flag2)
					{
						int num2 = 0;
						for (int i = 0; i < this.AllCapturePoints.Count; i++)
						{
							if (this.AllCapturePoints[i] != this._masterFlag && !this.AllCapturePoints[i].IsDeactivated)
							{
								num2++;
							}
						}
						num += -6 + num2;
					}
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0008B934 File Offset: 0x00089B34
		public Team GetFlagOwnerTeam(FlagCapturePoint flag)
		{
			return this._capturePointOwners[flag.FlagIndex];
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0008B943 File Offset: 0x00089B43
		private bool CheckObjectives(float dt)
		{
			this._dtSumObjectiveCheck += dt;
			if (this._dtSumObjectiveCheck >= 0.25f)
			{
				this._dtSumObjectiveCheck -= 0.25f;
				return true;
			}
			return false;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0008B978 File Offset: 0x00089B78
		private void TickFlags(float dt)
		{
			foreach (FlagCapturePoint flagCapturePoint in this.AllCapturePoints)
			{
				if (!flagCapturePoint.IsDeactivated)
				{
					Team flagOwnerTeam = this.GetFlagOwnerTeam(flagCapturePoint);
					Agent agent = null;
					float num = float.MaxValue;
					AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, flagCapturePoint.Position.AsVec2, 4f, false);
					while (proximityMapSearchStruct.LastFoundAgent != null)
					{
						Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
						if (!lastFoundAgent.IsMount && lastFoundAgent.IsActive())
						{
							float num2 = lastFoundAgent.Position.DistanceSquared(flagCapturePoint.Position);
							if (num2 <= 16f && num2 < num)
							{
								agent = lastFoundAgent;
								num = num2;
							}
						}
						AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
					}
					if (flagCapturePoint == this._masterFlag)
					{
						this._masterFlagBestAgent = agent;
					}
					CaptureTheFlagFlagDirection captureTheFlagFlagDirection = CaptureTheFlagFlagDirection.None;
					bool isContested = flagCapturePoint.IsContested;
					if (flagOwnerTeam == null)
					{
						if (!isContested && agent != null)
						{
							captureTheFlagFlagDirection = CaptureTheFlagFlagDirection.Down;
						}
						else if (agent == null && isContested)
						{
							captureTheFlagFlagDirection = CaptureTheFlagFlagDirection.Up;
						}
					}
					else if (agent != null)
					{
						if (agent.Team != flagOwnerTeam && !isContested)
						{
							captureTheFlagFlagDirection = CaptureTheFlagFlagDirection.Down;
						}
						else if (agent.Team == flagOwnerTeam && isContested)
						{
							captureTheFlagFlagDirection = CaptureTheFlagFlagDirection.Up;
						}
					}
					else if (isContested)
					{
						captureTheFlagFlagDirection = CaptureTheFlagFlagDirection.Up;
					}
					if (captureTheFlagFlagDirection != CaptureTheFlagFlagDirection.None)
					{
						flagCapturePoint.SetMoveFlag(captureTheFlagFlagDirection, 1f);
					}
					bool flag;
					flagCapturePoint.OnAfterTick(agent != null, out flag);
					if (flag)
					{
						Team team = agent.Team;
						uint color = (team != null) ? team.Color : 4284111450U;
						uint color2 = (team != null) ? team.Color2 : uint.MaxValue;
						flagCapturePoint.SetTeamColorsSynched(color, color2);
						this._capturePointOwners[flagCapturePoint.FlagIndex] = team;
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new FlagDominationCapturePointMessage(flagCapturePoint.FlagIndex, (team != null) ? team.TeamIndex : -1));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
						MissionMultiplayerSiegeClient gameModeSiegeClient = this._gameModeSiegeClient;
						if (gameModeSiegeClient != null)
						{
							gameModeSiegeClient.OnCapturePointOwnerChanged(flagCapturePoint, team);
						}
						this.NotificationsComponent.FlagXCapturedByTeamX(flagCapturePoint, agent.Team);
					}
				}
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0008BB94 File Offset: 0x00089D94
		private void TickObjectives(float dt)
		{
			for (int i = this._movingObjectives.Length - 1; i >= 0; i--)
			{
				IMoveableSiegeWeapon item = this._movingObjectives[i].Item1;
				if (item != null)
				{
					SiegeWeapon siegeWeapon = item as SiegeWeapon;
					if (siegeWeapon.IsDeactivated || siegeWeapon.IsDestroyed || siegeWeapon.IsDisabled)
					{
						this._movingObjectives[i].Item1 = null;
					}
					else
					{
						if (item.MovementComponent.HasArrivedAtTarget)
						{
							this._movingObjectives[i].Item1 = null;
							GameEntity root = siegeWeapon.GameEntity.Root;
							List<KeyValuePair<MissionPeer, float>> allContributorsForSideAndClear = this._objectiveSystem.GetAllContributorsForSideAndClear(root, BattleSideEnum.Attacker);
							float num = allContributorsForSideAndClear.Sum((KeyValuePair<MissionPeer, float> ac) => ac.Value);
							using (List<KeyValuePair<MissionPeer, float>>.Enumerator enumerator = allContributorsForSideAndClear.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									KeyValuePair<MissionPeer, float> keyValuePair = enumerator.Current;
									int goldGainsFromObjectiveAssist = (keyValuePair.Key.Representative as SiegeMissionRepresentative).GetGoldGainsFromObjectiveAssist(root, keyValuePair.Value / num, true);
									if (goldGainsFromObjectiveAssist > 0)
									{
										base.ChangeCurrentGoldForPeer(keyValuePair.Key, keyValuePair.Key.Representative.Gold + goldGainsFromObjectiveAssist);
										MissionMultiplayerSiege.OnObjectiveGoldGainedDelegate onObjectiveGoldGained = this.OnObjectiveGoldGained;
										if (onObjectiveGoldGained != null)
										{
											onObjectiveGoldGained(keyValuePair.Key, goldGainsFromObjectiveAssist);
										}
									}
								}
								goto IL_223;
							}
						}
						GameEntity gameEntity = siegeWeapon.GameEntity;
						Vec3 item2 = this._movingObjectives[i].Item2;
						Vec3 globalPosition = gameEntity.GlobalPosition;
						float lengthSquared = (globalPosition - item2).LengthSquared;
						if (lengthSquared > 1f)
						{
							this._movingObjectives[i].Item2 = globalPosition;
							foreach (StandingPoint standingPoint in siegeWeapon.StandingPoints)
							{
								Agent userAgent = standingPoint.UserAgent;
								if (((userAgent != null) ? userAgent.MissionPeer : null) != null && userAgent.MissionPeer.Team.Side == siegeWeapon.Side)
								{
									this._objectiveSystem.AddContributionForObjective(gameEntity.Root, userAgent.MissionPeer, lengthSquared);
								}
							}
						}
					}
				}
				IL_223:;
			}
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0008BDEC File Offset: 0x00089FEC
		private void OnWarmupEnding()
		{
			this.NotificationsComponent.WarmupEnding();
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0008BDFC File Offset: 0x00089FFC
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

		// Token: 0x060024F6 RID: 9462 RVA: 0x0008BEA0 File Offset: 0x0008A0A0
		protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			networkPeer.AddComponent<SiegeMissionRepresentative>();
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0008BEAC File Offset: 0x0008A0AC
		protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			int num = 120;
			if (this._warmupComponent != null && this._warmupComponent.IsInWarmup)
			{
				num = 160;
			}
			base.ChangeCurrentGoldForPeer(networkPeer.GetComponent<MissionPeer>(), num);
			MissionMultiplayerSiegeClient gameModeSiegeClient = this._gameModeSiegeClient;
			if (gameModeSiegeClient != null)
			{
				gameModeSiegeClient.OnGoldAmountChangedForRepresentative(networkPeer.GetComponent<SiegeMissionRepresentative>(), num);
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

		// Token: 0x060024F8 RID: 9464 RVA: 0x0008BFA4 File Offset: 0x0008A1A4
		public override void OnPeerChangedTeam(NetworkCommunicator peer, Team oldTeam, Team newTeam)
		{
			if (this.MissionLobbyComponent.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing && oldTeam != null && oldTeam != newTeam)
			{
				base.ChangeCurrentGoldForPeer(peer.GetComponent<MissionPeer>(), 100);
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0008BFCC File Offset: 0x0008A1CC
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (this.MissionLobbyComponent.CurrentMultiplayerState == MissionLobbyComponent.MultiplayerGameState.Playing && blow.DamageType != DamageTypes.Invalid && (agentState == AgentState.Unconscious || agentState == AgentState.Killed) && affectedAgent.IsHuman)
			{
				MissionPeer missionPeer = affectedAgent.MissionPeer;
				if (missionPeer != null)
				{
					int num = 100;
					if (affectorAgent != affectedAgent)
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
						int num2 = array[1].Count - array[0].Count;
						BattleSideEnum battleSideEnum = (num2 == 0) ? BattleSideEnum.None : ((num2 < 0) ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
						if (battleSideEnum != BattleSideEnum.None && battleSideEnum == missionPeer.Team.Side)
						{
							num2 = MathF.Abs(num2);
							int count = array[(int)battleSideEnum].Count;
							if (count > 0)
							{
								int num3 = num * num2 / 10 / count * 10;
								num += num3;
							}
						}
					}
					base.ChangeCurrentGoldForPeer(missionPeer, missionPeer.Representative.Gold + num);
				}
				bool isFriendly = ((affectorAgent != null) ? affectorAgent.Team : null) != null && affectedAgent.Team != null && affectorAgent.Team.Side == affectedAgent.Team.Side;
				MultiplayerClassDivisions.MPHeroClass mpheroClassForCharacter = MultiplayerClassDivisions.GetMPHeroClassForCharacter(affectedAgent.Character);
				Agent.Hitter assistingHitter = affectedAgent.GetAssistingHitter((affectorAgent != null) ? affectorAgent.MissionPeer : null);
				if (((affectorAgent != null) ? affectorAgent.MissionPeer : null) != null && affectorAgent != affectedAgent && affectedAgent.Team != affectorAgent.Team)
				{
					SiegeMissionRepresentative siegeMissionRepresentative = affectorAgent.MissionPeer.Representative as SiegeMissionRepresentative;
					int goldGainsFromKillDataAndUpdateFlags = siegeMissionRepresentative.GetGoldGainsFromKillDataAndUpdateFlags(MPPerkObject.GetPerkHandler(affectorAgent.MissionPeer), MPPerkObject.GetPerkHandler((assistingHitter != null) ? assistingHitter.HitterPeer : null), mpheroClassForCharacter, false, blow.IsMissile, isFriendly);
					base.ChangeCurrentGoldForPeer(affectorAgent.MissionPeer, siegeMissionRepresentative.Gold + goldGainsFromKillDataAndUpdateFlags);
				}
				if (((assistingHitter != null) ? assistingHitter.HitterPeer : null) != null && !assistingHitter.IsFriendlyHit)
				{
					SiegeMissionRepresentative siegeMissionRepresentative2 = assistingHitter.HitterPeer.Representative as SiegeMissionRepresentative;
					int goldGainsFromKillDataAndUpdateFlags2 = siegeMissionRepresentative2.GetGoldGainsFromKillDataAndUpdateFlags(MPPerkObject.GetPerkHandler((affectorAgent != null) ? affectorAgent.MissionPeer : null), MPPerkObject.GetPerkHandler(assistingHitter.HitterPeer), mpheroClassForCharacter, true, blow.IsMissile, isFriendly);
					base.ChangeCurrentGoldForPeer(assistingHitter.HitterPeer, siegeMissionRepresentative2.Gold + goldGainsFromKillDataAndUpdateFlags2);
				}
				if (((missionPeer != null) ? missionPeer.Team : null) != null)
				{
					MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(missionPeer);
					IEnumerable<ValueTuple<MissionPeer, int>> enumerable = (perkHandler != null) ? perkHandler.GetTeamGoldRewardsOnDeath() : null;
					if (enumerable != null)
					{
						foreach (ValueTuple<MissionPeer, int> valueTuple in enumerable)
						{
							MissionPeer item = valueTuple.Item1;
							int item2 = valueTuple.Item2;
							SiegeMissionRepresentative siegeMissionRepresentative3;
							if (item2 > 0 && (siegeMissionRepresentative3 = (((item != null) ? item.Representative : null) as SiegeMissionRepresentative)) != null)
							{
								int goldGainsFromAllyDeathReward = siegeMissionRepresentative3.GetGoldGainsFromAllyDeathReward(item2);
								if (goldGainsFromAllyDeathReward > 0)
								{
									base.ChangeCurrentGoldForPeer(item, siegeMissionRepresentative3.Gold + goldGainsFromAllyDeathReward);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0008C324 File Offset: 0x0008A524
		protected override void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new SiegeMoraleChangeMessage(this._morales[1], this._morales[0], this._capturePointRemainingMoraleGains));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0008C352 File Offset: 0x0008A552
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			if (this._warmupComponent != null)
			{
				this._warmupComponent.OnWarmupEnding -= this.OnWarmupEnding;
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0008C37C File Offset: 0x0008A57C
		public override void OnClearScene()
		{
			base.OnClearScene();
			foreach (CastleGate castleGate in Mission.Current.MissionObjects.FindAllWithType<CastleGate>())
			{
				foreach (StandingPoint standingPoint in castleGate.StandingPoints)
				{
					standingPoint.SetIsDeactivatedSynched(false);
				}
			}
		}

		// Token: 0x04000D94 RID: 3476
		public const int NumberOfFlagsInGame = 7;

		// Token: 0x04000D95 RID: 3477
		public const int NumberOfFlagsAffectingMoraleInGame = 6;

		// Token: 0x04000D96 RID: 3478
		public const int MaxMorale = 1440;

		// Token: 0x04000D97 RID: 3479
		public const int StartingMorale = 360;

		// Token: 0x04000D98 RID: 3480
		private const int FirstSpawnGold = 120;

		// Token: 0x04000D99 RID: 3481
		private const int FirstSpawnGoldForEarlyJoin = 160;

		// Token: 0x04000D9A RID: 3482
		private const int RespawnGold = 100;

		// Token: 0x04000D9B RID: 3483
		private const float ObjectiveCheckPeriod = 0.25f;

		// Token: 0x04000D9C RID: 3484
		private const float MoraleTickTimeInSeconds = 1f;

		// Token: 0x04000D9D RID: 3485
		public const int MaxMoraleGainPerFlag = 90;

		// Token: 0x04000D9E RID: 3486
		private const int MoraleBoostOnFlagRemoval = 90;

		// Token: 0x04000D9F RID: 3487
		private const int MoraleDecayInTick = -1;

		// Token: 0x04000DA0 RID: 3488
		private const int MoraleDecayOnDefenderInTick = -6;

		// Token: 0x04000DA1 RID: 3489
		public const int MoraleGainPerFlag = 1;

		// Token: 0x04000DA2 RID: 3490
		public const int GoldBonusOnFlagRemoval = 35;

		// Token: 0x04000DA3 RID: 3491
		public const string MasterFlagTag = "keep_capture_point";

		// Token: 0x04000DA6 RID: 3494
		private int[] _morales;

		// Token: 0x04000DA7 RID: 3495
		private Agent _masterFlagBestAgent;

		// Token: 0x04000DA8 RID: 3496
		private FlagCapturePoint _masterFlag;

		// Token: 0x04000DA9 RID: 3497
		private Team[] _capturePointOwners;

		// Token: 0x04000DAA RID: 3498
		private int[] _capturePointRemainingMoraleGains;

		// Token: 0x04000DAC RID: 3500
		private float _dtSumCheckMorales;

		// Token: 0x04000DAD RID: 3501
		private float _dtSumObjectiveCheck;

		// Token: 0x04000DAE RID: 3502
		private MissionMultiplayerSiege.ObjectiveSystem _objectiveSystem;

		// Token: 0x04000DAF RID: 3503
		private ValueTuple<IMoveableSiegeWeapon, Vec3>[] _movingObjectives;

		// Token: 0x04000DB0 RID: 3504
		private ValueTuple<RangedSiegeWeapon, Agent>[] _lastReloadingAgentPerRangedSiegeMachine;

		// Token: 0x04000DB1 RID: 3505
		private MissionMultiplayerSiegeClient _gameModeSiegeClient;

		// Token: 0x04000DB2 RID: 3506
		private MultiplayerWarmupComponent _warmupComponent;

		// Token: 0x04000DB3 RID: 3507
		private Dictionary<GameEntity, List<DestructableComponent>> _childDestructableComponents;

		// Token: 0x04000DB4 RID: 3508
		private bool _firstTickDone;

		// Token: 0x02000568 RID: 1384
		private class ObjectiveSystem
		{
			// Token: 0x060039A5 RID: 14757 RVA: 0x000E5548 File Offset: 0x000E3748
			public ObjectiveSystem()
			{
				this._objectiveContributorMap = new Dictionary<GameEntity, List<MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor>[]>();
			}

			// Token: 0x060039A6 RID: 14758 RVA: 0x000E555C File Offset: 0x000E375C
			public bool RegisterObjective(GameEntity entity)
			{
				if (!this._objectiveContributorMap.ContainsKey(entity))
				{
					this._objectiveContributorMap.Add(entity, new List<MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor>[2]);
					for (int i = 0; i < 2; i++)
					{
						this._objectiveContributorMap[entity][i] = new List<MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor>();
					}
					return true;
				}
				return false;
			}

			// Token: 0x060039A7 RID: 14759 RVA: 0x000E55AC File Offset: 0x000E37AC
			public void AddContributionForObjective(GameEntity objectiveEntity, MissionPeer contributorPeer, float contribution)
			{
				string text = objectiveEntity.Tags.FirstOrDefault((string x) => x.StartsWith("mp_siege_objective_")) ?? "";
				bool flag = false;
				for (int i = 0; i < 2; i++)
				{
					foreach (MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor objectiveContributor in this._objectiveContributorMap[objectiveEntity][i])
					{
						if (objectiveContributor.Peer == contributorPeer)
						{
							Debug.Print(string.Format("[CONT > {0}] Increased contribution for {1}({2}) by {3}.", new object[]
							{
								text,
								contributorPeer.Name,
								contributorPeer.Team.Side.ToString(),
								contribution
							}), 0, Debug.DebugColor.White, 17179869184UL);
							objectiveContributor.IncreaseAmount(contribution);
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					Debug.Print(string.Format("[CONT > {0}] Adding {1} contribution for {2}({3}).", new object[]
					{
						text,
						contribution,
						contributorPeer.Name,
						contributorPeer.Team.Side.ToString()
					}), 0, Debug.DebugColor.White, 17179869184UL);
					this._objectiveContributorMap[objectiveEntity][(int)contributorPeer.Team.Side].Add(new MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor(contributorPeer, contribution));
				}
			}

			// Token: 0x060039A8 RID: 14760 RVA: 0x000E5734 File Offset: 0x000E3934
			public List<KeyValuePair<MissionPeer, float>> GetAllContributorsForSideAndClear(GameEntity objectiveEntity, BattleSideEnum side)
			{
				List<KeyValuePair<MissionPeer, float>> list = new List<KeyValuePair<MissionPeer, float>>();
				string text = objectiveEntity.Tags.FirstOrDefault((string x) => x.StartsWith("mp_siege_objective_")) ?? "";
				foreach (MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor objectiveContributor in this._objectiveContributorMap[objectiveEntity][(int)side])
				{
					Debug.Print(string.Format("[CONT > {0}] Rewarding {1} contribution for {2}({3}).", new object[]
					{
						text,
						objectiveContributor.Contribution,
						objectiveContributor.Peer.Name,
						side.ToString()
					}), 0, Debug.DebugColor.White, 17179869184UL);
					list.Add(new KeyValuePair<MissionPeer, float>(objectiveContributor.Peer, objectiveContributor.Contribution));
				}
				this._objectiveContributorMap[objectiveEntity][(int)side].Clear();
				return list;
			}

			// Token: 0x04001D09 RID: 7433
			private readonly Dictionary<GameEntity, List<MissionMultiplayerSiege.ObjectiveSystem.ObjectiveContributor>[]> _objectiveContributorMap;

			// Token: 0x02000685 RID: 1669
			private class ObjectiveContributor
			{
				// Token: 0x17000A3C RID: 2620
				// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000EE0A3 File Offset: 0x000EC2A3
				// (set) Token: 0x06003DF1 RID: 15857 RVA: 0x000EE0AB File Offset: 0x000EC2AB
				public float Contribution { get; private set; }

				// Token: 0x06003DF2 RID: 15858 RVA: 0x000EE0B4 File Offset: 0x000EC2B4
				public ObjectiveContributor(MissionPeer peer, float initialContribution)
				{
					this.Peer = peer;
					this.Contribution = initialContribution;
				}

				// Token: 0x06003DF3 RID: 15859 RVA: 0x000EE0CA File Offset: 0x000EC2CA
				public void IncreaseAmount(float deltaContribution)
				{
					this.Contribution += deltaContribution;
				}

				// Token: 0x04002189 RID: 8585
				public readonly MissionPeer Peer;
			}
		}

		// Token: 0x02000569 RID: 1385
		// (Invoke) Token: 0x060039AA RID: 14762
		public delegate void OnDestructableComponentDestroyedDelegate(DestructableComponent destructableComponent, ScriptComponentBehavior attackerScriptComponentBehaviour, MissionPeer[] contributors);

		// Token: 0x0200056A RID: 1386
		// (Invoke) Token: 0x060039AE RID: 14766
		public delegate void OnObjectiveGoldGainedDelegate(MissionPeer peer, int goldGain);
	}
}
