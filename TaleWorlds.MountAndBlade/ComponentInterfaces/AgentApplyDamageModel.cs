using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C4 RID: 964
	public abstract class AgentApplyDamageModel : GameModel
	{
		// Token: 0x06003346 RID: 13126
		public abstract float CalculateDamage(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float baseDamage);

		// Token: 0x06003347 RID: 13127
		public abstract void DecideMissileWeaponFlags(Agent attackerAgent, MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags);

		// Token: 0x06003348 RID: 13128
		public abstract void CalculateDefendedBlowStunMultipliers(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, WeaponComponentData attackerWeapon, WeaponComponentData defenderWeapon, out float attackerStunMultiplier, out float defenderStunMultiplier);

		// Token: 0x06003349 RID: 13129
		public abstract float CalculateStaggerThresholdDamage(Agent defenderAgent, in Blow blow);

		// Token: 0x0600334A RID: 13130
		public abstract float CalculateAlternativeAttackDamage(BasicCharacterObject attackerCharacter, WeaponComponentData weapon);

		// Token: 0x0600334B RID: 13131
		public abstract float CalculatePassiveAttackDamage(BasicCharacterObject attackerCharacter, in AttackCollisionData collisionData, float baseDamage);

		// Token: 0x0600334C RID: 13132
		public abstract MeleeCollisionReaction DecidePassiveAttackCollisionReaction(Agent attacker, Agent defender, bool isFatalHit);

		// Token: 0x0600334D RID: 13133
		public abstract float CalculateShieldDamage(in AttackInformation attackInformation, float baseDamage);

		// Token: 0x0600334E RID: 13134
		public abstract float GetDamageMultiplierForBodyPart(BoneBodyPartType bodyPart, DamageTypes type, bool isHuman, bool isMissile);

		// Token: 0x0600334F RID: 13135
		public abstract bool CanWeaponIgnoreFriendlyFireChecks(WeaponComponentData weapon);

		// Token: 0x06003350 RID: 13136
		public abstract bool CanWeaponDismount(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData);

		// Token: 0x06003351 RID: 13137
		public abstract bool CanWeaponKnockback(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData);

		// Token: 0x06003352 RID: 13138
		public abstract bool CanWeaponKnockDown(Agent attackerAgent, Agent victimAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData);

		// Token: 0x06003353 RID: 13139
		public abstract bool DecideCrushedThrough(Agent attackerAgent, Agent defenderAgent, float totalAttackEnergy, Agent.UsageDirection attackDirection, StrikeType strikeType, WeaponComponentData defendItem, bool isPassiveUsageHit);

		// Token: 0x06003354 RID: 13140
		public abstract bool DecideAgentShrugOffBlow(Agent victimAgent, AttackCollisionData collisionData, in Blow blow);

		// Token: 0x06003355 RID: 13141
		public abstract bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow);

		// Token: 0x06003356 RID: 13142
		public abstract bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow);

		// Token: 0x06003357 RID: 13143
		public abstract bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow);

		// Token: 0x06003358 RID: 13144
		public abstract bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow);

		// Token: 0x06003359 RID: 13145
		public abstract float GetDismountPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData);

		// Token: 0x0600335A RID: 13146
		public abstract float GetKnockBackPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData);

		// Token: 0x0600335B RID: 13147
		public abstract float GetKnockDownPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData);

		// Token: 0x0600335C RID: 13148
		public abstract float GetHorseChargePenetration();
	}
}
