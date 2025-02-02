using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Clan
{
	// Token: 0x02000167 RID: 359
	public class ClanFinancePaymentSliderWidget : SliderWidget
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x00033206 File Offset: 0x00031406
		public ClanFinancePaymentSliderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00033210 File Offset: 0x00031410
		protected override void OnLateUpdate(float dt)
		{
			this.CurrentRatioIndicatorWidget.ScaledPositionXOffset = Mathf.Clamp(base.Size.X * ((float)this.CurrentSize / (float)this.SizeLimit) - this.CurrentRatioIndicatorWidget.Size.X / 2f, 0f, base.Size.X);
			this.InitialFillWidget.ScaledPositionXOffset = this.CurrentRatioIndicatorWidget.PositionXOffset * base._scaleToUse + this.CurrentRatioIndicatorWidget.Size.X / 2f;
			this.InitialFillWidget.ScaledSuggestedWidth = base.Size.X - this.CurrentRatioIndicatorWidget.PositionXOffset * base._scaleToUse - this.CurrentRatioIndicatorWidget.Size.X / 2f;
			if (base.Handle.PositionXOffset > this.CurrentRatioIndicatorWidget.PositionXOffset)
			{
				this.NewIncreaseFillWidget.ScaledPositionXOffset = this.CurrentRatioIndicatorWidget.PositionXOffset * base._scaleToUse + this.CurrentRatioIndicatorWidget.Size.X / 2f;
				this.NewIncreaseFillWidget.ScaledSuggestedWidth = Mathf.Clamp((base.Handle.PositionXOffset - this.CurrentRatioIndicatorWidget.PositionXOffset) * base._scaleToUse, 0f, base.Size.X);
				this.NewDecreaseFillWidget.ScaledSuggestedWidth = 0f;
			}
			else if (base.Handle.PositionXOffset < this.CurrentRatioIndicatorWidget.PositionXOffset)
			{
				this.NewDecreaseFillWidget.ScaledPositionXOffset = base.Handle.PositionXOffset * base._scaleToUse + base.Handle.Size.X / 2f;
				this.NewDecreaseFillWidget.ScaledSuggestedWidth = Mathf.Clamp(this.CurrentRatioIndicatorWidget.PositionXOffset * base._scaleToUse + this.CurrentRatioIndicatorWidget.Size.X / 2f - (base.Handle.PositionXOffset * base._scaleToUse + base.Handle.Size.X / 2f), 0f, base.Size.X);
				this.NewIncreaseFillWidget.ScaledSuggestedWidth = 0f;
			}
			else
			{
				this.NewIncreaseFillWidget.ScaledSuggestedWidth = 0f;
				this.NewDecreaseFillWidget.ScaledSuggestedWidth = 0f;
			}
			base.OnLateUpdate(dt);
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00033480 File Offset: 0x00031680
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x00033488 File Offset: 0x00031688
		[Editor(false)]
		public Widget InitialFillWidget
		{
			get
			{
				return this._initialFillWidget;
			}
			set
			{
				if (this._initialFillWidget != value)
				{
					this._initialFillWidget = value;
				}
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0003349A File Offset: 0x0003169A
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x000334A2 File Offset: 0x000316A2
		[Editor(false)]
		public Widget NewIncreaseFillWidget
		{
			get
			{
				return this._newIncreaseFillWidget;
			}
			set
			{
				if (this._newIncreaseFillWidget != value)
				{
					this._newIncreaseFillWidget = value;
				}
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000334B4 File Offset: 0x000316B4
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x000334BC File Offset: 0x000316BC
		[Editor(false)]
		public Widget NewDecreaseFillWidget
		{
			get
			{
				return this._newDecreaseFillWidget;
			}
			set
			{
				if (this._newDecreaseFillWidget != value)
				{
					this._newDecreaseFillWidget = value;
				}
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000334CE File Offset: 0x000316CE
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x000334D6 File Offset: 0x000316D6
		[Editor(false)]
		public Widget CurrentRatioIndicatorWidget
		{
			get
			{
				return this._currentRatioIndicatorWidget;
			}
			set
			{
				if (this._currentRatioIndicatorWidget != value)
				{
					this._currentRatioIndicatorWidget = value;
				}
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000334E8 File Offset: 0x000316E8
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x000334F0 File Offset: 0x000316F0
		[Editor(false)]
		public int CurrentSize
		{
			get
			{
				return this._currentSize;
			}
			set
			{
				if (this._currentSize != value)
				{
					this._currentSize = value;
				}
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00033502 File Offset: 0x00031702
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x0003350A File Offset: 0x0003170A
		[Editor(false)]
		public int TargetSize
		{
			get
			{
				return this._targetSize;
			}
			set
			{
				if (this._targetSize != value)
				{
					this._targetSize = value;
				}
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0003351C File Offset: 0x0003171C
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00033524 File Offset: 0x00031724
		[Editor(false)]
		public int SizeLimit
		{
			get
			{
				return this._sizeLimit;
			}
			set
			{
				if (this._sizeLimit != value)
				{
					this._sizeLimit = value;
				}
			}
		}

		// Token: 0x04000887 RID: 2183
		private Widget _initialFillWidget;

		// Token: 0x04000888 RID: 2184
		private Widget _newIncreaseFillWidget;

		// Token: 0x04000889 RID: 2185
		private Widget _newDecreaseFillWidget;

		// Token: 0x0400088A RID: 2186
		private Widget _currentRatioIndicatorWidget;

		// Token: 0x0400088B RID: 2187
		private int _currentSize;

		// Token: 0x0400088C RID: 2188
		private int _targetSize;

		// Token: 0x0400088D RID: 2189
		private int _sizeLimit;
	}
}
