using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000179 RID: 377
	public class TeamQuerySystem
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000489B9 File Offset: 0x00046BB9
		public int MemberCount
		{
			get
			{
				return this._memberCount.Value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000489C6 File Offset: 0x00046BC6
		public WorldPosition MedianPosition
		{
			get
			{
				return this._medianPosition.Value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x000489D3 File Offset: 0x00046BD3
		public Vec2 AveragePosition
		{
			get
			{
				return this._averagePosition.Value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x000489E0 File Offset: 0x00046BE0
		public Vec2 AverageEnemyPosition
		{
			get
			{
				return this._averageEnemyPosition.Value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x000489ED File Offset: 0x00046BED
		public FormationQuerySystem MedianTargetFormation
		{
			get
			{
				return this._medianTargetFormation.Value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000489FA File Offset: 0x00046BFA
		public WorldPosition MedianTargetFormationPosition
		{
			get
			{
				return this._medianTargetFormationPosition.Value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00048A07 File Offset: 0x00046C07
		public WorldPosition LeftFlankEdgePosition
		{
			get
			{
				return this._leftFlankEdgePosition.Value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00048A14 File Offset: 0x00046C14
		public WorldPosition RightFlankEdgePosition
		{
			get
			{
				return this._rightFlankEdgePosition.Value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00048A21 File Offset: 0x00046C21
		public float InfantryRatio
		{
			get
			{
				return this._infantryRatio.Value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00048A2E File Offset: 0x00046C2E
		public float RangedRatio
		{
			get
			{
				return this._rangedRatio.Value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00048A3B File Offset: 0x00046C3B
		public float CavalryRatio
		{
			get
			{
				return this._cavalryRatio.Value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00048A48 File Offset: 0x00046C48
		public float RangedCavalryRatio
		{
			get
			{
				return this._rangedCavalryRatio.Value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00048A55 File Offset: 0x00046C55
		public int AllyUnitCount
		{
			get
			{
				return this._allyMemberCount.Value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00048A62 File Offset: 0x00046C62
		public int EnemyUnitCount
		{
			get
			{
				return this._enemyMemberCount.Value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00048A6F File Offset: 0x00046C6F
		public float AllyInfantryRatio
		{
			get
			{
				return this._allyInfantryRatio.Value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00048A7C File Offset: 0x00046C7C
		public float AllyRangedRatio
		{
			get
			{
				return this._allyRangedRatio.Value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00048A89 File Offset: 0x00046C89
		public float AllyCavalryRatio
		{
			get
			{
				return this._allyCavalryRatio.Value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00048A96 File Offset: 0x00046C96
		public float AllyRangedCavalryRatio
		{
			get
			{
				return this._allyRangedCavalryRatio.Value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00048AA3 File Offset: 0x00046CA3
		public float EnemyInfantryRatio
		{
			get
			{
				return this._enemyInfantryRatio.Value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00048AB0 File Offset: 0x00046CB0
		public float EnemyRangedRatio
		{
			get
			{
				return this._enemyRangedRatio.Value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00048ABD File Offset: 0x00046CBD
		public float EnemyCavalryRatio
		{
			get
			{
				return this._enemyCavalryRatio.Value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00048ACA File Offset: 0x00046CCA
		public float EnemyRangedCavalryRatio
		{
			get
			{
				return this._enemyRangedCavalryRatio.Value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00048AD7 File Offset: 0x00046CD7
		public float RemainingPowerRatio
		{
			get
			{
				return this._remainingPowerRatio.Value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00048AE4 File Offset: 0x00046CE4
		public float TeamPower
		{
			get
			{
				return this._teamPower.Value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00048AF1 File Offset: 0x00046CF1
		public float TotalPowerRatio
		{
			get
			{
				return this._totalPowerRatio.Value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00048AFE File Offset: 0x00046CFE
		public float InsideWallsRatio
		{
			get
			{
				return this._insideWallsRatio.Value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00048B0B File Offset: 0x00046D0B
		public BattlePowerCalculationLogic BattlePowerLogic
		{
			get
			{
				if (this._battlePowerLogic == null)
				{
					this._battlePowerLogic = this._mission.GetMissionBehavior<BattlePowerCalculationLogic>();
				}
				return this._battlePowerLogic;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00048B2C File Offset: 0x00046D2C
		public CasualtyHandler CasualtyHandler
		{
			get
			{
				if (this._casualtyHandler == null)
				{
					this._casualtyHandler = this._mission.GetMissionBehavior<CasualtyHandler>();
				}
				return this._casualtyHandler;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00048B4D File Offset: 0x00046D4D
		public float MaxUnderRangedAttackRatio
		{
			get
			{
				return this._maxUnderRangedAttackRatio.Value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00048B5A File Offset: 0x00046D5A
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x00048B62 File Offset: 0x00046D62
		public int DeathCount { get; private set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00048B6B File Offset: 0x00046D6B
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x00048B73 File Offset: 0x00046D73
		public int DeathByRangedCount { get; private set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00048B7C File Offset: 0x00046D7C
		public int AllyRangedUnitCount
		{
			get
			{
				return (int)(this.AllyRangedRatio * (float)this.AllyUnitCount);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00048B8D File Offset: 0x00046D8D
		public int AllCavalryUnitCount
		{
			get
			{
				return (int)(this.AllyCavalryRatio * (float)this.AllyUnitCount);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00048B9E File Offset: 0x00046D9E
		public int EnemyRangedUnitCount
		{
			get
			{
				return (int)(this.EnemyRangedRatio * (float)this.EnemyUnitCount);
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00048BB0 File Offset: 0x00046DB0
		public void Expire()
		{
			this._memberCount.Expire();
			this._medianPosition.Expire();
			this._averagePosition.Expire();
			this._averageEnemyPosition.Expire();
			this._medianTargetFormationPosition.Expire();
			this._leftFlankEdgePosition.Expire();
			this._rightFlankEdgePosition.Expire();
			this._infantryRatio.Expire();
			this._rangedRatio.Expire();
			this._cavalryRatio.Expire();
			this._rangedCavalryRatio.Expire();
			this._allyMemberCount.Expire();
			this._enemyMemberCount.Expire();
			this._allyInfantryRatio.Expire();
			this._allyRangedRatio.Expire();
			this._allyCavalryRatio.Expire();
			this._allyRangedCavalryRatio.Expire();
			this._enemyInfantryRatio.Expire();
			this._enemyRangedRatio.Expire();
			this._enemyCavalryRatio.Expire();
			this._enemyRangedCavalryRatio.Expire();
			this._remainingPowerRatio.Expire();
			this._teamPower.Expire();
			this._totalPowerRatio.Expire();
			this._insideWallsRatio.Expire();
			this._maxUnderRangedAttackRatio.Expire();
			foreach (Formation formation in this.Team.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.QuerySystem.Expire();
				}
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00048D34 File Offset: 0x00046F34
		public void ExpireAfterUnitAddRemove()
		{
			this._memberCount.Expire();
			this._medianPosition.Expire();
			this._averagePosition.Expire();
			this._leftFlankEdgePosition.Expire();
			this._rightFlankEdgePosition.Expire();
			this._infantryRatio.Expire();
			this._rangedRatio.Expire();
			this._cavalryRatio.Expire();
			this._rangedCavalryRatio.Expire();
			this._allyMemberCount.Expire();
			this._allyInfantryRatio.Expire();
			this._allyRangedRatio.Expire();
			this._allyCavalryRatio.Expire();
			this._allyRangedCavalryRatio.Expire();
			this._remainingPowerRatio.Expire();
			this._teamPower.Expire();
			this._totalPowerRatio.Expire();
			this._insideWallsRatio.Expire();
			this._maxUnderRangedAttackRatio.Expire();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00048E12 File Offset: 0x00047012
		private void InitializeTelemetryScopeNames()
		{
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00048E14 File Offset: 0x00047014
		public TeamQuerySystem(Team team)
		{
			TeamQuerySystem <>4__this = this;
			this.Team = team;
			this._mission = Mission.Current;
			this._memberCount = new QueryData<int>(delegate()
			{
				int num = 0;
				foreach (Formation formation in <>4__this.Team.FormationsIncludingSpecialAndEmpty)
				{
					num += formation.CountOfUnits;
				}
				return num;
			}, 2f);
			this._allyMemberCount = new QueryData<int>(delegate()
			{
				int num = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsFriendOf(<>4__this.Team))
					{
						num += team2.QuerySystem.MemberCount;
					}
				}
				return num;
			}, 2f);
			this._enemyMemberCount = new QueryData<int>(delegate()
			{
				int num = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						num += team2.QuerySystem.MemberCount;
					}
				}
				return num;
			}, 2f);
			this._averagePosition = new QueryData<Vec2>(new Func<Vec2>(team.GetAveragePosition), 5f);
			this._medianPosition = new QueryData<WorldPosition>(() => team.GetMedianPosition(<>4__this.AveragePosition), 5f);
			this._averageEnemyPosition = new QueryData<Vec2>(delegate()
			{
				Vec2 averagePositionOfEnemies = team.GetAveragePositionOfEnemies();
				if (averagePositionOfEnemies.IsValid)
				{
					return averagePositionOfEnemies;
				}
				if (team.Side == BattleSideEnum.Attacker)
				{
					SiegeDeploymentHandler missionBehavior = <>4__this._mission.GetMissionBehavior<SiegeDeploymentHandler>();
					if (missionBehavior != null)
					{
						return missionBehavior.GetEstimatedAverageDefenderPosition();
					}
				}
				if (!<>4__this.AveragePosition.IsValid)
				{
					return team.GetAveragePosition();
				}
				return <>4__this.AveragePosition;
			}, 5f);
			this._medianTargetFormation = new QueryData<FormationQuerySystem>(delegate()
			{
				float num = float.MaxValue;
				Formation formation = null;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						foreach (Formation formation2 in team2.FormationsIncludingSpecialAndEmpty)
						{
							if (formation2.CountOfUnits > 0)
							{
								float num2 = formation2.QuerySystem.MedianPosition.AsVec2.DistanceSquared(<>4__this.AverageEnemyPosition);
								if (num > num2)
								{
									num = num2;
									formation = formation2;
								}
							}
						}
					}
				}
				if (formation != null)
				{
					return formation.QuerySystem;
				}
				return null;
			}, 1f);
			this._medianTargetFormationPosition = new QueryData<WorldPosition>(delegate()
			{
				if (<>4__this.MedianTargetFormation != null)
				{
					return <>4__this.MedianTargetFormation.MedianPosition;
				}
				return <>4__this.MedianPosition;
			}, 1f);
			QueryData<WorldPosition>.SetupSyncGroup(new IQueryData[]
			{
				this._averageEnemyPosition,
				this._medianTargetFormationPosition
			});
			this._leftFlankEdgePosition = new QueryData<WorldPosition>(delegate()
			{
				Vec2 v = (<>4__this.MedianTargetFormationPosition.AsVec2 - <>4__this.AveragePosition).RightVec();
				v.Normalize();
				WorldPosition medianTargetFormationPosition = <>4__this.MedianTargetFormationPosition;
				medianTargetFormationPosition.SetVec2(medianTargetFormationPosition.AsVec2 - v * 50f);
				return medianTargetFormationPosition;
			}, 5f);
			this._rightFlankEdgePosition = new QueryData<WorldPosition>(delegate()
			{
				Vec2 v = (<>4__this.MedianTargetFormationPosition.AsVec2 - <>4__this.AveragePosition).RightVec();
				v.Normalize();
				WorldPosition medianTargetFormationPosition = <>4__this.MedianTargetFormationPosition;
				medianTargetFormationPosition.SetVec2(medianTargetFormationPosition.AsVec2 + v * 50f);
				return medianTargetFormationPosition;
			}, 5f);
			this._infantryRatio = new QueryData<float>(delegate()
			{
				if (<>4__this.MemberCount != 0)
				{
					return (<>4__this.Team.FormationsIncludingSpecialAndEmpty.Sum(delegate(Formation f)
					{
						if (f.CountOfUnits <= 0)
						{
							return 0f;
						}
						return f.QuerySystem.InfantryUnitRatio * (float)f.CountOfUnits;
					}) + (float)team.Heroes.Count((Agent h) => QueryLibrary.IsInfantry(h))) / (float)<>4__this.MemberCount;
				}
				return 0f;
			}, 15f);
			this._rangedRatio = new QueryData<float>(delegate()
			{
				if (<>4__this.MemberCount != 0)
				{
					return (<>4__this.Team.FormationsIncludingSpecialAndEmpty.Sum(delegate(Formation f)
					{
						if (f.CountOfUnits <= 0)
						{
							return 0f;
						}
						return f.QuerySystem.RangedUnitRatio * (float)f.CountOfUnits;
					}) + (float)team.Heroes.Count((Agent h) => QueryLibrary.IsRanged(h))) / (float)<>4__this.MemberCount;
				}
				return 0f;
			}, 15f);
			this._cavalryRatio = new QueryData<float>(delegate()
			{
				if (<>4__this.MemberCount != 0)
				{
					return (<>4__this.Team.FormationsIncludingSpecialAndEmpty.Sum(delegate(Formation f)
					{
						if (f.CountOfUnits <= 0)
						{
							return 0f;
						}
						return f.QuerySystem.CavalryUnitRatio * (float)f.CountOfUnits;
					}) + (float)team.Heroes.Count((Agent h) => QueryLibrary.IsCavalry(h))) / (float)<>4__this.MemberCount;
				}
				return 0f;
			}, 15f);
			this._rangedCavalryRatio = new QueryData<float>(delegate()
			{
				if (<>4__this.MemberCount != 0)
				{
					return (<>4__this.Team.FormationsIncludingSpecialAndEmpty.Sum(delegate(Formation f)
					{
						if (f.CountOfUnits <= 0)
						{
							return 0f;
						}
						return f.QuerySystem.RangedCavalryUnitRatio * (float)f.CountOfUnits;
					}) + (float)team.Heroes.Count((Agent h) => QueryLibrary.IsRangedCavalry(h))) / (float)<>4__this.MemberCount;
				}
				return 0f;
			}, 15f);
			QueryData<float>.SetupSyncGroup(new IQueryData[]
			{
				this._infantryRatio,
				this._rangedRatio,
				this._cavalryRatio,
				this._rangedCavalryRatio
			});
			this._allyInfantryRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsFriendOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.InfantryRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._allyRangedRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsFriendOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.RangedRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._allyCavalryRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsFriendOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.CavalryRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._allyRangedCavalryRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsFriendOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.RangedCavalryRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			QueryData<float>.SetupSyncGroup(new IQueryData[]
			{
				this._allyInfantryRatio,
				this._allyRangedRatio,
				this._allyCavalryRatio,
				this._allyRangedCavalryRatio
			});
			this._enemyInfantryRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.InfantryRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._enemyRangedRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.RangedRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._enemyCavalryRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.CavalryRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._enemyRangedCavalryRatio = new QueryData<float>(delegate()
			{
				float num = 0f;
				int num2 = 0;
				foreach (Team team2 in <>4__this._mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						int memberCount = team2.QuerySystem.MemberCount;
						num += team2.QuerySystem.RangedCavalryRatio * (float)memberCount;
						num2 += memberCount;
					}
				}
				if (num2 != 0)
				{
					return num / (float)num2;
				}
				return 0f;
			}, 15f);
			this._teamPower = new QueryData<float>(() => team.FormationsIncludingSpecialAndEmpty.Sum(delegate(Formation f)
			{
				if (f.CountOfUnits <= 0)
				{
					return 0f;
				}
				return f.GetFormationPower();
			}), 5f);
			this._remainingPowerRatio = new QueryData<float>(delegate()
			{
				BattlePowerCalculationLogic battlePowerLogic = <>4__this.BattlePowerLogic;
				CasualtyHandler casualtyHandler = <>4__this.CasualtyHandler;
				float num = 0f;
				float num2 = 0f;
				foreach (Team team2 in <>4__this.Team.Mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						num2 += battlePowerLogic.GetTotalTeamPower(team2);
						using (List<Formation>.Enumerator enumerator2 = team2.FormationsIncludingSpecialAndEmpty.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Formation formation = enumerator2.Current;
								num2 -= casualtyHandler.GetCasualtyPowerLossOfFormation(formation);
							}
							continue;
						}
					}
					num += battlePowerLogic.GetTotalTeamPower(team2);
					foreach (Formation formation2 in team2.FormationsIncludingSpecialAndEmpty)
					{
						num -= casualtyHandler.GetCasualtyPowerLossOfFormation(formation2);
					}
				}
				num = MathF.Max(0f, num);
				num2 = MathF.Max(0f, num2);
				return (num + 1f) / (num2 + 1f);
			}, 5f);
			this._totalPowerRatio = new QueryData<float>(delegate()
			{
				BattlePowerCalculationLogic battlePowerLogic = <>4__this.BattlePowerLogic;
				float num = 0f;
				float num2 = 0f;
				foreach (Team team2 in <>4__this.Team.Mission.Teams)
				{
					if (team2.IsEnemyOf(<>4__this.Team))
					{
						num2 += battlePowerLogic.GetTotalTeamPower(team2);
					}
					else
					{
						num += battlePowerLogic.GetTotalTeamPower(team2);
					}
				}
				return (num + 1f) / (num2 + 1f);
			}, 10f);
			this._insideWallsRatio = new QueryData<float>(delegate()
			{
				if (!(team.TeamAI is TeamAISiegeComponent))
				{
					return 1f;
				}
				if (<>4__this.AllyUnitCount == 0)
				{
					return 0f;
				}
				int num = 0;
				foreach (Team team2 in Mission.Current.Teams)
				{
					if (team2.IsFriendOf(team))
					{
						foreach (Formation formation in team2.FormationsIncludingSpecialAndEmpty)
						{
							if (formation.CountOfUnits > 0)
							{
								num += formation.CountUnitsOnNavMeshIDMod10(1, false);
							}
						}
					}
				}
				return (float)num / (float)<>4__this.AllyUnitCount;
			}, 10f);
			this._maxUnderRangedAttackRatio = new QueryData<float>(delegate()
			{
				float num;
				if (<>4__this.AllyUnitCount == 0)
				{
					num = 0f;
				}
				else
				{
					float currentTime = MBCommon.GetTotalMissionTime();
					int num2 = 0;
					Func<Agent, bool> <>9__35;
					foreach (Team team2 in <>4__this._mission.Teams)
					{
						if (team2.IsFriendOf(<>4__this.Team))
						{
							for (int i = 0; i < Math.Min(team2.FormationsIncludingSpecialAndEmpty.Count, 8); i++)
							{
								Formation formation = team2.FormationsIncludingSpecialAndEmpty[i];
								if (formation.CountOfUnits > 0)
								{
									int num3 = num2;
									Formation formation2 = formation;
									Func<Agent, bool> function;
									if ((function = <>9__35) == null)
									{
										function = (<>9__35 = ((Agent agent) => currentTime - agent.LastRangedHitTime < 10f && !agent.Equipment.HasShield()));
									}
									num2 = num3 + formation2.GetCountOfUnitsWithCondition(function);
								}
							}
						}
					}
					num = (float)num2 / (float)<>4__this.AllyUnitCount;
				}
				if (num <= <>4__this._maxUnderRangedAttackRatio.GetCachedValue())
				{
					return <>4__this._maxUnderRangedAttackRatio.GetCachedValue();
				}
				return num;
			}, 3f);
			this.DeathCount = 0;
			this.DeathByRangedCount = 0;
			this.InitializeTelemetryScopeNames();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000491DC File Offset: 0x000473DC
		public void RegisterDeath()
		{
			int deathCount = this.DeathCount;
			this.DeathCount = deathCount + 1;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000491FC File Offset: 0x000473FC
		public void RegisterDeathByRanged()
		{
			int deathByRangedCount = this.DeathByRangedCount;
			this.DeathByRangedCount = deathByRangedCount + 1;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004921C File Offset: 0x0004741C
		public float GetLocalAllyPower(Vec2 target)
		{
			return this.Team.FormationsIncludingSpecialAndEmpty.Sum(delegate(Formation f)
			{
				if (f.CountOfUnits <= 0)
				{
					return 0f;
				}
				return f.QuerySystem.FormationPower / f.QuerySystem.AveragePosition.Distance(target);
			});
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00049254 File Offset: 0x00047454
		public float GetLocalEnemyPower(Vec2 target)
		{
			float num = 0f;
			foreach (Team team in Mission.Current.Teams)
			{
				if (this.Team.IsEnemyOf(team))
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						if (formation.CountOfUnits > 0)
						{
							num += formation.QuerySystem.FormationPower / formation.QuerySystem.AveragePosition.Distance(target);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x04000539 RID: 1337
		public readonly Team Team;

		// Token: 0x0400053A RID: 1338
		private readonly Mission _mission;

		// Token: 0x0400053B RID: 1339
		private readonly QueryData<int> _memberCount;

		// Token: 0x0400053C RID: 1340
		private readonly QueryData<WorldPosition> _medianPosition;

		// Token: 0x0400053D RID: 1341
		private readonly QueryData<Vec2> _averagePosition;

		// Token: 0x0400053E RID: 1342
		private readonly QueryData<Vec2> _averageEnemyPosition;

		// Token: 0x0400053F RID: 1343
		private readonly QueryData<FormationQuerySystem> _medianTargetFormation;

		// Token: 0x04000540 RID: 1344
		private readonly QueryData<WorldPosition> _medianTargetFormationPosition;

		// Token: 0x04000541 RID: 1345
		private readonly QueryData<WorldPosition> _leftFlankEdgePosition;

		// Token: 0x04000542 RID: 1346
		private readonly QueryData<WorldPosition> _rightFlankEdgePosition;

		// Token: 0x04000543 RID: 1347
		private readonly QueryData<float> _infantryRatio;

		// Token: 0x04000544 RID: 1348
		private readonly QueryData<float> _rangedRatio;

		// Token: 0x04000545 RID: 1349
		private readonly QueryData<float> _cavalryRatio;

		// Token: 0x04000546 RID: 1350
		private readonly QueryData<float> _rangedCavalryRatio;

		// Token: 0x04000547 RID: 1351
		private readonly QueryData<int> _allyMemberCount;

		// Token: 0x04000548 RID: 1352
		private readonly QueryData<int> _enemyMemberCount;

		// Token: 0x04000549 RID: 1353
		private readonly QueryData<float> _allyInfantryRatio;

		// Token: 0x0400054A RID: 1354
		private readonly QueryData<float> _allyRangedRatio;

		// Token: 0x0400054B RID: 1355
		private readonly QueryData<float> _allyCavalryRatio;

		// Token: 0x0400054C RID: 1356
		private readonly QueryData<float> _allyRangedCavalryRatio;

		// Token: 0x0400054D RID: 1357
		private readonly QueryData<float> _enemyInfantryRatio;

		// Token: 0x0400054E RID: 1358
		private readonly QueryData<float> _enemyRangedRatio;

		// Token: 0x0400054F RID: 1359
		private readonly QueryData<float> _enemyCavalryRatio;

		// Token: 0x04000550 RID: 1360
		private readonly QueryData<float> _enemyRangedCavalryRatio;

		// Token: 0x04000551 RID: 1361
		private readonly QueryData<float> _remainingPowerRatio;

		// Token: 0x04000552 RID: 1362
		private readonly QueryData<float> _teamPower;

		// Token: 0x04000553 RID: 1363
		private readonly QueryData<float> _totalPowerRatio;

		// Token: 0x04000554 RID: 1364
		private readonly QueryData<float> _insideWallsRatio;

		// Token: 0x04000555 RID: 1365
		private BattlePowerCalculationLogic _battlePowerLogic;

		// Token: 0x04000556 RID: 1366
		private CasualtyHandler _casualtyHandler;

		// Token: 0x04000557 RID: 1367
		private readonly QueryData<float> _maxUnderRangedAttackRatio;
	}
}
