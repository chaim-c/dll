using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BasePropertyDefinitionWrapper : IPropertyDefinitionBase
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000A0E1 File Offset: 0x000082E1
		public string DisplayName { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000A0E9 File Offset: 0x000082E9
		public int Order { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000A0F1 File Offset: 0x000082F1
		public bool RequireRestart { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000A0F9 File Offset: 0x000082F9
		public string HintText { get; }

		// Token: 0x0600031A RID: 794 RVA: 0x0000A104 File Offset: 0x00008304
		protected BasePropertyDefinitionWrapper(object @object)
		{
			Type type = @object.GetType();
			PropertyInfo property = type.GetProperty("DisplayName");
			this.DisplayName = ((((property != null) ? property.GetValue(@object) : null) as string) ?? "ERROR");
			PropertyInfo property2 = type.GetProperty("Order");
			this.Order = (((property2 != null) ? property2.GetValue(@object) : null) as int?).GetValueOrDefault(-1);
			PropertyInfo property3 = type.GetProperty("RequireRestart");
			this.RequireRestart = (((property3 != null) ? property3.GetValue(@object) : null) as bool?).GetValueOrDefault(true);
			PropertyInfo property4 = type.GetProperty("HintText");
			this.HintText = ((((property4 != null) ? property4.GetValue(@object) : null) as string) ?? "ERROR");
		}
	}
}
