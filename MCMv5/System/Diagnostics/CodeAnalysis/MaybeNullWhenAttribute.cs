using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200015D RID: 349
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00021ACD File Offset: 0x0001FCCD
		public bool ReturnValue { get; }

		// Token: 0x060009C8 RID: 2504 RVA: 0x00021AD5 File Offset: 0x0001FCD5
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}
	}
}
