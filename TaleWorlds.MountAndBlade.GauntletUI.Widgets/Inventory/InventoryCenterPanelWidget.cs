using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x0200012C RID: 300
	public class InventoryCenterPanelWidget : Widget
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x0002A8A7 File Offset: 0x00028AA7
		public InventoryCenterPanelWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0002A8B0 File Offset: 0x00028AB0
		protected override bool OnPreviewDragBegin()
		{
			return false;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0002A8B3 File Offset: 0x00028AB3
		protected override bool OnPreviewDrop()
		{
			return true;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0002A8B6 File Offset: 0x00028AB6
		protected override bool OnPreviewDragHover()
		{
			return false;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002A8B9 File Offset: 0x00028AB9
		protected override bool OnPreviewMouseMove()
		{
			return false;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002A8BC File Offset: 0x00028ABC
		protected override bool OnPreviewMousePressed()
		{
			return false;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002A8BF File Offset: 0x00028ABF
		protected override bool OnPreviewMouseReleased()
		{
			return false;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0002A8C2 File Offset: 0x00028AC2
		protected override bool OnPreviewMouseScroll()
		{
			return false;
		}
	}
}
