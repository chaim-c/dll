using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036A RID: 874
	public abstract class UsableMachine : SynchedMissionObject, IFocusable, IOrderable, IDetachment
	{
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x000C4BE4 File Offset: 0x000C2DE4
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x000C4BEC File Offset: 0x000C2DEC
		public MBList<StandingPoint> StandingPoints { get; private set; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000C4BF5 File Offset: 0x000C2DF5
		// (set) Token: 0x06002F95 RID: 12181 RVA: 0x000C4BFD File Offset: 0x000C2DFD
		public StandingPoint PilotStandingPoint { get; private set; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000C4C06 File Offset: 0x000C2E06
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x000C4C0E File Offset: 0x000C2E0E
		protected internal List<StandingPoint> AmmoPickUpPoints { get; private set; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x000C4C17 File Offset: 0x000C2E17
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x000C4C1F File Offset: 0x000C2E1F
		private protected List<GameEntity> WaitStandingPoints { protected get; private set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000C4C28 File Offset: 0x000C2E28
		public DestructableComponent DestructionComponent
		{
			get
			{
				return this._destructionComponent;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000C4C30 File Offset: 0x000C2E30
		public bool IsDestructible
		{
			get
			{
				return this.DestructionComponent != null;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000C4C3B File Offset: 0x000C2E3B
		public bool IsDestroyed
		{
			get
			{
				return this.DestructionComponent != null && this.DestructionComponent.IsDestroyed;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000C4C52 File Offset: 0x000C2E52
		public Agent PilotAgent
		{
			get
			{
				StandingPoint pilotStandingPoint = this.PilotStandingPoint;
				if (pilotStandingPoint == null)
				{
					return null;
				}
				return pilotStandingPoint.UserAgent;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000C4C65 File Offset: 0x000C2E65
		public bool IsLoose
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000C4C68 File Offset: 0x000C2E68
		public UsableMachineAIBase Ai
		{
			get
			{
				if (this._ai == null)
				{
					this._ai = this.CreateAIBehaviorObject();
				}
				return this._ai;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000C4C84 File Offset: 0x000C2E84
		public virtual FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.Item;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000C4C87 File Offset: 0x000C2E87
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x000C4C8F File Offset: 0x000C2E8F
		public StandingPoint CurrentlyUsedAmmoPickUpPoint
		{
			get
			{
				return this._currentlyUsedAmmoPickUpPoint;
			}
			set
			{
				this._currentlyUsedAmmoPickUpPoint = value;
				base.SetScriptComponentToTick(this.GetTickRequirement());
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000C4CA4 File Offset: 0x000C2EA4
		public bool HasAIPickingUpAmmo
		{
			get
			{
				return this.CurrentlyUsedAmmoPickUpPoint != null;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000C4CAF File Offset: 0x000C2EAF
		public MBReadOnlyList<Formation> UserFormations
		{
			get
			{
				return this._userFormations;
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000C4CB8 File Offset: 0x000C2EB8
		protected UsableMachine()
		{
			this._components = new List<UsableMissionObjectComponent>();
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000C4D10 File Offset: 0x000C2F10
		public void AddComponent(UsableMissionObjectComponent component)
		{
			this._components.Add(component);
			component.OnAdded(base.Scene);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000C4D36 File Offset: 0x000C2F36
		public void RemoveComponent(UsableMissionObjectComponent component)
		{
			component.OnRemoved();
			this._components.Remove(component);
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000C4D57 File Offset: 0x000C2F57
		public T GetComponent<T>() where T : UsableMissionObjectComponent
		{
			return this._components.Find((UsableMissionObjectComponent c) => c is T) as T;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000C4D8D File Offset: 0x000C2F8D
		public virtual OrderType GetOrder(BattleSideEnum side)
		{
			return OrderType.Use;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000C4D91 File Offset: 0x000C2F91
		public virtual UsableMachineAIBase CreateAIBehaviorObject()
		{
			return null;
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000C4D94 File Offset: 0x000C2F94
		public GameEntity GetValidStandingPointForAgent(Agent agent)
		{
			float num = float.MaxValue;
			StandingPoint standingPoint = null;
			foreach (StandingPoint standingPoint2 in this.StandingPoints)
			{
				if (!standingPoint2.IsDisabledForAgent(agent) && (!standingPoint2.HasUser || standingPoint2.HasAIUser))
				{
					WorldFrame userFrameForAgent = standingPoint2.GetUserFrameForAgent(agent);
					float num2 = userFrameForAgent.Origin.AsVec2.DistanceSquared(agent.Position.AsVec2);
					if (agent.CanReachAndUseObject(standingPoint2, num2) && num2 < num)
					{
						userFrameForAgent = standingPoint2.GetUserFrameForAgent(agent);
						if (MathF.Abs(userFrameForAgent.Origin.GetGroundVec3().z - agent.Position.z) < 1.5f)
						{
							num = num2;
							standingPoint = standingPoint2;
						}
					}
				}
			}
			if (standingPoint == null)
			{
				return null;
			}
			return standingPoint.GameEntity;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000C4E8C File Offset: 0x000C308C
		public GameEntity GetValidStandingPointForAgentWithoutDistanceCheck(Agent agent)
		{
			float num = float.MaxValue;
			StandingPoint standingPoint = null;
			foreach (StandingPoint standingPoint2 in this.StandingPoints)
			{
				if (!standingPoint2.IsDisabledForAgent(agent) && (!standingPoint2.HasUser || standingPoint2.HasAIUser))
				{
					WorldFrame userFrameForAgent = standingPoint2.GetUserFrameForAgent(agent);
					float num2 = userFrameForAgent.Origin.AsVec2.DistanceSquared(agent.Position.AsVec2);
					if (num2 < num)
					{
						userFrameForAgent = standingPoint2.GetUserFrameForAgent(agent);
						if (MathF.Abs(userFrameForAgent.Origin.GetGroundVec3().z - agent.Position.z) < 1.5f)
						{
							num = num2;
							standingPoint = standingPoint2;
						}
					}
				}
			}
			if (standingPoint == null)
			{
				return null;
			}
			return standingPoint.GameEntity;
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000C4F78 File Offset: 0x000C3178
		public StandingPoint GetVacantStandingPointForAI(Agent agent)
		{
			if (this.PilotStandingPoint != null && !this.PilotStandingPoint.IsDisabledForAgent(agent) && !this.AmmoPickUpPoints.Contains(this.PilotStandingPoint))
			{
				return this.PilotStandingPoint;
			}
			float num = 100000000f;
			StandingPoint result = null;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				bool flag = true;
				if (this.AmmoPickUpPoints.Contains(standingPoint))
				{
					foreach (StandingPoint standingPoint2 in this.StandingPoints)
					{
						if (standingPoint2 is StandingPointWithWeaponRequirement && !this.AmmoPickUpPoints.Contains(standingPoint2) && (standingPoint2.IsDeactivated || standingPoint2.HasUser || standingPoint2.HasAIMovingTo))
						{
							flag = false;
							break;
						}
					}
				}
				if (flag && !standingPoint.IsDisabledForAgent(agent))
				{
					float num2 = (agent.Position - standingPoint.GetUserFrameForAgent(agent).Origin.GetGroundVec3()).LengthSquared;
					if (!standingPoint.IsDisabledForPlayers)
					{
						num2 -= 100000f;
					}
					if (num2 < num)
					{
						num = num2;
						result = standingPoint;
					}
				}
			}
			return result;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000C50E0 File Offset: 0x000C32E0
		public StandingPoint GetTargetStandingPointOfAIAgent(Agent agent)
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (standingPoint.IsAIMovingTo(agent))
				{
					return standingPoint;
				}
			}
			return null;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000C513C File Offset: 0x000C333C
		public override void OnMissionEnded()
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				Agent userAgent = standingPoint.UserAgent;
				if (userAgent != null)
				{
					userAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				standingPoint.IsDeactivated = true;
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000C51A0 File Offset: 0x000C33A0
		public override void SetVisibleSynched(bool value, bool forceChildrenVisible = false)
		{
			base.SetVisibleSynched(value, forceChildrenVisible);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000C51AC File Offset: 0x000C33AC
		public override void SetPhysicsStateSynched(bool value, bool setChildren = true)
		{
			base.SetPhysicsStateSynched(value, setChildren);
			this.SetAbilityOfFaces(value);
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				standingPoint.OnParentMachinePhysicsStateChanged();
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000C520C File Offset: 0x000C340C
		public int UserCountNotInStruckAction
		{
			get
			{
				int num = 0;
				foreach (StandingPoint standingPoint in this.StandingPoints)
				{
					if (standingPoint.HasUser && !standingPoint.UserAgent.IsInBeingStruckAction)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000C5274 File Offset: 0x000C3474
		public int UserCountIncludingInStruckAction
		{
			get
			{
				int num = 0;
				using (List<StandingPoint>.Enumerator enumerator = this.StandingPoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasUser)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000C52D0 File Offset: 0x000C34D0
		public virtual int MaxUserCount
		{
			get
			{
				return this.StandingPoints.Count;
			}
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000C52DD File Offset: 0x000C34DD
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.CollectAndSetStandingPoints();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000C52EC File Offset: 0x000C34EC
		protected internal override void OnInit()
		{
			base.OnInit();
			this._isDisabledForAttackerAIDueToEnemyInRange = new QueryData<bool>(delegate()
			{
				bool result = false;
				if (this.EnemyRangeToStopUsing > 0f)
				{
					Vec3 v = base.GameEntity.GetGlobalFrame().rotation.TransformToParent(new Vec3(this.MachinePositionOffsetToStopUsingLocal, 0f, -1f));
					Vec3 vec = base.GameEntity.GlobalPosition + v;
					Agent closestEnemyAgent = Mission.Current.GetClosestEnemyAgent(Mission.Current.Teams.Attacker, vec, this.EnemyRangeToStopUsing);
					result = (closestEnemyAgent != null && closestEnemyAgent.Position.z > vec.z - 2f && closestEnemyAgent.Position.z < vec.z + 4f);
				}
				return result;
			}, 1f);
			this._isDisabledForDefenderAIDueToEnemyInRange = new QueryData<bool>(delegate()
			{
				bool result = false;
				if (this.EnemyRangeToStopUsing > 0f)
				{
					Vec3 v = base.GameEntity.GetGlobalFrame().rotation.TransformToParent(new Vec3(this.MachinePositionOffsetToStopUsingLocal, 0f, -1f));
					Vec3 vec = base.GameEntity.GlobalPosition + v;
					Agent closestEnemyAgent = Mission.Current.GetClosestEnemyAgent(Mission.Current.Teams.Defender, vec, this.EnemyRangeToStopUsing);
					result = (closestEnemyAgent != null && closestEnemyAgent.Position.z > vec.z - 2f && closestEnemyAgent.Position.z < vec.z + 4f);
				}
				return result;
			}, 1f);
			this.CollectAndSetStandingPoints();
			this.AmmoPickUpPoints = new List<StandingPoint>();
			this._destructionComponent = base.GameEntity.GetFirstScriptOfType<DestructableComponent>();
			this.PilotStandingPoint = null;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (standingPoint.GameEntity.HasTag(this.PilotStandingPointTag))
				{
					this.PilotStandingPoint = standingPoint;
				}
				if (standingPoint.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					this.AmmoPickUpPoints.Add(standingPoint);
				}
				standingPoint.InitializeDefendingAgents();
			}
			this.WaitStandingPoints = base.GameEntity.CollectChildrenEntitiesWithTag(this.WaitStandingPointTag);
			if (this.WaitStandingPoints.Count > 0)
			{
				this.ActiveWaitStandingPoint = this.WaitStandingPoints[0];
			}
			this._userFormations = new MBList<Formation>();
			this._usableStandingPoints = new List<ValueTuple<int, StandingPoint>>();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000C5438 File Offset: 0x000C3638
		private void CollectAndSetStandingPoints()
		{
			GameEntity parent = base.GameEntity.Parent;
			if (parent != null && parent.HasTag("machine_parent"))
			{
				this.StandingPoints = base.GameEntity.Parent.CollectObjects<StandingPoint>();
				return;
			}
			this.StandingPoints = base.GameEntity.CollectObjects<StandingPoint>();
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000C548C File Offset: 0x000C368C
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			bool flag = false;
			using (List<UsableMissionObjectComponent>.Enumerator enumerator = this._components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOnTickRequired())
					{
						flag = true;
						break;
					}
				}
			}
			if (base.GameEntity.IsVisibleIncludeParents() && (flag || (!GameNetwork.IsClientOrReplay && this.HasAIPickingUpAmmo)))
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.Tick;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000C5514 File Offset: 0x000C3714
		protected internal override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this.MakeVisibilityCheck && !base.GameEntity.IsVisibleIncludeParents())
			{
				return;
			}
			if (!GameNetwork.IsClientOrReplay && this.HasAIPickingUpAmmo && !this.CurrentlyUsedAmmoPickUpPoint.HasAIMovingTo && !this.CurrentlyUsedAmmoPickUpPoint.HasAIUser)
			{
				this.CurrentlyUsedAmmoPickUpPoint = null;
			}
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnTick(dt);
			}
			bool isClientOrReplay = GameNetwork.IsClientOrReplay;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000C55B8 File Offset: 0x000C37B8
		private static string DebugGetMemberNameOf<T>(object instance, T sp) where T : class
		{
			Type type = instance.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(propertyInfo.GetMethod == null))
				{
					if (propertyInfo.GetValue(instance) == sp)
					{
						return propertyInfo.Name;
					}
					IReadOnlyList<StandingPoint> readOnlyList;
					if (propertyInfo.GetType().IsGenericType && (propertyInfo.GetType().GetGenericTypeDefinition() == typeof(List<>) || propertyInfo.GetType().GetGenericTypeDefinition() == typeof(MBList<>) || propertyInfo.GetType().GetGenericTypeDefinition() == typeof(MBReadOnlyList<>)) && (readOnlyList = (propertyInfo.GetValue(instance) as IReadOnlyList<StandingPoint>)) != null)
					{
						for (int j = 0; j < readOnlyList.Count; j++)
						{
							StandingPoint standingPoint = readOnlyList[j];
							if (sp == standingPoint)
							{
								return string.Concat(new object[]
								{
									propertyInfo.Name,
									"[",
									j,
									"]"
								});
							}
						}
					}
				}
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (fieldInfo.GetValue(instance) == sp)
				{
					return fieldInfo.Name;
				}
				IReadOnlyList<StandingPoint> readOnlyList2;
				if (fieldInfo.FieldType.IsGenericType && (fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(MBList<>) || fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(MBReadOnlyList<>)) && (readOnlyList2 = (fieldInfo.GetValue(instance) as IReadOnlyList<StandingPoint>)) != null)
				{
					for (int k = 0; k < readOnlyList2.Count; k++)
					{
						StandingPoint standingPoint2 = readOnlyList2[k];
						if (sp == standingPoint2)
						{
							return string.Concat(new object[]
							{
								fieldInfo.Name,
								"[",
								k,
								"]"
							});
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000C57F8 File Offset: 0x000C39F8
		[Conditional("_RGL_KEEP_ASSERTS")]
		protected virtual void DebugTick(float dt)
		{
			if (MBDebug.IsDisplayingHighLevelAI)
			{
				foreach (StandingPoint standingPoint in this.StandingPoints)
				{
					Vec3 globalPosition = standingPoint.GameEntity.GlobalPosition;
					Vec3.One / 3f;
					bool isDeactivated = standingPoint.IsDeactivated;
				}
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000C586C File Offset: 0x000C3A6C
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorTick(dt);
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000C58C4 File Offset: 0x000C3AC4
		protected internal override void OnEditorValidate()
		{
			base.OnEditorValidate();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnEditorValidate();
			}
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000C591C File Offset: 0x000C3B1C
		public virtual void OnFocusGain(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusGain(userAgent);
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000C5970 File Offset: 0x000C3B70
		public virtual void OnFocusLose(Agent userAgent)
		{
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnFocusLose(userAgent);
			}
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000C59C4 File Offset: 0x000C3BC4
		public virtual TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return TextObject.Empty;
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000C59CB File Offset: 0x000C3BCB
		public virtual bool HasWaitFrame
		{
			get
			{
				return this.ActiveWaitStandingPoint != null;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000C59DC File Offset: 0x000C3BDC
		public MatrixFrame WaitFrame
		{
			get
			{
				if (this.ActiveWaitStandingPoint != null)
				{
					return this.ActiveWaitStandingPoint.GetGlobalFrame();
				}
				return default(MatrixFrame);
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000C5A0C File Offset: 0x000C3C0C
		public GameEntity WaitEntity
		{
			get
			{
				return this.ActiveWaitStandingPoint;
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000C5A14 File Offset: 0x000C3C14
		protected internal override void OnMissionReset()
		{
			base.OnMissionReset();
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnMissionReset();
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000C5A6C File Offset: 0x000C3C6C
		public virtual bool IsDeactivated
		{
			get
			{
				return this._isMachineDeactivated || this.IsDestroyed;
			}
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000C5A80 File Offset: 0x000C3C80
		public void Deactivate()
		{
			this._isMachineDeactivated = true;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				standingPoint.IsDeactivated = true;
			}
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000C5AD8 File Offset: 0x000C3CD8
		public void Activate()
		{
			this._isMachineDeactivated = false;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				standingPoint.IsDeactivated = false;
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000C5B30 File Offset: 0x000C3D30
		public virtual bool IsDisabledForBattleSide(BattleSideEnum sideEnum)
		{
			return this.IsDeactivated;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000C5B38 File Offset: 0x000C3D38
		public virtual bool IsDisabledForBattleSideAI(BattleSideEnum sideEnum)
		{
			return this._isDisabledForAI || (this.EnemyRangeToStopUsing > 0f && sideEnum != BattleSideEnum.None && this.IsDisabledDueToEnemyInRange(sideEnum));
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000C5B60 File Offset: 0x000C3D60
		public virtual bool ShouldAutoLeaveDetachmentWhenDisabled(BattleSideEnum sideEnum)
		{
			return true;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000C5B63 File Offset: 0x000C3D63
		protected bool IsDisabledDueToEnemyInRange(BattleSideEnum sideEnum)
		{
			if (sideEnum == BattleSideEnum.Attacker)
			{
				return this._isDisabledForAttackerAIDueToEnemyInRange.Value;
			}
			return this._isDisabledForDefenderAIDueToEnemyInRange.Value;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000C5B80 File Offset: 0x000C3D80
		public virtual bool AutoAttachUserToFormation(BattleSideEnum sideEnum)
		{
			return true;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000C5B83 File Offset: 0x000C3D83
		public virtual bool HasToBeDefendedByUser(BattleSideEnum sideEnum)
		{
			return false;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000C5B88 File Offset: 0x000C3D88
		public virtual void Disable()
		{
			foreach (Team team in from t in Mission.Current.Teams
			where t.DetachmentManager.ContainsDetachment(this)
			select t)
			{
				team.DetachmentManager.DestroyDetachment(this);
			}
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (!standingPoint.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					if (standingPoint.HasUser)
					{
						standingPoint.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
					}
					standingPoint.SetIsDeactivatedSynched(true);
				}
			}
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000C5C58 File Offset: 0x000C3E58
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				usableMissionObjectComponent.OnRemoved();
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000C5CB0 File Offset: 0x000C3EB0
		public override string ToString()
		{
			string text = base.GetType() + " with Components:";
			foreach (UsableMissionObjectComponent usableMissionObjectComponent in this._components)
			{
				text = string.Concat(new object[]
				{
					text,
					"[",
					usableMissionObjectComponent,
					"]"
				});
			}
			return text;
		}

		// Token: 0x06002FD1 RID: 12241
		public abstract TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject);

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000C5D34 File Offset: 0x000C3F34
		public virtual StandingPoint GetBestPointAlternativeTo(StandingPoint standingPoint, Agent agent)
		{
			return standingPoint;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000C5D38 File Offset: 0x000C3F38
		public virtual bool IsInRangeToCheckAlternativePoints(Agent agent)
		{
			float num = (this.StandingPoints.Count > 0) ? (agent.GetInteractionDistanceToUsable(this.StandingPoints[0]) + 1f) : 2f;
			return base.GameEntity.GlobalPosition.DistanceSquared(agent.Position) < num * num;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000C5D94 File Offset: 0x000C3F94
		void IDetachment.OnFormationLeave(Formation formation)
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				Agent userAgent = standingPoint.UserAgent;
				if (userAgent != null && userAgent.Formation == formation && !userAgent.IsPlayerControlled)
				{
					this.OnFormationLeaveHelper(formation, userAgent);
				}
				Agent movingAgent = standingPoint.MovingAgent;
				if (movingAgent != null && movingAgent.Formation == formation)
				{
					this.OnFormationLeaveHelper(formation, movingAgent);
				}
				for (int i = standingPoint.GetDefendingAgentCount() - 1; i >= 0; i--)
				{
					Agent agent = standingPoint.DefendingAgents[i];
					if (agent.Formation == formation)
					{
						this.OnFormationLeaveHelper(formation, agent);
					}
				}
			}
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000C5E5C File Offset: 0x000C405C
		private void OnFormationLeaveHelper(Formation formation, Agent agent)
		{
			((IDetachment)this).RemoveAgent(agent);
			formation.AttachUnit(agent);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000C5E6C File Offset: 0x000C406C
		bool IDetachment.IsAgentUsingOrInterested(Agent agent)
		{
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (agent.CurrentlyUsedGameObject == standingPoint || (agent.IsAIControlled && agent.AIInterestedInGameObject(standingPoint)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000C5EDC File Offset: 0x000C40DC
		protected virtual float GetWeightOfStandingPoint(StandingPoint sp)
		{
			if (!sp.HasAIMovingTo)
			{
				return 0.6f;
			}
			return 0.2f;
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000C5EF1 File Offset: 0x000C40F1
		float IDetachment.GetDetachmentWeight(BattleSideEnum side)
		{
			return this.GetDetachmentWeightAux(side);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000C5EFC File Offset: 0x000C40FC
		protected virtual float GetDetachmentWeightAux(BattleSideEnum side)
		{
			if (this.IsDisabledForBattleSideAI(side))
			{
				return float.MinValue;
			}
			this._usableStandingPoints.Clear();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.StandingPoints.Count; i++)
			{
				StandingPoint standingPoint = this.StandingPoints[i];
				if (standingPoint.IsUsableBySide(side))
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
						goto IL_81;
					}
					flag = true;
					this._usableStandingPoints.Add(new ValueTuple<int, StandingPoint>(i, standingPoint));
				}
				IL_81:;
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

		// Token: 0x06002FDA RID: 12250 RVA: 0x000C5FC8 File Offset: 0x000C41C8
		void IDetachment.GetSlotIndexWeightTuples(List<ValueTuple<int, float>> slotIndexWeightTuples)
		{
			foreach (ValueTuple<int, StandingPoint> valueTuple in this._usableStandingPoints)
			{
				StandingPoint item = valueTuple.Item2;
				slotIndexWeightTuples.Add(new ValueTuple<int, float>(valueTuple.Item1, this.GetWeightOfStandingPoint(item) * ((!this._areUsableStandingPointsVacant && item.HasRecentlyBeenRechecked) ? 0.1f : 1f)));
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000C6050 File Offset: 0x000C4250
		bool IDetachment.IsSlotAtIndexAvailableForAgent(int slotIndex, Agent agent)
		{
			return agent.CanBeAssignedForScriptedMovement() && !this.StandingPoints[slotIndex].IsDisabledForAgent(agent) && !this.IsAgentOnInconvenientNavmesh(agent, this.StandingPoints[slotIndex]);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000C6088 File Offset: 0x000C4288
		protected virtual bool IsAgentOnInconvenientNavmesh(Agent agent, StandingPoint standingPoint)
		{
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege)
			{
				return false;
			}
			int currentNavigationFaceId = agent.GetCurrentNavigationFaceId();
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = (agent.Team.TeamAI as TeamAISiegeComponent)) != null)
			{
				if (teamAISiegeComponent is TeamAISiegeAttacker && currentNavigationFaceId % 10 == 1)
				{
					return true;
				}
				if (teamAISiegeComponent is TeamAISiegeDefender && currentNavigationFaceId % 10 != 1)
				{
					return true;
				}
				foreach (int num in teamAISiegeComponent.DifficultNavmeshIDs)
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

		// Token: 0x06002FDD RID: 12253 RVA: 0x000C6130 File Offset: 0x000C4330
		bool IDetachment.IsAgentEligible(Agent agent)
		{
			return true;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000C6134 File Offset: 0x000C4334
		public void AddAgentAtSlotIndex(Agent agent, int slotIndex)
		{
			StandingPoint standingPoint = this.StandingPoints[slotIndex];
			if (standingPoint.HasAIMovingTo)
			{
				Agent movingAgent = standingPoint.MovingAgent;
				if (movingAgent != null)
				{
					((IDetachment)this).RemoveAgent(movingAgent);
					Formation formation = movingAgent.Formation;
					if (formation != null)
					{
						formation.AttachUnit(movingAgent);
					}
				}
			}
			if (standingPoint.HasDefendingAgent)
			{
				for (int i = standingPoint.DefendingAgents.Count - 1; i >= 0; i--)
				{
					Agent agent2 = standingPoint.DefendingAgents[i];
					if (agent2 != null)
					{
						((IDetachment)this).RemoveAgent(agent2);
						Formation formation2 = agent2.Formation;
						if (formation2 != null)
						{
							formation2.AttachUnit(agent2);
						}
					}
				}
			}
			((IDetachment)this).AddAgent(agent, slotIndex);
			Formation formation3 = agent.Formation;
			if (formation3 != null)
			{
				formation3.DetachUnit(agent, false);
			}
			agent.Detachment = this;
			agent.DetachmentWeight = 1f;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000C61EF File Offset: 0x000C43EF
		Agent IDetachment.GetMovingAgentAtSlotIndex(int slotIndex)
		{
			return this.StandingPoints[slotIndex].MovingAgent;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000C6202 File Offset: 0x000C4402
		bool IDetachment.IsDetachmentRecentlyEvaluated()
		{
			return this._isDetachmentRecentlyEvaluated;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000C620A File Offset: 0x000C440A
		void IDetachment.UnmarkDetachment()
		{
			this._isDetachmentRecentlyEvaluated = false;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000C6214 File Offset: 0x000C4414
		void IDetachment.MarkSlotAtIndex(int slotIndex)
		{
			int count = this._usableStandingPoints.Count;
			int num = this._reevaluatedCount + 1;
			this._reevaluatedCount = num;
			if (num >= count)
			{
				foreach (ValueTuple<int, StandingPoint> valueTuple in this._usableStandingPoints)
				{
					valueTuple.Item2.HasRecentlyBeenRechecked = false;
				}
				this._isDetachmentRecentlyEvaluated = true;
				this._reevaluatedCount = 0;
				return;
			}
			this.StandingPoints[slotIndex].HasRecentlyBeenRechecked = true;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000C62AC File Offset: 0x000C44AC
		float? IDetachment.GetWeightOfNextSlot(BattleSideEnum side)
		{
			if (this.IsDisabledForBattleSideAI(side))
			{
				return null;
			}
			StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(side, null, null, null);
			if (suitableStandingPointFor != null)
			{
				return new float?(this.GetWeightOfStandingPoint(suitableStandingPointFor));
			}
			return null;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000C62F0 File Offset: 0x000C44F0
		float IDetachment.GetExactCostOfAgentAtSlot(Agent candidate, int slotIndex)
		{
			StandingPoint standingPoint = this.StandingPoints[slotIndex];
			Vec3 globalPosition = standingPoint.GameEntity.GlobalPosition;
			WorldPosition worldPosition = new WorldPosition(candidate.Mission.Scene, globalPosition);
			WorldPosition worldPosition2 = candidate.GetWorldPosition();
			float maxValue;
			if (!standingPoint.Scene.GetPathDistanceBetweenPositions(ref worldPosition, ref worldPosition2, candidate.Monster.BodyCapsuleRadius, out maxValue))
			{
				maxValue = float.MaxValue;
			}
			return maxValue;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000C6354 File Offset: 0x000C4554
		List<float> IDetachment.GetTemplateCostsOfAgent(Agent candidate, List<float> oldValue)
		{
			List<float> list = oldValue ?? new List<float>(this.StandingPoints.Count);
			list.Clear();
			for (int i = 0; i < this.StandingPoints.Count; i++)
			{
				list.Add(float.MaxValue);
			}
			foreach (ValueTuple<int, StandingPoint> valueTuple in this._usableStandingPoints)
			{
				float num = valueTuple.Item2.GameEntity.GlobalPosition.Distance(candidate.Position);
				list[valueTuple.Item1] = num * MissionGameModels.Current.AgentStatCalculateModel.GetDetachmentCostMultiplierOfAgent(candidate, this);
			}
			return list;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000C6420 File Offset: 0x000C4620
		float IDetachment.GetTemplateWeightOfAgent(Agent candidate)
		{
			Scene scene = Mission.Current.Scene;
			Vec3 globalPosition = base.GameEntity.GlobalPosition;
			WorldPosition worldPosition = candidate.GetWorldPosition();
			WorldPosition worldPosition2 = new WorldPosition(scene, UIntPtr.Zero, globalPosition, true);
			float maxValue;
			if (!scene.GetPathDistanceBetweenPositions(ref worldPosition2, ref worldPosition, candidate.Monster.BodyCapsuleRadius, out maxValue))
			{
				maxValue = float.MaxValue;
			}
			return maxValue;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000C647C File Offset: 0x000C467C
		float IDetachment.GetWeightOfOccupiedSlot(Agent agent)
		{
			return this.GetWeightOfStandingPoint(this.StandingPoints.FirstOrDefault((StandingPoint sp) => sp.UserAgent == agent || sp.IsAIMovingTo(agent)));
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000C64B4 File Offset: 0x000C46B4
		WorldFrame? IDetachment.GetAgentFrame(Agent agent)
		{
			return null;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000C64CA File Offset: 0x000C46CA
		void IDetachment.RemoveAgent(Agent agent)
		{
			agent.StopUsingGameObjectMT(true, Agent.StopUsingGameObjectFlags.None);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000C64D4 File Offset: 0x000C46D4
		public int GetNumberOfUsableSlots()
		{
			return this._usableStandingPoints.Count;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000C64E4 File Offset: 0x000C46E4
		public bool IsStandingPointAvailableForAgent(Agent agent)
		{
			bool result = false;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (!standingPoint.IsDeactivated && (standingPoint.IsInstantUse || ((!standingPoint.HasUser || standingPoint.UserAgent == agent) && (!standingPoint.HasAIMovingTo || standingPoint.IsAIMovingTo(agent)))) && !standingPoint.IsDisabledForAgent(agent) && !this.IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(standingPoint))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000C657C File Offset: 0x000C477C
		float? IDetachment.GetWeightOfAgentAtNextSlot(List<Agent> candidates, out Agent match)
		{
			BattleSideEnum side = candidates[0].Team.Side;
			StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(side, null, candidates, null);
			if (suitableStandingPointFor == null)
			{
				match = null;
				return null;
			}
			match = UsableMachineAIBase.GetSuitableAgentForStandingPoint(this, suitableStandingPointFor, candidates, new List<Agent>());
			if (match == null)
			{
				return null;
			}
			float? weightOfNextSlot = ((IDetachment)this).GetWeightOfNextSlot(side);
			float num = 1f;
			float? num2 = weightOfNextSlot;
			float num3 = num;
			if (num2 == null)
			{
				return null;
			}
			return new float?(num2.GetValueOrDefault() * num3);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000C6608 File Offset: 0x000C4808
		float? IDetachment.GetWeightOfAgentAtNextSlot(List<ValueTuple<Agent, float>> candidates, out Agent match)
		{
			BattleSideEnum side = candidates[0].Item1.Team.Side;
			StandingPoint suitableStandingPointFor = this.GetSuitableStandingPointFor(side, null, null, candidates);
			if (suitableStandingPointFor == null)
			{
				match = null;
				return null;
			}
			float? weightOfNextSlot = ((IDetachment)this).GetWeightOfNextSlot(side);
			match = UsableMachineAIBase.GetSuitableAgentForStandingPoint(this, suitableStandingPointFor, candidates, new List<Agent>(), weightOfNextSlot.Value);
			if (match == null)
			{
				return null;
			}
			float num = 1f;
			float? num2 = weightOfNextSlot;
			float num3 = num;
			if (num2 == null)
			{
				return null;
			}
			return new float?(num2.GetValueOrDefault() * num3);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000C66A0 File Offset: 0x000C48A0
		float? IDetachment.GetWeightOfAgentAtOccupiedSlot(Agent detachedAgent, List<Agent> candidates, out Agent match)
		{
			BattleSideEnum side = candidates[0].Team.Side;
			match = null;
			foreach (StandingPoint standingPoint in this.StandingPoints)
			{
				if (standingPoint.IsAIMovingTo(detachedAgent) || standingPoint.UserAgent == detachedAgent)
				{
					match = UsableMachineAIBase.GetSuitableAgentForStandingPoint(this, standingPoint, candidates, new List<Agent>());
					break;
				}
			}
			if (match == null)
			{
				return null;
			}
			float? weightOfNextSlot = ((IDetachment)this).GetWeightOfNextSlot(side);
			float num = 1f;
			if (weightOfNextSlot == null)
			{
				return null;
			}
			return new float?(weightOfNextSlot.GetValueOrDefault() * num * 0.5f);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000C676C File Offset: 0x000C496C
		void IDetachment.AddAgent(Agent agent, int slotIndex)
		{
			StandingPoint standingPoint = (slotIndex == -1) ? this.GetSuitableStandingPointFor(agent.Team.Side, agent, null, null) : this.StandingPoints[slotIndex];
			if (standingPoint != null)
			{
				if (standingPoint.HasAIMovingTo && !standingPoint.IsInstantUse)
				{
					standingPoint.MovingAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
				}
				agent.AIMoveToGameObjectEnable(standingPoint, this, this.Ai.GetScriptedFrameFlags(agent));
				if (standingPoint.GameEntity.HasTag(this.AmmoPickUpTag))
				{
					this.CurrentlyUsedAmmoPickUpPoint = standingPoint;
					return;
				}
			}
			else
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Usables\\UsableMachine.cs", "AddAgent", 1367);
			}
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000C6808 File Offset: 0x000C4A08
		void IDetachment.FormationStartUsing(Formation formation)
		{
			this._userFormations.Add(formation);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000C6816 File Offset: 0x000C4A16
		void IDetachment.FormationStopUsing(Formation formation)
		{
			this._userFormations.Remove(formation);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000C6825 File Offset: 0x000C4A25
		public bool IsUsedByFormation(Formation formation)
		{
			return this._userFormations.Contains(formation);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000C6833 File Offset: 0x000C4A33
		void IDetachment.ResetEvaluation()
		{
			this._isEvaluated = false;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000C683C File Offset: 0x000C4A3C
		bool IDetachment.IsEvaluated()
		{
			return this._isEvaluated;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000C6844 File Offset: 0x000C4A44
		void IDetachment.SetAsEvaluated()
		{
			this._isEvaluated = true;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000C684D File Offset: 0x000C4A4D
		float IDetachment.GetDetachmentWeightFromCache()
		{
			return this._cachedDetachmentWeight;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000C6855 File Offset: 0x000C4A55
		float IDetachment.ComputeAndCacheDetachmentWeight(BattleSideEnum side)
		{
			this._cachedDetachmentWeight = this.GetDetachmentWeightAux(side);
			return this._cachedDetachmentWeight;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000C686A File Offset: 0x000C4A6A
		protected internal virtual bool IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(StandingPoint standingPoint)
		{
			return this.AmmoPickUpPoints.Contains(standingPoint) && (this.StandingPoints.Any((StandingPoint standingPoint2) => (standingPoint2.IsDeactivated || standingPoint2.HasUser || standingPoint2.HasAIMovingTo) && !standingPoint2.GameEntity.HasTag(this.AmmoPickUpTag) && standingPoint2 is StandingPointWithWeaponRequirement) || this.HasAIPickingUpAmmo);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000C68A4 File Offset: 0x000C4AA4
		protected virtual StandingPoint GetSuitableStandingPointFor(BattleSideEnum side, Agent agent = null, List<Agent> agents = null, List<ValueTuple<Agent, float>> agentValuePairs = null)
		{
			return this.StandingPoints.FirstOrDefault((StandingPoint sp) => !sp.IsDeactivated && (sp.IsInstantUse || (!sp.HasUser && !sp.HasAIMovingTo)) && (agent == null || !sp.IsDisabledForAgent(agent)) && (agents == null || agents.Any((Agent a) => !sp.IsDisabledForAgent(a))) && (agentValuePairs == null || agentValuePairs.Any((ValueTuple<Agent, float> avp) => !sp.IsDisabledForAgent(avp.Item1))) && !this.IsStandingPointNotUsedOnAccountOfBeingAmmoLoad(sp));
		}

		// Token: 0x06002FFA RID: 12282
		public abstract string GetDescriptionText(GameEntity gameEntity = null);

		// Token: 0x0400145A RID: 5210
		public const string UsableMachineParentTag = "machine_parent";

		// Token: 0x0400145B RID: 5211
		public string PilotStandingPointTag = "Pilot";

		// Token: 0x0400145C RID: 5212
		public string AmmoPickUpTag = "ammopickup";

		// Token: 0x0400145D RID: 5213
		public string WaitStandingPointTag = "Wait";

		// Token: 0x04001462 RID: 5218
		protected GameEntity ActiveWaitStandingPoint;

		// Token: 0x04001463 RID: 5219
		private readonly List<UsableMissionObjectComponent> _components;

		// Token: 0x04001464 RID: 5220
		private DestructableComponent _destructionComponent;

		// Token: 0x04001465 RID: 5221
		protected bool _areUsableStandingPointsVacant = true;

		// Token: 0x04001466 RID: 5222
		protected List<ValueTuple<int, StandingPoint>> _usableStandingPoints;

		// Token: 0x04001467 RID: 5223
		protected bool _isDetachmentRecentlyEvaluated;

		// Token: 0x04001468 RID: 5224
		private int _reevaluatedCount;

		// Token: 0x04001469 RID: 5225
		private bool _isEvaluated;

		// Token: 0x0400146A RID: 5226
		private float _cachedDetachmentWeight;

		// Token: 0x0400146B RID: 5227
		protected float EnemyRangeToStopUsing;

		// Token: 0x0400146C RID: 5228
		protected Vec2 MachinePositionOffsetToStopUsingLocal = Vec2.Zero;

		// Token: 0x0400146D RID: 5229
		protected bool MakeVisibilityCheck = true;

		// Token: 0x0400146E RID: 5230
		private UsableMachineAIBase _ai;

		// Token: 0x0400146F RID: 5231
		private StandingPoint _currentlyUsedAmmoPickUpPoint;

		// Token: 0x04001470 RID: 5232
		private QueryData<bool> _isDisabledForAttackerAIDueToEnemyInRange;

		// Token: 0x04001471 RID: 5233
		private QueryData<bool> _isDisabledForDefenderAIDueToEnemyInRange;

		// Token: 0x04001472 RID: 5234
		protected bool _isDisabledForAI;

		// Token: 0x04001473 RID: 5235
		protected MBList<Formation> _userFormations;

		// Token: 0x04001474 RID: 5236
		private bool _isMachineDeactivated;
	}
}
