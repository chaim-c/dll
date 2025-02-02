using System;
using System.Runtime.CompilerServices;
using System.Xml;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace MCM.UI.UIExtenderEx
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	[PrefabExtension("Options", "descendant::ListPanel[@Id='TabToggleList']/Children/OptionsTabToggle[5]", "Options")]
	internal sealed class OptionsPrefabExtension1 : PrefabExtensionInsertPatch
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003B55 File Offset: 0x00001D55
		public override InsertType Type
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B58 File Offset: 0x00001D58
		public OptionsPrefabExtension1()
		{
			this._xmlDocument.LoadXml("<OptionsTabToggle DataSource=\"{ModOptions}\" PositionYOffset=\"2\" Parameter.ButtonBrush=\"Header.Tab.Center\" Parameter.TabName=\"ModOptionsPage\" />");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B7E File Offset: 0x00001D7E
		[PrefabExtensionInsertPatch.PrefabExtensionXmlNodeAttribute(false)]
		public XmlNode GetPrefabExtension()
		{
			return this._xmlDocument;
		}

		// Token: 0x04000024 RID: 36
		private readonly XmlDocument _xmlDocument = new XmlDocument();
	}
}
