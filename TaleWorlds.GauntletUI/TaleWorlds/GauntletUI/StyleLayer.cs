using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200001A RID: 26
	public class StyleLayer : IBrushLayerData, IDataSource
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00009B04 File Offset: 0x00007D04
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00009B0C File Offset: 0x00007D0C
		public BrushLayer SourceLayer { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00009B15 File Offset: 0x00007D15
		public uint Version
		{
			get
			{
				return this._localVersion + this.SourceLayer.Version;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009B29 File Offset: 0x00007D29
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00009B36 File Offset: 0x00007D36
		[Editor(false)]
		public string Name
		{
			get
			{
				return this.SourceLayer.Name;
			}
			set
			{
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009B38 File Offset: 0x00007D38
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00009B54 File Offset: 0x00007D54
		[Editor(false)]
		public Sprite Sprite
		{
			get
			{
				if (this._isSpriteChanged)
				{
					return this._sprite;
				}
				return this.SourceLayer.Sprite;
			}
			set
			{
				if (this.Sprite != value)
				{
					this._isSpriteChanged = (this.SourceLayer.Sprite != value);
					this._sprite = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009B8B File Offset: 0x00007D8B
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00009BA7 File Offset: 0x00007DA7
		[Editor(false)]
		public Color Color
		{
			get
			{
				if (this._isColorChanged)
				{
					return this._color;
				}
				return this.SourceLayer.Color;
			}
			set
			{
				if (this.Color != value)
				{
					this._isColorChanged = (this.SourceLayer.Color != value);
					this._color = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009BE3 File Offset: 0x00007DE3
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00009BFF File Offset: 0x00007DFF
		[Editor(false)]
		public float ColorFactor
		{
			get
			{
				if (this._isColorFactorChanged)
				{
					return this._colorFactor;
				}
				return this.SourceLayer.ColorFactor;
			}
			set
			{
				if (this.ColorFactor != value)
				{
					this._isColorFactorChanged = (MathF.Abs(this.SourceLayer.ColorFactor - value) > 1E-05f);
					this._colorFactor = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009C3E File Offset: 0x00007E3E
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00009C5A File Offset: 0x00007E5A
		[Editor(false)]
		public float AlphaFactor
		{
			get
			{
				if (this._isAlphaFactorChanged)
				{
					return this._alphaFactor;
				}
				return this.SourceLayer.AlphaFactor;
			}
			set
			{
				if (this.AlphaFactor != value)
				{
					this._isAlphaFactorChanged = (MathF.Abs(this.SourceLayer.AlphaFactor - value) > 1E-05f);
					this._alphaFactor = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00009C99 File Offset: 0x00007E99
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00009CB5 File Offset: 0x00007EB5
		[Editor(false)]
		public float HueFactor
		{
			get
			{
				if (this._isHueFactorChanged)
				{
					return this._hueFactor;
				}
				return this.SourceLayer.HueFactor;
			}
			set
			{
				if (this.HueFactor != value)
				{
					this._isHueFactorChanged = (MathF.Abs(this.SourceLayer.HueFactor - value) > 1E-05f);
					this._hueFactor = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00009CF4 File Offset: 0x00007EF4
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00009D10 File Offset: 0x00007F10
		[Editor(false)]
		public float SaturationFactor
		{
			get
			{
				if (this._isSaturationFactorChanged)
				{
					return this._saturationFactor;
				}
				return this.SourceLayer.SaturationFactor;
			}
			set
			{
				if (this.SaturationFactor != value)
				{
					this._isSaturationFactorChanged = (MathF.Abs(this.SourceLayer.SaturationFactor - value) > 1E-05f);
					this._saturationFactor = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00009D4F File Offset: 0x00007F4F
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00009D6B File Offset: 0x00007F6B
		[Editor(false)]
		public float ValueFactor
		{
			get
			{
				if (this._isValueFactorChanged)
				{
					return this._valueFactor;
				}
				return this.SourceLayer.ValueFactor;
			}
			set
			{
				if (this.ValueFactor != value)
				{
					this._isValueFactorChanged = (MathF.Abs(this.SourceLayer.ValueFactor - value) > 1E-05f);
					this._valueFactor = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00009DAA File Offset: 0x00007FAA
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00009DC6 File Offset: 0x00007FC6
		[Editor(false)]
		public bool IsHidden
		{
			get
			{
				if (this._isIsHiddenChanged)
				{
					return this._isHidden;
				}
				return this.SourceLayer.IsHidden;
			}
			set
			{
				if (this.IsHidden != value)
				{
					this._isIsHiddenChanged = (this.SourceLayer.IsHidden != value);
					this._isHidden = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00009DFD File Offset: 0x00007FFD
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009E19 File Offset: 0x00008019
		[Editor(false)]
		public bool UseOverlayAlphaAsMask
		{
			get
			{
				if (this._isUseOverlayAlphaAsMaskChanged)
				{
					return this._useOverlayAlphaAsMask;
				}
				return this.SourceLayer.UseOverlayAlphaAsMask;
			}
			set
			{
				if (this.UseOverlayAlphaAsMask != value)
				{
					this._isUseOverlayAlphaAsMaskChanged = (this.SourceLayer.UseOverlayAlphaAsMask != value);
					this._useOverlayAlphaAsMask = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009E50 File Offset: 0x00008050
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009E6C File Offset: 0x0000806C
		[Editor(false)]
		public float XOffset
		{
			get
			{
				if (this._isXOffsetChanged)
				{
					return this._xOffset;
				}
				return this.SourceLayer.XOffset;
			}
			set
			{
				if (this.XOffset != value)
				{
					this._isXOffsetChanged = (MathF.Abs(this.SourceLayer.XOffset - value) > 1E-05f);
					this._xOffset = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009EAB File Offset: 0x000080AB
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009EC7 File Offset: 0x000080C7
		[Editor(false)]
		public float YOffset
		{
			get
			{
				if (this._isYOffsetChanged)
				{
					return this._yOffset;
				}
				return this.SourceLayer.YOffset;
			}
			set
			{
				if (this.YOffset != value)
				{
					this._isYOffsetChanged = (MathF.Abs(this.SourceLayer.YOffset - value) > 1E-05f);
					this._yOffset = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009F06 File Offset: 0x00008106
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009F22 File Offset: 0x00008122
		[Editor(false)]
		public float ExtendLeft
		{
			get
			{
				if (this._isExtendLeftChanged)
				{
					return this._extendLeft;
				}
				return this.SourceLayer.ExtendLeft;
			}
			set
			{
				if (this.ExtendLeft != value)
				{
					this._isExtendLeftChanged = (MathF.Abs(this.SourceLayer.ExtendLeft - value) > 1E-05f);
					this._extendLeft = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009F61 File Offset: 0x00008161
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00009F7D File Offset: 0x0000817D
		[Editor(false)]
		public float ExtendRight
		{
			get
			{
				if (this._isExtendRightChanged)
				{
					return this._extendRight;
				}
				return this.SourceLayer.ExtendRight;
			}
			set
			{
				if (this.ExtendRight != value)
				{
					this._isExtendRightChanged = (MathF.Abs(this.SourceLayer.ExtendRight - value) > 1E-05f);
					this._extendRight = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009FBC File Offset: 0x000081BC
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00009FD8 File Offset: 0x000081D8
		[Editor(false)]
		public float ExtendTop
		{
			get
			{
				if (this._isExtendTopChanged)
				{
					return this._extendTop;
				}
				return this.SourceLayer.ExtendTop;
			}
			set
			{
				if (this.ExtendTop != value)
				{
					this._isExtendTopChanged = (MathF.Abs(this.SourceLayer.ExtendTop - value) > 1E-05f);
					this._extendTop = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A017 File Offset: 0x00008217
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000A033 File Offset: 0x00008233
		[Editor(false)]
		public float ExtendBottom
		{
			get
			{
				if (this._isExtendBottomChanged)
				{
					return this._extendBottom;
				}
				return this.SourceLayer.ExtendBottom;
			}
			set
			{
				if (this.ExtendBottom != value)
				{
					this._isExtendBottomChanged = (MathF.Abs(this.SourceLayer.ExtendBottom - value) > 1E-05f);
					this._extendBottom = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000A072 File Offset: 0x00008272
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000A08E File Offset: 0x0000828E
		[Editor(false)]
		public float OverridenWidth
		{
			get
			{
				if (this._isOverridenWidthChanged)
				{
					return this._overridenWidth;
				}
				return this.SourceLayer.OverridenWidth;
			}
			set
			{
				if (this.OverridenWidth != value)
				{
					this._isOverridenWidthChanged = (MathF.Abs(this.SourceLayer.OverridenWidth - value) > 1E-05f);
					this._overridenWidth = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A0CD File Offset: 0x000082CD
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000A0E9 File Offset: 0x000082E9
		[Editor(false)]
		public float OverridenHeight
		{
			get
			{
				if (this._isOverridenHeightChanged)
				{
					return this._overridenHeight;
				}
				return this.SourceLayer.OverridenHeight;
			}
			set
			{
				if (this.OverridenHeight != value)
				{
					this._isOverridenHeightChanged = (MathF.Abs(this.SourceLayer.OverridenHeight - value) > 1E-05f);
					this._overridenHeight = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A128 File Offset: 0x00008328
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000A144 File Offset: 0x00008344
		[Editor(false)]
		public BrushLayerSizePolicy WidthPolicy
		{
			get
			{
				if (this._isWidthPolicyChanged)
				{
					return this._widthPolicy;
				}
				return this.SourceLayer.WidthPolicy;
			}
			set
			{
				if (this.WidthPolicy != value)
				{
					this._isWidthPolicyChanged = (this.SourceLayer.WidthPolicy != value);
					this._widthPolicy = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A17B File Offset: 0x0000837B
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A197 File Offset: 0x00008397
		public BrushLayerSizePolicy HeightPolicy
		{
			get
			{
				if (this._isHeightPolicyChanged)
				{
					return this._heightPolicy;
				}
				return this.SourceLayer.HeightPolicy;
			}
			set
			{
				if (this.HeightPolicy != value)
				{
					this._isHeightPolicyChanged = (this.SourceLayer.HeightPolicy != value);
					this._heightPolicy = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A1CE File Offset: 0x000083CE
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A1EA File Offset: 0x000083EA
		[Editor(false)]
		public bool HorizontalFlip
		{
			get
			{
				if (this._isHorizontalFlipChanged)
				{
					return this._horizontalFlip;
				}
				return this.SourceLayer.HorizontalFlip;
			}
			set
			{
				if (this.HorizontalFlip != value)
				{
					this._isHorizontalFlipChanged = (this.SourceLayer.HorizontalFlip != value);
					this._horizontalFlip = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A221 File Offset: 0x00008421
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A23D File Offset: 0x0000843D
		[Editor(false)]
		public bool VerticalFlip
		{
			get
			{
				if (this._isVerticalFlipChanged)
				{
					return this._verticalFlip;
				}
				return this.SourceLayer.VerticalFlip;
			}
			set
			{
				if (this.VerticalFlip != value)
				{
					this._isVerticalFlipChanged = (this.SourceLayer.VerticalFlip != value);
					this._verticalFlip = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A274 File Offset: 0x00008474
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000A290 File Offset: 0x00008490
		[Editor(false)]
		public BrushOverlayMethod OverlayMethod
		{
			get
			{
				if (this._isOverlayMethodChanged)
				{
					return this._overlayMethod;
				}
				return this.SourceLayer.OverlayMethod;
			}
			set
			{
				if (this.OverlayMethod != value)
				{
					this._isOverlayMethodChanged = (this.SourceLayer.OverlayMethod != value);
					this._overlayMethod = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A2C7 File Offset: 0x000084C7
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000A2E3 File Offset: 0x000084E3
		[Editor(false)]
		public Sprite OverlaySprite
		{
			get
			{
				if (this._isOverlaySpriteChanged)
				{
					return this._overlaySprite;
				}
				return this.SourceLayer.OverlaySprite;
			}
			set
			{
				if (this.OverlaySprite != value)
				{
					this._isOverlaySpriteChanged = (this.SourceLayer.OverlaySprite != value);
					this._overlaySprite = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A31A File Offset: 0x0000851A
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000A336 File Offset: 0x00008536
		[Editor(false)]
		public float OverlayXOffset
		{
			get
			{
				if (this._isOverlayXOffsetChanged)
				{
					return this._overlayXOffset;
				}
				return this.SourceLayer.OverlayXOffset;
			}
			set
			{
				if (this.OverlayXOffset != value)
				{
					this._isOverlayXOffsetChanged = (MathF.Abs(this.SourceLayer.OverlayXOffset - value) > 1E-05f);
					this._overlayXOffset = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A375 File Offset: 0x00008575
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000A391 File Offset: 0x00008591
		[Editor(false)]
		public float OverlayYOffset
		{
			get
			{
				if (this._isOverlayYOffsetChanged)
				{
					return this._overlayYOffset;
				}
				return this.SourceLayer.OverlayYOffset;
			}
			set
			{
				if (this.OverlayYOffset != value)
				{
					this._isOverlayYOffsetChanged = (MathF.Abs(this.SourceLayer.OverlayYOffset - value) > 1E-05f);
					this._overlayYOffset = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A3D0 File Offset: 0x000085D0
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000A3EC File Offset: 0x000085EC
		[Editor(false)]
		public bool UseRandomBaseOverlayXOffset
		{
			get
			{
				if (this._isUseRandomBaseOverlayXOffset)
				{
					return this._useRandomBaseOverlayXOffset;
				}
				return this.SourceLayer.UseRandomBaseOverlayXOffset;
			}
			set
			{
				if (this.UseRandomBaseOverlayXOffset != value)
				{
					this._isUseRandomBaseOverlayXOffset = (this._useRandomBaseOverlayXOffset != value);
					this._useRandomBaseOverlayXOffset = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A41E File Offset: 0x0000861E
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000A43A File Offset: 0x0000863A
		[Editor(false)]
		public bool UseRandomBaseOverlayYOffset
		{
			get
			{
				if (this._isUseRandomBaseOverlayYOffset)
				{
					return this._useRandomBaseOverlayYOffset;
				}
				return this.SourceLayer.UseRandomBaseOverlayYOffset;
			}
			set
			{
				if (this.UseRandomBaseOverlayYOffset != value)
				{
					this._isUseRandomBaseOverlayYOffset = (this._useRandomBaseOverlayYOffset != value);
					this._useRandomBaseOverlayYOffset = value;
					this._localVersion += 1U;
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A46C File Offset: 0x0000866C
		public StyleLayer(BrushLayer brushLayer)
		{
			this.SourceLayer = brushLayer;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A47B File Offset: 0x0000867B
		public static StyleLayer CreateFrom(StyleLayer source)
		{
			StyleLayer styleLayer = new StyleLayer(source.SourceLayer);
			styleLayer.FillFrom(source);
			return styleLayer;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A490 File Offset: 0x00008690
		public void FillFrom(StyleLayer source)
		{
			this.Sprite = source.Sprite;
			this.Color = source.Color;
			this.ColorFactor = source.ColorFactor;
			this.AlphaFactor = source.AlphaFactor;
			this.HueFactor = source.HueFactor;
			this.SaturationFactor = source.SaturationFactor;
			this.ValueFactor = source.ValueFactor;
			this.IsHidden = source.IsHidden;
			this.XOffset = source.XOffset;
			this.YOffset = source.YOffset;
			this.ExtendLeft = source.ExtendLeft;
			this.ExtendRight = source.ExtendRight;
			this.ExtendTop = source.ExtendTop;
			this.ExtendBottom = source.ExtendBottom;
			this.OverridenWidth = source.OverridenWidth;
			this.OverridenHeight = source.OverridenHeight;
			this.WidthPolicy = source.WidthPolicy;
			this.HeightPolicy = source.HeightPolicy;
			this.HorizontalFlip = source.HorizontalFlip;
			this.VerticalFlip = source.VerticalFlip;
			this.OverlayMethod = source.OverlayMethod;
			this.OverlaySprite = source.OverlaySprite;
			this.OverlayXOffset = source.OverlayXOffset;
			this.OverlayYOffset = source.OverlayYOffset;
			this.UseRandomBaseOverlayXOffset = source.UseRandomBaseOverlayXOffset;
			this.UseRandomBaseOverlayYOffset = source.UseRandomBaseOverlayYOffset;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public float GetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
				return this.ColorFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.Color:
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
				break;
			case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
				return this.AlphaFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
				return this.HueFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
				return this.SaturationFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
				return this.ValueFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
				return this.OverlayXOffset;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
				return this.OverlayYOffset;
			default:
				switch (propertyType)
				{
				case BrushAnimationProperty.BrushAnimationPropertyType.XOffset:
					return this.XOffset;
				case BrushAnimationProperty.BrushAnimationPropertyType.YOffset:
					return this.YOffset;
				case BrushAnimationProperty.BrushAnimationPropertyType.OverridenWidth:
					return this.OverridenWidth;
				case BrushAnimationProperty.BrushAnimationPropertyType.OverridenHeight:
					return this.OverridenHeight;
				case BrushAnimationProperty.BrushAnimationPropertyType.ExtendLeft:
					return this.ExtendLeft;
				case BrushAnimationProperty.BrushAnimationPropertyType.ExtendRight:
					return this.ExtendRight;
				case BrushAnimationProperty.BrushAnimationPropertyType.ExtendTop:
					return this.ExtendTop;
				case BrushAnimationProperty.BrushAnimationPropertyType.ExtendBottom:
					return this.ExtendBottom;
				}
				break;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\StyleLayer.cs", "GetValueAsFloat", 830);
			return 0f;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A6DB File Offset: 0x000088DB
		public Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Color)
			{
				return this.Color;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\StyleLayer.cs", "GetValueAsColor", 844);
			return Color.Black;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A706 File Offset: 0x00008906
		public Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Sprite)
			{
				return this.Sprite;
			}
			if (propertyType != BrushAnimationProperty.BrushAnimationPropertyType.OverlaySprite)
			{
				Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\StyleLayer.cs", "GetValueAsSprite", 861);
				return null;
			}
			return this.OverlaySprite;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A73C File Offset: 0x0000893C
		public bool GetIsValueChanged(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.Name:
				return false;
			case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
				return this._isColorFactorChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.Color:
				return this._isSpriteChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
				return this._isAlphaFactorChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
				return this._isHueFactorChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
				return this._isSaturationFactorChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
				return this._isValueFactorChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
				return this._isOverlayXOffsetChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
				return this._isOverlayYOffsetChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.Sprite:
				return this._isSpriteChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.IsHidden:
				return this._isIsHiddenChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.XOffset:
				return this._isXOffsetChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.YOffset:
				return this._isYOffsetChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverridenWidth:
				return this._isOverridenWidthChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverridenHeight:
				return this._isOverridenHeightChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.WidthPolicy:
				return this._isWidthPolicyChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.HeightPolicy:
				return this._isHeightPolicyChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.HorizontalFlip:
				return this._isHorizontalFlipChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.VerticalFlip:
				return this._isVerticalFlipChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayMethod:
				return this._isOverlayMethodChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlaySprite:
				return this._isOverlaySpriteChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.ExtendLeft:
				return this._isExtendLeftChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.ExtendRight:
				return this._isExtendRightChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.ExtendTop:
				return this._isExtendTopChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.ExtendBottom:
				return this._isExtendBottomChanged;
			case BrushAnimationProperty.BrushAnimationPropertyType.UseRandomBaseOverlayXOffset:
				return this._isUseRandomBaseOverlayXOffset;
			case BrushAnimationProperty.BrushAnimationPropertyType.UseRandomBaseOverlayYOffset:
				return this._isUseRandomBaseOverlayYOffset;
			}
			return false;
		}

		// Token: 0x040000C3 RID: 195
		private uint _localVersion;

		// Token: 0x040000C4 RID: 196
		private bool _isSpriteChanged;

		// Token: 0x040000C5 RID: 197
		private bool _isColorChanged;

		// Token: 0x040000C6 RID: 198
		private bool _isColorFactorChanged;

		// Token: 0x040000C7 RID: 199
		private bool _isAlphaFactorChanged;

		// Token: 0x040000C8 RID: 200
		private bool _isHueFactorChanged;

		// Token: 0x040000C9 RID: 201
		private bool _isSaturationFactorChanged;

		// Token: 0x040000CA RID: 202
		private bool _isValueFactorChanged;

		// Token: 0x040000CB RID: 203
		private bool _isIsHiddenChanged;

		// Token: 0x040000CC RID: 204
		private bool _isXOffsetChanged;

		// Token: 0x040000CD RID: 205
		private bool _isYOffsetChanged;

		// Token: 0x040000CE RID: 206
		private bool _isExtendLeftChanged;

		// Token: 0x040000CF RID: 207
		private bool _isExtendRightChanged;

		// Token: 0x040000D0 RID: 208
		private bool _isExtendTopChanged;

		// Token: 0x040000D1 RID: 209
		private bool _isExtendBottomChanged;

		// Token: 0x040000D2 RID: 210
		private bool _isOverridenWidthChanged;

		// Token: 0x040000D3 RID: 211
		private bool _isOverridenHeightChanged;

		// Token: 0x040000D4 RID: 212
		private bool _isWidthPolicyChanged;

		// Token: 0x040000D5 RID: 213
		private bool _isHeightPolicyChanged;

		// Token: 0x040000D6 RID: 214
		private bool _isHorizontalFlipChanged;

		// Token: 0x040000D7 RID: 215
		private bool _isVerticalFlipChanged;

		// Token: 0x040000D8 RID: 216
		private bool _isOverlayMethodChanged;

		// Token: 0x040000D9 RID: 217
		private bool _isOverlaySpriteChanged;

		// Token: 0x040000DA RID: 218
		private bool _isUseOverlayAlphaAsMaskChanged;

		// Token: 0x040000DB RID: 219
		private bool _isOverlayXOffsetChanged;

		// Token: 0x040000DC RID: 220
		private bool _isOverlayYOffsetChanged;

		// Token: 0x040000DD RID: 221
		private bool _isUseRandomBaseOverlayXOffset;

		// Token: 0x040000DE RID: 222
		private bool _isUseRandomBaseOverlayYOffset;

		// Token: 0x040000DF RID: 223
		private Sprite _sprite;

		// Token: 0x040000E0 RID: 224
		private Color _color;

		// Token: 0x040000E1 RID: 225
		private float _colorFactor;

		// Token: 0x040000E2 RID: 226
		private float _alphaFactor;

		// Token: 0x040000E3 RID: 227
		private float _hueFactor;

		// Token: 0x040000E4 RID: 228
		private float _saturationFactor;

		// Token: 0x040000E5 RID: 229
		private float _valueFactor;

		// Token: 0x040000E6 RID: 230
		private bool _isHidden;

		// Token: 0x040000E7 RID: 231
		private bool _useOverlayAlphaAsMask;

		// Token: 0x040000E8 RID: 232
		private float _xOffset;

		// Token: 0x040000E9 RID: 233
		private float _yOffset;

		// Token: 0x040000EA RID: 234
		private float _extendLeft;

		// Token: 0x040000EB RID: 235
		private float _extendRight;

		// Token: 0x040000EC RID: 236
		private float _extendTop;

		// Token: 0x040000ED RID: 237
		private float _extendBottom;

		// Token: 0x040000EE RID: 238
		private float _overridenWidth;

		// Token: 0x040000EF RID: 239
		private float _overridenHeight;

		// Token: 0x040000F0 RID: 240
		private BrushLayerSizePolicy _widthPolicy;

		// Token: 0x040000F1 RID: 241
		private BrushLayerSizePolicy _heightPolicy;

		// Token: 0x040000F2 RID: 242
		private bool _horizontalFlip;

		// Token: 0x040000F3 RID: 243
		private bool _verticalFlip;

		// Token: 0x040000F4 RID: 244
		private BrushOverlayMethod _overlayMethod;

		// Token: 0x040000F5 RID: 245
		private Sprite _overlaySprite;

		// Token: 0x040000F6 RID: 246
		private float _overlayXOffset;

		// Token: 0x040000F7 RID: 247
		private float _overlayYOffset;

		// Token: 0x040000F8 RID: 248
		private bool _useRandomBaseOverlayXOffset;

		// Token: 0x040000F9 RID: 249
		private bool _useRandomBaseOverlayYOffset;
	}
}
