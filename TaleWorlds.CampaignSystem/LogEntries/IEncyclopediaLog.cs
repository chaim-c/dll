﻿using System;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002F4 RID: 756
	public interface IEncyclopediaLog
	{
		// Token: 0x06002BE8 RID: 11240
		bool IsVisibleInEncyclopediaPageOf<T>(T obj) where T : MBObjectBase;

		// Token: 0x06002BE9 RID: 11241
		TextObject GetEncyclopediaText();

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002BEA RID: 11242
		CampaignTime GameTime { get; }
	}
}
