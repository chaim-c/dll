using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000036 RID: 54
	public class RelationTextWidget : TextWidget
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00009C75 File Offset: 0x00007E75
		public RelationTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009C88 File Offset: 0x00007E88
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isVisualsDirty)
			{
				base.Text = ((this.Amount > 0) ? ("+" + this.Amount.ToString()) : this.Amount.ToString());
				if (this.Amount > 0)
				{
					base.Brush.FontColor = this.PositiveColor;
				}
				else if (this.Amount < 0)
				{
					base.Brush.FontColor = this.NegativeColor;
				}
				else
				{
					base.Brush.FontColor = this.ZeroColor;
				}
				this._isVisualsDirty = false;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009D2E File Offset: 0x00007F2E
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00009D36 File Offset: 0x00007F36
		[Editor(false)]
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if (this._amount != value)
				{
					this._amount = value;
					base.OnPropertyChanged(value, "Amount");
					this._isVisualsDirty = true;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009D5B File Offset: 0x00007F5B
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00009D63 File Offset: 0x00007F63
		[Editor(false)]
		public Color ZeroColor
		{
			get
			{
				return this._zeroColor;
			}
			set
			{
				if (value != this._zeroColor)
				{
					this._zeroColor = value;
					base.OnPropertyChanged(value, "ZeroColor");
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009D86 File Offset: 0x00007F86
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00009D8E File Offset: 0x00007F8E
		[Editor(false)]
		public Color PositiveColor
		{
			get
			{
				return this._positiveColor;
			}
			set
			{
				if (value != this._positiveColor)
				{
					this._positiveColor = value;
					base.OnPropertyChanged(value, "PositiveColor");
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00009DB1 File Offset: 0x00007FB1
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00009DB9 File Offset: 0x00007FB9
		[Editor(false)]
		public Color NegativeColor
		{
			get
			{
				return this._negativeColor;
			}
			set
			{
				if (value != this._negativeColor)
				{
					this._negativeColor = value;
					base.OnPropertyChanged(value, "NegativeColor");
				}
			}
		}

		// Token: 0x04000135 RID: 309
		private bool _isVisualsDirty = true;

		// Token: 0x04000136 RID: 310
		private int _amount;

		// Token: 0x04000137 RID: 311
		private Color _zeroColor;

		// Token: 0x04000138 RID: 312
		private Color _positiveColor;

		// Token: 0x04000139 RID: 313
		private Color _negativeColor;
	}
}
