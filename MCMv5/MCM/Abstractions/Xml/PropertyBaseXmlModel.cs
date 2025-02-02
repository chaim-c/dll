using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x02000083 RID: 131
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public abstract class PropertyBaseXmlModel : IPropertyDefinitionBase, IPropertyDefinitionWithId
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00009E3D File Offset: 0x0000803D
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00009E45 File Offset: 0x00008045
		[XmlAttribute("Id")]
		public string Id { get; set; } = null;

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009E4E File Offset: 0x0000804E
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00009E56 File Offset: 0x00008056
		[XmlAttribute("DisplayName")]
		public string DisplayName { get; set; } = null;

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009E5F File Offset: 0x0000805F
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00009E67 File Offset: 0x00008067
		[XmlAttribute("Order")]
		public int Order { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00009E70 File Offset: 0x00008070
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00009E78 File Offset: 0x00008078
		[XmlAttribute("HintText")]
		public string HintText { get; set; } = string.Empty;

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00009E81 File Offset: 0x00008081
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00009E89 File Offset: 0x00008089
		[XmlAttribute("RequireRestart")]
		public bool RequireRestart { get; set; } = true;
	}
}
