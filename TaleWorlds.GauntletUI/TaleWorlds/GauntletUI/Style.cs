using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000018 RID: 24
	public class Style : IDataSource
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000906D File Offset: 0x0000726D
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00009075 File Offset: 0x00007275
		public Style DefaultStyle { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000907E File Offset: 0x0000727E
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00009086 File Offset: 0x00007286
		[Editor(false)]
		public string Name { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000908F File Offset: 0x0000728F
		private uint DefaultStyleVersion
		{
			get
			{
				if (this.DefaultStyle == null)
				{
					return 0U;
				}
				return (uint)((long)this.DefaultStyle._localVersion % (long)((ulong)-1));
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000090AC File Offset: 0x000072AC
		public long Version
		{
			get
			{
				uint num = 0U;
				for (int i = 0; i < this._layersWithIndex.Count; i++)
				{
					num += this._layersWithIndex[i].Version;
				}
				return ((long)this._localVersion << 32 | (long)((ulong)num)) + (long)((ulong)this.DefaultStyleVersion);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000090FA File Offset: 0x000072FA
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009102 File Offset: 0x00007302
		[Editor(false)]
		public string AnimationToPlayOnBegin
		{
			get
			{
				return this._animationToPlayOnBegin;
			}
			set
			{
				this._animationToPlayOnBegin = value;
				this.AnimationMode = StyleAnimationMode.Animation;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009112 File Offset: 0x00007312
		public int LayerCount
		{
			get
			{
				return this._layers.Count;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000911F File Offset: 0x0000731F
		public StyleLayer DefaultLayer
		{
			get
			{
				return this._layers["Default"];
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009131 File Offset: 0x00007331
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009139 File Offset: 0x00007339
		[Editor(false)]
		public StyleAnimationMode AnimationMode { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009142 File Offset: 0x00007342
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000915E File Offset: 0x0000735E
		[Editor(false)]
		public Color FontColor
		{
			get
			{
				if (this._isFontColorChanged)
				{
					return this._fontColor;
				}
				return this.DefaultStyle.FontColor;
			}
			set
			{
				if (this.FontColor != value)
				{
					this._isFontColorChanged = true;
					this._fontColor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000918A File Offset: 0x0000738A
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000091A6 File Offset: 0x000073A6
		[Editor(false)]
		public Color TextGlowColor
		{
			get
			{
				if (this._isTextGlowColorChanged)
				{
					return this._textGlowColor;
				}
				return this.DefaultStyle.TextGlowColor;
			}
			set
			{
				if (this.TextGlowColor != value)
				{
					this._isTextGlowColorChanged = true;
					this._textGlowColor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000091D2 File Offset: 0x000073D2
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000091EE File Offset: 0x000073EE
		[Editor(false)]
		public Color TextOutlineColor
		{
			get
			{
				if (this._isTextOutlineColorChanged)
				{
					return this._textOutlineColor;
				}
				return this.DefaultStyle.TextOutlineColor;
			}
			set
			{
				if (this.TextOutlineColor != value)
				{
					this._isTextOutlineColorChanged = true;
					this._textOutlineColor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000921A File Offset: 0x0000741A
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009236 File Offset: 0x00007436
		[Editor(false)]
		public float TextOutlineAmount
		{
			get
			{
				if (this._isTextOutlineAmountChanged)
				{
					return this._textOutlineAmount;
				}
				return this.DefaultStyle.TextOutlineAmount;
			}
			set
			{
				if (this.TextOutlineAmount != value)
				{
					this._isTextOutlineAmountChanged = true;
					this._textOutlineAmount = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000925D File Offset: 0x0000745D
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00009279 File Offset: 0x00007479
		[Editor(false)]
		public float TextGlowRadius
		{
			get
			{
				if (this._isTextGlowRadiusChanged)
				{
					return this._textGlowRadius;
				}
				return this.DefaultStyle.TextGlowRadius;
			}
			set
			{
				if (this.TextGlowRadius != value)
				{
					this._isTextGlowRadiusChanged = true;
					this._textGlowRadius = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000092A0 File Offset: 0x000074A0
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000092BC File Offset: 0x000074BC
		[Editor(false)]
		public float TextBlur
		{
			get
			{
				if (this._isTextBlurChanged)
				{
					return this._textBlur;
				}
				return this.DefaultStyle.TextBlur;
			}
			set
			{
				if (this.TextBlur != value)
				{
					this._isTextBlurChanged = true;
					this._textBlur = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000092E3 File Offset: 0x000074E3
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000092FF File Offset: 0x000074FF
		[Editor(false)]
		public float TextShadowOffset
		{
			get
			{
				if (this._isTextShadowOffsetChanged)
				{
					return this._textShadowOffset;
				}
				return this.DefaultStyle.TextShadowOffset;
			}
			set
			{
				if (this.TextShadowOffset != value)
				{
					this._isTextShadowOffsetChanged = true;
					this._textShadowOffset = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009326 File Offset: 0x00007526
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009342 File Offset: 0x00007542
		[Editor(false)]
		public float TextShadowAngle
		{
			get
			{
				if (this._isTextShadowAngleChanged)
				{
					return this._textShadowAngle;
				}
				return this.DefaultStyle.TextShadowAngle;
			}
			set
			{
				if (this.TextShadowAngle != value)
				{
					this._isTextShadowAngleChanged = true;
					this._textShadowAngle = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009369 File Offset: 0x00007569
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00009385 File Offset: 0x00007585
		[Editor(false)]
		public float TextColorFactor
		{
			get
			{
				if (this._isTextColorFactorChanged)
				{
					return this._textColorFactor;
				}
				return this.DefaultStyle.TextColorFactor;
			}
			set
			{
				if (this.TextColorFactor != value)
				{
					this._isTextColorFactorChanged = true;
					this._textColorFactor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000093AC File Offset: 0x000075AC
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000093C8 File Offset: 0x000075C8
		[Editor(false)]
		public float TextAlphaFactor
		{
			get
			{
				if (this._isTextAlphaFactorChanged)
				{
					return this._textAlphaFactor;
				}
				return this.DefaultStyle.TextAlphaFactor;
			}
			set
			{
				if (this.TextAlphaFactor != value)
				{
					this._isTextAlphaFactorChanged = true;
					this._textAlphaFactor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000093EF File Offset: 0x000075EF
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000940B File Offset: 0x0000760B
		[Editor(false)]
		public float TextHueFactor
		{
			get
			{
				if (this._isTextHueFactorChanged)
				{
					return this._textHueFactor;
				}
				return this.DefaultStyle.TextHueFactor;
			}
			set
			{
				if (this.TextHueFactor != value)
				{
					this._isTextHueFactorChanged = true;
					this._textHueFactor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00009432 File Offset: 0x00007632
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000944E File Offset: 0x0000764E
		[Editor(false)]
		public float TextSaturationFactor
		{
			get
			{
				if (this._isTextSaturationFactorChanged)
				{
					return this._textSaturationFactor;
				}
				return this.DefaultStyle.TextSaturationFactor;
			}
			set
			{
				if (this.TextSaturationFactor != value)
				{
					this._isTextSaturationFactorChanged = true;
					this._textSaturationFactor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009475 File Offset: 0x00007675
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009491 File Offset: 0x00007691
		[Editor(false)]
		public float TextValueFactor
		{
			get
			{
				if (this._isTextValueFactorChanged)
				{
					return this._textValueFactor;
				}
				return this.DefaultStyle.TextValueFactor;
			}
			set
			{
				if (this.TextValueFactor != value)
				{
					this._isTextValueFactorChanged = true;
					this._textValueFactor = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000094B8 File Offset: 0x000076B8
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000094D4 File Offset: 0x000076D4
		[Editor(false)]
		public Font Font
		{
			get
			{
				if (this._isFontChanged)
				{
					return this._font;
				}
				return this.DefaultStyle.Font;
			}
			set
			{
				if (this.Font != value)
				{
					this._isFontChanged = true;
					this._font = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000094FB File Offset: 0x000076FB
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009517 File Offset: 0x00007717
		[Editor(false)]
		public FontStyle FontStyle
		{
			get
			{
				if (this._isFontStyleChanged)
				{
					return this._fontStyle;
				}
				return this.DefaultStyle.FontStyle;
			}
			set
			{
				if (this.FontStyle != value)
				{
					this._isFontStyleChanged = true;
					this._fontStyle = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000953E File Offset: 0x0000773E
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000955A File Offset: 0x0000775A
		[Editor(false)]
		public int FontSize
		{
			get
			{
				if (this._isFontSizeChanged)
				{
					return this._fontSize;
				}
				return this.DefaultStyle.FontSize;
			}
			set
			{
				if (this.FontSize != value)
				{
					this._isFontSizeChanged = true;
					this._fontSize = value;
					this._localVersion++;
				}
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009584 File Offset: 0x00007784
		public Style(IEnumerable<BrushLayer> layers)
		{
			this.AnimationMode = StyleAnimationMode.BasicTransition;
			this._layers = new Dictionary<string, StyleLayer>();
			this._layersWithIndex = new MBList<StyleLayer>();
			this._fontColor = new Color(0f, 0f, 0f, 1f);
			this._textGlowColor = new Color(0f, 0f, 0f, 1f);
			this._textOutlineColor = new Color(0f, 0f, 0f, 1f);
			this._textOutlineAmount = 0f;
			this._textGlowRadius = 0.2f;
			this._textBlur = 0.8f;
			this._textShadowOffset = 0.5f;
			this._textShadowAngle = 45f;
			this._textColorFactor = 1f;
			this._textAlphaFactor = 1f;
			this._textHueFactor = 0f;
			this._textSaturationFactor = 0f;
			this._textValueFactor = 0f;
			this._fontSize = 30;
			foreach (BrushLayer brushLayer in layers)
			{
				StyleLayer layer = new StyleLayer(brushLayer);
				this.AddLayer(layer);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000096C8 File Offset: 0x000078C8
		public void FillFrom(Style style)
		{
			this.Name = style.Name;
			this.FontColor = style.FontColor;
			this.TextGlowColor = style.TextGlowColor;
			this.TextOutlineColor = style.TextOutlineColor;
			this.TextOutlineAmount = style.TextOutlineAmount;
			this.TextGlowRadius = style.TextGlowRadius;
			this.TextBlur = style.TextBlur;
			this.TextShadowOffset = style.TextShadowOffset;
			this.TextShadowAngle = style.TextShadowAngle;
			this.TextColorFactor = style.TextColorFactor;
			this.TextAlphaFactor = style.TextAlphaFactor;
			this.TextHueFactor = style.TextHueFactor;
			this.TextSaturationFactor = style.TextSaturationFactor;
			this.TextValueFactor = style.TextValueFactor;
			this.Font = style.Font;
			this.FontStyle = style.FontStyle;
			this.FontSize = style.FontSize;
			this.AnimationToPlayOnBegin = style.AnimationToPlayOnBegin;
			this.AnimationMode = style.AnimationMode;
			foreach (StyleLayer styleLayer in style._layers.Values)
			{
				this._layers[styleLayer.Name].FillFrom(styleLayer);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009814 File Offset: 0x00007A14
		public void AddLayer(StyleLayer layer)
		{
			this._layers.Add(layer.Name, layer);
			this._layersWithIndex.Add(layer);
			this._localVersion++;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009842 File Offset: 0x00007A42
		public void RemoveLayer(string layerName)
		{
			this._layersWithIndex.Remove(this._layers[layerName]);
			this._layers.Remove(layerName);
			this._localVersion++;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009877 File Offset: 0x00007A77
		public StyleLayer GetLayer(int index)
		{
			return this._layersWithIndex[index];
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009885 File Offset: 0x00007A85
		public StyleLayer GetLayer(string name)
		{
			if (this._layers.ContainsKey(name))
			{
				return this._layers[name];
			}
			return null;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000098A3 File Offset: 0x00007AA3
		public StyleLayer[] GetLayers()
		{
			return this._layersWithIndex.ToArray();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000098B0 File Offset: 0x00007AB0
		public TextMaterial CreateTextMaterial(TwoDimensionDrawContext drawContext)
		{
			TextMaterial textMaterial = drawContext.CreateTextMaterial();
			textMaterial.Color = this.FontColor;
			textMaterial.GlowColor = this.TextGlowColor;
			textMaterial.OutlineColor = this.TextOutlineColor;
			textMaterial.OutlineAmount = this.TextOutlineAmount;
			textMaterial.GlowRadius = this.TextGlowRadius;
			textMaterial.Blur = this.TextBlur;
			textMaterial.ShadowOffset = this.TextShadowOffset;
			textMaterial.ShadowAngle = this.TextShadowAngle;
			textMaterial.ColorFactor = this.TextColorFactor;
			textMaterial.AlphaFactor = this.TextAlphaFactor;
			textMaterial.HueFactor = this.TextHueFactor;
			textMaterial.SaturationFactor = this.TextSaturationFactor;
			textMaterial.ValueFactor = this.TextValueFactor;
			return textMaterial;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009960 File Offset: 0x00007B60
		public float GetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineAmount:
				return this.TextOutlineAmount;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowRadius:
				return this.TextGlowRadius;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextBlur:
				return this.TextBlur;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowOffset:
				return this.TextShadowOffset;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowAngle:
				return this.TextShadowAngle;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextColorFactor:
				return this.TextColorFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextAlphaFactor:
				return this.TextAlphaFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextHueFactor:
				return this.TextHueFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextSaturationFactor:
				return this.TextSaturationFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextValueFactor:
				return this.TextValueFactor;
			default:
				Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\Style.cs", "GetValueAsFloat", 615);
				return 0f;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009A04 File Offset: 0x00007C04
		public Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
				return this.FontColor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
				return this.TextGlowColor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
				return this.TextOutlineColor;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\Style.cs", "GetValueAsColor", 635);
			return Color.Black;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009A62 File Offset: 0x00007C62
		public Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\Style.cs", "GetValueAsSprite", 643);
			return null;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009A80 File Offset: 0x00007C80
		public void SetAsDefaultStyle()
		{
			this._isFontColorChanged = true;
			this._isTextGlowColorChanged = true;
			this._isTextOutlineColorChanged = true;
			this._isTextOutlineAmountChanged = true;
			this._isTextGlowRadiusChanged = true;
			this._isTextBlurChanged = true;
			this._isTextShadowOffsetChanged = true;
			this._isTextShadowAngleChanged = true;
			this._isTextColorFactorChanged = true;
			this._isTextAlphaFactorChanged = true;
			this._isTextHueFactorChanged = true;
			this._isTextSaturationFactorChanged = true;
			this._isTextValueFactorChanged = true;
			this._isFontChanged = true;
			this._isFontStyleChanged = true;
			this._isFontSizeChanged = true;
			this.DefaultStyle = null;
		}

		// Token: 0x04000099 RID: 153
		private int _localVersion;

		// Token: 0x0400009A RID: 154
		private bool _isFontColorChanged;

		// Token: 0x0400009B RID: 155
		private bool _isTextGlowColorChanged;

		// Token: 0x0400009C RID: 156
		private bool _isTextOutlineColorChanged;

		// Token: 0x0400009D RID: 157
		private bool _isTextOutlineAmountChanged;

		// Token: 0x0400009E RID: 158
		private bool _isTextGlowRadiusChanged;

		// Token: 0x0400009F RID: 159
		private bool _isTextBlurChanged;

		// Token: 0x040000A0 RID: 160
		private bool _isTextShadowOffsetChanged;

		// Token: 0x040000A1 RID: 161
		private bool _isTextShadowAngleChanged;

		// Token: 0x040000A2 RID: 162
		private bool _isTextColorFactorChanged;

		// Token: 0x040000A3 RID: 163
		private bool _isTextAlphaFactorChanged;

		// Token: 0x040000A4 RID: 164
		private bool _isTextHueFactorChanged;

		// Token: 0x040000A5 RID: 165
		private bool _isTextSaturationFactorChanged;

		// Token: 0x040000A6 RID: 166
		private bool _isTextValueFactorChanged;

		// Token: 0x040000A7 RID: 167
		private bool _isFontChanged;

		// Token: 0x040000A8 RID: 168
		private bool _isFontStyleChanged;

		// Token: 0x040000A9 RID: 169
		private bool _isFontSizeChanged;

		// Token: 0x040000AA RID: 170
		private Color _fontColor;

		// Token: 0x040000AB RID: 171
		private Color _textGlowColor;

		// Token: 0x040000AC RID: 172
		private Color _textOutlineColor;

		// Token: 0x040000AD RID: 173
		private float _textOutlineAmount;

		// Token: 0x040000AE RID: 174
		private float _textGlowRadius;

		// Token: 0x040000AF RID: 175
		private float _textBlur;

		// Token: 0x040000B0 RID: 176
		private float _textShadowOffset;

		// Token: 0x040000B1 RID: 177
		private float _textShadowAngle;

		// Token: 0x040000B2 RID: 178
		private float _textColorFactor;

		// Token: 0x040000B3 RID: 179
		private float _textAlphaFactor;

		// Token: 0x040000B4 RID: 180
		private float _textHueFactor;

		// Token: 0x040000B5 RID: 181
		private float _textSaturationFactor;

		// Token: 0x040000B6 RID: 182
		private float _textValueFactor;

		// Token: 0x040000B7 RID: 183
		private Font _font;

		// Token: 0x040000B8 RID: 184
		private FontStyle _fontStyle;

		// Token: 0x040000B9 RID: 185
		private int _fontSize;

		// Token: 0x040000BA RID: 186
		private string _animationToPlayOnBegin;

		// Token: 0x040000BB RID: 187
		private Dictionary<string, StyleLayer> _layers;

		// Token: 0x040000BC RID: 188
		private MBList<StyleLayer> _layersWithIndex;
	}
}
