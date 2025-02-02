using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000342 RID: 834
	public class SiegeWeaponMovementComponent : UsableMissionObjectComponent
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000B8F5D File Offset: 0x000B715D
		public bool HasApproachedTarget
		{
			get
			{
				return !this._pathTracker.PathExists() || this._pathTracker.PathTraveledPercentage > 0.7f;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000B8F80 File Offset: 0x000B7180
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x000B8F88 File Offset: 0x000B7188
		public Vec3 Velocity { get; private set; }

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B8F94 File Offset: 0x000B7194
		protected internal override void OnAdded(Scene scene)
		{
			base.OnAdded(scene);
			this._path = scene.GetPathWithName(this.PathEntityName);
			MatrixFrame matrixFrame = this.MainObject.GameEntity.GetFrame();
			Vec3 scaleVector = matrixFrame.rotation.GetScaleVector();
			this._wheels = this.MainObject.GameEntity.CollectChildrenEntitiesWithTag("wheel");
			this._standingPoints = this.MainObject.GameEntity.CollectObjectsWithTag("move");
			this._pathTracker = new PathTracker(this._path, scaleVector);
			this._pathTracker.Reset();
			this.SetTargetFrame();
			MatrixFrame globalFrame = this.MainObject.GameEntity.GetGlobalFrame();
			this._standingPointLocalIKFrames = new MatrixFrame[this._standingPoints.Count];
			for (int i = 0; i < this._standingPoints.Count; i++)
			{
				MatrixFrame[] standingPointLocalIKFrames = this._standingPointLocalIKFrames;
				int num = i;
				matrixFrame = this._standingPoints[i].GameEntity.GetGlobalFrame();
				standingPointLocalIKFrames[num] = matrixFrame.TransformToLocal(globalFrame);
				this._standingPoints[i].AddComponent(new ClearHandInverseKinematicsOnStopUsageComponent());
			}
			this.Velocity = Vec3.Zero;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B90BC File Offset: 0x000B72BC
		public void HighlightPath()
		{
			MatrixFrame[] array = new MatrixFrame[this._path.NumberOfPoints];
			this._path.GetPoints(array);
			for (int i = 1; i < this._path.NumberOfPoints; i++)
			{
				MatrixFrame matrixFrame = array[i];
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B910C File Offset: 0x000B730C
		public void SetupGhostEntity()
		{
			Path pathWithName = this.MainObject.Scene.GetPathWithName(this.PathEntityName);
			Vec3 scaleVector = this.MainObject.GameEntity.GetFrame().rotation.GetScaleVector();
			this._pathTracker = new PathTracker(pathWithName, scaleVector);
			this._ghostEntityPathTracker = new PathTracker(pathWithName, scaleVector);
			this._ghostObjectPos = ((pathWithName != null) ? pathWithName.GetTotalLength() : 0f);
			this._wheels = this.MainObject.GameEntity.CollectChildrenEntitiesWithTag("wheel");
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000B919F File Offset: 0x000B739F
		public bool HasArrivedAtTarget
		{
			get
			{
				return !this._pathTracker.PathExists() || this._pathTracker.HasReachedEnd;
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B91BC File Offset: 0x000B73BC
		private void SetPath()
		{
			Path pathWithName = this.MainObject.Scene.GetPathWithName(this.PathEntityName);
			Vec3 scaleVector = this.MainObject.GameEntity.GetFrame().rotation.GetScaleVector();
			this._pathTracker = new PathTracker(pathWithName, scaleVector);
			this._ghostEntityPathTracker = new PathTracker(pathWithName, scaleVector);
			this._ghostObjectPos = ((pathWithName != null) ? pathWithName.GetTotalLength() : 0f);
			this.UpdateGhostObject(0f);
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000B923F File Offset: 0x000B743F
		// (set) Token: 0x06002DA7 RID: 11687 RVA: 0x000B9247 File Offset: 0x000B7447
		public float CurrentSpeed { get; private set; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000B9250 File Offset: 0x000B7450
		// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x000B9258 File Offset: 0x000B7458
		public int MovementSoundCodeID { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000B9261 File Offset: 0x000B7461
		// (set) Token: 0x06002DAB RID: 11691 RVA: 0x000B9269 File Offset: 0x000B7469
		public float MinSpeed { get; set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000B9272 File Offset: 0x000B7472
		// (set) Token: 0x06002DAD RID: 11693 RVA: 0x000B927A File Offset: 0x000B747A
		public float MaxSpeed { get; set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000B9283 File Offset: 0x000B7483
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x000B928B File Offset: 0x000B748B
		public string PathEntityName { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B9294 File Offset: 0x000B7494
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x000B929C File Offset: 0x000B749C
		public float GhostEntitySpeedMultiplier { get; set; }

		// Token: 0x17000874 RID: 2164
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000B92A5 File Offset: 0x000B74A5
		public float WheelDiameter
		{
			set
			{
				this._wheelDiameter = value;
				this._wheelCircumference = this._wheelDiameter * 3.1415927f;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000B92C0 File Offset: 0x000B74C0
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000B92C8 File Offset: 0x000B74C8
		public SynchedMissionObject MainObject { get; set; }

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000B92D1 File Offset: 0x000B74D1
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.UpdateGhostObject(dt);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000B92E1 File Offset: 0x000B74E1
		public void SetGhostVisibility(bool isVisible)
		{
			this.MainObject.GameEntity.CollectChildrenEntitiesWithTag("ghost_object").FirstOrDefault<GameEntity>().SetVisibilityExcludeParents(isVisible);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000B9303 File Offset: 0x000B7503
		public void OnEditorInit()
		{
			this.SetPath();
			this._wheels = this.MainObject.GameEntity.CollectChildrenEntitiesWithTag("wheel");
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000B9328 File Offset: 0x000B7528
		private void UpdateGhostObject(float dt)
		{
			if (this._pathTracker.HasChanged)
			{
				this.SetPath();
				this._pathTracker.Advance(this._pathTracker.GetPathLength());
				this._ghostEntityPathTracker.Advance(this._ghostEntityPathTracker.GetPathLength());
			}
			List<GameEntity> list = this.MainObject.GameEntity.CollectChildrenEntitiesWithTag("ghost_object");
			if (this.MainObject.GameEntity.IsSelectedOnEditor())
			{
				if (this._pathTracker.IsValid)
				{
					float num = 10f;
					if (Input.DebugInput.IsShiftDown())
					{
						num = 1f;
					}
					if (Input.DebugInput.IsKeyDown(InputKey.MouseScrollUp))
					{
						this._ghostObjectPos += dt * num;
					}
					else if (Input.DebugInput.IsKeyDown(InputKey.MouseScrollDown))
					{
						this._ghostObjectPos -= dt * num;
					}
					this._ghostObjectPos = MBMath.ClampFloat(this._ghostObjectPos, 0f, this._pathTracker.GetPathLength());
				}
				else
				{
					this._ghostObjectPos = 0f;
				}
			}
			if (list.Count > 0)
			{
				GameEntity gameEntity = list[0];
				IPathHolder pathHolder;
				if ((pathHolder = (this.MainObject as IPathHolder)) != null && pathHolder.EditorGhostEntityMove)
				{
					if (this._ghostEntityPathTracker.IsValid)
					{
						this._ghostEntityPathTracker.Advance(0.05f * this.GhostEntitySpeedMultiplier);
						MatrixFrame matrixFrame = MatrixFrame.Identity;
						matrixFrame = this.LinearInterpolatedIK(ref this._ghostEntityPathTracker);
						gameEntity.SetGlobalFrame(matrixFrame);
						if (this._ghostEntityPathTracker.HasReachedEnd)
						{
							this._ghostEntityPathTracker.Reset();
							return;
						}
					}
				}
				else if (this._pathTracker.IsValid)
				{
					this._pathTracker.Advance(this._ghostObjectPos);
					MatrixFrame matrixFrame2 = this.LinearInterpolatedIK(ref this._pathTracker);
					GameEntity gameEntity2 = gameEntity;
					MatrixFrame matrixFrame3 = this.FindGroundFrameForWheels(ref matrixFrame2);
					gameEntity2.SetGlobalFrame(matrixFrame3);
					this._pathTracker.Reset();
				}
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B9508 File Offset: 0x000B7708
		private void RotateWheels(float angleInRadian)
		{
			foreach (GameEntity gameEntity in this._wheels)
			{
				MatrixFrame frame = gameEntity.GetFrame();
				frame.rotation.RotateAboutSide(angleInRadian);
				gameEntity.SetFrame(ref frame);
			}
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000B9570 File Offset: 0x000B7770
		private MatrixFrame LinearInterpolatedIK(ref PathTracker pathTracker)
		{
			MatrixFrame m;
			Vec3 vec;
			pathTracker.CurrentFrameAndColor(out m, out vec);
			MatrixFrame m2 = this.FindGroundFrameForWheels(ref m);
			return MatrixFrame.Lerp(m, m2, vec.x);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000B959E File Offset: 0x000B779E
		public void SetDistanceTraveledAsClient(float distance)
		{
			this._advancementError = distance - this._pathTracker.TotalDistanceTraveled;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000B95B3 File Offset: 0x000B77B3
		public override bool IsOnTickRequired()
		{
			return true;
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B95B8 File Offset: 0x000B77B8
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._ghostEntityPathTracker != null)
			{
				this.UpdateGhostObject(dt);
			}
			if (!this._pathTracker.PathExists() || this._pathTracker.HasReachedEnd)
			{
				this.CurrentSpeed = 0f;
				if (!GameNetwork.IsClientOrReplay)
				{
					foreach (StandingPoint standingPoint in this._standingPoints)
					{
						standingPoint.SetIsDeactivatedSynched(true);
					}
				}
			}
			this.TickSound();
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000B9654 File Offset: 0x000B7854
		public void TickParallelManually(float dt)
		{
			if (this._pathTracker.PathExists() && !this._pathTracker.HasReachedEnd)
			{
				int num = 0;
				foreach (StandingPoint standingPoint in this._standingPoints)
				{
					if (standingPoint.HasUser && !standingPoint.UserAgent.IsInBeingStruckAction)
					{
						num++;
					}
				}
				if (num > 0)
				{
					int count = this._standingPoints.Count;
					this.CurrentSpeed = MBMath.Lerp(this.MinSpeed, this.MaxSpeed, (float)(num - 1) / (float)(count - 1), 1E-05f);
					MatrixFrame globalFrame = this.MainObject.GameEntity.GetGlobalFrame();
					for (int i = 0; i < this._standingPoints.Count; i++)
					{
						StandingPoint standingPoint2 = this._standingPoints[i];
						if (standingPoint2.HasUser)
						{
							Agent userAgent = standingPoint2.UserAgent;
							ActionIndexValueCache action = userAgent.GetCurrentActionValue(0);
							ActionIndexValueCache action2 = userAgent.GetCurrentActionValue(1);
							if (action != SiegeWeaponMovementComponent.act_usage_siege_machine_push)
							{
								if (userAgent.SetActionChannel(0, SiegeWeaponMovementComponent.act_usage_siege_machine_push, false, 0UL, 0f, this.CurrentSpeed, MBAnimation.GetAnimationBlendInPeriod(MBActionSet.GetAnimationIndexOfAction(userAgent.ActionSet, SiegeWeaponMovementComponent.act_usage_siege_machine_push)) * this.CurrentSpeed, 0.4f, 0f, false, -0.2f, 0, true))
								{
									action = ActionIndexValueCache.Create(SiegeWeaponMovementComponent.act_usage_siege_machine_push);
								}
								else if (MBMath.IsBetween((int)userAgent.GetCurrentActionType(0), 47, 51) && action != SiegeWeaponMovementComponent.act_strike_bent_over && userAgent.SetActionChannel(0, SiegeWeaponMovementComponent.act_strike_bent_over, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
								{
									action = ActionIndexValueCache.Create(SiegeWeaponMovementComponent.act_strike_bent_over);
								}
							}
							if (action2 != SiegeWeaponMovementComponent.act_usage_siege_machine_push)
							{
								if (userAgent.SetActionChannel(1, SiegeWeaponMovementComponent.act_usage_siege_machine_push, false, 0UL, 0f, this.CurrentSpeed, MBAnimation.GetAnimationBlendInPeriod(MBActionSet.GetAnimationIndexOfAction(userAgent.ActionSet, SiegeWeaponMovementComponent.act_usage_siege_machine_push)) * this.CurrentSpeed, 0.4f, 0f, false, -0.2f, 0, true))
								{
									action2 = ActionIndexValueCache.Create(SiegeWeaponMovementComponent.act_usage_siege_machine_push);
								}
								else if (MBMath.IsBetween((int)userAgent.GetCurrentActionType(1), 47, 51) && action2 != SiegeWeaponMovementComponent.act_strike_bent_over && userAgent.SetActionChannel(1, SiegeWeaponMovementComponent.act_strike_bent_over, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
								{
									action2 = ActionIndexValueCache.Create(SiegeWeaponMovementComponent.act_strike_bent_over);
								}
							}
							if (action == SiegeWeaponMovementComponent.act_usage_siege_machine_push)
							{
								userAgent.SetCurrentActionSpeed(0, this.CurrentSpeed);
							}
							if (action2 == SiegeWeaponMovementComponent.act_usage_siege_machine_push)
							{
								userAgent.SetCurrentActionSpeed(1, this.CurrentSpeed);
							}
							if ((action == SiegeWeaponMovementComponent.act_usage_siege_machine_push || action == SiegeWeaponMovementComponent.act_strike_bent_over) && (action2 == SiegeWeaponMovementComponent.act_usage_siege_machine_push || action2 == SiegeWeaponMovementComponent.act_strike_bent_over))
							{
								standingPoint2.UserAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(this._standingPointLocalIKFrames[i], globalFrame, 0f);
							}
							else
							{
								standingPoint2.UserAgent.ClearHandInverseKinematics();
								if (!GameNetwork.IsClientOrReplay && userAgent.Controller != Agent.ControllerType.AI)
								{
									userAgent.StopUsingGameObjectMT(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
								}
							}
						}
					}
				}
				else
				{
					this.CurrentSpeed = this._advancementError;
				}
				if (!this.CurrentSpeed.ApproximatelyEqualsTo(0f, 1E-05f))
				{
					float num2 = this.CurrentSpeed * dt;
					if (!this._advancementError.ApproximatelyEqualsTo(0f, 1E-05f))
					{
						float num3 = 3f * this.CurrentSpeed * dt * (float)MathF.Sign(this._advancementError);
						if (MathF.Abs(num3) >= MathF.Abs(this._advancementError))
						{
							num3 = this._advancementError;
							this._advancementError = 0f;
						}
						else
						{
							this._advancementError -= num3;
						}
						num2 += num3;
					}
					this._pathTracker.Advance(num2);
					this.SetTargetFrame();
					float angleInRadian = num2 / this._wheelCircumference * 2f * 3.1415927f;
					this.RotateWheels(angleInRadian);
					if (GameNetwork.IsServerOrRecorder && this._pathTracker.TotalDistanceTraveled - this._lastSynchronizedDistance > 1f)
					{
						this._lastSynchronizedDistance = this._pathTracker.TotalDistanceTraveled;
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SetSiegeMachineMovementDistance(this.MainObject.Id, this._lastSynchronizedDistance));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					}
				}
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000B9AFC File Offset: 0x000B7CFC
		public MatrixFrame GetInitialFrame()
		{
			PathTracker pathTracker = new PathTracker(this._path, Vec3.One);
			pathTracker.Reset();
			return this.LinearInterpolatedIK(ref pathTracker);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B9B28 File Offset: 0x000B7D28
		private void SetTargetFrame()
		{
			if (!this._pathTracker.PathExists())
			{
				return;
			}
			MatrixFrame matrixFrame = this.LinearInterpolatedIK(ref this._pathTracker);
			GameEntity gameEntity = this.MainObject.GameEntity;
			this.Velocity = gameEntity.GlobalPosition;
			gameEntity.SetGlobalFrameMT(matrixFrame);
			this.Velocity = (gameEntity.GlobalPosition - this.Velocity).NormalizedCopy() * this.CurrentSpeed;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B9B9C File Offset: 0x000B7D9C
		public MatrixFrame GetTargetFrame()
		{
			float totalDistanceTraveled = this._pathTracker.TotalDistanceTraveled;
			this._pathTracker.Advance(1000000f);
			MatrixFrame currentFrame = this._pathTracker.CurrentFrame;
			this._pathTracker.Reset();
			this._pathTracker.Advance(totalDistanceTraveled);
			return currentFrame;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B9BE7 File Offset: 0x000B7DE7
		public void SetDestinationNavMeshIdState(bool enabled)
		{
			if (this.NavMeshIdToDisableOnDestination != -1)
			{
				Mission.Current.Scene.SetAbilityOfFacesWithId(this.NavMeshIdToDisableOnDestination, enabled);
			}
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000B9C08 File Offset: 0x000B7E08
		public void MoveToTargetAsClient()
		{
			if (this._pathTracker.IsValid)
			{
				float totalDistanceTraveled = this._pathTracker.TotalDistanceTraveled;
				this._pathTracker.Advance(1000000f);
				this.SetTargetFrame();
				float angleInRadian = (this._pathTracker.TotalDistanceTraveled - totalDistanceTraveled) / this._wheelCircumference * 2f * 3.1415927f;
				this.RotateWheels(angleInRadian);
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B9C6C File Offset: 0x000B7E6C
		private void TickSound()
		{
			if (this.CurrentSpeed > 0f)
			{
				this.PlayMovementSound();
				return;
			}
			this.StopMovementSound();
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B9C88 File Offset: 0x000B7E88
		private void PlayMovementSound()
		{
			if (!this._isMoveSoundPlaying)
			{
				this._movementSound = SoundEvent.CreateEvent(this.MovementSoundCodeID, this.MainObject.GameEntity.Scene);
				this._movementSound.Play();
				this._isMoveSoundPlaying = true;
			}
			this._movementSound.SetPosition(this.MainObject.GameEntity.GlobalPosition);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B9CEC File Offset: 0x000B7EEC
		private void StopMovementSound()
		{
			if (this._isMoveSoundPlaying)
			{
				this._movementSound.Stop();
				this._isMoveSoundPlaying = false;
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B9D08 File Offset: 0x000B7F08
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			this.CurrentSpeed = 0f;
			this._lastSynchronizedDistance = 0f;
			this._advancementError = 0f;
			this._pathTracker.Reset();
			this.SetTargetFrame();
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B9D42 File Offset: 0x000B7F42
		public float GetTotalDistanceTraveledForPathTracker()
		{
			return this._pathTracker.TotalDistanceTraveled;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B9D4F File Offset: 0x000B7F4F
		private MatrixFrame FindGroundFrameForWheels(ref MatrixFrame frame)
		{
			return SiegeWeaponMovementComponent.FindGroundFrameForWheelsStatic(ref frame, this.AxleLength, this._wheelDiameter, this.MainObject.GameEntity, this._wheels, this.MainObject.Scene);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000B9D7F File Offset: 0x000B7F7F
		public void SetTotalDistanceTraveledForPathTracker(float distanceTraveled)
		{
			this._pathTracker.TotalDistanceTraveled = distanceTraveled;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000B9D8D File Offset: 0x000B7F8D
		public void SetTargetFrameForPathTracker()
		{
			this.SetTargetFrame();
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000B9D98 File Offset: 0x000B7F98
		public static MatrixFrame FindGroundFrameForWheelsStatic(ref MatrixFrame frame, float axleLength, float wheelDiameter, GameEntity gameEntity, List<GameEntity> wheels, Scene scene)
		{
			Vec3.StackArray8Vec3 stackArray8Vec = default(Vec3.StackArray8Vec3);
			bool visibilityExcludeParents = gameEntity.GetVisibilityExcludeParents();
			if (visibilityExcludeParents)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
			int num = 0;
			using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
			{
				foreach (GameEntity gameEntity2 in wheels)
				{
					Vec3 v = frame.TransformToParent(gameEntity2.GetFrame().origin);
					Vec3 vec = v + frame.rotation.s * axleLength + (wheelDiameter * 0.5f + 0.5f) * frame.rotation.u;
					Vec3 vec2 = v - frame.rotation.s * axleLength + (wheelDiameter * 0.5f + 0.5f) * frame.rotation.u;
					vec.z = scene.GetGroundHeightAtPositionMT(vec, BodyFlags.CommonCollisionExcludeFlags);
					vec2.z = scene.GetGroundHeightAtPositionMT(vec2, BodyFlags.CommonCollisionExcludeFlags);
					stackArray8Vec[num++] = vec;
					stackArray8Vec[num++] = vec2;
				}
			}
			if (visibilityExcludeParents)
			{
				gameEntity.SetVisibilityExcludeParents(true);
			}
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			Vec3 vec3 = default(Vec3);
			for (int i = 0; i < num; i++)
			{
				vec3 += stackArray8Vec[i];
			}
			vec3 /= (float)num;
			for (int j = 0; j < num; j++)
			{
				Vec3 vec4 = stackArray8Vec[j] - vec3;
				num2 += vec4.x * vec4.x;
				num3 += vec4.x * vec4.y;
				num4 += vec4.y * vec4.y;
				num5 += vec4.x * vec4.z;
				num6 += vec4.y * vec4.z;
			}
			float num7 = num2 * num4 - num3 * num3;
			float x = (num6 * num3 - num5 * num4) / num7;
			float y = (num3 * num5 - num2 * num6) / num7;
			MatrixFrame matrixFrame;
			matrixFrame.origin = vec3;
			matrixFrame.rotation.u = new Vec3(x, y, 1f, -1f);
			matrixFrame.rotation.u.Normalize();
			matrixFrame.rotation.f = frame.rotation.f;
			matrixFrame.rotation.f = matrixFrame.rotation.f - Vec3.DotProduct(matrixFrame.rotation.f, matrixFrame.rotation.u) * matrixFrame.rotation.u;
			matrixFrame.rotation.f.Normalize();
			matrixFrame.rotation.s = Vec3.CrossProduct(matrixFrame.rotation.f, matrixFrame.rotation.u);
			matrixFrame.rotation.s.Normalize();
			return matrixFrame;
		}

		// Token: 0x040012F7 RID: 4855
		public const string GhostObjectTag = "ghost_object";

		// Token: 0x040012F8 RID: 4856
		private static readonly ActionIndexCache act_strike_bent_over = ActionIndexCache.Create("act_strike_bent_over");

		// Token: 0x040012F9 RID: 4857
		private static readonly ActionIndexCache act_usage_siege_machine_push = ActionIndexCache.Create("act_usage_siege_machine_push");

		// Token: 0x040012FA RID: 4858
		private const string WheelTag = "wheel";

		// Token: 0x040012FB RID: 4859
		public const string MoveStandingPointTag = "move";

		// Token: 0x040012FC RID: 4860
		public float AxleLength = 2.45f;

		// Token: 0x040012FD RID: 4861
		public int NavMeshIdToDisableOnDestination = -1;

		// Token: 0x040012FE RID: 4862
		private float _ghostObjectPos;

		// Token: 0x040012FF RID: 4863
		private List<GameEntity> _wheels;

		// Token: 0x04001300 RID: 4864
		private List<StandingPoint> _standingPoints;

		// Token: 0x04001301 RID: 4865
		private MatrixFrame[] _standingPointLocalIKFrames;

		// Token: 0x04001302 RID: 4866
		private SoundEvent _movementSound;

		// Token: 0x04001303 RID: 4867
		private float _wheelCircumference;

		// Token: 0x04001304 RID: 4868
		private bool _isMoveSoundPlaying;

		// Token: 0x04001305 RID: 4869
		private float _wheelDiameter;

		// Token: 0x04001306 RID: 4870
		private Path _path;

		// Token: 0x04001307 RID: 4871
		private PathTracker _pathTracker;

		// Token: 0x04001308 RID: 4872
		private PathTracker _ghostEntityPathTracker;

		// Token: 0x04001309 RID: 4873
		private float _advancementError;

		// Token: 0x0400130A RID: 4874
		private float _lastSynchronizedDistance;
	}
}
