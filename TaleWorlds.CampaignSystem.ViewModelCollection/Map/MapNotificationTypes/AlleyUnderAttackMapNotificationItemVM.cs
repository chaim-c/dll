﻿using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000038 RID: 56
	public class AlleyUnderAttackMapNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0001B590 File Offset: 0x00019790
		public AlleyUnderAttackMapNotificationItemVM(AlleyUnderAttackMapNotification data) : base(data)
		{
			this._alley = data.Alley;
			base.NotificationIdentifier = "alley_under_attack";
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEnter));
			this._onInspect = delegate()
			{
				base.GoToMapPosition(this._alley.Settlement.Position2D);
			};
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001B5E4 File Offset: 0x000197E4
		private void OnSettlementEnter(MobileParty party, Settlement settlement, Hero hero)
		{
			if (party != null && party.IsMainParty && settlement == this._alley.Settlement)
			{
				CampaignEventDispatcher.Instance.RemoveListeners(this);
				base.ExecuteRemove();
			}
		}

		// Token: 0x04000246 RID: 582
		private Alley _alley;
	}
}
