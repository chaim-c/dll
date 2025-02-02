using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006958 File Offset: 0x00004B58
		public string ParameterName { get; }

		// Token: 0x0600010C RID: 268 RVA: 0x00006960 File Offset: 0x00004B60
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}
	}
}
