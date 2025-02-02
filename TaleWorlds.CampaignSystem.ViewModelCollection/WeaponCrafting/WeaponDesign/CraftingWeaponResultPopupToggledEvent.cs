using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000ED RID: 237
	public class CraftingWeaponResultPopupToggledEvent : EventBase
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x000518C4 File Offset: 0x0004FAC4
		public bool IsOpen { get; }

		// Token: 0x060015DD RID: 5597 RVA: 0x000518CC File Offset: 0x0004FACC
		public CraftingWeaponResultPopupToggledEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
