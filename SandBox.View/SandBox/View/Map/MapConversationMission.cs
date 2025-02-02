using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace SandBox.View.Map
{
	// Token: 0x02000040 RID: 64
	public class MapConversationMission : ICampaignMission
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00015484 File Offset: 0x00013684
		GameState ICampaignMission.State
		{
			get
			{
				return GameStateManager.Current.ActiveState;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00015490 File Offset: 0x00013690
		IMissionTroopSupplier ICampaignMission.AgentSupplier
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00015493 File Offset: 0x00013693
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0001549B File Offset: 0x0001369B
		Location ICampaignMission.Location { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000154A4 File Offset: 0x000136A4
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000154AC File Offset: 0x000136AC
		Alley ICampaignMission.LastVisitedAlley { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000154B5 File Offset: 0x000136B5
		MissionMode ICampaignMission.Mode
		{
			get
			{
				return MissionMode.Conversation;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000154B8 File Offset: 0x000136B8
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000154C0 File Offset: 0x000136C0
		public MapConversationTableau ConversationTableau { get; private set; }

		// Token: 0x0600022F RID: 559 RVA: 0x000154C9 File Offset: 0x000136C9
		public MapConversationMission()
		{
			CampaignMission.Current = this;
			this._conversationPlayQueue = new Queue<MapConversationMission.ConversationPlayArgs>();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000154E2 File Offset: 0x000136E2
		public void SetConversationTableau(MapConversationTableau tableau)
		{
			this.ConversationTableau = tableau;
			this.PlayCachedConversations();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000154F1 File Offset: 0x000136F1
		public void Tick(float dt)
		{
			this.PlayCachedConversations();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000154F9 File Offset: 0x000136F9
		public void OnFinalize()
		{
			this.ConversationTableau = null;
			this._conversationPlayQueue = null;
			CampaignMission.Current = null;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00015510 File Offset: 0x00013710
		private void PlayCachedConversations()
		{
			if (this.ConversationTableau != null)
			{
				while (this._conversationPlayQueue.Count > 0)
				{
					MapConversationMission.ConversationPlayArgs conversationPlayArgs = this._conversationPlayQueue.Dequeue();
					this.ConversationTableau.OnConversationPlay(conversationPlayArgs.IdleActionId, conversationPlayArgs.IdleFaceAnimId, conversationPlayArgs.ReactionId, conversationPlayArgs.ReactionFaceAnimId, conversationPlayArgs.SoundPath);
				}
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001556A File Offset: 0x0001376A
		void ICampaignMission.OnConversationPlay(string idleActionId, string idleFaceAnimId, string reactionId, string reactionFaceAnimId, string soundPath)
		{
			if (this.ConversationTableau != null)
			{
				this.ConversationTableau.OnConversationPlay(idleActionId, idleFaceAnimId, reactionId, reactionFaceAnimId, soundPath);
				return;
			}
			this._conversationPlayQueue.Enqueue(new MapConversationMission.ConversationPlayArgs(idleActionId, idleFaceAnimId, reactionId, reactionFaceAnimId, soundPath));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001559E File Offset: 0x0001379E
		void ICampaignMission.AddAgentFollowing(IAgent agent)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000155A0 File Offset: 0x000137A0
		bool ICampaignMission.AgentLookingAtAgent(IAgent agent1, IAgent agent2)
		{
			return false;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000155A3 File Offset: 0x000137A3
		bool ICampaignMission.CheckIfAgentCanFollow(IAgent agent)
		{
			return false;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000155A6 File Offset: 0x000137A6
		bool ICampaignMission.CheckIfAgentCanUnFollow(IAgent agent)
		{
			return false;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000155A9 File Offset: 0x000137A9
		void ICampaignMission.EndMission()
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000155AB File Offset: 0x000137AB
		void ICampaignMission.OnCharacterLocationChanged(LocationCharacter locationCharacter, Location fromLocation, Location toLocation)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000155AD File Offset: 0x000137AD
		void ICampaignMission.OnCloseEncounterMenu()
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000155AF File Offset: 0x000137AF
		void ICampaignMission.OnConversationContinue()
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000155B1 File Offset: 0x000137B1
		void ICampaignMission.OnConversationEnd(IAgent agent)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000155B3 File Offset: 0x000137B3
		void ICampaignMission.OnConversationStart(IAgent agent, bool setActionsInstantly)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000155B5 File Offset: 0x000137B5
		void ICampaignMission.OnProcessSentence()
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000155B7 File Offset: 0x000137B7
		void ICampaignMission.RemoveAgentFollowing(IAgent agent)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000155B9 File Offset: 0x000137B9
		void ICampaignMission.SetMissionMode(MissionMode newMode, bool atStart)
		{
		}

		// Token: 0x0400013C RID: 316
		private Queue<MapConversationMission.ConversationPlayArgs> _conversationPlayQueue;

		// Token: 0x0200007C RID: 124
		public struct ConversationPlayArgs
		{
			// Token: 0x06000462 RID: 1122 RVA: 0x0002335D File Offset: 0x0002155D
			public ConversationPlayArgs(string idleActionId, string idleFaceAnimId, string reactionId, string reactionFaceAnimId, string soundPath)
			{
				this.IdleActionId = idleActionId;
				this.IdleFaceAnimId = idleFaceAnimId;
				this.ReactionId = reactionId;
				this.ReactionFaceAnimId = reactionFaceAnimId;
				this.SoundPath = soundPath;
			}

			// Token: 0x040002F1 RID: 753
			public readonly string IdleActionId;

			// Token: 0x040002F2 RID: 754
			public readonly string IdleFaceAnimId;

			// Token: 0x040002F3 RID: 755
			public readonly string ReactionId;

			// Token: 0x040002F4 RID: 756
			public readonly string ReactionFaceAnimId;

			// Token: 0x040002F5 RID: 757
			public readonly string SoundPath;
		}
	}
}
