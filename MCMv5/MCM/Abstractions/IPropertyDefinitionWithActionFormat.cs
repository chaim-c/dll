using System;
using System.Runtime.CompilerServices;

namespace MCM.Abstractions
{
	// Token: 0x0200004B RID: 75
	public interface IPropertyDefinitionWithActionFormat
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A8 RID: 424
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		Func<object, string> ValueFormatFunc { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; }
	}
}
