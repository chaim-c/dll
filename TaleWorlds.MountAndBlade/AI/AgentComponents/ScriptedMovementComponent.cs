using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.AI.AgentComponents
{
	// Token: 0x020003D2 RID: 978
	public class ScriptedMovementComponent : AgentComponent
	{
		// Token: 0x060033A9 RID: 13225 RVA: 0x000D6050 File Offset: 0x000D4250
		public ScriptedMovementComponent(Agent agent, bool isCharacterToTalkTo = false, float dialogueProximityOffset = 0f) : base(agent)
		{
			this._isCharacterToTalkTo = isCharacterToTalkTo;
			this._agentSpeedLimit = this.Agent.GetMaximumSpeedLimit();
			if (!this._isCharacterToTalkTo)
			{
				this.Agent.SetMaximumSpeedLimit(MBRandom.RandomFloatRanged(0.2f, 0.3f), true);
				this._dialogueTriggerProximity += dialogueProximityOffset;
			}
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000D60B8 File Offset: 0x000D42B8
		public void SetTargetAgent(Agent targetAgent)
		{
			this._targetAgent = targetAgent;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000D60C4 File Offset: 0x000D42C4
		public override void OnTickAsAI(float dt)
		{
			if (this._targetAgent != null)
			{
				bool flag = this._targetAgent.State != AgentState.Routed && this._targetAgent.State != AgentState.Deleted;
				if (!this._isInDialogueRange)
				{
					float num = this._targetAgent.Position.DistanceSquared(this.Agent.Position);
					this._isInDialogueRange = (num <= this._dialogueTriggerProximity * this._dialogueTriggerProximity);
					if (this._isInDialogueRange)
					{
						this.Agent.SetScriptedFlags(this.Agent.GetScriptedFlags() & ~Agent.AIScriptedFrameFlags.DoNotRun);
						this.Agent.DisableScriptedMovement();
						if (flag)
						{
							this.Agent.SetLookAgent(this._targetAgent);
						}
						this.Agent.SetMaximumSpeedLimit(this._agentSpeedLimit, false);
						return;
					}
					WorldPosition worldPosition = this._targetAgent.Position.ToWorldPosition();
					this.Agent.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.DoNotRun);
					return;
				}
				else if (!flag)
				{
					this.Agent.SetLookAgent(null);
				}
			}
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000D61C6 File Offset: 0x000D43C6
		public bool ShouldConversationStartWithAgent()
		{
			return this._isInDialogueRange && this._isCharacterToTalkTo;
		}

		// Token: 0x04001661 RID: 5729
		private bool _isInDialogueRange;

		// Token: 0x04001662 RID: 5730
		private readonly bool _isCharacterToTalkTo;

		// Token: 0x04001663 RID: 5731
		private readonly float _dialogueTriggerProximity = 10f;

		// Token: 0x04001664 RID: 5732
		private readonly float _agentSpeedLimit;

		// Token: 0x04001665 RID: 5733
		private Agent _targetAgent;
	}
}
