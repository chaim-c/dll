using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200008D RID: 141
	public sealed class PropertyDefinitionBoolWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionBool, IPropertyDefinitionBase
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000A1DA File Offset: 0x000083DA
		[NullableContext(1)]
		public PropertyDefinitionBoolWrapper(object @object) : base(@object)
		{
		}
	}
}
