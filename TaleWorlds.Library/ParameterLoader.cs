using System;
using System.IO;
using System.Xml;

namespace TaleWorlds.Library
{
	// Token: 0x02000071 RID: 113
	public class ParameterLoader
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public static ParameterContainer LoadParametersFromClientProfile(string configurationName)
		{
			ParameterContainer parameterContainer = new ParameterContainer();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(VirtualFolders.GetFileContent(BasePath.Name + "Parameters/ClientProfile.xml"));
			string innerText = xmlDocument.ChildNodes[0].Attributes["Value"].InnerText;
			ParameterLoader.LoadParametersInto(string.Concat(new string[]
			{
				"ClientProfiles/",
				innerText,
				"/",
				configurationName,
				".xml"
			}), parameterContainer);
			return parameterContainer;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public static void LoadParametersInto(string fileFullName, ParameterContainer parameters)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string filePath = BasePath.Name + "Parameters/" + fileFullName;
			xmlDocument.LoadXml(VirtualFolders.GetFileContent(filePath));
			foreach (object obj in xmlDocument.FirstChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Parameters")
				{
					XmlAttributeCollection attributes = xmlNode.Attributes;
					string text;
					if (attributes == null)
					{
						text = null;
					}
					else
					{
						XmlAttribute xmlAttribute = attributes["Platforms"];
						text = ((xmlAttribute != null) ? xmlAttribute.InnerText : null);
					}
					string text2 = text;
					if (!string.IsNullOrWhiteSpace(text2))
					{
						if (text2.Split(new char[]
						{
							','
						}).FindIndex((string p) => p.Trim().Equals(string.Concat(ApplicationPlatform.CurrentPlatform))) < 0)
						{
							continue;
						}
					}
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						string innerText = xmlNode2.Attributes["Name"].InnerText;
						string text3;
						string value;
						string text4;
						if (ParameterLoader.TryGetFromFile(xmlNode2, out text3))
						{
							value = text3;
						}
						else if (ParameterLoader.TryGetFromEnvironment(xmlNode2, out text4))
						{
							value = text4;
						}
						else if (xmlNode2.Attributes["DefaultValue"] != null)
						{
							value = xmlNode2.Attributes["DefaultValue"].InnerText;
						}
						else
						{
							value = xmlNode2.Attributes["Value"].InnerText;
						}
						parameters.AddParameter(innerText, value, true);
					}
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000CF30 File Offset: 0x0000B130
		private static bool TryGetFromFile(XmlNode node, out string value)
		{
			value = "";
			XmlAttributeCollection attributes = node.Attributes;
			if (((attributes != null) ? attributes["LoadFromFile"] : null) != null && node.Attributes["LoadFromFile"].InnerText.ToLower() == "true")
			{
				string innerText = node.Attributes["File"].InnerText;
				if (File.Exists(innerText))
				{
					string text = File.ReadAllText(innerText);
					value = text;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		private static bool TryGetFromEnvironment(XmlNode node, out string value)
		{
			value = "";
			XmlAttributeCollection attributes = node.Attributes;
			if (((attributes != null) ? attributes["GetFromEnvironment"] : null) != null && node.Attributes["GetFromEnvironment"].InnerText.ToLower() == "true")
			{
				string environmentVariable = Environment.GetEnvironmentVariable(node.Attributes["Variable"].InnerText);
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					value = environmentVariable;
					return true;
				}
			}
			return false;
		}
	}
}
