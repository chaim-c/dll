using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000F8 RID: 248
	public class AutoClosePopupWidget : Widget
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x0002433C File Offset: 0x0002253C
		public AutoClosePopupWidget(UIContext context) : base(context)
		{
			for (int i = 0; i < base.ChildCount; i++)
			{
				AutoClosePopupClosingWidget item;
				if ((item = (base.GetChild(i) as AutoClosePopupClosingWidget)) != null)
				{
					this._closingWidgets.Add(item);
				}
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00024388 File Offset: 0x00022588
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsVisible && base.EventManager.LatestMouseUpWidget != this.PopupParentWidget && base.EventManager.LatestMouseUpWidget != this._lastCheckedMouseUpWidget)
			{
				base.IsVisible = (base.EventManager.LatestMouseUpWidget == this || base.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget));
				this.CheckClosingWidgetsAndUpdateVisibility();
				this._lastCheckedMouseUpWidget = (base.IsVisible ? base.EventManager.LatestMouseUpWidget : null);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00024414 File Offset: 0x00022614
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			AutoClosePopupClosingWidget item;
			if ((item = (child as AutoClosePopupClosingWidget)) != null)
			{
				this._closingWidgets.Add(item);
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00024440 File Offset: 0x00022640
		protected void CheckClosingWidgetsAndUpdateVisibility()
		{
			if (base.IsVisible)
			{
				using (List<AutoClosePopupClosingWidget>.Enumerator enumerator = this._closingWidgets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ShouldClosePopup())
						{
							base.IsVisible = false;
							break;
						}
					}
				}
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x000244A4 File Offset: 0x000226A4
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x000244AC File Offset: 0x000226AC
		[Editor(false)]
		public Widget PopupParentWidget
		{
			get
			{
				return this._popupParentWidget;
			}
			set
			{
				if (this._popupParentWidget != value)
				{
					this._popupParentWidget = value;
					base.OnPropertyChanged<Widget>(value, "PopupParentWidget");
				}
			}
		}

		// Token: 0x04000604 RID: 1540
		private List<AutoClosePopupClosingWidget> _closingWidgets = new List<AutoClosePopupClosingWidget>();

		// Token: 0x04000605 RID: 1541
		protected Widget _lastCheckedMouseUpWidget;

		// Token: 0x04000606 RID: 1542
		private Widget _popupParentWidget;
	}
}
