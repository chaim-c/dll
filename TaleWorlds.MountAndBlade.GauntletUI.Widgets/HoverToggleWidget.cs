using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000023 RID: 35
	public class HoverToggleWidget : Widget
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007141 File Offset: 0x00005341
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00007149 File Offset: 0x00005349
		public bool IsOverWidget { get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x00007152 File Offset: 0x00005352
		public HoverToggleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000715C File Offset: 0x0000535C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.IsVisible)
			{
				this.IsOverWidget = this.IsMouseOverWidget();
				if (this.IsOverWidget && !this._hoverBegan)
				{
					base.EventFired("HoverBegin", Array.Empty<object>());
					this._hoverBegan = true;
				}
				else if (!this.IsOverWidget && this._hoverBegan)
				{
					base.EventFired("HoverEnd", Array.Empty<object>());
					this._hoverBegan = false;
				}
				if (this.WidgetToShow != null)
				{
					this.WidgetToShow.IsVisible = this._hoverBegan;
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000071F0 File Offset: 0x000053F0
		private bool IsMouseOverWidget()
		{
			return this.IsBetween(base.EventManager.MousePosition.X, base.GlobalPosition.X, base.GlobalPosition.X + base.Size.X) && this.IsBetween(base.EventManager.MousePosition.Y, base.GlobalPosition.Y, base.GlobalPosition.Y + base.Size.Y);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007271 File Offset: 0x00005471
		private bool IsBetween(float number, float min, float max)
		{
			return number > min && number < max;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000727D File Offset: 0x0000547D
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007285 File Offset: 0x00005485
		[Editor(false)]
		public Widget WidgetToShow
		{
			get
			{
				return this._widgetToShow;
			}
			set
			{
				if (this._widgetToShow != value)
				{
					this._widgetToShow = value;
					base.OnPropertyChanged<Widget>(value, "WidgetToShow");
				}
			}
		}

		// Token: 0x040000DF RID: 223
		private bool _hoverBegan;

		// Token: 0x040000E0 RID: 224
		private Widget _widgetToShow;
	}
}
