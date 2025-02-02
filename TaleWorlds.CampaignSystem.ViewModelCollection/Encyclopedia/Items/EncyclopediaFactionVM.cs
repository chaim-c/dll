using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000CA RID: 202
	public class EncyclopediaFactionVM : ViewModel
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0004B932 File Offset: 0x00049B32
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0004B93A File Offset: 0x00049B3A
		public IFaction Faction { get; private set; }

		// Token: 0x0600139D RID: 5021 RVA: 0x0004B944 File Offset: 0x00049B44
		public EncyclopediaFactionVM(IFaction faction)
		{
			this.Faction = faction;
			if (faction != null)
			{
				this.ImageIdentifier = new ImageIdentifierVM(BannerCode.CreateFrom(faction.Banner), true);
				this.IsDestroyed = faction.IsEliminated;
			}
			else
			{
				this.ImageIdentifier = new ImageIdentifierVM(ImageIdentifierType.Null);
				this.IsDestroyed = false;
			}
			this.RefreshValues();
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004B99F File Offset: 0x00049B9F
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Faction != null)
			{
				this.NameText = this.Faction.Name.ToString();
				return;
			}
			this.NameText = new TextObject("{=2abtb4xu}Independent", null).ToString();
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004B9DC File Offset: 0x00049BDC
		public void ExecuteLink()
		{
			if (this.Faction != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Faction.EncyclopediaLink);
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0004BA00 File Offset: 0x00049C00
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x0004BA08 File Offset: 0x00049C08
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChanged("Banner");
				}
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0004BA25 File Offset: 0x00049C25
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x0004BA2D File Offset: 0x00049C2D
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

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0004BA50 File Offset: 0x00049C50
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0004BA58 File Offset: 0x00049C58
		[DataSourceProperty]
		public bool IsDestroyed
		{
			get
			{
				return this._isDestroyed;
			}
			set
			{
				if (value != this._isDestroyed)
				{
					this._isDestroyed = value;
					base.OnPropertyChangedWithValue(value, "IsDestroyed");
				}
			}
		}

		// Token: 0x04000913 RID: 2323
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x04000914 RID: 2324
		private string _nameText;

		// Token: 0x04000915 RID: 2325
		private bool _isDestroyed;
	}
}
