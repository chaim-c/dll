using System;

namespace MCM.Abstractions
{
	// Token: 0x02000050 RID: 80
	public interface IPropertyDefinitionWithMinMax
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001AE RID: 430
		decimal MinValue { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001AF RID: 431
		decimal MaxValue { get; }
	}
}
