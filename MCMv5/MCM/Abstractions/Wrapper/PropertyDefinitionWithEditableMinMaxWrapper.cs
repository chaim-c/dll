using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000094 RID: 148
	public sealed class PropertyDefinitionWithEditableMinMaxWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionWithEditableMinMax
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000A3AD File Offset: 0x000085AD
		public decimal EditableMinValue { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A3B5 File Offset: 0x000085B5
		public decimal EditableMaxValue { get; }

		// Token: 0x06000329 RID: 809 RVA: 0x0000A3C0 File Offset: 0x000085C0
		[NullableContext(1)]
		public PropertyDefinitionWithEditableMinMaxWrapper(object @object) : base(@object)
		{
			PropertyInfo property = @object.GetType().GetProperty("EditableMinValue");
			this.EditableMinValue = (((property != null) ? property.GetValue(@object) : null) as decimal?).GetValueOrDefault();
			PropertyInfo property2 = @object.GetType().GetProperty("EditableMaxValue");
			this.EditableMaxValue = (((property2 != null) ? property2.GetValue(@object) : null) as decimal?).GetValueOrDefault();
		}
	}
}
