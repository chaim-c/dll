using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x0200012B RID: 299
	public class PerkSelectionToggleEvent : EventBase
	{
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00069434 File Offset: 0x00067634
		// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0006943C File Offset: 0x0006763C
		public bool IsCurrentlyActive { get; private set; }

		// Token: 0x06001D54 RID: 7508 RVA: 0x00069445 File Offset: 0x00067645
		public PerkSelectionToggleEvent(bool isCurrentlyActive)
		{
			this.IsCurrentlyActive = isCurrentlyActive;
		}
	}
}
