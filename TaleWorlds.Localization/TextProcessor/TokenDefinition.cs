using System;
using System.Text.RegularExpressions;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x0200002A RID: 42
	internal class TokenDefinition
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005EA2 File Offset: 0x000040A2
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005EAA File Offset: 0x000040AA
		public TokenType TokenType { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005EB3 File Offset: 0x000040B3
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005EBB File Offset: 0x000040BB
		public int Precedence { get; private set; }

		// Token: 0x06000119 RID: 281 RVA: 0x00005EC4 File Offset: 0x000040C4
		public TokenDefinition(TokenType tokenType, string regexPattern, int precedence)
		{
			this._regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			this.TokenType = tokenType;
			this.Precedence = precedence;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005EE8 File Offset: 0x000040E8
		internal Match CheckMatch(string str, int beginIndex)
		{
			beginIndex = this.SkipWhiteSpace(str, beginIndex);
			Match match = this._regex.Match(str, beginIndex);
			if (match.Success && match.Index == beginIndex)
			{
				return match;
			}
			return null;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005F24 File Offset: 0x00004124
		private int SkipWhiteSpace(string str, int beginIndex)
		{
			int num = beginIndex;
			int length = str.Length;
			while (num < length && char.IsWhiteSpace(str[num]))
			{
				num++;
			}
			return num;
		}

		// Token: 0x04000061 RID: 97
		private readonly Regex _regex;
	}
}
