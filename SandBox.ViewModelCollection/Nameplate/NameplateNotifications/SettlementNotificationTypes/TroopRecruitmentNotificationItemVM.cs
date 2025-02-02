using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes
{
	// Token: 0x02000024 RID: 36
	public class TroopRecruitmentNotificationItemVM : SettlementNotificationItemBaseVM
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000E520 File Offset: 0x0000C720
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000E528 File Offset: 0x0000C728
		public Hero RecruiterHero { get; private set; }

		// Token: 0x060002EC RID: 748 RVA: 0x0000E534 File Offset: 0x0000C734
		public TroopRecruitmentNotificationItemVM(Action<SettlementNotificationItemBaseVM> onRemove, Hero recruiterHero, int amount, int createdTick) : base(onRemove, createdTick)
		{
			base.Text = SandBoxUIHelper.GetRecruitNotificationText(amount);
			this._recruitAmount = amount;
			this.RecruiterHero = recruiterHero;
			base.CharacterName = ((recruiterHero != null) ? recruiterHero.Name.ToString() : "null hero");
			base.CharacterVisual = ((recruiterHero != null) ? new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(recruiterHero.CharacterObject, false)) : new ImageIdentifierVM(ImageIdentifierType.Null));
			base.RelationType = 0;
			base.CreatedTick = createdTick;
			if (recruiterHero != null)
			{
				base.RelationType = (recruiterHero.Clan.IsAtWarWith(Hero.MainHero.Clan) ? -1 : 1);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public void AddNewAction(int addedAmount)
		{
			this._recruitAmount += addedAmount;
			base.Text = SandBoxUIHelper.GetRecruitNotificationText(this._recruitAmount);
		}

		// Token: 0x04000177 RID: 375
		private int _recruitAmount;
	}
}
