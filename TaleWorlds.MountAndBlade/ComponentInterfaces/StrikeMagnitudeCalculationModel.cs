using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C3 RID: 963
	public abstract class StrikeMagnitudeCalculationModel : GameModel
	{
		// Token: 0x0600333E RID: 13118
		public abstract float CalculateStrikeMagnitudeForMissile(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float missileSpeed);

		// Token: 0x0600333F RID: 13119
		public abstract float CalculateStrikeMagnitudeForSwing(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float swingSpeed, float impactPointAsPercent, float extraLinearSpeed);

		// Token: 0x06003340 RID: 13120
		public abstract float CalculateStrikeMagnitudeForThrust(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float thrustSpeed, float extraLinearSpeed, bool isThrown = false);

		// Token: 0x06003341 RID: 13121
		public abstract float ComputeRawDamage(DamageTypes damageType, float magnitude, float armorEffectiveness, float absorbedDamageRatio);

		// Token: 0x06003342 RID: 13122
		public abstract float GetBluntDamageFactorByDamageType(DamageTypes damageType);

		// Token: 0x06003343 RID: 13123
		public abstract float CalculateHorseArcheryFactor(BasicCharacterObject characterObject);

		// Token: 0x06003344 RID: 13124 RVA: 0x000D578E File Offset: 0x000D398E
		public virtual float CalculateAdjustedArmorForBlow(float baseArmor, BasicCharacterObject attackerCharacter, BasicCharacterObject attackerCaptainCharacter, BasicCharacterObject victimCharacter, BasicCharacterObject victimCaptainCharacter, WeaponComponentData weaponComponent)
		{
			return baseArmor;
		}
	}
}
