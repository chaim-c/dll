using System;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000088 RID: 136
	[XmlType("PropertyInteger")]
	[Serializable]
	public class PropertyIntegerXmlModel : PropertyBaseXmlModel, IPropertyDefinitionWithMinMax
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00009FA2 File Offset: 0x000081A2
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00009FAA File Offset: 0x000081AA
		[XmlAttribute("MinValue")]
		public decimal MinValue { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00009FB3 File Offset: 0x000081B3
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00009FBB File Offset: 0x000081BB
		[XmlAttribute("MaxValue")]
		public decimal MaxValue { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00009FC4 File Offset: 0x000081C4
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00009FCC File Offset: 0x000081CC
		[XmlElement("Value")]
		public decimal Value { get; set; }
	}
}
