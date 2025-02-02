using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000187 RID: 391
	public class TeamAISiegeDefender : TeamAISiegeComponent
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0004C984 File Offset: 0x0004AB84
		public List<ArcherPosition> ArcherPositions { get; }

		// Token: 0x06001407 RID: 5127 RVA: 0x0004C98C File Offset: 0x0004AB8C
		public TeamAISiegeDefender(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
			TeamAISiegeComponent.QuerySystem = new SiegeQuerySystem(this.Team, TeamAISiegeComponent.SiegeLanes);
			TeamAISiegeComponent.QuerySystem.Expire();
			IEnumerable<GameEntity> source = from ap in currentMission.Scene.FindEntitiesWithTag("archer_position")
			where ap.Parent == null || ap.Parent.IsVisibleIncludeParents()
			select ap;
			this.ArcherPositions = (from ap in source
			select new ArcherPosition(ap, TeamAISiegeComponent.QuerySystem, BattleSideEnum.Defender)).ToList<ArcherPosition>();
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0004CA28 File Offset: 0x0004AC28
		public override void OnUnitAddedToFormationForTheFirstTime(Formation formation)
		{
			if (formation.AI.GetBehavior<BehaviorCharge>() == null)
			{
				if (formation.FormationIndex == FormationClass.NumberOfRegularFormations)
				{
					formation.AI.AddAiBehavior(new BehaviorGeneral(formation));
				}
				else if (formation.FormationIndex == FormationClass.Bodyguard)
				{
					formation.AI.AddAiBehavior(new BehaviorProtectGeneral(formation));
				}
				formation.AI.AddAiBehavior(new BehaviorCharge(formation));
				formation.AI.AddAiBehavior(new BehaviorPullBack(formation));
				formation.AI.AddAiBehavior(new BehaviorRegroup(formation));
				formation.AI.AddAiBehavior(new BehaviorReserve(formation));
				formation.AI.AddAiBehavior(new BehaviorRetreat(formation));
				formation.AI.AddAiBehavior(new BehaviorStop(formation));
				formation.AI.AddAiBehavior(new BehaviorTacticalCharge(formation));
				formation.AI.AddAiBehavior(new BehaviorDefendCastleKeyPosition(formation));
				formation.AI.AddAiBehavior(new BehaviorEliminateEnemyInsideCastle(formation));
				formation.AI.AddAiBehavior(new BehaviorRetakeCastleKeyPosition(formation));
				formation.AI.AddAiBehavior(new BehaviorRetreatToKeep(formation));
				formation.AI.AddAiBehavior(new BehaviorSallyOut(formation));
				formation.AI.AddAiBehavior(new BehaviorUseMurderHole(formation));
				formation.AI.AddAiBehavior(new BehaviorShootFromCastleWalls(formation));
				formation.AI.AddAiBehavior(new BehaviorSparseSkirmish(formation));
			}
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0004CB7C File Offset: 0x0004AD7C
		public override void OnDeploymentFinished()
		{
			base.OnDeploymentFinished();
			foreach (SiegeTower siegeTower in this.SiegeTowers)
			{
				base.DifficultNavmeshIDs.AddRange(siegeTower.CollectGetDifficultNavmeshIDsForDefenders());
			}
			List<SiegeLane> list = TeamAISiegeComponent.SiegeLanes.ToList<SiegeLane>();
			TeamAISiegeComponent.SiegeLanes.Clear();
			int i;
			int i2;
			for (i = 0; i < 3; i = i2 + 1)
			{
				TeamAISiegeComponent.SiegeLanes.Add(new SiegeLane((FormationAI.BehaviorSide)i, TeamAISiegeComponent.QuerySystem));
				SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes[i];
				siegeLane.SetPrimarySiegeWeapons((from psw in base.PrimarySiegeWeapons
				where psw.WeaponSide == (FormationAI.BehaviorSide)i
				select psw).ToList<IPrimarySiegeWeapon>());
				siegeLane.SetDefensePoints((from ckp in this.CastleKeyPositions
				where (ckp as ICastleKeyPosition).DefenseSide == (FormationAI.BehaviorSide)i
				select ckp into dp
				select dp as ICastleKeyPosition).ToList<ICastleKeyPosition>());
				siegeLane.RefreshLane();
				siegeLane.DetermineLaneState();
				siegeLane.DetermineOrigins();
				if (i < list.Count)
				{
					for (int j = 0; j < Mission.Current.Teams.Count; j++)
					{
						siegeLane.SetLastAssignedFormation(j, list[i].GetLastAssignedFormation(j));
					}
				}
				i2 = i;
			}
			TeamAISiegeComponent.QuerySystem = new SiegeQuerySystem(this.Team, TeamAISiegeComponent.SiegeLanes);
			TeamAISiegeComponent.QuerySystem.Expire();
			TeamAISiegeComponent.SiegeLanes.ForEach(delegate(SiegeLane sl)
			{
				sl.SetSiegeQuerySystem(TeamAISiegeComponent.QuerySystem);
			});
			this.ArcherPositions.ForEach(delegate(ArcherPosition ap)
			{
				ap.OnDeploymentFinished(TeamAISiegeComponent.QuerySystem, BattleSideEnum.Defender);
			});
		}

		// Token: 0x040005B1 RID: 1457
		public const float InsideEnemyThresholdRatio = 0.5f;

		// Token: 0x040005B3 RID: 1459
		public Vec3 MurderHolePosition;
	}
}
