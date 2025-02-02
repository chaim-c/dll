using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Clan
{
	// Token: 0x0200016A RID: 362
	public class ClanPartyRoleSelectionPopupWidget : AutoClosePopupWidget
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x000336B8 File Offset: 0x000318B8
		public ClanPartyRoleSelectionPopupWidget(UIContext context) : base(context)
		{
			this._toggleWidgets = new List<Widget>();
			base.IsVisible = false;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000336D4 File Offset: 0x000318D4
		protected override void OnLateUpdate(float dt)
		{
			if (base.IsVisible && base.EventManager.LatestMouseUpWidget != this._lastCheckedMouseUpWidget && !this._toggleWidgets.Contains(base.EventManager.LatestMouseUpWidget))
			{
				base.IsVisible = (base.EventManager.LatestMouseUpWidget == this || base.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget));
				base.CheckClosingWidgetsAndUpdateVisibility();
				if (!base.IsVisible)
				{
					this.ActiveToggleWidget = null;
				}
			}
			this._lastCheckedMouseUpWidget = base.EventManager.LatestMouseUpWidget;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00033762 File Offset: 0x00031962
		public void AddToggleWidget(Widget widget)
		{
			if (!this._toggleWidgets.Contains(widget))
			{
				this._toggleWidgets.Add(widget);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0003377E File Offset: 0x0003197E
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00033786 File Offset: 0x00031986
		[Editor(false)]
		public Widget ActiveToggleWidget
		{
			get
			{
				return this._activeToggleWidget;
			}
			set
			{
				if (this._activeToggleWidget != value)
				{
					this._activeToggleWidget = value;
					base.OnPropertyChanged<Widget>(value, "ActiveToggleWidget");
				}
			}
		}

		// Token: 0x04000890 RID: 2192
		private List<Widget> _toggleWidgets;

		// Token: 0x04000891 RID: 2193
		private Widget _activeToggleWidget;
	}
}
