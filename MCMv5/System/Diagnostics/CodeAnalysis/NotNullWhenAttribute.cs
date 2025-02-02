using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000162 RID: 354
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00021B85 File Offset: 0x0001FD85
		public bool ReturnValue { get; }

		// Token: 0x060009D4 RID: 2516 RVA: 0x00021B8D File Offset: 0x0001FD8D
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}
	}
}
