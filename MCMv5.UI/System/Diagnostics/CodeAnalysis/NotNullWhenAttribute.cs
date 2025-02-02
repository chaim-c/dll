using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000076 RID: 118
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000139AD File Offset: 0x00011BAD
		public bool ReturnValue { get; }

		// Token: 0x060004A0 RID: 1184 RVA: 0x000139B5 File Offset: 0x00011BB5
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}
	}
}
