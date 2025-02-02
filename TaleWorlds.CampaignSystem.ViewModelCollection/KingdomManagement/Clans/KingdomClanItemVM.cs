using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Clans
{
	// Token: 0x02000075 RID: 117
	public class KingdomClanItemVM : KingdomItemVM
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x00029828 File Offset: 0x00027A28
		public KingdomClanItemVM(Clan clan, Action<KingdomClanItemVM> onSelect)
		{
			this.Clan = clan;
			this._onSelect = onSelect;
			this.Banner = new ImageIdentifierVM(clan.Banner);
			this.Banner_9 = new ImageIdentifierVM(BannerCode.CreateFrom(clan.Banner), true);
			this.RefreshValues();
			this.Refresh();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00029884 File Offset: 0x00027A84
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.Clan.Name.ToString();
			GameTexts.SetVariable("TIER", this.Clan.Tier);
			this.TierText = GameTexts.FindText("str_clan_tier", null).ToString();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000298D8 File Offset: 0x00027AD8
		public void Refresh()
		{
			this.Members = new MBBindingList<HeroVM>();
			this.ClanType = 0;
			if (this.Clan.IsUnderMercenaryService)
			{
				this.ClanType = 2;
			}
			else if (this.Clan.Kingdom.RulingClan == this.Clan)
			{
				this.ClanType = 1;
			}
			foreach (Hero hero in from h in this.Clan.Heroes
			where !h.IsDisabled && !h.IsNotSpawned && h.IsAlive && !h.IsChild
			select h)
			{
				this.Members.Add(new HeroVM(hero, false));
			}
			this.NumOfMembers = this.Members.Count;
			this.Fiefs = new MBBindingList<KingdomClanFiefItemVM>();
			foreach (Settlement settlement in from s in this.Clan.Settlements
			where s.IsTown || s.IsCastle
			select s)
			{
				this.Fiefs.Add(new KingdomClanFiefItemVM(settlement));
			}
			this.NumOfFiefs = this.Fiefs.Count;
			this.Influence = (int)this.Clan.Influence;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00029A50 File Offset: 0x00027C50
		protected override void OnSelect()
		{
			base.OnSelect();
			this._onSelect(this);
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00029A64 File Offset: 0x00027C64
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00029A6C File Offset: 0x00027C6C
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00029A8F File Offset: 0x00027C8F
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00029A97 File Offset: 0x00027C97
		[DataSourceProperty]
		public int ClanType
		{
			get
			{
				return this._clanType;
			}
			set
			{
				if (value != this._clanType)
				{
					this._clanType = value;
					base.OnPropertyChangedWithValue(value, "ClanType");
				}
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00029AB5 File Offset: 0x00027CB5
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00029ABD File Offset: 0x00027CBD
		[DataSourceProperty]
		public int NumOfMembers
		{
			get
			{
				return this._numOfMembers;
			}
			set
			{
				if (value != this._numOfMembers)
				{
					this._numOfMembers = value;
					base.OnPropertyChangedWithValue(value, "NumOfMembers");
				}
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00029ADB File Offset: 0x00027CDB
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00029AE3 File Offset: 0x00027CE3
		[DataSourceProperty]
		public int NumOfFiefs
		{
			get
			{
				return this._numOfFiefs;
			}
			set
			{
				if (value != this._numOfFiefs)
				{
					this._numOfFiefs = value;
					base.OnPropertyChangedWithValue(value, "NumOfFiefs");
				}
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00029B01 File Offset: 0x00027D01
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00029B09 File Offset: 0x00027D09
		[DataSourceProperty]
		public string TierText
		{
			get
			{
				return this._tierText;
			}
			set
			{
				if (value != this._tierText)
				{
					this._tierText = value;
					base.OnPropertyChangedWithValue<string>(value, "TierText");
				}
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00029B2C File Offset: 0x00027D2C
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x00029B34 File Offset: 0x00027D34
		[DataSourceProperty]
		public ImageIdentifierVM Banner
		{
			get
			{
				return this._banner;
			}
			set
			{
				if (value != this._banner)
				{
					this._banner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner");
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00029B52 File Offset: 0x00027D52
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x00029B5A File Offset: 0x00027D5A
		[DataSourceProperty]
		public ImageIdentifierVM Banner_9
		{
			get
			{
				return this._banner_9;
			}
			set
			{
				if (value != this._banner_9)
				{
					this._banner_9 = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner_9");
				}
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00029B78 File Offset: 0x00027D78
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x00029B80 File Offset: 0x00027D80
		[DataSourceProperty]
		public MBBindingList<HeroVM> Members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != this._members)
				{
					this._members = value;
					base.OnPropertyChangedWithValue<MBBindingList<HeroVM>>(value, "Members");
				}
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00029B9E File Offset: 0x00027D9E
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00029BA6 File Offset: 0x00027DA6
		[DataSourceProperty]
		public MBBindingList<KingdomClanFiefItemVM> Fiefs
		{
			get
			{
				return this._fiefs;
			}
			set
			{
				if (value != this._fiefs)
				{
					this._fiefs = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomClanFiefItemVM>>(value, "Fiefs");
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00029BC4 File Offset: 0x00027DC4
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00029BCC File Offset: 0x00027DCC
		[DataSourceProperty]
		public int Influence
		{
			get
			{
				return this._influence;
			}
			set
			{
				if (value != this._influence)
				{
					this._influence = value;
					base.OnPropertyChangedWithValue(value, "Influence");
				}
			}
		}

		// Token: 0x0400049A RID: 1178
		private readonly Action<KingdomClanItemVM> _onSelect;

		// Token: 0x0400049B RID: 1179
		public readonly Clan Clan;

		// Token: 0x0400049C RID: 1180
		private string _name;

		// Token: 0x0400049D RID: 1181
		private ImageIdentifierVM _banner;

		// Token: 0x0400049E RID: 1182
		private ImageIdentifierVM _banner_9;

		// Token: 0x0400049F RID: 1183
		private MBBindingList<HeroVM> _members;

		// Token: 0x040004A0 RID: 1184
		private MBBindingList<KingdomClanFiefItemVM> _fiefs;

		// Token: 0x040004A1 RID: 1185
		private int _influence;

		// Token: 0x040004A2 RID: 1186
		private int _numOfMembers;

		// Token: 0x040004A3 RID: 1187
		private int _numOfFiefs;

		// Token: 0x040004A4 RID: 1188
		private string _tierText;

		// Token: 0x040004A5 RID: 1189
		private int _clanType = -1;

		// Token: 0x020001B2 RID: 434
		private enum ClanTypes
		{
			// Token: 0x04001009 RID: 4105
			Normal,
			// Token: 0x0400100A RID: 4106
			Leader,
			// Token: 0x0400100B RID: 4107
			Mercenary
		}
	}
}
