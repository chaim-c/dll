using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000041 RID: 65
	public class ToggleStateButtonWidget : ButtonWidget
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public ToggleStateButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000B0C0 File Offset: 0x000092C0
		protected override void HandleClick()
		{
			foreach (Action<Widget> action in this.ClickEventHandlers)
			{
				action(this);
			}
			bool isSelected = base.IsSelected;
			if (!base.IsSelected)
			{
				base.IsSelected = true;
			}
			else if (this.AllowSwitchOff)
			{
				base.IsSelected = false;
			}
			if (base.IsSelected && !isSelected && this.NotifyParentForSelection && base.ParentWidget is Container)
			{
				(base.ParentWidget as Container).OnChildSelected(this);
			}
			if (this.AllowSwitchOff && !base.IsSelected && this.NotifyParentForSelection && base.ParentWidget is Container)
			{
				(base.ParentWidget as Container).OnChildSelected(null);
			}
			this.OnClick();
			base.EventFired("Click", Array.Empty<object>());
			if (base.Context.EventManager.Time - this._lastClickTime < 0.5f)
			{
				base.EventFired("DoubleClick", Array.Empty<object>());
				return;
			}
			this._lastClickTime = base.Context.EventManager.Time;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000B1FC File Offset: 0x000093FC
		protected override void RefreshState()
		{
			base.RefreshState();
			if (base.UpdateChildrenStates)
			{
				this.UpdateChildrenStatesRecursively(this);
			}
			if (this._widgetToClose != null)
			{
				this._widgetToClose.IsVisible = base.IsSelected;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000B22C File Offset: 0x0000942C
		private void UpdateChildrenStatesRecursively(Widget parent)
		{
			parent.SetState(base.CurrentState);
			if (parent.ChildCount > 0)
			{
				foreach (Widget parent2 in parent.Children)
				{
					this.UpdateChildrenStatesRecursively(parent2);
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000B294 File Offset: 0x00009494
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000B29C File Offset: 0x0000949C
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
					if (this._widgetToClose != null)
					{
						this._widgetToClose.IsVisible = base.IsSelected;
					}
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000B2D3 File Offset: 0x000094D3
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000B2DB File Offset: 0x000094DB
		[Editor(false)]
		public bool AllowSwitchOff
		{
			get
			{
				return this._allowSwitchOff;
			}
			set
			{
				if (this._allowSwitchOff != value)
				{
					this._allowSwitchOff = value;
					base.OnPropertyChanged(value, "AllowSwitchOff");
				}
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000B2F9 File Offset: 0x000094F9
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000B301 File Offset: 0x00009501
		[Editor(false)]
		public bool NotifyParentForSelection
		{
			get
			{
				return this._notifyParentForSelection;
			}
			set
			{
				if (this._notifyParentForSelection != value)
				{
					this._notifyParentForSelection = value;
					base.OnPropertyChanged(value, "NotifyParentForSelection");
				}
			}
		}

		// Token: 0x04000172 RID: 370
		private Widget _widgetToClose;

		// Token: 0x04000173 RID: 371
		private bool _allowSwitchOff = true;

		// Token: 0x04000174 RID: 372
		private bool _notifyParentForSelection = true;
	}
}
