using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization
{
	// Token: 0x02000007 RID: 7
	public static class LocalizedTextManager
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002E38 File Offset: 0x00001038
		public static string GetTranslatedText(string languageId, string id)
		{
			LocalizedText localizedText;
			if (LocalizedTextManager._gameTextDictionary.TryGetValue(id, out localizedText))
			{
				return localizedText.GetTranslatedText(languageId);
			}
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E60 File Offset: 0x00001060
		public static List<string> GetLanguageIds(bool developmentMode)
		{
			List<string> list = new List<string>();
			foreach (LanguageData languageData in LanguageData.All)
			{
				bool flag = developmentMode || !languageData.IsUnderDevelopment;
				if (languageData != null && languageData.IsValid && flag)
				{
					list.Add(languageData.StringId);
				}
			}
			return list;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EE0 File Offset: 0x000010E0
		public static string GetLanguageTitle(string id)
		{
			LanguageData languageData = LanguageData.GetLanguageData(id);
			if (languageData != null)
			{
				return languageData.Title;
			}
			return LanguageData.GetLanguageData("English").Title;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F10 File Offset: 0x00001110
		public static LanguageSpecificTextProcessor CreateTextProcessorForLanguage(string id)
		{
			LanguageData languageData = LanguageData.GetLanguageData(id);
			if (languageData == null || languageData.TextProcessor == null)
			{
				return new DefaultTextProcessor();
			}
			Type type = Type.GetType(languageData.TextProcessor);
			if (type == null)
			{
				Debug.FailedAssert("Can't find the type: " + languageData.TextProcessor, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\LocalizedTextManager.cs", "CreateTextProcessorForLanguage", 71);
				return new DefaultTextProcessor();
			}
			return (LanguageSpecificTextProcessor)Activator.CreateInstance(type);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F7C File Offset: 0x0000117C
		public static void AddLanguageTest(string id, string processor)
		{
			LanguageData languageData = new LanguageData(id);
			languageData.InitializeDefault(id, new string[]
			{
				id
			}, id, processor, false);
			LanguageData.LoadTestData(languageData);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FAC File Offset: 0x000011AC
		public static int GetLanguageIndex(string id)
		{
			int languageDataIndex = LanguageData.GetLanguageDataIndex(id);
			if (languageDataIndex == -1)
			{
				languageDataIndex = LanguageData.GetLanguageDataIndex("English");
			}
			return languageDataIndex;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FD0 File Offset: 0x000011D0
		public static void LoadLocalizationXmls(string[] loadedModules)
		{
			LanguageData.Clear();
			for (int i = 0; i < loadedModules.Length; i++)
			{
				string text = loadedModules[i] + "/ModuleData/Languages";
				if (Directory.Exists(text))
				{
					string[] files = Directory.GetFiles(text, "language_data.xml", SearchOption.AllDirectories);
					for (int j = 0; j < files.Length; j++)
					{
						XmlDocument xmlDocument = LocalizedTextManager.LoadXmlFile(files[j]);
						if (xmlDocument != null)
						{
							LanguageData.LoadFromXml(xmlDocument, text);
						}
					}
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003040 File Offset: 0x00001240
		public static string GetDateFormattedByLanguage(string languageCode, DateTime dateTime)
		{
			string shortDatePattern = LocalizedTextManager.GetCultureInfo(languageCode).DateTimeFormat.ShortDatePattern;
			return dateTime.ToString(shortDatePattern);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003068 File Offset: 0x00001268
		public static string GetTimeFormattedByLanguage(string languageCode, DateTime dateTime)
		{
			string shortTimePattern = LocalizedTextManager.GetCultureInfo(languageCode).DateTimeFormat.ShortTimePattern;
			return dateTime.ToString(shortTimePattern);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000308E File Offset: 0x0000128E
		public static string GetSubtitleExtensionOfLanguage(string languageId)
		{
			return LocalizedTextManager.GetLanguageData(languageId).SubtitleExtension;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000309C File Offset: 0x0000129C
		public static string GetLocalizationCodeOfISOLanguageCode(string isoLanguageCode)
		{
			foreach (LanguageData languageData in LanguageData.All)
			{
				string[] supportedIsoCodes = languageData.SupportedIsoCodes;
				for (int i = 0; i < supportedIsoCodes.Length; i++)
				{
					if (string.Equals(supportedIsoCodes[i], isoLanguageCode, StringComparison.InvariantCultureIgnoreCase))
					{
						return languageData.StringId;
					}
				}
			}
			Debug.FailedAssert("Undefined language code " + isoLanguageCode, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\LocalizedTextManager.cs", "GetLocalizationCodeOfISOLanguageCode", 166);
			return "English";
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000313C File Offset: 0x0000133C
		private static CultureInfo GetCultureInfo(string languageId)
		{
			LanguageData languageData = LocalizedTextManager.GetLanguageData(languageId);
			CultureInfo result = CultureInfo.InvariantCulture;
			if (languageData.SupportedIsoCodes != null && languageData.SupportedIsoCodes.Length != 0)
			{
				result = new CultureInfo(languageData.SupportedIsoCodes[0]);
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003178 File Offset: 0x00001378
		private static LanguageData GetLanguageData(string languageId)
		{
			LanguageData languageData = LanguageData.GetLanguageData(languageId);
			if (languageData == null || !languageData.IsValid)
			{
				languageData = LanguageData.GetLanguageData("English");
				Debug.FailedAssert("Undefined language code: " + languageId, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\LocalizedTextManager.cs", "GetLanguageData", 189);
			}
			return languageData;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031C4 File Offset: 0x000013C4
		private static XmlDocument LoadXmlFile(string path)
		{
			try
			{
				Debug.Print("opening " + path, 0, Debug.DebugColor.White, 17592186044416UL);
				XmlDocument xmlDocument = new XmlDocument();
				StreamReader streamReader = new StreamReader(path);
				string xml = streamReader.ReadToEnd();
				xmlDocument.LoadXml(xml);
				streamReader.Close();
				return xmlDocument;
			}
			catch
			{
				Debug.FailedAssert("Could not parse: " + path, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\LocalizedTextManager.cs", "LoadXmlFile", 209);
			}
			return null;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003248 File Offset: 0x00001448
		internal static void LoadLanguage(string languageId)
		{
			LocalizedTextManager._gameTextDictionary.Clear();
			LanguageData languageData = LanguageData.GetLanguageData(languageId);
			if (languageData != null)
			{
				LocalizedTextManager.LoadLanguage(languageData);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003270 File Offset: 0x00001470
		private static void LoadLanguage(LanguageData language)
		{
			MBTextManager.ResetFunctions();
			string stringId = language.StringId;
			bool flag = stringId != "English";
			foreach (string path in language.XmlPaths)
			{
				XmlDocument xmlDocument = LocalizedTextManager.LoadXmlFile(path);
				if (xmlDocument != null)
				{
					for (XmlNode xmlNode = xmlDocument.ChildNodes[1].FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
					{
						if (xmlNode.Name == "strings" && xmlNode.HasChildNodes)
						{
							if (flag)
							{
								for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
								{
									if (xmlNode2.Name == "string" && xmlNode2.NodeType != XmlNodeType.Comment)
									{
										LocalizedTextManager.DeserializeStrings(xmlNode2, stringId);
									}
								}
							}
						}
						else if (xmlNode.Name == "functions" && xmlNode.HasChildNodes)
						{
							for (XmlNode xmlNode3 = xmlNode.FirstChild; xmlNode3 != null; xmlNode3 = xmlNode3.NextSibling)
							{
								if (xmlNode3.Name == "function" && xmlNode3.NodeType != XmlNodeType.Comment)
								{
									string value = xmlNode3.Attributes["functionName"].Value;
									string value2 = xmlNode3.Attributes["functionBody"].Value;
									MBTextManager.SetFunction(value, value2);
								}
							}
						}
					}
				}
			}
			Debug.Print("Loading localized text xml.", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000341C File Offset: 0x0000161C
		private static void DeserializeStrings(XmlNode node, string languageId)
		{
			if (node.Attributes == null)
			{
				throw new TWXmlLoadException("Node attributes are null!");
			}
			string value = node.Attributes["id"].Value;
			string value2 = node.Attributes["text"].Value;
			if (!LocalizedTextManager._gameTextDictionary.ContainsKey(value))
			{
				LocalizedText value3 = new LocalizedText();
				LocalizedTextManager._gameTextDictionary.Add(value, value3);
			}
			LocalizedTextManager._gameTextDictionary[value].AddTranslation(languageId, value2);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000349C File Offset: 0x0000169C
		[CommandLineFunctionality.CommandLineArgumentFunction("change_language", "localization")]
		public static string ChangeLanguage(List<string> strings)
		{
			if (strings.Count != 1)
			{
				return "Format is \"localization.change_language [LanguageCode/LanguageName/ISOCode]\".";
			}
			string value = strings[0];
			int activeTextLanguageIndex = MBTextManager.GetActiveTextLanguageIndex();
			string text = null;
			foreach (string text2 in LocalizedTextManager.GetLanguageIds(true))
			{
				if (LocalizedTextManager.GetLanguageTitle(text2).Equals(value, StringComparison.OrdinalIgnoreCase) || LocalizedTextManager.GetSubtitleExtensionOfLanguage(text2).Contains(value))
				{
					text = text2;
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return "cant find the language in current configuration.";
			}
			if (LocalizedTextManager.GetLanguageIndex(text) == activeTextLanguageIndex)
			{
				return "Same language";
			}
			MBTextManager.ChangeLanguage(text);
			return "New language is " + text;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000355C File Offset: 0x0000175C
		[CommandLineFunctionality.CommandLineArgumentFunction("reload_texts", "localization")]
		public static string ReloadTexts(List<string> strings)
		{
			LocalizedTextManager.LoadLanguage(MBTextManager.ActiveTextLanguage);
			return "OK";
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003570 File Offset: 0x00001770
		[CommandLineFunctionality.CommandLineArgumentFunction("check_for_errors", "localization")]
		public static string CheckValidity(List<string> strings)
		{
			if (File.Exists("faulty_translation_lines.txt"))
			{
				File.Delete("faulty_translation_lines.txt");
			}
			bool flag = false;
			foreach (string language in LocalizedTextManager.GetLanguageIds(false))
			{
				MBTextManager.ChangeLanguage(language);
				LocalizedTextManager.<CheckValidity>g__Write|22_0("Testing Language: " + MBTextManager.ActiveTextLanguage + "\n\n");
				foreach (KeyValuePair<string, LocalizedText> keyValuePair in LocalizedTextManager._gameTextDictionary)
				{
					string key = keyValuePair.Key;
					string s;
					bool flag2 = keyValuePair.Value.CheckValidity(key, out s);
					if (flag2)
					{
						LocalizedTextManager.<CheckValidity>g__Write|22_0(s);
					}
					flag = (flag2 || flag);
				}
				LocalizedTextManager.<CheckValidity>g__Write|22_0("\nTesting Language: " + MBTextManager.ActiveTextLanguage + "\n\n");
			}
			if (!flag)
			{
				return "No errors are found.";
			}
			return "Errors are written into 'faulty_translation_lines.txt' file in the binary folder.";
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000368C File Offset: 0x0000188C
		[CompilerGenerated]
		internal static void <CheckValidity>g__Write|22_0(string s)
		{
			File.AppendAllText("faulty_translation_lines.txt", s, Encoding.Unicode);
		}

		// Token: 0x04000011 RID: 17
		public const string LanguageDataFileName = "language_data";

		// Token: 0x04000012 RID: 18
		public const string DefaultEnglishLanguageId = "English";

		// Token: 0x04000013 RID: 19
		private static readonly Dictionary<string, LocalizedText> _gameTextDictionary = new Dictionary<string, LocalizedText>();
	}
}
