using System;
using System.Runtime.CompilerServices;
using System.Xml;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace MCM.UI.UIExtenderEx
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	[PrefabExtension("Options", "descendant::TabControl[@Id='TabControl']/Children/*[5]", "Options")]
	internal sealed class OptionsPrefabExtension2 : PrefabExtensionInsertPatch
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003B86 File Offset: 0x00001D86
		public override InsertType Type
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B89 File Offset: 0x00001D89
		public OptionsPrefabExtension2()
		{
			this._xmlDocument.LoadXml("<ModOptionsPageView Id=\"ModOptionsPage\" DataSource=\"{ModOptions}\" />");
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003BAF File Offset: 0x00001DAF
		[PrefabExtensionInsertPatch.PrefabExtensionXmlDocumentAttribute(false)]
		public XmlDocument GetPrefabExtension()
		{
			return this._xmlDocument;
		}

		// Token: 0x04000025 RID: 37
		private readonly XmlDocument _xmlDocument = new XmlDocument();
	}
}
