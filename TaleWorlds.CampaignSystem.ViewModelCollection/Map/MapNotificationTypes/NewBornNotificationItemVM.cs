﻿using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000044 RID: 68
	public class NewBornNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0001C148 File Offset: 0x0001A348
		public NewBornNotificationItemVM(ChildBornMapNotification data) : base(data)
		{
			base.NotificationIdentifier = "newborn";
			if (data.NewbornHero != null)
			{
				Hero mother = data.NewbornHero.Mother;
				if (mother.Spouse == Hero.MainHero)
				{
					Hero spouse = mother.Spouse;
					if (spouse != null && spouse.IsAlive)
					{
						this._notification = new NewBornFemaleHeroSceneNotificationItem(mother.Spouse, mother, data.CreationTime);
					}
					else
					{
						this._notification = new NewBornFemaleHeroSceneAlternateNotificationItem(mother.Spouse, mother, data.CreationTime);
					}
				}
				else
				{
					Hero spouse2 = mother.Spouse;
					if (spouse2 != null && spouse2.IsAlive)
					{
						this._notification = new NewBornSceneNotificationItem(mother.Spouse, mother, data.CreationTime);
					}
					else
					{
						this._notification = new NewBornFemaleHeroSceneAlternateNotificationItem(mother.Spouse, mother, data.CreationTime);
					}
				}
			}
			if (this._notification != null)
			{
				this._onInspect = delegate()
				{
					MBInformationManager.ShowSceneNotification(this._notification);
				};
				return;
			}
			this._onInspect = delegate()
			{
			};
		}

		// Token: 0x04000263 RID: 611
		private readonly SceneNotificationData _notification;
	}
}
