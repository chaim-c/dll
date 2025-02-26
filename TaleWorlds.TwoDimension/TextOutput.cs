﻿using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000016 RID: 22
	internal class TextOutput
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005E5C File Offset: 0x0000405C
		public float TextHeight
		{
			get
			{
				float num = 0f;
				for (int i = 0; i < this.LineCount; i++)
				{
					TextLineOutput line = this.GetLine(i);
					num += line.Height;
				}
				return num;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005E94 File Offset: 0x00004094
		public float TotalLineScale
		{
			get
			{
				float num = 0f;
				for (int i = 0; i < this.LineCount; i++)
				{
					TextLineOutput line = this.GetLine(i);
					num += line.MaxScale;
				}
				return num;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005ECA File Offset: 0x000040CA
		public float LastLineWidth
		{
			get
			{
				return this._tokensWithLines[this._tokensWithLines.Count - 1].Width;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005EE9 File Offset: 0x000040E9
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00005EF1 File Offset: 0x000040F1
		public float MaxLineHeight { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005EFA File Offset: 0x000040FA
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00005F02 File Offset: 0x00004102
		public float MaxLineWidth { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005F0B File Offset: 0x0000410B
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00005F13 File Offset: 0x00004113
		public float MaxLineScale { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005F1C File Offset: 0x0000411C
		public int LineCount
		{
			get
			{
				return this._tokensWithLines.Count;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005F2C File Offset: 0x0000412C
		public TextOutput(float lineHeight)
		{
			this._tokensWithLines = new List<TextLineOutput>();
			this._lineHeight = lineHeight;
			TextLineOutput textLineOutput = new TextLineOutput(this._lineHeight);
			this._tokensWithLines.Add(textLineOutput);
			textLineOutput.LineEnded = true;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005F70 File Offset: 0x00004170
		public TextLineOutput AddNewLine(bool currentLineEnded, float newLineBaseHeight = 0f)
		{
			TextLineOutput textLineOutput = this._tokensWithLines[this._tokensWithLines.Count - 1];
			textLineOutput.LineEnded = currentLineEnded;
			TextLineOutput textLineOutput2 = new TextLineOutput(newLineBaseHeight);
			this._tokensWithLines.Add(textLineOutput2);
			textLineOutput2.LineEnded = true;
			if (textLineOutput.Width > this.MaxLineWidth)
			{
				this.MaxLineWidth = textLineOutput.Width;
			}
			if (textLineOutput.MaxScale > this.MaxLineScale)
			{
				this.MaxLineScale = textLineOutput.MaxScale;
			}
			return textLineOutput2;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005FEC File Offset: 0x000041EC
		public void AddToken(TextToken textToken, float tokenWidth, float scaleValue, string style = "Default", float tokenHeight = -1f)
		{
			TextLineOutput textLineOutput = this._tokensWithLines[this._tokensWithLines.Count - 1];
			textLineOutput.AddToken(textToken, tokenWidth, tokenHeight, style, scaleValue);
			if (tokenHeight > this.MaxLineHeight)
			{
				this.MaxLineHeight = tokenHeight;
			}
			if (textLineOutput.Width > this.MaxLineWidth)
			{
				this.MaxLineWidth = textLineOutput.Width;
			}
			if (textLineOutput.MaxScale > this.MaxLineScale)
			{
				this.MaxLineScale = textLineOutput.MaxScale;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006068 File Offset: 0x00004268
		public List<TextTokenOutput> RemoveTokensFromEnd(int numberOfTokensToRemove)
		{
			List<TextTokenOutput> list = new List<TextTokenOutput>();
			for (int i = 0; i < numberOfTokensToRemove; i++)
			{
				if (this._tokensWithLines[this._tokensWithLines.Count - 1].TokenCount > 0)
				{
					TextLineOutput textLineOutput = this._tokensWithLines[this._tokensWithLines.Count - 1];
					list.Add(textLineOutput.RemoveTokenFromEnd());
				}
				else
				{
					this._tokensWithLines.RemoveAt(this._tokensWithLines.Count - 1);
					TextLineOutput textLineOutput2 = this._tokensWithLines[this._tokensWithLines.Count - 1];
					list.Add(textLineOutput2.RemoveTokenFromEnd());
				}
			}
			return list;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006112 File Offset: 0x00004312
		public TextLineOutput GetLine(int i)
		{
			return this._tokensWithLines[i];
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00006120 File Offset: 0x00004320
		public IEnumerable<TextTokenOutput> Tokens
		{
			get
			{
				int num;
				for (int i = 0; i < this._tokensWithLines.Count; i = num + 1)
				{
					TextLineOutput tokensWithLine = this._tokensWithLines[i];
					for (int j = 0; j < tokensWithLine.TokenCount; j = num + 1)
					{
						yield return tokensWithLine.GetTokenOutput(j);
						num = j;
					}
					tokensWithLine = null;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006130 File Offset: 0x00004330
		public IEnumerable<TextTokenOutput> TokensWithNewLines
		{
			get
			{
				int num;
				for (int i = 0; i < this._tokensWithLines.Count; i = num + 1)
				{
					TextLineOutput tokensWithLine = this._tokensWithLines[i];
					for (int j = 0; j < tokensWithLine.TokenCount; j = num + 1)
					{
						yield return tokensWithLine.GetTokenOutput(j);
						num = j;
					}
					if (i < this._tokensWithLines.Count - 1)
					{
						yield return new TextTokenOutput(TextToken.CreateNewLine(), 0f, 0f, string.Empty, 0f);
					}
					tokensWithLine = null;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006140 File Offset: 0x00004340
		public void Clear()
		{
			this.MaxLineHeight = 0f;
			this.MaxLineWidth = 0f;
			this.MaxLineScale = 0f;
			this._tokensWithLines.Clear();
			TextLineOutput textLineOutput = new TextLineOutput(this._lineHeight);
			this._tokensWithLines.Add(textLineOutput);
			textLineOutput.LineEnded = true;
		}

		// Token: 0x04000087 RID: 135
		private List<TextLineOutput> _tokensWithLines;

		// Token: 0x04000088 RID: 136
		private readonly float _lineHeight;
	}
}
