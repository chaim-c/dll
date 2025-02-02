using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021F1 File Offset: 0x000003F1
		public NullableAttribute(byte A_1)
		{
			this.NullableFlags = new byte[]
			{
				A_1
			};
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220A File Offset: 0x0000040A
		public NullableAttribute(byte[] A_1)
		{
			this.NullableFlags = A_1;
		}

		// Token: 0x04000004 RID: 4
		public readonly byte[] NullableFlags;
	}
}
