using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001ED RID: 493
	public abstract class AgentStatCalculateModel : GameModel
	{
		// Token: 0x06001BB0 RID: 7088
		public abstract void InitializeAgentStats(Agent agent, Equipment spawnEquipment, AgentDrivenProperties agentDrivenProperties, AgentBuildData agentBuildData);

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0005EF06 File Offset: 0x0005D106
		public virtual void InitializeMissionEquipment(Agent agent)
		{
		}

		// Token: 0x06001BB2 RID: 7090
		public abstract void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties);

		// Token: 0x06001BB3 RID: 7091
		public abstract float GetDifficultyModifier();

		// Token: 0x06001BB4 RID: 7092
		public abstract bool CanAgentRideMount(Agent agent, Agent targetMount);

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0005EF08 File Offset: 0x0005D108
		public virtual bool HasHeavyArmor(Agent agent)
		{
			return agent.GetBaseArmorEffectivenessForBodyPart(BoneBodyPartType.Chest) >= 24f;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0005EF1B File Offset: 0x0005D11B
		public virtual float GetEffectiveMaxHealth(Agent agent)
		{
			return agent.BaseHealthLimit;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0005EF24 File Offset: 0x0005D124
		public virtual float GetEnvironmentSpeedFactor(Agent agent)
		{
			Scene scene = agent.Mission.Scene;
			float num = 1f;
			if (!scene.IsAtmosphereIndoor)
			{
				if (scene.GetRainDensity() > 0f)
				{
					num *= 0.9f;
				}
				if (!agent.IsHuman && !MBMath.IsBetween(scene.TimeOfDay, 4f, 20.01f))
				{
					num *= 0.9f;
				}
			}
			return num;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0005EF88 File Offset: 0x0005D188
		public float CalculateAIAttackOnDecideMaxValue()
		{
			if (this.GetDifficultyModifier() < 0.5f)
			{
				return 0.16f;
			}
			return 0.48f;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
		public virtual float GetWeaponInaccuracy(Agent agent, WeaponComponentData weapon, int weaponSkill)
		{
			float a = 0f;
			if (weapon.IsRangedWeapon)
			{
				a = (100f - (float)weapon.Accuracy) * (1f - 0.002f * (float)weaponSkill) * 0.001f;
			}
			else if (weapon.WeaponFlags.HasAllFlags(WeaponFlags.WideGrip))
			{
				a = 1f - (float)weaponSkill * 0.01f;
			}
			return MathF.Max(a, 0f);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0005F00E File Offset: 0x0005D20E
		public virtual float GetDetachmentCostMultiplierOfAgent(Agent agent, IDetachment detachment)
		{
			if (agent.Banner != null)
			{
				return 10f;
			}
			return 1f;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0005F023 File Offset: 0x0005D223
		public virtual float GetInteractionDistance(Agent agent)
		{
			return 1.5f;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0005F02A File Offset: 0x0005D22A
		public virtual float GetMaxCameraZoom(Agent agent)
		{
			return 1f;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0005F031 File Offset: 0x0005D231
		public virtual int GetEffectiveSkill(Agent agent, SkillObject skill)
		{
			return agent.Character.GetSkillValue(skill);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0005F03F File Offset: 0x0005D23F
		public virtual int GetEffectiveSkillForWeapon(Agent agent, WeaponComponentData weapon)
		{
			return this.GetEffectiveSkill(agent, weapon.RelevantSkill);
		}

		// Token: 0x06001BBF RID: 7103
		public abstract float GetWeaponDamageMultiplier(Agent agent, WeaponComponentData weapon);

		// Token: 0x06001BC0 RID: 7104
		public abstract float GetKnockBackResistance(Agent agent);

		// Token: 0x06001BC1 RID: 7105
		public abstract float GetKnockDownResistance(Agent agent, StrikeType strikeType = StrikeType.Invalid);

		// Token: 0x06001BC2 RID: 7106
		public abstract float GetDismountResistance(Agent agent);

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0005F04E File Offset: 0x0005D24E
		public virtual string GetMissionDebugInfoForAgent(Agent agent)
		{
			return "Debug info not supported in this model";
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0005F055 File Offset: 0x0005D255
		public void ResetAILevelMultiplier()
		{
			this._AILevelMultiplier = 1f;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0005F062 File Offset: 0x0005D262
		public void SetAILevelMultiplier(float multiplier)
		{
			this._AILevelMultiplier = multiplier;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0005F06C File Offset: 0x0005D26C
		protected int GetMeleeSkill(Agent agent, WeaponComponentData equippedItem, WeaponComponentData secondaryItem)
		{
			SkillObject skill = DefaultSkills.Athletics;
			if (equippedItem != null)
			{
				SkillObject relevantSkill = equippedItem.RelevantSkill;
				if (relevantSkill == DefaultSkills.OneHanded || relevantSkill == DefaultSkills.Polearm)
				{
					skill = relevantSkill;
				}
				else if (relevantSkill == DefaultSkills.TwoHanded)
				{
					skill = ((secondaryItem == null) ? DefaultSkills.TwoHanded : DefaultSkills.OneHanded);
				}
				else
				{
					skill = DefaultSkills.OneHanded;
				}
			}
			return this.GetEffectiveSkill(agent, skill);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0005F0C8 File Offset: 0x0005D2C8
		protected float CalculateAILevel(Agent agent, int relevantSkillLevel)
		{
			float difficultyModifier = this.GetDifficultyModifier();
			return MBMath.ClampFloat((float)relevantSkillLevel / 300f * difficultyModifier, 0f, 1f);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0005F0F8 File Offset: 0x0005D2F8
		protected void SetAiRelatedProperties(Agent agent, AgentDrivenProperties agentDrivenProperties, WeaponComponentData equippedItem, WeaponComponentData secondaryItem)
		{
			int meleeSkill = this.GetMeleeSkill(agent, equippedItem, secondaryItem);
			SkillObject skill = (equippedItem == null) ? DefaultSkills.Athletics : equippedItem.RelevantSkill;
			int effectiveSkill = this.GetEffectiveSkill(agent, skill);
			float num = MBMath.ClampFloat(this.CalculateAILevel(agent, meleeSkill) * this._AILevelMultiplier, 0f, 1f);
			float num2 = MBMath.ClampFloat(this.CalculateAILevel(agent, effectiveSkill) * this._AILevelMultiplier, 0f, 1f);
			float num3 = num + agent.Defensiveness;
			agentDrivenProperties.AiRangedHorsebackMissileRange = 0.3f + 0.4f * num2;
			agentDrivenProperties.AiFacingMissileWatch = -0.96f + num * 0.06f;
			agentDrivenProperties.AiFlyingMissileCheckRadius = 8f - 6f * num;
			agentDrivenProperties.AiShootFreq = 0.3f + 0.7f * num2;
			agentDrivenProperties.AiWaitBeforeShootFactor = (agent.PropertyModifiers.resetAiWaitBeforeShootFactor ? 0f : (1f - 0.5f * num2));
			agentDrivenProperties.AIBlockOnDecideAbility = MBMath.Lerp(0.5f, 0.99f, MBMath.ClampFloat(MathF.Pow(num, 0.5f), 0f, 1f), 1E-05f);
			agentDrivenProperties.AIParryOnDecideAbility = MBMath.Lerp(0.5f, 0.95f, MBMath.ClampFloat(num, 0f, 1f), 1E-05f);
			agentDrivenProperties.AiTryChamberAttackOnDecide = (num - 0.15f) * 0.1f;
			agentDrivenProperties.AIAttackOnParryChance = 0.08f - 0.02f * agent.Defensiveness;
			agentDrivenProperties.AiAttackOnParryTiming = -0.2f + 0.3f * num;
			agentDrivenProperties.AIDecideOnAttackChance = 0.5f * agent.Defensiveness;
			agentDrivenProperties.AIParryOnAttackAbility = MBMath.ClampFloat(num, 0f, 1f);
			agentDrivenProperties.AiKick = -0.1f + ((num > 0.4f) ? 0.4f : num);
			agentDrivenProperties.AiAttackCalculationMaxTimeFactor = num;
			agentDrivenProperties.AiDecideOnAttackWhenReceiveHitTiming = -0.25f * (1f - num);
			agentDrivenProperties.AiDecideOnAttackContinueAction = -0.5f * (1f - num);
			agentDrivenProperties.AiDecideOnAttackingContinue = 0.1f * num;
			agentDrivenProperties.AIParryOnAttackingContinueAbility = MBMath.Lerp(0.5f, 0.95f, MBMath.ClampFloat(num, 0f, 1f), 1E-05f);
			agentDrivenProperties.AIDecideOnRealizeEnemyBlockingAttackAbility = MBMath.ClampFloat(MathF.Pow(num, 2.5f) - 0.1f, 0f, 1f);
			agentDrivenProperties.AIRealizeBlockingFromIncorrectSideAbility = MBMath.ClampFloat(MathF.Pow(num, 2.5f) - 0.01f, 0f, 1f);
			agentDrivenProperties.AiAttackingShieldDefenseChance = 0.2f + 0.3f * num;
			agentDrivenProperties.AiAttackingShieldDefenseTimer = -0.3f + 0.3f * num;
			agentDrivenProperties.AiRandomizedDefendDirectionChance = 1f - MathF.Pow(num, 3f);
			agentDrivenProperties.AiShooterError = 0.008f;
			agentDrivenProperties.AISetNoAttackTimerAfterBeingHitAbility = MBMath.Lerp(0.33f, 1f, num, 1E-05f);
			agentDrivenProperties.AISetNoAttackTimerAfterBeingParriedAbility = MBMath.Lerp(0.2f, 1f, num * num, 1E-05f);
			agentDrivenProperties.AISetNoDefendTimerAfterHittingAbility = MBMath.Lerp(0.1f, 0.99f, num * num, 1E-05f);
			agentDrivenProperties.AISetNoDefendTimerAfterParryingAbility = MBMath.Lerp(0.15f, 1f, num * num, 1E-05f);
			agentDrivenProperties.AIEstimateStunDurationPrecision = 1f - MBMath.Lerp(0.2f, 1f, num, 1E-05f);
			agentDrivenProperties.AIHoldingReadyMaxDuration = MBMath.Lerp(0.25f, 0f, MathF.Min(1f, num * 2f), 1E-05f);
			agentDrivenProperties.AIHoldingReadyVariationPercentage = num;
			agentDrivenProperties.AiRaiseShieldDelayTimeBase = -0.75f + 0.5f * num;
			agentDrivenProperties.AiUseShieldAgainstEnemyMissileProbability = 0.1f + num * 0.6f + num3 * 0.2f;
			agentDrivenProperties.AiCheckMovementIntervalFactor = 0.005f * (1.1f - num);
			agentDrivenProperties.AiMovementDelayFactor = 4f / (3f + num2);
			agentDrivenProperties.AiParryDecisionChangeValue = 0.05f + 0.7f * num;
			agentDrivenProperties.AiDefendWithShieldDecisionChanceValue = MathF.Min(2f, 0.5f + num + 0.6f * num3);
			agentDrivenProperties.AiMoveEnemySideTimeValue = -2.5f + 0.5f * num;
			agentDrivenProperties.AiMinimumDistanceToContinueFactor = 2f + 0.3f * (3f - num);
			agentDrivenProperties.AiHearingDistanceFactor = 1f + num;
			agentDrivenProperties.AiChargeHorsebackTargetDistFactor = 1.5f * (3f - num);
			agentDrivenProperties.AiWaitBeforeShootFactor = (agent.PropertyModifiers.resetAiWaitBeforeShootFactor ? 0f : (1f - 0.5f * num2));
			float num4 = 1f - num2;
			agentDrivenProperties.AiRangerLeadErrorMin = -num4 * 0.35f;
			agentDrivenProperties.AiRangerLeadErrorMax = num4 * 0.2f;
			agentDrivenProperties.AiRangerVerticalErrorMultiplier = num4 * 0.1f;
			agentDrivenProperties.AiRangerHorizontalErrorMultiplier = num4 * 0.034906585f;
			agentDrivenProperties.AIAttackOnDecideChance = MathF.Clamp(0.1f * this.CalculateAIAttackOnDecideMaxValue() * (3f - agent.Defensiveness), 0.05f, 1f);
			agentDrivenProperties.SetStat(DrivenProperty.UseRealisticBlocking, (agent.Controller != Agent.ControllerType.Player) ? 1f : 0f);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0005F611 File Offset: 0x0005D811
		protected void SetAllWeaponInaccuracy(Agent agent, AgentDrivenProperties agentDrivenProperties, int equippedIndex, WeaponComponentData equippedWeaponComponent)
		{
			if (equippedWeaponComponent != null)
			{
				agentDrivenProperties.WeaponInaccuracy = this.GetWeaponInaccuracy(agent, equippedWeaponComponent, this.GetEffectiveSkillForWeapon(agent, equippedWeaponComponent));
				return;
			}
			agentDrivenProperties.WeaponInaccuracy = 0f;
		}

		// Token: 0x04000909 RID: 2313
		protected const float MaxHorizontalErrorRadian = 0.034906585f;

		// Token: 0x0400090A RID: 2314
		private float _AILevelMultiplier = 1f;
	}
}
