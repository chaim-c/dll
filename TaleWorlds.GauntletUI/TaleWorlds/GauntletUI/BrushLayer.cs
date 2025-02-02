using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000011 RID: 17
	public class BrushLayer : IBrushLayerData
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000610D File Offset: 0x0000430D
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006115 File Offset: 0x00004315
		public uint Version { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000611E File Offset: 0x0000431E
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006128 File Offset: 0x00004328
		[Editor(false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000615A File Offset: 0x0000435A
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006164 File Offset: 0x00004364
		[Editor(false)]
		public Sprite Sprite
		{
			get
			{
				return this._sprite;
			}
			set
			{
				if (value != this._sprite)
				{
					this._sprite = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006191 File Offset: 0x00004391
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000619C File Offset: 0x0000439C
		[Editor(false)]
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (value != this._color)
				{
					this._color = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000061CE File Offset: 0x000043CE
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000061D8 File Offset: 0x000043D8
		[Editor(false)]
		public float ColorFactor
		{
			get
			{
				return this._colorFactor;
			}
			set
			{
				if (value != this._colorFactor)
				{
					this._colorFactor = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006205 File Offset: 0x00004405
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006210 File Offset: 0x00004410
		[Editor(false)]
		public float AlphaFactor
		{
			get
			{
				return this._alphaFactor;
			}
			set
			{
				if (value != this._alphaFactor)
				{
					this._alphaFactor = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000623D File Offset: 0x0000443D
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00006248 File Offset: 0x00004448
		[Editor(false)]
		public float HueFactor
		{
			get
			{
				return this._hueFactor;
			}
			set
			{
				if (value != this._hueFactor)
				{
					this._hueFactor = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006275 File Offset: 0x00004475
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006280 File Offset: 0x00004480
		[Editor(false)]
		public float SaturationFactor
		{
			get
			{
				return this._saturationFactor;
			}
			set
			{
				if (value != this._saturationFactor)
				{
					this._saturationFactor = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000062AD File Offset: 0x000044AD
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000062B8 File Offset: 0x000044B8
		[Editor(false)]
		public float ValueFactor
		{
			get
			{
				return this._valueFactor;
			}
			set
			{
				if (value != this._valueFactor)
				{
					this._valueFactor = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000062E5 File Offset: 0x000044E5
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000062F0 File Offset: 0x000044F0
		[Editor(false)]
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
			set
			{
				if (value != this._isHidden)
				{
					this._isHidden = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000631D File Offset: 0x0000451D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006328 File Offset: 0x00004528
		[Editor(false)]
		public bool UseOverlayAlphaAsMask
		{
			get
			{
				return this._useOverlayAlphaAsMask;
			}
			set
			{
				if (value != this._useOverlayAlphaAsMask)
				{
					this._useOverlayAlphaAsMask = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006355 File Offset: 0x00004555
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006360 File Offset: 0x00004560
		[Editor(false)]
		public float XOffset
		{
			get
			{
				return this._xOffset;
			}
			set
			{
				if (value != this._xOffset)
				{
					this._xOffset = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000638D File Offset: 0x0000458D
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006398 File Offset: 0x00004598
		[Editor(false)]
		public float YOffset
		{
			get
			{
				return this._yOffset;
			}
			set
			{
				if (value != this._yOffset)
				{
					this._yOffset = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000063C5 File Offset: 0x000045C5
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000063D0 File Offset: 0x000045D0
		[Editor(false)]
		public float ExtendLeft
		{
			get
			{
				return this._extendLeft;
			}
			set
			{
				if (value != this._extendLeft)
				{
					this._extendLeft = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000063FD File Offset: 0x000045FD
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006408 File Offset: 0x00004608
		[Editor(false)]
		public float ExtendRight
		{
			get
			{
				return this._extendRight;
			}
			set
			{
				if (value != this._extendRight)
				{
					this._extendRight = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006435 File Offset: 0x00004635
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006440 File Offset: 0x00004640
		[Editor(false)]
		public float ExtendTop
		{
			get
			{
				return this._extendTop;
			}
			set
			{
				if (value != this._extendTop)
				{
					this._extendTop = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000646D File Offset: 0x0000466D
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006478 File Offset: 0x00004678
		[Editor(false)]
		public float ExtendBottom
		{
			get
			{
				return this._extendBottom;
			}
			set
			{
				if (value != this._extendBottom)
				{
					this._extendBottom = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000064A5 File Offset: 0x000046A5
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000064B0 File Offset: 0x000046B0
		[Editor(false)]
		public float OverridenWidth
		{
			get
			{
				return this._overridenWidth;
			}
			set
			{
				if (value != this._overridenWidth)
				{
					this._overridenWidth = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000064DD File Offset: 0x000046DD
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000064E8 File Offset: 0x000046E8
		[Editor(false)]
		public float OverridenHeight
		{
			get
			{
				return this._overridenHeight;
			}
			set
			{
				if (value != this._overridenHeight)
				{
					this._overridenHeight = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006515 File Offset: 0x00004715
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006520 File Offset: 0x00004720
		[Editor(false)]
		public BrushLayerSizePolicy WidthPolicy
		{
			get
			{
				return this._widthPolicy;
			}
			set
			{
				if (value != this._widthPolicy)
				{
					this._widthPolicy = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000654D File Offset: 0x0000474D
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006558 File Offset: 0x00004758
		[Editor(false)]
		public BrushLayerSizePolicy HeightPolicy
		{
			get
			{
				return this._heightPolicy;
			}
			set
			{
				if (value != this._heightPolicy)
				{
					this._heightPolicy = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006585 File Offset: 0x00004785
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006590 File Offset: 0x00004790
		[Editor(false)]
		public bool HorizontalFlip
		{
			get
			{
				return this._horizontalFlip;
			}
			set
			{
				if (value != this._horizontalFlip)
				{
					this._horizontalFlip = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000065BD File Offset: 0x000047BD
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000065C8 File Offset: 0x000047C8
		[Editor(false)]
		public bool VerticalFlip
		{
			get
			{
				return this._verticalFlip;
			}
			set
			{
				if (value != this._verticalFlip)
				{
					this._verticalFlip = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000065F5 File Offset: 0x000047F5
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006600 File Offset: 0x00004800
		[Editor(false)]
		public BrushOverlayMethod OverlayMethod
		{
			get
			{
				return this._overlayMethod;
			}
			set
			{
				if (value != this._overlayMethod)
				{
					this._overlayMethod = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000662D File Offset: 0x0000482D
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006638 File Offset: 0x00004838
		[Editor(false)]
		public Sprite OverlaySprite
		{
			get
			{
				return this._overlaySprite;
			}
			set
			{
				this._overlaySprite = value;
				uint version = this.Version;
				this.Version = version + 1U;
				if (this._overlaySprite != null)
				{
					if (this.OverlayMethod == BrushOverlayMethod.None)
					{
						this.OverlayMethod = BrushOverlayMethod.CoverWithTexture;
						return;
					}
				}
				else
				{
					this.OverlayMethod = BrushOverlayMethod.None;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000667B File Offset: 0x0000487B
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006684 File Offset: 0x00004884
		[Editor(false)]
		public float OverlayXOffset
		{
			get
			{
				return this._overlayXOffset;
			}
			set
			{
				if (value != this._overlayXOffset)
				{
					this._overlayXOffset = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000066B1 File Offset: 0x000048B1
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000066BC File Offset: 0x000048BC
		[Editor(false)]
		public float OverlayYOffset
		{
			get
			{
				return this._overlayYOffset;
			}
			set
			{
				if (value != this._overlayYOffset)
				{
					this._overlayYOffset = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000066E9 File Offset: 0x000048E9
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000066F4 File Offset: 0x000048F4
		[Editor(false)]
		public bool UseRandomBaseOverlayXOffset
		{
			get
			{
				return this._useRandomBaseOverlayXOffset;
			}
			set
			{
				if (value != this._useRandomBaseOverlayXOffset)
				{
					this._useRandomBaseOverlayXOffset = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006721 File Offset: 0x00004921
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000672C File Offset: 0x0000492C
		[Editor(false)]
		public bool UseRandomBaseOverlayYOffset
		{
			get
			{
				return this._useRandomBaseOverlayYOffset;
			}
			set
			{
				if (value != this._useRandomBaseOverlayYOffset)
				{
					this._useRandomBaseOverlayYOffset = value;
					uint version = this.Version;
					this.Version = version + 1U;
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000675C File Offset: 0x0000495C
		public BrushLayer()
		{
			this.Color = new Color(1f, 1f, 1f, 1f);
			this.ColorFactor = 1f;
			this.AlphaFactor = 1f;
			this.HueFactor = 0f;
			this.SaturationFactor = 0f;
			this.ValueFactor = 0f;
			this.XOffset = 0f;
			this.YOffset = 0f;
			this.IsHidden = false;
			this.WidthPolicy = BrushLayerSizePolicy.StretchToTarget;
			this.HeightPolicy = BrushLayerSizePolicy.StretchToTarget;
			this.HorizontalFlip = false;
			this.VerticalFlip = false;
			this.OverlayMethod = BrushOverlayMethod.None;
			this.ExtendLeft = 0f;
			this.ExtendRight = 0f;
			this.ExtendTop = 0f;
			this.ExtendBottom = 0f;
			this.OverlayXOffset = 0f;
			this.OverlayYOffset = 0f;
			this.UseRandomBaseOverlayXOffset = false;
			this.UseRandomBaseOverlayYOffset = false;
			this.UseOverlayAlphaAsMask = false;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000685C File Offset: 0x00004A5C
		public void FillFrom(BrushLayer brushLayer)
		{
			this.Sprite = brushLayer.Sprite;
			this.Color = brushLayer.Color;
			this.ColorFactor = brushLayer.ColorFactor;
			this.AlphaFactor = brushLayer.AlphaFactor;
			this.HueFactor = brushLayer.HueFactor;
			this.SaturationFactor = brushLayer.SaturationFactor;
			this.ValueFactor = brushLayer.ValueFactor;
			this.XOffset = brushLayer.XOffset;
			this.YOffset = brushLayer.YOffset;
			this.Name = brushLayer.Name;
			this.IsHidden = brushLayer.IsHidden;
			this.WidthPolicy = brushLayer.WidthPolicy;
			this.HeightPolicy = brushLayer.HeightPolicy;
			this.OverridenWidth = brushLayer.OverridenWidth;
			this.OverridenHeight = brushLayer.OverridenHeight;
			this.HorizontalFlip = brushLayer.HorizontalFlip;
			this.VerticalFlip = brushLayer.VerticalFlip;
			this.OverlayMethod = brushLayer.OverlayMethod;
			this.OverlaySprite = brushLayer.OverlaySprite;
			this.ExtendLeft = brushLayer.ExtendLeft;
			this.ExtendRight = brushLayer.ExtendRight;
			this.ExtendTop = brushLayer.ExtendTop;
			this.ExtendBottom = brushLayer.ExtendBottom;
			this.OverlayXOffset = brushLayer.OverlayXOffset;
			this.OverlayYOffset = brushLayer.OverlayYOffset;
			this.UseRandomBaseOverlayXOffset = brushLayer.UseRandomBaseOverlayXOffset;
			this.UseRandomBaseOverlayYOffset = brushLayer.UseRandomBaseOverlayYOffset;
			this.UseOverlayAlphaAsMask = brushLayer.UseOverlayAlphaAsMask;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000069BC File Offset: 0x00004BBC
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
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayer.cs", "GetValueAsFloat", 669);
			return 0f;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006ABF File Offset: 0x00004CBF
		public Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Color)
			{
				return this.Color;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayer.cs", "GetValueAsColor", 683);
			return Color.Black;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006AEA File Offset: 0x00004CEA
		public Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Sprite)
			{
				return this.Sprite;
			}
			if (propertyType != BrushAnimationProperty.BrushAnimationPropertyType.OverlaySprite)
			{
				Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayer.cs", "GetValueAsSprite", 700);
				return null;
			}
			return this.OverlaySprite;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006B20 File Offset: 0x00004D20
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x0400004C RID: 76
		private string _name;

		// Token: 0x0400004D RID: 77
		private Sprite _sprite;

		// Token: 0x0400004E RID: 78
		private Color _color;

		// Token: 0x0400004F RID: 79
		private float _colorFactor;

		// Token: 0x04000050 RID: 80
		private float _alphaFactor;

		// Token: 0x04000051 RID: 81
		private float _hueFactor;

		// Token: 0x04000052 RID: 82
		private float _saturationFactor;

		// Token: 0x04000053 RID: 83
		private float _valueFactor;

		// Token: 0x04000054 RID: 84
		private bool _isHidden;

		// Token: 0x04000055 RID: 85
		private bool _useOverlayAlphaAsMask;

		// Token: 0x04000056 RID: 86
		private float _xOffset;

		// Token: 0x04000057 RID: 87
		private float _yOffset;

		// Token: 0x04000058 RID: 88
		private float _extendLeft;

		// Token: 0x04000059 RID: 89
		private float _extendRight;

		// Token: 0x0400005A RID: 90
		private float _extendTop;

		// Token: 0x0400005B RID: 91
		private float _extendBottom;

		// Token: 0x0400005C RID: 92
		private float _overridenWidth;

		// Token: 0x0400005D RID: 93
		private float _overridenHeight;

		// Token: 0x0400005E RID: 94
		private BrushLayerSizePolicy _widthPolicy;

		// Token: 0x0400005F RID: 95
		private BrushLayerSizePolicy _heightPolicy;

		// Token: 0x04000060 RID: 96
		private bool _horizontalFlip;

		// Token: 0x04000061 RID: 97
		private bool _verticalFlip;

		// Token: 0x04000062 RID: 98
		private BrushOverlayMethod _overlayMethod;

		// Token: 0x04000063 RID: 99
		private Sprite _overlaySprite;

		// Token: 0x04000064 RID: 100
		private float _overlayXOffset;

		// Token: 0x04000065 RID: 101
		private float _overlayYOffset;

		// Token: 0x04000066 RID: 102
		private bool _useRandomBaseOverlayXOffset;

		// Token: 0x04000067 RID: 103
		private bool _useRandomBaseOverlayYOffset;
	}
}
