using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000285 RID: 645
	public abstract class SallyOutMissionController : MissionLogic
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0007B46A File Offset: 0x0007966A
		private float BesiegedDeploymentDuration
		{
			get
			{
				return 55f;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0007B471 File Offset: 0x00079671
		private float BesiegerActivationDuration
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0007B478 File Offset: 0x00079678
		public MBReadOnlyList<SiegeWeapon> BesiegerSiegeEngines
		{
			get
			{
				return this._besiegerSiegeEngines;
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0007B480 File Offset: 0x00079680
		public override void OnBehaviorInitialize()
		{
			this.MissionAgentSpawnLogic = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
			this._sallyOutNotificationsHandler = new SallyOutMissionNotificationsHandler(this.MissionAgentSpawnLogic, this);
			Mission.Current.GetOverriddenFleePositionForAgent += this.GetSallyOutFleePositionForAgent;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0007B4BC File Offset: 0x000796BC
		public override void AfterStart()
		{
			this._sallyOutNotificationsHandler.OnAfterStart();
			int besiegedTotalTroopCount;
			int besiegerTotalTroopCount;
			this.GetInitialTroopCounts(out besiegedTotalTroopCount, out besiegerTotalTroopCount);
			this.SetupInitialSpawn(besiegedTotalTroopCount, besiegerTotalTroopCount);
			this._castleGates = base.Mission.MissionObjects.FindAllWithType<CastleGate>().ToList<CastleGate>();
			this._besiegedDeploymentTimer = new BasicMissionTimer();
			TeamAIComponent teamAI = base.Mission.DefenderTeam.TeamAI;
			teamAI.OnNotifyTacticalDecision = (TeamAIComponent.TacticalDecisionDelegate)Delegate.Combine(teamAI.OnNotifyTacticalDecision, new TeamAIComponent.TacticalDecisionDelegate(this.OnDefenderTeamTacticalDecision));
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0007B53D File Offset: 0x0007973D
		public override void OnMissionTick(float dt)
		{
			this._sallyOutNotificationsHandler.OnMissionTick(dt);
			this.UpdateTimers();
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0007B551 File Offset: 0x00079751
		public override void OnDeploymentFinished()
		{
			this._besiegerSiegeEngines = SallyOutMissionController.GetBesiegerSiegeEngines();
			SallyOutMissionController.DisableSiegeEngines();
			Mission.Current.AddMissionBehavior(new SallyOutEndLogic());
			this._sallyOutNotificationsHandler.OnDeploymentFinished();
			this._besiegerActivationTimer = new BasicMissionTimer();
			this.DeactivateBesiegers();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0007B58E File Offset: 0x0007978E
		protected override void OnEndMission()
		{
			this._sallyOutNotificationsHandler.OnMissionEnd();
			Mission.Current.GetOverriddenFleePositionForAgent -= this.GetSallyOutFleePositionForAgent;
		}

		// Token: 0x060021DA RID: 8666
		protected abstract void GetInitialTroopCounts(out int besiegedTotalTroopCount, out int besiegerTotalTroopCount);

		// Token: 0x060021DB RID: 8667 RVA: 0x0007B5B4 File Offset: 0x000797B4
		private void UpdateTimers()
		{
			if (this._besiegedDeploymentTimer != null)
			{
				if (this._besiegedDeploymentTimer.ElapsedTime >= this.BesiegedDeploymentDuration)
				{
					foreach (CastleGate castleGate in this._castleGates)
					{
						castleGate.SetAutoOpenState(true);
					}
					this._besiegedDeploymentTimer = null;
					goto IL_127;
				}
				using (List<CastleGate>.Enumerator enumerator = this._castleGates.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CastleGate castleGate2 = enumerator.Current;
						if (!castleGate2.IsDestroyed && !castleGate2.IsGateOpen)
						{
							castleGate2.OpenDoor();
						}
					}
					goto IL_127;
				}
			}
			Agent mainAgent = base.Mission.MainAgent;
			if (mainAgent != null && mainAgent.IsActive())
			{
				Vec3 eyeGlobalPosition = mainAgent.GetEyeGlobalPosition();
				foreach (CastleGate castleGate3 in this._castleGates)
				{
					if (!castleGate3.IsDestroyed && !castleGate3.IsGateOpen && eyeGlobalPosition.DistanceSquared(castleGate3.GameEntity.GlobalPosition) <= 25f)
					{
						castleGate3.OpenDoor();
					}
				}
			}
			IL_127:
			if (this._besiegerActivationTimer != null && this._besiegerActivationTimer.ElapsedTime >= this.BesiegerActivationDuration)
			{
				this.ActivateBesiegers();
				this._besiegerActivationTimer = null;
			}
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0007B738 File Offset: 0x00079938
		private void AdjustTotalTroopCounts(ref int besiegedTotalTroopCount, ref int besiegerTotalTroopCount)
		{
			float num = 0.25f;
			float num2 = 1f - num;
			int b = (int)((float)this.MissionAgentSpawnLogic.BattleSize * num);
			int b2 = (int)((float)this.MissionAgentSpawnLogic.BattleSize * num2);
			besiegedTotalTroopCount = MathF.Min(besiegedTotalTroopCount, b);
			besiegerTotalTroopCount = MathF.Min(besiegerTotalTroopCount, b2);
			float num3 = num2 / num;
			if ((float)besiegerTotalTroopCount / (float)besiegedTotalTroopCount <= num3)
			{
				int a = (int)((float)besiegerTotalTroopCount / num3);
				besiegedTotalTroopCount = MathF.Min(a, besiegedTotalTroopCount);
				return;
			}
			int a2 = (int)((float)besiegedTotalTroopCount * num3);
			besiegerTotalTroopCount = MathF.Min(a2, besiegerTotalTroopCount);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0007B7C0 File Offset: 0x000799C0
		private void SetupInitialSpawn(int besiegedTotalTroopCount, int besiegerTotalTroopCount)
		{
			this.AdjustTotalTroopCounts(ref besiegedTotalTroopCount, ref besiegerTotalTroopCount);
			int num = besiegedTotalTroopCount + besiegerTotalTroopCount;
			int defenderInitialSpawn = MathF.Min(besiegedTotalTroopCount, MathF.Ceiling((float)num * 0.1f));
			int attackerInitialSpawn = MathF.Min(besiegerTotalTroopCount, MathF.Ceiling((float)num * 0.1f));
			this.MissionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Defender, true);
			this.MissionAgentSpawnLogic.SetSpawnHorses(BattleSideEnum.Attacker, false);
			MissionSpawnSettings missionSpawnSettings = SallyOutMissionController.CreateSallyOutSpawnSettings(0.01f, 0.1f);
			this.MissionAgentSpawnLogic.InitWithSinglePhase(besiegedTotalTroopCount, besiegerTotalTroopCount, defenderInitialSpawn, attackerInitialSpawn, false, false, missionSpawnSettings);
			this.MissionAgentSpawnLogic.SetCustomReinforcementSpawnTimer(new SallyOutReinforcementSpawnTimer(1f, 90f, 15f, 5));
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0007B860 File Offset: 0x00079A60
		private WorldPosition? GetSallyOutFleePositionForAgent(Agent agent)
		{
			if (!agent.IsHuman)
			{
				return null;
			}
			Formation formation = agent.Formation;
			if (formation == null || formation.Team.Side == BattleSideEnum.Attacker)
			{
				return null;
			}
			bool flag = !agent.HasMount;
			bool isRangedCached = agent.IsRangedCached;
			FormationClass fClass;
			if (flag)
			{
				fClass = (isRangedCached ? FormationClass.Ranged : FormationClass.Infantry);
			}
			else
			{
				fClass = (isRangedCached ? FormationClass.HorseArcher : FormationClass.Cavalry);
			}
			return new WorldPosition?(Mission.Current.DeploymentPlan.GetFormationPlan(formation.Team.Side, fClass, DeploymentPlanType.Initial).CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache.GroundVec3));
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0007B8EC File Offset: 0x00079AEC
		private static MissionSpawnSettings CreateSallyOutSpawnSettings(float besiegedReinforcementPercentage, float besiegerReinforcementPercentage)
		{
			return new MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod.FreeAllocation, MissionSpawnSettings.ReinforcementTimingMethod.CustomTimer, MissionSpawnSettings.ReinforcementSpawnMethod.Fixed, 0f, 0f, 0f, 0f, 0, besiegedReinforcementPercentage, besiegerReinforcementPercentage, 1f, 0.75f);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0007B924 File Offset: 0x00079B24
		private void OnDefenderTeamTacticalDecision(in TacticalDecision decision)
		{
			TacticalDecision tacticalDecision = decision;
			if (tacticalDecision.DecisionCode == 31)
			{
				this._sallyOutNotificationsHandler.OnBesiegedSideFallsbackToKeep();
			}
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0007B950 File Offset: 0x00079B50
		private void DeactivateBesiegers()
		{
			foreach (Formation formation in base.Mission.AttackerTeam.FormationsIncludingSpecialAndEmpty)
			{
				formation.SetMovementOrder(MovementOrder.MovementOrderStop);
				formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
				formation.SetControlledByAI(false, false);
			}
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0007B9C4 File Offset: 0x00079BC4
		private void ActivateBesiegers()
		{
			Team attackerTeam = base.Mission.AttackerTeam;
			foreach (Formation formation in base.Mission.AttackerTeam.FormationsIncludingSpecialAndEmpty)
			{
				formation.SetControlledByAI(true, false);
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0007BA2C File Offset: 0x00079C2C
		public static MBReadOnlyList<SiegeWeapon> GetBesiegerSiegeEngines()
		{
			MBList<SiegeWeapon> mblist = new MBList<SiegeWeapon>();
			using (List<MissionObject>.Enumerator enumerator = Mission.Current.ActiveMissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SiegeWeapon siegeWeapon;
					if ((siegeWeapon = (enumerator.Current as SiegeWeapon)) != null && siegeWeapon.DestructionComponent != null && siegeWeapon.Side == BattleSideEnum.Attacker)
					{
						mblist.Add(siegeWeapon);
					}
				}
			}
			return mblist;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0007BAA4 File Offset: 0x00079CA4
		public static void DisableSiegeEngines()
		{
			using (List<MissionObject>.Enumerator enumerator = Mission.Current.ActiveMissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SiegeWeapon siegeWeapon;
					if ((siegeWeapon = (enumerator.Current as SiegeWeapon)) != null && siegeWeapon.DestructionComponent != null && !siegeWeapon.IsDeactivated)
					{
						siegeWeapon.Disable();
						siegeWeapon.Deactivate();
					}
				}
			}
		}

		// Token: 0x04000C9C RID: 3228
		private const float BesiegedTotalTroopRatio = 0.25f;

		// Token: 0x04000C9D RID: 3229
		private const float BesiegedInitialTroopRatio = 0.1f;

		// Token: 0x04000C9E RID: 3230
		private const float BesiegedReinforcementRatio = 0.01f;

		// Token: 0x04000C9F RID: 3231
		private const float BesiegerInitialTroopRatio = 0.1f;

		// Token: 0x04000CA0 RID: 3232
		private const float BesiegerReinforcementRatio = 0.1f;

		// Token: 0x04000CA1 RID: 3233
		private const float BesiegedInitialInterval = 1f;

		// Token: 0x04000CA2 RID: 3234
		private const float BesiegerInitialInterval = 90f;

		// Token: 0x04000CA3 RID: 3235
		private const float BesiegerIntervalChange = 15f;

		// Token: 0x04000CA4 RID: 3236
		private const int BesiegerIntervalChangeCount = 5;

		// Token: 0x04000CA5 RID: 3237
		private const float PlayerToGateSquaredDistanceThreshold = 25f;

		// Token: 0x04000CA6 RID: 3238
		private SallyOutMissionNotificationsHandler _sallyOutNotificationsHandler;

		// Token: 0x04000CA7 RID: 3239
		private List<CastleGate> _castleGates;

		// Token: 0x04000CA8 RID: 3240
		private BasicMissionTimer _besiegedDeploymentTimer;

		// Token: 0x04000CA9 RID: 3241
		private BasicMissionTimer _besiegerActivationTimer;

		// Token: 0x04000CAA RID: 3242
		private MBReadOnlyList<SiegeWeapon> _besiegerSiegeEngines;

		// Token: 0x04000CAB RID: 3243
		protected MissionAgentSpawnLogic MissionAgentSpawnLogic;
	}
}
