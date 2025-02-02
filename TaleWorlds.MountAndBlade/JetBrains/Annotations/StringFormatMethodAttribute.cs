using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000DC RID: 220
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0000F137 File Offset: 0x0000D337
		public StringFormatMethodAttribute(string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0000F146 File Offset: 0x0000D346
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x0000F14E File Offset: 0x0000D34E
		[UsedImplicitly]
		public string FormatParameterName { get; private set; }
	}
}
