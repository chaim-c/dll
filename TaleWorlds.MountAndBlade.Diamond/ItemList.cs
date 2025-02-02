using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012A RID: 298
	internal class ItemList
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x00008A98 File Offset: 0x00006C98
		static ItemList()
		{
			XmlDocument xmlDocument = new XmlDocument();
			string filename = ModuleHelper.GetModuleFullPath("Native") + "ModuleData/mpitems.xml";
			if (ConfigurationManager.GetAppSettings("MultiplayerItemsFileName") != null)
			{
				filename = ConfigurationManager.GetAppSettings("MultiplayerItemsFileName");
			}
			xmlDocument.Load(filename);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Items");
			if (xmlNode == null)
			{
				throw new Exception("'Items' node is not defined in mpitems.xml");
			}
			Debug.Print("---" + xmlNode.Name, 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.NodeType == XmlNodeType.Element)
				{
					ItemInnerData itemInnerData = new ItemInnerData();
					itemInnerData.Deserialize(xmlNode2);
					Debug.Print(itemInnerData.TypeId, 0, Debug.DebugColor.White, 17592186044416UL);
					if (!ItemList._items.ContainsKey(itemInnerData.TypeId))
					{
						ItemList._items.Add(itemInnerData.TypeId, itemInnerData);
					}
					else
					{
						Debug.Print("--- Item type id already exists, check mpitems.xml for item type Id:" + itemInnerData.TypeId, 0, Debug.DebugColor.White, 17592186044416UL);
					}
				}
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00008BEC File Offset: 0x00006DEC
		internal static ItemType GetItemTypeOf(string typeId)
		{
			return ItemList._items[typeId].Type;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00008BFE File Offset: 0x00006DFE
		internal static bool IsItemValid(string itemId, string modifierId)
		{
			return ItemList._items.ContainsKey(itemId);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00008C0B File Offset: 0x00006E0B
		internal static int GetPriceOf(string itemId, string modifierId)
		{
			return ItemList._items[itemId].Price;
		}

		// Token: 0x040002D5 RID: 725
		private static Dictionary<string, ItemInnerData> _items = new Dictionary<string, ItemInnerData>();
	}
}
