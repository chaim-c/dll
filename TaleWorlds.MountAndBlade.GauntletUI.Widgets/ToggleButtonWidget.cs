using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000040 RID: 64
	public class ToggleButtonWidget : ButtonWidget
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000B000 File Offset: 0x00009200
		public ToggleButtonWidget(UIContext context) : base(context)
		{
			this.ClickEventHandlers.Add(new Action<Widget>(this.OnClick));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B021 File Offset: 0x00009221
		protected virtual void OnClick(Widget widget)
		{
			if (this._widgetToClose != null)
			{
				this.IsTargetVisible = !this._widgetToClose.IsVisible;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000B03F File Offset: 0x0000923F
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000B052 File Offset: 0x00009252
		public bool IsTargetVisible
		{
			get
			{
				Widget widgetToClose = this._widgetToClose;
				return widgetToClose != null && widgetToClose.IsVisible;
			}
			set
			{
				if (this._widgetToClose != null && this._widgetToClose.IsVisible != value)
				{
					this._widgetToClose.IsVisible = value;
					base.OnPropertyChanged(value, "IsTargetVisible");
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000B082 File Offset: 0x00009282
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000B08A File Offset: 0x0000928A
		[Editor(false)]
		public Widget WidgetToClose
		{
			get
			{
				return this._widgetToClose;
			}
			set
			{
				if (this._widgetToClose != value)
				{
					this._widgetToClose = value;
					base.OnPropertyChanged<Widget>(value, "WidgetToClose");
				}
			}
		}

		// Token: 0x04000171 RID: 369
		private Widget _widgetToClose;
	}
}
