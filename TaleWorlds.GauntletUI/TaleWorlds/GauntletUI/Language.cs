using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000021 RID: 33
	public class Language : ILanguage
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000DFCF File Offset: 0x0000C1CF
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000DFD7 File Offset: 0x0000C1D7
		public char[] ForbiddenStartOfLineCharacters { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		public char[] ForbiddenEndOfLineCharacters { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000DFF1 File Offset: 0x0000C1F1
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000DFF9 File Offset: 0x0000C1F9
		public string LanguageID { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000E002 File Offset: 0x0000C202
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000E00A File Offset: 0x0000C20A
		public string DefaultFontName { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000E013 File Offset: 0x0000C213
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000E01B File Offset: 0x0000C21B
		public bool DoesFontRequireSpaceForNewline { get; private set; } = true;

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000E024 File Offset: 0x0000C224
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000E02C File Offset: 0x0000C22C
		public Font DefaultFont { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000E035 File Offset: 0x0000C235
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000E03D File Offset: 0x0000C23D
		public char LineSeperatorChar { get; private set; }

		// Token: 0x060002CF RID: 719 RVA: 0x0000E046 File Offset: 0x0000C246
		public bool FontMapHasKey(string keyFontName)
		{
			return this._fontMap.ContainsKey(keyFontName);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E054 File Offset: 0x0000C254
		public Font GetMappedFont(string keyFontName)
		{
			return this._fontMap[keyFontName];
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E062 File Offset: 0x0000C262
		private Language()
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E07C File Offset: 0x0000C27C
		public static Language CreateFrom(XmlNode languageNode, FontFactory fontFactory)
		{
			Language language = new Language
			{
				LanguageID = languageNode.Attributes["id"].InnerText
			};
			Language language2 = language;
			XmlAttribute xmlAttribute = languageNode.Attributes["DefaultFont"];
			language2.DefaultFontName = (((xmlAttribute != null) ? xmlAttribute.InnerText : null) ?? "Galahad");
			Language language3 = language;
			XmlAttribute xmlAttribute2 = languageNode.Attributes["LineSeperatorChar"];
			language3.LineSeperatorChar = ((xmlAttribute2 != null) ? xmlAttribute2.InnerText[0] : '-');
			language.DefaultFont = fontFactory.GetFont(language.DefaultFontName);
			foreach (object obj in languageNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (xmlNode.Name == "Map")
					{
						string innerText = xmlNode.Attributes["From"].InnerText;
						string innerText2 = xmlNode.Attributes["To"].InnerText;
						language._fontMap.Add(innerText, fontFactory.GetFont(innerText2));
					}
					else if (xmlNode.Name == "NewlineDoesntRequireSpace")
					{
						language.DoesFontRequireSpaceForNewline = false;
					}
					else if (xmlNode.Name == "ForbiddenStartOfLineCharacters")
					{
						Language language4 = language;
						XmlAttribute xmlAttribute3 = xmlNode.Attributes["Characters"];
						language4.ForbiddenStartOfLineCharacters = ((xmlAttribute3 != null) ? xmlAttribute3.InnerText.ToCharArray() : null);
					}
					else if (xmlNode.Name == "ForbiddenEndOfLineCharacters")
					{
						Language language5 = language;
						XmlAttribute xmlAttribute4 = xmlNode.Attributes["Characters"];
						language5.ForbiddenEndOfLineCharacters = ((xmlAttribute4 != null) ? xmlAttribute4.InnerText.ToCharArray() : null);
					}
				}
			}
			return language;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E260 File Offset: 0x0000C460
		IEnumerable<char> ILanguage.GetForbiddenStartOfLineCharacters()
		{
			return this.ForbiddenStartOfLineCharacters;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E268 File Offset: 0x0000C468
		IEnumerable<char> ILanguage.GetForbiddenEndOfLineCharacters()
		{
			return this.ForbiddenEndOfLineCharacters;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E270 File Offset: 0x0000C470
		bool ILanguage.IsCharacterForbiddenAtStartOfLine(char character)
		{
			if (this.ForbiddenStartOfLineCharacters == null || this.ForbiddenStartOfLineCharacters.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < this.ForbiddenStartOfLineCharacters.Length; i++)
			{
				if (this.ForbiddenStartOfLineCharacters[i] == character)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		bool ILanguage.IsCharacterForbiddenAtEndOfLine(char character)
		{
			if (this.ForbiddenEndOfLineCharacters == null || this.ForbiddenEndOfLineCharacters.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < this.ForbiddenEndOfLineCharacters.Length; i++)
			{
				if (this.ForbiddenEndOfLineCharacters[i] == character)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E2F5 File Offset: 0x0000C4F5
		string ILanguage.GetLanguageID()
		{
			return this.LanguageID;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E2FD File Offset: 0x0000C4FD
		string ILanguage.GetDefaultFontName()
		{
			return this.DefaultFontName;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E305 File Offset: 0x0000C505
		Font ILanguage.GetDefaultFont()
		{
			return this.DefaultFont;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E30D File Offset: 0x0000C50D
		char ILanguage.GetLineSeperatorChar()
		{
			return this.LineSeperatorChar;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E315 File Offset: 0x0000C515
		bool ILanguage.DoesLanguageRequireSpaceForNewline()
		{
			return this.DoesFontRequireSpaceForNewline;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E31D File Offset: 0x0000C51D
		bool ILanguage.FontMapHasKey(string keyFontName)
		{
			return this._fontMap.ContainsKey(keyFontName);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E32B File Offset: 0x0000C52B
		Font ILanguage.GetMappedFont(string keyFontName)
		{
			return this._fontMap[keyFontName];
		}

		// Token: 0x04000165 RID: 357
		private readonly Dictionary<string, Font> _fontMap = new Dictionary<string, Font>();
	}
}
