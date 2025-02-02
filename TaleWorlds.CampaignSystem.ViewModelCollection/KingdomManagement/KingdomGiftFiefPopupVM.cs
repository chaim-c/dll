using System;
using Helpers;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Clans;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement
{
	// Token: 0x02000057 RID: 87
	public class KingdomGiftFiefPopupVM : ViewModel
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x0001F4F5 File Offset: 0x0001D6F5
		public KingdomGiftFiefPopupVM(Action onSettlementGranted)
		{
			this._clans = new MBBindingList<KingdomClanItemVM>();
			this._onSettlementGranted = onSettlementGranted;
			this.ClanSortController = new KingdomClanSortControllerVM(ref this._clans);
			this.RefreshValues();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001F528 File Offset: 0x0001D728
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TitleText = new TextObject("{=rOKAvjtT}Gift Settlement", null).ToString();
			this.GiftText = GameTexts.FindText("str_gift", null).ToString();
			this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
			this.NameText = GameTexts.FindText("str_scoreboard_header", "name").ToString();
			this.InfluenceText = GameTexts.FindText("str_influence", null).ToString();
			this.FiefsText = GameTexts.FindText("str_fiefs", null).ToString();
			this.MembersText = GameTexts.FindText("str_members", null).ToString();
			this.BannerText = GameTexts.FindText("str_banner", null).ToString();
			this.TypeText = GameTexts.FindText("str_sort_by_type_label", null).ToString();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001F605 File Offset: 0x0001D805
		private void SetCurrentSelectedClan(KingdomClanItemVM clan)
		{
			if (clan != this.CurrentSelectedClan)
			{
				if (this.CurrentSelectedClan != null)
				{
					this.CurrentSelectedClan.IsSelected = false;
				}
				this.CurrentSelectedClan = clan;
				this.CurrentSelectedClan.IsSelected = true;
				this.IsAnyClanSelected = true;
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001F640 File Offset: 0x0001D840
		private void RefreshClanList()
		{
			this.Clans.Clear();
			foreach (Clan clan in Clan.PlayerClan.Kingdom.Clans)
			{
				if (FactionHelper.CanClanBeGrantedFief(clan))
				{
					this.Clans.Add(new KingdomClanItemVM(clan, new Action<KingdomClanItemVM>(this.SetCurrentSelectedClan)));
				}
			}
			if (this.Clans.Count > 0)
			{
				this.SetCurrentSelectedClan(this.Clans[0]);
			}
			if (this.ClanSortController != null)
			{
				this.ClanSortController.SortByCurrentState();
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		public void OpenWith(Settlement settlement)
		{
			this._settlementToGive = settlement;
			this.RefreshClanList();
			this.IsOpen = true;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001F70E File Offset: 0x0001D90E
		public void Close()
		{
			this._settlementToGive = null;
			this.IsOpen = false;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001F720 File Offset: 0x0001D920
		private void ExecuteGiftSettlement()
		{
			if (this._settlementToGive != null && this.CurrentSelectedClan != null)
			{
				Campaign.Current.KingdomManager.GiftSettlementOwnership(this._settlementToGive, this.CurrentSelectedClan.Clan);
				this.Close();
				this._onSettlementGranted();
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001F76E File Offset: 0x0001D96E
		private void ExecuteClose()
		{
			this.Close();
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001F776 File Offset: 0x0001D976
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001F77E File Offset: 0x0001D97E
		[DataSourceProperty]
		public bool IsAnyClanSelected
		{
			get
			{
				return this._isAnyClanSelected;
			}
			set
			{
				if (value != this._isAnyClanSelected)
				{
					this._isAnyClanSelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyClanSelected");
				}
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001F79C File Offset: 0x0001D99C
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
		[DataSourceProperty]
		public MBBindingList<KingdomClanItemVM> Clans
		{
			get
			{
				return this._clans;
			}
			set
			{
				if (value != this._clans)
				{
					this._clans = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomClanItemVM>>(value, "Clans");
				}
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001F7C2 File Offset: 0x0001D9C2
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001F7CA File Offset: 0x0001D9CA
		[DataSourceProperty]
		public KingdomClanItemVM CurrentSelectedClan
		{
			get
			{
				return this._currentSelectedClan;
			}
			set
			{
				if (value != this._currentSelectedClan)
				{
					this._currentSelectedClan = value;
					base.OnPropertyChangedWithValue<KingdomClanItemVM>(value, "CurrentSelectedClan");
				}
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		[DataSourceProperty]
		public KingdomClanSortControllerVM ClanSortController
		{
			get
			{
				return this._clanSortController;
			}
			set
			{
				if (value != this._clanSortController)
				{
					this._clanSortController = value;
					base.OnPropertyChangedWithValue<KingdomClanSortControllerVM>(value, "ClanSortController");
				}
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001F80E File Offset: 0x0001DA0E
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001F816 File Offset: 0x0001DA16
		[DataSourceProperty]
		public bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
			set
			{
				if (value != this._isOpen)
				{
					this._isOpen = value;
					base.OnPropertyChangedWithValue(value, "IsOpen");
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001F834 File Offset: 0x0001DA34
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001F83C File Offset: 0x0001DA3C
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001F85F File Offset: 0x0001DA5F
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001F867 File Offset: 0x0001DA67
		[DataSourceProperty]
		public string GiftText
		{
			get
			{
				return this._giftText;
			}
			set
			{
				if (value != this._giftText)
				{
					this._giftText = value;
					base.OnPropertyChangedWithValue<string>(value, "GiftText");
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001F88A File Offset: 0x0001DA8A
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001F892 File Offset: 0x0001DA92
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001F8B5 File Offset: 0x0001DAB5
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001F8BD File Offset: 0x0001DABD
		[DataSourceProperty]
		public string BannerText
		{
			get
			{
				return this._bannerText;
			}
			set
			{
				if (value != this._bannerText)
				{
					this._bannerText = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerText");
				}
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		[DataSourceProperty]
		public string TypeText
		{
			get
			{
				return this._typeText;
			}
			set
			{
				if (value != this._typeText)
				{
					this._typeText = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeText");
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001F90B File Offset: 0x0001DB0B
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001F913 File Offset: 0x0001DB13
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001F936 File Offset: 0x0001DB36
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001F93E File Offset: 0x0001DB3E
		[DataSourceProperty]
		public string InfluenceText
		{
			get
			{
				return this._influenceText;
			}
			set
			{
				if (value != this._influenceText)
				{
					this._influenceText = value;
					base.OnPropertyChangedWithValue<string>(value, "InfluenceText");
				}
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001F961 File Offset: 0x0001DB61
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001F969 File Offset: 0x0001DB69
		[DataSourceProperty]
		public string FiefsText
		{
			get
			{
				return this._fiefsText;
			}
			set
			{
				if (value != this._fiefsText)
				{
					this._fiefsText = value;
					base.OnPropertyChangedWithValue<string>(value, "FiefsText");
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001F98C File Offset: 0x0001DB8C
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001F994 File Offset: 0x0001DB94
		[DataSourceProperty]
		public string MembersText
		{
			get
			{
				return this._membersText;
			}
			set
			{
				if (value != this._membersText)
				{
					this._membersText = value;
					base.OnPropertyChangedWithValue<string>(value, "MembersText");
				}
			}
		}

		// Token: 0x0400030E RID: 782
		private Settlement _settlementToGive;

		// Token: 0x0400030F RID: 783
		private Action _onSettlementGranted;

		// Token: 0x04000310 RID: 784
		private bool _isAnyClanSelected;

		// Token: 0x04000311 RID: 785
		private MBBindingList<KingdomClanItemVM> _clans;

		// Token: 0x04000312 RID: 786
		private KingdomClanItemVM _currentSelectedClan;

		// Token: 0x04000313 RID: 787
		private KingdomClanSortControllerVM _clanSortController;

		// Token: 0x04000314 RID: 788
		private bool _isOpen;

		// Token: 0x04000315 RID: 789
		private string _titleText;

		// Token: 0x04000316 RID: 790
		private string _giftText;

		// Token: 0x04000317 RID: 791
		private string _cancelText;

		// Token: 0x04000318 RID: 792
		private string _bannerText;

		// Token: 0x04000319 RID: 793
		private string _nameText;

		// Token: 0x0400031A RID: 794
		private string _influenceText;

		// Token: 0x0400031B RID: 795
		private string _membersText;

		// Token: 0x0400031C RID: 796
		private string _fiefsText;

		// Token: 0x0400031D RID: 797
		private string _typeText;
	}
}
