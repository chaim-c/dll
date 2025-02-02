using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000EA RID: 234
	public class CraftingOrderSelectionOpenedEvent : EventBase
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00050F22 File Offset: 0x0004F122
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00050F2A File Offset: 0x0004F12A
		public bool IsOpen { get; private set; }

		// Token: 0x06001598 RID: 5528 RVA: 0x00050F33 File Offset: 0x0004F133
		public CraftingOrderSelectionOpenedEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
