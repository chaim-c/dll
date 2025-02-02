using System;
using System.Collections.Generic;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x0200037F RID: 895
	public class OptionCategory
	{
		// Token: 0x0600313B RID: 12603 RVA: 0x000CBC95 File Offset: 0x000C9E95
		public OptionCategory(IEnumerable<IOptionData> baseOptions, IEnumerable<OptionGroup> groups)
		{
			this.BaseOptions = baseOptions;
			this.Groups = groups;
		}

		// Token: 0x0400151D RID: 5405
		public readonly IEnumerable<IOptionData> BaseOptions;

		// Token: 0x0400151E RID: 5406
		public readonly IEnumerable<OptionGroup> Groups;
	}
}
