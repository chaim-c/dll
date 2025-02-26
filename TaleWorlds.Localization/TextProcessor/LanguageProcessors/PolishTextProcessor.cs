﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using TaleWorlds.Library;

namespace TaleWorlds.Localization.TextProcessor.LanguageProcessors
{
	// Token: 0x02000035 RID: 53
	public class PolishTextProcessor : LanguageSpecificTextProcessor
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000D070 File Offset: 0x0000B270
		public override CultureInfo CultureInfoForLanguage
		{
			get
			{
				return PolishTextProcessor.CultureInfo;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D077 File Offset: 0x0000B277
		public override void ClearTemporaryData()
		{
			PolishTextProcessor.LinkList.Clear();
			PolishTextProcessor.WordGroups.Clear();
			PolishTextProcessor.WordGroupsNoTags.Clear();
			PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.NoDeclination;
			PolishTextProcessor._doesComeFromWordGroup = false;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000D0A3 File Offset: 0x0000B2A3
		private bool MasculinePersonal
		{
			get
			{
				return PolishTextProcessor._curGender == PolishTextProcessor.WordGenderEnum.MasculinePersonal;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000D0AD File Offset: 0x0000B2AD
		private bool MasculineAnimate
		{
			get
			{
				return PolishTextProcessor._curGender == PolishTextProcessor.WordGenderEnum.MasculineAnimate;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		private bool MasculineInanimate
		{
			get
			{
				return PolishTextProcessor._curGender == PolishTextProcessor.WordGenderEnum.MasculineInanimate;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		private bool Feminine
		{
			get
			{
				return PolishTextProcessor._curGender == PolishTextProcessor.WordGenderEnum.Feminine;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000D0CB File Offset: 0x0000B2CB
		private bool Neuter
		{
			get
			{
				return PolishTextProcessor._curGender == PolishTextProcessor.WordGenderEnum.Neuter;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000D0D5 File Offset: 0x0000B2D5
		[TupleElementNames(new string[]
		{
			"wordGroup",
			"firstMarkerPost"
		})]
		private static List<ValueTuple<string, int>> WordGroups
		{
			[return: TupleElementNames(new string[]
			{
				"wordGroup",
				"firstMarkerPost"
			})]
			get
			{
				if (PolishTextProcessor._wordGroups == null)
				{
					PolishTextProcessor._wordGroups = new List<ValueTuple<string, int>>();
				}
				return PolishTextProcessor._wordGroups;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000D0ED File Offset: 0x0000B2ED
		private static List<string> LinkList
		{
			get
			{
				if (PolishTextProcessor._linkList == null)
				{
					PolishTextProcessor._linkList = new List<string>();
				}
				return PolishTextProcessor._linkList;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D105 File Offset: 0x0000B305
		private static List<string> WordGroupsNoTags
		{
			get
			{
				if (PolishTextProcessor._wordGroupsNoTags == null)
				{
					PolishTextProcessor._wordGroupsNoTags = new List<string>();
				}
				return PolishTextProcessor._wordGroupsNoTags;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000D11D File Offset: 0x0000B31D
		private string LinkTag
		{
			get
			{
				return ".link";
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D124 File Offset: 0x0000B324
		private int LinkTagLength
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000D127 File Offset: 0x0000B327
		private string LinkStarter
		{
			get
			{
				return "<a style=\"Link.";
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D12E File Offset: 0x0000B32E
		private string LinkEnding
		{
			get
			{
				return "</b></a>";
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000D135 File Offset: 0x0000B335
		private int LinkEndingLength
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D138 File Offset: 0x0000B338
		public override void ProcessToken(string sourceText, ref int cursorPos, string token, StringBuilder outputString)
		{
			bool flag = false;
			if (token == this.LinkTag)
			{
				PolishTextProcessor.LinkList.Add(sourceText.Substring(this.LinkTagLength));
			}
			else if (sourceText.Contains(this.LinkStarter))
			{
				flag = this.IsLink(sourceText, token.Length + 2, cursorPos);
			}
			if (flag)
			{
				cursorPos -= this.LinkEndingLength;
				outputString.Remove(outputString.Length - this.LinkEndingLength, this.LinkEndingLength);
			}
			int num2;
			if (token.EndsWith("Creator"))
			{
				outputString.Append("{" + token.Replace("Creator", "") + "}");
			}
			else if (Array.IndexOf<string>(PolishTextProcessor.GenderTokens.TokenList, token) >= 0)
			{
				if (token == ".MP")
				{
					this.SetMasculinePersonal();
				}
				else if (token == ".MI")
				{
					this.SetMasculineInanimate();
				}
				else if (token == ".MA")
				{
					this.SetMasculineAnimate();
				}
				else if (token == ".F")
				{
					this.SetFeminine();
				}
				else if (token == ".N")
				{
					this.SetNeuter();
				}
				if (cursorPos == token.Length + 2 && sourceText.IndexOf("{.", cursorPos, StringComparison.InvariantCulture) == -1 && sourceText.IndexOf(' ', cursorPos) == -1)
				{
					PolishTextProcessor.WordGroups.Add(new ValueTuple<string, int>(sourceText + "{.nn}", cursorPos));
					PolishTextProcessor.WordGroupsNoTags.Add(sourceText.Substring(cursorPos));
				}
			}
			else if (token == ".nnp" || token == ".ajp" || token == ".aj" || token == ".nn")
			{
				if (token == ".nnp" || token == ".ajp" || token == ".aj")
				{
					string value;
					int num;
					if (this.IsIrregularWord(sourceText, cursorPos, token, out value, out num))
					{
						outputString.Remove(outputString.Length - num, num);
						outputString.Append(value);
					}
					else if (token == ".nnp")
					{
						this.AddSuffixNounNominativePlural(outputString);
					}
					else if (token == ".ajp")
					{
						this.AddSuffixAdjectiveNominativePlural(outputString);
					}
					else if (token == ".aj")
					{
						this.AddSuffixAdjectiveNominative(outputString);
					}
				}
				PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.NoDeclination;
				this.WordGroupProcessor(sourceText, cursorPos);
			}
			else if (Array.IndexOf<string>(PolishTextProcessor.NounTokens.TokenList, token) >= 0 && (!PolishTextProcessor._doesComeFromWordGroup || (PolishTextProcessor._doesComeFromWordGroup && PolishTextProcessor._curGender == PolishTextProcessor.WordGenderEnum.NoDeclination)) && this.IsWordGroup(token.Length, sourceText, cursorPos, out num2))
			{
				if (num2 >= 0)
				{
					token = "{" + token + "}";
					PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.NoDeclination;
					this.AddSuffixWordGroup(token, num2, outputString);
				}
			}
			else if (PolishTextProcessor._curGender != PolishTextProcessor.WordGenderEnum.NoDeclination)
			{
				string value2;
				int num3;
				if (this.IsIrregularWord(sourceText, cursorPos, token, out value2, out num3))
				{
					outputString.Remove(outputString.Length - num3, num3);
					outputString.Append(value2);
				}
				else if (token == ".p")
				{
					this.AddSuffixNounNominativePlural(outputString);
				}
				else if (token == ".a")
				{
					this.AddSuffixNounAccusative(outputString);
				}
				else if (token == ".ap")
				{
					this.AddSuffixNounAccusativePlural(outputString);
				}
				else if (token == ".v")
				{
					this.AddSuffixNounVocative(outputString);
				}
				else if (token == ".vp")
				{
					this.AddSuffixNounVocativePlural(outputString);
				}
				else if (token == ".g")
				{
					this.AddSuffixNounGenitive(outputString);
				}
				else if (token == ".gp")
				{
					this.AddSuffixNounGenitivePlural(outputString);
				}
				else if (token == ".d")
				{
					this.AddSuffixNounDative(outputString);
				}
				else if (token == ".dp")
				{
					this.AddSuffixNounDativePlural(outputString);
				}
				else if (token == ".l")
				{
					this.AddSuffixNounLocative(outputString);
				}
				else if (token == ".lp")
				{
					this.AddSuffixNounLocativePlural(outputString);
				}
				else if (token == ".i")
				{
					this.AddSuffixNounInstrumental(outputString);
				}
				else if (token == ".ip")
				{
					this.AddSuffixNounInstrumentalPlural(outputString);
				}
				else if (token == ".j")
				{
					this.AddSuffixAdjectiveNominative(outputString);
				}
				else if (token == ".jp")
				{
					this.AddSuffixAdjectiveNominativePlural(outputString);
				}
				else if (token == ".ja")
				{
					this.AddSuffixAdjectiveAccusative(outputString);
				}
				else if (token == ".jap")
				{
					this.AddSuffixAdjectiveAccusativePlural(outputString);
				}
				else if (token == ".jv")
				{
					this.AddSuffixAdjectiveVocative(outputString);
				}
				else if (token == ".jvp")
				{
					this.AddSuffixAdjectiveVocativePlural(outputString);
				}
				else if (token == ".jg")
				{
					this.AddSuffixAdjectiveGenitive(outputString);
				}
				else if (token == ".jgp")
				{
					this.AddSuffixAdjectiveGenitivePlural(outputString);
				}
				else if (token == ".jd")
				{
					this.AddSuffixAdjectiveDative(outputString);
				}
				else if (token == ".jdp")
				{
					this.AddSuffixAdjectiveDativePlural(outputString);
				}
				else if (token == ".jl")
				{
					this.AddSuffixAdjectiveLocative(outputString);
				}
				else if (token == ".jlp")
				{
					this.AddSuffixAdjectiveLocativePlural(outputString);
				}
				else if (token == ".ji")
				{
					this.AddSuffixAdjectiveInstrumental(outputString);
				}
				else if (token == ".jip")
				{
					this.AddSuffixAdjectiveInstrumentalPlural(outputString);
				}
				PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.NoDeclination;
			}
			if (flag)
			{
				cursorPos += this.LinkEndingLength;
				outputString.Append(this.LinkEnding);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D720 File Offset: 0x0000B920
		private void AddSuffixWordGroup(string token, int wordGroupIndex, StringBuilder outputString)
		{
			string text = PolishTextProcessor.WordGroups[wordGroupIndex].Item1;
			outputString.Remove(outputString.Length - PolishTextProcessor.WordGroupsNoTags[wordGroupIndex].Length, PolishTextProcessor.WordGroupsNoTags[wordGroupIndex].Length);
			text = text.Replace("{.nn}", token);
			if (token.Equals("{.n}"))
			{
				text = text.Replace("{.nnp}", "{.p}");
				text = text.Replace("{.ajp}", "{.jp}");
				text = text.Replace("{.aj}", "{.j}");
			}
			else
			{
				text = text.Replace("{.aj}", token.Insert(2, "j"));
				if (token.Contains("p"))
				{
					text = text.Replace("{.nnp}", token);
					text = text.Replace("{.ajp}", token.Insert(2, "j"));
				}
				else
				{
					text = text.Replace("{.nnp}", token.Insert(3, "p"));
					text = text.Replace("{.ajp}", token.Insert(2, "j").Insert(4, "p"));
				}
			}
			PolishTextProcessor._doesComeFromWordGroup = true;
			string text2 = base.Process(text);
			bool flag = char.IsUpper(text2[0]);
			PolishTextProcessor._doesComeFromWordGroup = false;
			if (flag && char.IsLower(text2[0]))
			{
				outputString.Append(char.ToUpperInvariant(text2[0]));
				outputString.Append(text2.Substring(1));
				return;
			}
			if (!flag && char.IsUpper(text2[0]))
			{
				outputString.Append(char.ToLowerInvariant(text2[0]));
				outputString.Append(text2.Substring(1));
				return;
			}
			outputString.Append(text2);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		private bool IsWordGroup(int tokenLength, string sourceText, int curPos, out int wordGroupIndex)
		{
			int num = 0;
			wordGroupIndex = -1;
			int num2 = PolishTextProcessor.WordGroupsNoTags.Count - 1;
			while (0 <= num2)
			{
				if (curPos - tokenLength - 2 - PolishTextProcessor.WordGroupsNoTags[num2].Length >= 0 && PolishTextProcessor.WordGroupsNoTags[num2].Length > num && sourceText.Substring(curPos - tokenLength - 2 - PolishTextProcessor.WordGroupsNoTags[num2].Length, PolishTextProcessor.WordGroupsNoTags[num2].Length).Equals(PolishTextProcessor.WordGroupsNoTags[num2]))
				{
					wordGroupIndex = num2;
					num = PolishTextProcessor.WordGroupsNoTags[num2].Length;
				}
				num2--;
			}
			return num > 0;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D988 File Offset: 0x0000BB88
		private void AddSuffixNounNominativePlural(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Neuter)
			{
				if (PolishTextProcessor.IsVowel(ending[1]))
				{
					outputString.Remove(outputString.Length - 1, 1);
				}
				if (!PolishTextProcessor.GetEnding(outputString, 2).Equals("um"))
				{
					outputString.Append('a');
					return;
				}
			}
			else if (this.Feminine || this.MasculineAnimate || this.MasculineInanimate || PolishTextProcessor.GetLastCharacter(outputString) == 'a')
			{
				if (ending[1] == 'a' && ending[0] != 't')
				{
					outputString.Remove(outputString.Length - 1, 1);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (ending.Equals("ta"))
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("ci");
					return;
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("ość"))
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append("ci");
					return;
				}
				if (this.MasculineInanimate && ending.Equals("to"))
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append('a');
					return;
				}
				if (ending.Equals("ch"))
				{
					outputString.Append("y");
					return;
				}
				if (ending[1] == 'g' || ending[1] == 'k')
				{
					outputString.Append('i');
					return;
				}
				if (outputString.Length > 4 && PolishTextProcessor.GetEnding(outputString, 5).Equals("rzecz"))
				{
					outputString.Append('y');
					return;
				}
				if (!PolishTextProcessor.IsVowel(ending[1]) && !PolishTextProcessor.IsSoftConsonant(ending) && !PolishTextProcessor.IsHardenedConsonant(ending))
				{
					outputString.Append('y');
					return;
				}
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
					outputString.Append("e");
					return;
				}
				outputString.Append('e');
				return;
			}
			else
			{
				if (ending.Equals("ch"))
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("si");
					return;
				}
				if (ending[1] == 'g')
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append("dzy");
					return;
				}
				if (ending[1] == 'k')
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append("cy");
					return;
				}
				if (ending[1] == 'r')
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append("rzy");
					return;
				}
				if (ending.Equals("ec"))
				{
					outputString.Append("y");
					return;
				}
				if (ending[1] == 't')
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append("ci");
					return;
				}
				if (PolishTextProcessor.IsSoftConsonant(ending) || PolishTextProcessor.IsHardenedConsonant(ending))
				{
					if (PolishTextProcessor.IsSoftConsonant(ending))
					{
						PolishTextProcessor.PalatalizeConsonant(outputString, ending);
						outputString.Append("e");
						return;
					}
					outputString.Append('e');
					return;
				}
				else
				{
					outputString.Append('i');
				}
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000DD48 File Offset: 0x0000BF48
		private void AddSuffixNounAccusative(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
				if (PolishTextProcessor.IsVowel(ending[1]))
				{
					if (ending[1] != 'a')
					{
						outputString.Append('a');
						return;
					}
				}
				else
				{
					if (PolishTextProcessor.IsSoftConsonant(ending))
					{
						PolishTextProcessor.PalatalizeConsonant(outputString, ending);
						outputString.Append("a");
						return;
					}
					outputString.Append('a');
					return;
				}
			}
			else if (this.Feminine && PolishTextProcessor.IsVowel(ending[1]))
			{
				outputString.Remove(outputString.Length - 1, 1);
				outputString.Append('ę');
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DE88 File Offset: 0x0000C088
		private void AddSuffixNounAccusativePlural(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Neuter)
			{
				if (PolishTextProcessor.IsVowel(ending[1]))
				{
					outputString.Remove(outputString.Length - 1, 1);
				}
				if (!PolishTextProcessor.GetEnding(outputString, 2).Equals("um"))
				{
					outputString.Append('a');
					return;
				}
			}
			else if (this.Feminine || this.MasculineAnimate || this.MasculineInanimate || PolishTextProcessor.GetLastCharacter(outputString) == 'a')
			{
				if (ending[1] == 'a')
				{
					outputString.Remove(outputString.Length - 1, 1);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("ość"))
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append("ci");
					return;
				}
				if (this.MasculineInanimate && ending.Equals("to"))
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append('a');
					return;
				}
				if (ending[1] == 'g' || ending[1] == 'k')
				{
					outputString.Append('i');
					return;
				}
				if (outputString.Length > 4 && PolishTextProcessor.GetEnding(outputString, 5).Equals("rzecz"))
				{
					outputString.Append('y');
					return;
				}
				if (PolishTextProcessor.IsVowel(ending[1]) || PolishTextProcessor.IsSoftConsonant(ending) || PolishTextProcessor.IsHardenedConsonant(ending))
				{
					outputString.Append('e');
					return;
				}
				outputString.Append('y');
				return;
			}
			else
			{
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
					outputString.Append("ów");
					return;
				}
				outputString.Append("ów");
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		private void AddSuffixNounGenitive(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Feminine)
			{
				if (ending[1] == 'a')
				{
					outputString.Remove(outputString.Length - 1, 1);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (!PolishTextProcessor.IsVowel(ending[1]))
				{
					if (PolishTextProcessor.IsSoftConsonant(ending))
					{
						PolishTextProcessor.PalatalizeConsonant(outputString, ending);
						return;
					}
					if (ending[1] == 'g' || ending[1] == 'k' || PolishTextProcessor.GetEnding(outputString, 2).Equals("ch"))
					{
						outputString.Append('i');
						return;
					}
					outputString.Append('y');
					return;
				}
			}
			else
			{
				if (PolishTextProcessor.IsVowel(ending[1]))
				{
					outputString.Remove(outputString.Length - 1, 1);
				}
				if (!PolishTextProcessor.GetEnding(outputString, 2).Equals("um"))
				{
					if (PolishTextProcessor.IsSoftConsonant(ending))
					{
						PolishTextProcessor.PalatalizeConsonant(outputString, ending);
					}
					outputString.Append('a');
				}
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E280 File Offset: 0x0000C480
		private void AddSuffixNounGenitivePlural(StringBuilder outputString)
		{
			if (PolishTextProcessor.IsVowel(PolishTextProcessor.GetLastCharacter(outputString)))
			{
				outputString.Remove(outputString.Length - 1, 1);
			}
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Feminine && PolishTextProcessor.IsHardenedConsonant(ending))
			{
				outputString.Append('y');
				return;
			}
			if (this.MasculinePersonal)
			{
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				}
				outputString.Append("ów");
				return;
			}
			if (this.MasculineAnimate || this.MasculineInanimate)
			{
				if (this.MasculineInanimate && ending.Equals("to"))
				{
					outputString.Remove(outputString.Length - 1, 1);
					return;
				}
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					outputString.Append('i');
					return;
				}
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				}
				outputString.Append("ów");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E408 File Offset: 0x0000C608
		private void AddSuffixNounDative(StringBuilder outputString)
		{
			char lastCharacter = PolishTextProcessor.GetLastCharacter(outputString);
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Feminine)
			{
				if (lastCharacter == 'a')
				{
					outputString.Remove(outputString.Length - 1, 1);
					lastCharacter = PolishTextProcessor.GetLastCharacter(outputString);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (!PolishTextProcessor.IsVowel(lastCharacter))
				{
					if (PolishTextProcessor.IsSoftConsonant(ending))
					{
						PolishTextProcessor.PalatalizeConsonant(outputString, ending);
						return;
					}
					if (PolishTextProcessor.IsHardenedConsonant(ending))
					{
						outputString.Append('y');
						return;
					}
					if (ending.Equals("ch"))
					{
						outputString.Remove(outputString.Length - 2, 2);
						outputString.Append("szie");
						return;
					}
					if (lastCharacter == 'g')
					{
						outputString.Remove(outputString.Length - 1, 1);
						outputString.Append("dzie");
						return;
					}
					if (lastCharacter == 'k')
					{
						outputString.Remove(outputString.Length - 1, 1);
						outputString.Append("cie");
						return;
					}
					outputString.Append("ie");
					return;
				}
			}
			else if (this.Neuter)
			{
				if (PolishTextProcessor.IsVowel(lastCharacter))
				{
					outputString.Remove(outputString.Length - 1, 1);
				}
				if (!PolishTextProcessor.GetEnding(outputString, 2).Equals("um"))
				{
					outputString.Append('u');
					return;
				}
			}
			else
			{
				if (this.MasculineInanimate && ending.Equals("to"))
				{
					outputString.Remove(outputString.Length - 1, 1);
					outputString.Append('u');
					return;
				}
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				}
				outputString.Append("owi");
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E638 File Offset: 0x0000C838
		private void AddSuffixNounDativePlural(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (PolishTextProcessor.IsVowel(ending[1]))
			{
				outputString.Remove(outputString.Length - 1, 1);
			}
			if (!ending.Equals("um"))
			{
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				}
				outputString.Append("om");
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E748 File Offset: 0x0000C948
		private void AddSuffixNounLocative(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Feminine)
			{
				if (ending[1] == 'a')
				{
					outputString.Remove(outputString.Length - 1, 1);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (!PolishTextProcessor.IsVowel(ending[1]))
				{
					if (PolishTextProcessor.IsSoftConsonant(ending))
					{
						outputString.Append('i');
						return;
					}
					if (PolishTextProcessor.IsHardenedConsonant(ending))
					{
						outputString.Append('y');
						return;
					}
					if (ending.Equals("ch"))
					{
						outputString.Remove(outputString.Length - 2, 2);
						outputString.Append("szie");
						return;
					}
					if (ending[1] == 'g')
					{
						outputString.Remove(outputString.Length - 1, 1);
						outputString.Append("dzie");
						return;
					}
					if (ending[1] == 'k')
					{
						outputString.Remove(outputString.Length - 1, 1);
						outputString.Append("cie");
						return;
					}
					outputString.Append("ie");
					return;
				}
				else
				{
					if (PolishTextProcessor.IsVowel(ending[1]))
					{
						outputString.Remove(outputString.Length - 1, 1);
					}
					if (PolishTextProcessor.IsSoftConsonant(ending) || PolishTextProcessor.IsHardenedConsonant(ending) || ending.Equals("ch") || ending[1] == 'g' || ending[1] == 'k')
					{
						outputString.Append('u');
						return;
					}
					outputString.Append('e');
					return;
				}
			}
			else
			{
				if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
				{
					this.AddSuffixNounVocative(outputString);
					return;
				}
				if (PolishTextProcessor.IsVowel(ending[1]))
				{
					outputString.Remove(outputString.Length - 1, 1);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (PolishTextProcessor.IsVowel(ending[1]) || ending.Equals("um"))
				{
					outputString.Append("u");
					return;
				}
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
					outputString.Append("u");
					return;
				}
				if (PolishTextProcessor.IsHardenedConsonant(ending) || ending.Equals("ch") || ending[1] == 'g' || ending[1] == 'k')
				{
					outputString.Append("u");
					return;
				}
				PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				outputString.Append("e");
				return;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000EA38 File Offset: 0x0000CC38
		private void AddSuffixNounLocativePlural(StringBuilder outputString)
		{
			if (PolishTextProcessor.IsVowel(PolishTextProcessor.GetLastCharacter(outputString)))
			{
				outputString.Remove(outputString.Length - 1, 1);
			}
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (PolishTextProcessor.IsSoftConsonant(ending))
			{
				PolishTextProcessor.PalatalizeConsonant(outputString, ending);
			}
			outputString.Append("ach");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		private void AddSuffixNounVocative(StringBuilder outputString)
		{
			char lastCharacter = PolishTextProcessor.GetLastCharacter(outputString);
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
					outputString.Append("z");
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
			}
			if (!this.Feminine && lastCharacter != 'a')
			{
				if (!this.Neuter)
				{
					if (PolishTextProcessor.IsSoftConsonant(ending) || ending.Equals("ch") || lastCharacter == 'g' || lastCharacter == 'k')
					{
						if (PolishTextProcessor.IsSoftConsonant(ending))
						{
							PolishTextProcessor.PalatalizeConsonant(outputString, ending);
						}
						outputString.Append("u");
						return;
					}
					if (PolishTextProcessor.IsHardConsonant(ending))
					{
						if (!PolishTextProcessor.IsHardenedConsonant(ending))
						{
							PolishTextProcessor.PalatalizeConsonant(outputString, ending);
						}
						outputString.Append("e");
						return;
					}
					outputString.Append("u");
				}
				return;
			}
			if (lastCharacter == 'a')
			{
				outputString.Remove(outputString.Length - 1, 1);
				outputString.Append('o');
				return;
			}
			if (PolishTextProcessor.IsSoftConsonant(ending))
			{
				PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				return;
			}
			outputString.Append('i');
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		private void AddSuffixNounVocativePlural(StringBuilder outputString)
		{
			this.AddSuffixNounNominativePlural(outputString);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private void AddSuffixNounInstrumental(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (this.Feminine)
			{
				if (ending[1] == 'a')
				{
					outputString.Remove(outputString.Length - 1, 1);
					ending = PolishTextProcessor.GetEnding(outputString, 2);
				}
				if (PolishTextProcessor.IsSoftConsonant(ending))
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				}
				outputString.Append('ą');
				return;
			}
			if (PolishTextProcessor.IsVowel(ending[1]))
			{
				outputString.Remove(outputString.Length - 1, 1);
				ending = PolishTextProcessor.GetEnding(outputString, 2);
			}
			if (!ending.Equals("um"))
			{
				if (PolishTextProcessor.IsSoftConsonant(ending) || ending[1] == 'k')
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
				}
				outputString.Append("em");
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000EE50 File Offset: 0x0000D050
		private void AddSuffixNounInstrumentalPlural(StringBuilder outputString)
		{
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			if (this.MasculineAnimate || this.MasculineInanimate || this.MasculinePersonal)
			{
				if (!this.MasculinePersonal && ending[0] == 'ó')
				{
					outputString.Remove(outputString.Length - 2, 2);
					outputString.Append("o" + ending[1].ToString());
				}
				if (PolishTextProcessor.GetEnding(outputString, 3).Equals("iec"))
				{
					if (PolishTextProcessor.GetEnding(outputString, 4).Equals("niec"))
					{
						outputString.Remove(outputString.Length - 4, 4);
						outputString.Append("ńc");
					}
					else
					{
						outputString.Remove(outputString.Length - 3, 2);
					}
				}
			}
			if (PolishTextProcessor.IsVowel(ending[1]))
			{
				outputString.Remove(outputString.Length - 1, 1);
				ending = PolishTextProcessor.GetEnding(outputString, 2);
			}
			if (PolishTextProcessor.IsSoftConsonant(ending))
			{
				PolishTextProcessor.PalatalizeConsonant(outputString, ending);
			}
			outputString.Append("ami");
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000EF5C File Offset: 0x0000D15C
		private void AddSuffixAdjectiveNominative(StringBuilder outputString)
		{
			char c = this.RemoveSuffixFromAdjective(outputString);
			if (this.Feminine)
			{
				outputString.Append('a');
				return;
			}
			if (this.Neuter)
			{
				outputString.Append('e');
				return;
			}
			if (c == 'y' && (outputString.Length < 4 || !outputString.ToString().EndsWith("Nasz", true, CultureInfo.InvariantCulture)))
			{
				outputString.Append('y');
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		private void AddSuffixAdjectiveNominativePlural(StringBuilder outputString)
		{
			char c = this.RemoveSuffixFromAdjective(outputString);
			string ending = PolishTextProcessor.GetEnding(outputString, 2);
			string text = outputString.ToString();
			if (outputString.Length >= 4 && text.EndsWith("Nasz", true, CultureInfo.InvariantCulture))
			{
				outputString.Remove(outputString.Length - 1, 1);
				outputString.Append('i');
				return;
			}
			if (this.MasculinePersonal)
			{
				if (c == 'y')
				{
					PolishTextProcessor.PalatalizeConsonant(outputString, ending);
					text = outputString.ToString();
					if ((outputString.Length >= 10 && text.EndsWith("Wyszkoloni", true, CultureInfo.InvariantCulture)) || (outputString.Length >= 11 && text.EndsWith("Zgromadzoni", true, CultureInfo.InvariantCulture)))
					{
						outputString.Replace('o', 'e', outputString.Length - 3, 1);
						return;
					}
					if (PolishTextProcessor.GetLastCharacter(outputString) == 'l')
					{
						outputString.Append('i');
						return;
					}
					if (!PolishTextProcessor.IsVowel(PolishTextProcessor.GetLastCharacter(outputString)))
					{
						outputString.Append('y');
						return;
					}
				}
			}
			else
			{
				outputString.Append('e');
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		private void AddSuffixAdjectiveAccusative(StringBuilder outputString)
		{
			this.RemoveSuffixFromAdjective(outputString);
			if (this.Feminine)
			{
				outputString.Append('ą');
				return;
			}
			if (this.Neuter)
			{
				outputString.Append('e');
				return;
			}
			if (this.MasculineAnimate || this.MasculinePersonal)
			{
				outputString.Append("ego");
				return;
			}
			if (this.MasculineInanimate)
			{
				outputString.Append('y');
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000F130 File Offset: 0x0000D330
		private void AddSuffixAdjectiveAccusativePlural(StringBuilder outputString)
		{
			char c = this.RemoveSuffixFromAdjective(outputString);
			if (this.MasculinePersonal)
			{
				if (c == 'y')
				{
					outputString.Append("y");
				}
				outputString.Append("ch");
				return;
			}
			outputString.Append('e');
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000F174 File Offset: 0x0000D374
		private void AddSuffixAdjectiveVocative(StringBuilder outputString)
		{
			this.AddSuffixAdjectiveNominative(outputString);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000F17D File Offset: 0x0000D37D
		private void AddSuffixAdjectiveVocativePlural(StringBuilder outputString)
		{
			this.AddSuffixAdjectiveNominativePlural(outputString);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000F186 File Offset: 0x0000D386
		private void AddSuffixAdjectiveGenitive(StringBuilder outputString)
		{
			this.RemoveSuffixFromAdjective(outputString);
			if (this.Feminine)
			{
				outputString.Append("ej");
				return;
			}
			outputString.Append("ego");
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000F1B1 File Offset: 0x0000D3B1
		private void AddSuffixAdjectiveGenitivePlural(StringBuilder outputString)
		{
			if ('y' == this.RemoveSuffixFromAdjective(outputString))
			{
				outputString.Append("y");
			}
			outputString.Append("ch");
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000F1D6 File Offset: 0x0000D3D6
		private void AddSuffixAdjectiveDative(StringBuilder outputString)
		{
			this.RemoveSuffixFromAdjective(outputString);
			if (this.Feminine)
			{
				outputString.Append("ej");
				return;
			}
			outputString.Append("emu");
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000F201 File Offset: 0x0000D401
		private void AddSuffixAdjectiveDativePlural(StringBuilder outputString)
		{
			if ('y' == this.RemoveSuffixFromAdjective(outputString))
			{
				outputString.Append("y");
			}
			outputString.Append("m");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000F228 File Offset: 0x0000D428
		private void AddSuffixAdjectiveLocative(StringBuilder outputString)
		{
			char c = this.RemoveSuffixFromAdjective(outputString);
			if (this.Feminine)
			{
				outputString.Append("ej");
				return;
			}
			if (c == 'y')
			{
				outputString.Append("y");
			}
			outputString.Append("m");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000F26F File Offset: 0x0000D46F
		private void AddSuffixAdjectiveLocativePlural(StringBuilder outputString)
		{
			this.AddSuffixAdjectiveGenitivePlural(outputString);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000F278 File Offset: 0x0000D478
		private void AddSuffixAdjectiveInstrumental(StringBuilder outputString)
		{
			char c = this.RemoveSuffixFromAdjective(outputString);
			if (this.Feminine)
			{
				outputString.Append('ą');
				return;
			}
			if (c == 'y')
			{
				outputString.Append("y");
			}
			outputString.Append("m");
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		private void AddSuffixAdjectiveInstrumentalPlural(StringBuilder outputString)
		{
			if ('y' == this.RemoveSuffixFromAdjective(outputString))
			{
				outputString.Append("y");
			}
			outputString.Append("mi");
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		private char RemoveSuffixFromAdjective(StringBuilder outputString)
		{
			if (PolishTextProcessor.GetLastCharacter(outputString) == 'i')
			{
				return 'i';
			}
			if (outputString.Length >= 4 && outputString.ToString().EndsWith("Nasz", true, CultureInfo.InvariantCulture))
			{
				return 'y';
			}
			outputString.Remove(outputString.Length - 1, 1);
			if (PolishTextProcessor.GetLastCharacter(outputString) == 'i')
			{
				return 'i';
			}
			return 'y';
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000F341 File Offset: 0x0000D541
		private void SetFeminine()
		{
			PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.Feminine;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000F349 File Offset: 0x0000D549
		private void SetNeuter()
		{
			PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.Neuter;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000F351 File Offset: 0x0000D551
		private void SetMasculineAnimate()
		{
			PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.MasculineAnimate;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000F359 File Offset: 0x0000D559
		private void SetMasculineInanimate()
		{
			PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.MasculineInanimate;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000F361 File Offset: 0x0000D561
		private void SetMasculinePersonal()
		{
			PolishTextProcessor._curGender = PolishTextProcessor.WordGenderEnum.MasculinePersonal;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000F36C File Offset: 0x0000D56C
		private bool IsRecordedWithPreviousTag(string sourceText, int cursorPos)
		{
			for (int i = 0; i < PolishTextProcessor.WordGroups.Count; i++)
			{
				if (PolishTextProcessor.WordGroups[i].Item1 == sourceText && PolishTextProcessor.WordGroups[i].Item2 != cursorPos)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000F3BC File Offset: 0x0000D5BC
		private void WordGroupProcessor(string sourceText, int cursorPos)
		{
			if (!this.IsRecordedWithPreviousTag(sourceText, cursorPos))
			{
				PolishTextProcessor.WordGroups.Add(new ValueTuple<string, int>(sourceText, cursorPos));
				string text = sourceText.Replace("{.nnp}", "{.p}");
				text = text.Replace("{.ajp}", "{.jp}");
				text = text.Replace("{.nn}", "{.n}");
				text = text.Replace("{.aj}", "{.j}");
				PolishTextProcessor._doesComeFromWordGroup = true;
				PolishTextProcessor.WordGroupsNoTags.Add(base.Process(text));
				PolishTextProcessor._doesComeFromWordGroup = false;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000F448 File Offset: 0x0000D648
		private bool IsLink(string sourceText, int tokenLength, int cursorPos)
		{
			string text = sourceText.Remove(cursorPos - tokenLength);
			for (int i = 0; i < PolishTextProcessor.LinkList.Count; i++)
			{
				if (sourceText.Length >= PolishTextProcessor.LinkList[i].Length && text.EndsWith(PolishTextProcessor.LinkList[i]))
				{
					PolishTextProcessor.LinkList.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		private bool IsIrregularWord(string sourceText, int cursorPos, string token, out string irregularWord, out int lengthOfWordToReplace)
		{
			int num = sourceText.Remove(cursorPos - token.Length - 2).LastIndexOf('}') + 1;
			lengthOfWordToReplace = cursorPos - token.Length - 2 - num;
			irregularWord = "";
			string text = "";
			if (lengthOfWordToReplace > 0)
			{
				text = sourceText.Substring(num, lengthOfWordToReplace);
				char key = char.ToUpperInvariant(text[0]);
				List<PolishTextProcessor.IrregularWord> list;
				if (this.MasculinePersonal && PolishTextProcessor.IrregularMasculinePersonalDictionary.TryGetValue(key, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].Nominative.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							irregularWord = PolishTextProcessor.IrregularWordWithCase(token, list[i]);
							break;
						}
					}
				}
				else if (this.MasculineAnimate && PolishTextProcessor.IrregularMasculineAnimateDictionary.TryGetValue(key, out list))
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].Nominative.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							irregularWord = PolishTextProcessor.IrregularWordWithCase(token, list[j]);
							break;
						}
					}
				}
				else if (this.MasculineInanimate && PolishTextProcessor.IrregularMasculineInanimateDictionary.TryGetValue(key, out list))
				{
					for (int k = 0; k < list.Count; k++)
					{
						if (list[k].Nominative.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							irregularWord = PolishTextProcessor.IrregularWordWithCase(token, list[k]);
							break;
						}
					}
				}
				else if (this.Feminine && PolishTextProcessor.IrregularFeminineDictionary.TryGetValue(key, out list))
				{
					for (int l = 0; l < list.Count; l++)
					{
						if (list[l].Nominative.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							irregularWord = PolishTextProcessor.IrregularWordWithCase(token, list[l]);
							break;
						}
					}
				}
				else if (this.Neuter && PolishTextProcessor.IrregularNeuterDictionary.TryGetValue(key, out list))
				{
					for (int m = 0; m < list.Count; m++)
					{
						if (list[m].Nominative.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							irregularWord = PolishTextProcessor.IrregularWordWithCase(token, list[m]);
							break;
						}
					}
				}
			}
			if (irregularWord.Length > 0)
			{
				if (char.IsLower(text[0]))
				{
					irregularWord = irregularWord.ToLower();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000F6FF File Offset: 0x0000D8FF
		private static bool IsVowel(char c)
		{
			return Array.IndexOf<char>(PolishTextProcessor.Vowels, c) >= 0;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000F712 File Offset: 0x0000D912
		private static bool IsSoftConsonant(string s)
		{
			return Array.IndexOf<char>(PolishTextProcessor.SoftConsonants, s[1]) >= 0;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000F72C File Offset: 0x0000D92C
		private static bool IsHardenedConsonant(string s)
		{
			return Array.IndexOf<string>(PolishTextProcessor.HardenedConsonants, s) >= 0 || Array.IndexOf<string>(PolishTextProcessor.HardenedConsonants, s[1].ToString()) >= 0;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000F768 File Offset: 0x0000D968
		private static bool IsHardConsonant(string s)
		{
			return Array.IndexOf<string>(PolishTextProcessor.HardConsonants, s) >= 0 || Array.IndexOf<string>(PolishTextProcessor.HardConsonants, s[1].ToString()) >= 0;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		private static char GetLastCharacter(StringBuilder outputString)
		{
			if (outputString.Length <= 0)
			{
				return '*';
			}
			return outputString[outputString.Length - 1];
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		private static string GetEnding(StringBuilder outputString, int numChars)
		{
			numChars = MathF.Min(numChars, outputString.Length);
			return outputString.ToString(outputString.Length - numChars, numChars);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		private static void PalatalizeConsonant(StringBuilder outputString, string lastTwoCharacters)
		{
			int num = 1;
			string value;
			if (!PolishTextProcessor.Palatalization.TryGetValue(lastTwoCharacters[1].ToString(), out value) && PolishTextProcessor.Palatalization.TryGetValue(lastTwoCharacters, out value))
			{
				num = 2;
				value = "";
			}
			outputString.Remove(outputString.Length - num, num);
			outputString.Append(value);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000F83C File Offset: 0x0000DA3C
		private static string IrregularWordWithCase(string token, PolishTextProcessor.IrregularWord irregularWord)
		{
			if (token == ".j" || token == ".aj" || token == ".nn")
			{
				return irregularWord.Nominative;
			}
			if (token == ".jp" || token == ".p" || token == ".nnp" || token == ".ajp")
			{
				return irregularWord.NominativePlural;
			}
			if (token == ".jvp" || token == ".vp")
			{
				return irregularWord.VocativePlural;
			}
			if (token == ".ja" || token == ".a")
			{
				return irregularWord.Accusative;
			}
			if (token == ".jap" || token == ".ap")
			{
				return irregularWord.AccusativePlural;
			}
			if (token == ".jg" || token == ".g")
			{
				return irregularWord.Genitive;
			}
			if (token == ".jgp" || token == ".gp")
			{
				return irregularWord.GenitivePlural;
			}
			if (token == ".jd" || token == ".d")
			{
				return irregularWord.Dative;
			}
			if (token == ".jdp" || token == ".dp")
			{
				return irregularWord.DativePlural;
			}
			if (token == ".jl" || token == ".l")
			{
				return irregularWord.Locative;
			}
			if (token == ".jv" || token == ".v")
			{
				return irregularWord.Vocative;
			}
			if (token == ".jlp" || token == ".lp")
			{
				return irregularWord.LocativePlural;
			}
			if (token == ".ji" || token == ".i")
			{
				return irregularWord.Instrumental;
			}
			if (token == ".jip" || token == ".ip")
			{
				return irregularWord.InstrumentalPlural;
			}
			return "";
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000FA44 File Offset: 0x0000DC44
		public static string[] GetProcessedNouns(string str, string gender, string[] tokens = null)
		{
			if (tokens == null)
			{
				tokens = new string[]
				{
					".n",
					".g",
					".d",
					".a",
					".i",
					".l",
					".v",
					".p",
					".gp",
					".dp",
					".ap",
					".ip",
					".lp",
					".vp"
				};
			}
			List<string> list = new List<string>();
			PolishTextProcessor polishTextProcessor = new PolishTextProcessor();
			foreach (string text in tokens)
			{
				string text2 = string.Concat(new string[]
				{
					"{=!}",
					gender,
					str,
					"{",
					text,
					"}"
				});
				list.Add(polishTextProcessor.Process(text2));
			}
			return list.ToArray();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public static string[] GetProcessedAdjectives(string str, string gender, string[] tokens = null)
		{
			if (tokens == null)
			{
				tokens = PolishTextProcessor.AdjectiveTokens.TokenList;
			}
			List<string> list = new List<string>();
			PolishTextProcessor polishTextProcessor = new PolishTextProcessor();
			foreach (string text in tokens)
			{
				string text2 = string.Concat(new string[]
				{
					"{=!}",
					gender,
					str,
					"{",
					text,
					"}"
				});
				list.Add(polishTextProcessor.Process(text2));
			}
			return list.ToArray();
		}

		// Token: 0x040000D4 RID: 212
		private static readonly CultureInfo CultureInfo = new CultureInfo("pl-PL");

		// Token: 0x040000D5 RID: 213
		[ThreadStatic]
		private static PolishTextProcessor.WordGenderEnum _curGender = PolishTextProcessor.WordGenderEnum.NoDeclination;

		// Token: 0x040000D6 RID: 214
		[TupleElementNames(new string[]
		{
			"wordGroup",
			"firstMarkerPost"
		})]
		[ThreadStatic]
		private static List<ValueTuple<string, int>> _wordGroups;

		// Token: 0x040000D7 RID: 215
		[ThreadStatic]
		private static List<string> _wordGroupsNoTags;

		// Token: 0x040000D8 RID: 216
		[ThreadStatic]
		private static List<string> _linkList;

		// Token: 0x040000D9 RID: 217
		[ThreadStatic]
		private static bool _doesComeFromWordGroup = false;

		// Token: 0x040000DA RID: 218
		private static readonly char[] Vowels = new char[]
		{
			'a',
			'ą',
			'e',
			'ę',
			'i',
			'o',
			'ó',
			'u',
			'y'
		};

		// Token: 0x040000DB RID: 219
		private static readonly char[] SoftConsonants = new char[]
		{
			'ć',
			'ń',
			'ś',
			'ź',
			'j'
		};

		// Token: 0x040000DC RID: 220
		private static readonly string[] HardenedConsonants = new string[]
		{
			"dz",
			"c",
			"sz",
			"rz",
			"cz",
			"ż"
		};

		// Token: 0x040000DD RID: 221
		private static readonly string[] HardConsonants = new string[]
		{
			"g",
			"k",
			"ch",
			"r",
			"w",
			"f",
			"p",
			"m",
			"b",
			"d",
			"t",
			"n",
			"s",
			"z",
			"ł",
			"h"
		};

		// Token: 0x040000DE RID: 222
		private static readonly Dictionary<string, string> Palatalization = new Dictionary<string, string>
		{
			{
				"g",
				"gi"
			},
			{
				"k",
				"ki"
			},
			{
				"ch",
				"sz"
			},
			{
				"r",
				"rz"
			},
			{
				"w",
				"wi"
			},
			{
				"f",
				"fi"
			},
			{
				"p",
				"pi"
			},
			{
				"m",
				"mi"
			},
			{
				"j",
				"j"
			},
			{
				"b",
				"bi"
			},
			{
				"d",
				"dzi"
			},
			{
				"t",
				"ci"
			},
			{
				"n",
				"ni"
			},
			{
				"s",
				"si"
			},
			{
				"z",
				"zi"
			},
			{
				"ł",
				"l"
			},
			{
				"ś",
				"si"
			},
			{
				"ź",
				"zi"
			},
			{
				"ń",
				"ni"
			},
			{
				"ć",
				"ci"
			}
		};

		// Token: 0x040000DF RID: 223
		private static readonly Dictionary<char, List<PolishTextProcessor.IrregularWord>> IrregularMasculinePersonalDictionary = new Dictionary<char, List<PolishTextProcessor.IrregularWord>>
		{
			{
				'A',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Alary", "Alary", "Alarego", "Alarego", "Alaremu", "Alaremu", "Alarego", "Alarego", "Alarym", "Alarym", "Alarym", "Alarym"),
					new PolishTextProcessor.IrregularWord("Alcza", "Alcza", "Alczy", "Alczy", "Alczy", "Alczy", "Alczę", "Alczę", "Alczą", "Alczą", "Alczy", "Alczy")
				}
			},
			{
				'B',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Bandyta", "Bandyci", "Bandyty", "Bandytów", "Bandycie", "Bandytom", "Bandytę", "Bandytów", "Bandytą", "Bandytami", "Bandycie", "Bandytach"),
					new PolishTextProcessor.IrregularWord("Bat", "Batowie", "Bata", "Batów", "Batowi", "Batom", "Bata", "Batów", "Batem", "Batami", "Bacie", "Batach"),
					new PolishTextProcessor.IrregularWord("Berserk", "Berserkowie", "Berserka", "Berserków", "Berserkowi", "Berserkom", "Berserka", "Berserków", "Berserkiem", "Berserkami", "Berserku", "Berserkach"),
					new PolishTextProcessor.IrregularWord("Bilija", "Bilija", "Bilii", "Bilii", "Bilii", "Bilii", "Biliję", "Biliję", "Biliją", "Biliją", "Bilii", "Bilii"),
					new PolishTextProcessor.IrregularWord("Bohater", "Bohaterowie", "Bohatera", "Bohaterów", "Bohaterowi", "Bohaterom", "Bohatera", "Bohaterów", "Bohateram", "Bohateremi", "Bohaterze", "Bohaterach"),
					new PolishTextProcessor.IrregularWord("Borcza", "Borcza", "Borczy", "Borczy", "Borczy", "Borczy", "Borczę", "Borczę", "Borczą", "Borczą", "Borczy", "Borczy"),
					new PolishTextProcessor.IrregularWord("Botero", "Botero", "Botera", "Botera", "Boterowi", " Boterowi", "Botera", "Botera", "Boterem", "Boterem", "Boterze", "Boterze"),
					new PolishTextProcessor.IrregularWord("Bratanek", "Bratankowie", "Bratanka", "Bratanków", "Bratankowi", "Bratankom", "Bratanka", "Bratanków", "Bratankiem", "Bratankami", "Bratanku", "Bratankach"),
					new PolishTextProcessor.IrregularWord("Budowniczy", "Budowniczowie", "Budowniczego", "Budowniczych", "Budowniczemu", "Budowniczym", "Budowniczego", "Budowniczych", "Budowniczym", "Budowniczymi", "Budowniczym", "Budowniczych")
				}
			},
			{
				'C',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Cesarski", "Cesarscy", "Cesarskiego", "Cesarskich", "Cesarskiemu", "Cesarskim", "Cesarskiego", "Cesarskich", "Cesarskim", "Cesarskimi", "Cesarskim", "Cesarskich"),
					new PolishTextProcessor.IrregularWord("Chłopiec", "Chłopcy", "Chłopca", "Chłopców", "Chłopcu", "Chłopcom", "Chłopca", "Chłopców", "Chłopcem", "Chłopcami", "Chłopca", "Chłopcach"),
					new PolishTextProcessor.IrregularWord("Chorąży", "Chorążowie", "Chorążego", "Chorążych", "Chorążemu", "Chorążym", "Chorążego", "Chorążych", "Chorążym", "Chorążymi", "Chorążym", "Chorążych"),
					new PolishTextProcessor.IrregularWord("Cieśla", "Cieśle", "Cieśli", "Cieśli", "Cieśli", "Cieślom", "Cieślę", "Cieśli", "Cieślą", "Cieślami", "Cieśli", "Cieślach"),
					new PolishTextProcessor.IrregularWord("Członek", "Członkowie", "Członka", "Członków", "Członkowi", "Członkom", "Członka", "Członków", "Członkiem", "Członkami", "Członku", "Członkach"),
					new PolishTextProcessor.IrregularWord("Człowiek", "Ludzie", "Człowieka", "Ludzi", "Człowiekowi", "Ludziom", "Człowieka", "Ludzi", "Człowiekiem", "Ludźmi", "Człowieku", "Ludziach")
				}
			},
			{
				'D',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Dalibol", "Dalibol", "Dalibola", "Dalibola", "Dalibolowi", "Dalibolowi", "Dalibola", "Dalibola", "Dalibolem", "Dalibolem", "Dalibolu", "Dalibolu"),
					new PolishTextProcessor.IrregularWord("Daszwal", "Daszwal", "Daszwala", "Daszwala", "Daszwalowi", "Daszwalowi", "Daszwala", "Daszwala", "Daszwalem", "Daszwalem", "Daszwalu", "Daszwalu"),
					new PolishTextProcessor.IrregularWord("Despota", "Despoci", "Despoty", "Despotów", "Despocie", "Despotom", "Despotę", "Despotów", "Despotą", "Despotami", "Despocie", "Despotach"),
					new PolishTextProcessor.IrregularWord("Dijul", "Dijul", "Dijula", "Dijula", "Dijulowi", "Dijulowi", "Dijula", "Dijula", "Dijulem", "Dijulem", "Dijulu", "Dijulu"),
					new PolishTextProcessor.IrregularWord("Dowódca", "Dowódcy", "Dowódcy", "Dowódców", "Dowódcy", "Dowódcom", "Dowódcę", "Dowódców", "Dowódcą", "Dowódcami", "Dowódcy", "Dowódcach")
				}
			},
			{
				'E',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Eksmałżonek", "Eksmałżonkowie", "Eksmałżonka", "Eksmałżonków", "Eksmałżonkowi", "Eksmałżonkom", "Eksmałżonka", "Eksmałżonków", "Eksmałżonkiem", "Eksmałżonkami", "Eksmałżonku", "Eksmałżonkach"),
					new PolishTextProcessor.IrregularWord("Ekwita", "Ekwici", "Ekwity", "Ekwitów", "Ekwicie", "Ekwitom", "Ekwitę", "Ekwitów", "Ekwitą", "Ekwitami", "Ekwicie", "Ekwitach"),
					new PolishTextProcessor.IrregularWord("Erigaj", "Erigaj", "Erigaja", "Erigaja", "Erigajow", "Erigajowii", "Erigaja", "Erigaja", "Erigajem", "Erigajem", "Erigaju", "Erigaju"),
					new PolishTextProcessor.IrregularWord("Esos", "Esos", "Esosa", "Esosa", "Esosowi", "Esosowi", "Esosa", "Esosa", "Esosem", "Esosem", "Esosie", "Esosie"),
					new PolishTextProcessor.IrregularWord("Elefter", "Elefterowie", "Eleftera", "Elefterów", "Elefterowi", "Elefterom", "Eleftera", "Elefterów", "Elefterem", "Elefterami", "Elefterze", "Elefterach")
				}
			},
			{
				'G',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Garwi", "Garwi", "Garwiego", "Garwiego", "Garwiemu", "Garwiemu", "Garwiego", "Garwiego", "Garwim", "Garwim", "Garwim", "Garwim"),
					new PolishTextProcessor.IrregularWord("Ghilman", "Ghilmanowie", "Ghilmana", "Ghilmanów", "Ghilmanowi", "Ghilmanom", "Ghilmana", "Ghilmanów", "Ghilmanem", "Ghilmanami", "Ghilmanie", "Ghilmanach"),
					new PolishTextProcessor.IrregularWord("Gorgi", "Gorgi", "Gorgiego", "Gorgiego", "Gorgiemu", "Gorgiemu", "Gorgiego", "Gorgiego", "Gorgim", "Gorgim", "Gorgim", "Gorgim"),
					new PolishTextProcessor.IrregularWord("Grabieżca", "Grabieżcy", "Grabieżcy", "Grabieżców", "Grabieżcy", "Grabieżcom", "Grabieżcę", "Grabieżców", "Grabieżcą", "Grabieżcami", "Grabieżcy", "Grabieżcach")
				}
			},
			{
				'H',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Hodowca", "Hodowcy", "Hodowcy", "Hodowców", "Hodowcy", "Hodowcami", "Hodowcę", "Hodowców", "Hodowcą", "Hodowcami", "Hodowcy", "Hodowcach"),
					new PolishTextProcessor.IrregularWord("Harcownik", "Harcownicy", "Harcownika", "Harcowników", "Harcownikowi", "Harcownikom", "Harcownika", "Harcowników", "Harcownikiem", "Harcownikami", "Harcowniku", "Harcownikach")
				}
			},
			{
				'I',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Impestor", "Impestorowie", "Impestorów", "Impestorów", "Impestorom", "Impestorom", "Impestorów", "Impestorów", "Impestorami", "Impestorami", "Impestorach", "Impestorach"),
					new PolishTextProcessor.IrregularWord("Ingunde", "Ingunde", "Ingundego", "Ingundego", "Ingundemu", "Ingundemu", "Ingundego", "Ingundego", "Ingundem", "Ingundem", "Ingundem", "Ingundem"),
					new PolishTextProcessor.IrregularWord("Itsul", "Itsul", "Itsula", "Itsula", "Itsulowi", "Itsulowi", "Itsula", "Itsula", "Itsulem", "Itsulem", "Itsulu", "Itsulu")
				}
			},
			{
				'J',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Jeniec", "Jeńcy", "Jeńca", "Jeńców", "Jeńcowi", "Jeńcom", "Jeńca", "Jeńców", "Jeńcem", "Jeńcami", "Jeńcu", "Jeńcach"),
					new PolishTextProcessor.IrregularWord("Jeździec", "Jeźdźcy", "Jeźdźca", "Jeźdźców", "Jeźdźcowi", "Jeźdźcom", "Jeźdźca", "Jeźdźców", "Jeźdźcem", "Jeźdźcami", "Jeźdźcu", "Jeźdźcach")
				}
			},
			{
				'K',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Kada", "Kada", "Kady", "Kady", "Kadzie", "Kadzie", "Kadę", "Kadę", "Kadą", "Kadą", "Kadzie", "Kadzie"),
					new PolishTextProcessor.IrregularWord("Karakergita", "Karakergici", "Karakergity", "Karakergitów", "Karakergicie", "Karakergitom", "Karakergitę", "Karakergitów", "Karakergitą", "Karakergitami", "Karakergicie", "Karakergitach"),
					new PolishTextProcessor.IrregularWord("Kirasław", "Kirasław", "Kirasława", "Kirasława", "Kirasławowi", "Kirasławowi", "Kirasława", "Kirasława", "Kirasławem", "Kirasławem", "Kirasławie", "Kirasławie"),
					new PolishTextProcessor.IrregularWord("Komendant", "Komendanci", "Komendanta", "Komendantów", "Komendantowi", "Komendantom", "Komendanta", "Komendantów", "Komendantem", "Komendantami", "Komendancie", "Komendantach"),
					new PolishTextProcessor.IrregularWord("Koniuszy", "Koniuszowie", "Koniuszego", "Koniuszych", "Koniuszemu", "Koniuszym", "Koniuszego", "Koniuszych", "Koniuszym", "Koniuszymi", "Koniuszym", "Koniuszych"),
					new PolishTextProcessor.IrregularWord("Książę", "Książęta", "Księcia", "Książąt", "Księciu", "Książętom", "Księcia", "Książąt", "Księciem", "Książętami", "Księciu", "Książętach")
				}
			},
			{
				'L',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Legionista", "Legioniści", "Legionisty", "Legionistów", "Legioniście", "Legionistom", "Legionistę", "Legionistów", "Legionistą", "Legionistami", "Legioniście", "Legionistach"),
					new PolishTextProcessor.IrregularWord("Leśny", "Leśni", "Leśnego", "Leśnych", "Leśnemu", "Leśnym", "Leśnego", "Leśnych", "Leśnym", "Leśnymi", "Leśnym", "Leśnych")
				}
			},
			{
				'M',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Marszałek", "Marszałkowie", "Marszałka", "Marszałków", "Marszałkowi", "Marszałkom", "Marszałka", "Marszałków", "Marszałkiem", "Marszałkami", "Marszałku", "Marszałkach"),
					new PolishTextProcessor.IrregularWord("Merović", "Merović", "Merovicia", "Merovicia", "Meroviciem", "Meroviciem", "Merovicia", "Merovicia", "Meroviciem", "Meroviciem", "Meroviciu", "Meroviciu"),
					new PolishTextProcessor.IrregularWord("Mieszczanin", "Mieszczanie", "Mieszczanina", "Mieszczan", "Mieszczaninowi", "Mieszczanom", "Mieszczanina", "Mieszczan", "Mieszczaninem", "Mieszczanami", "Mieszczaninie", "Mieszczanach"),
					new PolishTextProcessor.IrregularWord("Mikri", "Mikri", "Mikriego", "Mikriego", "Mikriemu", "Mikriemu", "Mikriego", "Mikriego", "Mikrim", "Mikrim", "Mikrim", "Mikrim"),
					new PolishTextProcessor.IrregularWord("Minstrel", "Minstrele", "Minstrela", "Minstreli", "Minstrelowi", "Minstrelom", "Minstrela", "Minstreli", "Minstrelem", "Minstrelami", "Minstrelu", "Minstrelach"),
					new PolishTextProcessor.IrregularWord("Mój", "Moi", "Mojego", "Moich", "Mojemu", "Moim", "Mojego", "Moich", "Moim", "Moimi", "Moim", "Moich", "Mój", "Moi"),
					new PolishTextProcessor.IrregularWord("Myśliwy", "Myśliwi", "Myśliwego", "Myśliwych", "Myśliwemu", "Myśliwym", "Myśliwego", "Myśliwych", "Myśliwym", "Myśliwymi", "Myśliwym", "Myśliwych"),
					new PolishTextProcessor.IrregularWord("Mąż", "Mężowie", "Męża", "Mężów", "Mężowi", "Mężom", "Męża", "Mężów", "Mężem", "Mężami", "Mężu", "Mężach")
				}
			},
			{
				'N',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Nadrzewny", "Nadrzewni", "Nadrzewnego", "Nadrzewnych", "Nadrzewnemu", "Nadrzewnym", "Nadrzewnego", "Nadrzewnych", "Nadrzewnym", "Nadrzewnymi", "Nadrzewnym", "Nadrzewnych"),
					new PolishTextProcessor.IrregularWord("Nal", "Nal", "Nala", "Nala", "Nalowi", "Nalowi", "Nala", "Nala", "Nalem", "Nalem", "Nalu", "Nalu"),
					new PolishTextProcessor.IrregularWord("Neutralny", "Neutralni", "Neutralnego", "Neutralnych", "Neutralnemu", "Neutralnym", "Neutralnego", "Neutralnych", "Neutralnym", "Neutralnymi", "Neutralnym", "Neutralnych"),
					new PolishTextProcessor.IrregularWord("Nikt", "Nikt", "Nikogo", "Nikogo", "Nikomu", "Nikomu", "Nikogo", "Nikogo", "Nikim", "Nikim", "Nikim", "Nikim")
				}
			},
			{
				'O',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Obrońca", "Obrońcy", "Obrońcy", "Obrońców", "Obrońcy", "Obrońcom", "Obrońcę", "Obrońców", "Obrońcą", "Obrońcami", "Obrońcy", "Obrońcach"),
					new PolishTextProcessor.IrregularWord("Obywatel", "Obywatele", "Obywatela", "Obywateli", "Obywatelowi", "Obywatelom", "Obywatela", "Obywateli", "Obywatelem", "Obywatelami", "Obywatelu", "Obywatelach"),
					new PolishTextProcessor.IrregularWord("Ocalały", "Ocalali", "Ocalałego", "Ocalałych", "Ocalałemu", "Ocalałym", "Ocalałego", "Ocalałych", "Ocalałym", "Ocalałymi", "Ocalałym", "Ocalałych"),
					new PolishTextProcessor.IrregularWord("Ochotnik", "Ochotnicy", "Ochotnika", "Ochotników", "Ochotnikowi", "Ochotnikom", "Ochotnika", "Ochotników", "Ochotnikiem", "Ochotnikami", "Ochotniku", "Ochotnikach"),
					new PolishTextProcessor.IrregularWord("Oczekiwany", "Oczekiwani", "Oczekiwanego", "Oczekiwanych", "Oczekiwanemu", "Oczekiwanym", "Oczekiwanego", "Oczekiwanych", "Oczekiwanym", "Oczekiwanymi", "Oczekiwanym", "Oczekiwanych"),
					new PolishTextProcessor.IrregularWord("Oddelegowany", "Oddelegowani", "Oddelegowanego", "Oddelegowanych", "Oddelegowanemu", "Oddelegowanym", "Oddelegowanego", "Oddelegowanych", "Oddelegowanem", "Oddelegowanymi", "Oddelegowanem", "Oddelegowanych"),
					new PolishTextProcessor.IrregularWord("Ojciec", "Ojcowie", "Ojca", "Ojców", "Ojcu", "Ojcom", "Ojca", "Ojców", "Ojcem", "Ojcami", "Ojcu", "Ojcach"),
					new PolishTextProcessor.IrregularWord("Orest", "Orest", "Oresta", "Oresta", "Orestowi", "Orestowi", "Oresta", "Oresta", "Orestem", "Orestem", "Oreście", "Oreście"),
					new PolishTextProcessor.IrregularWord("Oto", "Oto", "Otona", "Otona", "Otonowi", "Otonowi", "Otona", "Otona", "Otonem", "Otonem", "Otonie", "Otonie")
				}
			},
			{
				'P',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Pan", "Panowie", "Pana", "Panów", "Panu", "Panom", "Pana", "Panów", "Panem", "Panami", "Panie", "Panach", "Panie", "Panowie"),
					new PolishTextProcessor.IrregularWord("Pasterz", "Pasterze", "Pasterza", "Pasterzy", "Pasterzowi", "Pasterzom", "Pasterza", "Pasterzy", "Pasterzem", "Pasterzami", "Pasterzu", "Pasterzach"),
					new PolishTextProcessor.IrregularWord("Pazel", "Pazel", "Pazela", "Pazela", "Pazelowi", "Pazelowi", "Pazela", "Pazela", "Pazelem", "Pazelem", "Pazelu", "Pazelu"),
					new PolishTextProcessor.IrregularWord("Pikinier", "Pikinierzy", "Pikiniera", "Pikinierów", "Pikinierowi", "Pikinierom", "Pikiniera", "Pikinierów", "Pikinierem", "Pikinierami", "Pikinierze", "Pikinierach"),
					new PolishTextProcessor.IrregularWord("Planista", "Planiści", "Planisty", "Planistów", "Planiście", "Planistom", "Planistę", "Planistów", "Planistą", "Planistami", "Planiście", "Planistach"),
					new PolishTextProcessor.IrregularWord("Przestępca", "Przestępcy", "Przestępcy", "Przestępców", "Przestępcy", "Przestępcom", "Przestępcę", "Przestępców", "Przestępcy", "Przestępcami", "Przestępcy", "Przestępcach"),
					new PolishTextProcessor.IrregularWord("Przywódca", "Przywódcy", "Przywódcy", "Przywódców", "Przywódcą", "Przywódcom", "Przywódcę", "Przywódców", "Przywódcą", "Przywódcami", "Przywódcy", "Przywódcach")
				}
			},
			{
				'R',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ratagost", "Ratagost", "Ratagosta", "Ratagosta", "Ratagostowi", "Ratagostowi", "Ratagosta", "Ratagosta", "Ratagostem", "Ratagostem", "Ratagoście", "Ratagoście"),
					new PolishTextProcessor.IrregularWord("Rożywol", "Rożywol", "Rożywola", "Rożywola", "Rożywolowi", "Rożywolowi", "Rożywola", "Rożywola", "Rożywolem", "Rożywolem", "Rożywolu", "Rożywolu"),
					new PolishTextProcessor.IrregularWord("Rzeczoznawca", "Rzeczoznawcy", "Rzeczoznawcy", "Rzeczoznawców", "Rzeczoznawcy", "Rzeczoznawcom", "Rzeczoznawcę", "Rzeczoznawców", "Rzeczoznawcą", "Rzeczoznawcami", "Rzeczoznawcy", "Rzeczoznawcach"),
					new PolishTextProcessor.IrregularWord("Rządca", "Rządcy", "Rządcy", "Rządców", "Rządcy", "Rządcom", "Rządcę", "Rządców", "Rządcą", "Rządcami", "Rządcy", "Rządcach")
				}
			},
			{
				'S',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Sacha", "Sacha", "Sachy", "Sachy", "Sasze", "Sasze", "Sachę", "Sachę", "Sachą", "Sachą", "Sasze", "Sasze"),
					new PolishTextProcessor.IrregularWord("Sasal", "Sasal", "Sasala", "Sasala", "Sasalowi", "Sasalowi", "Sasala", "Sasala", "Sasalem", "Sasalem", "Sasalu", "Sasalu"),
					new PolishTextProcessor.IrregularWord("Sierota", "Sieroty", "Sieroty", "Sierotach", "Sierocie", "Sierotom", "Sierotę", "Sieroty", "Sierotą", "Sierotami", "Sierocie", "Sierotach"),
					new PolishTextProcessor.IrregularWord("Skene", "Skene", "Skenego", "Skenego", "Skenemu", "Skenemu", "Skenego", "Skenego", "Skenem", "Skenem", "Skenie", "Skenie"),
					new PolishTextProcessor.IrregularWord("Skolderbrod", "Skolderbrodowie", "Skolderbroda", "Skolderbrodów", "Skolderbrodowi", "Skolderbrodom", "Skolderbroda", "Skolderbrodów", "Skolderbrodem", "Skolderbrodami", "Skolderbrodzie", "Skolderbrodach"),
					new PolishTextProcessor.IrregularWord("Spartanin", "Spartanie", "Spartanina", "Spartan", "Spartaninie", "Spartanom", "Spartanina", "Spartan", "Spartaninem", "Spartanami", "Spartaninie", "Spartanach"),
					new PolishTextProcessor.IrregularWord("Starszy", "Starsi", "Starszego", "Starszych", "Starszemu", "Starszym", "Starszego", "Starszych", "Starszym", "Starszymi", "Starszym", "Starszych"),
					new PolishTextProcessor.IrregularWord("Strażnik", "Strażnicy", "Strażnika", "Strażników", "Strażnikowi", "Strażnikom", "Strażnika", "Strażników", "Strażnikiem", "Strażnikami", "Strażniku", "Strażnikach"),
					new PolishTextProcessor.IrregularWord("Stróż", "Stróże", "Stróża", "Stróżów", "Stróżowi", "Stróżom", "Stróża", "Stróżów", "Stróżem", "Stróżami", "Stróżu", "Stróżach"),
					new PolishTextProcessor.IrregularWord("Strzelec", "Strzelcy", "Strzelca", "Strzelców", "Strzelcowi", "Strzelcom", "Strzelca", "Strzelców", "Strzelciem", "Strzelcami", "Strzelcu", "Strzelcach"),
					new PolishTextProcessor.IrregularWord("Sujkana", "Sujkana", "Sujkany", "Sujkany", "Sujkanie", "Sujkanie", "Sujkanę", "Sujkanę", "Sujkaną", "Sujkaną", "Sujkanie", "Sujkanie"),
					new PolishTextProcessor.IrregularWord("Szawił", "Szawił", "Szawiła", "Szawiła", "Szawiłowi", "Szawiłowi", "Szawiła", "Szawiła", "Szawiłem", "Szawiłem", "Szawile", "Szawile"),
					new PolishTextProcessor.IrregularWord("Szkoleniowiec", "Szkoleniowcy", "Szkoleniowca", "Szkoleniowców", "Szkoleniowcowi", "Szkoleniowcom", "Szkoleniowca", "Szkoleniowców", "Szkoleniowcem", "Szkoleniowcami", "Szkoleniowcu", "Szkoleniowcach"),
					new PolishTextProcessor.IrregularWord("Szumowina", "Szumowiny", "Szumowiny", "Szumowin", "Szumowinie", "Szumowinom", "Szumowinę", "Szumowiny", "Szumowiną", "Szumowinami", "Szumowinie", "Szumowinach"),
					new PolishTextProcessor.IrregularWord("Sędzia", "Sędziowie", "Sędzia", "Sędziów", "Sędziemu", "Sędziom", "Sędziego", "Sędziów", "Sędzią", "Sędziami", "Sędzi", "Sędziach"),
					new PolishTextProcessor.IrregularWord("Sierżant", "Sierżanci", "Sierżanta", "Sierżantów", "Sierżantowi", "Sierżantom", "Sierżanta", "Sierżantów", "Sierżantem", "Sierżantami", "Sierżancie", "Sierżantach")
				}
			},
			{
				'T',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Taja", "Taja", "Tai", "Tai", "Tai", "Tai", "Taję", "Taję", "Tają", "Tają", "Tai", "Tai"),
					new PolishTextProcessor.IrregularWord("Tamza", "Tamza", "Tamzy", "Tamzy", "Tamzie", "Tamzie", "Tamzę", "Tamzę", "Tamzą", "Tamzą", "Tamzie", "Tamzie"),
					new PolishTextProcessor.IrregularWord("Ten", "Ci", "Tego", "Tych", "Temu", "Tym", "Tego", "Tych", "Tym", "Tymi", "Tym", "Tych"),
					new PolishTextProcessor.IrregularWord("Tochi", "Tochi", "Tochiego", "Tochiego", "Tochiemu", "Tochiemu", "Tochiego", "Tochiego", "Tochim", "Tochim", "Tochim", "Tochim")
				}
			},
			{
				'U',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Untery", "Untery", "Unterego", "Unterego", "Unteremu", "Unteremu", "Unterego", "Unterego", "Unterym", "Unterym", "Unterym", "Unterym")
				}
			},
			{
				'V',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("VIP", "VIP-owie", "VIP-a", "VIP-ów", "VIP-owi", "VIP-om", "VIP-a", "VIP-ów", "VIP-em", "VIP-ami", "VIP-ie", "VIP-ach")
				}
			},
			{
				'W',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Waszorki", "Waszorki", "Waszorkiego", "Waszorkiego", "Waszorkiemu", "Waszorkiemu", "Waszorkiego", "Waszorkiego", "Waszorkim", "Waszorkim", "Waszorkim", "Waszorkim"),
					new PolishTextProcessor.IrregularWord("Weliszyn", "Weliszyn", "Weliszyna", "Weliszyna", "Weliszynowi", "Weliszynowi", "Weliszyn", "Weliszyn", "Weliszynem", "Weliszynem", "Weliszynie", "Weliszynie"),
					new PolishTextProcessor.IrregularWord("Wilczoskóry", "Wilczoskórzy", "Wilczoskórego", "Wilczoskórych", "Wilczoskóremu", "Wilczoskórym", "Wilczoskórego", "Wilczoskórych", "Wilczoskórym", "Wilczoskórymi", "Wilczoskórym", "Wilczoskórych"),
					new PolishTextProcessor.IrregularWord("Wiejski", "Wiejscy", "Wiejskiego", "Wiejskich", "Wiejskiemu", "Wiejskim", "Wiejskiego", "Wiejskich", "Wiejskim", "Wiejskimi", "Wiejskim", "Wiejskich"),
					new PolishTextProcessor.IrregularWord("Więzień", "Więźniowie", "Więźnia", "Więźniów", "Więźniowi", "Więźniom", "Więźnia", "Więźniów", "Więźniem", "Więźniami", "Więźniu", "Więźniach"),
					new PolishTextProcessor.IrregularWord("Władca", "Władcy", "Władcy", "Władców", "Władcy", "Władcom", "Władcę", "Władców", "Władcą", "Władcami", "Władcy", "Władcach"),
					new PolishTextProcessor.IrregularWord("Włócznik", "Włócznicy", "Włócznika", "Włóczników", "Włócznikowi", "Włócznikom", "Włócznika", "Włóczników", "Włócznikiem", "Włócznikami", "Włóczniku", "Włócznikach"),
					new PolishTextProcessor.IrregularWord("Wojownik", "Wojownicy", "Wojownika", "Wojowników", "Wojownikowi", "Wojownikami", "Wojownika", "Wojowników", "Wojownikiem", "Wojownikami", "Wojowniku", "Wojownikach"),
					new PolishTextProcessor.IrregularWord("Wór", "Wory", "Wora", "Worów", "Worowi", "Worom", "Wór", "Wory", "Worem", "Worami", "Worze", "Worach"),
					new PolishTextProcessor.IrregularWord("Wspierający", "Wspierający", "Wspierającego", "Wspierających", "Wspierającemu", "Wspierającym", "Wspierającego", "Wspierających", "Wspierającym", "Wspierającymi", "Wspierającym", "Wspierających"),
					new PolishTextProcessor.IrregularWord("Współwinny", "Współwinni", "Współwinnego", "Współwinnych", "Współwinnemu", "Współwinnym", "Współwinnego", "Współwinnych", "Współwinnym", "Współwinnymi", "Współwinnym", "Współwinnych"),
					new PolishTextProcessor.IrregularWord("Wybraniec", "Wybrańcy", "Wybrańca", "Wybrańców", "Wybrańcowi", "Wybrańcom", "Wybrańca", "Wybrańców", "Wybrańcem", "Wybrańcami", "Wybrańcu", "Wybrańcach"),
					new PolishTextProcessor.IrregularWord("Wędrowiec", "Wędrowcy", "Wędrowca", "Wędrowców", "Wędrowcowi", "Wędrowcom", "Wędrowca", "Wędrowców", "Wędrowcem", "Wędrowcami", "Wędrowcu", "Wędrowcach"),
					new PolishTextProcessor.IrregularWord("Wojewoda", "Wojewodowie", "Wojewody", "Wojewodów", "Wojewodzie", "Wojewodom", "Wojewodę", "Wojewodów", "Wojewodą", "Wojewodami", "Wojewodzie", "Wojewodach")
				}
			},
			{
				'Z',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Zabity", "Zabici", "Zabitego", "Zabitych", "Zabitemu", "Zabitym", "Zabitego", "Zabitych", "Zabitym", "Zabitymi", "Zabitym", "Zabitych"),
					new PolishTextProcessor.IrregularWord("Zarządca", "Zarządcy", "Zarządcy", "Zarządców", "Zarządcy", "Zarządcom", "Zarządcę", "Zarządców", "Zarządcą", "Zarządcami", "Zarządcy", "Zarządcach"),
					new PolishTextProcessor.IrregularWord("Zbójca", "Zbójcy", "Zbójcy", "Zbójców", "Zbójcy", "Zbójcom", "Zbójcę", "Zbójców", "Zbójcą", "Zbójcami", "Zbójcy", "Zbójcach"),
					new PolishTextProcessor.IrregularWord("Zgniłozęby", "Zgniłozębi", "Zgniłozębego", "Zgniłozębych", "Zgniłozębemu", "Zgniłozębym", "Zgniłozębego", "Zgniłozębych", "Zgniłozębym", "Zgniłozębymi", "Zgniłozębym", "Zgniłozębych"),
					new PolishTextProcessor.IrregularWord("Znakowy", "Znakowi", "Znakowego", "Znakowych", "Znakowemu", "Znakowym", "Znakowego", "Znakowych", "Znakowym", "Znakowymi", "Znakowym", "Znakowych")
				}
			},
			{
				'Ż',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Żołnierz", "Żołnierze", "Żołnierza", "Żołnierzy", "Żołnierzowi", "Żołnierzom", "Żołnierza", "Żołnierzy", "Żołnierzem", "Żołnierzami", "Żołnierzu", "Żołnierzach")
				}
			},
			{
				'Ł',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Łowca", "Łowcy", "Łowcy", "Łowców", "Łowcy", "Łowcom", "Łowcę", "Łowców", "Łowcą", "Łowcami", "Łowcy", "Łowcach"),
					new PolishTextProcessor.IrregularWord("Łupieżca", "Łupieżcy", "Łupieżcy", "Łupieżców", "Łupieżcy", "Łupieżcom", "Łupieżcę", "Łupieżców", "Łupieżcą", "Łupieżcami", "Łupieżcy", "Łupieżcach")
				}
			}
		};

		// Token: 0x040000E0 RID: 224
		private static readonly Dictionary<char, List<PolishTextProcessor.IrregularWord>> IrregularMasculineAnimateDictionary = new Dictionary<char, List<PolishTextProcessor.IrregularWord>>
		{
			{
				'K',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Koń", "Konie", "Konia", "Koni", "Koniowi", "Koniom", "Konia", "Konie", "Koniem", "Końmi", "Koniu", "Koniach")
				}
			},
			{
				'P',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Pionek", "Pionki", "Pionka", "Pionków", "Pionkowi", "Pionkom", "Pionka", "Pionki", "Pionkiem", "Pionkami", "Pionku", "Pionkach")
				}
			},
			{
				'T',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ten", "Te", "Tego", "Tych", "Temu", "Tym", "Tego", "Te", "Tym", "Tymi", "Tym", "Tych")
				}
			},
			{
				'W',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Wiejski", "Wiejskie", "Wiejskiego", "Wiejskich", "Wiejskiemu", "Wiejskim", "Wiejskiego", "Wiejskich", "Wiejskim", "Wiejskimi", "Wiejskim", "Wiejskich")
				}
			}
		};

		// Token: 0x040000E1 RID: 225
		private static readonly Dictionary<char, List<PolishTextProcessor.IrregularWord>> IrregularMasculineInanimateDictionary = new Dictionary<char, List<PolishTextProcessor.IrregularWord>>
		{
			{
				'A',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ałow", "Ałow", "Ałowu", "Ałowu", "Ałowowi", "Ałowowi", "Ałow", "Ałow", "Ałowem", "Ałowem", "Ałowie", "Ałowie")
				}
			},
			{
				'C',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ciesielski", "Ciesielskie", "Ciesielskiego", "Ciesielskich", "Ciesielskiemu", "Ciesielskim", "Ciesielski", "Ciesielskie", "Ciesielskim", "Ciesielskimi", "Ciesielskim", "Ciesielskich", "Ciesielski", "Ciesielskie"),
					new PolishTextProcessor.IrregularWord("Czepiec", "Czepce", "Czepca", "Czepców", "Czepcowi", "Czepcom", "Czepiec", "Czepce", "Czepcem", "Czepcami", "Czepcu", "Czepcach")
				}
			},
			{
				'D',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Dech", "Tchy", "Tchu", "Tchów", "Tchowi", "Tchom", "Dech", "Tchy", "Tchem", "Tchami", "Tchu", "Tchach")
				}
			},
			{
				'G',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Gławstrom", "Gławstrom", "Gławstromu", "Gławstromu", "Gławstromowi", "Gławstromowi", "Gławstrom", "Gławstrom", "Gławstromem", "Gławstromem", "Gławstromie", "Gławstromie")
				}
			},
			{
				'K',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Kufer", "Kufry", "Kufra", "Kufrów", "Kufrowi", "Kufrom", "Kufer", "Kufry", "Kufrem", "Kuframi", "Kufrze", "Kufrach")
				}
			},
			{
				'M',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Monopol", "Monopole", "Monopolu", "Monopoli", "Monopolowi", "Monopolom", "Monopol", "Monopole", "Monopolem", "Monopolami", "Monopolu", "Monopolach", "Monopolu", "Monopole")
				}
			},
			{
				'N',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Newijańsk", "Newijańsk", "Newijańska", "Newijańska", "Newijańskowi", "Newijańskowi", "Newijańsk", "Newijańsk", "Newijańskiem", "Newijańskiem", "Newijańsku", "Newijańsku")
				}
			},
			{
				'P',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Pęd", "Pędy", "Pędu", "Pędów", "Pędowi", "Pędom", "Pęd", "Pędy", "Pędem", "Pędami", "Pędzie", "Pędach"),
					new PolishTextProcessor.IrregularWord("Pojedynek", "Pojedynki", "Pojedynku", "Pojedynków", "Pojedynkowi", "Pojedynkom", "Pojedynek", "Pojedynki", "Pojedynkiem", "Pojedynkami", "Pojedynku", "Pojedynkach")
				}
			},
			{
				'S',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Szpikulec", "Szpikulce", "Szpikulca", "Szpikulców", "Szpikulcowi", "Szpikulcom", "Szpikulec", "Szpikulce", "Szpikulcem", "Szpikulcami", "Szpikulcu", "Szpikulcach"),
					new PolishTextProcessor.IrregularWord("Spryt", "Sprytu", "Sprytowi", "Spryt", "Sprytem", "Sprycie", "Sprycie"),
					new PolishTextProcessor.IrregularWord("Samouczek", "Samouczki", "Samouczka", "Samouczków", "Samouczkowi", "Samouczkom", "Samouczek", "Samouczki", "Samouczkiem", "Samouczkami", "Samouczku", "Samouczkach")
				}
			},
			{
				'T',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ten", "Te", "Tego", "Tych", "Temu", "Tym", "Ten", "Te", "Tym", "Tymi", "Tym", "Tych")
				}
			},
			{
				'U',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ustokoł", "Ustokoł", "Ustokołu", "Ustokołu", "Ustokołowi", "Ustokołowi", "Ustokoł", "Ustokoł", "Ustokołem", "Ustokołem", "Ustokole", "Ustokole")
				}
			},
			{
				'W',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Wiejski", "Wiejskie", "Wiejskiego", "Wiejskich", "Wiejskiemu", "Wiejskim", "Wiejski", "Wiejskich", "Wiejskim", "Wiejskimi", "Wiejskim", "Wiejskich"),
					new PolishTextProcessor.IrregularWord("Wigor", "Wigoru", "Wigorowi", "Wigor", "Wigorem", "Wigorze", "Wigorze"),
					new PolishTextProcessor.IrregularWord("Wydatek", "Wydatki", "Wydatku", "Wydatków", "Wydatkowi", "Wydatkom", "Wydatek", "Wydatki", "Wydatkiem", "Wydatkami", "Wydatku", "Wydatkach"),
					new PolishTextProcessor.IrregularWord("Władyw", "Władyw", "Władywu", "Władywu", "Władywowi", "Władywowi", "Władyw", "Władyw", "Władywem", "Władywem", "Władywie", "Władywie")
				}
			},
			{
				'Z',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Zamek", "Zamki", "Zamku", "Zamków", "Zamkowi", "Zamkom", "Zamek", "Zamki", "Zamkiem", "Zamkami", "Zamku", "Zamkach")
				}
			}
		};

		// Token: 0x040000E2 RID: 226
		private static readonly Dictionary<char, List<PolishTextProcessor.IrregularWord>> IrregularFeminineDictionary = new Dictionary<char, List<PolishTextProcessor.IrregularWord>>
		{
			{
				'A',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Aika", "Aika", "Aiki", "Aiki", "Aice", "Aice", "Aikę", "Aikę", "Aiką", "Aiką", "Aice", "Aice"),
					new PolishTextProcessor.IrregularWord("Arga", "Arga", "Argi", "Argi", "Ardze", "Ardze", "Argę", "Argę", "Argą", "Argą", "Ardze", "Ardze"),
					new PolishTextProcessor.IrregularWord("Asta", "Asta", "Asty", "Asty", "Aście", "Aście", "Astę", "Astę", "Astą", "Astą", "Aście", "Aście")
				}
			},
			{
				'B',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Bela", "Bele", "Beli", "Beli", "Beli", "Belom", "Belę", "Bele", "Belą", "Belami", "Beli", "Belach", "Belo", "Bele")
				}
			},
			{
				'C',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Część", "Części", "Części", "Części", "Części", "Częściom", "Część", "Części", "Częścią", "Częściami", "Części", "Częściach")
				}
			},
			{
				'D',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Dłoń", "Dłonie", "Dłoni", "Dłoni", "Dłoni", "Dłoniom", "Dłoń", "Dłonie", "Dłonią", "Dłońmi", "Dłoni", "Dłoniach")
				}
			},
			{
				'E',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Echa", "Echa", "Esze", "Esze", "Esze", "Esze", "Echę", "Echę", "Echą", "Echą", "Esze", "Esze"),
					new PolishTextProcessor.IrregularWord("Epikrotea", "Epikrotea", "Epikrotei", "Epikrotei", "Epikrotei", "Epikrotei", "Epikroteę", "Epikroteę", "Epikroteą", "Epikroteą", "Epikrotei", "Epikrotei", "Epikroteo", "Epikroteo")
				}
			},
			{
				'F',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Fianna", "Fianna", "Fianny", "Fianny", "Fiannie", "Fiannie", "Fiannę", "Fiannę", "Fianną", "Fianną", "Fiannie", "Fiannie")
				}
			},
			{
				'I',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Inteligencja", "Inteligencji", "Inteligencji", "Inteligencję", "Inteligencją", "Inteligencji", "Inteligencjo")
				}
			},
			{
				'K',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Kieszeń", "Kieszenie", "Kieszeni", "Kieszeni", "Kieszeni", "Kieszeniom", "Kieszeń", "Kieszenie", "Kieszenią", "Kieszeniami", "Kieszeni", "Kieszeniach"),
					new PolishTextProcessor.IrregularWord("Kuka", "Kuka", "Kuki", "Kuki", "Kuce", "Kuce", "Kukę", "Kukę", "Kuką", "Kuką", "Kuce", "Kuce"),
					new PolishTextProcessor.IrregularWord("Kontrola", "Kontroli", "Kontroli", "Kontrolę", "Kontrolą", "Kontroli", "Kontrolo")
				}
			},
			{
				'L',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Laska", "Laska", "Laski", "Laski", "Lasce", "Lasce", "Laskę", "Laskę", "Laską", "Laską", "Lasce", "Lasce"),
					new PolishTextProcessor.IrregularWord("Litka", "Litka", "Litki", "Litki", "Litce", "Litce", "Litkę", "Litkę", "Litką", "Litką", "Litce", "Litce")
				}
			},
			{
				'P',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Postać", "Postacie", "Postaci", "Postaci", "Postaci", "Postaciom", "Postać", "Postacie", "Postacią", "Postaciami", "Postaci", "Postaciach")
				}
			},
			{
				'R',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Ręka", "Ręce", "Rąk", "Ręki", "Ręce", "Rękom", "Rękę", "Ręce", "Ręką", "Rękami", "Ręce", "Rękach"),
					new PolishTextProcessor.IrregularWord("Rotea", "Rotea", "Rotei", "Rotei", "Rotei", "Rotei", "Roteę", "Roteę", "Roteą", "Roteą", "Rotei", "Rotei", "Roteo", "Roteo")
				}
			},
			{
				'S',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Sieć", "Sieci", "Sieci", "Sieci", "Sieci", "Sieciom", "Sieć", "Sieci", "Siecią", "Sieciami", "Sieci", "Sieciach"),
					new PolishTextProcessor.IrregularWord("Sztuka", "Sztuki", "Sztuki", "Sztuk", "Sztuce", "Sztukom", "Sztukę", "Sztuki", "Sztuką", "Sztukami", "Sztuce", "Sztukach")
				}
			},
			{
				'V',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Vikka", "Vikka", "Vikki", "Vikki", "Vikce", "Vikce", "Vikkę", "Vikkę", "Vikką", "Vikką", "Vikce", "Vikce")
				}
			},
			{
				'W',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Wdowa", "Wdowy", "Wdowy", "Wdów", "Wdowie", "Wdowom", "Wdowę", "Wdowy", "Wdową", "Wdowami", "Wdowie", "Wdowach"),
					new PolishTextProcessor.IrregularWord("Wiejska", "Wiejskie", "Wiejskiej", "Wiejskich", "Wiejskiej", "Wiejskim", "Wiejską", "Wiejskich", "Wiejską", "Wiejskimi", "Wiejskiej", "Wiejskich"),
					new PolishTextProcessor.IrregularWord("Wieś", "Wsie", "Wsi", "Wsi", "Wsi", "Wsiom", "Wieś", "Wsie", "Wsią", "Wsiami", "Wsi", "Wsiach"),
					new PolishTextProcessor.IrregularWord("Wić", "Wici", "Wici", "Wici", "Wici", "Wiciom", "Wić", "Wici", "Wicią", "Wićmi", "Wici", "Wiciach")
				}
			},
			{
				'U',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Uprząż", "Uprzęże", "Uprzęży", "Uprzęży", "Uprzęży", "Uprzężom", "Uprząż", "Uprzęże", "Uprzężą", "Uprzężami", "Uprzęży", "Uprzężach")
				}
			}
		};

		// Token: 0x040000E3 RID: 227
		private static readonly Dictionary<char, List<PolishTextProcessor.IrregularWord>> IrregularNeuterDictionary = new Dictionary<char, List<PolishTextProcessor.IrregularWord>>
		{
			{
				'D',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Drzwi", "Drzwi", "Drzwi", "Drzwi", "Drzwiom", "Drzwiom", "Drzwi", "Drzwi", "Drzwiami", "Drzwiami", "Drzwiach", "Drzwiach")
				}
			},
			{
				'P',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Prosię", "Prosięta", "Prosięcia", "Prosiąt", "Prosięciu", "Prosiętami", "Prosię", "Prosięta", "Prosięciem", "Prosiętami", "Prosięciu", "Prosiętach")
				}
			},
			{
				'R',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Rusztowanie", "Rusztowania", "Rusztowania", "Rusztowań", "Rusztowaniu", "Rusztowaniom", "Rusztowanie", "Rusztowania", "Rusztowaniem", "Rusztowaniami", "Rusztowaniu", "Rusztowaniach")
				}
			},
			{
				'S',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Sanie", "Sanie", "Sań", "Sań", "Saniom", "Saniom", "Sanie", "Sanie", "Saniami", "Saniami", "Saniach", "Saniach"),
					new PolishTextProcessor.IrregularWord("Spodnie", "Spodnie", "Spodni", "Spodni", "Spodniom", "Spodniom", "Spodnie", "Spodnie", "Spodniami", "Spodniami", "Spodniach", "Spodniach")
				}
			},
			{
				'T',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Twarde drewno", "Twarde drewno", "Twardego drewna", "Twardego drewna", "Twardemu drewnu", "Twardemu drewnu", "Twarde drewno", "Twarde drewno", "Twardym drewnem", "Twardym drewnem", "Twardym drewnie", "Twardym drewnie", "Twarde drewno", "Twarde drewno")
				}
			},
			{
				'W',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Wiejskie", "Wiejskie", "Wiejskiego", "Wiejskich", "Wiejskiemu", "Wiejskim", "Wiejskie", "Wiejskie", "Wiejskim", "Wiejskimi", "Wiejskim", "Wiejskich")
				}
			},
			{
				'Z',
				new List<PolishTextProcessor.IrregularWord>
				{
					new PolishTextProcessor.IrregularWord("Zarękawie", "Zarękawia", "Zarękawia", "Zarękawi", "Zarękawiu", "Zarękawiom", "Zarękawie", "Zarękawia", "Zarękawiem", "Zarękawiami", "Zarękawiu", "Zarękawiach"),
					new PolishTextProcessor.IrregularWord("Zarośla", "Zarośla", "Zarośli", "Zarośli", "Zaroślom", "Zaroślom", "Zarośla", "Zarośla", "Zaroślami", "Zaroślami", "Zaroślach", "Zaroślach"),
					new PolishTextProcessor.IrregularWord("Zwierzę", "Zwierzęta", "Zwierzęcia", "Zwierząt", "Zwierzęciu", "Zwierzętom", "Zwierzę", "Zwierzęta", "Zwierzęciem", "Zwierzętami", "Zwierzeciu", "Zwierzętach")
				}
			}
		};

		// Token: 0x02000051 RID: 81
		private enum WordGenderEnum
		{
			// Token: 0x040001B3 RID: 435
			MasculinePersonal,
			// Token: 0x040001B4 RID: 436
			MasculineAnimate,
			// Token: 0x040001B5 RID: 437
			MasculineInanimate,
			// Token: 0x040001B6 RID: 438
			Feminine,
			// Token: 0x040001B7 RID: 439
			Neuter,
			// Token: 0x040001B8 RID: 440
			NoDeclination
		}

		// Token: 0x02000052 RID: 82
		private static class NounTokens
		{
			// Token: 0x040001B9 RID: 441
			public const string Nominative = ".n";

			// Token: 0x040001BA RID: 442
			public const string NominativePlural = ".p";

			// Token: 0x040001BB RID: 443
			public const string Accusative = ".a";

			// Token: 0x040001BC RID: 444
			public const string Genitive = ".g";

			// Token: 0x040001BD RID: 445
			public const string Instrumental = ".i";

			// Token: 0x040001BE RID: 446
			public const string Locative = ".l";

			// Token: 0x040001BF RID: 447
			public const string Dative = ".d";

			// Token: 0x040001C0 RID: 448
			public const string Vocative = ".v";

			// Token: 0x040001C1 RID: 449
			public const string AccusativePlural = ".ap";

			// Token: 0x040001C2 RID: 450
			public const string GenitivePlural = ".gp";

			// Token: 0x040001C3 RID: 451
			public const string InstrumentalPlural = ".ip";

			// Token: 0x040001C4 RID: 452
			public const string LocativePlural = ".lp";

			// Token: 0x040001C5 RID: 453
			public const string DativePlural = ".dp";

			// Token: 0x040001C6 RID: 454
			public const string VocativePlural = ".vp";

			// Token: 0x040001C7 RID: 455
			public static readonly string[] TokenList = new string[]
			{
				".n",
				".p",
				".a",
				".g",
				".i",
				".l",
				".d",
				".v",
				".ap",
				".gp",
				".ip",
				".lp",
				".dp",
				".vp"
			};
		}

		// Token: 0x02000053 RID: 83
		private static class AdjectiveTokens
		{
			// Token: 0x040001C8 RID: 456
			public const string Nominative = ".j";

			// Token: 0x040001C9 RID: 457
			public const string NominativePlural = ".jp";

			// Token: 0x040001CA RID: 458
			public const string Accusative = ".ja";

			// Token: 0x040001CB RID: 459
			public const string Genitive = ".jg";

			// Token: 0x040001CC RID: 460
			public const string Instrumental = ".ji";

			// Token: 0x040001CD RID: 461
			public const string Locative = ".jl";

			// Token: 0x040001CE RID: 462
			public const string Dative = ".jd";

			// Token: 0x040001CF RID: 463
			public const string Vocative = ".jv";

			// Token: 0x040001D0 RID: 464
			public const string AccusativePlural = ".jap";

			// Token: 0x040001D1 RID: 465
			public const string GenitivePlural = ".jgp";

			// Token: 0x040001D2 RID: 466
			public const string InstrumentalPlural = ".jip";

			// Token: 0x040001D3 RID: 467
			public const string LocativePlural = ".jlp";

			// Token: 0x040001D4 RID: 468
			public const string DativePlural = ".jdp";

			// Token: 0x040001D5 RID: 469
			public const string VocativePlural = ".jvp";

			// Token: 0x040001D6 RID: 470
			public static readonly string[] TokenList = new string[]
			{
				".j",
				".jg",
				".jd",
				".ja",
				".ji",
				".jl",
				".jv",
				".jp",
				".jgp",
				".jdp",
				".jap",
				".jip",
				".jlp",
				".jvp"
			};
		}

		// Token: 0x02000054 RID: 84
		private static class GenderTokens
		{
			// Token: 0x040001D7 RID: 471
			public const string MasculinePersonal = ".MP";

			// Token: 0x040001D8 RID: 472
			public const string MasculineInanimate = ".MI";

			// Token: 0x040001D9 RID: 473
			public const string MasculineAnimate = ".MA";

			// Token: 0x040001DA RID: 474
			public const string Feminine = ".F";

			// Token: 0x040001DB RID: 475
			public const string Neuter = ".N";

			// Token: 0x040001DC RID: 476
			public static readonly string[] TokenList = new string[]
			{
				".MP",
				".MI",
				".MA",
				".F",
				".N"
			};
		}

		// Token: 0x02000055 RID: 85
		private static class WordGroupTokens
		{
			// Token: 0x040001DD RID: 477
			public const string NounNominativePlural = ".nnp";

			// Token: 0x040001DE RID: 478
			public const string NounNominative = ".nn";

			// Token: 0x040001DF RID: 479
			public const string AdjectiveNominativePlural = ".ajp";

			// Token: 0x040001E0 RID: 480
			public const string AdjectiveNominative = ".aj";

			// Token: 0x040001E1 RID: 481
			public const string NounNominativePluralWithBrackets = "{.nnp}";

			// Token: 0x040001E2 RID: 482
			public const string NounNominativeWithBrackets = "{.nn}";

			// Token: 0x040001E3 RID: 483
			public const string AdjectiveNominativePluralWithBrackets = "{.ajp}";

			// Token: 0x040001E4 RID: 484
			public const string AdjectiveNominativeWithBrackets = "{.aj}";
		}

		// Token: 0x02000056 RID: 86
		private struct IrregularWord
		{
			// Token: 0x060002B0 RID: 688 RVA: 0x00019D18 File Offset: 0x00017F18
			public IrregularWord(string nominative, string nominativePlural, string genitive, string genitivePlural, string dative, string dativePlural, string accusative, string accusativePlural, string instrumental, string instrumentalPlural, string locative, string locativePlural)
			{
				this.Nominative = nominative;
				this.NominativePlural = nominativePlural;
				this.Accusative = accusative;
				this.Genitive = genitive;
				this.Instrumental = instrumental;
				this.Locative = locative;
				this.Dative = dative;
				this.Vocative = locative;
				this.AccusativePlural = accusativePlural;
				this.GenitivePlural = genitivePlural;
				this.InstrumentalPlural = instrumentalPlural;
				this.LocativePlural = locativePlural;
				this.DativePlural = dativePlural;
				this.VocativePlural = nominativePlural;
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x00019D94 File Offset: 0x00017F94
			public IrregularWord(string nominative, string nominativePlural, string genitive, string genitivePlural, string dative, string dativePlural, string accusative, string accusativePlural, string instrumental, string instrumentalPlural, string locative, string locativePlural, string vocative, string vocativePlural)
			{
				this.Nominative = nominative;
				this.NominativePlural = nominativePlural;
				this.Accusative = accusative;
				this.Genitive = genitive;
				this.Instrumental = instrumental;
				this.Locative = locative;
				this.Dative = dative;
				this.Vocative = vocative;
				this.AccusativePlural = accusativePlural;
				this.GenitivePlural = genitivePlural;
				this.InstrumentalPlural = instrumentalPlural;
				this.LocativePlural = locativePlural;
				this.DativePlural = dativePlural;
				this.VocativePlural = vocativePlural;
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x00019E10 File Offset: 0x00018010
			public IrregularWord(string nominative, string genitive, string dative, string accusative, string instrumental, string locative, string vocative)
			{
				this.Nominative = nominative;
				this.NominativePlural = nominative;
				this.Accusative = accusative;
				this.Genitive = genitive;
				this.Instrumental = instrumental;
				this.Locative = locative;
				this.Dative = dative;
				this.Vocative = vocative;
				this.AccusativePlural = accusative;
				this.GenitivePlural = genitive;
				this.InstrumentalPlural = instrumental;
				this.LocativePlural = locative;
				this.DativePlural = dative;
				this.VocativePlural = vocative;
			}

			// Token: 0x040001E5 RID: 485
			public readonly string Nominative;

			// Token: 0x040001E6 RID: 486
			public readonly string NominativePlural;

			// Token: 0x040001E7 RID: 487
			public readonly string Accusative;

			// Token: 0x040001E8 RID: 488
			public readonly string Genitive;

			// Token: 0x040001E9 RID: 489
			public readonly string Instrumental;

			// Token: 0x040001EA RID: 490
			public readonly string Locative;

			// Token: 0x040001EB RID: 491
			public readonly string Dative;

			// Token: 0x040001EC RID: 492
			public readonly string Vocative;

			// Token: 0x040001ED RID: 493
			public readonly string AccusativePlural;

			// Token: 0x040001EE RID: 494
			public readonly string GenitivePlural;

			// Token: 0x040001EF RID: 495
			public readonly string InstrumentalPlural;

			// Token: 0x040001F0 RID: 496
			public readonly string LocativePlural;

			// Token: 0x040001F1 RID: 497
			public readonly string DativePlural;

			// Token: 0x040001F2 RID: 498
			public readonly string VocativePlural;
		}
	}
}
