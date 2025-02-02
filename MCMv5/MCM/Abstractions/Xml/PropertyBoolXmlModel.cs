using System;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000084 RID: 132
	[XmlType("PropertyBool")]
	[Serializable]
	public class PropertyBoolXmlModel : PropertyBaseXmlModel, IPropertyDefinitionBool, IPropertyDefinitionBase, IPropertyDefinitionGroupToggle
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00009EBB File Offset: 0x000080BB
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00009EC3 File Offset: 0x000080C3
		[XmlAttribute("IsToggle")]
		public bool IsToggle { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00009ECC File Offset: 0x000080CC
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00009ED4 File Offset: 0x000080D4
		[XmlElement("Value")]
		public bool Value { get; set; }
	}
}
