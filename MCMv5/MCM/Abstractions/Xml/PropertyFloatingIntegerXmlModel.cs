using System;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000086 RID: 134
	[XmlType("PropertyFloatingInteger")]
	[Serializable]
	public class PropertyFloatingIntegerXmlModel : PropertyBaseXmlModel, IPropertyDefinitionWithMinMax
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009F1C File Offset: 0x0000811C
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00009F24 File Offset: 0x00008124
		[XmlAttribute("MinValue")]
		public decimal MinValue { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009F2D File Offset: 0x0000812D
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00009F35 File Offset: 0x00008135
		[XmlAttribute("MaxValue")]
		public decimal MaxValue { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009F3E File Offset: 0x0000813E
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00009F46 File Offset: 0x00008146
		[XmlElement("Value")]
		public decimal Value { get; set; }
	}
}
