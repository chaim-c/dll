using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200004F RID: 79
	public class WarNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		public WarNotificationItemVM(WarMapNotification data) : base(data)
		{
			WarNotificationItemVM <>4__this = this;
			base.NotificationIdentifier = "battle";
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnPeaceMade));
			if (!data.FirstFaction.IsRebelClan && !data.SecondFaction.IsRebelClan)
			{
				this._onInspect = delegate()
				{
					INavigationHandler navigationHandler = <>4__this.NavigationHandler;
					if (navigationHandler == null)
					{
						return;
					}
					navigationHandler.OpenKingdom((data.FirstFaction == Hero.MainHero.MapFaction) ? data.SecondFaction : data.FirstFaction);
				};
				return;
			}
			this._onInspect = null;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001CBA9 File Offset: 0x0001ADA9
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.MakePeace.ClearListeners(this);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		private void OnPeaceMade(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			if (faction1 == Hero.MainHero.Clan || (Hero.MainHero.MapFaction != null && (faction1 == Hero.MainHero.MapFaction || faction2 == Hero.MainHero.MapFaction)))
			{
				base.ExecuteRemove();
			}
		}
	}
}
