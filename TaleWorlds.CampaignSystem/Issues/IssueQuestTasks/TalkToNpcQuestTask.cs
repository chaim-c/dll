using System;

namespace TaleWorlds.CampaignSystem.Issues.IssueQuestTasks
{
	// Token: 0x02000327 RID: 807
	public class TalkToNpcQuestTask : QuestTaskBase
	{
		// Token: 0x06002E38 RID: 11832 RVA: 0x000C192B File Offset: 0x000BFB2B
		public TalkToNpcQuestTask(Hero hero, Action onSucceededAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, null, null)
		{
			this._character = hero.CharacterObject;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000C1943 File Offset: 0x000BFB43
		public TalkToNpcQuestTask(CharacterObject character, Action onSucceededAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, null, null)
		{
			this._character = character;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000C1956 File Offset: 0x000BFB56
		public bool IsTaskCharacter()
		{
			return this._character == CharacterObject.OneToOneConversationCharacter;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000C1965 File Offset: 0x000BFB65
		protected override void OnFinished()
		{
			this._character = null;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000C196E File Offset: 0x000BFB6E
		public override void SetReferences()
		{
		}

		// Token: 0x04000DD7 RID: 3543
		private CharacterObject _character;
	}
}
