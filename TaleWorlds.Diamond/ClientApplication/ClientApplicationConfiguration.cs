using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x0200005C RID: 92
	public class ClientApplicationConfiguration
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00005DD1 File Offset: 0x00003FD1
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00005DD9 File Offset: 0x00003FD9
		public string Name { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00005DE2 File Offset: 0x00003FE2
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00005DEA File Offset: 0x00003FEA
		public string InheritFrom { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00005DF3 File Offset: 0x00003FF3
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00005DFB File Offset: 0x00003FFB
		public string[] Clients { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005E04 File Offset: 0x00004004
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00005E0C File Offset: 0x0000400C
		public string[] SessionlessClients { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00005E15 File Offset: 0x00004015
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00005E1D File Offset: 0x0000401D
		public SessionProviderType SessionProviderType { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00005E26 File Offset: 0x00004026
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00005E2E File Offset: 0x0000402E
		public ParameterContainer Parameters { get; set; }

		// Token: 0x06000227 RID: 551 RVA: 0x00005E38 File Offset: 0x00004038
		public ClientApplicationConfiguration()
		{
			this.Name = "NewlyCreated";
			this.InheritFrom = "";
			this.Clients = new string[0];
			this.SessionlessClients = new string[0];
			this.Parameters = new ParameterContainer();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00005E84 File Offset: 0x00004084
		private void FillFromBase(ClientApplicationConfiguration baseConfiguration)
		{
			this.SessionProviderType = baseConfiguration.SessionProviderType;
			this.Parameters = baseConfiguration.Parameters.Clone();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static string GetDefaultConfigurationFromFile()
		{
			XmlDocument xmlDocument = new XmlDocument();
			string fileContent = VirtualFolders.GetFileContent(BasePath.Name + "Parameters/ClientProfile.xml");
			if (fileContent == "")
			{
				return "";
			}
			xmlDocument.LoadXml(fileContent);
			return xmlDocument.ChildNodes[0].Attributes["Value"].InnerText;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00005F06 File Offset: 0x00004106
		public static void SetDefualtConfigurationCategory(string category)
		{
			ClientApplicationConfiguration._defaultConfigurationCategory = category;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00005F0E File Offset: 0x0000410E
		public void FillFrom(string configurationName)
		{
			if (string.IsNullOrEmpty(ClientApplicationConfiguration._defaultConfigurationCategory))
			{
				ClientApplicationConfiguration._defaultConfigurationCategory = ClientApplicationConfiguration.GetDefaultConfigurationFromFile();
			}
			this.FillFrom(ClientApplicationConfiguration._defaultConfigurationCategory, configurationName);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00005F34 File Offset: 0x00004134
		public void FillFrom(string configurationCategory, string configurationName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (configurationCategory == "")
			{
				return;
			}
			string fileContent = VirtualFolders.GetFileContent(string.Concat(new string[]
			{
				BasePath.Name,
				"Parameters/ClientProfiles/",
				configurationCategory,
				"/",
				configurationName,
				".xml"
			}));
			if (fileContent == "")
			{
				return;
			}
			xmlDocument.LoadXml(fileContent);
			this.Name = Path.GetFileNameWithoutExtension(configurationName);
			XmlNode firstChild = xmlDocument.FirstChild;
			if (firstChild.Attributes != null && firstChild.Attributes["InheritFrom"] != null)
			{
				this.InheritFrom = firstChild.Attributes["InheritFrom"].InnerText;
				ClientApplicationConfiguration clientApplicationConfiguration = new ClientApplicationConfiguration();
				clientApplicationConfiguration.FillFrom(configurationCategory, this.InheritFrom);
				this.FillFromBase(clientApplicationConfiguration);
			}
			ParameterLoader.LoadParametersInto(string.Concat(new string[]
			{
				"ClientProfiles/",
				configurationCategory,
				"/",
				configurationName,
				".xml"
			}), this.Parameters);
			foreach (object obj in firstChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "SessionProvider")
				{
					string innerText = xmlNode.Attributes["Type"].InnerText;
					this.SessionProviderType = (SessionProviderType)Enum.Parse(typeof(SessionProviderType), innerText);
				}
				else if (xmlNode.Name == "Clients")
				{
					List<string> list = new List<string>();
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						string innerText2 = ((XmlNode)obj2).Attributes["Type"].InnerText;
						list.Add(innerText2);
					}
					this.Clients = list.ToArray();
				}
				else if (xmlNode.Name == "SessionlessClients")
				{
					List<string> list2 = new List<string>();
					foreach (object obj3 in xmlNode.ChildNodes)
					{
						string innerText3 = ((XmlNode)obj3).Attributes["Type"].InnerText;
						list2.Add(innerText3);
					}
					this.SessionlessClients = list2.ToArray();
				}
				else
				{
					xmlNode.Name == "Parameters";
				}
			}
		}

		// Token: 0x040000C7 RID: 199
		private static string _defaultConfigurationCategory = "";
	}
}
