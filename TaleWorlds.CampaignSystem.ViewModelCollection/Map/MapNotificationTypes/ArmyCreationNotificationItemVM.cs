using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000039 RID: 57
	public class ArmyCreationNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001B628 File Offset: 0x00019828
		public Army Army { get; }

		// Token: 0x06000560 RID: 1376 RVA: 0x0001B630 File Offset: 0x00019830
		public ArmyCreationNotificationItemVM(ArmyCreationMapNotification data) : base(data)
		{
			this.Army = data.CreatedArmy;
			base.NotificationIdentifier = "armycreation";
			this._onInspect = delegate()
			{
				Army army = this.Army;
				Vec2? vec;
				if (army == null)
				{
					vec = null;
				}
				else
				{
					MobileParty leaderParty = army.LeaderParty;
					vec = ((leaderParty != null) ? new Vec2?(leaderParty.Position2D) : null);
				}
				base.GoToMapPosition(vec ?? MobileParty.MainParty.Position2D);
			};
			CampaignEvents.OnPartyJoinedArmyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyJoinedArmy));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001B69B File Offset: 0x0001989B
		private void OnArmyDispersed(Army arg1, Army.ArmyDispersionReason arg2, bool isPlayersArmy)
		{
			if (arg1 == this.Army)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001B6AC File Offset: 0x000198AC
		private void OnPartyJoinedArmy(MobileParty party)
		{
			if (party == MobileParty.MainParty && party.Army == this.Army)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001B6CA File Offset: 0x000198CA
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnPartyJoinedArmyEvent.ClearListeners(this);
			CampaignEvents.ArmyDispersed.ClearListeners(this);
		}
	}
}
