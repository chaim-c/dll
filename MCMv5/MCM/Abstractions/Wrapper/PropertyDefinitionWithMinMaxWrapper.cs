using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000097 RID: 151
	public sealed class PropertyDefinitionWithMinMaxWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionWithMinMax
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000A4E6 File Offset: 0x000086E6
		public decimal MinValue { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000A4EE File Offset: 0x000086EE
		public decimal MaxValue { get; }

		// Token: 0x06000330 RID: 816 RVA: 0x0000A4F8 File Offset: 0x000086F8
		[NullableContext(1)]
		public PropertyDefinitionWithMinMaxWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "MinValue", true);
			this.MinValue = (((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as decimal?).GetValueOrDefault();
			PropertyInfo propertyInfo2 = AccessTools2.Property(type, "MaxValue", true);
			this.MaxValue = (((propertyInfo2 != null) ? propertyInfo2.GetValue(@object) : null) as decimal?).GetValueOrDefault();
		}
	}
}
