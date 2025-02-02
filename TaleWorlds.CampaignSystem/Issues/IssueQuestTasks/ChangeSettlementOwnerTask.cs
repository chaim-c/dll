﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Issues.IssueQuestTasks
{
	// Token: 0x02000324 RID: 804
	public class ChangeSettlementOwnerTask : QuestTaskBase
	{
		// Token: 0x06002E21 RID: 11809 RVA: 0x000C151E File Offset: 0x000BF71E
		internal static void AutoGeneratedStaticCollectObjectsChangeSettlementOwnerTask(object o, List<object> collectedObjects)
		{
			((ChangeSettlementOwnerTask)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000C152C File Offset: 0x000BF72C
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this._settlement);
			collectedObjects.Add(this._newOwner);
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000C154D File Offset: 0x000BF74D
		internal static object AutoGeneratedGetMemberValue_settlement(object o)
		{
			return ((ChangeSettlementOwnerTask)o)._settlement;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000C155A File Offset: 0x000BF75A
		internal static object AutoGeneratedGetMemberValue_newOwner(object o)
		{
			return ((ChangeSettlementOwnerTask)o)._newOwner;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000C1567 File Offset: 0x000BF767
		public ChangeSettlementOwnerTask(Settlement settlement, Hero newOwner, Action onSucceededAction, Action onFailedAction, Action onCanceledAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, onFailedAction, onCanceledAction)
		{
			this._settlement = settlement;
			this._newOwner = newOwner;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000C1584 File Offset: 0x000BF784
		public void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (this._settlement == settlement && this._newOwner == newOwner)
			{
				base.Finish(QuestTaskBase.FinishStates.Success);
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000C159F File Offset: 0x000BF79F
		public override void SetReferences()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
		}

		// Token: 0x04000DCD RID: 3533
		[SaveableField(30)]
		private readonly Settlement _settlement;

		// Token: 0x04000DCE RID: 3534
		[SaveableField(31)]
		private readonly Hero _newOwner;
	}
}
