using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000091 RID: 145
	public sealed class PropertyDefinitionTextWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionText, IPropertyDefinitionBase
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000A2EA File Offset: 0x000084EA
		[NullableContext(1)]
		public PropertyDefinitionTextWrapper(object @object) : base(@object)
		{
		}
	}
}
