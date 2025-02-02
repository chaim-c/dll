using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000089 RID: 137
	[NullableContext(1)]
	[Nullable(0)]
	[XmlType("PropertyText")]
	[Serializable]
	public class PropertyTextXmlModel : PropertyBaseXmlModel, IPropertyDefinitionText, IPropertyDefinitionBase
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009FDE File Offset: 0x000081DE
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00009FE6 File Offset: 0x000081E6
		[XmlElement("Value")]
		public string Value { get; set; } = string.Empty;
	}
}
