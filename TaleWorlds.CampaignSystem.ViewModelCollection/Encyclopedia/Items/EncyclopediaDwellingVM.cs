using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000C9 RID: 201
	public class EncyclopediaDwellingVM : ViewModel
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x0004B848 File Offset: 0x00049A48
		public EncyclopediaDwellingVM(WorkshopType workshop)
		{
			this._workshop = workshop;
			this.FileName = workshop.StringId;
			this.RefreshValues();
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004B869 File Offset: 0x00049A69
		public EncyclopediaDwellingVM(VillageType villageType)
		{
			this._villageType = villageType;
			this.FileName = villageType.StringId;
			this.RefreshValues();
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004B88C File Offset: 0x00049A8C
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._workshop != null)
			{
				this.NameText = this._workshop.Name.ToString();
				return;
			}
			if (this._villageType != null)
			{
				this.NameText = this._villageType.ShortName.ToString();
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004B8DC File Offset: 0x00049ADC
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		[DataSourceProperty]
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				if (value != this._fileName)
				{
					this._fileName = value;
					base.OnPropertyChangedWithValue<string>(value, "FileName");
				}
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0004B907 File Offset: 0x00049B07
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0004B90F File Offset: 0x00049B0F
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

		// Token: 0x0400090E RID: 2318
		private readonly WorkshopType _workshop;

		// Token: 0x0400090F RID: 2319
		private readonly VillageType _villageType;

		// Token: 0x04000910 RID: 2320
		private string _fileName;

		// Token: 0x04000911 RID: 2321
		private string _nameText;
	}
}
