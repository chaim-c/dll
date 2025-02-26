﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension.BitmapFont;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000007 RID: 7
	public class RichText : IText
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002DBB File Offset: 0x00000FBB
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002DC3 File Offset: 0x00000FC3
		internal int Width { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002DD4 File Offset: 0x00000FD4
		internal int Height { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002DDD File Offset: 0x00000FDD
		internal float WidthSize
		{
			get
			{
				if (this._widthSize >= 1E-05f)
				{
					return this._widthSize;
				}
				return (float)this.Width;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002E02 File Offset: 0x00001002
		public string CurrentStyle { get; set; } = "Default";

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002E0B File Offset: 0x0000100B
		public int TextHeight
		{
			get
			{
				if (this.TextOutput == null)
				{
					return -1;
				}
				return (int)this.TextOutput.TextHeight;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002E23 File Offset: 0x00001023
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002E2B File Offset: 0x0000102B
		public StyleFontContainer StyleFontContainer { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002E34 File Offset: 0x00001034
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002E3C File Offset: 0x0000103C
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
					this._positionNeedsUpdate = true;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002E55 File Offset: 0x00001055
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002E5D File Offset: 0x0000105D
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
					this._positionNeedsUpdate = true;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002E76 File Offset: 0x00001076
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002E80 File Offset: 0x00001080
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
					try
					{
						this._tokens = RichTextParser.Parse(text);
					}
					catch (RichTextException ex)
					{
						string message = ex.Message;
						this._tokens = TextToken.CreateTokenArrayFromWord(text);
					}
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002EF0 File Offset: 0x000010F0
		internal TextOutput TextOutput { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002EF9 File Offset: 0x000010F9
		private int _textLength
		{
			get
			{
				return this._text.Length + this._numOfAddedSeparators;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002F0D File Offset: 0x0000110D
		public RichTextLinkGroup FocusedLinkGroup
		{
			get
			{
				return this._focusedLinkGroup;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002F15 File Offset: 0x00001115
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002F1D File Offset: 0x0000111D
		public bool SkipLineOnContainerExceeded
		{
			get
			{
				return this._shouldAddNewLineWhenExceedingContainerWidth;
			}
			set
			{
				if (value != this._shouldAddNewLineWhenExceedingContainerWidth)
				{
					this._shouldAddNewLineWhenExceedingContainerWidth = value;
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002F3D File Offset: 0x0000113D
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002F45 File Offset: 0x00001145
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
					this._meshNeedsUpdate = true;
					this._preferredSizeNeedsUpdate = true;
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F68 File Offset: 0x00001168
		public RichText(int width, int height, Font font, Func<int, Font> getUsableFontForCharacter)
		{
			this.Width = width;
			this.Height = height;
			this._getUsableFontForCharacter = getUsableFontForCharacter;
			this._gotFocus = false;
			this._text = "";
			this._tokens = null;
			this.TextOutput = new TextOutput(0f);
			this._focusedToken = null;
			this._focusedLinkGroup = null;
			this._richTextParts = new List<RichTextPart>();
			this._linkGroups = new List<RichTextLinkGroup>();
			this._styleStack = new Stack<string>();
			this.StyleFontContainer = new StyleFontContainer();
			this.StyleFontContainer.Add("Default", font, 1f);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000304C File Offset: 0x0000124C
		public virtual void Update(SpriteData spriteData, Vector2 focusPosition, bool focus, bool isFixedWidth, bool isFixedHeight, float renderScale)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(this._text))
			{
				if (this._tokensNeedUpdate)
				{
					this.CalculateTextOutput(isFixedWidth, isFixedHeight, this.WidthSize, (float)this.Height, spriteData, renderScale);
					flag = true;
				}
				if (this._gotFocus != focus)
				{
					this._gotFocus = focus;
					flag = true;
				}
				if (this._meshNeedsUpdate)
				{
					this.FindLinkGroups();
					flag = true;
					this._meshNeedsUpdate = false;
				}
				TextTokenOutput tokenUnderPosition = this.GetTokenUnderPosition(focusPosition);
				if (tokenUnderPosition != this._focusedToken)
				{
					this._focusedToken = tokenUnderPosition;
					TextTokenOutput focusedToken = this._focusedToken;
					RichTextLinkGroup richTextLinkGroup = this.FindLinkGroup((focusedToken != null) ? focusedToken.Token : null);
					if (this._focusedLinkGroup != richTextLinkGroup)
					{
						this._focusedLinkGroup = richTextLinkGroup;
						flag = true;
					}
				}
				if (this._positionNeedsUpdate)
				{
					this.PositionTokensInTextOutput(spriteData, renderScale);
					if (!this._positionNeedsUpdate)
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.FillPartsWithTokens(spriteData, renderScale);
					this.GenerateMeshes(renderScale);
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000312A File Offset: 0x0000132A
		public void SetAllDirty()
		{
			this._meshNeedsUpdate = true;
			this._preferredSizeNeedsUpdate = true;
			this._positionNeedsUpdate = true;
			this._tokensNeedUpdate = true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003148 File Offset: 0x00001348
		private float GetEmptyCharacterWidth(Font font, float scaleValue)
		{
			return ((float)font.Characters[32].XAdvance + 0.5f) * scaleValue;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003168 File Offset: 0x00001368
		public Vector2 GetPreferredSize(bool fixedWidth, float widthSize, bool fixedHeight, float heightSize, SpriteData spriteData, float renderScale)
		{
			this._isFixedHeight = fixedHeight;
			this._isFixedWidth = fixedWidth;
			this._widthSize = widthSize;
			if (!string.IsNullOrEmpty(this._text))
			{
				if (this._tokensNeedUpdate)
				{
					this.CalculateTextOutput(fixedWidth, fixedHeight, this.WidthSize, heightSize, spriteData, renderScale);
				}
				if (this._preferredSizeNeedsUpdate)
				{
					TextOutput textOutput = this.TextOutput;
					bool flag;
					if (textOutput == null)
					{
						flag = false;
					}
					else
					{
						IEnumerable<TextTokenOutput> tokens = textOutput.Tokens;
						bool? flag2 = (tokens != null) ? new bool?(tokens.Any<TextTokenOutput>()) : null;
						bool flag3 = true;
						flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
					}
					if (flag)
					{
						Vector2 preferredSize = this._preferredSize;
						float maxLineWidth = this.TextOutput.MaxLineWidth;
						float textHeight = this.TextOutput.TextHeight;
						this._preferredSize = new Vector2((float)Math.Ceiling((double)maxLineWidth), (float)Math.Ceiling((double)textHeight));
						if (preferredSize != this._preferredSize)
						{
							this._meshNeedsUpdate = true;
							this._positionNeedsUpdate = true;
						}
						this._preferredSizeNeedsUpdate = false;
					}
				}
			}
			return this._preferredSize;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003268 File Offset: 0x00001468
		public void CalculateTextOutput(bool fixedWidth, bool fixedHeight, float width, float height, SpriteData spriteData, float renderScale)
		{
			if (this._tokensNeedUpdate)
			{
				this.TextOutput.Clear();
				this._numOfAddedSeparators = 0;
				this._styleStack.Clear();
				this._styleStack.Push(this.CurrentStyle);
				for (int i = 0; i < this._tokens.Count; i++)
				{
					TextToken textToken = this._tokens[i];
					bool flag = i == this._tokens.Count - 1;
					string style = this._styleStack.Peek();
					StyleFontContainer.FontData fontData = this.StyleFontContainer.GetFontData(style);
					Font font = fontData.Font;
					float num = fontData.FontSize / (float)font.Size;
					float num2 = ((float)font.Base + 5f) * num;
					if (textToken.Type == TextToken.TokenType.NewLine)
					{
						this.TextOutput.AddNewLine(true, num2);
					}
					else if (textToken.Type == TextToken.TokenType.EmptyCharacter)
					{
						float emptyCharacterWidth = this.GetEmptyCharacterWidth(font, num);
						bool flag2 = this.TextOutput.LastLineWidth + emptyCharacterWidth > width;
						float num3 = this.TextOutput.TextHeight + num2;
						bool flag3 = !fixedHeight || num3 < height;
						if (fixedWidth && flag2 && flag3 && this.SkipLineOnContainerExceeded)
						{
							this.TextOutput.AddNewLine(false, num2);
						}
						else
						{
							this.TextOutput.AddToken(textToken, emptyCharacterWidth, num, style, num2);
						}
					}
					else if (textToken.Type == TextToken.TokenType.ZeroWidthSpace)
					{
						this.TextOutput.AddToken(textToken, 0f, num, style, num2);
					}
					else if (textToken.Type == TextToken.TokenType.NonBreakingSpace)
					{
						float emptyCharacterWidth2 = this.GetEmptyCharacterWidth(font, num);
						this.TextOutput.AddToken(textToken, emptyCharacterWidth2, num, style, num2);
					}
					else if (textToken.Type == TextToken.TokenType.WordJoiner)
					{
						this.TextOutput.AddToken(textToken, 0f, num, style, num2);
					}
					else if (textToken.Type == TextToken.TokenType.Character)
					{
						char token = textToken.Token;
						float num4;
						if (!font.Characters.ContainsKey((int)token))
						{
							Font font2 = this._getUsableFontForCharacter((int)token) ?? fontData.Font;
							num = fontData.FontSize / (float)font2.Size;
							num4 = font2.GetCharacterWidth(token, 0.5f) * num;
						}
						else
						{
							num = fontData.FontSize / (float)font.Size;
							num4 = font.GetCharacterWidth(token, 0.5f) * num;
						}
						bool flag4 = this.TextOutput.LastLineWidth + num4 > width;
						if (fixedWidth && flag4 && this.SkipLineOnContainerExceeded)
						{
							List<TextToken> list = (from t in this.TextOutput.Tokens
							select t.Token).ToList<TextToken>();
							int indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex = TextHelper.GetIndexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex(list, list.Count - 1, this.CurrentLanguage, this.CanBreakWords);
							int num5 = TextHelper.GetIndexOfFirstAppropriateCharacterToMoveToNextLineForwardsFromIndex(this._tokens, i, this.CurrentLanguage, this.CanBreakWords);
							float num6 = 0f;
							if (indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex != -1)
							{
								if (num5 == -1)
								{
									num5 = this._tokens.Count;
								}
								num6 = TextHelper.GetTotalWordWidthBetweenIndices(indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex, num5, this._tokens, new Func<TextToken, Font>(this.GetFontForTextToken), 0.5f, num);
							}
							bool flag5 = num4 <= width;
							if (!flag5 || (num6 != 0f && (indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex == -1 || num6 > width)) || indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex == -1)
							{
								this._numOfAddedSeparators++;
								float tokenWidth = fontData.Font.GetCharacterWidth(this.CurrentLanguage.GetLineSeperatorChar(), 0.5f) * num;
								if (!flag5)
								{
									this.TextOutput.AddToken(textToken, num4, num, style, num2);
									if (!flag)
									{
										this.TextOutput.AddToken(TextToken.CreateCharacter(this.CurrentLanguage.GetLineSeperatorChar()), tokenWidth, num, "Default", -1f);
										this.TextOutput.AddNewLine(false, num2);
									}
								}
								else if (this.TextOutput.Tokens.Any<TextTokenOutput>())
								{
									List<TextTokenOutput> list2 = this.TextOutput.RemoveTokensFromEnd(1);
									this.TextOutput.AddToken(TextToken.CreateCharacter(this.CurrentLanguage.GetLineSeperatorChar()), tokenWidth, num, "Default", -1f);
									this.TextOutput.AddNewLine(false, num2);
									this.TextOutput.AddToken(list2[0].Token, list2[0].Width, num, "Default", -1f);
									this.TextOutput.AddToken(textToken, num4, num, style, num2);
								}
							}
							else
							{
								List<TextTokenOutput> list3 = this.TextOutput.RemoveTokensFromEnd(list.Count - indexOfFirstAppropriateCharacterToMoveToNextLineBackwardsFromIndex);
								this.TextOutput.AddNewLine(false, num2);
								for (int j = list3.Count - 1; j >= 0; j--)
								{
									TextTokenOutput textTokenOutput = list3[j];
									if (textTokenOutput.Token.Type != TextToken.TokenType.EmptyCharacter && textTokenOutput.Token.Type != TextToken.TokenType.ZeroWidthSpace)
									{
										this.TextOutput.AddToken(textTokenOutput.Token, textTokenOutput.Width, textTokenOutput.Scale, textTokenOutput.Style, num2);
									}
								}
								this.TextOutput.AddToken(textToken, num4, num, style, num2);
							}
						}
						else
						{
							this.TextOutput.AddToken(textToken, num4, num, style, num2);
						}
					}
					else if (textToken.Type == TextToken.TokenType.Tag)
					{
						RichTextTag tag = textToken.Tag;
						if (tag.Name == "img")
						{
							string attribute = tag.GetAttribute("src");
							Sprite sprite = null;
							if (!string.IsNullOrEmpty(attribute))
							{
								sprite = spriteData.GetSprite(attribute);
							}
							float num7 = 0f;
							if (sprite != null)
							{
								num7 = ((float)fontData.Font.Base + 0f) * num * ((float)sprite.Height / (float)sprite.Width) + 8f * renderScale;
							}
							bool flag6 = this.TextOutput.LastLineWidth + num7 > width;
							bool flag7 = this.TextOutput.TextHeight + num2 < height;
							if (fixedWidth && flag6 && flag7 && this.SkipLineOnContainerExceeded)
							{
								this.TextOutput.AddNewLine(false, num2);
							}
							this.TextOutput.AddToken(textToken, num7, num, style, num2);
						}
						else if (tag.Name == "a")
						{
							this.FindLinkGroup(textToken);
							string text = "Link";
							string attribute2 = tag.GetAttribute("style");
							if (!string.IsNullOrEmpty(attribute2))
							{
								text = attribute2;
							}
							if (tag.Type == RichTextTagType.Open)
							{
								this._styleStack.Push(text);
							}
							else if (tag.Type == RichTextTagType.Close)
							{
								this._styleStack.Pop();
							}
							this.TextOutput.AddToken(textToken, 0f, num, text, -1f);
						}
						else if (tag.Name == "span")
						{
							string attribute3 = tag.GetAttribute("style");
							if (tag.Type == RichTextTagType.Open)
							{
								this._styleStack.Push(attribute3);
							}
							else if (tag.Type == RichTextTagType.Close)
							{
								this._styleStack.Pop();
							}
							this.TextOutput.AddToken(textToken, 0f, num, attribute3, -1f);
						}
					}
				}
				this._tokensNeedUpdate = false;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000039B4 File Offset: 0x00001BB4
		public void UpdateSize(int width, int height)
		{
			bool flag = this.Width != width;
			bool flag2 = this.Height != height;
			if (flag || flag2)
			{
				this.Width = Math.Max(0, width);
				this.Height = Math.Max(0, height);
				this.SetAllDirty();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A00 File Offset: 0x00001C00
		private TextTokenOutput GetTokenUnderPosition(Vector2 position)
		{
			if (position.X >= 0f && position.Y >= 0f && position.X < (float)this.Width && position.Y < (float)this.Height && this.TextOutput != null && this.TextOutput.Tokens != null)
			{
				foreach (TextTokenOutput textTokenOutput in this.TextOutput.Tokens)
				{
					if (textTokenOutput == null)
					{
						Debug.FailedAssert("TextOutputToken returned null. This shouldn't happen.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.TwoDimension\\BitmapFont\\RichText.cs", "GetTokenUnderPosition", 561);
					}
					else if (textTokenOutput.Rectangle.IsPointInside(position))
					{
						return textTokenOutput;
					}
				}
			}
			return null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003AD8 File Offset: 0x00001CD8
		private void PositionTokensInTextOutput(SpriteData spriteData, float renderScale)
		{
			if (this._preferredSize.X == 0f && this._preferredSize.Y == 0f)
			{
				return;
			}
			float num = 0f;
			float num2 = 0f;
			if (this._verticalAlignment == TextVerticalAlignment.Center || this._verticalAlignment == TextVerticalAlignment.Bottom)
			{
				float textHeight = this.TextOutput.TextHeight;
				float num3;
				if (this._verticalAlignment == TextVerticalAlignment.Center)
				{
					num3 = (float)this.Height - textHeight;
					num3 *= 0.5f;
				}
				else
				{
					num3 = (float)this.Height - textHeight;
				}
				num2 += num3;
			}
			for (int i = 0; i < this.TextOutput.LineCount; i++)
			{
				TextLineOutput line = this.TextOutput.GetLine(i);
				if (this._horizontalAlignment != TextHorizontalAlignment.Left)
				{
					if (this._horizontalAlignment == TextHorizontalAlignment.Center)
					{
						num = ((float)this.Width - line.Width) * 0.5f;
					}
					else if (this._horizontalAlignment == TextHorizontalAlignment.Right)
					{
						num = (float)this.Width - line.Width;
					}
					else if (this._horizontalAlignment == TextHorizontalAlignment.Justify)
					{
						float num4 = (float)this.Width - line.TextWidth;
						if (!line.LineEnded && line.TokenCount > 0)
						{
							int num5 = line.EmptyCharacterCount;
							int num6 = 1;
							while (line.GetToken(line.TokenCount - num6).Type == TextToken.TokenType.EmptyCharacter)
							{
								num5--;
								num6++;
							}
							num6 = 0;
							while (line.GetToken(num6).Type == TextToken.TokenType.EmptyCharacter)
							{
								num5--;
								num6++;
							}
							float num7 = num4 / (float)num5;
						}
					}
				}
				float num8 = 0f;
				for (int j = 0; j < line.TokenCount; j++)
				{
					TextTokenOutput tokenOutput = line.GetTokenOutput(j);
					StyleFontContainer.FontData fontData = this.StyleFontContainer.GetFontData(tokenOutput.Style);
					Font font = fontData.Font;
					float num9 = fontData.FontSize / (float)font.Size;
					float num10 = ((float)font.Base + 5f) * num9;
					if (num10 > num8)
					{
						num8 = num10;
					}
				}
				for (int k = 0; k < line.TokenCount; k++)
				{
					TextTokenOutput tokenOutput2 = line.GetTokenOutput(k);
					TextToken token = tokenOutput2.Token;
					StyleFontContainer.FontData fontData2 = this.StyleFontContainer.GetFontData(tokenOutput2.Style);
					Font font2 = fontData2.Font;
					float num11 = fontData2.FontSize / (float)font2.Size;
					float num12 = ((float)font2.Base + 5f) * num11;
					int @base = font2.Base;
					tokenOutput2.SetPosition(num, num2 + (num8 - num12));
					if (token.Type == TextToken.TokenType.EmptyCharacter || token.Type == TextToken.TokenType.NonBreakingSpace)
					{
						float num7 = this.GetEmptyCharacterWidth(font2, num11);
						num += num7;
					}
					else if (token.Type == TextToken.TokenType.Character)
					{
						char token2 = token.Token;
						float num13;
						if (!font2.Characters.ContainsKey((int)token2))
						{
							Font font3 = this._getUsableFontForCharacter((int)token2) ?? fontData2.Font;
							num11 = fontData2.FontSize / (float)font3.Size;
							num13 = font3.GetCharacterWidth(token2, 0.5f) * num11;
						}
						else
						{
							num11 = fontData2.FontSize / (float)font2.Size;
							num13 = font2.GetCharacterWidth(token2, 0.5f) * num11;
						}
						num += num13;
					}
					else if (token.Type == TextToken.TokenType.Tag)
					{
						RichTextTag tag = token.Tag;
						if (tag.Name == "img")
						{
							string attribute = tag.GetAttribute("src");
							Sprite sprite = null;
							if (!string.IsNullOrEmpty(attribute))
							{
								sprite = spriteData.GetSprite(attribute);
							}
							float num14 = 0f;
							if (sprite != null)
							{
								num14 = ((float)font2.Base + 0f) * num11 * ((float)sprite.Height / (float)sprite.Width) + 8f * renderScale;
							}
							num += num14;
						}
					}
				}
				num = 0f;
				num2 += line.Height;
			}
			this._positionNeedsUpdate = false;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003ECC File Offset: 0x000020CC
		private void FindLinkGroups()
		{
			this._linkGroups.Clear();
			RichTextLinkGroup richTextLinkGroup = null;
			for (int i = 0; i < this._tokens.Count; i++)
			{
				TextToken textToken = this._tokens[i];
				if (textToken.Type == TextToken.TokenType.Tag)
				{
					RichTextTag tag = textToken.Tag;
					if (tag.Name == "a")
					{
						if (tag.Type == RichTextTagType.Open)
						{
							richTextLinkGroup = new RichTextLinkGroup(i, tag.GetAttribute("href"));
						}
						else if (tag.Type == RichTextTagType.Close)
						{
							richTextLinkGroup.AddToken(textToken);
							this._linkGroups.Add(richTextLinkGroup);
							richTextLinkGroup = null;
						}
					}
				}
				if (richTextLinkGroup != null)
				{
					richTextLinkGroup.AddToken(textToken);
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003F74 File Offset: 0x00002174
		private RichTextPart GetOrCreatTextPartyOfStyle(string style, Font font, float x, float y)
		{
			foreach (RichTextPart richTextPart in this._richTextParts)
			{
				if (richTextPart.Type == RichTextPartType.Text && richTextPart.Style == style && richTextPart.DefaultFont == font)
				{
					return richTextPart;
				}
			}
			float scaleValue = this.StyleFontContainer.GetFontData(style).FontSize / (float)font.Size;
			TextMeshGenerator textMeshGenerator = new TextMeshGenerator();
			textMeshGenerator.Refresh(font, this._textLength, scaleValue);
			RichTextPart richTextPart2 = new RichTextPart
			{
				TextMeshGenerator = textMeshGenerator,
				Type = RichTextPartType.Text,
				Style = style,
				WordWidth = 0f,
				PartPosition = new Vector2(x, y),
				DefaultFont = font
			};
			this._richTextParts.Add(richTextPart2);
			return richTextPart2;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004068 File Offset: 0x00002268
		private void FillPartsWithTokens(SpriteData spriteData, float renderScale)
		{
			this._richTextParts.Clear();
			foreach (TextTokenOutput textTokenOutput in this.TextOutput.Tokens.ToArray<TextTokenOutput>())
			{
				string text = textTokenOutput.Style;
				TextToken token = textTokenOutput.Token;
				float x = textTokenOutput.X;
				float y = textTokenOutput.Y;
				StyleFontContainer.FontData fontData = this.StyleFontContainer.GetFontData(text);
				Font font = fontData.Font;
				float num = fontData.FontSize / (float)font.Size;
				if (token.Type == TextToken.TokenType.Character)
				{
					int token2 = (int)token.Token;
					float num2 = x;
					float num3 = y;
					int num4 = token2;
					if (!fontData.Font.Characters.ContainsKey(num4))
					{
						font = this._getUsableFontForCharacter(num4);
						if (font == null)
						{
							font = fontData.Font;
							num4 = 0;
						}
						num = fontData.FontSize / (float)font.Size;
					}
					else
					{
						font = fontData.Font;
						num = fontData.FontSize / (float)fontData.Font.Size;
					}
					RichTextLinkGroup richTextLinkGroup = this.FindLinkGroup(textTokenOutput.Token);
					if (richTextLinkGroup != null && this._focusedLinkGroup == richTextLinkGroup)
					{
						if (this._gotFocus)
						{
							text += ".MouseDown";
						}
						else
						{
							text += ".MouseOver";
						}
					}
					RichTextPart orCreatTextPartyOfStyle = this.GetOrCreatTextPartyOfStyle(text, font, x, y);
					TextMeshGenerator textMeshGenerator = orCreatTextPartyOfStyle.TextMeshGenerator;
					BitmapFontCharacter bitmapFontCharacter = font.Characters[num4];
					float x2 = num2 + (float)bitmapFontCharacter.XOffset * num;
					float y2 = num3 + (float)bitmapFontCharacter.YOffset * num;
					textMeshGenerator.AddCharacterToMesh(x2, y2, bitmapFontCharacter);
					orCreatTextPartyOfStyle.WordWidth += ((float)bitmapFontCharacter.XAdvance + 0.5f) * num;
					num2 += ((float)bitmapFontCharacter.XAdvance + 0.5f) * num;
				}
				else if (token.Type == TextToken.TokenType.EmptyCharacter || token.Type == TextToken.TokenType.NonBreakingSpace)
				{
					this.GetOrCreatTextPartyOfStyle(text, font, x, y).WordWidth += this.GetEmptyCharacterWidth(font, num);
				}
				else if (token.Type == TextToken.TokenType.Tag)
				{
					RichTextTag tag = token.Tag;
					if (tag.Name == "img")
					{
						string attribute = tag.GetAttribute("extend");
						float num5 = 0f;
						float num6;
						if (!string.IsNullOrEmpty(attribute) && float.TryParse(attribute, out num6))
						{
							num5 = num6;
						}
						string attribute2 = tag.GetAttribute("src");
						Sprite sprite = null;
						float num7 = (float)font.Base * num * 0.2f;
						num7 -= num5 * renderScale;
						float num8 = (float)font.Base * num * 0.1f;
						num8 -= num5 * renderScale;
						num8 += 4f * renderScale;
						if (!string.IsNullOrEmpty(attribute2))
						{
							sprite = spriteData.GetSprite(attribute2);
						}
						float x3 = x + num8;
						float y3 = y + num7;
						RichTextPart richTextPart = new RichTextPart();
						richTextPart.Sprite = sprite;
						richTextPart.SpritePosition = new Vector2(x3, y3);
						richTextPart.Type = RichTextPartType.Sprite;
						richTextPart.Extend = num5;
						this._richTextParts.Add(richTextPart);
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004384 File Offset: 0x00002584
		private void GenerateMeshes(float renderScale)
		{
			for (int i = 0; i < this._richTextParts.Count; i++)
			{
				RichTextPart richTextPart = this._richTextParts[i];
				Sprite sprite = richTextPart.Sprite;
				TextMeshGenerator textMeshGenerator = richTextPart.TextMeshGenerator;
				if (sprite != null)
				{
					StyleFontContainer.FontData fontData = this.StyleFontContainer.GetFontData(richTextPart.Style);
					float num = fontData.FontSize / (float)fontData.Font.Size;
					float num2 = (float)fontData.Font.Base * num * 0.8f + richTextPart.Extend * 2f * renderScale;
					float width = num2 * ((float)sprite.Height / (float)sprite.Width);
					DrawObject2D arrays = sprite.GetArrays(new SpriteDrawData(richTextPart.SpritePosition.X, richTextPart.SpritePosition.Y, num, width, num2, false, false));
					richTextPart.DrawObject2D = arrays;
				}
				if (textMeshGenerator != null)
				{
					DrawObject2D drawObject2D = textMeshGenerator.GenerateMesh();
					richTextPart.DrawObject2D = drawObject2D;
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004479 File Offset: 0x00002679
		private Font GetFontForTextToken(TextToken token)
		{
			return this._getUsableFontForCharacter((int)token.Token);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000448C File Offset: 0x0000268C
		public List<RichTextPart> GetParts()
		{
			return this._richTextParts;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004494 File Offset: 0x00002694
		private RichTextLinkGroup FindLinkGroup(TextToken textToken)
		{
			foreach (RichTextLinkGroup richTextLinkGroup in this._linkGroups)
			{
				if (richTextLinkGroup.Contains(textToken))
				{
					return richTextLinkGroup;
				}
			}
			return null;
		}

		// Token: 0x04000020 RID: 32
		public ILanguage CurrentLanguage;

		// Token: 0x04000021 RID: 33
		private TextHorizontalAlignment _horizontalAlignment;

		// Token: 0x04000022 RID: 34
		private TextVerticalAlignment _verticalAlignment;

		// Token: 0x04000023 RID: 35
		private bool _meshNeedsUpdate = true;

		// Token: 0x04000024 RID: 36
		private bool _preferredSizeNeedsUpdate = true;

		// Token: 0x04000025 RID: 37
		private bool _positionNeedsUpdate = true;

		// Token: 0x04000026 RID: 38
		private bool _tokensNeedUpdate = true;

		// Token: 0x04000027 RID: 39
		private bool _isFixedWidth;

		// Token: 0x04000028 RID: 40
		private bool _isFixedHeight;

		// Token: 0x04000029 RID: 41
		private Vector2 _preferredSize;

		// Token: 0x0400002A RID: 42
		private string _text;

		// Token: 0x0400002B RID: 43
		private List<TextToken> _tokens;

		// Token: 0x0400002E RID: 46
		private float _widthSize = -1f;

		// Token: 0x04000031 RID: 49
		private const float ExtraLetterPaddingHorizontal = 0.5f;

		// Token: 0x04000032 RID: 50
		private const float ExtraLetterPaddingVertical = 5f;

		// Token: 0x04000033 RID: 51
		private const float RichTextIconHorizontalPadding = 8f;

		// Token: 0x04000034 RID: 52
		private const float RichTextIconVerticalPadding = 0f;

		// Token: 0x04000035 RID: 53
		private List<RichTextPart> _richTextParts;

		// Token: 0x04000036 RID: 54
		private List<RichTextLinkGroup> _linkGroups;

		// Token: 0x04000037 RID: 55
		private Stack<string> _styleStack;

		// Token: 0x04000039 RID: 57
		private TextTokenOutput _focusedToken;

		// Token: 0x0400003A RID: 58
		private RichTextLinkGroup _focusedLinkGroup;

		// Token: 0x0400003B RID: 59
		private bool _gotFocus;

		// Token: 0x0400003C RID: 60
		private int _numOfAddedSeparators;

		// Token: 0x0400003D RID: 61
		private readonly Func<int, Font> _getUsableFontForCharacter;

		// Token: 0x0400003E RID: 62
		private bool _shouldAddNewLineWhenExceedingContainerWidth = true;

		// Token: 0x0400003F RID: 63
		private bool _canBreakWords = true;
	}
}
