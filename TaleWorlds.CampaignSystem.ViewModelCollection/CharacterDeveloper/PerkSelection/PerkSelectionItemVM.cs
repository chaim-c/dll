using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x02000128 RID: 296
	public class PerkSelectionItemVM : ViewModel
	{
		// Token: 0x06001D38 RID: 7480 RVA: 0x00068FB9 File Offset: 0x000671B9
		public PerkSelectionItemVM(PerkObject perk, Action<PerkSelectionItemVM> onSelection)
		{
			this.Perk = perk;
			this._onSelection = onSelection;
			this.RefreshValues();
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x00068FD8 File Offset: 0x000671D8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PickText = new TextObject("{=1CXlqb2U}Pick:", null).ToString();
			this.PerkName = this.Perk.Name.ToString();
			this.PerkDescription = this.Perk.Description.ToString();
			TextObject combinedPerkRoleText = CampaignUIHelper.GetCombinedPerkRoleText(this.Perk);
			this.PerkRole = (((combinedPerkRoleText != null) ? combinedPerkRoleText.ToString() : null) ?? "");
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x00069053 File Offset: 0x00067253
		public void ExecuteSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00069061 File Offset: 0x00067261
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x00069069 File Offset: 0x00067269
		[DataSourceProperty]
		public string PickText
		{
			get
			{
				return this._pickText;
			}
			set
			{
				if (value != this._pickText)
				{
					this._pickText = value;
					base.OnPropertyChangedWithValue<string>(value, "PickText");
				}
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0006908C File Offset: 0x0006728C
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x00069094 File Offset: 0x00067294
		[DataSourceProperty]
		public string PerkName
		{
			get
			{
				return this._perkName;
			}
			set
			{
				if (value != this._perkName)
				{
					this._perkName = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkName");
				}
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000690B7 File Offset: 0x000672B7
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x000690BF File Offset: 0x000672BF
		[DataSourceProperty]
		public string PerkDescription
		{
			get
			{
				return this._perkDescription;
			}
			set
			{
				if (value != this._perkDescription)
				{
					this._perkDescription = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkDescription");
				}
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x000690E2 File Offset: 0x000672E2
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x000690EA File Offset: 0x000672EA
		[DataSourceProperty]
		public string PerkRole
		{
			get
			{
				return this._perkRole;
			}
			set
			{
				if (value != this._perkRole)
				{
					this._perkRole = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkRole");
				}
			}
		}

		// Token: 0x04000DCC RID: 3532
		private readonly Action<PerkSelectionItemVM> _onSelection;

		// Token: 0x04000DCD RID: 3533
		public readonly PerkObject Perk;

		// Token: 0x04000DCE RID: 3534
		private string _pickText;

		// Token: 0x04000DCF RID: 3535
		private string _perkName;

		// Token: 0x04000DD0 RID: 3536
		private string _perkDescription;

		// Token: 0x04000DD1 RID: 3537
		private string _perkRole;
	}
}
