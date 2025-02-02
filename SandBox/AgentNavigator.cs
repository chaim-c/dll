using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Conversation;
using SandBox.Conversation.MissionLogics;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x0200001D RID: 29
	public sealed class AgentNavigator
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004C0B File Offset: 0x00002E0B
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00004C13 File Offset: 0x00002E13
		public UsableMachine TargetUsableMachine { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004C1C File Offset: 0x00002E1C
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00004C24 File Offset: 0x00002E24
		public WorldPosition TargetPosition { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004C2D File Offset: 0x00002E2D
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004C35 File Offset: 0x00002E35
		public Vec2 TargetDirection { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004C3E File Offset: 0x00002E3E
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004C46 File Offset: 0x00002E46
		public GameEntity TargetEntity { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004C4F File Offset: 0x00002E4F
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004C57 File Offset: 0x00002E57
		public Alley MemberOfAlley { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004C60 File Offset: 0x00002E60
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004C68 File Offset: 0x00002E68
		public string SpecialTargetTag
		{
			get
			{
				return this._specialTargetTag;
			}
			set
			{
				if (value != this._specialTargetTag)
				{
					this._specialTargetTag = value;
					AgentBehavior activeBehavior = this.GetActiveBehavior();
					if (activeBehavior != null)
					{
						activeBehavior.OnSpecialTargetChanged();
					}
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004C9A File Offset: 0x00002E9A
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004CA2 File Offset: 0x00002EA2
		private Dictionary<KeyValuePair<sbyte, string>, int> _bodyComponents { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004CAB File Offset: 0x00002EAB
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004CB3 File Offset: 0x00002EB3
		public AgentNavigator.NavigationState _agentState { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004CBC File Offset: 0x00002EBC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public bool CharacterHasVisiblePrefabs { get; private set; }

		// Token: 0x0600008C RID: 140 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public AgentNavigator(Agent agent, LocationCharacter locationCharacter) : this(agent)
		{
			this.SpecialTargetTag = locationCharacter.SpecialTargetTag;
			this._prefabNamesForBones = locationCharacter.PrefabNamesForBones;
			this._specialItem = locationCharacter.SpecialItem;
			this.MemberOfAlley = locationCharacter.MemberOfAlley;
			this.SetItemsVisibility(true);
			this.SetSpecialItem();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004D24 File Offset: 0x00002F24
		public AgentNavigator(Agent agent)
		{
			this._mission = agent.Mission;
			this._conversationHandler = this._mission.GetMissionBehavior<MissionConversationLogic>();
			this.OwnerAgent = agent;
			this._prefabNamesForBones = new Dictionary<sbyte, string>();
			this._behaviorGroups = new List<AgentBehaviorGroup>();
			this._bodyComponents = new Dictionary<KeyValuePair<sbyte, string>, int>();
			this.SpecialTargetTag = string.Empty;
			this.MemberOfAlley = null;
			this.TargetUsableMachine = null;
			this._checkBehaviorGroupsTimer = new BasicMissionTimer();
			this._prevPrefabs = new List<int>();
			this.CharacterHasVisiblePrefabs = false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004DB2 File Offset: 0x00002FB2
		public void OnStopUsingGameObject()
		{
			this._targetBehavior = null;
			this.TargetUsableMachine = null;
			this._agentState = AgentNavigator.NavigationState.NoTarget;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004DCC File Offset: 0x00002FCC
		public void OnAgentRemoved(Agent agent)
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				if (agentBehaviorGroup.IsActive)
				{
					agentBehaviorGroup.OnAgentRemoved(agent);
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004E28 File Offset: 0x00003028
		public void SetTarget(UsableMachine usableMachine, bool isInitialTarget = false)
		{
			if (usableMachine == null)
			{
				UsableMachine targetUsableMachine = this.TargetUsableMachine;
				if (targetUsableMachine != null)
				{
					((IDetachment)targetUsableMachine).RemoveAgent(this.OwnerAgent);
				}
				this.TargetUsableMachine = null;
				this.OwnerAgent.DisableScriptedMovement();
				this.OwnerAgent.ClearTargetFrame();
				this.TargetPosition = WorldPosition.Invalid;
				this.TargetEntity = null;
				this._agentState = AgentNavigator.NavigationState.NoTarget;
				return;
			}
			if (this.TargetUsableMachine != usableMachine || isInitialTarget)
			{
				this.TargetPosition = WorldPosition.Invalid;
				this._agentState = AgentNavigator.NavigationState.NoTarget;
				UsableMachine targetUsableMachine2 = this.TargetUsableMachine;
				if (targetUsableMachine2 != null)
				{
					((IDetachment)targetUsableMachine2).RemoveAgent(this.OwnerAgent);
				}
				if (usableMachine.IsStandingPointAvailableForAgent(this.OwnerAgent))
				{
					this.TargetUsableMachine = usableMachine;
					this.TargetPosition = WorldPosition.Invalid;
					this._agentState = AgentNavigator.NavigationState.UseMachine;
					this._targetBehavior = this.TargetUsableMachine.CreateAIBehaviorObject();
					((IDetachment)this.TargetUsableMachine).AddAgent(this.OwnerAgent, -1);
					this._targetReached = false;
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004F10 File Offset: 0x00003110
		public void SetTargetFrame(WorldPosition position, float rotation, float rangeThreshold = 1f, float rotationThreshold = -10f, Agent.AIScriptedFrameFlags flags = Agent.AIScriptedFrameFlags.None, bool disableClearTargetWhenTargetIsReached = false)
		{
			if (this._agentState != AgentNavigator.NavigationState.NoTarget)
			{
				this.ClearTarget();
			}
			this.TargetPosition = position;
			this.TargetDirection = Vec2.FromRotation(rotation);
			this._rangeThreshold = rangeThreshold;
			this._rotationScoreThreshold = rotationThreshold;
			this._disableClearTargetWhenTargetIsReached = disableClearTargetWhenTargetIsReached;
			if (this.IsTargetReached())
			{
				this.TargetPosition = WorldPosition.Invalid;
				this._agentState = AgentNavigator.NavigationState.NoTarget;
				return;
			}
			this.OwnerAgent.SetScriptedPositionAndDirection(ref position, rotation, false, flags);
			this._agentState = AgentNavigator.NavigationState.GoToTarget;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004F88 File Offset: 0x00003188
		public void ClearTarget()
		{
			this.SetTarget(null, false);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F94 File Offset: 0x00003194
		public void Tick(float dt, bool isSimulation = false)
		{
			this.HandleBehaviorGroups(isSimulation);
			if (ConversationMission.ConversationAgents.Contains(this.OwnerAgent))
			{
				using (List<AgentBehaviorGroup>.Enumerator enumerator = this._behaviorGroups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AgentBehaviorGroup agentBehaviorGroup = enumerator.Current;
						if (agentBehaviorGroup.IsActive)
						{
							agentBehaviorGroup.ConversationTick();
						}
					}
					goto IL_5E;
				}
			}
			this.TickActiveGroups(dt, isSimulation);
			IL_5E:
			if (this.TargetUsableMachine != null)
			{
				this._targetBehavior.Tick(this.OwnerAgent, null, null, dt);
			}
			else
			{
				this.HandleMovement();
			}
			if (this.TargetUsableMachine != null && isSimulation)
			{
				this._targetBehavior.TeleportUserAgentsToMachine(new List<Agent>
				{
					this.OwnerAgent
				});
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000505C File Offset: 0x0000325C
		public float GetDistanceToTarget(UsableMachine target)
		{
			float result = 100000f;
			if (target != null && this.OwnerAgent.CurrentlyUsedGameObject != null)
			{
				result = this.OwnerAgent.CurrentlyUsedGameObject.GetUserFrameForAgent(this.OwnerAgent).Origin.GetGroundVec3().Distance(this.OwnerAgent.Position);
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000050B8 File Offset: 0x000032B8
		public bool IsTargetReached()
		{
			if (this.TargetDirection.IsValid && this.TargetPosition.IsValid)
			{
				float num = Vec2.DotProduct(this.TargetDirection, this.OwnerAgent.GetMovementDirection());
				this._targetReached = ((this.OwnerAgent.Position - this.TargetPosition.GetGroundVec3()).LengthSquared < this._rangeThreshold * this._rangeThreshold && num > this._rotationScoreThreshold);
			}
			return this._targetReached;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005149 File Offset: 0x00003349
		private void HandleMovement()
		{
			if (this._agentState == AgentNavigator.NavigationState.GoToTarget && this.IsTargetReached())
			{
				this._agentState = AgentNavigator.NavigationState.AtTargetPosition;
				if (!this._disableClearTargetWhenTargetIsReached)
				{
					this.OwnerAgent.ClearTargetFrame();
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005178 File Offset: 0x00003378
		public void HoldAndHideRecentlyUsedMeshes()
		{
			foreach (KeyValuePair<KeyValuePair<sbyte, string>, int> keyValuePair in this._bodyComponents)
			{
				if (this.OwnerAgent.IsSynchedPrefabComponentVisible(keyValuePair.Value))
				{
					this.OwnerAgent.SetSynchedPrefabComponentVisibility(keyValuePair.Value, false);
					this._prevPrefabs.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005200 File Offset: 0x00003400
		public void RecoverRecentlyUsedMeshes()
		{
			foreach (int componentIndex in this._prevPrefabs)
			{
				this.OwnerAgent.SetSynchedPrefabComponentVisibility(componentIndex, true);
			}
			this._prevPrefabs.Clear();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005264 File Offset: 0x00003464
		public bool CanSeeAgent(Agent otherAgent)
		{
			if ((this.OwnerAgent.Position - otherAgent.Position).Length < 30f)
			{
				Vec3 eyeGlobalPosition = otherAgent.GetEyeGlobalPosition();
				Vec3 eyeGlobalPosition2 = this.OwnerAgent.GetEyeGlobalPosition();
				if (MathF.Abs(Vec3.AngleBetweenTwoVectors(otherAgent.Position - this.OwnerAgent.Position, this.OwnerAgent.LookDirection)) < 1.5f)
				{
					float num;
					return !Mission.Current.Scene.RayCastForClosestEntityOrTerrain(eyeGlobalPosition2, eyeGlobalPosition, out num, 0.01f, BodyFlags.CommonFocusRayCastExcludeFlags);
				}
			}
			return false;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000052FD File Offset: 0x000034FD
		public bool IsCarryingSomething()
		{
			return this.OwnerAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand) >= EquipmentIndex.WeaponItemBeginSlot || this.OwnerAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand) >= EquipmentIndex.WeaponItemBeginSlot || this._bodyComponents.Any((KeyValuePair<KeyValuePair<sbyte, string>, int> component) => this.OwnerAgent.IsSynchedPrefabComponentVisible(component.Value));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005338 File Offset: 0x00003538
		public void SetPrefabVisibility(sbyte realBoneIndex, string prefabName, bool isVisible)
		{
			KeyValuePair<sbyte, string> key = new KeyValuePair<sbyte, string>(realBoneIndex, prefabName);
			int componentIndex2;
			if (isVisible)
			{
				int componentIndex;
				if (!this._bodyComponents.TryGetValue(key, out componentIndex))
				{
					this._bodyComponents.Add(key, this.OwnerAgent.AddSynchedPrefabComponentToBone(prefabName, realBoneIndex));
					return;
				}
				if (!this.OwnerAgent.IsSynchedPrefabComponentVisible(componentIndex))
				{
					this.OwnerAgent.SetSynchedPrefabComponentVisibility(componentIndex, true);
					return;
				}
			}
			else if (this._bodyComponents.TryGetValue(key, out componentIndex2) && this.OwnerAgent.IsSynchedPrefabComponentVisible(componentIndex2))
			{
				this.OwnerAgent.SetSynchedPrefabComponentVisibility(componentIndex2, false);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000053C4 File Offset: 0x000035C4
		public bool GetPrefabVisibility(sbyte realBoneIndex, string prefabName)
		{
			KeyValuePair<sbyte, string> key = new KeyValuePair<sbyte, string>(realBoneIndex, prefabName);
			int componentIndex;
			return this._bodyComponents.TryGetValue(key, out componentIndex) && this.OwnerAgent.IsSynchedPrefabComponentVisible(componentIndex);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000053FC File Offset: 0x000035FC
		public void SetSpecialItem()
		{
			if (this._specialItem != null)
			{
				bool flag = false;
				EquipmentIndex equipmentIndex = EquipmentIndex.None;
				for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 <= EquipmentIndex.Weapon3; equipmentIndex2++)
				{
					if (this.OwnerAgent.Equipment[equipmentIndex2].IsEmpty)
					{
						equipmentIndex = equipmentIndex2;
					}
					else if (this.OwnerAgent.Equipment[equipmentIndex2].Item == this._specialItem)
					{
						equipmentIndex = equipmentIndex2;
						flag = true;
						break;
					}
				}
				if (equipmentIndex == EquipmentIndex.None)
				{
					this.OwnerAgent.DropItem(EquipmentIndex.Weapon3, WeaponClass.Undefined);
					equipmentIndex = EquipmentIndex.Weapon3;
				}
				if (!flag)
				{
					ItemObject specialItem = this._specialItem;
					ItemModifier itemModifier = null;
					IAgentOriginBase origin = this.OwnerAgent.Origin;
					MissionWeapon missionWeapon = new MissionWeapon(specialItem, itemModifier, (origin != null) ? origin.Banner : null);
					this.OwnerAgent.EquipWeaponWithNewEntity(equipmentIndex, ref missionWeapon);
				}
				this.OwnerAgent.TryToWieldWeaponInSlot(equipmentIndex, Agent.WeaponWieldActionType.Instant, false);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000054C4 File Offset: 0x000036C4
		public void SetItemsVisibility(bool isVisible)
		{
			foreach (KeyValuePair<sbyte, string> keyValuePair in this._prefabNamesForBones)
			{
				this.SetPrefabVisibility(keyValuePair.Key, keyValuePair.Value, isVisible);
			}
			this.CharacterHasVisiblePrefabs = (this._prefabNamesForBones.Count > 0 && isVisible);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000553C File Offset: 0x0000373C
		public void SetCommonArea(Alley alley)
		{
			if (alley != this.MemberOfAlley)
			{
				this.MemberOfAlley = alley;
				this.SpecialTargetTag = ((alley == null) ? "" : alley.Tag);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005564 File Offset: 0x00003764
		public void ForceThink(float inSeconds)
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				agentBehaviorGroup.ForceThink(inSeconds);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000055B8 File Offset: 0x000037B8
		public T AddBehaviorGroup<T>() where T : AgentBehaviorGroup
		{
			T t = this.GetBehaviorGroup<T>();
			if (t == null)
			{
				t = (Activator.CreateInstance(typeof(T), new object[]
				{
					this,
					this._mission
				}) as T);
				if (t != null)
				{
					this._behaviorGroups.Add(t);
				}
			}
			return t;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000561C File Offset: 0x0000381C
		public T GetBehaviorGroup<T>() where T : AgentBehaviorGroup
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				if (agentBehaviorGroup is T)
				{
					return (T)((object)agentBehaviorGroup);
				}
			}
			return default(T);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005684 File Offset: 0x00003884
		public AgentBehavior GetBehavior<T>() where T : AgentBehavior
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				foreach (AgentBehavior agentBehavior in agentBehaviorGroup.Behaviors)
				{
					if (agentBehavior.GetType() == typeof(T))
					{
						return agentBehavior;
					}
				}
			}
			return null;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005728 File Offset: 0x00003928
		public bool HasBehaviorGroup<T>()
		{
			using (List<AgentBehaviorGroup>.Enumerator enumerator = this._behaviorGroups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() is T)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005788 File Offset: 0x00003988
		public void RemoveBehaviorGroup<T>() where T : AgentBehaviorGroup
		{
			for (int i = 0; i < this._behaviorGroups.Count; i++)
			{
				if (this._behaviorGroups[i] is T)
				{
					this._behaviorGroups.RemoveAt(i);
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000057CC File Offset: 0x000039CC
		public void RefreshBehaviorGroups(bool isSimulation)
		{
			this._checkBehaviorGroupsTimer.Reset();
			float num = 0f;
			AgentBehaviorGroup agentBehaviorGroup = null;
			foreach (AgentBehaviorGroup agentBehaviorGroup2 in this._behaviorGroups)
			{
				float score = agentBehaviorGroup2.GetScore(isSimulation);
				if (score > num)
				{
					num = score;
					agentBehaviorGroup = agentBehaviorGroup2;
				}
			}
			if (num > 0f && agentBehaviorGroup != null && !agentBehaviorGroup.IsActive)
			{
				this.ActivateGroup(agentBehaviorGroup);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005858 File Offset: 0x00003A58
		private void ActivateGroup(AgentBehaviorGroup behaviorGroup)
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				agentBehaviorGroup.IsActive = false;
			}
			behaviorGroup.IsActive = true;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000058B0 File Offset: 0x00003AB0
		private void HandleBehaviorGroups(bool isSimulation)
		{
			if (isSimulation || this._checkBehaviorGroupsTimer.ElapsedTime > 1f)
			{
				this.RefreshBehaviorGroups(isSimulation);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000058D0 File Offset: 0x00003AD0
		private void TickActiveGroups(float dt, bool isSimulation)
		{
			if (!this.OwnerAgent.IsActive())
			{
				return;
			}
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				if (agentBehaviorGroup.IsActive)
				{
					agentBehaviorGroup.Tick(dt, isSimulation);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000593C File Offset: 0x00003B3C
		public AgentBehavior GetActiveBehavior()
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				if (agentBehaviorGroup.IsActive)
				{
					return agentBehaviorGroup.GetActiveBehavior();
				}
			}
			return null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000599C File Offset: 0x00003B9C
		public AgentBehaviorGroup GetActiveBehaviorGroup()
		{
			foreach (AgentBehaviorGroup agentBehaviorGroup in this._behaviorGroups)
			{
				if (agentBehaviorGroup.IsActive)
				{
					return agentBehaviorGroup;
				}
			}
			return null;
		}

		// Token: 0x04000030 RID: 48
		private const float SeeingDistance = 30f;

		// Token: 0x04000031 RID: 49
		public readonly Agent OwnerAgent;

		// Token: 0x04000037 RID: 55
		private readonly Mission _mission;

		// Token: 0x04000038 RID: 56
		private readonly List<AgentBehaviorGroup> _behaviorGroups;

		// Token: 0x04000039 RID: 57
		private readonly ItemObject _specialItem;

		// Token: 0x0400003A RID: 58
		private UsableMachineAIBase _targetBehavior;

		// Token: 0x0400003B RID: 59
		private bool _targetReached;

		// Token: 0x0400003C RID: 60
		private float _rangeThreshold;

		// Token: 0x0400003D RID: 61
		private float _rotationScoreThreshold;

		// Token: 0x0400003E RID: 62
		private string _specialTargetTag;

		// Token: 0x0400003F RID: 63
		private bool _disableClearTargetWhenTargetIsReached;

		// Token: 0x04000041 RID: 65
		private readonly Dictionary<sbyte, string> _prefabNamesForBones;

		// Token: 0x04000042 RID: 66
		private readonly List<int> _prevPrefabs;

		// Token: 0x04000045 RID: 69
		private readonly MissionConversationLogic _conversationHandler;

		// Token: 0x04000046 RID: 70
		private readonly BasicMissionTimer _checkBehaviorGroupsTimer;

		// Token: 0x020000E4 RID: 228
		public enum NavigationState
		{
			// Token: 0x04000453 RID: 1107
			NoTarget,
			// Token: 0x04000454 RID: 1108
			GoToTarget,
			// Token: 0x04000455 RID: 1109
			AtTargetPosition,
			// Token: 0x04000456 RID: 1110
			UseMachine
		}
	}
}
