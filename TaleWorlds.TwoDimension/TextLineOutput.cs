using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000014 RID: 20
	internal class TextLineOutput
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005918 File Offset: 0x00003B18
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005920 File Offset: 0x00003B20
		public float Width { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005929 File Offset: 0x00003B29
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00005931 File Offset: 0x00003B31
		public float TextWidth { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000593A File Offset: 0x00003B3A
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005942 File Offset: 0x00003B42
		public bool LineEnded { get; internal set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000594B File Offset: 0x00003B4B
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005953 File Offset: 0x00003B53
		public int EmptyCharacterCount { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000595C File Offset: 0x00003B5C
		public int TokenCount
		{
			get
			{
				return this._tokens.Count;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005969 File Offset: 0x00003B69
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00005971 File Offset: 0x00003B71
		public float Height { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000597A File Offset: 0x00003B7A
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005982 File Offset: 0x00003B82
		public float MaxScale { get; private set; }

		// Token: 0x060000BA RID: 186 RVA: 0x0000598B File Offset: 0x00003B8B
		public TextLineOutput(float lineHeight)
		{
			this._tokens = new List<TextTokenOutput>();
			this.Height = lineHeight;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000059A8 File Offset: 0x00003BA8
		public void AddToken(TextToken textToken, float tokenWidth, float tokenHeight, string style, float scaleValue)
		{
			if (textToken.Type == TextToken.TokenType.EmptyCharacter)
			{
				int emptyCharacterCount = this.EmptyCharacterCount;
				this.EmptyCharacterCount = emptyCharacterCount + 1;
			}
			else
			{
				this.TextWidth += tokenWidth;
			}
			TextTokenOutput item;
			if (tokenHeight > 0f)
			{
				item = new TextTokenOutput(textToken, tokenWidth, tokenHeight, style, scaleValue);
			}
			else
			{
				item = new TextTokenOutput(textToken, tokenWidth, this.Height, style, scaleValue);
			}
			this._tokens.Add(item);
			this.Width += tokenWidth;
			if (tokenHeight > this.Height)
			{
				this.Height = tokenHeight;
			}
			if (scaleValue > this.MaxScale)
			{
				this.MaxScale = scaleValue;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005A42 File Offset: 0x00003C42
		public TextToken GetToken(int i)
		{
			return this._tokens[i].Token;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005A55 File Offset: 0x00003C55
		public TextTokenOutput GetTokenOutput(int i)
		{
			return this._tokens[i];
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005A64 File Offset: 0x00003C64
		public TextTokenOutput RemoveTokenFromEnd()
		{
			TextTokenOutput textTokenOutput = this._tokens[this._tokens.Count - 1];
			this._tokens.Remove(textTokenOutput);
			this.Width -= textTokenOutput.Width;
			return textTokenOutput;
		}

		// Token: 0x0400007B RID: 123
		private List<TextTokenOutput> _tokens;
	}
}
