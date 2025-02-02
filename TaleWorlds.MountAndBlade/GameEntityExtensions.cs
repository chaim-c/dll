using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021B RID: 539
	public static class GameEntityExtensions
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x0006C040 File Offset: 0x0006A240
		public static GameEntity Instantiate(Scene scene, MissionWeapon weapon, bool showHolsterWithWeapon, bool needBatchedVersion)
		{
			WeaponData weaponData = weapon.GetWeaponData(needBatchedVersion);
			WeaponStatsData[] weaponStatsData = weapon.GetWeaponStatsData();
			WeaponData ammoWeaponData = weapon.GetAmmoWeaponData(needBatchedVersion);
			WeaponStatsData[] ammoWeaponStatsData = weapon.GetAmmoWeaponStatsData();
			GameEntity result = MBAPI.IMBGameEntityExtensions.CreateFromWeapon(scene.Pointer, weaponData, weaponStatsData, weaponStatsData.Length, ammoWeaponData, ammoWeaponStatsData, ammoWeaponStatsData.Length, showHolsterWithWeapon);
			weaponData.DeinitializeManagedPointers();
			return result;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0006C093 File Offset: 0x0006A293
		public static void CreateSimpleSkeleton(this GameEntity gameEntity, string skeletonName)
		{
			gameEntity.Skeleton = MBAPI.IMBSkeletonExtensions.CreateSimpleSkeleton(skeletonName);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0006C0A8 File Offset: 0x0006A2A8
		public static void CreateAgentSkeleton(this GameEntity gameEntity, string skeletonName, bool isHumanoid, MBActionSet actionSet, string monsterUsageSetName, Monster monster)
		{
			AnimationSystemData animationSystemData = monster.FillAnimationSystemData(actionSet, 1f, false);
			gameEntity.Skeleton = MBAPI.IMBSkeletonExtensions.CreateAgentSkeleton(skeletonName, isHumanoid, actionSet.Index, monsterUsageSetName, ref animationSystemData);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0006C0E0 File Offset: 0x0006A2E0
		public static void CreateSkeletonWithActionSet(this GameEntity gameEntity, ref AnimationSystemData animationSystemData)
		{
			gameEntity.Skeleton = MBSkeletonExtensions.CreateWithActionSet(ref animationSystemData);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0006C0EE File Offset: 0x0006A2EE
		public static void FadeOut(this GameEntity gameEntity, float interval, bool isRemovingFromScene)
		{
			MBAPI.IMBGameEntityExtensions.FadeOut(gameEntity.Pointer, interval, isRemovingFromScene);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0006C102 File Offset: 0x0006A302
		public static void FadeIn(this GameEntity gameEntity, bool resetAlpha = true)
		{
			MBAPI.IMBGameEntityExtensions.FadeIn(gameEntity.Pointer, resetAlpha);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0006C115 File Offset: 0x0006A315
		public static void HideIfNotFadingOut(this GameEntity gameEntity)
		{
			MBAPI.IMBGameEntityExtensions.HideIfNotFadingOut(gameEntity.Pointer);
		}
	}
}
