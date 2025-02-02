using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.TwoDimension.BitmapFont;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000012 RID: 18
	public class Text : IText
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004BA4 File Offset: 0x00002DA4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004BAC File Offset: 0x00002DAC
		public ILanguage CurrentLanguage { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004BB5 File Offset: 0x00002DB5
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004BBD File Offset: 0x00002DBD
		public float ScaleToFitTextInLayout { get; private set; } = 1f;

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004BC6 File Offset: 0x00002DC6
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004BCE File Offset: 0x00002DCE
		public int LineCount { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public DrawObject2D DrawObject2D
		{
			get
			{
				if (this._meshNeedsUpdate)
				{
					this.RecalculateTextMesh(1f);
					if (this.ScaleToFitTextInLayout != 1f)
					{
						this.RecalculateTextMesh(this.ScaleToFitTextInLayout);
					}
				}
				return this._drawObject2D;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004C0B File Offset: 0x00002E0B
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00004C13 File Offset: 0x00002E13
		public Font Font
		{
			get
			{
				return this._font;
			}
			set
			{
				if (this._font != value)
				{
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
					this._font = value;
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004C33 File Offset: 0x00002E33
		private float ExtraPaddingHorizontal
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004C3A File Offset: 0x00002E3A
		private float ExtraPaddingVertical
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004C41 File Offset: 0x00002E41
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004C49 File Offset: 0x00002E49
		public TextHorizontalAlignment HorizontalAlignment
		{
			get
			{
				return this._horizontalAlignment;
			}
			set
			{
				if (this._horizontalAlignment != value)
				{
					this._horizontalAlignment = value;
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004C69 File Offset: 0x00002E69
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004C71 File Offset: 0x00002E71
		public TextVerticalAlignment VerticalAlignment
		{
			get
			{
				return this._verticalAlignment;
			}
			set
			{
				if (this._verticalAlignment != value)
				{
					this._verticalAlignment = value;
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004C91 File Offset: 0x00002E91
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004C9A File Offset: 0x00002E9A
		public float FontSize
		{
			get
			{
				return (float)this._fontSize;
			}
			set
			{
				if (this._fontSize != (int)value)
				{
					this._fontSize = (int)value;
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004CBC File Offset: 0x00002EBC
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public string Value
		{
			get
			{
				return this._text;
			}
			set
			{
				string text = value;
				if (text == null)
				{
					text = "";
				}
				if (this._text != text)
				{
					this._text = text;
					this._tokens = TextParser.Parse(text, this.CurrentLanguage);
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004D11 File Offset: 0x00002F11
		private float EmptyCharacterWidth
		{
			get
			{
				return ((float)this.Font.Characters[32].XAdvance + this.ExtraPaddingHorizontal) * this._scaleValue;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004D39 File Offset: 0x00002F39
		private float LineHeight
		{
			get
			{
				return ((float)this.Font.Base + this.ExtraPaddingVertical) * this._scaleValue;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004D55 File Offset: 0x00002F55
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004D5D File Offset: 0x00002F5D
		public bool SkipLineOnContainerExceeded
		{
			get
			{
				return this._skipLineOnContainerExceeded;
			}
			set
			{
				if (value != this._skipLineOnContainerExceeded)
				{
					this._skipLineOnContainerExceeded = value;
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004D80 File Offset: 0x00002F80
		public Text(int width, int height, Font bitmapFont, Func<int, Font> getUsableFontForCharacter)
		{
			this.Font = bitmapFont;
			this._width = width;
			this._height = height;
			this._getUsableFontForCharacter = getUsableFontForCharacter;
			this._meshNeedsUpdate = true;
			this._preferredSizeNeedsUpdate = true;
			this._text = "";
			this._fontSize = 32;
			this._tokens = null;
			this._textMeshGenerator = new TextMeshGenerator();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public Vector2 GetPreferredSize(bool fixedWidth, float widthSize, bool fixedHeight, float heightSize, SpriteData spriteData, float renderScale)
		{
			this._fixedWidth = fixedWidth;
			this._fixedHeight = fixedHeight;
			this._desiredHeight = heightSize;
			this._desiredWidth = widthSize;
			if (this._preferredSizeNeedsUpdate)
			{
				this._preferredSize = new Vector2(0f, 0f);
				if (this._fontSize != 0 && !string.IsNullOrEmpty(this._text))
				{
					this._scaleValue = (float)this._fontSize / (float)this.Font.Size;
					float num = 0f;
					this.LineCount = 1;
					float lineHeight = this.LineHeight;
					float emptyCharacterWidth = this.EmptyCharacterWidth;
					for (int i = 0; i < this._tokens.Count; i++)
					{
						TextToken textToken = this._tokens[i];
						if (textToken.Type == TextToken.TokenType.NewLine)
						{
							int lineCount = this.LineCount;
							this.LineCount = lineCount + 1;
							if (num > this._preferredSize.X)
							{
								this._preferredSize.X = num;
							}
							num = 0f;
						}
						else if (textToken.Type == TextToken.TokenType.EmptyCharacter || textToken.Type == TextToken.TokenType.NonBreakingSpace)
						{
							num += emptyCharacterWidth;
						}
						else if (textToken.Type == TextToken.TokenType.Character)
						{
							char token = textToken.Token;
							float num2 = this.Font.GetCharacterWidth(token, this.ExtraPaddingHorizontal) * this._scaleValue;
							if (fixedWidth && this._skipLineOnContainerExceeded)
							{
								if (num + num2 > widthSize && num > 0f)
								{
									int indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex = TextHelper.GetIndexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex(this._tokens, i, this.CurrentLanguage, true);
									if (indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex == -1)
									{
										int startIndex = Math.Max(0, this._tokens.Count - 2);
										int endIndex = Math.Max(0, this._tokens.Count - 1);
										float totalWordWidthBetweenIndices = TextHelper.GetTotalWordWidthBetweenIndices(startIndex, endIndex, this._tokens, new Func<TextToken, Font>(this.GetFontForTextToken), this.ExtraPaddingHorizontal, this._scaleValue);
										int lineCount = this.LineCount;
										this.LineCount = lineCount + 1;
										num = totalWordWidthBetweenIndices + num2;
									}
									else
									{
										float totalWordWidthBetweenIndices2 = TextHelper.GetTotalWordWidthBetweenIndices(indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex, i, this._tokens, new Func<TextToken, Font>(this.GetFontForTextToken), this.ExtraPaddingHorizontal, this._scaleValue);
										if (num - totalWordWidthBetweenIndices2 > this._preferredSize.X)
										{
											this._preferredSize.X = num - totalWordWidthBetweenIndices2;
										}
										num = totalWordWidthBetweenIndices2 + num2;
										int lineCount = this.LineCount;
										this.LineCount = lineCount + 1;
									}
								}
								else
								{
									num += num2;
								}
							}
							else
							{
								num += num2;
							}
						}
					}
					if (num > this._preferredSize.X)
					{
						this._preferredSize.X = num;
					}
					this._preferredSize.Y = (float)this.LineCount * lineHeight;
				}
				this._preferredSize = new Vector2((float)Math.Ceiling((double)this._preferredSize.X) + 1f, (float)Math.Ceiling((double)this._preferredSize.Y) + 1f);
				this._preferredSizeNeedsUpdate = false;
			}
			return this._preferredSize;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000050D6 File Offset: 0x000032D6
		public void UpdateSize(int width, int height)
		{
			if (this._width != width || this._height != height)
			{
				this._width = width;
				this._height = height;
				this._meshNeedsUpdate = true;
				this._preferredSizeNeedsUpdate = true;
				this.ScaleToFitTextInLayout = 1f;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005111 File Offset: 0x00003311
		private Font GetFontForTextToken(TextToken token)
		{
			return this._getUsableFontForCharacter((int)token.Token);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005124 File Offset: 0x00003324
		private void RecalculateTextMesh(float customScaleToFitText = 1f)
		{
			if (this._fontSize == 0 || string.IsNullOrEmpty(this._text))
			{
				this._drawObject2D = null;
				return;
			}
			int num = this._text.Length;
			this._scaleValue = (float)this._fontSize / (float)this.Font.Size * customScaleToFitText;
			float num2 = 0f;
			float num3 = 0f;
			ref BitmapFontCharacter ptr = this.Font.Characters[32];
			float num4 = ((float)this.Font.Base + this.ExtraPaddingVertical) * this._scaleValue;
			float num5 = ((float)ptr.XAdvance + this.ExtraPaddingHorizontal) * this._scaleValue;
			TextOutput textOutput = new TextOutput(num4);
			for (int i = 0; i < this._tokens.Count; i++)
			{
				TextToken textToken = this._tokens[i];
				if (textToken.Type == TextToken.TokenType.NewLine)
				{
					textOutput.AddNewLine(true, 0f);
					num2 = 0f;
					num3 += num4;
				}
				else if (textToken.Type == TextToken.TokenType.EmptyCharacter || textToken.Type == TextToken.TokenType.NonBreakingSpace)
				{
					textOutput.AddToken(textToken, num5, this._scaleValue, "Default", -1f);
					num2 += num5;
				}
				else if (textToken.Type != TextToken.TokenType.ZeroWidthSpace)
				{
					if (textToken.Type == TextToken.TokenType.WordJoiner)
					{
						textOutput.AddToken(textToken, 0f, this._scaleValue, "Default", -1f);
					}
					else if (textToken.Type == TextToken.TokenType.Character)
					{
						char token = textToken.Token;
						float num6 = this.Font.GetCharacterWidth(token, this.ExtraPaddingHorizontal) * this._scaleValue;
						if (num6 == 0f)
						{
							Font font = this._getUsableFontForCharacter((int)token);
							num6 = ((((font != null) ? new float?(font.GetCharacterWidth(token, this.ExtraPaddingHorizontal)) : null) * this._scaleValue) ?? 0f);
						}
						if (num2 + num6 > (float)this._width && num2 > 0f && this._skipLineOnContainerExceeded)
						{
							float num7 = num3 + num4;
							int height = this._height;
							int indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex = TextHelper.GetIndexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex(this._tokens, i, this.CurrentLanguage, true);
							int num8 = TextHelper.GetIndexOfFirstAppropriateCharacterToMoveToNextLineForwardsFromIndex(this._tokens, i, this.CurrentLanguage, true);
							float num9 = 0f;
							if (indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex != -1)
							{
								if (num8 == -1)
								{
									num8 = this._tokens.Count;
								}
								num9 = TextHelper.GetTotalWordWidthBetweenIndices(indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex, num8, this._tokens, new Func<TextToken, Font>(this.GetFontForTextToken), this.ExtraPaddingHorizontal, this._scaleValue);
							}
							if (((num9 != 0f && (indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex == -1 || num9 > (float)this._width)) || indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex == -1) && textOutput.Tokens.Any<TextTokenOutput>())
							{
								List<TextTokenOutput> list = textOutput.RemoveTokensFromEnd(1);
								float tokenWidth = this.Font.GetCharacterWidth(this.CurrentLanguage.GetLineSeperatorChar(), this.ExtraPaddingHorizontal) * this._scaleValue;
								textOutput.AddToken(TextToken.CreateCharacter(this.CurrentLanguage.GetLineSeperatorChar()), tokenWidth, this._scaleValue, "Default", -1f);
								textOutput.AddNewLine(false, 0f);
								num3 += num4;
								textOutput.AddToken(list[0].Token, list[0].Width, this._scaleValue, "Default", -1f);
								textOutput.AddToken(textToken, num6, this._scaleValue, "Default", -1f);
								num++;
								num2 = num6 + list[0].Width;
							}
							else
							{
								num2 = TextHelper.GetTotalWordWidthBetweenIndices(indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex, i, this._tokens, new Func<TextToken, Font>(this.GetFontForTextToken), this.ExtraPaddingHorizontal, this._scaleValue);
								List<TextTokenOutput> list2 = textOutput.RemoveTokensFromEnd(i - indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex);
								textOutput.AddNewLine(false, 0f);
								num3 += num4;
								for (int j = list2.Count - 1; j >= 0; j--)
								{
									TextTokenOutput textTokenOutput = list2[j];
									if (textTokenOutput.Token.Type != TextToken.TokenType.EmptyCharacter && textTokenOutput.Token.Type != TextToken.TokenType.ZeroWidthSpace)
									{
										textOutput.AddToken(textTokenOutput.Token, textTokenOutput.Width, this._scaleValue, "Default", -1f);
									}
								}
								textOutput.AddToken(textToken, num6, this._scaleValue, "Default", -1f);
								num2 += num6;
							}
						}
						else
						{
							textOutput.AddToken(textToken, num6, this._scaleValue, "Default", -1f);
							num2 += num6;
						}
					}
				}
			}
			this._textMeshGenerator.Refresh(this.Font, num, this._scaleValue);
			num2 = 0f;
			num3 = 0f;
			for (int k = 0; k < textOutput.LineCount; k++)
			{
				TextLineOutput line = textOutput.GetLine(k);
				float num10 = num5;
				switch (this._horizontalAlignment)
				{
				case TextHorizontalAlignment.Right:
					num2 = (float)this._width - line.Width;
					break;
				case TextHorizontalAlignment.Center:
				{
					float num11 = 0f;
					if (!line.LineEnded)
					{
						int num12 = 1;
						while (num12 < line.TokenCount && line.GetToken(line.TokenCount - num12).Type == TextToken.TokenType.EmptyCharacter)
						{
							num11 += num5;
							num12++;
						}
						num12 = 0;
						while (num12 < line.TokenCount && line.GetToken(num12).Type == TextToken.TokenType.EmptyCharacter)
						{
							num11 += num5;
							num12++;
						}
					}
					num2 = ((float)this._width - (line.Width - num11)) * 0.5f;
					break;
				}
				case TextHorizontalAlignment.Justify:
				{
					float num13 = (float)this._width - line.TextWidth;
					if (!line.LineEnded)
					{
						int num14 = line.EmptyCharacterCount;
						int num15 = 1;
						while (line.GetToken(line.TokenCount - num15).Type == TextToken.TokenType.EmptyCharacter)
						{
							num14--;
							num15++;
						}
						num15 = 0;
						while (line.GetToken(num15).Type == TextToken.TokenType.EmptyCharacter)
						{
							num14--;
							num15++;
						}
						num10 = num13 / (float)num14;
					}
					break;
				}
				}
				for (int l = 0; l < line.TokenCount; l++)
				{
					Font font2 = this.Font;
					TextToken token2 = line.GetToken(l);
					TextToken.TokenType type = token2.Type;
					if (type != TextToken.TokenType.EmptyCharacter && type != TextToken.TokenType.NonBreakingSpace)
					{
						if (type == TextToken.TokenType.Character)
						{
							int key = (int)token2.Token;
							if (!this.Font.Characters.ContainsKey(key))
							{
								key = 0;
							}
							BitmapFontCharacter bitmapFontCharacter = font2.Characters[key];
							float x = num2 + (float)bitmapFontCharacter.XOffset * this._scaleValue;
							float y = num3 + (float)bitmapFontCharacter.YOffset * this._scaleValue;
							this._textMeshGenerator.AddCharacterToMesh(x, y, bitmapFontCharacter);
							num2 += ((float)bitmapFontCharacter.XAdvance + this.ExtraPaddingHorizontal) * this._scaleValue;
						}
					}
					else
					{
						num2 += num10;
					}
				}
				num2 = 0f;
				num3 += num4;
			}
			if (this._verticalAlignment == TextVerticalAlignment.Center || this._verticalAlignment == TextVerticalAlignment.Bottom)
			{
				float num16;
				if (this._verticalAlignment == TextVerticalAlignment.Center)
				{
					num16 = (float)this._height - num3;
					num16 *= 0.5f;
				}
				else
				{
					num16 = (float)this._height - num3;
				}
				this._textMeshGenerator.AddValueToY(num16);
			}
			this._drawObject2D = this._textMeshGenerator.GenerateMesh();
			this._meshNeedsUpdate = false;
			if (this._fixedHeight && num3 > this._desiredHeight && this._desiredHeight > 1f)
			{
				this.ScaleToFitTextInLayout = this._desiredHeight / num3;
			}
			if (this._fixedWidth && num2 > this._desiredWidth && this._desiredWidth > 1f)
			{
				this.ScaleToFitTextInLayout = Math.Min(this.ScaleToFitTextInLayout, this._desiredWidth / num2);
			}
		}

		// Token: 0x0400005E RID: 94
		private TextHorizontalAlignment _horizontalAlignment;

		// Token: 0x0400005F RID: 95
		private TextVerticalAlignment _verticalAlignment;

		// Token: 0x04000060 RID: 96
		private DrawObject2D _drawObject2D;

		// Token: 0x04000061 RID: 97
		private bool _meshNeedsUpdate;

		// Token: 0x04000062 RID: 98
		private bool _preferredSizeNeedsUpdate;

		// Token: 0x04000063 RID: 99
		private bool _fixedHeight;

		// Token: 0x04000064 RID: 100
		private bool _fixedWidth;

		// Token: 0x04000065 RID: 101
		private float _desiredHeight;

		// Token: 0x04000066 RID: 102
		private float _desiredWidth;

		// Token: 0x04000067 RID: 103
		private Vector2 _preferredSize;

		// Token: 0x04000068 RID: 104
		private string _text;

		// Token: 0x04000069 RID: 105
		private List<TextToken> _tokens;

		// Token: 0x0400006A RID: 106
		private int _fontSize;

		// Token: 0x0400006B RID: 107
		private int _width;

		// Token: 0x0400006C RID: 108
		private int _height;

		// Token: 0x0400006D RID: 109
		private Font _font;

		// Token: 0x0400006E RID: 110
		private float _scaleValue;

		// Token: 0x0400006F RID: 111
		private readonly TextMeshGenerator _textMeshGenerator;

		// Token: 0x04000070 RID: 112
		private readonly Func<int, Font> _getUsableFontForCharacter;

		// Token: 0x04000071 RID: 113
		private bool _skipLineOnContainerExceeded = true;
	}
}
