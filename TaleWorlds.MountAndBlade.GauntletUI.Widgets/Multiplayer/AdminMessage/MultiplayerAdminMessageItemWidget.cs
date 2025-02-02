using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.AdminMessage
{
	// Token: 0x020000CA RID: 202
	public class MultiplayerAdminMessageItemWidget : Widget
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0001DBB6 File Offset: 0x0001BDB6
		public MultiplayerAdminMessageItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001DBBF File Offset: 0x0001BDBF
		public void Remove()
		{
			base.EventFired("Remove", Array.Empty<object>());
		}
	}
}
