using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000075 RID: 117
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00013994 File Offset: 0x00011B94
		public string ParameterName { get; }

		// Token: 0x0600049E RID: 1182 RVA: 0x0001399C File Offset: 0x00011B9C
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}
	}
}
