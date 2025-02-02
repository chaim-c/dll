using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000332 RID: 818
	public class DestructableComponent : SynchedMissionObject, IFocusable
	{
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06002C18 RID: 11288 RVA: 0x000AC5AC File Offset: 0x000AA7AC
		// (remove) Token: 0x06002C19 RID: 11289 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
		public event Action OnNextDestructionState;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06002C1A RID: 11290 RVA: 0x000AC61C File Offset: 0x000AA81C
		// (remove) Token: 0x06002C1B RID: 11291 RVA: 0x000AC654 File Offset: 0x000AA854
		public event DestructableComponent.OnHitTakenAndDestroyedDelegate OnDestroyed;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06002C1C RID: 11292 RVA: 0x000AC68C File Offset: 0x000AA88C
		// (remove) Token: 0x06002C1D RID: 11293 RVA: 0x000AC6C4 File Offset: 0x000AA8C4
		public event DestructableComponent.OnHitTakenAndDestroyedDelegate OnHitTaken;

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000AC6F9 File Offset: 0x000AA8F9
		// (set) Token: 0x06002C1F RID: 11295 RVA: 0x000AC704 File Offset: 0x000AA904
		public float HitPoint
		{
			get
			{
				return this._hitPoint;
			}
			set
			{
				if (!this._hitPoint.Equals(value))
				{
					this._hitPoint = MathF.Max(value, 0f);
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SyncObjectHitpoints(base.Id, value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					}
				}
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000AC755 File Offset: 0x000AA955
		public FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.None;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000AC758 File Offset: 0x000AA958
		public bool IsDestroyed
		{
			get
			{
				return this.HitPoint <= 0f;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000AC76A File Offset: 0x000AA96A
		// (set) Token: 0x06002C23 RID: 11299 RVA: 0x000AC772 File Offset: 0x000AA972
		public GameEntity CurrentState { get; private set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x000AC77B File Offset: 0x000AA97B
		private bool HasDestructionState
		{
			get
			{
				return this._destructionStates != null && !this._destructionStates.IsEmpty<string>();
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000AC798 File Offset: 0x000AA998
		private DestructableComponent()
		{
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000AC7F0 File Offset: 0x000AA9F0
		protected internal override void OnInit()
		{
			base.OnInit();
			this._hitPoint = this.MaxHitPoint;
			this._referenceEntity = (string.IsNullOrEmpty(this.ReferenceEntityTag) ? base.GameEntity : base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.HasTag(this.ReferenceEntityTag)));
			if (!string.IsNullOrEmpty(this.DestructionStates))
			{
				this._destructionStates = this.DestructionStates.Replace(" ", string.Empty).Split(new char[]
				{
					','
				});
				bool flag = false;
				string[] destructionStates = this._destructionStates;
				for (int i = 0; i < destructionStates.Length; i++)
				{
					string item = destructionStates[i];
					if (!string.IsNullOrEmpty(item))
					{
						GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.Name == item);
						if (gameEntity != null)
						{
							gameEntity.AddBodyFlags(BodyFlags.Moveable, true);
							PhysicsShape bodyShape = gameEntity.GetBodyShape();
							if (bodyShape != null)
							{
								PhysicsShape.AddPreloadQueueWithName(bodyShape.GetName(), gameEntity.GetGlobalScale());
								flag = true;
							}
						}
						else
						{
							GameEntity gameEntity2 = GameEntity.Instantiate(null, item, false);
							List<GameEntity> list = new List<GameEntity>();
							gameEntity2.GetChildrenRecursive(ref list);
							list.Add(gameEntity2);
							foreach (GameEntity gameEntity3 in list)
							{
								PhysicsShape bodyShape2 = gameEntity3.GetBodyShape();
								if (bodyShape2 != null)
								{
									Vec3 globalScale = gameEntity3.GetGlobalScale();
									Vec3 globalScale2 = this._referenceEntity.GetGlobalScale();
									Vec3 scale = new Vec3(globalScale.x * globalScale2.x, globalScale.y * globalScale2.y, globalScale.z * globalScale2.z, -1f);
									PhysicsShape.AddPreloadQueueWithName(bodyShape2.GetName(), scale);
									flag = true;
								}
							}
						}
					}
				}
				if (flag)
				{
					PhysicsShape.ProcessPreloadQueue();
				}
			}
			this._originalState = this.GetOriginalState(base.GameEntity);
			if (this._originalState == null)
			{
				this._originalState = base.GameEntity;
			}
			this.CurrentState = this._originalState;
			this._originalState.AddBodyFlags(BodyFlags.Moveable, true);
			List<GameEntity> source = new List<GameEntity>();
			base.GameEntity.GetChildrenRecursive(ref source);
			foreach (GameEntity gameEntity4 in from child in source
			where child.BodyFlag.HasAnyFlag(BodyFlags.Dynamic)
			select child)
			{
				gameEntity4.SetPhysicsState(false, true);
				gameEntity4.SetFrameChanged();
			}
			this._heavyHitParticles = base.GameEntity.CollectChildrenEntitiesWithTag(this.HeavyHitParticlesTag);
			base.GameEntity.SetAnimationSoundActivation(true);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000ACAF0 File Offset: 0x000AACF0
		public GameEntity GetOriginalState(GameEntity parent)
		{
			int childCount = parent.ChildCount;
			for (int i = 0; i < childCount; i++)
			{
				GameEntity child = parent.GetChild(i);
				if (!child.HasScriptOfType<DestructableComponent>())
				{
					if (child.HasTag(this.OriginalStateTag))
					{
						return child;
					}
					GameEntity originalState = this.GetOriginalState(child);
					if (originalState != null)
					{
						return originalState;
					}
				}
			}
			return null;
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000ACB44 File Offset: 0x000AAD44
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this._referenceEntity = (string.IsNullOrEmpty(this.ReferenceEntityTag) ? base.GameEntity : base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.HasTag(this.ReferenceEntityTag)));
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000ACB84 File Offset: 0x000AAD84
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName.Equals(this.ReferenceEntityTag))
			{
				this._referenceEntity = (string.IsNullOrEmpty(this.ReferenceEntityTag) ? base.GameEntity : base.GameEntity.GetChildren().SingleOrDefault((GameEntity x) => x.HasTag(this.ReferenceEntityTag)));
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000ACBDD File Offset: 0x000AADDD
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			this.Reset();
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000ACBEB File Offset: 0x000AADEB
		public void Reset()
		{
			this.RestoreEntity();
			this._hitPoint = this.MaxHitPoint;
			this._currentStateIndex = 0;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000ACC08 File Offset: 0x000AAE08
		private void RestoreEntity()
		{
			if (this._destructionStates != null)
			{
				int j;
				int i;
				for (i = 0; i < this._destructionStates.Length; i = j + 1)
				{
					GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.Name == this._destructionStates[i].ToString());
					if (gameEntity != null)
					{
						Skeleton skeleton = gameEntity.Skeleton;
						if (skeleton != null)
						{
							skeleton.SetAnimationAtChannel(-1, 0, 1f, -1f, 0f);
						}
					}
					j = i;
				}
			}
			if (this.CurrentState != this._originalState)
			{
				this.CurrentState.SetVisibilityExcludeParents(false);
				this.CurrentState = this._originalState;
			}
			this.CurrentState.SetVisibilityExcludeParents(true);
			this.CurrentState.SetPhysicsState(true, true);
			this.CurrentState.SetFrameChanged();
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000ACCE8 File Offset: 0x000AAEE8
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (this._referenceEntity != null && this._referenceEntity != base.GameEntity && MBEditor.IsEntitySelected(this._referenceEntity))
			{
				new Vec3(-2f, -0.5f, -1f, -1f);
				new Vec3(2f, 0.5f, 1f, -1f);
				MatrixFrame identity = MatrixFrame.Identity;
				GameEntity gameEntity = this._referenceEntity;
				while (gameEntity.Parent != null)
				{
					gameEntity = gameEntity.Parent;
				}
				gameEntity.GetMeshBendedFrame(this._referenceEntity.GetGlobalFrame(), ref identity);
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000ACD98 File Offset: 0x000AAF98
		public void TriggerOnHit(Agent attackerAgent, int inflictedDamage, Vec3 impactPosition, Vec3 impactDirection, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior)
		{
			bool flag;
			this.OnHit(attackerAgent, inflictedDamage, impactPosition, impactDirection, weapon, attackerScriptComponentBehavior, out flag);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000ACDB8 File Offset: 0x000AAFB8
		protected internal override bool OnHit(Agent attackerAgent, int inflictedDamage, Vec3 impactPosition, Vec3 impactDirection, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, out bool reportDamage)
		{
			reportDamage = false;
			if (base.IsDisabled)
			{
				return true;
			}
			MissionWeapon missionWeapon = weapon;
			if (missionWeapon.IsEmpty && !(attackerScriptComponentBehavior is BatteringRam))
			{
				inflictedDamage = 0;
			}
			else if (this.DestroyedByStoneOnly)
			{
				missionWeapon = weapon;
				WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
				if ((currentUsageItem.WeaponClass != WeaponClass.Stone && currentUsageItem.WeaponClass != WeaponClass.Boulder) || !currentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.NotUsableWithOneHand))
				{
					inflictedDamage = 0;
				}
			}
			bool isDestroyed = this.IsDestroyed;
			if (this.DestroyOnAnyHit)
			{
				inflictedDamage = (int)(this.MaxHitPoint + 1f);
			}
			if (inflictedDamage > 0 && !isDestroyed)
			{
				this.HitPoint -= (float)inflictedDamage;
				if ((float)inflictedDamage > this.HeavyHitParticlesThreshold)
				{
					this.BurstHeavyHitParticles();
				}
				int state = this.CalculateNextDestructionLevel(inflictedDamage);
				if (!this.IsDestroyed)
				{
					DestructableComponent.OnHitTakenAndDestroyedDelegate onHitTaken = this.OnHitTaken;
					if (onHitTaken != null)
					{
						onHitTaken(this, attackerAgent, weapon, attackerScriptComponentBehavior, inflictedDamage);
					}
				}
				else if (this.IsDestroyed && !isDestroyed)
				{
					Mission.Current.OnObjectDisabled(this);
					DestructableComponent.OnHitTakenAndDestroyedDelegate onHitTaken2 = this.OnHitTaken;
					if (onHitTaken2 != null)
					{
						onHitTaken2(this, attackerAgent, weapon, attackerScriptComponentBehavior, inflictedDamage);
					}
					DestructableComponent.OnHitTakenAndDestroyedDelegate onDestroyed = this.OnDestroyed;
					if (onDestroyed != null)
					{
						onDestroyed(this, attackerAgent, weapon, attackerScriptComponentBehavior, inflictedDamage);
					}
					MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
					globalFrame.origin += globalFrame.rotation.u * this.SoundAndParticleEffectHeightOffset + globalFrame.rotation.f * this.SoundAndParticleEffectForwardOffset;
					globalFrame.rotation.Orthonormalize();
					if (this.ParticleEffectOnDestroy != "")
					{
						Mission.Current.Scene.CreateBurstParticle(ParticleSystemManager.GetRuntimeIdByName(this.ParticleEffectOnDestroy), globalFrame);
					}
					if (this.SoundEffectOnDestroy != "")
					{
						Mission.Current.MakeSound(SoundEvent.GetEventIdFromString(this.SoundEffectOnDestroy), globalFrame.origin, false, true, (attackerAgent != null) ? attackerAgent.Index : -1, -1);
					}
				}
				this.SetDestructionLevel(state, -1, (float)inflictedDamage, impactPosition, impactDirection, false);
				reportDamage = true;
			}
			return !this.PassHitOnToParent;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000ACFE0 File Offset: 0x000AB1E0
		public void BurstHeavyHitParticles()
		{
			foreach (GameEntity gameEntity in this._heavyHitParticles)
			{
				gameEntity.BurstEntityParticle(false);
			}
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new BurstAllHeavyHitParticles(base.Id));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000AD050 File Offset: 0x000AB250
		private int CalculateNextDestructionLevel(int inflictedDamage)
		{
			if (this.HasDestructionState)
			{
				int num = this._destructionStates.Length;
				float num2 = this.MaxHitPoint / (float)num;
				float num3 = this.MaxHitPoint;
				int num4 = 0;
				while (num3 - num2 >= this.HitPoint)
				{
					num3 -= num2;
					num4++;
				}
				Func<int, int, int, int> onCalculateDestructionStateIndex = this.OnCalculateDestructionStateIndex;
				return (onCalculateDestructionStateIndex != null) ? onCalculateDestructionStateIndex(num4, inflictedDamage, this.DestructionStates.Length) : num4;
			}
			if (this.IsDestroyed)
			{
				return this._currentStateIndex + 1;
			}
			return this._currentStateIndex;
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000AD0D0 File Offset: 0x000AB2D0
		public void SetDestructionLevel(int state, int forcedId, float blowMagnitude, Vec3 blowPosition, Vec3 blowDirection, bool noEffects = false)
		{
			if (this._currentStateIndex != state)
			{
				float blowMagnitude2 = MBMath.ClampFloat(blowMagnitude, 1f, DestructableComponent.MaxBlowMagnitude);
				this._currentStateIndex = state;
				this.ReplaceEntityWithBrokenEntity(forcedId);
				if (this.CurrentState != null)
				{
					foreach (GameEntity gameEntity in from child in this.CurrentState.GetChildren()
					where child.BodyFlag.HasAnyFlag(BodyFlags.Dynamic)
					select child)
					{
						gameEntity.SetPhysicsState(true, true);
						gameEntity.SetFrameChanged();
					}
					if (!GameNetwork.IsDedicatedServer && !noEffects)
					{
						this.CurrentState.BurstEntityParticle(true);
						this.ApplyPhysics(blowMagnitude2, blowPosition, blowDirection);
					}
					Action onNextDestructionState = this.OnNextDestructionState;
					if (onNextDestructionState != null)
					{
						onNextDestructionState();
					}
				}
				if (GameNetwork.IsServerOrRecorder)
				{
					if (this.CurrentState != null)
					{
						MissionObject firstScriptOfType = this.CurrentState.GetFirstScriptOfType<MissionObject>();
						if (firstScriptOfType != null)
						{
							forcedId = firstScriptOfType.Id.Id;
						}
					}
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new SyncObjectDestructionLevel(base.Id, state, forcedId, blowMagnitude2, blowPosition, blowDirection));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
				}
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000AD210 File Offset: 0x000AB410
		private void ApplyPhysics(float blowMagnitude, Vec3 blowPosition, Vec3 blowDirection)
		{
			if (this.CurrentState != null)
			{
				IEnumerable<GameEntity> enumerable = from child in this.CurrentState.GetChildren()
				where child.HasBody() && child.BodyFlag.HasAnyFlag(BodyFlags.Dynamic) && !child.HasScriptOfType<SpawnedItemEntity>()
				select child;
				int num = enumerable.Count<GameEntity>();
				float f = (num > 1) ? (blowMagnitude / (float)num) : blowMagnitude;
				foreach (GameEntity gameEntity in enumerable)
				{
					gameEntity.ApplyLocalImpulseToDynamicBody(Vec3.Zero, blowDirection * f);
					Mission.Current.AddTimerToDynamicEntity(gameEntity, 10f + MBRandom.RandomFloat * 2f);
				}
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000AD2D4 File Offset: 0x000AB4D4
		private void ReplaceEntityWithBrokenEntity(int forcedId)
		{
			this._previousState = this.CurrentState;
			this._previousState.SetVisibilityExcludeParents(false);
			if (this.HasDestructionState)
			{
				bool flag;
				this.CurrentState = this.AddBrokenEntity(this._destructionStates[this._currentStateIndex - 1], out flag);
				if (flag)
				{
					if (this._originalState != base.GameEntity)
					{
						base.GameEntity.AddChild(this.CurrentState, true);
					}
					if (forcedId != -1)
					{
						MissionObject firstScriptOfType = this.CurrentState.GetFirstScriptOfType<MissionObject>();
						if (firstScriptOfType != null)
						{
							firstScriptOfType.Id = new MissionObjectId(forcedId, true);
							using (IEnumerator<GameEntity> enumerator = this.CurrentState.GetChildren().GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									GameEntity gameEntity = enumerator.Current;
									MissionObject firstScriptOfType2 = gameEntity.GetFirstScriptOfType<MissionObject>();
									if (firstScriptOfType2 != null && firstScriptOfType2.Id.CreatedAtRuntime)
									{
										firstScriptOfType2.Id = new MissionObjectId(++forcedId, true);
									}
								}
								return;
							}
						}
						MBDebug.ShowWarning("Current destruction state doesn't have mission object script component.");
						return;
					}
				}
				else
				{
					MatrixFrame frame = this._previousState.GetFrame();
					this.CurrentState.SetFrame(ref frame);
				}
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000AD3FC File Offset: 0x000AB5FC
		protected internal override bool MovesEntity()
		{
			return true;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000AD3FF File Offset: 0x000AB5FF
		public void PreDestroy()
		{
			DestructableComponent.OnHitTakenAndDestroyedDelegate onDestroyed = this.OnDestroyed;
			if (onDestroyed != null)
			{
				onDestroyed(this, null, MissionWeapon.Invalid, null, 0);
			}
			this.SetVisibleSynched(false, true);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000AD424 File Offset: 0x000AB624
		private GameEntity AddBrokenEntity(string prefab, out bool newCreated)
		{
			if (!string.IsNullOrEmpty(prefab))
			{
				GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.Name == prefab);
				if (gameEntity != null)
				{
					gameEntity.SetVisibilityExcludeParents(true);
					if (!GameNetwork.IsClientOrReplay)
					{
						MissionObject missionObject = gameEntity.GetScriptComponents<MissionObject>().FirstOrDefault<MissionObject>();
						if (missionObject != null)
						{
							missionObject.SetAbilityOfFaces(true);
						}
					}
					newCreated = false;
				}
				else
				{
					gameEntity = GameEntity.Instantiate(Mission.Current.Scene, prefab, this._referenceEntity.GetGlobalFrame());
					if (gameEntity != null)
					{
						gameEntity.SetMobility(GameEntity.Mobility.stationary);
					}
					if (base.GameEntity.Parent != null)
					{
						base.GameEntity.Parent.AddChild(gameEntity, true);
					}
					newCreated = true;
				}
				if (this._referenceEntity.Skeleton != null && gameEntity.Skeleton != null)
				{
					Skeleton skeleton = ((this.CurrentState != this._originalState) ? this.CurrentState : this._referenceEntity).Skeleton;
					int animationIndexAtChannel = skeleton.GetAnimationIndexAtChannel(0);
					float animationParameterAtChannel = skeleton.GetAnimationParameterAtChannel(0);
					if (animationIndexAtChannel != -1)
					{
						gameEntity.Skeleton.SetAnimationAtChannel(animationIndexAtChannel, 0, 1f, -1f, animationParameterAtChannel);
						gameEntity.ResumeSkeletonAnimation();
					}
				}
				return gameEntity;
			}
			newCreated = false;
			return null;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000AD578 File Offset: 0x000AB778
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteFloatToPacket(MathF.Max(this.HitPoint, 0f), CompressionMission.UsableGameObjectHealthCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this._currentStateIndex, CompressionMission.UsableGameObjectDestructionStateCompressionInfo);
			if (this._currentStateIndex != 0)
			{
				MissionObject firstScriptOfType = this.CurrentState.GetFirstScriptOfType<MissionObject>();
				GameNetworkMessage.WriteBoolToPacket(firstScriptOfType != null);
				if (firstScriptOfType != null)
				{
					GameNetworkMessage.WriteMissionObjectIdToPacket(firstScriptOfType.Id);
				}
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000AD5E0 File Offset: 0x000AB7E0
		public override void AddStuckMissile(GameEntity missileEntity)
		{
			if (this.CurrentState != null)
			{
				this.CurrentState.AddChild(missileEntity, false);
				return;
			}
			base.GameEntity.AddChild(missileEntity, false);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000AD60C File Offset: 0x000AB80C
		protected internal override bool OnCheckForProblems()
		{
			bool result = base.OnCheckForProblems();
			if ((string.IsNullOrEmpty(this.ReferenceEntityTag) ? base.GameEntity : base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.HasTag(this.ReferenceEntityTag))) == null)
			{
				MBEditor.AddEntityWarning(base.GameEntity, "Reference entity must be assigned. Root entity is " + base.GameEntity.Root.Name + ", child is " + base.GameEntity.Name);
				result = true;
			}
			string[] array = this.DestructionStates.Replace(" ", string.Empty).Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string destructionState = array[i];
				if (!string.IsNullOrEmpty(destructionState) && !(base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.Name == destructionState) != null) && GameEntity.Instantiate(null, destructionState, false) == null)
				{
					MBEditor.AddEntityWarning(base.GameEntity, "Destruction state '" + destructionState + "' is not valid.");
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000AD73F File Offset: 0x000AB93F
		public void OnFocusGain(Agent userAgent)
		{
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000AD741 File Offset: 0x000AB941
		public void OnFocusLose(Agent userAgent)
		{
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000AD743 File Offset: 0x000AB943
		public TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return new TextObject("", null);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000AD750 File Offset: 0x000AB950
		public string GetDescriptionText(GameEntity gameEntity = null)
		{
			int num;
			if (int.TryParse(gameEntity.Name.Split(new char[]
			{
				'_'
			}).Last<string>(), out num))
			{
				string text = gameEntity.Name;
				text = text.Remove(text.Length - num.ToString().Length);
				text += "x";
				TextObject textObject;
				if (GameTexts.TryGetText("str_destructible_component", out textObject, text))
				{
					return textObject.ToString();
				}
				return "";
			}
			else
			{
				TextObject textObject2;
				if (GameTexts.TryGetText("str_destructible_component", out textObject2, gameEntity.Name))
				{
					return textObject2.ToString();
				}
				return "";
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000AD7EC File Offset: 0x000AB9EC
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord);
			DestructableComponent.DestructableComponentRecord destructableComponentRecord = (DestructableComponent.DestructableComponentRecord)synchedMissionObjectReadableRecord.Item2;
			this.HitPoint = destructableComponentRecord.HitPoint;
			if (destructableComponentRecord.DestructionState != 0)
			{
				if (this.IsDestroyed)
				{
					DestructableComponent.OnHitTakenAndDestroyedDelegate onDestroyed = this.OnDestroyed;
					if (onDestroyed != null)
					{
						onDestroyed(this, null, MissionWeapon.Invalid, null, 0);
					}
				}
				this.SetDestructionLevel(destructableComponentRecord.DestructionState, destructableComponentRecord.ForceIndex, 0f, Vec3.Zero, Vec3.Zero, true);
			}
		}

		// Token: 0x04001183 RID: 4483
		public const string CleanStateTag = "operational";

		// Token: 0x04001184 RID: 4484
		public static float MaxBlowMagnitude = 20f;

		// Token: 0x04001185 RID: 4485
		public string DestructionStates;

		// Token: 0x04001186 RID: 4486
		public bool DestroyedByStoneOnly;

		// Token: 0x04001187 RID: 4487
		public bool CanBeDestroyedInitially = true;

		// Token: 0x04001188 RID: 4488
		public float MaxHitPoint = 100f;

		// Token: 0x04001189 RID: 4489
		public bool DestroyOnAnyHit;

		// Token: 0x0400118A RID: 4490
		public bool PassHitOnToParent;

		// Token: 0x0400118B RID: 4491
		public string ReferenceEntityTag;

		// Token: 0x0400118C RID: 4492
		public string HeavyHitParticlesTag;

		// Token: 0x0400118D RID: 4493
		public float HeavyHitParticlesThreshold = 5f;

		// Token: 0x0400118E RID: 4494
		public string ParticleEffectOnDestroy = "";

		// Token: 0x0400118F RID: 4495
		public string SoundEffectOnDestroy = "";

		// Token: 0x04001190 RID: 4496
		public float SoundAndParticleEffectHeightOffset;

		// Token: 0x04001191 RID: 4497
		public float SoundAndParticleEffectForwardOffset;

		// Token: 0x04001195 RID: 4501
		public BattleSideEnum BattleSide = BattleSideEnum.None;

		// Token: 0x04001196 RID: 4502
		[EditableScriptComponentVariable(false)]
		public Func<int, int, int, int> OnCalculateDestructionStateIndex;

		// Token: 0x04001197 RID: 4503
		private float _hitPoint;

		// Token: 0x04001198 RID: 4504
		private string OriginalStateTag = "operational";

		// Token: 0x04001199 RID: 4505
		private GameEntity _referenceEntity;

		// Token: 0x0400119A RID: 4506
		private GameEntity _previousState;

		// Token: 0x0400119B RID: 4507
		private GameEntity _originalState;

		// Token: 0x0400119D RID: 4509
		private string[] _destructionStates;

		// Token: 0x0400119E RID: 4510
		private int _currentStateIndex;

		// Token: 0x0400119F RID: 4511
		private IEnumerable<GameEntity> _heavyHitParticles;

		// Token: 0x020005E8 RID: 1512
		[DefineSynchedMissionObjectType(typeof(DestructableComponent))]
		public struct DestructableComponentRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000E932B File Offset: 0x000E752B
			// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000E9333 File Offset: 0x000E7533
			public float HitPoint { get; private set; }

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000E933C File Offset: 0x000E753C
			// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x000E9344 File Offset: 0x000E7544
			public int DestructionState { get; private set; }

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000E934D File Offset: 0x000E754D
			// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000E9355 File Offset: 0x000E7555
			public int ForceIndex { get; private set; }

			// Token: 0x170009DE RID: 2526
			// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000E935E File Offset: 0x000E755E
			// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000E9366 File Offset: 0x000E7566
			public bool IsMissionObject { get; private set; }

			// Token: 0x06003BA6 RID: 15270 RVA: 0x000E936F File Offset: 0x000E756F
			public DestructableComponentRecord(float hitPoint, int destructionState, int forceIndex, bool isMissionObject)
			{
				this.HitPoint = hitPoint;
				this.DestructionState = destructionState;
				this.ForceIndex = forceIndex;
				this.IsMissionObject = isMissionObject;
			}

			// Token: 0x06003BA7 RID: 15271 RVA: 0x000E9390 File Offset: 0x000E7590
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.HitPoint = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.UsableGameObjectHealthCompressionInfo, ref bufferReadValid);
				this.DestructionState = GameNetworkMessage.ReadIntFromPacket(CompressionMission.UsableGameObjectDestructionStateCompressionInfo, ref bufferReadValid);
				this.ForceIndex = -1;
				if (this.DestructionState != 0)
				{
					this.IsMissionObject = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
					if (this.IsMissionObject)
					{
						this.ForceIndex = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref bufferReadValid).Id;
					}
				}
				return bufferReadValid;
			}
		}

		// Token: 0x020005E9 RID: 1513
		// (Invoke) Token: 0x06003BA9 RID: 15273
		public delegate void OnHitTakenAndDestroyedDelegate(DestructableComponent target, Agent attackerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage);
	}
}
