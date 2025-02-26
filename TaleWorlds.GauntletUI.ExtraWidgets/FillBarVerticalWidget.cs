﻿using System;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x02000009 RID: 9
	public class FillBarVerticalWidget : Widget
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000032BE File Offset: 0x000014BE
		public FillBarVerticalWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000032C8 File Offset: 0x000014C8
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (this.FillWidget != null)
			{
				float y = this.FillWidget.ParentWidget.Size.Y;
				float num = 0f;
				if (this.MaxAmount != 0)
				{
					num = Mathf.Clamp(Mathf.Clamp((float)this.InitialAmount, 0f, (float)this.MaxAmount) / (float)this.MaxAmount, 0f, 1f);
				}
				float num2 = this._isCurrentValueSet ? Mathf.Clamp((float)(this.CurrentAmount - this.InitialAmount), (float)(-(float)this.MaxAmount), (float)this.MaxAmount) : 0f;
				float num3 = 0f;
				if (this.MaxAmount != 0)
				{
					num3 = (this._isCurrentValueSet ? Mathf.Clamp(num2 / (float)this.MaxAmount, -1f, 1f) : 0f);
				}
				if (this.IsDirectionUpward)
				{
					this.FillWidget.VerticalAlignment = VerticalAlignment.Bottom;
					this.FillWidget.ScaledSuggestedHeight = num * y;
					if (this.ChangeWidget != null)
					{
						this.ChangeWidget.ScaledSuggestedHeight = num3 * y;
						if (num3 >= 0f)
						{
							this.ChangeWidget.ScaledPositionYOffset = -this.FillWidget.ScaledSuggestedHeight;
							this.ChangeWidget.Color = new Color(1f, 1f, 1f, 1f);
						}
						else
						{
							this.ChangeWidget.ScaledPositionYOffset = -this.FillWidget.ScaledSuggestedHeight + this.ChangeWidget.ScaledSuggestedHeight;
							this.ChangeWidget.Color = new Color(1f, 0f, 0f, 1f);
						}
					}
				}
				else
				{
					this.FillWidget.VerticalAlignment = VerticalAlignment.Top;
					this.FillWidget.ScaledSuggestedHeight = num * y;
					if (this.ChangeWidget != null)
					{
						this.ChangeWidget.ScaledSuggestedHeight = num3 * y;
						this.ChangeWidget.VerticalAlignment = VerticalAlignment.Bottom;
						if (num3 >= 0f)
						{
							this.ChangeWidget.ScaledPositionYOffset = -this.FillWidget.ScaledSuggestedHeight;
							this.ChangeWidget.Color = new Color(1f, 1f, 1f, 1f);
						}
						else
						{
							this.ChangeWidget.ScaledPositionYOffset = -this.FillWidget.ScaledSuggestedHeight + this.ChangeWidget.ScaledSuggestedHeight;
							this.ChangeWidget.Color = new Color(1f, 0f, 0f, 1f);
						}
					}
				}
				if (this.ChangeWidget != null && this.DividerWidget != null)
				{
					this.DividerWidget.IsVisible = (this.ChangeWidget != null && num3 != 0f);
				}
			}
			base.OnRender(twoDimensionContext, drawContext);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003572 File Offset: 0x00001772
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000357A File Offset: 0x0000177A
		[Editor(false)]
		public bool IsDirectionUpward
		{
			get
			{
				return this._isDirectionUpward;
			}
			set
			{
				if (this._isDirectionUpward != value)
				{
					this._isDirectionUpward = value;
					base.OnPropertyChanged(value, "IsDirectionUpward");
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000035A1 File Offset: 0x000017A1
		[Editor(false)]
		public int CurrentAmount
		{
			get
			{
				return (int)this._currentAmount;
			}
			set
			{
				if (this._currentAmount != (float)value)
				{
					this._currentAmount = (float)value;
					base.OnPropertyChanged(value, "CurrentAmount");
					this._isCurrentValueSet = true;
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000035C8 File Offset: 0x000017C8
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000035D1 File Offset: 0x000017D1
		[Editor(false)]
		public int MaxAmount
		{
			get
			{
				return (int)this._maxAmount;
			}
			set
			{
				if (this._maxAmount != (float)value)
				{
					this._maxAmount = (float)value;
					base.OnPropertyChanged(value, "MaxAmount");
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000035F1 File Offset: 0x000017F1
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000035FA File Offset: 0x000017FA
		[Editor(false)]
		public int InitialAmount
		{
			get
			{
				return (int)this._initialAmount;
			}
			set
			{
				if (this._initialAmount != (float)value)
				{
					this._initialAmount = (float)value;
					base.OnPropertyChanged(value, "InitialAmount");
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000361A File Offset: 0x0000181A
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003622 File Offset: 0x00001822
		[Editor(false)]
		public float MaxAmountAsFloat
		{
			get
			{
				return this._maxAmount;
			}
			set
			{
				if (this._maxAmount != value)
				{
					this._maxAmount = value;
					base.OnPropertyChanged(value, "MaxAmountAsFloat");
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003640 File Offset: 0x00001840
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003648 File Offset: 0x00001848
		[Editor(false)]
		public float CurrentAmountAsFloat
		{
			get
			{
				return this._currentAmount;
			}
			set
			{
				if (this._currentAmount != value)
				{
					this._currentAmount = value;
					base.OnPropertyChanged(value, "CurrentAmountAsFloat");
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003666 File Offset: 0x00001866
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000366E File Offset: 0x0000186E
		[Editor(false)]
		public float InitialAmountAsFloat
		{
			get
			{
				return this._initialAmount;
			}
			set
			{
				if (this._initialAmount != value)
				{
					this._initialAmount = value;
					base.OnPropertyChanged(value, "InitialAmountAsFloat");
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000368C File Offset: 0x0000188C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003694 File Offset: 0x00001894
		public Widget FillWidget
		{
			get
			{
				return this._fillWidget;
			}
			set
			{
				if (this._fillWidget != value)
				{
					this._fillWidget = value;
					base.OnPropertyChanged<Widget>(value, "FillWidget");
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000036B2 File Offset: 0x000018B2
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000036BA File Offset: 0x000018BA
		public Widget ChangeWidget
		{
			get
			{
				return this._changeWidget;
			}
			set
			{
				if (this._changeWidget != value)
				{
					this._changeWidget = value;
					base.OnPropertyChanged<Widget>(value, "ChangeWidget");
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000036D8 File Offset: 0x000018D8
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000036E0 File Offset: 0x000018E0
		public Widget DividerWidget
		{
			get
			{
				return this._dividerWidget;
			}
			set
			{
				if (this._dividerWidget != value)
				{
					this._dividerWidget = value;
					base.OnPropertyChanged<Widget>(value, "DividerWidget");
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000036FE File Offset: 0x000018FE
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003706 File Offset: 0x00001906
		public Widget ContainerWidget
		{
			get
			{
				return this._containerWidget;
			}
			set
			{
				if (this._containerWidget != value)
				{
					this._containerWidget = value;
					base.OnPropertyChanged<Widget>(value, "ContainerWidget");
				}
			}
		}

		// Token: 0x04000036 RID: 54
		private bool _isCurrentValueSet;

		// Token: 0x04000037 RID: 55
		private Widget _fillWidget;

		// Token: 0x04000038 RID: 56
		private Widget _changeWidget;

		// Token: 0x04000039 RID: 57
		private Widget _containerWidget;

		// Token: 0x0400003A RID: 58
		private Widget _dividerWidget;

		// Token: 0x0400003B RID: 59
		private float _maxAmount;

		// Token: 0x0400003C RID: 60
		private float _currentAmount;

		// Token: 0x0400003D RID: 61
		private float _initialAmount;

		// Token: 0x0400003E RID: 62
		private bool _isDirectionUpward;
	}
}
