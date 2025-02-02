using System;
using System.Collections.Generic;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x0200003B RID: 59
	public class StatCategory
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0001304A File Offset: 0x0001124A
		public StatCategory(string id, IEnumerable<StatItem> items)
		{
			this.ID = id;
			this.Items = items;
		}

		// Token: 0x04000234 RID: 564
		public readonly IEnumerable<StatItem> Items;

		// Token: 0x04000235 RID: 565
		public readonly string ID;
	}
}
