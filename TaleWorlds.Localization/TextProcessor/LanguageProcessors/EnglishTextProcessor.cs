﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TaleWorlds.Localization.TextProcessor.LanguageProcessors
{
	// Token: 0x02000031 RID: 49
	public class EnglishTextProcessor : LanguageSpecificTextProcessor
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000072B4 File Offset: 0x000054B4
		public override void ProcessToken(string sourceText, ref int cursorPos, string token, StringBuilder outputString)
		{
			char c = token[1];
			if (c == 'a')
			{
				if (this.CheckNextCharIsVowel(sourceText, cursorPos))
				{
					outputString.Append("an");
					return;
				}
				outputString.Append("a");
				return;
			}
			else
			{
				if (c != 'A')
				{
					if (c == 's')
					{
						string text = "";
						int startIndex = 0;
						for (int i = outputString.Length - 1; i >= 0; i--)
						{
							if (outputString[i] == ' ')
							{
								startIndex = i + 1;
								break;
							}
							text += outputString[i].ToString();
						}
						text = new string(text.Reverse<char>().ToArray<char>());
						int length = text.Length;
						if (text.Length > 1)
						{
							string newValue;
							if (this.HandleIrregularNouns(text, out newValue))
							{
								outputString.Replace(text, newValue, startIndex, length);
								return;
							}
							if (this.Handle_ves_Suffix(text, out newValue))
							{
								outputString.Replace(text, newValue, startIndex, length);
								return;
							}
							if (this.Handle_ies_Suffix(text, out newValue))
							{
								outputString.Replace(text, newValue, startIndex, length);
								return;
							}
							if (this.Handle_es_Suffix(text, out newValue))
							{
								outputString.Replace(text, newValue, startIndex, length);
								return;
							}
							if (this.Handle_s_Suffix(text, out newValue))
							{
								outputString.Replace(text, newValue, startIndex, length);
								return;
							}
							outputString.Append(c);
							return;
						}
					}
					else if (c == 'o')
					{
						this.HandleApostrophe(outputString, cursorPos);
					}
					return;
				}
				if (this.CheckNextCharIsVowel(sourceText, cursorPos))
				{
					outputString.Append("An");
					return;
				}
				outputString.Append("A");
				return;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007430 File Offset: 0x00005630
		private char GetLastCharacter(StringBuilder outputText, int cursorPos)
		{
			for (int i = cursorPos - 1; i >= 0; i--)
			{
				if (char.IsLetter(outputText[i]))
				{
					return outputText[i];
				}
			}
			return 'x';
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007464 File Offset: 0x00005664
		private void HandleApostrophe(StringBuilder outputString, int cursorPos)
		{
			string text = outputString.ToString();
			bool flag = false;
			if (text.Length < cursorPos)
			{
				cursorPos = text.Length;
			}
			if (text.EndsWith("</b></a>"))
			{
				cursorPos -= 8;
				outputString.Remove(outputString.Length - 8, 8);
				flag = true;
			}
			int lastCharacter = (int)this.GetLastCharacter(outputString, cursorPos);
			outputString.Append('\'');
			if (lastCharacter != 115)
			{
				outputString.Append('s');
			}
			if (flag)
			{
				outputString.Append("</b></a>");
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000074DC File Offset: 0x000056DC
		private bool CheckNextCharIsVowel(string sourceText, int cursorPos)
		{
			while (cursorPos < sourceText.Length)
			{
				char value = sourceText[cursorPos];
				if ("aeiouAEIOU".Contains(value))
				{
					return true;
				}
				if ("bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(value))
				{
					return false;
				}
				cursorPos++;
			}
			return false;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007520 File Offset: 0x00005720
		private bool HandleIrregularNouns(string text, out string resultPlural)
		{
			resultPlural = null;
			char.IsLower(text[text.Length - 1]);
			string key = text.ToLower();
			string text2;
			if (this.IrregularNouns.TryGetValue(key, out text2))
			{
				if (text.All((char c) => char.IsUpper(c)))
				{
					resultPlural = text2.ToUpper();
				}
				else if (char.IsUpper(text[0]))
				{
					char[] array = text2.ToCharArray();
					array[0] = char.ToUpper(array[0]);
					resultPlural = new string(array);
				}
				else
				{
					resultPlural = text2.ToLower();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000075C4 File Offset: 0x000057C4
		private bool Handle_ves_Suffix(string text, out string resultPlural)
		{
			resultPlural = null;
			bool flag = char.IsLower(text[text.Length - 1]);
			char c = char.ToLower(text[text.Length - 1]);
			char c2 = char.ToLower(text[text.Length - 2]);
			if (c2 != 'o' && "aeiouAEIOU".Contains(c2) && c == 'f')
			{
				resultPlural = text.Remove(text.Length - 1);
				resultPlural += (flag ? "ves" : "VES");
				return true;
			}
			if (c2 == 'f' && "aeiouAEIOU".Contains(c))
			{
				resultPlural = text.Remove(text.Length - 2, 2);
				resultPlural += (flag ? "v" : "V");
				resultPlural += (flag ? c : char.ToUpper(c)).ToString();
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			if (c2 == 'l' && c == 'f')
			{
				resultPlural = text.Remove(text.Length - 1);
				resultPlural += (flag ? "ves" : "VES");
				return true;
			}
			return false;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000076F8 File Offset: 0x000058F8
		private bool Handle_ies_Suffix(string text, out string resultPlural)
		{
			resultPlural = null;
			bool flag = char.IsLower(text[text.Length - 1]);
			char c = char.ToLower(text[text.Length - 1]);
			char value = char.ToLower(text[text.Length - 2]);
			if ("bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(value) && c == 'y')
			{
				resultPlural = text.Remove(text.Length - 1);
				resultPlural += (flag ? "ies" : "IES");
				return true;
			}
			return false;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007784 File Offset: 0x00005984
		private bool Handle_es_Suffix(string text, out string resultPlural)
		{
			resultPlural = null;
			bool flag = char.IsLower(text[text.Length - 1]);
			string text2 = text[text.Length - 1].ToString();
			string text3 = text[text.Length - 2].ToString();
			if (text2 == "z")
			{
				resultPlural = text;
				resultPlural += (flag ? "zes" : "ZES");
				return true;
			}
			if (this.Sibilants.Contains(text2))
			{
				resultPlural = text;
				resultPlural += (flag ? "es" : "ES");
				return true;
			}
			if (this.Sibilants.Contains(text3 + text2))
			{
				resultPlural = text;
				resultPlural += (flag ? "es" : "ES");
				return true;
			}
			if ("bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(text3) && text2 == "o")
			{
				resultPlural = text;
				resultPlural += (flag ? "es" : "ES");
				return true;
			}
			if (text3 == "o" && text2 == "e")
			{
				resultPlural = text;
				resultPlural = resultPlural.Remove(resultPlural.Length - 1);
				resultPlural += (flag ? "es" : "ES");
				return true;
			}
			if (text3 == "i" && text2 == "s")
			{
				resultPlural = text;
				resultPlural = resultPlural.Remove(resultPlural.Length - 2);
				resultPlural += (flag ? "es" : "ES");
				return true;
			}
			return false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007928 File Offset: 0x00005B28
		private bool Handle_s_Suffix(string text, out string resultPlural)
		{
			resultPlural = null;
			bool flag = char.IsLower(text[text.Length - 1]);
			char c = char.ToLower(text[text.Length - 1]);
			char c2 = char.ToLower(text[text.Length - 2]);
			if ("bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(c))
			{
				resultPlural = text;
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			if (c == 'e')
			{
				resultPlural = text;
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			if ("aeiouAEIOU".Contains(c2) && c == 'y')
			{
				resultPlural = text;
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			if (c2 == 'f' && c == 'f')
			{
				resultPlural = text;
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			if (c2 == 'o' && c == 'f')
			{
				resultPlural = text;
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			if ("aeiouAEIOU".Contains(c2) && c == 'o')
			{
				resultPlural = text;
				resultPlural += (flag ? "s" : "S");
				return true;
			}
			return false;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007A6D File Offset: 0x00005C6D
		public override CultureInfo CultureInfoForLanguage
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007A74 File Offset: 0x00005C74
		public override void ClearTemporaryData()
		{
		}

		// Token: 0x040000A8 RID: 168
		private Dictionary<string, string> IrregularNouns = new Dictionary<string, string>
		{
			{
				"man",
				"men"
			},
			{
				"footman",
				"footmen"
			},
			{
				"crossbowman",
				"crossbowmen"
			},
			{
				"pikeman",
				"pikemen"
			},
			{
				"shieldman",
				"shieldmen"
			},
			{
				"shieldsman",
				"shieldsmen"
			},
			{
				"woman",
				"women"
			},
			{
				"child",
				"children"
			},
			{
				"mouse",
				"mice"
			},
			{
				"louse",
				"lice"
			},
			{
				"tooth",
				"teeth"
			},
			{
				"goose",
				"geese"
			},
			{
				"foot",
				"feet"
			},
			{
				"ox",
				"oxen"
			},
			{
				"sheep",
				"sheep"
			},
			{
				"fish",
				"fish"
			},
			{
				"species",
				"species"
			},
			{
				"aircraft",
				"aircraft"
			},
			{
				"news",
				"news"
			},
			{
				"advice",
				"advice"
			},
			{
				"information",
				"information"
			},
			{
				"luggage",
				"luggage"
			},
			{
				"athletics",
				"athletics"
			},
			{
				"linguistics",
				"linguistics"
			},
			{
				"curriculum",
				"curricula"
			},
			{
				"analysis",
				"analyses"
			},
			{
				"ellipsis",
				"ellipses"
			},
			{
				"bison",
				"bison"
			},
			{
				"corpus",
				"corpora"
			},
			{
				"crisis",
				"crises"
			},
			{
				"criterion",
				"criteria"
			},
			{
				"die",
				"dice"
			},
			{
				"graffito",
				"graffiti"
			},
			{
				"cactus",
				"cacti"
			},
			{
				"focus",
				"foci"
			},
			{
				"fungus",
				"fungi"
			},
			{
				"headquarters",
				"headquarters"
			},
			{
				"trousers",
				"trousers"
			},
			{
				"cattle",
				"cattle"
			},
			{
				"scissors",
				"scissors"
			},
			{
				"index",
				"indices"
			},
			{
				"vertex",
				"vertices"
			},
			{
				"matrix",
				"matrices"
			},
			{
				"radius",
				"radii"
			},
			{
				"photo",
				"photos"
			},
			{
				"piano",
				"pianos"
			},
			{
				"dwarf",
				"dwarves"
			},
			{
				"wharf",
				"wharves"
			},
			{
				"formula",
				"formulae"
			},
			{
				"moose",
				"moose"
			},
			{
				"phenomenon",
				"phenomena"
			}
		};

		// Token: 0x040000A9 RID: 169
		private string[] Sibilants = new string[]
		{
			"s",
			"x",
			"ch",
			"sh",
			"es",
			"ss"
		};

		// Token: 0x040000AA RID: 170
		private const string Vowels = "aeiouAEIOU";

		// Token: 0x040000AB RID: 171
		private const string Consonants = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
	}
}
