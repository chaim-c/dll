using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000031 RID: 49
	public class NumericUpDownWidget : Widget
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000933F File Offset: 0x0000753F
		public NumericUpDownWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000935E File Offset: 0x0000755E
		private void OnUpButtonClicked(Widget widget)
		{
			this.ChangeValue(1);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009367 File Offset: 0x00007567
		private void OnDownButtonClicked(Widget widget)
		{
			this.ChangeValue(-1);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00009370 File Offset: 0x00007570
		private void ChangeValue(int changeAmount)
		{
			int num = this.IntValue + changeAmount;
			if ((float)num <= this.MaxValue && (float)num >= this.MinValue)
			{
				this.IntValue = num;
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000093A4 File Offset: 0x000075A4
		private void UpdateControlButtonsEnabled()
		{
			if (this.UpButton != null)
			{
				this.UpButton.IsEnabled = ((float)(this._intValue + 1) <= this.MaxValue);
			}
			if (this.DownButton != null)
			{
				this.DownButton.IsEnabled = ((float)(this._intValue - 1) >= this.MinValue);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000093FF File Offset: 0x000075FF
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00009407 File Offset: 0x00007607
		[Editor(false)]
		public bool ShowOneAdded
		{
			get
			{
				return this._showOneAdded;
			}
			set
			{
				if (this._showOneAdded != value)
				{
					this._showOneAdded = value;
					base.OnPropertyChanged(value, "ShowOneAdded");
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00009425 File Offset: 0x00007625
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00009430 File Offset: 0x00007630
		[Editor(false)]
		public int IntValue
		{
			get
			{
				return this._intValue;
			}
			set
			{
				if (this._intValue != value)
				{
					this._intValue = value;
					this.Value = (float)this._intValue;
					base.OnPropertyChanged(value, "IntValue");
					this._textWidget.IntText = (this.ShowOneAdded ? (this.IntValue + 1) : this.IntValue);
					this.UpdateControlButtonsEnabled();
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000948F File Offset: 0x0000768F
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00009497 File Offset: 0x00007697
		[Editor(false)]
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value != value)
				{
					this._value = value;
					this.IntValue = (int)this._value;
					base.OnPropertyChanged(value, "Value");
				}
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000094C2 File Offset: 0x000076C2
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x000094CA File Offset: 0x000076CA
		[Editor(false)]
		public float MinValue
		{
			get
			{
				return this._minValue;
			}
			set
			{
				if (value != this._minValue)
				{
					this._minValue = value;
					base.OnPropertyChanged(value, "MinValue");
					this.UpdateControlButtonsEnabled();
				}
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000094EE File Offset: 0x000076EE
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000094F6 File Offset: 0x000076F6
		[Editor(false)]
		public float MaxValue
		{
			get
			{
				return this._maxValue;
			}
			set
			{
				if (value != this._maxValue)
				{
					this._maxValue = value;
					base.OnPropertyChanged(value, "MaxValue");
					this.UpdateControlButtonsEnabled();
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000951A File Offset: 0x0000771A
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00009522 File Offset: 0x00007722
		[Editor(false)]
		public TextWidget TextWidget
		{
			get
			{
				return this._textWidget;
			}
			set
			{
				if (this._textWidget != value)
				{
					this._textWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "TextWidget");
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009540 File Offset: 0x00007740
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00009548 File Offset: 0x00007748
		[Editor(false)]
		public ButtonWidget UpButton
		{
			get
			{
				return this._upButton;
			}
			set
			{
				if (this._upButton != value)
				{
					this._upButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "UpButton");
					if (value != null && !this._upButton.ClickEventHandlers.Contains(new Action<Widget>(this.OnUpButtonClicked)))
					{
						this._upButton.ClickEventHandlers.Add(new Action<Widget>(this.OnUpButtonClicked));
					}
				}
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000095AE File Offset: 0x000077AE
		// (set) Token: 0x060002DD RID: 733 RVA: 0x000095B8 File Offset: 0x000077B8
		[Editor(false)]
		public ButtonWidget DownButton
		{
			get
			{
				return this._downButton;
			}
			set
			{
				if (this._downButton != value)
				{
					this._downButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "DownButton");
					if (value != null && !this._downButton.ClickEventHandlers.Contains(new Action<Widget>(this.OnDownButtonClicked)))
					{
						this._downButton.ClickEventHandlers.Add(new Action<Widget>(this.OnDownButtonClicked));
					}
				}
			}
		}

		// Token: 0x04000124 RID: 292
		private bool _showOneAdded;

		// Token: 0x04000125 RID: 293
		private float _minValue;

		// Token: 0x04000126 RID: 294
		private float _maxValue;

		// Token: 0x04000127 RID: 295
		private int _intValue = int.MinValue;

		// Token: 0x04000128 RID: 296
		private float _value = float.MinValue;

		// Token: 0x04000129 RID: 297
		private TextWidget _textWidget;

		// Token: 0x0400012A RID: 298
		private ButtonWidget _upButton;

		// Token: 0x0400012B RID: 299
		private ButtonWidget _downButton;
	}
}
