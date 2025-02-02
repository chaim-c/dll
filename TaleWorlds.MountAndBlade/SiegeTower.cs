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
	// Token: 0x0200033F RID: 831
	public class SiegeTower : SiegeWeapon, IPathHolder, IPrimarySiegeWeapon, IMoveableSiegeWeapon, ISpawnable
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000B60C0 File Offset: 0x000B42C0
		public MissionObject TargetCastlePosition
		{
			get
			{
				return this._targetWallSegment;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000B60C8 File Offset: 0x000B42C8
		// (set) Token: 0x06002D3F RID: 11583 RVA: 0x000B60E5 File Offset: 0x000B42E5
		private GameEntity CleanState
		{
			get
			{
				if (!(this._cleanState == null))
				{
					return this._cleanState;
				}
				return base.GameEntity;
			}
			set
			{
				this._cleanState = value;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x000B60EE File Offset: 0x000B42EE
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x000B60F6 File Offset: 0x000B42F6
		public FormationAI.BehaviorSide WeaponSide { get; private set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x000B60FF File Offset: 0x000B42FF
		// (set) Token: 0x06002D43 RID: 11587 RVA: 0x000B6107 File Offset: 0x000B4307
		public string PathEntity { get; private set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000B6110 File Offset: 0x000B4310
		public bool EditorGhostEntityMove
		{
			get
			{
				return this.GhostEntityMove;
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000B6118 File Offset: 0x000B4318
		public bool HasCompletedAction()
		{
			return !base.IsDisabled && this.IsDeactivated && this._hasArrivedAtTarget && !base.IsDestroyed;
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x000B613D File Offset: 0x000B433D
		public float SiegeWeaponPriority
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000B6144 File Offset: 0x000B4344
		public int OverTheWallNavMeshID
		{
			get
			{
				return this.GetGateNavMeshId();
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000B614C File Offset: 0x000B434C
		// (set) Token: 0x06002D49 RID: 11593 RVA: 0x000B6154 File Offset: 0x000B4354
		public SiegeWeaponMovementComponent MovementComponent { get; private set; }

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x000B615D File Offset: 0x000B435D
		public bool HoldLadders
		{
			get
			{
				return !this.MovementComponent.HasArrivedAtTarget;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000B616D File Offset: 0x000B436D
		public bool SendLadders
		{
			get
			{
				return this.MovementComponent.HasArrivedAtTarget;
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000B617A File Offset: 0x000B437A
		public int GetGateNavMeshId()
		{
			if (this.GateNavMeshId != 0)
			{
				return this.GateNavMeshId;
			}
			if (this.DynamicNavmeshIdStart == 0)
			{
				return 0;
			}
			return this.DynamicNavmeshIdStart + 3;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000B61A0 File Offset: 0x000B43A0
		public List<int> CollectGetDifficultNavmeshIDs()
		{
			List<int> list = new List<int>();
			if (!this._hasLadders)
			{
				return list;
			}
			list.Add(this.DynamicNavmeshIdStart + 1);
			list.Add(this.DynamicNavmeshIdStart + 5);
			list.Add(this.DynamicNavmeshIdStart + 6);
			list.Add(this.DynamicNavmeshIdStart + 7);
			return list;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000B61F8 File Offset: 0x000B43F8
		public List<int> CollectGetDifficultNavmeshIDsForAttackers()
		{
			List<int> list = new List<int>();
			if (!this._hasLadders)
			{
				return list;
			}
			list = this.CollectGetDifficultNavmeshIDs();
			list.Add(this.DynamicNavmeshIdStart + 3);
			return list;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B622C File Offset: 0x000B442C
		public List<int> CollectGetDifficultNavmeshIDsForDefenders()
		{
			List<int> list = new List<int>();
			if (!this._hasLadders)
			{
				return list;
			}
			list = this.CollectGetDifficultNavmeshIDs();
			list.Add(this.DynamicNavmeshIdStart + 2);
			return list;
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x000B625F File Offset: 0x000B445F
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x000B6268 File Offset: 0x000B4468
		public bool HasArrivedAtTarget
		{
			get
			{
				return this._hasArrivedAtTarget;
			}
			set
			{
				if (!GameNetwork.IsClientOrReplay)
				{
					this.MovementComponent.SetDestinationNavMeshIdState(!this.HasArrivedAtTarget);
				}
				if (this._hasArrivedAtTarget != value)
				{
					this._hasArrivedAtTarget = value;
					if (this._hasArrivedAtTarget)
					{
						this.ActiveWaitStandingPoint = base.WaitStandingPoints[1];
						if (GameNetwork.IsClientOrReplay)
						{
							goto IL_C2;
						}
						using (List<LadderQueueManager>.Enumerator enumerator = this._queueManagers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								LadderQueueManager ladderQueueManager = enumerator.Current;
								this.CleanState.Scene.SetAbilityOfFacesWithId(ladderQueueManager.ManagedNavigationFaceId, true);
								ladderQueueManager.Activate();
							}
							goto IL_C2;
						}
					}
					if (!GameNetwork.IsClientOrReplay && this.GetGateNavMeshId() > 0)
					{
						this.CleanState.Scene.SetAbilityOfFacesWithId(this.GetGateNavMeshId(), false);
					}
					IL_C2:
					if (GameNetwork.IsServerOrRecorder)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new SetSiegeTowerHasArrivedAtTarget(base.Id));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
						return;
					}
					if (GameNetwork.IsClientOrReplay)
					{
						this.MovementComponent.MoveToTargetAsClient();
					}
				}
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000B6380 File Offset: 0x000B4580
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000B6388 File Offset: 0x000B4588
		public SiegeTower.GateState State
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
						GameNetwork.WriteMessage(new SetSiegeTowerGateState(base.Id, value));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
					}
					this._state = value;
					this.OnSiegeTowerGateStateChange();
				}
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000B63C8 File Offset: 0x000B45C8
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			if (gameEntity == null || !gameEntity.HasScriptOfType<UsableMissionObject>() || gameEntity.HasTag("move"))
			{
				return new TextObject("{=aXjlMBiE}Siege Tower", null).ToString();
			}
			return new TextObject("{=6wZUG0ev}Gate", null).ToString();
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000B6414 File Offset: 0x000B4614
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = usableGameObject.GameEntity.HasTag("move") ? new TextObject("{=rwZAZSvX}{KEY} Move", null) : new TextObject("{=5oozsaIb}{KEY} Open", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000B6468 File Offset: 0x000B4668
		public override void WriteToNetwork()
		{
			base.WriteToNetwork();
			GameNetworkMessage.WriteBoolToPacket(this.HasArrivedAtTarget);
			GameNetworkMessage.WriteIntToPacket((int)this.State, CompressionMission.SiegeTowerGateStateCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this._fallAngularSpeed, CompressionMission.SiegeMachineComponentAngularSpeedCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.MovementComponent.GetTotalDistanceTraveledForPathTracker(), CompressionBasic.PositionCompressionInfo);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000B64BB File Offset: 0x000B46BB
		public override OrderType GetOrder(BattleSideEnum side)
		{
			if (base.IsDestroyed)
			{
				return OrderType.None;
			}
			if (side != BattleSideEnum.Attacker)
			{
				return OrderType.AttackEntity;
			}
			if (this.HasCompletedAction())
			{
				return OrderType.Use;
			}
			return OrderType.FollowEntity;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000B64DC File Offset: 0x000B46DC
		public override TargetFlags GetTargetFlags()
		{
			TargetFlags targetFlags = TargetFlags.None;
			if (base.UserCountNotInStruckAction > 0)
			{
				targetFlags |= TargetFlags.IsMoving;
			}
			targetFlags |= TargetFlags.IsSiegeEngine;
			targetFlags |= TargetFlags.IsAttacker;
			if (this.HasCompletedAction() || base.IsDestroyed || this.IsDeactivated)
			{
				targetFlags |= TargetFlags.NotAThreat;
			}
			if (this.Side == BattleSideEnum.Attacker && DebugSiegeBehavior.DebugDefendState == DebugSiegeBehavior.DebugStateDefender.DebugDefendersToTower)
			{
				targetFlags |= TargetFlags.DebugThreat;
			}
			return targetFlags | TargetFlags.IsSiegeTower;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000B6540 File Offset: 0x000B4740
		public override float GetTargetValue(List<Vec3> weaponPos)
		{
			return 90f * base.GetUserMultiplierOfWeapon() * this.GetDistanceMultiplierOfWeapon(weaponPos[0]) * base.GetHitPointMultiplierOfWeapon();
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000B6564 File Offset: 0x000B4764
		public override void Disable()
		{
			base.Disable();
			this.SetAbilityOfFaces(false);
			if (this._queueManagers != null)
			{
				foreach (LadderQueueManager ladderQueueManager in this._queueManagers)
				{
					this.CleanState.Scene.SetAbilityOfFacesWithId(ladderQueueManager.ManagedNavigationFaceId, false);
					ladderQueueManager.DeactivateImmediate();
				}
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000B65E4 File Offset: 0x000B47E4
		public override SiegeEngineType GetSiegeEngineType()
		{
			return DefaultSiegeEngineTypes.SiegeTower;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000B65EB File Offset: 0x000B47EB
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new SiegeTowerAI(this);
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000B65F3 File Offset: 0x000B47F3
		public override bool IsDeactivated
		{
			get
			{
				return (this.MovementComponent.HasArrivedAtTarget && this.State == SiegeTower.GateState.Open) || base.IsDeactivated;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000B6614 File Offset: 0x000B4814
		protected internal override void OnDeploymentStateChanged(bool isDeployed)
		{
			base.OnDeploymentStateChanged(isDeployed);
			if (this._ditchFillDebris != null)
			{
				if (!GameNetwork.IsClientOrReplay)
				{
					this._ditchFillDebris.SetVisibleSynched(isDeployed, false);
				}
				if (!GameNetwork.IsClientOrReplay)
				{
					if (isDeployed)
					{
						if (this._soilGenericNavMeshID > 0)
						{
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._soilGenericNavMeshID, true);
						}
						if (this._soilNavMeshID1 > 0 && this._groundToSoilNavMeshID1 > 0 && this._ditchNavMeshID1 > 0)
						{
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._soilNavMeshID1, true);
							Mission.Current.Scene.SwapFaceConnectionsWithID(this._groundToSoilNavMeshID1, this._ditchNavMeshID1, this._soilNavMeshID1);
						}
						if (this._soilNavMeshID2 > 0 && this._groundToSoilNavMeshID2 > 0 && this._ditchNavMeshID2 > 0)
						{
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._soilNavMeshID2, true);
							Mission.Current.Scene.SwapFaceConnectionsWithID(this._groundToSoilNavMeshID2, this._ditchNavMeshID2, this._soilNavMeshID2);
						}
						if (this._groundGenericNavMeshID > 0)
						{
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._groundGenericNavMeshID, false);
						}
					}
					else
					{
						if (this._groundGenericNavMeshID > 0)
						{
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._groundGenericNavMeshID, true);
						}
						if (this._soilNavMeshID1 > 0 && this._groundToSoilNavMeshID1 > 0 && this._ditchNavMeshID1 > 0)
						{
							Mission.Current.Scene.SwapFaceConnectionsWithID(this._groundToSoilNavMeshID1, this._soilNavMeshID1, this._ditchNavMeshID1);
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._soilNavMeshID1, false);
						}
						if (this._soilNavMeshID2 > 0 && this._groundToSoilNavMeshID2 > 0 && this._ditchNavMeshID2 > 0)
						{
							Mission.Current.Scene.SwapFaceConnectionsWithID(this._groundToSoilNavMeshID2, this._soilNavMeshID2, this._ditchNavMeshID2);
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._soilNavMeshID2, false);
						}
						if (this._soilGenericNavMeshID > 0)
						{
							Mission.Current.Scene.SetAbilityOfFacesWithId(this._soilGenericNavMeshID, false);
						}
					}
				}
			}
			if (this._sameSideSiegeLadders == null)
			{
				this._sameSideSiegeLadders = (from sl in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeLadder>()
				where sl.WeaponSide == this.WeaponSide
				select sl).ToList<SiegeLadder>();
			}
			foreach (SiegeLadder siegeLadder in this._sameSideSiegeLadders)
			{
				siegeLadder.GameEntity.SetVisibilityExcludeParents(!isDeployed);
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000B68A4 File Offset: 0x000B4AA4
		protected override void AttachDynamicNavmeshToEntity()
		{
			if (this.NavMeshPrefabName.Length > 0)
			{
				this.DynamicNavmeshIdStart = Mission.Current.GetNextDynamicNavMeshIdStart();
				this.CleanState.Scene.ImportNavigationMeshPrefab(this.NavMeshPrefabName, this.DynamicNavmeshIdStart);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 1, false, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 2, true, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 4, false, true, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 5, false, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 6, false, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 7, false, false, false);
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000B6972 File Offset: 0x000B4B72
		protected override GameEntity GetEntityToAttachNavMeshFaces()
		{
			return this.CleanState;
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000B697A File Offset: 0x000B4B7A
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			SiegeWeaponMovementComponent movementComponent = this.MovementComponent;
			if (movementComponent == null)
			{
				return;
			}
			movementComponent.OnRemoved();
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000B6994 File Offset: 0x000B4B94
		public override void SetAbilityOfFaces(bool enabled)
		{
			base.SetAbilityOfFaces(enabled);
			if (this._queueManagers != null)
			{
				foreach (LadderQueueManager ladderQueueManager in this._queueManagers)
				{
					this.CleanState.Scene.SetAbilityOfFacesWithId(ladderQueueManager.ManagedNavigationFaceId, enabled);
					if (ladderQueueManager.IsDeactivated != !enabled)
					{
						if (enabled)
						{
							ladderQueueManager.Activate();
						}
						else
						{
							ladderQueueManager.DeactivateImmediate();
						}
					}
				}
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000B6A24 File Offset: 0x000B4C24
		protected override float GetDistanceMultiplierOfWeapon(Vec3 weaponPos)
		{
			float minimumDistanceBetweenPositions = this.GetMinimumDistanceBetweenPositions(weaponPos);
			if (minimumDistanceBetweenPositions < 10f)
			{
				return 1f;
			}
			if (minimumDistanceBetweenPositions < 25f)
			{
				return 0.8f;
			}
			return 0.6f;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000B6A5C File Offset: 0x000B4C5C
		private bool IsNavmeshOnThisTowerAttackerDifficultNavmeshIDs(int testedNavmeshID)
		{
			return this._hasLadders && (testedNavmeshID == this.DynamicNavmeshIdStart + 1 || testedNavmeshID == this.DynamicNavmeshIdStart + 5 || testedNavmeshID == this.DynamicNavmeshIdStart + 6 || testedNavmeshID == this.DynamicNavmeshIdStart + 7 || testedNavmeshID == this.DynamicNavmeshIdStart + 3);
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000B6AAC File Offset: 0x000B4CAC
		protected override bool IsAgentOnInconvenientNavmesh(Agent agent, StandingPoint standingPoint)
		{
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege)
			{
				return false;
			}
			int currentNavigationFaceId = agent.GetCurrentNavigationFaceId();
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = (agent.Team.TeamAI as TeamAISiegeComponent)) != null)
			{
				if (teamAISiegeComponent is TeamAISiegeDefender && currentNavigationFaceId % 10 != 1)
				{
					return true;
				}
				foreach (int num in teamAISiegeComponent.DifficultNavmeshIDs)
				{
					if (currentNavigationFaceId == num)
					{
						return standingPoint != this._gateStandingPoint || !this.IsNavmeshOnThisTowerAttackerDifficultNavmeshIDs(currentNavigationFaceId);
					}
				}
				if (teamAISiegeComponent is TeamAISiegeAttacker && currentNavigationFaceId % 10 == 1)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000B6B68 File Offset: 0x000B4D68
		protected internal override void OnInit()
		{
			this.CleanState = base.GameEntity.GetFirstChildEntityWithTag("body");
			base.OnInit();
			base.DestructionComponent.OnDestroyed += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnDestroyed);
			base.DestructionComponent.BattleSide = BattleSideEnum.Attacker;
			this._aiBarriers = base.Scene.FindEntitiesWithTag(this.BarrierTagToRemove).ToList<GameEntity>();
			if (!GameNetwork.IsClientOrReplay && this._soilGenericNavMeshID > 0)
			{
				this.CleanState.Scene.SetAbilityOfFacesWithId(this._soilGenericNavMeshID, false);
			}
			List<SynchedMissionObject> list = this.CleanState.CollectObjectsWithTag(this.GateTag);
			if (list.Count > 0)
			{
				this._gateObject = list[0];
			}
			this.AddRegularMovementComponent();
			List<GameEntity> list2 = base.Scene.FindEntitiesWithTag("breakable_wall").ToList<GameEntity>();
			if (!list2.IsEmpty<GameEntity>())
			{
				float num = 10000000f;
				GameEntity entity = null;
				MatrixFrame targetFrame = this.MovementComponent.GetTargetFrame();
				foreach (GameEntity gameEntity in list2)
				{
					float lengthSquared = (gameEntity.GlobalPosition - targetFrame.origin).LengthSquared;
					if (lengthSquared < num)
					{
						num = lengthSquared;
						entity = gameEntity;
					}
				}
				list2 = entity.CollectChildrenEntitiesWithTag("destroyed");
				if (list2.Count > 0)
				{
					this._destroyedWallEntity = list2[0];
				}
				list2 = entity.CollectChildrenEntitiesWithTag("non_destroyed");
				if (list2.Count > 0)
				{
					this._nonDestroyedWallEntity = list2[0];
				}
				list2 = entity.CollectChildrenEntitiesWithTag("particle_spawnpoint");
				if (list2.Count > 0)
				{
					this._battlementDestroyedParticle = list2[0];
				}
			}
			list = this.CleanState.CollectObjectsWithTag(this.HandleTag);
			this._handleObject = ((list.Count < 1) ? null : list[0]);
			this._gateHandleIdleAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.GateHandleIdleAnimation);
			this._gateTrembleAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.GateTrembleAnimation);
			this._queueManagers = new List<LadderQueueManager>();
			if (!GameNetwork.IsClientOrReplay)
			{
				List<GameEntity> list3 = this.CleanState.CollectChildrenEntitiesWithTag("ladder");
				if (list3.Count > 0)
				{
					this._hasLadders = true;
					GameEntity gameEntity2 = list3.ElementAt(list3.Count / 2);
					foreach (GameEntity gameEntity3 in list3)
					{
						if (gameEntity3.Name.Contains("middle"))
						{
							gameEntity2 = gameEntity3;
						}
						else
						{
							LadderQueueManager ladderQueueManager = gameEntity3.GetScriptComponents<LadderQueueManager>().FirstOrDefault<LadderQueueManager>();
							ladderQueueManager.Initialize(-1, MatrixFrame.Identity, Vec3.Zero, BattleSideEnum.None, int.MaxValue, 1f, 5f, 5f, 5f, 0f, false, 1f, 0f, 0f, false, -1, -1, int.MaxValue, int.MaxValue);
							ladderQueueManager.DeactivateImmediate();
						}
					}
					int num2 = 0;
					int num3 = 1;
					for (int i = base.GameEntity.Name.Length - 1; i >= 0; i--)
					{
						if (char.IsDigit(base.GameEntity.Name[i]))
						{
							num2 += (int)(base.GameEntity.Name[i] - '0') * num3;
							num3 *= 10;
						}
						else if (num2 > 0)
						{
							break;
						}
					}
					LadderQueueManager ladderQueueManager2 = gameEntity2.GetScriptComponents<LadderQueueManager>().FirstOrDefault<LadderQueueManager>();
					if (ladderQueueManager2 != null)
					{
						MatrixFrame identity = MatrixFrame.Identity;
						identity.rotation.RotateAboutSide(1.5707964f);
						identity.rotation.RotateAboutForward(0.3926991f);
						ladderQueueManager2.Initialize(this.DynamicNavmeshIdStart + 5, identity, new Vec3(0f, 0f, 1f, -1f), BattleSideEnum.Attacker, list3.Count * 2, 0.7853982f, 2f, 1f, 4f, 3f, false, 0.8f, (float)num2 * 2f / 3f, 5f, list3.Count > 1, this.DynamicNavmeshIdStart + 6, this.DynamicNavmeshIdStart + 7, num2 * MathF.Round((float)list3.Count * 0.666f), list3.Count + 1);
						this._queueManagers.Add(ladderQueueManager2);
					}
					base.GameEntity.Scene.MarkFacesWithIdAsLadder(5, true);
					base.GameEntity.Scene.MarkFacesWithIdAsLadder(6, true);
					base.GameEntity.Scene.MarkFacesWithIdAsLadder(7, true);
				}
				else
				{
					this._hasLadders = false;
					LadderQueueManager ladderQueueManager3 = this.CleanState.GetScriptComponents<LadderQueueManager>().FirstOrDefault<LadderQueueManager>();
					if (ladderQueueManager3 != null)
					{
						MatrixFrame identity2 = MatrixFrame.Identity;
						identity2.origin.y = identity2.origin.y + 4f;
						identity2.rotation.RotateAboutSide(-1.5707964f);
						identity2.rotation.RotateAboutUp(3.1415927f);
						ladderQueueManager3.Initialize(this.DynamicNavmeshIdStart + 2, identity2, new Vec3(0f, -1f, 0f, -1f), BattleSideEnum.Attacker, 15, 0.7853982f, 2f, 1f, 3f, 1f, false, 0.8f, 4f, 5f, false, -2, -2, int.MaxValue, 15);
						this._queueManagers.Add(ladderQueueManager3);
					}
				}
			}
			this._state = SiegeTower.GateState.Closed;
			this._gateOpenSoundIndex = SoundEvent.GetEventIdFromString("event:/mission/siege/siegetower/dooropen");
			this._closedStateRotation = this._gateObject.GameEntity.GetFrame().rotation;
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				standingPoint.AddComponent(new ResetAnimationOnStopUsageComponent(ActionIndexCache.act_none));
				if (!standingPoint.GameEntity.HasTag("move"))
				{
					this._gateStandingPoint = standingPoint;
					standingPoint.IsDeactivated = true;
					this._gateStandingPointLocalIKFrame = standingPoint.GameEntity.GetGlobalFrame().TransformToLocal(this.CleanState.GetGlobalFrame());
					standingPoint.AddComponent(new ClearHandInverseKinematicsOnStopUsageComponent());
				}
			}
			if (base.WaitStandingPoints[0].GlobalPosition.z > base.WaitStandingPoints[1].GlobalPosition.z)
			{
				GameEntity value = base.WaitStandingPoints[0];
				base.WaitStandingPoints[0] = base.WaitStandingPoints[1];
				base.WaitStandingPoints[1] = value;
				this.ActiveWaitStandingPoint = base.WaitStandingPoints[0];
			}
			IEnumerable<GameEntity> source = from ewtwst in base.Scene.FindEntitiesWithTag(this._targetWallSegmentTag).ToList<GameEntity>()
			where ewtwst.HasScriptOfType<WallSegment>()
			select ewtwst;
			if (!source.IsEmpty<GameEntity>())
			{
				this._targetWallSegment = source.First<GameEntity>().GetFirstScriptOfType<WallSegment>();
				this._targetWallSegment.AttackerSiegeWeapon = this;
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
			if (!GameNetwork.IsClientOrReplay)
			{
				if (this.GetGateNavMeshId() != 0)
				{
					this.CleanState.Scene.SetAbilityOfFacesWithId(this.GetGateNavMeshId(), false);
				}
				foreach (LadderQueueManager ladderQueueManager4 in this._queueManagers)
				{
					this.CleanState.Scene.SetAbilityOfFacesWithId(ladderQueueManager4.ManagedNavigationFaceId, false);
					ladderQueueManager4.DeactivateImmediate();
				}
			}
			GameEntity gameEntity4 = base.Scene.FindEntitiesWithTag("ditch_filler").FirstOrDefault((GameEntity df) => df.HasTag(this._sideTag));
			if (gameEntity4 != null)
			{
				this._ditchFillDebris = gameEntity4.GetFirstScriptOfType<SynchedMissionObject>();
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				this._gateObject.GameEntity.AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 3, true, false, false);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
			Mission.Current.AddToWeaponListForFriendlyFirePreventing(this);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000B73D0 File Offset: 0x000B55D0
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents())
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick | ScriptComponentBehavior.TickRequirement.TickParallel;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000B73F0 File Offset: 0x000B55F0
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!this.CleanState.IsVisibleIncludeParents())
			{
				return;
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					if (standingPoint.GameEntity.HasTag("move"))
					{
						standingPoint.SetIsDeactivatedSynched(this.MovementComponent.HasArrivedAtTarget);
					}
					else
					{
						UsableMissionObject usableMissionObject = standingPoint;
						bool isDeactivatedSynched;
						if (this.MovementComponent.HasArrivedAtTarget && this.State != SiegeTower.GateState.Open)
						{
							if (this.State == SiegeTower.GateState.GateFalling || this.State == SiegeTower.GateState.GateFallingWallDestroyed)
							{
								Agent userAgent = standingPoint.UserAgent;
								isDeactivatedSynched = (userAgent != null && userAgent.IsPlayerControlled);
							}
							else
							{
								isDeactivatedSynched = false;
							}
						}
						else
						{
							isDeactivatedSynched = true;
						}
						usableMissionObject.SetIsDeactivatedSynched(isDeactivatedSynched);
					}
				}
			}
			if (!GameNetwork.IsClientOrReplay && this.MovementComponent.HasArrivedAtTarget && !this.HasArrivedAtTarget)
			{
				this.HasArrivedAtTarget = true;
				this.ActiveWaitStandingPoint = base.WaitStandingPoints[1];
			}
			if (this.HasArrivedAtTarget)
			{
				switch (this.State)
				{
				case SiegeTower.GateState.Closed:
					if (!GameNetwork.IsClientOrReplay && base.UserCountNotInStruckAction > 0)
					{
						this.State = SiegeTower.GateState.GateFalling;
						return;
					}
					break;
				case SiegeTower.GateState.Open:
					break;
				case SiegeTower.GateState.GateFalling:
				{
					MatrixFrame frame = this._gateObject.GameEntity.GetFrame();
					frame.rotation.RotateAboutSide(this._fallAngularSpeed * dt);
					this._gateObject.GameEntity.SetFrame(ref frame);
					if (Vec3.DotProduct(frame.rotation.u, this._openStateRotation.f) < 0.025f)
					{
						this.State = SiegeTower.GateState.GateFallingWallDestroyed;
					}
					this._fallAngularSpeed += dt * 2f * MathF.Max(0.3f, 1f - frame.rotation.u.z);
					return;
				}
				case SiegeTower.GateState.GateFallingWallDestroyed:
				{
					MatrixFrame frame2 = this._gateObject.GameEntity.GetFrame();
					frame2.rotation.RotateAboutSide(this._fallAngularSpeed * dt);
					this._gateObject.GameEntity.SetFrame(ref frame2);
					float num = Vec3.DotProduct(frame2.rotation.u, this._openStateRotation.f);
					if (this._fallAngularSpeed > 0f && num < 0.05f)
					{
						frame2.rotation = this._openStateRotation;
						this._gateObject.GameEntity.SetFrame(ref frame2);
						this._gateObject.GameEntity.Skeleton.SetAnimationAtChannel(this._gateTrembleAnimationIndex, 0, 1f, -1f, 0f);
						SoundEvent gateOpenSound = this._gateOpenSound;
						if (gateOpenSound != null)
						{
							gateOpenSound.Stop();
						}
						if (!GameNetwork.IsClientOrReplay)
						{
							this.State = SiegeTower.GateState.Open;
						}
					}
					this._fallAngularSpeed += dt * 3f * MathF.Max(0.3f, 1f - frame2.rotation.u.z);
					return;
				}
				default:
					Debug.FailedAssert("Invalid gate state.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SiegeTower.cs", "OnTick", 974);
					break;
				}
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000B76FC File Offset: 0x000B58FC
		protected internal override void OnTickParallel(float dt)
		{
			base.OnTickParallel(dt);
			if (!this.CleanState.IsVisibleIncludeParents())
			{
				return;
			}
			this.MovementComponent.TickParallelManually(dt);
			if (this._gateStandingPoint.HasUser)
			{
				Agent userAgent = this._gateStandingPoint.UserAgent;
				if (userAgent.IsInBeingStruckAction)
				{
					userAgent.ClearHandInverseKinematics();
					return;
				}
				Agent userAgent2 = this._gateStandingPoint.UserAgent;
				MatrixFrame globalFrame = this.CleanState.GetGlobalFrame();
				userAgent2.SetHandInverseKinematicsFrameForMissionObjectUsage(this._gateStandingPointLocalIKFrame, globalFrame, 0f);
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000B777C File Offset: 0x000B597C
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			if (!GameNetwork.IsClientOrReplay && this.GetGateNavMeshId() > 0)
			{
				this.CleanState.Scene.SetAbilityOfFacesWithId(this.GetGateNavMeshId(), false);
			}
			this._state = SiegeTower.GateState.Closed;
			this._hasArrivedAtTarget = false;
			MatrixFrame frame = this._gateObject.GameEntity.GetFrame();
			frame.rotation = this._closedStateRotation;
			SynchedMissionObject handleObject = this._handleObject;
			if (handleObject != null)
			{
				handleObject.GameEntity.Skeleton.SetAnimationAtChannel(-1, 0, 1f, -1f, 0f);
			}
			this._gateObject.GameEntity.Skeleton.SetAnimationAtChannel(-1, 0, 1f, -1f, 0f);
			this._gateObject.GameEntity.SetFrame(ref frame);
			if (this._destroyedWallEntity != null && this._nonDestroyedWallEntity != null)
			{
				this._nonDestroyedWallEntity.SetVisibilityExcludeParents(false);
				this._destroyedWallEntity.SetVisibilityExcludeParents(true);
			}
			foreach (StandingPoint standingPoint in base.StandingPoints)
			{
				standingPoint.IsDeactivated = !standingPoint.GameEntity.HasTag("move");
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000B78D0 File Offset: 0x000B5AD0
		public void OnDestroyed(DestructableComponent destroyedComponent, Agent destroyerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			bool burnAgents = false;
			MissionWeapon missionWeapon = weapon;
			if (missionWeapon.CurrentUsageItem != null)
			{
				missionWeapon = weapon;
				bool flag;
				if (missionWeapon.CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.Burning))
				{
					missionWeapon = weapon;
					flag = missionWeapon.CurrentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.AffectsArea | WeaponFlags.AffectsAreaBig);
				}
				else
				{
					flag = false;
				}
				burnAgents = flag;
			}
			Mission.Current.KillAgentsOnEntity(destroyedComponent.CurrentState, destroyerAgent, burnAgents);
			foreach (GameEntity gameEntity in this._aiBarriers)
			{
				gameEntity.SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000B7988 File Offset: 0x000B5B88
		public void HighlightPath()
		{
			this.MovementComponent.HighlightPath();
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000B7998 File Offset: 0x000B5B98
		public void SwitchGhostEntityMovementMode(bool isGhostEnabled)
		{
			if (isGhostEnabled)
			{
				if (!this._isGhostMovementOn)
				{
					base.RemoveComponent(this.MovementComponent);
					this.GhostEntityMove = true;
					this.MovementComponent.GhostEntitySpeedMultiplier *= 3f;
					this.MovementComponent.SetGhostVisibility(true);
				}
				this._isGhostMovementOn = true;
				return;
			}
			if (this._isGhostMovementOn)
			{
				base.RemoveComponent(this.MovementComponent);
				PathLastNodeFixer component = base.GetComponent<PathLastNodeFixer>();
				base.RemoveComponent(component);
				this.AddRegularMovementComponent();
				this.MovementComponent.SetGhostVisibility(false);
			}
			this._isGhostMovementOn = false;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000B7A29 File Offset: 0x000B5C29
		public MatrixFrame GetInitialFrame()
		{
			SiegeWeaponMovementComponent movementComponent = this.MovementComponent;
			if (movementComponent == null)
			{
				return this.CleanState.GetGlobalFrame();
			}
			return movementComponent.GetInitialFrame();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000B7A48 File Offset: 0x000B5C48
		private void OnSiegeTowerGateStateChange()
		{
			switch (this.State)
			{
			case SiegeTower.GateState.Closed:
			{
				SynchedMissionObject handleObject = this._handleObject;
				if (handleObject != null)
				{
					handleObject.GameEntity.Skeleton.SetAnimationAtChannel(this._gateHandleIdleAnimationIndex, 0, 1f, -1f, 0f);
				}
				if (!GameNetwork.IsClientOrReplay && this.GetGateNavMeshId() != 0)
				{
					this.CleanState.Scene.SetAbilityOfFacesWithId(this.GetGateNavMeshId(), false);
					return;
				}
				break;
			}
			case SiegeTower.GateState.Open:
				if (this._gateObject.GameEntity.Skeleton.GetAnimationIndexAtChannel(0) != this._gateHandleIdleAnimationIndex)
				{
					MatrixFrame frame = this._gateObject.GameEntity.GetFrame();
					frame.rotation = this._openStateRotation;
					this._gateObject.GameEntity.SetFrame(ref frame);
					this._gateObject.GameEntity.Skeleton.SetAnimationAtChannel(this._gateTrembleAnimationIndex, 0, 1f, -1f, 0f);
					SoundEvent gateOpenSound = this._gateOpenSound;
					if (gateOpenSound != null)
					{
						gateOpenSound.Stop();
					}
					if (!GameNetwork.IsClientOrReplay && this.GetGateNavMeshId() != 0)
					{
						this.CleanState.Scene.SetAbilityOfFacesWithId(this.GetGateNavMeshId(), true);
					}
				}
				if (!GameNetwork.IsClientOrReplay)
				{
					this.CleanState.Scene.SetAbilityOfFacesWithId(this.GetGateNavMeshId(), true);
				}
				foreach (GameEntity gameEntity in this._aiBarriers)
				{
					gameEntity.SetVisibilityExcludeParents(false);
				}
				break;
			case SiegeTower.GateState.GateFalling:
				this._fallAngularSpeed = 0f;
				this._gateOpenSound = SoundEvent.CreateEvent(this._gateOpenSoundIndex, base.Scene);
				this._gateOpenSound.PlayInPosition(this._gateObject.GameEntity.GlobalPosition);
				return;
			case SiegeTower.GateState.GateFallingWallDestroyed:
				if (this._destroyedWallEntity != null && this._nonDestroyedWallEntity != null)
				{
					this._fallAngularSpeed *= 0.1f;
					this._nonDestroyedWallEntity.SetVisibilityExcludeParents(false);
					this._destroyedWallEntity.SetVisibilityExcludeParents(true);
					if (this._battlementDestroyedParticle != null)
					{
						Mission.Current.AddParticleSystemBurstByName(this.BattlementDestroyedParticle, this._battlementDestroyedParticle.GetGlobalFrame(), false);
						return;
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000B7CA0 File Offset: 0x000B5EA0
		private void AddRegularMovementComponent()
		{
			this.MovementComponent = new SiegeWeaponMovementComponent
			{
				PathEntityName = this.PathEntity,
				MinSpeed = this.MinSpeed,
				MaxSpeed = this.MaxSpeed,
				MainObject = this,
				WheelDiameter = this.WheelDiameter,
				NavMeshIdToDisableOnDestination = this.NavMeshIdToDisableOnDestination,
				MovementSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/siege/siegetower/move"),
				GhostEntitySpeedMultiplier = this.GhostEntitySpeedMultiplier
			};
			base.AddComponent(this.MovementComponent);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000B7D24 File Offset: 0x000B5F24
		private void SetUpGhostEntity()
		{
			PathLastNodeFixer component = new PathLastNodeFixer
			{
				PathHolder = this
			};
			base.AddComponent(component);
			this.MovementComponent = new SiegeWeaponMovementComponent
			{
				PathEntityName = this.PathEntity,
				MainObject = this,
				GhostEntitySpeedMultiplier = this.GhostEntitySpeedMultiplier
			};
			base.AddComponent(this.MovementComponent);
			this.MovementComponent.SetupGhostEntity();
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000B7D88 File Offset: 0x000B5F88
		private void UpdateGhostEntity()
		{
			List<GameEntity> list = this.CleanState.CollectChildrenEntitiesWithTag("ghost_object");
			if (list.Count > 0)
			{
				GameEntity gameEntity = list[0];
				if (gameEntity.ChildCount > 0)
				{
					this.MovementComponent.GhostEntitySpeedMultiplier = this.GhostEntitySpeedMultiplier;
					GameEntity child = gameEntity.GetChild(0);
					MatrixFrame frame = child.GetFrame();
					child.SetFrame(ref frame);
				}
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000B7DE6 File Offset: 0x000B5FE6
		public void SetSpawnedFromSpawner()
		{
			this._spawnedFromSpawner = true;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000B7DF0 File Offset: 0x000B5FF0
		public void AssignParametersFromSpawner(string pathEntityName, string targetWallSegment, string sideTag, int soilNavMeshID1, int soilNavMeshID2, int ditchNavMeshID1, int ditchNavMeshID2, int groundToSoilNavMeshID1, int groundToSoilNavMeshID2, int soilGenericNavMeshID, int groundGenericNavMeshID, Mat3 openStateRotation, string barrierTagToRemove)
		{
			this.PathEntity = pathEntityName;
			this._targetWallSegmentTag = targetWallSegment;
			this._sideTag = sideTag;
			this._soilNavMeshID1 = soilNavMeshID1;
			this._soilNavMeshID2 = soilNavMeshID2;
			this._ditchNavMeshID1 = ditchNavMeshID1;
			this._ditchNavMeshID2 = ditchNavMeshID2;
			this._groundToSoilNavMeshID1 = groundToSoilNavMeshID1;
			this._groundToSoilNavMeshID2 = groundToSoilNavMeshID2;
			this._soilGenericNavMeshID = soilGenericNavMeshID;
			this._groundGenericNavMeshID = groundGenericNavMeshID;
			this._openStateRotation = openStateRotation;
			this.BarrierTagToRemove = barrierTagToRemove;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000B7E64 File Offset: 0x000B6064
		public override void OnAfterReadFromNetwork(ValueTuple<BaseSynchedMissionObjectReadableRecord, ISynchedMissionObjectReadableRecord> synchedMissionObjectReadableRecord)
		{
			base.OnAfterReadFromNetwork(synchedMissionObjectReadableRecord);
			SiegeTower.SiegeTowerRecord siegeTowerRecord = (SiegeTower.SiegeTowerRecord)synchedMissionObjectReadableRecord.Item2;
			this.HasArrivedAtTarget = siegeTowerRecord.HasArrivedAtTarget;
			this._state = (SiegeTower.GateState)siegeTowerRecord.State;
			this._fallAngularSpeed = siegeTowerRecord.FallAngularSpeed;
			if (this._state == SiegeTower.GateState.Open)
			{
				if (this._destroyedWallEntity != null && this._nonDestroyedWallEntity != null)
				{
					this._nonDestroyedWallEntity.SetVisibilityExcludeParents(false);
					this._destroyedWallEntity.SetVisibilityExcludeParents(true);
				}
				MatrixFrame frame = this._gateObject.GameEntity.GetFrame();
				frame.rotation = this._openStateRotation;
				this._gateObject.GameEntity.SetFrame(ref frame);
			}
			float num = siegeTowerRecord.TotalDistanceTraveled;
			num += 0.05f;
			this.MovementComponent.SetTotalDistanceTraveledForPathTracker(num);
			this.MovementComponent.SetTargetFrameForPathTracker();
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000B7F40 File Offset: 0x000B6140
		public bool GetNavmeshFaceIds(out List<int> navmeshFaceIds)
		{
			navmeshFaceIds = new List<int>
			{
				this.DynamicNavmeshIdStart + 1,
				this.DynamicNavmeshIdStart + 3,
				this.DynamicNavmeshIdStart + 5,
				this.DynamicNavmeshIdStart + 6,
				this.DynamicNavmeshIdStart + 7
			};
			return true;
		}

		// Token: 0x040012AF RID: 4783
		private const int LeftLadderNavMeshIdLocal = 5;

		// Token: 0x040012B0 RID: 4784
		private const int MiddleLadderNavMeshIdLocal = 6;

		// Token: 0x040012B1 RID: 4785
		private const int RightLadderNavMeshIdLocal = 7;

		// Token: 0x040012B2 RID: 4786
		private const string BreakableWallTag = "breakable_wall";

		// Token: 0x040012B3 RID: 4787
		private const string DestroyedWallTag = "destroyed";

		// Token: 0x040012B4 RID: 4788
		private const string NonDestroyedWallTag = "non_destroyed";

		// Token: 0x040012B5 RID: 4789
		private const string LadderTag = "ladder";

		// Token: 0x040012B6 RID: 4790
		private const string BattlementDestroyedParticleTag = "particle_spawnpoint";

		// Token: 0x040012B7 RID: 4791
		public string GateTag = "gate";

		// Token: 0x040012B8 RID: 4792
		public string GateOpenTag = "gateOpen";

		// Token: 0x040012B9 RID: 4793
		public string HandleTag = "handle";

		// Token: 0x040012BA RID: 4794
		public string GateHandleIdleAnimation = "siegetower_handle_idle";

		// Token: 0x040012BB RID: 4795
		private int _gateHandleIdleAnimationIndex = -1;

		// Token: 0x040012BC RID: 4796
		public string GateTrembleAnimation = "siegetower_door_stop";

		// Token: 0x040012BD RID: 4797
		private int _gateTrembleAnimationIndex = -1;

		// Token: 0x040012BE RID: 4798
		public string BattlementDestroyedParticle = "psys_adobe_battlement_destroyed";

		// Token: 0x040012BF RID: 4799
		private string _targetWallSegmentTag;

		// Token: 0x040012C0 RID: 4800
		public bool GhostEntityMove = true;

		// Token: 0x040012C1 RID: 4801
		public float GhostEntitySpeedMultiplier = 1f;

		// Token: 0x040012C2 RID: 4802
		private string _sideTag;

		// Token: 0x040012C3 RID: 4803
		private bool _hasLadders;

		// Token: 0x040012C4 RID: 4804
		public float WheelDiameter = 1.3f;

		// Token: 0x040012C5 RID: 4805
		public float MinSpeed = 0.5f;

		// Token: 0x040012C6 RID: 4806
		public float MaxSpeed = 1f;

		// Token: 0x040012C7 RID: 4807
		public int GateNavMeshId;

		// Token: 0x040012C8 RID: 4808
		public int NavMeshIdToDisableOnDestination = -1;

		// Token: 0x040012C9 RID: 4809
		private int _soilNavMeshID1;

		// Token: 0x040012CA RID: 4810
		private int _soilNavMeshID2;

		// Token: 0x040012CB RID: 4811
		private int _ditchNavMeshID1;

		// Token: 0x040012CC RID: 4812
		private int _ditchNavMeshID2;

		// Token: 0x040012CD RID: 4813
		private int _groundToSoilNavMeshID1;

		// Token: 0x040012CE RID: 4814
		private int _groundToSoilNavMeshID2;

		// Token: 0x040012CF RID: 4815
		private int _soilGenericNavMeshID;

		// Token: 0x040012D0 RID: 4816
		private int _groundGenericNavMeshID;

		// Token: 0x040012D1 RID: 4817
		public string BarrierTagToRemove = "barrier";

		// Token: 0x040012D2 RID: 4818
		private List<GameEntity> _aiBarriers;

		// Token: 0x040012D3 RID: 4819
		private bool _isGhostMovementOn;

		// Token: 0x040012D4 RID: 4820
		private bool _hasArrivedAtTarget;

		// Token: 0x040012D5 RID: 4821
		private SiegeTower.GateState _state;

		// Token: 0x040012D6 RID: 4822
		private SynchedMissionObject _gateObject;

		// Token: 0x040012D7 RID: 4823
		private SynchedMissionObject _handleObject;

		// Token: 0x040012D8 RID: 4824
		private SoundEvent _gateOpenSound;

		// Token: 0x040012D9 RID: 4825
		private int _gateOpenSoundIndex = -1;

		// Token: 0x040012DA RID: 4826
		private Mat3 _openStateRotation;

		// Token: 0x040012DB RID: 4827
		private Mat3 _closedStateRotation;

		// Token: 0x040012DC RID: 4828
		private float _fallAngularSpeed;

		// Token: 0x040012DD RID: 4829
		private GameEntity _cleanState;

		// Token: 0x040012DE RID: 4830
		private GameEntity _destroyedWallEntity;

		// Token: 0x040012DF RID: 4831
		private GameEntity _nonDestroyedWallEntity;

		// Token: 0x040012E0 RID: 4832
		private GameEntity _battlementDestroyedParticle;

		// Token: 0x040012E1 RID: 4833
		private StandingPoint _gateStandingPoint;

		// Token: 0x040012E2 RID: 4834
		private MatrixFrame _gateStandingPointLocalIKFrame;

		// Token: 0x040012E3 RID: 4835
		private SynchedMissionObject _ditchFillDebris;

		// Token: 0x040012E4 RID: 4836
		private List<LadderQueueManager> _queueManagers;

		// Token: 0x040012E5 RID: 4837
		private WallSegment _targetWallSegment;

		// Token: 0x040012E6 RID: 4838
		private List<SiegeLadder> _sameSideSiegeLadders;

		// Token: 0x020005FB RID: 1531
		[DefineSynchedMissionObjectType(typeof(SiegeTower))]
		public struct SiegeTowerRecord : ISynchedMissionObjectReadableRecord
		{
			// Token: 0x170009EC RID: 2540
			// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000E98F3 File Offset: 0x000E7AF3
			// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x000E98FB File Offset: 0x000E7AFB
			public bool HasArrivedAtTarget { get; private set; }

			// Token: 0x170009ED RID: 2541
			// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x000E9904 File Offset: 0x000E7B04
			// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x000E990C File Offset: 0x000E7B0C
			public int State { get; private set; }

			// Token: 0x170009EE RID: 2542
			// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000E9915 File Offset: 0x000E7B15
			// (set) Token: 0x06003BEA RID: 15338 RVA: 0x000E991D File Offset: 0x000E7B1D
			public float FallAngularSpeed { get; private set; }

			// Token: 0x170009EF RID: 2543
			// (get) Token: 0x06003BEB RID: 15339 RVA: 0x000E9926 File Offset: 0x000E7B26
			// (set) Token: 0x06003BEC RID: 15340 RVA: 0x000E992E File Offset: 0x000E7B2E
			public float TotalDistanceTraveled { get; private set; }

			// Token: 0x06003BED RID: 15341 RVA: 0x000E9938 File Offset: 0x000E7B38
			public bool ReadFromNetwork(ref bool bufferReadValid)
			{
				this.HasArrivedAtTarget = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				this.State = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeTowerGateStateCompressionInfo, ref bufferReadValid);
				this.FallAngularSpeed = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.SiegeMachineComponentAngularSpeedCompressionInfo, ref bufferReadValid);
				this.TotalDistanceTraveled = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.PositionCompressionInfo, ref bufferReadValid);
				return bufferReadValid;
			}
		}

		// Token: 0x020005FC RID: 1532
		public enum GateState
		{
			// Token: 0x04001F3A RID: 7994
			Closed,
			// Token: 0x04001F3B RID: 7995
			Open,
			// Token: 0x04001F3C RID: 7996
			GateFalling,
			// Token: 0x04001F3D RID: 7997
			GateFallingWallDestroyed,
			// Token: 0x04001F3E RID: 7998
			NumberOfStates
		}
	}
}
