using System;

namespace Mono.Cecil
{
	// Token: 0x020000B7 RID: 183
	public interface IMetadataResolver
	{
		// Token: 0x06000692 RID: 1682
		TypeDefinition Resolve(TypeReference type);

		// Token: 0x06000693 RID: 1683
		FieldDefinition Resolve(FieldReference field);

		// Token: 0x06000694 RID: 1684
		MethodDefinition Resolve(MethodReference method);
	}
}
