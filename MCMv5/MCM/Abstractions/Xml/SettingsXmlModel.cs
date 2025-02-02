using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace MCM.Abstractions.Xml
{
	// Token: 0x0200008A RID: 138
	[NullableContext(1)]
	[Nullable(0)]
	[XmlRoot("Settings")]
	[Serializable]
	public class SettingsXmlModel
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000A003 File Offset: 0x00008203
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000A00B File Offset: 0x0000820B
		[XmlAttribute("Id")]
		public string Id { get; set; } = null;

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000A014 File Offset: 0x00008214
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000A01C File Offset: 0x0000821C
		[XmlAttribute("DisplayName")]
		public string DisplayName { get; set; } = null;

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000A025 File Offset: 0x00008225
		[XmlAttribute("FolderName")]
		public string FolderName { get; } = string.Empty;

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000A02D File Offset: 0x0000822D
		[XmlAttribute("SubFolder")]
		public string SubFolder
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000A034 File Offset: 0x00008234
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000A03C File Offset: 0x0000823C
		[XmlAttribute("UIVersion")]
		public int UIVersion { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000A045 File Offset: 0x00008245
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000A04D File Offset: 0x0000824D
		[XmlAttribute("SubGroupDelimiter")]
		public string SubGroupDelimiter { get; set; } = null;

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000A056 File Offset: 0x00008256
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000A05E File Offset: 0x0000825E
		[XmlAttribute("FormatType")]
		public string FormatType { get; set; } = null;

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000A067 File Offset: 0x00008267
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000A06F File Offset: 0x0000826F
		[XmlArray("Properties")]
		[XmlArrayItem("PropertyBool", typeof(PropertyBoolXmlModel))]
		[XmlArrayItem("PropertyDropdown", typeof(PropertyDropdownXmlModel))]
		[XmlArrayItem("PropertyFloatingInteger", typeof(PropertyFloatingIntegerXmlModel))]
		[XmlArrayItem("PropertyInteger", typeof(PropertyIntegerXmlModel))]
		[XmlArrayItem("PropertyText", typeof(PropertyTextXmlModel))]
		public List<PropertyBaseXmlModel> Properties { get; set; } = null;

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000A078 File Offset: 0x00008278
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000A080 File Offset: 0x00008280
		[XmlArray("PropertyGroups")]
		[XmlArrayItem("PropertyGroup", typeof(PropertyGroupXmlModel))]
		public List<PropertyGroupXmlModel> Groups { get; set; } = null;
	}
}
