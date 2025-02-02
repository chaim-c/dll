using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000E9 RID: 233
	public class CraftingWeaponClassSelectionOpenedEvent : EventBase
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00050F02 File Offset: 0x0004F102
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x00050F0A File Offset: 0x0004F10A
		public bool IsOpen { get; private set; }

		// Token: 0x06001595 RID: 5525 RVA: 0x00050F13 File Offset: 0x0004F113
		public CraftingWeaponClassSelectionOpenedEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
