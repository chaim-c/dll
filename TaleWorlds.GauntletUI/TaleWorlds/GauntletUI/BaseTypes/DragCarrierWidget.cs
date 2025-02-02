using System;
using TaleWorlds.GauntletUI.Layout;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200005C RID: 92
	public class DragCarrierWidget : Widget
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x00018905 File Offset: 0x00016B05
		public DragCarrierWidget(UIContext context) : base(context)
		{
			base.LayoutImp = new DragCarrierLayout();
			base.DoNotAcceptEvents = true;
			base.DoNotPassEventsToChildren = true;
			base.IsDisabled = true;
		}
	}
}
