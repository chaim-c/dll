using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000183 RID: 387
	public class TeamAISallyOutAttacker : TeamAISiegeComponent
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0004B69C File Offset: 0x0004989C
		public TeamAISallyOutAttacker(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
			this.ArcherPositions = currentMission.Scene.FindEntitiesWithTag("archer_position").ToMBList<GameEntity>();
			this.BesiegerRangedSiegeWeapons = new List<UsableMachine>(from w in currentMission.ActiveMissionObjects.FindAllWithType<RangedSiegeWeapon>()
			where w.Side == BattleSideEnum.Attacker && !w.IsDisabled
			select w);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004B70C File Offset: 0x0004990C
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
				formation.AI.AddAiBehavior(new BehaviorShootFromCastleWalls(formation));
				formation.AI.AddAiBehavior(new BehaviorDestroySiegeWeapons(formation));
				formation.AI.AddAiBehavior(new BehaviorSparseSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorDefend(formation));
				formation.AI.AddAiBehavior(new BehaviorRetreatToCastle(formation));
				formation.AI.AddAiBehavior(new BehaviorRetreatToKeep(formation));
				formation.AI.AddAiBehavior(new BehaviorDefendCastleKeyPosition(formation));
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004B84E File Offset: 0x00049A4E
		public override void OnDeploymentFinished()
		{
			base.OnDeploymentFinished();
			if (base.CurrentTactic != null)
			{
				base.CurrentTactic.ResetTactic();
			}
		}

		// Token: 0x04000598 RID: 1432
		public MBList<GameEntity> ArcherPositions;

		// Token: 0x04000599 RID: 1433
		public readonly List<UsableMachine> BesiegerRangedSiegeWeapons;
	}
}
