using System;

namespace Mono.Cecil
{
	// Token: 0x020000BA RID: 186
	public interface IModifierType
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006AB RID: 1707
		TypeReference ModifierType { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006AC RID: 1708
		TypeReference ElementType { get; }
	}
}
