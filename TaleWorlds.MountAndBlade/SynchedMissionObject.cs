using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000351 RID: 849
	public class SynchedMissionObject : MissionObject
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000BE225 File Offset: 0x000BC425
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x000BE22D File Offset: 0x000BC42D
		public uint Color { get; private set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000BE236 File Offset: 0x000BC436
		// (set) Token: 0x06002E75 RID: 11893 RVA: 0x000BE23E File Offset: 0x000BC43E
		public uint Color2 { get; private set; }

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000BE247 File Offset: 0x000BC447
		public bool SynchronizeCompleted
		{
			get
			{
				return this._synchState == SynchedMissionObject.SynchState.SynchronizeCompleted;
			}
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000BE252 File Offset: 0x000BC452
		protected internal override void OnInit()
		{
			base.OnInit();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000BE266 File Offset: 0x000BC466
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (!this.SynchronizeCompleted)
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000BE280 File Offset: 0x000BC480
		protected internal override void OnTick(float dt)
		{
			if (!this.SynchronizeCompleted)
			{
				MatrixFrame frame = base.GameEntity.GetFrame();
				if ((this._synchState == SynchedMissionObject.SynchState.SynchronizePosition && this._lastSynchedFrame.origin.NearlyEquals(frame.origin, 1E-05f)) || this._lastSynchedFrame.NearlyEquals(frame, 1E-05f))
				{
					this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeCompleted);
					return;
				}
				MatrixFrame matrixFrame;
				matrixFrame.origin = ((this._synchState == SynchedMissionObject.SynchState.SynchronizeFrameOverTime) ? MBMath.Lerp(this._firstFrame.origin, this._lastSynchedFrame.origin, this._timer / this._duration, 0.2f * dt) : MBMath.Lerp(frame.origin, this._lastSynchedFrame.origin, 8f * dt, 0.2f * dt));
				if (this._synchState == SynchedMissionObject.SynchState.SynchronizeFrame || this._synchState == SynchedMissionObject.SynchState.SynchronizeFrameOverTime)
				{
					matrixFrame.rotation.s = MBMath.Lerp(frame.rotation.s, this._lastSynchedFrame.rotation.s, 8f * dt, 0.2f * dt);
					matrixFrame.rotation.f = MBMath.Lerp(frame.rotation.f, this._lastSynchedFrame.rotation.f, 8f * dt, 0.2f * dt);
					matrixFrame.rotation.u = MBMath.Lerp(frame.rotation.u, this._lastSynchedFrame.rotation.u, 8f * dt, 0.2f * dt);
					if (matrixFrame.origin != this._lastSynchedFrame.origin || matrixFrame.rotation.s != this._lastSynchedFrame.rotation.s || matrixFrame.rotation.f != this._lastSynchedFrame.rotation.f || matrixFrame.rotation.u != this._lastSynchedFrame.rotation.u)
					{
						matrixFrame.rotation.Orthonormalize();
						if (this._lastSynchedFrame.rotation.HasScale())
						{
							matrixFrame.rotation.ApplyScaleLocal(this._lastSynchedFrame.rotation.GetScaleVector());
						}
					}
					base.GameEntity.SetFrame(ref matrixFrame);
				}
				else
				{
					base.GameEntity.SetLocalPosition(matrixFrame.origin);
				}
				this._timer = MathF.Min(this._timer + dt, this._duration);
			}
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000BE4FE File Offset: 0x000BC6FE
		private void SetSynchState(SynchedMissionObject.SynchState newState)
		{
			if (newState != this._synchState)
			{
				this._synchState = newState;
				base.SetScriptComponentToTick(this.GetTickRequirement());
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000BE51C File Offset: 0x000BC71C
		public void SetLocalPositionSmoothStep(ref Vec3 targetPosition)
		{
			this._lastSynchedFrame.origin = targetPosition;
			this.SetSynchState(SynchedMissionObject.SynchState.SynchronizePosition);
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000BE538 File Offset: 0x000BC738
		public virtual void SetVisibleSynched(bool value, bool forceChildrenVisible = false)
		{
			bool flag = base.GameEntity.IsVisibleIncludeParents() != value;
			List<GameEntity> list = null;
			if (!flag && forceChildrenVisible)
			{
				list = new List<GameEntity>();
				base.GameEntity.GetChildrenRecursive(ref list);
				using (List<GameEntity>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.GetPhysicsState() != value)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (base.GameEntity != null && flag)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectVisibility(base.Id, value));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				base.GameEntity.SetVisibilityExcludeParents(value);
				if (forceChildrenVisible)
				{
					if (list == null)
					{
						list = new List<GameEntity>();
						base.GameEntity.GetChildrenRecursive(ref list);
					}
					foreach (GameEntity gameEntity in list)
					{
						gameEntity.SetVisibilityExcludeParents(value);
					}
				}
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000BE650 File Offset: 0x000BC850
		public virtual void SetPhysicsStateSynched(bool value, bool setChildren = true)
		{
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000BE652 File Offset: 0x000BC852
		public virtual void SetDisabledSynched()
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetMissionObjectDisabled(base.Id));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			base.SetDisabledAndMakeInvisible(false);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000BE680 File Offset: 0x000BC880
		public void SetFrameSynched(ref MatrixFrame frame, bool isClient = false)
		{
			if (base.GameEntity.GetFrame() != frame || this._synchState != SynchedMissionObject.SynchState.SynchronizeCompleted)
			{
				this._duration = 0f;
				this._timer = 0f;
				if (GameNetwork.IsClientOrReplay)
				{
					this._lastSynchedFrame = frame;
					this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeFrame);
					return;
				}
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectFrame(base.Id, ref frame));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeCompleted);
				base.GameEntity.SetFrame(ref frame);
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchTransform;
			}
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000BE724 File Offset: 0x000BC924
		public void SetGlobalFrameSynched(ref MatrixFrame frame, bool isClient = false)
		{
			this._duration = 0f;
			this._timer = 0f;
			if (base.GameEntity.GetGlobalFrame() != frame)
			{
				if (GameNetwork.IsClientOrReplay)
				{
					this._lastSynchedFrame = ((base.GameEntity.Parent != null) ? base.GameEntity.Parent.GetGlobalFrame().TransformToLocalNonOrthogonal(ref frame) : frame);
					this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeFrame);
					return;
				}
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectGlobalFrame(base.Id, ref frame));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeCompleted);
				base.GameEntity.SetGlobalFrame(frame);
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchTransform;
			}
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000BE7F4 File Offset: 0x000BC9F4
		public void SetFrameSynchedOverTime(ref MatrixFrame frame, float duration, bool isClient = false)
		{
			if (base.GameEntity.GetFrame() != frame || duration.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				this._firstFrame = base.GameEntity.GetFrame();
				this._lastSynchedFrame = frame;
				this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeFrameOverTime);
				this._duration = (duration.ApproximatelyEqualsTo(0f, 1E-05f) ? 0.1f : duration);
				this._timer = 0f;
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectFrameOverTime(base.Id, ref frame, duration));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchTransform;
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000BE8B4 File Offset: 0x000BCAB4
		public void SetGlobalFrameSynchedOverTime(ref MatrixFrame frame, float duration, bool isClient = false)
		{
			if (base.GameEntity.GetGlobalFrame() != frame || duration.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				this._firstFrame = base.GameEntity.GetFrame();
				this._lastSynchedFrame = ((base.GameEntity.Parent != null) ? base.GameEntity.Parent.GetGlobalFrame().TransformToLocalNonOrthogonal(ref frame) : frame);
				this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeFrameOverTime);
				this._duration = (duration.ApproximatelyEqualsTo(0f, 1E-05f) ? 0.1f : duration);
				this._timer = 0f;
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectGlobalFrameOverTime(base.Id, ref frame, duration));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchTransform;
			}
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000BE99F File Offset: 0x000BCB9F
		public void SetAnimationAtChannelSynched(string animationName, int channelNo, float animationSpeed = 1f)
		{
			this.SetAnimationAtChannelSynched(MBAnimation.GetAnimationIndexWithName(animationName), channelNo, animationSpeed);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000BE9B0 File Offset: 0x000BCBB0
		public void SetAnimationAtChannelSynched(int animationIndex, int channelNo, float animationSpeed = 1f)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				int animationIndexAtChannel = base.GameEntity.Skeleton.GetAnimationIndexAtChannel(channelNo);
				bool flag = true;
				if (animationIndexAtChannel == animationIndex && base.GameEntity.Skeleton.GetAnimationSpeedAtChannel(channelNo).ApproximatelyEqualsTo(animationSpeed, 1E-05f) && base.GameEntity.Skeleton.GetAnimationParameterAtChannel(channelNo) < 0.02f)
				{
					flag = false;
				}
				if (flag)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectAnimationAtChannel(base.Id, channelNo, animationIndex, animationSpeed));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchAnimation;
				}
			}
			base.GameEntity.Skeleton.SetAnimationAtChannel(animationIndex, channelNo, animationSpeed, -1f, 0f);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000BEA60 File Offset: 0x000BCC60
		public void SetAnimationChannelParameterSynched(int channelNo, float parameter)
		{
			if (!base.GameEntity.Skeleton.GetAnimationParameterAtChannel(channelNo).ApproximatelyEqualsTo(parameter, 1E-05f))
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectAnimationChannelParameter(base.Id, channelNo, parameter));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				base.GameEntity.Skeleton.SetAnimationParameterAtChannel(channelNo, parameter);
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchAnimation;
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000BEAD4 File Offset: 0x000BCCD4
		public void PauseSkeletonAnimationSynched()
		{
			if (!base.GameEntity.IsSkeletonAnimationPaused())
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectAnimationPaused(base.Id, true));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				base.GameEntity.PauseSkeletonAnimation();
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchAnimation;
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000BEB2C File Offset: 0x000BCD2C
		public void ResumeSkeletonAnimationSynched()
		{
			if (base.GameEntity.IsSkeletonAnimationPaused())
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectAnimationPaused(base.Id, false));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				base.GameEntity.ResumeSkeletonAnimation();
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchAnimation;
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000BEB84 File Offset: 0x000BCD84
		public void BurstParticlesSynched(bool doChildren = true)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new BurstMissionObjectParticles(base.Id, false));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			base.GameEntity.BurstEntityParticle(doChildren);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000BEBB8 File Offset: 0x000BCDB8
		public void ApplyImpulseSynched(Vec3 localPosition, Vec3 impulse)
		{
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetMissionObjectImpulse(base.Id, localPosition, impulse));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			base.GameEntity.ApplyLocalImpulseToDynamicBody(localPosition, impulse);
			this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchTransform;
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000BEC08 File Offset: 0x000BCE08
		public void AddBodyFlagsSynched(BodyFlags flags, bool applyToChildren = true)
		{
			if ((base.GameEntity.BodyFlag & flags) != flags)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new AddMissionObjectBodyFlags(base.Id, flags, applyToChildren));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				base.GameEntity.AddBodyFlags(flags, applyToChildren);
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchBodyFlags;
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000BEC68 File Offset: 0x000BCE68
		public void RemoveBodyFlagsSynched(BodyFlags flags, bool applyToChildren = true)
		{
			if ((base.GameEntity.BodyFlag & flags) != BodyFlags.None)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new RemoveMissionObjectBodyFlags(base.Id, flags, applyToChildren));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				base.GameEntity.RemoveBodyFlags(flags, applyToChildren);
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SynchBodyFlags;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000BECC5 File Offset: 0x000BCEC5
		public void SetTeamColors(uint color, uint color2)
		{
			this.Color = color;
			this.Color2 = color2;
			base.GameEntity.SetColor(color, color2, "use_team_color");
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000BECE8 File Offset: 0x000BCEE8
		public virtual void SetTeamColorsSynched(uint color, uint color2)
		{
			if (base.GameEntity != null)
			{
				if (GameNetwork.IsServerOrRecorder)
				{
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SetMissionObjectColors(base.Id, color, color2));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
				this.SetTeamColors(color, color2);
				this._initialSynchFlags |= SynchedMissionObject.SynchFlags.SyncColors;
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000BED40 File Offset: 0x000BCF40
		public virtual void WriteToNetwork()
		{
			GameEntity gameEntity = base.GameEntity;
			GameNetworkMessage.WriteBoolToPacket(gameEntity.GetVisibilityExcludeParents());
			GameNetworkMessage.WriteBoolToPacket(this._initialSynchFlags.HasAnyFlag(SynchedMissionObject.SynchFlags.SynchTransform));
			if (this._initialSynchFlags.HasAnyFlag(SynchedMissionObject.SynchFlags.SynchTransform))
			{
				GameNetworkMessage.WriteMatrixFrameToPacket(gameEntity.GetFrame());
				GameNetworkMessage.WriteBoolToPacket(this._synchState == SynchedMissionObject.SynchState.SynchronizeFrameOverTime);
				if (this._synchState == SynchedMissionObject.SynchState.SynchronizeFrameOverTime)
				{
					GameNetworkMessage.WriteMatrixFrameToPacket(this._lastSynchedFrame);
					GameNetworkMessage.WriteFloatToPacket(this._duration - this._timer, CompressionMission.FlagCapturePointDurationCompressionInfo);
				}
			}
			Skeleton skeleton = gameEntity.Skeleton;
			GameNetworkMessage.WriteBoolToPacket(skeleton != null);
			if (skeleton != null)
			{
				int animationIndexAtChannel = skeleton.GetAnimationIndexAtChannel(0);
				bool flag = animationIndexAtChannel >= 0;
				GameNetworkMessage.WriteBoolToPacket(flag && this._initialSynchFlags.HasAnyFlag(SynchedMissionObject.SynchFlags.SynchAnimation));
				if (flag && this._initialSynchFlags.HasAnyFlag(SynchedMissionObject.SynchFlags.SynchAnimation))
				{
					float animationSpeedAtChannel = skeleton.GetAnimationSpeedAtChannel(0);
					float animationParameterAtChannel = skeleton.GetAnimationParameterAtChannel(0);
					GameNetworkMessage.WriteIntToPacket(animationIndexAtChannel, CompressionBasic.AnimationIndexCompressionInfo);
					GameNetworkMessage.WriteFloatToPacket(animationSpeedAtChannel, CompressionBasic.AnimationSpeedCompressionInfo);
					GameNetworkMessage.WriteFloatToPacket(animationParameterAtChannel, CompressionBasic.AnimationProgressCompressionInfo);
					GameNetworkMessage.WriteBoolToPacket(gameEntity.IsSkeletonAnimationPaused());
				}
			}
			GameNetworkMessage.WriteBoolToPacket(this._initialSynchFlags.HasAnyFlag(SynchedMissionObject.SynchFlags.SyncColors));
			if (this._initialSynchFlags.HasAnyFlag(SynchedMissionObject.SynchFlags.SyncColors))
			{
				GameNetworkMessage.WriteUintToPacket(this.Color, CompressionBasic.ColorCompressionInfo);
				GameNetworkMessage.WriteUintToPacket(this.Color2, CompressionBasic.ColorCompressionInfo);
			}
			GameNetworkMessage.WriteBoolToPacket(base.IsDisabled);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000BEE98 File Offset: 0x000BD098
		public virtual void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			BaseSynchedMissionObjectReadableRecord item = synchedMissionObjectReadableRecord.Item1;
			base.GameEntity.SetVisibilityExcludeParents(item.SetVisibilityExcludeParents);
			if (item.SynchTransform)
			{
				MatrixFrame gameObjectFrame = item.GameObjectFrame;
				base.GameEntity.SetFrame(ref gameObjectFrame);
				if (item.SynchronizeFrameOverTime)
				{
					this._firstFrame = item.GameObjectFrame;
					this._lastSynchedFrame = item.LastSynchedFrame;
					this.SetSynchState(SynchedMissionObject.SynchState.SynchronizeFrameOverTime);
					this._duration = item.Duration;
					this._timer = 0f;
					if (this._duration.ApproximatelyEqualsTo(0f, 1E-05f))
					{
						this._duration = 0.1f;
					}
				}
			}
			if (item.HasSkeleton && item.SynchAnimation)
			{
				base.GameEntity.Skeleton.SetAnimationAtChannel(item.AnimationIndex, 0, item.AnimationSpeed, 0f, 0f);
				base.GameEntity.Skeleton.SetAnimationParameterAtChannel(0, item.AnimationParameter);
				if (item.IsSkeletonAnimationPaused)
				{
					base.GameEntity.Skeleton.TickAnimationsAndForceUpdate(0.001f, base.GameEntity.GetGlobalFrame(), true);
					base.GameEntity.PauseSkeletonAnimation();
				}
				else
				{
					base.GameEntity.ResumeSkeletonAnimation();
				}
			}
			if (item.SynchColors)
			{
				this.SetTeamColors(item.Color, item.Color2);
			}
			if (item.IsDisabled)
			{
				base.SetDisabledAndMakeInvisible(false);
			}
		}

		// Token: 0x04001394 RID: 5012
		private SynchedMissionObject.SynchFlags _initialSynchFlags;

		// Token: 0x04001395 RID: 5013
		private SynchedMissionObject.SynchState _synchState;

		// Token: 0x04001396 RID: 5014
		private MatrixFrame _lastSynchedFrame;

		// Token: 0x04001397 RID: 5015
		private MatrixFrame _firstFrame;

		// Token: 0x04001398 RID: 5016
		private float _timer;

		// Token: 0x04001399 RID: 5017
		private float _duration;

		// Token: 0x0200060C RID: 1548
		private enum SynchState
		{
			// Token: 0x04001F77 RID: 8055
			SynchronizeCompleted,
			// Token: 0x04001F78 RID: 8056
			SynchronizePosition,
			// Token: 0x04001F79 RID: 8057
			SynchronizeFrame,
			// Token: 0x04001F7A RID: 8058
			SynchronizeFrameOverTime
		}

		// Token: 0x0200060D RID: 1549
		[Flags]
		public enum SynchFlags : uint
		{
			// Token: 0x04001F7C RID: 8060
			SynchNone = 0U,
			// Token: 0x04001F7D RID: 8061
			SynchTransform = 1U,
			// Token: 0x04001F7E RID: 8062
			SynchAnimation = 2U,
			// Token: 0x04001F7F RID: 8063
			SynchBodyFlags = 4U,
			// Token: 0x04001F80 RID: 8064
			SyncColors = 8U,
			// Token: 0x04001F81 RID: 8065
			SynchAll = 4294967295U
		}
	}
}
