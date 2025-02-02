using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Issues.IssueQuestTasks
{
	// Token: 0x02000326 RID: 806
	public class RaidVillageQuestTask : QuestTaskBase
	{
		// Token: 0x06002E34 RID: 11828 RVA: 0x000C188A File Offset: 0x000BFA8A
		public RaidVillageQuestTask(Village village, Action onSucceededAction, Action onFailedAction, Action onCanceledAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, onFailedAction, onCanceledAction)
		{
			this._targetVillage = village;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000C189F File Offset: 0x000BFA9F
		public void OnVillageLooted(Village village)
		{
			if (this._targetVillage == village)
			{
				base.Finish((this._targetVillage.Owner.MapEvent.AttackerSide.LeaderParty == MobileParty.MainParty.Party) ? QuestTaskBase.FinishStates.Success : QuestTaskBase.FinishStates.Fail);
			}
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000C18DA File Offset: 0x000BFADA
		public void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			if (!FactionManager.IsAtWarAgainstFaction(newKingdom, this._targetVillage.Settlement.MapFaction))
			{
				base.Finish(QuestTaskBase.FinishStates.Cancel);
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000C18FB File Offset: 0x000BFAFB
		public override void SetReferences()
		{
			CampaignEvents.VillageLooted.AddNonSerializedListener(this, new Action<Village>(this.OnVillageLooted));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
		}

		// Token: 0x04000DD6 RID: 3542
		[SaveableField(50)]
		private readonly Village _targetVillage;
	}
}
