using System;

namespace MCM.LightInject
{
	// Token: 0x020000D8 RID: 216
	internal interface IGenericArgumentMapper
	{
		// Token: 0x06000490 RID: 1168
		GenericMappingResult Map(Type genericServiceType, Type openGenericImplementingType);

		// Token: 0x06000491 RID: 1169
		Type TryMakeGenericType(Type genericServiceType, Type openGenericImplementingType);
	}
}
