using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FB RID: 507
	public class MultiplayerAgentApplyDamageModel : AgentApplyDamageModel
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x00061AA4 File Offset: 0x0005FCA4
		public override float CalculateDamage(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float baseDamage)
		{
			float num = baseDamage;
			Agent attackerAgent = attackInformation.AttackerAgent;
			Agent victimAgent = attackInformation.VictimAgent;
			MPPerkObject.MPCombatPerkHandler combatPerkHandler = MPPerkObject.GetCombatPerkHandler(attackerAgent, victimAgent);
			if (combatPerkHandler != null)
			{
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.AttackBlockedWithShield)
				{
					float num2 = 1f;
					MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler = combatPerkHandler;
					attackCollisionData = collisionData;
					float num3 = num2 + mpcombatPerkHandler.GetShieldDamage(attackCollisionData.CorrectSideShieldBlock);
					MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler2 = combatPerkHandler;
					attackCollisionData = collisionData;
					float num4 = num3 + mpcombatPerkHandler2.GetShieldDamageTaken(attackCollisionData.CorrectSideShieldBlock);
					num = MathF.Max(0f, num * num4);
				}
				bool flag = MissionCombatMechanicsHelper.IsCollisionBoneDifferentThanWeaponAttachBone(collisionData, attackInformation.WeaponAttachBoneIndex);
				MissionWeapon missionWeapon = weapon;
				bool flag2;
				if (!missionWeapon.IsEmpty && !flag)
				{
					attackCollisionData = collisionData;
					if (!attackCollisionData.IsAlternativeAttack)
					{
						attackCollisionData = collisionData;
						if (!attackCollisionData.IsFallDamage)
						{
							attackCollisionData = collisionData;
							flag2 = attackCollisionData.IsHorseCharge;
							goto IL_C4;
						}
					}
				}
				flag2 = true;
				IL_C4:
				DamageTypes damageTypes;
				if (!flag2)
				{
					attackCollisionData = collisionData;
					damageTypes = (DamageTypes)attackCollisionData.DamageType;
				}
				else
				{
					damageTypes = DamageTypes.Blunt;
				}
				DamageTypes damageTypes2 = damageTypes;
				float a = 0f;
				float num5 = 1f;
				MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler3 = combatPerkHandler;
				missionWeapon = weapon;
				WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
				DamageTypes damageType = damageTypes2;
				attackCollisionData = collisionData;
				float num6 = num5 + mpcombatPerkHandler3.GetDamage(currentUsageItem, damageType, attackCollisionData.IsAlternativeAttack);
				MPPerkObject.MPCombatPerkHandler mpcombatPerkHandler4 = combatPerkHandler;
				missionWeapon = weapon;
				float num7 = MathF.Max(a, num6 + mpcombatPerkHandler4.GetDamageTaken(missionWeapon.CurrentUsageItem, damageTypes2));
				if (attackInformation.IsHeadShot)
				{
					missionWeapon = weapon;
					if (missionWeapon.CurrentUsageItem != null)
					{
						missionWeapon = weapon;
						if (!missionWeapon.CurrentUsageItem.IsConsumable)
						{
							missionWeapon = weapon;
							if (!missionWeapon.CurrentUsageItem.IsRangedWeapon)
							{
								goto IL_17A;
							}
						}
						num7 += combatPerkHandler.GetRangedHeadShotDamage();
					}
				}
				IL_17A:
				num *= num7;
			}
			return num;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00061C31 File Offset: 0x0005FE31
		public override void DecideMissileWeaponFlags(Agent attackerAgent, MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags)
		{
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00061C34 File Offset: 0x0005FE34
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

		// Token: 0x06001C23 RID: 7203 RVA: 0x00061CB8 File Offset: 0x0005FEB8
		public override bool CanWeaponDismount(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			return MBMath.IsBetween((int)blow.VictimBodyPart, 0, 6) && ((!attackerAgent.HasMount && blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook)) || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanDismount)));
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00061D21 File Offset: 0x0005FF21
		public override void CalculateDefendedBlowStunMultipliers(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, WeaponComponentData attackerWeapon, WeaponComponentData defenderWeapon, out float attackerStunMultiplier, out float defenderStunMultiplier)
		{
			attackerStunMultiplier = 1f;
			defenderStunMultiplier = 1f;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00061D34 File Offset: 0x0005FF34
		public override bool CanWeaponKnockback(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			AttackCollisionData attackCollisionData = collisionData;
			return MBMath.IsBetween((int)attackCollisionData.VictimHitBodyPart, 0, 6) && !attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && (attackerWeapon.IsConsumable || (blow.BlowFlag & BlowFlags.CrushThrough) != BlowFlags.None || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.WideGrip)));
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00061DA4 File Offset: 0x0005FFA4
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

		// Token: 0x06001C27 RID: 7207 RVA: 0x00061E30 File Offset: 0x00060030
		public override float GetDismountPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			return 0f;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00061E37 File Offset: 0x00060037
		public override float GetKnockBackPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			return 0f;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00061E40 File Offset: 0x00060040
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

		// Token: 0x06001C2A RID: 7210 RVA: 0x00061EAD File Offset: 0x000600AD
		public override float GetHorseChargePenetration()
		{
			return 0.4f;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00061EB4 File Offset: 0x000600B4
		public override float CalculateStaggerThresholdDamage(Agent defenderAgent, in Blow blow)
		{
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(defenderAgent);
			float? num = (perkHandler != null) ? new float?(perkHandler.GetDamageInterruptionThreshold()) : null;
			if (num != null && num.Value > 0f)
			{
				return num.Value;
			}
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

		// Token: 0x06001C2C RID: 7212 RVA: 0x00061F29 File Offset: 0x00060129
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

		// Token: 0x06001C2D RID: 7213 RVA: 0x00061F67 File Offset: 0x00060167
		public override float CalculatePassiveAttackDamage(BasicCharacterObject attackerCharacter, in AttackCollisionData collisionData, float baseDamage)
		{
			return baseDamage;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00061F6A File Offset: 0x0006016A
		public override MeleeCollisionReaction DecidePassiveAttackCollisionReaction(Agent attacker, Agent defender, bool isFatalHit)
		{
			return MeleeCollisionReaction.Bounced;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00061F70 File Offset: 0x00060170
		public override float CalculateShieldDamage(in AttackInformation attackInformation, float baseDamage)
		{
			baseDamage *= 1.25f;
			MissionMultiplayerFlagDomination missionBehavior = Mission.Current.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			if (missionBehavior != null && missionBehavior.GetMissionType() == MultiplayerGameType.Captain)
			{
				return baseDamage * 0.75f;
			}
			return baseDamage;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00061FA8 File Offset: 0x000601A8
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

		// Token: 0x06001C31 RID: 7217 RVA: 0x000620C2 File Offset: 0x000602C2
		public override bool CanWeaponIgnoreFriendlyFireChecks(WeaponComponentData weapon)
		{
			return weapon != null && weapon.IsConsumable && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield) && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.MultiplePenetration);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000620F8 File Offset: 0x000602F8
		public override bool DecideAgentShrugOffBlow(Agent victimAgent, AttackCollisionData collisionData, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentShrugOffBlow(victimAgent, collisionData, blow);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00062102 File Offset: 0x00060302
		public override bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentDismountedByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00062110 File Offset: 0x00060310
		public override bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedBackByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0006211E File Offset: 0x0006031E
		public override bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedDownByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0006212C File Offset: 0x0006032C
		public override bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideMountRearedByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}
	}
}
