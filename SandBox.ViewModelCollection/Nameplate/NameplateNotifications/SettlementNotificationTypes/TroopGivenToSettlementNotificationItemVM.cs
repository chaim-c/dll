using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes
{
	// Token: 0x02000023 RID: 35
	public class TroopGivenToSettlementNotificationItemVM : SettlementNotificationItemBaseVM
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000E411 File Offset: 0x0000C611
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000E419 File Offset: 0x0000C619
		public Hero GiverHero { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000E422 File Offset: 0x0000C622
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000E42A File Offset: 0x0000C62A
		public TroopRoster Troops { get; private set; }

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E434 File Offset: 0x0000C634
		public TroopGivenToSettlementNotificationItemVM(Action<SettlementNotificationItemBaseVM> onRemove, Hero giverHero, TroopRoster troops, int createdTick) : base(onRemove, createdTick)
		{
			this.GiverHero = giverHero;
			this.Troops = troops;
			base.Text = SandBoxUIHelper.GetTroopGivenToSettlementNotificationText(this.Troops.TotalManCount);
			base.CharacterName = ((this.GiverHero != null) ? this.GiverHero.Name.ToString() : "null hero");
			base.CharacterVisual = ((this.GiverHero != null) ? new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(this.GiverHero.CharacterObject, false)) : new ImageIdentifierVM(ImageIdentifierType.Null));
			base.RelationType = 0;
			base.CreatedTick = createdTick;
			if (this.GiverHero != null)
			{
				base.RelationType = (this.GiverHero.Clan.IsAtWarWith(Hero.MainHero.Clan) ? -1 : 1);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public void AddNewAction(TroopRoster newTroops)
		{
			this.Troops.Add(newTroops);
			base.Text = SandBoxUIHelper.GetTroopGivenToSettlementNotificationText(this.Troops.TotalManCount);
		}
	}
}
