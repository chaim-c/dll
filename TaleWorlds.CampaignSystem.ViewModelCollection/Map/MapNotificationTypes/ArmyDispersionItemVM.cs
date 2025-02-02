using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200003A RID: 58
	public class ArmyDispersionItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x0001B74C File Offset: 0x0001994C
		public ArmyDispersionItemVM(ArmyDispersionMapNotification data) : base(data)
		{
			ArmyDispersionItemVM <>4__this = this;
			base.NotificationIdentifier = "armydispersion";
			this._onInspect = delegate()
			{
				INavigationHandler navigationHandler = <>4__this.NavigationHandler;
				if (navigationHandler == null)
				{
					return;
				}
				navigationHandler.OpenKingdom(data.DispersedArmy);
			};
		}
	}
}
