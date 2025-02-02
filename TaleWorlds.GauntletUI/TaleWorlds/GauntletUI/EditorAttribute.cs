using System;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class EditorAttribute : Attribute
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000B435 File Offset: 0x00009635
		public EditorAttribute(bool includeInnerProperties = false)
		{
			this.IncludeInnerProperties = includeInnerProperties;
		}

		// Token: 0x04000117 RID: 279
		public readonly bool IncludeInnerProperties;
	}
}
