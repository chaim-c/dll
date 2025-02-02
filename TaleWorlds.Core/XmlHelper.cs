using System;
using System.Globalization;
using System.Xml;

namespace TaleWorlds.Core
{
	// Token: 0x020000CA RID: 202
	public static class XmlHelper
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00020738 File Offset: 0x0001E938
		public static int ReadInt(XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute == null)
			{
				return 0;
			}
			return int.Parse(xmlAttribute.Value);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00020764 File Offset: 0x0001E964
		public static void ReadInt(ref int val, XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute != null)
			{
				val = int.Parse(xmlAttribute.Value);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00020790 File Offset: 0x0001E990
		public static float ReadFloat(XmlNode node, string str, float defaultValue = 0f)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute == null)
			{
				return defaultValue;
			}
			return float.Parse(xmlAttribute.Value);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000207BC File Offset: 0x0001E9BC
		public static string ReadString(XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute == null)
			{
				return "";
			}
			return xmlAttribute.Value;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public static void ReadHexCode(ref uint val, XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute != null)
			{
				string text = xmlAttribute.Value;
				text = text.Substring(2);
				val = uint.Parse(text, NumberStyles.HexNumber);
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00020824 File Offset: 0x0001EA24
		public static bool ReadBool(XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			return xmlAttribute != null && Convert.ToBoolean(xmlAttribute.InnerText);
		}
	}
}
