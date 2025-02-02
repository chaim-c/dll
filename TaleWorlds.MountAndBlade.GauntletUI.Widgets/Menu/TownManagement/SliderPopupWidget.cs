using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000FF RID: 255
	public class SliderPopupWidget : Widget
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x000255FA File Offset: 0x000237FA
		public SliderPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00025604 File Offset: 0x00023804
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.SliderValueTextWidget != null && this.ReserveAmountSlider != null)
			{
				this.SliderValueTextWidget.Text = this.ReserveAmountSlider.ValueInt.ToString();
			}
			if (base.ParentWidget.IsVisible && base.EventManager.LatestMouseDownWidget != this.PopupParentWidget && base.EventManager.LatestMouseUpWidget != this.PopupParentWidget && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget) && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseDownWidget))
			{
				base.EventFired("ClosePopup", Array.Empty<object>());
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000256AF File Offset: 0x000238AF
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x000256B7 File Offset: 0x000238B7
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

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000256D5 File Offset: 0x000238D5
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x000256DD File Offset: 0x000238DD
		[Editor(false)]
		public ButtonWidget ClosePopupWidget
		{
			get
			{
				return this._closePopupWidget;
			}
			set
			{
				if (this._closePopupWidget != value)
				{
					this._closePopupWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ClosePopupWidget");
				}
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000256FB File Offset: 0x000238FB
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00025703 File Offset: 0x00023903
		[Editor(false)]
		public TextWidget SliderValueTextWidget
		{
			get
			{
				return this._sliderValueTextWidget;
			}
			set
			{
				if (this._sliderValueTextWidget != value)
				{
					this._sliderValueTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "SliderValueTextWidget");
				}
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00025721 File Offset: 0x00023921
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00025729 File Offset: 0x00023929
		[Editor(false)]
		public SliderWidget ReserveAmountSlider
		{
			get
			{
				return this._reserveAmountSlider;
			}
			set
			{
				if (this._reserveAmountSlider != value)
				{
					this._reserveAmountSlider = value;
					base.OnPropertyChanged<SliderWidget>(value, "ReserveAmountSlider");
				}
			}
		}

		// Token: 0x0400062D RID: 1581
		private ButtonWidget _closePopupWidget;

		// Token: 0x0400062E RID: 1582
		private TextWidget _sliderValueTextWidget;

		// Token: 0x0400062F RID: 1583
		private SliderWidget _reserveAmountSlider;

		// Token: 0x04000630 RID: 1584
		private Widget _popupParentWidget;
	}
}
