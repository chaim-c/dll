using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200006F RID: 111
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000138D2 File Offset: 0x00011AD2
		public bool ParameterValue { get; }

		// Token: 0x06000491 RID: 1169 RVA: 0x000138DA File Offset: 0x00011ADA
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}
	}
}
