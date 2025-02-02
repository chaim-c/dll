using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000288 RID: 648
	public class SiegeDeploymentMissionController : DeploymentMissionController
	{
		// Token: 0x060021F6 RID: 8694 RVA: 0x0007C0DB File Offset: 0x0007A2DB
		public SiegeDeploymentMissionController(bool isPlayerAttacker) : base(isPlayerAttacker)
		{
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0007C0E4 File Offset: 0x0007A2E4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._siegeDeploymentHandler = base.Mission.GetMissionBehavior<SiegeDeploymentHandler>();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0007C0FD File Offset: 0x0007A2FD
		public override void AfterStart()
		{
			base.Mission.GetMissionBehavior<DeploymentHandler>().InitializeDeploymentPoints();
			base.AfterStart();
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0007C118 File Offset: 0x0007A318
		protected override void SetupTeamsOfSide(BattleSideEnum side)
		{
			Team team = (side == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam : base.Mission.DefenderTeam;
			if (team == base.Mission.PlayerTeam)
			{
				this._siegeDeploymentHandler.RemoveUnavailableDeploymentPoints(side);
				this._siegeDeploymentHandler.UnHideDeploymentPoints(side);
				this._siegeDeploymentHandler.DeployAllSiegeWeaponsOfPlayer();
			}
			else
			{
				this._siegeDeploymentHandler.DeployAllSiegeWeaponsOfAi();
			}
			this.MissionAgentSpawnLogic.SetSpawnTroops(side, true, true);
			foreach (GameEntity gameEntity in base.Mission.GetActiveEntitiesWithScriptComponentOfType<SiegeWeapon>())
			{
				SiegeWeapon siegeWeapon = gameEntity.GetScriptComponents<SiegeWeapon>().FirstOrDefault<SiegeWeapon>();
				if (siegeWeapon != null && siegeWeapon.GetSide() == side)
				{
					siegeWeapon.TickAuxForInit();
				}
			}
			base.SetupTeamsOfSideAux(side);
			if (team == base.Mission.PlayerTeam)
			{
				foreach (Formation formation in team.FormationsIncludingEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						formation.SetControlledByAI(true, false);
					}
				}
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0007C24C File Offset: 0x0007A44C
		public override void OnBeforeDeploymentFinished()
		{
			BattleSideEnum side = base.Mission.PlayerTeam.Side;
			this._siegeDeploymentHandler.RemoveDeploymentPoints(side);
			foreach (SiegeLadder siegeLadder in (from sl in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeLadder>()
			where !sl.GameEntity.IsVisibleIncludeParents()
			select sl).ToList<SiegeLadder>())
			{
				siegeLadder.SetDisabledSynched();
			}
			base.OnSideDeploymentFinished(side);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0007C2F4 File Offset: 0x0007A4F4
		public override void OnAfterDeploymentFinished()
		{
			base.Mission.RemoveMissionBehavior(this._siegeDeploymentHandler);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0007C308 File Offset: 0x0007A508
		public List<ItemObject> GetSiegeMissiles()
		{
			List<ItemObject> list = new List<ItemObject>();
			ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>("grapeshot_fire_projectile");
			list.Add(@object);
			foreach (GameEntity gameEntity in Mission.Current.GetActiveEntitiesWithScriptComponentOfType<RangedSiegeWeapon>())
			{
				RangedSiegeWeapon firstScriptOfType = gameEntity.GetFirstScriptOfType<RangedSiegeWeapon>();
				if (!string.IsNullOrEmpty(firstScriptOfType.MissileItemID))
				{
					ItemObject object2 = MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MissileItemID);
					if (!list.Contains(object2))
					{
						list.Add(object2);
					}
				}
			}
			foreach (GameEntity gameEntity2 in Mission.Current.GetActiveEntitiesWithScriptComponentOfType<StonePile>())
			{
				StonePile firstScriptOfType2 = gameEntity2.GetFirstScriptOfType<StonePile>();
				if (!string.IsNullOrEmpty(firstScriptOfType2.GivenItemID))
				{
					ItemObject object3 = MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType2.GivenItemID);
					if (!list.Contains(object3))
					{
						list.Add(object3);
					}
				}
			}
			return list;
		}

		// Token: 0x04000CBE RID: 3262
		private SiegeDeploymentHandler _siegeDeploymentHandler;
	}
}
