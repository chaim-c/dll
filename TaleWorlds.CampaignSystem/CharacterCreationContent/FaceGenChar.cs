﻿using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x020001D2 RID: 466
	public class FaceGenChar
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0007EFCC File Offset: 0x0007D1CC
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0007EFD4 File Offset: 0x0007D1D4
		public BodyProperties BodyProperties { get; private set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0007EFDD File Offset: 0x0007D1DD
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0007EFE5 File Offset: 0x0007D1E5
		public int Race { get; private set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0007EFEE File Offset: 0x0007D1EE
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0007EFF6 File Offset: 0x0007D1F6
		public Equipment Equipment { get; private set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0007EFFF File Offset: 0x0007D1FF
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0007F007 File Offset: 0x0007D207
		public bool IsFemale { get; private set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0007F010 File Offset: 0x0007D210
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x0007F018 File Offset: 0x0007D218
		public string ActionName { get; set; }

		// Token: 0x06001C06 RID: 7174 RVA: 0x0007F021 File Offset: 0x0007D221
		public FaceGenChar(BodyProperties bodyProperties, int race, Equipment equipment, bool isFemale, string actionName = "act_inventory_idle_start")
		{
			this.BodyProperties = bodyProperties;
			this.Race = race;
			this.Equipment = equipment;
			this.IsFemale = isFemale;
			this.ActionName = actionName;
		}
	}
}
