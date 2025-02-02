using System;
using System.Numerics;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000071 RID: 113
	public class TextWidget : ImageWidget
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001FDD8 File Offset: 0x0001DFD8
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public bool AutoHideIfEmpty { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001FDE9 File Offset: 0x0001DFE9
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0001FDF6 File Offset: 0x0001DFF6
		[Editor(false)]
		public string Text
		{
			get
			{
				return this._text.Value;
			}
			set
			{
				if (this._text.Value != value)
				{
					this.SetText(value);
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001FE14 File Offset: 0x0001E014
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001FE38 File Offset: 0x0001E038
		[Editor(false)]
		public int IntText
		{
			get
			{
				int result;
				if (int.TryParse(this._text.Value, out result))
				{
					return result;
				}
				return -1;
			}
			set
			{
				if (this._text.Value != value.ToString())
				{
					this.SetText(value.ToString());
				}
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001FE60 File Offset: 0x0001E060
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0001FE88 File Offset: 0x0001E088
		[Editor(false)]
		public float FloatText
		{
			get
			{
				float result;
				if (float.TryParse(this._text.Value, out result))
				{
					return result;
				}
				return -1f;
			}
			set
			{
				if (this._text.Value != value.ToString())
				{
					this.SetText(value.ToString());
				}
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
		public TextWidget(UIContext context) : base(context)
		{
			FontFactory fontFactory = context.FontFactory;
			this._text = new Text((int)base.Size.X, (int)base.Size.Y, fontFactory.DefaultFont, new Func<int, Font>(fontFactory.GetUsableFontForCharacter));
			base.LayoutImp = new TextLayout(this._text);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001FF18 File Offset: 0x0001E118
		protected virtual void SetText(string value)
		{
			base.SetMeasureAndLayoutDirty();
			this._text.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
			this._text.Value = value;
			base.OnPropertyChanged(this.FloatText, "FloatText");
			base.OnPropertyChanged(this.IntText, "IntText");
			base.OnPropertyChanged<string>(this.Text, "Text");
			this.RefreshTextParameters();
			if (this.AutoHideIfEmpty)
			{
				base.IsVisible = !string.IsNullOrEmpty(this.Text);
			}
			this.IsTextValueDirty = true;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		protected void RefreshTextParameters()
		{
			float fontSize = (float)base.ReadOnlyBrush.FontSize * base._scaleToUse;
			this._text.HorizontalAlignment = base.ReadOnlyBrush.TextHorizontalAlignment;
			this._text.VerticalAlignment = base.ReadOnlyBrush.TextVerticalAlignment;
			this._text.FontSize = fontSize;
			this._text.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
			if (base.ReadOnlyBrush.Font != null)
			{
				this._text.Font = base.Context.FontFactory.GetMappedFontForLocalization(base.ReadOnlyBrush.Font.Name);
			}
			else
			{
				this._text.Font = base.Context.FontFactory.DefaultFont;
			}
			if (this.IsTextValueDirty)
			{
				int i = 0;
				while (i < this._text.Value.Length)
				{
					if (char.IsLetter(this._text.Value[i]) && !this._text.Font.Characters.ContainsKey((int)this._text.Value[i]))
					{
						Font usableFontForCharacter = base.Context.FontFactory.GetUsableFontForCharacter((int)this._text.Value[i]);
						if (usableFontForCharacter != null)
						{
							this._text.Font = usableFontForCharacter;
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				this.IsTextValueDirty = false;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002011C File Offset: 0x0001E31C
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			base.OnRender(twoDimensionContext, drawContext);
			this.RefreshTextParameters();
			TextMaterial textMaterial = base.BrushRenderer.CreateTextMaterial(drawContext);
			textMaterial.AlphaFactor *= base.Context.ContextAlpha;
			Vector2 cachedGlobalPosition = this._cachedGlobalPosition;
			drawContext.Draw(this._text, textMaterial, cachedGlobalPosition.X, cachedGlobalPosition.Y, base.Size.X, base.Size.Y);
		}

		// Token: 0x04000371 RID: 881
		protected readonly Text _text;

		// Token: 0x04000373 RID: 883
		protected bool IsTextValueDirty = true;
	}
}
