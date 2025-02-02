using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Missions
{
	// Token: 0x020003BF RID: 959
	public class MissionSiegeWeaponsController : IMissionSiegeWeaponsController
	{
		// Token: 0x06003314 RID: 13076 RVA: 0x000D49C2 File Offset: 0x000D2BC2
		public MissionSiegeWeaponsController(BattleSideEnum side, List<MissionSiegeWeapon> weapons)
		{
			this._side = side;
			this._weapons = weapons;
			this._undeployedWeapons = new List<MissionSiegeWeapon>(this._weapons);
			this._deployedWeapons = new Dictionary<DestructableComponent, MissionSiegeWeapon>();
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000D49F4 File Offset: 0x000D2BF4
		public int GetMaxDeployableWeaponCount(Type t)
		{
			int num = 0;
			using (List<MissionSiegeWeapon>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (MissionSiegeWeaponsController.GetSiegeWeaponBaseType(enumerator.Current.Type) == t)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000D4A58 File Offset: 0x000D2C58
		public IEnumerable<IMissionSiegeWeapon> GetSiegeWeapons()
		{
			return this._weapons.Cast<IMissionSiegeWeapon>();
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000D4A68 File Offset: 0x000D2C68
		public void OnWeaponDeployed(SiegeWeapon missionWeapon)
		{
			SiegeEngineType missionWeaponType = missionWeapon.GetSiegeEngineType();
			int index = this._undeployedWeapons.FindIndex((MissionSiegeWeapon uw) => uw.Type == missionWeaponType);
			MissionSiegeWeapon missionSiegeWeapon = this._undeployedWeapons[index];
			DestructableComponent destructionComponent = missionWeapon.DestructionComponent;
			destructionComponent.MaxHitPoint = missionSiegeWeapon.MaxHealth;
			destructionComponent.HitPoint = missionSiegeWeapon.InitialHealth;
			destructionComponent.OnHitTaken += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnWeaponHit);
			destructionComponent.OnDestroyed += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnWeaponDestroyed);
			this._undeployedWeapons.RemoveAt(index);
			this._deployedWeapons.Add(destructionComponent, missionSiegeWeapon);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000D4B08 File Offset: 0x000D2D08
		public void OnWeaponUndeployed(SiegeWeapon missionWeapon)
		{
			DestructableComponent destructionComponent = missionWeapon.DestructionComponent;
			MissionSiegeWeapon item;
			this._deployedWeapons.TryGetValue(destructionComponent, out item);
			SiegeEngineType siegeEngineType = missionWeapon.GetSiegeEngineType();
			destructionComponent.MaxHitPoint = (float)siegeEngineType.BaseHitPoints;
			destructionComponent.HitPoint = destructionComponent.MaxHitPoint;
			destructionComponent.OnHitTaken -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnWeaponHit);
			destructionComponent.OnDestroyed -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnWeaponDestroyed);
			this._deployedWeapons.Remove(destructionComponent);
			this._undeployedWeapons.Add(item);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000D4B88 File Offset: 0x000D2D88
		private void OnWeaponHit(DestructableComponent target, Agent attackerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			MissionSiegeWeapon missionSiegeWeapon;
			if (target.BattleSide == this._side && this._deployedWeapons.TryGetValue(target, out missionSiegeWeapon))
			{
				float health = Math.Max(0f, missionSiegeWeapon.Health - (float)inflictedDamage);
				missionSiegeWeapon.SetHealth(health);
			}
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000D4BD0 File Offset: 0x000D2DD0
		private void OnWeaponDestroyed(DestructableComponent target, Agent attackerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			MissionSiegeWeapon missionSiegeWeapon;
			if (target.BattleSide == this._side && this._deployedWeapons.TryGetValue(target, out missionSiegeWeapon))
			{
				missionSiegeWeapon.SetHealth(0f);
				target.OnHitTaken -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnWeaponHit);
				target.OnDestroyed -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnWeaponDestroyed);
				this._deployedWeapons.Remove(target);
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000D4C38 File Offset: 0x000D2E38
		public static Type GetWeaponType(ScriptComponentBehavior weapon)
		{
			if (weapon is UsableGameObjectGroup)
			{
				return weapon.GameEntity.GetChildren().SelectMany((GameEntity c) => c.GetScriptComponents()).First((ScriptComponentBehavior s) => s is IFocusable).GetType();
			}
			return weapon.GetType();
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000D4CAC File Offset: 0x000D2EAC
		private static Type GetSiegeWeaponBaseType(SiegeEngineType siegeWeaponType)
		{
			if (siegeWeaponType == DefaultSiegeEngineTypes.Ladder)
			{
				return typeof(SiegeLadder);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Ballista)
			{
				return typeof(Ballista);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.FireBallista)
			{
				return typeof(FireBallista);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Ram)
			{
				return typeof(BatteringRam);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.SiegeTower)
			{
				return typeof(SiegeTower);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Onager || siegeWeaponType == DefaultSiegeEngineTypes.Catapult)
			{
				return typeof(Mangonel);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.FireOnager || siegeWeaponType == DefaultSiegeEngineTypes.FireCatapult)
			{
				return typeof(FireMangonel);
			}
			if (siegeWeaponType == DefaultSiegeEngineTypes.Trebuchet)
			{
				return typeof(Trebuchet);
			}
			return null;
		}

		// Token: 0x0400162C RID: 5676
		private readonly List<MissionSiegeWeapon> _weapons;

		// Token: 0x0400162D RID: 5677
		private readonly List<MissionSiegeWeapon> _undeployedWeapons;

		// Token: 0x0400162E RID: 5678
		private readonly Dictionary<DestructableComponent, MissionSiegeWeapon> _deployedWeapons;

		// Token: 0x0400162F RID: 5679
		private BattleSideEnum _side;
	}
}
