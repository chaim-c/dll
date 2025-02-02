using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200015B RID: 347
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00021AAA File Offset: 0x0001FCAA
		public bool ParameterValue { get; }

		// Token: 0x060009C5 RID: 2501 RVA: 0x00021AB2 File Offset: 0x0001FCB2
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}
	}
}
