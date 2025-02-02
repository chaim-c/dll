using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000011 RID: 17
	public class StyleFontContainer
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004B2D File Offset: 0x00002D2D
		public StyleFontContainer()
		{
			this._styleFonts = new Dictionary<string, StyleFontContainer.FontData>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004B40 File Offset: 0x00002D40
		public void Add(string style, Font font, float fontSize)
		{
			StyleFontContainer.FontData value = new StyleFontContainer.FontData(font, fontSize);
			this._styleFonts.Add(style, value);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004B64 File Offset: 0x00002D64
		public StyleFontContainer.FontData GetFontData(string style)
		{
			StyleFontContainer.FontData result;
			if (this._styleFonts.TryGetValue(style, out result))
			{
				return result;
			}
			StyleFontContainer.FontData result2;
			this._styleFonts.TryGetValue("Default", out result2);
			return result2;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004B97 File Offset: 0x00002D97
		public void ClearFonts()
		{
			this._styleFonts.Clear();
		}

		// Token: 0x0400005A RID: 90
		private readonly Dictionary<string, StyleFontContainer.FontData> _styleFonts;

		// Token: 0x0200003D RID: 61
		public struct FontData
		{
			// Token: 0x0600029D RID: 669 RVA: 0x0000A185 File Offset: 0x00008385
			public FontData(Font font, float fontSize)
			{
				this.Font = font;
				this.FontSize = fontSize;
			}

			// Token: 0x04000154 RID: 340
			public Font Font;

			// Token: 0x04000155 RID: 341
			public float FontSize;
		}
	}
}
