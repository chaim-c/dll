using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000339 RID: 825
	public class Mangonel : RangedSiegeWeapon, ISpawnable
	{
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000AEE11 File Offset: 0x000AD011
		protected override float MaximumBallisticError
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000AEE18 File Offset: 0x000AD018
		protected override float ShootingSpeed
		{
			get
			{
				return this.ProjectileSpeed;
			}
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000AEE20 File Offset: 0x000AD020
		protected override void RegisterAnimationParameters()
		{
			this.SkeletonOwnerObjects = new SynchedMissionObject[2];
			this.Skeletons = new Skeleton[2];
			this.SkeletonNames = new string[1];
			this.FireAnimations = new string[2];
			this.FireAnimationIndices = new int[2];
			this.SetUpAnimations = new string[2];
			this.SetUpAnimationIndices = new int[2];
			this.SkeletonOwnerObjects[0] = this._body;
			this.Skeletons[0] = this._body.GameEntity.Skeleton;
			this.SkeletonNames[0] = this.MangonelBodySkeleton;
			this.FireAnimations[0] = this.MangonelBodyFire;
			this.FireAnimationIndices[0] = MBAnimation.GetAnimationIndexWithName(this.MangonelBodyFire);
			this.SetUpAnimations[0] = this.MangonelBodyReload;
			this.SetUpAnimationIndices[0] = MBAnimation.GetAnimationIndexWithName(this.MangonelBodyReload);
			this.SkeletonOwnerObjects[1] = this._rope;
			this.Skeletons[1] = this._rope.GameEntity.Skeleton;
			this.FireAnimations[1] = this.MangonelRopeFire;
			this.FireAnimationIndices[1] = MBAnimation.GetAnimationIndexWithName(this.MangonelRopeFire);
			this.SetUpAnimations[1] = this.MangonelRopeReload;
			this.SetUpAnimationIndices[1] = MBAnimation.GetAnimationIndexWithName(this.MangonelRopeReload);
			this._missileBoneName = this.ProjectileBoneName;
			this._idleAnimationActionIndex = ActionIndexCache.Create(this.IdleActionName);
			this._shootAnimationActionIndex = ActionIndexCache.Create(this.ShootActionName);
			this._reload1AnimationActionIndex = ActionIndexCache.Create(this.Reload1ActionName);
			this._reload2AnimationActionIndex = ActionIndexCache.Create(this.Reload2ActionName);
			this._rotateLeftAnimationActionIndex = ActionIndexCache.Create(this.RotateLeftActionName);
			this._rotateRightAnimationActionIndex = ActionIndexCache.Create(this.RotateRightActionName);
			this._loadAmmoBeginAnimationActionIndex = ActionIndexCache.Create(this.LoadAmmoBeginActionName);
			this._loadAmmoEndAnimationActionIndex = ActionIndexCache.Create(this.LoadAmmoEndActionName);
			this._reload2IdleActionIndex = ActionIndexCache.Create(this.Reload2IdleActionName);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000AF004 File Offset: 0x000AD204
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new MangonelAI(this);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000AF00C File Offset: 0x000AD20C
		public override void AfterMissionStart()
		{
			if (this.AmmoPickUpStandingPoints != null)
			{
				foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement in this.AmmoPickUpStandingPoints)
				{
					standingPointWithWeaponRequirement.LockUserFrames = true;
				}
			}
			this.UpdateProjectilePosition();
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000AF06C File Offset: 0x000AD26C
		public override SiegeEngineType GetSiegeEngineType()
		{
			if (this._defaultSide != BattleSideEnum.Attacker)
			{
				return DefaultSiegeEngineTypes.Catapult;
			}
			return DefaultSiegeEngineTypes.Onager;
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000AF084 File Offset: 0x000AD284
		protected internal override void OnInit()
		{
			List<SynchedMissionObject> list = base.GameEntity.CollectObjectsWithTag("rope");
			if (list.Count > 0)
			{
				this._rope = list[0];
			}
			list = base.GameEntity.CollectObjectsWithTag("body");
			this._body = list[0];
			this._bodySkeleton = this._body.GameEntity.Skeleton;
			this.RotationObject = this._body;
			List<GameEntity> list2 = base.GameEntity.CollectChildrenEntitiesWithTag("vertical_adjuster");
			this._verticalAdjuster = list2[0];
			this._verticalAdjusterSkeleton = this._verticalAdjuster.Skeleton;
			if (this._verticalAdjusterSkeleton != null)
			{
				this._verticalAdjusterSkeleton.SetAnimationAtChannel(this.MangonelAimAnimation, 0, 1f, -1f, 0f);
			}
			this._verticalAdjusterStartingLocalFrame = this._verticalAdjuster.GetFrame();
			this._verticalAdjusterStartingLocalFrame = this._body.GameEntity.GetBoneEntitialFrameWithIndex(0).TransformToLocal(this._verticalAdjusterStartingLocalFrame);
			base.OnInit();
			this.timeGapBetweenShootActionAndProjectileLeaving = 0.23f;
			this.timeGapBetweenShootingEndAndReloadingStart = 0f;
			this._rotateStandingPoints = new List<StandingPoint>();
			if (base.StandingPoints != null)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					if (standingPoint.GameEntity.HasTag("rotate"))
					{
						if (standingPoint.GameEntity.HasTag("left") && this._rotateStandingPoints.Count > 0)
						{
							this._rotateStandingPoints.Insert(0, standingPoint);
						}
						else
						{
							this._rotateStandingPoints.Add(standingPoint);
						}
					}
				}
				MatrixFrame globalFrame = this._body.GameEntity.GetGlobalFrame();
				this._standingPointLocalIKFrames = new MatrixFrame[base.StandingPoints.Count];
				for (int i = 0; i < base.StandingPoints.Count; i++)
				{
					this._standingPointLocalIKFrames[i] = base.StandingPoints[i].GameEntity.GetGlobalFrame().TransformToLocal(globalFrame);
					base.StandingPoints[i].AddComponent(new ClearHandInverseKinematicsOnStopUsageComponent());
				}
			}
			this._missileBoneIndex = Skeleton.GetBoneIndexFromName(this.Skeletons[0].GetName(), this._missileBoneName);
			this.ApplyAimChange();
			foreach (StandingPoint standingPoint2 in this.ReloadStandingPoints)
			{
				if (standingPoint2 != base.PilotStandingPoint)
				{
					this._reloadWithoutPilot = standingPoint2;
				}
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				this.SetActivationLoadAmmoPoint(false);
			}
			this.EnemyRangeToStopUsing = 9f;
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000AF370 File Offset: 0x000AD570
		protected internal override void OnEditorInit()
		{
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000AF372 File Offset: 0x000AD572
		protected override bool CanRotate()
		{
			return base.State == RangedSiegeWeapon.WeaponState.Idle || base.State == RangedSiegeWeapon.WeaponState.LoadingAmmo || base.State == RangedSiegeWeapon.WeaponState.WaitingBeforeIdle;
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000AF390 File Offset: 0x000AD590
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents())
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000AF3B0 File Offset: 0x000AD5B0
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement in this.AmmoPickUpStandingPoints)
				{
					if (standingPointWithWeaponRequirement.HasUser)
					{
						Agent userAgent = standingPointWithWeaponRequirement.UserAgent;
						ActionIndexValueCache currentActionValue = userAgent.GetCurrentActionValue(1);
						if (!(currentActionValue == Mangonel.act_pickup_boulder_begin))
						{
							if (currentActionValue == Mangonel.act_pickup_boulder_end)
							{
								MissionWeapon missionWeapon = new MissionWeapon(this.OriginalMissileItem, null, null, 1);
								userAgent.EquipWeaponToExtraSlotAndWield(ref missionWeapon);
								userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.None);
								this.ConsumeAmmo();
								if (userAgent.IsAIControlled)
								{
									if (!this.LoadAmmoStandingPoint.HasUser && !this.LoadAmmoStandingPoint.IsDeactivated)
									{
										userAgent.AIMoveToGameObjectEnable(this.LoadAmmoStandingPoint, this, base.Ai.GetScriptedFrameFlags(userAgent));
									}
									else if (this.ReloaderAgentOriginalPoint != null && !this.ReloaderAgentOriginalPoint.HasUser && !this.ReloaderAgentOriginalPoint.HasAIMovingTo)
									{
										userAgent.AIMoveToGameObjectEnable(this.ReloaderAgentOriginalPoint, this, base.Ai.GetScriptedFrameFlags(userAgent));
									}
									else
									{
										Agent reloaderAgent = this.ReloaderAgent;
										if (reloaderAgent != null)
										{
											Formation formation = reloaderAgent.Formation;
											if (formation != null)
											{
												formation.AttachUnit(this.ReloaderAgent);
											}
										}
										this.ReloaderAgent = null;
									}
								}
							}
							else if (!userAgent.SetActionChannel(1, Mangonel.act_pickup_boulder_begin, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && userAgent.Controller != Agent.ControllerType.AI)
							{
								userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							}
						}
					}
				}
			}
			switch (base.State)
			{
			case RangedSiegeWeapon.WeaponState.LoadingAmmo:
				if (!GameNetwork.IsClientOrReplay)
				{
					if (this.LoadAmmoStandingPoint.HasUser)
					{
						Agent userAgent2 = this.LoadAmmoStandingPoint.UserAgent;
						if (userAgent2.GetCurrentActionValue(1) == this._loadAmmoEndAnimationActionIndex)
						{
							EquipmentIndex wieldedItemIndex = userAgent2.GetWieldedItemIndex(Agent.HandIndex.MainHand);
							if (wieldedItemIndex != EquipmentIndex.None && userAgent2.Equipment[wieldedItemIndex].CurrentUsageItem.WeaponClass == this.OriginalMissileItem.PrimaryWeapon.WeaponClass)
							{
								base.ChangeProjectileEntityServer(userAgent2, userAgent2.Equipment[wieldedItemIndex].Item.StringId);
								userAgent2.RemoveEquippedWeapon(wieldedItemIndex);
								this._timeElapsedAfterLoading = 0f;
								base.Projectile.SetVisibleSynched(true, false);
								base.State = RangedSiegeWeapon.WeaponState.WaitingBeforeIdle;
								return;
							}
							userAgent2.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.None);
							if (!userAgent2.IsPlayerControlled)
							{
								base.SendAgentToAmmoPickup(userAgent2);
								return;
							}
						}
						else if (userAgent2.GetCurrentActionValue(1) != this._loadAmmoBeginAnimationActionIndex && !userAgent2.SetActionChannel(1, this._loadAmmoBeginAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
						{
							for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
							{
								if (!userAgent2.Equipment[equipmentIndex].IsEmpty && userAgent2.Equipment[equipmentIndex].CurrentUsageItem.WeaponClass == this.OriginalMissileItem.PrimaryWeapon.WeaponClass)
								{
									userAgent2.RemoveEquippedWeapon(equipmentIndex);
								}
							}
							userAgent2.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.None);
							if (!userAgent2.IsPlayerControlled)
							{
								base.SendAgentToAmmoPickup(userAgent2);
								return;
							}
						}
					}
					else if (this.LoadAmmoStandingPoint.HasAIMovingTo)
					{
						Agent movingAgent = this.LoadAmmoStandingPoint.MovingAgent;
						EquipmentIndex wieldedItemIndex2 = movingAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
						if (wieldedItemIndex2 == EquipmentIndex.None || movingAgent.Equipment[wieldedItemIndex2].CurrentUsageItem.WeaponClass != this.OriginalMissileItem.PrimaryWeapon.WeaponClass)
						{
							movingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.None);
							base.SendAgentToAmmoPickup(movingAgent);
						}
					}
				}
				break;
			case RangedSiegeWeapon.WeaponState.WaitingBeforeIdle:
				this._timeElapsedAfterLoading += dt;
				if (this._timeElapsedAfterLoading > 1f)
				{
					base.State = RangedSiegeWeapon.WeaponState.Idle;
					return;
				}
				break;
			case RangedSiegeWeapon.WeaponState.Reloading:
			case RangedSiegeWeapon.WeaponState.ReloadingPaused:
				break;
			default:
				return;
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000AF7DC File Offset: 0x000AD9DC
		protected internal override void OnTickParallel(float dt)
		{
			base.OnTickParallel(dt);
			if (!base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (base.State == RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving)
			{
				this.UpdateProjectilePosition();
			}
			if (this._verticalAdjusterSkeleton != null)
			{
				float parameter = MBMath.ClampFloat((this.currentReleaseAngle - this.BottomReleaseAngleRestriction) / (this.TopReleaseAngleRestriction - this.BottomReleaseAngleRestriction), 0f, 1f);
				this._verticalAdjusterSkeleton.SetAnimationParameterAtChannel(0, parameter);
			}
			MatrixFrame matrixFrame = this.Skeletons[0].GetBoneEntitialFrameWithIndex(0).TransformToParent(this._verticalAdjusterStartingLocalFrame);
			this._verticalAdjuster.SetFrame(ref matrixFrame);
			MatrixFrame globalFrame = this._body.GameEntity.GetGlobalFrame();
			for (int i = 0; i < base.StandingPoints.Count; i++)
			{
				if (base.StandingPoints[i].HasUser)
				{
					if (base.StandingPoints[i].UserAgent.IsInBeingStruckAction)
					{
						base.StandingPoints[i].UserAgent.ClearHandInverseKinematics();
					}
					else if (base.StandingPoints[i] != base.PilotStandingPoint)
					{
						if (base.StandingPoints[i].UserAgent.GetCurrentActionValue(1) != this._reload2IdleActionIndex)
						{
							base.StandingPoints[i].UserAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(this._standingPointLocalIKFrames[i], globalFrame, 0f);
						}
						else
						{
							base.StandingPoints[i].UserAgent.ClearHandInverseKinematics();
						}
					}
					else
					{
						base.StandingPoints[i].UserAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(this._standingPointLocalIKFrames[i], globalFrame, 0f);
					}
				}
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				for (int j = 0; j < this._rotateStandingPoints.Count; j++)
				{
					StandingPoint standingPoint = this._rotateStandingPoints[j];
					if (standingPoint.HasUser && !standingPoint.UserAgent.SetActionChannel(1, (j == 0) ? this._rotateLeftAnimationActionIndex : this._rotateRightAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && standingPoint.UserAgent.Controller != Agent.ControllerType.AI)
					{
						standingPoint.UserAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
				if (base.PilotAgent != null)
				{
					ActionIndexValueCache currentActionValue = base.PilotAgent.GetCurrentActionValue(1);
					if (base.State == RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving)
					{
						if (base.PilotAgent.IsInBeingStruckAction)
						{
							if (currentActionValue != ActionIndexValueCache.act_none && currentActionValue != Mangonel.act_strike_bent_over)
							{
								base.PilotAgent.SetActionChannel(1, Mangonel.act_strike_bent_over, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
							}
						}
						else if (!base.PilotAgent.SetActionChannel(1, this._shootAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && base.PilotAgent.Controller != Agent.ControllerType.AI)
						{
							base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
					}
					else if (!base.PilotAgent.SetActionChannel(1, this._idleAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && currentActionValue != this._reload1AnimationActionIndex && currentActionValue != this._shootAnimationActionIndex && base.PilotAgent.Controller != Agent.ControllerType.AI)
					{
						base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
				if (this._reloadWithoutPilot.HasUser)
				{
					Agent userAgent = this._reloadWithoutPilot.UserAgent;
					if (!userAgent.SetActionChannel(1, this._reload2IdleActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && userAgent.GetCurrentActionValue(1) != this._reload2AnimationActionIndex && userAgent.Controller != Agent.ControllerType.AI)
					{
						userAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
			}
			RangedSiegeWeapon.WeaponState state = base.State;
			if (state == RangedSiegeWeapon.WeaponState.Reloading)
			{
				foreach (StandingPoint standingPoint2 in this.ReloadStandingPoints)
				{
					if (standingPoint2.HasUser)
					{
						ActionIndexValueCache currentActionValue2 = standingPoint2.UserAgent.GetCurrentActionValue(1);
						if (currentActionValue2 == this._reload1AnimationActionIndex || currentActionValue2 == this._reload2AnimationActionIndex)
						{
							standingPoint2.UserAgent.SetCurrentActionProgress(1, this._bodySkeleton.GetAnimationParameterAtChannel(0));
						}
						else if (!GameNetwork.IsClientOrReplay)
						{
							ActionIndexCache actionIndexCache = (standingPoint2 == base.PilotStandingPoint) ? this._reload1AnimationActionIndex : this._reload2AnimationActionIndex;
							if (!standingPoint2.UserAgent.SetActionChannel(1, actionIndexCache, false, 0UL, 0f, 1f, -0.2f, 0.4f, this._bodySkeleton.GetAnimationParameterAtChannel(0), false, -0.2f, 0, true) && standingPoint2.UserAgent.Controller != Agent.ControllerType.AI)
							{
								standingPoint2.UserAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000AFD40 File Offset: 0x000ADF40
		protected override void SetActivationLoadAmmoPoint(bool activate)
		{
			this.LoadAmmoStandingPoint.SetIsDeactivatedSynched(!activate);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000AFD54 File Offset: 0x000ADF54
		protected override void UpdateProjectilePosition()
		{
			MatrixFrame boneEntitialFrameWithIndex = this.Skeletons[0].GetBoneEntitialFrameWithIndex(this._missileBoneIndex);
			base.Projectile.GameEntity.SetFrame(ref boneEntitialFrameWithIndex);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000AFD88 File Offset: 0x000ADF88
		protected override void OnRangedSiegeWeaponStateChange()
		{
			base.OnRangedSiegeWeaponStateChange();
			RangedSiegeWeapon.WeaponState state = base.State;
			if (state != RangedSiegeWeapon.WeaponState.Idle)
			{
				if (state != RangedSiegeWeapon.WeaponState.Shooting)
				{
					if (state == RangedSiegeWeapon.WeaponState.WaitingBeforeIdle)
					{
						this.UpdateProjectilePosition();
						return;
					}
				}
				else
				{
					if (!GameNetwork.IsClientOrReplay)
					{
						base.Projectile.SetVisibleSynched(false, false);
						return;
					}
					base.Projectile.GameEntity.SetVisibilityExcludeParents(false);
					return;
				}
			}
			else
			{
				if (!GameNetwork.IsClientOrReplay)
				{
					base.Projectile.SetVisibleSynched(true, false);
					return;
				}
				base.Projectile.GameEntity.SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000AFE01 File Offset: 0x000AE001
		protected override void GetSoundEventIndices()
		{
			this.MoveSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/mangonel/move");
			this.ReloadSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/mangonel/reload");
			this.ReloadEndSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/mangonel/reload_end");
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000AFE34 File Offset: 0x000AE034
		protected override float HorizontalAimSensitivity
		{
			get
			{
				if (this._defaultSide == BattleSideEnum.Defender)
				{
					return 0.25f;
				}
				float num = 0.05f;
				foreach (StandingPoint standingPoint in this._rotateStandingPoints)
				{
					if (standingPoint.HasUser && !standingPoint.UserAgent.IsInBeingStruckAction)
					{
						num += 0.1f;
					}
				}
				return num;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000AFEB4 File Offset: 0x000AE0B4
		protected override float VerticalAimSensitivity
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000AFEBC File Offset: 0x000AE0BC
		protected override Vec3 ShootingDirection
		{
			get
			{
				Mat3 rotation = this._body.GameEntity.GetGlobalFrame().rotation;
				rotation.RotateAboutSide(-this.currentReleaseAngle);
				return rotation.TransformToParent(new Vec3(0f, -1f, 0f, -1f));
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000AFF0D File Offset: 0x000AE10D
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x000AFF39 File Offset: 0x000AE139
		protected override bool HasAmmo
		{
			get
			{
				return base.HasAmmo || base.CurrentlyUsedAmmoPickUpPoint != null || this.LoadAmmoStandingPoint.HasUser || this.LoadAmmoStandingPoint.HasAIMovingTo;
			}
			set
			{
				base.HasAmmo = value;
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000AFF44 File Offset: 0x000AE144
		protected override void ApplyAimChange()
		{
			base.ApplyAimChange();
			this.ShootingDirection.Normalize();
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000AFF66 File Offset: 0x000AE166
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			if (!gameEntity.HasTag(this.AmmoPickUpTag))
			{
				return new TextObject("{=NbpcDXtJ}Mangonel", null).ToString();
			}
			return new TextObject("{=pzfbPbWW}Boulder", null).ToString();
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000AFF98 File Offset: 0x000AE198
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject;
			if (usableGameObject.GameEntity.HasTag("reload"))
			{
				textObject = new TextObject((base.PilotStandingPoint == usableGameObject) ? "{=fEQAPJ2e}{KEY} Use" : "{=Na81xuXn}{KEY} Rearm", null);
			}
			else if (usableGameObject.GameEntity.HasTag("rotate"))
			{
				textObject = new TextObject("{=5wx4BF5h}{KEY} Rotate", null);
			}
			else if (usableGameObject.GameEntity.HasTag(this.AmmoPickUpTag))
			{
				textObject = new TextObject("{=bNYm3K6b}{KEY} Pick Up", null);
			}
			else if (usableGameObject.GameEntity.HasTag("ammoload"))
			{
				textObject = new TextObject("{=ibC4xPoo}{KEY} Load Ammo", null);
			}
			else
			{
				textObject = new TextObject("{=fEQAPJ2e}{KEY} Use", null);
			}
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000B0060 File Offset: 0x000AE260
		public override TargetFlags GetTargetFlags()
		{
			TargetFlags targetFlags = TargetFlags.None;
			targetFlags |= TargetFlags.IsFlammable;
			targetFlags |= TargetFlags.IsSiegeEngine;
			if (this.Side == BattleSideEnum.Attacker)
			{
				targetFlags |= TargetFlags.IsAttacker;
			}
			if (base.IsDestroyed || this.IsDeactivated)
			{
				targetFlags |= TargetFlags.NotAThreat;
			}
			if (this.Side == BattleSideEnum.Attacker && DebugSiegeBehavior.DebugDefendState == DebugSiegeBehavior.DebugStateDefender.DebugDefendersToMangonels)
			{
				targetFlags |= TargetFlags.DebugThreat;
			}
			if (this.Side == BattleSideEnum.Defender && DebugSiegeBehavior.DebugAttackState == DebugSiegeBehavior.DebugStateAttacker.DebugAttackersToMangonels)
			{
				targetFlags |= TargetFlags.DebugThreat;
			}
			return targetFlags;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000B00CC File Offset: 0x000AE2CC
		public override float GetTargetValue(List<Vec3> weaponPos)
		{
			return 40f * base.GetUserMultiplierOfWeapon() * this.GetDistanceMultiplierOfWeapon(weaponPos[0]) * base.GetHitPointMultiplierOfWeapon();
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000B00F0 File Offset: 0x000AE2F0
		public override float ProcessTargetValue(float baseValue, TargetFlags flags)
		{
			if (flags.HasAnyFlag(TargetFlags.NotAThreat))
			{
				return -1000f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSiegeEngine))
			{
				baseValue *= 10000f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsStructure))
			{
				baseValue *= 2.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSmall))
			{
				baseValue *= 8f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsMoving))
			{
				baseValue *= 8f;
			}
			if (flags.HasAnyFlag(TargetFlags.DebugThreat))
			{
				baseValue *= 10000f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSiegeTower))
			{
				baseValue *= 8f;
			}
			return baseValue;
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000B0183 File Offset: 0x000AE383
		protected override float GetDetachmentWeightAux(BattleSideEnum side)
		{
			return base.GetDetachmentWeightAuxForExternalAmmoWeapons(side);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000B018C File Offset: 0x000AE38C
		public void SetSpawnedFromSpawner()
		{
			this._spawnedFromSpawner = true;
		}

		// Token: 0x040011C3 RID: 4547
		private const string BodyTag = "body";

		// Token: 0x040011C4 RID: 4548
		private const string RopeTag = "rope";

		// Token: 0x040011C5 RID: 4549
		private const string RotateTag = "rotate";

		// Token: 0x040011C6 RID: 4550
		private const string LeftTag = "left";

		// Token: 0x040011C7 RID: 4551
		private const string VerticalAdjusterTag = "vertical_adjuster";

		// Token: 0x040011C8 RID: 4552
		private static readonly ActionIndexCache act_usage_mangonel_idle = ActionIndexCache.Create("act_usage_mangonel_idle");

		// Token: 0x040011C9 RID: 4553
		private static readonly ActionIndexCache act_usage_mangonel_load_ammo_begin = ActionIndexCache.Create("act_usage_mangonel_load_ammo_begin");

		// Token: 0x040011CA RID: 4554
		private static readonly ActionIndexCache act_usage_mangonel_load_ammo_end = ActionIndexCache.Create("act_usage_mangonel_load_ammo_end");

		// Token: 0x040011CB RID: 4555
		private static readonly ActionIndexCache act_pickup_boulder_begin = ActionIndexCache.Create("act_pickup_boulder_begin");

		// Token: 0x040011CC RID: 4556
		private static readonly ActionIndexCache act_pickup_boulder_end = ActionIndexCache.Create("act_pickup_boulder_end");

		// Token: 0x040011CD RID: 4557
		private static readonly ActionIndexCache act_usage_mangonel_reload = ActionIndexCache.Create("act_usage_mangonel_reload");

		// Token: 0x040011CE RID: 4558
		private static readonly ActionIndexCache act_usage_mangonel_reload_2 = ActionIndexCache.Create("act_usage_mangonel_reload_2");

		// Token: 0x040011CF RID: 4559
		private static readonly ActionIndexCache act_usage_mangonel_reload_2_idle = ActionIndexCache.Create("act_usage_mangonel_reload_2_idle");

		// Token: 0x040011D0 RID: 4560
		private static readonly ActionIndexCache act_usage_mangonel_rotate_left = ActionIndexCache.Create("act_usage_mangonel_rotate_left");

		// Token: 0x040011D1 RID: 4561
		private static readonly ActionIndexCache act_usage_mangonel_rotate_right = ActionIndexCache.Create("act_usage_mangonel_rotate_right");

		// Token: 0x040011D2 RID: 4562
		private static readonly ActionIndexCache act_usage_mangonel_shoot = ActionIndexCache.Create("act_usage_mangonel_shoot");

		// Token: 0x040011D3 RID: 4563
		private static readonly ActionIndexCache act_usage_mangonel_big_idle = ActionIndexCache.Create("act_usage_mangonel_big_idle");

		// Token: 0x040011D4 RID: 4564
		private static readonly ActionIndexCache act_usage_mangonel_big_shoot = ActionIndexCache.Create("act_usage_mangonel_big_shoot");

		// Token: 0x040011D5 RID: 4565
		private static readonly ActionIndexCache act_usage_mangonel_big_reload = ActionIndexCache.Create("act_usage_mangonel_big_reload");

		// Token: 0x040011D6 RID: 4566
		private static readonly ActionIndexCache act_usage_mangonel_big_load_ammo_begin = ActionIndexCache.Create("act_usage_mangonel_big_load_ammo_begin");

		// Token: 0x040011D7 RID: 4567
		private static readonly ActionIndexCache act_usage_mangonel_big_load_ammo_end = ActionIndexCache.Create("act_usage_mangonel_big_load_ammo_end");

		// Token: 0x040011D8 RID: 4568
		private static readonly ActionIndexCache act_strike_bent_over = ActionIndexCache.Create("act_strike_bent_over");

		// Token: 0x040011D9 RID: 4569
		private string _missileBoneName = "end_throwarm";

		// Token: 0x040011DA RID: 4570
		private List<StandingPoint> _rotateStandingPoints;

		// Token: 0x040011DB RID: 4571
		private SynchedMissionObject _body;

		// Token: 0x040011DC RID: 4572
		private SynchedMissionObject _rope;

		// Token: 0x040011DD RID: 4573
		private GameEntity _verticalAdjuster;

		// Token: 0x040011DE RID: 4574
		private MatrixFrame _verticalAdjusterStartingLocalFrame;

		// Token: 0x040011DF RID: 4575
		private Skeleton _verticalAdjusterSkeleton;

		// Token: 0x040011E0 RID: 4576
		private Skeleton _bodySkeleton;

		// Token: 0x040011E1 RID: 4577
		private float _timeElapsedAfterLoading;

		// Token: 0x040011E2 RID: 4578
		private MatrixFrame[] _standingPointLocalIKFrames;

		// Token: 0x040011E3 RID: 4579
		private StandingPoint _reloadWithoutPilot;

		// Token: 0x040011E4 RID: 4580
		public string MangonelBodySkeleton = "mangonel_skeleton";

		// Token: 0x040011E5 RID: 4581
		public string MangonelBodyFire = "mangonel_fire";

		// Token: 0x040011E6 RID: 4582
		public string MangonelBodyReload = "mangonel_set_up";

		// Token: 0x040011E7 RID: 4583
		public string MangonelRopeFire = "mangonel_holder_fire";

		// Token: 0x040011E8 RID: 4584
		public string MangonelRopeReload = "mangonel_holder_set_up";

		// Token: 0x040011E9 RID: 4585
		public string MangonelAimAnimation = "mangonel_a_anglearm_state";

		// Token: 0x040011EA RID: 4586
		public string ProjectileBoneName = "end_throwarm";

		// Token: 0x040011EB RID: 4587
		public string IdleActionName;

		// Token: 0x040011EC RID: 4588
		public string ShootActionName;

		// Token: 0x040011ED RID: 4589
		public string Reload1ActionName;

		// Token: 0x040011EE RID: 4590
		public string Reload2ActionName;

		// Token: 0x040011EF RID: 4591
		public string RotateLeftActionName;

		// Token: 0x040011F0 RID: 4592
		public string RotateRightActionName;

		// Token: 0x040011F1 RID: 4593
		public string LoadAmmoBeginActionName;

		// Token: 0x040011F2 RID: 4594
		public string LoadAmmoEndActionName;

		// Token: 0x040011F3 RID: 4595
		public string Reload2IdleActionName;

		// Token: 0x040011F4 RID: 4596
		public float ProjectileSpeed = 40f;

		// Token: 0x040011F5 RID: 4597
		private ActionIndexCache _idleAnimationActionIndex;

		// Token: 0x040011F6 RID: 4598
		private ActionIndexCache _shootAnimationActionIndex;

		// Token: 0x040011F7 RID: 4599
		private ActionIndexCache _reload1AnimationActionIndex;

		// Token: 0x040011F8 RID: 4600
		private ActionIndexCache _reload2AnimationActionIndex;

		// Token: 0x040011F9 RID: 4601
		private ActionIndexCache _rotateLeftAnimationActionIndex;

		// Token: 0x040011FA RID: 4602
		private ActionIndexCache _rotateRightAnimationActionIndex;

		// Token: 0x040011FB RID: 4603
		private ActionIndexCache _loadAmmoBeginAnimationActionIndex;

		// Token: 0x040011FC RID: 4604
		private ActionIndexCache _loadAmmoEndAnimationActionIndex;

		// Token: 0x040011FD RID: 4605
		private ActionIndexCache _reload2IdleActionIndex;

		// Token: 0x040011FE RID: 4606
		private sbyte _missileBoneIndex;
	}
}
