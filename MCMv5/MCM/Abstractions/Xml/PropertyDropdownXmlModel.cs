using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000085 RID: 133
	[NullableContext(1)]
	[Nullable(0)]
	[XmlType("PropertyDropdown")]
	[Serializable]
	public class PropertyDropdownXmlModel : PropertyBaseXmlModel, IPropertyDefinitionDropdown, IPropertyDefinitionBase
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009EE6 File Offset: 0x000080E6
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00009EEE File Offset: 0x000080EE
		[XmlAttribute("SelectedIndex")]
		public int SelectedIndex { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009EF7 File Offset: 0x000080F7
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00009EFF File Offset: 0x000080FF
		[XmlArray("Values")]
		public string[] Values { get; set; } = Array.Empty<string>();
	}
}
