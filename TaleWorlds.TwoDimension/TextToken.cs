using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000018 RID: 24
	public class TextToken
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006296 File Offset: 0x00004496
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000629E File Offset: 0x0000449E
		public char Token { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000062A7 File Offset: 0x000044A7
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000062AF File Offset: 0x000044AF
		public TextToken.TokenType Type { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000062B8 File Offset: 0x000044B8
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000062C0 File Offset: 0x000044C0
		public RichTextTag Tag { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000062C9 File Offset: 0x000044C9
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000062D1 File Offset: 0x000044D1
		public bool CannotStartLineWithCharacter { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000062DA File Offset: 0x000044DA
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000062E2 File Offset: 0x000044E2
		public bool CannotEndLineWithCharacter { get; set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x000062EB File Offset: 0x000044EB
		private TextToken(TextToken.TokenType type, char token)
		{
			this.Type = type;
			this.Token = token;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006301 File Offset: 0x00004501
		private TextToken(RichTextTag tag)
		{
			this.Type = TextToken.TokenType.Tag;
			this.Tag = tag;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006317 File Offset: 0x00004517
		public static TextToken CreateEmptyCharacter()
		{
			return new TextToken(TextToken.TokenType.EmptyCharacter, ' ');
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006321 File Offset: 0x00004521
		public static TextToken CreateZeroWidthSpaceCharacter()
		{
			return new TextToken(TextToken.TokenType.ZeroWidthSpace, '\0');
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000632A File Offset: 0x0000452A
		public static TextToken CreateNonBreakingSpaceCharacter()
		{
			return new TextToken(TextToken.TokenType.NonBreakingSpace, ' ');
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006334 File Offset: 0x00004534
		public static TextToken CreateWordJoinerCharacter()
		{
			return new TextToken(TextToken.TokenType.WordJoiner, Convert.ToChar(8288));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006346 File Offset: 0x00004546
		public static TextToken CreateNewLine()
		{
			return new TextToken(TextToken.TokenType.NewLine, '\n');
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006350 File Offset: 0x00004550
		public static TextToken CreateTab()
		{
			return new TextToken(TextToken.TokenType.Tab, '\t');
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000635A File Offset: 0x0000455A
		public static TextToken CreateCharacter(char character)
		{
			return new TextToken(TextToken.TokenType.Character, character);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006363 File Offset: 0x00004563
		public static TextToken CreateTag(RichTextTag tag)
		{
			return new TextToken(tag);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000636B File Offset: 0x0000456B
		public static TextToken CreateCharacterCannotEndLineWith(char character)
		{
			return new TextToken(TextToken.TokenType.Character, character)
			{
				CannotEndLineWithCharacter = true
			};
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000637B File Offset: 0x0000457B
		public static TextToken CreateCharacterCannotStartLineWith(char character)
		{
			return new TextToken(TextToken.TokenType.Character, character)
			{
				CannotStartLineWithCharacter = true
			};
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000638C File Offset: 0x0000458C
		public static List<TextToken> CreateTokenArrayFromWord(string word)
		{
			List<TextToken> list = new List<TextToken>();
			foreach (char character in word)
			{
				list.Add(TextToken.CreateCharacter(character));
			}
			return list;
		}

		// Token: 0x02000040 RID: 64
		public enum TokenType
		{
			// Token: 0x04000165 RID: 357
			EmptyCharacter,
			// Token: 0x04000166 RID: 358
			ZeroWidthSpace,
			// Token: 0x04000167 RID: 359
			NonBreakingSpace,
			// Token: 0x04000168 RID: 360
			WordJoiner,
			// Token: 0x04000169 RID: 361
			NewLine,
			// Token: 0x0400016A RID: 362
			Tab,
			// Token: 0x0400016B RID: 363
			Character,
			// Token: 0x0400016C RID: 364
			Tag
		}
	}
}
