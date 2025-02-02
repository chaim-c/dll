using System;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace SandBox.GameComponents
{
	// Token: 0x0200009B RID: 155
	public class SandboxStrikeMagnitudeModel : StrikeMagnitudeCalculationModel
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0002AB95 File Offset: 0x00028D95
		public override float CalculateHorseArcheryFactor(BasicCharacterObject characterObject)
		{
			return 100f;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002AB9C File Offset: 0x00028D9C
		public override float CalculateStrikeMagnitudeForMissile(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float missileSpeed)
		{
			BasicCharacterObject attackerAgentCharacter = attackInformation.AttackerAgentCharacter;
			MissionWeapon missionWeapon = weapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			AttackCollisionData attackCollisionData = collisionData;
			float missileTotalDamage = attackCollisionData.MissileTotalDamage;
			attackCollisionData = collisionData;
			float missileStartingBaseSpeed = attackCollisionData.MissileStartingBaseSpeed;
			float num = missileSpeed;
			float num2 = missileSpeed - missileStartingBaseSpeed;
			if (num2 > 0f)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(0f, false, null);
				CharacterObject characterObject = attackerAgentCharacter as CharacterObject;
				if (characterObject != null && characterObject.IsHero)
				{
					WeaponClass ammoClass = currentUsageItem.AmmoClass;
					if (ammoClass == WeaponClass.Stone || ammoClass == WeaponClass.ThrowingAxe || ammoClass == WeaponClass.ThrowingKnife || ammoClass == WeaponClass.Javelin)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.RunningThrow, characterObject, true, ref explainedNumber);
					}
				}
				num += num2 * explainedNumber.ResultNumber;
			}
			num /= missileStartingBaseSpeed;
			return num * num * missileTotalDamage;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002AC68 File Offset: 0x00028E68
		public override float CalculateStrikeMagnitudeForSwing(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float swingSpeed, float impactPointAsPercent, float extraLinearSpeed)
		{
			BasicCharacterObject attackerAgentCharacter = attackInformation.AttackerAgentCharacter;
			BasicCharacterObject attackerCaptainCharacter = attackInformation.AttackerCaptainCharacter;
			bool doesAttackerHaveMountAgent = attackInformation.DoesAttackerHaveMountAgent;
			MissionWeapon missionWeapon = weapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			CharacterObject characterObject = attackerAgentCharacter as CharacterObject;
			ExplainedNumber explainedNumber = new ExplainedNumber(extraLinearSpeed, false, null);
			if (characterObject != null && extraLinearSpeed > 0f)
			{
				SkillObject relevantSkill = currentUsageItem.RelevantSkill;
				CharacterObject captainCharacter = attackerCaptainCharacter as CharacterObject;
				if (doesAttackerHaveMountAgent)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.NomadicTraditions, captainCharacter, ref explainedNumber);
				}
				else
				{
					if (relevantSkill == DefaultSkills.TwoHanded)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.RecklessCharge, characterObject, true, ref explainedNumber);
					}
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Roguery.DashAndSlash, characterObject, true, ref explainedNumber);
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.SurgingBlow, characterObject, true, ref explainedNumber);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.SurgingBlow, captainCharacter, ref explainedNumber);
				}
				if (relevantSkill == DefaultSkills.Polearm)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Lancer, captainCharacter, ref explainedNumber);
					if (doesAttackerHaveMountAgent)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.Lancer, characterObject, true, ref explainedNumber);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.UnstoppableForce, captainCharacter, ref explainedNumber);
					}
				}
			}
			missionWeapon = weapon;
			ItemObject item = missionWeapon.Item;
			float num = CombatStatCalculator.CalculateStrikeMagnitudeForSwing(swingSpeed, impactPointAsPercent, item.Weight, currentUsageItem.GetRealWeaponLength(), currentUsageItem.Inertia, currentUsageItem.CenterOfMass, explainedNumber.ResultNumber);
			if (item.IsCraftedByPlayer)
			{
				ExplainedNumber explainedNumber2 = new ExplainedNumber(num, false, null);
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crafting.SharpenedEdge, characterObject, true, ref explainedNumber2);
				num = explainedNumber2.ResultNumber;
			}
			return num;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0002ADC0 File Offset: 0x00028FC0
		public override float CalculateStrikeMagnitudeForThrust(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float thrustWeaponSpeed, float extraLinearSpeed, bool isThrown = false)
		{
			BasicCharacterObject attackerAgentCharacter = attackInformation.AttackerAgentCharacter;
			BasicCharacterObject attackerCaptainCharacter = attackInformation.AttackerCaptainCharacter;
			bool doesAttackerHaveMountAgent = attackInformation.DoesAttackerHaveMountAgent;
			MissionWeapon missionWeapon = weapon;
			ItemObject item = missionWeapon.Item;
			float weight = item.Weight;
			missionWeapon = weapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			CharacterObject characterObject = attackerAgentCharacter as CharacterObject;
			ExplainedNumber explainedNumber = new ExplainedNumber(extraLinearSpeed, false, null);
			if (characterObject != null && extraLinearSpeed > 0f)
			{
				SkillObject relevantSkill = currentUsageItem.RelevantSkill;
				CharacterObject captainCharacter = attackerCaptainCharacter as CharacterObject;
				if (doesAttackerHaveMountAgent)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.NomadicTraditions, captainCharacter, ref explainedNumber);
				}
				else
				{
					if (relevantSkill == DefaultSkills.TwoHanded)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.RecklessCharge, characterObject, true, ref explainedNumber);
					}
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Roguery.DashAndSlash, characterObject, true, ref explainedNumber);
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.SurgingBlow, characterObject, true, ref explainedNumber);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.SurgingBlow, captainCharacter, ref explainedNumber);
				}
				if (relevantSkill == DefaultSkills.Polearm)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Lancer, captainCharacter, ref explainedNumber);
					if (doesAttackerHaveMountAgent)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.Lancer, characterObject, true, ref explainedNumber);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.UnstoppableForce, captainCharacter, ref explainedNumber);
					}
				}
			}
			float num = CombatStatCalculator.CalculateStrikeMagnitudeForThrust(thrustWeaponSpeed, weight, explainedNumber.ResultNumber, isThrown);
			if (item.IsCraftedByPlayer)
			{
				ExplainedNumber explainedNumber2 = new ExplainedNumber(num, false, null);
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crafting.SharpenedTip, characterObject, true, ref explainedNumber2);
				num = explainedNumber2.ResultNumber;
			}
			return num;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0002AF10 File Offset: 0x00029110
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
				Debug.FailedAssert("Given damage type is invalid.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\GameComponents\\SandboxStrikeMagnitudeModel.cs", "ComputeRawDamage", 210);
				return 0f;
			}
			num3 += (1f - bluntDamageFactorByDamageType) * num4;
			return num3 * absorbedDamageRatio;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0002AFC8 File Offset: 0x000291C8
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

		// Token: 0x060005EB RID: 1515 RVA: 0x0002B008 File Offset: 0x00029208
		public override float CalculateAdjustedArmorForBlow(float baseArmor, BasicCharacterObject attackerCharacter, BasicCharacterObject attackerCaptainCharacter, BasicCharacterObject victimCharacter, BasicCharacterObject victimCaptainCharacter, WeaponComponentData weaponComponent)
		{
			bool flag = false;
			float num = baseArmor;
			CharacterObject characterObject = attackerCharacter as CharacterObject;
			CharacterObject characterObject2 = attackerCaptainCharacter as CharacterObject;
			if (attackerCharacter == characterObject2)
			{
				characterObject2 = null;
			}
			if (num > 0f && characterObject != null)
			{
				if (weaponComponent != null && weaponComponent.RelevantSkill == DefaultSkills.Crossbow && characterObject.GetPerkValue(DefaultPerks.Crossbow.Piercer) && baseArmor < DefaultPerks.Crossbow.Piercer.PrimaryBonus)
				{
					flag = true;
				}
				if (flag)
				{
					num = 0f;
				}
				else
				{
					ExplainedNumber explainedNumber = new ExplainedNumber(baseArmor, false, null);
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.Vandal, characterObject, true, ref explainedNumber);
					if (weaponComponent != null)
					{
						if (weaponComponent.RelevantSkill == DefaultSkills.OneHanded)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.ChinkInTheArmor, characterObject, true, ref explainedNumber);
						}
						else if (weaponComponent.RelevantSkill == DefaultSkills.Bow)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.Bodkin, characterObject, true, ref explainedNumber);
							if (characterObject2 != null)
							{
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.Bodkin, characterObject2, ref explainedNumber);
							}
						}
						else if (weaponComponent.RelevantSkill == DefaultSkills.Crossbow)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Puncture, characterObject, true, ref explainedNumber);
							if (characterObject2 != null)
							{
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.Puncture, characterObject2, ref explainedNumber);
							}
						}
						else if (weaponComponent.RelevantSkill == DefaultSkills.Throwing)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.WeakSpot, characterObject, true, ref explainedNumber);
							if (characterObject2 != null)
							{
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.WeakSpot, characterObject2, ref explainedNumber);
							}
						}
					}
					float num2 = explainedNumber.ResultNumber - baseArmor;
					num = MathF.Max(0f, baseArmor - num2);
				}
			}
			return num;
		}
	}
}
