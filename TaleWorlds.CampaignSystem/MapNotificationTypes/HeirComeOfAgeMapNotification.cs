﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.MapNotificationTypes
{
	// Token: 0x02000268 RID: 616
	public class HeirComeOfAgeMapNotification : InformationData
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x0008AB74 File Offset: 0x00088D74
		internal static void AutoGeneratedStaticCollectObjectsHeirComeOfAgeMapNotification(object o, List<object> collectedObjects)
		{
			((HeirComeOfAgeMapNotification)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0008AB82 File Offset: 0x00088D82
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.ComeOfAgeHero);
			collectedObjects.Add(this.MentorHero);
			CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.CreationTime, collectedObjects);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0008ABB4 File Offset: 0x00088DB4
		internal static object AutoGeneratedGetMemberValueComeOfAgeHero(object o)
		{
			return ((HeirComeOfAgeMapNotification)o).ComeOfAgeHero;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0008ABC1 File Offset: 0x00088DC1
		internal static object AutoGeneratedGetMemberValueMentorHero(object o)
		{
			return ((HeirComeOfAgeMapNotification)o).MentorHero;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0008ABCE File Offset: 0x00088DCE
		internal static object AutoGeneratedGetMemberValueCreationTime(object o)
		{
			return ((HeirComeOfAgeMapNotification)o).CreationTime;
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0008ABE0 File Offset: 0x00088DE0
		public override TextObject TitleText
		{
			get
			{
				return new TextObject("{=5jAH0VV7}Heir Come of Age", null);
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x0008ABED File Offset: 0x00088DED
		public override string SoundEventPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0008ABF4 File Offset: 0x00088DF4
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x0008ABFC File Offset: 0x00088DFC
		[SaveableProperty(1)]
		public Hero ComeOfAgeHero { get; private set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0008AC05 File Offset: 0x00088E05
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x0008AC0D File Offset: 0x00088E0D
		[SaveableProperty(2)]
		public Hero MentorHero { get; private set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0008AC16 File Offset: 0x00088E16
		// (set) Token: 0x06002051 RID: 8273 RVA: 0x0008AC1E File Offset: 0x00088E1E
		[SaveableProperty(3)]
		public CampaignTime CreationTime { get; private set; }

		// Token: 0x06002052 RID: 8274 RVA: 0x0008AC27 File Offset: 0x00088E27
		public HeirComeOfAgeMapNotification(Hero comeOfAgeHero, Hero mentorHero, TextObject descriptionText, CampaignTime creationTime) : base(descriptionText)
		{
			this.ComeOfAgeHero = comeOfAgeHero;
			this.MentorHero = mentorHero;
			this.CreationTime = creationTime;
		}
	}
}
