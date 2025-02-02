using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000184 RID: 388
	public class TeamAISallyOutDefender : TeamAISiegeComponent
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0004B869 File Offset: 0x00049A69
		public List<ArcherPosition> ArcherPositions { get; }

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004B874 File Offset: 0x00049A74
		public TeamAISallyOutDefender(Mission currentMission, Team currentTeam, float thinkTimerTime, float applyTimerTime) : base(currentMission, currentTeam, thinkTimerTime, applyTimerTime)
		{
			TeamAISallyOutDefender <>4__this = this;
			TeamAISiegeComponent.QuerySystem = new SiegeQuerySystem(this.Team, TeamAISiegeComponent.SiegeLanes);
			TeamAISiegeComponent.QuerySystem.Expire();
			this.DefensePosition = (() => new WorldPosition(currentMission.Scene, UIntPtr.Zero, <>4__this.Ram.GameEntity.GlobalPosition, false));
			IEnumerable<GameEntity> source = from ap in currentMission.Scene.FindEntitiesWithTag("archer_position")
			where ap.Parent == null || ap.Parent.IsVisibleIncludeParents()
			select ap;
			this.ArcherPositions = (from ap in source
			select new ArcherPosition(ap, TeamAISiegeComponent.QuerySystem, BattleSideEnum.Defender)).ToList<ArcherPosition>();
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004B940 File Offset: 0x00049B40
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
				formation.AI.AddAiBehavior(new BehaviorDefendSiegeWeapon(formation));
				formation.AI.AddAiBehavior(new BehaviorSparseSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorSkirmishLine(formation));
				formation.AI.AddAiBehavior(new BehaviorScreenedSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorProtectFlank(formation));
				formation.AI.AddAiBehavior(new BehaviorFlank(formation));
				formation.AI.AddAiBehavior(new BehaviorHorseArcherSkirmish(formation));
				formation.AI.AddAiBehavior(new BehaviorDefend(formation));
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004BAA4 File Offset: 0x00049CA4
		public Vec3 CalculateSallyOutReferencePosition(FormationAI.BehaviorSide side)
		{
			if (side != FormationAI.BehaviorSide.Left)
			{
				if (side != FormationAI.BehaviorSide.Right)
				{
					return this.Ram.GameEntity.GlobalPosition;
				}
				SiegeTower siegeTower = this.SiegeTowers.FirstOrDefault((SiegeTower st) => st.WeaponSide == FormationAI.BehaviorSide.Right);
				if (siegeTower == null)
				{
					return this.Ram.GameEntity.GlobalPosition;
				}
				return siegeTower.GameEntity.GlobalPosition;
			}
			else
			{
				SiegeTower siegeTower2 = this.SiegeTowers.FirstOrDefault((SiegeTower st) => st.WeaponSide == FormationAI.BehaviorSide.Left);
				if (siegeTower2 == null)
				{
					return this.Ram.GameEntity.GlobalPosition;
				}
				return siegeTower2.GameEntity.GlobalPosition;
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004BB64 File Offset: 0x00049D64
		public override void OnDeploymentFinished()
		{
			TeamAISiegeComponent.SiegeLanes.Clear();
			int i;
			int j;
			for (i = 0; i < 3; i = j + 1)
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
				j = i;
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

		// Token: 0x0400059A RID: 1434
		public readonly Func<WorldPosition> DefensePosition;
	}
}
