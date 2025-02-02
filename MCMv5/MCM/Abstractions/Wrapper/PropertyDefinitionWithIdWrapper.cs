using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000096 RID: 150
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PropertyDefinitionWithIdWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionWithId
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000A492 File Offset: 0x00008692
		public string Id { get; }

		// Token: 0x0600032D RID: 813 RVA: 0x0000A49C File Offset: 0x0000869C
		public PropertyDefinitionWithIdWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "Id", true);
			this.Id = ((((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as string) ?? string.Empty);
		}
	}
}
