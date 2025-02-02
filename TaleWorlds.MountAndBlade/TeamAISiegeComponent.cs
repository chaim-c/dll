using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000186 RID: 390
	public abstract class TeamAISiegeComponent : TeamAIComponent
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0004BF10 File Offset: 0x0004A110
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x0004BF17 File Offset: 0x0004A117
		public static List<SiegeLane> SiegeLanes { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0004BF1F File Offset: 0x0004A11F
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x0004BF26 File Offset: 0x0004A126
		public static SiegeQuerySystem QuerySystem { get; protected set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0004BF2E File Offset: 0x0004A12E
		public CastleGate OuterGate { get; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0004BF36 File Offset: 0x0004A136
		public List<IPrimarySiegeWeapon> PrimarySiegeWeapons { get; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0004BF3E File Offset: 0x0004A13E
		public CastleGate InnerGate { get; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0004BF46 File Offset: 0x0004A146
		public MBReadOnlyList<SiegeLadder> Ladders
		{
			get
			{
				return this._ladders;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0004BF4E File Offset: 0x0004A14E
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x0004BF56 File Offset: 0x0004A156
		public bool AreLaddersReady { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0004BF5F File Offset: 0x0004A15F
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x0004BF67 File Offset: 0x0004A167
		public List<int> DifficultNavmeshIDs { get; private set; }

		// Token: 0x060013FA RID: 5114 RVA: 0x0004BF70 File Offset: 0x0004A170
		protected TeamAISiegeComponent(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
			this.CastleGates = currentMission.ActiveMissionObjects.FindAllWithType<CastleGate>().ToList<CastleGate>();
			this.WallSegments = currentMission.ActiveMissionObjects.FindAllWithType<WallSegment>().ToList<WallSegment>();
			this.OuterGate = this.CastleGates.FirstOrDefault((CastleGate g) => g.GameEntity.HasTag("outer_gate"));
			this.InnerGate = this.CastleGates.FirstOrDefault((CastleGate g) => g.GameEntity.HasTag("inner_gate"));
			this.SceneSiegeWeapons = Mission.Current.MissionObjects.FindAllWithType<SiegeWeapon>().ToList<SiegeWeapon>();
			this._ladders = this.SceneSiegeWeapons.OfType<SiegeLadder>().ToMBList<SiegeLadder>();
			this.Ram = (this.SceneSiegeWeapons.FirstOrDefault((SiegeWeapon ssw) => ssw is BatteringRam) as BatteringRam);
			this.SiegeTowers = this.SceneSiegeWeapons.OfType<SiegeTower>().ToList<SiegeTower>();
			this.PrimarySiegeWeapons = new List<IPrimarySiegeWeapon>();
			this.PrimarySiegeWeapons.AddRange(this._ladders);
			if (this.Ram != null)
			{
				this.PrimarySiegeWeapons.Add(this.Ram);
			}
			this.PrimarySiegeWeapons.AddRange(this.SiegeTowers);
			this.PrimarySiegeWeaponNavMeshFaceIDs = new HashSet<int>();
			using (List<IPrimarySiegeWeapon>.Enumerator enumerator = this.PrimarySiegeWeapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IPrimarySiegeWeapon primarySiegeWeapon;
					List<int> other;
					if ((primarySiegeWeapon = enumerator.Current) != null && primarySiegeWeapon.GetNavmeshFaceIds(out other))
					{
						this.PrimarySiegeWeaponNavMeshFaceIDs.UnionWith(other);
					}
				}
			}
			this.CastleKeyPositions = new List<MissionObject>();
			this.CastleKeyPositions.AddRange(this.CastleGates);
			this.CastleKeyPositions.AddRange(this.WallSegments);
			TeamAISiegeComponent.SiegeLanes = new List<SiegeLane>();
			int i;
			int j;
			for (i = 0; i < 3; i = j + 1)
			{
				TeamAISiegeComponent.SiegeLanes.Add(new SiegeLane((FormationAI.BehaviorSide)i, TeamAISiegeComponent.QuerySystem));
				TeamAISiegeComponent.SiegeLanes[i].SetPrimarySiegeWeapons((from psw in this.PrimarySiegeWeapons
				where psw.WeaponSide == (FormationAI.BehaviorSide)i
				select psw into um
				select um).ToList<IPrimarySiegeWeapon>());
				TeamAISiegeComponent.SiegeLanes[i].SetDefensePoints((from ckp in this.CastleKeyPositions
				where ((ICastleKeyPosition)ckp).DefenseSide == (FormationAI.BehaviorSide)i
				select ckp into dp
				select (ICastleKeyPosition)dp).ToList<ICastleKeyPosition>());
				TeamAISiegeComponent.SiegeLanes[i].RefreshLane();
				j = i;
			}
			TeamAISiegeComponent.SiegeLanes.ForEach(delegate(SiegeLane sl)
			{
				sl.SetSiegeQuerySystem(TeamAISiegeComponent.QuerySystem);
			});
			this.DifficultNavmeshIDs = new List<int>();
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0004C2AC File Offset: 0x0004A4AC
		protected internal override void Tick(float dt)
		{
			if (!this._noProperLaneRemains)
			{
				int num = 0;
				SiegeLane siegeLane = null;
				foreach (SiegeLane siegeLane2 in TeamAISiegeComponent.SiegeLanes)
				{
					siegeLane2.RefreshLane();
					siegeLane2.DetermineLaneState();
					if (siegeLane2.IsBreach)
					{
						num++;
					}
					else
					{
						siegeLane = siegeLane2;
					}
				}
				if (siegeLane != null && num >= 2 && !siegeLane.IsOpen && siegeLane.LaneState >= SiegeLane.LaneStateEnum.Used)
				{
					siegeLane.SetLaneState(SiegeLane.LaneStateEnum.Unused);
				}
				if (TeamAISiegeComponent.SiegeLanes.Count != 0)
				{
					goto IL_1D0;
				}
				this._noProperLaneRemains = true;
				using (IEnumerator<FormationAI.BehaviorSide> enumerator2 = (from ckp in this.CastleKeyPositions.Where(delegate(MissionObject ckp)
				{
					CastleGate castleGate;
					return (castleGate = (ckp as CastleGate)) != null && castleGate.DefenseSide != FormationAI.BehaviorSide.BehaviorSideNotSet;
				})
				select ((CastleGate)ckp).DefenseSide).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						FormationAI.BehaviorSide difficultLaneSide = enumerator2.Current;
						SiegeLane siegeLane3 = new SiegeLane(difficultLaneSide, TeamAISiegeComponent.QuerySystem);
						siegeLane3.SetPrimarySiegeWeapons(new List<IPrimarySiegeWeapon>());
						siegeLane3.SetDefensePoints((from ckp in this.CastleKeyPositions
						where ((ICastleKeyPosition)ckp).DefenseSide == difficultLaneSide && ckp is CastleGate
						select ckp into dp
						select dp as ICastleKeyPosition).ToList<ICastleKeyPosition>());
						siegeLane3.RefreshLane();
						siegeLane3.DetermineLaneState();
						TeamAISiegeComponent.SiegeLanes.Add(siegeLane3);
					}
					goto IL_1D0;
				}
			}
			foreach (SiegeLane siegeLane4 in TeamAISiegeComponent.SiegeLanes)
			{
				siegeLane4.RefreshLane();
				siegeLane4.DetermineLaneState();
			}
			IL_1D0:
			base.Tick(dt);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004C4B8 File Offset: 0x0004A6B8
		public static void OnMissionFinalize()
		{
			if (TeamAISiegeComponent.SiegeLanes != null)
			{
				TeamAISiegeComponent.SiegeLanes.Clear();
				TeamAISiegeComponent.SiegeLanes = null;
			}
			TeamAISiegeComponent.QuerySystem = null;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0004C4D8 File Offset: 0x0004A6D8
		public bool CalculateIsChargePastWallsApplicable(FormationAI.BehaviorSide side)
		{
			if (Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.SallyOut)
			{
				return false;
			}
			if (side == FormationAI.BehaviorSide.BehaviorSideNotSet && this.InnerGate != null && !this.InnerGate.IsGateOpen)
			{
				return false;
			}
			foreach (SiegeLane siegeLane in TeamAISiegeComponent.SiegeLanes)
			{
				if (side == FormationAI.BehaviorSide.BehaviorSideNotSet)
				{
					if (!siegeLane.IsOpen)
					{
						return false;
					}
				}
				else if (side == siegeLane.LaneSide)
				{
					return siegeLane.IsOpen && (siegeLane.IsBreach || (siegeLane.HasGate && (this.InnerGate == null || this.InnerGate.IsGateOpen)));
				}
			}
			return true;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0004C5A4 File Offset: 0x0004A7A4
		public void SetAreLaddersReady(bool areLaddersReady)
		{
			this.AreLaddersReady = areLaddersReady;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004C5AD File Offset: 0x0004A7AD
		public bool CalculateIsAnyLaneOpenToGetInside()
		{
			return TeamAISiegeComponent.SiegeLanes.Any((SiegeLane sl) => sl.IsOpen);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004C5D8 File Offset: 0x0004A7D8
		public bool CalculateIsAnyLaneOpenToGoOutside()
		{
			return TeamAISiegeComponent.SiegeLanes.Any(delegate(SiegeLane sl)
			{
				if (!sl.IsOpen)
				{
					return false;
				}
				if (!sl.IsBreach && !sl.HasGate)
				{
					return sl.PrimarySiegeWeapons.Any((IPrimarySiegeWeapon psw) => psw is SiegeTower);
				}
				return true;
			});
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004C603 File Offset: 0x0004A803
		public bool IsPrimarySiegeWeaponNavmeshFaceId(int id)
		{
			return this.PrimarySiegeWeaponNavMeshFaceIDs.Contains(id);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0004C614 File Offset: 0x0004A814
		public static bool IsFormationGroupInsideCastle(MBList<Formation> formationGroup, bool includeOnlyPositionedUnits, float thresholdPercentage = 0.4f)
		{
			int num = 0;
			foreach (Formation formation in formationGroup)
			{
				num += (includeOnlyPositionedUnits ? formation.Arrangement.PositionedUnitCount : formation.CountOfUnits);
			}
			float num2 = (float)num * thresholdPercentage;
			foreach (Formation formation2 in formationGroup)
			{
				if (formation2.CountOfUnits > 0)
				{
					num2 -= (float)formation2.CountUnitsOnNavMeshIDMod10(1, includeOnlyPositionedUnits);
					if (num2 <= 0f)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0004C6DC File Offset: 0x0004A8DC
		public static bool IsFormationInsideCastle(Formation formation, bool includeOnlyPositionedUnits, float thresholdPercentage = 0.4f)
		{
			int num = includeOnlyPositionedUnits ? formation.Arrangement.PositionedUnitCount : formation.CountOfUnits;
			float num2 = (float)num * thresholdPercentage;
			if (num == 0)
			{
				return !(formation.Team.TeamAI is TeamAISiegeAttacker) && !(formation.Team.TeamAI is TeamAISallyOutDefender) && (formation.Team.TeamAI is TeamAISiegeDefender || formation.Team.TeamAI is TeamAISallyOutAttacker);
			}
			if (includeOnlyPositionedUnits)
			{
				return (float)formation.QuerySystem.InsideCastleUnitCountPositioned >= num2;
			}
			return (float)formation.QuerySystem.InsideCastleUnitCountIncludingUnpositioned >= num2;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004C77C File Offset: 0x0004A97C
		public bool IsCastleBreached()
		{
			int num = 0;
			int num2 = 0;
			foreach (Formation formation in this.Mission.AttackerTeam.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					num2++;
					if (TeamAISiegeComponent.IsFormationInsideCastle(formation, true, 0.4f))
					{
						num++;
					}
				}
			}
			if (this.Mission.AttackerAllyTeam != null)
			{
				foreach (Formation formation2 in this.Mission.AttackerAllyTeam.FormationsIncludingSpecialAndEmpty)
				{
					if (formation2.CountOfUnits > 0)
					{
						num2++;
						if (TeamAISiegeComponent.IsFormationInsideCastle(formation2, true, 0.4f))
						{
							num++;
						}
					}
				}
			}
			return (float)num >= (float)num2 * 0.7f;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0004C878 File Offset: 0x0004AA78
		public override void OnDeploymentFinished()
		{
			foreach (SiegeLadder siegeLadder in from l in this._ladders
			where !l.IsDisabled
			select l)
			{
				this.DifficultNavmeshIDs.Add(siegeLadder.OnWallNavMeshId);
			}
			foreach (SiegeTower siegeTower in this.SiegeTowers)
			{
				this.DifficultNavmeshIDs.AddRange(siegeTower.CollectGetDifficultNavmeshIDs());
			}
			foreach (Formation formation in this.Team.FormationsIncludingEmpty)
			{
				formation.OnDeploymentFinished();
			}
		}

		// Token: 0x0400059D RID: 1437
		public const int InsideCastleNavMeshID = 1;

		// Token: 0x0400059E RID: 1438
		public const int SiegeTokenForceSize = 15;

		// Token: 0x0400059F RID: 1439
		private const float FormationInsideCastleThresholdPercentage = 0.4f;

		// Token: 0x040005A0 RID: 1440
		private const float CastleBreachThresholdPercentage = 0.7f;

		// Token: 0x040005A3 RID: 1443
		public readonly IEnumerable<WallSegment> WallSegments;

		// Token: 0x040005A4 RID: 1444
		public readonly List<SiegeWeapon> SceneSiegeWeapons;

		// Token: 0x040005A5 RID: 1445
		protected readonly IEnumerable<CastleGate> CastleGates;

		// Token: 0x040005A6 RID: 1446
		protected readonly List<SiegeTower> SiegeTowers;

		// Token: 0x040005A7 RID: 1447
		protected readonly HashSet<int> PrimarySiegeWeaponNavMeshFaceIDs;

		// Token: 0x040005A8 RID: 1448
		protected BatteringRam Ram;

		// Token: 0x040005A9 RID: 1449
		protected List<MissionObject> CastleKeyPositions;

		// Token: 0x040005AA RID: 1450
		private readonly MBList<SiegeLadder> _ladders;

		// Token: 0x040005AB RID: 1451
		private bool _noProperLaneRemains;
	}
}
