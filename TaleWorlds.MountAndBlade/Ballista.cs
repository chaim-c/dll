using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200032E RID: 814
	public class Ballista : RangedSiegeWeapon, ISpawnable
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000A7FED File Offset: 0x000A61ED
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x000A7FF5 File Offset: 0x000A61F5
		private protected SynchedMissionObject ballistaBody { protected get; private set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000A7FFE File Offset: 0x000A61FE
		// (set) Token: 0x06002B6C RID: 11116 RVA: 0x000A8006 File Offset: 0x000A6206
		private protected SynchedMissionObject ballistaNavel { protected get; private set; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000A800F File Offset: 0x000A620F
		public override float DirectionRestriction
		{
			get
			{
				return this.HorizontalDirectionRestriction;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000A8017 File Offset: 0x000A6217
		protected override float ShootingSpeed
		{
			get
			{
				return this.BallistaShootingSpeed;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000A801F File Offset: 0x000A621F
		public override Vec3 CanShootAtPointCheckingOffset
		{
			get
			{
				return new Vec3(0f, 0f, 0.5f, -1f);
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000A803C File Offset: 0x000A623C
		protected override void RegisterAnimationParameters()
		{
			this.SkeletonOwnerObjects = new SynchedMissionObject[1];
			this.Skeletons = new Skeleton[1];
			this.SkeletonOwnerObjects[0] = this.ballistaBody;
			this.Skeletons[0] = this.ballistaBody.GameEntity.Skeleton;
			base.SkeletonName = "ballista_skeleton";
			base.FireAnimation = "ballista_fire";
			base.FireAnimationIndex = MBAnimation.GetAnimationIndexWithName("ballista_fire");
			base.SetUpAnimation = "ballista_set_up";
			base.SetUpAnimationIndex = MBAnimation.GetAnimationIndexWithName("ballista_set_up");
			this._idleAnimationActionIndex = ActionIndexCache.Create(this.IdleActionName);
			this._reloadAnimationActionIndex = ActionIndexCache.Create(this.ReloadActionName);
			this._placeAmmoStartAnimationActionIndex = ActionIndexCache.Create(this.PlaceAmmoStartActionName);
			this._placeAmmoEndAnimationActionIndex = ActionIndexCache.Create(this.PlaceAmmoEndActionName);
			this._pickUpAmmoStartAnimationActionIndex = ActionIndexCache.Create(this.PickUpAmmoStartActionName);
			this._pickUpAmmoEndAnimationActionIndex = ActionIndexCache.Create(this.PickUpAmmoEndActionName);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000A812E File Offset: 0x000A632E
		public override SiegeEngineType GetSiegeEngineType()
		{
			return DefaultSiegeEngineTypes.Ballista;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000A8138 File Offset: 0x000A6338
		protected internal override void OnInit()
		{
			this.ballistaBody = base.GameEntity.CollectObjectsWithTag(this.BodyTag)[0];
			this.ballistaNavel = base.GameEntity.CollectObjectsWithTag(this.NavelTag)[0];
			this.RotationObject = this;
			base.OnInit();
			this.UsesMouseForAiming = true;
			this.GetSoundEventIndices();
			this._ballistaNavelInitialFrame = this.ballistaNavel.GameEntity.GetFrame();
			MatrixFrame globalFrame = this.ballistaBody.GameEntity.GetGlobalFrame();
			this._ballistaBodyInitialLocalFrame = this.ballistaBody.GameEntity.GetFrame();
			MatrixFrame globalFrame2 = base.PilotStandingPoint.GameEntity.GetGlobalFrame();
			this._pilotInitialLocalFrame = base.PilotStandingPoint.GameEntity.GetFrame();
			this._pilotInitialLocalIKFrame = globalFrame2.TransformToLocal(globalFrame);
			this._missileInitialLocalFrame = base.Projectile.GameEntity.GetFrame();
			base.PilotStandingPoint.AddComponent(new ClearHandInverseKinematicsOnStopUsageComponent());
			this.MissileStartingPositionEntityForSimulation = base.Projectile.GameEntity.Parent.GetChildren().FirstOrDefault((GameEntity x) => x.Name == "projectile_leaving_position");
			this.EnemyRangeToStopUsing = 7f;
			this.AttackClickWillReload = true;
			this.WeaponNeedsClickToReload = true;
			base.SetScriptComponentToTick(this.GetTickRequirement());
			Vec3 shootingDirection = this.ShootingDirection;
			Vec3 v = new Vec3(0f, shootingDirection.AsVec2.Length, shootingDirection.z, -1f);
			this._verticalOffsetAngle = Vec3.AngleBetweenTwoVectors(v, Vec3.Forward);
			this.ApplyAimChange();
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000A82DA File Offset: 0x000A64DA
		protected override bool CanRotate()
		{
			return base.State != RangedSiegeWeapon.WeaponState.Shooting;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000A82E8 File Offset: 0x000A64E8
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new BallistaAI(this);
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000A82F0 File Offset: 0x000A64F0
		protected override void OnRangedSiegeWeaponStateChange()
		{
			base.OnRangedSiegeWeaponStateChange();
			RangedSiegeWeapon.WeaponState state = base.State;
			if (state != RangedSiegeWeapon.WeaponState.Idle)
			{
				if (state == RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving)
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
			else if (base.AmmoCount > 0)
			{
				if (!GameNetwork.IsClientOrReplay)
				{
					this.ConsumeAmmo();
					return;
				}
				this.SetAmmo(base.AmmoCount - 1);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000A835D File Offset: 0x000A655D
		protected override float MaximumBallisticError
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000A8364 File Offset: 0x000A6564
		protected override float HorizontalAimSensitivity
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000A836B File Offset: 0x000A656B
		protected override float VerticalAimSensitivity
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000A8372 File Offset: 0x000A6572
		protected override void HandleUserAiming(float dt)
		{
			if (base.PilotAgent == null)
			{
				this.targetReleaseAngle = 0f;
			}
			base.HandleUserAiming(dt);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000A8390 File Offset: 0x000A6590
		protected override void ApplyAimChange()
		{
			MatrixFrame ballistaNavelInitialFrame = this._ballistaNavelInitialFrame;
			ballistaNavelInitialFrame.rotation.RotateAboutUp(this.currentDirection);
			this.ballistaNavel.GameEntity.SetFrame(ref ballistaNavelInitialFrame);
			MatrixFrame m = this._ballistaNavelInitialFrame.TransformToLocal(this._pilotInitialLocalFrame);
			MatrixFrame matrixFrame = ballistaNavelInitialFrame.TransformToParent(m);
			base.PilotStandingPoint.GameEntity.SetFrame(ref matrixFrame);
			MatrixFrame ballistaBodyInitialLocalFrame = this._ballistaBodyInitialLocalFrame;
			ballistaBodyInitialLocalFrame.rotation.RotateAboutSide(-this.currentReleaseAngle + this._verticalOffsetAngle);
			this.ballistaBody.GameEntity.SetFrame(ref ballistaBodyInitialLocalFrame);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000A8428 File Offset: 0x000A6628
		protected override void ApplyCurrentDirectionToEntity()
		{
			this.ApplyAimChange();
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000A8430 File Offset: 0x000A6630
		protected override void GetSoundEventIndices()
		{
			this.MoveSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/ballista/move");
			this.ReloadSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/ballista/reload");
			this.ReloadEndSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/ballista/reload_end");
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000A8462 File Offset: 0x000A6662
		protected internal override bool IsTargetValid(ITargetable target)
		{
			return !(target is ICastleKeyPosition);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000A8470 File Offset: 0x000A6670
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents())
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000A8490 File Offset: 0x000A6690
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._changeToState != RangedSiegeWeapon.WeaponState.Invalid)
			{
				base.State = this._changeToState;
				this._changeToState = RangedSiegeWeapon.WeaponState.Invalid;
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000A84B8 File Offset: 0x000A66B8
		protected internal override void OnTickParallel(float dt)
		{
			base.OnTickParallel(dt);
			if (!base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (base.PilotAgent != null)
			{
				Agent pilotAgent = base.PilotAgent;
				MatrixFrame globalFrame = this.ballistaBody.GameEntity.GetGlobalFrame();
				pilotAgent.SetHandInverseKinematicsFrameForMissionObjectUsage(this._pilotInitialLocalIKFrame, globalFrame, this.AnimationHeightDifference);
				ActionIndexValueCache currentActionValue = base.PilotAgent.GetCurrentActionValue(1);
				if (currentActionValue == this._pickUpAmmoEndAnimationActionIndex || currentActionValue == this._placeAmmoStartAnimationActionIndex)
				{
					MatrixFrame m = base.PilotAgent.AgentVisuals.GetBoneEntitialFrame(base.PilotAgent.Monster.MainHandItemBoneIndex, false);
					globalFrame = base.PilotAgent.AgentVisuals.GetGlobalFrame();
					m = globalFrame.TransformToParent(m);
					base.Projectile.GameEntity.SetGlobalFrame(m);
				}
				else
				{
					base.Projectile.GameEntity.SetFrame(ref this._missileInitialLocalFrame);
				}
			}
			if (GameNetwork.IsClientOrReplay)
			{
				return;
			}
			switch (base.State)
			{
			case RangedSiegeWeapon.WeaponState.LoadingAmmo:
			{
				bool value = false;
				if (base.PilotAgent != null)
				{
					ActionIndexValueCache currentActionValue2 = base.PilotAgent.GetCurrentActionValue(1);
					if (currentActionValue2 != this._pickUpAmmoStartAnimationActionIndex && currentActionValue2 != this._pickUpAmmoEndAnimationActionIndex && currentActionValue2 != this._placeAmmoStartAnimationActionIndex && currentActionValue2 != this._placeAmmoEndAnimationActionIndex && !base.PilotAgent.SetActionChannel(1, this._pickUpAmmoStartAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && base.PilotAgent.Controller != Agent.ControllerType.AI)
					{
						base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					else if (currentActionValue2 == this._pickUpAmmoEndAnimationActionIndex || currentActionValue2 == this._placeAmmoStartAnimationActionIndex)
					{
						value = true;
					}
					else if (currentActionValue2 == this._placeAmmoEndAnimationActionIndex)
					{
						value = true;
						this._changeToState = RangedSiegeWeapon.WeaponState.WaitingBeforeIdle;
					}
				}
				base.Projectile.SetVisibleSynched(value, false);
				return;
			}
			case RangedSiegeWeapon.WeaponState.WaitingBeforeIdle:
				if (base.PilotAgent == null)
				{
					this._changeToState = RangedSiegeWeapon.WeaponState.Idle;
					return;
				}
				if (base.PilotAgent.GetCurrentActionValue(1) != this._placeAmmoEndAnimationActionIndex)
				{
					if (base.PilotAgent.Controller != Agent.ControllerType.AI)
					{
						base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					this._changeToState = RangedSiegeWeapon.WeaponState.Idle;
					return;
				}
				if (base.PilotAgent.GetCurrentActionProgress(1) > 0.9999f)
				{
					this._changeToState = RangedSiegeWeapon.WeaponState.Idle;
					if (base.PilotAgent != null && !base.PilotAgent.SetActionChannel(1, this._idleAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && base.PilotAgent.Controller != Agent.ControllerType.AI)
					{
						base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
						return;
					}
				}
				break;
			case RangedSiegeWeapon.WeaponState.Reloading:
				if (base.PilotAgent != null && !base.PilotAgent.SetActionChannel(1, this._reloadAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && base.PilotAgent.Controller != Agent.ControllerType.AI)
				{
					base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					return;
				}
				break;
			default:
				if (base.PilotAgent != null)
				{
					if (base.PilotAgent.IsInBeingStruckAction)
					{
						if (base.PilotAgent.GetCurrentActionValue(1) != Ballista.act_strike_bent_over)
						{
							base.PilotAgent.SetActionChannel(1, Ballista.act_strike_bent_over, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
							return;
						}
					}
					else if (!base.PilotAgent.SetActionChannel(1, this._idleAnimationActionIndex, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true) && base.PilotAgent.Controller != Agent.ControllerType.AI)
					{
						base.PilotAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
				}
				break;
			}
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000A88B1 File Offset: 0x000A6AB1
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = new TextObject("{=fEQAPJ2e}{KEY} Use", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000A88DB File Offset: 0x000A6ADB
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return new TextObject("{=abbALYlp}Ballista", null).ToString();
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000A88F0 File Offset: 0x000A6AF0
		protected override void UpdateAmmoMesh()
		{
			int num = 8 - base.AmmoCount;
			foreach (GameEntity gameEntity in base.GameEntity.GetChildren())
			{
				for (int i = 0; i < gameEntity.MultiMeshComponentCount; i++)
				{
					MetaMesh metaMesh = gameEntity.GetMetaMesh(i);
					for (int j = 0; j < metaMesh.MeshCount; j++)
					{
						metaMesh.GetMeshAtIndex(j).SetVectorArgument(0f, (float)num, 0f, 0f);
					}
				}
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000A8994 File Offset: 0x000A6B94
		public override float ProcessTargetValue(float baseValue, TargetFlags flags)
		{
			if (flags.HasAnyFlag(TargetFlags.NotAThreat))
			{
				return -1000f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSiegeEngine))
			{
				baseValue *= 0.2f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsStructure))
			{
				baseValue *= 0.05f;
			}
			if (flags.HasAnyFlag(TargetFlags.DebugThreat))
			{
				baseValue *= 10000f;
			}
			return baseValue;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000A89EC File Offset: 0x000A6BEC
		public override TargetFlags GetTargetFlags()
		{
			TargetFlags targetFlags = TargetFlags.None;
			targetFlags |= TargetFlags.IsFlammable;
			targetFlags |= TargetFlags.IsSiegeEngine;
			if (this.Side == BattleSideEnum.Attacker)
			{
				targetFlags |= TargetFlags.IsAttacker;
			}
			targetFlags |= TargetFlags.IsSmall;
			if (base.IsDestroyed || this.IsDeactivated)
			{
				targetFlags |= TargetFlags.NotAThreat;
			}
			if (this.Side == BattleSideEnum.Attacker && DebugSiegeBehavior.DebugDefendState == DebugSiegeBehavior.DebugStateDefender.DebugDefendersToBallistae)
			{
				targetFlags |= TargetFlags.DebugThreat;
			}
			if (this.Side == BattleSideEnum.Defender && DebugSiegeBehavior.DebugAttackState == DebugSiegeBehavior.DebugStateAttacker.DebugAttackersToBallistae)
			{
				targetFlags |= TargetFlags.DebugThreat;
			}
			return targetFlags;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000A8A5D File Offset: 0x000A6C5D
		public override float GetTargetValue(List<Vec3> weaponPos)
		{
			return 30f * base.GetUserMultiplierOfWeapon() * this.GetDistanceMultiplierOfWeapon(weaponPos[0]) * base.GetHitPointMultiplierOfWeapon();
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000A8A80 File Offset: 0x000A6C80
		public void SetSpawnedFromSpawner()
		{
			this._spawnedFromSpawner = true;
		}

		// Token: 0x040010E9 RID: 4329
		private static readonly ActionIndexCache act_usage_ballista_ammo_pick_up_end = ActionIndexCache.Create("act_usage_ballista_ammo_pick_up_end");

		// Token: 0x040010EA RID: 4330
		private static readonly ActionIndexCache act_usage_ballista_ammo_pick_up_start = ActionIndexCache.Create("act_usage_ballista_ammo_pick_up_start");

		// Token: 0x040010EB RID: 4331
		private static readonly ActionIndexCache act_usage_ballista_ammo_place_end = ActionIndexCache.Create("act_usage_ballista_ammo_place_end");

		// Token: 0x040010EC RID: 4332
		private static readonly ActionIndexCache act_usage_ballista_ammo_place_start = ActionIndexCache.Create("act_usage_ballista_ammo_place_start");

		// Token: 0x040010ED RID: 4333
		private static readonly ActionIndexCache act_usage_ballista_idle = ActionIndexCache.Create("act_usage_ballista_idle");

		// Token: 0x040010EE RID: 4334
		private static readonly ActionIndexCache act_usage_ballista_reload = ActionIndexCache.Create("act_usage_ballista_reload");

		// Token: 0x040010EF RID: 4335
		private static readonly ActionIndexCache act_strike_bent_over = ActionIndexCache.Create("act_strike_bent_over");

		// Token: 0x040010F0 RID: 4336
		public string NavelTag = "BallistaNavel";

		// Token: 0x040010F1 RID: 4337
		public string BodyTag = "BallistaBody";

		// Token: 0x040010F2 RID: 4338
		public float AnimationHeightDifference;

		// Token: 0x040010F5 RID: 4341
		private MatrixFrame _ballistaBodyInitialLocalFrame;

		// Token: 0x040010F6 RID: 4342
		private MatrixFrame _ballistaNavelInitialFrame;

		// Token: 0x040010F7 RID: 4343
		private MatrixFrame _pilotInitialLocalFrame;

		// Token: 0x040010F8 RID: 4344
		private MatrixFrame _pilotInitialLocalIKFrame;

		// Token: 0x040010F9 RID: 4345
		private MatrixFrame _missileInitialLocalFrame;

		// Token: 0x040010FA RID: 4346
		[EditableScriptComponentVariable(true)]
		protected string IdleActionName = "act_usage_ballista_idle_attacker";

		// Token: 0x040010FB RID: 4347
		[EditableScriptComponentVariable(true)]
		protected string ReloadActionName = "act_usage_ballista_reload_attacker";

		// Token: 0x040010FC RID: 4348
		[EditableScriptComponentVariable(true)]
		protected string PlaceAmmoStartActionName = "act_usage_ballista_ammo_place_start_attacker";

		// Token: 0x040010FD RID: 4349
		[EditableScriptComponentVariable(true)]
		protected string PlaceAmmoEndActionName = "act_usage_ballista_ammo_place_end_attacker";

		// Token: 0x040010FE RID: 4350
		[EditableScriptComponentVariable(true)]
		protected string PickUpAmmoStartActionName = "act_usage_ballista_ammo_pick_up_start_attacker";

		// Token: 0x040010FF RID: 4351
		[EditableScriptComponentVariable(true)]
		protected string PickUpAmmoEndActionName = "act_usage_ballista_ammo_pick_up_end_attacker";

		// Token: 0x04001100 RID: 4352
		private ActionIndexCache _idleAnimationActionIndex;

		// Token: 0x04001101 RID: 4353
		private ActionIndexCache _reloadAnimationActionIndex;

		// Token: 0x04001102 RID: 4354
		private ActionIndexCache _placeAmmoStartAnimationActionIndex;

		// Token: 0x04001103 RID: 4355
		private ActionIndexCache _placeAmmoEndAnimationActionIndex;

		// Token: 0x04001104 RID: 4356
		private ActionIndexCache _pickUpAmmoStartAnimationActionIndex;

		// Token: 0x04001105 RID: 4357
		private ActionIndexCache _pickUpAmmoEndAnimationActionIndex;

		// Token: 0x04001106 RID: 4358
		private float _verticalOffsetAngle;

		// Token: 0x04001107 RID: 4359
		[EditableScriptComponentVariable(false)]
		public float HorizontalDirectionRestriction = 1.5707964f;

		// Token: 0x04001108 RID: 4360
		public float BallistaShootingSpeed = 120f;

		// Token: 0x04001109 RID: 4361
		private RangedSiegeWeapon.WeaponState _changeToState = RangedSiegeWeapon.WeaponState.Invalid;
	}
}
