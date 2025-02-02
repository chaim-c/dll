using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x0200000F RID: 15
	public class SiblingIndexVisibilityWidget : Widget
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000601A File Offset: 0x0000421A
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00006022 File Offset: 0x00004222
		public SiblingIndexVisibilityWidget.WatchTypes WatchType { get; set; }

		// Token: 0x060000EE RID: 238 RVA: 0x0000602B File Offset: 0x0000422B
		public SiblingIndexVisibilityWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006034 File Offset: 0x00004234
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.UpdateVisibility();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006044 File Offset: 0x00004244
		private void UpdateVisibility()
		{
			Widget widget = this.WidgetToWatch ?? this;
			if (((widget != null) ? widget.ParentWidget : null) != null)
			{
				switch (this.WatchType)
				{
				case SiblingIndexVisibilityWidget.WatchTypes.Equal:
					base.IsVisible = (widget.GetSiblingIndex() == this.IndexToBeVisible);
					return;
				case SiblingIndexVisibilityWidget.WatchTypes.BiggerThan:
					base.IsVisible = (widget.GetSiblingIndex() > this.IndexToBeVisible);
					return;
				case SiblingIndexVisibilityWidget.WatchTypes.BiggerThanEqual:
					base.IsVisible = (widget.GetSiblingIndex() >= this.IndexToBeVisible);
					return;
				case SiblingIndexVisibilityWidget.WatchTypes.LessThan:
					base.IsVisible = (widget.GetSiblingIndex() < this.IndexToBeVisible);
					return;
				case SiblingIndexVisibilityWidget.WatchTypes.LessThanEqual:
					base.IsVisible = (widget.GetSiblingIndex() <= this.IndexToBeVisible);
					return;
				case SiblingIndexVisibilityWidget.WatchTypes.Odd:
					base.IsVisible = (widget.GetSiblingIndex() % 2 == 1);
					return;
				case SiblingIndexVisibilityWidget.WatchTypes.Even:
					base.IsVisible = (widget.GetSiblingIndex() % 2 == 0);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000612A File Offset: 0x0000432A
		private void OnWidgetToWatchParentEventFired(Widget arg1, string arg2, object[] arg3)
		{
			if (arg2 == "ItemAdd" || arg2 == "ItemRemove")
			{
				this.UpdateVisibility();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000614C File Offset: 0x0000434C
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00006154 File Offset: 0x00004354
		[Editor(false)]
		public int IndexToBeVisible
		{
			get
			{
				return this._indexToBeVisible;
			}
			set
			{
				if (this._indexToBeVisible != value)
				{
					this._indexToBeVisible = value;
					base.OnPropertyChanged(value, "IndexToBeVisible");
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006172 File Offset: 0x00004372
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000617A File Offset: 0x0000437A
		[Editor(false)]
		public Widget WidgetToWatch
		{
			get
			{
				return this._widgetToWatch;
			}
			set
			{
				if (this._widgetToWatch != value)
				{
					this._widgetToWatch = value;
					base.OnPropertyChanged<Widget>(value, "WidgetToWatch");
					value.ParentWidget.EventFire += this.OnWidgetToWatchParentEventFired;
					this.UpdateVisibility();
				}
			}
		}

		// Token: 0x04000071 RID: 113
		private Widget _widgetToWatch;

		// Token: 0x04000072 RID: 114
		private int _indexToBeVisible;

		// Token: 0x02000019 RID: 25
		public enum WatchTypes
		{
			// Token: 0x040000AC RID: 172
			Equal,
			// Token: 0x040000AD RID: 173
			BiggerThan,
			// Token: 0x040000AE RID: 174
			BiggerThanEqual,
			// Token: 0x040000AF RID: 175
			LessThan,
			// Token: 0x040000B0 RID: 176
			LessThanEqual,
			// Token: 0x040000B1 RID: 177
			Odd,
			// Token: 0x040000B2 RID: 178
			Even
		}
	}
}
