using System;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000367 RID: 871
	public class SpawnedItemEntity : UsableMissionObject
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000C147B File Offset: 0x000BF67B
		public MissionWeapon WeaponCopy
		{
			get
			{
				return this._weapon;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000C1483 File Offset: 0x000BF683
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x000C148B File Offset: 0x000BF68B
		public bool HasLifeTime
		{
			get
			{
				return this._hasLifeTime;
			}
			set
			{
				if (this._hasLifeTime != value)
				{
					this._hasLifeTime = value;
					base.SetScriptComponentToTickMT(this.GetTickRequirement());
				}
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000C14A9 File Offset: 0x000BF6A9
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x000C14B1 File Offset: 0x000BF6B1
		private bool PhysicsStopped
		{
			get
			{
				return this._physicsStopped;
			}
			set
			{
				if (this._physicsStopped != value)
				{
					this._physicsStopped = value;
					base.SetScriptComponentToTickMT(this.GetTickRequirement());
				}
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000C14CF File Offset: 0x000BF6CF
		public bool IsRemoved
		{
			get
			{
				return this._ownerGameEntity == null;
			}
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000C14E0 File Offset: 0x000BF6E0
		public TextObject GetActionMessage(ItemObject weaponToReplaceWith, bool fillUp)
		{
			GameTexts.SetVariable("USE_KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
			if (weaponToReplaceWith == null)
			{
				MBTextManager.SetTextVariable("ACTION", fillUp ? GameTexts.FindText("str_ui_fill", null) : GameTexts.FindText("str_ui_equip", null), false);
			}
			else
			{
				MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_ui_swap", null), false);
				MBTextManager.SetTextVariable("ITEM_NAME", weaponToReplaceWith.Name, false);
			}
			return GameTexts.FindText("str_key_action", null);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000C157C File Offset: 0x000BF77C
		public TextObject GetDescriptionMessage(bool fillUp)
		{
			if (!fillUp)
			{
				return this._weapon.GetModifiedItemName();
			}
			return GameTexts.FindText("str_inventory_weapon", ((int)this._weapon.CurrentUsageItem.WeaponClass).ToString());
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000C15BA File Offset: 0x000BF7BA
		protected internal override bool LockUserFrames
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000C15BD File Offset: 0x000BF7BD
		// (set) Token: 0x06002F4D RID: 12109 RVA: 0x000C15C5 File Offset: 0x000BF7C5
		public Mission.WeaponSpawnFlags SpawnFlags { get; private set; }

		// Token: 0x06002F4E RID: 12110 RVA: 0x000C15D0 File Offset: 0x000BF7D0
		public void Initialize(MissionWeapon weapon, bool hasLifeTime, Mission.WeaponSpawnFlags spawnFlags, in Vec3 fakeSimulationVelocity)
		{
			this._weapon = weapon;
			this.HasLifeTime = hasLifeTime;
			this.SpawnFlags = spawnFlags;
			this._fakeSimulationVelocity = fakeSimulationVelocity;
			if (this.HasLifeTime)
			{
				float duration = 0f;
				if (!this._weapon.IsEmpty)
				{
					duration = (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.QuickFadeOut) ? 5f : 180f);
					base.IsDeactivated = this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.CannotBePickedUp);
					if (this._weapon.CurrentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.RangedWeapon | WeaponFlags.NotUsableWithOneHand | WeaponFlags.Consumable))
					{
						this._lastSoundPlayTime = 0.333f;
					}
					else
					{
						this._lastSoundPlayTime = -0.333f;
					}
				}
				else
				{
					base.IsDeactivated = true;
				}
				this._deletionTimer = new Timer(Mission.Current.CurrentTime, duration, true);
			}
			else
			{
				this._deletionTimer = new Timer(Mission.Current.CurrentTime, float.MaxValue, true);
			}
			if (spawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics))
			{
				this._disablePhysicsTimer = new Timer(Mission.Current.CurrentTime, 10f, true);
			}
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000C1700 File Offset: 0x000BF900
		protected internal override void OnInit()
		{
			base.OnInit();
			this._ownerGameEntity = base.GameEntity;
			if (!string.IsNullOrEmpty(this.WeaponName))
			{
				ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(this.WeaponName);
				this._weapon = new MissionWeapon(@object, null, null);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000C175C File Offset: 0x000BF95C
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (GameNetwork.IsClientOrReplay || base.HasUser || !this.PhysicsStopped)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel2;
			}
			if (!this.HasLifeTime)
			{
				return base.GetTickRequirement();
			}
			if (!base.GetTickRequirement().HasAnyFlag(ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel2))
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.TickOccasionally;
			}
			return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel2;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000C17BC File Offset: 0x000BF9BC
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (GameNetwork.IsClientOrReplay && this._clientSyncData != null)
			{
				if (this._clientSyncData.Timer.Check(Mission.Current.CurrentTime))
				{
					this._ownerGameEntity.SetAlpha(1f);
					this._clientSyncData = null;
					return;
				}
				float duration = this._clientSyncData.Timer.Duration;
				float num = MBMath.ClampFloat(this._clientSyncData.Timer.ElapsedTime() / duration, 0f, 1f);
				if (num < (1f - 0.1f / duration) * 0.5f)
				{
					this._ownerGameEntity.SetAlpha(1f - num * 2f);
					return;
				}
				if (num < (1f + 0.1f / duration) * 0.5f)
				{
					this._ownerGameEntity.SetAlpha(0f);
					this._ownerGameEntity.SetGlobalFrame(this._clientSyncData.Frame);
					GameEntity parent = this._clientSyncData.Parent;
					if (parent != null)
					{
						parent.AddChild(this._ownerGameEntity, true);
					}
					this._clientSyncData.Timer.Reset(Mission.Current.CurrentTime - duration * (1f + 0.1f / duration) * 0.5f);
					return;
				}
				this._ownerGameEntity.SetAlpha(num * 2f - 1f);
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000C1920 File Offset: 0x000BFB20
		protected internal override void OnTickParallel2(float dt)
		{
			base.OnTickParallel2(dt);
			if (!GameNetwork.IsClientOrReplay)
			{
				if (base.HasUser)
				{
					ActionIndexValueCache currentActionValue = base.UserAgent.GetCurrentActionValue(this._usedChannelIndex);
					if (currentActionValue == this._successActionIndex)
					{
						base.UserAgent.StopUsingGameObjectMT(base.UserAgent.CanUseObject(this), Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					else if (currentActionValue != this._progressActionIndex)
					{
						base.UserAgent.StopUsingGameObjectMT(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
				else if (this.HasLifeTime && this._deletionTimer.Check(Mission.Current.CurrentTime))
				{
					this._readyToBeDeleted = true;
				}
				if (!this.PhysicsStopped)
				{
					if (this._ownerGameEntity != null)
					{
						if (this._weapon.IsBanner())
						{
							MatrixFrame globalFrame = this._ownerGameEntity.GetGlobalFrame();
							this._fakeSimulationVelocity.z = this._fakeSimulationVelocity.z - dt * 9.8f;
							globalFrame.origin += this._fakeSimulationVelocity * dt;
							this._ownerGameEntity.SetGlobalFrameMT(globalFrame);
							using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
							{
								if (this._ownerGameEntity.Scene.GetGroundHeightAtPositionMT(globalFrame.origin, BodyFlags.CommonCollisionExcludeFlags) > globalFrame.origin.z + 0.3f)
								{
									this.PhysicsStopped = true;
								}
								return;
							}
						}
						Vec3 globalPosition = this._ownerGameEntity.GlobalPosition;
						if (globalPosition.z <= CompressionBasic.PositionCompressionInfo.GetMinimumValue() + 5f)
						{
							this._readyToBeDeleted = true;
						}
						if (!this._ownerGameEntity.BodyFlag.HasAnyFlag(BodyFlags.Dynamic))
						{
							this.PhysicsStopped = true;
							return;
						}
						MatrixFrame globalFrame2 = this._ownerGameEntity.GetGlobalFrame();
						if (!globalFrame2.rotation.IsUnit())
						{
							globalFrame2.rotation.Orthonormalize();
							this._ownerGameEntity.SetGlobalFrame(globalFrame2);
						}
						bool flag = this._disablePhysicsTimer.Check(Mission.Current.CurrentTime);
						if (flag || this._disablePhysicsTimer.ElapsedTime() > 1f)
						{
							bool flag2;
							using (new TWSharedMutexUpgradeableReadLock(Scene.PhysicsAndRayCastLock))
							{
								flag2 = (flag || this._ownerGameEntity.IsDynamicBodyStationaryMT());
								if (flag2)
								{
									this._groundEntityWhenDisabled = this.TryFindProperGroundEntityForSpawnedEntity();
									if (this._groundEntityWhenDisabled != null)
									{
										this._groundEntityWhenDisabled.AddChild(base.GameEntity, true);
									}
									using (new TWSharedMutexWriteLock(Scene.PhysicsAndRayCastLock))
									{
										if (!this._weapon.IsEmpty && !this._ownerGameEntity.BodyFlag.HasAnyFlag(BodyFlags.Disabled))
										{
											this._ownerGameEntity.DisableDynamicBodySimulationMT();
										}
										else
										{
											this._ownerGameEntity.RemovePhysicsMT(false);
										}
									}
								}
							}
							if (flag2)
							{
								this.ClampEntityPositionForStoppingIfNeeded();
								this.PhysicsStopped = true;
								if ((!base.IsDeactivated || this._groundEntityWhenDisabled != null) && !this._weapon.IsEmpty && GameNetwork.IsServerOrRecorder)
								{
									GameNetwork.BeginBroadcastModuleEvent();
									MissionObjectId id = base.Id;
									GameEntity groundEntityWhenDisabled = this._groundEntityWhenDisabled;
									GameNetwork.WriteMessage(new StopPhysicsAndSetFrameOfMissionObject(id, (groundEntityWhenDisabled != null) ? groundEntityWhenDisabled.GetFirstScriptOfType<MissionObject>().Id : MissionObjectId.Invalid, this._ownerGameEntity.GetFrame()));
									GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
								}
							}
						}
						if (!this.PhysicsStopped && this._disablePhysicsTimer.ElapsedTime() > 0.2f)
						{
							Vec3 v;
							Vec3 v2;
							this._ownerGameEntity.GetPhysicsMinMax(true, out v, out v2, true);
							MatrixFrame globalFrame3 = this._ownerGameEntity.GetGlobalFrame();
							MatrixFrame previousGlobalFrame = this._ownerGameEntity.GetPreviousGlobalFrame();
							Vec3 v3 = globalFrame3.TransformToParent(v);
							Vec3 v4 = previousGlobalFrame.TransformToParent(v);
							Vec3 v5 = globalFrame3.TransformToParent(v2);
							Vec3 v6 = previousGlobalFrame.TransformToParent(v2);
							Vec3 vec = Vec3.Vec3Min(v3, v5);
							Vec3 vec2 = Vec3.Vec3Min(v4, v6);
							float waterLevelAtPositionMT;
							using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
							{
								waterLevelAtPositionMT = Mission.Current.GetWaterLevelAtPositionMT(vec.AsVec2);
							}
							bool flag3 = vec.z < waterLevelAtPositionMT;
							if (vec2.z >= waterLevelAtPositionMT && flag3)
							{
								Vec3 linearVelocityMT;
								using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
								{
									linearVelocityMT = this._ownerGameEntity.GetLinearVelocityMT();
								}
								float num = this._ownerGameEntity.Mass * linearVelocityMT.Length;
								if (num > 0f)
								{
									num *= 0.0625f;
									num = MathF.Min(num, 1f);
									Vec3 position = globalPosition;
									position.z = waterLevelAtPositionMT;
									SoundEventParameter soundEventParameter = new SoundEventParameter("Size", num);
									Mission.Current.MakeSound(ItemPhysicsSoundContainer.SoundCodePhysicsWater, position, false, true, -1, -1, ref soundEventParameter);
									return;
								}
							}
						}
					}
					else
					{
						this.PhysicsStopped = true;
					}
				}
			}
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000C1E2C File Offset: 0x000C002C
		private GameEntity TryFindProperGroundEntityForSpawnedEntity()
		{
			Vec3 vec;
			Vec3 vec2;
			this._ownerGameEntity.GetPhysicsMinMax(true, out vec, out vec2, false);
			float num = vec2.z - vec.z;
			vec.z = vec2.z - 0.001f;
			Vec3 vec3 = (vec2 + vec) * 0.5f;
			float num2;
			Vec3 vec4;
			this._ownerGameEntity.Scene.RayCastForClosestEntityOrTerrainMT(vec3, vec3 - new Vec3(0f, 0f, num + 0.05f, -1f), out num2, out vec4, out this._groundEntityWhenDisabled, 0.01f, BodyFlags.CommonCollisionExcludeFlags);
			GameEntity groundEntityWhenDisabled = this._groundEntityWhenDisabled;
			GameEntity groundEntityWhenDisabled2;
			if (groundEntityWhenDisabled == null)
			{
				groundEntityWhenDisabled2 = null;
			}
			else
			{
				MissionObject firstScriptOfTypeInFamily = groundEntityWhenDisabled.GetFirstScriptOfTypeInFamily<MissionObject>();
				groundEntityWhenDisabled2 = ((firstScriptOfTypeInFamily != null) ? firstScriptOfTypeInFamily.GameEntity : null);
			}
			this._groundEntityWhenDisabled = groundEntityWhenDisabled2;
			if (MathF.Abs(vec4.z - vec3.z) <= num + 0.05f)
			{
				if (this._groundEntityWhenDisabled != null)
				{
					return this._groundEntityWhenDisabled;
				}
				return null;
			}
			else
			{
				this._ownerGameEntity.Scene.BoxCast(vec, vec2, false, Vec3.Zero, -Vec3.Up, num + 0.05f, out num2, out vec4, out this._groundEntityWhenDisabled, BodyFlags.CommonCollisionExcludeFlags);
				GameEntity groundEntityWhenDisabled3 = this._groundEntityWhenDisabled;
				GameEntity groundEntityWhenDisabled4;
				if (groundEntityWhenDisabled3 == null)
				{
					groundEntityWhenDisabled4 = null;
				}
				else
				{
					MissionObject firstScriptOfTypeInFamily2 = groundEntityWhenDisabled3.GetFirstScriptOfTypeInFamily<MissionObject>();
					groundEntityWhenDisabled4 = ((firstScriptOfTypeInFamily2 != null) ? firstScriptOfTypeInFamily2.GameEntity : null);
				}
				this._groundEntityWhenDisabled = groundEntityWhenDisabled4;
				if (this._groundEntityWhenDisabled != null && MathF.Abs(vec4.z - vec3.z) <= num + 0.05f)
				{
					return this._groundEntityWhenDisabled;
				}
				return null;
			}
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000C1FAA File Offset: 0x000C01AA
		protected internal override void OnTickOccasionally(float currentFrameDeltaTime)
		{
			this.OnTickParallel2(currentFrameDeltaTime);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000C1FB4 File Offset: 0x000C01B4
		private void ClampEntityPositionForStoppingIfNeeded()
		{
			GameEntity gameEntity = base.GameEntity;
			float minimumValue = CompressionBasic.PositionCompressionInfo.GetMinimumValue();
			float maximumValue = CompressionBasic.PositionCompressionInfo.GetMaximumValue();
			Vec3 localPosition = gameEntity.GetFrame().origin;
			bool flag;
			localPosition = localPosition.ClampedCopy(minimumValue, maximumValue, out flag);
			if (flag)
			{
				gameEntity.SetLocalPosition(localPosition);
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000C2001 File Offset: 0x000C0201
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			if (base.CreatedAtRuntime)
			{
				Mission.Current.AddSpawnedItemEntityCreatedAtRuntime(this);
			}
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000C201C File Offset: 0x000C021C
		protected override void OnRemoved(int removeReason)
		{
			if (base.HasUser && !GameNetwork.IsClientOrReplay)
			{
				base.UserAgent.StopUsingGameObject(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
			base.OnRemoved(removeReason);
			base.InvalidateWeakPointersIfValid();
			this._ownerGameEntity = null;
			Agent userAgent = base.UserAgent;
			if (userAgent != null)
			{
				userAgent.OnItemRemovedFromScene();
			}
			Agent movingAgent = this.MovingAgent;
			if (movingAgent == null)
			{
				return;
			}
			movingAgent.OnItemRemovedFromScene();
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000C207A File Offset: 0x000C027A
		public void AttachWeaponToWeapon(MissionWeapon attachedWeapon, ref MatrixFrame attachLocalFrame)
		{
			this._weapon.AttachWeapon(attachedWeapon, ref attachLocalFrame);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000C208C File Offset: 0x000C028C
		public bool IsReadyToBeDeleted()
		{
			return (!base.HasUser && this._readyToBeDeleted) || (this._groundEntityWhenDisabled != null && !this._groundEntityWhenDisabled.HasScene()) || (this._groundEntityWhenDisabled != null && !this._groundEntityWhenDisabled.IsVisibleIncludeParents() && (!this._groundEntityWhenDisabled.HasBody() || this._groundEntityWhenDisabled.BodyFlag.HasAnyFlag(BodyFlags.Disabled)));
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000C2104 File Offset: 0x000C0304
		public override void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			base.OnUseStopped(userAgent, isSuccessful, preferenceIndex);
			if (isSuccessful)
			{
				if (this._clientSyncData != null)
				{
					this._clientSyncData = null;
					base.GameEntity.SetAlpha(1f);
				}
				bool flag;
				userAgent.OnItemPickup(this, (EquipmentIndex)preferenceIndex, out flag);
				if (flag)
				{
					this._readyToBeDeleted = true;
					this.PhysicsStopped = true;
					base.IsDeactivated = true;
				}
			}
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000C2160 File Offset: 0x000C0360
		public override void OnUse(Agent userAgent)
		{
			base.OnUse(userAgent);
			if (!GameNetwork.IsClientOrReplay)
			{
				MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
				float num = globalFrame.origin.z;
				num = Math.Max(num, globalFrame.origin.z + globalFrame.rotation.u.z * (float)this._weapon.CurrentUsageItem.WeaponLength * 0.0075f);
				float eyeGlobalHeight = userAgent.GetEyeGlobalHeight();
				bool isLeftStance = userAgent.GetIsLeftStance();
				ItemObject.ItemTypeEnum itemType = this._weapon.Item.ItemType;
				if (userAgent.HasMount)
				{
					this._usedChannelIndex = 1;
					MatrixFrame frame = userAgent.Frame;
					bool flag = Vec2.DotProduct(frame.rotation.f.AsVec2.LeftVec(), (base.GameEntity.GetGlobalFrame().origin - frame.origin).AsVec2) > 0f;
					if (num < eyeGlobalHeight * 0.7f + userAgent.Position.z)
					{
						if (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.AttachmentMask) || itemType == ItemObject.ItemTypeEnum.Bow || itemType == ItemObject.ItemTypeEnum.Shield)
						{
							this._progressActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_down_horseback_left_begin : SpawnedItemEntity.act_pickup_from_right_down_horseback_left_begin);
							this._successActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_down_horseback_left_end : SpawnedItemEntity.act_pickup_from_right_down_horseback_left_end);
						}
						else
						{
							this._progressActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_down_horseback_begin : SpawnedItemEntity.act_pickup_from_right_down_horseback_begin);
							this._successActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_down_horseback_end : SpawnedItemEntity.act_pickup_from_right_down_horseback_end);
						}
					}
					else if (num < eyeGlobalHeight * 1.1f + userAgent.Position.z)
					{
						if (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.AttachmentMask) || itemType == ItemObject.ItemTypeEnum.Bow || itemType == ItemObject.ItemTypeEnum.Shield)
						{
							this._progressActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_middle_horseback_left_begin : SpawnedItemEntity.act_pickup_from_right_middle_horseback_left_begin);
							this._successActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_middle_horseback_left_end : SpawnedItemEntity.act_pickup_from_right_middle_horseback_left_end);
						}
						else
						{
							this._progressActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_middle_horseback_begin : SpawnedItemEntity.act_pickup_from_right_middle_horseback_begin);
							this._successActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_middle_horseback_end : SpawnedItemEntity.act_pickup_from_right_middle_horseback_end);
						}
					}
					else if (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.AttachmentMask) || itemType == ItemObject.ItemTypeEnum.Bow || itemType == ItemObject.ItemTypeEnum.Shield)
					{
						this._progressActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_up_horseback_left_begin : SpawnedItemEntity.act_pickup_from_right_up_horseback_left_begin);
						this._successActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_up_horseback_left_end : SpawnedItemEntity.act_pickup_from_right_up_horseback_left_end);
					}
					else
					{
						this._progressActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_up_horseback_begin : SpawnedItemEntity.act_pickup_from_right_up_horseback_begin);
						this._successActionIndex = (flag ? SpawnedItemEntity.act_pickup_from_left_up_horseback_end : SpawnedItemEntity.act_pickup_from_right_up_horseback_end);
					}
				}
				else if (this._weapon.CurrentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.RangedWeapon | WeaponFlags.NotUsableWithOneHand | WeaponFlags.Consumable))
				{
					this._usedChannelIndex = 0;
					this._progressActionIndex = SpawnedItemEntity.act_pickup_boulder_begin;
					this._successActionIndex = SpawnedItemEntity.act_pickup_boulder_end;
				}
				else if (num < eyeGlobalHeight * 0.4f + userAgent.Position.z)
				{
					this._usedChannelIndex = 0;
					if (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.AttachmentMask) || itemType == ItemObject.ItemTypeEnum.Bow || itemType == ItemObject.ItemTypeEnum.Shield)
					{
						this._progressActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_down_left_begin_left_stance : SpawnedItemEntity.act_pickup_down_left_begin);
						this._successActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_down_left_end_left_stance : SpawnedItemEntity.act_pickup_down_left_end);
					}
					else
					{
						this._progressActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_down_begin_left_stance : SpawnedItemEntity.act_pickup_down_begin);
						this._successActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_down_end_left_stance : SpawnedItemEntity.act_pickup_down_end);
					}
				}
				else if (num < eyeGlobalHeight * 1.1f + userAgent.Position.z)
				{
					this._usedChannelIndex = 1;
					if (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.AttachmentMask) || itemType == ItemObject.ItemTypeEnum.Bow || itemType == ItemObject.ItemTypeEnum.Shield)
					{
						this._progressActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_middle_left_begin_left_stance : SpawnedItemEntity.act_pickup_middle_left_begin);
						this._successActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_middle_left_end_left_stance : SpawnedItemEntity.act_pickup_middle_left_end);
					}
					else
					{
						this._progressActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_middle_begin_left_stance : SpawnedItemEntity.act_pickup_middle_begin);
						this._successActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_middle_end_left_stance : SpawnedItemEntity.act_pickup_middle_end);
					}
				}
				else
				{
					this._usedChannelIndex = 1;
					if (this._weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.AttachmentMask) || itemType == ItemObject.ItemTypeEnum.Bow || itemType == ItemObject.ItemTypeEnum.Shield)
					{
						this._progressActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_up_left_begin_left_stance : SpawnedItemEntity.act_pickup_up_left_begin);
						this._successActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_up_left_end_left_stance : SpawnedItemEntity.act_pickup_up_left_end);
					}
					else
					{
						this._progressActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_up_begin_left_stance : SpawnedItemEntity.act_pickup_up_begin);
						this._successActionIndex = (isLeftStance ? SpawnedItemEntity.act_pickup_up_end_left_stance : SpawnedItemEntity.act_pickup_up_end);
					}
				}
				userAgent.SetActionChannel(this._usedChannelIndex, this._progressActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
			}
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000C2664 File Offset: 0x000C0864
		public override bool IsDisabledForAgent(Agent agent)
		{
			return (this._weapon.IsAnyConsumable() && this._weapon.Amount == 0) || (this._weapon.IsBanner() && !MissionGameModels.Current.BattleBannerBearersModel.IsInteractableFormationBanner(this, agent));
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000C26B0 File Offset: 0x000C08B0
		protected internal override void OnPhysicsCollision(ref PhysicsContact contact)
		{
			if (!GameNetwork.IsDedicatedServer && contact.NumberOfContactPairs > 0)
			{
				PhysicsContactInfo physicsContactInfo = default(PhysicsContactInfo);
				bool flag = false;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < contact.NumberOfContactPairs; i++)
				{
					for (int j = 0; j < contact[i].NumberOfContacts; j++)
					{
						if (!flag || contact[i][j].Impulse.LengthSquared > physicsContactInfo.Impulse.LengthSquared)
						{
							physicsContactInfo = contact[i][j];
							flag = true;
						}
					}
					switch (contact[i].ContactEventType)
					{
					case PhysicsEventType.CollisionStart:
						num++;
						break;
					case PhysicsEventType.CollisionStay:
						num2++;
						break;
					case PhysicsEventType.CollisionEnd:
						num3++;
						break;
					default:
						Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Usables\\SpawnedItemEntity.cs", "OnPhysicsCollision", 780);
						break;
					}
				}
				if (num2 > 0)
				{
					this.PlayPhysicsRollSound(physicsContactInfo.Impulse, physicsContactInfo.Position, physicsContactInfo.PhysicsMaterial1);
					return;
				}
				if (num > 0)
				{
					this.PlayPhysicsCollisionSound(physicsContactInfo.Impulse, physicsContactInfo.PhysicsMaterial1, physicsContactInfo.Position);
				}
			}
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000C27F0 File Offset: 0x000C09F0
		private void PlayPhysicsCollisionSound(Vec3 impulse, PhysicsMaterial collidedMat, Vec3 collisionPoint)
		{
			float num = this._deletionTimer.ElapsedTime();
			if (impulse.LengthSquared > 0.0025000002f && this._lastSoundPlayTime + 0.333f < num)
			{
				this._lastSoundPlayTime = num;
				WeaponClass weaponClass = this._weapon.CurrentUsageItem.WeaponClass;
				float num2 = impulse.Length;
				bool flag = false;
				int num3;
				int num4;
				int num5;
				switch (weaponClass)
				{
				case WeaponClass.Dagger:
				case WeaponClass.ThrowingAxe:
				case WeaponClass.ThrowingKnife:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeStone;
					goto IL_1A0;
				case WeaponClass.OneHandedSword:
				case WeaponClass.OneHandedAxe:
				case WeaponClass.Mace:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeStone;
					goto IL_1A0;
				case WeaponClass.TwoHandedSword:
				case WeaponClass.TwoHandedAxe:
				case WeaponClass.TwoHandedMace:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeStone;
					goto IL_1A0;
				case WeaponClass.OneHandedPolearm:
				case WeaponClass.TwoHandedPolearm:
				case WeaponClass.LowGripPolearm:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeStone;
					goto IL_1A0;
				case WeaponClass.Arrow:
				case WeaponClass.Bolt:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeStone;
					goto IL_1A0;
				case WeaponClass.Bow:
				case WeaponClass.Crossbow:
				case WeaponClass.Javelin:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeStone;
					goto IL_1A0;
				case WeaponClass.Stone:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeStone;
					goto IL_1A0;
				case WeaponClass.Boulder:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsBoulderDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsBoulderWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsBoulderStone;
					flag = true;
					goto IL_1A0;
				case WeaponClass.SmallShield:
				case WeaponClass.LargeShield:
					num3 = ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeDefault;
					num4 = ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeWood;
					num5 = ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeStone;
					goto IL_1A0;
				}
				num3 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeDefault;
				num4 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeWood;
				num5 = ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeStone;
				IL_1A0:
				if (!flag)
				{
					num2 *= 0.16666667f;
					num2 = MBMath.ClampFloat(num2, 0f, 1f);
				}
				else
				{
					num2 = (num2 - 7f) * 0.030303031f * 0.1f + 0.9f;
					num2 = MBMath.ClampFloat(num2, 0.9f, 1f);
				}
				string name = collidedMat.Name;
				int soundIndex = num3;
				if (name.Contains("wood"))
				{
					soundIndex = num4;
				}
				else if (name.Contains("stone"))
				{
					soundIndex = num5;
				}
				SoundEventParameter soundEventParameter = new SoundEventParameter("Force", num2);
				Mission.Current.MakeSound(soundIndex, collisionPoint, true, false, -1, -1, ref soundEventParameter);
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000C2A40 File Offset: 0x000C0C40
		private void PlayPhysicsRollSound(Vec3 impulse, Vec3 collisionPoint, PhysicsMaterial collidedMat)
		{
			WeaponComponentData currentUsageItem = this._weapon.CurrentUsageItem;
			if (currentUsageItem.WeaponClass == WeaponClass.Boulder && currentUsageItem.WeaponFlags.HasAllFlags(WeaponFlags.RangedWeapon | WeaponFlags.NotUsableWithOneHand | WeaponFlags.Consumable))
			{
				float num = this._deletionTimer.ElapsedTime();
				if (impulse.LengthSquared > 0.0001f && this._lastSoundPlayTime + 0.333f < num)
				{
					if (this._rollingSoundEvent == null || !this._rollingSoundEvent.IsValid)
					{
						this._lastSoundPlayTime = num;
						int soundCodeId = ItemPhysicsSoundContainer.SoundCodePhysicsBoulderDefault;
						string name = collidedMat.Name;
						if (name.Contains("stone"))
						{
							soundCodeId = ItemPhysicsSoundContainer.SoundCodePhysicsBoulderStone;
						}
						else if (name.Contains("wood"))
						{
							soundCodeId = ItemPhysicsSoundContainer.SoundCodePhysicsBoulderWood;
						}
						this._rollingSoundEvent = SoundEvent.CreateEvent(soundCodeId, Mission.Current.Scene);
						this._rollingSoundEvent.PlayInPosition(collisionPoint);
					}
					float value = impulse.Length * 0.033333335f;
					value = MBMath.ClampFloat(value, 0f, 1f);
					this._rollingSoundEvent.SetParameter("Force", value);
					this._rollingSoundEvent.SetPosition(collisionPoint);
				}
			}
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000C2B61 File Offset: 0x000C0D61
		public bool IsStuckMissile()
		{
			return this.SpawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.AsMissile);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000C2B6F File Offset: 0x000C0D6F
		public bool IsQuiverAndNotEmpty()
		{
			return this._weapon.Item.PrimaryWeapon.IsConsumable && this._weapon.Amount > 0;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000C2B98 File Offset: 0x000C0D98
		public bool IsBanner()
		{
			return this._weapon.IsBanner();
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000C2BA5 File Offset: 0x000C0DA5
		public override TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			if (!base.IsDeactivated && this._weapon.IsAnyConsumable() && this._weapon.Amount == 0)
			{
				return GameTexts.FindText("str_ui_empty_quiver", null);
			}
			return base.GetInfoTextForBeingNotInteractable(userAgent);
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000C2BDC File Offset: 0x000C0DDC
		public void StopPhysicsAndSetFrameForClient(MatrixFrame frame, GameEntity parent)
		{
			if (parent != null)
			{
				frame = parent.GetGlobalFrame().TransformToParent(frame);
			}
			frame.rotation.Orthonormalize();
			this._clientSyncData = new SpawnedItemEntity.ClientSyncData();
			this._clientSyncData.Frame = frame;
			this._clientSyncData.Timer = new Timer(Mission.Current.CurrentTime, 0.5f, false);
			this._clientSyncData.Parent = parent;
			if (!this.PhysicsStopped)
			{
				this.PhysicsStopped = true;
				GameEntity gameEntity = base.GameEntity;
				if (!this._weapon.IsEmpty && !gameEntity.BodyFlag.HasAnyFlag(BodyFlags.Disabled))
				{
					gameEntity.DisableDynamicBodySimulation();
					return;
				}
				gameEntity.RemovePhysics(false);
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000C2C91 File Offset: 0x000C0E91
		public void ConsumeWeaponAmount(short consumedAmount)
		{
			this._weapon.Consume(consumedAmount);
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000C2CA0 File Offset: 0x000C0EA0
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return string.Empty;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000C2CA7 File Offset: 0x000C0EA7
		public void RequestDeletionOnNextTick()
		{
			this._deletionTimer = new Timer(Mission.Current.CurrentTime, -1f, true);
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000C2CC4 File Offset: 0x000C0EC4
		public SpawnedItemEntity() : base(false)
		{
		}

		// Token: 0x04001400 RID: 5120
		private static readonly ActionIndexCache act_pickup_down_begin = ActionIndexCache.Create("act_pickup_down_begin");

		// Token: 0x04001401 RID: 5121
		private static readonly ActionIndexCache act_pickup_down_end = ActionIndexCache.Create("act_pickup_down_end");

		// Token: 0x04001402 RID: 5122
		private static readonly ActionIndexCache act_pickup_down_begin_left_stance = ActionIndexCache.Create("act_pickup_down_begin_left_stance");

		// Token: 0x04001403 RID: 5123
		private static readonly ActionIndexCache act_pickup_down_end_left_stance = ActionIndexCache.Create("act_pickup_down_end_left_stance");

		// Token: 0x04001404 RID: 5124
		private static readonly ActionIndexCache act_pickup_down_left_begin = ActionIndexCache.Create("act_pickup_down_left_begin");

		// Token: 0x04001405 RID: 5125
		private static readonly ActionIndexCache act_pickup_down_left_end = ActionIndexCache.Create("act_pickup_down_left_end");

		// Token: 0x04001406 RID: 5126
		private static readonly ActionIndexCache act_pickup_down_left_begin_left_stance = ActionIndexCache.Create("act_pickup_down_left_begin_left_stance");

		// Token: 0x04001407 RID: 5127
		private static readonly ActionIndexCache act_pickup_down_left_end_left_stance = ActionIndexCache.Create("act_pickup_down_left_end_left_stance");

		// Token: 0x04001408 RID: 5128
		private static readonly ActionIndexCache act_pickup_middle_begin = ActionIndexCache.Create("act_pickup_middle_begin");

		// Token: 0x04001409 RID: 5129
		private static readonly ActionIndexCache act_pickup_middle_end = ActionIndexCache.Create("act_pickup_middle_end");

		// Token: 0x0400140A RID: 5130
		private static readonly ActionIndexCache act_pickup_middle_begin_left_stance = ActionIndexCache.Create("act_pickup_middle_begin_left_stance");

		// Token: 0x0400140B RID: 5131
		private static readonly ActionIndexCache act_pickup_middle_end_left_stance = ActionIndexCache.Create("act_pickup_middle_end_left_stance");

		// Token: 0x0400140C RID: 5132
		private static readonly ActionIndexCache act_pickup_middle_left_begin = ActionIndexCache.Create("act_pickup_middle_left_begin");

		// Token: 0x0400140D RID: 5133
		private static readonly ActionIndexCache act_pickup_middle_left_end = ActionIndexCache.Create("act_pickup_middle_left_end");

		// Token: 0x0400140E RID: 5134
		private static readonly ActionIndexCache act_pickup_middle_left_begin_left_stance = ActionIndexCache.Create("act_pickup_middle_left_begin_left_stance");

		// Token: 0x0400140F RID: 5135
		private static readonly ActionIndexCache act_pickup_middle_left_end_left_stance = ActionIndexCache.Create("act_pickup_middle_left_end_left_stance");

		// Token: 0x04001410 RID: 5136
		private static readonly ActionIndexCache act_pickup_up_begin = ActionIndexCache.Create("act_pickup_up_begin");

		// Token: 0x04001411 RID: 5137
		private static readonly ActionIndexCache act_pickup_up_end = ActionIndexCache.Create("act_pickup_up_end");

		// Token: 0x04001412 RID: 5138
		private static readonly ActionIndexCache act_pickup_up_begin_left_stance = ActionIndexCache.Create("act_pickup_up_begin_left_stance");

		// Token: 0x04001413 RID: 5139
		private static readonly ActionIndexCache act_pickup_up_end_left_stance = ActionIndexCache.Create("act_pickup_up_end_left_stance");

		// Token: 0x04001414 RID: 5140
		private static readonly ActionIndexCache act_pickup_up_left_begin = ActionIndexCache.Create("act_pickup_up_left_begin");

		// Token: 0x04001415 RID: 5141
		private static readonly ActionIndexCache act_pickup_up_left_end = ActionIndexCache.Create("act_pickup_up_left_end");

		// Token: 0x04001416 RID: 5142
		private static readonly ActionIndexCache act_pickup_up_left_begin_left_stance = ActionIndexCache.Create("act_pickup_up_left_begin_left_stance");

		// Token: 0x04001417 RID: 5143
		private static readonly ActionIndexCache act_pickup_up_left_end_left_stance = ActionIndexCache.Create("act_pickup_up_left_end_left_stance");

		// Token: 0x04001418 RID: 5144
		private static readonly ActionIndexCache act_pickup_from_right_down_horseback_begin = ActionIndexCache.Create("act_pickup_from_right_down_horseback_begin");

		// Token: 0x04001419 RID: 5145
		private static readonly ActionIndexCache act_pickup_from_right_down_horseback_end = ActionIndexCache.Create("act_pickup_from_right_down_horseback_end");

		// Token: 0x0400141A RID: 5146
		private static readonly ActionIndexCache act_pickup_from_right_down_horseback_left_begin = ActionIndexCache.Create("act_pickup_from_right_down_horseback_left_begin");

		// Token: 0x0400141B RID: 5147
		private static readonly ActionIndexCache act_pickup_from_right_down_horseback_left_end = ActionIndexCache.Create("act_pickup_from_right_down_horseback_left_end");

		// Token: 0x0400141C RID: 5148
		private static readonly ActionIndexCache act_pickup_from_right_middle_horseback_begin = ActionIndexCache.Create("act_pickup_from_right_middle_horseback_begin");

		// Token: 0x0400141D RID: 5149
		private static readonly ActionIndexCache act_pickup_from_right_middle_horseback_end = ActionIndexCache.Create("act_pickup_from_right_middle_horseback_end");

		// Token: 0x0400141E RID: 5150
		private static readonly ActionIndexCache act_pickup_from_right_middle_horseback_left_begin = ActionIndexCache.Create("act_pickup_from_right_middle_horseback_left_begin");

		// Token: 0x0400141F RID: 5151
		private static readonly ActionIndexCache act_pickup_from_right_middle_horseback_left_end = ActionIndexCache.Create("act_pickup_from_right_middle_horseback_left_end");

		// Token: 0x04001420 RID: 5152
		private static readonly ActionIndexCache act_pickup_from_right_up_horseback_begin = ActionIndexCache.Create("act_pickup_from_right_up_horseback_begin");

		// Token: 0x04001421 RID: 5153
		private static readonly ActionIndexCache act_pickup_from_right_up_horseback_end = ActionIndexCache.Create("act_pickup_from_right_up_horseback_end");

		// Token: 0x04001422 RID: 5154
		private static readonly ActionIndexCache act_pickup_from_right_up_horseback_left_begin = ActionIndexCache.Create("act_pickup_from_right_up_horseback_left_begin");

		// Token: 0x04001423 RID: 5155
		private static readonly ActionIndexCache act_pickup_from_right_up_horseback_left_end = ActionIndexCache.Create("act_pickup_from_right_up_horseback_left_end");

		// Token: 0x04001424 RID: 5156
		private static readonly ActionIndexCache act_pickup_from_left_down_horseback_begin = ActionIndexCache.Create("act_pickup_from_left_down_horseback_begin");

		// Token: 0x04001425 RID: 5157
		private static readonly ActionIndexCache act_pickup_from_left_down_horseback_end = ActionIndexCache.Create("act_pickup_from_left_down_horseback_end");

		// Token: 0x04001426 RID: 5158
		private static readonly ActionIndexCache act_pickup_from_left_down_horseback_left_begin = ActionIndexCache.Create("act_pickup_from_left_down_horseback_left_begin");

		// Token: 0x04001427 RID: 5159
		private static readonly ActionIndexCache act_pickup_from_left_down_horseback_left_end = ActionIndexCache.Create("act_pickup_from_left_down_horseback_left_end");

		// Token: 0x04001428 RID: 5160
		private static readonly ActionIndexCache act_pickup_from_left_middle_horseback_begin = ActionIndexCache.Create("act_pickup_from_left_middle_horseback_begin");

		// Token: 0x04001429 RID: 5161
		private static readonly ActionIndexCache act_pickup_from_left_middle_horseback_end = ActionIndexCache.Create("act_pickup_from_left_middle_horseback_end");

		// Token: 0x0400142A RID: 5162
		private static readonly ActionIndexCache act_pickup_from_left_middle_horseback_left_begin = ActionIndexCache.Create("act_pickup_from_left_middle_horseback_left_begin");

		// Token: 0x0400142B RID: 5163
		private static readonly ActionIndexCache act_pickup_from_left_middle_horseback_left_end = ActionIndexCache.Create("act_pickup_from_left_middle_horseback_left_end");

		// Token: 0x0400142C RID: 5164
		private static readonly ActionIndexCache act_pickup_from_left_up_horseback_begin = ActionIndexCache.Create("act_pickup_from_left_up_horseback_begin");

		// Token: 0x0400142D RID: 5165
		private static readonly ActionIndexCache act_pickup_from_left_up_horseback_end = ActionIndexCache.Create("act_pickup_from_left_up_horseback_end");

		// Token: 0x0400142E RID: 5166
		private static readonly ActionIndexCache act_pickup_from_left_up_horseback_left_begin = ActionIndexCache.Create("act_pickup_from_left_up_horseback_left_begin");

		// Token: 0x0400142F RID: 5167
		private static readonly ActionIndexCache act_pickup_from_left_up_horseback_left_end = ActionIndexCache.Create("act_pickup_from_left_up_horseback_left_end");

		// Token: 0x04001430 RID: 5168
		private static readonly ActionIndexCache act_pickup_boulder_begin = ActionIndexCache.Create("act_pickup_boulder_begin");

		// Token: 0x04001431 RID: 5169
		private static readonly ActionIndexCache act_pickup_boulder_end = ActionIndexCache.Create("act_pickup_boulder_end");

		// Token: 0x04001432 RID: 5170
		private MissionWeapon _weapon;

		// Token: 0x04001433 RID: 5171
		private bool _hasLifeTime;

		// Token: 0x04001434 RID: 5172
		public string WeaponName = "";

		// Token: 0x04001435 RID: 5173
		private const float LongLifeTime = 180f;

		// Token: 0x04001436 RID: 5174
		private const float DisablePhysicsTime = 10f;

		// Token: 0x04001437 RID: 5175
		private const float QuickFadeoutLifeTime = 5f;

		// Token: 0x04001438 RID: 5176
		private const float TotalFadeOutInDuration = 0.5f;

		// Token: 0x04001439 RID: 5177
		private const float PreventStationaryCheckTime = 1f;

		// Token: 0x0400143A RID: 5178
		private Timer _disablePhysicsTimer;

		// Token: 0x0400143B RID: 5179
		private bool _physicsStopped;

		// Token: 0x0400143C RID: 5180
		private bool _readyToBeDeleted;

		// Token: 0x0400143D RID: 5181
		private Timer _deletionTimer;

		// Token: 0x0400143E RID: 5182
		private int _usedChannelIndex;

		// Token: 0x0400143F RID: 5183
		private ActionIndexCache _progressActionIndex;

		// Token: 0x04001440 RID: 5184
		private ActionIndexCache _successActionIndex;

		// Token: 0x04001441 RID: 5185
		private float _lastSoundPlayTime;

		// Token: 0x04001442 RID: 5186
		private const float MinSoundDelay = 0.333f;

		// Token: 0x04001443 RID: 5187
		private SoundEvent _rollingSoundEvent;

		// Token: 0x04001444 RID: 5188
		private SpawnedItemEntity.ClientSyncData _clientSyncData;

		// Token: 0x04001445 RID: 5189
		private GameEntity _ownerGameEntity;

		// Token: 0x04001446 RID: 5190
		private Vec3 _fakeSimulationVelocity;

		// Token: 0x04001447 RID: 5191
		private GameEntity _groundEntityWhenDisabled;

		// Token: 0x02000617 RID: 1559
		private class ClientSyncData
		{
			// Token: 0x04001FA8 RID: 8104
			public MatrixFrame Frame;

			// Token: 0x04001FA9 RID: 8105
			public GameEntity Parent;

			// Token: 0x04001FAA RID: 8106
			public Timer Timer;
		}
	}
}
