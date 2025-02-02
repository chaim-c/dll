using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200026F RID: 623
	public class BattleReinforcementsSpawnController : MissionLogic
	{
		// Token: 0x060020E7 RID: 8423 RVA: 0x00076244 File Offset: 0x00074444
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentSpawnLogic = base.Mission.GetMissionBehavior<IMissionAgentSpawnLogic>();
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00076260 File Offset: 0x00074460
		public override void AfterStart()
		{
			foreach (Team team in base.Mission.Teams)
			{
				foreach (Formation formation in team.FormationsIncludingEmpty)
				{
					formation.OnBeforeMovementOrderApplied += this.OnBeforeFormationMovementOrderApplied;
				}
			}
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000762FC File Offset: 0x000744FC
		public override void OnMissionTick(float dt)
		{
			for (int i = 0; i < 2; i++)
			{
				if (this._sideRequiresUpdate[i])
				{
					this.UpdateSide((BattleSideEnum)i);
					this._sideRequiresUpdate[i] = false;
				}
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00076330 File Offset: 0x00074530
		protected override void OnEndMission()
		{
			foreach (Team team in base.Mission.Teams)
			{
				foreach (Formation formation in team.FormationsIncludingEmpty)
				{
					formation.OnBeforeMovementOrderApplied -= this.OnBeforeFormationMovementOrderApplied;
				}
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000763CC File Offset: 0x000745CC
		private void UpdateSide(BattleSideEnum side)
		{
			if (this.IsBattleSideRetreating(side))
			{
				if (!this._sideReinforcementSuspended[(int)side] && this._missionAgentSpawnLogic.IsSideSpawnEnabled(side))
				{
					this._missionAgentSpawnLogic.StopSpawner(side);
					this._sideReinforcementSuspended[(int)side] = true;
					return;
				}
			}
			else if (this._sideReinforcementSuspended[(int)side])
			{
				this._missionAgentSpawnLogic.StartSpawner(side);
				this._sideReinforcementSuspended[(int)side] = false;
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00076434 File Offset: 0x00074634
		private bool IsBattleSideRetreating(BattleSideEnum side)
		{
			bool result = true;
			foreach (Team team in base.Mission.Teams)
			{
				if (team.Side == side)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						if (formation.CountOfUnits > 0 && formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat)
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000764EC File Offset: 0x000746EC
		private unsafe void OnBeforeFormationMovementOrderApplied(Formation formation, MovementOrder.MovementOrderEnum orderEnum)
		{
			if (formation.GetReadonlyMovementOrderReference()->OrderEnum == MovementOrder.MovementOrderEnum.Retreat || orderEnum == MovementOrder.MovementOrderEnum.Retreat)
			{
				int side = (int)formation.Team.Side;
				this._sideRequiresUpdate[side] = true;
			}
		}

		// Token: 0x04000C34 RID: 3124
		private IMissionAgentSpawnLogic _missionAgentSpawnLogic;

		// Token: 0x04000C35 RID: 3125
		private bool[] _sideReinforcementSuspended = new bool[2];

		// Token: 0x04000C36 RID: 3126
		private bool[] _sideRequiresUpdate = new bool[2];
	}
}
