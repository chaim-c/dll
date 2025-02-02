using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x02000072 RID: 114
	public class GameTextManager
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x00019589 File Offset: 0x00017789
		public GameTextManager()
		{
			this._gameTexts = new Dictionary<string, GameText>();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001959C File Offset: 0x0001779C
		public GameText GetGameText(string id)
		{
			GameText result;
			if (this._gameTexts.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000195BC File Offset: 0x000177BC
		public GameText AddGameText(string id)
		{
			GameText gameText;
			if (!this._gameTexts.TryGetValue(id, out gameText))
			{
				gameText = new GameText(id);
				this._gameTexts.Add(gameText.Id, gameText);
			}
			return gameText;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000195F4 File Offset: 0x000177F4
		public bool TryGetText(string id, string variation, out TextObject text)
		{
			text = null;
			GameText gameText;
			this._gameTexts.TryGetValue(id, out gameText);
			if (gameText != null)
			{
				if (variation == null)
				{
					text = gameText.DefaultText;
				}
				else
				{
					text = gameText.GetVariation(variation);
				}
				if (text != null)
				{
					text = text.CopyTextObject();
					text.AddIDToValue(id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00019644 File Offset: 0x00017844
		public TextObject FindText(string id, string variation = null)
		{
			TextObject result;
			if (this.TryGetText(id, variation, out result))
			{
				return result;
			}
			TextObject result2;
			if (variation == null)
			{
				result2 = new TextObject("{=!}ERROR: Text with id " + id + " doesn't exist!", null);
			}
			else
			{
				result2 = new TextObject("{=!}ERROR: Text with id " + id + " doesn't exist! Variation: " + variation, null);
			}
			return result2;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00019694 File Offset: 0x00017894
		public IEnumerable<TextObject> FindAllTextVariations(string id)
		{
			GameText gameText;
			this._gameTexts.TryGetValue(id, out gameText);
			if (gameText != null)
			{
				foreach (GameText.GameTextVariation gameTextVariation in gameText.Variations)
				{
					yield return gameTextVariation.Text;
				}
				IEnumerator<GameText.GameTextVariation> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000196AC File Offset: 0x000178AC
		public void LoadGameTexts()
		{
			Game game = Game.Current;
			bool ignoreGameTypeInclusionCheck = false;
			string gameType = "";
			if (game != null)
			{
				ignoreGameTypeInclusionCheck = game.GameType.IsDevelopment;
				gameType = game.GameType.GetType().Name;
			}
			XmlDocument mergedXmlForManaged = MBObjectManager.GetMergedXmlForManaged("GameText", false, ignoreGameTypeInclusionCheck, gameType);
			try
			{
				this.LoadFromXML(mergedXmlForManaged);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00019714 File Offset: 0x00017914
		public void LoadDefaultTexts()
		{
			string text = ModuleHelper.GetModuleFullPath("Native") + "ModuleData/global_strings.xml";
			Debug.Print("opening " + text, 0, Debug.DebugColor.White, 17592186044416UL);
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(text);
			string xml = streamReader.ReadToEnd();
			xmlDocument.LoadXml(xml);
			streamReader.Close();
			this.LoadFromXML(xmlDocument);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00019778 File Offset: 0x00017978
		private void LoadFromXML(XmlDocument doc)
		{
			XmlNode xmlNode = null;
			for (int i = 0; i < doc.ChildNodes.Count; i++)
			{
				XmlNode xmlNode2 = doc.ChildNodes[i];
				if (xmlNode2.NodeType != XmlNodeType.Comment && xmlNode2.Name == "strings" && xmlNode2.ChildNodes.Count > 0)
				{
					xmlNode = xmlNode2.ChildNodes[0];
					IL_1F8:
					while (xmlNode != null)
					{
						if (xmlNode.Name == "string" && xmlNode.NodeType != XmlNodeType.Comment)
						{
							if (xmlNode.Attributes == null)
							{
								throw new TWXmlLoadException("Node attributes are null.");
							}
							string[] array = xmlNode.Attributes["id"].Value.Split(new char[]
							{
								'.'
							});
							string id = array[0];
							GameText gameText = this.AddGameText(id);
							string variationId = "";
							if (array.Length > 1)
							{
								variationId = array[1];
							}
							TextObject textObject = new TextObject(xmlNode.Attributes["text"].Value, null);
							List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
							foreach (object obj in xmlNode.ChildNodes)
							{
								XmlNode xmlNode3 = (XmlNode)obj;
								if (xmlNode3.Name == "tags")
								{
									XmlNodeList childNodes = xmlNode3.ChildNodes;
									for (int j = 0; j < childNodes.Count; j++)
									{
										XmlAttributeCollection attributes = childNodes[j].Attributes;
										if (attributes != null)
										{
											int weight = 1;
											if (attributes["weight"] != null)
											{
												int.TryParse(attributes["weight"].Value, out weight);
											}
											GameTextManager.ChoiceTag item = new GameTextManager.ChoiceTag(attributes["tag_name"].Value, weight);
											list.Add(item);
										}
									}
								}
							}
							textObject.CacheTokens();
							gameText.AddVariationWithId(variationId, textObject, list);
						}
						xmlNode = xmlNode.NextSibling;
					}
					return;
				}
			}
			goto IL_1F8;
		}

		// Token: 0x040003BD RID: 957
		private readonly Dictionary<string, GameText> _gameTexts;

		// Token: 0x020000FD RID: 253
		public struct ChoiceTag
		{
			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002170B File Offset: 0x0001F90B
			// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00021713 File Offset: 0x0001F913
			public string TagName { get; private set; }

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002171C File Offset: 0x0001F91C
			// (set) Token: 0x06000A4A RID: 2634 RVA: 0x00021724 File Offset: 0x0001F924
			public uint Weight { get; private set; }

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002172D File Offset: 0x0001F92D
			// (set) Token: 0x06000A4C RID: 2636 RVA: 0x00021735 File Offset: 0x0001F935
			public bool IsTagReversed { get; private set; }

			// Token: 0x06000A4D RID: 2637 RVA: 0x0002173E File Offset: 0x0001F93E
			public ChoiceTag(string tagName, int weight)
			{
				this = default(GameTextManager.ChoiceTag);
				this.TagName = tagName;
				this.Weight = (uint)MathF.Abs(weight);
				this.IsTagReversed = (weight < 0);
			}
		}
	}
}
