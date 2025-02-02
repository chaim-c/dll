using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000037 RID: 55
	public class AlleyLeaderDiedMapNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0001B486 File Offset: 0x00019686
		public AlleyLeaderDiedMapNotificationItemVM(AlleyLeaderDiedMapNotification data) : base(data)
		{
			this._alley = data.Alley;
			base.NotificationIdentifier = "alley_leader_died";
			this._onInspect = new Action(this.CreateAlleyLeaderDiedPopUp);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001B4B8 File Offset: 0x000196B8
		private void CreateAlleyLeaderDiedPopUp()
		{
			object obj = new TextObject("{=6QoSHiWC}An alley without a leader", null);
			TextObject textObject = new TextObject("{=FzbeSkBb}One of your alleys has lost its leader or is lacking troops. It will be abandoned after {DAYS} days have passed. You can assign a new clan member from the clan screen or travel to the alley to add more troops, if you wish to keep it. Any troops left in the alley will be lost when it is abandoned.", null);
			textObject.SetTextVariable("DAYS", (int)Campaign.Current.Models.AlleyModel.DestroyAlleyAfterDaysWhenLeaderIsDeath.ToDays);
			TextObject textObject2 = new TextObject("{=jVLJTuwl}Learn more", null);
			InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, textObject2.ToString(), GameTexts.FindText("str_dismiss", null).ToString(), new Action(this.OpenClanScreenAfterAlleyLeaderDeath), new Action(base.ExecuteRemove), "", 0f, null, null, null), false, false);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001B565 File Offset: 0x00019765
		private void OpenClanScreenAfterAlleyLeaderDeath()
		{
			if (base.NavigationHandler != null && this._alley != null)
			{
				base.NavigationHandler.OpenClan(this._alley);
				base.ExecuteRemove();
			}
		}

		// Token: 0x04000245 RID: 581
		private Alley _alley;
	}
}
