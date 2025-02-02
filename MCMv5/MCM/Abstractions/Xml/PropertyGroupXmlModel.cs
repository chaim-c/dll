using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000087 RID: 135
	[NullableContext(1)]
	[Nullable(0)]
	[XmlType("PropertyGroups")]
	[Serializable]
	public class PropertyGroupXmlModel : IPropertyGroupDefinition
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00009F58 File Offset: 0x00008158
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00009F60 File Offset: 0x00008160
		[XmlAttribute("DisplayName")]
		public string GroupName { get; set; } = null;

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00009F69 File Offset: 0x00008169
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00009F71 File Offset: 0x00008171
		[XmlAttribute("Order")]
		public int GroupOrder { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00009F7A File Offset: 0x0000817A
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00009F82 File Offset: 0x00008182
		[XmlArray("Properties")]
		[XmlArrayItem("PropertyBool", typeof(PropertyBoolXmlModel))]
		[XmlArrayItem("PropertyDropdown", typeof(PropertyDropdownXmlModel))]
		[XmlArrayItem("PropertyFloatingInteger", typeof(PropertyFloatingIntegerXmlModel))]
		[XmlArrayItem("PropertyInteger", typeof(PropertyIntegerXmlModel))]
		[XmlArrayItem("PropertyText", typeof(PropertyTextXmlModel))]
		public List<PropertyBaseXmlModel> Properties { get; set; } = null;
	}
}
