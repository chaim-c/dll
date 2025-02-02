using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006971 File Offset: 0x00004B71
		public bool ReturnValue { get; }

		// Token: 0x0600010E RID: 270 RVA: 0x00006979 File Offset: 0x00004B79
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}
	}
}
