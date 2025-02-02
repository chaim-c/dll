using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x02000013 RID: 19
	public class TwoWaySliderWidget : SliderWidget
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000693D File Offset: 0x00004B3D
		public TwoWaySliderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006948 File Offset: 0x00004B48
		protected override void OnValueIntChanged(int value)
		{
			base.OnValueIntChanged(value);
			if (this.ChangeFillWidget == null || base.MaxValueInt == 0)
			{
				return;
			}
			float num = base.Size.X / base._scaleToUse;
			float num2 = (float)this.BaseValueInt / base.MaxValueFloat * num;
			if (value < this.BaseValueInt)
			{
				this.ChangeFillWidget.SetState("Positive");
				this.ChangeFillWidget.SuggestedWidth = (float)(this.BaseValueInt - value) / base.MaxValueFloat * num;
				this.ChangeFillWidget.PositionXOffset = num2 - this.ChangeFillWidget.SuggestedWidth;
			}
			else if (value > this.BaseValueInt)
			{
				this.ChangeFillWidget.SetState("Negative");
				this.ChangeFillWidget.SuggestedWidth = (float)(value - this.BaseValueInt) / base.MaxValueFloat * num;
				this.ChangeFillWidget.PositionXOffset = num2;
			}
			else
			{
				this.ChangeFillWidget.SetState("Default");
				this.ChangeFillWidget.SuggestedWidth = 0f;
			}
			if (this._handleClicked || this._valueChangedByMouse || this._manuallyIncreased)
			{
				this._manuallyIncreased = false;
				base.OnPropertyChanged(base.ValueInt, "ValueInt");
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006A75 File Offset: 0x00004C75
		private void ChangeFillWidgetUpdated()
		{
			if (this.ChangeFillWidget != null)
			{
				this.ChangeFillWidget.AddState("Negative");
				this.ChangeFillWidget.AddState("Positive");
				this.ChangeFillWidget.HorizontalAlignment = HorizontalAlignment.Left;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006AAB File Offset: 0x00004CAB
		private void BaseValueIntUpdated()
		{
			this.OnValueIntChanged(base.ValueInt);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006AB9 File Offset: 0x00004CB9
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00006AC1 File Offset: 0x00004CC1
		[Editor(false)]
		public BrushWidget ChangeFillWidget
		{
			get
			{
				return this._changeFillWidget;
			}
			set
			{
				if (this._changeFillWidget != value)
				{
					this._changeFillWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "ChangeFillWidget");
					this.ChangeFillWidgetUpdated();
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006AE5 File Offset: 0x00004CE5
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00006AED File Offset: 0x00004CED
		[Editor(false)]
		public int BaseValueInt
		{
			get
			{
				return this._baseValueInt;
			}
			set
			{
				if (this._baseValueInt != value)
				{
					this._baseValueInt = value;
					base.OnPropertyChanged(value, "BaseValueInt");
					this.BaseValueIntUpdated();
				}
			}
		}

		// Token: 0x04000084 RID: 132
		protected bool _manuallyIncreased;

		// Token: 0x04000085 RID: 133
		private BrushWidget _changeFillWidget;

		// Token: 0x04000086 RID: 134
		private int _baseValueInt;
	}
}
