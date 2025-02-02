using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TaleWorlds.Localization.TextProcessor.LanguageProcessors
{
	// Token: 0x02000037 RID: 55
	public class SpanishTextProcessor : LanguageSpecificTextProcessor
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00018540 File Offset: 0x00016740
		public override void ProcessToken(string sourceText, ref int cursorPos, string token, StringBuilder outputString)
		{
			if (SpanishTextProcessor.GenderTokens.TokenList.Contains(token))
			{
				this.SetGender(token);
			}
			if (token == ".l" || token == ".L")
			{
				this.HandleDefiniteArticles(sourceText, token, cursorPos, outputString);
				SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.NoDeclination;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001858D File Offset: 0x0001678D
		private bool CheckWhiteSpaceAndTextEnd(string sourceText, int cursorPos)
		{
			return cursorPos < sourceText.Length && !char.IsWhiteSpace(sourceText[cursorPos]);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000185AC File Offset: 0x000167AC
		private void SetGender(string token)
		{
			if (token == ".MS")
			{
				SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.MasculineSingular;
				return;
			}
			if (token == ".MP")
			{
				SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.MasculinePlural;
				return;
			}
			if (token == ".FS")
			{
				SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.FeminineSingular;
				return;
			}
			if (token == ".FP")
			{
				SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.FemininePlural;
				return;
			}
			if (token == ".NS")
			{
				SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.NeuterSingular;
				return;
			}
			if (!(token == ".NP"))
			{
				return;
			}
			SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.NeuterPlural;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00018634 File Offset: 0x00016834
		private void HandleDefiniteArticles(string text, string token, int cursorPos, StringBuilder stringBuilder)
		{
			if (!this.CheckWhiteSpaceAndTextEnd(text, cursorPos))
			{
				return;
			}
			if (SpanishTextProcessor._curGender == SpanishTextProcessor.WordGenderEnum.MasculineSingular || SpanishTextProcessor._curGender == SpanishTextProcessor.WordGenderEnum.MasculinePlural || SpanishTextProcessor._curGender == SpanishTextProcessor.WordGenderEnum.FeminineSingular || SpanishTextProcessor._curGender == SpanishTextProcessor.WordGenderEnum.FemininePlural)
			{
				string text2 = SpanishTextProcessor._genderToDefiniteArticle[SpanishTextProcessor._curGender];
				bool flag = false;
				string text3;
				if (SpanishTextProcessor._curGender == SpanishTextProcessor.WordGenderEnum.MasculineSingular && this.HandleContractions(text, text2, cursorPos, out text3))
				{
					text2 = text3;
					flag = true;
					if (char.IsWhiteSpace(stringBuilder[stringBuilder.Length - 1]))
					{
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
					}
				}
				if (!flag && token == ".L")
				{
					text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
				}
				stringBuilder.Append(text2);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000186FC File Offset: 0x000168FC
		private bool HandleContractions(string text, string article, int cursorPos, out string newVersion)
		{
			string previousWord = this.GetPreviousWord(text, cursorPos);
			Dictionary<string, string> dictionary;
			if (SpanishTextProcessor.Contractions.TryGetValue(previousWord.ToLower(), out dictionary) && dictionary.TryGetValue(article.TrimEnd(Array.Empty<char>()), out newVersion))
			{
				return true;
			}
			newVersion = string.Empty;
			return false;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00018748 File Offset: 0x00016948
		private string GetPreviousWord(string sourceText, int cursorPos)
		{
			string[] array = sourceText.Substring(0, cursorPos).Split(new char[]
			{
				' '
			});
			int num = array.Length;
			if (num < 2)
			{
				return "";
			}
			return array[num - 2];
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00018781 File Offset: 0x00016981
		public override CultureInfo CultureInfoForLanguage
		{
			get
			{
				return SpanishTextProcessor.CultureInfo;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00018788 File Offset: 0x00016988
		public override void ClearTemporaryData()
		{
			SpanishTextProcessor._curGender = SpanishTextProcessor.WordGenderEnum.NoDeclination;
		}

		// Token: 0x040000F6 RID: 246
		[ThreadStatic]
		private static SpanishTextProcessor.WordGenderEnum _curGender;

		// Token: 0x040000F7 RID: 247
		private static readonly Dictionary<string, Dictionary<string, string>> Contractions = new Dictionary<string, Dictionary<string, string>>
		{
			{
				"de",
				new Dictionary<string, string>
				{
					{
						"el",
						"l "
					}
				}
			},
			{
				"a",
				new Dictionary<string, string>
				{
					{
						"el",
						"l "
					}
				}
			}
		};

		// Token: 0x040000F8 RID: 248
		private static Dictionary<SpanishTextProcessor.WordGenderEnum, string> _genderToDefiniteArticle = new Dictionary<SpanishTextProcessor.WordGenderEnum, string>
		{
			{
				SpanishTextProcessor.WordGenderEnum.MasculineSingular,
				"el "
			},
			{
				SpanishTextProcessor.WordGenderEnum.MasculinePlural,
				"los "
			},
			{
				SpanishTextProcessor.WordGenderEnum.FeminineSingular,
				"la "
			},
			{
				SpanishTextProcessor.WordGenderEnum.FemininePlural,
				"las "
			},
			{
				SpanishTextProcessor.WordGenderEnum.NeuterSingular,
				""
			},
			{
				SpanishTextProcessor.WordGenderEnum.NeuterPlural,
				""
			}
		};

		// Token: 0x040000F9 RID: 249
		private static readonly CultureInfo CultureInfo = new CultureInfo("es-es");

		// Token: 0x0200005D RID: 93
		private enum WordGenderEnum
		{
			// Token: 0x04000231 RID: 561
			MasculineSingular,
			// Token: 0x04000232 RID: 562
			MasculinePlural,
			// Token: 0x04000233 RID: 563
			FeminineSingular,
			// Token: 0x04000234 RID: 564
			FemininePlural,
			// Token: 0x04000235 RID: 565
			NeuterSingular,
			// Token: 0x04000236 RID: 566
			NeuterPlural,
			// Token: 0x04000237 RID: 567
			NoDeclination
		}

		// Token: 0x0200005E RID: 94
		private static class GenderTokens
		{
			// Token: 0x04000238 RID: 568
			public const string MasculineSingular = ".MS";

			// Token: 0x04000239 RID: 569
			public const string MasculinePlural = ".MP";

			// Token: 0x0400023A RID: 570
			public const string FeminineSingular = ".FS";

			// Token: 0x0400023B RID: 571
			public const string FemininePlural = ".FP";

			// Token: 0x0400023C RID: 572
			public const string NeuterSingular = ".NS";

			// Token: 0x0400023D RID: 573
			public const string NeuterPlural = ".NP";

			// Token: 0x0400023E RID: 574
			public static readonly List<string> TokenList = new List<string>
			{
				".MS",
				".FS",
				".NS",
				".MP",
				".FP",
				".NP"
			};
		}

		// Token: 0x0200005F RID: 95
		private static class FunctionTokens
		{
			// Token: 0x0400023F RID: 575
			public const string DefiniteArticle = ".l";

			// Token: 0x04000240 RID: 576
			public const string DefiniteArticleInUpperCase = ".L";
		}
	}
}
