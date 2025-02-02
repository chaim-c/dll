using System;
using System.IO;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x02000093 RID: 147
	public sealed class ManagedParameters : IManagedParametersInitializer
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001B3B1 File Offset: 0x000195B1
		public static ManagedParameters Instance { get; } = new ManagedParameters();

		// Token: 0x060007FB RID: 2043 RVA: 0x0001B3B8 File Offset: 0x000195B8
		public static float GetParameter(ManagedParametersEnum managedParameterType)
		{
			return ManagedParameters.Instance._managedParametersArray[(int)managedParameterType];
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001B3C6 File Offset: 0x000195C6
		public static void SetParameter(ManagedParametersEnum managedParameterType, float newValue)
		{
			ManagedParameters.Instance._managedParametersArray[(int)managedParameterType] = newValue;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001B3D8 File Offset: 0x000195D8
		public void Initialize(string relativeXmlPath)
		{
			XmlDocument doc = ManagedParameters.LoadXmlFile(relativeXmlPath);
			this.LoadFromXml(doc);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001B3F3 File Offset: 0x000195F3
		private ManagedParameters()
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001B408 File Offset: 0x00019608
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

		// Token: 0x06000800 RID: 2048 RVA: 0x0001B454 File Offset: 0x00019654
		private void LoadFromXml(XmlNode doc)
		{
			Debug.Print("loading managed_core_parameters.xml", 0, Debug.DebugColor.White, 17592186044416UL);
			if (doc.ChildNodes.Count <= 1)
			{
				throw new TWXmlLoadException("Incorrect XML document format.");
			}
			if (doc.ChildNodes[1].Name != "base")
			{
				throw new TWXmlLoadException("Incorrect XML document format.");
			}
			if (doc.ChildNodes[1].ChildNodes[0].Name != "managed_core_parameters")
			{
				throw new TWXmlLoadException("Incorrect XML document format.");
			}
			XmlNode xmlNode = null;
			if (doc.ChildNodes[1].ChildNodes[0].Name == "managed_core_parameters")
			{
				xmlNode = doc.ChildNodes[1].ChildNodes[0].ChildNodes[0];
			}
			while (xmlNode != null)
			{
				ManagedParametersEnum managedParametersEnum;
				if (xmlNode.Name == "managed_core_parameter" && xmlNode.NodeType != XmlNodeType.Comment && Enum.TryParse<ManagedParametersEnum>(xmlNode.Attributes["id"].Value, true, out managedParametersEnum))
				{
					this._managedParametersArray[(int)managedParametersEnum] = float.Parse(xmlNode.Attributes["value"].Value);
				}
				xmlNode = xmlNode.NextSibling;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001B5A3 File Offset: 0x000197A3
		public float GetManagedParameter(ManagedParametersEnum managedParameterEnum)
		{
			return this._managedParametersArray[(int)managedParameterEnum];
		}

		// Token: 0x04000492 RID: 1170
		private readonly float[] _managedParametersArray = new float[68];
	}
}
