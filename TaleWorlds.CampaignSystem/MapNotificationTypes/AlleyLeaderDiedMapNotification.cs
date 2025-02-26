﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.MapNotificationTypes
{
	// Token: 0x0200026A RID: 618
	public class AlleyLeaderDiedMapNotification : InformationData
	{
		// Token: 0x0600205E RID: 8286 RVA: 0x0008AD00 File Offset: 0x00088F00
		internal static void AutoGeneratedStaticCollectObjectsAlleyLeaderDiedMapNotification(object o, List<object> collectedObjects)
		{
			((AlleyLeaderDiedMapNotification)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0008AD0E File Offset: 0x00088F0E
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.Alley);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0008AD23 File Offset: 0x00088F23
		internal static object AutoGeneratedGetMemberValueAlley(object o)
		{
			return ((AlleyLeaderDiedMapNotification)o).Alley;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0008AD30 File Offset: 0x00088F30
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x0008AD38 File Offset: 0x00088F38
		[SaveableProperty(10)]
		public Alley Alley { get; private set; }

		// Token: 0x06002063 RID: 8291 RVA: 0x0008AD41 File Offset: 0x00088F41
		public AlleyLeaderDiedMapNotification(Alley alley, TextObject description) : base(description)
		{
			this.Alley = alley;
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x0008AD51 File Offset: 0x00088F51
		public override TextObject TitleText
		{
			get
			{
				return new TextObject("{=6QoSHiWC}An alley without a leader", null);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x0008AD5E File Offset: 0x00088F5E
		public override string SoundEventPath
		{
			get
			{
				return "event:/ui/notification/death";
			}
		}
	}
}
