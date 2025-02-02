using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000090 RID: 144
	public sealed class PropertyDefinitionGroupToggleWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionGroupToggle, IPropertyDefinitionBase
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000A292 File Offset: 0x00008492
		public bool IsToggle { get; }

		// Token: 0x06000321 RID: 801 RVA: 0x0000A29C File Offset: 0x0000849C
		[NullableContext(1)]
		public PropertyDefinitionGroupToggleWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "IsToggle", true);
			this.IsToggle = (((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as bool?).GetValueOrDefault();
		}
	}
}
