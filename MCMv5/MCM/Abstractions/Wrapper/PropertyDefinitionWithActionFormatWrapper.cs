using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000092 RID: 146
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PropertyDefinitionWithActionFormatWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionWithActionFormat
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A2F5 File Offset: 0x000084F5
		public Func<object, string> ValueFormatFunc { get; }

		// Token: 0x06000324 RID: 804 RVA: 0x0000A300 File Offset: 0x00008500
		public PropertyDefinitionWithActionFormatWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "ValueFormatFunc", true);
			Func<object, string> func;
			if ((func = (((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as Func<object, string>)) == null && (func = PropertyDefinitionWithActionFormatWrapper.<>c.<>9__3_0) == null)
			{
				func = (PropertyDefinitionWithActionFormatWrapper.<>c.<>9__3_0 = ((object obj) => obj.ToString()));
			}
			this.ValueFormatFunc = func;
		}
	}
}
