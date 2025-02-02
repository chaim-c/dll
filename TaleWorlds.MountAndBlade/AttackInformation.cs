using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000192 RID: 402
	public struct AttackInformation
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x0004DA44 File Offset: 0x0004BC44
		public AttackInformation(Agent attackerAgent, Agent victimAgent, GameEntity hitObject, in AttackCollisionData attackCollisionData, in MissionWeapon attackerWeapon)
		{
			this.AttackerAgent = attackerAgent;
			this.VictimAgent = victimAgent;
			this.IsAttackerAgentNull = (attackerAgent == null);
			this.IsVictimAgentNull = (victimAgent == null);
			this.ArmorAmountFloat = 0f;
			AttackCollisionData attackCollisionData2;
			if (!this.IsVictimAgentNull)
			{
				attackCollisionData2 = attackCollisionData;
				this.ArmorAmountFloat = victimAgent.GetBaseArmorEffectivenessForBodyPart(attackCollisionData2.VictimHitBodyPart);
			}
			this.ShieldOnBack = null;
			if (!this.IsVictimAgentNull && (victimAgent.GetAgentFlags() & AgentFlag.CanWieldWeapon) != AgentFlag.None)
			{
				EquipmentIndex wieldedItemIndex = victimAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				for (int i = 0; i < 4; i++)
				{
					WeaponComponentData currentUsageItem = victimAgent.Equipment[i].CurrentUsageItem;
					if (i != (int)wieldedItemIndex && currentUsageItem != null && currentUsageItem.IsShield)
					{
						this.ShieldOnBack = currentUsageItem;
						break;
					}
				}
			}
			this.VictimShield = MissionWeapon.Invalid;
			this.VictimMainHandWeapon = MissionWeapon.Invalid;
			if (!this.IsVictimAgentNull && (victimAgent.GetAgentFlags() & AgentFlag.CanWieldWeapon) != AgentFlag.None)
			{
				EquipmentIndex wieldedItemIndex2 = victimAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				if (wieldedItemIndex2 != EquipmentIndex.None)
				{
					this.VictimShield = victimAgent.Equipment[wieldedItemIndex2];
				}
				EquipmentIndex wieldedItemIndex3 = victimAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				if (wieldedItemIndex3 != EquipmentIndex.None)
				{
					this.VictimMainHandWeapon = victimAgent.Equipment[wieldedItemIndex3];
				}
			}
			this.AttackerAgentMountMovementDirection = default(Vec2);
			if (!this.IsAttackerAgentNull && attackerAgent.HasMount)
			{
				this.AttackerAgentMountMovementDirection = attackerAgent.MountAgent.GetMovementDirection();
			}
			this.VictimAgentMountMovementDirection = default(Vec2);
			if (!this.IsVictimAgentNull && victimAgent.HasMount)
			{
				this.VictimAgentMountMovementDirection = victimAgent.MountAgent.GetMovementDirection();
			}
			this.IsVictimAgentSameWithAttackerAgent = (!this.IsAttackerAgentNull && attackerAgent == victimAgent);
			MissionWeapon missionWeapon = attackerWeapon;
			int weaponAttachBoneIndex;
			if (missionWeapon.IsEmpty || this.IsAttackerAgentNull || !attackerAgent.IsHuman)
			{
				weaponAttachBoneIndex = -1;
			}
			else
			{
				Monster monster = attackerAgent.Monster;
				missionWeapon = attackerWeapon;
				weaponAttachBoneIndex = (int)monster.GetBoneToAttachForItemFlags(missionWeapon.Item.ItemFlags);
			}
			this.WeaponAttachBoneIndex = weaponAttachBoneIndex;
			DestructableComponent destructableComponent = (hitObject != null) ? hitObject.GetFirstScriptOfTypeInFamily<DestructableComponent>() : null;
			this.HitObjectDestructibleComponent = destructableComponent;
			bool isFriendlyFire;
			if (!this.IsAttackerAgentNull)
			{
				if (!this.IsVictimAgentSameWithAttackerAgent && (this.IsVictimAgentNull || !victimAgent.IsFriendOf(attackerAgent)))
				{
					if (destructableComponent != null)
					{
						BattleSideEnum battleSide = destructableComponent.BattleSide;
						Team team = attackerAgent.Team;
						BattleSideEnum? battleSideEnum = (team != null) ? new BattleSideEnum?(team.Side) : null;
						isFriendlyFire = (battleSide == battleSideEnum.GetValueOrDefault() & battleSideEnum != null);
					}
					else
					{
						isFriendlyFire = false;
					}
				}
				else
				{
					isFriendlyFire = true;
				}
			}
			else
			{
				isFriendlyFire = false;
			}
			this.IsFriendlyFire = isFriendlyFire;
			this.OffHandItem = default(MissionWeapon);
			if (!this.IsAttackerAgentNull && (attackerAgent.GetAgentFlags() & AgentFlag.CanWieldWeapon) != AgentFlag.None)
			{
				EquipmentIndex wieldedItemIndex4 = attackerAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				if (wieldedItemIndex4 != EquipmentIndex.None)
				{
					this.OffHandItem = attackerAgent.Equipment[wieldedItemIndex4];
				}
			}
			attackCollisionData2 = attackCollisionData;
			this.IsHeadShot = (attackCollisionData2.VictimHitBodyPart == BoneBodyPartType.Head);
			this.VictimAgentAbsorbedDamageRatio = 0f;
			this.DamageMultiplierOfBone = 0f;
			this.VictimMovementDirectionAsAngle = 0f;
			this.VictimAgentScale = 0f;
			this.VictimAgentHealth = 0f;
			this.VictimAgentMaxHealth = 0f;
			this.VictimAgentWeight = 0f;
			this.VictimAgentTotalEncumbrance = 0f;
			this.CombatDifficultyMultiplier = 1f;
			this.VictimHitPointRate = 0f;
			this.VictimAgentFlag = AgentFlag.CanAttack;
			this.IsVictimAgentLeftStance = false;
			this.DoesVictimHaveMountAgent = false;
			this.IsVictimAgentMine = false;
			this.DoesVictimHaveRiderAgent = false;
			this.IsVictimAgentRiderAgentMine = false;
			this.IsVictimAgentMount = false;
			this.IsVictimAgentHuman = false;
			this.IsVictimRiderAgentSameAsAttackerAgent = false;
			this.IsVictimPlayer = false;
			this.VictimAgentCharacter = null;
			this.VictimRiderAgentCharacter = null;
			this.VictimAgentMovementVelocity = default(Vec2);
			this.VictimAgentPosition = default(Vec3);
			this.VictimAgentVelocity = default(Vec3);
			this.VictimCaptainCharacter = null;
			this.VictimAgentOrigin = null;
			this.VictimRiderAgentOrigin = null;
			this.VictimFormation = null;
			if (!this.IsVictimAgentNull)
			{
				this.IsVictimAgentMount = victimAgent.IsMount;
				this.IsVictimAgentMine = victimAgent.IsMine;
				this.IsVictimAgentHuman = victimAgent.IsHuman;
				this.IsVictimAgentLeftStance = victimAgent.GetIsLeftStance();
				this.DoesVictimHaveMountAgent = victimAgent.HasMount;
				this.DoesVictimHaveRiderAgent = (victimAgent.RiderAgent != null);
				this.IsVictimRiderAgentSameAsAttackerAgent = (this.DoesVictimHaveRiderAgent && victimAgent.RiderAgent == attackerAgent);
				this.IsVictimPlayer = victimAgent.IsPlayerControlled;
				this.VictimAgentAbsorbedDamageRatio = victimAgent.Monster.AbsorbedDamageRatio;
				AgentApplyDamageModel agentApplyDamageModel = MissionGameModels.Current.AgentApplyDamageModel;
				attackCollisionData2 = attackCollisionData;
				BoneBodyPartType bodyPart;
				if (attackCollisionData2.CollisionBoneIndex == -1)
				{
					bodyPart = BoneBodyPartType.None;
				}
				else
				{
					MBAgentVisuals agentVisuals = victimAgent.AgentVisuals;
					attackCollisionData2 = attackCollisionData;
					bodyPart = agentVisuals.GetBoneTypeData(attackCollisionData2.CollisionBoneIndex).BodyPartType;
				}
				attackCollisionData2 = attackCollisionData;
				DamageTypes damageType = (DamageTypes)attackCollisionData2.DamageType;
				bool isVictimAgentHuman = this.IsVictimAgentHuman;
				attackCollisionData2 = attackCollisionData;
				this.DamageMultiplierOfBone = agentApplyDamageModel.GetDamageMultiplierForBodyPart(bodyPart, damageType, isVictimAgentHuman, attackCollisionData2.IsMissile);
				this.VictimMovementDirectionAsAngle = victimAgent.MovementDirectionAsAngle;
				this.VictimAgentScale = victimAgent.AgentScale;
				this.VictimAgentHealth = victimAgent.Health;
				this.VictimAgentMaxHealth = victimAgent.HealthLimit;
				this.VictimAgentWeight = (victimAgent.IsMount ? victimAgent.SpawnEquipment[EquipmentIndex.ArmorItemEndSlot].Weight : ((float)victimAgent.Monster.Weight));
				this.VictimAgentTotalEncumbrance = victimAgent.GetTotalEncumbrance();
				this.CombatDifficultyMultiplier = Mission.Current.GetDamageMultiplierOfCombatDifficulty(victimAgent, attackerAgent);
				this.VictimHitPointRate = victimAgent.Health / victimAgent.HealthLimit;
				this.VictimAgentMovementVelocity = victimAgent.MovementVelocity;
				this.VictimAgentVelocity = victimAgent.Velocity;
				this.VictimAgentPosition = victimAgent.Position;
				this.VictimAgentFlag = victimAgent.GetAgentFlags();
				this.VictimAgentCharacter = victimAgent.Character;
				this.VictimAgentOrigin = victimAgent.Origin;
				if (this.DoesVictimHaveRiderAgent)
				{
					Agent riderAgent = victimAgent.RiderAgent;
					this.IsVictimAgentRiderAgentMine = riderAgent.IsMine;
					this.VictimRiderAgentCharacter = riderAgent.Character;
					this.VictimRiderAgentOrigin = riderAgent.Origin;
					Formation formation = riderAgent.Formation;
					Agent agent = (formation != null) ? formation.Captain : null;
					this.VictimCaptainCharacter = ((riderAgent != agent) ? ((agent != null) ? agent.Character : null) : null);
					this.VictimFormation = riderAgent.Formation;
				}
				else
				{
					Formation formation2 = victimAgent.Formation;
					Agent agent2 = (formation2 != null) ? formation2.Captain : null;
					this.VictimCaptainCharacter = ((victimAgent != agent2) ? ((agent2 != null) ? agent2.Character : null) : null);
					this.VictimFormation = victimAgent.Formation;
				}
			}
			this.AttackerMovementDirectionAsAngle = 0f;
			this.AttackerAgentMountChargeDamageProperty = 0f;
			this.DoesAttackerHaveMountAgent = false;
			this.IsAttackerAgentMine = false;
			this.DoesAttackerHaveRiderAgent = false;
			this.IsAttackerAgentRiderAgentMine = false;
			this.IsAttackerAgentMount = false;
			this.IsAttackerAgentHuman = false;
			this.IsAttackerAgentActive = false;
			this.IsAttackerAgentDoingPassiveAttack = false;
			this.IsAttackerPlayer = false;
			this.AttackerAgentMovementVelocity = default(Vec2);
			this.AttackerAgentCharacter = null;
			this.AttackerRiderAgentCharacter = null;
			this.AttackerAgentOrigin = null;
			this.AttackerRiderAgentOrigin = null;
			this.AttackerAgentMovementDirection = default(Vec2);
			this.AttackerAgentVelocity = default(Vec3);
			this.AttackerAgentCurrentWeaponOffset = default(Vec3);
			this.IsAttackerAIControlled = false;
			this.AttackerCaptainCharacter = null;
			this.AttackerFormation = null;
			this.AttackerHitPointRate = 0f;
			if (!this.IsAttackerAgentNull)
			{
				this.DoesAttackerHaveMountAgent = attackerAgent.HasMount;
				this.IsAttackerAgentMine = attackerAgent.IsMine;
				this.IsAttackerAgentMount = attackerAgent.IsMount;
				this.IsAttackerAgentHuman = attackerAgent.IsHuman;
				this.IsAttackerAgentActive = attackerAgent.IsActive();
				this.IsAttackerAgentDoingPassiveAttack = attackerAgent.IsDoingPassiveAttack;
				this.DoesAttackerHaveRiderAgent = (attackerAgent.RiderAgent != null);
				this.IsAttackerAIControlled = attackerAgent.IsAIControlled;
				this.IsAttackerPlayer = attackerAgent.IsPlayerControlled;
				this.AttackerMovementDirectionAsAngle = attackerAgent.MovementDirectionAsAngle;
				this.AttackerAgentMountChargeDamageProperty = attackerAgent.GetAgentDrivenPropertyValue(DrivenProperty.MountChargeDamage);
				this.AttackerHitPointRate = attackerAgent.Health / attackerAgent.HealthLimit;
				this.AttackerAgentMovementVelocity = attackerAgent.MovementVelocity;
				this.AttackerAgentMovementDirection = attackerAgent.GetMovementDirection();
				this.AttackerAgentVelocity = attackerAgent.Velocity;
				if (this.IsAttackerAgentActive)
				{
					this.AttackerAgentCurrentWeaponOffset = attackerAgent.GetCurWeaponOffset();
				}
				this.AttackerAgentCharacter = attackerAgent.Character;
				this.AttackerAgentOrigin = attackerAgent.Origin;
				if (this.DoesAttackerHaveRiderAgent)
				{
					Agent riderAgent2 = attackerAgent.RiderAgent;
					this.IsAttackerAgentRiderAgentMine = riderAgent2.IsMine;
					this.AttackerRiderAgentCharacter = riderAgent2.Character;
					this.AttackerRiderAgentOrigin = riderAgent2.Origin;
					Formation formation3 = riderAgent2.Formation;
					Agent agent3 = (formation3 != null) ? formation3.Captain : null;
					this.AttackerCaptainCharacter = ((riderAgent2 != agent3) ? ((agent3 != null) ? agent3.Character : null) : null);
					this.AttackerFormation = riderAgent2.Formation;
				}
				else
				{
					Formation formation4 = attackerAgent.Formation;
					Agent agent4 = (formation4 != null) ? formation4.Captain : null;
					this.AttackerCaptainCharacter = ((attackerAgent != agent4) ? ((agent4 != null) ? agent4.Character : null) : null);
					this.AttackerFormation = attackerAgent.Formation;
				}
			}
			this.CanGiveDamageToAgentShield = true;
			if (!this.IsVictimAgentSameWithAttackerAgent)
			{
				Mission mission = Mission.Current;
				missionWeapon = attackerWeapon;
				this.CanGiveDamageToAgentShield = mission.CanGiveDamageToAgentShield(attackerAgent, missionWeapon.CurrentUsageItem, victimAgent);
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004E330 File Offset: 0x0004C530
		public AttackInformation(Agent attackerAgent, Agent victimAgent, float armorAmountFloat, WeaponComponentData shieldOnBack, AgentFlag victimAgentFlag, float victimAgentAbsorbedDamageRatio, float damageMultiplierOfBone, float combatDifficultyMultiplier, MissionWeapon victimMainHandWeapon, MissionWeapon victimShield, bool canGiveDamageToAgentShield, bool isVictimAgentLeftStance, bool isFriendlyFire, bool doesAttackerHaveMountAgent, bool doesVictimHaveMountAgent, Vec2 attackerAgentMovementVelocity, Vec2 attackerAgentMountMovementDirection, float attackerMovementDirectionAsAngle, Vec2 victimAgentMovementVelocity, Vec2 victimAgentMountMovementDirection, float victimMovementDirectionAsAngle, bool isVictimAgentSameWithAttackerAgent, bool isAttackerAgentMine, bool doesAttackerHaveRiderAgent, bool isAttackerAgentRiderAgentMine, bool isAttackerAgentMount, bool isVictimAgentMine, bool doesVictimHaveRiderAgent, bool isVictimAgentRiderAgentMine, bool isVictimAgentMount, bool isAttackerAgentNull, bool isAttackerAIControlled, BasicCharacterObject attackerAgentCharacter, BasicCharacterObject attackerRiderAgentCharacter, IAgentOriginBase attackerAgentOrigin, IAgentOriginBase attackerRiderAgentOrigin, BasicCharacterObject victimAgentCharacter, BasicCharacterObject victimRiderAgentCharacter, IAgentOriginBase victimAgentOrigin, IAgentOriginBase victimRiderAgentOrigin, Vec2 attackerAgentMovementDirection, Vec3 attackerAgentVelocity, float attackerAgentMountChargeDamageProperty, Vec3 attackerAgentCurrentWeaponOffset, bool isAttackerAgentHuman, bool isAttackerAgentActive, bool isAttackerAgentDoingPassiveAttack, bool isVictimAgentNull, float victimAgentScale, float victimAgentHealth, float victimAgentMaxHealth, float victimAgentWeight, float victimAgentTotalEncumbrance, bool isVictimAgentHuman, Vec3 victimAgentVelocity, Vec3 victimAgentPosition, int weaponAttachBoneIndex, MissionWeapon offHandItem, bool isHeadShot, bool isVictimRiderAgentSameAsAttackerAgent, bool isAttackerPlayer, bool isVictimPlayer, DestructableComponent hitObjectDestructibleComponent)
		{
			this.AttackerAgent = attackerAgent;
			this.VictimAgent = victimAgent;
			this.ArmorAmountFloat = armorAmountFloat;
			this.ShieldOnBack = shieldOnBack;
			this.VictimAgentFlag = victimAgentFlag;
			this.VictimAgentAbsorbedDamageRatio = victimAgentAbsorbedDamageRatio;
			this.DamageMultiplierOfBone = damageMultiplierOfBone;
			this.CombatDifficultyMultiplier = combatDifficultyMultiplier;
			this.VictimMainHandWeapon = victimMainHandWeapon;
			this.VictimShield = victimShield;
			this.CanGiveDamageToAgentShield = canGiveDamageToAgentShield;
			this.IsVictimAgentLeftStance = isVictimAgentLeftStance;
			this.IsFriendlyFire = isFriendlyFire;
			this.DoesAttackerHaveMountAgent = doesAttackerHaveMountAgent;
			this.DoesVictimHaveMountAgent = doesVictimHaveMountAgent;
			this.AttackerAgentMovementVelocity = attackerAgentMovementVelocity;
			this.AttackerAgentMountMovementDirection = attackerAgentMountMovementDirection;
			this.AttackerMovementDirectionAsAngle = attackerMovementDirectionAsAngle;
			this.VictimAgentMovementVelocity = victimAgentMovementVelocity;
			this.VictimAgentMountMovementDirection = victimAgentMountMovementDirection;
			this.VictimMovementDirectionAsAngle = victimMovementDirectionAsAngle;
			this.IsVictimAgentSameWithAttackerAgent = isVictimAgentSameWithAttackerAgent;
			this.IsAttackerAgentMine = isAttackerAgentMine;
			this.DoesAttackerHaveRiderAgent = doesAttackerHaveRiderAgent;
			this.IsAttackerAgentRiderAgentMine = isAttackerAgentRiderAgentMine;
			this.IsAttackerAgentMount = isAttackerAgentMount;
			this.IsVictimAgentMine = isVictimAgentMine;
			this.DoesVictimHaveRiderAgent = doesVictimHaveRiderAgent;
			this.IsVictimAgentRiderAgentMine = isVictimAgentRiderAgentMine;
			this.IsVictimAgentMount = isVictimAgentMount;
			this.IsAttackerAgentNull = isAttackerAgentNull;
			this.IsAttackerAIControlled = isAttackerAIControlled;
			this.AttackerAgentCharacter = attackerAgentCharacter;
			this.AttackerRiderAgentCharacter = attackerRiderAgentCharacter;
			this.AttackerAgentOrigin = attackerAgentOrigin;
			this.AttackerRiderAgentOrigin = attackerRiderAgentOrigin;
			this.VictimAgentCharacter = victimAgentCharacter;
			this.VictimRiderAgentCharacter = victimRiderAgentCharacter;
			this.VictimAgentOrigin = victimAgentOrigin;
			this.VictimRiderAgentOrigin = victimRiderAgentOrigin;
			this.AttackerAgentMovementDirection = attackerAgentMovementDirection;
			this.AttackerAgentVelocity = attackerAgentVelocity;
			this.AttackerAgentMountChargeDamageProperty = attackerAgentMountChargeDamageProperty;
			this.AttackerAgentCurrentWeaponOffset = attackerAgentCurrentWeaponOffset;
			this.IsAttackerAgentHuman = isAttackerAgentHuman;
			this.IsAttackerAgentActive = isAttackerAgentActive;
			this.IsAttackerAgentDoingPassiveAttack = isAttackerAgentDoingPassiveAttack;
			this.VictimAgentScale = victimAgentScale;
			this.IsVictimAgentNull = isVictimAgentNull;
			this.VictimAgentHealth = victimAgentHealth;
			this.VictimAgentMaxHealth = victimAgentMaxHealth;
			this.VictimAgentWeight = victimAgentWeight;
			this.VictimAgentTotalEncumbrance = victimAgentTotalEncumbrance;
			this.IsVictimAgentHuman = isVictimAgentHuman;
			this.VictimAgentVelocity = victimAgentVelocity;
			this.VictimAgentPosition = victimAgentPosition;
			this.WeaponAttachBoneIndex = weaponAttachBoneIndex;
			this.OffHandItem = offHandItem;
			this.IsHeadShot = isHeadShot;
			this.IsVictimRiderAgentSameAsAttackerAgent = isVictimRiderAgentSameAsAttackerAgent;
			this.AttackerCaptainCharacter = null;
			this.VictimCaptainCharacter = null;
			this.VictimFormation = null;
			this.AttackerFormation = null;
			this.AttackerHitPointRate = 1f;
			this.VictimHitPointRate = 1f;
			this.IsAttackerPlayer = isAttackerPlayer;
			this.IsVictimPlayer = isVictimPlayer;
			this.HitObjectDestructibleComponent = hitObjectDestructibleComponent;
		}

		// Token: 0x0400063E RID: 1598
		public Agent AttackerAgent;

		// Token: 0x0400063F RID: 1599
		public Agent VictimAgent;

		// Token: 0x04000640 RID: 1600
		public float ArmorAmountFloat;

		// Token: 0x04000641 RID: 1601
		public WeaponComponentData ShieldOnBack;

		// Token: 0x04000642 RID: 1602
		public AgentFlag VictimAgentFlag;

		// Token: 0x04000643 RID: 1603
		public float VictimAgentAbsorbedDamageRatio;

		// Token: 0x04000644 RID: 1604
		public float DamageMultiplierOfBone;

		// Token: 0x04000645 RID: 1605
		public float CombatDifficultyMultiplier;

		// Token: 0x04000646 RID: 1606
		public MissionWeapon VictimMainHandWeapon;

		// Token: 0x04000647 RID: 1607
		public MissionWeapon VictimShield;

		// Token: 0x04000648 RID: 1608
		public bool CanGiveDamageToAgentShield;

		// Token: 0x04000649 RID: 1609
		public bool IsVictimAgentLeftStance;

		// Token: 0x0400064A RID: 1610
		public bool IsFriendlyFire;

		// Token: 0x0400064B RID: 1611
		public bool DoesAttackerHaveMountAgent;

		// Token: 0x0400064C RID: 1612
		public bool DoesVictimHaveMountAgent;

		// Token: 0x0400064D RID: 1613
		public Vec2 AttackerAgentMovementVelocity;

		// Token: 0x0400064E RID: 1614
		public Vec2 AttackerAgentMountMovementDirection;

		// Token: 0x0400064F RID: 1615
		public float AttackerMovementDirectionAsAngle;

		// Token: 0x04000650 RID: 1616
		public Vec2 VictimAgentMovementVelocity;

		// Token: 0x04000651 RID: 1617
		public Vec2 VictimAgentMountMovementDirection;

		// Token: 0x04000652 RID: 1618
		public float VictimMovementDirectionAsAngle;

		// Token: 0x04000653 RID: 1619
		public bool IsVictimAgentSameWithAttackerAgent;

		// Token: 0x04000654 RID: 1620
		public bool IsAttackerAgentMine;

		// Token: 0x04000655 RID: 1621
		public bool DoesAttackerHaveRiderAgent;

		// Token: 0x04000656 RID: 1622
		public bool IsAttackerAgentRiderAgentMine;

		// Token: 0x04000657 RID: 1623
		public bool IsAttackerAgentMount;

		// Token: 0x04000658 RID: 1624
		public bool IsVictimAgentMine;

		// Token: 0x04000659 RID: 1625
		public bool DoesVictimHaveRiderAgent;

		// Token: 0x0400065A RID: 1626
		public bool IsVictimAgentRiderAgentMine;

		// Token: 0x0400065B RID: 1627
		public bool IsVictimAgentMount;

		// Token: 0x0400065C RID: 1628
		public bool IsAttackerAgentNull;

		// Token: 0x0400065D RID: 1629
		public bool IsAttackerAIControlled;

		// Token: 0x0400065E RID: 1630
		public BasicCharacterObject AttackerAgentCharacter;

		// Token: 0x0400065F RID: 1631
		public BasicCharacterObject AttackerRiderAgentCharacter;

		// Token: 0x04000660 RID: 1632
		public IAgentOriginBase AttackerAgentOrigin;

		// Token: 0x04000661 RID: 1633
		public IAgentOriginBase AttackerRiderAgentOrigin;

		// Token: 0x04000662 RID: 1634
		public BasicCharacterObject VictimAgentCharacter;

		// Token: 0x04000663 RID: 1635
		public BasicCharacterObject VictimRiderAgentCharacter;

		// Token: 0x04000664 RID: 1636
		public IAgentOriginBase VictimAgentOrigin;

		// Token: 0x04000665 RID: 1637
		public IAgentOriginBase VictimRiderAgentOrigin;

		// Token: 0x04000666 RID: 1638
		public Vec2 AttackerAgentMovementDirection;

		// Token: 0x04000667 RID: 1639
		public Vec3 AttackerAgentVelocity;

		// Token: 0x04000668 RID: 1640
		public float AttackerAgentMountChargeDamageProperty;

		// Token: 0x04000669 RID: 1641
		public Vec3 AttackerAgentCurrentWeaponOffset;

		// Token: 0x0400066A RID: 1642
		public bool IsAttackerAgentHuman;

		// Token: 0x0400066B RID: 1643
		public bool IsAttackerAgentActive;

		// Token: 0x0400066C RID: 1644
		public bool IsAttackerAgentDoingPassiveAttack;

		// Token: 0x0400066D RID: 1645
		public bool IsVictimAgentNull;

		// Token: 0x0400066E RID: 1646
		public float VictimAgentScale;

		// Token: 0x0400066F RID: 1647
		public float VictimAgentWeight;

		// Token: 0x04000670 RID: 1648
		public float VictimAgentHealth;

		// Token: 0x04000671 RID: 1649
		public float VictimAgentMaxHealth;

		// Token: 0x04000672 RID: 1650
		public float VictimAgentTotalEncumbrance;

		// Token: 0x04000673 RID: 1651
		public bool IsVictimAgentHuman;

		// Token: 0x04000674 RID: 1652
		public Vec3 VictimAgentVelocity;

		// Token: 0x04000675 RID: 1653
		public Vec3 VictimAgentPosition;

		// Token: 0x04000676 RID: 1654
		public int WeaponAttachBoneIndex;

		// Token: 0x04000677 RID: 1655
		public MissionWeapon OffHandItem;

		// Token: 0x04000678 RID: 1656
		public bool IsHeadShot;

		// Token: 0x04000679 RID: 1657
		public bool IsVictimRiderAgentSameAsAttackerAgent;

		// Token: 0x0400067A RID: 1658
		public BasicCharacterObject AttackerCaptainCharacter;

		// Token: 0x0400067B RID: 1659
		public BasicCharacterObject VictimCaptainCharacter;

		// Token: 0x0400067C RID: 1660
		public Formation AttackerFormation;

		// Token: 0x0400067D RID: 1661
		public Formation VictimFormation;

		// Token: 0x0400067E RID: 1662
		public float AttackerHitPointRate;

		// Token: 0x0400067F RID: 1663
		public float VictimHitPointRate;

		// Token: 0x04000680 RID: 1664
		public bool IsAttackerPlayer;

		// Token: 0x04000681 RID: 1665
		public bool IsVictimPlayer;

		// Token: 0x04000682 RID: 1666
		public DestructableComponent HitObjectDestructibleComponent;
	}
}
