using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.AI;

namespace TaleWorlds.MountAndBlade.Missions.Handlers
{
	// Token: 0x020003C1 RID: 961
	public class SiegeDeploymentHandler : BattleDeploymentHandler
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000D4EC6 File Offset: 0x000D30C6
		// (set) Token: 0x06003326 RID: 13094 RVA: 0x000D4ECE File Offset: 0x000D30CE
		public IEnumerable<DeploymentPoint> PlayerDeploymentPoints { get; private set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000D4ED7 File Offset: 0x000D30D7
		// (set) Token: 0x06003328 RID: 13096 RVA: 0x000D4EDF File Offset: 0x000D30DF
		public IEnumerable<DeploymentPoint> AllDeploymentPoints { get; private set; }

		// Token: 0x06003329 RID: 13097 RVA: 0x000D4EE8 File Offset: 0x000D30E8
		public SiegeDeploymentHandler(bool isPlayerAttacker) : base(isPlayerAttacker)
		{
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000D4EF4 File Offset: 0x000D30F4
		public override void OnBehaviorInitialize()
		{
			MissionSiegeEnginesLogic missionBehavior = base.Mission.GetMissionBehavior<MissionSiegeEnginesLogic>();
			this._defenderSiegeWeaponsController = missionBehavior.GetSiegeWeaponsController(BattleSideEnum.Defender);
			this._attackerSiegeWeaponsController = missionBehavior.GetSiegeWeaponsController(BattleSideEnum.Attacker);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000D4F28 File Offset: 0x000D3128
		public override void AfterStart()
		{
			base.AfterStart();
			this.AllDeploymentPoints = Mission.Current.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			this.PlayerDeploymentPoints = from dp in this.AllDeploymentPoints
			where dp.Side == base.team.Side
			select dp;
			foreach (DeploymentPoint deploymentPoint in this.AllDeploymentPoints)
			{
				deploymentPoint.OnDeploymentStateChanged += this.OnDeploymentStateChange;
			}
			base.Mission.IsFormationUnitPositionAvailable_AdditionalCondition += this.Mission_IsFormationUnitPositionAvailable_AdditionalCondition;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000D4FD0 File Offset: 0x000D31D0
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			base.Mission.IsFormationUnitPositionAvailable_AdditionalCondition -= this.Mission_IsFormationUnitPositionAvailable_AdditionalCondition;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000D4FF0 File Offset: 0x000D31F0
		public override void FinishDeployment()
		{
			foreach (DeploymentPoint deploymentPoint in this.AllDeploymentPoints)
			{
				deploymentPoint.OnDeploymentStateChanged -= this.OnDeploymentStateChange;
			}
			base.FinishDeployment();
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000D504C File Offset: 0x000D324C
		public void DeployAllSiegeWeaponsOfPlayer()
		{
			BattleSideEnum side = this.isPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender;
			new SiegeWeaponAutoDeployer((from dp in base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>()
			where dp.Side == side
			select dp).ToList<DeploymentPoint>(), this.GetWeaponsControllerOfSide(side)).DeployAll(side);
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000D50B3 File Offset: 0x000D32B3
		public int GetMaxDeployableWeaponCountOfPlayer(Type weapon)
		{
			return this.GetWeaponsControllerOfSide(this.isPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender).GetMaxDeployableWeaponCount(weapon);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000D50D0 File Offset: 0x000D32D0
		public void DeployAllSiegeWeaponsOfAi()
		{
			BattleSideEnum side = this.isPlayerAttacker ? BattleSideEnum.Defender : BattleSideEnum.Attacker;
			new SiegeWeaponAutoDeployer((from dp in base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>()
			where dp.Side == side
			select dp).ToList<DeploymentPoint>(), this.GetWeaponsControllerOfSide(side)).DeployAll(side);
			this.RemoveDeploymentPoints(side);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000D5144 File Offset: 0x000D3344
		public void RemoveDeploymentPoints(BattleSideEnum side)
		{
			IEnumerable<DeploymentPoint> source = base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			Func<DeploymentPoint, bool> <>9__0;
			Func<DeploymentPoint, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((DeploymentPoint dp) => dp.Side == side));
			}
			foreach (DeploymentPoint deploymentPoint in source.Where(predicate).ToArray<DeploymentPoint>())
			{
				foreach (SynchedMissionObject synchedMissionObject in deploymentPoint.DeployableWeapons.ToArray<SynchedMissionObject>())
				{
					if (deploymentPoint.DeployedWeapon == null || !synchedMissionObject.GameEntity.IsVisibleIncludeParents())
					{
						SiegeWeapon siegeWeapon = synchedMissionObject as SiegeWeapon;
						if (siegeWeapon != null)
						{
							siegeWeapon.SetDisabledSynched();
						}
					}
				}
				deploymentPoint.SetDisabledSynched();
			}
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000D5208 File Offset: 0x000D3408
		public void RemoveUnavailableDeploymentPoints(BattleSideEnum side)
		{
			IMissionSiegeWeaponsController weapons = (side == BattleSideEnum.Defender) ? this._defenderSiegeWeaponsController : this._attackerSiegeWeaponsController;
			IEnumerable<DeploymentPoint> source = base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			Func<DeploymentPoint, bool> <>9__0;
			Func<DeploymentPoint, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((DeploymentPoint dp) => dp.Side == side));
			}
			Func<Type, bool> <>9__1;
			foreach (DeploymentPoint deploymentPoint in source.Where(predicate).ToArray<DeploymentPoint>())
			{
				IEnumerable<Type> deployableWeaponTypes = deploymentPoint.DeployableWeaponTypes;
				Func<Type, bool> predicate2;
				if ((predicate2 = <>9__1) == null)
				{
					predicate2 = (<>9__1 = ((Type wt) => weapons.GetMaxDeployableWeaponCount(wt) > 0));
				}
				if (!deployableWeaponTypes.Any(predicate2))
				{
					foreach (SiegeWeapon siegeWeapon in from sw in deploymentPoint.DeployableWeapons
					select sw as SiegeWeapon)
					{
						siegeWeapon.SetDisabledSynched();
					}
					deploymentPoint.SetDisabledSynched();
				}
			}
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000D5330 File Offset: 0x000D3530
		public void UnHideDeploymentPoints(BattleSideEnum side)
		{
			IEnumerable<DeploymentPoint> source = base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			Func<DeploymentPoint, bool> <>9__0;
			Func<DeploymentPoint, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((DeploymentPoint dp) => !dp.IsDisabled && dp.Side == side));
			}
			foreach (DeploymentPoint deploymentPoint in source.Where(predicate))
			{
				deploymentPoint.Show();
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000D53B8 File Offset: 0x000D35B8
		public int GetDeployableWeaponCountOfPlayer(Type weapon)
		{
			return this.GetWeaponsControllerOfSide(this.isPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender).GetMaxDeployableWeaponCount(weapon) - this.PlayerDeploymentPoints.Count((DeploymentPoint dp) => dp.IsDeployed && MissionSiegeWeaponsController.GetWeaponType(dp.DeployedWeapon) == weapon);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000D5408 File Offset: 0x000D3608
		protected bool Mission_IsFormationUnitPositionAvailable_AdditionalCondition(WorldPosition position, Team team)
		{
			if (team != null && team.Side == BattleSideEnum.Defender)
			{
				Scene scene = base.Mission.Scene;
				Vec3 globalPosition = scene.FindEntityWithTag("defender_infantry").GlobalPosition;
				WorldPosition position2 = new WorldPosition(scene, UIntPtr.Zero, globalPosition, false);
				return scene.DoesPathExistBetweenPositions(position2, position);
			}
			return true;
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000D5458 File Offset: 0x000D3658
		private void OnDeploymentStateChange(DeploymentPoint deploymentPoint, SynchedMissionObject targetObject)
		{
			if (!deploymentPoint.IsDeployed && base.team.DetachmentManager.ContainsDetachment(deploymentPoint.DisbandedWeapon as IDetachment))
			{
				base.team.DetachmentManager.DestroyDetachment(deploymentPoint.DisbandedWeapon as IDetachment);
			}
			SiegeWeapon missionWeapon;
			if ((missionWeapon = (targetObject as SiegeWeapon)) != null)
			{
				IMissionSiegeWeaponsController weaponsControllerOfSide = this.GetWeaponsControllerOfSide(deploymentPoint.Side);
				if (deploymentPoint.IsDeployed)
				{
					weaponsControllerOfSide.OnWeaponDeployed(missionWeapon);
					return;
				}
				weaponsControllerOfSide.OnWeaponUndeployed(missionWeapon);
			}
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000D54D3 File Offset: 0x000D36D3
		private IMissionSiegeWeaponsController GetWeaponsControllerOfSide(BattleSideEnum side)
		{
			if (side != BattleSideEnum.Defender)
			{
				return this._attackerSiegeWeaponsController;
			}
			return this._defenderSiegeWeaponsController;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000D54E8 File Offset: 0x000D36E8
		[Conditional("DEBUG")]
		private void AssertSiegeWeapons(IEnumerable<DeploymentPoint> allDeploymentPoints)
		{
			HashSet<SynchedMissionObject> hashSet = new HashSet<SynchedMissionObject>();
			foreach (SynchedMissionObject item in allDeploymentPoints.SelectMany((DeploymentPoint amo) => amo.DeployableWeapons))
			{
				if (!hashSet.Add(item))
				{
					break;
				}
			}
		}

		// Token: 0x04001632 RID: 5682
		private IMissionSiegeWeaponsController _defenderSiegeWeaponsController;

		// Token: 0x04001633 RID: 5683
		private IMissionSiegeWeaponsController _attackerSiegeWeaponsController;
	}
}
