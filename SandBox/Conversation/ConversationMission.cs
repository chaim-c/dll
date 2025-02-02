using System;
using System.Collections.Generic;
using SandBox.Conversation.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Conversation
{
	// Token: 0x0200009C RID: 156
	public static class ConversationMission
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0002B165 File Offset: 0x00029365
		public static Agent OneToOneConversationAgent
		{
			get
			{
				return Campaign.Current.ConversationManager.OneToOneConversationAgent as Agent;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0002B17B File Offset: 0x0002937B
		public static CharacterObject OneToOneConversationCharacter
		{
			get
			{
				return Campaign.Current.ConversationManager.OneToOneConversationCharacter;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0002B18C File Offset: 0x0002938C
		public static IEnumerable<Agent> ConversationAgents
		{
			get
			{
				foreach (IAgent agent in Campaign.Current.ConversationManager.ConversationAgents)
				{
					yield return agent as Agent;
				}
				IEnumerator<IAgent> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0002B195 File Offset: 0x00029395
		public static void StartConversationWithAgent(Agent agent)
		{
			MissionConversationLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionConversationLogic>();
			if (missionBehavior == null)
			{
				return;
			}
			missionBehavior.StartConversation(agent, true, false);
		}
	}
}
