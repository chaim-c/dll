using System;

namespace Mono.Cecil
{
	// Token: 0x02000052 RID: 82
	internal interface IGenericContext
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000294 RID: 660
		bool IsDefinition { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000295 RID: 661
		IGenericParameterProvider Type { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000296 RID: 662
		IGenericParameterProvider Method { get; }
	}
}
