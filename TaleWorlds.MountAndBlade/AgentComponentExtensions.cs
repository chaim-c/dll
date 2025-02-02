using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000105 RID: 261
	public static class AgentComponentExtensions
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x00016CD4 File Offset: 0x00014ED4
		public static float GetMorale(this Agent agent)
		{
			CommonAIComponent commonAIComponent = agent.CommonAIComponent;
			if (commonAIComponent != null)
			{
				return commonAIComponent.Morale;
			}
			return -1f;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00016CF8 File Offset: 0x00014EF8
		public static void SetMorale(this Agent agent, float morale)
		{
			CommonAIComponent commonAIComponent = agent.CommonAIComponent;
			if (commonAIComponent != null)
			{
				commonAIComponent.Morale = morale;
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00016D18 File Offset: 0x00014F18
		public static void ChangeMorale(this Agent agent, float delta)
		{
			CommonAIComponent commonAIComponent = agent.CommonAIComponent;
			if (commonAIComponent != null)
			{
				commonAIComponent.Morale += delta;
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00016D40 File Offset: 0x00014F40
		public static bool IsRetreating(this Agent agent, bool isComponentAssured = true)
		{
			CommonAIComponent commonAIComponent = agent.CommonAIComponent;
			return commonAIComponent != null && commonAIComponent.IsRetreating;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00016D5F File Offset: 0x00014F5F
		public static void Retreat(this Agent agent, bool useCachingSystem = false)
		{
			CommonAIComponent commonAIComponent = agent.CommonAIComponent;
			if (commonAIComponent == null)
			{
				return;
			}
			commonAIComponent.Retreat(useCachingSystem);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00016D72 File Offset: 0x00014F72
		public static void StopRetreatingMoraleComponent(this Agent agent)
		{
			CommonAIComponent commonAIComponent = agent.CommonAIComponent;
			if (commonAIComponent == null)
			{
				return;
			}
			commonAIComponent.StopRetreating();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00016D84 File Offset: 0x00014F84
		public static void SetBehaviorValueSet(this Agent agent, HumanAIComponent.BehaviorValueSet behaviorValueSet)
		{
			HumanAIComponent humanAIComponent = agent.HumanAIComponent;
			if (humanAIComponent == null)
			{
				return;
			}
			humanAIComponent.SetBehaviorValueSet(behaviorValueSet);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00016D97 File Offset: 0x00014F97
		public static void RefreshBehaviorValues(this Agent agent, MovementOrder.MovementOrderEnum movementOrder, ArrangementOrder.ArrangementOrderEnum arrangementOrder)
		{
			HumanAIComponent humanAIComponent = agent.HumanAIComponent;
			if (humanAIComponent == null)
			{
				return;
			}
			humanAIComponent.RefreshBehaviorValues(movementOrder, arrangementOrder);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00016DAB File Offset: 0x00014FAB
		public static void SetAIBehaviorValues(this Agent agent, HumanAIComponent.AISimpleBehaviorKind behavior, float y1, float x2, float y2, float x3, float y3)
		{
			HumanAIComponent humanAIComponent = agent.HumanAIComponent;
			if (humanAIComponent == null)
			{
				return;
			}
			humanAIComponent.SetBehaviorParams(behavior, y1, x2, y2, x3, y3);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00016DC6 File Offset: 0x00014FC6
		public static void AIMoveToGameObjectEnable(this Agent agent, UsableMissionObject usedObject, IDetachment detachment, Agent.AIScriptedFrameFlags scriptedFrameFlags = Agent.AIScriptedFrameFlags.NoAttack)
		{
			agent.HumanAIComponent.MoveToUsableGameObject(usedObject, detachment, scriptedFrameFlags);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00016DD6 File Offset: 0x00014FD6
		public static void AIMoveToGameObjectDisable(this Agent agent)
		{
			agent.HumanAIComponent.MoveToClear();
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00016DE3 File Offset: 0x00014FE3
		public static bool AIMoveToGameObjectIsEnabled(this Agent agent)
		{
			return agent.AIStateFlags.HasAnyFlag(Agent.AIStateFlag.UseObjectMoving);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00016DF1 File Offset: 0x00014FF1
		public static void AIDefendGameObjectEnable(this Agent agent, UsableMissionObject usedObject, IDetachment detachment)
		{
			agent.HumanAIComponent.StartDefendingGameObject(usedObject, detachment);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00016E00 File Offset: 0x00015000
		public static void AIDefendGameObjectDisable(this Agent agent)
		{
			agent.HumanAIComponent.StopDefendingGameObject();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00016E0D File Offset: 0x0001500D
		public static bool AIDefendGameObjectIsEnabled(this Agent agent)
		{
			return agent.HumanAIComponent.IsDefending;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00016E1A File Offset: 0x0001501A
		public static bool AIInterestedInAnyGameObject(this Agent agent)
		{
			return agent.HumanAIComponent.IsInterestedInAnyGameObject();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00016E27 File Offset: 0x00015027
		public static bool AIInterestedInGameObject(this Agent agent, UsableMissionObject usableMissionObject)
		{
			return agent.HumanAIComponent.IsInterestedInGameObject(usableMissionObject);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00016E35 File Offset: 0x00015035
		public static void AIUseGameObjectEnable(this Agent agent)
		{
			agent.AIStateFlags |= Agent.AIStateFlag.UseObjectUsing;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00016E46 File Offset: 0x00015046
		public static void AIUseGameObjectDisable(this Agent agent)
		{
			agent.AIStateFlags &= ~Agent.AIStateFlag.UseObjectUsing;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00016E57 File Offset: 0x00015057
		public static bool AIUseGameObjectIsEnabled(this Agent agent)
		{
			return agent.AIStateFlags.HasAnyFlag(Agent.AIStateFlag.UseObjectUsing);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00016E66 File Offset: 0x00015066
		public static Agent GetFollowedUnit(this Agent agent)
		{
			HumanAIComponent humanAIComponent = agent.HumanAIComponent;
			if (humanAIComponent == null)
			{
				return null;
			}
			return humanAIComponent.FollowedAgent;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00016E79 File Offset: 0x00015079
		public static void SetFollowedUnit(this Agent agent, Agent followedUnit)
		{
			agent.HumanAIComponent.FollowAgent(followedUnit);
		}
	}
}
