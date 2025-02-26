﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.MapNotificationTypes
{
	// Token: 0x02000269 RID: 617
	public class KingdomDestroyedMapNotification : InformationData
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x0008AC46 File Offset: 0x00088E46
		internal static void AutoGeneratedStaticCollectObjectsKingdomDestroyedMapNotification(object o, List<object> collectedObjects)
		{
			((KingdomDestroyedMapNotification)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0008AC54 File Offset: 0x00088E54
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.DestroyedKingdom);
			CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.CreationTime, collectedObjects);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0008AC7A File Offset: 0x00088E7A
		internal static object AutoGeneratedGetMemberValueDestroyedKingdom(object o)
		{
			return ((KingdomDestroyedMapNotification)o).DestroyedKingdom;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0008AC87 File Offset: 0x00088E87
		internal static object AutoGeneratedGetMemberValueCreationTime(object o)
		{
			return ((KingdomDestroyedMapNotification)o).CreationTime;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0008AC99 File Offset: 0x00088E99
		public override TextObject TitleText
		{
			get
			{
				return new TextObject("{=QFKK1H4U}Kingdom Destroyed", null);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0008ACA6 File Offset: 0x00088EA6
		public override string SoundEventPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0008ACAD File Offset: 0x00088EAD
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x0008ACB5 File Offset: 0x00088EB5
		[SaveableProperty(1)]
		public Kingdom DestroyedKingdom { get; private set; }

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x0008ACBE File Offset: 0x00088EBE
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x0008ACC6 File Offset: 0x00088EC6
		[SaveableProperty(2)]
		public CampaignTime CreationTime { get; private set; }

		// Token: 0x0600205D RID: 8285 RVA: 0x0008ACCF File Offset: 0x00088ECF
		public KingdomDestroyedMapNotification(Kingdom destroyedKingdom, CampaignTime creationTime) : base(new TextObject("{=b5paUJjG}{NAME} is no more", null).SetTextVariable("NAME", destroyedKingdom.Name))
		{
			this.DestroyedKingdom = destroyedKingdom;
			this.CreationTime = creationTime;
		}
	}
}
