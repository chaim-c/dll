using System;
using System.Collections.Generic;
using TaleWorlds.Engine.Options;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x02000380 RID: 896
	public class OptionGroup
	{
		// Token: 0x0600313C RID: 12604 RVA: 0x000CBCAB File Offset: 0x000C9EAB
		public OptionGroup(TextObject groupName, IEnumerable<IOptionData> options)
		{
			this.GroupName = groupName;
			this.Options = options;
		}

		// Token: 0x0400151F RID: 5407
		public readonly TextObject GroupName;

		// Token: 0x04001520 RID: 5408
		public readonly IEnumerable<IOptionData> Options;
	}
}
