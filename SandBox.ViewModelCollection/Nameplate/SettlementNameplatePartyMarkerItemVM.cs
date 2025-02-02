using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000019 RID: 25
	public class SettlementNameplatePartyMarkerItemVM : ViewModel
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000BBCD File Offset: 0x00009DCD
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public MobileParty Party { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000BBDE File Offset: 0x00009DDE
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000BBE6 File Offset: 0x00009DE6
		public int SortIndex { get; private set; }

		// Token: 0x0600025C RID: 604 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		public SettlementNameplatePartyMarkerItemVM(MobileParty mobileParty)
		{
			this.Party = mobileParty;
			if (mobileParty.IsCaravan)
			{
				this.IsCaravan = true;
				this.SortIndex = 1;
				return;
			}
			if (mobileParty.IsLordParty && mobileParty.LeaderHero != null)
			{
				this.IsLord = true;
				Clan actualClan = mobileParty.ActualClan;
				this.Visual = new ImageIdentifierVM(BannerCode.CreateFrom((actualClan != null) ? actualClan.Banner : null), true);
				this.SortIndex = 0;
				return;
			}
			this.IsDefault = true;
			this.SortIndex = 2;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000BC71 File Offset: 0x00009E71
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000BC79 File Offset: 0x00009E79
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000BC97 File Offset: 0x00009E97
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000BC9F File Offset: 0x00009E9F
		public bool IsCaravan
		{
			get
			{
				return this._isCaravan;
			}
			set
			{
				if (value != this._isCaravan)
				{
					this._isCaravan = value;
					base.OnPropertyChangedWithValue(value, "IsCaravan");
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000BCBD File Offset: 0x00009EBD
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000BCC5 File Offset: 0x00009EC5
		public bool IsLord
		{
			get
			{
				return this._isLord;
			}
			set
			{
				if (value != this._isLord)
				{
					this._isLord = value;
					base.OnPropertyChangedWithValue(value, "IsLord");
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000BCE3 File Offset: 0x00009EE3
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000BCEB File Offset: 0x00009EEB
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
			set
			{
				if (value != this._isDefault)
				{
					this._isDefault = value;
					base.OnPropertyChangedWithValue(value, "IsDefault");
				}
			}
		}

		// Token: 0x04000124 RID: 292
		private ImageIdentifierVM _visual;

		// Token: 0x04000125 RID: 293
		private bool _isCaravan;

		// Token: 0x04000126 RID: 294
		private bool _isLord;

		// Token: 0x04000127 RID: 295
		private bool _isDefault;
	}
}
