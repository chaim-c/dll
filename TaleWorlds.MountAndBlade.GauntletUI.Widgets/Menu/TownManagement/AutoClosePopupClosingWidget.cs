using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000F7 RID: 247
	public class AutoClosePopupClosingWidget : Widget
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000242AF File Offset: 0x000224AF
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x000242B7 File Offset: 0x000224B7
		public Widget Target { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000242C0 File Offset: 0x000224C0
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x000242C8 File Offset: 0x000224C8
		public bool IncludeChildren { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x000242D1 File Offset: 0x000224D1
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x000242D9 File Offset: 0x000224D9
		public bool IncludeTarget { get; set; }

		// Token: 0x06000D14 RID: 3348 RVA: 0x000242E2 File Offset: 0x000224E2
		public AutoClosePopupClosingWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000242EC File Offset: 0x000224EC
		public bool ShouldClosePopup()
		{
			if (this.IncludeTarget && base.EventManager.LatestMouseUpWidget == this.Target)
			{
				return true;
			}
			if (this.IncludeChildren)
			{
				Widget target = this.Target;
				return target != null && target.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget);
			}
			return false;
		}
	}
}
