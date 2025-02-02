using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000268 RID: 616
	public class BattleEndLogic : MissionLogic, IBattleEndLogic
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000752FA File Offset: 0x000734FA
		public bool PlayerVictory
		{
			get
			{
				return (this._isEnemySideRetreating || this._isEnemySideDepleted) && !this._isEnemyDefenderPulledBack;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x00075317 File Offset: 0x00073517
		public bool EnemyVictory
		{
			get
			{
				return this._isPlayerSideRetreating || this._isPlayerSideDepleted;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x00075329 File Offset: 0x00073529
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x00075331 File Offset: 0x00073531
		private bool _notificationsDisabled { get; set; }

		// Token: 0x060020C3 RID: 8387 RVA: 0x0007533C File Offset: 0x0007353C
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			bool flag = false;
			if (this._isEnemySideDepleted && this._isEnemyDefenderPulledBack)
			{
				missionResult = MissionResult.CreateDefenderPushedBack();
				flag = true;
			}
			else if (this._isEnemySideRetreating || this._isEnemySideDepleted)
			{
				missionResult = MissionResult.CreateSuccessful(base.Mission, this._isEnemySideRetreating);
				flag = true;
			}
			else if (this._isPlayerSideRetreating || this._isPlayerSideDepleted)
			{
				missionResult = MissionResult.CreateDefeated(base.Mission);
				flag = true;
			}
			if (flag)
			{
				this._missionAgentSpawnLogic.StopSpawner(BattleSideEnum.Attacker);
				this._missionAgentSpawnLogic.StopSpawner(BattleSideEnum.Defender);
			}
			return flag;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000753C8 File Offset: 0x000735C8
		public override void OnMissionTick(float dt)
		{
			if (base.Mission.IsMissionEnding)
			{
				if (this._notificationsDisabled)
				{
					this._scoreBoardOpenedOnceOnMissionEnd = true;
				}
				if (this._missionEndedMessageShown && !this._scoreBoardOpenedOnceOnMissionEnd)
				{
					if (this._checkRetreatingTimer.ElapsedTime > 7f)
					{
						this.CheckIsEnemySideRetreatingOrOneSideDepleted();
						this._checkRetreatingTimer.Reset();
						if (base.Mission.MissionResult != null && base.Mission.MissionResult.PlayerDefeated)
						{
							GameTexts.SetVariable("leave_key", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 4)));
							MBInformationManager.AddQuickInformation(GameTexts.FindText("str_battle_lost_press_tab_to_view_results", null), 0, null, "");
						}
						else if (base.Mission.MissionResult != null && base.Mission.MissionResult.PlayerVictory)
						{
							if (this._isEnemySideDepleted)
							{
								GameTexts.SetVariable("leave_key", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 4)));
								MBInformationManager.AddQuickInformation(GameTexts.FindText("str_battle_won_press_tab_to_view_results", null), 0, null, "");
							}
						}
						else
						{
							GameTexts.SetVariable("leave_key", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 4)));
							MBInformationManager.AddQuickInformation(GameTexts.FindText("str_battle_finished_press_tab_to_view_results", null), 0, null, "");
						}
					}
				}
				else if (this._checkRetreatingTimer.ElapsedTime > 3f && !this._scoreBoardOpenedOnceOnMissionEnd)
				{
					if (base.Mission.MissionResult != null && base.Mission.MissionResult.PlayerDefeated)
					{
						if (this._isPlayerSideDepleted)
						{
							MBInformationManager.AddQuickInformation(GameTexts.FindText("str_battle_lost", null), 0, null, "");
						}
						else if (this._isPlayerSideRetreating)
						{
							MBInformationManager.AddQuickInformation(GameTexts.FindText("str_friendlies_are_fleeing_you_lost", null), 0, null, "");
						}
					}
					else if (base.Mission.MissionResult != null && base.Mission.MissionResult.PlayerVictory)
					{
						if (this._isEnemySideDepleted)
						{
							MBInformationManager.AddQuickInformation(GameTexts.FindText("str_battle_won", null), 0, null, "");
						}
						else if (this._isEnemySideRetreating)
						{
							MBInformationManager.AddQuickInformation(GameTexts.FindText("str_enemies_are_fleeing_you_won", null), 0, null, "");
						}
					}
					else
					{
						MBInformationManager.AddQuickInformation(GameTexts.FindText("str_battle_finished", null), 0, null, "");
					}
					this._missionEndedMessageShown = true;
					this._checkRetreatingTimer.Reset();
				}
				if (!this._victoryReactionsActivated)
				{
					AgentVictoryLogic missionBehavior = base.Mission.GetMissionBehavior<AgentVictoryLogic>();
					if (missionBehavior != null)
					{
						this.CheckIsEnemySideRetreatingOrOneSideDepleted();
						if (this._isEnemySideDepleted)
						{
							missionBehavior.SetTimersOfVictoryReactionsOnBattleEnd(base.Mission.PlayerTeam.Side);
							this._victoryReactionsActivated = true;
							return;
						}
						if (this._isPlayerSideDepleted)
						{
							missionBehavior.SetTimersOfVictoryReactionsOnBattleEnd(base.Mission.PlayerEnemyTeam.Side);
							this._victoryReactionsActivated = true;
							return;
						}
						if (this._isEnemySideRetreating && !this._victoryReactionsActivatedForRetreating)
						{
							missionBehavior.SetTimersOfVictoryReactionsOnRetreat(base.Mission.PlayerTeam.Side);
							this._victoryReactionsActivatedForRetreating = true;
							return;
						}
						if (this._isPlayerSideRetreating && !this._victoryReactionsActivatedForRetreating)
						{
							missionBehavior.SetTimersOfVictoryReactionsOnRetreat(base.Mission.PlayerEnemyTeam.Side);
							this._victoryReactionsActivatedForRetreating = true;
							return;
						}
					}
				}
			}
			else if (this._checkRetreatingTimer.ElapsedTime > 1f)
			{
				this.CheckIsEnemySideRetreatingOrOneSideDepleted();
				this._checkRetreatingTimer.Reset();
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0007571A File Offset: 0x0007391A
		public void ChangeCanCheckForEndCondition(bool canCheckForEndCondition)
		{
			this._canCheckForEndCondition = canCheckForEndCondition;
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00075724 File Offset: 0x00073924
		private void CheckIsEnemySideRetreatingOrOneSideDepleted()
		{
			if (!this._canCheckForEndConditionSiege)
			{
				this._canCheckForEndConditionSiege = (base.Mission.GetMissionBehavior<BattleDeploymentHandler>() == null);
				return;
			}
			if (this._canCheckForEndCondition)
			{
				BattleSideEnum side = base.Mission.PlayerTeam.Side;
				BattleSideEnum oppositeSide = side.GetOppositeSide();
				this._isPlayerSideDepleted = this._missionAgentSpawnLogic.IsSideDepleted(side);
				this._isEnemySideDepleted = this._missionAgentSpawnLogic.IsSideDepleted(oppositeSide);
				if (this._isEnemySideDepleted || this._isPlayerSideDepleted)
				{
					return;
				}
				if (base.Mission.GetMissionBehavior<HideoutPhasedMissionController>() != null)
				{
					return;
				}
				float num = this._missionAgentSpawnLogic.GetReinforcementInterval() + 3f;
				if (base.Mission.MainAgent != null && base.Mission.MainAgent.IsPlayerControlled && base.Mission.MainAgent.IsActive())
				{
					this._playerSideNotYetRetreatingTime = MissionTime.Now;
				}
				else
				{
					bool flag = true;
					foreach (Team team in base.Mission.Teams)
					{
						if (team.IsFriendOf(base.Mission.PlayerTeam))
						{
							using (List<Agent>.Enumerator enumerator2 = team.ActiveAgents.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (!enumerator2.Current.IsRunningAway)
									{
										flag = false;
										break;
									}
								}
							}
						}
					}
					if (!flag)
					{
						this._playerSideNotYetRetreatingTime = MissionTime.Now;
					}
				}
				if (this._playerSideNotYetRetreatingTime.ElapsedSeconds > num)
				{
					this._isPlayerSideRetreating = true;
				}
				if (oppositeSide != BattleSideEnum.Defender || !this._enemyDefenderPullbackEnabled)
				{
					float num2 = this._missionAgentSpawnLogic.GetReinforcementInterval() + 3f;
					bool flag2 = true;
					foreach (Team team2 in base.Mission.Teams)
					{
						if (team2.IsEnemyOf(base.Mission.PlayerTeam))
						{
							using (List<Agent>.Enumerator enumerator2 = team2.ActiveAgents.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (!enumerator2.Current.IsRunningAway)
									{
										flag2 = false;
										break;
									}
								}
							}
						}
					}
					if (!flag2)
					{
						this._enemySideNotYetRetreatingTime = MissionTime.Now;
					}
					if (this._enemySideNotYetRetreatingTime.ElapsedSeconds > num2)
					{
						this._isEnemySideRetreating = true;
					}
				}
			}
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000759B4 File Offset: 0x00073BB4
		public BattleEndLogic.ExitResult TryExit()
		{
			if (GameNetwork.IsClientOrReplay)
			{
				return BattleEndLogic.ExitResult.False;
			}
			Agent mainAgent = base.Mission.MainAgent;
			if ((mainAgent != null && mainAgent.IsActive() && base.Mission.IsPlayerCloseToAnEnemy(5f)) || (!base.Mission.MissionEnded && (this.PlayerVictory || this.EnemyVictory)))
			{
				return BattleEndLogic.ExitResult.False;
			}
			if (base.Mission.MissionEnded || this._isEnemySideRetreating)
			{
				base.Mission.EndMission();
				return BattleEndLogic.ExitResult.True;
			}
			if (Mission.Current.IsSiegeBattle && base.Mission.PlayerTeam.IsDefender)
			{
				return BattleEndLogic.ExitResult.SurrenderSiege;
			}
			return BattleEndLogic.ExitResult.NeedsPlayerConfirmation;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00075A57 File Offset: 0x00073C57
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._checkRetreatingTimer = new BasicMissionTimer();
			this._missionAgentSpawnLogic = base.Mission.GetMissionBehavior<IMissionAgentSpawnLogic>();
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00075A7C File Offset: 0x00073C7C
		protected override void OnEndMission()
		{
			if (this._isEnemySideRetreating)
			{
				foreach (Agent agent in base.Mission.PlayerEnemyTeam.ActiveAgents)
				{
					IAgentOriginBase origin = agent.Origin;
					if (origin != null)
					{
						origin.SetRouted();
					}
				}
			}
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00075AEC File Offset: 0x00073CEC
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (this._enemyDefenderPullbackEnabled && this._troopNumberNeededForEnemyDefenderPullBack > 0 && affectedAgent.IsHuman && agentState == AgentState.Routed && affectedAgent.Team != null && affectedAgent.Team.Side == BattleSideEnum.Defender && affectedAgent.Team.Side != base.Mission.PlayerTeam.Side)
			{
				this._troopNumberNeededForEnemyDefenderPullBack--;
				this._isEnemyDefenderPulledBack = (this._troopNumberNeededForEnemyDefenderPullBack <= 0);
			}
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00075B68 File Offset: 0x00073D68
		public void EnableEnemyDefenderPullBack(int neededTroopNumber)
		{
			this._enemyDefenderPullbackEnabled = true;
			this._troopNumberNeededForEnemyDefenderPullBack = neededTroopNumber;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00075B78 File Offset: 0x00073D78
		public bool IsEnemySideRetreating
		{
			get
			{
				return this._isEnemySideRetreating;
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00075B80 File Offset: 0x00073D80
		public void SetNotificationDisabled(bool value)
		{
			this._notificationsDisabled = value;
		}

		// Token: 0x04000C1B RID: 3099
		private IMissionAgentSpawnLogic _missionAgentSpawnLogic;

		// Token: 0x04000C1C RID: 3100
		private MissionTime _enemySideNotYetRetreatingTime;

		// Token: 0x04000C1D RID: 3101
		private MissionTime _playerSideNotYetRetreatingTime;

		// Token: 0x04000C1E RID: 3102
		private BasicMissionTimer _checkRetreatingTimer;

		// Token: 0x04000C1F RID: 3103
		private bool _isEnemySideRetreating;

		// Token: 0x04000C20 RID: 3104
		private bool _isPlayerSideRetreating;

		// Token: 0x04000C21 RID: 3105
		private bool _isEnemySideDepleted;

		// Token: 0x04000C22 RID: 3106
		private bool _isPlayerSideDepleted;

		// Token: 0x04000C23 RID: 3107
		private bool _isEnemyDefenderPulledBack;

		// Token: 0x04000C24 RID: 3108
		private bool _canCheckForEndCondition = true;

		// Token: 0x04000C25 RID: 3109
		private bool _canCheckForEndConditionSiege;

		// Token: 0x04000C26 RID: 3110
		private bool _enemyDefenderPullbackEnabled;

		// Token: 0x04000C27 RID: 3111
		private int _troopNumberNeededForEnemyDefenderPullBack;

		// Token: 0x04000C28 RID: 3112
		private bool _missionEndedMessageShown;

		// Token: 0x04000C29 RID: 3113
		private bool _victoryReactionsActivated;

		// Token: 0x04000C2A RID: 3114
		private bool _victoryReactionsActivatedForRetreating;

		// Token: 0x04000C2B RID: 3115
		private bool _scoreBoardOpenedOnceOnMissionEnd;

		// Token: 0x0200051C RID: 1308
		public enum ExitResult
		{
			// Token: 0x04001C31 RID: 7217
			False,
			// Token: 0x04001C32 RID: 7218
			NeedsPlayerConfirmation,
			// Token: 0x04001C33 RID: 7219
			SurrenderSiege,
			// Token: 0x04001C34 RID: 7220
			True
		}
	}
}
