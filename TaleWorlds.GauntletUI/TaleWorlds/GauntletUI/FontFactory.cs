using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000020 RID: 32
	public class FontFactory
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000DA00 File Offset: 0x0000BC00
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000DA08 File Offset: 0x0000BC08
		public string DefaultLangageID { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000DA11 File Offset: 0x0000BC11
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000DA19 File Offset: 0x0000BC19
		public string CurrentLangageID { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000DA22 File Offset: 0x0000BC22
		public Font DefaultFont
		{
			get
			{
				return this.GetCurrentLanguage().DefaultFont;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DA2F File Offset: 0x0000BC2F
		public FontFactory(ResourceDepot resourceDepot)
		{
			this._resourceDepot = resourceDepot;
			this._bitmapFonts = new Dictionary<string, Font>();
			this._fontLanguageMap = new Dictionary<string, Language>();
			this._resourceDepot.OnResourceChange += this.OnResourceChange;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DA6B File Offset: 0x0000BC6B
		private void OnResourceChange()
		{
			this.CheckForUpdates();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DA74 File Offset: 0x0000BC74
		public void LoadAllFonts(SpriteData spriteData)
		{
			foreach (string path in this._resourceDepot.GetFiles("Fonts", ".fnt", false))
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
				this.TryAddFontDefinition(Path.GetDirectoryName(path) + "/", fileNameWithoutExtension, spriteData);
			}
			foreach (string text in this._resourceDepot.GetFiles("Fonts", ".xml", false))
			{
				if (Path.GetFileNameWithoutExtension(text).EndsWith("Languages"))
				{
					try
					{
						this.LoadLocalizationValues(text);
					}
					catch (Exception)
					{
						Debug.FailedAssert("Failed to load language at path: " + text, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\FontFactory.cs", "LoadAllFonts", 71);
					}
				}
			}
			if (string.IsNullOrEmpty(this.DefaultLangageID))
			{
				this.DefaultLangageID = "English";
				this.CurrentLangageID = this.DefaultLangageID;
			}
			this._latestSpriteData = spriteData;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public bool TryAddFontDefinition(string fontPath, string fontName, SpriteData spriteData)
		{
			Font font = new Font(fontName);
			string path = fontPath + fontName + ".fnt";
			bool flag = font.TryLoadFontFromPath(path, spriteData);
			if (flag)
			{
				this._bitmapFonts.Add(fontName, font);
			}
			return flag;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public void LoadLocalizationValues(string sourceXMLPath)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(sourceXMLPath);
			XmlElement xmlElement = xmlDocument["Languages"];
			XmlAttribute xmlAttribute = xmlElement.Attributes["DefaultLanguage"];
			if (!string.IsNullOrEmpty((xmlAttribute != null) ? xmlAttribute.InnerText : null))
			{
				XmlAttribute xmlAttribute2 = xmlElement.Attributes["DefaultLanguage"];
				this.DefaultLangageID = (((xmlAttribute2 != null) ? xmlAttribute2.InnerText : null) ?? "English");
				this.CurrentLangageID = this.DefaultLangageID;
			}
			foreach (object obj in xmlElement)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == "Language")
				{
					Language language = Language.CreateFrom(xmlNode, this);
					Language language2;
					if (this._fontLanguageMap.TryGetValue(language.LanguageID, out language2))
					{
						this._fontLanguageMap[language.LanguageID] = language;
					}
					else
					{
						this._fontLanguageMap.Add(language.LanguageID, language);
					}
				}
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public Language GetCurrentLanguage()
		{
			Language language;
			Language language2;
			if (this._fontLanguageMap.TryGetValue(this.CurrentLangageID, out language))
			{
				language2 = language;
			}
			else if (this.DefaultLangageID != null && this._fontLanguageMap.TryGetValue(this.DefaultLangageID, out language))
			{
				Debug.Print("Couldn't find language in language map: " + this.CurrentLangageID, 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert("Couldn't find language in language map: " + this.CurrentLangageID, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\FontFactory.cs", "GetCurrentLanguage", 148);
				language2 = language;
			}
			else if (this._fontLanguageMap.TryGetValue("English", out language))
			{
				Debug.Print("Couldn't find default language(" + (this.DefaultLangageID ?? "INVALID") + ") in language map.", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert("Couldn't find default language(" + (this.DefaultLangageID ?? "INVALID") + ") in language map.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\FontFactory.cs", "GetCurrentLanguage", 154);
				language2 = language;
			}
			else
			{
				Debug.Print("Couldn't find English language in language map.", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert("Couldn't find English language in language map.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\FontFactory.cs", "GetCurrentLanguage", 160);
				language2 = this._fontLanguageMap.FirstOrDefault<KeyValuePair<string, Language>>().Value;
			}
			if (language2 == null)
			{
				Debug.Print("There are no languages in language map", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert("There are no languages in language map", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\FontFactory.cs", "GetCurrentLanguage", 167);
			}
			return language2;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000DE4B File Offset: 0x0000C04B
		public Font GetFont(string fontName)
		{
			if (this._bitmapFonts.ContainsKey(fontName))
			{
				return this._bitmapFonts[fontName];
			}
			return this.DefaultFont;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000DE6E File Offset: 0x0000C06E
		public IEnumerable<Font> GetFonts()
		{
			return this._bitmapFonts.Values;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public string GetFontName(Font font)
		{
			return this._bitmapFonts.FirstOrDefault((KeyValuePair<string, Font> f) => f.Value == font).Key;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public Font GetMappedFontForLocalization(string englishFontName)
		{
			if (string.IsNullOrEmpty(englishFontName))
			{
				return this.DefaultFont;
			}
			if (!(this.CurrentLangageID != this.DefaultLangageID))
			{
				return this.GetFont(englishFontName);
			}
			Language currentLanguage = this.GetCurrentLanguage();
			if (currentLanguage.FontMapHasKey(englishFontName))
			{
				return currentLanguage.GetMappedFont(englishFontName);
			}
			return this.DefaultFont;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000DF0D File Offset: 0x0000C10D
		public void OnLanguageChange(string newLanguageCode)
		{
			this.CurrentLangageID = newLanguageCode;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000DF18 File Offset: 0x0000C118
		public Font GetUsableFontForCharacter(int characterCode)
		{
			for (int i = 0; i < this._fontLanguageMap.Values.Count; i++)
			{
				if (this._fontLanguageMap.ElementAt(i).Value.DefaultFont.Characters.ContainsKey(characterCode))
				{
					return this._fontLanguageMap.ElementAt(i).Value.DefaultFont;
				}
			}
			return null;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000DF84 File Offset: 0x0000C184
		public void CheckForUpdates()
		{
			string currentLangageID = this.CurrentLangageID;
			this.CurrentLangageID = null;
			this.DefaultLangageID = null;
			this._bitmapFonts.Clear();
			this._fontLanguageMap.Clear();
			this.LoadAllFonts(this._latestSpriteData);
			this.CurrentLangageID = currentLangageID;
		}

		// Token: 0x0400015A RID: 346
		private readonly Dictionary<string, Font> _bitmapFonts;

		// Token: 0x0400015B RID: 347
		private readonly ResourceDepot _resourceDepot;

		// Token: 0x0400015C RID: 348
		private readonly Dictionary<string, Language> _fontLanguageMap;

		// Token: 0x0400015D RID: 349
		private SpriteData _latestSpriteData;
	}
}
