using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions
{
	// Token: 0x02000069 RID: 105
	public class PlayerSelectedAKingdomDecisionOptionEvent : EventBase
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00025FD0 File Offset: 0x000241D0
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00025FD8 File Offset: 0x000241D8
		public DecisionOutcome Option { get; private set; }

		// Token: 0x0600091F RID: 2335 RVA: 0x00025FE1 File Offset: 0x000241E1
		public PlayerSelectedAKingdomDecisionOptionEvent(DecisionOutcome option)
		{
			this.Option = option;
		}
	}
}
