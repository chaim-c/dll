using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x0200002C RID: 44
	public abstract class LanguageSpecificTextProcessor
	{
		// Token: 0x0600011D RID: 285
		public abstract void ProcessToken(string sourceText, ref int cursorPos, string token, StringBuilder outputString);

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600011E RID: 286
		public abstract CultureInfo CultureInfoForLanguage { get; }

		// Token: 0x0600011F RID: 287
		public abstract void ClearTemporaryData();

		// Token: 0x06000120 RID: 288 RVA: 0x00005FE4 File Offset: 0x000041E4
		public LanguageSpecificTextProcessor()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005FF8 File Offset: 0x000041F8
		public string Process(string text)
		{
			if (text == null)
			{
				return null;
			}
			bool flag = false;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '{')
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int j = 0;
			while (j < text.Length)
			{
				if (text[j] != '{')
				{
					stringBuilder.Append(text[j]);
					j++;
				}
				else
				{
					string token = LanguageSpecificTextProcessor.ReadFirstToken(text, ref j);
					if (LanguageSpecificTextProcessor.IsPostProcessToken(token))
					{
						this.ProcessTokenInternal(text, ref j, token, stringBuilder);
					}
				}
			}
			this.ProcessLowerCaseMarkers(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006090 File Offset: 0x00004290
		private void ProcessTokenInternal(string sourceText, ref int cursorPos, string token, StringBuilder outputString)
		{
			CultureInfo cultureInfoForLanguage = this.CultureInfoForLanguage;
			char c = token[1];
			if (c == '^' && token.Length == 2)
			{
				int num = LanguageSpecificTextProcessor.FindNextLetter(sourceText, cursorPos);
				if (num > cursorPos && num < sourceText.Length)
				{
					outputString.Append(sourceText.Substring(cursorPos, num - cursorPos));
				}
				if (num < sourceText.Length)
				{
					outputString.Append(char.ToUpper(sourceText[num], cultureInfoForLanguage));
					cursorPos = num + 1;
					return;
				}
			}
			else if (c == '_' && token.Length == 2)
			{
				int num2 = LanguageSpecificTextProcessor.FindNextLetter(sourceText, cursorPos);
				if (num2 > cursorPos && num2 < sourceText.Length)
				{
					outputString.Append(sourceText.Substring(cursorPos, num2 - cursorPos));
				}
				if (num2 < sourceText.Length)
				{
					outputString.Append(char.ToLower(sourceText[num2], cultureInfoForLanguage));
					cursorPos = num2 + 1;
					return;
				}
			}
			else
			{
				if (c == '%' && token.Length == 2)
				{
					this._lowerMarkers.Add(outputString.Length - 1);
					return;
				}
				this.ProcessToken(sourceText, ref cursorPos, token, outputString);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006198 File Offset: 0x00004398
		private void ProcessLowerCaseMarkers(StringBuilder stringBuilder)
		{
			if (this._lowerMarkers.Count > 0)
			{
				for (int i = 0; i < this._lowerMarkers.Count; i += 2)
				{
					int num = this._lowerMarkers[i];
					if (i + 1 < this._lowerMarkers.Count)
					{
						int num2 = this._lowerMarkers[i + 1];
						if (num != num2)
						{
							if (num > stringBuilder.Length)
							{
								num = -1;
							}
							int length = Math.Min(num2 - num, stringBuilder.Length - num - 1);
							string text = stringBuilder.ToString(num + 1, length);
							stringBuilder = stringBuilder.Remove(num + 1, length).Insert(num + 1, text.ToLower());
						}
					}
					else
					{
						if (num > stringBuilder.Length)
						{
							num = -1;
						}
						if (num + 1 < stringBuilder.Length)
						{
							string text2 = stringBuilder.ToString(num + 1, stringBuilder.Length - num - 1);
							stringBuilder = stringBuilder.Remove(num + 1, stringBuilder.Length - num - 1).Insert(num + 1, text2.ToLower());
						}
					}
				}
				this._lowerMarkers.Clear();
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000062A8 File Offset: 0x000044A8
		private static int FindNextLetter(string sourceText, int cursorPos)
		{
			int i = cursorPos;
			if (sourceText.Length > i + "<a style=\"Link.".Length && sourceText.Substring(i, "<a style=\"Link.".Length).Equals("<a style=\"Link."))
			{
				i += "<a style=\"Link.".Length;
				while (sourceText[i++] != '>')
				{
				}
			}
			while (i < sourceText.Length)
			{
				if (sourceText[i] == '<')
				{
					i += 2;
				}
				if (char.IsLetter(sourceText, i))
				{
					return i;
				}
				i++;
			}
			return i;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000632E File Offset: 0x0000452E
		private static bool IsPostProcessToken(string token)
		{
			return token.Length > 1 && token[0] == '.';
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006348 File Offset: 0x00004548
		private static string ReadFirstToken(string text, ref int i)
		{
			int num = i;
			while (i < text.Length && text[i] != '}')
			{
				i++;
			}
			int length = i - num - 1;
			if (i < text.Length)
			{
				i++;
			}
			return text.Substring(num + 1, length);
		}

		// Token: 0x04000064 RID: 100
		private List<int> _lowerMarkers = new List<int>();
	}
}
