using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000017 RID: 23
	public static class TextParser
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00006198 File Offset: 0x00004398
		public static List<TextToken> Parse(string text, ILanguage currentLanguage)
		{
			List<TextToken> list = new List<TextToken>(text.Length);
			foreach (char c in text)
			{
				if (c != '\r')
				{
					bool cannotEndLineWithCharacter = currentLanguage.IsCharacterForbiddenAtEndOfLine(c);
					bool cannotStartLineWithCharacter = currentLanguage.IsCharacterForbiddenAtStartOfLine(c);
					if (c == ' ')
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
					else
					{
						TextToken textToken = TextToken.CreateCharacter(c);
						textToken.CannotEndLineWithCharacter = cannotEndLineWithCharacter;
						textToken.CannotStartLineWithCharacter = cannotStartLineWithCharacter;
						list.Add(textToken);
					}
				}
			}
			return list;
		}
	}
}
