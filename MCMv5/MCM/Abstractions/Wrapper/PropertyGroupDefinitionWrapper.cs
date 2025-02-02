using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000098 RID: 152
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PropertyGroupDefinitionWrapper : IPropertyGroupDefinition
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000A577 File Offset: 0x00008777
		public string GroupName { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000A57F File Offset: 0x0000877F
		public int GroupOrder { get; }

		// Token: 0x06000333 RID: 819 RVA: 0x0000A588 File Offset: 0x00008788
		public PropertyGroupDefinitionWrapper(object @object)
		{
			Type type = @object.GetType();
			PropertyInfo property = type.GetProperty("GroupName");
			this.GroupName = ((((property != null) ? property.GetValue(@object) : null) as string) ?? "ERROR");
			PropertyInfo property2 = type.GetProperty("GroupOrder");
			this.GroupOrder = (((property2 != null) ? property2.GetValue(@object) : null) as int?).GetValueOrDefault(-1);
		}
	}
}
