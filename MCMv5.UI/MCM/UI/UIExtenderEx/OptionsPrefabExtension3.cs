using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace MCM.UI.UIExtenderEx
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	[PrefabExtension("Options", "descendant::Widget[@Id='DescriptionsRightPanel']", "Options")]
	internal sealed class OptionsPrefabExtension3 : PrefabExtensionSetAttributePatch
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003BB7 File Offset: 0x00001DB7
		public override List<PrefabExtensionSetAttributePatch.Attribute> Attributes
		{
			get
			{
				return new List<PrefabExtensionSetAttributePatch.Attribute>
				{
					new PrefabExtensionSetAttributePatch.Attribute("SuggestedWidth", "@DescriptionWidth")
				};
			}
		}
	}
}
