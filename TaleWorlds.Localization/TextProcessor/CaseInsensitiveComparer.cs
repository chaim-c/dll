using System;
using System.Collections.Generic;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x0200002E RID: 46
	internal class CaseInsensitiveComparer : IEqualityComparer<string>
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00006D8E File Offset: 0x00004F8E
		public bool Equals(string x, string y)
		{
			return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006D98 File Offset: 0x00004F98
		public int GetHashCode(string x)
		{
			return x.ToLowerInvariant().GetHashCode();
		}
	}
}
