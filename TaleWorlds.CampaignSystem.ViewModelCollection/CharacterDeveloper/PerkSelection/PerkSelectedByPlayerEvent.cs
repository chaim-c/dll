using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x0200012A RID: 298
	public class PerkSelectedByPlayerEvent : EventBase
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00069414 File Offset: 0x00067614
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0006941C File Offset: 0x0006761C
		public PerkObject SelectedPerk { get; private set; }

		// Token: 0x06001D51 RID: 7505 RVA: 0x00069425 File Offset: 0x00067625
		public PerkSelectedByPlayerEvent(PerkObject selectedPerk)
		{
			this.SelectedPerk = selectedPerk;
		}
	}
}
