using System;
using System.Collections.Generic;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x0200009F RID: 159
	public interface ISelectionOptionData : IOptionData
	{
		// Token: 0x06000BE8 RID: 3048
		int GetSelectableOptionsLimit();

		// Token: 0x06000BE9 RID: 3049
		IEnumerable<SelectionData> GetSelectableOptionNames();
	}
}
