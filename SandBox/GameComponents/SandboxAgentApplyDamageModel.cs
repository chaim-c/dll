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
	// Token: 0x02000092 RID: 146
	public class SandboxAgentApplyDamageModel : AgentApplyDamageModel
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0002551C File Offset: 0x0002371C
		public override float CalculateDamage(in AttackInformation attackInformation, in AttackCollisionData collisionData, in MissionWeapon weapon, float baseDamage)
		{
			Formation attackerFormation = attackInformation.AttackerFormation;
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(attackerFormation);
			Agent agent = attackInformation.IsAttackerAgentMount ? attackInformation.AttackerAgent.RiderAgent : attackInformation.AttackerAgent;
			CharacterObject characterObject = (attackInformation.IsAttackerAgentMount ? attackInformation.AttackerRiderAgentCharacter : attackInformation.AttackerAgentCharacter) as CharacterObject;
			CharacterObject captainCharacter = attackInformation.AttackerCaptainCharacter as CharacterObject;
			bool flag = attackInformation.IsAttackerAgentHuman && !attackInformation.DoesAttackerHaveMountAgent;
			bool flag2 = attackInformation.DoesAttackerHaveMountAgent || attackInformation.DoesAttackerHaveRiderAgent;
			CharacterObject characterObject2 = (attackInformation.IsVictimAgentMount ? attackInformation.VictimRiderAgentCharacter : attackInformation.VictimAgentCharacter) as CharacterObject;
			CharacterObject characterObject3 = attackInformation.VictimCaptainCharacter as CharacterObject;
			bool flag3 = attackInformation.IsVictimAgentHuman && !attackInformation.DoesVictimHaveMountAgent;
			bool flag4 = attackInformation.DoesVictimHaveMountAgent || attackInformation.DoesVictimHaveRiderAgent;
			Formation victimFormation = attackInformation.VictimFormation;
			BannerComponent activeBanner2 = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			MissionWeapon missionWeapon = attackInformation.VictimMainHandWeapon;
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			AttackCollisionData attackCollisionData = collisionData;
			bool flag5;
			if (!attackCollisionData.AttackBlockedWithShield)
			{
				attackCollisionData = collisionData;
				flag5 = attackCollisionData.CollidedWithShieldOnBack;
			}
			else
			{
				flag5 = true;
			}
			bool flag6 = flag5;
			float b = 0f;
			missionWeapon = weapon;
			WeaponComponentData currentUsageItem2 = missionWeapon.CurrentUsageItem;
			bool flag7 = false;
			if (currentUsageItem2 != null && currentUsageItem2.IsConsumable)
			{
				attackCollisionData = collisionData;
				if (attackCollisionData.CollidedWithShieldOnBack && characterObject2 != null && characterObject2.GetPerkValue(DefaultPerks.Crossbow.Pavise))
				{
					float num = MBMath.ClampFloat(DefaultPerks.Crossbow.Pavise.PrimaryBonus, 0f, 1f);
					flag7 = (MBRandom.RandomFloat <= num);
				}
			}
			if (!flag7)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(baseDamage, false, null);
				if (characterObject != null)
				{
					if (currentUsageItem2 != null)
					{
						if (currentUsageItem2.IsMeleeWeapon)
						{
							if (currentUsageItem2.RelevantSkill == DefaultSkills.OneHanded)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.DeadlyPurpose, characterObject, true, ref explainedNumber);
								if (flag2)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.Cavalry, characterObject, true, ref explainedNumber);
								}
								missionWeapon = attackInformation.OffHandItem;
								if (missionWeapon.IsEmpty)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.Duelist, characterObject, true, ref explainedNumber);
								}
								if (currentUsageItem2.WeaponClass == WeaponClass.Mace || currentUsageItem2.WeaponClass == WeaponClass.OneHandedAxe)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.ToBeBlunt, characterObject, true, ref explainedNumber);
								}
								if (flag6)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.Prestige, characterObject, true, ref explainedNumber);
								}
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Roguery.Carver, captainCharacter, ref explainedNumber);
								PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.OneHanded.WayOfTheSword, characterObject, DefaultSkills.OneHanded, false, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
							}
							else if (currentUsageItem2.RelevantSkill == DefaultSkills.TwoHanded)
							{
								if (flag6)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.WoodChopper, characterObject, true, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.WoodChopper, captainCharacter, ref explainedNumber);
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.ShieldBreaker, characterObject, true, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.ShieldBreaker, captainCharacter, ref explainedNumber);
								}
								if (currentUsageItem2.WeaponClass == WeaponClass.TwoHandedAxe || currentUsageItem2.WeaponClass == WeaponClass.TwoHandedMace)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.HeadBasher, characterObject, true, ref explainedNumber);
								}
								if (attackInformation.IsVictimAgentMount)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.BeastSlayer, characterObject, true, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.BeastSlayer, captainCharacter, ref explainedNumber);
								}
								if (attackInformation.AttackerHitPointRate < 0.5f)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.Berserker, characterObject, true, ref explainedNumber);
								}
								else if (attackInformation.AttackerHitPointRate > 0.9f)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.Confidence, characterObject, true, ref explainedNumber);
								}
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.BladeMaster, characterObject, true, ref explainedNumber);
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Roguery.DashAndSlash, captainCharacter, ref explainedNumber);
								PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.TwoHanded.WayOfTheGreatAxe, characterObject, DefaultSkills.TwoHanded, false, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
							}
							else if (currentUsageItem2.RelevantSkill == DefaultSkills.Polearm)
							{
								if (flag2)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.Cavalry, characterObject, true, ref explainedNumber);
								}
								else
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.Pikeman, characterObject, true, ref explainedNumber);
								}
								attackCollisionData = collisionData;
								if (attackCollisionData.StrikeType == 1)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.CleanThrust, characterObject, true, ref explainedNumber);
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.SharpenTheTip, characterObject, true, ref explainedNumber);
								}
								if (attackInformation.IsVictimAgentMount)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.SteedKiller, characterObject, true, ref explainedNumber);
									if (flag)
									{
										PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.SteedKiller, captainCharacter, ref explainedNumber);
									}
								}
								if (attackInformation.IsHeadShot)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.Guards, characterObject, true, ref explainedNumber);
								}
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Phalanx, captainCharacter, ref explainedNumber);
								PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Polearm.WayOfTheSpear, characterObject, DefaultSkills.Polearm, false, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
							}
							else if (currentUsageItem2.IsShield)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.Basher, characterObject, true, ref explainedNumber);
							}
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.Powerful, characterObject, true, ref explainedNumber);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.Powerful, captainCharacter, ref explainedNumber);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Engineering.ImprovedTools, captainCharacter, ref explainedNumber);
							missionWeapon = weapon;
							bool flag8;
							if (missionWeapon.Item != null)
							{
								missionWeapon = weapon;
								flag8 = (missionWeapon.Item.ItemType == ItemObject.ItemTypeEnum.Thrown);
							}
							else
							{
								flag8 = false;
							}
							if (flag8)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.FlexibleFighter, characterObject, true, ref explainedNumber);
							}
							if (flag2)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.MountedWarrior, characterObject, true, ref explainedNumber);
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.MountedWarrior, captainCharacter, ref explainedNumber);
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.Cavalry, captainCharacter, ref explainedNumber);
							}
							else
							{
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.DeadlyPurpose, captainCharacter, ref explainedNumber);
								attackCollisionData = collisionData;
								if (attackCollisionData.StrikeType == 1)
								{
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.SharpenTheTip, captainCharacter, ref explainedNumber);
								}
							}
							if (activeBanner != null)
							{
								BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMeleeDamage, activeBanner, ref explainedNumber);
								if (attackInformation.DoesVictimHaveMountAgent)
								{
									BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMeleeDamageAgainstMountedTroops, activeBanner, ref explainedNumber);
								}
							}
						}
						else if (currentUsageItem2.IsConsumable)
						{
							if (currentUsageItem2.RelevantSkill == DefaultSkills.Bow)
							{
								attackCollisionData = collisionData;
								if (attackCollisionData.CollisionBoneIndex != -1)
								{
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.BowControl, captainCharacter, ref explainedNumber);
									if (attackInformation.IsHeadShot)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.DeadAim, characterObject, true, ref explainedNumber);
									}
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.StrongBows, characterObject, true, ref explainedNumber);
									if (characterObject.Tier >= 3)
									{
										PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.StrongBows, captainCharacter, ref explainedNumber);
									}
									if (attackInformation.IsVictimAgentMount)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.HunterClan, characterObject, true, ref explainedNumber);
									}
									PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Bow.Deadshot, characterObject, DefaultSkills.Bow, false, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus);
									goto IL_81B;
								}
							}
							if (currentUsageItem2.RelevantSkill == DefaultSkills.Crossbow)
							{
								attackCollisionData = collisionData;
								if (attackCollisionData.CollisionBoneIndex != -1)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Engineering.TorsionEngines, characterObject, false, ref explainedNumber);
									if (attackInformation.IsVictimAgentMount)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Unhorser, characterObject, true, ref explainedNumber);
										PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.Unhorser, captainCharacter, ref explainedNumber);
									}
									if (attackInformation.IsHeadShot)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Sheriff, characterObject, true, ref explainedNumber);
									}
									if (flag3)
									{
										PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.Sheriff, captainCharacter, ref explainedNumber);
									}
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.HammerBolts, captainCharacter, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Engineering.DreadfulSieger, captainCharacter, ref explainedNumber);
									PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Crossbow.MightyPull, characterObject, DefaultSkills.Crossbow, false, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus);
									goto IL_81B;
								}
							}
							if (currentUsageItem2.RelevantSkill == DefaultSkills.Throwing)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.StrongArms, characterObject, true, ref explainedNumber);
								if (flag6)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.ShieldBreaker, characterObject, true, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.ShieldBreaker, captainCharacter, ref explainedNumber);
									if (currentUsageItem2.WeaponClass == WeaponClass.ThrowingAxe)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.Splinters, characterObject, true, ref explainedNumber);
									}
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.Splinters, captainCharacter, ref explainedNumber);
								}
								if (attackInformation.IsVictimAgentMount)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.Hunter, characterObject, true, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.Hunter, captainCharacter, ref explainedNumber);
								}
								if (flag2)
								{
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.MountedSkirmisher, captainCharacter, ref explainedNumber);
								}
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.Impale, captainCharacter, ref explainedNumber);
								if (flag4)
								{
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.KnockOff, captainCharacter, ref explainedNumber);
								}
								if (attackInformation.VictimAgentHealth <= attackInformation.VictimAgentMaxHealth * 0.5f)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.LastHit, characterObject, true, ref explainedNumber);
								}
								if (attackInformation.IsHeadShot)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.HeadHunter, characterObject, true, ref explainedNumber);
								}
								PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Throwing.UnstoppableForce, characterObject, DefaultSkills.Throwing, false, ref explainedNumber, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus);
							}
							IL_81B:
							if (flag2)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.HorseArcher, characterObject, true, ref explainedNumber);
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.HorseArcher, captainCharacter, ref explainedNumber);
							}
							if (activeBanner != null)
							{
								BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedRangedDamage, activeBanner, ref explainedNumber);
							}
						}
						missionWeapon = weapon;
						if (missionWeapon.Item != null)
						{
							missionWeapon = weapon;
							if (missionWeapon.Item.IsCivilian)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Roguery.Carver, characterObject, true, ref explainedNumber);
							}
						}
					}
					attackCollisionData = collisionData;
					if (attackCollisionData.IsHorseCharge)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.FullSpeed, characterObject, true, ref explainedNumber);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.FullSpeed, captainCharacter, ref explainedNumber);
						if (characterObject.GetPerkValue(DefaultPerks.Riding.TheWayOfTheSaddle))
						{
							float value = (float)MathF.Max(MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(agent, DefaultSkills.Riding) - Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus, 0) * DefaultPerks.Riding.TheWayOfTheSaddle.PrimaryBonus;
							explainedNumber.Add(value, null, null);
						}
						if (activeBanner != null)
						{
							BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedChargeDamage, activeBanner, ref explainedNumber);
						}
					}
					if (flag)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.HeadBasher, captainCharacter, ref explainedNumber);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.RecklessCharge, captainCharacter, ref explainedNumber);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Pikeman, captainCharacter, ref explainedNumber);
						if (flag4)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Braced, captainCharacter, ref explainedNumber);
						}
					}
					if (flag2)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Cavalry, captainCharacter, ref explainedNumber);
					}
					if (currentUsageItem2 == null)
					{
						attackCollisionData = collisionData;
						if (attackCollisionData.IsAlternativeAttack && characterObject.GetPerkValue(DefaultPerks.Athletics.StrongLegs))
						{
							explainedNumber.AddFactor(1f, null);
						}
					}
					if (flag6)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Engineering.WallBreaker, captainCharacter, ref explainedNumber);
					}
					attackCollisionData = collisionData;
					if (attackCollisionData.EntityExists)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.Vandal, captainCharacter, ref explainedNumber);
					}
					if (characterObject2 != null)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.Coaching, captainCharacter, ref explainedNumber);
						if (characterObject2.Culture.IsBandit)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.LawKeeper, captainCharacter, ref explainedNumber);
						}
						if (flag2 && flag3)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.Gensdarmes, captainCharacter, ref explainedNumber);
						}
					}
					if (characterObject.Culture.IsBandit)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Roguery.PartnersInCrime, captainCharacter, ref explainedNumber);
					}
				}
				float num2 = 1f;
				if (Mission.Current.IsSallyOutBattle)
				{
					DestructableComponent hitObjectDestructibleComponent = attackInformation.HitObjectDestructibleComponent;
					if (hitObjectDestructibleComponent != null && hitObjectDestructibleComponent.GameEntity.GetFirstScriptOfType<SiegeWeapon>() != null)
					{
						num2 *= 4.5f;
					}
				}
				explainedNumber = new ExplainedNumber(explainedNumber.ResultNumber * num2, false, null);
				if (attackInformation.DoesAttackerHaveMountAgent && (currentUsageItem2 == null || currentUsageItem2.RelevantSkill != DefaultSkills.Crossbow))
				{
					int effectiveSkill = MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(agent, DefaultSkills.Riding);
					float value2 = -0.01f * MathF.Max(0f, DefaultSkillEffects.MountedWeaponDamagePenalty.GetPrimaryValue(effectiveSkill));
					explainedNumber.AddFactor(value2, null);
				}
				if (characterObject2 != null)
				{
					if (currentUsageItem2 != null)
					{
						if (currentUsageItem2.IsConsumable)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.SkirmishPhaseMaster, characterObject2, true, ref explainedNumber);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.Skirmisher, characterObject3, ref explainedNumber);
							if (characterObject2.IsRanged)
							{
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.SkirmishPhaseMaster, characterObject3, ref explainedNumber);
							}
							if (currentUsageItem != null)
							{
								if (currentUsageItem.WeaponClass == WeaponClass.Crossbow)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.CounterFire, characterObject2, true, ref explainedNumber);
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.CounterFire, characterObject3, ref explainedNumber);
								}
								else if (currentUsageItem.RelevantSkill == DefaultSkills.Throwing)
								{
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.Skirmisher, characterObject2, true, ref explainedNumber);
								}
							}
							if (activeBanner2 != null)
							{
								BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedRangedAttackDamage, activeBanner2, ref explainedNumber);
							}
						}
						else if (currentUsageItem2.IsMeleeWeapon)
						{
							if (characterObject3 != null)
							{
								Formation victimFormation2 = attackInformation.VictimFormation;
								if (victimFormation2 != null && victimFormation2.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall)
								{
									PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.Basher, characterObject3, ref explainedNumber);
								}
							}
							if (activeBanner2 != null)
							{
								BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedMeleeAttackDamage, activeBanner2, ref explainedNumber);
							}
						}
					}
					if (flag6)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.SteelCoreShields, characterObject2, true, ref explainedNumber);
						if (flag3)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.SteelCoreShields, characterObject3, ref explainedNumber);
						}
						attackCollisionData = collisionData;
						if (attackCollisionData.AttackBlockedWithShield)
						{
							attackCollisionData = collisionData;
							if (!attackCollisionData.CorrectSideShieldBlock)
							{
								PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.ShieldWall, characterObject2, true, ref explainedNumber);
							}
						}
					}
					attackCollisionData = collisionData;
					if (attackCollisionData.IsHorseCharge)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.SureFooted, characterObject2, true, ref explainedNumber);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.Braced, characterObject2, true, ref explainedNumber);
						if (characterObject3 != null)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.SureFooted, characterObject3, ref explainedNumber);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.Braced, characterObject3, ref explainedNumber);
						}
					}
					attackCollisionData = collisionData;
					if (attackCollisionData.IsFallDamage)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.StrongLegs, characterObject2, true, ref explainedNumber);
					}
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.EliteReserves, characterObject3, ref explainedNumber);
				}
				b = explainedNumber.ResultNumber;
			}
			return MathF.Max(0f, b);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000261D8 File Offset: 0x000243D8
		public override bool DecideCrushedThrough(Agent attackerAgent, Agent defenderAgent, float totalAttackEnergy, Agent.UsageDirection attackDirection, StrikeType strikeType, WeaponComponentData defendItem, bool isPassiveUsage)
		{
			EquipmentIndex wieldedItemIndex = attackerAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex == EquipmentIndex.None)
			{
				wieldedItemIndex = attackerAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			}
			if (((wieldedItemIndex != EquipmentIndex.None) ? attackerAgent.Equipment[wieldedItemIndex].CurrentUsageItem : null) == null || isPassiveUsage || strikeType != StrikeType.Swing || attackDirection != Agent.UsageDirection.AttackUp)
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

		// Token: 0x0600059A RID: 1434 RVA: 0x00026248 File Offset: 0x00024448
		public override void DecideMissileWeaponFlags(Agent attackerAgent, MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags)
		{
			CharacterObject characterObject = ((attackerAgent != null) ? attackerAgent.Character : null) as CharacterObject;
			if (characterObject != null && missileWeapon.CurrentUsageItem.WeaponClass == WeaponClass.Javelin && characterObject.GetPerkValue(DefaultPerks.Throwing.Impale))
			{
				missileWeaponFlags |= WeaponFlags.CanPenetrateShield;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00026292 File Offset: 0x00024492
		public override bool CanWeaponIgnoreFriendlyFireChecks(WeaponComponentData weapon)
		{
			return weapon != null && weapon.IsConsumable && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanPenetrateShield) && weapon.WeaponFlags.HasAnyFlag(WeaponFlags.MultiplePenetration);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000262C8 File Offset: 0x000244C8
		public override bool CanWeaponDismount(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			CharacterObject characterObject;
			return MBMath.IsBetween((int)blow.VictimBodyPart, 0, 6) && ((!attackerAgent.HasMount && blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook)) || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanDismount)) || ((characterObject = (attackerAgent.Character as CharacterObject)) != null && ((attackerWeapon.RelevantSkill == DefaultSkills.Crossbow && attackerWeapon.IsConsumable && characterObject.GetPerkValue(DefaultPerks.Crossbow.HammerBolts)) || (attackerWeapon.RelevantSkill == DefaultSkills.Throwing && attackerWeapon.IsConsumable && characterObject.GetPerkValue(DefaultPerks.Throwing.KnockOff)))));
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002638C File Offset: 0x0002458C
		public override void CalculateDefendedBlowStunMultipliers(Agent attackerAgent, Agent defenderAgent, CombatCollisionResult collisionResult, WeaponComponentData attackerWeapon, WeaponComponentData defenderWeapon, out float attackerStunMultiplier, out float defenderStunMultiplier)
		{
			float num = 1f;
			float b = 1f;
			CharacterObject characterObject;
			if ((characterObject = (attackerAgent.Character as CharacterObject)) != null && (collisionResult == CombatCollisionResult.Blocked || collisionResult == CombatCollisionResult.Parried) && characterObject.GetPerkValue(DefaultPerks.Athletics.MightyBlow))
			{
				num += num * DefaultPerks.Athletics.MightyBlow.PrimaryBonus;
			}
			defenderStunMultiplier = MathF.Max(0f, num);
			attackerStunMultiplier = MathF.Max(0f, b);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000263F4 File Offset: 0x000245F4
		public override bool CanWeaponKnockback(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			AttackCollisionData attackCollisionData = collisionData;
			return MBMath.IsBetween((int)attackCollisionData.VictimHitBodyPart, 0, 6) && !attackerWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanKnockDown) && (attackerWeapon.IsConsumable || (blow.BlowFlag & BlowFlags.CrushThrough) != BlowFlags.None || (blow.StrikeType == StrikeType.Thrust && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.WideGrip)));
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00026464 File Offset: 0x00024664
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

		// Token: 0x060005A0 RID: 1440 RVA: 0x000264F0 File Offset: 0x000246F0
		public override float GetDismountPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			float num = 0f;
			if (blow.StrikeType == StrikeType.Swing && blow.WeaponRecord.WeaponFlags.HasAnyFlag(WeaponFlags.CanHook))
			{
				num += 0.25f;
			}
			CharacterObject characterObject;
			if (attackerWeapon != null && (characterObject = (attackerAgent.Character as CharacterObject)) != null)
			{
				if (attackerWeapon.RelevantSkill == DefaultSkills.Polearm && characterObject.GetPerkValue(DefaultPerks.Polearm.Braced))
				{
					num += DefaultPerks.Polearm.Braced.PrimaryBonus;
				}
				else if (attackerWeapon.RelevantSkill == DefaultSkills.Crossbow && attackerWeapon.IsConsumable && characterObject.GetPerkValue(DefaultPerks.Crossbow.HammerBolts))
				{
					num += DefaultPerks.Crossbow.HammerBolts.PrimaryBonus;
				}
				else if (attackerWeapon.RelevantSkill == DefaultSkills.Throwing && attackerWeapon.IsConsumable && characterObject.GetPerkValue(DefaultPerks.Throwing.KnockOff))
				{
					num += DefaultPerks.Throwing.KnockOff.PrimaryBonus;
				}
			}
			return MathF.Max(0f, num);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000265D8 File Offset: 0x000247D8
		public override float GetKnockBackPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			float num = 0f;
			CharacterObject characterObject;
			if (attackerWeapon != null && attackerWeapon.RelevantSkill == DefaultSkills.Polearm && (characterObject = (((attackerAgent != null) ? attackerAgent.Character : null) as CharacterObject)) != null && blow.StrikeType == StrikeType.Thrust && characterObject.GetPerkValue(DefaultPerks.Polearm.KeepAtBay))
			{
				num += DefaultPerks.Polearm.KeepAtBay.PrimaryBonus;
			}
			return num;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00026634 File Offset: 0x00024834
		public override float GetKnockDownPenetration(Agent attackerAgent, WeaponComponentData attackerWeapon, in Blow blow, in AttackCollisionData collisionData)
		{
			float num = 0f;
			if (attackerWeapon.WeaponClass == WeaponClass.Boulder)
			{
				num += 0.25f;
			}
			else if (attackerWeapon.IsMeleeWeapon)
			{
				CharacterObject characterObject = ((attackerAgent != null) ? attackerAgent.Character : null) as CharacterObject;
				AttackCollisionData attackCollisionData;
				if (blow.StrikeType == StrikeType.Swing)
				{
					attackCollisionData = collisionData;
					if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.Legs)
					{
						num += 0.1f;
					}
					if (characterObject != null && attackerWeapon.RelevantSkill == DefaultSkills.TwoHanded && characterObject.GetPerkValue(DefaultPerks.TwoHanded.ShowOfStrength))
					{
						num += DefaultPerks.TwoHanded.ShowOfStrength.PrimaryBonus;
					}
				}
				attackCollisionData = collisionData;
				if (attackCollisionData.VictimHitBodyPart == BoneBodyPartType.Head)
				{
					num += 0.15f;
				}
				if (characterObject != null && attackerWeapon.RelevantSkill == DefaultSkills.Polearm && characterObject.GetPerkValue(DefaultPerks.Polearm.HardKnock))
				{
					num += DefaultPerks.Polearm.HardKnock.PrimaryBonus;
				}
			}
			return num;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002670B File Offset: 0x0002490B
		public override float GetHorseChargePenetration()
		{
			return 0.4f;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00026714 File Offset: 0x00024914
		public override float CalculateStaggerThresholdDamage(Agent defenderAgent, in Blow blow)
		{
			float num = 1f;
			CharacterObject characterObject = defenderAgent.Character as CharacterObject;
			Formation formation = defenderAgent.Formation;
			object obj;
			if (formation == null)
			{
				obj = null;
			}
			else
			{
				Agent captain = formation.Captain;
				obj = ((captain != null) ? captain.Character : null);
			}
			CharacterObject characterObject2 = obj as CharacterObject;
			if (characterObject != null)
			{
				if (characterObject2 == characterObject)
				{
					characterObject2 = null;
				}
				ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);
				if (defenderAgent.HasMount)
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.DauntlessSteed, characterObject, true, ref explainedNumber);
				}
				else
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.Spartan, characterObject, true, ref explainedNumber);
				}
				WeaponComponentData currentUsageItem = defenderAgent.WieldedWeapon.CurrentUsageItem;
				if (currentUsageItem != null && currentUsageItem.WeaponClass == WeaponClass.Crossbow && defenderAgent.WieldedWeapon.IsReloading)
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.DeftHands, characterObject, true, ref explainedNumber);
					if (characterObject2 != null)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.DeftHands, characterObject2, ref explainedNumber);
					}
				}
				num = explainedNumber.ResultNumber;
			}
			TaleWorlds.Core.ManagedParametersEnum managedParameterEnum;
			if (blow.DamageType == DamageTypes.Cut)
			{
				managedParameterEnum = TaleWorlds.Core.ManagedParametersEnum.DamageInterruptAttackThresholdCut;
			}
			else if (blow.DamageType == DamageTypes.Pierce)
			{
				managedParameterEnum = TaleWorlds.Core.ManagedParametersEnum.DamageInterruptAttackThresholdPierce;
			}
			else
			{
				managedParameterEnum = TaleWorlds.Core.ManagedParametersEnum.DamageInterruptAttackThresholdBlunt;
			}
			return TaleWorlds.Core.ManagedParameters.Instance.GetManagedParameter(managedParameterEnum) * num;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002681A File Offset: 0x00024A1A
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

		// Token: 0x060005A6 RID: 1446 RVA: 0x00026858 File Offset: 0x00024A58
		public override float CalculatePassiveAttackDamage(BasicCharacterObject attackerCharacter, in AttackCollisionData collisionData, float baseDamage)
		{
			CharacterObject characterObject = attackerCharacter as CharacterObject;
			if (characterObject != null)
			{
				AttackCollisionData attackCollisionData = collisionData;
				if (attackCollisionData.AttackBlockedWithShield && characterObject.GetPerkValue(DefaultPerks.Polearm.UnstoppableForce))
				{
					baseDamage *= DefaultPerks.Polearm.UnstoppableForce.PrimaryBonus;
				}
			}
			return baseDamage;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002689C File Offset: 0x00024A9C
		public override MeleeCollisionReaction DecidePassiveAttackCollisionReaction(Agent attacker, Agent defender, bool isFatalHit)
		{
			MeleeCollisionReaction result = MeleeCollisionReaction.Bounced;
			if (isFatalHit && attacker.HasMount)
			{
				float num = 0.05f;
				CharacterObject characterObject;
				if ((characterObject = (attacker.Character as CharacterObject)) != null && characterObject.GetPerkValue(DefaultPerks.Polearm.Skewer))
				{
					num += DefaultPerks.Polearm.Skewer.PrimaryBonus;
				}
				if (MBRandom.RandomFloat < num)
				{
					result = MeleeCollisionReaction.SlicedThrough;
				}
			}
			return result;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000268F0 File Offset: 0x00024AF0
		public override float CalculateShieldDamage(in AttackInformation attackInformation, float baseDamage)
		{
			Formation victimFormation = attackInformation.VictimFormation;
			ExplainedNumber explainedNumber = new ExplainedNumber(baseDamage, false, null);
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(victimFormation);
			if (activeBanner != null)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedShieldDamage, activeBanner, ref explainedNumber);
			}
			return explainedNumber.ResultNumber;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00026938 File Offset: 0x00024B38
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
				result = (isHuman ? 1f : 0.8f);
				break;
			case BoneBodyPartType.Legs:
				result = 0.8f;
				break;
			}
			return result;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00026A51 File Offset: 0x00024C51
		public override bool DecideAgentShrugOffBlow(Agent victimAgent, AttackCollisionData collisionData, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentShrugOffBlow(victimAgent, collisionData, blow);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00026A5B File Offset: 0x00024C5B
		public override bool DecideAgentDismountedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentDismountedByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00026A69 File Offset: 0x00024C69
		public override bool DecideAgentKnockedBackByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedBackByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00026A77 File Offset: 0x00024C77
		public override bool DecideAgentKnockedDownByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideAgentKnockedDownByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00026A85 File Offset: 0x00024C85
		public override bool DecideMountRearedByBlow(Agent attackerAgent, Agent victimAgent, in AttackCollisionData collisionData, WeaponComponentData attackerWeapon, in Blow blow)
		{
			return MissionCombatMechanicsHelper.DecideMountRearedByBlow(attackerAgent, victimAgent, collisionData, attackerWeapon, blow);
		}

		// Token: 0x040002A6 RID: 678
		private const float SallyOutSiegeEngineDamageMultiplier = 4.5f;
	}
}
