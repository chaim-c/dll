using System;
using System.IO;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000095 RID: 149
	public sealed class ManagedParameters : IManagedParametersInitializer
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0004C372 File Offset: 0x0004A572
		public static ManagedParameters Instance { get; } = new ManagedParameters();

		// Token: 0x06001135 RID: 4405 RVA: 0x0004C37C File Offset: 0x0004A57C
		public void Initialize(string relativeXmlPath)
		{
			XmlDocument doc = ManagedParameters.LoadXmlFile(relativeXmlPath);
			this.LoadFromXml(doc);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004C398 File Offset: 0x0004A598
		private void LoadFromXml(XmlNode doc)
		{
			XmlNode xmlNode = null;
			if (doc.ChildNodes[1].ChildNodes[0].Name == "managed_campaign_parameters")
			{
				xmlNode = doc.ChildNodes[1].ChildNodes[0].ChildNodes[0];
			}
			while (xmlNode != null)
			{
				ManagedParametersEnum managedParametersEnum;
				if (xmlNode.Name == "managed_campaign_parameter" && xmlNode.NodeType != XmlNodeType.Comment && Enum.TryParse<ManagedParametersEnum>(xmlNode.Attributes["id"].Value, true, out managedParametersEnum))
				{
					this._managedParametersArray[(int)managedParametersEnum] = bool.Parse(xmlNode.Attributes["value"].Value);
				}
				xmlNode = xmlNode.NextSibling;
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004C460 File Offset: 0x0004A660
		private static XmlDocument LoadXmlFile(string path)
		{
			Debug.Print("opening " + path, 0, Debug.DebugColor.White, 17592186044416UL);
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(path);
			string xml = streamReader.ReadToEnd();
			xmlDocument.LoadXml(xml);
			streamReader.Close();
			return xmlDocument;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004C4A9 File Offset: 0x0004A6A9
		public bool GetManagedParameter(ManagedParametersEnum _managedParametersEnum)
		{
			return this._managedParametersArray[(int)_managedParametersEnum];
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004C4B4 File Offset: 0x0004A6B4
		public bool SetManagedParameter(ManagedParametersEnum _managedParametersEnum, bool value)
		{
			this._managedParametersArray[(int)_managedParametersEnum] = value;
			return value;
		}

		// Token: 0x040005DD RID: 1501
		private readonly bool[] _managedParametersArray = new bool[2];
	}
}
