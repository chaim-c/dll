using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TaleWorlds.Library
{
	// Token: 0x0200007C RID: 124
	public class ProfanityChecker
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0000DE90 File Offset: 0x0000C090
		public ProfanityChecker(string[] profanityList, string[] allowList)
		{
			this.ProfanityList = profanityList;
			this.AllowList = allowList;
			for (int i = 0; i < this.ProfanityList.Length; i++)
			{
				this.ProfanityList[i] = this.ProfanityList[i].ToLower();
			}
			for (int j = 0; j < this.AllowList.Length; j++)
			{
				this.AllowList[j] = this.AllowList[j].ToLower();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000DF01 File Offset: 0x0000C101
		public bool IsProfane(string word)
		{
			if (string.IsNullOrEmpty(word) || word.Length == 0)
			{
				return false;
			}
			word = word.ToLower();
			return !this.AllowList.Contains(word) && this.ProfanityList.Contains(word);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000DF3C File Offset: 0x0000C13C
		public bool ContainsProfanity(string text, ProfanityChecker.ProfanityChechkerType checkType)
		{
			if (string.IsNullOrEmpty(text) || text.Length == 0)
			{
				return false;
			}
			List<string> list = new List<string>();
			foreach (string text2 in this.ProfanityList)
			{
				if (text.Length >= text2.Length)
				{
					list.Add(text2);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			text = text.ToLower();
			if (checkType == ProfanityChecker.ProfanityChechkerType.FalsePositive)
			{
				using (IEnumerator enumerator = new Regex(string.Format("(?:{0})", string.Join("|", list).Replace("$", "\\$"), RegexOptions.IgnoreCase)).Matches(text).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object value = enumerator.Current;
						if (!this.AllowList.Contains(value))
						{
							return true;
						}
					}
					return false;
				}
			}
			if (checkType == ProfanityChecker.ProfanityChechkerType.FalseNegative)
			{
				foreach (object obj in new Regex("\\w(?<!\\d)[\\w'-]*", RegexOptions.IgnoreCase).Matches(text))
				{
					string value2 = obj.ToString();
					if (this.ProfanityList.Contains(value2) && !this.AllowList.Contains(value2))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		public string CensorText(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = text.ToLower();
				StringBuilder stringBuilder = new StringBuilder(text);
				string[] array = text.Split(new char[]
				{
					' '
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text3 = array[i].ToLower();
					foreach (string text4 in this.ProfanityList)
					{
						string text5 = text3;
						while (text3.Contains(text4) && !this.AllowList.Contains(text3))
						{
							string text6 = stringBuilder.ToString().ToLower();
							int num = text6.IndexOf(text4, StringComparison.Ordinal);
							if (num < 0)
							{
								num = text2.IndexOf(text4, StringComparison.Ordinal);
								text6.Substring(num, text4.Length);
							}
							int startIndex = text3.IndexOf(text4, StringComparison.Ordinal);
							text3 = text3.Remove(startIndex, text4.Length);
							for (int k = num; k < num + text4.Length; k++)
							{
								stringBuilder[k] = '*';
							}
						}
						text3 = text5;
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x04000140 RID: 320
		private readonly string[] ProfanityList;

		// Token: 0x04000141 RID: 321
		private readonly string[] AllowList;

		// Token: 0x020000D2 RID: 210
		public enum ProfanityChechkerType
		{
			// Token: 0x0400029B RID: 667
			FalsePositive,
			// Token: 0x0400029C RID: 668
			FalseNegative
		}
	}
}
