using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006896 File Offset: 0x00004A96
		public bool ParameterValue { get; }

		// Token: 0x060000FF RID: 255 RVA: 0x0000689E File Offset: 0x00004A9E
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}
	}
}
