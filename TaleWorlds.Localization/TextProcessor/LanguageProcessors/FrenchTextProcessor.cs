﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TaleWorlds.Localization.TextProcessor.LanguageProcessors
{
	// Token: 0x02000032 RID: 50
	public class FrenchTextProcessor : LanguageSpecificTextProcessor
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00007E02 File Offset: 0x00006002
		public static Dictionary<string, ValueTuple<string, int, bool>> WordGroups
		{
			get
			{
				if (FrenchTextProcessor._wordGroups == null)
				{
					FrenchTextProcessor._wordGroups = new Dictionary<string, ValueTuple<string, int, bool>>();
				}
				return FrenchTextProcessor._wordGroups;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007E1A File Offset: 0x0000601A
		private string LinkTag
		{
			get
			{
				return ".link";
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007E21 File Offset: 0x00006021
		private int LinkTagLength
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007E24 File Offset: 0x00006024
		private string LinkStarter
		{
			get
			{
				return "<a style=\"Link.";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007E2B File Offset: 0x0000602B
		private string LinkEnding
		{
			get
			{
				return "</b></a>";
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007E34 File Offset: 0x00006034
		public override void ProcessToken(string sourceText, ref int cursorPos, string token, StringBuilder outputString)
		{
			if (sourceText.Length > this.LinkStarter.Length + cursorPos)
			{
				string text = sourceText.Substring(cursorPos, this.LinkStarter.Length);
				if (token == this.LinkTag || text.Equals(this.LinkStarter))
				{
					cursorPos = this.ProcessLink(sourceText, cursorPos, token, outputString);
				}
			}
			string text2 = token.ToLower();
			if (FrenchTextProcessor.GenderTokens.TokenList.IndexOf(token) >= 0)
			{
				this.SetGenderInfo(token);
				this.ProcessWordGroup(sourceText, token, cursorPos);
				this.ResetGender();
				return;
			}
			if (FrenchTextProcessor.FunctionTokens.TokenList.IndexOf(text2) >= 0 && this.CheckWhiteSpaceAndTextEnd(sourceText, cursorPos))
			{
				ValueTuple<string, bool> valueTuple;
				if (this.IsWordGroup(sourceText, token, cursorPos, out valueTuple))
				{
					this.SetGenderInfo(valueTuple.Item1);
					if (valueTuple.Item2)
					{
						this.SetPlural();
					}
					else
					{
						this.SetSingular();
					}
				}
				if (!(text2 == ".cl"))
				{
					if (!(text2 == ".dl"))
					{
						if (!(text2 == ".l"))
						{
							if (!(text2 == ".a"))
							{
								if (!(text2 == ".d"))
								{
									if (text2 == ".c")
									{
										this.HandleAPreposition(sourceText, token, ref cursorPos, outputString);
									}
								}
								else
								{
									this.HandleDePreposition(sourceText, token, ref cursorPos, outputString);
								}
							}
							else
							{
								this.HandleIndefiniteArticles(sourceText, token, cursorPos, outputString);
							}
						}
						else
						{
							this.HandleDefiniteArticles(sourceText, token, cursorPos, outputString);
						}
					}
					else
					{
						this.HandleDePrepositionFollowedByArticle(sourceText, token, ref cursorPos, outputString);
					}
				}
				else
				{
					this.HandleAPrepositionFollowedByDefiniteArticle(sourceText, token, ref cursorPos, outputString);
				}
				FrenchTextProcessor._isPlural = false;
				FrenchTextProcessor._curGender = FrenchTextProcessor.WordGenderEnum.NoDeclination;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007FBC File Offset: 0x000061BC
		private bool IsWordGroup(string sourceText, string token, int cursorPos, out ValueTuple<string, bool> tags)
		{
			int num = 0;
			string text = string.Empty;
			tags = new ValueTuple<string, bool>(string.Empty, false);
			foreach (KeyValuePair<string, ValueTuple<string, int, bool>> keyValuePair in FrenchTextProcessor.WordGroups)
			{
				if (keyValuePair.Key.Length > 0 && sourceText.Length >= cursorPos + keyValuePair.Key.Length && keyValuePair.Key.Length > num && keyValuePair.Key.Equals(sourceText.Substring(cursorPos, keyValuePair.Key.Length)))
				{
					text = keyValuePair.Key;
					num = keyValuePair.Key.Length;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				tags = new ValueTuple<string, bool>(FrenchTextProcessor.WordGroups[text].Item1, FrenchTextProcessor.WordGroups[text].Item3);
				return true;
			}
			return false;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000080C4 File Offset: 0x000062C4
		private bool CheckNextCharIsVowel(string sourceText, int cursorPos)
		{
			return cursorPos < sourceText.Length && FrenchTextProcessor.Vowels.Contains(char.ToLower(sourceText[cursorPos]));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000080E7 File Offset: 0x000062E7
		private bool CheckWhiteSpaceAndTextEnd(string sourceText, int cursorPos)
		{
			return cursorPos < sourceText.Length && !char.IsWhiteSpace(sourceText[cursorPos]);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008103 File Offset: 0x00006303
		private void SetFeminine()
		{
			FrenchTextProcessor._curGender = FrenchTextProcessor.WordGenderEnum.Feminine;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000810B File Offset: 0x0000630B
		private void SetNeuter()
		{
			FrenchTextProcessor._curGender = FrenchTextProcessor.WordGenderEnum.Neuter;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008113 File Offset: 0x00006313
		private void SetMasculine()
		{
			FrenchTextProcessor._curGender = FrenchTextProcessor.WordGenderEnum.Masculine;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000811B File Offset: 0x0000631B
		private void SetPlural()
		{
			FrenchTextProcessor._isPlural = true;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008123 File Offset: 0x00006323
		private void SetSingular()
		{
			FrenchTextProcessor._isPlural = false;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000812C File Offset: 0x0000632C
		private void HandleDefiniteArticles(string text, string token, int cursorPos, StringBuilder outputString)
		{
			string definiteArticle = this.GetDefiniteArticle(text, token, cursorPos);
			if (!string.IsNullOrEmpty(definiteArticle))
			{
				outputString.Append(definiteArticle);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008154 File Offset: 0x00006354
		private string GetDefiniteArticle(string text, string token, int cursorPos)
		{
			string text2 = null;
			if (FrenchTextProcessor._curGender != FrenchTextProcessor.WordGenderEnum.NoDeclination)
			{
				if (FrenchTextProcessor._isPlural)
				{
					text2 = FrenchTextProcessor._genderToDefiniteArticle[FrenchTextProcessor.WordType.Plural];
				}
				else if (this.CheckNextCharIsVowel(text, cursorPos) && !this.CheckIfNextWordShouldBeConsideredAConsonant(text, token, cursorPos))
				{
					text2 = FrenchTextProcessor._genderToDefiniteArticle[FrenchTextProcessor.WordType.StartingWithaVowel];
				}
				else
				{
					text2 = FrenchTextProcessor._genderToDefiniteArticle[this.GetWordTypeFromGender(FrenchTextProcessor._curGender)];
				}
				string a = token.ToLowerInvariant();
				if (a == ".cl" || a == ".dl")
				{
					if (char.IsUpper(token[2]))
					{
						text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
					}
				}
				else if (text2 != null)
				{
					if (token.All((char x) => !char.IsLetter(x) || char.IsUpper(x)))
					{
						text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
					}
				}
			}
			return text2;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000825C File Offset: 0x0000645C
		private void HandleIndefiniteArticles(string text, string token, int cursorPos, StringBuilder stringBuilder)
		{
			string text2 = null;
			if (FrenchTextProcessor._isPlural)
			{
				text2 = FrenchTextProcessor._genderToIndefiniteArticle[FrenchTextProcessor.WordType.Plural];
			}
			else if (FrenchTextProcessor._curGender != FrenchTextProcessor.WordGenderEnum.NoDeclination)
			{
				text2 = FrenchTextProcessor._genderToIndefiniteArticle[this.GetWordTypeFromGender(FrenchTextProcessor._curGender)];
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (token.All((char x) => !char.IsLetter(x) || char.IsUpper(x)))
				{
					text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
				}
				stringBuilder.Append(text2);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000082F8 File Offset: 0x000064F8
		private void HandleAPreposition(string text, string token, ref int cursorPos, StringBuilder outputString)
		{
			string apreposition = this.GetAPreposition(text, token, cursorPos);
			string nextWord = this.GetNextWord(text, token, cursorPos);
			string value;
			if (this.CheckIfWordsHaveContraction(apreposition, nextWord.Trim(), out value))
			{
				outputString.Append(value);
				cursorPos += nextWord.Length;
				return;
			}
			outputString.Append(apreposition + " ");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008354 File Offset: 0x00006554
		private string GetAPreposition(string text, string token, int cursorPos)
		{
			string text2 = FrenchTextProcessor._aPreposition;
			if (char.IsUpper(token[1]))
			{
				text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
			}
			return text2;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008398 File Offset: 0x00006598
		private void HandleAPrepositionFollowedByDefiniteArticle(string text, string token, ref int cursorPos, StringBuilder outputString)
		{
			string apreposition = this.GetAPreposition(text, token, cursorPos);
			string definiteArticle = this.GetDefiniteArticle(text, token, cursorPos);
			string text2 = string.Empty;
			if (definiteArticle != null && this.CheckIfWordsHaveContraction(apreposition, definiteArticle.Trim(), out text2))
			{
				if (char.IsUpper(token[1]))
				{
					text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
				}
				outputString.Append(text2 + " ");
				return;
			}
			outputString.Append(apreposition + " " + definiteArticle);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008430 File Offset: 0x00006630
		private void HandleDePrepositionFollowedByArticle(string text, string token, ref int cursorPos, StringBuilder outputString)
		{
			string dePreposition = this.GetDePreposition(text, token, cursorPos);
			string definiteArticle = this.GetDefiniteArticle(text, token, cursorPos);
			string text2 = string.Empty;
			if (definiteArticle != null && this.CheckIfWordsHaveContraction(dePreposition, definiteArticle.Trim(), out text2))
			{
				if (char.IsUpper(token[1]))
				{
					text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
				}
				outputString.Append(text2 + " ");
				return;
			}
			outputString.Append(dePreposition + " " + definiteArticle);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000084C8 File Offset: 0x000066C8
		private string GetDePreposition(string text, string token, int cursorPos)
		{
			string text2 = FrenchTextProcessor._dePreposition;
			if (char.IsUpper(token[1]))
			{
				text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
			}
			return text2;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000850C File Offset: 0x0000670C
		private void HandleDePreposition(string text, string token, ref int cursorPos, StringBuilder outputString)
		{
			string dePreposition = this.GetDePreposition(text, token, cursorPos);
			string nextWord = this.GetNextWord(text, token, cursorPos);
			bool flag = this.CheckNextCharIsVowel(text, cursorPos) && !this.CheckIfNextWordShouldBeConsideredAConsonant(text, token, cursorPos);
			if (!this.CheckIfWordIsAnArticle(nextWord) && flag)
			{
				outputString.Append(FrenchTextProcessor._dePrepositionWithVowel);
				return;
			}
			string value;
			if (this.CheckIfWordsHaveContraction(dePreposition, nextWord.Trim(), out value))
			{
				outputString.Append(value);
				cursorPos += nextWord.Length;
				return;
			}
			outputString.Append(dePreposition + " ");
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000085A0 File Offset: 0x000067A0
		private bool CheckIfNextWordShouldBeConsideredAConsonant(string text, string token, int cursorPos)
		{
			string nextWord = this.GetNextWord(text, token, cursorPos);
			return !string.IsNullOrEmpty(nextWord) && FrenchTextProcessor._shouldBeConsideredConsonants.Contains(nextWord.ToLowerInvariant());
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000085D4 File Offset: 0x000067D4
		private bool CheckIfWordsHaveContraction(string t1, string t2, out string result)
		{
			result = string.Empty;
			Dictionary<string, string> dictionary;
			string text;
			if (FrenchTextProcessor.Contractions.TryGetValue(t1.ToLowerInvariant(), out dictionary) && dictionary.TryGetValue(t2.ToLowerInvariant(), out text))
			{
				result = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008612 File Offset: 0x00006812
		private bool CheckIfWordIsAnArticle(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				text = text.ToLowerInvariant();
				return FrenchTextProcessor._articles.Contains(text) || text.StartsWith(FrenchTextProcessor._articleVowelStart);
			}
			return false;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008640 File Offset: 0x00006840
		private string GetNextWord(string text, string token, int cursorPos)
		{
			if (cursorPos - token.Length - 2 < text.IndexOf('}'))
			{
				return text.Remove(0, text.IndexOf('}') + 1).Split(new char[]
				{
					' '
				})[0];
			}
			return "";
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000867F File Offset: 0x0000687F
		private FrenchTextProcessor.WordType GetWordTypeFromGender(FrenchTextProcessor.WordGenderEnum gender)
		{
			switch (gender)
			{
			case FrenchTextProcessor.WordGenderEnum.Masculine:
				return FrenchTextProcessor.WordType.Masculine;
			case FrenchTextProcessor.WordGenderEnum.Feminine:
				return FrenchTextProcessor.WordType.Feminine;
			}
			return FrenchTextProcessor.WordType.None;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000086A0 File Offset: 0x000068A0
		private void SetGenderInfo(string token)
		{
			if (token == ".M")
			{
				this.SetMasculine();
				return;
			}
			if (token == ".F")
			{
				this.SetFeminine();
				return;
			}
			if (token == ".N")
			{
				this.SetNeuter();
				return;
			}
			if (token == ".P")
			{
				this.SetPlural();
				return;
			}
			if (token == ".S")
			{
				this.SetSingular();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008710 File Offset: 0x00006910
		private void ProcessWordGroup(string text, string token, int cursorPos)
		{
			string key = text.Substring(text.LastIndexOf('}') + 1);
			ValueTuple<string, int, bool> valueTuple;
			if (FrenchTextProcessor.WordGroups.TryGetValue(key, out valueTuple))
			{
				FrenchTextProcessor.WordGroups[key] = new ValueTuple<string, int, bool>(valueTuple.Item1, valueTuple.Item2, FrenchTextProcessor._isPlural);
				return;
			}
			FrenchTextProcessor.WordGroups.Add(key, new ValueTuple<string, int, bool>(token, cursorPos, FrenchTextProcessor._isPlural));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008776 File Offset: 0x00006976
		private void ResetGender()
		{
			FrenchTextProcessor._curGender = FrenchTextProcessor.WordGenderEnum.NoDeclination;
			FrenchTextProcessor._isPlural = false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008784 File Offset: 0x00006984
		private int ProcessLink(string text, int cursorPos, string token, StringBuilder outputString)
		{
			int num = text.IndexOf(this.LinkEnding, cursorPos);
			if (num > cursorPos)
			{
				string text2 = text.Substring(cursorPos, num - cursorPos);
				string text3 = text2.Substring(0, text2.LastIndexOf('>') + 1);
				string key = text2.Substring(text3.Length);
				this.ResetGender();
				ValueTuple<string, int, bool> valueTuple;
				if (token != this.LinkTag && FrenchTextProcessor.WordGroups.TryGetValue(key, out valueTuple))
				{
					this.SetGenderInfo(valueTuple.Item1);
					if (valueTuple.Item3)
					{
						this.SetPlural();
					}
					else
					{
						this.SetSingular();
					}
				}
				outputString.Append(text3);
				return cursorPos + text3.Length;
			}
			return cursorPos;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000882A File Offset: 0x00006A2A
		public override CultureInfo CultureInfoForLanguage
		{
			get
			{
				return FrenchTextProcessor.CultureInfo;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008831 File Offset: 0x00006A31
		public override void ClearTemporaryData()
		{
			FrenchTextProcessor.WordGroups.Clear();
			FrenchTextProcessor._isPlural = false;
			FrenchTextProcessor._curGender = FrenchTextProcessor.WordGenderEnum.NoDeclination;
		}

		// Token: 0x040000AC RID: 172
		private static char[] Vowels = new char[]
		{
			'a',
			'e',
			'i',
			'o',
			'u',
			'h'
		};

		// Token: 0x040000AD RID: 173
		[ThreadStatic]
		private static Dictionary<string, ValueTuple<string, int, bool>> _wordGroups;

		// Token: 0x040000AE RID: 174
		[ThreadStatic]
		private static FrenchTextProcessor.WordGenderEnum _curGender;

		// Token: 0x040000AF RID: 175
		[ThreadStatic]
		private static bool _isPlural = false;

		// Token: 0x040000B0 RID: 176
		private static List<string> _articles = new List<string>
		{
			"le",
			"la",
			"les"
		};

		// Token: 0x040000B1 RID: 177
		private static string _articleVowelStart = "l'";

		// Token: 0x040000B2 RID: 178
		private static string _dePreposition = "de";

		// Token: 0x040000B3 RID: 179
		private static string _dePrepositionWithVowel = "d'";

		// Token: 0x040000B4 RID: 180
		private static string _aPreposition = "à";

		// Token: 0x040000B5 RID: 181
		private static readonly Dictionary<string, Dictionary<string, string>> Contractions = new Dictionary<string, Dictionary<string, string>>
		{
			{
				"de",
				new Dictionary<string, string>
				{
					{
						"les",
						"des"
					},
					{
						"le",
						"du"
					}
				}
			},
			{
				"à",
				new Dictionary<string, string>
				{
					{
						"les",
						"aux"
					},
					{
						"le",
						"au"
					}
				}
			}
		};

		// Token: 0x040000B6 RID: 182
		private static Dictionary<FrenchTextProcessor.WordType, string> _genderToDefiniteArticle = new Dictionary<FrenchTextProcessor.WordType, string>
		{
			{
				FrenchTextProcessor.WordType.Masculine,
				"le "
			},
			{
				FrenchTextProcessor.WordType.Feminine,
				"la "
			},
			{
				FrenchTextProcessor.WordType.Plural,
				"les "
			},
			{
				FrenchTextProcessor.WordType.StartingWithaVowel,
				"l'"
			},
			{
				FrenchTextProcessor.WordType.None,
				""
			}
		};

		// Token: 0x040000B7 RID: 183
		private static Dictionary<FrenchTextProcessor.WordType, string> _genderToIndefiniteArticle = new Dictionary<FrenchTextProcessor.WordType, string>
		{
			{
				FrenchTextProcessor.WordType.Masculine,
				"un "
			},
			{
				FrenchTextProcessor.WordType.Feminine,
				"une "
			},
			{
				FrenchTextProcessor.WordType.Plural,
				"des "
			},
			{
				FrenchTextProcessor.WordType.StartingWithaVowel,
				""
			},
			{
				FrenchTextProcessor.WordType.None,
				""
			}
		};

		// Token: 0x040000B8 RID: 184
		private static List<string> _shouldBeConsideredConsonants = new List<string>
		{
			"hache",
			"hachette",
			"héros",
			"houe",
			"haute",
			"hardes",
			"hachoir",
			"harnais",
			"harpon",
			"haubert",
			"haut",
			"horde"
		};

		// Token: 0x040000B9 RID: 185
		private static readonly CultureInfo CultureInfo = new CultureInfo("fr-FR");

		// Token: 0x0200003F RID: 63
		private enum WordGenderEnum
		{
			// Token: 0x04000121 RID: 289
			Masculine,
			// Token: 0x04000122 RID: 290
			Feminine,
			// Token: 0x04000123 RID: 291
			Neuter,
			// Token: 0x04000124 RID: 292
			NoDeclination
		}

		// Token: 0x02000040 RID: 64
		private enum WordType
		{
			// Token: 0x04000126 RID: 294
			StartingWithaVowel,
			// Token: 0x04000127 RID: 295
			Masculine,
			// Token: 0x04000128 RID: 296
			Feminine,
			// Token: 0x04000129 RID: 297
			Plural,
			// Token: 0x0400012A RID: 298
			None
		}

		// Token: 0x02000041 RID: 65
		private static class GenderTokens
		{
			// Token: 0x0400012B RID: 299
			public const string Masculine = ".M";

			// Token: 0x0400012C RID: 300
			public const string Feminine = ".F";

			// Token: 0x0400012D RID: 301
			public const string Neuter = ".N";

			// Token: 0x0400012E RID: 302
			public const string Plural = ".P";

			// Token: 0x0400012F RID: 303
			public const string Singular = ".S";

			// Token: 0x04000130 RID: 304
			public static readonly List<string> TokenList = new List<string>
			{
				".M",
				".F",
				".N",
				".P",
				".S"
			};
		}

		// Token: 0x02000042 RID: 66
		private static class FunctionTokens
		{
			// Token: 0x04000131 RID: 305
			public const string DefiniteArticle = ".l";

			// Token: 0x04000132 RID: 306
			public const string DefiniteArticleWithBrackets = "{.l}";

			// Token: 0x04000133 RID: 307
			public const string IndefiniteArticle = ".a";

			// Token: 0x04000134 RID: 308
			public const string APreposition = ".c";

			// Token: 0x04000135 RID: 309
			public const string APrepositionFollowedByDefiniteArticle = ".cl";

			// Token: 0x04000136 RID: 310
			public const string DePreposition = ".d";

			// Token: 0x04000137 RID: 311
			public const string DePrepositionFollowedByDefiniteArticle = ".dl";

			// Token: 0x04000138 RID: 312
			public static readonly List<string> TokenList = new List<string>
			{
				".l",
				".a",
				".d",
				".c",
				".cl",
				".dl"
			};
		}
	}
}
