using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x0200007D RID: 125
	public class EscortAgentBehavior : AgentBehavior
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00020713 File Offset: 0x0001E913
		public Agent EscortedAgent
		{
			get
			{
				return this._escortedAgent;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0002071B File Offset: 0x0001E91B
		public Agent TargetAgent
		{
			get
			{
				return this._targetAgent;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00020723 File Offset: 0x0001E923
		public EscortAgentBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._targetAgent = null;
			this._escortedAgent = null;
			this._myLastStateWasRunning = false;
			this._initialMaxSpeedLimit = 1f;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0002074C File Offset: 0x0001E94C
		public void Initialize(Agent escortedAgent, Agent targetAgent, EscortAgentBehavior.OnTargetReachedDelegate onTargetReached = null)
		{
			this._escortedAgent = escortedAgent;
			this._targetAgent = targetAgent;
			this._targetMachine = null;
			this._targetPosition = null;
			this._onTargetReached = onTargetReached;
			this._escortFinished = false;
			this._initialMaxSpeedLimit = base.OwnerAgent.GetMaximumSpeedLimit();
			this._state = EscortAgentBehavior.State.Escorting;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000207A0 File Offset: 0x0001E9A0
		public void Initialize(Agent escortedAgent, UsableMachine targetMachine, EscortAgentBehavior.OnTargetReachedDelegate onTargetReached = null)
		{
			this._escortedAgent = escortedAgent;
			this._targetAgent = null;
			this._targetMachine = targetMachine;
			this._targetPosition = null;
			this._onTargetReached = onTargetReached;
			this._escortFinished = false;
			this._initialMaxSpeedLimit = base.OwnerAgent.GetMaximumSpeedLimit();
			this._state = EscortAgentBehavior.State.Escorting;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000207F4 File Offset: 0x0001E9F4
		public void Initialize(Agent escortedAgent, Vec3? targetPosition, EscortAgentBehavior.OnTargetReachedDelegate onTargetReached = null)
		{
			this._escortedAgent = escortedAgent;
			this._targetAgent = null;
			this._targetMachine = null;
			this._targetPosition = targetPosition;
			this._onTargetReached = onTargetReached;
			this._escortFinished = false;
			this._initialMaxSpeedLimit = base.OwnerAgent.GetMaximumSpeedLimit();
			this._state = EscortAgentBehavior.State.Escorting;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00020844 File Offset: 0x0001EA44
		public override void Tick(float dt, bool isSimulation)
		{
			if (this._escortedAgent == null || !this._escortedAgent.IsActive() || this._targetAgent == null || !this._targetAgent.IsActive())
			{
				this._state = EscortAgentBehavior.State.NotEscorting;
			}
			if (this._escortedAgent != null && this._state != EscortAgentBehavior.State.NotEscorting)
			{
				this.ControlMovement();
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00020898 File Offset: 0x0001EA98
		public bool IsEscortFinished()
		{
			return this._escortFinished;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000208A0 File Offset: 0x0001EAA0
		private void ControlMovement()
		{
			int nearbyEnemyAgentCount = base.Mission.GetNearbyEnemyAgentCount(this._escortedAgent.Team, this._escortedAgent.Position.AsVec2, 5f);
			if (this._state != EscortAgentBehavior.State.NotEscorting && nearbyEnemyAgentCount > 0)
			{
				this._state = EscortAgentBehavior.State.NotEscorting;
				base.OwnerAgent.ResetLookAgent();
				base.Navigator.ClearTarget();
				base.OwnerAgent.DisableScriptedMovement();
				base.OwnerAgent.SetMaximumSpeedLimit(this._initialMaxSpeedLimit, false);
				Debug.Print("[Escort agent behavior] Escorted agent got into a fight... Disable!", 0, Debug.DebugColor.White, 17592186044416UL);
				return;
			}
			float rangeThreshold = base.OwnerAgent.HasMount ? 2.2f : 1.2f;
			float num = base.OwnerAgent.Position.DistanceSquared(this._escortedAgent.Position);
			float num2;
			WorldPosition targetPosition;
			float targetRotation;
			if (this._targetAgent != null)
			{
				num2 = base.OwnerAgent.Position.DistanceSquared(this._targetAgent.Position);
				targetPosition = this._targetAgent.GetWorldPosition();
				MatrixFrame frame = this._targetAgent.Frame;
				targetRotation = frame.rotation.f.AsVec2.RotationInRadians;
			}
			else if (this._targetMachine != null)
			{
				MatrixFrame globalFrame = this._targetMachine.GameEntity.GetGlobalFrame();
				num2 = base.OwnerAgent.Position.DistanceSquared(globalFrame.origin);
				targetPosition = globalFrame.origin.ToWorldPosition();
				targetRotation = globalFrame.rotation.f.AsVec2.RotationInRadians;
			}
			else if (this._targetPosition != null)
			{
				num2 = base.OwnerAgent.Position.DistanceSquared(this._targetPosition.Value);
				targetPosition = this._targetPosition.Value.ToWorldPosition();
				targetRotation = (this._targetPosition.Value - base.OwnerAgent.Position).AsVec2.RotationInRadians;
			}
			else
			{
				Debug.FailedAssert("At least one target must be specified for the escort behavior.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\AgentBehaviors\\EscortAgentBehavior.cs", "ControlMovement", 158);
				num2 = 0f;
				targetPosition = base.OwnerAgent.GetWorldPosition();
				targetRotation = 0f;
			}
			if (this._escortFinished)
			{
				bool flag = false;
				base.OwnerAgent.SetMaximumSpeedLimit(this._initialMaxSpeedLimit, false);
				if (this._onTargetReached != null)
				{
					flag = this._onTargetReached(base.OwnerAgent, ref this._escortedAgent, ref this._targetAgent, ref this._targetMachine, ref this._targetPosition);
				}
				if (flag && this._escortedAgent != null && (this._targetAgent != null || this._targetMachine != null || this._targetPosition != null))
				{
					this._state = EscortAgentBehavior.State.Escorting;
				}
				else
				{
					this._state = EscortAgentBehavior.State.NotEscorting;
				}
			}
			switch (this._state)
			{
			case EscortAgentBehavior.State.ReturnToEscortedAgent:
				if (num < 25f)
				{
					this._state = EscortAgentBehavior.State.Wait;
				}
				else
				{
					WorldPosition worldPosition = this._escortedAgent.GetWorldPosition();
					MatrixFrame frame = this._escortedAgent.Frame;
					this.SetMovePos(worldPosition, frame.rotation.f.AsVec2.RotationInRadians, rangeThreshold);
				}
				break;
			case EscortAgentBehavior.State.Wait:
				if (num < 25f)
				{
					this._state = EscortAgentBehavior.State.Escorting;
					Debug.Print("[Escort agent behavior] Escorting!", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				else if (num > 100f)
				{
					this._state = EscortAgentBehavior.State.ReturnToEscortedAgent;
					Debug.Print("[Escort agent behavior] Escorted agent is too far away! Return to escorted agent!", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				else
				{
					WorldPosition worldPosition2 = base.OwnerAgent.GetWorldPosition();
					MatrixFrame frame = base.OwnerAgent.Frame;
					this.SetMovePos(worldPosition2, frame.rotation.f.AsVec2.RotationInRadians, 0f);
				}
				break;
			case EscortAgentBehavior.State.Escorting:
				if (num >= 25f)
				{
					this._state = EscortAgentBehavior.State.Wait;
					Debug.Print("[Escort agent behavior] Stop walking! Wait", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				else
				{
					this.SetMovePos(targetPosition, targetRotation, 3f);
				}
				break;
			}
			if (this._state == EscortAgentBehavior.State.Escorting && num2 < 16f && num < 16f)
			{
				this._escortFinished = true;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00020CC8 File Offset: 0x0001EEC8
		private void SetMovePos(WorldPosition targetPosition, float targetRotation, float rangeThreshold)
		{
			Agent.AIScriptedFrameFlags aiscriptedFrameFlags = Agent.AIScriptedFrameFlags.NoAttack;
			if (base.Navigator.CharacterHasVisiblePrefabs)
			{
				this._myLastStateWasRunning = false;
			}
			else
			{
				float num = base.OwnerAgent.Position.AsVec2.Distance(targetPosition.AsVec2);
				float length = this._escortedAgent.Velocity.AsVec2.Length;
				if (num - rangeThreshold <= 0.5f * (this._myLastStateWasRunning ? 1f : 1.2f) && length <= base.OwnerAgent.Monster.WalkingSpeedLimit * (this._myLastStateWasRunning ? 1f : 1.2f))
				{
					this._myLastStateWasRunning = false;
				}
				else
				{
					base.OwnerAgent.SetMaximumSpeedLimit(num - rangeThreshold + length, false);
					this._myLastStateWasRunning = true;
				}
			}
			if (!this._myLastStateWasRunning)
			{
				aiscriptedFrameFlags |= Agent.AIScriptedFrameFlags.DoNotRun;
			}
			base.Navigator.SetTargetFrame(targetPosition, targetRotation, rangeThreshold, -10f, aiscriptedFrameFlags, false);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00020DBB File Offset: 0x0001EFBB
		public override float GetAvailability(bool isSimulation)
		{
			return (float)((this._state == EscortAgentBehavior.State.NotEscorting) ? 0 : 1);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00020DCC File Offset: 0x0001EFCC
		protected override void OnDeactivate()
		{
			this._escortedAgent = null;
			this._targetAgent = null;
			this._targetMachine = null;
			this._targetPosition = null;
			this._onTargetReached = null;
			this._state = EscortAgentBehavior.State.NotEscorting;
			base.OwnerAgent.DisableScriptedMovement();
			base.OwnerAgent.ResetLookAgent();
			base.Navigator.ClearTarget();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00020E2C File Offset: 0x0001F02C
		public override string GetDebugInfo()
		{
			return string.Concat(new object[]
			{
				"Escort ",
				this._escortedAgent.Name,
				" (id:",
				this._escortedAgent.Index,
				")",
				(this._targetAgent != null) ? string.Concat(new object[]
				{
					" to ",
					this._targetAgent.Name,
					" (id:",
					this._targetAgent.Index,
					")"
				}) : ((this._targetMachine != null) ? string.Concat(new object[]
				{
					" to ",
					this._targetMachine,
					"(id:",
					this._targetMachine.Id,
					")"
				}) : ((this._targetPosition != null) ? (" to position: " + this._targetPosition.Value) : " to NO TARGET"))
			});
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00020F4C File Offset: 0x0001F14C
		public static void AddEscortAgentBehavior(Agent ownerAgent, Agent targetAgent, EscortAgentBehavior.OnTargetReachedDelegate onTargetReached)
		{
			AgentNavigator agentNavigator = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
			InterruptingBehaviorGroup interruptingBehaviorGroup = (agentNavigator != null) ? agentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>() : null;
			if (interruptingBehaviorGroup == null)
			{
				return;
			}
			bool flag = interruptingBehaviorGroup.GetBehavior<EscortAgentBehavior>() == null;
			EscortAgentBehavior escortAgentBehavior = interruptingBehaviorGroup.GetBehavior<EscortAgentBehavior>() ?? interruptingBehaviorGroup.AddBehavior<EscortAgentBehavior>();
			if (flag)
			{
				interruptingBehaviorGroup.SetScriptedBehavior<EscortAgentBehavior>();
			}
			escortAgentBehavior.Initialize(Agent.Main, targetAgent, onTargetReached);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		public static void RemoveEscortBehaviorOfAgent(Agent ownerAgent)
		{
			AgentNavigator agentNavigator = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
			InterruptingBehaviorGroup interruptingBehaviorGroup = (agentNavigator != null) ? agentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>() : null;
			if (interruptingBehaviorGroup == null)
			{
				return;
			}
			if (interruptingBehaviorGroup.GetBehavior<EscortAgentBehavior>() != null)
			{
				interruptingBehaviorGroup.RemoveBehavior<EscortAgentBehavior>();
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00020FDC File Offset: 0x0001F1DC
		public static bool CheckIfAgentIsEscortedBy(Agent ownerAgent, Agent escortedAgent)
		{
			AgentNavigator agentNavigator = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
			InterruptingBehaviorGroup interruptingBehaviorGroup = (agentNavigator != null) ? agentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>() : null;
			EscortAgentBehavior escortAgentBehavior = (interruptingBehaviorGroup != null) ? interruptingBehaviorGroup.GetBehavior<EscortAgentBehavior>() : null;
			return escortAgentBehavior != null && escortAgentBehavior.EscortedAgent == escortedAgent;
		}

		// Token: 0x04000236 RID: 566
		private const float StartWaitingDistanceSquared = 25f;

		// Token: 0x04000237 RID: 567
		private const float ReturnToEscortedAgentDistanceSquared = 100f;

		// Token: 0x04000238 RID: 568
		private const float EscortFinishedDistanceSquared = 16f;

		// Token: 0x04000239 RID: 569
		private const float TargetProximityThreshold = 3f;

		// Token: 0x0400023A RID: 570
		private const float MountedMoveProximityThreshold = 2.2f;

		// Token: 0x0400023B RID: 571
		private const float OnFootMoveProximityThreshold = 1.2f;

		// Token: 0x0400023C RID: 572
		private EscortAgentBehavior.State _state;

		// Token: 0x0400023D RID: 573
		private Agent _escortedAgent;

		// Token: 0x0400023E RID: 574
		private Agent _targetAgent;

		// Token: 0x0400023F RID: 575
		private UsableMachine _targetMachine;

		// Token: 0x04000240 RID: 576
		private Vec3? _targetPosition;

		// Token: 0x04000241 RID: 577
		private bool _myLastStateWasRunning;

		// Token: 0x04000242 RID: 578
		private float _initialMaxSpeedLimit;

		// Token: 0x04000243 RID: 579
		private EscortAgentBehavior.OnTargetReachedDelegate _onTargetReached;

		// Token: 0x04000244 RID: 580
		private bool _escortFinished;

		// Token: 0x02000149 RID: 329
		// (Invoke) Token: 0x06000C2A RID: 3114
		public delegate bool OnTargetReachedDelegate(Agent agent, ref Agent escortedAgent, ref Agent targetAgent, ref UsableMachine targetMachine, ref Vec3? targetPosition);

		// Token: 0x0200014A RID: 330
		private enum State
		{
			// Token: 0x040005A9 RID: 1449
			NotEscorting,
			// Token: 0x040005AA RID: 1450
			ReturnToEscortedAgent,
			// Token: 0x040005AB RID: 1451
			Wait,
			// Token: 0x040005AC RID: 1452
			Escorting
		}
	}
}
