using System;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200000C RID: 12
	public class CharacterTableauWidget : TextureWidget
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002B99 File Offset: 0x00000D99
		public CharacterTableauWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "CharacterTableauTextureProvider";
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BAD File Offset: 0x00000DAD
		protected override void OnMousePressed()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", true);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BC0 File Offset: 0x00000DC0
		protected override void OnMouseReleased()
		{
			base.SetTextureProviderProperty("CurrentlyRotating", false);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002BD3 File Offset: 0x00000DD3
		private void OnSwapClick(Widget obj)
		{
			this._isCharacterMountSwapped = !this._isCharacterMountSwapped;
			base.SetTextureProviderProperty("TriggerCharacterMountPlacesSwap", this._isCharacterMountSwapped);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BFC File Offset: 0x00000DFC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if ((this.LeftHandWieldedEquipmentIndex != -1 || this.RightHandWieldedEquipmentIndex != -1) && !base.IsRecursivelyVisible())
			{
				this.LeftHandWieldedEquipmentIndex = -1;
				this.RightHandWieldedEquipmentIndex = -1;
			}
			if (this.IsPlayingCustomAnimations && base.TextureProvider != null && !(bool)base.GetTextureProviderProperty("IsPlayingCustomAnimations"))
			{
				this.IsPlayingCustomAnimations = false;
			}
			if (base.TextureProvider != null)
			{
				this.CustomAnimationProgressRatio = (float)base.GetTextureProviderProperty("CustomAnimationProgressRatio");
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C80 File Offset: 0x00000E80
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			this._isRenderRequestedPreviousFrame = true;
			if (base.TextureProvider != null)
			{
				base.Texture = base.TextureProvider.GetTexture(twoDimensionContext, string.Empty);
				SimpleMaterial simpleMaterial = drawContext.CreateSimpleMaterial();
				Brush readOnlyBrush = base.ReadOnlyBrush;
				StyleLayer styleLayer;
				if (readOnlyBrush == null)
				{
					styleLayer = null;
				}
				else
				{
					StyleLayer[] layers = readOnlyBrush.GetStyleOrDefault(base.CurrentState).GetLayers();
					styleLayer = ((layers != null) ? layers.FirstOrDefault<StyleLayer>() : null);
				}
				StyleLayer styleLayer2 = styleLayer ?? null;
				simpleMaterial.OverlayEnabled = false;
				simpleMaterial.CircularMaskingEnabled = false;
				simpleMaterial.Texture = base.Texture;
				simpleMaterial.AlphaFactor = ((styleLayer2 != null) ? styleLayer2.AlphaFactor : 1f) * base.ReadOnlyBrush.GlobalAlphaFactor * base.Context.ContextAlpha;
				simpleMaterial.ColorFactor = ((styleLayer2 != null) ? styleLayer2.ColorFactor : 1f) * base.ReadOnlyBrush.GlobalColorFactor;
				simpleMaterial.HueFactor = ((styleLayer2 != null) ? styleLayer2.HueFactor : 0f);
				simpleMaterial.SaturationFactor = ((styleLayer2 != null) ? styleLayer2.SaturationFactor : 0f);
				simpleMaterial.ValueFactor = ((styleLayer2 != null) ? styleLayer2.ValueFactor : 0f);
				simpleMaterial.Color = ((styleLayer2 != null) ? styleLayer2.Color : Color.White) * base.ReadOnlyBrush.GlobalColor;
				Vector2 globalPosition = base.GlobalPosition;
				float x = globalPosition.X;
				float y = globalPosition.Y;
				Vector2 size = base.Size;
				Vector2 size2 = base.Size;
				DrawObject2D drawObject2D = null;
				if (this._cachedQuad != null && this._cachedQuadSize == base.Size)
				{
					drawObject2D = this._cachedQuad;
				}
				if (drawObject2D == null)
				{
					drawObject2D = DrawObject2D.CreateQuad(base.Size);
					this._cachedQuad = drawObject2D;
					this._cachedQuadSize = base.Size;
				}
				if (drawContext.CircularMaskEnabled)
				{
					simpleMaterial.CircularMaskingEnabled = true;
					simpleMaterial.CircularMaskingCenter = drawContext.CircularMaskCenter;
					simpleMaterial.CircularMaskingRadius = drawContext.CircularMaskRadius;
					simpleMaterial.CircularMaskingSmoothingRadius = drawContext.CircularMaskSmoothingRadius;
				}
				drawContext.Draw(x, y, simpleMaterial, drawObject2D, base.Size.X, base.Size.Y);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002E7E File Offset: 0x0000107E
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002E86 File Offset: 0x00001086
		[Editor(false)]
		public string BannerCodeText
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				if (value != this._bannerCode)
				{
					this._bannerCode = value;
					base.OnPropertyChanged<string>(value, "BannerCodeText");
					base.SetTextureProviderProperty("BannerCodeText", value);
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002EB5 File Offset: 0x000010B5
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002EBD File Offset: 0x000010BD
		[Editor(false)]
		public ButtonWidget SwapPlacesButtonWidget
		{
			get
			{
				return this._swapPlacesButtonWidget;
			}
			set
			{
				if (value != this._swapPlacesButtonWidget)
				{
					this._swapPlacesButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "SwapPlacesButtonWidget");
					if (value != null)
					{
						this._swapPlacesButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnSwapClick));
					}
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EFA File Offset: 0x000010FA
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002F02 File Offset: 0x00001102
		[Editor(false)]
		public string BodyProperties
		{
			get
			{
				return this._bodyProperties;
			}
			set
			{
				if (value != this._bodyProperties)
				{
					this._bodyProperties = value;
					base.OnPropertyChanged<string>(value, "BodyProperties");
					base.SetTextureProviderProperty("BodyProperties", value);
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F31 File Offset: 0x00001131
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002F39 File Offset: 0x00001139
		[Editor(false)]
		public float CustomAnimationProgressRatio
		{
			get
			{
				return this._customAnimationProgressRatio;
			}
			set
			{
				if (value != this._customAnimationProgressRatio)
				{
					this._customAnimationProgressRatio = value;
					base.OnPropertyChanged(value, "CustomAnimationProgressRatio");
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002F57 File Offset: 0x00001157
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002F5F File Offset: 0x0000115F
		[Editor(false)]
		public float CustomRenderScale
		{
			get
			{
				return this._customRenderScale;
			}
			set
			{
				if (value != this._customRenderScale)
				{
					this._customRenderScale = value;
					base.OnPropertyChanged(value, "CustomRenderScale");
					base.SetTextureProviderProperty("CustomRenderScale", value);
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002F8E File Offset: 0x0000118E
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F96 File Offset: 0x00001196
		[Editor(false)]
		public float CustomAnimationWaitDuration
		{
			get
			{
				return this._customAnimationWaitDuration;
			}
			set
			{
				if (value != this._customAnimationWaitDuration)
				{
					this._customAnimationWaitDuration = value;
					base.OnPropertyChanged(value, "CustomAnimationWaitDuration");
					base.SetTextureProviderProperty("CustomAnimationWaitDuration", value);
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002FC5 File Offset: 0x000011C5
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002FCD File Offset: 0x000011CD
		[Editor(false)]
		public string CharStringId
		{
			get
			{
				return this._charStringId;
			}
			set
			{
				if (value != this._charStringId)
				{
					this._charStringId = value;
					base.OnPropertyChanged<string>(value, "CharStringId");
					base.SetTextureProviderProperty("CharStringId", value);
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002FFC File Offset: 0x000011FC
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003004 File Offset: 0x00001204
		[Editor(false)]
		public int StanceIndex
		{
			get
			{
				return this._stanceIndex;
			}
			set
			{
				if (value != this._stanceIndex)
				{
					this._stanceIndex = value;
					base.OnPropertyChanged(value, "StanceIndex");
					base.SetTextureProviderProperty("StanceIndex", value);
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003033 File Offset: 0x00001233
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000303B File Offset: 0x0000123B
		[Editor(false)]
		public bool IsEquipmentAnimActive
		{
			get
			{
				return this._isEquipmentAnimActive;
			}
			set
			{
				if (value != this._isEquipmentAnimActive)
				{
					this._isEquipmentAnimActive = value;
					base.OnPropertyChanged(value, "IsEquipmentAnimActive");
					base.SetTextureProviderProperty("IsEquipmentAnimActive", value);
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000306A File Offset: 0x0000126A
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003072 File Offset: 0x00001272
		[Editor(false)]
		public bool IsFemale
		{
			get
			{
				return this._isFemale;
			}
			set
			{
				if (value != this._isFemale)
				{
					this._isFemale = value;
					base.OnPropertyChanged(value, "IsFemale");
					base.SetTextureProviderProperty("IsFemale", value);
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030A1 File Offset: 0x000012A1
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000030A9 File Offset: 0x000012A9
		[Editor(false)]
		public int Race
		{
			get
			{
				return this._race;
			}
			set
			{
				if (value != this._race)
				{
					this._race = value;
					base.OnPropertyChanged(value, "Race");
					base.SetTextureProviderProperty("Race", value);
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030D8 File Offset: 0x000012D8
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000030E0 File Offset: 0x000012E0
		[Editor(false)]
		public string EquipmentCode
		{
			get
			{
				return this._equipmentCode;
			}
			set
			{
				if (value != this._equipmentCode)
				{
					this._equipmentCode = value;
					base.OnPropertyChanged<string>(value, "EquipmentCode");
					base.SetTextureProviderProperty("EquipmentCode", value);
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000310F File Offset: 0x0000130F
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003117 File Offset: 0x00001317
		[Editor(false)]
		public string MountCreationKey
		{
			get
			{
				return this._mountCreationKey;
			}
			set
			{
				if (value != this._mountCreationKey)
				{
					this._mountCreationKey = value;
					base.OnPropertyChanged<string>(value, "MountCreationKey");
					base.SetTextureProviderProperty("MountCreationKey", value);
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003146 File Offset: 0x00001346
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000314E File Offset: 0x0000134E
		[Editor(false)]
		public string IdleAction
		{
			get
			{
				return this._idleAction;
			}
			set
			{
				if (value != this._idleAction)
				{
					this._idleAction = value;
					base.OnPropertyChanged<string>(value, "IdleAction");
					base.SetTextureProviderProperty("IdleAction", value);
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000317D File Offset: 0x0000137D
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003185 File Offset: 0x00001385
		[Editor(false)]
		public string IdleFaceAnim
		{
			get
			{
				return this._idleFaceAnim;
			}
			set
			{
				if (value != this._idleFaceAnim)
				{
					this._idleFaceAnim = value;
					base.OnPropertyChanged<string>(value, "IdleFaceAnim");
					base.SetTextureProviderProperty("IdleFaceAnim", value);
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000031B4 File Offset: 0x000013B4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000031BC File Offset: 0x000013BC
		[Editor(false)]
		public string CustomAnimation
		{
			get
			{
				return this._customAnimation;
			}
			set
			{
				if (value != this._customAnimation)
				{
					this._customAnimation = value;
					base.OnPropertyChanged<string>(value, "CustomAnimation");
					base.SetTextureProviderProperty("CustomAnimation", value);
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000031EB File Offset: 0x000013EB
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000031F3 File Offset: 0x000013F3
		[Editor(false)]
		public int LeftHandWieldedEquipmentIndex
		{
			get
			{
				return this._leftHandWieldedEquipmentIndex;
			}
			set
			{
				if (value != this._leftHandWieldedEquipmentIndex)
				{
					this._leftHandWieldedEquipmentIndex = value;
					base.OnPropertyChanged(value, "LeftHandWieldedEquipmentIndex");
					base.SetTextureProviderProperty("LeftHandWieldedEquipmentIndex", value);
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003222 File Offset: 0x00001422
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000322A File Offset: 0x0000142A
		[Editor(false)]
		public int RightHandWieldedEquipmentIndex
		{
			get
			{
				return this._rightHandWieldedEquipmentIndex;
			}
			set
			{
				if (value != this._rightHandWieldedEquipmentIndex)
				{
					this._rightHandWieldedEquipmentIndex = value;
					base.OnPropertyChanged(value, "RightHandWieldedEquipmentIndex");
					base.SetTextureProviderProperty("RightHandWieldedEquipmentIndex", value);
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003259 File Offset: 0x00001459
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003261 File Offset: 0x00001461
		[Editor(false)]
		public uint ArmorColor1
		{
			get
			{
				return this._armorColor1;
			}
			set
			{
				if (value != this._armorColor1)
				{
					this._armorColor1 = value;
					base.OnPropertyChanged(value, "ArmorColor1");
					base.SetTextureProviderProperty("ArmorColor1", value);
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003290 File Offset: 0x00001490
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003298 File Offset: 0x00001498
		[Editor(false)]
		public uint ArmorColor2
		{
			get
			{
				return this._armorColor2;
			}
			set
			{
				if (value != this._armorColor2)
				{
					this._armorColor2 = value;
					base.OnPropertyChanged(value, "ArmorColor2");
					base.SetTextureProviderProperty("ArmorColor2", value);
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000032C7 File Offset: 0x000014C7
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000032CF File Offset: 0x000014CF
		[Editor(false)]
		public bool IsBannerShownInBackground
		{
			get
			{
				return this._isBannerShownInBackground;
			}
			set
			{
				if (value != this._isBannerShownInBackground)
				{
					this._isBannerShownInBackground = value;
					base.OnPropertyChanged(value, "IsBannerShownInBackground");
					base.SetTextureProviderProperty("IsBannerShownInBackground", value);
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000032FE File Offset: 0x000014FE
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003306 File Offset: 0x00001506
		[Editor(false)]
		public bool IsPlayingCustomAnimations
		{
			get
			{
				return this._isPlayingCustomAnimations;
			}
			set
			{
				if (value != this._isPlayingCustomAnimations)
				{
					this._isPlayingCustomAnimations = value;
					base.OnPropertyChanged(value, "IsPlayingCustomAnimations");
					base.SetTextureProviderProperty("IsPlayingCustomAnimations", value);
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003335 File Offset: 0x00001535
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000333D File Offset: 0x0000153D
		[Editor(false)]
		public bool ShouldLoopCustomAnimation
		{
			get
			{
				return this._shouldLoopCustomAnimation;
			}
			set
			{
				if (value != this._shouldLoopCustomAnimation)
				{
					this._shouldLoopCustomAnimation = value;
					base.OnPropertyChanged(value, "ShouldLoopCustomAnimation");
					base.SetTextureProviderProperty("ShouldLoopCustomAnimation", value);
				}
			}
		}

		// Token: 0x0400001F RID: 31
		private ButtonWidget _swapPlacesButtonWidget;

		// Token: 0x04000020 RID: 32
		private string _bannerCode;

		// Token: 0x04000021 RID: 33
		private string _bodyProperties;

		// Token: 0x04000022 RID: 34
		private string _charStringId;

		// Token: 0x04000023 RID: 35
		private string _equipmentCode;

		// Token: 0x04000024 RID: 36
		private string _mountCreationKey;

		// Token: 0x04000025 RID: 37
		private string _idleAction;

		// Token: 0x04000026 RID: 38
		private string _idleFaceAnim;

		// Token: 0x04000027 RID: 39
		private string _customAnimation;

		// Token: 0x04000028 RID: 40
		private int _leftHandWieldedEquipmentIndex;

		// Token: 0x04000029 RID: 41
		private int _rightHandWieldedEquipmentIndex;

		// Token: 0x0400002A RID: 42
		private uint _armorColor1;

		// Token: 0x0400002B RID: 43
		private uint _armorColor2;

		// Token: 0x0400002C RID: 44
		private int _stanceIndex;

		// Token: 0x0400002D RID: 45
		private int _race;

		// Token: 0x0400002E RID: 46
		private bool _isEquipmentAnimActive;

		// Token: 0x0400002F RID: 47
		private bool _isFemale;

		// Token: 0x04000030 RID: 48
		private bool _isCharacterMountSwapped;

		// Token: 0x04000031 RID: 49
		private bool _isBannerShownInBackground;

		// Token: 0x04000032 RID: 50
		private bool _isPlayingCustomAnimations;

		// Token: 0x04000033 RID: 51
		private bool _shouldLoopCustomAnimation;

		// Token: 0x04000034 RID: 52
		private float _customAnimationProgressRatio;

		// Token: 0x04000035 RID: 53
		private float _customRenderScale;

		// Token: 0x04000036 RID: 54
		private float _customAnimationWaitDuration;
	}
}
