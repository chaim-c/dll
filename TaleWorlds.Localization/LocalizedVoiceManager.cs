using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.Localization
{
	// Token: 0x02000008 RID: 8
	public static class LocalizedVoiceManager
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000036A0 File Offset: 0x000018A0
		public static VoiceObject GetLocalizedVoice(string id)
		{
			VoiceObject result;
			if (LocalizedVoiceManager._voiceObjectDictionary.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000036C0 File Offset: 0x000018C0
		public static List<string> GetVoiceLanguageIds()
		{
			List<string> list = new List<string>();
			foreach (LanguageData languageData in LanguageData.All)
			{
				if (languageData != null && languageData.IsValid && languageData.VoiceXmlPathsAndModulePaths.Count > 0)
				{
					list.Add(languageData.StringId);
				}
			}
			return list;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003738 File Offset: 0x00001938
		internal static void LoadLanguage(string languageId)
		{
			LocalizedVoiceManager._voiceObjectDictionary.Clear();
			LanguageData languageData = LanguageData.GetLanguageData(languageId);
			if (languageData != null)
			{
				LocalizedVoiceManager.LoadLanguage(languageData);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003760 File Offset: 0x00001960
		private static XmlDocument LoadXmlFile(string xmlPath)
		{
			try
			{
				Debug.Print("opening " + xmlPath, 0, Debug.DebugColor.White, 17592186044416UL);
				XmlDocument xmlDocument = new XmlDocument();
				StreamReader streamReader = new StreamReader(xmlPath);
				string xml = streamReader.ReadToEnd();
				xmlDocument.LoadXml(xml);
				streamReader.Close();
				return xmlDocument;
			}
			catch
			{
				Debug.FailedAssert("Could not parse: " + xmlPath, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Localization\\LocalizedVoiceManager.cs", "LoadXmlFile", 69);
			}
			return null;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037E0 File Offset: 0x000019E0
		private static void LoadLanguage(LanguageData language)
		{
			foreach (KeyValuePair<string, string> keyValuePair in language.VoiceXmlPathsAndModulePaths)
			{
				XmlDocument xmlDocument = LocalizedVoiceManager.LoadXmlFile(keyValuePair.Key);
				if (xmlDocument != null)
				{
					XmlNode xmlNode = null;
					foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (xmlNode2.Name == "VoiceOvers")
						{
							xmlNode = xmlNode2;
							break;
						}
					}
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						if (xmlNode3.Name == "VoiceOver")
						{
							string innerText = xmlNode3.Attributes["id"].InnerText;
							if (LocalizedVoiceManager._voiceObjectDictionary.ContainsKey(innerText))
							{
								LocalizedVoiceManager._voiceObjectDictionary[innerText].AddVoicePaths(xmlNode3, keyValuePair.Value);
							}
							else
							{
								VoiceObject value = VoiceObject.Deserialize(xmlNode3, keyValuePair.Value);
								LocalizedVoiceManager._voiceObjectDictionary.Add(innerText, value);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000014 RID: 20
		private static readonly Dictionary<string, VoiceObject> _voiceObjectDictionary = new Dictionary<string, VoiceObject>();
	}
}
