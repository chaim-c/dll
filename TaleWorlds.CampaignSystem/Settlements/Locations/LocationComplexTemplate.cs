using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Settlements.Locations
{
	// Token: 0x0200036E RID: 878
	public sealed class LocationComplexTemplate : MBObjectBase
	{
		// Token: 0x060033AC RID: 13228 RVA: 0x000D6554 File Offset: 0x000D4754
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			List<string> list = new List<string>();
			base.Deserialize(objectManager, node);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Location")
				{
					if (xmlNode.Attributes == null)
					{
						throw new TWXmlLoadException("node.Attributes != null");
					}
					string value = xmlNode.Attributes["id"].Value;
					TextObject textObject = new TextObject(xmlNode.Attributes["name"].Value, null);
					string[] sceneNames = new string[]
					{
						(xmlNode.Attributes["scene_name"] != null) ? xmlNode.Attributes["scene_name"].Value : "",
						(xmlNode.Attributes["scene_name_1"] != null) ? xmlNode.Attributes["scene_name_1"].Value : "",
						(xmlNode.Attributes["scene_name_2"] != null) ? xmlNode.Attributes["scene_name_2"].Value : "",
						(xmlNode.Attributes["scene_name_3"] != null) ? xmlNode.Attributes["scene_name_3"].Value : ""
					};
					int prosperityMax = int.Parse(xmlNode.Attributes["max_prosperity"].Value);
					bool isIndoor = bool.Parse(xmlNode.Attributes["indoor"].Value);
					bool canBeReserved = xmlNode.Attributes["can_be_reserved"] != null && bool.Parse(xmlNode.Attributes["can_be_reserved"].Value);
					string innerText = xmlNode.Attributes["player_can_enter"].InnerText;
					string innerText2 = xmlNode.Attributes["player_can_see"].InnerText;
					string innerText3 = xmlNode.Attributes["ai_can_exit"].InnerText;
					string innerText4 = xmlNode.Attributes["ai_can_enter"].InnerText;
					list.Add(value);
					this.Locations.Add(new Location(value, textObject, textObject, prosperityMax, isIndoor, canBeReserved, innerText, innerText2, innerText3, innerText4, sceneNames, null));
				}
				if (xmlNode.Name == "Passages")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "Passage")
						{
							if (xmlNode2.Attributes == null)
							{
								throw new TWXmlLoadException("node.Attributes != null");
							}
							string value2 = xmlNode2.Attributes["location_1"].Value;
							if (!list.Contains(value2))
							{
								throw new TWXmlLoadException("Passage location does not exist with id :" + value2);
							}
							string value3 = xmlNode2.Attributes["location_2"].Value;
							if (!list.Contains(value3))
							{
								throw new TWXmlLoadException("Passage location does not exist with id :" + value2);
							}
							this.Passages.Add(new KeyValuePair<string, string>(value2, value3));
						}
					}
				}
			}
		}

		// Token: 0x040010AB RID: 4267
		public List<Location> Locations = new List<Location>();

		// Token: 0x040010AC RID: 4268
		public List<KeyValuePair<string, string>> Passages = new List<KeyValuePair<string, string>>();
	}
}
