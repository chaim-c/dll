using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Source.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000330 RID: 816
	public class CastleGate : UsableMachine, IPointDefendable, ICastleKeyPosition, ITargetable
	{
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000A9CF9 File Offset: 0x000A7EF9
		// (set) Token: 0x06002BBA RID: 11194 RVA: 0x000A9D01 File Offset: 0x000A7F01
		public TacticalPosition MiddlePosition { get; private set; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000A9D0A File Offset: 0x000A7F0A
		private static int BatteringRamHitSoundIdCache
		{
			get
			{
				if (CastleGate._batteringRamHitSoundId == -1)
				{
					CastleGate._batteringRamHitSoundId = SoundEvent.GetEventIdFromString("event:/mission/siege/door/hit");
				}
				return CastleGate._batteringRamHitSoundId;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000A9D28 File Offset: 0x000A7F28
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x000A9D30 File Offset: 0x000A7F30
		public TacticalPosition WaitPosition { get; private set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000A9D39 File Offset: 0x000A7F39
		public override FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.Gate;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000A9D3C File Offset: 0x000A7F3C
		// (set) Token: 0x06002BC0 RID: 11200 RVA: 0x000A9D44 File Offset: 0x000A7F44
		public CastleGate.GateState State { get; private set; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000A9D4D File Offset: 0x000A7F4D
		public bool IsGateOpen
		{
			get
			{
				return this.State == CastleGate.GateState.Open || base.IsDestroyed;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000A9D5F File Offset: 0x000A7F5F
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x000A9D67 File Offset: 0x000A7F67
		public IPrimarySiegeWeapon AttackerSiegeWeapon { get; set; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000A9D70 File Offset: 0x000A7F70
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000A9D78 File Offset: 0x000A7F78
		public IEnumerable<DefencePoint> DefencePoints { get; protected set; }

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000A9D84 File Offset: 0x000A7F84
		public CastleGate()
		{
			this._attackOnlyDoorColliders = new List<GameEntity>();
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000A9E42 File Offset: 0x000A8042
		public Vec3 GetPosition()
		{
			return base.GameEntity.GlobalPosition;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000A9E4F File Offset: 0x000A804F
		public override OrderType GetOrder(BattleSideEnum side)
		{
			if (base.IsDestroyed)
			{
				return OrderType.None;
			}
			if (side != BattleSideEnum.Attacker)
			{
				return OrderType.Use;
			}
			return OrderType.AttackEntity;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000A9E64 File Offset: 0x000A8064
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x000A9E6C File Offset: 0x000A806C
		public FormationAI.BehaviorSide DefenseSide { get; private set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x000A9E75 File Offset: 0x000A8075
		public WorldFrame MiddleFrame
		{
			get
			{
				return this._middleFrame;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000A9E7D File Offset: 0x000A807D
		public WorldFrame DefenseWaitFrame
		{
			get
			{
				return this._defenseWaitFrame;
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000A9E88 File Offset: 0x000A8088
		protected internal override void OnInit()
		{
			base.OnInit();
			DestructableComponent destructableComponent = base.GameEntity.GetScriptComponents<DestructableComponent>().FirstOrDefault<DestructableComponent>();
			if (destructableComponent != null)
			{
				destructableComponent.OnNextDestructionState += this.OnNextDestructionState;
				this.DestructibleComponentOnMissionReset = new Action(destructableComponent.OnMissionReset);
				if (!GameNetwork.IsClientOrReplay)
				{
					destructableComponent.OnDestroyed += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnDestroyed);
					destructableComponent.OnHitTaken += new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnHitTaken);
					DestructableComponent destructableComponent2 = destructableComponent;
					destructableComponent2.OnCalculateDestructionStateIndex = (Func<int, int, int, int>)Delegate.Combine(destructableComponent2.OnCalculateDestructionStateIndex, new Func<int, int, int, int>(this.OnCalculateDestructionStateIndex));
				}
				destructableComponent.BattleSide = BattleSideEnum.Defender;
			}
			this.CollectGameEntities(true);
			base.GameEntity.SetAnimationSoundActivation(true);
			if (GameNetwork.IsClientOrReplay)
			{
				return;
			}
			this._queueManager = base.GameEntity.GetScriptComponents<LadderQueueManager>().FirstOrDefault<LadderQueueManager>();
			if (this._queueManager == null)
			{
				GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity ce) => ce.GetScriptComponents<LadderQueueManager>().Any<LadderQueueManager>());
				if (gameEntity != null)
				{
					this._queueManager = gameEntity.GetFirstScriptOfType<LadderQueueManager>();
				}
			}
			if (this._queueManager != null)
			{
				MatrixFrame identity = MatrixFrame.Identity;
				identity.origin.y = identity.origin.y - 2f;
				identity.rotation.RotateAboutSide(-1.5707964f);
				identity.rotation.RotateAboutForward(3.1415927f);
				this._queueManager.Initialize(this._queueManager.ManagedNavigationFaceId, identity, -identity.rotation.u, BattleSideEnum.Defender, 15, 0.62831855f, 3f, 2.2f, 0f, 0f, false, 1f, 2.1474836E+09f, 5f, false, -2, -2, int.MaxValue, 15);
				this._queueManager.Activate();
			}
			string sideTag = this.SideTag;
			if (!(sideTag == "left"))
			{
				if (!(sideTag == "middle"))
				{
					if (!(sideTag == "right"))
					{
						this.DefenseSide = FormationAI.BehaviorSide.BehaviorSideNotSet;
					}
					else
					{
						this.DefenseSide = FormationAI.BehaviorSide.Right;
					}
				}
				else
				{
					this.DefenseSide = FormationAI.BehaviorSide.Middle;
				}
			}
			else
			{
				this.DefenseSide = FormationAI.BehaviorSide.Left;
			}
			List<GameEntity> list = base.GameEntity.CollectChildrenEntitiesWithTag("middle_pos");
			if (list.Count > 0)
			{
				GameEntity gameEntity2 = list.FirstOrDefault<GameEntity>();
				this.MiddlePosition = gameEntity2.GetFirstScriptOfType<TacticalPosition>();
				MatrixFrame globalFrame = gameEntity2.GetGlobalFrame();
				this._middleFrame = new WorldFrame(globalFrame.rotation, globalFrame.origin.ToWorldPosition());
				this._middleFrame.Origin.GetGroundVec3();
			}
			else
			{
				MatrixFrame globalFrame2 = base.GameEntity.GetGlobalFrame();
				this._middleFrame = new WorldFrame(globalFrame2.rotation, globalFrame2.origin.ToWorldPosition());
			}
			List<GameEntity> list2 = base.GameEntity.CollectChildrenEntitiesWithTag("wait_pos");
			if (list2.Count > 0)
			{
				GameEntity gameEntity3 = list2.FirstOrDefault<GameEntity>();
				this.WaitPosition = gameEntity3.GetFirstScriptOfType<TacticalPosition>();
				MatrixFrame globalFrame3 = gameEntity3.GetGlobalFrame();
				this._defenseWaitFrame = new WorldFrame(globalFrame3.rotation, globalFrame3.origin.ToWorldPosition());
				this._defenseWaitFrame.Origin.GetGroundVec3();
			}
			else
			{
				this._defenseWaitFrame = this._middleFrame;
			}
			this._openingAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.OpeningAnimationName);
			this._closingAnimationIndex = MBAnimation.GetAnimationIndexWithName(this.ClosingAnimationName);
			base.SetScriptComponentToTick(this.GetTickRequirement());
			this.OnCheckForProblems();
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000AA1F0 File Offset: 0x000A83F0
		public void SetUsableTeam(Team team)
		{
			using (List<StandingPoint>.Enumerator enumerator = base.StandingPoints.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StandingPointWithTeamLimit standingPointWithTeamLimit;
					if ((standingPointWithTeamLimit = (enumerator.Current as StandingPointWithTeamLimit)) != null)
					{
						standingPointWithTeamLimit.UsableTeam = team;
					}
				}
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000AA24C File Offset: 0x000A844C
		public override void AfterMissionStart()
		{
			this._afterMissionStartTriggered = true;
			base.AfterMissionStart();
			this.SetInitialStateOfGate();
			this.InitializeExtraColliderPositions();
			if (!GameNetwork.IsClientOrReplay)
			{
				this.SetAutoOpenState(Mission.Current.IsSallyOutBattle);
			}
			if (this.OwningTeam == CastleGate.DoorOwnership.Attackers)
			{
				this.SetUsableTeam(Mission.Current.AttackerTeam);
			}
			else if (this.OwningTeam == CastleGate.DoorOwnership.Defenders)
			{
				this.SetUsableTeam(Mission.Current.DefenderTeam);
			}
			this._pathChecker = new AgentPathNavMeshChecker(Mission.Current, base.GameEntity.GetGlobalFrame(), 2f, this.NavigationMeshId, BattleSideEnum.Defender, AgentPathNavMeshChecker.Direction.BothDirections, 14f, 3f);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000AA2F0 File Offset: 0x000A84F0
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			DestructableComponent destructableComponent = base.GameEntity.GetScriptComponents<DestructableComponent>().FirstOrDefault<DestructableComponent>();
			if (destructableComponent != null)
			{
				destructableComponent.OnNextDestructionState -= this.OnNextDestructionState;
				if (!GameNetwork.IsClientOrReplay)
				{
					destructableComponent.OnDestroyed -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnDestroyed);
					destructableComponent.OnHitTaken -= new DestructableComponent.OnHitTakenAndDestroyedDelegate(this.OnHitTaken);
					DestructableComponent destructableComponent2 = destructableComponent;
					destructableComponent2.OnCalculateDestructionStateIndex = (Func<int, int, int, int>)Delegate.Remove(destructableComponent2.OnCalculateDestructionStateIndex, new Func<int, int, int, int>(this.OnCalculateDestructionStateIndex));
				}
			}
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000AA377 File Offset: 0x000A8577
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			if (base.GameEntity.HasTag("outer_gate") && base.GameEntity.HasTag("inner_gate"))
			{
				MBDebug.ShowWarning("Castle gate has both the outer gate tag and the inner gate tag.");
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000AA3AD File Offset: 0x000A85AD
		protected internal override void OnMissionReset()
		{
			Action destructibleComponentOnMissionReset = this.DestructibleComponentOnMissionReset;
			if (destructibleComponentOnMissionReset != null)
			{
				destructibleComponentOnMissionReset();
			}
			this.CollectGameEntities(false);
			base.OnMissionReset();
			this.SetInitialStateOfGate();
			this._previousAnimationProgress = -1f;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000AA3E0 File Offset: 0x000A85E0
		private void SetInitialStateOfGate()
		{
			if (!GameNetwork.IsClientOrReplay && this.NavigationMeshIdToDisableOnOpen != -1)
			{
				this._openNavMeshIdDisabled = false;
				base.Scene.SetAbilityOfFacesWithId(this.NavigationMeshIdToDisableOnOpen, true);
			}
			if (!this._civilianMission)
			{
				this._doorSkeleton.SetAnimationAtChannel(this._closingAnimationIndex, 0, 1f, -1f, 0f);
				this._doorSkeleton.SetAnimationParameterAtChannel(0, 0.99f);
				this._doorSkeleton.Freeze(false);
				this.State = CastleGate.GateState.Closed;
				return;
			}
			this.OpenDoor();
			if (this._doorSkeleton != null)
			{
				this._door.SetAnimationChannelParameterSynched(0, 1f);
			}
			this.SetGateNavMeshState(true);
			base.SetDisabled(true);
			DestructableComponent firstScriptOfType = base.GameEntity.GetFirstScriptOfType<DestructableComponent>();
			if (firstScriptOfType == null)
			{
				return;
			}
			firstScriptOfType.SetDisabled(false);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000AA4AD File Offset: 0x000A86AD
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return new TextObject("{=6wZUG0ev}Gate", null).ToString();
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000AA4C0 File Offset: 0x000A86C0
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			TextObject textObject = new TextObject(usableGameObject.GameEntity.HasTag("open") ? "{=5oozsaIb}{KEY} Open" : "{=TJj71hPO}{KEY} Close", null);
			textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
			return textObject;
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000AA50E File Offset: 0x000A870E
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new CastleGateAI(this);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000AA516 File Offset: 0x000A8716
		public void OpenDoorAndDisableGateForCivilianMission()
		{
			this._civilianMission = true;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000AA520 File Offset: 0x000A8720
		public void OpenDoor()
		{
			if (!base.IsDisabled)
			{
				this.State = CastleGate.GateState.Open;
				if (!this.AutoOpen)
				{
					this.SetGateNavMeshState(true);
				}
				else
				{
					this.SetGateNavMeshStateForEnemies(true);
				}
				int animationIndexAtChannel = this._doorSkeleton.GetAnimationIndexAtChannel(0);
				float animationParameterAtChannel = this._doorSkeleton.GetAnimationParameterAtChannel(0);
				this._door.SetAnimationAtChannelSynched(this._openingAnimationIndex, 0, 1f);
				if (animationIndexAtChannel == this._closingAnimationIndex)
				{
					this._door.SetAnimationChannelParameterSynched(0, 1f - animationParameterAtChannel);
				}
				SynchedMissionObject plank = this._plank;
				if (plank == null)
				{
					return;
				}
				plank.SetVisibleSynched(false, false);
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000AA5B4 File Offset: 0x000A87B4
		public void CloseDoor()
		{
			if (!base.IsDisabled)
			{
				this.State = CastleGate.GateState.Closed;
				if (!this.AutoOpen)
				{
					this.SetGateNavMeshState(false);
				}
				else
				{
					this.SetGateNavMeshStateForEnemies(false);
				}
				int animationIndexAtChannel = this._doorSkeleton.GetAnimationIndexAtChannel(0);
				float animationParameterAtChannel = this._doorSkeleton.GetAnimationParameterAtChannel(0);
				this._door.SetAnimationAtChannelSynched(this._closingAnimationIndex, 0, 1f);
				if (animationIndexAtChannel == this._openingAnimationIndex)
				{
					this._door.SetAnimationChannelParameterSynched(0, 1f - animationParameterAtChannel);
				}
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000AA634 File Offset: 0x000A8834
		private void UpdateDoorBodies(bool updateAnyway)
		{
			if (this._attackOnlyDoorColliders.Count == 2)
			{
				float animationParameterAtChannel = this._doorSkeleton.GetAnimationParameterAtChannel(0);
				if (this._previousAnimationProgress != animationParameterAtChannel || updateAnyway)
				{
					this._previousAnimationProgress = animationParameterAtChannel;
					MatrixFrame matrixFrame = this._doorSkeleton.GetBoneEntitialFrameWithIndex(this._leftDoorBoneIndex);
					MatrixFrame matrixFrame2 = this._doorSkeleton.GetBoneEntitialFrameWithIndex(this._rightDoorBoneIndex);
					this._attackOnlyDoorColliders[0].SetFrame(ref matrixFrame2);
					this._attackOnlyDoorColliders[1].SetFrame(ref matrixFrame);
					GameEntity agentColliderLeft = this._agentColliderLeft;
					if (agentColliderLeft != null)
					{
						agentColliderLeft.SetFrame(ref matrixFrame);
					}
					GameEntity agentColliderRight = this._agentColliderRight;
					if (agentColliderRight != null)
					{
						agentColliderRight.SetFrame(ref matrixFrame2);
					}
					if (this._extraColliderLeft != null && this._extraColliderRight != null)
					{
						if (this.State == CastleGate.GateState.Closed)
						{
							if (!this._leftExtraColliderDisabled)
							{
								this._extraColliderLeft.SetBodyFlags(this._extraColliderLeft.BodyFlag | BodyFlags.Disabled);
								this._leftExtraColliderDisabled = true;
							}
							if (!this._rightExtraColliderDisabled)
							{
								this._extraColliderRight.SetBodyFlags(this._extraColliderRight.BodyFlag | BodyFlags.Disabled);
								this._rightExtraColliderDisabled = true;
								return;
							}
						}
						else
						{
							float num = (matrixFrame2.origin - matrixFrame.origin).Length * 0.5f;
							float num2 = Vec3.DotProduct(matrixFrame2.rotation.s, Vec3.Side) / (matrixFrame2.rotation.s.Length * 1f);
							float num3 = MathF.Sqrt(1f - num2 * num2);
							float num4 = num * 1.1f;
							float num5 = MBMath.Map(num2, 0.3f, 1f, 0f, 1f) * (num * 0.2f);
							this._extraColliderLeft.SetLocalPosition(matrixFrame.origin - new Vec3(num4 - num + num5, num * num3, 0f, -1f));
							this._extraColliderRight.SetLocalPosition(matrixFrame2.origin - new Vec3(-(num4 - num) - num5, num * num3, 0f, -1f));
							float num6;
							if (num2 < 0f)
							{
								num6 = num;
								num6 += num * -num2;
							}
							else
							{
								num6 = num - num * num2;
							}
							num6 = (num4 - num6) / num;
							if (num6 <= 0.0001f)
							{
								if (!this._leftExtraColliderDisabled)
								{
									this._extraColliderLeft.SetBodyFlags(this._extraColliderLeft.BodyFlag | BodyFlags.Disabled);
									this._leftExtraColliderDisabled = true;
								}
							}
							else
							{
								if (this._leftExtraColliderDisabled)
								{
									this._extraColliderLeft.SetBodyFlags(this._extraColliderLeft.BodyFlag & ~BodyFlags.Disabled);
									this._leftExtraColliderDisabled = false;
								}
								matrixFrame = this._extraColliderLeft.GetFrame();
								matrixFrame.rotation.Orthonormalize();
								matrixFrame.origin -= new Vec3(num4 - num4 * num6, 0f, 0f, -1f);
								this._extraColliderLeft.SetFrame(ref matrixFrame);
							}
							matrixFrame2 = this._extraColliderRight.GetFrame();
							matrixFrame2.rotation.Orthonormalize();
							float num7;
							if (num2 < 0f)
							{
								num7 = num;
								num7 += num * -num2;
							}
							else
							{
								num7 = num - num * num2;
							}
							num7 = (num4 - num7) / num;
							if (num7 > 0.0001f)
							{
								if (this._rightExtraColliderDisabled)
								{
									this._extraColliderRight.SetBodyFlags(this._extraColliderRight.BodyFlag & ~BodyFlags.Disabled);
									this._rightExtraColliderDisabled = false;
								}
								matrixFrame2.origin += new Vec3(num4 - num4 * num7, 0f, 0f, -1f);
								this._extraColliderRight.SetFrame(ref matrixFrame2);
								return;
							}
							if (!this._rightExtraColliderDisabled)
							{
								this._extraColliderRight.SetBodyFlags(this._extraColliderRight.BodyFlag | BodyFlags.Disabled);
								this._rightExtraColliderDisabled = true;
								return;
							}
						}
					}
				}
			}
			else if (this._attackOnlyDoorColliders.Count == 1)
			{
				MatrixFrame boneEntitialFrameWithName = this._doorSkeleton.GetBoneEntitialFrameWithName(this.RightDoorBoneName);
				this._attackOnlyDoorColliders[0].SetFrame(ref boneEntitialFrameWithName);
				GameEntity agentColliderRight2 = this._agentColliderRight;
				if (agentColliderRight2 == null)
				{
					return;
				}
				agentColliderRight2.SetFrame(ref boneEntitialFrameWithName);
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000AAA6C File Offset: 0x000A8C6C
		private void SetGateNavMeshState(bool isEnabled)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				base.Scene.SetAbilityOfFacesWithId(this.NavigationMeshId, isEnabled);
				if (this._queueManager != null)
				{
					this._queueManager.Activate();
					base.Scene.SetAbilityOfFacesWithId(this._queueManager.ManagedNavigationFaceId, isEnabled);
				}
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000AAABC File Offset: 0x000A8CBC
		private void SetGateNavMeshStateForEnemies(bool isEnabled)
		{
			Team attackerTeam = Mission.Current.AttackerTeam;
			if (attackerTeam != null)
			{
				foreach (Agent agent in attackerTeam.ActiveAgents)
				{
					if (agent.IsAIControlled)
					{
						agent.SetAgentExcludeStateForFaceGroupId(this.NavigationMeshId, !isEnabled);
					}
				}
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000AAB30 File Offset: 0x000A8D30
		public void SetAutoOpenState(bool isEnabled)
		{
			this.AutoOpen = isEnabled;
			if (this.AutoOpen)
			{
				this.SetGateNavMeshState(true);
				this.SetGateNavMeshStateForEnemies(this.State == CastleGate.GateState.Open);
				return;
			}
			if (this.State == CastleGate.GateState.Open)
			{
				this.CloseDoor();
			}
			else
			{
				this.SetGateNavMeshState(false);
			}
			this.SetGateNavMeshStateForEnemies(true);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000AAB81 File Offset: 0x000A8D81
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents())
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000AABA0 File Offset: 0x000A8DA0
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (!GameNetwork.IsClientOrReplay && this.NavigationMeshIdToDisableOnOpen != -1)
			{
				if (this._openNavMeshIdDisabled)
				{
					if (base.IsDestroyed)
					{
						base.Scene.SetAbilityOfFacesWithId(this.NavigationMeshIdToDisableOnOpen, true);
						this._openNavMeshIdDisabled = false;
					}
					else if (this.State == CastleGate.GateState.Closed)
					{
						int animationIndexAtChannel = this._doorSkeleton.GetAnimationIndexAtChannel(0);
						float animationParameterAtChannel = this._doorSkeleton.GetAnimationParameterAtChannel(0);
						if (animationIndexAtChannel != this._closingAnimationIndex || animationParameterAtChannel > 0.4f)
						{
							base.Scene.SetAbilityOfFacesWithId(this.NavigationMeshIdToDisableOnOpen, true);
							this._openNavMeshIdDisabled = false;
						}
					}
				}
				else if (this.State == CastleGate.GateState.Open && !base.IsDestroyed)
				{
					base.Scene.SetAbilityOfFacesWithId(this.NavigationMeshIdToDisableOnOpen, false);
					this._openNavMeshIdDisabled = true;
				}
			}
			if (this._afterMissionStartTriggered)
			{
				this.UpdateDoorBodies(false);
			}
			if (!GameNetwork.IsClientOrReplay)
			{
				this.ServerTick(dt);
			}
			if (base.Ai.HasActionCompleted)
			{
				bool flag = false;
				for (int i = 0; i < base.StandingPoints.Count; i++)
				{
					if (base.StandingPoints[i].HasUser || base.StandingPoints[i].HasAIMovingTo)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					bool flag2 = false;
					for (int j = 0; j < this._userFormations.Count; j++)
					{
						if (this._userFormations[j].CountOfDetachableNonplayerUnits > 0)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						((CastleGateAI)base.Ai).ResetInitialGateState(this.State);
					}
				}
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000AAD3C File Offset: 0x000A8F3C
		protected override bool IsAgentOnInconvenientNavmesh(Agent agent, StandingPoint standingPoint)
		{
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege)
			{
				return false;
			}
			int currentNavigationFaceId = agent.GetCurrentNavigationFaceId();
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = (agent.Team.TeamAI as TeamAISiegeComponent)) != null && currentNavigationFaceId % 10 != 1)
			{
				if (base.GameEntity.HasTag("inner_gate"))
				{
					return true;
				}
				if (base.GameEntity.HasTag("outer_gate"))
				{
					CastleGate innerGate = teamAISiegeComponent.InnerGate;
					if (innerGate != null)
					{
						Vec3 vec = base.GameEntity.GlobalPosition - agent.Position;
						Vec3 vec2 = innerGate.GameEntity.GlobalPosition - agent.Position;
						if (vec.AsVec2.DotProduct(vec2.AsVec2) > 0f)
						{
							return true;
						}
					}
				}
				foreach (int num in (Mission.Current.DefenderTeam.TeamAI as TeamAISiegeDefender).DifficultNavmeshIDs)
				{
					if (currentNavigationFaceId == num)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000AAE64 File Offset: 0x000A9064
		private void ServerTick(float dt)
		{
			if (!this.IsDeactivated)
			{
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					if (standingPoint.HasUser)
					{
						if (standingPoint.GameEntity.HasTag("open"))
						{
							this.OpenDoor();
							if (this.AutoOpen)
							{
								this.SetAutoOpenState(false);
							}
						}
						else
						{
							this.CloseDoor();
							if (Mission.Current.IsSallyOutBattle)
							{
								this.SetAutoOpenState(true);
							}
						}
					}
				}
				if (this.AutoOpen && this._pathChecker != null)
				{
					this._pathChecker.Tick(dt);
					if (this._pathChecker.HasAgentsUsingPath())
					{
						if (this.State != CastleGate.GateState.Open)
						{
							this.OpenDoor();
						}
					}
					else if (this.State != CastleGate.GateState.Closed)
					{
						this.CloseDoor();
					}
				}
				if (this._doorSkeleton != null && !base.IsDestroyed)
				{
					float animationParameterAtChannel = this._doorSkeleton.GetAnimationParameterAtChannel(0);
					foreach (StandingPoint standingPoint2 in base.StandingPoints)
					{
						bool isDeactivatedSynched = animationParameterAtChannel < 1f || standingPoint2.GameEntity.HasTag((this.State == CastleGate.GateState.Open) ? "open" : "close");
						standingPoint2.SetIsDeactivatedSynched(isDeactivatedSynched);
					}
					if (animationParameterAtChannel >= 1f && this.State == CastleGate.GateState.Open)
					{
						if (this._extraColliderRight != null)
						{
							this._extraColliderRight.SetBodyFlags(this._extraColliderRight.BodyFlag | BodyFlags.Disabled);
							this._rightExtraColliderDisabled = true;
						}
						if (this._extraColliderLeft != null)
						{
							this._extraColliderLeft.SetBodyFlags(this._extraColliderLeft.BodyFlag | BodyFlags.Disabled);
							this._leftExtraColliderDisabled = true;
						}
					}
					if (this._plank != null && this.State == CastleGate.GateState.Closed && animationParameterAtChannel > 0.9f)
					{
						this._plank.SetVisibleSynched(true, false);
					}
				}
			}
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000AB078 File Offset: 0x000A9278
		public TargetFlags GetTargetFlags()
		{
			TargetFlags targetFlags = TargetFlags.None;
			targetFlags |= TargetFlags.IsStructure;
			if (base.IsDestroyed)
			{
				targetFlags |= TargetFlags.NotAThreat;
			}
			if (DebugSiegeBehavior.DebugAttackState == DebugSiegeBehavior.DebugStateAttacker.DebugAttackersToBattlements)
			{
				targetFlags |= TargetFlags.DebugThreat;
			}
			return targetFlags;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000AB0A9 File Offset: 0x000A92A9
		public float GetTargetValue(List<Vec3> weaponPos)
		{
			return 10f;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000AB0B0 File Offset: 0x000A92B0
		public GameEntity GetTargetEntity()
		{
			return base.GameEntity;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000AB0B8 File Offset: 0x000A92B8
		public BattleSideEnum GetSide()
		{
			return BattleSideEnum.Defender;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000AB0BB File Offset: 0x000A92BB
		public GameEntity Entity()
		{
			return base.GameEntity;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000AB0C4 File Offset: 0x000A92C4
		protected void CollectGameEntities(bool calledFromOnInit)
		{
			this.CollectDynamicGameEntities(calledFromOnInit);
			if (!GameNetwork.IsClientOrReplay)
			{
				List<GameEntity> list = base.GameEntity.CollectChildrenEntitiesWithTag("plank");
				if (list.Count > 0)
				{
					this._plank = list.FirstOrDefault<GameEntity>().GetFirstScriptOfType<SynchedMissionObject>();
				}
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000AB10A File Offset: 0x000A930A
		protected void OnNextDestructionState()
		{
			this.CollectDynamicGameEntities(false);
			this.UpdateDoorBodies(true);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000AB11C File Offset: 0x000A931C
		protected void CollectDynamicGameEntities(bool calledFromOnInit)
		{
			this._attackOnlyDoorColliders.Clear();
			List<GameEntity> list;
			if (calledFromOnInit)
			{
				list = base.GameEntity.CollectChildrenEntitiesWithTag("gate").ToList<GameEntity>();
				this._leftExtraColliderDisabled = false;
				this._rightExtraColliderDisabled = false;
				this._agentColliderLeft = base.GameEntity.GetFirstChildEntityWithTag("collider_agent_l");
				this._agentColliderRight = base.GameEntity.GetFirstChildEntityWithTag("collider_agent_r");
			}
			else
			{
				list = (from x in base.GameEntity.CollectChildrenEntitiesWithTag("gate")
				where x.IsVisibleIncludeParents()
				select x).ToList<GameEntity>();
			}
			if (list.Count == 0)
			{
				return;
			}
			if (list.Count > 1)
			{
				int num = int.MinValue;
				int num2 = int.MaxValue;
				GameEntity gameEntity = null;
				GameEntity gameEntity2 = null;
				foreach (GameEntity gameEntity3 in list)
				{
					int num3 = int.Parse(gameEntity3.Tags.FirstOrDefault((string x) => x.Contains("state_")).Split(new char[]
					{
						'_'
					}).Last<string>());
					if (num3 > num)
					{
						num = num3;
						gameEntity = gameEntity3;
					}
					if (num3 < num2)
					{
						num2 = num3;
						gameEntity2 = gameEntity3;
					}
				}
				this._door = (calledFromOnInit ? gameEntity2.GetFirstScriptOfType<SynchedMissionObject>() : gameEntity.GetFirstScriptOfType<SynchedMissionObject>());
			}
			else
			{
				this._door = list[0].GetFirstScriptOfType<SynchedMissionObject>();
			}
			this._doorSkeleton = this._door.GameEntity.Skeleton;
			GameEntity gameEntity4 = this._door.GameEntity.CollectChildrenEntitiesWithTag("collider_r").FirstOrDefault<GameEntity>();
			if (gameEntity4 != null)
			{
				this._attackOnlyDoorColliders.Add(gameEntity4);
			}
			GameEntity gameEntity5 = this._door.GameEntity.CollectChildrenEntitiesWithTag("collider_l").FirstOrDefault<GameEntity>();
			if (gameEntity5 != null)
			{
				this._attackOnlyDoorColliders.Add(gameEntity5);
			}
			if (gameEntity4 == null || gameEntity5 == null)
			{
				GameEntity agentColliderLeft = this._agentColliderLeft;
				if (agentColliderLeft != null)
				{
					agentColliderLeft.SetVisibilityExcludeParents(false);
				}
				GameEntity agentColliderRight = this._agentColliderRight;
				if (agentColliderRight != null)
				{
					agentColliderRight.SetVisibilityExcludeParents(false);
				}
			}
			GameEntity gameEntity6 = this._door.GameEntity.CollectChildrenEntitiesWithTag(this.ExtraCollisionObjectTagLeft).FirstOrDefault<GameEntity>();
			if (gameEntity6 != null)
			{
				if (!this.ActivateExtraColliders)
				{
					gameEntity6.RemovePhysics(false);
				}
				else
				{
					if (!calledFromOnInit)
					{
						MatrixFrame matrixFrame = (this._extraColliderLeft != null) ? this._extraColliderLeft.GetFrame() : this._doorSkeleton.GetBoneEntitialFrameWithName(this.LeftDoorBoneName);
						this._extraColliderLeft = gameEntity6;
						this._extraColliderLeft.SetFrame(ref matrixFrame);
					}
					else
					{
						this._extraColliderLeft = gameEntity6;
					}
					if (this._leftExtraColliderDisabled)
					{
						this._extraColliderLeft.SetBodyFlags(this._extraColliderLeft.BodyFlag | BodyFlags.Disabled);
					}
					else
					{
						this._extraColliderLeft.SetBodyFlags(this._extraColliderLeft.BodyFlag & ~BodyFlags.Disabled);
					}
				}
			}
			GameEntity gameEntity7 = this._door.GameEntity.CollectChildrenEntitiesWithTag(this.ExtraCollisionObjectTagRight).FirstOrDefault<GameEntity>();
			if (gameEntity7 != null)
			{
				if (!this.ActivateExtraColliders)
				{
					gameEntity7.RemovePhysics(false);
				}
				else
				{
					if (!calledFromOnInit)
					{
						MatrixFrame matrixFrame2 = (this._extraColliderRight != null) ? this._extraColliderRight.GetFrame() : this._doorSkeleton.GetBoneEntitialFrameWithName(this.RightDoorBoneName);
						this._extraColliderRight = gameEntity7;
						this._extraColliderRight.SetFrame(ref matrixFrame2);
					}
					else
					{
						this._extraColliderRight = gameEntity7;
					}
					if (this._rightExtraColliderDisabled)
					{
						this._extraColliderRight.SetBodyFlags(this._extraColliderRight.BodyFlag | BodyFlags.Disabled);
					}
					else
					{
						this._extraColliderRight.SetBodyFlags(this._extraColliderRight.BodyFlag & ~BodyFlags.Disabled);
					}
				}
			}
			if (this._door != null && this._doorSkeleton != null)
			{
				this._leftDoorBoneIndex = Skeleton.GetBoneIndexFromName(this._doorSkeleton.GetName(), this.LeftDoorBoneName);
				this._rightDoorBoneIndex = Skeleton.GetBoneIndexFromName(this._doorSkeleton.GetName(), this.RightDoorBoneName);
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000AB544 File Offset: 0x000A9744
		private void InitializeExtraColliderPositions()
		{
			if (this._extraColliderLeft != null)
			{
				MatrixFrame boneEntitialFrameWithName = this._doorSkeleton.GetBoneEntitialFrameWithName(this.LeftDoorBoneName);
				this._extraColliderLeft.SetFrame(ref boneEntitialFrameWithName);
				this._extraColliderLeft.SetVisibilityExcludeParents(true);
			}
			if (this._extraColliderRight != null)
			{
				MatrixFrame boneEntitialFrameWithName2 = this._doorSkeleton.GetBoneEntitialFrameWithName(this.RightDoorBoneName);
				this._extraColliderRight.SetFrame(ref boneEntitialFrameWithName2);
				this._extraColliderRight.SetVisibilityExcludeParents(true);
			}
			this.UpdateDoorBodies(true);
			foreach (GameEntity gameEntity in this._attackOnlyDoorColliders)
			{
				gameEntity.SetVisibilityExcludeParents(true);
			}
			if (this._agentColliderLeft != null)
			{
				this._agentColliderLeft.SetVisibilityExcludeParents(true);
			}
			if (this._agentColliderRight != null)
			{
				this._agentColliderRight.SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000AB644 File Offset: 0x000A9844
		private void OnHitTaken(DestructableComponent hitComponent, Agent hitterAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			if (!GameNetwork.IsClientOrReplay && inflictedDamage >= 200 && this.State == CastleGate.GateState.Closed && attackerScriptComponentBehavior is BatteringRam)
			{
				SynchedMissionObject plank = this._plank;
				if (plank != null)
				{
					plank.SetAnimationAtChannelSynched(this.PlankHitAnimationName, 0, 1f);
				}
				this._door.SetAnimationAtChannelSynched(this.HitAnimationName, 0, 1f);
				Mission.Current.MakeSound(CastleGate.BatteringRamHitSoundIdCache, base.GameEntity.GlobalPosition, false, true, -1, -1);
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000AB6C8 File Offset: 0x000A98C8
		private void OnDestroyed(DestructableComponent destroyedComponent, Agent destroyerAgent, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, int inflictedDamage)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				SynchedMissionObject plank = this._plank;
				if (plank != null)
				{
					plank.SetVisibleSynched(false, false);
				}
				foreach (StandingPoint standingPoint in base.StandingPoints)
				{
					standingPoint.SetIsDeactivatedSynched(true);
				}
				if (attackerScriptComponentBehavior is BatteringRam)
				{
					this._door.SetAnimationAtChannelSynched(this.DestroyAnimationName, 0, 1f);
				}
				this.SetGateNavMeshState(true);
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000AB75C File Offset: 0x000A995C
		private int OnCalculateDestructionStateIndex(int destructionStateIndex, int inflictedDamage, int destructionStateCount)
		{
			if (inflictedDamage < 200)
			{
				return destructionStateIndex;
			}
			return MathF.Min(destructionStateIndex, destructionStateCount - 1);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000AB774 File Offset: 0x000A9974
		protected internal override bool OnCheckForProblems()
		{
			bool result = base.OnCheckForProblems();
			if (base.GameEntity.HasTag("outer_gate") && base.GameEntity.HasTag("inner_gate"))
			{
				MBEditor.AddEntityWarning(base.GameEntity, "This castle gate has both outer and inner tag at the same time.");
				result = true;
			}
			if (base.GameEntity.CollectChildrenEntitiesWithTag("wait_pos").Count != 1)
			{
				MBEditor.AddEntityWarning(base.GameEntity, "There must be one entity with wait position tag under castle gate.");
				result = true;
			}
			if (base.GameEntity.HasTag("outer_gate"))
			{
				uint visibilityMask = base.GameEntity.GetVisibilityLevelMaskIncludingParents();
				GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity x) => x.HasTag("middle_pos") && x.GetVisibilityLevelMaskIncludingParents() == visibilityMask);
				if (gameEntity != null)
				{
					GameEntity gameEntity2 = base.Scene.FindEntitiesWithTag("inner_gate").FirstOrDefault((GameEntity x) => x.GetVisibilityLevelMaskIncludingParents() == visibilityMask);
					if (gameEntity2 != null)
					{
						if (gameEntity2.HasScriptOfType<CastleGate>())
						{
							Vec2 va = gameEntity2.GlobalPosition.AsVec2 - gameEntity.GlobalPosition.AsVec2;
							Vec2 vb = base.GameEntity.GlobalPosition.AsVec2 - gameEntity.GlobalPosition.AsVec2;
							if (Vec2.DotProduct(va, vb) <= 0f)
							{
								MBEditor.AddEntityWarning(base.GameEntity, "Outer gate's middle position must not be between outer and inner gate.");
								result = true;
							}
						}
						else
						{
							MBEditor.AddEntityWarning(base.GameEntity, gameEntity2.Name + " this entity has inner gate tag but doesn't have castle gate script.");
							result = true;
						}
					}
					else
					{
						MBEditor.AddEntityWarning(base.GameEntity, "There is no entity with inner gate tag.");
						result = true;
					}
				}
				else
				{
					MBEditor.AddEntityWarning(base.GameEntity, "Outer gate doesn't have any middle positions");
					result = true;
				}
			}
			Vec3 scaleVector = base.GameEntity.GetGlobalFrame().rotation.GetScaleVector();
			if (MathF.Abs(scaleVector.x - scaleVector.y) > 1E-05f || MathF.Abs(scaleVector.x - scaleVector.z) > 1E-05f || MathF.Abs(scaleVector.y - scaleVector.z) > 1E-05f)
			{
				MBEditor.AddEntityWarning(base.GameEntity, "$$$ Non uniform scale on CastleGate at scene " + base.GameEntity.Scene.GetName());
				result = true;
			}
			return result;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000AB9BB File Offset: 0x000A9BBB
		public Vec3 GetTargetingOffset()
		{
			return Vec3.Zero;
		}

		// Token: 0x0400113B RID: 4411
		public const string OuterGateTag = "outer_gate";

		// Token: 0x0400113C RID: 4412
		public const string InnerGateTag = "inner_gate";

		// Token: 0x0400113D RID: 4413
		private const float ExtraColliderScaleFactor = 1.1f;

		// Token: 0x0400113E RID: 4414
		private const string LeftDoorBodyTag = "collider_l";

		// Token: 0x0400113F RID: 4415
		private const string RightDoorBodyTag = "collider_r";

		// Token: 0x04001140 RID: 4416
		private const string RightDoorAgentOnlyBodyTag = "collider_agent_r";

		// Token: 0x04001141 RID: 4417
		private const string OpenTag = "open";

		// Token: 0x04001142 RID: 4418
		private const string CloseTag = "close";

		// Token: 0x04001143 RID: 4419
		private const string MiddlePositionTag = "middle_pos";

		// Token: 0x04001144 RID: 4420
		private const string WaitPositionTag = "wait_pos";

		// Token: 0x04001145 RID: 4421
		private const string LeftDoorAgentOnlyBodyTag = "collider_agent_l";

		// Token: 0x04001146 RID: 4422
		private const int HeavyBlowDamageLimit = 200;

		// Token: 0x04001148 RID: 4424
		private static int _batteringRamHitSoundId = -1;

		// Token: 0x0400114A RID: 4426
		public CastleGate.DoorOwnership OwningTeam;

		// Token: 0x0400114B RID: 4427
		public string OpeningAnimationName = "castle_gate_a_opening";

		// Token: 0x0400114C RID: 4428
		public string ClosingAnimationName = "castle_gate_a_closing";

		// Token: 0x0400114D RID: 4429
		public string HitAnimationName = "castle_gate_a_hit";

		// Token: 0x0400114E RID: 4430
		public string PlankHitAnimationName = "castle_gate_a_plank_hit";

		// Token: 0x0400114F RID: 4431
		public string HitMeleeAnimationName = "castle_gate_a_hit_melee";

		// Token: 0x04001150 RID: 4432
		public string DestroyAnimationName = "castle_gate_a_break";

		// Token: 0x04001151 RID: 4433
		public int NavigationMeshId = 1000;

		// Token: 0x04001152 RID: 4434
		public int NavigationMeshIdToDisableOnOpen = -1;

		// Token: 0x04001153 RID: 4435
		public string LeftDoorBoneName = "bn_bottom_l";

		// Token: 0x04001154 RID: 4436
		public string RightDoorBoneName = "bn_bottom_r";

		// Token: 0x04001155 RID: 4437
		public string ExtraCollisionObjectTagRight = "extra_collider_r";

		// Token: 0x04001156 RID: 4438
		public string ExtraCollisionObjectTagLeft = "extra_collider_l";

		// Token: 0x04001157 RID: 4439
		private int _openingAnimationIndex = -1;

		// Token: 0x04001158 RID: 4440
		private int _closingAnimationIndex = -1;

		// Token: 0x04001159 RID: 4441
		private bool _leftExtraColliderDisabled;

		// Token: 0x0400115A RID: 4442
		private bool _rightExtraColliderDisabled;

		// Token: 0x0400115B RID: 4443
		private bool _civilianMission;

		// Token: 0x0400115C RID: 4444
		public bool ActivateExtraColliders = true;

		// Token: 0x0400115D RID: 4445
		public string SideTag;

		// Token: 0x0400115F RID: 4447
		private bool _openNavMeshIdDisabled;

		// Token: 0x04001160 RID: 4448
		private SynchedMissionObject _door;

		// Token: 0x04001161 RID: 4449
		private Skeleton _doorSkeleton;

		// Token: 0x04001162 RID: 4450
		private GameEntity _extraColliderRight;

		// Token: 0x04001163 RID: 4451
		private GameEntity _extraColliderLeft;

		// Token: 0x04001164 RID: 4452
		private readonly List<GameEntity> _attackOnlyDoorColliders;

		// Token: 0x04001165 RID: 4453
		private float _previousAnimationProgress = -1f;

		// Token: 0x04001166 RID: 4454
		private GameEntity _agentColliderRight;

		// Token: 0x04001167 RID: 4455
		private GameEntity _agentColliderLeft;

		// Token: 0x04001168 RID: 4456
		private LadderQueueManager _queueManager;

		// Token: 0x04001169 RID: 4457
		private bool _afterMissionStartTriggered;

		// Token: 0x0400116A RID: 4458
		private sbyte _rightDoorBoneIndex;

		// Token: 0x0400116B RID: 4459
		private sbyte _leftDoorBoneIndex;

		// Token: 0x0400116E RID: 4462
		private AgentPathNavMeshChecker _pathChecker;

		// Token: 0x0400116F RID: 4463
		public bool AutoOpen;

		// Token: 0x04001170 RID: 4464
		private SynchedMissionObject _plank;

		// Token: 0x04001172 RID: 4466
		private WorldFrame _middleFrame;

		// Token: 0x04001173 RID: 4467
		private WorldFrame _defenseWaitFrame;

		// Token: 0x04001174 RID: 4468
		private Action DestructibleComponentOnMissionReset;

		// Token: 0x020005DD RID: 1501
		public enum DoorOwnership
		{
			// Token: 0x04001EC0 RID: 7872
			Defenders,
			// Token: 0x04001EC1 RID: 7873
			Attackers
		}

		// Token: 0x020005DE RID: 1502
		public enum GateState
		{
			// Token: 0x04001EC3 RID: 7875
			Open,
			// Token: 0x04001EC4 RID: 7876
			Closed
		}
	}
}
