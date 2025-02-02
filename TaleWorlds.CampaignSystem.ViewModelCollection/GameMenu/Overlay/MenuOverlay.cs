using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000A9 RID: 169
	public class MenuOverlay : Attribute
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x00042E27 File Offset: 0x00041027
		public MenuOverlay(string typeId)
		{
			this.TypeId = typeId;
		}

		// Token: 0x040007D9 RID: 2009
		public new string TypeId;
	}
}
