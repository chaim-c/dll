using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000106 RID: 262
	public class HumanAIComponent : AgentComponent
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00016E87 File Offset: 0x00015087
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00016E8F File Offset: 0x0001508F
		public Agent FollowedAgent { get; private set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00016E98 File Offset: 0x00015098
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00016EA0 File Offset: 0x000150A0
		public bool ShouldCatchUpWithFormation
		{
			get
			{
				return this._shouldCatchUpWithFormation;
			}
			private set
			{
				if (this._shouldCatchUpWithFormation != value)
				{
					this._shouldCatchUpWithFormation = value;
					this.Agent.SetShouldCatchUpWithFormation(value);
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00016EBE File Offset: 0x000150BE
		public bool IsDefending
		{
			get
			{
				return this._objectInterestKind == HumanAIComponent.UsableObjectInterestKind.Defending;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00016ECC File Offset: 0x000150CC
		public HumanAIComponent(Agent agent) : base(agent)
		{
			this._behaviorValues = new HumanAIComponent.BehaviorValues[7];
			this.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.Default);
			this.Agent.SetAllBehaviorParams(this._behaviorValues);
			this._hasNewBehaviorValues = false;
			Agent agent2 = this.Agent;
			agent2.OnAgentWieldedItemChange = (Action)Delegate.Combine(agent2.OnAgentWieldedItemChange, new Action(this.DisablePickUpForAgentIfNeeded));
			Agent agent3 = this.Agent;
			agent3.OnAgentMountedStateChanged = (Action)Delegate.Combine(agent3.OnAgentMountedStateChanged, new Action(this.DisablePickUpForAgentIfNeeded));
			this._itemPickUpTickTimer = new MissionTimer(2.5f + MBRandom.RandomFloat);
			this._mountSearchTimer = new MissionTimer(2f + MBRandom.RandomFloat);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00016FA0 File Offset: 0x000151A0
		public void SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind behavior, float y1, float x2, float y2, float x3, float y3)
		{
			this._behaviorValues[(int)behavior].y1 = y1;
			this._behaviorValues[(int)behavior].x2 = x2;
			this._behaviorValues[(int)behavior].y2 = y2;
			this._behaviorValues[(int)behavior].x3 = x3;
			this._behaviorValues[(int)behavior].y3 = y3;
			this._hasNewBehaviorValues = true;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00017011 File Offset: 0x00015211
		public void SyncBehaviorParamsIfNecessary()
		{
			if (this._hasNewBehaviorValues)
			{
				this.Agent.SetAllBehaviorParams(this._behaviorValues);
				this._hasNewBehaviorValues = false;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00017034 File Offset: 0x00015234
		public void DisablePickUpForAgentIfNeeded()
		{
			this._disablePickUpForAgent = true;
			if (this.Agent.MountAgent == null)
			{
				if (this.Agent.HasLostShield())
				{
					this._disablePickUpForAgent = false;
				}
				else
				{
					for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
					{
						MissionWeapon missionWeapon = this.Agent.Equipment[equipmentIndex];
						if (!missionWeapon.IsEmpty && missionWeapon.IsAnyConsumable())
						{
							this._disablePickUpForAgent = false;
							break;
						}
					}
				}
			}
			if (this._disablePickUpForAgent && this.Agent.Formation != null && MissionGameModels.Current.BattleBannerBearersModel.IsBannerSearchingAgent(this.Agent))
			{
				this._disablePickUpForAgent = false;
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000170D8 File Offset: 0x000152D8
		public override void OnTickAsAI(float dt)
		{
			this.SyncBehaviorParamsIfNecessary();
			if (this._itemToPickUp != null)
			{
				if (!this._itemToPickUp.IsAIMovingTo(this.Agent) || this.Agent.Mission.MissionEnded)
				{
					this._itemToPickUp = null;
				}
				else if (this._itemToPickUp.GameEntity == null)
				{
					this.Agent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
			}
			if (this._itemPickUpTickTimer.Check(true) && !this.Agent.Mission.MissionEnded)
			{
				EquipmentIndex wieldedItemIndex = this.Agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				WeaponComponentData weaponComponentData = (wieldedItemIndex == EquipmentIndex.None) ? null : this.Agent.Equipment[wieldedItemIndex].CurrentUsageItem;
				bool flag = weaponComponentData != null && weaponComponentData.IsRangedWeapon;
				if (!this._disablePickUpForAgent && MissionGameModels.Current.ItemPickupModel.IsAgentEquipmentSuitableForPickUpAvailability(this.Agent) && this.Agent.CanBeAssignedForScriptedMovement() && this.Agent.CurrentWatchState == Agent.WatchState.Alarmed && (this.Agent.GetAgentFlags() & AgentFlag.CanAttack) != AgentFlag.None && !this.IsInImportantCombatAction())
				{
					Agent targetAgent = this.Agent.GetTargetAgent();
					if (targetAgent != null)
					{
						Vec3 position = targetAgent.Position;
						if (position.DistanceSquared(this.Agent.Position) <= 400f)
						{
							goto IL_223;
						}
						if (flag && !this.IsAnyConsumableDepleted())
						{
							position = targetAgent.Position;
							if (position.DistanceSquared(this.Agent.Position) < this.Agent.GetMissileRange() * 1.2f && this.Agent.GetLastTargetVisibilityState() == AITargetVisibilityState.TargetIsClear)
							{
								goto IL_223;
							}
						}
					}
					float maximumForwardUnlimitedSpeed = this.Agent.MaximumForwardUnlimitedSpeed;
					if (this._itemToPickUp == null)
					{
						Vec3 bMin = this.Agent.Position - new Vec3(maximumForwardUnlimitedSpeed, maximumForwardUnlimitedSpeed, 1f, -1f);
						Vec3 bMax = this.Agent.Position + new Vec3(maximumForwardUnlimitedSpeed, maximumForwardUnlimitedSpeed, 1.8f, -1f);
						this._itemToPickUp = this.SelectPickableItem(bMin, bMax);
						if (this._itemToPickUp != null)
						{
							this.RequestMoveToItem(this._itemToPickUp);
						}
					}
				}
			}
			IL_223:
			if (this._itemToPickUp != null && !this.Agent.IsRunningAway && this.Agent.AIMoveToGameObjectIsEnabled())
			{
				float num = this._itemToPickUp.IsBanner() ? MissionGameModels.Current.BattleBannerBearersModel.GetBannerInteractionDistance(this.Agent) : MissionGameModels.Current.AgentStatCalculateModel.GetInteractionDistance(this.Agent);
				num *= 3f;
				ref WorldFrame ptr = ref this._itemToPickUp.GetUserFrameForAgent(this.Agent);
				Vec3 position = this.Agent.Position;
				float distanceSq = ptr.Origin.DistanceSquaredWithLimit(position, num * num + 1E-05f);
				if (this.Agent.CanReachAndUseObject(this._itemToPickUp, distanceSq))
				{
					this.Agent.UseGameObject(this._itemToPickUp, -1);
				}
			}
			if (this.Agent.CommonAIComponent != null && this.Agent.MountAgent == null && !this.Agent.CommonAIComponent.IsRetreating && this._mountSearchTimer.Check(true) && this.Agent.GetRidingOrder() == 1)
			{
				Agent agent = this.FindReservedMount();
				bool flag2;
				if (agent != null && agent.State == AgentState.Active && agent.RiderAgent == null)
				{
					Vec3 position = this.Agent.Position;
					flag2 = (position.DistanceSquared(agent.Position) >= 256f);
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					if (agent != null)
					{
						this.UnreserveMount(agent);
					}
					Agent agent2 = this.FindClosestMountAvailable();
					if (agent2 != null)
					{
						this.ReserveMount(agent2);
					}
				}
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00017494 File Offset: 0x00015694
		private Agent FindClosestMountAvailable()
		{
			float num = 6400f;
			Agent result = null;
			foreach (KeyValuePair<Agent, MissionTime> keyValuePair in Mission.Current.MountsWithoutRiders)
			{
				Agent key = keyValuePair.Key;
				if (key.IsActive() && key.CommonAIComponent.ReservedRiderAgentIndex < 0 && key.RiderAgent == null && !key.IsRunningAway && MissionGameModels.Current.AgentStatCalculateModel.CanAgentRideMount(this.Agent, key))
				{
					float num2 = this.Agent.Position.DistanceSquared(key.Position);
					if (num > num2)
					{
						result = key;
						num = num2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00017564 File Offset: 0x00015764
		private Agent FindReservedMount()
		{
			Agent result = null;
			int selectedMountIndex = this.Agent.GetSelectedMountIndex();
			if (selectedMountIndex >= 0)
			{
				foreach (KeyValuePair<Agent, MissionTime> keyValuePair in Mission.Current.MountsWithoutRiders)
				{
					Agent key = keyValuePair.Key;
					if (key.Index == selectedMountIndex)
					{
						result = key;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000175E0 File Offset: 0x000157E0
		internal void ReserveMount(Agent mount)
		{
			this.Agent.SetSelectedMountIndex(mount.Index);
			mount.CommonAIComponent.OnMountReserved(this.Agent.Index);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00017609 File Offset: 0x00015809
		internal void UnreserveMount(Agent mount)
		{
			this.Agent.SetSelectedMountIndex(-1);
			mount.CommonAIComponent.OnMountUnreserved();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00017624 File Offset: 0x00015824
		public override void OnAgentRemoved()
		{
			Agent agent = this.FindReservedMount();
			if (agent != null)
			{
				this.UnreserveMount(agent);
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00017644 File Offset: 0x00015844
		public override void OnComponentRemoved()
		{
			Agent agent = this.FindReservedMount();
			if (agent != null)
			{
				this.UnreserveMount(agent);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00017664 File Offset: 0x00015864
		public bool IsInImportantCombatAction()
		{
			Agent.ActionCodeType currentActionType = this.Agent.GetCurrentActionType(1);
			return currentActionType == Agent.ActionCodeType.ReadyMelee || currentActionType == Agent.ActionCodeType.ReadyRanged || currentActionType == Agent.ActionCodeType.ReleaseMelee || currentActionType == Agent.ActionCodeType.ReleaseRanged || currentActionType == Agent.ActionCodeType.ReleaseThrowing || currentActionType == Agent.ActionCodeType.DefendShield;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000176A0 File Offset: 0x000158A0
		private bool IsAnyConsumableDepleted()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				MissionWeapon missionWeapon = this.Agent.Equipment[equipmentIndex];
				if (!missionWeapon.IsEmpty && missionWeapon.IsAnyConsumable() && missionWeapon.Amount == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000176EC File Offset: 0x000158EC
		private SpawnedItemEntity SelectPickableItem(Vec3 bMin, Vec3 bMax)
		{
			Agent targetAgent = this.Agent.GetTargetAgent();
			Vec3 v = (targetAgent == null) ? Vec3.Invalid : (targetAgent.Position - this.Agent.Position);
			int num = this.Agent.Mission.Scene.SelectEntitiesInBoxWithScriptComponent<SpawnedItemEntity>(ref bMin, ref bMax, this._tempPickableEntities, this._pickableItemsId);
			float num2 = 0f;
			SpawnedItemEntity result = null;
			for (int i = 0; i < num; i++)
			{
				SpawnedItemEntity firstScriptOfType = this._tempPickableEntities[i].GetFirstScriptOfType<SpawnedItemEntity>();
				bool flag = false;
				if (firstScriptOfType != null)
				{
					MissionWeapon weaponCopy = firstScriptOfType.WeaponCopy;
					flag = (!weaponCopy.IsEmpty && (weaponCopy.IsShield() || weaponCopy.IsBanner() || firstScriptOfType.IsStuckMissile() || firstScriptOfType.IsQuiverAndNotEmpty()));
				}
				if (flag && !firstScriptOfType.HasUser && (!firstScriptOfType.HasAIMovingTo || firstScriptOfType.IsAIMovingTo(this.Agent)) && firstScriptOfType.GameEntityWithWorldPosition.WorldPosition.GetNavMesh() != UIntPtr.Zero)
				{
					Vec3 vec = firstScriptOfType.GetUserFrameForAgent(this.Agent).Origin.GetGroundVec3() - this.Agent.Position;
					float z = vec.z;
					vec.Normalize();
					if (targetAgent == null || v.Length - Vec3.DotProduct(v, vec) > targetAgent.MaximumForwardUnlimitedSpeed * 3f)
					{
						EquipmentIndex equipmentIndex = MissionEquipment.SelectWeaponPickUpSlot(this.Agent, firstScriptOfType.WeaponCopy, firstScriptOfType.IsStuckMissile());
						WorldPosition worldPosition = firstScriptOfType.GameEntityWithWorldPosition.WorldPosition;
						if (equipmentIndex != EquipmentIndex.None && worldPosition.GetNavMesh() != UIntPtr.Zero && MissionGameModels.Current.ItemPickupModel.IsItemAvailableForAgent(firstScriptOfType, this.Agent, equipmentIndex))
						{
							Agent agent = this.Agent;
							Vec2 asVec = worldPosition.AsVec2;
							if (agent.CanMoveDirectlyToPosition(asVec) && (!this.Agent.Mission.IsPositionInsideAnyBlockerNavMeshFace2D(worldPosition.AsVec2) || MathF.Abs(z) >= 1.5f))
							{
								float itemScoreForAgent = MissionGameModels.Current.ItemPickupModel.GetItemScoreForAgent(firstScriptOfType, this.Agent);
								if (itemScoreForAgent > num2)
								{
									result = firstScriptOfType;
									num2 = itemScoreForAgent;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00017937 File Offset: 0x00015B37
		internal void ItemPickupDone(SpawnedItemEntity spawnedItemEntity)
		{
			this._itemToPickUp = null;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00017940 File Offset: 0x00015B40
		private void RequestMoveToItem(SpawnedItemEntity item)
		{
			Agent movingAgent = item.MovingAgent;
			if (movingAgent != null)
			{
				movingAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
			this.MoveToUsableGameObject(item, null, Agent.AIScriptedFrameFlags.NoAttack);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0001795E File Offset: 0x00015B5E
		public UsableMissionObject GetCurrentlyMovingGameObject()
		{
			return this._objectOfInterest;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00017966 File Offset: 0x00015B66
		private void SetCurrentlyMovingGameObject(UsableMissionObject objectOfInterest)
		{
			this._objectOfInterest = objectOfInterest;
			this._objectInterestKind = ((this._objectOfInterest != null) ? HumanAIComponent.UsableObjectInterestKind.MovingTo : HumanAIComponent.UsableObjectInterestKind.None);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00017981 File Offset: 0x00015B81
		public UsableMissionObject GetCurrentlyDefendingGameObject()
		{
			return this._objectOfInterest;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00017989 File Offset: 0x00015B89
		private void SetCurrentlyDefendingGameObject(UsableMissionObject objectOfInterest)
		{
			this._objectOfInterest = objectOfInterest;
			this._objectInterestKind = ((this._objectOfInterest != null) ? HumanAIComponent.UsableObjectInterestKind.Defending : HumanAIComponent.UsableObjectInterestKind.None);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000179A4 File Offset: 0x00015BA4
		public void MoveToUsableGameObject(UsableMissionObject usedObject, IDetachment detachment, Agent.AIScriptedFrameFlags scriptedFrameFlags = Agent.AIScriptedFrameFlags.NoAttack)
		{
			this.Agent.AIStateFlags |= Agent.AIStateFlag.UseObjectMoving;
			this.SetCurrentlyMovingGameObject(usedObject);
			usedObject.OnAIMoveToUse(this.Agent, detachment);
			WorldFrame userFrameForAgent = usedObject.GetUserFrameForAgent(this.Agent);
			this.Agent.SetScriptedPositionAndDirection(ref userFrameForAgent.Origin, userFrameForAgent.Rotation.f.AsVec2.RotationInRadians, false, scriptedFrameFlags);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00017A12 File Offset: 0x00015C12
		public void MoveToClear()
		{
			UsableMissionObject currentlyMovingGameObject = this.GetCurrentlyMovingGameObject();
			if (currentlyMovingGameObject != null)
			{
				currentlyMovingGameObject.OnMoveToStopped(this.Agent);
			}
			this.SetCurrentlyMovingGameObject(null);
			this.Agent.AIStateFlags &= ~Agent.AIStateFlag.UseObjectMoving;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00017A46 File Offset: 0x00015C46
		public void StartDefendingGameObject(UsableMissionObject usedObject, IDetachment detachment)
		{
			this.SetCurrentlyDefendingGameObject(usedObject);
			usedObject.OnAIDefendBegin(this.Agent, detachment);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00017A5C File Offset: 0x00015C5C
		public void StopDefendingGameObject()
		{
			this.GetCurrentlyDefendingGameObject().OnAIDefendEnd(this.Agent);
			this.SetCurrentlyDefendingGameObject(null);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00017A76 File Offset: 0x00015C76
		public bool IsInterestedInAnyGameObject()
		{
			return this._objectInterestKind > HumanAIComponent.UsableObjectInterestKind.None;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00017A84 File Offset: 0x00015C84
		public bool IsInterestedInGameObject(UsableMissionObject usableMissionObject)
		{
			bool result = false;
			switch (this._objectInterestKind)
			{
			case HumanAIComponent.UsableObjectInterestKind.None:
				break;
			case HumanAIComponent.UsableObjectInterestKind.MovingTo:
				result = (usableMissionObject == this.GetCurrentlyMovingGameObject());
				break;
			case HumanAIComponent.UsableObjectInterestKind.Defending:
				result = (usableMissionObject == this.GetCurrentlyDefendingGameObject());
				break;
			default:
				Debug.FailedAssert("Unexpected object interest kind: " + this._objectInterestKind, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\AgentComponents\\HumanAIComponent.cs", "IsInterestedInGameObject", 580);
				break;
			}
			return result;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00017AF0 File Offset: 0x00015CF0
		public void FollowAgent(Agent agent)
		{
			this.FollowedAgent = agent;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00017AFC File Offset: 0x00015CFC
		public float GetDesiredSpeedInFormation(bool isCharging)
		{
			if (this.ShouldCatchUpWithFormation && (!isCharging || !Mission.Current.IsMissionEnding))
			{
				Agent mountAgent = this.Agent.MountAgent;
				float num = (mountAgent != null) ? mountAgent.MaximumForwardUnlimitedSpeed : this.Agent.MaximumForwardUnlimitedSpeed;
				bool flag = !isCharging;
				if (isCharging)
				{
					FormationQuerySystem closestEnemyFormation = this.Agent.Formation.QuerySystem.ClosestEnemyFormation;
					float num2 = float.MaxValue;
					float num3 = 4f * num * num;
					if (closestEnemyFormation != null)
					{
						WorldPosition medianPosition = this.Agent.Formation.QuerySystem.MedianPosition;
						WorldPosition medianPosition2 = closestEnemyFormation.MedianPosition;
						num2 = medianPosition.AsVec2.DistanceSquared(medianPosition2.AsVec2);
						if (num2 <= num3)
						{
							num2 = this.Agent.Formation.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(closestEnemyFormation.MedianPosition.GetNavMeshVec3());
						}
					}
					flag = (num2 > num3);
				}
				if (flag)
				{
					Vec2 v = this.Agent.Formation.GetCurrentGlobalPositionOfUnit(this.Agent, true) - this.Agent.Position.AsVec2;
					float num4 = -this.Agent.GetMovementDirection().DotProduct(v);
					num4 = MathF.Clamp(num4, 0f, 100f);
					float num5 = (this.Agent.MountAgent != null) ? 4f : 2f;
					float num6 = (isCharging ? this.Agent.Formation.QuerySystem.FormationIntegrityData.AverageMaxUnlimitedSpeedExcludeFarAgents : this.Agent.Formation.QuerySystem.MovementSpeed) / num;
					return MathF.Clamp((0.7f + 0.4f * ((num - num4 * num5) / (num + num4 * num5))) * num6, 0.3f, 1f);
				}
			}
			return 1f;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00017CE0 File Offset: 0x00015EE0
		private unsafe bool GetFormationFrame(out WorldPosition formationPosition, out Vec2 formationDirection, out float speedLimit, out bool isSettingDestinationSpeed, out bool limitIsMultiplier, bool finalDestination = false)
		{
			Formation formation = this.Agent.Formation;
			isSettingDestinationSpeed = false;
			limitIsMultiplier = false;
			bool result = false;
			if (formation != null)
			{
				formationPosition = formation.GetOrderPositionOfUnit(this.Agent);
				formationDirection = formation.GetDirectionOfUnit(this.Agent);
			}
			else
			{
				formationPosition = WorldPosition.Invalid;
				formationDirection = Vec2.Invalid;
			}
			if (HumanAIComponent.FormationSpeedAdjustmentEnabled && this.Agent.IsMount)
			{
				formationPosition = WorldPosition.Invalid;
				formationDirection = Vec2.Invalid;
				if (this.Agent.RiderAgent == null || (this.Agent.RiderAgent != null && (!this.Agent.RiderAgent.IsActive() || this.Agent.RiderAgent.Formation == null)))
				{
					speedLimit = -1f;
				}
				else
				{
					limitIsMultiplier = true;
					HumanAIComponent humanAIComponent = this.Agent.RiderAgent.HumanAIComponent;
					MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
					speedLimit = humanAIComponent.GetDesiredSpeedInFormation(movementOrder.MovementState == MovementOrder.MovementStateEnum.Charge);
				}
			}
			else if (formation == null)
			{
				speedLimit = -1f;
			}
			else if (this.Agent.IsDetachedFromFormation)
			{
				speedLimit = -1f;
				WorldFrame? worldFrame = null;
				MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
				if (movementOrder.MovementState != MovementOrder.MovementStateEnum.Charge || (this.Agent.Detachment != null && (!this.Agent.Detachment.IsLoose || formationPosition.IsValid)))
				{
					worldFrame = formation.GetDetachmentFrame(this.Agent);
				}
				if (worldFrame != null)
				{
					formationDirection = worldFrame.Value.Rotation.f.AsVec2.Normalized();
					result = true;
				}
				else
				{
					formationDirection = Vec2.Invalid;
				}
			}
			else
			{
				MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
				switch (movementOrder.MovementState)
				{
				case MovementOrder.MovementStateEnum.Charge:
					limitIsMultiplier = true;
					speedLimit = (HumanAIComponent.FormationSpeedAdjustmentEnabled ? this.GetDesiredSpeedInFormation(true) : -1f);
					result = formationPosition.IsValid;
					break;
				case MovementOrder.MovementStateEnum.Hold:
					isSettingDestinationSpeed = true;
					if (HumanAIComponent.FormationSpeedAdjustmentEnabled && this.ShouldCatchUpWithFormation)
					{
						limitIsMultiplier = true;
						speedLimit = this.GetDesiredSpeedInFormation(false);
					}
					else
					{
						speedLimit = -1f;
					}
					result = true;
					break;
				case MovementOrder.MovementStateEnum.Retreat:
					speedLimit = -1f;
					break;
				case MovementOrder.MovementStateEnum.StandGround:
					formationDirection = this.Agent.Frame.rotation.f.AsVec2;
					speedLimit = -1f;
					result = true;
					break;
				default:
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\AgentComponents\\HumanAIComponent.cs", "GetFormationFrame", 767);
					speedLimit = -1f;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00017F8D File Offset: 0x0001618D
		public void AdjustSpeedLimit(Agent agent, float desiredSpeed, bool limitIsMultiplier)
		{
			if (agent.MissionPeer != null)
			{
				desiredSpeed = -1f;
			}
			this.Agent.SetMaximumSpeedLimit(desiredSpeed, limitIsMultiplier);
			Agent mountAgent = agent.MountAgent;
			if (mountAgent == null)
			{
				return;
			}
			mountAgent.SetMaximumSpeedLimit(desiredSpeed, limitIsMultiplier);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00017FC0 File Offset: 0x000161C0
		public unsafe void UpdateFormationMovement()
		{
			WorldPosition worldPosition;
			Vec2 vec;
			float desiredSpeed;
			bool flag;
			bool limitIsMultiplier;
			bool formationFrame = this.GetFormationFrame(out worldPosition, out vec, out desiredSpeed, out flag, out limitIsMultiplier, false);
			this.AdjustSpeedLimit(this.Agent, desiredSpeed, limitIsMultiplier);
			if (this.Agent.Controller == Agent.ControllerType.AI && this.Agent.Formation != null && this.Agent.Formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Stop && this.Agent.Formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat && !this.Agent.IsRetreating())
			{
				FormationQuerySystem.FormationIntegrityDataGroup formationIntegrityData = this.Agent.Formation.QuerySystem.FormationIntegrityData;
				float num = formationIntegrityData.AverageMaxUnlimitedSpeedExcludeFarAgents * 3f;
				if (formationIntegrityData.DeviationOfPositionsExcludeFarAgents > num * 100f)
				{
					this.ShouldCatchUpWithFormation = false;
					this.Agent.SetFormationIntegrityData(Vec2.Zero, Vec2.Zero, Vec2.Zero, 0f, 0f);
				}
				else
				{
					Vec2 currentGlobalPositionOfUnit = this.Agent.Formation.GetCurrentGlobalPositionOfUnit(this.Agent, true);
					float num2 = this.Agent.Position.AsVec2.Distance(currentGlobalPositionOfUnit);
					this.ShouldCatchUpWithFormation = (num2 < num * 2f);
					this.Agent.SetFormationIntegrityData(this.ShouldCatchUpWithFormation ? currentGlobalPositionOfUnit : Vec2.Zero, this.Agent.Formation.CurrentDirection, formationIntegrityData.AverageVelocityExcludeFarAgents, formationIntegrityData.AverageMaxUnlimitedSpeedExcludeFarAgents, formationIntegrityData.DeviationOfPositionsExcludeFarAgents);
				}
			}
			else
			{
				this.ShouldCatchUpWithFormation = false;
			}
			bool flag2 = worldPosition.IsValid;
			if (!formationFrame || !flag2)
			{
				this.Agent.SetFormationFrameDisabled();
				return;
			}
			if (!GameNetwork.IsMultiplayer && this.Agent.Mission.Mode == MissionMode.Deployment)
			{
				MBSceneUtilities.ProjectPositionToDeploymentBoundaries(this.Agent.Formation.Team.Side, ref worldPosition);
				flag2 = this.Agent.Mission.IsFormationUnitPositionAvailable(ref worldPosition, this.Agent.Team);
			}
			if (flag2)
			{
				Agent agent = this.Agent;
				WorldPosition position = worldPosition;
				Vec2 direction = vec;
				MovementOrder movementOrder = *this.Agent.Formation.GetReadonlyMovementOrderReference();
				agent.SetFormationFrameEnabled(position, direction, movementOrder.GetTargetVelocity(), this.Agent.Formation.CalculateFormationDirectionEnforcingFactorForRank(((IFormationUnit)this.Agent).FormationRankIndex));
				float directionChangeTendency = 1f;
				if (this.Agent.Formation.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.ShieldWall && !this.Agent.IsDetachedFromFormation)
				{
					directionChangeTendency = this.Agent.Formation.Arrangement.GetDirectionChangeTendencyOfUnit(this.Agent);
				}
				this.Agent.SetDirectionChangeTendency(directionChangeTendency);
				return;
			}
			this.Agent.SetFormationFrameDisabled();
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00018270 File Offset: 0x00016470
		public override void OnRetreating()
		{
			base.OnRetreating();
			this.AdjustSpeedLimit(this.Agent, -1f, false);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0001828A File Offset: 0x0001648A
		public override void OnDismount(Agent mount)
		{
			base.OnDismount(mount);
			mount.SetMaximumSpeedLimit(-1f, false);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000182A0 File Offset: 0x000164A0
		public override void OnMount(Agent mount)
		{
			base.OnMount(mount);
			int selectedMountIndex = this.Agent.GetSelectedMountIndex();
			if (selectedMountIndex >= 0 && selectedMountIndex != mount.Index)
			{
				Agent agent = Mission.Current.FindAgentWithIndex(selectedMountIndex);
				if (agent != null)
				{
					this.UnreserveMount(agent);
				}
			}
			int reservedRiderAgentIndex = mount.CommonAIComponent.ReservedRiderAgentIndex;
			if (reservedRiderAgentIndex >= 0)
			{
				if (reservedRiderAgentIndex == this.Agent.Index)
				{
					this.UnreserveMount(mount);
					return;
				}
				Agent agent2 = Mission.Current.FindAgentWithIndex(reservedRiderAgentIndex);
				if (agent2 != null)
				{
					agent2.HumanAIComponent.UnreserveMount(mount);
				}
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00018324 File Offset: 0x00016524
		public void SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet behaviorValueSet)
		{
			switch (behaviorValueSet)
			{
			case HumanAIComponent.BehaviorValueSet.Default:
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3f, 7f, 5f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Melee, 8f, 7f, 4f, 20f, 1f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Ranged, 2f, 7f, 4f, 20f, 5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 2f, 25f, 5f, 30f, 5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 2f, 15f, 6.5f, 30f, 5.5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 5f, 12f, 7.5f, 30f, 4f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 5.5f, 12f, 8f, 30f, 4.5f);
				return;
			case HumanAIComponent.BehaviorValueSet.DefensiveArrangementMove:
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3f, 8f, 5f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Melee, 4f, 5f, 0f, 20f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Ranged, 0f, 7f, 0f, 20f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 0f, 7f, 0f, 30f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 0f, 15f, 0f, 30f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 5f, 12f, 7.5f, 30f, 9f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 0.55f, 12f, 0.8f, 30f, 0.45f);
				return;
			case HumanAIComponent.BehaviorValueSet.Follow:
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3f, 7f, 5f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Melee, 6f, 7f, 4f, 20f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Ranged, 0f, 7f, 0f, 20f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 0f, 7f, 0f, 30f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 0f, 15f, 0f, 30f, 0f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 5f, 12f, 7.5f, 30f, 9f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 0.55f, 12f, 0.8f, 30f, 0.45f);
				return;
			case HumanAIComponent.BehaviorValueSet.DefaultMove:
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3f, 7f, 5f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Melee, 8f, 7f, 5f, 20f, 0.01f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Ranged, 0.02f, 7f, 0.04f, 20f, 0.03f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 10f, 7f, 5f, 30f, 0.05f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 0.02f, 15f, 0.065f, 30f, 0.055f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 5f, 12f, 7.5f, 30f, 9f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 0.55f, 12f, 0.8f, 30f, 0.45f);
				return;
			case HumanAIComponent.BehaviorValueSet.Charge:
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3f, 7f, 5f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Melee, 8f, 7f, 4f, 20f, 1f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Ranged, 2f, 7f, 4f, 20f, 5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 2f, 25f, 5f, 30f, 5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 0f, 10f, 3f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 5f, 12f, 7.5f, 30f, 9f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 0.55f, 12f, 0.8f, 30f, 0.45f);
				return;
			case HumanAIComponent.BehaviorValueSet.DefaultDetached:
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3f, 7f, 5f, 20f, 6f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Melee, 8f, 7f, 4f, 20f, 1f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.Ranged, 0.2f, 7f, 0.4f, 20f, 0.5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 2f, 25f, 5f, 30f, 5f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 0.2f, 15f, 0.65f, 30f, 0.55f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 5f, 12f, 7.5f, 30f, 4f);
				this.SetBehaviorParams(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 5.5f, 12f, 8f, 30f, 4.5f);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00018895 File Offset: 0x00016A95
		public void RefreshBehaviorValues(MovementOrder.MovementOrderEnum movementOrder, ArrangementOrder.ArrangementOrderEnum arrangementOrder)
		{
			if (movementOrder == MovementOrder.MovementOrderEnum.Charge || movementOrder == MovementOrder.MovementOrderEnum.ChargeToTarget)
			{
				this.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.Charge);
				return;
			}
			if (movementOrder == MovementOrder.MovementOrderEnum.Follow || arrangementOrder == ArrangementOrder.ArrangementOrderEnum.Column)
			{
				this.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.Follow);
				return;
			}
			if (arrangementOrder != ArrangementOrder.ArrangementOrderEnum.ShieldWall && arrangementOrder != ArrangementOrder.ArrangementOrderEnum.Circle && arrangementOrder != ArrangementOrder.ArrangementOrderEnum.Square)
			{
				this.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefaultMove);
				return;
			}
			this.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefensiveArrangementMove);
		}

		// Token: 0x040002DC RID: 732
		private const float AvoidPickUpIfLookAgentIsCloseDistance = 20f;

		// Token: 0x040002DD RID: 733
		private const float AvoidPickUpIfLookAgentIsCloseDistanceSquared = 400f;

		// Token: 0x040002DE RID: 734
		private const float ClosestMountSearchRangeSq = 6400f;

		// Token: 0x040002DF RID: 735
		public static bool FormationSpeedAdjustmentEnabled = true;

		// Token: 0x040002E0 RID: 736
		private readonly HumanAIComponent.BehaviorValues[] _behaviorValues;

		// Token: 0x040002E1 RID: 737
		private bool _hasNewBehaviorValues;

		// Token: 0x040002E2 RID: 738
		private readonly GameEntity[] _tempPickableEntities = new GameEntity[16];

		// Token: 0x040002E3 RID: 739
		private readonly UIntPtr[] _pickableItemsId = new UIntPtr[16];

		// Token: 0x040002E4 RID: 740
		private SpawnedItemEntity _itemToPickUp;

		// Token: 0x040002E5 RID: 741
		private readonly MissionTimer _itemPickUpTickTimer;

		// Token: 0x040002E6 RID: 742
		private bool _disablePickUpForAgent;

		// Token: 0x040002E7 RID: 743
		private readonly MissionTimer _mountSearchTimer;

		// Token: 0x040002E8 RID: 744
		private UsableMissionObject _objectOfInterest;

		// Token: 0x040002E9 RID: 745
		private HumanAIComponent.UsableObjectInterestKind _objectInterestKind;

		// Token: 0x040002EB RID: 747
		private bool _shouldCatchUpWithFormation;

		// Token: 0x020003F8 RID: 1016
		[EngineStruct("behavior_values_struct", false)]
		public struct BehaviorValues
		{
			// Token: 0x060033E5 RID: 13285 RVA: 0x000D650C File Offset: 0x000D470C
			public float GetValueAt(float x)
			{
				if (x <= this.x2)
				{
					return (this.y2 - this.y1) * x / this.x2 + this.y1;
				}
				if (x <= this.x3)
				{
					return (this.y3 - this.y2) * (x - this.x2) / (this.x3 - this.x2) + this.y2;
				}
				return this.y3;
			}

			// Token: 0x04001786 RID: 6022
			public float y1;

			// Token: 0x04001787 RID: 6023
			public float x2;

			// Token: 0x04001788 RID: 6024
			public float y2;

			// Token: 0x04001789 RID: 6025
			public float x3;

			// Token: 0x0400178A RID: 6026
			public float y3;
		}

		// Token: 0x020003F9 RID: 1017
		public enum AISimpleBehaviorKind
		{
			// Token: 0x0400178C RID: 6028
			GoToPos,
			// Token: 0x0400178D RID: 6029
			Melee,
			// Token: 0x0400178E RID: 6030
			Ranged,
			// Token: 0x0400178F RID: 6031
			ChargeHorseback,
			// Token: 0x04001790 RID: 6032
			RangedHorseback,
			// Token: 0x04001791 RID: 6033
			AttackEntityMelee,
			// Token: 0x04001792 RID: 6034
			AttackEntityRanged,
			// Token: 0x04001793 RID: 6035
			Count
		}

		// Token: 0x020003FA RID: 1018
		public enum BehaviorValueSet
		{
			// Token: 0x04001795 RID: 6037
			Default,
			// Token: 0x04001796 RID: 6038
			DefensiveArrangementMove,
			// Token: 0x04001797 RID: 6039
			Follow,
			// Token: 0x04001798 RID: 6040
			DefaultMove,
			// Token: 0x04001799 RID: 6041
			Charge,
			// Token: 0x0400179A RID: 6042
			DefaultDetached,
			// Token: 0x0400179B RID: 6043
			Count
		}

		// Token: 0x020003FB RID: 1019
		public enum UsableObjectInterestKind
		{
			// Token: 0x0400179D RID: 6045
			None,
			// Token: 0x0400179E RID: 6046
			MovingTo,
			// Token: 0x0400179F RID: 6047
			Defending,
			// Token: 0x040017A0 RID: 6048
			Count
		}
	}
}
