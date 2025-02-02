using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000070 RID: 112
	public class TextureWidget : ImageWidget
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x0001F8CE File Offset: 0x0001DACE
		internal static void RecollectProviderTypes()
		{
			TextureWidget._typeCollector.Collect();
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001F8DA File Offset: 0x0001DADA
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001F8E2 File Offset: 0x0001DAE2
		public Widget LoadingIconWidget { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001F8EB File Offset: 0x0001DAEB
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0001F8F3 File Offset: 0x0001DAF3
		public TextureProvider TextureProvider { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001F904 File Offset: 0x0001DB04
		[Editor(false)]
		public string TextureProviderName
		{
			get
			{
				return this._textureProviderName;
			}
			set
			{
				if (this._textureProviderName != value)
				{
					this._textureProviderName = value;
					base.OnPropertyChanged<string>(value, "TextureProviderName");
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001F927 File Offset: 0x0001DB27
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001F92F File Offset: 0x0001DB2F
		public Texture Texture
		{
			get
			{
				return this._texture;
			}
			protected set
			{
				if (value != this._texture)
				{
					this._texture = value;
					this.OnTextureUpdated();
				}
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001F947 File Offset: 0x0001DB47
		static TextureWidget()
		{
			TextureWidget._typeCollector.Collect();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001F95D File Offset: 0x0001DB5D
		public TextureWidget(UIContext context) : base(context)
		{
			this.TextureProviderName = "ResourceTextureProvider";
			this.TextureProvider = null;
			this._textureProviderProperties = new Dictionary<string, object>();
			this._cachedQuad = null;
			this._cachedQuadSize = Vector2.Zero;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001F995 File Offset: 0x0001DB95
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			TextureProvider textureProvider = this.TextureProvider;
			if (textureProvider != null)
			{
				textureProvider.Clear(true);
			}
			this._setForClearNextFrame = true;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001F9B8 File Offset: 0x0001DBB8
		private void SetTextureProviderProperties()
		{
			if (this.TextureProvider != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this._textureProviderProperties)
				{
					this.TextureProvider.SetProperty(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001FA28 File Offset: 0x0001DC28
		protected void SetTextureProviderProperty(string name, object value)
		{
			this._textureProviderProperties[name] = value;
			if (this.TextureProvider != null)
			{
				this.TextureProvider.SetProperty(name, value);
			}
			this.Texture = null;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001FA53 File Offset: 0x0001DC53
		protected object GetTextureProviderProperty(string propertyName)
		{
			TextureProvider textureProvider = this.TextureProvider;
			if (textureProvider == null)
			{
				return null;
			}
			return textureProvider.GetProperty(propertyName);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001FA68 File Offset: 0x0001DC68
		protected void UpdateTextureWidget()
		{
			if (this._isRenderRequestedPreviousFrame)
			{
				if (this.TextureProvider != null)
				{
					if (this._lastWidth != base.Size.X || this._lastHeight != base.Size.Y || this._isTargetSizeDirty)
					{
						int width = MathF.Round(base.Size.X);
						int height = MathF.Round(base.Size.Y);
						this.TextureProvider.SetTargetSize(width, height);
						this._lastWidth = base.Size.X;
						this._lastHeight = base.Size.Y;
						this._isTargetSizeDirty = false;
						return;
					}
				}
				else
				{
					this.TextureProvider = TextureWidget._typeCollector.Instantiate(this.TextureProviderName, Array.Empty<object>());
					this.SetTextureProviderProperties();
				}
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001FB34 File Offset: 0x0001DD34
		protected virtual void OnTextureUpdated()
		{
			bool toShow = this.Texture == null;
			if (this.LoadingIconWidget != null)
			{
				this.LoadingIconWidget.IsVisible = toShow;
				this.LoadingIconWidget.ApplyActionOnAllChildren(delegate(Widget w)
				{
					w.IsVisible = toShow;
				});
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001FB86 File Offset: 0x0001DD86
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.UpdateTextureWidget();
			if (this._isRenderRequestedPreviousFrame)
			{
				TextureProvider textureProvider = this.TextureProvider;
				if (textureProvider != null)
				{
					textureProvider.Tick(dt);
				}
			}
			this._isRenderRequestedPreviousFrame = false;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			this._isRenderRequestedPreviousFrame = true;
			if (this.TextureProvider != null)
			{
				this.Texture = this.TextureProvider.GetTexture(twoDimensionContext, string.Empty);
				SimpleMaterial simpleMaterial = drawContext.CreateSimpleMaterial();
				StyleLayer[] layers = base.ReadOnlyBrush.GetStyleOrDefault(base.CurrentState).GetLayers();
				simpleMaterial.OverlayEnabled = false;
				simpleMaterial.CircularMaskingEnabled = false;
				simpleMaterial.Texture = this.Texture;
				if (layers != null && layers.Length != 0)
				{
					StyleLayer styleLayer = layers[0];
					simpleMaterial.AlphaFactor = styleLayer.AlphaFactor * base.ReadOnlyBrush.GlobalAlphaFactor * base.Context.ContextAlpha;
					simpleMaterial.ColorFactor = styleLayer.ColorFactor * base.ReadOnlyBrush.GlobalColorFactor;
					simpleMaterial.HueFactor = styleLayer.HueFactor;
					simpleMaterial.SaturationFactor = styleLayer.SaturationFactor;
					simpleMaterial.ValueFactor = styleLayer.ValueFactor;
					simpleMaterial.Color = styleLayer.Color * base.ReadOnlyBrush.GlobalColor;
				}
				else
				{
					simpleMaterial.AlphaFactor = base.ReadOnlyBrush.GlobalAlphaFactor * base.Context.ContextAlpha;
					simpleMaterial.ColorFactor = base.ReadOnlyBrush.GlobalColorFactor;
					simpleMaterial.HueFactor = 0f;
					simpleMaterial.SaturationFactor = 0f;
					simpleMaterial.ValueFactor = 0f;
					simpleMaterial.Color = Color.White * base.ReadOnlyBrush.GlobalColor;
				}
				Vector2 globalPosition = base.GlobalPosition;
				float x = globalPosition.X;
				float y = globalPosition.Y;
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

		// Token: 0x04000364 RID: 868
		protected static TypeCollector<TextureProvider> _typeCollector = new TypeCollector<TextureProvider>();

		// Token: 0x04000367 RID: 871
		protected bool _setForClearNextFrame;

		// Token: 0x04000368 RID: 872
		private string _textureProviderName;

		// Token: 0x04000369 RID: 873
		private Texture _texture;

		// Token: 0x0400036A RID: 874
		private float _lastWidth;

		// Token: 0x0400036B RID: 875
		private float _lastHeight;

		// Token: 0x0400036C RID: 876
		protected bool _isTargetSizeDirty;

		// Token: 0x0400036D RID: 877
		private Dictionary<string, object> _textureProviderProperties;

		// Token: 0x0400036E RID: 878
		protected bool _isRenderRequestedPreviousFrame;

		// Token: 0x0400036F RID: 879
		protected DrawObject2D _cachedQuad;

		// Token: 0x04000370 RID: 880
		protected Vector2 _cachedQuadSize;
	}
}
