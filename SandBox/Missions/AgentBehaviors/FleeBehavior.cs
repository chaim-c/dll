using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Missions.MissionLogics;
using SandBox.Objects.Usables;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x0200007F RID: 127
	public class FleeBehavior : AgentBehavior
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00021121 File Offset: 0x0001F321
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0002112C File Offset: 0x0001F32C
		private FleeBehavior.FleeTargetType SelectedFleeTargetType
		{
			get
			{
				return this._selectedFleeTargetType;
			}
			set
			{
				if (value != this._selectedFleeTargetType)
				{
					this._selectedFleeTargetType = value;
					MBActionSet actionSet = base.OwnerAgent.ActionSet;
					ActionIndexValueCache currentActionValue = base.OwnerAgent.GetCurrentActionValue(1);
					if (this._selectedFleeTargetType != FleeBehavior.FleeTargetType.Cover && !actionSet.AreActionsAlternatives(currentActionValue, FleeBehavior.act_scared_idle_1) && !actionSet.AreActionsAlternatives(currentActionValue, FleeBehavior.act_scared_reaction_1))
					{
						base.OwnerAgent.SetActionChannel(1, FleeBehavior.act_scared_reaction_1, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					}
					if (this._selectedFleeTargetType == FleeBehavior.FleeTargetType.Cover)
					{
						this.BeAfraid();
					}
					this._selectedGoal.GoToTarget();
				}
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000211DC File Offset: 0x0001F3DC
		public FleeBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			this._missionFightHandler = base.Mission.GetMissionBehavior<MissionFightHandler>();
			this._reconsiderFleeTargetTimer = new BasicMissionTimer();
			this._state = FleeBehavior.State.None;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0002121C File Offset: 0x0001F41C
		public override void Tick(float dt, bool isSimulation)
		{
			switch (this._state)
			{
			case FleeBehavior.State.None:
				base.OwnerAgent.DisableScriptedMovement();
				base.OwnerAgent.SetActionChannel(1, FleeBehavior.act_scared_reaction_1, false, 0UL, 0f, 1f, -0.2f, 0.4f, MBRandom.RandomFloat, false, -0.2f, 0, true);
				this._selectedGoal = new FleeBehavior.FleeCoverTarget(base.Navigator, base.OwnerAgent);
				this.SelectedFleeTargetType = FleeBehavior.FleeTargetType.Cover;
				return;
			case FleeBehavior.State.Afraid:
				if (this._scareTimer.ElapsedTime > this._scareTime)
				{
					this._state = FleeBehavior.State.LookForPlace;
					this._scareTimer = null;
					return;
				}
				break;
			case FleeBehavior.State.LookForPlace:
				this.LookForPlace();
				return;
			case FleeBehavior.State.Flee:
				this.Flee();
				return;
			case FleeBehavior.State.Complain:
				if (this._complainToGuardTimer != null && this._complainToGuardTimer.ElapsedTime > 2f)
				{
					this._complainToGuardTimer = null;
					base.OwnerAgent.SetActionChannel(0, ActionIndexCache.act_none, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					base.OwnerAgent.SetLookAgent(null);
					(this._selectedGoal as FleeBehavior.FleeAgentTarget).Savior.SetLookAgent(null);
					AlarmedBehaviorGroup.AlarmAgent((this._selectedGoal as FleeBehavior.FleeAgentTarget).Savior);
					this._state = FleeBehavior.State.LookForPlace;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00021378 File Offset: 0x0001F578
		private Vec3 GetDangerPosition()
		{
			Vec3 vec = Vec3.Zero;
			if (this._missionFightHandler != null)
			{
				IEnumerable<Agent> dangerSources = this._missionFightHandler.GetDangerSources(base.OwnerAgent);
				if (dangerSources.Any<Agent>())
				{
					foreach (Agent agent in dangerSources)
					{
						vec += agent.Position;
					}
					vec /= (float)dangerSources.Count<Agent>();
				}
			}
			return vec;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00021400 File Offset: 0x0001F600
		private bool IsThereDanger()
		{
			return this._missionFightHandler != null && this._missionFightHandler.GetDangerSources(base.OwnerAgent).Any<Agent>();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00021424 File Offset: 0x0001F624
		private float GetPathScore(WorldPosition startWorldPos, WorldPosition targetWorldPos)
		{
			float num = 1f;
			NavigationPath navigationPath = new NavigationPath();
			base.Mission.Scene.GetPathBetweenAIFaces(startWorldPos.GetNearestNavMesh(), targetWorldPos.GetNearestNavMesh(), startWorldPos.AsVec2, targetWorldPos.AsVec2, 0f, navigationPath, null);
			Vec2 asVec = this.GetDangerPosition().AsVec2;
			float toAngle = MBMath.WrapAngle((asVec - startWorldPos.AsVec2).RotationInRadians);
			float num2 = MathF.Abs(MBMath.GetSmallestDifferenceBetweenTwoAngles(MBMath.WrapAngle((navigationPath.Size > 0) ? (navigationPath.PathPoints[0] - startWorldPos.AsVec2).RotationInRadians : (targetWorldPos.AsVec2 - startWorldPos.AsVec2).RotationInRadians), toAngle)) / 3.1415927f * 1f;
			float num3 = startWorldPos.AsVec2.DistanceSquared(asVec);
			if (navigationPath.Size > 0)
			{
				float num4 = float.MaxValue;
				Vec2 line = startWorldPos.AsVec2;
				for (int i = 0; i < navigationPath.Size; i++)
				{
					float num5 = Vec2.DistanceToLineSegmentSquared(navigationPath.PathPoints[i], line, asVec);
					line = navigationPath.PathPoints[i];
					if (num5 < num4)
					{
						num4 = num5;
					}
				}
				if (num3 > num4 && num4 < 25f)
				{
					num = 1f * (num4 - num3) / 225f;
				}
				else if (num3 > 4f)
				{
					num = 1f * num4 / 225f;
				}
				else
				{
					num = 1f;
				}
			}
			float num6 = 1f * (225f / startWorldPos.AsVec2.DistanceSquared(targetWorldPos.AsVec2));
			return (1f + num2) * (1f + num2) - 2f + num + num6;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00021608 File Offset: 0x0001F808
		private void LookForPlace()
		{
			FleeBehavior.FleeGoalBase selectedGoal = new FleeBehavior.FleeCoverTarget(base.Navigator, base.OwnerAgent);
			FleeBehavior.FleeTargetType selectedFleeTargetType = FleeBehavior.FleeTargetType.Cover;
			if (this.IsThereDanger())
			{
				List<ValueTuple<float, Agent>> availableGuardScores = this.GetAvailableGuardScores(5);
				List<ValueTuple<float, Passage>> availablePassageScores = this.GetAvailablePassageScores(10);
				float num = float.MinValue;
				foreach (ValueTuple<float, Passage> valueTuple in availablePassageScores)
				{
					float item = valueTuple.Item1;
					if (item > num)
					{
						num = item;
						selectedFleeTargetType = FleeBehavior.FleeTargetType.Indoor;
						selectedGoal = new FleeBehavior.FleePassageTarget(base.Navigator, base.OwnerAgent, valueTuple.Item2);
					}
				}
				foreach (ValueTuple<float, Agent> valueTuple2 in availableGuardScores)
				{
					float item2 = valueTuple2.Item1;
					if (item2 > num)
					{
						num = item2;
						selectedFleeTargetType = FleeBehavior.FleeTargetType.Guard;
						selectedGoal = new FleeBehavior.FleeAgentTarget(base.Navigator, base.OwnerAgent, valueTuple2.Item2);
					}
				}
			}
			this._selectedGoal = selectedGoal;
			this.SelectedFleeTargetType = selectedFleeTargetType;
			this._state = FleeBehavior.State.Flee;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002172C File Offset: 0x0001F92C
		private bool ShouldChangeTarget()
		{
			if (this._selectedFleeTargetType == FleeBehavior.FleeTargetType.Guard)
			{
				WorldPosition worldPosition = (this._selectedGoal as FleeBehavior.FleeAgentTarget).Savior.GetWorldPosition();
				WorldPosition worldPosition2 = base.OwnerAgent.GetWorldPosition();
				return this.GetPathScore(worldPosition2, worldPosition) <= 1f && this.IsThereASafePlaceToEscape();
			}
			if (this._selectedFleeTargetType != FleeBehavior.FleeTargetType.Indoor)
			{
				return true;
			}
			StandingPoint vacantStandingPointForAI = (this._selectedGoal as FleeBehavior.FleePassageTarget).EscapePortal.GetVacantStandingPointForAI(base.OwnerAgent);
			if (vacantStandingPointForAI == null)
			{
				return true;
			}
			WorldPosition worldPosition3 = base.OwnerAgent.GetWorldPosition();
			WorldPosition origin = vacantStandingPointForAI.GetUserFrameForAgent(base.OwnerAgent).Origin;
			return this.GetPathScore(worldPosition3, origin) <= 1f && this.IsThereASafePlaceToEscape();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000217E0 File Offset: 0x0001F9E0
		private bool IsThereASafePlaceToEscape()
		{
			if (!this.GetAvailablePassageScores(1).Any((ValueTuple<float, Passage> d) => d.Item1 > 1f))
			{
				return this.GetAvailableGuardScores(1).Any((ValueTuple<float, Agent> d) => d.Item1 > 1f);
			}
			return true;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00021848 File Offset: 0x0001FA48
		private List<ValueTuple<float, Passage>> GetAvailablePassageScores(int maxPaths = 10)
		{
			WorldPosition worldPosition = base.OwnerAgent.GetWorldPosition();
			List<ValueTuple<float, Passage>> list = new List<ValueTuple<float, Passage>>();
			List<ValueTuple<float, Passage>> list2 = new List<ValueTuple<float, Passage>>();
			List<ValueTuple<WorldPosition, Passage>> list3 = new List<ValueTuple<WorldPosition, Passage>>();
			if (this._missionAgentHandler.TownPassageProps != null)
			{
				foreach (UsableMachine usableMachine in this._missionAgentHandler.TownPassageProps)
				{
					StandingPoint vacantStandingPointForAI = usableMachine.GetVacantStandingPointForAI(base.OwnerAgent);
					Passage passage = usableMachine as Passage;
					if (vacantStandingPointForAI != null && passage != null)
					{
						WorldPosition origin = vacantStandingPointForAI.GetUserFrameForAgent(base.OwnerAgent).Origin;
						list3.Add(new ValueTuple<WorldPosition, Passage>(origin, passage));
					}
				}
			}
			list3 = (from a in list3
			orderby base.OwnerAgent.Position.AsVec2.DistanceSquared(a.Item1.AsVec2)
			select a).ToList<ValueTuple<WorldPosition, Passage>>();
			foreach (ValueTuple<WorldPosition, Passage> valueTuple in list3)
			{
				WorldPosition item = valueTuple.Item1;
				if (item.IsValid && !(item.GetNearestNavMesh() == UIntPtr.Zero))
				{
					float pathScore = this.GetPathScore(worldPosition, item);
					ValueTuple<float, Passage> item2 = new ValueTuple<float, Passage>(pathScore, valueTuple.Item2);
					list.Add(item2);
					if (pathScore > 1f)
					{
						list2.Add(item2);
					}
					if (list2.Count >= maxPaths)
					{
						break;
					}
				}
			}
			if (list2.Count > 0)
			{
				return list2;
			}
			return list;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000219C4 File Offset: 0x0001FBC4
		private List<ValueTuple<float, Agent>> GetAvailableGuardScores(int maxGuards = 5)
		{
			WorldPosition worldPosition = base.OwnerAgent.GetWorldPosition();
			List<ValueTuple<float, Agent>> list = new List<ValueTuple<float, Agent>>();
			List<ValueTuple<float, Agent>> list2 = new List<ValueTuple<float, Agent>>();
			List<Agent> list3 = new List<Agent>();
			foreach (Agent agent in base.OwnerAgent.Team.ActiveAgents)
			{
				CharacterObject characterObject;
				if ((characterObject = (agent.Character as CharacterObject)) != null && agent.IsAIControlled && agent.CurrentWatchState != Agent.WatchState.Alarmed && (characterObject.Occupation == Occupation.Soldier || characterObject.Occupation == Occupation.Guard || characterObject.Occupation == Occupation.PrisonGuard))
				{
					list3.Add(agent);
				}
			}
			list3 = (from a in list3
			orderby base.OwnerAgent.Position.DistanceSquared(a.Position)
			select a).ToList<Agent>();
			foreach (Agent agent2 in list3)
			{
				WorldPosition worldPosition2 = agent2.GetWorldPosition();
				if (worldPosition2.IsValid)
				{
					float pathScore = this.GetPathScore(worldPosition, worldPosition2);
					ValueTuple<float, Agent> item = new ValueTuple<float, Agent>(pathScore, agent2);
					list.Add(item);
					if (pathScore > 1f)
					{
						list2.Add(item);
					}
					if (list2.Count >= maxGuards)
					{
						break;
					}
				}
			}
			if (list2.Count > 0)
			{
				return list2;
			}
			return list;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00021B30 File Offset: 0x0001FD30
		protected override void OnActivate()
		{
			base.OnActivate();
			this._state = FleeBehavior.State.None;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00021B40 File Offset: 0x0001FD40
		private void Flee()
		{
			if (this._selectedGoal.IsGoalAchievable())
			{
				if (this._selectedGoal.IsGoalAchieved())
				{
					this._selectedGoal.TargetReached();
					FleeBehavior.FleeTargetType selectedFleeTargetType = this.SelectedFleeTargetType;
					if (selectedFleeTargetType == FleeBehavior.FleeTargetType.Guard)
					{
						this._complainToGuardTimer = new BasicMissionTimer();
						this._state = FleeBehavior.State.Complain;
						return;
					}
					if (selectedFleeTargetType == FleeBehavior.FleeTargetType.Cover && this._reconsiderFleeTargetTimer.ElapsedTime > 0.5f)
					{
						this._state = FleeBehavior.State.LookForPlace;
						this._reconsiderFleeTargetTimer.Reset();
						return;
					}
				}
				else
				{
					if (this.SelectedFleeTargetType == FleeBehavior.FleeTargetType.Guard)
					{
						this._selectedGoal.GoToTarget();
					}
					if (this._reconsiderFleeTargetTimer.ElapsedTime > 1f)
					{
						this._reconsiderFleeTargetTimer.Reset();
						if (this.ShouldChangeTarget())
						{
							this._state = FleeBehavior.State.LookForPlace;
							return;
						}
					}
				}
			}
			else
			{
				this._state = FleeBehavior.State.LookForPlace;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00021C07 File Offset: 0x0001FE07
		private void BeAfraid()
		{
			this._scareTimer = new BasicMissionTimer();
			this._scareTime = 0.5f + MBRandom.RandomFloat * 0.5f;
			this._state = FleeBehavior.State.Afraid;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00021C32 File Offset: 0x0001FE32
		public override string GetDebugInfo()
		{
			return "Flee " + this._state;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00021C49 File Offset: 0x0001FE49
		public override float GetAvailability(bool isSimulation)
		{
			if (base.Mission.CurrentTime < 3f)
			{
				return 0f;
			}
			if (!MissionFightHandler.IsAgentAggressive(base.OwnerAgent))
			{
				return 0.9f;
			}
			return 0.1f;
		}

		// Token: 0x04000246 RID: 582
		private static readonly ActionIndexCache act_scared_reaction_1 = ActionIndexCache.Create("act_scared_reaction_1");

		// Token: 0x04000247 RID: 583
		private static readonly ActionIndexCache act_scared_idle_1 = ActionIndexCache.Create("act_scared_idle_1");

		// Token: 0x04000248 RID: 584
		private static readonly ActionIndexCache act_cheer_1 = ActionIndexCache.Create("act_cheer_1");

		// Token: 0x04000249 RID: 585
		public const float ScoreThreshold = 1f;

		// Token: 0x0400024A RID: 586
		public const float DangerDistance = 5f;

		// Token: 0x0400024B RID: 587
		public const float ImmediateDangerDistance = 2f;

		// Token: 0x0400024C RID: 588
		public const float DangerDistanceSquared = 25f;

		// Token: 0x0400024D RID: 589
		public const float ImmediateDangerDistanceSquared = 4f;

		// Token: 0x0400024E RID: 590
		private readonly MissionAgentHandler _missionAgentHandler;

		// Token: 0x0400024F RID: 591
		private readonly MissionFightHandler _missionFightHandler;

		// Token: 0x04000250 RID: 592
		private FleeBehavior.State _state;

		// Token: 0x04000251 RID: 593
		private readonly BasicMissionTimer _reconsiderFleeTargetTimer;

		// Token: 0x04000252 RID: 594
		private const float ReconsiderImmobilizedFleeTargetTime = 0.5f;

		// Token: 0x04000253 RID: 595
		private const float ReconsiderDefaultFleeTargetTime = 1f;

		// Token: 0x04000254 RID: 596
		private FleeBehavior.FleeGoalBase _selectedGoal;

		// Token: 0x04000255 RID: 597
		private BasicMissionTimer _scareTimer;

		// Token: 0x04000256 RID: 598
		private float _scareTime;

		// Token: 0x04000257 RID: 599
		private BasicMissionTimer _complainToGuardTimer;

		// Token: 0x04000258 RID: 600
		private const float ComplainToGuardTime = 2f;

		// Token: 0x04000259 RID: 601
		private FleeBehavior.FleeTargetType _selectedFleeTargetType;

		// Token: 0x0200014B RID: 331
		private abstract class FleeGoalBase
		{
			// Token: 0x06000C2D RID: 3117 RVA: 0x00053485 File Offset: 0x00051685
			protected FleeGoalBase(AgentNavigator navigator, Agent ownerAgent)
			{
				this._navigator = navigator;
				this._ownerAgent = ownerAgent;
			}

			// Token: 0x06000C2E RID: 3118
			public abstract void TargetReached();

			// Token: 0x06000C2F RID: 3119
			public abstract void GoToTarget();

			// Token: 0x06000C30 RID: 3120
			public abstract bool IsGoalAchievable();

			// Token: 0x06000C31 RID: 3121
			public abstract bool IsGoalAchieved();

			// Token: 0x040005AD RID: 1453
			protected readonly AgentNavigator _navigator;

			// Token: 0x040005AE RID: 1454
			protected readonly Agent _ownerAgent;
		}

		// Token: 0x0200014C RID: 332
		private class FleeAgentTarget : FleeBehavior.FleeGoalBase
		{
			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0005349B File Offset: 0x0005169B
			// (set) Token: 0x06000C33 RID: 3123 RVA: 0x000534A3 File Offset: 0x000516A3
			public Agent Savior { get; private set; }

			// Token: 0x06000C34 RID: 3124 RVA: 0x000534AC File Offset: 0x000516AC
			public FleeAgentTarget(AgentNavigator navigator, Agent ownerAgent, Agent savior) : base(navigator, ownerAgent)
			{
				this.Savior = savior;
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x000534C0 File Offset: 0x000516C0
			public override void GoToTarget()
			{
				this._navigator.SetTargetFrame(this.Savior.GetWorldPosition(), this.Savior.Frame.rotation.f.AsVec2.RotationInRadians, 0.2f, 0.02f, Agent.AIScriptedFrameFlags.NoAttack | Agent.AIScriptedFrameFlags.NeverSlowDown, false);
			}

			// Token: 0x06000C36 RID: 3126 RVA: 0x00053518 File Offset: 0x00051718
			public override bool IsGoalAchievable()
			{
				return this.Savior.GetWorldPosition().GetNearestNavMesh() != UIntPtr.Zero && this._navigator.TargetPosition.IsValid && this.Savior.IsActive() && this.Savior.CurrentWatchState != Agent.WatchState.Alarmed;
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x0005357C File Offset: 0x0005177C
			public override bool IsGoalAchieved()
			{
				return this._navigator.TargetPosition.IsValid && this._navigator.TargetPosition.GetGroundVec3().Distance(this._ownerAgent.Position) <= this._ownerAgent.GetInteractionDistanceToUsable(this.Savior);
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x000535DC File Offset: 0x000517DC
			public override void TargetReached()
			{
				this._ownerAgent.SetActionChannel(0, FleeBehavior.act_cheer_1, true, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				this._ownerAgent.SetActionChannel(1, ActionIndexCache.act_none, true, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				this._ownerAgent.DisableScriptedMovement();
				this.Savior.DisableScriptedMovement();
				this.Savior.SetLookAgent(this._ownerAgent);
				this._ownerAgent.SetLookAgent(this.Savior);
			}
		}

		// Token: 0x0200014D RID: 333
		private class FleePassageTarget : FleeBehavior.FleeGoalBase
		{
			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0005368D File Offset: 0x0005188D
			// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00053695 File Offset: 0x00051895
			public Passage EscapePortal { get; private set; }

			// Token: 0x06000C3B RID: 3131 RVA: 0x0005369E File Offset: 0x0005189E
			public FleePassageTarget(AgentNavigator navigator, Agent ownerAgent, Passage escapePortal) : base(navigator, ownerAgent)
			{
				this.EscapePortal = escapePortal;
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x000536AF File Offset: 0x000518AF
			public override void GoToTarget()
			{
				this._navigator.SetTarget(this.EscapePortal, false);
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x000536C3 File Offset: 0x000518C3
			public override bool IsGoalAchievable()
			{
				return this.EscapePortal.GetVacantStandingPointForAI(this._ownerAgent) != null && !this.EscapePortal.IsDestroyed;
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x000536E8 File Offset: 0x000518E8
			public override bool IsGoalAchieved()
			{
				StandingPoint vacantStandingPointForAI = this.EscapePortal.GetVacantStandingPointForAI(this._ownerAgent);
				return vacantStandingPointForAI != null && vacantStandingPointForAI.IsUsableByAgent(this._ownerAgent);
			}

			// Token: 0x06000C3F RID: 3135 RVA: 0x00053718 File Offset: 0x00051918
			public override void TargetReached()
			{
			}
		}

		// Token: 0x0200014E RID: 334
		private class FleePositionTarget : FleeBehavior.FleeGoalBase
		{
			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0005371A File Offset: 0x0005191A
			// (set) Token: 0x06000C41 RID: 3137 RVA: 0x00053722 File Offset: 0x00051922
			public Vec3 Position { get; private set; }

			// Token: 0x06000C42 RID: 3138 RVA: 0x0005372B File Offset: 0x0005192B
			public FleePositionTarget(AgentNavigator navigator, Agent ownerAgent, Vec3 position) : base(navigator, ownerAgent)
			{
				this.Position = position;
			}

			// Token: 0x06000C43 RID: 3139 RVA: 0x0005373C File Offset: 0x0005193C
			public override void GoToTarget()
			{
			}

			// Token: 0x06000C44 RID: 3140 RVA: 0x00053740 File Offset: 0x00051940
			public override bool IsGoalAchievable()
			{
				return this._navigator.TargetPosition.IsValid;
			}

			// Token: 0x06000C45 RID: 3141 RVA: 0x00053760 File Offset: 0x00051960
			public override bool IsGoalAchieved()
			{
				return this._navigator.TargetPosition.IsValid && this._navigator.IsTargetReached();
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x0005378F File Offset: 0x0005198F
			public override void TargetReached()
			{
			}
		}

		// Token: 0x0200014F RID: 335
		private class FleeCoverTarget : FleeBehavior.FleeGoalBase
		{
			// Token: 0x06000C47 RID: 3143 RVA: 0x00053791 File Offset: 0x00051991
			public FleeCoverTarget(AgentNavigator navigator, Agent ownerAgent) : base(navigator, ownerAgent)
			{
			}

			// Token: 0x06000C48 RID: 3144 RVA: 0x0005379B File Offset: 0x0005199B
			public override void GoToTarget()
			{
				this._ownerAgent.DisableScriptedMovement();
			}

			// Token: 0x06000C49 RID: 3145 RVA: 0x000537A8 File Offset: 0x000519A8
			public override bool IsGoalAchievable()
			{
				return true;
			}

			// Token: 0x06000C4A RID: 3146 RVA: 0x000537AB File Offset: 0x000519AB
			public override bool IsGoalAchieved()
			{
				return true;
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x000537AE File Offset: 0x000519AE
			public override void TargetReached()
			{
			}
		}

		// Token: 0x02000150 RID: 336
		private enum State
		{
			// Token: 0x040005B3 RID: 1459
			None,
			// Token: 0x040005B4 RID: 1460
			Afraid,
			// Token: 0x040005B5 RID: 1461
			LookForPlace,
			// Token: 0x040005B6 RID: 1462
			Flee,
			// Token: 0x040005B7 RID: 1463
			Complain
		}

		// Token: 0x02000151 RID: 337
		private enum FleeTargetType
		{
			// Token: 0x040005B9 RID: 1465
			Indoor,
			// Token: 0x040005BA RID: 1466
			Guard,
			// Token: 0x040005BB RID: 1467
			Cover
		}
	}
}
