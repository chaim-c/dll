using System;
using System.Xml;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012B RID: 299
	internal class ItemInnerData
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00008C25 File Offset: 0x00006E25
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00008C2D File Offset: 0x00006E2D
		internal string TypeId { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00008C36 File Offset: 0x00006E36
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00008C3E File Offset: 0x00006E3E
		internal ItemType Type { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00008C47 File Offset: 0x00006E47
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00008C4F File Offset: 0x00006E4F
		internal int Price { get; private set; }

		// Token: 0x060006AB RID: 1707 RVA: 0x00008C58 File Offset: 0x00006E58
		internal void Deserialize(XmlNode node)
		{
			this.TypeId = node.Attributes["id"].Value;
			this.Price = ((node.Attributes["value"] != null) ? int.Parse(node.Attributes["value"].Value) : 0);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "flags")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "flag" && xmlNode2.Attributes["name"].Value == "type")
						{
							string value = xmlNode2.Attributes["value"].Value;
							this.Type = (ItemType)Enum.Parse(typeof(ItemType), value, true);
						}
					}
				}
			}
		}
	}
}
