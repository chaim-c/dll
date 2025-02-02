using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000CD RID: 205
	public class EncyclopediaSettlementVM : ViewModel
	{
		// Token: 0x060013B1 RID: 5041 RVA: 0x0004BBA8 File Offset: 0x00049DA8
		public EncyclopediaSettlementVM(Settlement settlement)
		{
			if (!settlement.IsHideout)
			{
				this._settlement = settlement;
			}
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.FileName = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.RefreshValues();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004BBF7 File Offset: 0x00049DF7
		public override void RefreshValues()
		{
			base.RefreshValues();
			Settlement settlement = this._settlement;
			this.NameText = (((settlement != null) ? settlement.Name.ToString() : null) ?? "");
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004BC25 File Offset: 0x00049E25
		public void ExecuteLink()
		{
			if (this._settlement != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._settlement.EncyclopediaLink);
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004BC49 File Offset: 0x00049E49
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0004BC50 File Offset: 0x00049E50
		public void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this._settlement,
				true
			});
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0004BC79 File Offset: 0x00049E79
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0004BC81 File Offset: 0x00049E81
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

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0004BCA4 File Offset: 0x00049EA4
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x0004BCAC File Offset: 0x00049EAC
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

		// Token: 0x0400091B RID: 2331
		private Settlement _settlement;

		// Token: 0x0400091C RID: 2332
		private string _fileName;

		// Token: 0x0400091D RID: 2333
		private string _nameText;
	}
}
