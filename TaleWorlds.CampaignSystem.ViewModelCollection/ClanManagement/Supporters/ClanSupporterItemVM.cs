using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Supporters
{
	// Token: 0x02000112 RID: 274
	public class ClanSupporterItemVM : ViewModel
	{
		// Token: 0x06001A73 RID: 6771 RVA: 0x0005F977 File Offset: 0x0005DB77
		public ClanSupporterItemVM(Hero hero)
		{
			this.Hero = new HeroVM(hero, false);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0005F98C File Offset: 0x0005DB8C
		public void ExecuteOpenTooltip()
		{
			InformationManager.ShowTooltip(typeof(Hero), new object[]
			{
				this.Hero.Hero,
				false
			});
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0005F9BA File Offset: 0x0005DBBA
		public void ExecuteCloseTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0005F9C1 File Offset: 0x0005DBC1
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0005F9C9 File Offset: 0x0005DBC9
		[DataSourceProperty]
		public HeroVM Hero
		{
			get
			{
				return this._hero;
			}
			set
			{
				if (value != this._hero)
				{
					this._hero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Hero");
				}
			}
		}

		// Token: 0x04000C81 RID: 3201
		private HeroVM _hero;
	}
}
