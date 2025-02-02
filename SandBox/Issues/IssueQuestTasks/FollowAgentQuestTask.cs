using System;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace SandBox.Issues.IssueQuestTasks
{
	// Token: 0x02000091 RID: 145
	public class FollowAgentQuestTask : QuestTaskBase
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x000253B0 File Offset: 0x000235B0
		public FollowAgentQuestTask(Agent followedAgent, GameEntity targetEntity, Action onSucceededAction, Action onCanceledAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, null, onCanceledAction)
		{
			this._followedAgent = followedAgent;
			this._followedAgentChar = (CharacterObject)this._followedAgent.Character;
			this._targetEntity = targetEntity;
			this.StartAgentMovement();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000253E8 File Offset: 0x000235E8
		public FollowAgentQuestTask(Agent followedAgent, Agent targetAgent, Action onSucceededAction, Action onCanceledAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, null, onCanceledAction)
		{
			this._followedAgent = followedAgent;
			this._targetAgent = targetAgent;
			this.StartAgentMovement();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002540C File Offset: 0x0002360C
		private void StartAgentMovement()
		{
			if (this._targetEntity != null)
			{
				UsableMachine firstScriptOfType = this._targetEntity.GetFirstScriptOfType<UsableMachine>();
				ScriptBehavior.AddUsableMachineTarget(this._followedAgent, firstScriptOfType);
				return;
			}
			if (this._targetAgent != null)
			{
				ScriptBehavior.AddAgentTarget(this._followedAgent, this._targetAgent);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002545C File Offset: 0x0002365C
		public void MissionTick(float dt)
		{
			ScriptBehavior scriptBehavior = (ScriptBehavior)this._followedAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehavior<ScriptBehavior>();
			if (scriptBehavior != null && scriptBehavior.IsNearTarget(this._targetAgent) && this._followedAgent.GetCurrentVelocity().LengthSquared < 0.0001f && this._followedAgent.Position.DistanceSquared(Mission.Current.MainAgent.Position) < 16f)
			{
				base.Finish(QuestTaskBase.FinishStates.Success);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000254E4 File Offset: 0x000236E4
		protected override void OnFinished()
		{
			this._followedAgent = null;
			this._followedAgentChar = null;
			this._targetEntity = null;
			this._targetAgent = null;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00025502 File Offset: 0x00023702
		public override void SetReferences()
		{
			CampaignEvents.MissionTickEvent.AddNonSerializedListener(this, new Action<float>(this.MissionTick));
		}

		// Token: 0x040002A2 RID: 674
		private Agent _followedAgent;

		// Token: 0x040002A3 RID: 675
		private CharacterObject _followedAgentChar;

		// Token: 0x040002A4 RID: 676
		private GameEntity _targetEntity;

		// Token: 0x040002A5 RID: 677
		private Agent _targetAgent;
	}
}
