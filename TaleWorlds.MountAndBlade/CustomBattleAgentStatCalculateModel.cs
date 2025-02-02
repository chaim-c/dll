using System;
using MBHelpers;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EF RID: 495
	public class CustomBattleAgentStatCalculateModel : AgentStatCalculateModel
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x0005FD12 File Offset: 0x0005DF12
		public override float GetDifficultyModifier()
		{
			return 1f;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0005FD19 File Offset: 0x0005DF19
		public override bool CanAgentRideMount(Agent agent, Agent targetMount)
		{
			return agent.CheckSkillForMounting(targetMount);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0005FD24 File Offset: 0x0005DF24
		public override void InitializeAgentStats(Agent agent, Equipment spawnEquipment, AgentDrivenProperties agentDrivenProperties, AgentBuildData agentBuildData)
		{
			agentDrivenProperties.ArmorEncumbrance = spawnEquipment.GetTotalWeightOfArmor(agent.IsHuman);
			if (agent.IsHuman)
			{
				agentDrivenProperties.ArmorHead = spawnEquipment.GetHeadArmorSum();
				agentDrivenProperties.ArmorTorso = spawnEquipment.GetHumanBodyArmorSum();
				agentDrivenProperties.ArmorLegs = spawnEquipment.GetLegArmorSum();
				agentDrivenProperties.ArmorArms = spawnEquipment.GetArmArmorSum();
				return;
			}
			agentDrivenProperties.AiSpeciesIndex = (int)spawnEquipment[EquipmentIndex.ArmorItemEndSlot].Item.Id.InternalValue;
			agentDrivenProperties.AttributeRiding = 0.8f + ((spawnEquipment[EquipmentIndex.HorseHarness].Item != null) ? 0.2f : 0f);
			float num = 0f;
			for (int i = 1; i < 12; i++)
			{
				if (spawnEquipment[i].Item != null)
				{
					num += (float)spawnEquipment[i].GetModifiedMountBodyArmor();
				}
			}
			agentDrivenProperties.ArmorTorso = num;
			ItemObject item = spawnEquipment[EquipmentIndex.ArmorItemEndSlot].Item;
			if (item != null)
			{
				HorseComponent horseComponent = item.HorseComponent;
				EquipmentElement equipmentElement = spawnEquipment[EquipmentIndex.ArmorItemEndSlot];
				EquipmentElement equipmentElement2 = spawnEquipment[EquipmentIndex.HorseHarness];
				agentDrivenProperties.MountChargeDamage = (float)equipmentElement.GetModifiedMountCharge(equipmentElement2) * 0.01f;
				agentDrivenProperties.MountDifficulty = (float)equipmentElement.Item.Difficulty;
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0005FE65 File Offset: 0x0005E065
		public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
		{
			if (agent.IsHuman)
			{
				this.UpdateHumanStats(agent, agentDrivenProperties);
				return;
			}
			this.UpdateHorseStats(agent, agentDrivenProperties);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0005FE80 File Offset: 0x0005E080
		public override float GetWeaponDamageMultiplier(Agent agent, WeaponComponentData weapon)
		{
			float num = 1f;
			SkillObject skillObject = (weapon != null) ? weapon.RelevantSkill : null;
			if (skillObject != null)
			{
				int effectiveSkill = MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(agent, skillObject);
				if (skillObject == DefaultSkills.OneHanded)
				{
					num += (float)effectiveSkill * 0.0015f;
				}
				else if (skillObject == DefaultSkills.TwoHanded)
				{
					num += (float)effectiveSkill * 0.0016f;
				}
				else if (skillObject == DefaultSkills.Polearm)
				{
					num += (float)effectiveSkill * 0.0007f;
				}
				else if (skillObject == DefaultSkills.Bow)
				{
					num += (float)effectiveSkill * 0.0011f;
				}
				else if (skillObject == DefaultSkills.Throwing)
				{
					num += (float)effectiveSkill * 0.0006f;
				}
			}
			return Math.Max(0f, num);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0005FF28 File Offset: 0x0005E128
		public override float GetKnockBackResistance(Agent agent)
		{
			if (agent.IsHuman)
			{
				int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Athletics);
				float val = 0.15f + (float)effectiveSkill * 0.001f;
				return Math.Max(0f, val);
			}
			return float.MaxValue;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0005FF6C File Offset: 0x0005E16C
		public override float GetKnockDownResistance(Agent agent, StrikeType strikeType = StrikeType.Invalid)
		{
			if (agent.IsHuman)
			{
				int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Athletics);
				float num = 0.4f + (float)effectiveSkill * 0.001f;
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

		// Token: 0x06001BEA RID: 7146 RVA: 0x0005FFCC File Offset: 0x0005E1CC
		public override float GetDismountResistance(Agent agent)
		{
			if (agent.IsHuman)
			{
				int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Riding);
				float val = 0.4f + (float)effectiveSkill * 0.001f;
				return Math.Max(0f, val);
			}
			return float.MaxValue;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0006000E File Offset: 0x0005E20E
		private int GetSkillValueForItem(Agent agent, ItemObject primaryItem)
		{
			return this.GetEffectiveSkill(agent, (primaryItem != null) ? primaryItem.RelevantSkill : DefaultSkills.Athletics);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00060028 File Offset: 0x0005E228
		private void UpdateHumanStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
		{
			BasicCharacterObject character = agent.Character;
			MissionEquipment equipment = agent.Equipment;
			float num = equipment.GetTotalWeightOfWeapons();
			int weight = agent.Monster.Weight;
			float num2 = agentDrivenProperties.ArmorEncumbrance + num;
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			EquipmentIndex wieldedItemIndex2 = agent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex != EquipmentIndex.None)
			{
				ItemObject item = equipment[wieldedItemIndex].Item;
				WeaponComponent weaponComponent = item.WeaponComponent;
				if (weaponComponent != null)
				{
					float realWeaponLength = weaponComponent.PrimaryWeapon.GetRealWeaponLength();
					num += 1.5f * item.Weight * MathF.Sqrt(realWeaponLength);
				}
			}
			if (wieldedItemIndex2 != EquipmentIndex.None)
			{
				ItemObject item2 = equipment[wieldedItemIndex2].Item;
				num += 1.5f * item2.Weight;
			}
			agentDrivenProperties.WeaponsEncumbrance = num;
			EquipmentIndex wieldedItemIndex3 = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			WeaponComponentData weaponComponentData = (wieldedItemIndex3 != EquipmentIndex.None) ? equipment[wieldedItemIndex3].CurrentUsageItem : null;
			ItemObject primaryItem = (wieldedItemIndex3 != EquipmentIndex.None) ? equipment[wieldedItemIndex3].Item : null;
			EquipmentIndex wieldedItemIndex4 = agent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			WeaponComponentData secondaryItem = (wieldedItemIndex4 != EquipmentIndex.None) ? equipment[wieldedItemIndex4].CurrentUsageItem : null;
			agentDrivenProperties.SwingSpeedMultiplier = 0.93f + 0.0007f * (float)this.GetSkillValueForItem(agent, primaryItem);
			agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier = agentDrivenProperties.SwingSpeedMultiplier;
			agentDrivenProperties.HandlingMultiplier = 1f;
			agentDrivenProperties.ShieldBashStunDurationMultiplier = 1f;
			agentDrivenProperties.KickStunDurationMultiplier = 1f;
			agentDrivenProperties.ReloadSpeed = 0.93f + 0.0007f * (float)this.GetSkillValueForItem(agent, primaryItem);
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
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty = 0.125f * num3;
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = 0.1f * num3;
					}
					else
					{
						float num4 = MathF.Max(0f, (1f - (float)effectiveSkillForWeapon / 500f) * (1f - (float)effectiveSkill2 / 1800f));
						agentDrivenProperties.WeaponMaxMovementAccuracyPenalty = 0.025f * num4;
						agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = 0.12f * num4;
					}
					agentDrivenProperties.WeaponMaxMovementAccuracyPenalty = MathF.Max(0f, agentDrivenProperties.WeaponMaxMovementAccuracyPenalty);
					agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty = MathF.Max(0f, agentDrivenProperties.WeaponMaxUnsteadyAccuracyPenalty);
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
			agentDrivenProperties.TopSpeedReachDuration = 2f / MathF.Max((200f + (float)effectiveSkill) / 300f * ((float)weight / ((float)weight + num2)), 0.3f);
			float num8 = 0.7f + 0.00070000015f * (float)effectiveSkill;
			float num9 = MathF.Max(0.2f * (1f - (float)effectiveSkill * 0.001f), 0f) * num2 / (float)weight;
			float num10 = MBMath.ClampFloat(num8 - num9, 0f, 0.91f);
			agentDrivenProperties.MaxSpeedMultiplier = this.GetEnvironmentSpeedFactor(agent) * num10;
			float managedParameter = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalCombatSpeedMinMultiplier);
			float managedParameter2 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalCombatSpeedMaxMultiplier);
			float amount = MathF.Min(num2 / (float)weight, 1f);
			agentDrivenProperties.CombatMaxSpeedMultiplier = MathF.Min(MBMath.Lerp(managedParameter2, managedParameter, amount, 1E-05f), 1f);
			agentDrivenProperties.AttributeShieldMissileCollisionBodySizeAdder = 0.3f;
			Agent mountAgent = agent.MountAgent;
			float num11 = (mountAgent != null) ? mountAgent.GetAgentDrivenPropertyValue(DrivenProperty.AttributeRiding) : 1f;
			agentDrivenProperties.AttributeRiding = (float)effectiveSkill2 * num11;
			agentDrivenProperties.AttributeHorseArchery = MissionGameModels.Current.StrikeMagnitudeModel.CalculateHorseArcheryFactor(character);
			agentDrivenProperties.BipedalRangedReadySpeedMultiplier = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRangedReadySpeedMultiplier);
			agentDrivenProperties.BipedalRangedReloadSpeedMultiplier = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.BipedalRangedReloadSpeedMultiplier);
			if (agent.Character != null)
			{
				if (agent.HasMount && weaponComponentData != null)
				{
					this.SetMountedWeaponPenaltiesOnAgent(agent, agentDrivenProperties, weaponComponentData);
				}
				this.SetBannerEffectsOnAgent(agent, agentDrivenProperties, weaponComponentData);
			}
			base.SetAiRelatedProperties(agent, agentDrivenProperties, weaponComponentData, secondaryItem);
			float num12 = 1f;
			if (!agent.Mission.Scene.IsAtmosphereIndoor)
			{
				float rainDensity = agent.Mission.Scene.GetRainDensity();
				float fog = agent.Mission.Scene.GetFog();
				if (rainDensity > 0f || fog > 0f)
				{
					num12 += MathF.Min(0.3f, rainDensity + fog);
				}
				if (!MBMath.IsBetween(agent.Mission.Scene.TimeOfDay, 4f, 20.01f))
				{
					num12 += 0.1f;
				}
			}
			agentDrivenProperties.AiShooterError *= num12;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00060780 File Offset: 0x0005E980
		private void UpdateHorseStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
		{
			Equipment spawnEquipment = agent.SpawnEquipment;
			EquipmentElement equipmentElement = spawnEquipment[EquipmentIndex.ArmorItemEndSlot];
			EquipmentElement equipmentElement2 = spawnEquipment[EquipmentIndex.HorseHarness];
			ItemObject item = equipmentElement.Item;
			float num = (float)(equipmentElement.GetModifiedMountSpeed(equipmentElement2) + 1);
			int modifiedMountManeuver = equipmentElement.GetModifiedMountManeuver(equipmentElement2);
			int num2 = 0;
			float environmentSpeedFactor = this.GetEnvironmentSpeedFactor(agent);
			if (agent.RiderAgent != null)
			{
				num2 = this.GetEffectiveSkill(agent.RiderAgent, DefaultSkills.Riding);
				FactoredNumber factoredNumber = new FactoredNumber(num);
				FactoredNumber factoredNumber2 = new FactoredNumber((float)modifiedMountManeuver);
				factoredNumber.AddFactor((float)num2 * 0.001f);
				factoredNumber2.AddFactor((float)num2 * 0.0004f);
				Formation formation = agent.RiderAgent.Formation;
				BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(formation);
				if (activeBanner != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedMountMovementSpeed, activeBanner, ref factoredNumber);
				}
				agentDrivenProperties.MountManeuver = factoredNumber2.ResultNumber;
				agentDrivenProperties.MountSpeed = environmentSpeedFactor * 0.22f * (1f + factoredNumber.ResultNumber);
			}
			else
			{
				agentDrivenProperties.MountManeuver = (float)modifiedMountManeuver;
				agentDrivenProperties.MountSpeed = environmentSpeedFactor * 0.22f * (1f + num);
			}
			float num3 = equipmentElement.Weight / 2f + (equipmentElement2.IsEmpty ? 0f : equipmentElement2.Weight);
			agentDrivenProperties.MountDashAccelerationMultiplier = ((num3 > 200f) ? ((num3 < 300f) ? (1f - (num3 - 200f) / 111f) : 0.1f) : 1f);
			agentDrivenProperties.TopSpeedReachDuration = Game.Current.BasicModels.RidingModel.CalculateAcceleration(equipmentElement, equipmentElement2, num2);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0006091C File Offset: 0x0005EB1C
		private void SetBannerEffectsOnAgent(Agent agent, AgentDrivenProperties agentDrivenProperties, WeaponComponentData equippedWeaponComponent)
		{
			BannerComponent activeBanner = MissionGameModels.Current.BattleBannerBearersModel.GetActiveBanner(agent.Formation);
			if (activeBanner != null)
			{
				bool flag = equippedWeaponComponent != null && equippedWeaponComponent.IsRangedWeapon;
				FactoredNumber factoredNumber = new FactoredNumber(agentDrivenProperties.MaxSpeedMultiplier);
				FactoredNumber factoredNumber2 = new FactoredNumber(agentDrivenProperties.WeaponInaccuracy);
				if (flag && equippedWeaponComponent != null)
				{
					BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.DecreasedRangedAccuracyPenalty, activeBanner, ref factoredNumber2);
				}
				BannerHelper.AddBannerBonusForBanner(DefaultBannerEffects.IncreasedTroopMovementSpeed, activeBanner, ref factoredNumber);
				agentDrivenProperties.MaxSpeedMultiplier = factoredNumber.ResultNumber;
				agentDrivenProperties.WeaponInaccuracy = factoredNumber2.ResultNumber;
			}
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000609A4 File Offset: 0x0005EBA4
		private void SetMountedWeaponPenaltiesOnAgent(Agent agent, AgentDrivenProperties agentDrivenProperties, WeaponComponentData equippedWeaponComponent)
		{
			int effectiveSkill = this.GetEffectiveSkill(agent, DefaultSkills.Riding);
			float num = 0.3f - (float)effectiveSkill * 0.003f;
			if (num > 0f)
			{
				float val = agentDrivenProperties.SwingSpeedMultiplier * (1f - num);
				float val2 = agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier * (1f - num);
				float val3 = agentDrivenProperties.ReloadSpeed * (1f - num);
				float val4 = agentDrivenProperties.WeaponBestAccuracyWaitTime * (1f + num);
				agentDrivenProperties.SwingSpeedMultiplier = Math.Max(0f, val);
				agentDrivenProperties.ThrustOrRangedReadySpeedMultiplier = Math.Max(0f, val2);
				agentDrivenProperties.ReloadSpeed = Math.Max(0f, val3);
				agentDrivenProperties.WeaponBestAccuracyWaitTime = Math.Max(0f, val4);
			}
			float num2 = 15f - (float)effectiveSkill * 0.15f;
			if (num2 > 0f)
			{
				float val5 = agentDrivenProperties.WeaponInaccuracy * (1f + num2);
				agentDrivenProperties.WeaponInaccuracy = Math.Max(0f, val5);
			}
		}
	}
}
