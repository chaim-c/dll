using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade.Objects.Usables;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200033D RID: 829
	public abstract class RangedSiegeWeapon : SiegeWeapon
	{
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000B05A1 File Offset: 0x000AE7A1
		// (set) Token: 0x06002CAC RID: 11436 RVA: 0x000B05A9 File Offset: 0x000AE7A9
		public RangedSiegeWeapon.WeaponState State
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
						GameNetwork.WriteMessage(new SetRangedSiegeWeaponState(base.Id, value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					}
					this._state = value;
					this.OnRangedSiegeWeaponStateChange();
				}
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000B05E6 File Offset: 0x000AE7E6
		protected virtual float MaximumBallisticError
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002CAE RID: 11438
		protected abstract float ShootingSpeed { get; }

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000B05ED File Offset: 0x000AE7ED
		public virtual Vec3 CanShootAtPointCheckingOffset
		{
			get
			{
				return Vec3.Zero;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000B05F4 File Offset: 0x000AE7F4
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x000B05FC File Offset: 0x000AE7FC
		public GameEntity cameraHolder { get; private set; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000B0605 File Offset: 0x000AE805
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x000B060D File Offset: 0x000AE80D
		private protected SynchedMissionObject Projectile { protected get; private set; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000B0616 File Offset: 0x000AE816
		protected Vec3 MissleStartingPositionForSimulation
		{
			get
			{
				if (this.MissileStartingPositionEntityForSimulation != null)
				{
					return this.MissileStartingPositionEntityForSimulation.GlobalPosition;
				}
				SynchedMissionObject projectile = this.Projectile;
				if (projectile == null)
				{
					return Vec3.Zero;
				}
				return projectile.GameEntity.GlobalPosition;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x000B064C File Offset: 0x000AE84C
		protected string SkeletonName
		{
			set
			{
				this.SkeletonNames = new string[]
				{
					value
				};
			}
		}

		// Token: 0x17000842 RID: 2114
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000B065E File Offset: 0x000AE85E
		protected string FireAnimation
		{
			set
			{
				this.FireAnimations = new string[]
				{
					value
				};
			}
		}

		// Token: 0x17000843 RID: 2115
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x000B0670 File Offset: 0x000AE870
		protected string SetUpAnimation
		{
			set
			{
				this.SetUpAnimations = new string[]
				{
					value
				};
			}
		}

		// Token: 0x17000844 RID: 2116
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x000B0682 File Offset: 0x000AE882
		protected int FireAnimationIndex
		{
			set
			{
				this.FireAnimationIndices = new int[]
				{
					value
				};
			}
		}

		// Token: 0x17000845 RID: 2117
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000B0694 File Offset: 0x000AE894
		protected int SetUpAnimationIndex
		{
			set
			{
				this.SetUpAnimationIndices = new int[]
				{
					value
				};
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06002CBA RID: 11450 RVA: 0x000B06A8 File Offset: 0x000AE8A8
		// (remove) Token: 0x06002CBB RID: 11451 RVA: 0x000B06E0 File Offset: 0x000AE8E0
		public event RangedSiegeWeapon.OnSiegeWeaponReloadDone OnReloadDone;

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000B0715 File Offset: 0x000AE915
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000B071D File Offset: 0x000AE91D
		public int AmmoCount
		{
			get
			{
				return this.CurrentAmmo;
			}
			protected set
			{
				this.CurrentAmmo = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000B0726 File Offset: 0x000AE926
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x000B072E File Offset: 0x000AE92E
		protected virtual bool HasAmmo
		{
			get
			{
				return this._hasAmmo;
			}
			set
			{
				this._hasAmmo = value;
			}
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000B0738 File Offset: 0x000AE938
		protected virtual void ConsumeAmmo()
		{
			int ammoCount = this.AmmoCount;
			this.AmmoCount = ammoCount - 1;
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new SetRangedSiegeWeaponAmmo(base.Id, this.AmmoCount));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			this.UpdateAmmoMesh();
			this.CheckAmmo();
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000B078B File Offset: 0x000AE98B
		public virtual void SetAmmo(int ammoLeft)
		{
			if (this.AmmoCount != ammoLeft)
			{
				this.AmmoCount = ammoLeft;
				this.UpdateAmmoMesh();
				this.CheckAmmo();
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000B07AC File Offset: 0x000AE9AC
		protected virtual void CheckAmmo()
		{
			if (this.AmmoCount <= 0)
			{
				this.HasAmmo = false;
				base.SetForcedUse(false);
				foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement in this.AmmoPickUpStandingPoints)
				{
					standingPointWithWeaponRequirement.IsDeactivated = true;
				}
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000B0814 File Offset: 0x000AEA14
		public virtual float DirectionRestriction
		{
			get
			{
				return 2.0943952f;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000B081B File Offset: 0x000AEA1B
		public Vec3 OriginalDirection
		{
			get
			{
				return this._originalDirection;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000B0823 File Offset: 0x000AEA23
		protected virtual float HorizontalAimSensitivity
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000B082A File Offset: 0x000AEA2A
		protected virtual float VerticalAimSensitivity
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x06002CC7 RID: 11463
		protected abstract void RegisterAnimationParameters();

		// Token: 0x06002CC8 RID: 11464
		protected abstract void GetSoundEventIndices();

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06002CC9 RID: 11465 RVA: 0x000B0834 File Offset: 0x000AEA34
		// (remove) Token: 0x06002CCA RID: 11466 RVA: 0x000B086C File Offset: 0x000AEA6C
		public event Action<RangedSiegeWeapon, Agent> OnAgentLoadsMachine;

		// Token: 0x06002CCB RID: 11467 RVA: 0x000B08A4 File Offset: 0x000AEAA4
		protected void ChangeProjectileEntityServer(Agent loadingAgent, string missileItemID)
		{
			List<SynchedMissionObject> list = base.GameEntity.CollectObjectsWithTag("projectile");
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GameEntity.HasTag(missileItemID))
				{
					this.Projectile = list[i];
					this._projectileIndex = i;
					break;
				}
			}
			this.LoadedMissileItem = Game.Current.ObjectManager.GetObject<ItemObject>(missileItemID);
			if (GameNetwork.IsServerOrRecorder)
			{
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new RangedSiegeWeaponChangeProjectile(base.Id, this._projectileIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
			}
			Action<RangedSiegeWeapon, Agent> onAgentLoadsMachine = this.OnAgentLoadsMachine;
			if (onAgentLoadsMachine == null)
			{
				return;
			}
			onAgentLoadsMachine(this, loadingAgent);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000B0950 File Offset: 0x000AEB50
		public void ChangeProjectileEntityClient(int index)
		{
			List<SynchedMissionObject> list = base.GameEntity.CollectObjectsWithTag("projectile");
			this.Projectile = list[index];
			this._projectileIndex = index;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000B0984 File Offset: 0x000AEB84
		protected internal override void OnInit()
		{
			base.OnInit();
			DestructableComponent destructableComponent = base.GameEntity.GetScriptComponents<DestructableComponent>().FirstOrDefault<DestructableComponent>();
			if (destructableComponent != null)
			{
				this._defaultSide = destructableComponent.BattleSide;
			}
			else
			{
				Debug.FailedAssert("Ranged siege weapons must have destructible component.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\RangedSiegeWeapon.cs", "OnInit", 413);
			}
			this.ReleaseAngleRestrictionCenter = (this.TopReleaseAngleRestriction + this.BottomReleaseAngleRestriction) * 0.5f;
			this.ReleaseAngleRestrictionAngle = this.TopReleaseAngleRestriction - this.BottomReleaseAngleRestriction;
			this.currentReleaseAngle = (this._lastSyncedReleaseAngle = this.ReleaseAngleRestrictionCenter);
			this.OriginalMissileItem = Game.Current.ObjectManager.GetObject<ItemObject>(this.MissileItemID);
			this.LoadedMissileItem = this.OriginalMissileItem;
			this._originalMissileWeaponStatsDataForTargeting = new MissionWeapon(this.OriginalMissileItem, null, null).GetWeaponStatsDataForUsage(0);
			if (this.RotationObject == null)
			{
				this.RotationObject = this;
			}
			this._rotationObjectInitialFrame = this.RotationObject.GameEntity.GetFrame();
			this._originalDirection = this.RotationObject.GameEntity.GetGlobalFrame().rotation.f;
			this._originalDirection.RotateAboutZ(3.1415927f);
			this.currentDirection = (this._lastSyncedDirection = 0f);
			this._syncTimer = 0f;
			List<GameEntity> list = base.GameEntity.CollectChildrenEntitiesWithTag("cameraHolder");
			if (list.Count > 0)
			{
				this.cameraHolder = list[0];
				this.cameraHolderInitialFrame = this.cameraHolder.GetFrame();
				if (GameNetwork.IsClientOrReplay)
				{
					this.MakeVisibilityCheck = false;
				}
			}
			List<SynchedMissionObject> list2 = base.GameEntity.CollectObjectsWithTag("projectile");
			foreach (SynchedMissionObject synchedMissionObject in list2)
			{
				synchedMissionObject.GameEntity.SetVisibilityExcludeParents(false);
			}
			this.Projectile = list2.FirstOrDefault((SynchedMissionObject x) => x.GameEntity.HasTag(this.MissileItemID));
			this.Projectile.GameEntity.SetVisibilityExcludeParents(true);
			GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.Name == "clean");
			GameEntity missileStartingPositionEntityForSimulation;
			if (gameEntity == null)
			{
				missileStartingPositionEntityForSimulation = null;
			}
			else
			{
				missileStartingPositionEntityForSimulation = gameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.Name == "projectile_leaving_position");
			}
			this.MissileStartingPositionEntityForSimulation = missileStartingPositionEntityForSimulation;
			this.targetDirection = this.currentDirection;
			this.targetReleaseAngle = this.currentReleaseAngle;
			this.CanPickUpAmmoStandingPoints = new List<StandingPoint>();
			this.ReloadStandingPoints = new List<StandingPoint>();
			this.AmmoPickUpStandingPoints = new List<StandingPointWithWeaponRequirement>();
			if (base.StandingPoints != null)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					standingPoint.AddComponent(new ResetAnimationOnStopUsageComponent(ActionIndexCache.act_none));
					if (standingPoint.GameEntity.HasTag("reload"))
					{
						this.ReloadStandingPoints.Add(standingPoint);
					}
					if (standingPoint.GameEntity.HasTag("can_pick_up_ammo"))
					{
						this.CanPickUpAmmoStandingPoints.Add(standingPoint);
					}
				}
			}
			List<StandingPointWithWeaponRequirement> list3 = base.StandingPoints.OfType<StandingPointWithWeaponRequirement>().ToList<StandingPointWithWeaponRequirement>();
			List<StandingPointWithWeaponRequirement> list4 = new List<StandingPointWithWeaponRequirement>();
			foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement in list3)
			{
				if (standingPointWithWeaponRequirement.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					this.AmmoPickUpStandingPoints.Add(standingPointWithWeaponRequirement);
					standingPointWithWeaponRequirement.InitGivenWeapon(this.OriginalMissileItem);
					standingPointWithWeaponRequirement.SetupOnUsingStoppedBehavior(false, new Action<Agent, bool>(this.OnAmmoPickupUsingCancelled));
				}
				else
				{
					list4.Add(standingPointWithWeaponRequirement);
					standingPointWithWeaponRequirement.SetupOnUsingStoppedBehavior(false, new Action<Agent, bool>(this.OnLoadingAmmoPointUsingCancelled));
					standingPointWithWeaponRequirement.InitRequiredWeaponClasses(this.OriginalMissileItem.PrimaryWeapon.WeaponClass, WeaponClass.Undefined);
				}
			}
			if (this.AmmoPickUpStandingPoints.Count > 1)
			{
				this._stonePile = this.AmmoPickUpStandingPoints[0].GameEntity.Parent.GetFirstScriptOfType<SiegeMachineStonePile>();
				this._ammoPickupCenter = default(Vec3);
				foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement2 in this.AmmoPickUpStandingPoints)
				{
					standingPointWithWeaponRequirement2.SetHasAlternative(true);
					this._ammoPickupCenter += standingPointWithWeaponRequirement2.GameEntity.GlobalPosition;
				}
				this._ammoPickupCenter /= (float)this.AmmoPickUpStandingPoints.Count;
			}
			else
			{
				this._ammoPickupCenter = base.GameEntity.GlobalPosition;
			}
			list4.Sort(delegate(StandingPointWithWeaponRequirement element1, StandingPointWithWeaponRequirement element2)
			{
				if (element1.GameEntity.GlobalPosition.DistanceSquared(this._ammoPickupCenter) > element2.GameEntity.GlobalPosition.DistanceSquared(this._ammoPickupCenter))
				{
					return 1;
				}
				if (element1.GameEntity.GlobalPosition.DistanceSquared(this._ammoPickupCenter) < element2.GameEntity.GlobalPosition.DistanceSquared(this._ammoPickupCenter))
				{
					return -1;
				}
				return 0;
			});
			this.LoadAmmoStandingPoint = list4.FirstOrDefault<StandingPointWithWeaponRequirement>();
			this.SortCanPickUpAmmoStandingPoints();
			Vec3 v = base.PilotStandingPoint.GameEntity.GlobalPosition - base.GameEntity.GlobalPosition;
			foreach (StandingPoint standingPoint2 in this.CanPickUpAmmoStandingPoints)
			{
				if (standingPoint2 != base.PilotStandingPoint)
				{
					float length = (standingPoint2.GameEntity.GlobalPosition - base.GameEntity.GlobalPosition + v).Length;
					this.PilotReservePriorityValues.Add(standingPoint2, length);
				}
			}
			this.AmmoCount = this.startingAmmoCount;
			this.UpdateAmmoMesh();
			this.RegisterAnimationParameters();
			this.GetSoundEventIndices();
			this.InitAnimations();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000B0F60 File Offset: 0x000AF160
		private void SortCanPickUpAmmoStandingPoints()
		{
			if (MBMath.GetSmallestDifferenceBetweenTwoAngles(this._lastCanPickUpAmmoStandingPointsSortedAngle, this.currentDirection) > 0.18849556f)
			{
				this._lastCanPickUpAmmoStandingPointsSortedAngle = this.currentDirection;
				int signOfAmmoPile = Math.Sign(Vec3.DotProduct(base.GameEntity.GetGlobalFrame().rotation.s, this._ammoPickupCenter - base.GameEntity.GlobalPosition));
				this.CanPickUpAmmoStandingPoints.Sort(delegate(StandingPoint element1, StandingPoint element2)
				{
					Vec3 vec = this._ammoPickupCenter - element1.GameEntity.GlobalPosition;
					Vec3 vec2 = this._ammoPickupCenter - element2.GameEntity.GlobalPosition;
					float num = vec.LengthSquared;
					float num2 = vec2.LengthSquared;
					float num3 = Vec3.DotProduct(this.GameEntity.GetGlobalFrame().rotation.s, element1.GameEntity.GlobalPosition - this.GameEntity.GlobalPosition);
					float num4 = Vec3.DotProduct(this.GameEntity.GetGlobalFrame().rotation.s, element2.GameEntity.GlobalPosition - this.GameEntity.GlobalPosition);
					if (!element1.GameEntity.HasTag("no_ammo_pick_up_penalty") && signOfAmmoPile != Math.Sign(num3))
					{
						num += num3 * num3 * 64f;
					}
					if (!element2.GameEntity.HasTag("no_ammo_pick_up_penalty") && signOfAmmoPile != Math.Sign(num4))
					{
						num2 += num4 * num4 * 64f;
					}
					if (element1.GameEntity.HasTag(this.PilotStandingPointTag))
					{
						num += 25f;
					}
					else if (element2.GameEntity.HasTag(this.PilotStandingPointTag))
					{
						num2 += 25f;
					}
					if (num > num2)
					{
						return 1;
					}
					if (num < num2)
					{
						return -1;
					}
					return 0;
				});
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000B0FF0 File Offset: 0x000AF1F0
		protected internal override void OnEditorInit()
		{
			List<SynchedMissionObject> list = base.GameEntity.CollectObjectsWithTag("projectile");
			if (list.Count > 0)
			{
				this.Projectile = list[0];
			}
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000B1024 File Offset: 0x000AF224
		private void InitAnimations()
		{
			for (int i = 0; i < this.Skeletons.Length; i++)
			{
				this.Skeletons[i].SetAnimationAtChannel(this.SetUpAnimations[i], 0, 1f, 0f, 0f);
				this.Skeletons[i].SetAnimationParameterAtChannel(0, 1f);
				this.Skeletons[i].TickAnimations(0.0001f, MatrixFrame.Identity, true);
			}
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000B1094 File Offset: 0x000AF294
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			this.Projectile.GameEntity.SetVisibilityExcludeParents(true);
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				Agent userAgent = standingPoint.UserAgent;
				if (userAgent != null)
				{
					userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				standingPoint.IsDeactivated = false;
			}
			this._state = RangedSiegeWeapon.WeaponState.Idle;
			this.currentDirection = (this._lastSyncedDirection = 0f);
			this._syncTimer = 0f;
			this.currentReleaseAngle = (this._lastSyncedReleaseAngle = this.ReleaseAngleRestrictionCenter);
			this.targetDirection = this.currentDirection;
			this.targetReleaseAngle = this.currentReleaseAngle;
			this.ApplyCurrentDirectionToEntity();
			this.AmmoCount = this.startingAmmoCount;
			this.UpdateAmmoMesh();
			if (this.MoveSound != null)
			{
				this.MoveSound.Stop();
				this.MoveSound = null;
			}
			this.hasFrameChangedInPreviousFrame = false;
			Skeleton[] skeletons = this.Skeletons;
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].Freeze(false);
			}
			foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement in this.AmmoPickUpStandingPoints)
			{
				standingPointWithWeaponRequirement.IsDeactivated = false;
			}
			this.InitAnimations();
			this.UpdateProjectilePosition();
			if (!GameNetwork.IsClientOrReplay)
			{
				this.SetActivationLoadAmmoPoint(false);
			}
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000B1214 File Offset: 0x000AF414
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteIntToPacket((int)this.State, CompressionMission.RangedSiegeWeaponStateCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.targetDirection, CompressionBasic.RadianCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.targetReleaseAngle, CompressionBasic.RadianCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.AmmoCount, CompressionMission.RangedSiegeWeaponAmmoCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this._projectileIndex, CompressionMission.RangedSiegeWeaponAmmoIndexCompressionInfo);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000B1277 File Offset: 0x000AF477
		protected virtual void UpdateProjectilePosition()
		{
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000B127C File Offset: 0x000AF47C
		public override bool IsInRangeToCheckAlternativePoints(Agent agent)
		{
			float num = (this.AmmoPickUpStandingPoints.Count > 0) ? (agent.GetInteractionDistanceToUsable(this.AmmoPickUpStandingPoints[0]) + 2f) : 2f;
			return this._ammoPickupCenter.DistanceSquared(agent.Position) < num * num;
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000B12D0 File Offset: 0x000AF4D0
		public override StandingPoint GetBestPointAlternativeTo(StandingPoint standingPoint, Agent agent)
		{
			if (this.AmmoPickUpStandingPoints.Contains(standingPoint))
			{
				IEnumerable<StandingPointWithWeaponRequirement> enumerable = from sp in this.AmmoPickUpStandingPoints
				where !sp.IsDeactivated && (sp.IsInstantUse || (!sp.HasUser && !sp.HasAIMovingTo)) && !sp.IsDisabledForAgent(agent)
				select sp;
				float num = standingPoint.GameEntity.GlobalPosition.DistanceSquared(agent.Position);
				StandingPoint result = standingPoint;
				foreach (StandingPointWithWeaponRequirement standingPointWithWeaponRequirement in enumerable)
				{
					float num2 = standingPointWithWeaponRequirement.GameEntity.GlobalPosition.DistanceSquared(agent.Position);
					if (num2 < num)
					{
						num = num2;
						result = standingPointWithWeaponRequirement;
					}
				}
				return result;
			}
			return standingPoint;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000B139C File Offset: 0x000AF59C
		protected virtual void OnRangedSiegeWeaponStateChange()
		{
			switch (this.State)
			{
			case RangedSiegeWeapon.WeaponState.Idle:
			case RangedSiegeWeapon.WeaponState.WaitingBeforeIdle:
				if (this.cameraState == RangedSiegeWeapon.CameraState.FreeMove)
				{
					this.cameraState = RangedSiegeWeapon.CameraState.ApproachToCamera;
				}
				else
				{
					this.cameraState = RangedSiegeWeapon.CameraState.StickToWeapon;
				}
				break;
			case RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving:
				this.AttackClickWillReload = this.WeaponNeedsClickToReload;
				break;
			case RangedSiegeWeapon.WeaponState.Shooting:
				if (this.cameraHolder != null)
				{
					this.cameraState = RangedSiegeWeapon.CameraState.DontMove;
					this.dontMoveTimer = 0.35f;
				}
				break;
			case RangedSiegeWeapon.WeaponState.WaitingAfterShooting:
				this.AttackClickWillReload = this.WeaponNeedsClickToReload;
				this.CheckAmmo();
				break;
			case RangedSiegeWeapon.WeaponState.WaitingBeforeReloading:
				this.AttackClickWillReload = false;
				if (this.cameraHolder != null)
				{
					this.cameraState = RangedSiegeWeapon.CameraState.MoveDownToReload;
				}
				this.CheckAmmo();
				break;
			case RangedSiegeWeapon.WeaponState.LoadingAmmo:
				if (this.ReloadSound != null && this.ReloadSound.IsValid)
				{
					this.ReloadSound.Stop();
				}
				this.ReloadSound = null;
				Mission.Current.MakeSound(this.ReloadEndSoundIndex, base.GameEntity.GetGlobalFrame().origin, true, false, -1, -1);
				break;
			case RangedSiegeWeapon.WeaponState.Reloading:
				if (this.ReloadSound != null && this.ReloadSound.IsValid)
				{
					if (this.ReloadSound.IsPaused())
					{
						this.ReloadSound.Resume();
					}
					else
					{
						this.ReloadSound.PlayInPosition(base.GameEntity.GetGlobalFrame().origin);
					}
				}
				else
				{
					this.ReloadSound = SoundEvent.CreateEvent(this.ReloadSoundIndex, base.Scene);
					this.ReloadSound.PlayInPosition(base.GameEntity.GetGlobalFrame().origin);
				}
				break;
			case RangedSiegeWeapon.WeaponState.ReloadingPaused:
				if (this.ReloadSound != null && this.ReloadSound.IsValid)
				{
					this.ReloadSound.Pause();
				}
				break;
			default:
				Debug.FailedAssert("Invalid WeaponState.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\RangedSiegeWeapon.cs", "OnRangedSiegeWeaponStateChange", 854);
				break;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				switch (this.State)
				{
				case RangedSiegeWeapon.WeaponState.Idle:
				case RangedSiegeWeapon.WeaponState.WaitingAfterShooting:
				case RangedSiegeWeapon.WeaponState.WaitingBeforeReloading:
					break;
				case RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving:
					for (int i = 0; i < this.SkeletonOwnerObjects.Length; i++)
					{
						this.SkeletonOwnerObjects[i].SetAnimationAtChannelSynched(this.FireAnimations[i], 0, 1f);
					}
					return;
				case RangedSiegeWeapon.WeaponState.Shooting:
					this.ShootProjectile();
					return;
				case RangedSiegeWeapon.WeaponState.LoadingAmmo:
					this.SetActivationLoadAmmoPoint(true);
					this.ReloaderAgent = null;
					return;
				case RangedSiegeWeapon.WeaponState.WaitingBeforeIdle:
					this.SendReloaderAgentToOriginalPoint();
					this.SetActivationLoadAmmoPoint(false);
					return;
				case RangedSiegeWeapon.WeaponState.Reloading:
					for (int j = 0; j < this.SkeletonOwnerObjects.Length; j++)
					{
						if (this.SkeletonOwnerObjects[j].GameEntity.IsSkeletonAnimationPaused())
						{
							this.SkeletonOwnerObjects[j].ResumeSkeletonAnimationSynched();
						}
						else
						{
							this.SkeletonOwnerObjects[j].SetAnimationAtChannelSynched(this.SetUpAnimations[j], 0, 1f);
						}
					}
					this._currentReloaderCount = 1;
					return;
				case RangedSiegeWeapon.WeaponState.ReloadingPaused:
				{
					SynchedMissionObject[] skeletonOwnerObjects = this.SkeletonOwnerObjects;
					for (int k = 0; k < skeletonOwnerObjects.Length; k++)
					{
						skeletonOwnerObjects[k].PauseSkeletonAnimationSynched();
					}
					return;
				}
				default:
					Debug.FailedAssert("Invalid WeaponState.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\RangedSiegeWeapon.cs", "OnRangedSiegeWeaponStateChange", 927);
					break;
				}
			}
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000B16B0 File Offset: 0x000AF8B0
		protected virtual void SetActivationLoadAmmoPoint(bool activate)
		{
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000B16B4 File Offset: 0x000AF8B4
		protected float GetDetachmentWeightAuxForExternalAmmoWeapons(BattleSideEnum side)
		{
			if (this.IsDisabledForBattleSideAI(side))
			{
				return float.MinValue;
			}
			this._usableStandingPoints.Clear();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = !base.PilotStandingPoint.HasUser && !base.PilotStandingPoint.HasAIMovingTo && (this.ReloaderAgent == null || this.ReloaderAgentOriginalPoint != base.PilotStandingPoint);
			int num = -1;
			StandingPoint standingPoint = null;
			bool flag4 = false;
			for (int i = 0; i < base.StandingPoints.Count; i++)
			{
				StandingPoint standingPoint2 = base.StandingPoints[i];
				if (standingPoint2.GameEntity.HasTag("can_pick_up_ammo"))
				{
					if (this.ReloaderAgent == null || standingPoint2 != this.ReloaderAgentOriginalPoint)
					{
						if (standingPoint2.IsUsableBySide(side))
						{
							if (!standingPoint2.HasAIMovingTo)
							{
								if (!flag2)
								{
									this._usableStandingPoints.Clear();
									if (num != -1)
									{
										num = -1;
									}
								}
								flag2 = true;
							}
							else if (flag2 || standingPoint2.MovingAgent.Formation.Team.Side != side)
							{
								goto IL_16A;
							}
							flag = true;
							this._usableStandingPoints.Add(new ValueTuple<int, StandingPoint>(i, standingPoint2));
							if (flag3 && base.PilotStandingPoint == standingPoint2)
							{
								num = this._usableStandingPoints.Count - 1;
							}
						}
						else if (flag3 && standingPoint2.HasAIUser && (standingPoint == null || this.PilotReservePriorityValues[standingPoint2] > this.PilotReservePriorityValues[standingPoint] || flag4))
						{
							standingPoint = standingPoint2;
							flag4 = false;
						}
					}
					else if (flag3 && standingPoint == null)
					{
						standingPoint = standingPoint2;
						flag4 = true;
					}
				}
				IL_16A:;
			}
			if (standingPoint != null)
			{
				if (flag4)
				{
					this.ReloaderAgentOriginalPoint = base.PilotStandingPoint;
				}
				else
				{
					Agent userAgent = standingPoint.UserAgent;
					userAgent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.DoNotWieldWeaponAfterStoppingUsingGameObject);
					userAgent.AIMoveToGameObjectEnable(base.PilotStandingPoint, this, base.Ai.GetScriptedFrameFlags(userAgent));
				}
				if (num != -1)
				{
					this._usableStandingPoints.RemoveAt(num);
				}
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

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000B18C4 File Offset: 0x000AFAC4
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents())
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000B18E4 File Offset: 0x000AFAE4
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				this.UpdateState(dt);
				if (base.PilotAgent != null && !base.PilotAgent.IsInBeingStruckAction)
				{
					if (base.PilotAgent.MovementFlags.HasAnyFlag(Agent.MovementControlFlag.AttackMask))
					{
						if (this.State == RangedSiegeWeapon.WeaponState.Idle)
						{
							this._aiRequestsShoot = false;
							this.Shoot();
						}
						else if (this.State == RangedSiegeWeapon.WeaponState.WaitingAfterShooting && this.AttackClickWillReload)
						{
							this._aiRequestsManualReload = false;
							this.ManualReload();
						}
					}
					if (this._aiRequestsManualReload)
					{
						this.ManualReload();
					}
					if (this._aiRequestsShoot)
					{
						this.Shoot();
					}
				}
				this._aiRequestsShoot = false;
				this._aiRequestsManualReload = false;
			}
			this.HandleUserAiming(dt);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000B19AB File Offset: 0x000AFBAB
		protected virtual float CalculateShootingRange(float heightDifference)
		{
			return Mission.GetMissileRange(this.ShootingSpeed, heightDifference);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000B19BC File Offset: 0x000AFBBC
		protected static bool ApproachToAngle(ref float angle, float angleToApproach, bool isMouse, float speed_limit, float dt, float sensitivity)
		{
			speed_limit = MathF.Abs(speed_limit);
			if (angle != angleToApproach)
			{
				float num = sensitivity * dt;
				float num2 = MathF.Abs(angle - angleToApproach);
				if (isMouse)
				{
					num *= MathF.Max(num2 * 8f, 0.15f);
				}
				if (speed_limit > 0f)
				{
					num = MathF.Min(num, speed_limit * dt);
				}
				if (num2 <= num)
				{
					angle = angleToApproach;
				}
				else
				{
					angle += num * (float)MathF.Sign(angleToApproach - angle);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000B1A30 File Offset: 0x000AFC30
		protected virtual void HandleUserAiming(float dt)
		{
			bool flag = false;
			float horizontalAimSensitivity = this.HorizontalAimSensitivity;
			float verticalAimSensitivity = this.VerticalAimSensitivity;
			bool flag2 = false;
			if (this.cameraState != RangedSiegeWeapon.CameraState.DontMove)
			{
				if (this._inputGiven)
				{
					flag2 = true;
					if (this.CanRotate())
					{
						if (this._inputX != 0f)
						{
							this.targetDirection += horizontalAimSensitivity * dt * this._inputX;
							this.targetDirection = MBMath.WrapAngle(this.targetDirection);
							this.targetDirection = MBMath.ClampAngle(this.targetDirection, this.currentDirection, 0.7f);
							this.targetDirection = MBMath.ClampAngle(this.targetDirection, 0f, this.DirectionRestriction);
						}
						if (this._inputY != 0f)
						{
							this.targetReleaseAngle += verticalAimSensitivity * dt * this._inputY;
							this.targetReleaseAngle = MBMath.ClampAngle(this.targetReleaseAngle, this.currentReleaseAngle + 0.049999997f, 0.6f);
							this.targetReleaseAngle = MBMath.ClampAngle(this.targetReleaseAngle, this.ReleaseAngleRestrictionCenter, this.ReleaseAngleRestrictionAngle);
						}
					}
					this._inputGiven = false;
					this._inputX = 0f;
					this._inputY = 0f;
				}
				else if (this._exactInputGiven)
				{
					bool flag3 = false;
					if (this.CanRotate())
					{
						if (this.targetDirection != this._inputTargetX)
						{
							float num = horizontalAimSensitivity * dt;
							if (MathF.Abs(this.targetDirection - this._inputTargetX) < num)
							{
								this.targetDirection = this._inputTargetX;
							}
							else if (this.targetDirection < this._inputTargetX)
							{
								this.targetDirection += num;
								flag3 = true;
							}
							else
							{
								this.targetDirection -= num;
								flag3 = true;
							}
							this.targetDirection = MBMath.WrapAngle(this.targetDirection);
							this.targetDirection = MBMath.ClampAngle(this.targetDirection, this.currentDirection, 0.7f);
							this.targetDirection = MBMath.ClampAngle(this.targetDirection, 0f, this.DirectionRestriction);
						}
						if (this.targetReleaseAngle != this._inputTargetY)
						{
							float num2 = verticalAimSensitivity * dt;
							if (MathF.Abs(this.targetReleaseAngle - this._inputTargetY) < num2)
							{
								this.targetReleaseAngle = this._inputTargetY;
							}
							else if (this.targetReleaseAngle < this._inputTargetY)
							{
								this.targetReleaseAngle += num2;
								flag3 = true;
							}
							else
							{
								this.targetReleaseAngle -= num2;
								flag3 = true;
							}
							this.targetReleaseAngle = MBMath.ClampAngle(this.targetReleaseAngle, this.currentReleaseAngle + 0.049999997f, 0.6f);
							this.targetReleaseAngle = MBMath.ClampAngle(this.targetReleaseAngle, this.ReleaseAngleRestrictionCenter, this.ReleaseAngleRestrictionAngle);
						}
					}
					else
					{
						flag3 = true;
					}
					if (!flag3)
					{
						this._exactInputGiven = false;
					}
				}
			}
			switch (this.cameraState)
			{
			case RangedSiegeWeapon.CameraState.StickToWeapon:
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.currentDirection, this.targetDirection, this.UsesMouseForAiming, -1f, dt, horizontalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.currentReleaseAngle, this.targetReleaseAngle, this.UsesMouseForAiming, -1f, dt, verticalAimSensitivity) || flag);
				this.cameraDirection = this.currentDirection;
				this.cameraReleaseAngle = this.currentReleaseAngle;
				break;
			case RangedSiegeWeapon.CameraState.DontMove:
				this.dontMoveTimer -= dt;
				if (this.dontMoveTimer < 0f)
				{
					if (!this.AttackClickWillReload)
					{
						this.cameraState = RangedSiegeWeapon.CameraState.MoveDownToReload;
						this.maxRotateSpeed = 0f;
						this.reloadTargetReleaseAngle = MBMath.ClampAngle((MathF.Abs(this.currentReleaseAngle) > 0.17453292f) ? 0f : this.currentReleaseAngle, this.currentReleaseAngle - 0.049999997f, 0.6f);
						this.targetDirection = this.cameraDirection;
						this.cameraReleaseAngle = this.targetReleaseAngle;
					}
					else
					{
						this.cameraState = RangedSiegeWeapon.CameraState.StickToWeapon;
					}
				}
				break;
			case RangedSiegeWeapon.CameraState.MoveDownToReload:
				this.maxRotateSpeed += dt * 1.2f;
				this.maxRotateSpeed = MathF.Min(this.maxRotateSpeed, 1f);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.currentReleaseAngle, this.reloadTargetReleaseAngle, this.UsesMouseForAiming, 0.4f + this.maxRotateSpeed, dt, verticalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.cameraDirection, this.targetDirection, this.UsesMouseForAiming, -1f, dt, horizontalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.cameraReleaseAngle, this.reloadTargetReleaseAngle, this.UsesMouseForAiming, 0.5f + this.maxRotateSpeed, dt, verticalAimSensitivity) || flag);
				if (!flag)
				{
					this.cameraState = RangedSiegeWeapon.CameraState.RememberLastShotDirection;
				}
				break;
			case RangedSiegeWeapon.CameraState.RememberLastShotDirection:
				if (this.State == RangedSiegeWeapon.WeaponState.Idle || flag2)
				{
					this.cameraState = RangedSiegeWeapon.CameraState.FreeMove;
					RangedSiegeWeapon.OnSiegeWeaponReloadDone onReloadDone = this.OnReloadDone;
					if (onReloadDone != null)
					{
						onReloadDone();
					}
				}
				break;
			case RangedSiegeWeapon.CameraState.FreeMove:
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.cameraDirection, this.targetDirection, this.UsesMouseForAiming, -1f, dt, horizontalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.cameraReleaseAngle, this.targetReleaseAngle, this.UsesMouseForAiming, -1f, dt, verticalAimSensitivity) || flag);
				this.maxRotateSpeed = 0f;
				break;
			case RangedSiegeWeapon.CameraState.ApproachToCamera:
				this.maxRotateSpeed += 0.9f * dt + this.maxRotateSpeed * 2f * dt;
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.cameraDirection, this.targetDirection, this.UsesMouseForAiming, -1f, dt, horizontalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.cameraReleaseAngle, this.targetReleaseAngle, this.UsesMouseForAiming, -1f, dt, verticalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.currentDirection, this.targetDirection, this.UsesMouseForAiming, this.maxRotateSpeed, dt, horizontalAimSensitivity) || flag);
				flag = (RangedSiegeWeapon.ApproachToAngle(ref this.currentReleaseAngle, this.targetReleaseAngle, this.UsesMouseForAiming, this.maxRotateSpeed, dt, verticalAimSensitivity) || flag);
				if (!flag)
				{
					this.cameraState = RangedSiegeWeapon.CameraState.StickToWeapon;
				}
				break;
			}
			if (this.cameraHolder != null)
			{
				MatrixFrame globalFrame = this.cameraHolderInitialFrame;
				globalFrame.rotation.RotateAboutForward(this.cameraDirection - this.currentDirection);
				globalFrame.rotation.RotateAboutSide(this.cameraReleaseAngle - this.currentReleaseAngle);
				this.cameraHolder.SetFrame(ref globalFrame);
				globalFrame = this.cameraHolder.GetGlobalFrame();
				globalFrame.rotation.s.z = 0f;
				globalFrame.rotation.s.Normalize();
				globalFrame.rotation.u = Vec3.CrossProduct(globalFrame.rotation.s, globalFrame.rotation.f);
				globalFrame.rotation.u.Normalize();
				globalFrame.rotation.f = Vec3.CrossProduct(globalFrame.rotation.u, globalFrame.rotation.s);
				globalFrame.rotation.f.Normalize();
				this.cameraHolder.SetGlobalFrame(globalFrame);
			}
			if (flag && !this.hasFrameChangedInPreviousFrame)
			{
				this.OnRotationStarted();
			}
			else if (!flag && this.hasFrameChangedInPreviousFrame)
			{
				this.OnRotationStopped();
			}
			this.hasFrameChangedInPreviousFrame = flag;
			if ((flag && GameNetwork.IsClient && base.PilotAgent == Agent.Main) || GameNetwork.IsServerOrRecorder)
			{
				float num3 = (GameNetwork.IsClient && base.PilotAgent == Agent.Main) ? 0.0001f : 0.02f;
				if (this._syncTimer > 0.2f && (MathF.Abs(this.currentDirection - this._lastSyncedDirection) > num3 || MathF.Abs(this.currentReleaseAngle - this._lastSyncedReleaseAngle) > num3))
				{
					this._lastSyncedDirection = this.currentDirection;
					this._lastSyncedReleaseAngle = this.currentReleaseAngle;
					MissionLobbyComponent missionBehavior = Mission.Current.GetMissionBehavior<MissionLobbyComponent>();
					if ((missionBehavior == null || missionBehavior.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending) && GameNetwork.IsClient && base.PilotAgent == Agent.Main)
					{
						GameNetwork.BeginModuleEventAsClient();
						GameNetwork.WriteMessage(new SetMachineRotation(base.Id, this.currentDirection, this.currentReleaseAngle));
						GameNetwork.EndModuleEventAsClient();
					}
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SetMachineTargetRotation(base.Id, this.currentDirection, this.currentReleaseAngle));
						GameNetwork.EventBroadcastFlags broadcastFlags = GameNetwork.EventBroadcastFlags.ExcludeTargetPlayer | GameNetwork.EventBroadcastFlags.AddToMissionRecord;
						Agent pilotAgent = base.PilotAgent;
						NetworkCommunicator targetPlayer;
						if (pilotAgent == null)
						{
							targetPlayer = null;
						}
						else
						{
							MissionPeer missionPeer = pilotAgent.MissionPeer;
							targetPlayer = ((missionPeer != null) ? missionPeer.GetNetworkPeer() : null);
						}
						GameNetwork.EndBroadcastModuleEvent(broadcastFlags, targetPlayer);
					}
				}
			}
			this._syncTimer += dt;
			if (this._syncTimer >= 1f)
			{
				this._syncTimer -= 1f;
			}
			if (flag)
			{
				this.ApplyAimChange();
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000B22AC File Offset: 0x000B04AC
		public void GiveInput(float inputX, float inputY)
		{
			this._exactInputGiven = false;
			this._inputGiven = true;
			this._inputX = inputX;
			this._inputY = inputY;
			this._inputX = MBMath.ClampFloat(this._inputX, -1f, 1f);
			this._inputY = MBMath.ClampFloat(this._inputY, -1f, 1f);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000B230B File Offset: 0x000B050B
		public void GiveExactInput(float targetX, float targetY)
		{
			this._exactInputGiven = true;
			this._inputGiven = false;
			this._inputTargetX = MBMath.ClampAngle(targetX, 0f, this.DirectionRestriction);
			this._inputTargetY = MBMath.ClampAngle(targetY, this.ReleaseAngleRestrictionCenter, this.ReleaseAngleRestrictionAngle);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000B234A File Offset: 0x000B054A
		protected virtual bool CanRotate()
		{
			return this.State == RangedSiegeWeapon.WeaponState.Idle;
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000B2355 File Offset: 0x000B0555
		protected virtual void ApplyAimChange()
		{
			if (this.CanRotate())
			{
				this.ApplyCurrentDirectionToEntity();
				return;
			}
			this.targetDirection = this.currentDirection;
			this.targetReleaseAngle = this.currentReleaseAngle;
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000B2380 File Offset: 0x000B0580
		protected virtual void ApplyCurrentDirectionToEntity()
		{
			MatrixFrame rotationObjectInitialFrame = this._rotationObjectInitialFrame;
			rotationObjectInitialFrame.rotation.RotateAboutUp(this.currentDirection);
			this.RotationObject.GameEntity.SetFrame(ref rotationObjectInitialFrame);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000B23B8 File Offset: 0x000B05B8
		public virtual float GetTargetDirection(Vec3 target)
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			globalFrame.rotation.RotateAboutUp(3.1415927f);
			return globalFrame.TransformToLocal(target).AsVec2.RotationInRadians;
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000B23FC File Offset: 0x000B05FC
		public virtual float GetTargetReleaseAngle(Vec3 target)
		{
			return Mission.GetMissileVerticalAimCorrection(target - this.MissleStartingPositionForSimulation, this.ShootingSpeed, ref this._originalMissileWeaponStatsDataForTargeting, ItemObject.GetAirFrictionConstant(this.OriginalMissileItem.PrimaryWeapon.WeaponClass, this.OriginalMissileItem.PrimaryWeapon.WeaponFlags));
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000B244C File Offset: 0x000B064C
		public virtual bool AimAtThreat(Threat threat)
		{
			Vec3 target = threat.Position + this.GetEstimatedTargetMovementVector(threat.Position, threat.GetVelocity());
			float num = this.GetTargetDirection(target);
			float num2 = this.GetTargetReleaseAngle(target);
			num = MBMath.ClampAngle(num, 0f, this.DirectionRestriction);
			num2 = MBMath.ClampAngle(num2, this.ReleaseAngleRestrictionCenter, this.ReleaseAngleRestrictionAngle);
			if (!this._exactInputGiven || num != this._inputTargetX || num2 != this._inputTargetY)
			{
				this.GiveExactInput(num, num2);
			}
			return MathF.Abs(this.currentDirection - this._inputTargetX) < 0.001f && MathF.Abs(this.currentReleaseAngle - this._inputTargetY) < 0.001f;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000B2504 File Offset: 0x000B0704
		public virtual void AimAtRotation(float horizontalRotation, float verticalRotation)
		{
			horizontalRotation = MBMath.ClampFloat(horizontalRotation, -3.1415927f, 3.1415927f);
			verticalRotation = MBMath.ClampFloat(verticalRotation, -3.1415927f, 3.1415927f);
			horizontalRotation = MBMath.ClampAngle(horizontalRotation, 0f, this.DirectionRestriction);
			verticalRotation = MBMath.ClampAngle(verticalRotation, this.ReleaseAngleRestrictionCenter, this.ReleaseAngleRestrictionAngle);
			if (!this._exactInputGiven || horizontalRotation != this._inputTargetX || verticalRotation != this._inputTargetY)
			{
				this.GiveExactInput(horizontalRotation, verticalRotation);
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000B257E File Offset: 0x000B077E
		protected void OnLoadingAmmoPointUsingCancelled(Agent agent, bool isCanceledBecauseOfAnimation)
		{
			if (agent.IsAIControlled)
			{
				if (isCanceledBecauseOfAnimation)
				{
					this.SendAgentToAmmoPickup(agent);
					return;
				}
				this.SendReloaderAgentToOriginalPoint();
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B2599 File Offset: 0x000B0799
		protected void OnAmmoPickupUsingCancelled(Agent agent, bool isCanceledBecauseOfAnimation)
		{
			if (agent.IsAIControlled)
			{
				this.SendAgentToAmmoPickup(agent);
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000B25AC File Offset: 0x000B07AC
		protected void SendAgentToAmmoPickup(Agent agent)
		{
			this.ReloaderAgent = agent;
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			if (wieldedItemIndex != EquipmentIndex.None && agent.Equipment[wieldedItemIndex].CurrentUsageItem.WeaponClass == this.OriginalMissileItem.PrimaryWeapon.WeaponClass)
			{
				agent.AIMoveToGameObjectEnable(this.LoadAmmoStandingPoint, this, base.Ai.GetScriptedFrameFlags(agent));
				return;
			}
			StandingPoint standingPoint = base.AmmoPickUpPoints.FirstOrDefault((StandingPoint x) => !x.HasUser);
			if (standingPoint != null)
			{
				agent.AIMoveToGameObjectEnable(standingPoint, this, base.Ai.GetScriptedFrameFlags(agent));
				return;
			}
			this.SendReloaderAgentToOriginalPoint();
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000B265C File Offset: 0x000B085C
		protected void SendReloaderAgentToOriginalPoint()
		{
			if (this.ReloaderAgent != null)
			{
				if (this.ReloaderAgentOriginalPoint != null && !this.ReloaderAgentOriginalPoint.HasAIMovingTo && !this.ReloaderAgentOriginalPoint.HasUser)
				{
					if (this.ReloaderAgent.InteractingWithAnyGameObject())
					{
						this.ReloaderAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.None);
					}
					this.ReloaderAgent.AIMoveToGameObjectEnable(this.ReloaderAgentOriginalPoint, this, base.Ai.GetScriptedFrameFlags(this.ReloaderAgent));
					return;
				}
				if (this.ReloaderAgentOriginalPoint == null || (this.ReloaderAgentOriginalPoint.MovingAgent != this.ReloaderAgent && this.ReloaderAgentOriginalPoint.UserAgent != this.ReloaderAgent))
				{
					if (this.ReloaderAgent.IsUsingGameObject)
					{
						this.ReloaderAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					this.ReloaderAgent = null;
				}
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000B2724 File Offset: 0x000B0924
		private void UpdateState(float dt)
		{
			if (this.LoadAmmoStandingPoint != null)
			{
				if (this.ReloaderAgent != null)
				{
					if (!this.ReloaderAgent.IsActive() || this.ReloaderAgent.Detachment != this)
					{
						this.ReloaderAgent = null;
					}
					else if (this.ReloaderAgentOriginalPoint.UserAgent == this.ReloaderAgent)
					{
						this.ReloaderAgent = null;
					}
				}
				if (this.State == RangedSiegeWeapon.WeaponState.LoadingAmmo && this.ReloaderAgent == null && !this.LoadAmmoStandingPoint.HasUser)
				{
					this.SortCanPickUpAmmoStandingPoints();
					StandingPoint standingPoint = null;
					StandingPoint standingPoint2 = null;
					foreach (StandingPoint standingPoint3 in this.CanPickUpAmmoStandingPoints)
					{
						if (standingPoint3.HasUser && standingPoint3.UserAgent.IsAIControlled)
						{
							if (standingPoint3 != base.PilotStandingPoint)
							{
								standingPoint = standingPoint3;
								break;
							}
							standingPoint2 = standingPoint3;
						}
					}
					if (standingPoint == null && standingPoint2 != null)
					{
						standingPoint = standingPoint2;
					}
					if (standingPoint != null)
					{
						if (this.HasAmmo)
						{
							Agent userAgent = standingPoint.UserAgent;
							userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.DoNotWieldWeaponAfterStoppingUsingGameObject);
							this.ReloaderAgentOriginalPoint = standingPoint;
							this.SendAgentToAmmoPickup(userAgent);
						}
						else
						{
							this._isDisabledForAI = true;
						}
					}
				}
			}
			switch (this.State)
			{
			case RangedSiegeWeapon.WeaponState.Idle:
			case RangedSiegeWeapon.WeaponState.WaitingAfterShooting:
			case RangedSiegeWeapon.WeaponState.LoadingAmmo:
			case RangedSiegeWeapon.WeaponState.WaitingBeforeIdle:
				return;
			case RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving:
				goto IL_406;
			case RangedSiegeWeapon.WeaponState.Shooting:
				for (int i = 0; i < this.Skeletons.Length; i++)
				{
					int animationIndexAtChannel = this.Skeletons[i].GetAnimationIndexAtChannel(0);
					float animationParameterAtChannel = this.Skeletons[i].GetAnimationParameterAtChannel(0);
					if (animationIndexAtChannel == this.FireAnimationIndices[i] && animationParameterAtChannel >= 0.9999f)
					{
						this.State = ((!this.AttackClickWillReload) ? RangedSiegeWeapon.WeaponState.WaitingBeforeReloading : RangedSiegeWeapon.WeaponState.WaitingAfterShooting);
						this.animationTimeElapsed = 0f;
					}
				}
				return;
			case RangedSiegeWeapon.WeaponState.WaitingBeforeReloading:
				break;
			case RangedSiegeWeapon.WeaponState.Reloading:
			{
				int num = 0;
				if (this.ReloadStandingPoints.Count == 0)
				{
					if (base.PilotAgent != null && !base.PilotAgent.IsInBeingStruckAction)
					{
						num = 1;
					}
				}
				else
				{
					foreach (StandingPoint standingPoint4 in this.ReloadStandingPoints)
					{
						if (standingPoint4.HasUser && !standingPoint4.UserAgent.IsInBeingStruckAction)
						{
							num++;
						}
					}
				}
				if (num == 0)
				{
					this.State = RangedSiegeWeapon.WeaponState.ReloadingPaused;
					return;
				}
				if (this._currentReloaderCount != num)
				{
					this._currentReloaderCount = num;
					float animationSpeed = MathF.Sqrt((float)this._currentReloaderCount);
					for (int j = 0; j < this.SkeletonOwnerObjects.Length; j++)
					{
						float animationParameterAtChannel2 = this.SkeletonOwnerObjects[j].GameEntity.Skeleton.GetAnimationParameterAtChannel(0);
						this.SkeletonOwnerObjects[j].SetAnimationAtChannelSynched(this.SetUpAnimations[j], 0, animationSpeed);
						if (animationParameterAtChannel2 > 0f)
						{
							this.SkeletonOwnerObjects[j].SetAnimationChannelParameterSynched(0, animationParameterAtChannel2);
						}
					}
				}
				for (int k = 0; k < this.Skeletons.Length; k++)
				{
					int animationIndexAtChannel2 = this.Skeletons[k].GetAnimationIndexAtChannel(0);
					float animationParameterAtChannel3 = this.Skeletons[k].GetAnimationParameterAtChannel(0);
					if (animationIndexAtChannel2 == this.SetUpAnimationIndices[k] && animationParameterAtChannel3 >= 0.9999f)
					{
						this.State = RangedSiegeWeapon.WeaponState.LoadingAmmo;
						this.animationTimeElapsed = 0f;
					}
				}
				return;
			}
			case RangedSiegeWeapon.WeaponState.ReloadingPaused:
				if (this.ReloadStandingPoints.Count == 0)
				{
					if (base.PilotAgent != null && !base.PilotAgent.IsInBeingStruckAction)
					{
						this.State = RangedSiegeWeapon.WeaponState.Reloading;
						return;
					}
					return;
				}
				else
				{
					using (List<StandingPoint>.Enumerator enumerator = this.ReloadStandingPoints.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StandingPoint standingPoint5 = enumerator.Current;
							if (standingPoint5.HasUser && !standingPoint5.UserAgent.IsInBeingStruckAction)
							{
								this.State = RangedSiegeWeapon.WeaponState.Reloading;
								break;
							}
						}
						return;
					}
				}
				break;
			default:
				Debug.FailedAssert("Invalid WeaponState.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\RangedSiegeWeapon.cs", "UpdateState", 1899);
				return;
			}
			this.animationTimeElapsed += dt;
			if (this.animationTimeElapsed < this.timeGapBetweenShootingEndAndReloadingStart || (this.cameraState != RangedSiegeWeapon.CameraState.RememberLastShotDirection && this.cameraState != RangedSiegeWeapon.CameraState.FreeMove && this.cameraState != RangedSiegeWeapon.CameraState.StickToWeapon && !(this.cameraHolder == null)))
			{
				return;
			}
			if (this.ReloadStandingPoints.Count == 0)
			{
				if (base.PilotAgent != null && !base.PilotAgent.IsInBeingStruckAction)
				{
					this.State = RangedSiegeWeapon.WeaponState.Reloading;
					return;
				}
				return;
			}
			else
			{
				using (List<StandingPoint>.Enumerator enumerator = this.ReloadStandingPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StandingPoint standingPoint6 = enumerator.Current;
						if (standingPoint6.HasUser && !standingPoint6.UserAgent.IsInBeingStruckAction)
						{
							this.State = RangedSiegeWeapon.WeaponState.Reloading;
							break;
						}
					}
					return;
				}
			}
			IL_406:
			this.animationTimeElapsed += dt;
			if (this.animationTimeElapsed >= this.timeGapBetweenShootActionAndProjectileLeaving)
			{
				this.State = RangedSiegeWeapon.WeaponState.Shooting;
				return;
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000B2C14 File Offset: 0x000B0E14
		public bool Shoot()
		{
			this._lastShooterAgent = base.PilotAgent;
			if (this.State == RangedSiegeWeapon.WeaponState.Idle)
			{
				this.State = RangedSiegeWeapon.WeaponState.WaitingBeforeProjectileLeaving;
				if (!GameNetwork.IsClientOrReplay)
				{
					this.animationTimeElapsed = 0f;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000B2C46 File Offset: 0x000B0E46
		public void ManualReload()
		{
			if (this.AttackClickWillReload)
			{
				this.State = RangedSiegeWeapon.WeaponState.WaitingBeforeReloading;
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000B2C57 File Offset: 0x000B0E57
		public void AiRequestsShoot()
		{
			this._aiRequestsShoot = true;
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B2C60 File Offset: 0x000B0E60
		public void AiRequestsManualReload()
		{
			this._aiRequestsManualReload = true;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000B2C6C File Offset: 0x000B0E6C
		private Vec3 GetBallisticErrorAppliedDirection(float BallisticErrorAmount)
		{
			Mat3 mat = new Mat3
			{
				f = this.ShootingDirection,
				u = Vec3.Up
			};
			mat.Orthonormalize();
			float a = MBRandom.RandomFloat * 6.2831855f;
			mat.RotateAboutForward(a);
			float f = BallisticErrorAmount * MBRandom.RandomFloat;
			mat.RotateAboutSide(f.ToRadians());
			return mat.f;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000B2CD4 File Offset: 0x000B0ED4
		private void ShootProjectile()
		{
			if (this.LoadedMissileItem.StringId == "grapeshot_fire_stack")
			{
				ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>("grapeshot_fire_projectile");
				for (int i = 0; i < 5; i++)
				{
					this.ShootProjectileAux(@object, true);
				}
			}
			else
			{
				this.ShootProjectileAux(this.LoadedMissileItem, false);
			}
			this._lastShooterAgent = null;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000B2D38 File Offset: 0x000B0F38
		private void ShootProjectileAux(ItemObject missileItem, bool randomizeMissileSpeed)
		{
			Mat3 identity = Mat3.Identity;
			float num = this.ShootingSpeed;
			if (randomizeMissileSpeed)
			{
				num *= MBRandom.RandomFloatRanged(0.9f, 1.1f);
				identity.f = this.GetBallisticErrorAppliedDirection(2.5f);
				identity.Orthonormalize();
			}
			else
			{
				identity.f = this.GetBallisticErrorAppliedDirection(this.MaximumBallisticError);
				identity.Orthonormalize();
			}
			Mission mission = Mission.Current;
			Agent lastShooterAgent = this._lastShooterAgent;
			ItemModifier itemModifier = null;
			IAgentOriginBase origin = this._lastShooterAgent.Origin;
			mission.AddCustomMissile(lastShooterAgent, new MissionWeapon(missileItem, itemModifier, (origin != null) ? origin.Banner : null, 1), this.ProjectileEntityCurrentGlobalPosition, identity.f, identity, (float)this.LoadedMissileItem.PrimaryWeapon.MissileSpeed, num, false, this, -1);
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000B2DED File Offset: 0x000B0FED
		protected virtual Vec3 ShootingDirection
		{
			get
			{
				return this.Projectile.GameEntity.GetGlobalFrame().rotation.u;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000B2E09 File Offset: 0x000B1009
		public virtual Vec3 ProjectileEntityCurrentGlobalPosition
		{
			get
			{
				return this.Projectile.GameEntity.GetGlobalFrame().origin;
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000B2E20 File Offset: 0x000B1020
		protected void OnRotationStarted()
		{
			if (this.MoveSound == null || !this.MoveSound.IsValid)
			{
				this.MoveSound = SoundEvent.CreateEvent(this.MoveSoundIndex, base.Scene);
				this.MoveSound.PlayInPosition(this.RotationObject.GameEntity.GlobalPosition);
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000B2E75 File Offset: 0x000B1075
		protected void OnRotationStopped()
		{
			this.MoveSound.Stop();
			this.MoveSound = null;
		}

		// Token: 0x06002CF7 RID: 11511
		public abstract override SiegeEngineType GetSiegeEngineType();

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000B2E89 File Offset: 0x000B1089
		public override BattleSideEnum Side
		{
			get
			{
				if (base.PilotAgent != null)
				{
					return base.PilotAgent.Team.Side;
				}
				return this._defaultSide;
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000B2EAC File Offset: 0x000B10AC
		public bool CanShootAtBox(Vec3 boxMin, Vec3 boxMax, uint attempts = 5U)
		{
			Vec3 v;
			Vec3 vec = v = (boxMin + boxMax) / 2f;
			v.z = boxMin.z;
			Vec3 v2 = vec;
			v2.z = boxMax.z;
			uint num = attempts;
			for (;;)
			{
				Vec3 target = Vec3.Lerp(v, v2, num / attempts);
				if (this.CanShootAtPoint(target))
				{
					break;
				}
				num -= 1U;
				if (num <= 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000B2F0C File Offset: 0x000B110C
		public bool CanShootAtBoxSimplified(Vec3 boxMin, Vec3 boxMax)
		{
			Vec3 vec = (boxMin + boxMax) / 2f;
			Vec3 target = vec;
			target.z = boxMax.z;
			return this.CanShootAtPoint(vec) || this.CanShootAtPoint(target);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B2F4C File Offset: 0x000B114C
		public bool CanShootAtThreat(Threat threat)
		{
			Vec3 targetingOffset = threat.WeaponEntity.GetTargetingOffset();
			Vec3 vec = threat.BoundingBoxMax + targetingOffset;
			Vec3 v = threat.BoundingBoxMin + targetingOffset;
			Vec3 vec2 = (vec + v) * 0.5f;
			Vec3 estimatedTargetMovementVector = this.GetEstimatedTargetMovementVector(vec2, threat.GetVelocity());
			vec2 += estimatedTargetMovementVector;
			vec += estimatedTargetMovementVector;
			Vec3 target = vec2;
			target.z = vec.z;
			return this.CanShootAtPoint(vec2) || this.CanShootAtPoint(target);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000B2FD8 File Offset: 0x000B11D8
		public Vec3 GetEstimatedTargetMovementVector(Vec3 targetCurrentPosition, Vec3 targetVelocity)
		{
			if (targetVelocity != Vec3.Zero)
			{
				return targetVelocity * ((base.GameEntity.GlobalPosition - targetCurrentPosition).Length / this.ShootingSpeed + this.timeGapBetweenShootActionAndProjectileLeaving);
			}
			return Vec3.Zero;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000B3028 File Offset: 0x000B1228
		public bool CanShootAtAgent(Agent agent)
		{
			Vec3 boxMax = agent.CollisionCapsule.GetBoxMax();
			Vec3 target = (agent.CollisionCapsule.GetBoxMin() + boxMax) / 2f;
			return this.CanShootAtPoint(target);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000B306C File Offset: 0x000B126C
		public unsafe bool CanShootAtPoint(Vec3 target)
		{
			float num = this.GetTargetReleaseAngle(target);
			if (num < this.BottomReleaseAngleRestriction || num > this.TopReleaseAngleRestriction)
			{
				return false;
			}
			float f = (target.AsVec2 - this.ProjectileEntityCurrentGlobalPosition.AsVec2).Normalized().AngleBetween(this.OriginalDirection.AsVec2.Normalized());
			if (this.DirectionRestriction / 2f - MathF.Abs(f) < 0f)
			{
				return false;
			}
			if (this.Side == BattleSideEnum.Attacker)
			{
				foreach (SiegeWeapon siegeWeapon in *Mission.Current.GetAttackerWeaponsForFriendlyFirePreventing())
				{
					if (siegeWeapon.GameEntity != null && siegeWeapon.GameEntity.IsVisibleIncludeParents())
					{
						Vec3 vec = (siegeWeapon.GameEntity.PhysicsGlobalBoxMin + siegeWeapon.GameEntity.PhysicsGlobalBoxMax) * 0.5f;
						if ((MBMath.GetClosestPointInLineSegmentToPoint(vec, this.MissleStartingPositionForSimulation, target) - vec).LengthSquared < 100f)
						{
							return false;
						}
					}
				}
			}
			Vec3 missleStartingPositionForSimulation = this.MissleStartingPositionForSimulation;
			Vec3 v = (this.MissileStartingPositionEntityForSimulation == null) ? this.CanShootAtPointCheckingOffset : Vec3.Zero;
			Vec3 target2 = target;
			return base.Scene.CheckPointCanSeePoint(missleStartingPositionForSimulation + v, target2, null);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000B3204 File Offset: 0x000B1404
		protected internal virtual bool IsTargetValid(ITargetable target)
		{
			return true;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000B3207 File Offset: 0x000B1407
		public override OrderType GetOrder(BattleSideEnum side)
		{
			if (base.IsDestroyed)
			{
				return OrderType.None;
			}
			if (this.Side != side)
			{
				return OrderType.AttackEntity;
			}
			return OrderType.Use;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000B3221 File Offset: 0x000B1421
		protected override GameEntity GetEntityToAttachNavMeshFaces()
		{
			return this.RotationObject.GameEntity;
		}

		// Token: 0x06002D02 RID: 11522
		public abstract float ProcessTargetValue(float baseValue, TargetFlags flags);

		// Token: 0x06002D03 RID: 11523 RVA: 0x000B3230 File Offset: 0x000B1430
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord);
			RangedSiegeWeapon.RangedSiegeWeaponRecord rangedSiegeWeaponRecord = (RangedSiegeWeapon.RangedSiegeWeaponRecord)synchedMissionObjectReadableRecord.Item2;
			this._state = (RangedSiegeWeapon.WeaponState)rangedSiegeWeaponRecord.State;
			this.targetDirection = rangedSiegeWeaponRecord.TargetDirection;
			this.targetReleaseAngle = MBMath.ClampFloat(rangedSiegeWeaponRecord.TargetReleaseAngle, this.BottomReleaseAngleRestriction, this.TopReleaseAngleRestriction);
			this.AmmoCount = rangedSiegeWeaponRecord.AmmoCount;
			this.currentDirection = this.targetDirection;
			this.currentReleaseAngle = this.targetReleaseAngle;
			this.currentDirection = this.targetDirection;
			this.currentReleaseAngle = this.targetReleaseAngle;
			this.ApplyCurrentDirectionToEntity();
			this.CheckAmmo();
			this.UpdateAmmoMesh();
			this.ChangeProjectileEntityClient(rangedSiegeWeaponRecord.ProjectileIndex);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000B32E4 File Offset: 0x000B14E4
		protected virtual void UpdateAmmoMesh()
		{
			GameEntity gameEntity = this.AmmoPickUpStandingPoints[0].GameEntity;
			int num = 20 - this.AmmoCount;
			while (gameEntity.Parent != null)
			{
				for (int i = 0; i < gameEntity.MultiMeshComponentCount; i++)
				{
					MetaMesh metaMesh = gameEntity.GetMetaMesh(i);
					for (int j = 0; j < metaMesh.MeshCount; j++)
					{
						metaMesh.GetMeshAtIndex(j).SetVectorArgument(0f, (float)num, 0f, 0f);
					}
				}
				gameEntity = gameEntity.Parent;
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000B3370 File Offset: 0x000B1570
		protected override bool IsAnyUserBelongsToFormation(Formation formation)
		{
			bool flag = base.IsAnyUserBelongsToFormation(formation);
			Agent reloaderAgent = this.ReloaderAgent;
			return flag | ((reloaderAgent != null) ? reloaderAgent.Formation : null) == formation;
		}

		// Token: 0x04001209 RID: 4617
		public const float DefaultDirectionRestriction = 2.0943952f;

		// Token: 0x0400120A RID: 4618
		public const string MultipleProjectileId = "grapeshot_fire_stack";

		// Token: 0x0400120B RID: 4619
		public const string MultipleProjectileFlyingId = "grapeshot_fire_projectile";

		// Token: 0x0400120C RID: 4620
		public const int MultipleProjectileCount = 5;

		// Token: 0x0400120D RID: 4621
		public const string CanGoAmmoPickupTag = "can_pick_up_ammo";

		// Token: 0x0400120E RID: 4622
		public const string DontApplySidePenaltyTag = "no_ammo_pick_up_penalty";

		// Token: 0x0400120F RID: 4623
		public const string ReloadTag = "reload";

		// Token: 0x04001210 RID: 4624
		public const string AmmoLoadTag = "ammoload";

		// Token: 0x04001211 RID: 4625
		public const string CameraHolderTag = "cameraHolder";

		// Token: 0x04001212 RID: 4626
		public const string ProjectileTag = "projectile";

		// Token: 0x04001213 RID: 4627
		public string MissileItemID;

		// Token: 0x04001214 RID: 4628
		protected bool UsesMouseForAiming;

		// Token: 0x04001215 RID: 4629
		private RangedSiegeWeapon.WeaponState _state;

		// Token: 0x04001216 RID: 4630
		public RangedSiegeWeapon.FiringFocus Focus;

		// Token: 0x04001219 RID: 4633
		private int _projectileIndex;

		// Token: 0x0400121A RID: 4634
		protected GameEntity MissileStartingPositionEntityForSimulation;

		// Token: 0x0400121B RID: 4635
		protected Skeleton[] Skeletons;

		// Token: 0x0400121C RID: 4636
		protected SynchedMissionObject[] SkeletonOwnerObjects;

		// Token: 0x0400121D RID: 4637
		protected string[] SkeletonNames;

		// Token: 0x0400121E RID: 4638
		protected string[] FireAnimations;

		// Token: 0x0400121F RID: 4639
		protected string[] SetUpAnimations;

		// Token: 0x04001220 RID: 4640
		protected int[] FireAnimationIndices;

		// Token: 0x04001221 RID: 4641
		protected int[] SetUpAnimationIndices;

		// Token: 0x04001222 RID: 4642
		protected SynchedMissionObject RotationObject;

		// Token: 0x04001223 RID: 4643
		private MatrixFrame _rotationObjectInitialFrame;

		// Token: 0x04001224 RID: 4644
		protected SoundEvent MoveSound;

		// Token: 0x04001225 RID: 4645
		protected SoundEvent ReloadSound;

		// Token: 0x04001226 RID: 4646
		protected int MoveSoundIndex = -1;

		// Token: 0x04001227 RID: 4647
		protected int ReloadSoundIndex = -1;

		// Token: 0x04001228 RID: 4648
		protected int ReloadEndSoundIndex = -1;

		// Token: 0x04001229 RID: 4649
		protected ItemObject OriginalMissileItem;

		// Token: 0x0400122A RID: 4650
		private WeaponStatsData _originalMissileWeaponStatsDataForTargeting;

		// Token: 0x0400122B RID: 4651
		protected ItemObject LoadedMissileItem;

		// Token: 0x0400122C RID: 4652
		protected List<StandingPoint> CanPickUpAmmoStandingPoints;

		// Token: 0x0400122D RID: 4653
		protected List<StandingPoint> ReloadStandingPoints;

		// Token: 0x0400122E RID: 4654
		protected List<StandingPointWithWeaponRequirement> AmmoPickUpStandingPoints;

		// Token: 0x0400122F RID: 4655
		protected StandingPointWithWeaponRequirement LoadAmmoStandingPoint;

		// Token: 0x04001230 RID: 4656
		protected Dictionary<StandingPoint, float> PilotReservePriorityValues = new Dictionary<StandingPoint, float>();

		// Token: 0x04001231 RID: 4657
		protected Agent ReloaderAgent;

		// Token: 0x04001232 RID: 4658
		protected StandingPoint ReloaderAgentOriginalPoint;

		// Token: 0x04001234 RID: 4660
		protected bool AttackClickWillReload;

		// Token: 0x04001235 RID: 4661
		protected bool WeaponNeedsClickToReload;

		// Token: 0x04001236 RID: 4662
		public int startingAmmoCount = 20;

		// Token: 0x04001237 RID: 4663
		protected int CurrentAmmo = 1;

		// Token: 0x04001238 RID: 4664
		private bool _hasAmmo = true;

		// Token: 0x04001239 RID: 4665
		protected float targetDirection;

		// Token: 0x0400123A RID: 4666
		protected float targetReleaseAngle;

		// Token: 0x0400123B RID: 4667
		protected float cameraDirection;

		// Token: 0x0400123C RID: 4668
		protected float cameraReleaseAngle;

		// Token: 0x0400123D RID: 4669
		protected float reloadTargetReleaseAngle;

		// Token: 0x0400123E RID: 4670
		protected float maxRotateSpeed;

		// Token: 0x0400123F RID: 4671
		protected float dontMoveTimer;

		// Token: 0x04001240 RID: 4672
		private MatrixFrame cameraHolderInitialFrame;

		// Token: 0x04001241 RID: 4673
		private RangedSiegeWeapon.CameraState cameraState;

		// Token: 0x04001242 RID: 4674
		private bool _inputGiven;

		// Token: 0x04001243 RID: 4675
		private float _inputX;

		// Token: 0x04001244 RID: 4676
		private float _inputY;

		// Token: 0x04001245 RID: 4677
		private bool _exactInputGiven;

		// Token: 0x04001246 RID: 4678
		private float _inputTargetX;

		// Token: 0x04001247 RID: 4679
		private float _inputTargetY;

		// Token: 0x04001248 RID: 4680
		private Vec3 _ammoPickupCenter;

		// Token: 0x04001249 RID: 4681
		protected float currentDirection;

		// Token: 0x0400124A RID: 4682
		private Vec3 _originalDirection;

		// Token: 0x0400124B RID: 4683
		protected float currentReleaseAngle;

		// Token: 0x0400124C RID: 4684
		private float _lastSyncedDirection;

		// Token: 0x0400124D RID: 4685
		private float _lastSyncedReleaseAngle;

		// Token: 0x0400124E RID: 4686
		private float _syncTimer;

		// Token: 0x0400124F RID: 4687
		public float TopReleaseAngleRestriction = 1.5707964f;

		// Token: 0x04001250 RID: 4688
		public float BottomReleaseAngleRestriction = -1.5707964f;

		// Token: 0x04001251 RID: 4689
		protected float ReleaseAngleRestrictionCenter;

		// Token: 0x04001252 RID: 4690
		protected float ReleaseAngleRestrictionAngle;

		// Token: 0x04001253 RID: 4691
		private float animationTimeElapsed;

		// Token: 0x04001254 RID: 4692
		protected float timeGapBetweenShootingEndAndReloadingStart = 0.6f;

		// Token: 0x04001255 RID: 4693
		protected float timeGapBetweenShootActionAndProjectileLeaving;

		// Token: 0x04001256 RID: 4694
		private int _currentReloaderCount;

		// Token: 0x04001257 RID: 4695
		private Agent _lastShooterAgent;

		// Token: 0x04001258 RID: 4696
		private float _lastCanPickUpAmmoStandingPointsSortedAngle = -3.1415927f;

		// Token: 0x04001259 RID: 4697
		protected BattleSideEnum _defaultSide;

		// Token: 0x0400125A RID: 4698
		private bool hasFrameChangedInPreviousFrame;

		// Token: 0x0400125B RID: 4699
		protected SiegeMachineStonePile _stonePile;

		// Token: 0x0400125C RID: 4700
		private bool _aiRequestsShoot;

		// Token: 0x0400125D RID: 4701
		private bool _aiRequestsManualReload;

		// Token: 0x020005EF RID: 1519
		[DefineSynchedMissionObjectType(typeof(RangedSiegeWeapon))]
		public struct RangedSiegeWeaponRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x170009DF RID: 2527
			// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x000E94C5 File Offset: 0x000E76C5
			// (set) Token: 0x06003BBA RID: 15290 RVA: 0x000E94CD File Offset: 0x000E76CD
			public int State { get; private set; }

			// Token: 0x170009E0 RID: 2528
			// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000E94D6 File Offset: 0x000E76D6
			// (set) Token: 0x06003BBC RID: 15292 RVA: 0x000E94DE File Offset: 0x000E76DE
			public float TargetDirection { get; private set; }

			// Token: 0x170009E1 RID: 2529
			// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000E94E7 File Offset: 0x000E76E7
			// (set) Token: 0x06003BBE RID: 15294 RVA: 0x000E94EF File Offset: 0x000E76EF
			public float TargetReleaseAngle { get; private set; }

			// Token: 0x170009E2 RID: 2530
			// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000E94F8 File Offset: 0x000E76F8
			// (set) Token: 0x06003BC0 RID: 15296 RVA: 0x000E9500 File Offset: 0x000E7700
			public int AmmoCount { get; private set; }

			// Token: 0x170009E3 RID: 2531
			// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000E9509 File Offset: 0x000E7709
			// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x000E9511 File Offset: 0x000E7711
			public int ProjectileIndex { get; private set; }

			// Token: 0x06003BC3 RID: 15299 RVA: 0x000E951C File Offset: 0x000E771C
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.State = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponStateCompressionInfo, ref bufferReadValid);
				this.TargetDirection = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.RadianCompressionInfo, ref bufferReadValid);
				this.TargetReleaseAngle = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.RadianCompressionInfo, ref bufferReadValid);
				this.AmmoCount = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponAmmoCompressionInfo, ref bufferReadValid);
				this.ProjectileIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.RangedSiegeWeaponAmmoIndexCompressionInfo, ref bufferReadValid);
				return bufferReadValid;
			}
		}

		// Token: 0x020005F0 RID: 1520
		public enum WeaponState
		{
			// Token: 0x04001EFC RID: 7932
			Invalid = -1,
			// Token: 0x04001EFD RID: 7933
			Idle,
			// Token: 0x04001EFE RID: 7934
			WaitingBeforeProjectileLeaving,
			// Token: 0x04001EFF RID: 7935
			Shooting,
			// Token: 0x04001F00 RID: 7936
			WaitingAfterShooting,
			// Token: 0x04001F01 RID: 7937
			WaitingBeforeReloading,
			// Token: 0x04001F02 RID: 7938
			LoadingAmmo,
			// Token: 0x04001F03 RID: 7939
			WaitingBeforeIdle,
			// Token: 0x04001F04 RID: 7940
			Reloading,
			// Token: 0x04001F05 RID: 7941
			ReloadingPaused,
			// Token: 0x04001F06 RID: 7942
			NumberOfStates
		}

		// Token: 0x020005F1 RID: 1521
		public enum FiringFocus
		{
			// Token: 0x04001F08 RID: 7944
			Troops,
			// Token: 0x04001F09 RID: 7945
			Walls,
			// Token: 0x04001F0A RID: 7946
			RangedSiegeWeapons,
			// Token: 0x04001F0B RID: 7947
			PrimarySiegeWeapons
		}

		// Token: 0x020005F2 RID: 1522
		// (Invoke) Token: 0x06003BC5 RID: 15301
		public delegate void OnSiegeWeaponReloadDone();

		// Token: 0x020005F3 RID: 1523
		public enum CameraState
		{
			// Token: 0x04001F0D RID: 7949
			StickToWeapon,
			// Token: 0x04001F0E RID: 7950
			DontMove,
			// Token: 0x04001F0F RID: 7951
			MoveDownToReload,
			// Token: 0x04001F10 RID: 7952
			RememberLastShotDirection,
			// Token: 0x04001F11 RID: 7953
			FreeMove,
			// Token: 0x04001F12 RID: 7954
			ApproachToCamera
		}
	}
}
