using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Overlay
{
	// Token: 0x02000103 RID: 259
	public class OverlayBaseWidget : Widget
	{
		// Token: 0x06000DB6 RID: 3510 RVA: 0x000260DC File Offset: 0x000242DC
		public OverlayBaseWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000260E5 File Offset: 0x000242E5
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x000260ED File Offset: 0x000242ED
		[Editor(false)]
		public OverlayPopupWidget PopupWidget
		{
			get
			{
				return this._popupWidget;
			}
			set
			{
				if (this._popupWidget != value)
				{
					this._popupWidget = value;
					base.OnPropertyChanged<OverlayPopupWidget>(value, "PopupWidget");
				}
			}
		}

		// Token: 0x0400064F RID: 1615
		private OverlayPopupWidget _popupWidget;
	}
}
