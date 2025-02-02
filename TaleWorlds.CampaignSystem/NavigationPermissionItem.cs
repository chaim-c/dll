﻿using System;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200008D RID: 141
	public struct NavigationPermissionItem
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0004C230 File Offset: 0x0004A430
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x0004C238 File Offset: 0x0004A438
		public bool IsAuthorized { get; private set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0004C241 File Offset: 0x0004A441
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x0004C249 File Offset: 0x0004A449
		public TextObject ReasonString { get; private set; }

		// Token: 0x060010F0 RID: 4336 RVA: 0x0004C252 File Offset: 0x0004A452
		public NavigationPermissionItem(bool isAuthorized, TextObject reasonString)
		{
			this.IsAuthorized = isAuthorized;
			this.ReasonString = reasonString;
		}
	}
}
