﻿using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200000D RID: 13
	public class RichTextParser
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000462C File Offset: 0x0000282C
		public static List<TextToken> Parse(string text)
		{
			int i = 0;
			List<TextToken> list = new List<TextToken>(text.Length);
			while (i < text.Length)
			{
				char c = text[i];
				if (c != '\r')
				{
					if (c == ' ' || c == '\u3000')
					{
						list.Add(TextToken.CreateEmptyCharacter());
					}
					else if (c == '\t')
					{
						list.Add(TextToken.CreateTab());
					}
					else if (c == '\n')
					{
						list.Add(TextToken.CreateNewLine());
					}
					else if (c == '\u00a0' || c == '\u202f' || c == '\u2007')
					{
						list.Add(TextToken.CreateNonBreakingSpaceCharacter());
					}
					else if (c == '​')
					{
						list.Add(TextToken.CreateZeroWidthSpaceCharacter());
					}
					else if (c == '⁠')
					{
						list.Add(TextToken.CreateWordJoinerCharacter());
					}
					else if (c == '<')
					{
						int num = i;
						int num2 = -1;
						while (i < text.Length)
						{
							if (text[i] == '>')
							{
								num2 = i + 1;
								break;
							}
							i++;
						}
						RichTextTag richTextTag = RichTextTagParser.Parse(text, num, num2);
						if (richTextTag.Type == RichTextTagType.TextAfterError)
						{
							if (num2 == -1)
							{
								list.AddRange(TextToken.CreateTokenArrayFromWord(text.Substring(num, text.Length - num)));
							}
							else if (num2 > num)
							{
								list.AddRange(TextToken.CreateTokenArrayFromWord(text.Substring(num, num2 - num)));
							}
						}
						list.Add(TextToken.CreateTag(richTextTag));
					}
					else
					{
						list.Add(TextToken.CreateCharacter(c));
					}
				}
				i++;
			}
			return list;
		}
	}
}
