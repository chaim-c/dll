using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000095 RID: 149
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PropertyDefinitionWithFormatWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionWithFormat
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000A440 File Offset: 0x00008640
		public string ValueFormat { get; }

		// Token: 0x0600032B RID: 811 RVA: 0x0000A448 File Offset: 0x00008648
		public PropertyDefinitionWithFormatWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "ValueFormat", true);
			this.ValueFormat = ((((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as string) ?? string.Empty);
		}
	}
}
