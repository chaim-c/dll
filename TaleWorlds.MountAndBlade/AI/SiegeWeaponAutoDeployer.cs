using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions;

namespace TaleWorlds.MountAndBlade.AI
{
	// Token: 0x020003D1 RID: 977
	public class SiegeWeaponAutoDeployer
	{
		// Token: 0x060033A1 RID: 13217 RVA: 0x000D5CCD File Offset: 0x000D3ECD
		public SiegeWeaponAutoDeployer(List<DeploymentPoint> deploymentPoints, IMissionSiegeWeaponsController weaponsController)
		{
			this.deploymentPoints = deploymentPoints;
			this.siegeWeaponsController = weaponsController;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000D5CE3 File Offset: 0x000D3EE3
		public void DeployAll(BattleSideEnum side)
		{
			if (side == BattleSideEnum.Attacker)
			{
				this.DeployAllForAttackers();
				return;
			}
			if (side == BattleSideEnum.Defender)
			{
				this.DeployAllForDefenders();
				return;
			}
			Debug.FailedAssert("Invalid side", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponAutoDeployer.cs", "DeployAll", 32);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000D5D10 File Offset: 0x000D3F10
		private bool DeployWeaponFrom(DeploymentPoint dp)
		{
			IEnumerable<Type> source = from t in dp.DeployableWeaponTypes
			where this.deploymentPoints.Count((DeploymentPoint dep) => dep.IsDeployed && MissionSiegeWeaponsController.GetWeaponType(dep.DeployedWeapon) == t) < this.siegeWeaponsController.GetMaxDeployableWeaponCount(t)
			select t;
			if (!source.IsEmpty<Type>())
			{
				Type t2 = source.MaxBy((Type t) => this.GetWeaponValue(t));
				dp.Deploy(t2);
				return true;
			}
			return false;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000D5D5C File Offset: 0x000D3F5C
		private void DeployAllForAttackers()
		{
			List<DeploymentPoint> list = (from dp in this.deploymentPoints
			where !dp.IsDisabled && !dp.IsDeployed
			select dp).ToList<DeploymentPoint>();
			list.Shuffle<DeploymentPoint>();
			int num = this.deploymentPoints.Count((DeploymentPoint dp) => dp.GetDeploymentPointType() == DeploymentPoint.DeploymentPointType.Breach);
			bool flag = Mission.Current.AttackerTeam != Mission.Current.PlayerTeam && num >= 2;
			foreach (DeploymentPoint deploymentPoint in list)
			{
				if (!flag || deploymentPoint.GetDeploymentPointType() == DeploymentPoint.DeploymentPointType.Ranged)
				{
					this.DeployWeaponFrom(deploymentPoint);
				}
			}
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000D5E38 File Offset: 0x000D4038
		private void DeployAllForDefenders()
		{
			Mission mission = Mission.Current;
			Scene scene = mission.Scene;
			List<ICastleKeyPosition> castleKeyPositions = (from amo in mission.ActiveMissionObjects
			select amo.GameEntity into e
			select e.GetFirstScriptOfType<UsableMachine>() into um
			where um is ICastleKeyPosition
			select um).Cast<ICastleKeyPosition>().Where(delegate(ICastleKeyPosition x)
			{
				IPrimarySiegeWeapon attackerSiegeWeapon = x.AttackerSiegeWeapon;
				return attackerSiegeWeapon == null || attackerSiegeWeapon.WeaponSide != FormationAI.BehaviorSide.BehaviorSideNotSet;
			}).ToList<ICastleKeyPosition>();
			List<DeploymentPoint> list = (from dp in this.deploymentPoints
			where !dp.IsDeployed
			select dp).ToList<DeploymentPoint>();
			while (!list.IsEmpty<DeploymentPoint>())
			{
				Threat maxThreat = RangedSiegeWeaponAi.ThreatSeeker.GetMaxThreat(castleKeyPositions);
				Vec3 mostDangerousThreatPosition = maxThreat.Position;
				DeploymentPoint deploymentPoint = list.MinBy((DeploymentPoint dp) => dp.GameEntity.GlobalPosition.DistanceSquared(mostDangerousThreatPosition));
				if (this.DeployWeaponFrom(deploymentPoint))
				{
					maxThreat.ThreatValue *= 0.5f;
				}
				list.Remove(deploymentPoint);
			}
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000D5F80 File Offset: 0x000D4180
		protected virtual float GetWeaponValue(Type weaponType)
		{
			if (weaponType == typeof(BatteringRam) || weaponType == typeof(SiegeTower) || weaponType == typeof(SiegeLadder))
			{
				return 0.9f + MBRandom.RandomFloat * 0.2f;
			}
			if (typeof(RangedSiegeWeapon).IsAssignableFrom(weaponType))
			{
				return 0.7f + MBRandom.RandomFloat * 0.2f;
			}
			return 1f;
		}

		// Token: 0x0400165F RID: 5727
		private IMissionSiegeWeaponsController siegeWeaponsController;

		// Token: 0x04001660 RID: 5728
		private List<DeploymentPoint> deploymentPoints;
	}
}
