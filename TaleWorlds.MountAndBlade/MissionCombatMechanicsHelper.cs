using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200025F RID: 607
	public static class MissionCombatMechanicsHelper
	{
		// Token: 0x0600204F RID: 8271 RVA: 0x00071DF0 File Offset: 0x0006FFF0
		public static bool DecideAgentShrugOffBlow(Agent victimAgent, AttackCollisionData collisionData, in Blow blow)
		{
			bool result = false;
			if (victimAgent.Health - (float)collisionData.InflictedDamage >= 1f)
			{
				float num = MissionGameModels.Current.AgentApplyDamageModel.CalculateStaggerThresholdDamage(victimAgent, blow);
				result = ((float)collisionData.InflictedDamage <= num);
			}
			return result;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00071E3C File Offset: 0x0007003C
		public static bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			bool flag = false;
			int inflictedDamage = collisionData.InflictedDamage;
			bool flag2 = victimAgent.Health - (float)inflictedDamage >= 1f;
			bool flag3 = (blow.BlowFlag & BlowFlags.ShrugOff) > BlowFlags.None;
			if (attackerWeapon != null && flag2 && !flag3)
			{
				int num = (int)victimAgent.HealthLimit;
				if (MissionGameModels.Current.AgentApplyDamageModel.CanWeaponDismount(attackerAgent, attackerWeapon, blow, collisionData))
				{
					float dismountPenetration = MissionGameModels.Current.AgentApplyDamageModel.GetDismountPenetration(attackerAgent, attackerWeapon, blow, collisionData);
					float dismountResistance = MissionGameModels.Current.AgentStatCalculateModel.GetDismountResistance(victimAgent);
					flag = MissionCombatMechanicsHelper.DecideCombatEffect((float)inflictedDamage, (float)num, dismountResistance, dismountPenetration);
				}
				if (!flag)
				{
					flag = MissionCombatMechanicsHelper.DecideWeaponKnockDown(attackerAgent, victimAgent, attackerWeapon, collisionData, blow);
				}
			}
			return flag;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00071EE8 File Offset: 0x000700E8
		public static bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			bool result = false;
			int num = (int)victimAgent.HealthLimit;
			int inflictedDamage = collisionData.InflictedDamage;
			bool flag = (blow.BlowFlag & BlowFlags.ShrugOff) > BlowFlags.None;
			AttackCollisionData attackCollisionData = collisionData;
			if (attackCollisionData.IsHorseCharge)
			{
				Vec3 position = victimAgent.Position;
				Vec2 movementDirection = attackerAgent.GetMovementDirection();
				attackCollisionData = collisionData;
				Vec3 collisionGlobalPosition = attackCollisionData.CollisionGlobalPosition;
				if (MissionCombatMechanicsHelper.ChargeDamageDotProduct(position, movementDirection, collisionGlobalPosition) >= 0.7f)
				{
					result = true;
				}
			}
			else
			{
				attackCollisionData = collisionData;
				if (attackCollisionData.IsAlternativeAttack)
				{
					result = true;
				}
				else if (attackerWeapon != null && !flag && MissionGameModels.Current.AgentApplyDamageModel.CanWeaponKnockback(attackerAgent, attackerWeapon, blow, collisionData))
				{
					float knockBackPenetration = MissionGameModels.Current.AgentApplyDamageModel.GetKnockBackPenetration(attackerAgent, attackerWeapon, blow, collisionData);
					float knockBackResistance = MissionGameModels.Current.AgentStatCalculateModel.GetKnockBackResistance(victimAgent);
					result = MissionCombatMechanicsHelper.DecideCombatEffect((float)inflictedDamage, (float)num, knockBackResistance, knockBackPenetration);
				}
			}
			return result;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00071FC8 File Offset: 0x000701C8
		public static bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			bool result = false;
			if ((blow.BlowFlag & BlowFlags.ShrugOff) <= BlowFlags.None)
			{
				int num = (int)victimAgent.HealthLimit;
				float inflictedDamage = (float)collisionData.InflictedDamage;
				bool flag = (blow.BlowFlag & BlowFlags.KnockBack) > BlowFlags.None;
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.IsHorseCharge && flag)
				{
					float horseChargePenetration = MissionGameModels.Current.AgentApplyDamageModel.GetHorseChargePenetration();
					float knockDownResistance = MissionGameModels.Current.AgentStatCalculateModel.GetKnockDownResistance(victimAgent, StrikeType.Invalid);
					result = MissionCombatMechanicsHelper.DecideCombatEffect(inflictedDamage, (float)num, knockDownResistance, horseChargePenetration);
				}
				else if (attackerWeapon != null)
				{
					result = MissionCombatMechanicsHelper.DecideWeaponKnockDown(attackerAgent, victimAgent, attackerWeapon, collisionData, blow);
				}
			}
			return result;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00072060 File Offset: 0x00070260
		public static bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			float damageMultiplierOfCombatDifficulty = Mission.Current.GetDamageMultiplierOfCombatDifficulty(victimAgent, attackerAgent);
			if (attackerWeapon != null && attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.WideGrip) && attackerWeapon.WeaponLength > 120 && blow.StrikeType == StrikeType.Thrust)
			{
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.ThrustTipHit && attackerAgent != null && !attackerAgent.HasMount && victimAgent.GetAgentFlags().HasAnyFlag(AgentFlag.CanRear) && victimAgent.MovementVelocity.y > 5f && Vec3.DotProduct(blow.Direction, victimAgent.Frame.rotation.f) < -0.35f)
				{
					Vec3 globalPosition = blow.GlobalPosition;
					if (Vec2.DotProduct(globalPosition.AsVec2 - victimAgent.Position.AsVec2, victimAgent.GetMovementDirection()) > 0f)
					{
						return (float)collisionData.InflictedDamage >= ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.MakesRearAttackDamageThreshold) * damageMultiplierOfCombatDifficulty;
					}
				}
			}
			return false;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00072168 File Offset: 0x00070368
		public static bool IsCollisionBoneDifferentThanWeaponAttachBone(in AttackCollisionData collisionData, int weaponAttachBoneIndex)
		{
			AttackCollisionData attackCollisionData = collisionData;
			if (attackCollisionData.AttackBoneIndex != -1 && weaponAttachBoneIndex != -1)
			{
				attackCollisionData = collisionData;
				return weaponAttachBoneIndex != (int)attackCollisionData.AttackBoneIndex;
			}
			return false;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000721A0 File Offset: 0x000703A0
		public static bool DecideSweetSpotCollision(in AttackCollisionData collisionData)
		{
			AttackCollisionData attackCollisionData = collisionData;
			if (attackCollisionData.AttackProgress >= 0.22f)
			{
				attackCollisionData = collisionData;
				return attackCollisionData.AttackProgress <= 0.55f;
			}
			return false;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000721DC File Offset: 0x000703DC
		public static void GetAttackCollisionResults(in AttackInformation attackInformation, bool crushedThrough, float momentumRemaining, in MissionWeapon attackerWeapon, bool cancelDamage, ref AttackCollisionData attackCollisionData, out CombatLogData combatLog, out int speedBonus)
		{
			float distance = 0f;
			if (attackCollisionData.IsMissile)
			{
				distance = (attackCollisionData.MissileStartingPosition - attackCollisionData.CollisionGlobalPosition).Length;
			}
			combatLog = new CombatLogData(attackInformation.IsVictimAgentSameWithAttackerAgent, attackInformation.IsAttackerAgentHuman, attackInformation.IsAttackerAgentMine, attackInformation.DoesAttackerHaveRiderAgent, attackInformation.IsAttackerAgentRiderAgentMine, attackInformation.IsAttackerAgentMount, attackInformation.IsVictimAgentHuman, attackInformation.IsVictimAgentMine, false, attackInformation.DoesVictimHaveRiderAgent, attackInformation.IsVictimAgentRiderAgentMine, attackInformation.IsVictimAgentMount, false, attackInformation.IsVictimRiderAgentSameAsAttackerAgent, false, false, distance);
			bool flag = MissionCombatMechanicsHelper.IsCollisionBoneDifferentThanWeaponAttachBone(attackCollisionData, attackInformation.WeaponAttachBoneIndex);
			Vec2 agentVelocityContribution = MissionCombatMechanicsHelper.GetAgentVelocityContribution(attackInformation.DoesAttackerHaveMountAgent, attackInformation.AttackerAgentMovementVelocity, attackInformation.AttackerAgentMountMovementDirection, attackInformation.AttackerMovementDirectionAsAngle);
			Vec2 agentVelocityContribution2 = MissionCombatMechanicsHelper.GetAgentVelocityContribution(attackInformation.DoesVictimHaveMountAgent, attackInformation.VictimAgentMovementVelocity, attackInformation.VictimAgentMountMovementDirection, attackInformation.VictimMovementDirectionAsAngle);
			if (attackCollisionData.IsColliderAgent)
			{
				combatLog.IsRangedAttack = attackCollisionData.IsMissile;
				combatLog.HitSpeed = (attackCollisionData.IsMissile ? (agentVelocityContribution2.ToVec3(0f) - attackCollisionData.MissileVelocity).Length : (agentVelocityContribution - agentVelocityContribution2).Length);
			}
			float baseMagnitude;
			MissionCombatMechanicsHelper.ComputeBlowMagnitude(attackCollisionData, attackInformation, attackerWeapon, momentumRemaining, cancelDamage, flag, agentVelocityContribution, agentVelocityContribution2, out attackCollisionData.BaseMagnitude, out baseMagnitude, out attackCollisionData.MovementSpeedDamageModifier, out speedBonus);
			MissionWeapon missionWeapon = attackerWeapon;
			DamageTypes damageType = (DamageTypes)((missionWeapon.IsEmpty || flag || attackCollisionData.IsAlternativeAttack || attackCollisionData.IsFallDamage || attackCollisionData.IsHorseCharge) ? 2 : attackCollisionData.DamageType);
			combatLog.DamageType = damageType;
			if (!attackCollisionData.IsColliderAgent && attackCollisionData.EntityExists)
			{
				string name = PhysicsMaterial.GetFromIndex(attackCollisionData.PhysicsMaterialIndex).Name;
				bool isWoodenBody = name == "wood" || name == "wood_weapon" || name == "wood_shield";
				float baseMagnitude2 = attackCollisionData.BaseMagnitude;
				bool isAttackerAgentDoingPassiveAttack = attackInformation.IsAttackerAgentDoingPassiveAttack;
				missionWeapon = attackerWeapon;
				attackCollisionData.BaseMagnitude = baseMagnitude2 * MissionCombatMechanicsHelper.GetEntityDamageMultiplier(isAttackerAgentDoingPassiveAttack, missionWeapon.CurrentUsageItem, damageType, isWoodenBody);
				attackCollisionData.InflictedDamage = MBMath.ClampInt((int)attackCollisionData.BaseMagnitude, 0, 2000);
				combatLog.InflictedDamage = attackCollisionData.InflictedDamage;
			}
			if (attackCollisionData.IsColliderAgent && !attackInformation.IsVictimAgentNull)
			{
				if (attackCollisionData.IsAlternativeAttack)
				{
					baseMagnitude = attackCollisionData.BaseMagnitude;
				}
				if (attackCollisionData.AttackBlockedWithShield)
				{
					missionWeapon = attackerWeapon;
					MissionCombatMechanicsHelper.ComputeBlowDamageOnShield(attackInformation, attackCollisionData, missionWeapon.CurrentUsageItem, attackCollisionData.BaseMagnitude, out attackCollisionData.InflictedDamage);
					attackCollisionData.AbsorbedByArmor = attackCollisionData.InflictedDamage;
				}
				else if (attackCollisionData.MissileBlockedWithWeapon)
				{
					attackCollisionData.InflictedDamage = 0;
					attackCollisionData.AbsorbedByArmor = 0;
				}
				else
				{
					missionWeapon = attackerWeapon;
					MissionCombatMechanicsHelper.ComputeBlowDamage(attackInformation, attackCollisionData, missionWeapon.CurrentUsageItem, damageType, baseMagnitude, speedBonus, cancelDamage, out attackCollisionData.InflictedDamage, out attackCollisionData.AbsorbedByArmor);
				}
				combatLog.InflictedDamage = attackCollisionData.InflictedDamage;
				combatLog.AbsorbedDamage = attackCollisionData.AbsorbedByArmor;
				combatLog.AttackProgress = attackCollisionData.AttackProgress;
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00072508 File Offset: 0x00070708
		internal static void GetDefendCollisionResults(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, int attackerWeaponSlotIndex, bool isAlternativeAttack, StrikeType strikeType, Agent.UsageDirection attackDirection, float collisionDistanceOnWeapon, float attackProgress, bool attackIsParried, bool isPassiveUsageHit, bool isHeavyAttack, ref float defenderStunPeriod, ref float attackerStunPeriod, ref bool crushedThrough, ref bool chamber)
		{
			MissionWeapon missionWeapon = (attackerWeaponSlotIndex >= 0) ? attackerAgent.Equipment[attackerWeaponSlotIndex] : MissionWeapon.Invalid;
			WeaponComponentData weaponComponentData = missionWeapon.IsEmpty ? null : missionWeapon.CurrentUsageItem;
			EquipmentIndex wieldedItemIndex = defenderAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex == EquipmentIndex.None)
			{
				wieldedItemIndex = defenderAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			}
			ItemObject itemObject = (wieldedItemIndex != EquipmentIndex.None) ? defenderAgent.Equipment[wieldedItemIndex].Item : null;
			WeaponComponentData weaponComponentData2 = (wieldedItemIndex != EquipmentIndex.None) ? defenderAgent.Equipment[wieldedItemIndex].CurrentUsageItem : null;
			float num = 10f;
			attackerStunPeriod = ((strikeType == StrikeType.Thrust) ? ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunPeriodAttackerThrust) : ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunPeriodAttackerSwing));
			chamber = false;
			if (!missionWeapon.IsEmpty)
			{
				float z = attackerAgent.GetCurWeaponOffset().z;
				float realWeaponLength = weaponComponentData.GetRealWeaponLength();
				float num2 = realWeaponLength + z;
				float impactPoint = MBMath.ClampFloat((0.2f + collisionDistanceOnWeapon) / num2, 0.1f, 0.98f);
				float exraLinearSpeed = MissionCombatMechanicsHelper.ComputeRelativeSpeedDiffOfAgents(attackerAgent, defenderAgent);
				float num3;
				if (strikeType == StrikeType.Thrust)
				{
					num3 = CombatStatCalculator.CalculateBaseBlowMagnitudeForThrust((float)missionWeapon.GetModifiedThrustSpeedForCurrentUsage() / 11.764706f * MissionCombatMechanicsHelper.SpeedGraphFunction(attackProgress, strikeType, attackDirection), missionWeapon.Item.Weight, exraLinearSpeed);
				}
				else
				{
					num3 = CombatStatCalculator.CalculateBaseBlowMagnitudeForSwing((float)missionWeapon.GetModifiedSwingSpeedForCurrentUsage() / 4.5454545f * MissionCombatMechanicsHelper.SpeedGraphFunction(attackProgress, strikeType, attackDirection), realWeaponLength, missionWeapon.Item.Weight, weaponComponentData.Inertia, weaponComponentData.CenterOfMass, impactPoint, exraLinearSpeed);
				}
				if (strikeType == StrikeType.Thrust)
				{
					num3 *= 0.8f;
				}
				else if (attackDirection == Agent.UsageDirection.AttackUp)
				{
					num3 *= 1.25f;
				}
				else if (isHeavyAttack)
				{
					num3 *= ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.HeavyAttackMomentumMultiplier);
				}
				num += num3;
			}
			float num4 = 1f;
			defenderStunPeriod = num * ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunMomentumTransferFactor);
			if (weaponComponentData2 != null)
			{
				if (weaponComponentData2.IsShield)
				{
					float managedParameter = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightOffsetShield);
					num4 += managedParameter * itemObject.Weight;
				}
				else
				{
					num4 = 0.9f;
					float managedParameter2 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightMultiplierWeaponWeight);
					num4 += managedParameter2 * itemObject.Weight;
					ItemObject.ItemTypeEnum itemType = itemObject.ItemType;
					if (itemType == ItemObject.ItemTypeEnum.TwoHandedWeapon)
					{
						managedParameter2 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightBonusTwoHanded);
					}
					else if (itemType == ItemObject.ItemTypeEnum.Polearm)
					{
						num4 += ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightBonusPolearm);
					}
				}
				if (collisionResult == CombatCollisionResult.Parried)
				{
					attackerStunPeriod += MathF.Min(0.15f, 0.12f * num4);
					num4 += ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightBonusActiveBlocked);
				}
				else if (collisionResult == CombatCollisionResult.ChamberBlocked)
				{
					attackerStunPeriod += MathF.Min(0.25f, 0.25f * num4);
					num4 += ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightBonusChamberBlocked);
					chamber = true;
				}
			}
			if (!defenderAgent.GetIsLeftStance())
			{
				num4 += ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunDefendWeaponWeightBonusRightStance);
			}
			defenderStunPeriod /= num4;
			float num5;
			float num6;
			MissionGameModels.Current.AgentApplyDamageModel.CalculateDefendedBlowStunMultipliers(attackerAgent, defenderAgent, collisionResult, weaponComponentData, weaponComponentData2, out num5, out num6);
			attackerStunPeriod *= num5;
			defenderStunPeriod *= num6;
			float managedParameter3 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.StunPeriodMax);
			attackerStunPeriod = MathF.Min(attackerStunPeriod, managedParameter3);
			defenderStunPeriod = MathF.Min(defenderStunPeriod, managedParameter3);
			crushedThrough = (!chamber && MissionGameModels.Current.AgentApplyDamageModel.DecideCrushedThrough(attackerAgent, defenderAgent, num, attackDirection, strikeType, weaponComponentData2, isPassiveUsageHit));
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00072860 File Offset: 0x00070A60
		private static bool DecideWeaponKnockDown(Agent attackerAgent, Agent victimAgent, WeaponComponentData attackerWeapon, in AttackCollisionData collisionData, in Blow blow)
		{
			if (MissionGameModels.Current.AgentApplyDamageModel.CanWeaponKnockDown(attackerAgent, victimAgent, attackerWeapon, blow, collisionData))
			{
				float knockDownPenetration = MissionGameModels.Current.AgentApplyDamageModel.GetKnockDownPenetration(attackerAgent, attackerWeapon, blow, collisionData);
				float knockDownResistance = MissionGameModels.Current.AgentStatCalculateModel.GetKnockDownResistance(victimAgent, blow.StrikeType);
				return MissionCombatMechanicsHelper.DecideCombatEffect((float)collisionData.InflictedDamage, victimAgent.HealthLimit, knockDownResistance, knockDownPenetration);
			}
			return false;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000728C8 File Offset: 0x00070AC8
		private static bool DecideCombatEffect(float inflictedDamage, float victimMaxHealth, float victimResistance, float attackPenetration)
		{
			float num = victimMaxHealth * Math.Max(0f, victimResistance - attackPenetration);
			return inflictedDamage >= num;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000728EC File Offset: 0x00070AEC
		private static float ChargeDamageDotProduct(in Vec3 victimPosition, in Vec2 chargerMovementDirection, in Vec3 collisionPoint)
		{
			Vec3 vec = victimPosition;
			Vec2 asVec = vec.AsVec2;
			vec = collisionPoint;
			float b = Vec2.DotProduct((asVec - vec.AsVec2).Normalized(), chargerMovementDirection);
			return MathF.Max(0f, b);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0007293C File Offset: 0x00070B3C
		private static float SpeedGraphFunction(float progress, StrikeType strikeType, Agent.UsageDirection attackDir)
		{
			bool flag = strikeType == StrikeType.Thrust;
			bool flag2 = attackDir == Agent.UsageDirection.AttackUp;
			ManagedParametersEnum managedParameterEnum;
			ManagedParametersEnum managedParameterEnum2;
			ManagedParametersEnum managedParameterEnum3;
			ManagedParametersEnum managedParameterEnum4;
			if (flag)
			{
				managedParameterEnum = ManagedParametersEnum.ThrustCombatSpeedGraphZeroProgressValue;
				managedParameterEnum2 = ManagedParametersEnum.ThrustCombatSpeedGraphFirstMaximumPoint;
				managedParameterEnum3 = ManagedParametersEnum.ThrustCombatSpeedGraphSecondMaximumPoint;
				managedParameterEnum4 = ManagedParametersEnum.ThrustCombatSpeedGraphOneProgressValue;
			}
			else if (flag2)
			{
				managedParameterEnum = ManagedParametersEnum.OverSwingCombatSpeedGraphZeroProgressValue;
				managedParameterEnum2 = ManagedParametersEnum.OverSwingCombatSpeedGraphFirstMaximumPoint;
				managedParameterEnum3 = ManagedParametersEnum.OverSwingCombatSpeedGraphSecondMaximumPoint;
				managedParameterEnum4 = ManagedParametersEnum.OverSwingCombatSpeedGraphOneProgressValue;
			}
			else
			{
				managedParameterEnum = ManagedParametersEnum.SwingCombatSpeedGraphZeroProgressValue;
				managedParameterEnum2 = ManagedParametersEnum.SwingCombatSpeedGraphFirstMaximumPoint;
				managedParameterEnum3 = ManagedParametersEnum.SwingCombatSpeedGraphSecondMaximumPoint;
				managedParameterEnum4 = ManagedParametersEnum.SwingCombatSpeedGraphOneProgressValue;
			}
			float managedParameter = ManagedParameters.Instance.GetManagedParameter(managedParameterEnum);
			float managedParameter2 = ManagedParameters.Instance.GetManagedParameter(managedParameterEnum2);
			float managedParameter3 = ManagedParameters.Instance.GetManagedParameter(managedParameterEnum3);
			float managedParameter4 = ManagedParameters.Instance.GetManagedParameter(managedParameterEnum4);
			float result;
			if (progress < managedParameter2)
			{
				result = (1f - managedParameter) / managedParameter2 * progress + managedParameter;
			}
			else if (managedParameter3 < progress)
			{
				result = (managedParameter4 - 1f) / (1f - managedParameter3) * (progress - managedParameter3) + 1f;
			}
			else
			{
				result = 1f;
			}
			return result;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000729FE File Offset: 0x00070BFE
		private static float ConvertBaseAttackMagnitude(WeaponComponentData weapon, StrikeType strikeType, float baseMagnitude)
		{
			return baseMagnitude * ((strikeType == StrikeType.Thrust) ? weapon.ThrustDamageFactor : weapon.SwingDamageFactor);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00072A14 File Offset: 0x00070C14
		private static Vec2 GetAgentVelocityContribution(bool hasAgentMountAgent, Vec2 agentMovementVelocity, Vec2 agentMountMovementDirection, float agentMovementDirectionAsAngle)
		{
			Vec2 result = Vec2.Zero;
			if (hasAgentMountAgent)
			{
				result = agentMovementVelocity.y * agentMountMovementDirection;
			}
			else
			{
				result = agentMovementVelocity;
				result.RotateCCW(agentMovementDirectionAsAngle);
			}
			return result;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00072A44 File Offset: 0x00070C44
		private static float GetEntityDamageMultiplier(bool isAttackerAgentDoingPassiveAttack, WeaponComponentData weapon, DamageTypes damageType, bool isWoodenBody)
		{
			float num = 1f;
			if (isAttackerAgentDoingPassiveAttack)
			{
				num *= 0.2f;
			}
			if (weapon != null)
			{
				if (weapon.WeaponFlags.HasAnyFlag(WeaponFlags.BonusAgainstShield))
				{
					num *= 1.2f;
				}
				switch (damageType)
				{
				case DamageTypes.Cut:
					num *= 0.8f;
					break;
				case DamageTypes.Pierce:
					num *= 0.1f;
					break;
				}
				if (isWoodenBody && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.Burning))
				{
					num *= 1.5f;
				}
			}
			return num;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00072AC5 File Offset: 0x00070CC5
		private static float ComputeSpeedBonus(float baseMagnitude, float baseMagnitudeWithoutSpeedBonus)
		{
			return baseMagnitude / baseMagnitudeWithoutSpeedBonus - 1f;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00072AD0 File Offset: 0x00070CD0
		private static float ComputeRelativeSpeedDiffOfAgents(Agent agentA, Agent agentB)
		{
			Vec2 v = Vec2.Zero;
			if (agentA.MountAgent != null)
			{
				v = agentA.MountAgent.MovementVelocity.y * agentA.MountAgent.GetMovementDirection();
			}
			else
			{
				v = agentA.MovementVelocity;
				v.RotateCCW(agentA.MovementDirectionAsAngle);
			}
			Vec2 v2 = Vec2.Zero;
			if (agentB.MountAgent != null)
			{
				v2 = agentB.MountAgent.MovementVelocity.y * agentB.MountAgent.GetMovementDirection();
			}
			else
			{
				v2 = agentB.MovementVelocity;
				v2.RotateCCW(agentB.MovementDirectionAsAngle);
			}
			return (v - v2).Length;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00072B78 File Offset: 0x00070D78
		private static void ComputeBlowDamage(in AttackInformation attackInformation, in AttackCollisionData attackCollisionData, WeaponComponentData attackerWeapon, DamageTypes damageType, float magnitude, int speedBonus, bool cancelDamage, out int inflictedDamage, out int absorbedByArmor)
		{
			float armorAmountFloat = attackInformation.ArmorAmountFloat;
			WeaponComponentData shieldOnBack = attackInformation.ShieldOnBack;
			AgentFlag victimAgentFlag = attackInformation.VictimAgentFlag;
			float victimAgentAbsorbedDamageRatio = attackInformation.VictimAgentAbsorbedDamageRatio;
			float damageMultiplierOfBone = attackInformation.DamageMultiplierOfBone;
			float combatDifficultyMultiplier = attackInformation.CombatDifficultyMultiplier;
			AttackCollisionData attackCollisionData2 = attackCollisionData;
			Vec3 collisionGlobalPosition = attackCollisionData2.CollisionGlobalPosition;
			attackCollisionData2 = attackCollisionData;
			bool attackBlockedWithShield = attackCollisionData2.AttackBlockedWithShield;
			attackCollisionData2 = attackCollisionData;
			bool collidedWithShieldOnBack = attackCollisionData2.CollidedWithShieldOnBack;
			attackCollisionData2 = attackCollisionData;
			bool isFallDamage = attackCollisionData2.IsFallDamage;
			BasicCharacterObject attackerAgentCharacter = attackInformation.AttackerAgentCharacter;
			BasicCharacterObject attackerCaptainCharacter = attackInformation.AttackerCaptainCharacter;
			BasicCharacterObject victimAgentCharacter = attackInformation.VictimAgentCharacter;
			BasicCharacterObject victimCaptainCharacter = attackInformation.VictimCaptainCharacter;
			float num = 0f;
			if (!isFallDamage)
			{
				num = MissionGameModels.Current.StrikeMagnitudeModel.CalculateAdjustedArmorForBlow(armorAmountFloat, attackerAgentCharacter, attackerCaptainCharacter, victimAgentCharacter, victimCaptainCharacter, attackerWeapon);
			}
			if (collidedWithShieldOnBack && shieldOnBack != null)
			{
				num += 10f;
			}
			float absorbedDamageRatio = victimAgentAbsorbedDamageRatio;
			float num2 = MissionGameModels.Current.StrikeMagnitudeModel.ComputeRawDamage(damageType, magnitude, num, absorbedDamageRatio);
			float num3 = 1f;
			if (!attackBlockedWithShield && !isFallDamage)
			{
				num3 *= damageMultiplierOfBone;
				num3 *= combatDifficultyMultiplier;
			}
			num2 *= num3;
			inflictedDamage = MBMath.ClampInt(MathF.Ceiling(num2), 0, 2000);
			int num4 = MBMath.ClampInt(MathF.Ceiling(MissionGameModels.Current.StrikeMagnitudeModel.ComputeRawDamage(damageType, magnitude, 0f, absorbedDamageRatio) * num3), 0, 2000);
			absorbedByArmor = num4 - inflictedDamage;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00072CD0 File Offset: 0x00070ED0
		private static void ComputeBlowDamageOnShield(in AttackInformation attackInformation, in AttackCollisionData attackCollisionData, WeaponComponentData attackerWeapon, float blowMagnitude, out int inflictedDamage)
		{
			inflictedDamage = 0;
			MissionWeapon victimShield = attackInformation.VictimShield;
			if (victimShield.CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.CanBlockRanged) && attackInformation.CanGiveDamageToAgentShield)
			{
				AttackCollisionData attackCollisionData2 = attackCollisionData;
				DamageTypes damageType = (DamageTypes)attackCollisionData2.DamageType;
				int getModifiedArmorForCurrentUsage = victimShield.GetGetModifiedArmorForCurrentUsage();
				float absorbedDamageRatio = 1f;
				float num = MissionGameModels.Current.StrikeMagnitudeModel.ComputeRawDamage(damageType, blowMagnitude, (float)getModifiedArmorForCurrentUsage, absorbedDamageRatio);
				attackCollisionData2 = attackCollisionData;
				if (attackCollisionData2.IsMissile)
				{
					if (attackerWeapon.WeaponClass == WeaponClass.ThrowingAxe)
					{
						num *= 0.3f;
					}
					else if (attackerWeapon.WeaponClass == WeaponClass.Javelin)
					{
						num *= 0.5f;
					}
					else if (attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield) && attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.MultiplePenetration))
					{
						num *= 0.5f;
					}
					else
					{
						num *= 0.15f;
					}
				}
				else
				{
					attackCollisionData2 = attackCollisionData;
					switch (attackCollisionData2.DamageType)
					{
					case 0:
					case 2:
						num *= 0.7f;
						break;
					case 1:
						num *= 0.5f;
						break;
					}
				}
				if (attackerWeapon != null && attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.BonusAgainstShield))
				{
					num *= 2f;
				}
				if (num > 0f)
				{
					if (!attackInformation.IsVictimAgentLeftStance)
					{
						num *= ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ShieldRightStanceBlockDamageMultiplier);
					}
					attackCollisionData2 = attackCollisionData;
					if (attackCollisionData2.CorrectSideShieldBlock)
					{
						num *= ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ShieldCorrectSideBlockDamageMultiplier);
					}
					num = MissionGameModels.Current.AgentApplyDamageModel.CalculateShieldDamage(attackInformation, num);
					inflictedDamage = (int)num;
				}
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00072E7C File Offset: 0x0007107C
		public static float CalculateBaseMeleeBlowMagnitude(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, StrikeType strikeType, float progressEffect, float impactPointAsPercent, float exraLinearSpeed)
		{
			MissionWeapon missionWeapon = weapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			float num = MathF.Sqrt(progressEffect);
			float result;
			if (strikeType == StrikeType.Thrust)
			{
				exraLinearSpeed *= 0.5f;
				missionWeapon = weapon;
				float thrustSpeed = (float)missionWeapon.GetModifiedThrustSpeedForCurrentUsage() / 11.764706f * num;
				result = MissionGameModels.Current.StrikeMagnitudeModel.CalculateStrikeMagnitudeForThrust(attackInformation, collisionData, weapon, thrustSpeed, exraLinearSpeed, false);
			}
			else
			{
				exraLinearSpeed *= 0.7f;
				missionWeapon = weapon;
				float swingSpeed = (float)missionWeapon.GetModifiedSwingSpeedForCurrentUsage() / 4.5454545f * num;
				float num2 = MBMath.ClampFloat(0.4f / currentUsageItem.GetRealWeaponLength(), 0f, 1f);
				float num3 = MathF.Min(0.93f, impactPointAsPercent);
				float num4 = MathF.Min(0.93f, impactPointAsPercent + num2);
				float num5 = 0f;
				for (int i = 0; i < 5; i++)
				{
					float impactPointAsPercent2 = num3 + (float)i / 4f * (num4 - num3);
					float num6 = MissionGameModels.Current.StrikeMagnitudeModel.CalculateStrikeMagnitudeForSwing(attackInformation, collisionData, weapon, swingSpeed, impactPointAsPercent2, exraLinearSpeed);
					if (num5 < num6)
					{
						num5 = num6;
					}
				}
				result = num5;
			}
			return result;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00072F98 File Offset: 0x00071198
		private static void ComputeBlowMagnitude(in AttackCollisionData acd, in AttackInformation attackInformation, MissionWeapon weapon, float momentumRemaining, bool cancelDamage, bool hitWithAnotherBone, Vec2 attackerVelocity, Vec2 victimVelocity, out float baseMagnitude, out float specialMagnitude, out float movementSpeedDamageModifier, out int speedBonusInt)
		{
			AttackCollisionData attackCollisionData = acd;
			StrikeType strikeType = (StrikeType)attackCollisionData.StrikeType;
			attackCollisionData = acd;
			Agent.UsageDirection attackDirection = attackCollisionData.AttackDirection;
			bool attackerIsDoingPassiveAttack = !attackInformation.IsAttackerAgentNull && attackInformation.IsAttackerAgentHuman && attackInformation.IsAttackerAgentActive && attackInformation.IsAttackerAgentDoingPassiveAttack;
			movementSpeedDamageModifier = 0f;
			speedBonusInt = 0;
			attackCollisionData = acd;
			if (attackCollisionData.IsMissile)
			{
				MissionCombatMechanicsHelper.ComputeBlowMagnitudeMissile(attackInformation, acd, weapon, momentumRemaining, victimVelocity, out baseMagnitude, out specialMagnitude);
			}
			else
			{
				attackCollisionData = acd;
				if (attackCollisionData.IsFallDamage)
				{
					MissionCombatMechanicsHelper.ComputeBlowMagnitudeFromFall(attackInformation, acd, out baseMagnitude, out specialMagnitude);
				}
				else
				{
					attackCollisionData = acd;
					if (attackCollisionData.IsHorseCharge)
					{
						MissionCombatMechanicsHelper.ComputeBlowMagnitudeFromHorseCharge(attackInformation, acd, attackerVelocity, victimVelocity, out baseMagnitude, out specialMagnitude);
					}
					else
					{
						MissionCombatMechanicsHelper.ComputeBlowMagnitudeMelee(attackInformation, acd, momentumRemaining, cancelDamage, hitWithAnotherBone, strikeType, attackDirection, weapon, attackerIsDoingPassiveAttack, attackerVelocity, victimVelocity, out baseMagnitude, out specialMagnitude, out movementSpeedDamageModifier, out speedBonusInt);
					}
				}
			}
			specialMagnitude = MBMath.ClampFloat(specialMagnitude, 0f, 500f);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00073084 File Offset: 0x00071284
		private static void ComputeBlowMagnitudeMelee(in AttackInformation attackInformation, in AttackCollisionData collisionData, float momentumRemaining, bool cancelDamage, bool hitWithAnotherBone, StrikeType strikeType, Agent.UsageDirection attackDirection, in MissionWeapon weapon, bool attackerIsDoingPassiveAttack, Vec2 attackerVelocity, Vec2 victimVelocity, out float baseMagnitude, out float specialMagnitude, out float movementSpeedDamageModifier, out int speedBonusInt)
		{
			Vec3 attackerAgentCurrentWeaponOffset = attackInformation.AttackerAgentCurrentWeaponOffset;
			movementSpeedDamageModifier = 0f;
			speedBonusInt = 0;
			specialMagnitude = 0f;
			baseMagnitude = 0f;
			BasicCharacterObject attackerAgentCharacter = attackInformation.AttackerAgentCharacter;
			AttackCollisionData attackCollisionData = collisionData;
			MissionWeapon missionWeapon;
			if (attackCollisionData.IsAlternativeAttack)
			{
				missionWeapon = weapon;
				WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
				baseMagnitude = MissionGameModels.Current.AgentApplyDamageModel.CalculateAlternativeAttackDamage(attackerAgentCharacter, currentUsageItem);
				baseMagnitude *= momentumRemaining;
				specialMagnitude = baseMagnitude;
				return;
			}
			attackCollisionData = collisionData;
			Vec3 weaponBlowDir = attackCollisionData.WeaponBlowDir;
			Vec2 vb = attackerVelocity - victimVelocity;
			float num = vb.Normalize();
			float num2 = Vec2.DotProduct(weaponBlowDir.AsVec2, vb);
			if (num2 > 0f)
			{
				num2 += 0.2f;
				num2 = MathF.Min(num2, 1f);
			}
			float num3 = num * num2;
			missionWeapon = weapon;
			if (missionWeapon.IsEmpty)
			{
				attackCollisionData = collisionData;
				baseMagnitude = MissionCombatMechanicsHelper.SpeedGraphFunction(attackCollisionData.AttackProgress, strikeType, attackDirection) * momentumRemaining * ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.FistFightDamageMultiplier);
				specialMagnitude = baseMagnitude;
				return;
			}
			float z = attackerAgentCurrentWeaponOffset.z;
			missionWeapon = weapon;
			WeaponComponentData currentUsageItem2 = missionWeapon.CurrentUsageItem;
			float num4 = currentUsageItem2.GetRealWeaponLength() + z;
			attackCollisionData = collisionData;
			float impactPointAsPercent = MBMath.ClampFloat(attackCollisionData.CollisionDistanceOnWeapon, -0.2f, num4) / num4;
			if (attackerIsDoingPassiveAttack)
			{
				if (!attackInformation.DoesAttackerHaveMountAgent && !attackInformation.DoesVictimHaveMountAgent && !attackInformation.IsVictimAgentMount)
				{
					baseMagnitude = 0f;
				}
				else
				{
					missionWeapon = weapon;
					baseMagnitude = CombatStatCalculator.CalculateBaseBlowMagnitudeForPassiveUsage(missionWeapon.Item.Weight, num3);
				}
				baseMagnitude = MissionGameModels.Current.AgentApplyDamageModel.CalculatePassiveAttackDamage(attackerAgentCharacter, collisionData, baseMagnitude);
			}
			else
			{
				attackCollisionData = collisionData;
				float num5 = MissionCombatMechanicsHelper.SpeedGraphFunction(attackCollisionData.AttackProgress, strikeType, attackDirection);
				baseMagnitude = MissionCombatMechanicsHelper.CalculateBaseMeleeBlowMagnitude(attackInformation, collisionData, weapon, strikeType, num5, impactPointAsPercent, num3);
				if (baseMagnitude >= 0f && num5 > 0.7f)
				{
					float baseMagnitudeWithoutSpeedBonus = MissionCombatMechanicsHelper.CalculateBaseMeleeBlowMagnitude(attackInformation, collisionData, weapon, strikeType, num5, impactPointAsPercent, 0f);
					movementSpeedDamageModifier = MissionCombatMechanicsHelper.ComputeSpeedBonus(baseMagnitude, baseMagnitudeWithoutSpeedBonus);
					speedBonusInt = MathF.Round(100f * movementSpeedDamageModifier);
					speedBonusInt = MBMath.ClampInt(speedBonusInt, -1000, 1000);
				}
			}
			baseMagnitude *= momentumRemaining;
			float num6 = 1f;
			if (hitWithAnotherBone)
			{
				if (strikeType == StrikeType.Thrust)
				{
					num6 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ThrustHitWithArmDamageMultiplier);
				}
				else
				{
					num6 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.SwingHitWithArmDamageMultiplier);
				}
			}
			else if (strikeType == StrikeType.Thrust)
			{
				attackCollisionData = collisionData;
				if (!attackCollisionData.ThrustTipHit)
				{
					attackCollisionData = collisionData;
					if (!attackCollisionData.AttackBlockedWithShield)
					{
						num6 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.NonTipThrustHitDamageMultiplier);
					}
				}
			}
			baseMagnitude *= num6;
			if (attackInformation.AttackerAgent != null)
			{
				float weaponDamageMultiplier = MissionGameModels.Current.AgentStatCalculateModel.GetWeaponDamageMultiplier(attackInformation.AttackerAgent, currentUsageItem2);
				baseMagnitude *= weaponDamageMultiplier;
			}
			specialMagnitude = MissionCombatMechanicsHelper.ConvertBaseAttackMagnitude(currentUsageItem2, strikeType, baseMagnitude);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00073378 File Offset: 0x00071578
		private static void ComputeBlowMagnitudeFromHorseCharge(in AttackInformation attackInformation, in AttackCollisionData acd, Vec2 attackerAgentVelocity, Vec2 victimAgentVelocity, out float baseMagnitude, out float specialMagnitude)
		{
			Vec2 attackerAgentMovementDirection = attackInformation.AttackerAgentMovementDirection;
			Vec2 v = attackerAgentMovementDirection * Vec2.DotProduct(victimAgentVelocity, attackerAgentMovementDirection);
			Vec2 vec = attackerAgentVelocity - v;
			AttackCollisionData attackCollisionData = acd;
			Vec3 collisionGlobalPosition = attackCollisionData.CollisionGlobalPosition;
			float num = MissionCombatMechanicsHelper.ChargeDamageDotProduct(attackInformation.VictimAgentPosition, attackerAgentMovementDirection, collisionGlobalPosition);
			float num2 = vec.Length * num;
			baseMagnitude = num2 * num2 * num * attackInformation.AttackerAgentMountChargeDamageProperty;
			specialMagnitude = baseMagnitude;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000733E8 File Offset: 0x000715E8
		private static void ComputeBlowMagnitudeMissile(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float momentumRemaining, in Vec2 victimVelocity, out float baseMagnitude, out float specialMagnitude)
		{
			float length;
			if (!attackInformation.IsVictimAgentNull)
			{
				Vec2 vec = victimVelocity;
				Vec3 v = vec.ToVec3(0f);
				AttackCollisionData attackCollisionData = collisionData;
				length = (v - attackCollisionData.MissileVelocity).Length;
			}
			else
			{
				AttackCollisionData attackCollisionData = collisionData;
				length = attackCollisionData.MissileVelocity.Length;
			}
			baseMagnitude = MissionGameModels.Current.StrikeMagnitudeModel.CalculateStrikeMagnitudeForMissile(attackInformation, collisionData, weapon, length);
			baseMagnitude *= momentumRemaining;
			if (attackInformation.AttackerAgent != null)
			{
				AgentStatCalculateModel agentStatCalculateModel = MissionGameModels.Current.AgentStatCalculateModel;
				Agent attackerAgent = attackInformation.AttackerAgent;
				MissionWeapon missionWeapon = weapon;
				float weaponDamageMultiplier = agentStatCalculateModel.GetWeaponDamageMultiplier(attackerAgent, missionWeapon.CurrentUsageItem);
				baseMagnitude *= weaponDamageMultiplier;
			}
			specialMagnitude = baseMagnitude;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000734A4 File Offset: 0x000716A4
		private static void ComputeBlowMagnitudeFromFall(in AttackInformation attackInformation, in AttackCollisionData acd, out float baseMagnitude, out float specialMagnitude)
		{
			float victimAgentScale = attackInformation.VictimAgentScale;
			float num = attackInformation.VictimAgentWeight * victimAgentScale * victimAgentScale;
			float num2 = MathF.Sqrt(1f + attackInformation.VictimAgentTotalEncumbrance / num);
			AttackCollisionData attackCollisionData = acd;
			float num3 = -attackCollisionData.VictimAgentCurVelocity.z;
			if (attackInformation.DoesVictimHaveMountAgent)
			{
				float managedParameter = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.FallSpeedReductionMultiplierForRiderDamage);
				num3 *= managedParameter;
			}
			float num4;
			if (attackInformation.IsVictimAgentHuman)
			{
				num4 = 1f;
			}
			else
			{
				num4 = 1.41f;
			}
			float managedParameter2 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.FallDamageMultiplier);
			float managedParameter3 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.FallDamageAbsorption);
			baseMagnitude = (num3 * num3 * managedParameter2 - managedParameter3) * num2 * num4;
			baseMagnitude = MBMath.ClampFloat(baseMagnitude, 0f, 499.9f);
			specialMagnitude = baseMagnitude;
		}

		// Token: 0x04000BF4 RID: 3060
		private const float SpeedBonusFactorForSwing = 0.7f;

		// Token: 0x04000BF5 RID: 3061
		private const float SpeedBonusFactorForThrust = 0.5f;
	}
}
