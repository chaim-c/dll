using System;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000007 RID: 7
	public class BannerTableauWidget : TextureWidget
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		public BannerTableauWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "BannerTableauTextureProvider";
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E0 File Offset: 0x000003E0
		protected override void OnMousePressed()
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E2 File Offset: 0x000003E2
		protected override void OnMouseReleased()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021E4 File Offset: 0x000003E4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021F0 File Offset: 0x000003F0
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000023F6 File Offset: 0x000005F6
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000242D File Offset: 0x0000062D
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000245C File Offset: 0x0000065C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002464 File Offset: 0x00000664
		[Editor(false)]
		public bool IsNineGrid
		{
			get
			{
				return this._isNineGrid;
			}
			set
			{
				if (value != this._isNineGrid)
				{
					this._isNineGrid = value;
					base.OnPropertyChanged(value, "IsNineGrid");
					base.SetTextureProviderProperty("IsNineGrid", value);
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002493 File Offset: 0x00000693
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000249B File Offset: 0x0000069B
		[Editor(false)]
		public Vec2 UpdatePositionValueManual
		{
			get
			{
				return this._updatePositionRef;
			}
			set
			{
				if (value != this._updatePositionRef)
				{
					this._updatePositionRef = value;
					base.OnPropertyChanged(value, "UpdatePositionValueManual");
					base.SetTextureProviderProperty("UpdatePositionValueManual", value);
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024CF File Offset: 0x000006CF
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000024D7 File Offset: 0x000006D7
		[Editor(false)]
		public Vec2 UpdateSizeValueManual
		{
			get
			{
				return this._updateSizeRef;
			}
			set
			{
				if (value != this._updateSizeRef)
				{
					this._updateSizeRef = value;
					base.OnPropertyChanged(value, "UpdateSizeValueManual");
					base.SetTextureProviderProperty("UpdateSizeValueManual", value);
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000250B File Offset: 0x0000070B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002514 File Offset: 0x00000714
		[Editor(false)]
		public ValueTuple<float, bool> UpdateRotationValueManualWithMirror
		{
			get
			{
				return this._updateRotationWithMirrorRef;
			}
			set
			{
				if (value.Item1 != this._updateRotationWithMirrorRef.Item1 || value.Item2 != this._updateRotationWithMirrorRef.Item2)
				{
					this._updateRotationWithMirrorRef = value;
					base.OnPropertyChanged<string>("UpdateRotationValueManualWithMirror", "UpdateRotationValueManualWithMirror");
					base.SetTextureProviderProperty("UpdateRotationValueManualWithMirror", value);
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000256F File Offset: 0x0000076F
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002577 File Offset: 0x00000777
		[Editor(false)]
		public int MeshIndexToUpdate
		{
			get
			{
				return this._meshIndexToUpdate;
			}
			set
			{
				if (value != this._meshIndexToUpdate)
				{
					this._meshIndexToUpdate = value;
					base.OnPropertyChanged(value, "MeshIndexToUpdate");
					base.SetTextureProviderProperty("MeshIndexToUpdate", value);
				}
			}
		}

		// Token: 0x04000004 RID: 4
		private string _bannerCode;

		// Token: 0x04000005 RID: 5
		private float _customRenderScale;

		// Token: 0x04000006 RID: 6
		private bool _isNineGrid;

		// Token: 0x04000007 RID: 7
		private Vec2 _updatePositionRef;

		// Token: 0x04000008 RID: 8
		private Vec2 _updateSizeRef;

		// Token: 0x04000009 RID: 9
		private ValueTuple<float, bool> _updateRotationWithMirrorRef;

		// Token: 0x0400000A RID: 10
		private int _meshIndexToUpdate;
	}
}
