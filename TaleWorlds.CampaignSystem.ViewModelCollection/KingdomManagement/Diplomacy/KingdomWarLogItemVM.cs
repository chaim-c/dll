using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000066 RID: 102
	public class KingdomWarLogItemVM : ViewModel
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x00025470 File Offset: 0x00023670
		public KingdomWarLogItemVM(IEncyclopediaLog log, IFaction effectorFaction)
		{
			this._log = log;
			this.Banner = new ImageIdentifierVM(BannerCode.CreateFrom(effectorFaction.Banner), true);
			this.RefreshValues();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002549C File Offset: 0x0002369C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.WarLogTimeText = this._log.GameTime.ToString();
			this.WarLogText = this._log.GetEncyclopediaText().ToString();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000254E4 File Offset: 0x000236E4
		private void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000254F6 File Offset: 0x000236F6
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x000254FE File Offset: 0x000236FE
		[DataSourceProperty]
		public string WarLogTimeText
		{
			get
			{
				return this._warLogTimeText;
			}
			set
			{
				if (value != this._warLogTimeText)
				{
					this._warLogTimeText = value;
					base.OnPropertyChangedWithValue<string>(value, "WarLogTimeText");
				}
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00025521 File Offset: 0x00023721
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00025529 File Offset: 0x00023729
		[DataSourceProperty]
		public string WarLogText
		{
			get
			{
				return this._warLogText;
			}
			set
			{
				if (value != this._warLogText)
				{
					this._warLogText = value;
					base.OnPropertyChangedWithValue<string>(value, "WarLogText");
				}
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002554C File Offset: 0x0002374C
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00025554 File Offset: 0x00023754
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

		// Token: 0x040003F5 RID: 1013
		private readonly IEncyclopediaLog _log;

		// Token: 0x040003F6 RID: 1014
		private string _warLogText;

		// Token: 0x040003F7 RID: 1015
		private string _warLogTimeText;

		// Token: 0x040003F8 RID: 1016
		private ImageIdentifierVM _banner;
	}
}
