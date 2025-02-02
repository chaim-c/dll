using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200004D RID: 77
	public class TraitChangedNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0001C9B0 File Offset: 0x0001ABB0
		public TraitChangedNotificationItemVM(TraitChangedMapNotification data) : base(data)
		{
			int currentTraitLevel = data.CurrentTraitLevel;
			int previousTraitLevel = data.PreviousTraitLevel;
			if (currentTraitLevel == 0 && previousTraitLevel != 0)
			{
				base.NotificationIdentifier = "traitlost_" + data.Trait.StringId.ToLower() + "_by_" + ((previousTraitLevel > 0) ? "decrease" : "increase");
			}
			else
			{
				base.NotificationIdentifier = "traitgained_" + data.Trait.StringId.ToLower() + "_" + currentTraitLevel.ToString();
			}
			this._onInspect = delegate()
			{
				INavigationHandler navigationHandler = base.NavigationHandler;
				if (navigationHandler == null)
				{
					return;
				}
				navigationHandler.OpenCharacterDeveloper();
			};
		}
	}
}
