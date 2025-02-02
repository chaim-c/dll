using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x02000099 RID: 153
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SettingsDefinitionWrapper : SettingsDefinition
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000A604 File Offset: 0x00008804
		[return: Nullable(2)]
		private static string GetSettingsId(object @object)
		{
			PropertyInfo propInfo = @object.GetType().GetProperty("SettingsId");
			return ((propInfo != null) ? propInfo.GetValue(@object) : null) as string;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000A639 File Offset: 0x00008839
		public SettingsDefinitionWrapper(object @object) : base(SettingsDefinitionWrapper.GetSettingsId(@object) ?? "ERROR")
		{
		}
	}
}
