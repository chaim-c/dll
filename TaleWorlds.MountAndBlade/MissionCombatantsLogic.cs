﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027C RID: 636
	public class MissionCombatantsLogic : MissionLogic
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0007A57E File Offset: 0x0007877E
		public BattleSideEnum PlayerSide
		{
			get
			{
				if (this._playerBattleCombatant == null)
				{
					return BattleSideEnum.None;
				}
				if (this._playerBattleCombatant != this._defenderLeaderBattleCombatant)
				{
					return BattleSideEnum.Attacker;
				}
				return BattleSideEnum.Defender;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0007A59C File Offset: 0x0007879C
		public MissionCombatantsLogic(IEnumerable<IBattleCombatant> battleCombatants, IBattleCombatant playerBattleCombatant, IBattleCombatant defenderLeaderBattleCombatant, IBattleCombatant attackerLeaderBattleCombatant, Mission.MissionTeamAITypeEnum teamAIType, bool isPlayerSergeant)
		{
			if (battleCombatants == null)
			{
				battleCombatants = new IBattleCombatant[]
				{
					defenderLeaderBattleCombatant,
					attackerLeaderBattleCombatant
				};
			}
			this._battleCombatants = battleCombatants;
			this._playerBattleCombatant = playerBattleCombatant;
			this._defenderLeaderBattleCombatant = defenderLeaderBattleCombatant;
			this._attackerLeaderBattleCombatant = attackerLeaderBattleCombatant;
			this._teamAIType = teamAIType;
			this._isPlayerSergeant = isPlayerSergeant;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0007A5F0 File Offset: 0x000787F0
		public Banner GetBannerForSide(BattleSideEnum side)
		{
			if (side != BattleSideEnum.Defender)
			{
				return this._attackerLeaderBattleCombatant.Banner;
			}
			return this._defenderLeaderBattleCombatant.Banner;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0007A60C File Offset: 0x0007880C
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (!base.Mission.Teams.IsEmpty<Team>())
			{
				throw new MBIllegalValueException("Number of teams is not 0.");
			}
			BattleSideEnum side = this._playerBattleCombatant.Side;
			BattleSideEnum oppositeSide = side.GetOppositeSide();
			if (side == BattleSideEnum.Defender)
			{
				this.AddPlayerTeam(side);
			}
			else
			{
				this.AddEnemyTeam(oppositeSide);
			}
			if (side == BattleSideEnum.Attacker)
			{
				this.AddPlayerTeam(side);
			}
			else
			{
				this.AddEnemyTeam(oppositeSide);
			}
			this.AddPlayerAllyTeam(side);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0007A680 File Offset: 0x00078880
		public override void EarlyStart()
		{
			Mission.Current.MissionTeamAIType = this._teamAIType;
			switch (this._teamAIType)
			{
			case Mission.MissionTeamAITypeEnum.FieldBattle:
				using (List<Team>.Enumerator enumerator = Mission.Current.Teams.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Team team8 = enumerator.Current;
						team8.AddTeamAI(new TeamAIGeneral(base.Mission, team8, 10f, 1f), false);
					}
					goto IL_182;
				}
				break;
			case Mission.MissionTeamAITypeEnum.Siege:
				break;
			case Mission.MissionTeamAITypeEnum.SallyOut:
				goto IL_104;
			default:
				goto IL_182;
			}
			using (List<Team>.Enumerator enumerator = Mission.Current.Teams.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Team team2 = enumerator.Current;
					if (team2.Side == BattleSideEnum.Attacker)
					{
						team2.AddTeamAI(new TeamAISiegeAttacker(base.Mission, team2, 5f, 1f), false);
					}
					if (team2.Side == BattleSideEnum.Defender)
					{
						team2.AddTeamAI(new TeamAISiegeDefender(base.Mission, team2, 5f, 1f), false);
					}
				}
				goto IL_182;
			}
			IL_104:
			foreach (Team team3 in Mission.Current.Teams)
			{
				if (team3.Side == BattleSideEnum.Attacker)
				{
					team3.AddTeamAI(new TeamAISallyOutDefender(base.Mission, team3, 5f, 1f), false);
				}
				else
				{
					team3.AddTeamAI(new TeamAISallyOutAttacker(base.Mission, team3, 5f, 1f), false);
				}
			}
			IL_182:
			if (Mission.Current.Teams.Count > 0)
			{
				switch (Mission.Current.MissionTeamAIType)
				{
				case Mission.MissionTeamAITypeEnum.NoTeamAI:
					using (List<Team>.Enumerator enumerator = Mission.Current.Teams.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Team team4 = enumerator.Current;
							if (team4.HasTeamAi)
							{
								team4.AddTacticOption(new TacticCharge(team4));
							}
						}
						goto IL_4AD;
					}
					break;
				case Mission.MissionTeamAITypeEnum.FieldBattle:
					break;
				case Mission.MissionTeamAITypeEnum.Siege:
					goto IL_3C4;
				case Mission.MissionTeamAITypeEnum.SallyOut:
					goto IL_433;
				default:
					goto IL_4AD;
				}
				using (List<Team>.Enumerator enumerator = Mission.Current.Teams.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Team team = enumerator.Current;
						if (team.HasTeamAi)
						{
							int num = (from bc in this._battleCombatants
							where bc.Side == team.Side
							select bc).Max((IBattleCombatant bcs) => bcs.GetTacticsSkillAmount());
							team.AddTacticOption(new TacticCharge(team));
							if ((float)num >= 20f)
							{
								team.AddTacticOption(new TacticFullScaleAttack(team));
								if (team.Side == BattleSideEnum.Defender)
								{
									team.AddTacticOption(new TacticDefensiveEngagement(team));
									team.AddTacticOption(new TacticDefensiveLine(team));
								}
								if (team.Side == BattleSideEnum.Attacker)
								{
									team.AddTacticOption(new TacticRangedHarrassmentOffensive(team));
								}
							}
							if ((float)num >= 50f)
							{
								team.AddTacticOption(new TacticFrontalCavalryCharge(team));
								if (team.Side == BattleSideEnum.Defender)
								{
									team.AddTacticOption(new TacticDefensiveRing(team));
									team.AddTacticOption(new TacticHoldChokePoint(team));
								}
								if (team.Side == BattleSideEnum.Attacker)
								{
									team.AddTacticOption(new TacticCoordinatedRetreat(team));
								}
							}
						}
					}
					goto IL_4AD;
				}
				IL_3C4:
				using (List<Team>.Enumerator enumerator = Mission.Current.Teams.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Team team5 = enumerator.Current;
						if (team5.HasTeamAi)
						{
							if (team5.Side == BattleSideEnum.Attacker)
							{
								team5.AddTacticOption(new TacticBreachWalls(team5));
							}
							if (team5.Side == BattleSideEnum.Defender)
							{
								team5.AddTacticOption(new TacticDefendCastle(team5));
							}
						}
					}
					goto IL_4AD;
				}
				IL_433:
				foreach (Team team6 in Mission.Current.Teams)
				{
					if (team6.HasTeamAi)
					{
						if (team6.Side == BattleSideEnum.Defender)
						{
							team6.AddTacticOption(new TacticSallyOutHitAndRun(team6));
						}
						if (team6.Side == BattleSideEnum.Attacker)
						{
							team6.AddTacticOption(new TacticSallyOutDefense(team6));
						}
						team6.AddTacticOption(new TacticCharge(team6));
					}
				}
				IL_4AD:
				foreach (Team team7 in base.Mission.Teams)
				{
					team7.QuerySystem.Expire();
					team7.ResetTactic();
				}
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0007AC44 File Offset: 0x00078E44
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0007AC53 File Offset: 0x00078E53
		public IEnumerable<IBattleCombatant> GetAllCombatants()
		{
			foreach (IBattleCombatant battleCombatant in this._battleCombatants)
			{
				yield return battleCombatant;
			}
			IEnumerator<IBattleCombatant> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0007AC64 File Offset: 0x00078E64
		private void AddPlayerTeam(BattleSideEnum playerSide)
		{
			base.Mission.Teams.Add(playerSide, this._playerBattleCombatant.PrimaryColorPair.Item1, this._playerBattleCombatant.PrimaryColorPair.Item2, this._playerBattleCombatant.Banner, true, false, true);
			base.Mission.PlayerTeam = ((playerSide == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam : base.Mission.DefenderTeam);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0007ACD8 File Offset: 0x00078ED8
		private void AddEnemyTeam(BattleSideEnum enemySide)
		{
			IBattleCombatant battleCombatant = (enemySide == BattleSideEnum.Attacker) ? this._attackerLeaderBattleCombatant : this._defenderLeaderBattleCombatant;
			base.Mission.Teams.Add(enemySide, battleCombatant.PrimaryColorPair.Item1, battleCombatant.PrimaryColorPair.Item2, battleCombatant.Banner, true, false, true);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0007AD2C File Offset: 0x00078F2C
		private void AddPlayerAllyTeam(BattleSideEnum playerSide)
		{
			if (this._battleCombatants != null)
			{
				foreach (IBattleCombatant battleCombatant in this._battleCombatants)
				{
					if (battleCombatant != this._playerBattleCombatant && battleCombatant.Side == playerSide && !this._isPlayerSergeant)
					{
						base.Mission.Teams.Add(playerSide, battleCombatant.PrimaryColorPair.Item1, battleCombatant.PrimaryColorPair.Item2, battleCombatant.Banner, true, false, true);
						if (playerSide != BattleSideEnum.Attacker)
						{
							Team defenderAllyTeam = base.Mission.DefenderAllyTeam;
							break;
						}
						Team attackerAllyTeam = base.Mission.AttackerAllyTeam;
						break;
					}
				}
			}
		}

		// Token: 0x04000C7A RID: 3194
		private readonly IEnumerable<IBattleCombatant> _battleCombatants;

		// Token: 0x04000C7B RID: 3195
		private readonly IBattleCombatant _playerBattleCombatant;

		// Token: 0x04000C7C RID: 3196
		private readonly IBattleCombatant _defenderLeaderBattleCombatant;

		// Token: 0x04000C7D RID: 3197
		private readonly IBattleCombatant _attackerLeaderBattleCombatant;

		// Token: 0x04000C7E RID: 3198
		private readonly Mission.MissionTeamAITypeEnum _teamAIType;

		// Token: 0x04000C7F RID: 3199
		private readonly bool _isPlayerSergeant;
	}
}
