using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics
{
	// Token: 0x02000183 RID: 387
	public static class CosmeticsManager
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001229D File Offset: 0x0001049D
		static CosmeticsManager()
		{
			CosmeticsManager.LoadFromXml(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/mpcosmetics.xml");
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x000122CC File Offset: 0x000104CC
		public static MBReadOnlyList<CosmeticElement> CosmeticElementsList
		{
			get
			{
				return CosmeticsManager._cosmeticElementList;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000122D4 File Offset: 0x000104D4
		public static CosmeticElement GetCosmeticElement(string cosmeticId)
		{
			CosmeticElement result;
			if (CosmeticsManager._cosmeticElementsLookup.TryGetValue(cosmeticId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000122F4 File Offset: 0x000104F4
		public static void LoadFromXml(string path)
		{
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(path);
			streamReader.ReadToEnd();
			xmlDocument.Load(path);
			streamReader.Close();
			CosmeticsManager._cosmeticElementsLookup.Clear();
			MBList<CosmeticElement> mblist = new MBList<CosmeticElement>();
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Cosmetics")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "Cosmetic")
						{
							string value = xmlNode2.Attributes["id"].Value;
							CosmeticsManager.CosmeticType type = CosmeticsManager.CosmeticType.Clothing;
							string value2 = xmlNode2.Attributes["type"].Value;
							if (value2 == "Clothing")
							{
								type = CosmeticsManager.CosmeticType.Clothing;
							}
							else if (value2 == "Frame")
							{
								type = CosmeticsManager.CosmeticType.Frame;
							}
							else if (value2 == "Sigil")
							{
								type = CosmeticsManager.CosmeticType.Sigil;
							}
							else if (value2 == "Taunt")
							{
								type = CosmeticsManager.CosmeticType.Taunt;
							}
							else
							{
								Debug.FailedAssert("Invalid cosmetic type: " + value2, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Cosmetics\\CosmeticsManager.cs", "LoadFromXml", 103);
							}
							CosmeticsManager.CosmeticRarity rarity = CosmeticsManager.CosmeticRarity.Common;
							string value3 = xmlNode2.Attributes["rarity"].Value;
							if (value3 == "Common")
							{
								rarity = CosmeticsManager.CosmeticRarity.Common;
							}
							else if (value3 == "Rare")
							{
								rarity = CosmeticsManager.CosmeticRarity.Rare;
							}
							else if (value3 == "Unique")
							{
								rarity = CosmeticsManager.CosmeticRarity.Unique;
							}
							else
							{
								Debug.FailedAssert("Invalid cosmetic rarity: " + value3, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Cosmetics\\CosmeticsManager.cs", "LoadFromXml", 123);
							}
							int cost = int.Parse(xmlNode2.Attributes["cost"].Value);
							switch (type)
							{
							case CosmeticsManager.CosmeticType.Clothing:
							{
								List<string> list = new List<string>();
								List<Tuple<string, string>> list2 = new List<Tuple<string, string>>();
								foreach (object obj3 in xmlNode2.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj3;
									if (xmlNode3.Name == "Replace")
									{
										foreach (object obj4 in xmlNode3.ChildNodes)
										{
											XmlNode xmlNode4 = (XmlNode)obj4;
											if (xmlNode4.Name == "Item")
											{
												list.Add(xmlNode4.Attributes.Item(0).Value);
											}
											else if (xmlNode4.Name == "Itemless")
											{
												list2.Add(Tuple.Create<string, string>(xmlNode4.Attributes.Item(0).Value, xmlNode4.Attributes.Item(1).Value));
											}
										}
									}
								}
								mblist.Add(new ClothingCosmeticElement(value, rarity, cost, list, list2));
								break;
							}
							case CosmeticsManager.CosmeticType.Frame:
								mblist.Add(new CosmeticElement(value, rarity, cost, type));
								break;
							case CosmeticsManager.CosmeticType.Sigil:
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								string text;
								if (attributes == null)
								{
									text = null;
								}
								else
								{
									XmlAttribute xmlAttribute = attributes["banner_code"];
									text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
								}
								string bannerCode = text;
								mblist.Add(new SigilCosmeticElement(value, rarity, cost, bannerCode));
								break;
							}
							case CosmeticsManager.CosmeticType.Taunt:
							{
								XmlAttributeCollection attributes2 = xmlNode2.Attributes;
								string text2;
								if (attributes2 == null)
								{
									text2 = null;
								}
								else
								{
									XmlAttribute xmlAttribute2 = attributes2["name"];
									text2 = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
								}
								string name = text2;
								TauntCosmeticElement item = new TauntCosmeticElement(-1, value, rarity, cost, name);
								mblist.Add(item);
								break;
							}
							}
						}
					}
				}
			}
			CosmeticsManager._cosmeticElementsLookup = new Dictionary<string, CosmeticElement>();
			foreach (CosmeticElement cosmeticElement in mblist)
			{
				CosmeticsManager._cosmeticElementsLookup[cosmeticElement.Id] = cosmeticElement;
			}
			CosmeticsManager._cosmeticElementList = mblist;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000127B0 File Offset: 0x000109B0
		private static bool CheckForCosmeticsListDuplicatesDebug()
		{
			for (int i = 0; i < CosmeticsManager._cosmeticElementList.Count; i++)
			{
				for (int j = i + 1; j < CosmeticsManager._cosmeticElementList.Count; j++)
				{
					if (CosmeticsManager._cosmeticElementList[i].Id == CosmeticsManager._cosmeticElementList[j].Id)
					{
						Debug.FailedAssert(CosmeticsManager._cosmeticElementList[i].Id + " has more than one entry.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Cosmetics\\CosmeticsManager.cs", "CheckForCosmeticsListDuplicatesDebug", 200);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000529 RID: 1321
		private static MBReadOnlyList<CosmeticElement> _cosmeticElementList = new MBReadOnlyList<CosmeticElement>();

		// Token: 0x0400052A RID: 1322
		private static Dictionary<string, CosmeticElement> _cosmeticElementsLookup = new Dictionary<string, CosmeticElement>();

		// Token: 0x020001EC RID: 492
		public enum CosmeticRarity
		{
			// Token: 0x040006F5 RID: 1781
			Default,
			// Token: 0x040006F6 RID: 1782
			Common,
			// Token: 0x040006F7 RID: 1783
			Rare,
			// Token: 0x040006F8 RID: 1784
			Unique
		}

		// Token: 0x020001ED RID: 493
		public enum CosmeticType
		{
			// Token: 0x040006FA RID: 1786
			Clothing,
			// Token: 0x040006FB RID: 1787
			Frame,
			// Token: 0x040006FC RID: 1788
			Sigil,
			// Token: 0x040006FD RID: 1789
			Taunt
		}
	}
}
