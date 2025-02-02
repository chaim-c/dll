using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200008B RID: 139
	public sealed class AttributePropertyDefinitionGroupToggleWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionGroupToggle, IPropertyDefinitionBase
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000A0C7 File Offset: 0x000082C7
		public bool IsToggle { get; } = 1;

		// Token: 0x06000315 RID: 789 RVA: 0x0000A0CF File Offset: 0x000082CF
		[NullableContext(1)]
		public AttributePropertyDefinitionGroupToggleWrapper(object @object) : base(@object)
		{
		}
	}
}
