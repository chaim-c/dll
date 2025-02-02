using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000161 RID: 353
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00021B6C File Offset: 0x0001FD6C
		public string ParameterName { get; }

		// Token: 0x060009D2 RID: 2514 RVA: 0x00021B74 File Offset: 0x0001FD74
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}
	}
}
