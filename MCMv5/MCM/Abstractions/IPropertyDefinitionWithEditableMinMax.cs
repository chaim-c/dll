using System;

namespace MCM.Abstractions
{
	// Token: 0x0200004D RID: 77
	public interface IPropertyDefinitionWithEditableMinMax
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001AA RID: 426
		decimal EditableMinValue { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AB RID: 427
		decimal EditableMaxValue { get; }
	}
}
