﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TaleWorlds.Library;
using TaleWorlds.Localization.TextProcessor;
using TaleWorlds.Localization.TextProcessor.LanguageProcessors;

namespace TaleWorlds.Localization
{
	// Token: 0x0200000A RID: 10
	public static class MBTextManager
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000039C6 File Offset: 0x00001BC6
		public static string ActiveTextLanguage
		{
			get
			{
				return MBTextManager._activeTextLanguageId;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000039CD File Offset: 0x00001BCD
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000039D4 File Offset: 0x00001BD4
		public static bool LocalizationDebugMode { get; set; }

		// Token: 0x0600006C RID: 108 RVA: 0x000039DC File Offset: 0x00001BDC
		public static bool LanguageExistsInCurrentConfiguration(string language, bool developmentMode)
		{
			return LocalizedTextManager.GetLanguageIds(developmentMode).Any((string l) => l == language);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A10 File Offset: 0x00001C10
		public static bool ChangeLanguage(string language)
		{
			if (LocalizedTextManager.GetLanguageIds(true).Any((string l) => l == language))
			{
				MBTextManager._languageProcessor = LocalizedTextManager.CreateTextProcessorForLanguage(language);
				MBTextManager._activeTextLanguageId = language;
				MBTextManager._activeTextLanguageIndex = LocalizedTextManager.GetLanguageIndex(MBTextManager._activeTextLanguageId);
				LocalizedTextManager.LoadLanguage(MBTextManager._activeTextLanguageId);
				return true;
			}
			Debug.FailedAssert("Invalid language", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\MBTextManager.cs", "ChangeLanguage", 141);
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A93 File Offset: 0x00001C93
		public static int GetActiveTextLanguageIndex()
		{
			return MBTextManager._activeTextLanguageIndex;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A9C File Offset: 0x00001C9C
		public static bool TryChangeVoiceLanguage(string language)
		{
			if (LocalizedVoiceManager.GetVoiceLanguageIds().Any((string l) => l == language))
			{
				MBTextManager._activeVoiceLanguageId = language;
				LocalizedVoiceManager.LoadLanguage(MBTextManager._activeVoiceLanguageId);
				return true;
			}
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003AE6 File Offset: 0x00001CE6
		private static TextObject ProcessNumber(object integer)
		{
			return new TextObject(integer.ToString(), null);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003AF4 File Offset: 0x00001CF4
		internal static string ProcessTextToString(TextObject to, bool shouldClear)
		{
			if (to == null)
			{
				return null;
			}
			if (TextObject.IsNullOrEmpty(to))
			{
				return "";
			}
			string localizedText = MBTextManager.GetLocalizedText(to.Value);
			string text;
			if (!string.IsNullOrEmpty(to.Value))
			{
				text = MBTextManager.Process(localizedText, to);
				text = MBTextManager._languageProcessor.Process(text);
				if (shouldClear)
				{
					MBTextManager._languageProcessor.ClearTemporaryData();
				}
			}
			else
			{
				text = "";
			}
			if (MBTextManager.LocalizationDebugMode)
			{
				string text2 = to.GetID();
				if (string.IsNullOrEmpty(text2))
				{
					text2 = "!";
				}
				return "(" + text2 + ") " + text;
			}
			return text;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B88 File Offset: 0x00001D88
		internal static string ProcessWithoutLanguageProcessor(TextObject to)
		{
			if (to == null)
			{
				return null;
			}
			if (TextObject.IsNullOrEmpty(to))
			{
				return "";
			}
			string localizedText = MBTextManager.GetLocalizedText(to.Value);
			string result;
			if (!string.IsNullOrEmpty(to.Value))
			{
				result = MBTextManager.Process(localizedText, to);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private static string Process(string query, TextObject parent = null)
		{
			List<MBTextToken> list = null;
			if (parent != null)
			{
				list = parent.GetCachedTokens();
			}
			if (list == null)
			{
				list = MBTextManager.Tokenizer.Tokenize(query);
			}
			return TextGrammarProcessor.Process(MBTextParser.Parse(list), MBTextManager.TextContext, parent);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003C0D File Offset: 0x00001E0D
		public static void ClearAll()
		{
			MBTextManager.TextContext.ClearAll();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C19 File Offset: 0x00001E19
		public static void SetTextVariable(string variableName, string text, bool sendClients = false)
		{
			if (text == null)
			{
				return;
			}
			MBTextManager.TextContext.SetTextVariable(variableName, new TextObject(text, null));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003C31 File Offset: 0x00001E31
		public static void SetTextVariable(string variableName, TextObject text, bool sendClients = false)
		{
			if (text == null)
			{
				return;
			}
			MBTextManager.TextContext.SetTextVariable(variableName, text);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003C44 File Offset: 0x00001E44
		public static void SetTextVariable(string variableName, int content)
		{
			TextObject text = MBTextManager.ProcessNumber(content);
			MBTextManager.SetTextVariable(variableName, text, false);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C68 File Offset: 0x00001E68
		public static void SetTextVariable(string variableName, float content)
		{
			TextObject text = MBTextManager.ProcessNumber(content);
			MBTextManager.SetTextVariable(variableName, text, false);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C8C File Offset: 0x00001E8C
		public static void SetTextVariable(string variableName, object content)
		{
			if (content == null)
			{
				return;
			}
			TextObject text = new TextObject(content.ToString(), null);
			MBTextManager.SetTextVariable(variableName, text, false);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public static void SetTextVariable(string variableName, int arrayIndex, object content)
		{
			if (content == null)
			{
				return;
			}
			string text = content.ToString();
			MBTextManager.SetTextVariable(variableName + ":" + arrayIndex, text, false);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public static void SetFunction(string funcName, string functionBody)
		{
			MBTextModel functionBody2 = MBTextParser.Parse(MBTextManager.Tokenizer.Tokenize(functionBody));
			MBTextManager.TextContext.SetFunction(funcName, functionBody2);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003D0E File Offset: 0x00001F0E
		public static void ResetFunctions()
		{
			MBTextManager.TextContext.ResetFunctions();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003D1A File Offset: 0x00001F1A
		public static void ThrowLocalizationError(string message)
		{
			Debug.FailedAssert(message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\MBTextManager.cs", "ThrowLocalizationError", 342);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003D34 File Offset: 0x00001F34
		internal static string GetLocalizedText(string text)
		{
			if (text != null && text.Length > 2 && text[0] == '{' && text[1] == '=')
			{
				if (MBTextManager._idStringBuilder == null)
				{
					MBTextManager._idStringBuilder = new StringBuilder(8);
				}
				else
				{
					MBTextManager._idStringBuilder.Clear();
				}
				if (MBTextManager._targetStringBuilder == null)
				{
					MBTextManager._targetStringBuilder = new StringBuilder(100);
				}
				else
				{
					MBTextManager._targetStringBuilder.Clear();
				}
				int i = 2;
				while (i < text.Length)
				{
					if (text[i] != '}')
					{
						MBTextManager._idStringBuilder.Append(text[i]);
						i++;
					}
					else
					{
						for (i++; i < text.Length; i++)
						{
							MBTextManager._targetStringBuilder.Append(text[i]);
						}
						string text2 = "";
						if (MBTextManager._activeTextLanguageId == "English")
						{
							text2 = MBTextManager._targetStringBuilder.ToString();
							return MBTextManager.RemoveComments(text2);
						}
						if ((MBTextManager._idStringBuilder.Length != 1 || MBTextManager._idStringBuilder[0] != '*') && (MBTextManager._idStringBuilder.Length != 1 || MBTextManager._idStringBuilder[0] != '!'))
						{
							if (MBTextManager._activeTextLanguageId != "English")
							{
								text2 = LocalizedTextManager.GetTranslatedText(MBTextManager._activeTextLanguageId, MBTextManager._idStringBuilder.ToString());
							}
							if (text2 != null)
							{
								return MBTextManager.RemoveComments(text2);
							}
						}
						IL_164:
						return MBTextManager._targetStringBuilder.ToString();
					}
				}
				goto IL_164;
			}
			return text;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003EB0 File Offset: 0x000020B0
		private static string RemoveComments(string localizedText)
		{
			foreach (object obj in MBTextManager.CommentRemoverRegex.Matches(localizedText))
			{
				Match match = (Match)obj;
				localizedText = localizedText.Replace(match.Value, "");
			}
			return localizedText;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003F1C File Offset: 0x0000211C
		public static string DiscardAnimationTagsAndCheckAnimationTagPositions(string text)
		{
			return MBTextManager.DiscardAnimationTags(text);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003F24 File Offset: 0x00002124
		public static string DiscardAnimationTags(string text)
		{
			string text2 = "";
			bool flag = false;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '[')
				{
					flag = true;
				}
				if (!flag)
				{
					text2 += text[i].ToString();
				}
				if (text[i] == ']')
				{
					flag = false;
				}
			}
			return text2;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003F80 File Offset: 0x00002180
		private static bool CheckAnimationTagPositions(string text)
		{
			string text2 = "";
			Match match = MBTextManager.AnimationTagRemoverRegex.Match(text);
			if (match.Success)
			{
				text2 = MBTextManager.DiscardAnimationTags(match.Value);
			}
			return string.IsNullOrEmpty(text2.Replace(" ", ""));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003FC8 File Offset: 0x000021C8
		public static string[] GetConversationAnimations(TextObject to)
		{
			string text = to.CopyTextObject().ToString();
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = new string[4];
			bool flag = false;
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (text[i] == '[')
					{
						flag = true;
					}
					else if (flag)
					{
						if (text[i] == ',' || text[i] == ']')
						{
							array[num] = stringBuilder.ToString();
							stringBuilder.Clear();
							if (text[i] == ']')
							{
								flag = false;
							}
						}
						else if (text[i] == ':')
						{
							string a = stringBuilder.ToString();
							stringBuilder.Clear();
							if (a == "ib")
							{
								num = 0;
							}
							else if (a == "if")
							{
								num = 1;
							}
							else if (a == "rb")
							{
								num = 2;
							}
							else if (a == "rf")
							{
								num = 3;
							}
						}
						else if (text[i] != ' ')
						{
							stringBuilder.Append(text[i]);
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000040F4 File Offset: 0x000022F4
		public static bool TryGetVoiceObject(TextObject to, out VoiceObject vo)
		{
			if (!TextObject.IsNullOrEmpty(to))
			{
				vo = MBTextManager.ProcessTextForVocalization(to);
				return true;
			}
			vo = null;
			return false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000410C File Offset: 0x0000230C
		private static VoiceObject ProcessTextForVocalization(TextObject to)
		{
			if (TextObject.IsNullOrEmpty(to))
			{
				return null;
			}
			string localizationId = MBTextManager.GetLocalizationId(to);
			if (localizationId != "!")
			{
				return LocalizedVoiceManager.GetLocalizedVoice(localizationId);
			}
			Debug.Print("[VOICEOVER]VoiceObject search for: " + localizationId, 0, Debug.DebugColor.White, 17592186044416UL);
			List<MBTextToken> list = to.GetCachedTokens();
			if (list == null)
			{
				list = MBTextManager.Tokenizer.Tokenize(to.Value);
			}
			foreach (MBTextToken mbtextToken in list)
			{
				if (mbtextToken.TokenType == TokenType.Identifier)
				{
					VoiceObject voiceObject = MBTextManager.ProcessTextForVocalization(MBTextManager.TextContext.GetRawTextVariable(mbtextToken.Value, to));
					if (voiceObject != null)
					{
						return voiceObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000041E4 File Offset: 0x000023E4
		private static string GetLocalizationId(TextObject to)
		{
			string value = to.Value;
			if (value != null && value.Length > 2 && value[0] == '{' && value[1] == '=')
			{
				int num = 2;
				for (int i = num; i < value.Length; i++)
				{
					if (value[i] == '}')
					{
						IL_4F:
						return value.Substring(num, i - num);
					}
				}
				goto IL_4F;
			}
			return string.Empty;
		}

		// Token: 0x04000017 RID: 23
		public const string LinkAttribute = "LINK";

		// Token: 0x04000018 RID: 24
		internal const string LinkTag = ".link";

		// Token: 0x04000019 RID: 25
		internal const int LinkTagLength = 7;

		// Token: 0x0400001A RID: 26
		internal const string LinkEnding = "</b></a>";

		// Token: 0x0400001B RID: 27
		internal const int LinkEndingLength = 8;

		// Token: 0x0400001C RID: 28
		internal const string LinkStarter = "<a style=\"Link.";

		// Token: 0x0400001D RID: 29
		private const string CommentRegexPattern = "{%.+?}";

		// Token: 0x0400001E RID: 30
		private const string AnimationTagsRegexPattern = "\\[.+\\]";

		// Token: 0x0400001F RID: 31
		private static readonly TextProcessingContext TextContext = new TextProcessingContext();

		// Token: 0x04000020 RID: 32
		private static LanguageSpecificTextProcessor _languageProcessor = new EnglishTextProcessor();

		// Token: 0x04000021 RID: 33
		private static string _activeVoiceLanguageId = "English";

		// Token: 0x04000022 RID: 34
		private static string _activeTextLanguageId = "English";

		// Token: 0x04000023 RID: 35
		private static int _activeTextLanguageIndex = 0;

		// Token: 0x04000025 RID: 37
		[ThreadStatic]
		private static StringBuilder _idStringBuilder;

		// Token: 0x04000026 RID: 38
		[ThreadStatic]
		private static StringBuilder _targetStringBuilder;

		// Token: 0x04000027 RID: 39
		private static readonly Regex CommentRemoverRegex = new Regex("{%.+?}");

		// Token: 0x04000028 RID: 40
		private static readonly Regex AnimationTagRemoverRegex = new Regex("\\[.+\\]");

		// Token: 0x04000029 RID: 41
		internal static readonly Tokenizer Tokenizer = new Tokenizer();
	}
}
