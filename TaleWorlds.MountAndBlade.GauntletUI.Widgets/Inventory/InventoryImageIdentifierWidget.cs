using System;
using TaleWorlds.GauntletUI;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x0200012F RID: 303
	public class InventoryImageIdentifierWidget : ImageIdentifierWidget
	{
		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002B187 File Offset: 0x00029387
		public InventoryImageIdentifierWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002B190 File Offset: 0x00029390
		public void SetRenderRequestedPreviousFrame(bool isRequested)
		{
			this._isRenderRequestedPreviousFrame = isRequested;
		}
	}
}
