using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000067 RID: 103
	public class RichTextWidget : BrushWidget
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001C524 File Offset: 0x0001A724
		private Vector2 LocalMousePosition
		{
			get
			{
				Vector2 mousePosition = base.EventManager.MousePosition;
				Vector2 globalPosition = base.GlobalPosition;
				float x = mousePosition.X - globalPosition.X;
				float y = mousePosition.Y - globalPosition.Y;
				return new Vector2(x, y);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001C565 File Offset: 0x0001A765
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001C56D File Offset: 0x0001A76D
		[Editor(false)]
		public string LinkHoverCursorState
		{
			get
			{
				return this._linkHoverCursorState;
			}
			set
			{
				if (this._linkHoverCursorState != value)
				{
					this._linkHoverCursorState = value;
				}
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001C584 File Offset: 0x0001A784
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0001C594 File Offset: 0x0001A794
		[Editor(false)]
		public string Text
		{
			get
			{
				return this._richText.Value;
			}
			set
			{
				if (this._richText.Value != value)
				{
					this._richText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
					this._richText.Value = value;
					base.OnPropertyChanged<string>(value, "Text");
					base.SetMeasureAndLayoutDirty();
					this.SetText(this._richText.Value);
				}
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001C600 File Offset: 0x0001A800
		public RichTextWidget(UIContext context) : base(context)
		{
			this._fontFactory = context.FontFactory;
			this._textHeight = -1;
			Font defaultFont = base.Context.FontFactory.DefaultFont;
			this._richText = new RichText((int)base.Size.X, (int)base.Size.Y, defaultFont, new Func<int, Font>(this._fontFactory.GetUsableFontForCharacter));
			this._textureMaterialDict = new Dictionary<Texture, SimpleMaterial>();
			this._lastFontBrush = null;
			base.LayoutImp = new TextLayout(this._richText);
			this.CanBreakWords = true;
			base.AddState("Pressed");
			base.AddState("Hovered");
			base.AddState("Disabled");
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		public override void OnBrushChanged()
		{
			base.OnBrushChanged();
			this.UpdateFontData();
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001C6C6 File Offset: 0x0001A8C6
		protected virtual void SetText(string value)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		private void SetRichTextParameters()
		{
			bool flag = false;
			this._richText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
			this.UpdateFontData();
			if (this._richText.HorizontalAlignment != base.ReadOnlyBrush.TextHorizontalAlignment)
			{
				this._richText.HorizontalAlignment = base.ReadOnlyBrush.TextHorizontalAlignment;
				flag = true;
			}
			if (this._richText.VerticalAlignment != base.ReadOnlyBrush.TextVerticalAlignment)
			{
				this._richText.VerticalAlignment = base.ReadOnlyBrush.TextVerticalAlignment;
				flag = true;
			}
			if (this._richText.TextHeight != this._textHeight)
			{
				this._textHeight = this._richText.TextHeight;
				flag = true;
			}
			if (this._richText.CurrentStyle != base.CurrentState && !string.IsNullOrEmpty(base.CurrentState))
			{
				this._richText.CurrentStyle = base.CurrentState;
				flag = true;
			}
			if (flag)
			{
				base.SetMeasureAndLayoutDirty();
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001C7BF File Offset: 0x0001A9BF
		protected override void RefreshState()
		{
			base.RefreshState();
			this.UpdateText();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		private void UpdateText()
		{
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
				return;
			}
			if (base.IsPressed)
			{
				this.SetState("Pressed");
				return;
			}
			if (base.IsHovered)
			{
				this.SetState("Hovered");
				return;
			}
			this.SetState("Default");
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001C824 File Offset: 0x0001AA24
		private void UpdateFontData()
		{
			if (this._lastFontBrush == base.ReadOnlyBrush && this._lastContextScale == base._scaleToUse && this._lastLanguageCode == base.Context.FontFactory.CurrentLangageID)
			{
				return;
			}
			this._richText.StyleFontContainer.ClearFonts();
			foreach (Style style in base.ReadOnlyBrush.Styles)
			{
				Font font;
				if (style.Font != null)
				{
					font = style.Font;
				}
				else if (base.ReadOnlyBrush.Font != null)
				{
					font = base.ReadOnlyBrush.Font;
				}
				else
				{
					font = base.Context.FontFactory.DefaultFont;
				}
				Font mappedFontForLocalization = base.Context.FontFactory.GetMappedFontForLocalization(font.Name);
				this._richText.StyleFontContainer.Add(style.Name, mappedFontForLocalization, (float)style.FontSize * base._scaleToUse);
			}
			this._lastFontBrush = base.ReadOnlyBrush;
			this._lastLanguageCode = base.Context.FontFactory.CurrentLangageID;
			this._lastContextScale = base._scaleToUse;
			this._richText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001C98C File Offset: 0x0001AB8C
		private Font GetFont(Style style = null)
		{
			if (((style != null) ? style.Font : null) != null)
			{
				return base.Context.FontFactory.GetMappedFontForLocalization(style.Font.Name);
			}
			if (base.ReadOnlyBrush.Font != null)
			{
				return base.Context.FontFactory.GetMappedFontForLocalization(base.ReadOnlyBrush.Font.Name);
			}
			return base.Context.FontFactory.DefaultFont;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001CA04 File Offset: 0x0001AC04
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.SetRichTextParameters();
			if (base.Size.X > 0f && base.Size.Y > 0f)
			{
				Vector2 focusPosition = this.LocalMousePosition;
				bool flag = this._mouseState == RichTextWidget.MouseState.Down || this._mouseState == RichTextWidget.MouseState.AlternateDown;
				bool flag2 = this._mouseState == RichTextWidget.MouseState.Up || this._mouseState == RichTextWidget.MouseState.AlternateUp;
				if (flag)
				{
					focusPosition = this._mouseDownPosition;
				}
				RichTextLinkGroup focusedLinkGroup = this._richText.FocusedLinkGroup;
				this._richText.UpdateSize((int)base.Size.X, (int)base.Size.Y);
				if (focusedLinkGroup != null && this.LinkHoverCursorState != null)
				{
					base.Context.ActiveCursorOfContext = (UIContext.MouseCursors)Enum.Parse(typeof(UIContext.MouseCursors), this.LinkHoverCursorState);
				}
				bool isFixedWidth = base.WidthSizePolicy != SizePolicy.CoverChildren || base.MaxWidth != 0f;
				bool isFixedHeight = base.HeightSizePolicy != SizePolicy.CoverChildren || base.MaxHeight != 0f;
				this._richText.Update(base.Context.SpriteData, focusPosition, flag, isFixedWidth, isFixedHeight, base._scaleToUse);
				if (flag2)
				{
					RichTextLinkGroup focusedLinkGroup2 = this._richText.FocusedLinkGroup;
					if (focusedLinkGroup != null && focusedLinkGroup == focusedLinkGroup2)
					{
						string text = focusedLinkGroup.Href;
						string[] array = text.Split(new char[]
						{
							':'
						});
						if (array.Length == 2)
						{
							text = array[1];
						}
						if (this._mouseState == RichTextWidget.MouseState.Up)
						{
							base.EventFired("LinkClick", new object[]
							{
								text
							});
						}
						else if (this._mouseState == RichTextWidget.MouseState.AlternateUp)
						{
							base.EventFired("LinkAlternateClick", new object[]
							{
								text
							});
						}
					}
					this._mouseState = RichTextWidget.MouseState.None;
				}
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			base.OnRender(twoDimensionContext, drawContext);
			if (!string.IsNullOrEmpty(this._richText.Value))
			{
				foreach (RichTextPart richTextPart in this._richText.GetParts())
				{
					DrawObject2D drawObject2D = richTextPart.DrawObject2D;
					if (drawObject2D != null)
					{
						Material material = null;
						Vector2 cachedGlobalPosition = this._cachedGlobalPosition;
						if (richTextPart.Type == RichTextPartType.Text)
						{
							Style styleOrDefault = base.ReadOnlyBrush.GetStyleOrDefault(richTextPart.Style);
							Font defaultFont = richTextPart.DefaultFont;
							float scaleFactor = (float)styleOrDefault.FontSize * base._scaleToUse;
							TextMaterial textMaterial = styleOrDefault.CreateTextMaterial(drawContext);
							textMaterial.ColorFactor *= base.ReadOnlyBrush.GlobalColorFactor;
							textMaterial.AlphaFactor *= base.ReadOnlyBrush.GlobalAlphaFactor * base.Context.ContextAlpha;
							textMaterial.Color *= base.ReadOnlyBrush.GlobalColor;
							textMaterial.Texture = defaultFont.FontSprite.Texture;
							textMaterial.ScaleFactor = scaleFactor;
							textMaterial.SmoothingConstant = defaultFont.SmoothingConstant;
							textMaterial.Smooth = defaultFont.Smooth;
							if (textMaterial.GlowRadius > 0f || textMaterial.Blur > 0f || textMaterial.OutlineAmount > 0f)
							{
								TextMaterial textMaterial2 = styleOrDefault.CreateTextMaterial(drawContext);
								textMaterial2.CopyFrom(textMaterial);
								drawContext.Draw(cachedGlobalPosition.X + this._renderXOffset, cachedGlobalPosition.Y, textMaterial2, drawObject2D, base.Size.X, base.Size.Y);
							}
							textMaterial.GlowRadius = 0f;
							textMaterial.Blur = 0f;
							textMaterial.OutlineAmount = 0f;
							material = textMaterial;
						}
						else if (richTextPart.Type == RichTextPartType.Sprite)
						{
							Sprite sprite = richTextPart.Sprite;
							if (((sprite != null) ? sprite.Texture : null) != null)
							{
								if (!this._textureMaterialDict.ContainsKey(sprite.Texture))
								{
									this._textureMaterialDict[sprite.Texture] = new SimpleMaterial(sprite.Texture);
								}
								SimpleMaterial simpleMaterial = this._textureMaterialDict[sprite.Texture];
								if (simpleMaterial.ColorFactor != base.ReadOnlyBrush.GlobalColorFactor)
								{
									simpleMaterial.ColorFactor = base.ReadOnlyBrush.GlobalColorFactor;
								}
								if (simpleMaterial.AlphaFactor != base.ReadOnlyBrush.GlobalAlphaFactor * base.Context.ContextAlpha)
								{
									simpleMaterial.AlphaFactor = base.ReadOnlyBrush.GlobalAlphaFactor * base.Context.ContextAlpha;
								}
								if (simpleMaterial.Color != base.ReadOnlyBrush.GlobalColor)
								{
									simpleMaterial.Color = base.ReadOnlyBrush.GlobalColor;
								}
								material = simpleMaterial;
							}
						}
						if (material != null)
						{
							drawContext.Draw(cachedGlobalPosition.X + this._renderXOffset, cachedGlobalPosition.Y, material, drawObject2D, base.Size.X, base.Size.Y);
						}
					}
				}
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001CF0C File Offset: 0x0001B10C
		protected internal override void OnMousePressed()
		{
			if (this._mouseState == RichTextWidget.MouseState.None)
			{
				this._mouseDownPosition = this.LocalMousePosition;
				this._mouseState = RichTextWidget.MouseState.Down;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001CF29 File Offset: 0x0001B129
		protected internal override void OnMouseReleased()
		{
			if (this._mouseState == RichTextWidget.MouseState.Down)
			{
				this._mouseState = RichTextWidget.MouseState.Up;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001CF3B File Offset: 0x0001B13B
		protected internal override void OnMouseAlternatePressed()
		{
			if (this._mouseState == RichTextWidget.MouseState.None)
			{
				this._mouseDownPosition = this.LocalMousePosition;
				this._mouseState = RichTextWidget.MouseState.AlternateDown;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001CF58 File Offset: 0x0001B158
		protected internal override void OnMouseAlternateReleased()
		{
			if (this._mouseState == RichTextWidget.MouseState.AlternateDown)
			{
				this._mouseState = RichTextWidget.MouseState.AlternateUp;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001CF6A File Offset: 0x0001B16A
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001CF72 File Offset: 0x0001B172
		public bool CanBreakWords
		{
			get
			{
				return this._canBreakWords;
			}
			set
			{
				if (value != this._canBreakWords)
				{
					this._canBreakWords = value;
					this._richText.CanBreakWords = value;
					base.OnPropertyChanged(value, "CanBreakWords");
				}
			}
		}

		// Token: 0x0400030B RID: 779
		protected readonly RichText _richText;

		// Token: 0x0400030C RID: 780
		private Brush _lastFontBrush;

		// Token: 0x0400030D RID: 781
		private string _lastLanguageCode;

		// Token: 0x0400030E RID: 782
		private float _lastContextScale;

		// Token: 0x0400030F RID: 783
		private FontFactory _fontFactory;

		// Token: 0x04000310 RID: 784
		private RichTextWidget.MouseState _mouseState;

		// Token: 0x04000311 RID: 785
		private Dictionary<Texture, SimpleMaterial> _textureMaterialDict;

		// Token: 0x04000312 RID: 786
		private Vector2 _mouseDownPosition;

		// Token: 0x04000313 RID: 787
		private int _textHeight;

		// Token: 0x04000314 RID: 788
		protected float _renderXOffset;

		// Token: 0x04000315 RID: 789
		private string _linkHoverCursorState;

		// Token: 0x04000316 RID: 790
		private bool _canBreakWords;

		// Token: 0x02000094 RID: 148
		private enum MouseState
		{
			// Token: 0x0400048B RID: 1163
			None,
			// Token: 0x0400048C RID: 1164
			Down,
			// Token: 0x0400048D RID: 1165
			Up,
			// Token: 0x0400048E RID: 1166
			AlternateDown,
			// Token: 0x0400048F RID: 1167
			AlternateUp
		}
	}
}
