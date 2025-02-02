using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200008E RID: 142
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PropertyDefinitionButtonWrapper : BasePropertyDefinitionWrapper, IPropertyDefinitionButton, IPropertyDefinitionBase
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000A1E5 File Offset: 0x000083E5
		public string Content { get; }

		// Token: 0x0600031D RID: 797 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public PropertyDefinitionButtonWrapper(object @object) : base(@object)
		{
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "Content", true);
			this.Content = ((((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as string) ?? string.Empty);
		}
	}
}
