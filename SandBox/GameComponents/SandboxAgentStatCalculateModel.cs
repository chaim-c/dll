using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.GameComponents
{
	// Token: 0x02000094 RID: 148
	public class SandboxAgentStatCalculateModel : AgentStatCalculateModel
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x00026B84 File Offset: 0x00024D84
		public override float GetDifficultyModifier()
		{
			Campaign campaign = Campaign.Current;
			float? num;
			if (campaign == null)
			{
				num = null;
			}
			else
			{
				GameModels models = campaign.Models;
				if (models == null)
				{
					num = null;
				}
				else
				{
					DifficultyModel difficultyModel = models.DifficultyModel;
					num = ((difficultyModel != null) ? new float?(difficultyModel.GetCombatAIDifficultyMultiplier()) : null);
				}
			}
			float? num2 = num;
			if (num2 == null)
			{
				return 0.5f;
			}
			return num2.GetValueOrDefault();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00026BEE File Offset: 0x00024DEE
		public override bool CanAgentRideMount(Agent agent, Agent targetMount)
		{
			return agent.CheckSkillForMounting(targetMount);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00026BF8 File Offset: 0x00024DF8
		public override void InitializeAgentStats(Agent agent, Equipment spawnEquipment, AgentDrivenProperties agentDrivenProperties, AgentBuildData agentBuildData)
		{
			agentDrivenProperties.ArmorEncumbrance = this.GetEffectiveArmorEncumbrance(agent, spawnEquipment);
			if (agent.IsHero)
			{
				CharacterObject characterObject = agent.Character as CharacterObject;
				AgentFlag agentFlag = agent.GetAgentFlags();
				if (characterObject.GetPerkValue(DefaultPerks.Bow.HorseMaster))
				{
					agentFlag |= AgentFlag.CanUseAllBowsMounted;
				}
				if (characterObject.GetPerkValue(DefaultPerks.Crossbow.MountedCrossbowman))
				{
					agentFlag |= AgentFlag.CanReloadAllXBowsMounted;
				}
				if (characterObject.GetPerkValue(DefaultPerks.TwoHanded.ProjectileDeflection))
				{
					agentFlag |= AgentFlag.CanDeflectArrowsWith2HSword;
				}
				agent.SetAgentFlags(agentFlag);
			}
			else
			{
				agent.HealthLimit = this.GetEffectiveMaxHealth(agent);
				agent.Health = agent.HealthLimit;
			}
			this.UpdateAgentStats(agent, agentDrivenProperties);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00026C98 File Offset: 0x00024E98
		public override void InitializeMissionEquipment(Agent agent)
		{
			if (agent.IsHuman)
			{
				CharacterObject characterObject = agent.Character as CharacterObject;
				if (characterObject != null)
				{
					object obj;
					if (agent == null)
					{
						obj = null;
					}
					else
					{
						IAgentOriginBase origin = agent.Origin;
						obj = ((origin != null) ? origin.BattleCombatant : null);
					}
					PartyBase partyBase = (PartyBase)obj;
					MapEvent mapEvent = (partyBase != null) ? partyBase.MapEvent : null;
					MobileParty mobileParty = (partyBase != null && partyBase.IsMobile) ? partyBase.MobileParty : null;
					CharacterObject characterObject2 = PartyBaseHelper.GetVisualPartyLeader(partyBase);
					if (characterObject2 == characterObject)
					{
						characterObject2 = null;
					}
					MissionEquipment equipment = agent.Equipment;
					for (int i = 0; i < 5; i++)
					{
						EquipmentIndex equipmentIndex = (EquipmentIndex)i;
						MissionWeapon missionWeapon = equipment[equipmentIndex];
						if (!missionWeapon.IsEmpty)
						{
							WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
							if (currentUsageItem != null)
							{
								if (currentUsageItem.IsConsumable && currentUsageItem.RelevantSkill != null)
								{
									ExplainedNumber explainedNumber = new ExplainedNumber(0f, false, null);
									if (currentUsageItem.RelevantSkill == DefaultSkills.Bow)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.DeepQuivers, characterObject, true, ref explainedNumber);
										if (characterObject2 != null && characterObject2.GetPerkValue(DefaultPerks.Bow.DeepQuivers))
										{
											explainedNumber.Add(DefaultPerks.Bow.DeepQuivers.SecondaryBonus, null, null);
										}
									}
									else if (currentUsageItem.RelevantSkill == DefaultSkills.Crossbow)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Fletcher, characterObject, true, ref explainedNumber);
										if (characterObject2 != null && characterObject2.GetPerkValue(DefaultPerks.Crossbow.Fletcher))
										{
											explainedNumber.Add(DefaultPerks.Crossbow.Fletcher.SecondaryBonus, null, null);
										}
									}
									else if (currentUsageItem.RelevantSkill == DefaultSkills.Throwing)
									{
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.WellPrepared, characterObject, true, ref explainedNumber);
										PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.Resourceful, characterObject, true, ref explainedNumber);
										if (agent.HasMount)
										{
											PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.Saddlebags, characterObject, true, ref explainedNumber);
										}
										PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.WellPrepared, mobileParty, false, ref explainedNumber);
									}
									int num = MathF.Round(explainedNumber.ResultNumber);
									ExplainedNumber explainedNumber2 = new ExplainedNumber((float)((int)missionWeapon.Amount + num), false, null);
									if (mobileParty != null && mapEvent != null && mapEvent.AttackerSide == partyBase.MapEventSide && mapEvent.EventType == MapEvent.BattleTypes.Siege)
									{
										PerkHelper.AddPerkBonusForParty(DefaultPerks.Engineering.MilitaryPlanner, mobileParty, true, ref explainedNumber2);
									}
									int num2 = MathF.Round(explainedNumber2.ResultNumber);
									if (num2 != (int)missionWeapon.Amount)
									{
										equipment.SetAmountOfSlot(equipmentIndex, (short)num2, true);
									}
								}
								else if (currentUsageItem.IsShield)
								{
									ExplainedNumber explainedNumber3 = new ExplainedNumber((float)missionWeapon.HitPoints, false, null);
									PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Engineering.Scaffolds, characterObject, false, ref explainedNumber3);
									int num3 = MathF.Round(explainedNumber3.ResultNumber);
									if (num3 != (int)missionWeapon.HitPoints)
									{
										equipment.SetHitPointsOfSlot(equipmentIndex, (short)num3, true);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00026F21 File Offset: 0x00025121
		public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
		{
			if (agent.IsHuman)
			{
				this.UpdateHumanStats(agent, agentDrivenProperties);
				return;
			}
			this.UpdateHorseStats(agent, agentDrivenProperties);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00026F3C File Offset: 0x0002513C
		public override int GetEffectiveSkill(Agent agent, SkillObject skill)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber((float)base.GetEffectiveSkill(agent, skill), false, null);
			CharacterObject characterObject = agent.Character as CharacterObject;
			Formation formation = agent.Formation;
			IAgentOriginBase origin = agent.Origin;
			PartyBase partyBase = (PartyBase)((origin != null) ? origin.BattleCombatant : null);
			MobileParty mobileParty = (partyBase != null && partyBase.IsMobile) ? partyBase.MobileParty : null;
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
			if (characterObject2 == characterObject)
			{
				characterObject2 = null;
			}
			if (characterObject2 != null)
			{
				bool flag = skill == DefaultSkills.Bow || skill == DefaultSkills.Crossbow || skill == DefaultSkills.Throwing;
				bool flag2 = skill == DefaultSkills.OneHanded || skill == DefaultSkills.TwoHanded || skill == DefaultSkills.Polearm;
				if ((characterObject.IsInfantry && flag) || (characterObject.IsRanged && flag2))
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.FlexibleFighter, characterObject2, ref explainedNumber);
				}
			}
			if (skill == DefaultSkills.Bow)
			{
				if (characterObject2 != null)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.DeadAim, characterObject2, ref explainedNumber);
					if (characterObject.HasMount())
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.HorseMaster, characterObject2, ref explainedNumber);
					}
				}
			}
			else if (skill == DefaultSkills.Throwing)
			{
				if (characterObject2 != null)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.StrongArms, characterObject2, ref explainedNumber);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.RunningThrow, characterObject2, ref explainedNumber);
				}
			}
			else if (skill == DefaultSkills.Crossbow && characterObject2 != null)
			{
				PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.DonkeysSwiftness, characterObject2, ref explainedNumber);
			}
			if (mobileParty != null && mobileParty.HasPerk(DefaultPerks.Roguery.OneOfTheFamily, false) && characterObject.Occupation == Occupation.Bandit && (skill.CharacterAttribute == DefaultCharacterAttributes.Vigor || skill.CharacterAttribute == DefaultCharacterAttributes.Control))
			{
				explainedNumber.Add(DefaultPerks.Roguery.OneOfTheFamily.PrimaryBonus, DefaultPerks.Roguery.OneOfTheFamily.Name, null);
			}
			if (characterObject.HasMount())
			{
				if (skill == DefaultSkills.Riding && characterObject2 != null)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.NimbleSteed, characterObject2, ref explainedNumber);
				}
			}
			else
			{
				if (mobileParty != null && formation != null)
				{
					bool flag3 = skill == DefaultSkills.OneHanded || skill == DefaultSkills.TwoHanded || skill == DefaultSkills.Polearm;
					bool flag4 = formation.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall;
					if (flag3 && flag4)
					{
						PerkHelper.AddPerkBonusForParty(DefaultPerks.Polearm.Phalanx, mobileParty, true, ref explainedNumber);
					}
				}
				if (characterObject2 != null)
				{
					if (skill == DefaultSkills.OneHanded)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.WrappedHandles, characterObject2, ref explainedNumber);
					}
					else if (skill == DefaultSkills.TwoHanded)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.StrongGrip, characterObject2, ref explainedNumber);
					}
					else if (skill == DefaultSkills.Polearm)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.CleanThrust, characterObject2, ref explainedNumber);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.CounterWeight, characterObject2, ref explainedNumber);
					}
				}
			}
			return (int)explainedNumber.ResultNumber;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000271C4 File Offset: 0x000253C4
		public override float GetWeaponDamageMultiplier(Agent agent, WeaponComponentData weapon)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);
			SkillObject skillObject = (weapon != null) ? weapon.RelevantSkill : null;
			CharacterObject character;
			if ((character = (agent.Character as CharacterObject)) != null && skillObject != null)
			{
				if (skillObject == DefaultSkills.OneHanded)
				{
					int effectiveSkill = this.GetEffectiveSkill(agent, skillObject);
					SkillHelper.AddSkillBonusForCharacter(skillObject, DefaultSkillEffects.OneHandedDamage, character, ref explainedNumber, effectiveSkill, true, 0);
				}
				else if (skillObject == DefaultSkills.TwoHanded)
				{
					int effectiveSkill2 = this.GetEffectiveSkill(agent, skillObject);
					SkillHelper.AddSkillBonusForCharacter(skillObject, DefaultSkillEffects.TwoHandedDamage, character, ref explainedNumber, effectiveSkill2, true, 0);
				}
				else if (skillObject == DefaultSkills.Polearm)
				{
					int effectiveSkill3 = this.GetEffectiveSkill(agent, skillObject);
					SkillHelper.AddSkillBonusForCharacter(skillObject, DefaultSkillEffects.PolearmDamage, character, ref explainedNumber, effectiveSkill3, true, 0);
				}
				else if (skillObject == DefaultSkills.Bow)
				{
					int effectiveSkill4 = this.GetEffectiveSkill(agent, skillObject);
					SkillHelper.AddSkillBonusForCharacter(skillObject, DefaultSkillEffects.BowDamage, character, ref explainedNumber, effectiveSkill4, true, 0);
				}
				else if (skillObject == DefaultSkills.Throwing)
				{
					int effectiveSkill5 = this.GetEffectiveSkill(agent, skillObject);
					SkillHelper.AddSkillBonusForCharacter(skillObject, DefaultSkillEffects.ThrowingDamage, character, ref explainedNumber, effectiveSkill5, true, 0);
				}
			}
			return Math.Max(0f, explainedNumber.ResultNumber);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000272D4 File Offset: 0x000254D4
		public override float GetKnockBackResistance(Agent agent)
		{
			if (agent.IsHuman)
			{
				int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Athletics);
				float val = DefaultSkillEffects.KnockBackResistance.GetPrimaryValue(effectiveSkill) * 0.01f;
				return Math.Max(0f, val);
			}
			return float.MaxValue;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002731C File Offset: 0x0002551C
		public override float GetKnockDownResistance(Agent agent, StrikeType strikeType = StrikeType.Invalid)
		{
			if (agent.IsHuman)
			{
				int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Athletics);
				float num = DefaultSkillEffects.KnockDownResistance.GetPrimaryValue(effectiveSkill) * 0.01f;
				if (agent.HasMount)
				{
					num += 0.1f;
				}
				else if (strikeType == StrikeType.Thrust)
				{
					num += 0.15f;
				}
				return Math.Max(0f, num);
			}
			return float.MaxValue;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00027380 File Offset: 0x00025580
		public override float GetDismountResistance(Agent agent)
		{
			if (agent.IsHuman)
			{
				int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Riding);
				float val = DefaultSkillEffects.DismountResistance.GetPrimaryValue(effectiveSkill) * 0.01f;
				return Math.Max(0f, val);
			}
			return float.MaxValue;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000273C8 File Offset: 0x000255C8
		public override float GetWeaponInaccuracy(Agent agent, WeaponComponentData weapon, int weaponSkill)
		{
			CharacterObject characterObject = agent.Character as CharacterObject;
			Formation formation = agent.Formation;
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
			if (characterObject == characterObject2)
			{
				characterObject2 = null;
			}
			float a = 0f;
			if (weapon.IsRangedWeapon)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);
				if (characterObject != null)
				{
					if (weapon.RelevantSkill == DefaultSkills.Bow)
					{
						SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Bow, DefaultSkillEffects.BowAccuracy, characterObject, ref explainedNumber, weaponSkill, false, 0);
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.QuickAdjustments, characterObject2, ref explainedNumber);
					}
					else if (weapon.RelevantSkill == DefaultSkills.Crossbow)
					{
						SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Crossbow, DefaultSkillEffects.CrossbowAccuracy, characterObject, ref explainedNumber, weaponSkill, false, 0);
					}
					else if (weapon.RelevantSkill == DefaultSkills.Throwing)
					{
						SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Throwing, DefaultSkillEffects.ThrowingAccuracy, characterObject, ref explainedNumber, weaponSkill, false, 0);
					}
				}
				a = (100f - (float)weapon.Accuracy) * explainedNumber.ResultNumber * 0.001f;
			}
			else if (weapon.WeaponFlags.HasAllFlags(WeaponFlags.WideGrip))
			{
				a = 1f - (float)weaponSkill * 0.01f;
			}
			return MathF.Max(a, 0f);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000274EC File Offset: 0x000256EC
		public override float GetInteractionDistance(Agent agent)
		{
			CharacterObject characterObject;
			if (agent.HasMount && (characterObject = (agent.Character as CharacterObject)) != null && characterObject.GetPerkValue(DefaultPerks.Throwing.LongReach))
			{
				return 3f;
			}
			return base.GetInteractionDistance(agent);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002752C File Offset: 0x0002572C
		public override float GetMaxCameraZoom(Agent agent)
		{
			CharacterObject characterObject = agent.Character as CharacterObject;
			ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);
			if (characterObject != null)
			{
				MissionEquipment equipment = agent.Equipment;
				EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				WeaponComponentData weaponComponentData = (wieldedItemIndex != EquipmentIndex.None) ? equipment[wieldedItemIndex].CurrentUsageItem : null;
				if (weaponComponentData != null)
				{
					if (weaponComponentData.RelevantSkill == DefaultSkills.Bow)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.EagleEye, characterObject, true, ref explainedNumber);
					}
					else if (weaponComponentData.RelevantSkill == DefaultSkills.Crossbow)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.LongShots, characterObject, true, ref explainedNumber);
					}
					else if (weaponComponentData.RelevantSkill == DefaultSkills.Throwing)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.Focus, characterObject, true, ref explainedNumber);
					}
				}
			}
			return explainedNumber.ResultNumber;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000275E4 File Offset: 0x000257E4
		public List<PerkObject> GetPerksOfAgent(CharacterObject agentCharacter, SkillObject skill = null, bool filterPerkRole = false, SkillEffect.PerkRole perkRole = SkillEffect.PerkRole.Personal)
		{
			List<PerkObject> list = new List<PerkObject>();
			if (agentCharacter != null)
			{
				foreach (PerkObject perkObject in PerkObject.All)
				{
					if (agentCharacter.GetPerkValue(perkObject) && (skill == null || skill == perkObject.Skill))
					{
						if (filterPerkRole)
						{
							if (perkObject.PrimaryRole == perkRole || perkObject.SecondaryRole == perkRole)
							{
								list.Add(perkObject);
							}
						}
						else
						{
							list.Add(perkObject);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00027678 File Offset: 0x00025878
		public override string GetMissionDebugInfoForAgent(Agent agent)
		{
			string text = "";
			text += "Base: Initial stats modified only by skills\n";
			text += "Effective (Eff): Stats that are modified by perks & mission effects\n\n";
			string text2 = "{0,-20}";
			text = string.Concat(new string[]
			{
				text,
				string.Format(text2, "Name"),
				": ",
				agent.Name,
				"\n"
			});
			text = string.Concat(new object[]
			{
				text,
				string.Format(text2, "Age"),
				": ",
				(int)agent.Age,
				"\n"
			});
			text = string.Concat(new object[]
			{
				text,
				string.Format(text2, "Health"),
				": ",
				agent.Health,
				"\n"
			});
			int num = agent.IsHuman ? agent.Character.MaxHitPoints() : agent.Monster.HitPoints;
			text = string.Concat(new object[]
			{
				text,
				string.Format(text2, "Max.Health"),
				": ",
				num,
				"(Base)\n"
			});
			text = string.Concat(new object[]
			{
				text,
				string.Format(text2, ""),
				"  ",
				MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveMaxHealth(agent),
				"(Eff)\n"
			});
			text = string.Concat(new string[]
			{
				text,
				string.Format(text2, "Team"),
				": ",
				(agent.Team != null) ? (agent.Team.IsAttacker ? "Attacker" : "Defender") : "N/A",
				"\n"
			});
			if (agent.IsHuman)
			{
				string format = text2 + ": {1,4:G}, {2,4:G}";
				text += "-------------------------------------\n";
				text = text + string.Format(text2 + ": {1,4}, {2,4}", "Skills", "Base", "Eff") + "\n";
				text += "-------------------------------------\n";
				foreach (SkillObject skillObject in Skills.All)
				{
					int skillValue = agent.Character.GetSkillValue(skillObject);
					int effectiveSkill = MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(agent, skillObject);
					string str = string.Format(format, skillObject.Name, skillValue, effectiveSkill);
					text = text + str + "\n";
				}
				text += "-------------------------------------\n";
				CharacterObject agentCharacter = agent.Character as CharacterObject;
				string debugPerkInfoForAgent = this.GetDebugPerkInfoForAgent(agentCharacter, false, SkillEffect.PerkRole.Personal);
				if (debugPerkInfoForAgent.Length > 0)
				{
					text = text + string.Format(text2 + ": ", "Perks") + "\n";
					text += "-------------------------------------\n";
					text += debugPerkInfoForAgent;
					text += "-------------------------------------\n";
				}
				Formation formation = agent.Formation;
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
				CharacterObject characterObject = obj as CharacterObject;
				string debugPerkInfoForAgent2 = this.GetDebugPerkInfoForAgent(characterObject, true, SkillEffect.PerkRole.Captain);
				if (debugPerkInfoForAgent2.Length > 0)
				{
					text = string.Concat(new object[]
					{
						text,
						string.Format(text2 + ": ", "Captain Perks"),
						characterObject.Name,
						"\n"
					});
					text += "-------------------------------------\n";
					text += debugPerkInfoForAgent2;
					text += "-------------------------------------\n";
				}
				IAgentOriginBase origin = agent.Origin;
				PartyBase partyBase = (PartyBase)((origin != null) ? origin.BattleCombatant : null);
				PartyBase party;
				if (partyBase == null)
				{
					party = null;
				}
				else
				{
					MobileParty mobileParty = partyBase.MobileParty;
					party = ((mobileParty != null) ? mobileParty.Party : null);
				}
				CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(party);
				string debugPerkInfoForAgent3 = this.GetDebugPerkInfoForAgent(visualPartyLeader, true, SkillEffect.PerkRole.PartyLeader);
				if (debugPerkInfoForAgent3.Length > 0)
				{
					text = string.Concat(new object[]
					{
						text,
						string.Format(text2 + ": ", "Party Leader Perks"),
						visualPartyLeader.Name,
						"\n"
					});
					text += "-------------------------------------\n";
					text += debugPerkInfoForAgent3;
					text += "-------------------------------------\n";
				}
			}
			return text;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00027AEC File Offset: 0x00025CEC
		public float GetEffectiveArmorEncumbrance(Agent agent, Equipment equipment)
		{
			float totalWeightOfArmor = equipment.GetTotalWeightOfArmor(agent.IsHuman);
			float num = 1f;
			CharacterObject characterObject;
			if ((characterObject = (agent.Character as CharacterObject)) != null && characterObject.GetPerkValue(DefaultPerks.Athletics.FormFittingArmor))
			{
				num += DefaultPerks.Athletics.FormFittingArmor.PrimaryBonus;
			}
			return MathF.Max(0f, totalWeightOfArmor * num);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00027B44 File Offset: 0x00025D44
		public override float GetEffectiveMaxHealth(Agent agent)
		{
			if (agent.IsHero)
			{
				return (float)agent.Character.MaxHitPoints();
			}
			float baseHealthLimit = agent.BaseHealthLimit;
			ExplainedNumber explainedNumber = new ExplainedNumber(baseHealthLimit, false, null);
			if (agent.IsHuman)
			{
				CharacterObject characterObject = agent.Character as CharacterObject;
				IAgentOriginBase agentOriginBase = (agent != null) ? agent.Origin : null;
				PartyBase partyBase = (PartyBase)((agentOriginBase != null) ? agentOriginBase.BattleCombatant : null);
				MobileParty mobileParty = (partyBase != null) ? partyBase.MobileParty : null;
				CharacterObject characterObject2;
				if (mobileParty == null)
				{
					characterObject2 = null;
				}
				else
				{
					Hero leaderHero = mobileParty.LeaderHero;
					characterObject2 = ((leaderHero != null) ? leaderHero.CharacterObject : null);
				}
				CharacterObject characterObject3 = characterObject2;
				if (characterObject != null && characterObject3 != null)
				{
					PerkHelper.AddPerkBonusForParty(DefaultPerks.TwoHanded.ThickHides, mobileParty, false, ref explainedNumber);
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Polearm.HardyFrontline, mobileParty, true, ref explainedNumber);
					if (characterObject.IsRanged)
					{
						PerkHelper.AddPerkBonusForParty(DefaultPerks.Crossbow.PickedShots, mobileParty, false, ref explainedNumber);
					}
					if (!agent.HasMount)
					{
						PerkHelper.AddPerkBonusForParty(DefaultPerks.Athletics.WellBuilt, mobileParty, false, ref explainedNumber);
						PerkHelper.AddPerkBonusForParty(DefaultPerks.Polearm.HardKnock, mobileParty, false, ref explainedNumber);
						if (characterObject.IsInfantry)
						{
							PerkHelper.AddPerkBonusForParty(DefaultPerks.OneHanded.UnwaveringDefense, mobileParty, false, ref explainedNumber);
						}
					}
					if (characterObject3.GetPerkValue(DefaultPerks.Medicine.MinisterOfHealth))
					{
						int num = (int)((float)MathF.Max(characterObject3.GetSkillValue(DefaultSkills.Medicine) - Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus, 0) * DefaultPerks.Medicine.MinisterOfHealth.PrimaryBonus);
						if (num > 0)
						{
							explainedNumber.Add((float)num, null, null);
						}
					}
				}
			}
			else
			{
				Agent riderAgent = agent.RiderAgent;
				if (riderAgent != null)
				{
					CharacterObject character = ((riderAgent != null) ? riderAgent.Character : null) as CharacterObject;
					object obj;
					if (riderAgent == null)
					{
						obj = null;
					}
					else
					{
						IAgentOriginBase origin = riderAgent.Origin;
						obj = ((origin != null) ? origin.BattleCombatant : null);
					}
					PartyBase partyBase2 = (PartyBase)obj;
					MobileParty party = (partyBase2 != null) ? partyBase2.MobileParty : null;
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Medicine.Sledges, party, false, ref explainedNumber);
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.Veterinary, character, true, ref explainedNumber);
					PerkHelper.AddPerkBonusForParty(DefaultPerks.Riding.Veterinary, party, false, ref explainedNumber);
				}
			}
			return explainedNumber.ResultNumber;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00027D2C File Offset: 0x00025F2C
		public override float GetEnvironmentSpeedFactor(Agent agent)
		{
			Scene scene = agent.Mission.Scene;
			float num = 1f;
			if (!agent.Mission.Scene.IsAtmosphereIndoor)
			{
				if (agent.Mission.Scene.GetRainDensity() > 0f)
				{
					num *= 0.9f;
				}
				if (!agent.IsHuman && CampaignTime.Now.IsNightTime)
				{
					num *= 0.9f;
				}
			}
			return num;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00027D9C File Offset: 0x00025F9C
		private string GetDebugPerkInfoForAgent(CharacterObject agentCharacter, bool filterPerkRole = false, SkillEffect.PerkRole perkRole = SkillEffect.PerkRole.Personal)
		{
			string text = "";
			string format = "{0,-18}";
			if (this.GetPerksOfAgent(agentCharacter, null, filterPerkRole, perkRole).Count > 0)
			{
				foreach (SkillObject skillObject in Skills.All)
				{
					List<PerkObject> perksOfAgent = this.GetPerksOfAgent(agentCharacter, skillObject, filterPerkRole, perkRole);
					if (perksOfAgent != null && perksOfAgent.Count > 0)
					{
						string text2 = string.Format(format, skillObject.Name) + ": ";
						int num = 5;
						int num2 = 0;
						foreach (PerkObject perkObject in perksOfAgent)
						{
							string str = perkObject.Name.ToString();
							if (num2 == num)
							{
								text2 = text2 + "\n" + string.Format(format, "") + "  ";
								num2 = 0;
							}
							text2 = text2 + str + ", ";
							num2++;
						}
						text2 = text2.Remove(text2.LastIndexOf(","));
						text = text + text2 + "\n";
					}
				}
			}
			return text;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00027EF0 File Offset: 0x000260F0
		private void UpdateHumanStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
		{
			Equipment spawnEquipment = agent.SpawnEquipment;
			agentDrivenProperties.ArmorHead = spawnEquipment.GetHeadArmorSum();
			agentDrivenProperties.ArmorTorso = spawnEquipment.GetHumanBodyArmorSum();
			agentDrivenProperties.ArmorLegs = spawnEquipment.GetLegArmorSum();
			agentDrivenProperties.ArmorArms = spawnEquipment.GetArmArmorSum();
			BasicCharacterObject character = agent.Character;
			CharacterObject characterObject = character as CharacterObject;
			MissionEquipment equipment = agent.Equipment;
			float num = equipment.GetTotalWeightOfWeapons();
			float effectiveArmorEncumbrance = this.GetEffectiveArmorEncumbrance(agent, spawnEquipment);
			int weight = agent.Monster.Weight;
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			EquipmentIndex wieldedItemIndex2 = agent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex != EquipmentIndex.None)
			{
				ItemObject item = equipment[wieldedItemIndex].Item;
				WeaponComponent weaponComponent = item.WeaponComponent;
				if (weaponComponent != null)
				{
					ItemObject.ItemTypeEnum itemType = weaponComponent.GetItemType();
					bool flag = false;
					if (characterObject != null)
					{
						bool flag2 = itemType == ItemObject.ItemTypeEnum.Bow && characterObject.GetPerkValue(DefaultPerks.Bow.RangersSwiftness);
						bool flag3 = itemType == ItemObject.ItemTypeEnum.Crossbow && characterObject.GetPerkValue(DefaultPerks.Crossbow.LooseAndMove);
						flag = (flag2 || flag3);
					}
					if (!flag)
					{
						float realWeaponLength = weaponComponent.PrimaryWeapon.GetRealWeaponLength();
						num += 4f * MathF.Sqrt(realWeaponLength) * item.Weight;
					}
				}
			}
			if (wieldedItemIndex2 != EquipmentIndex.None)
			{
				ItemObject item2 = equipment[wieldedItemIndex2].Item;
				WeaponComponentData primaryWeapon = item2.PrimaryWeapon;
				if (primaryWeapon != null && primaryWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.CanBlockRanged) && (characterObject == null || !characterObject.GetPerkValue(DefaultPerks.OneHanded.ShieldBearer)))
				{
					num += 1.5f * item2.Weight;
				}
			}
			agentDrivenProperties.WeaponsEncumbrance = num;
			agentDrivenProperties.ArmorEncumbrance = effectiveArmorEncumbrance;
			float num2 = effectiveArmorEncumbrance + num;
			EquipmentIndex wieldedItemIndex3 = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			WeaponComponentData weaponComponentData = (wieldedItemIndex3 != EquipmentIndex.None) ? equipment[wieldedItemIndex3].CurrentUsageItem : null;
			EquipmentIndex wieldedItemIndex4 = agent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			WeaponComponentData secondaryItem = (wieldedItemIndex4 != EquipmentIndex.None) ? equipment[wieldedItemIndex4].CurrentUsageItem : null;
			agentDrivenProperties.SwingSpeedMultiplier = 0.93f;
			agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier = 0.93f;
			agentDrivenProperties.HandlingMultiplier = 1f;
			agentDrivenProperties.ShieldBashStunDurationMultiplier = 1f;
			agentDrivenProperties.KickStunDurationMultiplier = 1f;
			agentDrivenProperties.ReloadSpeed = 0.93f;
			agentDrivenProperties.MissileSpeedMultiplier = 1f;
			agentDrivenProperties.ReloadMovementPenaltyFactor = 1f;
			base.SetAllWeaponInaccuracy(agent, agentDrivenProperties, (int)wieldedItemIndex3, weaponComponentData);
			int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Athletics);
			int effectiveSkill2 = this.GetEffectiveSkill(agent, DefaultSkills.Riding);
			if (weaponComponentData != null)
			{
				WeaponComponentData weaponComponentData2 = weaponComponentData;
				int effectiveSkillForWeapon = this.GetEffectiveSkillForWeapon(agent, weaponComponentData2);
				if (weaponComponentData2.IsRangedWeapon)
				{
					int thrustSpeed = weaponComponentData2.ThrustSpeed;
					if (!agent.HasMount)
					{
						float num3 = MathF.Max(0f, 1f - (float)effectiveSkillForWeapon / 500f);
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty = MathF.Max(0f, 0.125f * num3);
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = MathF.Max(0f, 0.1f * num3);
					}
					else
					{
						float num4 = MathF.Max(0f, (1f - (float)effectiveSkillForWeapon / 500f) * (1f - (float)effectiveSkill2 / 1800f));
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty = MathF.Max(0f, 0.025f * num4);
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = MathF.Max(0f, 0.12f * num4);
					}
					if (weaponComponentData2.RelevantSkill == DefaultSkills.Bow)
					{
						float num5 = ((float)thrustSpeed - 45f) / 90f;
						num5 = MBMath.ClampFloat(num5, 0f, 1f);
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty *= 6f;
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty *= 4.5f / MBMath.Lerp(0.75f, 2f, num5, 1E-05f);
					}
					else if (weaponComponentData2.RelevantSkill == DefaultSkills.Throwing)
					{
						float num6 = ((float)thrustSpeed - 89f) / 13f;
						num6 = MBMath.ClampFloat(num6, 0f, 1f);
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty *= 0.5f;
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty *= 1.5f * MBMath.Lerp(1.5f, 0.8f, num6, 1E-05f);
					}
					else if (weaponComponentData2.RelevantSkill == DefaultSkills.Crossbow)
					{
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty *= 2.5f;
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty *= 1.2f;
					}
					if (weaponComponentData2.WeaponClass == WeaponClass.Bow)
					{
						agentDrivenProperties.WeaponBestAccuracyWaitTime = 0.3f + (95.75f - (float)thrustSpeed) * 0.005f;
						float num7 = ((float)thrustSpeed - 45f) / 90f;
						num7 = MBMath.ClampFloat(num7, 0f, 1f);
						agentDrivenProperties.WeaponUnsteadyBeginTime = 0.6f + (float)effectiveSkillForWeapon * 0.01f * MBMath.Lerp(2f, 4f, num7, 1E-05f);
						if (agent.IsAIControlled)
						{
							agentDrivenProperties.WeaponUnsteadyBeginTime *= 4f;
						}
						agentDrivenProperties.WeaponUnsteadyEndTime = 2f + agentDrivenProperties.WeaponUnsteadyBeginTime;
						agentDrivenProperties.WeaponRotationalAccuracyPenaltyInRadians = 0.1f;
					}
					else if (weaponComponentData2.WeaponClass == WeaponClass.Javelin || weaponComponentData2.WeaponClass == WeaponClass.ThrowingAxe || weaponComponentData2.WeaponClass == WeaponClass.ThrowingKnife)
					{
						agentDrivenProperties.WeaponBestAccuracyWaitTime = 0.2f + (89f - (float)thrustSpeed) * 0.009f;
						agentDrivenProperties.WeaponUnsteadyBeginTime = 2.5f + (float)effectiveSkillForWeapon * 0.01f;
						agentDrivenProperties.WeaponUnsteadyEndTime = 10f + agentDrivenProperties.WeaponUnsteadyBeginTime;
						agentDrivenProperties.WeaponRotationalAccuracyPenaltyInRadians = 0.025f;
					}
					else
					{
						agentDrivenProperties.WeaponBestAccuracyWaitTime = 0.1f;
						agentDrivenProperties.WeaponUnsteadyBeginTime = 0f;
						agentDrivenProperties.WeaponUnsteadyEndTime = 0f;
						agentDrivenProperties.WeaponRotationalAccuracyPenaltyInRadians = 0.1f;
					}
				}
				else if (weaponComponentData2.WeaponFlags.HasAllFlags(WeaponFlags.WideGrip))
				{
					agentDrivenProperties.WeaponUnsteadyBeginTime = 1f + (float)effectiveSkillForWeapon * 0.005f;
					agentDrivenProperties.WeaponUnsteadyEndTime = 3f + (float)effectiveSkillForWeapon * 0.01f;
				}
			}
			agentDrivenProperties.TopSpeedReachDuration = 2.5f + MathF.Max(5f - (1f + (float)effectiveSkill * 0.01f), 1f) / 3.5f - MathF.Min((float)weight / ((float)weight + num2), 0.8f);
			float num8 = 0.7f * (1f + 3f * DefaultSkillEffects.AthleticsSpeedFactor.PrimaryBonus);
			float num9 = (num8 - 0.7f) / 300f;
			float num10 = 0.7f + num9 * (float)effectiveSkill;
			float num11 = MathF.Max(0.2f * (1f - DefaultSkillEffects.AthleticsWeightFactor.GetPrimaryValue(effectiveSkill) * 0.01f), 0f) * num2 / (float)weight;
			float num12 = MBMath.ClampFloat(num10 - num11, 0f, num8);
			agentDrivenProperties.MaxSpeedMultiplier = this.GetEnvironmentSpeedFactor(agent) * num12;
			float managedParameter = TaleWorlds.Core.ManagedParameters.Instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.BipedalCombatSpeedMinMultiplier);
			float managedParameter2 = TaleWorlds.Core.ManagedParameters.Instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.BipedalCombatSpeedMaxMultiplier);
			float amount = MathF.Min(num2 / (float)weight, 1f);
			agentDrivenProperties.CombatMaxSpeedMultiplier = MathF.Min(MBMath.Lerp(managedParameter2, managedParameter, amount, 1E-05f), 1f);
			agentDrivenProperties.AttributeShieldMissileCollisionBodySizeAdder = 0.3f;
			Agent mountAgent = agent.MountAgent;
			float num13 = (mountAgent != null) ? mountAgent.GetAgentDrivenPropertyValue(DrivenProperty.AttributeRiding) : 1f;
			agentDrivenProperties.AttributeRiding = (float)effectiveSkill2 * num13;
			agentDrivenProperties.AttributeHorseArchery = MissionGameModels.Current.StrikeMagnitudeModel.CalculateHorseArcheryFactor(character);
			agentDrivenProperties.BipedalRangedReadySpeedMultiplier = TaleWorlds.Core.ManagedParameters.Instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.BipedalRangedReadySpeedMultiplier);
			agentDrivenProperties.BipedalRangedReloadSpeedMultiplier = TaleWorlds.Core.ManagedParameters.Instance.GetManagedParameter(TaleWorlds.Core.ManagedParametersEnum.BipedalRangedReloadSpeedMultiplier);
			if (characterObject != null)
			{
				if (weaponComponentData != null)
				{
					this.SetWeaponSkillEffectsOnAgent(agent, characterObject, agentDrivenProperties, weaponComponentData);
					if (agent.HasMount)
					{
						this.SetMountedPenaltiesOnAgent(agent, agentDrivenProperties, weaponComponentData);
					}
				}
				this.SetPerkAndBannerEffectsOnAgent(agent, characterObject, agentDrivenProperties, weaponComponentData);
			}
			base.SetAiRelatedProperties(agent, agentDrivenProperties, weaponComponentData, secondaryItem);
			float num14 = 1f;
			if (!agent.Mission.Scene.IsAtmosphereIndoor)
			{
				float rainDensity = agent.Mission.Scene.GetRainDensity();
				float fog = agent.Mission.Scene.GetFog();
				if (rainDensity > 0f || fog > 0f)
				{
					num14 += MathF.Min(0.3f, rainDensity + fog);
				}
				if (!MBMath.IsBetween(agent.Mission.Scene.TimeOfDay, 4f, 20.01f))
				{
					num14 += 0.1f;
				}
			}
			agentDrivenProperties.AiShooterError *= num14;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00028728 File Offset: 0x00026928
		private void UpdateHorseStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
		{
			Equipment spawnEquipment = agent.SpawnEquipment;
			EquipmentElement equipmentElement = spawnEquipment[EquipmentIndex.ArmorItemEndSlot];
			ItemObject item = equipmentElement.Item;
			EquipmentElement equipmentElement2 = spawnEquipment[EquipmentIndex.HorseHarness];
			agentDrivenProperties.AiSpeciesIndex = (int)item.Id.InternalValue;
			agentDrivenProperties.AttributeRiding = 0.8f + ((equipmentElement2.Item != null) ? 0.2f : 0f);
			float num = 0f;
			for (int i = 1; i < 12; i++)
			{
				if (spawnEquipment[i].Item != null)
				{
					num += (float)spawnEquipment[i].GetModifiedMountBodyArmor();
				}
			}
			agentDrivenProperties.ArmorTorso = num;
			int modifiedMountManeuver = equipmentElement.GetModifiedMountManeuver(equipmentElement2);
			int num2 = equipmentElement.GetModifiedMountSpeed(equipmentElement2) + 1;
			int num3 = 0;
			float environmentSpeedFactor = this.GetEnvironmentSpeedFactor(agent);
			bool flag = Campaign.Current.Models.MapWeatherModel.GetWeatherEffectOnTerrainForPosition(MobileParty.MainParty.Position2D) == MapWeatherModel.WeatherEventEffectOnTerrain.Wet;
			Agent riderAgent = agent.RiderAgent;
			if (riderAgent != null)
			{
				CharacterObject characterObject = riderAgent.Character as CharacterObject;
				Formation formation = riderAgent.Formation;
				Agent agent2 = (formation != null) ? formation.Captain : null;
				if (agent2 == riderAgent)
				{
					agent2 = null;
				}
				CharacterObject captainCharacter = ((agent2 != null) ? agent2.Character : null) as CharacterObject;
				BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation);
				ExplainedNumber explainedNumber = new ExplainedNumber((float)modifiedMountManeuver, false, null);
				ExplainedNumber explainedNumber2 = new ExplainedNumber((float)num2, false, null);
				num3 = this.GetEffectiveSkill(agent.RiderAgent, DefaultSkills.Riding);
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Riding, DefaultSkillEffects.HorseManeuver, agent.RiderAgent.Character as CharacterObject, ref explainedNumber, num3, true, 0);
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Riding, DefaultSkillEffects.HorseSpeed, agent.RiderAgent.Character as CharacterObject, ref explainedNumber2, num3, true, 0);
				if (activeBanner != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMountMovementSpeed, activeBanner, ref explainedNumber2);
				}
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.NimbleSteed, characterObject, true, ref explainedNumber);
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.SweepingWind, characterObject, true, ref explainedNumber2);
				ExplainedNumber explainedNumber3 = new ExplainedNumber(agentDrivenProperties.ArmorTorso, false, null);
				PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.ToughSteed, captainCharacter, ref explainedNumber3);
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.ToughSteed, characterObject, true, ref explainedNumber3);
				if (characterObject.GetPerkValue(DefaultPerks.Riding.TheWayOfTheSaddle))
				{
					float value = (float)MathF.Max(num3 - Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus, 0) * DefaultPerks.Riding.TheWayOfTheSaddle.PrimaryBonus;
					explainedNumber.Add(value, null, null);
				}
				if (equipmentElement2.Item == null)
				{
					explainedNumber.AddFactor(-0.1f, null);
					explainedNumber2.AddFactor(-0.1f, null);
				}
				if (flag)
				{
					explainedNumber2.AddFactor(-0.25f, null);
				}
				agentDrivenProperties.ArmorTorso = explainedNumber3.ResultNumber;
				agentDrivenProperties.MountManeuver = explainedNumber.ResultNumber;
				agentDrivenProperties.MountSpeed = environmentSpeedFactor * 0.22f * (1f + explainedNumber2.ResultNumber);
			}
			else
			{
				agentDrivenProperties.MountManeuver = (float)modifiedMountManeuver;
				agentDrivenProperties.MountSpeed = environmentSpeedFactor * 0.22f * (float)(1 + num2);
			}
			float num4 = equipmentElement.Weight / 2f + (equipmentElement2.IsEmpty ? 0f : equipmentElement2.Weight);
			agentDrivenProperties.MountDashAccelerationMultiplier = ((num4 > 200f) ? ((num4 < 300f) ? (1f - (num4 - 200f) / 111f) : 0.1f) : 1f);
			if (flag)
			{
				agentDrivenProperties.MountDashAccelerationMultiplier *= 0.75f;
			}
			agentDrivenProperties.TopSpeedReachDuration = Game.Current.BasicModels.RidingModel.CalculateAcceleration(equipmentElement, equipmentElement2, num3);
			agentDrivenProperties.MountChargeDamage = (float)equipmentElement.GetModifiedMountCharge(equipmentElement2) * 0.004f;
			agentDrivenProperties.MountDifficulty = (float)equipmentElement.Item.Difficulty;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00028AE4 File Offset: 0x00026CE4
		private void SetPerkAndBannerEffectsOnAgent(Agent agent, CharacterObject agentCharacter, AgentDrivenProperties agentDrivenProperties, WeaponComponentData equippedWeaponComponent)
		{
			Formation formation = agent.Formation;
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
			CharacterObject characterObject = obj as CharacterObject;
			Formation formation2 = agent.Formation;
			if (((formation2 != null) ? formation2.Captain : null) == agent)
			{
				characterObject = null;
			}
			ItemObject itemObject = null;
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex != EquipmentIndex.None)
			{
				itemObject = agent.Equipment[wieldedItemIndex].Item;
			}
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(agent.Formation);
			bool flag = equippedWeaponComponent != null && equippedWeaponComponent.IsRangedWeapon;
			bool flag2 = equippedWeaponComponent != null && equippedWeaponComponent.IsMeleeWeapon;
			bool flag3 = itemObject != null && itemObject.PrimaryWeapon.IsShield;
			ExplainedNumber explainedNumber = new ExplainedNumber(agentDrivenProperties.CombatMaxSpeedMultiplier, false, null);
			ExplainedNumber explainedNumber2 = new ExplainedNumber(agentDrivenProperties.MaxSpeedMultiplier, false, null);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.FleetOfFoot, agentCharacter, true, ref explainedNumber);
			ExplainedNumber explainedNumber3 = new ExplainedNumber(agentDrivenProperties.KickStunDurationMultiplier, false, null);
			PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Roguery.DirtyFighting, agentCharacter, true, ref explainedNumber3);
			agentDrivenProperties.KickStunDurationMultiplier = explainedNumber3.ResultNumber;
			if (equippedWeaponComponent != null)
			{
				ExplainedNumber explainedNumber4 = new ExplainedNumber(agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier, false, null);
				if (flag2)
				{
					ExplainedNumber explainedNumber5 = new ExplainedNumber(agentDrivenProperties.SwingSpeedMultiplier, false, null);
					ExplainedNumber explainedNumber6 = new ExplainedNumber(agentDrivenProperties.HandlingMultiplier, false, null);
					if (!agent.HasMount)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.Fury, agentCharacter, true, ref explainedNumber6);
						if (characterObject != null)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.Fury, characterObject, ref explainedNumber6);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.OnTheEdge, characterObject, ref explainedNumber5);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.BladeMaster, characterObject, ref explainedNumber5);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.SwiftSwing, characterObject, ref explainedNumber5);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.BladeMaster, characterObject, ref explainedNumber4);
						}
					}
					if (equippedWeaponComponent.RelevantSkill == DefaultSkills.OneHanded)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.SwiftStrike, agentCharacter, true, ref explainedNumber5);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.OneHanded.WayOfTheSword, agentCharacter, DefaultSkills.OneHanded, true, ref explainedNumber5, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.OneHanded.WayOfTheSword, agentCharacter, DefaultSkills.OneHanded, true, ref explainedNumber4, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.WrappedHandles, agentCharacter, true, ref explainedNumber6);
					}
					else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.TwoHanded)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.OnTheEdge, agentCharacter, true, ref explainedNumber5);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.TwoHanded.WayOfTheGreatAxe, agentCharacter, DefaultSkills.TwoHanded, true, ref explainedNumber5, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.TwoHanded.WayOfTheGreatAxe, agentCharacter, DefaultSkills.TwoHanded, true, ref explainedNumber4, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.TwoHanded.StrongGrip, agentCharacter, true, ref explainedNumber6);
					}
					else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Polearm)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.Footwork, agentCharacter, true, ref explainedNumber);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.SwiftSwing, agentCharacter, true, ref explainedNumber5);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Polearm.WayOfTheSpear, agentCharacter, DefaultSkills.Polearm, true, ref explainedNumber5, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Polearm.WayOfTheSpear, agentCharacter, DefaultSkills.Polearm, true, ref explainedNumber4, Campaign.Current.Models.CharacterDevelopmentModel.MaxSkillRequiredForEpicPerkBonus);
						if (equippedWeaponComponent.SwingDamageType != DamageTypes.Invalid)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Polearm.CounterWeight, agentCharacter, true, ref explainedNumber6);
						}
					}
					agentDrivenProperties.SwingSpeedMultiplier = explainedNumber5.ResultNumber;
					agentDrivenProperties.HandlingMultiplier = explainedNumber6.ResultNumber;
				}
				if (flag)
				{
					ExplainedNumber explainedNumber7 = new ExplainedNumber(agentDrivenProperties.WeaponInaccuracy, false, null);
					ExplainedNumber explainedNumber8 = new ExplainedNumber(agentDrivenProperties.WeaponMaxMovementAccuracyPenalty, false, null);
					ExplainedNumber explainedNumber9 = new ExplainedNumber(agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty, false, null);
					ExplainedNumber explainedNumber10 = new ExplainedNumber(agentDrivenProperties.WeaponRotationalAccuracyPenaltyInRadians, false, null);
					ExplainedNumber explainedNumber11 = new ExplainedNumber(agentDrivenProperties.WeaponUnsteadyBeginTime, false, null);
					ExplainedNumber explainedNumber12 = new ExplainedNumber(agentDrivenProperties.WeaponUnsteadyEndTime, false, null);
					ExplainedNumber explainedNumber13 = new ExplainedNumber(agentDrivenProperties.ReloadMovementPenaltyFactor, false, null);
					ExplainedNumber explainedNumber14 = new ExplainedNumber(agentDrivenProperties.ReloadSpeed, false, null);
					ExplainedNumber explainedNumber15 = new ExplainedNumber(agentDrivenProperties.MissileSpeedMultiplier, false, null);
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.NockingPoint, agentCharacter, true, ref explainedNumber13);
					if (characterObject != null)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.LooseAndMove, characterObject, ref explainedNumber2);
					}
					if (activeBanner != null)
					{
						BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedRangedAccuracyPenalty, activeBanner, ref explainedNumber7);
					}
					if (agent.HasMount)
					{
						if (agentCharacter.GetPerkValue(DefaultPerks.Riding.Sagittarius))
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.Sagittarius, agentCharacter, true, ref explainedNumber8);
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Riding.Sagittarius, agentCharacter, true, ref explainedNumber9);
						}
						if (characterObject != null && characterObject.GetPerkValue(DefaultPerks.Riding.Sagittarius))
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.Sagittarius, characterObject, ref explainedNumber8);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Riding.Sagittarius, characterObject, ref explainedNumber9);
						}
						if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Bow && agentCharacter.GetPerkValue(DefaultPerks.Bow.MountedArchery))
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.MountedArchery, agentCharacter, true, ref explainedNumber8);
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.MountedArchery, agentCharacter, true, ref explainedNumber9);
						}
						if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Throwing && agentCharacter.GetPerkValue(DefaultPerks.Throwing.MountedSkirmisher))
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.MountedSkirmisher, agentCharacter, true, ref explainedNumber8);
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.MountedSkirmisher, agentCharacter, true, ref explainedNumber9);
						}
					}
					bool flag4 = false;
					if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Bow)
					{
						flag4 = true;
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.BowControl, agentCharacter, true, ref explainedNumber8);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.RapidFire, agentCharacter, true, ref explainedNumber14);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.QuickAdjustments, agentCharacter, true, ref explainedNumber10);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.Discipline, agentCharacter, true, ref explainedNumber11);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.Discipline, agentCharacter, true, ref explainedNumber12);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Bow.QuickDraw, agentCharacter, true, ref explainedNumber4);
						if (characterObject != null)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.RapidFire, characterObject, ref explainedNumber14);
							if (!agent.HasMount)
							{
								PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Bow.NockingPoint, characterObject, ref explainedNumber2);
							}
						}
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Bow.Deadshot, agentCharacter, DefaultSkills.Bow, true, ref explainedNumber14, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus);
					}
					else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Crossbow)
					{
						flag4 = true;
						if (agent.HasMount)
						{
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Steady, agentCharacter, true, ref explainedNumber8);
							PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Steady, agentCharacter, true, ref explainedNumber10);
						}
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.WindWinder, agentCharacter, true, ref explainedNumber14);
						if (characterObject != null)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Crossbow.WindWinder, characterObject, ref explainedNumber14);
						}
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.DonkeysSwiftness, agentCharacter, true, ref explainedNumber8);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Crossbow.Marksmen, agentCharacter, true, ref explainedNumber4);
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Crossbow.MightyPull, agentCharacter, DefaultSkills.Crossbow, true, ref explainedNumber14, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus);
					}
					else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Throwing)
					{
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.QuickDraw, agentCharacter, true, ref explainedNumber14);
						PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Throwing.PerfectTechnique, agentCharacter, true, ref explainedNumber15);
						if (characterObject != null)
						{
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.QuickDraw, characterObject, ref explainedNumber14);
							PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Throwing.PerfectTechnique, characterObject, ref explainedNumber15);
						}
						PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Throwing.UnstoppableForce, agentCharacter, DefaultSkills.Throwing, true, ref explainedNumber15, Campaign.Current.Models.CharacterDevelopmentModel.MinSkillRequiredForEpicPerkBonus);
					}
					if (flag4 && Campaign.Current.Models.MapWeatherModel.GetWeatherEffectOnTerrainForPosition(MobileParty.MainParty.Position2D) == MapWeatherModel.WeatherEventEffectOnTerrain.Wet)
					{
						explainedNumber15.AddFactor(-0.2f, null);
					}
					agentDrivenProperties.ReloadMovementPenaltyFactor = explainedNumber13.ResultNumber;
					agentDrivenProperties.ReloadSpeed = explainedNumber14.ResultNumber;
					agentDrivenProperties.MissileSpeedMultiplier = explainedNumber15.ResultNumber;
					agentDrivenProperties.WeaponInaccuracy = explainedNumber7.ResultNumber;
					agentDrivenProperties.WeaponMaxMovementAccuracyPenalty = explainedNumber8.ResultNumber;
					agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = explainedNumber9.ResultNumber;
					agentDrivenProperties.WeaponUnsteadyBeginTime = explainedNumber11.ResultNumber;
					agentDrivenProperties.WeaponUnsteadyEndTime = explainedNumber12.ResultNumber;
					agentDrivenProperties.WeaponRotationalAccuracyPenaltyInRadians = explainedNumber10.ResultNumber;
				}
				agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier = explainedNumber4.ResultNumber;
			}
			if (flag3)
			{
				ExplainedNumber explainedNumber16 = new ExplainedNumber(agentDrivenProperties.AttributeShieldMissileCollisionBodySizeAdder, false, null);
				if (characterObject != null)
				{
					Formation formation3 = agent.Formation;
					if (formation3 != null && formation3.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.ShieldWall, characterObject, ref explainedNumber16);
					}
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.ArrowCatcher, characterObject, ref explainedNumber16);
				}
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.ArrowCatcher, agentCharacter, true, ref explainedNumber16);
				agentDrivenProperties.AttributeShieldMissileCollisionBodySizeAdder = explainedNumber16.ResultNumber;
				ExplainedNumber explainedNumber17 = new ExplainedNumber(agentDrivenProperties.ShieldBashStunDurationMultiplier, false, null);
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.OneHanded.Basher, agentCharacter, true, ref explainedNumber17);
				agentDrivenProperties.ShieldBashStunDurationMultiplier = explainedNumber17.ResultNumber;
			}
			else
			{
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.MorningExercise, agentCharacter, true, ref explainedNumber2);
				PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.SelfMedication, agentCharacter, false, ref explainedNumber2);
				if (!flag3 && !flag)
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Athletics.Sprint, agentCharacter, true, ref explainedNumber2);
				}
				if (equippedWeaponComponent == null && itemObject == null)
				{
					PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Roguery.FleetFooted, agentCharacter, true, ref explainedNumber2);
				}
				if (characterObject != null)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.MorningExercise, characterObject, ref explainedNumber2);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.ShieldBearer, characterObject, ref explainedNumber2);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.OneHanded.FleetOfFoot, characterObject, ref explainedNumber2);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.TwoHanded.RecklessCharge, characterObject, ref explainedNumber2);
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Polearm.Footwork, characterObject, ref explainedNumber2);
					if (agentCharacter.Tier >= 3)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.FormFittingArmor, characterObject, ref explainedNumber2);
					}
					if (agentCharacter.IsInfantry)
					{
						PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.Sprint, characterObject, ref explainedNumber2);
					}
				}
			}
			if (agent.IsHero)
			{
				ItemObject item = (Mission.Current.DoesMissionRequireCivilianEquipment ? agentCharacter.FirstCivilianEquipment : agentCharacter.FirstBattleEquipment)[EquipmentIndex.Body].Item;
				if (item != null && item.IsCivilian && agentCharacter.GetPerkValue(DefaultPerks.Roguery.SmugglerConnections))
				{
					agentDrivenProperties.ArmorTorso += DefaultPerks.Roguery.SmugglerConnections.PrimaryBonus;
				}
			}
			float num = 0f;
			float num2 = 0f;
			bool flag5 = false;
			if (characterObject != null)
			{
				if (agent.HasMount && characterObject.GetPerkValue(DefaultPerks.Riding.DauntlessSteed))
				{
					num += DefaultPerks.Riding.DauntlessSteed.SecondaryBonus;
					flag5 = true;
				}
				else if (!agent.HasMount && characterObject.GetPerkValue(DefaultPerks.Athletics.IgnorePain))
				{
					num += DefaultPerks.Athletics.IgnorePain.SecondaryBonus;
					flag5 = true;
				}
				if (characterObject.GetPerkValue(DefaultPerks.Engineering.Metallurgy))
				{
					num += DefaultPerks.Engineering.Metallurgy.SecondaryBonus;
					flag5 = true;
				}
			}
			if (!agent.HasMount && agentCharacter.GetPerkValue(DefaultPerks.Athletics.IgnorePain))
			{
				num2 += DefaultPerks.Athletics.IgnorePain.PrimaryBonus;
				flag5 = true;
			}
			if (flag5)
			{
				float num3 = 1f + num2;
				agentDrivenProperties.ArmorHead = MathF.Max(0f, (agentDrivenProperties.ArmorHead + num) * num3);
				agentDrivenProperties.ArmorTorso = MathF.Max(0f, (agentDrivenProperties.ArmorTorso + num) * num3);
				agentDrivenProperties.ArmorArms = MathF.Max(0f, (agentDrivenProperties.ArmorArms + num) * num3);
				agentDrivenProperties.ArmorLegs = MathF.Max(0f, (agentDrivenProperties.ArmorLegs + num) * num3);
			}
			if (Mission.Current != null && Mission.Current.HasValidTerrainType)
			{
				TerrainType terrainType = Mission.Current.TerrainType;
				if (terrainType == TerrainType.Snow || terrainType == TerrainType.Forest)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.ExtendedSkirmish, characterObject, ref explainedNumber2);
				}
				else if (terrainType == TerrainType.Plain || terrainType == TerrainType.Steppe || terrainType == TerrainType.Desert)
				{
					PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.DecisiveBattle, characterObject, ref explainedNumber2);
				}
			}
			if (agentCharacter.Tier >= 3 && agentCharacter.IsInfantry)
			{
				PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Athletics.FormFittingArmor, characterObject, ref explainedNumber2);
			}
			if (agent.Formation != null && agent.Formation.CountOfUnits <= 15)
			{
				PerkHelper.AddPerkBonusFromCaptain(DefaultPerks.Tactics.SmallUnitTactics, characterObject, ref explainedNumber2);
			}
			if (activeBanner != null)
			{
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedTroopMovementSpeed, activeBanner, ref explainedNumber2);
			}
			agentDrivenProperties.MaxSpeedMultiplier = explainedNumber2.ResultNumber;
			agentDrivenProperties.CombatMaxSpeedMultiplier = explainedNumber.ResultNumber;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000295CC File Offset: 0x000277CC
		private void SetWeaponSkillEffectsOnAgent(Agent agent, CharacterObject agentCharacter, AgentDrivenProperties agentDrivenProperties, WeaponComponentData equippedWeaponComponent)
		{
			int effectiveSkill = this.GetEffectiveSkill(agent, equippedWeaponComponent.RelevantSkill);
			ExplainedNumber explainedNumber = new ExplainedNumber(agentDrivenProperties.SwingSpeedMultiplier, false, null);
			ExplainedNumber explainedNumber2 = new ExplainedNumber(agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier, false, null);
			ExplainedNumber explainedNumber3 = new ExplainedNumber(agentDrivenProperties.ReloadSpeed, false, null);
			if (equippedWeaponComponent.RelevantSkill == DefaultSkills.OneHanded)
			{
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.OneHanded, DefaultSkillEffects.OneHandedSpeed, agentCharacter, ref explainedNumber, effectiveSkill, true, 0);
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.OneHanded, DefaultSkillEffects.OneHandedSpeed, agentCharacter, ref explainedNumber2, effectiveSkill, true, 0);
			}
			else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.TwoHanded)
			{
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.TwoHanded, DefaultSkillEffects.TwoHandedSpeed, agentCharacter, ref explainedNumber, effectiveSkill, true, 0);
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.TwoHanded, DefaultSkillEffects.TwoHandedSpeed, agentCharacter, ref explainedNumber2, effectiveSkill, true, 0);
			}
			else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Polearm)
			{
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Polearm, DefaultSkillEffects.PolearmSpeed, agentCharacter, ref explainedNumber, effectiveSkill, true, 0);
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Polearm, DefaultSkillEffects.PolearmSpeed, agentCharacter, ref explainedNumber2, effectiveSkill, true, 0);
			}
			else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Crossbow)
			{
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Crossbow, DefaultSkillEffects.CrossbowReloadSpeed, agentCharacter, ref explainedNumber3, effectiveSkill, true, 0);
			}
			else if (equippedWeaponComponent.RelevantSkill == DefaultSkills.Throwing)
			{
				SkillHelper.AddSkillBonusForCharacter(DefaultSkills.Throwing, DefaultSkillEffects.ThrowingSpeed, agentCharacter, ref explainedNumber2, effectiveSkill, true, 0);
			}
			agentDrivenProperties.SwingSpeedMultiplier = Math.Max(0f, explainedNumber.ResultNumber);
			agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier = Math.Max(0f, explainedNumber2.ResultNumber);
			agentDrivenProperties.ReloadSpeed = Math.Max(0f, explainedNumber3.ResultNumber);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00029758 File Offset: 0x00027958
		private void SetMountedPenaltiesOnAgent(Agent agent, AgentDrivenProperties agentDrivenProperties, WeaponComponentData equippedWeaponComponent)
		{
			int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Riding);
			float num = 0.01f * MathF.Max(0f, DefaultSkillEffects.MountedWeaponSpeedPenalty.GetPrimaryValue(effectiveSkill));
			if (num > 0f)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(agentDrivenProperties.WeaponBestAccuracyWaitTime, false, null);
				ExplainedNumber explainedNumber2 = new ExplainedNumber(agentDrivenProperties.SwingSpeedMultiplier, false, null);
				ExplainedNumber explainedNumber3 = new ExplainedNumber(agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier, false, null);
				ExplainedNumber explainedNumber4 = new ExplainedNumber(agentDrivenProperties.ReloadSpeed, false, null);
				explainedNumber2.AddFactor(-num, null);
				explainedNumber3.AddFactor(-num, null);
				explainedNumber4.AddFactor(-num, null);
				explainedNumber.AddFactor(num, null);
				agentDrivenProperties.SwingSpeedMultiplier = Math.Max(0f, explainedNumber2.ResultNumber);
				agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier = Math.Max(0f, explainedNumber3.ResultNumber);
				agentDrivenProperties.ReloadSpeed = Math.Max(0f, explainedNumber4.ResultNumber);
				agentDrivenProperties.WeaponBestAccuracyWaitTime = Math.Max(0f, explainedNumber.ResultNumber);
			}
			float num2 = 5f - (float)effectiveSkill * 0.05f;
			if (num2 > 0f)
			{
				ExplainedNumber explainedNumber5 = new ExplainedNumber(agentDrivenProperties.WeaponInaccuracy, false, null);
				explainedNumber5.AddFactor(num2, null);
				agentDrivenProperties.WeaponInaccuracy = Math.Max(0f, explainedNumber5.ResultNumber);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002989E File Offset: 0x00027A9E
		public static float CalculateMaximumSpeedMultiplier(int athletics, float baseWeight, float totalEncumbrance)
		{
			return MathF.Min((200f + (float)athletics) / 300f * (baseWeight * 2f / (baseWeight * 2f + totalEncumbrance)), 1f);
		}
	}
}
