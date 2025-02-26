﻿using System;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x0200035B RID: 859
	public abstract class SettlementArea
	{
		// Token: 0x060031BF RID: 12735 RVA: 0x000D0CD8 File Offset: 0x000CEED8
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060031C0 RID: 12736
		public abstract Settlement Settlement { get; }

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060031C1 RID: 12737
		public abstract TextObject Name { get; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060031C2 RID: 12738
		public abstract string Tag { get; }

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060031C3 RID: 12739
		public abstract Hero Owner { get; }
	}
}
