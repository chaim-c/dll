using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000093 RID: 147
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class PropertyDefinitionWithCustomFormatterWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionWithCustomFormatter
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A364 File Offset: 0x00008564
		public Type CustomFormatter { get; }

		// Token: 0x06000326 RID: 806 RVA: 0x0000A36C File Offset: 0x0000856C
		[NullableContext(1)]
		public PropertyDefinitionWithCustomFormatterWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "CustomFormatter", false);
			this.CustomFormatter = (((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as Type);
		}
	}
}
