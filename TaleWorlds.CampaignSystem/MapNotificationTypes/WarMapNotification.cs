﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.MapNotificationTypes
{
	// Token: 0x0200025D RID: 605
	public class WarMapNotification : InformationData
	{
		// Token: 0x06001FC8 RID: 8136 RVA: 0x0008A42F File Offset: 0x0008862F
		internal static void AutoGeneratedStaticCollectObjectsWarMapNotification(object o, List<object> collectedObjects)
		{
			((WarMapNotification)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0008A43D File Offset: 0x0008863D
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.FirstFaction);
			collectedObjects.Add(this.SecondFaction);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0008A45E File Offset: 0x0008865E
		internal static object AutoGeneratedGetMemberValueFirstFaction(object o)
		{
			return ((WarMapNotification)o).FirstFaction;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0008A46B File Offset: 0x0008866B
		internal static object AutoGeneratedGetMemberValueSecondFaction(object o)
		{
			return ((WarMapNotification)o).SecondFaction;
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0008A478 File Offset: 0x00088678
		public override TextObject TitleText
		{
			get
			{
				return new TextObject("{=qR6HqHgo}Declaration of War", null);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0008A485 File Offset: 0x00088685
		public override string SoundEventPath
		{
			get
			{
				return "event:/ui/notification/war_declared";
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0008A48C File Offset: 0x0008868C
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x0008A494 File Offset: 0x00088694
		[SaveableProperty(1)]
		public IFaction FirstFaction { get; private set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0008A49D File Offset: 0x0008869D
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x0008A4A5 File Offset: 0x000886A5
		[SaveableProperty(2)]
		public IFaction SecondFaction { get; private set; }

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0008A4AE File Offset: 0x000886AE
		public WarMapNotification(IFaction firstFaction, IFaction secondFaction, TextObject descriptionText) : base(descriptionText)
		{
			this.FirstFaction = firstFaction;
			this.SecondFaction = secondFaction;
		}
	}
}
