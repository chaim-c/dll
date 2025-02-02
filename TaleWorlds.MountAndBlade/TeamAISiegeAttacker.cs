using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000185 RID: 389
	public class TeamAISiegeAttacker : TeamAISiegeComponent
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0004BCBE File Offset: 0x00049EBE
		public MBReadOnlyList<ArcherPosition> ArcherPositions
		{
			get
			{
				return this._archerPositions;
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004BCC8 File Offset: 0x00049EC8
		public TeamAISiegeAttacker(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
			IEnumerable<GameEntity> source = currentMission.Scene.FindEntitiesWithTag("archer_position_attacker");
			this._archerPositions = (from ap in source
			select new ArcherPosition(ap, TeamAISiegeComponent.QuerySystem, BattleSideEnum.Attacker)).ToMBList<ArcherPosition>();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004BD24 File Offset: 0x00049F24
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
				formation.AI.AddAiBehavior(new BehaviorAssaultWalls(formation));
				formation.AI.AddAiBehavior(new BehaviorShootFromSiegeTower(formation));
				formation.AI.AddAiBehavior(new BehaviorUseSiegeMachines(formation));
				formation.AI.AddAiBehavior(new BehaviorWaitForLadders(formation));
				formation.AI.AddAiBehavior(new BehaviorSparseSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorRetreatToKeep(formation));
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004BE68 File Offset: 0x0004A068
		public override void OnDeploymentFinished()
		{
			base.OnDeploymentFinished();
			foreach (SiegeTower siegeTower in this.SiegeTowers)
			{
				base.DifficultNavmeshIDs.AddRange(siegeTower.CollectGetDifficultNavmeshIDsForAttackers());
			}
			foreach (ArcherPosition archerPosition in this._archerPositions)
			{
				archerPosition.OnDeploymentFinished(TeamAISiegeComponent.QuerySystem, BattleSideEnum.Attacker);
			}
		}

		// Token: 0x0400059C RID: 1436
		private readonly MBList<ArcherPosition> _archerPositions;
	}
}
