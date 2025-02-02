using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200033E RID: 830
	public class SiegeLadder : SiegeWeapon, IPrimarySiegeWeapon, IOrderableWithInteractionArea, IOrderable, ISpawnable
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x000B3492 File Offset: 0x000B1692
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x000B349A File Offset: 0x000B169A
		public GameEntity InitialWaitPosition { get; private set; }

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000B34A3 File Offset: 0x000B16A3
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x000B34AB File Offset: 0x000B16AB
		public int OnWallNavMeshId { get; private set; }

		// Token: 0x06002D0D RID: 11533 RVA: 0x000B34B4 File Offset: 0x000B16B4
		public override SiegeEngineType GetSiegeEngineType()
		{
			return DefaultSiegeEngineTypes.Ladder;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B34BC File Offset: 0x000B16BC
		protected internal override void OnInit()
		{
			base.OnInit();
			this._tickOccasionallyTimer = new Timer(Mission.Current.CurrentTime, 0.2f + MBRandom.RandomFloat * 0.05f, true);
			this._aiBarriers = base.Scene.FindEntitiesWithTag(this.BarrierTagToRemove).ToList<GameEntity>();
			if (this.IndestructibleMerlonsTag != string.Empty)
			{
				foreach (GameEntity gameEntity in base.Scene.FindEntitiesWithTag(this.IndestructibleMerlonsTag))
				{
					DestructableComponent firstScriptOfType = gameEntity.GetFirstScriptOfType<DestructableComponent>();
					firstScriptOfType.SetDisabled(false);
					firstScriptOfType.CanBeDestroyedInitially = false;
				}
			}
			this._attackerStandingPoints = base.GameEntity.CollectObjectsWithTag(this.AttackerTag);
			this._pushingWithForkStandingPoint = base.GameEntity.CollectObjectsWithTag(this.DefenderTag).FirstOrDefault<StandingPointWithWeaponRequirement>();
			this._pushingWithForkStandingPoint.AddComponent(new DropExtraWeaponOnStopUsageComponent());
			this._forkPickUpStandingPoint = base.GameEntity.CollectObjectsWithTag(this.AmmoPickUpTag).FirstOrDefault<StandingPointWithWeaponRequirement>();
			StandingPointWithWeaponRequirement forkPickUpStandingPoint = this._forkPickUpStandingPoint;
			if (forkPickUpStandingPoint != null)
			{
				forkPickUpStandingPoint.SetUsingBattleSide(BattleSideEnum.Defender);
			}
			this._ladderParticleObject = base.GameEntity.CollectObjectsWithTag("particles").FirstOrDefault<SynchedMissionObject>();
			this._forkEntity = base.GameEntity.CollectObjectsWithTag("push_fork").FirstOrDefault<SynchedMissionObject>();
			if (base.StandingPoints != null)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					if (!standingPoint.GameEntity.HasTag(this.AmmoPickUpTag))
					{
						standingPoint.AddComponent(new ResetAnimationOnStopUsageComponent(standingPoint.GameEntity.HasTag(this.DefenderTag) ? SiegeLadder.act_usage_ladder_push_back_stopped : ActionIndexCache.act_none));
						standingPoint.IsDeactivated = true;
					}
				}
			}
			this._forkItem = Game.Current.ObjectManager.GetObject<ItemObject>(this.PushForkItemID);
			this._pushingWithForkStandingPoint.InitRequiredWeapon(this._forkItem);
			this._forkPickUpStandingPoint.InitGivenWeapon(this._forkItem);
			GameEntity gameEntity2 = base.GameEntity.CollectChildrenEntitiesWithTag(this.upStateEntityTag)[0];
			List<SynchedMissionObject> list = base.GameEntity.CollectObjectsWithTag(this.downStateEntityTag);
			this._ladderObject = list[0];
			this._ladderSkeleton = this._ladderObject.GameEntity.Skeleton;
			list = base.GameEntity.CollectObjectsWithTag(this.BodyTag);
			this._ladderBodyObject = list[0];
			list = base.GameEntity.CollectObjectsWithTag(this.CollisionBodyTag);
			this._ladderCollisionBodyObject = list[0];
			this._ladderDownFrame = this._ladderObject.GameEntity.GetFrame();
			this._turningAngle = this._downStateRotationRadian - this._ladderDownFrame.rotation.GetEulerAngles().x;
			this._ladderDownFrame.rotation.RotateAboutSide(this._turningAngle);
			this._ladderObject.GameEntity.SetFrame(ref this._ladderDownFrame);
			MatrixFrame frame = gameEntity2.GetFrame();
			frame.rotation = Mat3.Identity;
			frame.rotation.RotateAboutSide(this._upStateRotationRadian);
			this._ladderUpFrame = frame;
			this._ladderUpFrame = this._ladderObject.GameEntity.Parent.GetFrame().TransformToLocal(this._ladderUpFrame);
			this._ladderInitialGlobalFrame = this._ladderObject.GameEntity.GetGlobalFrame();
			this._attackerStandingPointLocalIKFrames = new MatrixFrame[this._attackerStandingPoints.Count];
			MatrixFrame frame2 = this._ladderObject.GameEntity.Parent.GetFrame();
			MatrixFrame matrixFrame = frame2;
			matrixFrame.rotation.RotateAboutForward(this._turningAngle);
			this.State = this.initialState;
			for (int i = 0; i < this._attackerStandingPoints.Count; i++)
			{
				MatrixFrame m = this._attackerStandingPoints[i].GameEntity.GetFrame();
				m = matrixFrame.TransformToParent(m);
				m = frame2.TransformToLocal(m);
				this._attackerStandingPoints[i].GameEntity.SetFrame(ref m);
				this._attackerStandingPointLocalIKFrames[i] = this._attackerStandingPoints[i].GameEntity.GetGlobalFrame().TransformToLocal(this._ladderInitialGlobalFrame);
				this._attackerStandingPoints[i].AddComponent(new ClearHandInverseKinematicsOnStopUsageComponent());
			}
			this.CalculateNavigationAndPhysics();
			this.InitialWaitPosition = base.GameEntity.CollectChildrenEntitiesWithTag(this.InitialWaitPositionTag).FirstOrDefault<GameEntity>();
			foreach (GameEntity gameEntity3 in base.Scene.FindEntitiesWithTag(this._targetWallSegmentTag))
			{
				WallSegment firstScriptOfType2 = gameEntity3.GetFirstScriptOfType<WallSegment>();
				if (firstScriptOfType2 != null)
				{
					this._targetWallSegment = firstScriptOfType2;
					this._targetWallSegment.AttackerSiegeWeapon = this;
					break;
				}
			}
			string sideTag = this._sideTag;
			if (!(sideTag == "left"))
			{
				if (!(sideTag == "middle"))
				{
					if (!(sideTag == "right"))
					{
						this.WeaponSide = FormationAI.BehaviorSide.Middle;
					}
					else
					{
						this.WeaponSide = FormationAI.BehaviorSide.Right;
					}
				}
				else
				{
					this.WeaponSide = FormationAI.BehaviorSide.Middle;
				}
			}
			else
			{
				this.WeaponSide = FormationAI.BehaviorSide.Left;
			}
			base.SetForcedUse(false);
			LadderQueueManager[] array = base.GameEntity.GetScriptComponents<LadderQueueManager>().ToArray<LadderQueueManager>();
			MatrixFrame matrixFrame2 = base.GameEntity.GetGlobalFrame().TransformToLocal(this._ladderObject.GameEntity.GetGlobalFrame());
			int num = 0;
			int num2 = 1;
			for (int j = base.GameEntity.Name.Length - 1; j >= 0; j--)
			{
				if (char.IsDigit(base.GameEntity.Name[j]))
				{
					num += (int)(base.GameEntity.Name[j] - '0') * num2;
					num2 *= 10;
				}
				else if (num > 0)
				{
					break;
				}
			}
			if (array.Length != 0)
			{
				this._queueManagerForAttackers = array[0];
				this._queueManagerForAttackers.Initialize(this.OnWallNavMeshId, matrixFrame2, -matrixFrame2.rotation.f, BattleSideEnum.Attacker, 3, 2.3561945f, 2f, 0.8f, 6f, 5f, false, 0.8f, (float)num, 5f, false, -2, -2, num, 2);
			}
			if (array.Length > 1 && this._pushingWithForkStandingPoint != null)
			{
				this._queueManagerForDefenders = array[1];
				MatrixFrame matrixFrame3 = this._pushingWithForkStandingPoint.GameEntity.GetGlobalFrame();
				matrixFrame3.rotation.RotateAboutSide(1.5707964f);
				matrixFrame3.origin -= matrixFrame3.rotation.u;
				matrixFrame3 = base.GameEntity.GetGlobalFrame().TransformToLocal(matrixFrame3);
				this._queueManagerForDefenders.Initialize(this.OnWallNavMeshId, matrixFrame3, matrixFrame2.rotation.f, BattleSideEnum.Defender, 1, 2.8274333f, 0.5f, 0.8f, 6f, 5f, true, 0.8f, float.MaxValue, 5f, false, -2, -2, 0, 0);
			}
			base.GameEntity.Scene.MarkFacesWithIdAsLadder(this.OnWallNavMeshId, true);
			this.EnemyRangeToStopUsing = 0f;
			this._idleAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.IdleAnimation);
			this._raiseAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.RaiseAnimation);
			this._raiseAnimationWithoutRootBoneIndex = MBAnimation.GetAnimationIndexWithName(this.RaiseAnimationWithoutRootBone);
			this._pushBackAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.PushBackAnimation);
			this._pushBackAnimationWithoutRootBoneIndex = MBAnimation.GetAnimationIndexWithName(this.PushBackAnimationWithoutRootBone);
			this._trembleWallHeavyAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.TrembleWallHeavyAnimation);
			this._trembleWallLightAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.TrembleWallLightAnimation);
			this._trembleGroundAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.TrembleGroundAnimation);
			this.SetUpStateVisibility(false);
			base.SetScriptComponentToTick(this.GetTickRequirement());
			bool flag = false;
			foreach (GameEntity gameEntity4 in this._ladderObject.GameEntity.GetEntityAndChildren())
			{
				PhysicsShape bodyShape = gameEntity4.GetBodyShape();
				if (bodyShape != null)
				{
					PhysicsShape.AddPreloadQueueWithName(bodyShape.GetName(), gameEntity4.GetGlobalScale());
					flag = true;
				}
			}
			if (flag)
			{
				PhysicsShape.ProcessPreloadQueue();
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B3D38 File Offset: 0x000B1F38
		private float GetCurrentLadderAngularSpeed(int animationIndex)
		{
			float animationParameterAtChannel = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
			MatrixFrame boneEntitialFrameWithIndex = this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0);
			if (animationParameterAtChannel <= 0.01f)
			{
				return 0f;
			}
			this._ladderSkeleton.SetAnimationParameterAtChannel(0, animationParameterAtChannel - 0.01f);
			this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, this._ladderObject.GameEntity.GetGlobalFrame(), false);
			MatrixFrame boneEntitialFrameWithIndex2 = this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0);
			Vec2 vec = new Vec2(boneEntitialFrameWithIndex.rotation.f.y, boneEntitialFrameWithIndex.rotation.f.z);
			Vec2 vec2 = new Vec2(boneEntitialFrameWithIndex2.rotation.f.y, boneEntitialFrameWithIndex2.rotation.f.z);
			return (vec.RotationInRadians - vec2.RotationInRadians) / (MBAnimation.GetAnimationDuration(animationIndex) * 0.01f);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B3E18 File Offset: 0x000B2018
		private void OnLadderStateChange()
		{
			GameEntity gameEntity = this._ladderObject.GameEntity;
			if (this.State != SiegeLadder.LadderState.OnWall)
			{
				this.SetVisibilityOfAIBarriers(true);
			}
			switch (this.State)
			{
			case SiegeLadder.LadderState.OnLand:
				this._animationState = SiegeLadder.LadderAnimationState.Static;
				return;
			case SiegeLadder.LadderState.FallToLand:
				if (this._ladderSkeleton.GetAnimationIndexAtChannel(0) != this._trembleGroundAnimationIndex)
				{
					gameEntity.SetFrame(ref this._ladderDownFrame);
					this._ladderSkeleton.SetAnimationAtChannel(this._trembleGroundAnimationIndex, 0, 1f, -1f, 0f);
					this._animationState = SiegeLadder.LadderAnimationState.Static;
				}
				if (!GameNetwork.IsClientOrReplay)
				{
					this.State = SiegeLadder.LadderState.OnLand;
					return;
				}
				break;
			case SiegeLadder.LadderState.BeingRaised:
			case SiegeLadder.LadderState.BeingPushedBack:
				break;
			case SiegeLadder.LadderState.BeingRaisedStartFromGround:
			{
				this._animationState = SiegeLadder.LadderAnimationState.Animated;
				MatrixFrame frame = gameEntity.GetFrame();
				frame.rotation.RotateAboutSide(-1.5707964f);
				gameEntity.SetFrame(ref frame);
				this._ladderSkeleton.SetAnimationAtChannel(this._raiseAnimationIndex, 0, 1f, -1f, 0f);
				this._ladderSkeleton.ForceUpdateBoneFrames();
				this._lastDotProductOfAnimationAndTargetRotation = -1000f;
				if (!GameNetwork.IsClientOrReplay)
				{
					this._currentActionAgentCount = 1;
					this.State = SiegeLadder.LadderState.BeingRaised;
					return;
				}
				break;
			}
			case SiegeLadder.LadderState.BeingRaisedStopped:
			{
				this._animationState = SiegeLadder.LadderAnimationState.PhysicallyDynamic;
				MatrixFrame matrixFrame = gameEntity.GetGlobalFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
				matrixFrame.rotation.RotateAboutForward(1.5707964f);
				this._fallAngularSpeed = this.GetCurrentLadderAngularSpeed(this._raiseAnimationIndex);
				float animationParameterAtChannel = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
				gameEntity.SetGlobalFrame(matrixFrame);
				this._ladderSkeleton.SetAnimationAtChannel(this._raiseAnimationWithoutRootBoneIndex, 0, 1f, -1f, 0f);
				this._ladderSkeleton.SetAnimationParameterAtChannel(0, animationParameterAtChannel);
				this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
				this._ladderSkeleton.SetAnimationAtChannel(this._idleAnimationIndex, 0, 1f, -1f, 0f);
				this._ladderObject.SetLocalPositionSmoothStep(ref this._ladderDownFrame.origin);
				if (!GameNetwork.IsClientOrReplay)
				{
					this.State = SiegeLadder.LadderState.BeingPushedBack;
					return;
				}
				break;
			}
			case SiegeLadder.LadderState.OnWall:
				this._animationState = SiegeLadder.LadderAnimationState.Static;
				this.SetVisibilityOfAIBarriers(false);
				return;
			case SiegeLadder.LadderState.FallToWall:
				if (GameNetwork.IsClientOrReplay)
				{
					int animationIndexAtChannel = this._ladderSkeleton.GetAnimationIndexAtChannel(0);
					if (animationIndexAtChannel != this._trembleWallHeavyAnimationIndex && animationIndexAtChannel != this._trembleWallLightAnimationIndex)
					{
						gameEntity.SetFrame(ref this._ladderUpFrame);
						this._ladderSkeleton.SetAnimationAtChannel((this._fallAngularSpeed < -0.5f) ? this._trembleWallHeavyAnimationIndex : this._trembleWallLightAnimationIndex, 0, 1f, -1f, 0f);
						this._animationState = SiegeLadder.LadderAnimationState.Static;
						return;
					}
				}
				else
				{
					this.State = SiegeLadder.LadderState.OnWall;
					SynchedMissionObject ladderParticleObject = this._ladderParticleObject;
					if (ladderParticleObject == null)
					{
						return;
					}
					ladderParticleObject.BurstParticlesSynched(false);
					return;
				}
				break;
			case SiegeLadder.LadderState.BeingPushedBackStartFromWall:
				this._animationState = SiegeLadder.LadderAnimationState.Animated;
				this._ladderSkeleton.SetAnimationAtChannel(this._pushBackAnimationIndex, 0, 1f, -1f, 0f);
				this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
				this._lastDotProductOfAnimationAndTargetRotation = -1000f;
				if (!GameNetwork.IsClientOrReplay)
				{
					this._currentActionAgentCount = 1;
					this.State = SiegeLadder.LadderState.BeingPushedBack;
					return;
				}
				break;
			case SiegeLadder.LadderState.BeingPushedBackStopped:
			{
				this._animationState = SiegeLadder.LadderAnimationState.PhysicallyDynamic;
				MatrixFrame matrixFrame2 = gameEntity.GetGlobalFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
				matrixFrame2.rotation.RotateAboutForward(1.5707964f);
				this._fallAngularSpeed = this.GetCurrentLadderAngularSpeed(this._pushBackAnimationIndex);
				float animationParameterAtChannel2 = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
				gameEntity.SetGlobalFrame(matrixFrame2);
				this._ladderSkeleton.SetAnimationAtChannel(this._pushBackAnimationWithoutRootBoneIndex, 0, 1f, -1f, 0f);
				this._ladderSkeleton.SetAnimationParameterAtChannel(0, animationParameterAtChannel2);
				this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
				this._ladderSkeleton.SetAnimationAtChannel(this._idleAnimationIndex, 0, 1f, -1f, 0f);
				this._ladderObject.SetLocalPositionSmoothStep(ref this._ladderUpFrame.origin);
				if (!GameNetwork.IsClientOrReplay)
				{
					this.State = SiegeLadder.LadderState.BeingRaised;
				}
				this._ladderSkeleton.ForceUpdateBoneFrames();
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000B4230 File Offset: 0x000B2430
		private void SetVisibilityOfAIBarriers(bool visibility)
		{
			foreach (GameEntity gameEntity in this._aiBarriers)
			{
				gameEntity.SetVisibilityExcludeParents(visibility);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000B4284 File Offset: 0x000B2484
		public int OverTheWallNavMeshID
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000B4288 File Offset: 0x000B2488
		public override OrderType GetOrder(BattleSideEnum side)
		{
			if (side != BattleSideEnum.Attacker)
			{
				return OrderType.Move;
			}
			return base.GetOrder(side);
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000B4297 File Offset: 0x000B2497
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x000B42A0 File Offset: 0x000B24A0
		public SiegeLadder.LadderState State
		{
			get
			{
				return this._state;
			}
			set
			{
				if (this._state != value)
				{
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SetSiegeLadderState(base.Id, value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					}
					this._state = value;
					this.OnLadderStateChange();
					this.CalculateNavigationAndPhysics();
				}
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000B42F0 File Offset: 0x000B24F0
		private void CalculateNavigationAndPhysics()
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				bool flag = this.State != SiegeLadder.LadderState.OnWall;
				if (this._isNavigationMeshDisabled != flag)
				{
					this._isNavigationMeshDisabled = flag;
					this.SetAbilityOfFaces(!this._isNavigationMeshDisabled);
				}
			}
			bool flag2 = (this.State == SiegeLadder.LadderState.BeingRaisedStartFromGround || this.State == SiegeLadder.LadderState.BeingRaised) && this._animationState != SiegeLadder.LadderAnimationState.PhysicallyDynamic;
			bool flag3 = true;
			if (this._isLadderPhysicsDisabled != flag2)
			{
				this._isLadderPhysicsDisabled = flag2;
				this._ladderBodyObject.GameEntity.SetPhysicsState(!this._isLadderPhysicsDisabled, true);
			}
			if (!flag2)
			{
				MatrixFrame matrixFrame = this._ladderObject.GameEntity.GetGlobalFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
				matrixFrame.rotation.RotateAboutForward(1.5707964f);
				this._ladderBodyObject.GameEntity.SetGlobalFrame(matrixFrame);
				flag3 = (this.State != SiegeLadder.LadderState.BeingPushedBack || matrixFrame.rotation.f.z < 0f);
				if (!flag3)
				{
					float y = MathF.Min(2.01f - matrixFrame.rotation.u.z * 2f, 1f);
					matrixFrame.rotation.ApplyScaleLocal(new Vec3(1f, y, 1f, -1f));
					this._ladderCollisionBodyObject.GameEntity.SetGlobalFrame(matrixFrame);
				}
			}
			if (this._isLadderCollisionPhysicsDisabled != flag3)
			{
				this._isLadderCollisionPhysicsDisabled = flag3;
				this._ladderCollisionBodyObject.GameEntity.SetPhysicsState(!this._isLadderCollisionPhysicsDisabled, true);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x000B447A File Offset: 0x000B267A
		public MissionObject TargetCastlePosition
		{
			get
			{
				return this._targetWallSegment;
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000B4482 File Offset: 0x000B2682
		public bool HasCompletedAction()
		{
			return this.State == SiegeLadder.LadderState.OnWall;
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000B448D File Offset: 0x000B268D
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x000B4495 File Offset: 0x000B2695
		public FormationAI.BehaviorSide WeaponSide { get; private set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000B449E File Offset: 0x000B269E
		public float SiegeWeaponPriority
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000B44A8 File Offset: 0x000B26A8
		private ActionIndexCache GetActionCodeToUseForStandingPoint(StandingPoint standingPoint)
		{
			GameEntity gameEntity = standingPoint.GameEntity;
			if (!gameEntity.HasTag(this.RightStandingPointTag))
			{
				if (!gameEntity.HasTag(this.FrontStandingPointTag))
				{
					return SiegeLadder.act_usage_ladder_lift_from_left_2_start;
				}
				return SiegeLadder.act_usage_ladder_lift_from_left_1_start;
			}
			else
			{
				if (!gameEntity.HasTag(this.FrontStandingPointTag))
				{
					return SiegeLadder.act_usage_ladder_lift_from_right_2_start;
				}
				return SiegeLadder.act_usage_ladder_lift_from_right_1_start;
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000B4500 File Offset: 0x000B2700
		public override bool IsDisabledForBattleSide(BattleSideEnum sideEnum)
		{
			if (sideEnum == BattleSideEnum.Attacker)
			{
				return this.State == SiegeLadder.LadderState.FallToLand || this.State == SiegeLadder.LadderState.FallToWall || this.State == SiegeLadder.LadderState.OnWall || (this.State == SiegeLadder.LadderState.BeingPushedBack && this._animationState != SiegeLadder.LadderAnimationState.PhysicallyDynamic) || this.State == SiegeLadder.LadderState.BeingPushedBackStartFromWall || this.State == SiegeLadder.LadderState.BeingPushedBackStopped;
			}
			return this.State == SiegeLadder.LadderState.OnLand || this.State == SiegeLadder.LadderState.FallToLand || this.State == SiegeLadder.LadderState.BeingRaised || this.State == SiegeLadder.LadderState.BeingRaisedStartFromGround || this.State == SiegeLadder.LadderState.FallToWall;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000B4584 File Offset: 0x000B2784
		protected override float GetDetachmentWeightAux(BattleSideEnum side)
		{
			if (side == BattleSideEnum.Attacker)
			{
				return base.GetDetachmentWeightAux(side);
			}
			if (this.IsDisabledForBattleSideAI(side))
			{
				return float.MinValue;
			}
			this._usableStandingPoints.Clear();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < base.StandingPoints.Count; i++)
			{
				StandingPoint standingPoint = base.StandingPoints[i];
				if (standingPoint.IsUsableBySide(side) && (standingPoint != this._forkPickUpStandingPoint || this._pushingWithForkStandingPoint.IsUsableBySide(side)))
				{
					if (!standingPoint.HasAIMovingTo)
					{
						if (!flag2)
						{
							this._usableStandingPoints.Clear();
						}
						flag2 = true;
					}
					else if (flag2 || standingPoint.MovingAgent.Formation.Team.Side != side)
					{
						goto IL_A4;
					}
					flag = true;
					this._usableStandingPoints.Add(new ValueTuple<int, StandingPoint>(i, standingPoint));
				}
				IL_A4:;
			}
			this._areUsableStandingPointsVacant = flag2;
			if (!flag)
			{
				return float.MinValue;
			}
			if (flag2)
			{
				return 1f;
			}
			if (!this._isDetachmentRecentlyEvaluated)
			{
				return 0.1f;
			}
			return 0.01f;
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000B4676 File Offset: 0x000B2876
		public bool HoldLadders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000B4679 File Offset: 0x000B2879
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel;
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000B4685 File Offset: 0x000B2885
		public bool SendLadders
		{
			get
			{
				return this.State > SiegeLadder.LadderState.OnLand;
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000B4690 File Offset: 0x000B2890
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._tickOccasionallyTimer.Check(Mission.Current.CurrentTime))
			{
				this.TickRare();
			}
			if (!GameNetwork.IsClientOrReplay && this._forkReappearingTimer != null && this._forkReappearingTimer.Check(Mission.Current.CurrentTime))
			{
				this._forkPickUpStandingPoint.SetIsDeactivatedSynched(false);
				this._forkEntity.SetVisibleSynched(true, false);
			}
			int num = 0;
			int num2 = 0;
			GameEntity gameEntity = this._ladderObject.GameEntity;
			if (!GameNetwork.IsClientOrReplay)
			{
				if (this._queueManagerForAttackers != null)
				{
					if (this._queueManagerForAttackers.IsDeactivated)
					{
						if (this.State == SiegeLadder.LadderState.OnWall)
						{
							this._queueManagerForAttackers.Activate();
						}
					}
					else if (this.State == SiegeLadder.LadderState.OnLand)
					{
						this._queueManagerForAttackers.Deactivate();
					}
				}
				if (this._queueManagerForDefenders != null && this._queueManagerForDefenders.IsDeactivated != (this.State != SiegeLadder.LadderState.OnWall))
				{
					if (this.State != SiegeLadder.LadderState.OnWall)
					{
						this._queueManagerForDefenders.DeactivateImmediate();
					}
					else
					{
						this._queueManagerForDefenders.Activate();
					}
				}
				int animationIndexAtChannel = this._ladderSkeleton.GetAnimationIndexAtChannel(0);
				bool flag = false;
				if (animationIndexAtChannel >= 0)
				{
					flag = (animationIndexAtChannel == this._trembleGroundAnimationIndex || animationIndexAtChannel == this._trembleWallHeavyAnimationIndex || animationIndexAtChannel == this._trembleWallLightAnimationIndex);
					if (flag)
					{
						flag = (this._ladderSkeleton.GetAnimationParameterAtChannel(0) < 1f);
					}
				}
				num += ((this._pushingWithForkStandingPoint.HasUser && !this._pushingWithForkStandingPoint.UserAgent.IsInBeingStruckAction) ? 1 : 0);
				foreach (StandingPoint standingPoint in this._attackerStandingPoints)
				{
					if (standingPoint.HasUser && !standingPoint.UserAgent.IsInBeingStruckAction)
					{
						num2++;
					}
				}
				foreach (StandingPoint standingPoint2 in base.StandingPoints)
				{
					GameEntity gameEntity2 = standingPoint2.GameEntity;
					if (!gameEntity2.HasTag(this.AmmoPickUpTag))
					{
						bool flag2 = false;
						if ((!standingPoint2.HasUser || standingPoint2.UserAgent.IsInBeingStruckAction) && this.State == SiegeLadder.LadderState.BeingRaised && gameEntity2.HasTag(this.AttackerTag))
						{
							float animationParameterAtChannel = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
							float animationDuration = MBAnimation.GetAnimationDuration(this._ladderSkeleton.GetAnimationIndexAtChannel(0));
							ActionIndexCache actionCodeToUseForStandingPoint = this.GetActionCodeToUseForStandingPoint(standingPoint2);
							int animationIndexOfAction = MBActionSet.GetAnimationIndexOfAction(MBGlobals.GetActionSetWithSuffix(Game.Current.DefaultMonster, false, "_warrior"), actionCodeToUseForStandingPoint);
							flag2 = (animationParameterAtChannel * animationDuration / MathF.Max(MBAnimation.GetAnimationDuration(animationIndexOfAction), 0.01f) > 0.98f);
						}
						standingPoint2.SetIsDeactivatedSynched(flag2 || this.State == SiegeLadder.LadderState.BeingPushedBackStopped || (gameEntity2.HasTag(this.AttackerTag) && (this.State == SiegeLadder.LadderState.OnWall || this.State == SiegeLadder.LadderState.FallToWall || (this.State == SiegeLadder.LadderState.BeingPushedBack && this._animationState != SiegeLadder.LadderAnimationState.PhysicallyDynamic) || this.State == SiegeLadder.LadderState.BeingPushedBackStartFromWall)) || (gameEntity2.HasTag(this.DefenderTag) && (this.State == SiegeLadder.LadderState.OnLand || this._animationState == SiegeLadder.LadderAnimationState.PhysicallyDynamic || this.State == SiegeLadder.LadderState.BeingRaisedStopped || flag || this.State == SiegeLadder.LadderState.FallToLand || this.State == SiegeLadder.LadderState.BeingRaised || this.State == SiegeLadder.LadderState.BeingRaisedStartFromGround || !this.CanLadderBePushed())));
					}
				}
				if (this._forkPickUpStandingPoint.HasUser)
				{
					Agent userAgent = this._forkPickUpStandingPoint.UserAgent;
					ActionIndexValueCache currentActionValue = userAgent.GetCurrentActionValue(1);
					if (!(currentActionValue == SiegeLadder.act_usage_ladder_pick_up_fork_begin))
					{
						if (currentActionValue == SiegeLadder.act_usage_ladder_pick_up_fork_end)
						{
							MissionWeapon missionWeapon = new MissionWeapon(this._forkItem, null, null);
							userAgent.EquipWeaponToExtraSlotAndWield(ref missionWeapon);
							this._forkPickUpStandingPoint.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
							this._forkPickUpStandingPoint.SetIsDeactivatedSynched(true);
							this._forkEntity.SetVisibleSynched(false, false);
							this._forkReappearingTimer = new Timer(Mission.Current.CurrentTime, this._forkReappearingDelay, true);
							if (userAgent.IsAIControlled)
							{
								StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(userAgent.Team.Side, userAgent, null, null);
								if (suitableStandingPointFor != null)
								{
									((IDetachment)this).AddAgent(userAgent, -1);
									if (userAgent.Formation != null)
									{
										userAgent.Formation.DetachUnit(userAgent, ((IDetachment)this).IsLoose);
										userAgent.Detachment = this;
										userAgent.DetachmentWeight = this.GetWeightOfStandingPoint(suitableStandingPointFor);
									}
								}
							}
						}
						else if (!this._forkPickUpStandingPoint.UserAgent.SetActionChannel(1, SiegeLadder.act_usage_ladder_pick_up_fork_begin, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true))
						{
							this._forkPickUpStandingPoint.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						}
					}
				}
				else if (this._forkPickUpStandingPoint.HasAIMovingTo)
				{
					Agent movingAgent = this._forkPickUpStandingPoint.MovingAgent;
					if (movingAgent.Team != null && !this._pushingWithForkStandingPoint.IsUsableBySide(movingAgent.Team.Side))
					{
						movingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
			}
			switch (this.State)
			{
			case SiegeLadder.LadderState.OnLand:
			case SiegeLadder.LadderState.FallToLand:
				if (!GameNetwork.IsClientOrReplay && num2 > 0)
				{
					this.State = SiegeLadder.LadderState.BeingRaisedStartFromGround;
				}
				break;
			case SiegeLadder.LadderState.BeingRaised:
			case SiegeLadder.LadderState.BeingRaisedStartFromGround:
			case SiegeLadder.LadderState.BeingPushedBackStopped:
				if (this._animationState == SiegeLadder.LadderAnimationState.Animated)
				{
					float animationParameterAtChannel2 = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
					float animationDuration2 = MBAnimation.GetAnimationDuration(this._ladderSkeleton.GetAnimationIndexAtChannel(0));
					foreach (StandingPoint standingPoint3 in this._attackerStandingPoints)
					{
						if (standingPoint3.HasUser)
						{
							MBActionSet actionSet = standingPoint3.UserAgent.ActionSet;
							ActionIndexCache actionCodeToUseForStandingPoint2 = this.GetActionCodeToUseForStandingPoint(standingPoint3);
							ActionIndexValueCache currentActionValue2 = standingPoint3.UserAgent.GetCurrentActionValue(1);
							if (currentActionValue2 == actionCodeToUseForStandingPoint2)
							{
								int animationIndexOfAction2 = MBActionSet.GetAnimationIndexOfAction(actionSet, actionCodeToUseForStandingPoint2);
								float progress = MBMath.ClampFloat(animationParameterAtChannel2 * animationDuration2 / MathF.Max(MBAnimation.GetAnimationDuration(animationIndexOfAction2), 0.01f), 0f, 1f);
								standingPoint3.UserAgent.SetCurrentActionProgress(1, progress);
							}
							else if (MBAnimation.GetActionType(currentActionValue2) == Agent.ActionCodeType.LadderRaiseEnd)
							{
								float animationDuration3 = MBAnimation.GetAnimationDuration(MBActionSet.GetAnimationIndexOfAction(actionSet, currentActionValue2));
								float num3 = animationDuration2 - animationDuration3;
								float progress2 = MBMath.ClampFloat((animationParameterAtChannel2 * animationDuration2 - num3) / MathF.Max(animationDuration3, 0.01f), 0f, 1f);
								standingPoint3.UserAgent.SetCurrentActionProgress(1, progress2);
							}
						}
					}
					bool flag3 = false;
					if (!GameNetwork.IsClientOrReplay)
					{
						if (num2 > 0)
						{
							if (num2 != this._currentActionAgentCount)
							{
								this._currentActionAgentCount = num2;
								float animationSpeed = MathF.Sqrt((float)this._currentActionAgentCount);
								float animationParameterAtChannel3 = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
								this._ladderObject.SetAnimationAtChannelSynched(this._raiseAnimationIndex, 0, animationSpeed);
								if (animationParameterAtChannel3 > 0f)
								{
									this._ladderObject.SetAnimationChannelParameterSynched(0, animationParameterAtChannel3);
								}
							}
							using (List<StandingPoint>.Enumerator enumerator = this._attackerStandingPoints.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									StandingPoint standingPoint4 = enumerator.Current;
									if (standingPoint4.HasUser)
									{
										ActionIndexCache actionCodeToUseForStandingPoint3 = this.GetActionCodeToUseForStandingPoint(standingPoint4);
										Agent userAgent2 = standingPoint4.UserAgent;
										ActionIndexValueCache currentActionValue3 = userAgent2.GetCurrentActionValue(1);
										if (currentActionValue3 != actionCodeToUseForStandingPoint3 && MBAnimation.GetActionType(currentActionValue3) != Agent.ActionCodeType.LadderRaiseEnd)
										{
											if (!userAgent2.SetActionChannel(1, actionCodeToUseForStandingPoint3, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && !userAgent2.IsAIControlled)
											{
												userAgent2.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
											}
										}
										else if (MBAnimation.GetActionType(currentActionValue3) == Agent.ActionCodeType.LadderRaiseEnd)
										{
											standingPoint4.UserAgent.ClearTargetFrame();
										}
									}
								}
								goto IL_803;
							}
						}
						this.State = SiegeLadder.LadderState.BeingRaisedStopped;
						flag3 = true;
					}
					IL_803:
					if (!flag3)
					{
						MatrixFrame matrixFrame = gameEntity.GetGlobalFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
						matrixFrame.rotation.RotateAboutForward(1.5707964f);
						if ((animationParameterAtChannel2 > 0.9f && animationParameterAtChannel2 != 1f) || matrixFrame.rotation.f.z <= 0.2f)
						{
							this._animationState = SiegeLadder.LadderAnimationState.PhysicallyDynamic;
							this._fallAngularSpeed = this.GetCurrentLadderAngularSpeed(this._raiseAnimationIndex);
							gameEntity.SetGlobalFrame(matrixFrame);
							this._ladderSkeleton.SetAnimationAtChannel(this._raiseAnimationWithoutRootBoneIndex, 0, 1f, -1f, 0f);
							this._ladderSkeleton.SetAnimationParameterAtChannel(0, animationParameterAtChannel2);
							this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
							this._ladderSkeleton.SetAnimationAtChannel(this._idleAnimationIndex, 0, 1f, -1f, 0f);
							this._ladderObject.SetLocalPositionSmoothStep(ref this._ladderUpFrame.origin);
						}
					}
				}
				else if (this._animationState == SiegeLadder.LadderAnimationState.PhysicallyDynamic)
				{
					MatrixFrame frame = gameEntity.GetFrame();
					frame.rotation.RotateAboutSide(this._fallAngularSpeed * dt);
					gameEntity.SetFrame(ref frame);
					MatrixFrame matrixFrame2 = gameEntity.GetFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
					float num4 = Vec3.DotProduct(matrixFrame2.rotation.f, this._ladderUpFrame.rotation.f);
					if (this._fallAngularSpeed < 0f && num4 > 0.95f && num4 < this._lastDotProductOfAnimationAndTargetRotation)
					{
						gameEntity.SetFrame(ref this._ladderUpFrame);
						this._ladderSkeleton.SetAnimationParameterAtChannel(0, 0f);
						this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
						this._animationState = SiegeLadder.LadderAnimationState.Static;
						this._ladderSkeleton.SetAnimationAtChannel((this._fallAngularSpeed < -0.5f) ? this._trembleWallHeavyAnimationIndex : this._trembleWallLightAnimationIndex, 0, 1f, -1f, 0f);
						if (!GameNetwork.IsClientOrReplay)
						{
							this.State = SiegeLadder.LadderState.FallToWall;
						}
					}
					this._fallAngularSpeed -= dt * 2f * MathF.Max(0.3f, 1f - matrixFrame2.rotation.u.z);
					this._lastDotProductOfAnimationAndTargetRotation = num4;
				}
				break;
			case SiegeLadder.LadderState.BeingRaisedStopped:
			case SiegeLadder.LadderState.BeingPushedBack:
			case SiegeLadder.LadderState.BeingPushedBackStartFromWall:
				if (this._animationState == SiegeLadder.LadderAnimationState.Animated)
				{
					float animationParameterAtChannel4 = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
					if (this._pushingWithForkStandingPoint.HasUser)
					{
						ActionIndexCache action = SiegeLadder.act_usage_ladder_push_back;
						if (this._pushingWithForkStandingPoint.UserAgent.GetCurrentActionValue(1) == action)
						{
							this._pushingWithForkStandingPoint.UserAgent.SetCurrentActionProgress(1, animationParameterAtChannel4);
						}
					}
					bool flag4 = false;
					if (!GameNetwork.IsClientOrReplay)
					{
						if (num > 0)
						{
							if (num != this._currentActionAgentCount)
							{
								this._currentActionAgentCount = num;
								float animationSpeed2 = MathF.Sqrt((float)this._currentActionAgentCount);
								float animationParameterAtChannel5 = this._ladderSkeleton.GetAnimationParameterAtChannel(0);
								this._ladderObject.SetAnimationAtChannelSynched(this.PushBackAnimation, 0, animationSpeed2);
								if (animationParameterAtChannel5 > 0f)
								{
									this._ladderObject.SetAnimationChannelParameterSynched(0, animationParameterAtChannel5);
								}
							}
							if (this._pushingWithForkStandingPoint.HasUser)
							{
								ActionIndexCache actionIndexCache = SiegeLadder.act_usage_ladder_push_back;
								Agent userAgent3 = this._pushingWithForkStandingPoint.UserAgent;
								if (userAgent3.GetCurrentActionValue(1) != actionIndexCache && animationParameterAtChannel4 < 1f && !userAgent3.SetActionChannel(1, actionIndexCache, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && !userAgent3.IsAIControlled)
								{
									userAgent3.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
								}
							}
						}
						else
						{
							this.State = SiegeLadder.LadderState.BeingPushedBackStopped;
							flag4 = true;
						}
					}
					if (!flag4)
					{
						MatrixFrame matrixFrame3 = gameEntity.GetGlobalFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
						matrixFrame3.rotation.RotateAboutForward(1.5707964f);
						if (animationParameterAtChannel4 > 0.9999f || matrixFrame3.rotation.f.z >= 0f)
						{
							this._animationState = SiegeLadder.LadderAnimationState.PhysicallyDynamic;
							this._fallAngularSpeed = this.GetCurrentLadderAngularSpeed(this._pushBackAnimationIndex);
							gameEntity.SetGlobalFrame(matrixFrame3);
							this._ladderSkeleton.SetAnimationAtChannel(this._pushBackAnimationWithoutRootBoneIndex, 0, 1f, -1f, 0f);
							this._ladderSkeleton.SetAnimationParameterAtChannel(0, animationParameterAtChannel4);
							this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
							this._ladderSkeleton.SetAnimationAtChannel(this._idleAnimationIndex, 0, 1f, -1f, 0f);
							this._ladderObject.SetLocalPositionSmoothStep(ref this._ladderDownFrame.origin);
						}
					}
				}
				else if (this._animationState == SiegeLadder.LadderAnimationState.PhysicallyDynamic)
				{
					MatrixFrame frame2 = gameEntity.GetFrame();
					frame2.rotation.RotateAboutSide(this._fallAngularSpeed * dt);
					gameEntity.SetFrame(ref frame2);
					MatrixFrame matrixFrame4 = gameEntity.GetFrame().TransformToParent(this._ladderSkeleton.GetBoneEntitialFrameWithIndex(0));
					matrixFrame4.rotation.RotateAboutForward(1.5707964f);
					float num5 = Vec3.DotProduct(matrixFrame4.rotation.f, this._ladderDownFrame.rotation.f);
					if (this._fallAngularSpeed > 0f && num5 > 0.95f && num5 < this._lastDotProductOfAnimationAndTargetRotation)
					{
						this._animationState = SiegeLadder.LadderAnimationState.Static;
						gameEntity.SetFrame(ref this._ladderDownFrame);
						this._ladderSkeleton.SetAnimationParameterAtChannel(0, 0f);
						this._ladderSkeleton.TickAnimationsAndForceUpdate(0.0001f, gameEntity.GetGlobalFrame(), false);
						this._ladderSkeleton.SetAnimationAtChannel(this._trembleGroundAnimationIndex, 0, 1f, -1f, 0f);
						this._animationState = SiegeLadder.LadderAnimationState.Static;
						if (!GameNetwork.IsClientOrReplay)
						{
							this.State = SiegeLadder.LadderState.FallToLand;
						}
					}
					this._fallAngularSpeed += dt * 2f * MathF.Max(0.3f, 1f - matrixFrame4.rotation.u.z);
					this._lastDotProductOfAnimationAndTargetRotation = num5;
				}
				break;
			case SiegeLadder.LadderState.OnWall:
			case SiegeLadder.LadderState.FallToWall:
				if (num > 0 && !GameNetwork.IsClientOrReplay)
				{
					this.State = SiegeLadder.LadderState.BeingPushedBackStartFromWall;
				}
				break;
			default:
				Debug.FailedAssert("Invalid ladder action state.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SiegeLadder.cs", "OnTick", 1259);
				break;
			}
			this.CalculateNavigationAndPhysics();
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000B5524 File Offset: 0x000B3724
		protected internal override void OnTickParallel(float dt)
		{
			base.OnTickParallel(dt);
			for (int i = 0; i < this._attackerStandingPoints.Count; i++)
			{
				if (this._attackerStandingPoints[i].HasUser)
				{
					if (!this._attackerStandingPoints[i].UserAgent.IsInBeingStruckAction)
					{
						if (this._attackerStandingPoints[i].UserAgent.GetCurrentAction(1) != this.GetActionCodeToUseForStandingPoint(this._attackerStandingPoints[i]))
						{
							MatrixFrame matrixFrame = this._attackerStandingPointLocalIKFrames[i];
							matrixFrame.rotation = Mat3.Lerp(matrixFrame.rotation, this._ladderInitialGlobalFrame.TransformToLocal(this._attackerStandingPoints[i].UserAgent.Frame).rotation, MathF.Clamp(MathF.Lerp(0f, 1f - this._turningAngle * 1.2f, MathF.Pow(this._attackerStandingPoints[i].UserAgent.GetCurrentActionProgress(1), 6f), 1E-05f), 0f, 1f));
							this._attackerStandingPoints[i].UserAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(matrixFrame, this._ladderInitialGlobalFrame, 0f);
						}
						else
						{
							this._attackerStandingPoints[i].UserAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(this._attackerStandingPointLocalIKFrames[i], this._ladderInitialGlobalFrame, 0f);
						}
					}
					else
					{
						this._attackerStandingPoints[i].UserAgent.ClearHandInverseKinematics();
					}
				}
			}
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000B56B8 File Offset: 0x000B38B8
		private void TickRare()
		{
			if (!GameNetwork.IsReplay)
			{
				float num = 20f + (base.ForcedUse ? 3f : 0f);
				num *= num;
				GameEntity gameEntity = base.GameEntity;
				Mission.TeamCollection teams = Mission.Current.Teams;
				int count = teams.Count;
				Vec3 globalPosition = gameEntity.GlobalPosition;
				for (int i = 0; i < count; i++)
				{
					Team team = teams[i];
					if (team.Side == BattleSideEnum.Attacker)
					{
						base.SetForcedUse(false);
						foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation.CountOfUnits > 0 && formation.QuerySystem.MedianPosition.AsVec2.DistanceSquared(globalPosition.AsVec2) < num && formation.QuerySystem.MedianPosition.GetNavMeshZ() - globalPosition.z < 4f)
							{
								base.SetForcedUse(true);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000B57E0 File Offset: 0x000B39E0
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new SiegeLadderAI(this);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000B57E8 File Offset: 0x000B39E8
		public void SetUpStateVisibility(bool isVisible)
		{
			base.GameEntity.CollectChildrenEntitiesWithTag(this.upStateEntityTag)[0].SetVisibilityExcludeParents(isVisible);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000B5807 File Offset: 0x000B3A07
		private void FlushQueueManager()
		{
			LadderQueueManager queueManagerForAttackers = this._queueManagerForAttackers;
			if (queueManagerForAttackers == null)
			{
				return;
			}
			queueManagerForAttackers.FlushQueueManager();
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000B581C File Offset: 0x000B3A1C
		private void FlushNeighborQueueManagers()
		{
			foreach (SiegeLadder siegeLadder in (from sl in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeLadder>()
			where sl.WeaponSide == this.WeaponSide
			select sl).ToList<SiegeLadder>())
			{
				if (siegeLadder != this)
				{
					siegeLadder.FlushQueueManager();
				}
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000B5894 File Offset: 0x000B3A94
		private bool CanLadderBePushed()
		{
			float num = 0f;
			GameEntity gameEntity = this._ladderObject.GameEntity;
			Vec3 vec;
			Vec3 vec2;
			gameEntity.GetPhysicsMinMax(true, out vec, out vec2, false);
			float searchRadius = (vec2 - vec).AsVec2.Length * 0.5f;
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, gameEntity.GlobalPosition.AsVec2, searchRadius, false);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				if (lastFoundAgent.GetSteppedMachine() == this)
				{
					float num2 = (lastFoundAgent.Position.z - vec.z) / (vec2.z - vec.z) * 100f;
					if (num2 > this.LadderPushTresholdForOneAgent)
					{
						return false;
					}
					num += num2;
				}
				AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
			}
			return num <= this.LadderPushTreshold;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000B5970 File Offset: 0x000B3B70
		private void InformNeighborQueueManagers(LadderQueueManager ladderQueueManager)
		{
			foreach (SiegeLadder siegeLadder in (from sl in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeLadder>()
			where sl.WeaponSide == this.WeaponSide && sl._queueManagerForAttackers != null
			select sl).ToList<SiegeLadder>())
			{
				if (siegeLadder != this && siegeLadder._queueManagerForAttackers != null)
				{
					siegeLadder._queueManagerForAttackers.AssignNeighborQueueManager(ladderQueueManager);
					LadderQueueManager queueManagerForAttackers = this._queueManagerForAttackers;
					if (queueManagerForAttackers != null)
					{
						queueManagerForAttackers.AssignNeighborQueueManager(siegeLadder._queueManagerForAttackers);
					}
				}
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000B5A0C File Offset: 0x000B3C0C
		public override void SetAbilityOfFaces(bool enabled)
		{
			base.SetAbilityOfFaces(enabled);
			base.GameEntity.Scene.SetAbilityOfFacesWithId(this.OnWallNavMeshId, enabled);
			if (Mission.Current != null)
			{
				if (enabled)
				{
					this.FlushNeighborQueueManagers();
					this.InformNeighborQueueManagers(this._queueManagerForAttackers);
					return;
				}
				this.InformNeighborQueueManagers(null);
				LadderQueueManager queueManagerForAttackers = this._queueManagerForAttackers;
				if (queueManagerForAttackers == null)
				{
					return;
				}
				queueManagerForAttackers.AssignNeighborQueueManager(null);
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000B5A6C File Offset: 0x000B3C6C
		protected internal override void OnMissionReset()
		{
			this._ladderSkeleton.SetAnimationAtChannel(-1, 0, 1f, -1f, 0f);
			if (this.initialState == SiegeLadder.LadderState.OnLand)
			{
				if (!GameNetwork.IsClientOrReplay)
				{
					this.State = SiegeLadder.LadderState.OnLand;
				}
				this._ladderObject.GameEntity.SetFrame(ref this._ladderDownFrame);
				return;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				this.State = SiegeLadder.LadderState.OnWall;
			}
			this._ladderObject.GameEntity.SetFrame(ref this._ladderUpFrame);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000B5AE6 File Offset: 0x000B3CE6
		public override string GetDescriptionText(GameEntity gameEntity)
		{
			if (!gameEntity.HasTag(this.AmmoPickUpTag))
			{
				return new TextObject("{=G0AWk1rX}Ladder", null).ToString();
			}
			return new TextObject("{=F9AQxCax}Fork", null).ToString();
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000B5B18 File Offset: 0x000B3D18
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject;
			if (usableGameObject.GameEntity.HasTag(this.AmmoPickUpTag))
			{
				textObject = new TextObject("{=bNYm3K6b}{KEY} Pick Up", null);
			}
			else
			{
				textObject = (usableGameObject.GameEntity.HasTag(this.AttackerTag) ? new TextObject("{=kbNcm68J}{KEY} Lift", null) : new TextObject("{=MdQJxiGz}{KEY} Push", null));
			}
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000B5B90 File Offset: 0x000B3D90
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteBoolToPacket(this.initialState == SiegeLadder.LadderState.OnLand);
			GameNetworkMessage.WriteIntToPacket((int)this.State, CompressionMission.SiegeLadderStateCompressionInfo);
			GameNetworkMessage.WriteIntToPacket((int)this._animationState, CompressionMission.SiegeLadderAnimationStateCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this._fallAngularSpeed, CompressionMission.SiegeMachineComponentAngularSpeedCompressionInfo);
			GameNetworkMessage.WriteMatrixFrameToPacket(this._ladderObject.GameEntity.GetGlobalFrame());
			int animationIndexAtChannel = this._ladderSkeleton.GetAnimationIndexAtChannel(0);
			GameNetworkMessage.WriteBoolToPacket(animationIndexAtChannel >= 0);
			if (animationIndexAtChannel >= 0)
			{
				GameNetworkMessage.WriteIntToPacket(animationIndexAtChannel, CompressionBasic.AnimationIndexCompressionInfo);
				GameNetworkMessage.WriteFloatToPacket(this._ladderSkeleton.GetAnimationParameterAtChannel(0), CompressionBasic.AnimationProgressCompressionInfo);
			}
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000B5C34 File Offset: 0x000B3E34
		bool IOrderableWithInteractionArea.IsPointInsideInteractionArea(Vec3 point)
		{
			GameEntity gameEntity = base.GameEntity.CollectChildrenEntitiesWithTag("ui_interaction").FirstOrDefault<GameEntity>();
			return !(gameEntity == null) && gameEntity.GlobalPosition.AsVec2.DistanceSquared(point.AsVec2) < 25f;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000B5C88 File Offset: 0x000B3E88
		public override TargetFlags GetTargetFlags()
		{
			TargetFlags targetFlags = TargetFlags.None;
			targetFlags |= TargetFlags.IsFlammable;
			targetFlags |= TargetFlags.IsSiegeEngine;
			targetFlags |= TargetFlags.IsAttacker;
			if (this.HasCompletedAction() || this.IsDeactivated)
			{
				targetFlags |= TargetFlags.NotAThreat;
			}
			return targetFlags;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000B5CBA File Offset: 0x000B3EBA
		public override float GetTargetValue(List<Vec3> weaponPos)
		{
			return 10f * base.GetUserMultiplierOfWeapon() * this.GetDistanceMultiplierOfWeapon(weaponPos[0]);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000B5CD6 File Offset: 0x000B3ED6
		protected override float GetDistanceMultiplierOfWeapon(Vec3 weaponPos)
		{
			if (this.GetMinimumDistanceBetweenPositions(weaponPos) >= 10f)
			{
				return 0.9f;
			}
			return 1f;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000B5CF4 File Offset: 0x000B3EF4
		protected override StandingPoint GetSuitableStandingPointFor(BattleSideEnum side, Agent agent = null, List<Agent> agents = null, List<ValueTuple<Agent, float>> agentValuePairs = null)
		{
			if (side == BattleSideEnum.Attacker)
			{
				return this._attackerStandingPoints.FirstOrDefault((StandingPoint sp) => !sp.IsDeactivated && (sp.IsInstantUse || (!sp.HasUser && !sp.HasAIMovingTo)));
			}
			if (this._pushingWithForkStandingPoint.IsDeactivated || (!this._pushingWithForkStandingPoint.IsInstantUse && (this._pushingWithForkStandingPoint.HasUser || this._pushingWithForkStandingPoint.HasAIMovingTo)))
			{
				return null;
			}
			return this._pushingWithForkStandingPoint;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000B5D6C File Offset: 0x000B3F6C
		public void SetSpawnedFromSpawner()
		{
			this._spawnedFromSpawner = true;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000B5D75 File Offset: 0x000B3F75
		public void AssignParametersFromSpawner(string sideTag, string targetWallSegment, int onWallNavMeshId, float downStateRotationRadian, float upperStateRotationRadian, string barrierTagToRemove, string indestructibleMerlonsTag)
		{
			this._sideTag = sideTag;
			this._targetWallSegmentTag = targetWallSegment;
			this.OnWallNavMeshId = onWallNavMeshId;
			this._downStateRotationRadian = downStateRotationRadian;
			this._upStateRotationRadian = upperStateRotationRadian;
			this.BarrierTagToRemove = barrierTagToRemove;
			this.IndestructibleMerlonsTag = indestructibleMerlonsTag;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000B5DAC File Offset: 0x000B3FAC
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord);
			SiegeLadder.SiegeLadderRecord siegeLadderRecord = (SiegeLadder.SiegeLadderRecord)synchedMissionObjectReadableRecord.Item2;
			this.initialState = (siegeLadderRecord.IsStateLand ? SiegeLadder.LadderState.OnLand : SiegeLadder.LadderState.OnWall);
			this._state = (SiegeLadder.LadderState)siegeLadderRecord.State;
			this._animationState = (SiegeLadder.LadderAnimationState)siegeLadderRecord.AnimationState;
			this._fallAngularSpeed = siegeLadderRecord.FallAngularSpeed;
			this._lastDotProductOfAnimationAndTargetRotation = -1000f;
			MatrixFrame ladderFrame = siegeLadderRecord.LadderFrame;
			ladderFrame.rotation.Orthonormalize();
			GameEntity gameEntity = this._ladderObject.GameEntity;
			ladderFrame = siegeLadderRecord.LadderFrame;
			gameEntity.SetGlobalFrame(ladderFrame);
			if (siegeLadderRecord.LadderAnimationIndex >= 0)
			{
				this._ladderSkeleton.SetAnimationAtChannel(siegeLadderRecord.LadderAnimationIndex, 0, 1f, -1f, 0f);
				this._ladderSkeleton.SetAnimationParameterAtChannel(0, MBMath.ClampFloat(siegeLadderRecord.LadderAnimationProgress, 0f, 1f));
				this._ladderSkeleton.ForceUpdateBoneFrames();
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000B5E98 File Offset: 0x000B4098
		public bool GetNavmeshFaceIds(out List<int> navmeshFaceIds)
		{
			navmeshFaceIds = new List<int>
			{
				this.OnWallNavMeshId
			};
			return true;
		}

		// Token: 0x0400125F RID: 4703
		private static readonly ActionIndexCache act_usage_ladder_lift_from_left_1_start = ActionIndexCache.Create("act_usage_ladder_lift_from_left_1_start");

		// Token: 0x04001260 RID: 4704
		private static readonly ActionIndexCache act_usage_ladder_lift_from_left_2_start = ActionIndexCache.Create("act_usage_ladder_lift_from_left_2_start");

		// Token: 0x04001261 RID: 4705
		public const float ClimbingLimitRadian = -0.20135832f;

		// Token: 0x04001262 RID: 4706
		private static readonly ActionIndexCache act_usage_ladder_lift_from_right_1_start = ActionIndexCache.Create("act_usage_ladder_lift_from_right_1_start");

		// Token: 0x04001263 RID: 4707
		public const float ClimbingLimitDegree = -11.536982f;

		// Token: 0x04001264 RID: 4708
		private static readonly ActionIndexCache act_usage_ladder_lift_from_right_2_start = ActionIndexCache.Create("act_usage_ladder_lift_from_right_2_start");

		// Token: 0x04001265 RID: 4709
		public const float AutomaticUseActivationRange = 20f;

		// Token: 0x04001266 RID: 4710
		private static readonly ActionIndexCache act_usage_ladder_pick_up_fork_begin = ActionIndexCache.Create("act_usage_ladder_pick_up_fork_begin");

		// Token: 0x04001267 RID: 4711
		private static readonly ActionIndexCache act_usage_ladder_pick_up_fork_end = ActionIndexCache.Create("act_usage_ladder_pick_up_fork_end");

		// Token: 0x04001268 RID: 4712
		private static readonly ActionIndexCache act_usage_ladder_push_back = ActionIndexCache.Create("act_usage_ladder_push_back");

		// Token: 0x04001269 RID: 4713
		private static readonly ActionIndexCache act_usage_ladder_push_back_stopped = ActionIndexCache.Create("act_usage_ladder_push_back_stopped");

		// Token: 0x0400126A RID: 4714
		public string AttackerTag = "attacker";

		// Token: 0x0400126B RID: 4715
		public string DefenderTag = "defender";

		// Token: 0x0400126C RID: 4716
		public string downStateEntityTag = "ladderDown";

		// Token: 0x0400126D RID: 4717
		public string IdleAnimation = "siege_ladder_idle";

		// Token: 0x0400126E RID: 4718
		public int _idleAnimationIndex = -1;

		// Token: 0x0400126F RID: 4719
		public string RaiseAnimation = "siege_ladder_rise";

		// Token: 0x04001270 RID: 4720
		public string RaiseAnimationWithoutRootBone = "siege_ladder_rise_wo_rootbone";

		// Token: 0x04001271 RID: 4721
		public int _raiseAnimationWithoutRootBoneIndex = -1;

		// Token: 0x04001272 RID: 4722
		public string PushBackAnimation = "siege_ladder_push_back";

		// Token: 0x04001273 RID: 4723
		public int _pushBackAnimationIndex = -1;

		// Token: 0x04001274 RID: 4724
		public string PushBackAnimationWithoutRootBone = "siege_ladder_push_back_wo_rootbone";

		// Token: 0x04001275 RID: 4725
		public int _pushBackAnimationWithoutRootBoneIndex = -1;

		// Token: 0x04001276 RID: 4726
		public string TrembleWallHeavyAnimation = "siege_ladder_stop_wall_heavy";

		// Token: 0x04001277 RID: 4727
		public string TrembleWallLightAnimation = "siege_ladder_stop_wall_light";

		// Token: 0x04001278 RID: 4728
		public string TrembleGroundAnimation = "siege_ladder_stop_ground_heavy";

		// Token: 0x04001279 RID: 4729
		public string RightStandingPointTag = "right";

		// Token: 0x0400127A RID: 4730
		public string LeftStandingPointTag = "left";

		// Token: 0x0400127B RID: 4731
		public string FrontStandingPointTag = "front";

		// Token: 0x0400127C RID: 4732
		public string PushForkItemID = "push_fork";

		// Token: 0x0400127D RID: 4733
		public string upStateEntityTag = "ladderUp";

		// Token: 0x0400127E RID: 4734
		public string BodyTag = "ladder_body";

		// Token: 0x0400127F RID: 4735
		public string CollisionBodyTag = "ladder_collision_body";

		// Token: 0x04001280 RID: 4736
		public string InitialWaitPositionTag = "initialwaitposition";

		// Token: 0x04001281 RID: 4737
		private string _targetWallSegmentTag = "";

		// Token: 0x04001282 RID: 4738
		public float LadderPushTreshold = 170f;

		// Token: 0x04001283 RID: 4739
		public float LadderPushTresholdForOneAgent = 55f;

		// Token: 0x04001284 RID: 4740
		private WallSegment _targetWallSegment;

		// Token: 0x04001285 RID: 4741
		private string _sideTag;

		// Token: 0x04001286 RID: 4742
		private int _trembleWallLightAnimationIndex = -1;

		// Token: 0x04001287 RID: 4743
		public string BarrierTagToRemove = "barrier";

		// Token: 0x04001288 RID: 4744
		private int _trembleGroundAnimationIndex = -1;

		// Token: 0x04001289 RID: 4745
		public SiegeLadder.LadderState initialState;

		// Token: 0x0400128A RID: 4746
		private int _trembleWallHeavyAnimationIndex = -1;

		// Token: 0x0400128B RID: 4747
		public string IndestructibleMerlonsTag = string.Empty;

		// Token: 0x0400128C RID: 4748
		private int _raiseAnimationIndex = -1;

		// Token: 0x0400128D RID: 4749
		private bool _isNavigationMeshDisabled;

		// Token: 0x0400128E RID: 4750
		private bool _isLadderPhysicsDisabled;

		// Token: 0x0400128F RID: 4751
		private bool _isLadderCollisionPhysicsDisabled;

		// Token: 0x04001290 RID: 4752
		private Timer _tickOccasionallyTimer;

		// Token: 0x04001291 RID: 4753
		private float _upStateRotationRadian;

		// Token: 0x04001292 RID: 4754
		private float _downStateRotationRadian;

		// Token: 0x04001293 RID: 4755
		private float _fallAngularSpeed;

		// Token: 0x04001294 RID: 4756
		private MatrixFrame _ladderDownFrame;

		// Token: 0x04001295 RID: 4757
		private MatrixFrame _ladderUpFrame;

		// Token: 0x04001296 RID: 4758
		private SiegeLadder.LadderAnimationState _animationState;

		// Token: 0x04001297 RID: 4759
		private int _currentActionAgentCount;

		// Token: 0x04001298 RID: 4760
		private SiegeLadder.LadderState _state;

		// Token: 0x04001299 RID: 4761
		private List<GameEntity> _aiBarriers;

		// Token: 0x0400129A RID: 4762
		private List<StandingPoint> _attackerStandingPoints;

		// Token: 0x0400129B RID: 4763
		private StandingPointWithWeaponRequirement _pushingWithForkStandingPoint;

		// Token: 0x0400129C RID: 4764
		private StandingPointWithWeaponRequirement _forkPickUpStandingPoint;

		// Token: 0x0400129D RID: 4765
		private ItemObject _forkItem;

		// Token: 0x0400129E RID: 4766
		private MatrixFrame[] _attackerStandingPointLocalIKFrames;

		// Token: 0x0400129F RID: 4767
		private MatrixFrame _ladderInitialGlobalFrame;

		// Token: 0x040012A0 RID: 4768
		private SynchedMissionObject _ladderParticleObject;

		// Token: 0x040012A1 RID: 4769
		private SynchedMissionObject _ladderBodyObject;

		// Token: 0x040012A2 RID: 4770
		private SynchedMissionObject _ladderCollisionBodyObject;

		// Token: 0x040012A3 RID: 4771
		private SynchedMissionObject _ladderObject;

		// Token: 0x040012A4 RID: 4772
		private Skeleton _ladderSkeleton;

		// Token: 0x040012A5 RID: 4773
		private float _lastDotProductOfAnimationAndTargetRotation;

		// Token: 0x040012A6 RID: 4774
		private float _turningAngle;

		// Token: 0x040012A7 RID: 4775
		private LadderQueueManager _queueManagerForAttackers;

		// Token: 0x040012A8 RID: 4776
		private LadderQueueManager _queueManagerForDefenders;

		// Token: 0x040012AA RID: 4778
		private Timer _forkReappearingTimer;

		// Token: 0x040012AB RID: 4779
		private float _forkReappearingDelay = 10f;

		// Token: 0x040012AD RID: 4781
		private SynchedMissionObject _forkEntity;

		// Token: 0x020005F7 RID: 1527
		[DefineSynchedMissionObjectType(typeof(SiegeLadder))]
		public struct SiegeLadderRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x170009E4 RID: 2532
			// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x000E978B File Offset: 0x000E798B
			// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x000E9793 File Offset: 0x000E7993
			public bool IsStateLand { get; private set; }

			// Token: 0x170009E5 RID: 2533
			// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000E979C File Offset: 0x000E799C
			// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000E97A4 File Offset: 0x000E79A4
			public int State { get; private set; }

			// Token: 0x170009E6 RID: 2534
			// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000E97AD File Offset: 0x000E79AD
			// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x000E97B5 File Offset: 0x000E79B5
			public int AnimationState { get; private set; }

			// Token: 0x170009E7 RID: 2535
			// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000E97BE File Offset: 0x000E79BE
			// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x000E97C6 File Offset: 0x000E79C6
			public float FallAngularSpeed { get; private set; }

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000E97CF File Offset: 0x000E79CF
			// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000E97D7 File Offset: 0x000E79D7
			public MatrixFrame LadderFrame { get; private set; }

			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x06003BDB RID: 15323 RVA: 0x000E97E0 File Offset: 0x000E79E0
			// (set) Token: 0x06003BDC RID: 15324 RVA: 0x000E97E8 File Offset: 0x000E79E8
			public bool HasAnimation { get; private set; }

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x06003BDD RID: 15325 RVA: 0x000E97F1 File Offset: 0x000E79F1
			// (set) Token: 0x06003BDE RID: 15326 RVA: 0x000E97F9 File Offset: 0x000E79F9
			public int LadderAnimationIndex { get; private set; }

			// Token: 0x170009EB RID: 2539
			// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000E9802 File Offset: 0x000E7A02
			// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x000E980A File Offset: 0x000E7A0A
			public float LadderAnimationProgress { get; private set; }

			// Token: 0x06003BE1 RID: 15329 RVA: 0x000E9814 File Offset: 0x000E7A14
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.IsStateLand = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.State = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeLadderStateCompressionInfo, ref bufferReadValid);
				this.AnimationState = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeLadderAnimationStateCompressionInfo, ref bufferReadValid);
				this.FallAngularSpeed = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.SiegeMachineComponentAngularSpeedCompressionInfo, ref bufferReadValid);
				this.LadderFrame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref bufferReadValid);
				this.HasAnimation = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.LadderAnimationIndex = -1;
				this.LadderAnimationProgress = 0f;
				if (this.HasAnimation)
				{
					this.LadderAnimationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationIndexCompressionInfo, ref bufferReadValid);
					this.LadderAnimationProgress = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationProgressCompressionInfo, ref bufferReadValid);
				}
				return bufferReadValid;
			}
		}

		// Token: 0x020005F8 RID: 1528
		public enum LadderState
		{
			// Token: 0x04001F23 RID: 7971
			OnLand,
			// Token: 0x04001F24 RID: 7972
			FallToLand,
			// Token: 0x04001F25 RID: 7973
			BeingRaised,
			// Token: 0x04001F26 RID: 7974
			BeingRaisedStartFromGround,
			// Token: 0x04001F27 RID: 7975
			BeingRaisedStopped,
			// Token: 0x04001F28 RID: 7976
			OnWall,
			// Token: 0x04001F29 RID: 7977
			FallToWall,
			// Token: 0x04001F2A RID: 7978
			BeingPushedBack,
			// Token: 0x04001F2B RID: 7979
			BeingPushedBackStartFromWall,
			// Token: 0x04001F2C RID: 7980
			BeingPushedBackStopped,
			// Token: 0x04001F2D RID: 7981
			NumberOfStates
		}

		// Token: 0x020005F9 RID: 1529
		public enum LadderAnimationState
		{
			// Token: 0x04001F2F RID: 7983
			Static,
			// Token: 0x04001F30 RID: 7984
			Animated,
			// Token: 0x04001F31 RID: 7985
			PhysicallyDynamic,
			// Token: 0x04001F32 RID: 7986
			NumberOfStates
		}
	}
}
