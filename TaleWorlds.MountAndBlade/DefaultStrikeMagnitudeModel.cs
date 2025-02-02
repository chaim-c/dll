using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FA RID: 506
	public class DefaultStrikeMagnitudeModel : StrikeMagnitudeCalculationModel
	{
		// Token: 0x06001C19 RID: 7193 RVA: 0x000618F4 File Offset: 0x0005FAF4
		public override float CalculateStrikeMagnitudeForMissile(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float missileSpeed)
		{
			AttackCollisionData attackCollisionData = collisionData;
			float missileTotalDamage = attackCollisionData.MissileTotalDamage;
			attackCollisionData = collisionData;
			float missileStartingBaseSpeed = attackCollisionData.MissileStartingBaseSpeed;
			float num = missileSpeed / missileStartingBaseSpeed;
			return num * num * missileTotalDamage;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00061928 File Offset: 0x0005FB28
		public override float CalculateStrikeMagnitudeForSwing(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float swingSpeed, float impactPointAsPercent, float extraLinearSpeed)
		{
			MissionWeapon missionWeapon = weapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			missionWeapon = weapon;
			return CombatStatCalculator.CalculateStrikeMagnitudeForSwing(swingSpeed, impactPointAsPercent, missionWeapon.Item.Weight, currentUsageItem.GetRealWeaponLength(), currentUsageItem.Inertia, currentUsageItem.CenterOfMass, extraLinearSpeed);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00061974 File Offset: 0x0005FB74
		public override float CalculateStrikeMagnitudeForThrust(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float thrustWeaponSpeed, float extraLinearSpeed, bool isThrown = false)
		{
			MissionWeapon missionWeapon = weapon;
			return CombatStatCalculator.CalculateStrikeMagnitudeForThrust(thrustWeaponSpeed, missionWeapon.Item.Weight, extraLinearSpeed, isThrown);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000619A0 File Offset: 0x0005FBA0
		public override float ComputeRawDamage(DamageTypes damageType, float magnitude, float armorEffectiveness, float absorbedDamageRatio)
		{
			float bluntDamageFactorByDamageType = this.GetBluntDamageFactorByDamageType(damageType);
			float num = 50f / (50f + armorEffectiveness);
			float num2 = magnitude * num;
			float num3 = bluntDamageFactorByDamageType * num2;
			float num4;
			switch (damageType)
			{
			case DamageTypes.Cut:
				num4 = MathF.Max(0f, num2 - armorEffectiveness * 0.5f);
				break;
			case DamageTypes.Pierce:
				num4 = MathF.Max(0f, num2 - armorEffectiveness * 0.33f);
				break;
			case DamageTypes.Blunt:
				num4 = MathF.Max(0f, num2 - armorEffectiveness * 0.2f);
				break;
			default:
				Debug.FailedAssert("Given damage type is invalid.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\ComponentInterfaces\\DefaultStrikeMagnitudeModel.cs", "ComputeRawDamage", 59);
				return 0f;
			}
			num3 += (1f - bluntDamageFactorByDamageType) * num4;
			return num3 * absorbedDamageRatio;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00061A54 File Offset: 0x0005FC54
		public override float GetBluntDamageFactorByDamageType(DamageTypes damageType)
		{
			float result = 0f;
			switch (damageType)
			{
			case DamageTypes.Cut:
				result = 0.1f;
				break;
			case DamageTypes.Pierce:
				result = 0.25f;
				break;
			case DamageTypes.Blunt:
				result = 0.6f;
				break;
			}
			return result;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00061A92 File Offset: 0x0005FC92
		public override float CalculateHorseArcheryFactor(BasicCharacterObject characterObject)
		{
			return 100f;
		}
	}
}
