using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Armies
{
	// Token: 0x02000078 RID: 120
	public class KingdomArmyItemVM : KingdomItemVM
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0002AB3D File Offset: 0x00028D3D
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0002AB45 File Offset: 0x00028D45
		public float DistanceToMainParty { get; set; }

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002AB50 File Offset: 0x00028D50
		public KingdomArmyItemVM(Army army, Action<KingdomArmyItemVM> onSelect)
		{
			this.Army = army;
			this._onSelect = onSelect;
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			CampaignUIHelper.GetCharacterCode(army.ArmyOwner.CharacterObject, false);
			this.Leader = new HeroVM(this.Army.LeaderParty.LeaderHero, false);
			this.LordCount = army.Parties.Count;
			this.Strength = army.Parties.Sum((MobileParty p) => p.Party.NumberOfAllMembers);
			this.Location = army.GetBehaviorText(true).ToString();
			this.UpdateIsNew();
			this.Cohesion = (int)this.Army.Cohesion;
			this.Parties = new MBBindingList<KingdomArmyPartyItemVM>();
			foreach (MobileParty party in this.Army.Parties)
			{
				this.Parties.Add(new KingdomArmyPartyItemVM(party));
			}
			this.DistanceToMainParty = Campaign.Current.Models.MapDistanceModel.GetDistance(army.LeaderParty, MobileParty.MainParty);
			this.IsMainArmy = (army.LeaderParty == MobileParty.MainParty);
			this.RefreshValues();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ArmyName = this.Army.Name.ToString();
			GameTexts.SetVariable("STR1", GameTexts.FindText("str_cohesion", null));
			GameTexts.SetVariable("STR2", this.Cohesion.ToString());
			this.CohesionLabel = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_men_count", null));
			GameTexts.SetVariable("RIGHT", this.Strength.ToString());
			this.StrengthLabel = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
			this.Parties.ApplyActionOnAllItems(delegate(KingdomArmyPartyItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002AD91 File Offset: 0x00028F91
		protected override void OnSelect()
		{
			base.OnSelect();
			this._onSelect(this);
			this.ExecuteResetNew();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002ADAB File Offset: 0x00028FAB
		private void ExecuteResetNew()
		{
			if (base.IsNew)
			{
				this._viewDataTracker.OnArmyExamined(this.Army);
				this.UpdateIsNew();
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002ADCC File Offset: 0x00028FCC
		private void UpdateIsNew()
		{
			base.IsNew = this._viewDataTracker.UnExaminedArmies.Any((Army a) => a == this.Army);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002ADF0 File Offset: 0x00028FF0
		protected void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0002AE02 File Offset: 0x00029002
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0002AE0A File Offset: 0x0002900A
		[DataSourceProperty]
		public MBBindingList<KingdomArmyPartyItemVM> Parties
		{
			get
			{
				return this._parties;
			}
			set
			{
				if (value != this._parties)
				{
					this._parties = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomArmyPartyItemVM>>(value, "Parties");
				}
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002AE28 File Offset: 0x00029028
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0002AE30 File Offset: 0x00029030
		[DataSourceProperty]
		public HeroVM Leader
		{
			get
			{
				return this._leader;
			}
			set
			{
				if (value != this._leader)
				{
					this._leader = value;
					base.OnPropertyChanged("Visual");
				}
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002AE4D File Offset: 0x0002904D
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0002AE55 File Offset: 0x00029055
		[DataSourceProperty]
		public string ArmyName
		{
			get
			{
				return this._armyName;
			}
			set
			{
				if (value != this._armyName)
				{
					this._armyName = value;
					base.OnPropertyChangedWithValue<string>(value, "ArmyName");
				}
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002AE78 File Offset: 0x00029078
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0002AE80 File Offset: 0x00029080
		[DataSourceProperty]
		public int Cohesion
		{
			get
			{
				return this._cohesion;
			}
			set
			{
				if (value != this._cohesion)
				{
					this._cohesion = value;
					base.OnPropertyChangedWithValue(value, "Cohesion");
				}
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002AE9E File Offset: 0x0002909E
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0002AEA6 File Offset: 0x000290A6
		[DataSourceProperty]
		public string CohesionLabel
		{
			get
			{
				return this._cohesionLabel;
			}
			set
			{
				if (value != this._cohesionLabel)
				{
					this._cohesionLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "CohesionLabel");
				}
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002AEC9 File Offset: 0x000290C9
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0002AED1 File Offset: 0x000290D1
		[DataSourceProperty]
		public int LordCount
		{
			get
			{
				return this._lordCount;
			}
			set
			{
				if (value != this._lordCount)
				{
					this._lordCount = value;
					base.OnPropertyChangedWithValue(value, "LordCount");
				}
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002AEEF File Offset: 0x000290EF
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0002AEF7 File Offset: 0x000290F7
		[DataSourceProperty]
		public int Strength
		{
			get
			{
				return this._strength;
			}
			set
			{
				if (value != this._strength)
				{
					this._strength = value;
					base.OnPropertyChangedWithValue(value, "Strength");
				}
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002AF15 File Offset: 0x00029115
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002AF1D File Offset: 0x0002911D
		[DataSourceProperty]
		public string StrengthLabel
		{
			get
			{
				return this._strengthLabel;
			}
			set
			{
				if (value != this._strengthLabel)
				{
					this._strengthLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "StrengthLabel");
				}
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002AF40 File Offset: 0x00029140
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002AF48 File Offset: 0x00029148
		[DataSourceProperty]
		public string Location
		{
			get
			{
				return this._location;
			}
			set
			{
				if (value != this._location)
				{
					this._location = value;
					base.OnPropertyChanged("Objective");
				}
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002AF6A File Offset: 0x0002916A
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0002AF72 File Offset: 0x00029172
		[DataSourceProperty]
		public bool IsMainArmy
		{
			get
			{
				return this._isMainArmy;
			}
			set
			{
				if (value != this._isMainArmy)
				{
					this._isMainArmy = value;
					base.OnPropertyChangedWithValue(value, "IsMainArmy");
				}
			}
		}

		// Token: 0x040004CB RID: 1227
		public readonly Army Army;

		// Token: 0x040004CD RID: 1229
		private readonly Action<KingdomArmyItemVM> _onSelect;

		// Token: 0x040004CE RID: 1230
		private readonly IViewDataTracker _viewDataTracker;

		// Token: 0x040004CF RID: 1231
		private HeroVM _leader;

		// Token: 0x040004D0 RID: 1232
		private MBBindingList<KingdomArmyPartyItemVM> _parties;

		// Token: 0x040004D1 RID: 1233
		private string _armyName;

		// Token: 0x040004D2 RID: 1234
		private int _strength;

		// Token: 0x040004D3 RID: 1235
		private int _cohesion;

		// Token: 0x040004D4 RID: 1236
		private string _strengthLabel;

		// Token: 0x040004D5 RID: 1237
		private int _lordCount;

		// Token: 0x040004D6 RID: 1238
		private string _location;

		// Token: 0x040004D7 RID: 1239
		private string _cohesionLabel;

		// Token: 0x040004D8 RID: 1240
		private bool _isMainArmy;
	}
}
