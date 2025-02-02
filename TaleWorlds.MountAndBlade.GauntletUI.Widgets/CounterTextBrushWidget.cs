using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000015 RID: 21
	public class CounterTextBrushWidget : BrushWidget
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004C93 File Offset: 0x00002E93
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00004C9B File Offset: 0x00002E9B
		public float CounterTime { get; set; } = 0.5f;

		// Token: 0x0600010E RID: 270 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public CounterTextBrushWidget(UIContext context) : base(context)
		{
			FontFactory fontFactory = context.FontFactory;
			this._text = new Text((int)base.Size.X, (int)base.Size.Y, fontFactory.DefaultFont, new Func<int, Font>(fontFactory.GetUsableFontForCharacter));
			base.LayoutImp = new TextLayout(this._text);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004D28 File Offset: 0x00002F28
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (MathF.Abs(this._targetValue - this._currentValue) > 1E-45f && MathF.Abs(base.Context.EventManager.Time - this._startTime) < this.CounterTime)
			{
				this._currentValue = Mathf.Lerp(this._currentValue, this._targetValue, (base.Context.EventManager.Time - this._startTime) / this.CounterTime);
				if (this.Clamped)
				{
					this._currentValue = Mathf.Clamp(this._currentValue, this.MinValue, this.MaxValue);
				}
				this.ForceSetValue(this._currentValue);
			}
			else
			{
				this.ForceSetValue(this._targetValue);
			}
			this.RefreshTextParameters();
			TextMaterial material = base.BrushRenderer.CreateTextMaterial(drawContext);
			Vector2 globalPosition = base.GlobalPosition;
			drawContext.Draw(this._text, material, globalPosition.X, globalPosition.Y, base.Size.X, base.Size.Y);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004E34 File Offset: 0x00003034
		private void SetText(string value)
		{
			base.SetMeasureAndLayoutDirty();
			this._text.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
			if (this.ShowSign && this._currentValue > 0f)
			{
				this._text.Value = "+" + value;
			}
			else
			{
				this._text.Value = value;
			}
			this.RefreshTextParameters();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004EA1 File Offset: 0x000030A1
		public void SetInitialValue(float value)
		{
			this._initialValue = value;
			this._currentValue = value;
			this._initValueSet = true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004EB8 File Offset: 0x000030B8
		private void SetTargetValue(float targetValue)
		{
			if (!this._initValueSet)
			{
				this._currentValue = targetValue;
				this._initValueSet = true;
			}
			this._initialValue = this._currentValue;
			this._startTime = base.Context.EventManager.Time;
			this.RefreshTextAnimation(targetValue - this._targetValue);
			this._targetValue = targetValue;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004F14 File Offset: 0x00003114
		private void RefreshTextParameters()
		{
			float fontSize = (float)base.ReadOnlyBrush.FontSize * base._scaleToUse;
			this._text.HorizontalAlignment = base.ReadOnlyBrush.TextHorizontalAlignment;
			this._text.VerticalAlignment = base.ReadOnlyBrush.TextVerticalAlignment;
			this._text.FontSize = fontSize;
			this._text.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
			if (base.ReadOnlyBrush.Font != null)
			{
				this._text.Font = base.ReadOnlyBrush.Font;
				return;
			}
			FontFactory fontFactory = base.Context.FontFactory;
			this._text.Font = fontFactory.DefaultFont;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004FCC File Offset: 0x000031CC
		private void RefreshTextAnimation(float valueDifference)
		{
			if (valueDifference > 0f)
			{
				if (base.CurrentState == "Positive")
				{
					base.BrushRenderer.RestartAnimation();
					return;
				}
				this.SetState("Positive");
				return;
			}
			else
			{
				if (valueDifference >= 0f)
				{
					Debug.FailedAssert("Value change in party label cannot be 0", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\CounterTextBrushWidget.cs", "RefreshTextAnimation", 142);
					return;
				}
				if (base.CurrentState == "Negative")
				{
					base.BrushRenderer.RestartAnimation();
					return;
				}
				this.SetState("Negative");
				return;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005058 File Offset: 0x00003258
		public void ForceSetValue(float value)
		{
			this.SetText(this.ShowFloatingPoint ? value.ToString("F2") : MathF.Floor(value).ToString());
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000508F File Offset: 0x0000328F
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000509E File Offset: 0x0000329E
		[Editor(false)]
		public int IntTarget
		{
			get
			{
				return (int)Math.Round((double)this._targetValue);
			}
			set
			{
				if (this._targetValue != (float)value)
				{
					this.SetTargetValue((float)value);
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000050B2 File Offset: 0x000032B2
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000050BA File Offset: 0x000032BA
		[Editor(false)]
		public float FloatTarget
		{
			get
			{
				return this._targetValue;
			}
			set
			{
				if (this._targetValue != value)
				{
					this.SetTargetValue(value);
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000050CC File Offset: 0x000032CC
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000050D4 File Offset: 0x000032D4
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
				}
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000050F2 File Offset: 0x000032F2
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000050FA File Offset: 0x000032FA
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
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005118 File Offset: 0x00003318
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005120 File Offset: 0x00003320
		[Editor(false)]
		public bool ShowSign
		{
			get
			{
				return this._showSign;
			}
			set
			{
				if (this._showSign != value)
				{
					this._showSign = value;
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005132 File Offset: 0x00003332
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000513A File Offset: 0x0000333A
		[Editor(false)]
		public bool Clamped
		{
			get
			{
				return this._clamped;
			}
			set
			{
				if (this._clamped != value)
				{
					this._clamped = value;
				}
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000514C File Offset: 0x0000334C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005154 File Offset: 0x00003354
		[Editor(false)]
		public bool ShowFloatingPoint
		{
			get
			{
				return this._showFloatingPoint;
			}
			set
			{
				if (value != this._showFloatingPoint)
				{
					this._showFloatingPoint = value;
				}
			}
		}

		// Token: 0x04000084 RID: 132
		private readonly Text _text;

		// Token: 0x04000085 RID: 133
		private float _currentValue;

		// Token: 0x04000086 RID: 134
		private float _initialValue;

		// Token: 0x04000087 RID: 135
		private float _startTime;

		// Token: 0x04000088 RID: 136
		private bool _initValueSet;

		// Token: 0x04000089 RID: 137
		private float _targetValue;

		// Token: 0x0400008A RID: 138
		private float _minValue = float.MinValue;

		// Token: 0x0400008B RID: 139
		private float _maxValue = float.MaxValue;

		// Token: 0x0400008C RID: 140
		private bool _showSign;

		// Token: 0x0400008D RID: 141
		public bool _clamped;

		// Token: 0x0400008E RID: 142
		private bool _showFloatingPoint;
	}
}
