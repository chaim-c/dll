using System;
using MBHelpers;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EE RID: 494
	public class CustomAgentApplyDamageModel : AgentApplyDamageModel
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x0005F650 File Offset: 0x0005D850
		public override float CalculateDamage(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float baseDamage)
		{
			bool flag = (attackInformation.IsAttackerAgentMount ? attackInformation.AttackerRiderAgentCharacter : attackInformation.AttackerAgentCharacter) != null;
			Formation attackerFormation = attackInformation.AttackerFormation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(attackerFormation);
			BasicCharacterObject basicCharacterObject = attackInformation.IsVictimAgentMount ? attackInformation.VictimRiderAgentCharacter : attackInformation.VictimAgentCharacter;
			Formation victimFormation = attackInformation.VictimFormation;
			BannerComponent activeBanner2 = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			FactoredNumber factoredNumber = new FactoredNumber(baseDamage);
			MissionWeapon missionWeapon = weapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			if (flag)
			{
				if (currentUsageItem != null)
				{
					if (currentUsageItem.IsMeleeWeapon)
					{
						if (activeBanner != null)
						{
							BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMeleeDamage, activeBanner, ref factoredNumber);
							if (attackInformation.DoesVictimHaveMountAgent)
							{
								BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMeleeDamageAgainstMountedTroops, activeBanner, ref factoredNumber);
							}
						}
					}
					else if (currentUsageItem.IsConsumable && activeBanner != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedRangedDamage, activeBanner, ref factoredNumber);
					}
				}
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.IsHorseCharge && activeBanner != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedChargeDamage, activeBanner, ref factoredNumber);
				}
			}
			float num = 1f;
			if (Mission.Current.IsSallyOutBattle)
			{
				DestructableComponent hitObjectDestructibleComponent = attackInformation.HitObjectDestructibleComponent;
				if (hitObjectDestructibleComponent != null && hitObjectDestructibleComponent.GameEntity.GetFirstScriptOfType<SiegeWeapon>() != null)
				{
					num *= 4.5f;
				}
			}
			factoredNumber = new FactoredNumber(factoredNumber.ResultNumber * num);
			if (basicCharacterObject != null && currentUsageItem != null)
			{
				if (currentUsageItem.IsConsumable)
				{
					if (activeBanner2 != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedRangedAttackDamage, activeBanner2, ref factoredNumber);
					}
				}
				else if (currentUsageItem.IsMeleeWeapon && activeBanner2 != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMeleeAttackDamage, activeBanner2, ref factoredNumber);
				}
			}
			float resultNumber = factoredNumber.ResultNumber;
			return MathF.Max(0f, resultNumber);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0005F7EB File Offset: 0x0005D9EB
		public override void DecideMissileWeaponFlags(Agent attackerAgent, MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags)
		{
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0005F7F0 File Offset: 0x0005D9F0
		public override bool DecideCrushedThrough(Agent attackerAgent, Agent defenderAgent, float totalAttackEnergy, Agent.UsageDirection attackDirection, StrikeType strikeType, WeaponComponentData defendItem, bool isPassiveUsage)
		{
			EquipmentIndex wieldedItemIndex = attackerAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex == EquipmentIndex.None)
			{
				wieldedItemIndex = attackerAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			}
			WeaponComponentData weaponComponentData = (wieldedItemIndex != EquipmentIndex.None) ? attackerAgent.Equipment[wieldedItemIndex].CurrentUsageItem : null;
			if (weaponComponentData == null || isPassiveUsage || !weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.CanCrushThrough) || strikeType != StrikeType.Swing || attackDirection != Agent.UsageDirection.AttackUp)
			{
				return false;
			}
			float num = 58f;
			if (defendItem != null && defendItem.IsShield)
			{
				num *= 1.2f;
			}
			return totalAttackEnergy > num;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0005F874 File Offset: 0x0005DA74
		public override bool CanWeaponDismount(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			return MBMath.IsBetween((int)blow.VictimBodyPart, 0, 6) && ((!attackerAgent.HasMount && blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook)) || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanDismount)));
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0005F8DD File Offset: 0x0005DADD
		public override void CalculateDefendedBlowStunMultipliers(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, WeaponComponentData attackerWeapon, WeaponComponentData defenderWeapon, out float attackerStunMultiplier, out float defenderStunMultiplier)
		{
			attackerStunMultiplier = 1f;
			defenderStunMultiplier = 1f;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0005F8F0 File Offset: 0x0005DAF0
		public override bool CanWeaponKnockback(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			AttackCollisionData attackCollisionData = collisionData;
			return MBMath.IsBetween((int)attackCollisionData.VictimHitBodyPart, 0, 6) && !attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && (attackerWeapon.IsConsumable || (blow.BlowFlag & BlowFlags.CrushThrough) != BlowFlags.None || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.WideGrip)));
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0005F960 File Offset: 0x0005DB60
		public override bool CanWeaponKnockDown(Agent attackerAgent, Agent victimAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			if (attackerWeapon.WeaponClass == WeaponClass.Boulder)
			{
				return true;
			}
			AttackCollisionData attackCollisionData = collisionData;
			BoneBodyPartType victimHitBodyPart = attackCollisionData.VictimHitBodyPart;
			bool flag = MBMath.IsBetween((int)victimHitBodyPart, 0, 6);
			if (!victimAgent.HasMount && victimHitBodyPart == BoneBodyPartType.Legs)
			{
				flag = true;
			}
			return flag && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && ((attackerWeapon.IsPolearm && blow.StrikeType == StrikeType.Thrust) || (attackerWeapon.IsMeleeWeapon && blow.StrikeType == StrikeType.Swing && MissionCombatMechanicsHelper.DecideSweetSpotCollision(collisionData)));
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0005F9EC File Offset: 0x0005DBEC
		public override float GetDismountPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			float num = 0f;
			if (blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook))
			{
				num += 0.25f;
			}
			return num;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0005FA28 File Offset: 0x0005DC28
		public override float GetKnockBackPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			return 0f;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0005FA30 File Offset: 0x0005DC30
		public override float GetKnockDownPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			float num = 0f;
			if (attackerWeapon.WeaponClass == WeaponClass.Boulder)
			{
				num += 0.25f;
			}
			else if (attackerWeapon.IsMeleeWeapon)
			{
				AttackCollisionData attackCollisionData2 = attackCollisionData;
				if (attackCollisionData2.VictimHitBodyPart == BoneBodyPartType.Legs && blow.StrikeType == StrikeType.Swing)
				{
					num += 0.1f;
				}
				else
				{
					attackCollisionData2 = attackCollisionData;
					if (attackCollisionData2.VictimHitBodyPart == BoneBodyPartType.Head)
					{
						num += 0.15f;
					}
				}
			}
			return num;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0005FA9D File Offset: 0x0005DC9D
		public override float GetHorseChargePenetration()
		{
			return 0.4f;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0005FAA4 File Offset: 0x0005DCA4
		public override float CalculateStaggerThresholdDamage(Agent defenderAgent, in Blow blow)
		{
			ManagedParametersEnum managedParameterEnum;
			if (blow.DamageType == DamageTypes.Cut)
			{
				managedParameterEnum = ManagedParametersEnum.DamageInterruptAttackThresholdCut;
			}
			else if (blow.DamageType == DamageTypes.Pierce)
			{
				managedParameterEnum = ManagedParametersEnum.DamageInterruptAttackThresholdPierce;
			}
			else
			{
				managedParameterEnum = ManagedParametersEnum.DamageInterruptAttackThresholdBlunt;
			}
			return ManagedParameters.Instance.GetManagedParameter(managedParameterEnum);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0005FADA File Offset: 0x0005DCDA
		public override float CalculateAlternativeAttackDamage(BasicCharacterObject attackerCharacter, WeaponComponentData weapon)
		{
			if (weapon == null)
			{
				return 2f;
			}
			if (weapon.WeaponClass == WeaponClass.LargeShield)
			{
				return 2f;
			}
			if (weapon.WeaponClass == WeaponClass.SmallShield)
			{
				return 1f;
			}
			if (weapon.IsTwoHanded)
			{
				return 2f;
			}
			return 1f;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0005FB18 File Offset: 0x0005DD18
		public override float CalculatePassiveAttackDamage(BasicCharacterObject attackerCharacter, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0005FB1B File Offset: 0x0005DD1B
		public override MeleeCollisionReaction DecidePassiveAttackCollisionReaction(Agent attacker, Agent defender, bool isFatalHit)
		{
			return MeleeCollisionReaction.Bounced;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0005FB20 File Offset: 0x0005DD20
		public override float CalculateShieldDamage(in AttackInformation attackInformation, float baseDamage)
		{
			baseDamage *= 1.25f;
			FactoredNumber factoredNumber = new FactoredNumber(baseDamage);
			Formation victimFormation = attackInformation.VictimFormation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			if (activeBanner != null)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedShieldDamage, activeBanner, ref factoredNumber);
			}
			return Math.Max(0f, factoredNumber.ResultNumber);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0005FB78 File Offset: 0x0005DD78
		public override float GetDamageMultiplierForBodyPart(BoneBodyPartType bodyPart, DamageTypes type, bool isHuman, bool isMissile)
		{
			float result = 1f;
			switch (bodyPart)
			{
			case BoneBodyPartType.None:
				result = 1f;
				break;
			case BoneBodyPartType.Head:
				switch (type)
				{
				case DamageTypes.Invalid:
					result = 1.5f;
					break;
				case DamageTypes.Cut:
					result = 1.2f;
					break;
				case DamageTypes.Pierce:
					if (isHuman)
					{
						result = (isMissile ? 2f : 1.25f);
					}
					else
					{
						result = 1.2f;
					}
					break;
				case DamageTypes.Blunt:
					result = 1.2f;
					break;
				}
				break;
			case BoneBodyPartType.Neck:
				switch (type)
				{
				case DamageTypes.Invalid:
					result = 1.5f;
					break;
				case DamageTypes.Cut:
					result = 1.2f;
					break;
				case DamageTypes.Pierce:
					if (isHuman)
					{
						result = (isMissile ? 2f : 1.25f);
					}
					else
					{
						result = 1.2f;
					}
					break;
				case DamageTypes.Blunt:
					result = 1.2f;
					break;
				}
				break;
			case BoneBodyPartType.Chest:
			case BoneBodyPartType.Abdomen:
			case BoneBodyPartType.ShoulderLeft:
			case BoneBodyPartType.ShoulderRight:
			case BoneBodyPartType.ArmLeft:
			case BoneBodyPartType.ArmRight:
				if (isHuman)
				{
					result = 1f;
				}
				else
				{
					result = 0.8f;
				}
				break;
			case BoneBodyPartType.Legs:
				result = 0.8f;
				break;
			}
			return result;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0005FC92 File Offset: 0x0005DE92
		public override bool CanWeaponIgnoreFriendlyFireChecks(WeaponComponentData weapon)
		{
			return weapon != null && weapon.IsConsumable && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield) && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.MultiplePenetration);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0005FCC8 File Offset: 0x0005DEC8
		public override bool DecideAgentShrugOffBlow(Agent victimAgent, AttackCollisionData collisionData, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentShrugOffBlow(victimAgent, collisionData, blow);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0005FCD2 File Offset: 0x0005DED2
		public override bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentDismountedByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0005FCE0 File Offset: 0x0005DEE0
		public override bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedBackByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0005FCEE File Offset: 0x0005DEEE
		public override bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedDownByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0005FCFC File Offset: 0x0005DEFC
		public override bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideMountRearedByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x0400090B RID: 2315
		private const float SallyOutSiegeEngineDamageMultiplier = 4.5f;
	}
}
