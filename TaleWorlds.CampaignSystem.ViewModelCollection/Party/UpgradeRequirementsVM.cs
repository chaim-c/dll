using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x0200002E RID: 46
	public class UpgradeRequirementsVM : ViewModel
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00019112 File Offset: 0x00017312
		public UpgradeRequirementsVM()
		{
			this.IsItemRequirementMet = true;
			this.IsPerkRequirementMet = true;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001913E File Offset: 0x0001733E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.UpdateItemRequirementHint();
			this.UpdatePerkRequirementHint();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00019152 File Offset: 0x00017352
		public void SetItemRequirement(ItemCategory category)
		{
			if (category != null)
			{
				this.HasItemRequirement = true;
				this._category = category;
				this.ItemRequirement = category.StringId.ToLower();
				this.UpdateItemRequirementHint();
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001917C File Offset: 0x0001737C
		public void SetPerkRequirement(PerkObject perk)
		{
			if (perk != null)
			{
				this.HasPerkRequirement = true;
				this._perk = perk;
				this.PerkRequirement = perk.Skill.StringId.ToLower();
				this.UpdatePerkRequirementHint();
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000191AB File Offset: 0x000173AB
		public void SetRequirementsMet(bool isItemRequirementMet, bool isPerkRequirementMet)
		{
			this.IsItemRequirementMet = (!this.HasItemRequirement || isItemRequirementMet);
			this.IsPerkRequirementMet = (!this.HasPerkRequirement || isPerkRequirementMet);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000191D0 File Offset: 0x000173D0
		private void UpdateItemRequirementHint()
		{
			if (this._category == null)
			{
				return;
			}
			TextObject textObject = new TextObject("{=Q0j1umAt}Requirement: {REQUIREMENT_NAME}", null);
			textObject.SetTextVariable("REQUIREMENT_NAME", this._category.GetName().ToString());
			this.ItemRequirementHint = new HintViewModel(textObject, null);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001921C File Offset: 0x0001741C
		private void UpdatePerkRequirementHint()
		{
			if (this._perk == null)
			{
				return;
			}
			TextObject textObject = new TextObject("{=Q0j1umAt}Requirement: {REQUIREMENT_NAME}", null);
			textObject.SetTextVariable("REQUIREMENT_NAME", this._perk.Name.ToString());
			this.PerkRequirementHint = new HintViewModel(textObject, null);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00019267 File Offset: 0x00017467
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001926F File Offset: 0x0001746F
		[DataSourceProperty]
		public bool IsItemRequirementMet
		{
			get
			{
				return this._isItemRequirementMet;
			}
			set
			{
				if (value != this._isItemRequirementMet)
				{
					this._isItemRequirementMet = value;
					base.OnPropertyChangedWithValue(value, "IsItemRequirementMet");
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001928D File Offset: 0x0001748D
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00019295 File Offset: 0x00017495
		[DataSourceProperty]
		public bool IsPerkRequirementMet
		{
			get
			{
				return this._isPerkRequirementMet;
			}
			set
			{
				if (value != this._isPerkRequirementMet)
				{
					this._isPerkRequirementMet = value;
					base.OnPropertyChangedWithValue(value, "IsPerkRequirementMet");
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000192B3 File Offset: 0x000174B3
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000192BB File Offset: 0x000174BB
		[DataSourceProperty]
		public bool HasItemRequirement
		{
			get
			{
				return this._hasItemRequirement;
			}
			set
			{
				if (value != this._hasItemRequirement)
				{
					this._hasItemRequirement = value;
					base.OnPropertyChangedWithValue(value, "HasItemRequirement");
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000192D9 File Offset: 0x000174D9
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000192E1 File Offset: 0x000174E1
		[DataSourceProperty]
		public bool HasPerkRequirement
		{
			get
			{
				return this._hasPerkRequirement;
			}
			set
			{
				if (value != this._hasPerkRequirement)
				{
					this._hasPerkRequirement = value;
					base.OnPropertyChangedWithValue(value, "HasPerkRequirement");
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000192FF File Offset: 0x000174FF
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00019307 File Offset: 0x00017507
		[DataSourceProperty]
		public string PerkRequirement
		{
			get
			{
				return this._perkRequirement;
			}
			set
			{
				if (value != this._perkRequirement)
				{
					this._perkRequirement = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkRequirement");
				}
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001932A File Offset: 0x0001752A
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00019332 File Offset: 0x00017532
		[DataSourceProperty]
		public string ItemRequirement
		{
			get
			{
				return this._itemRequirement;
			}
			set
			{
				if (value != this._itemRequirement)
				{
					this._itemRequirement = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemRequirement");
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00019355 File Offset: 0x00017555
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001935D File Offset: 0x0001755D
		[DataSourceProperty]
		public HintViewModel ItemRequirementHint
		{
			get
			{
				return this._itemRequirementHint;
			}
			set
			{
				if (value != this._itemRequirementHint)
				{
					this._itemRequirementHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ItemRequirementHint");
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001937B File Offset: 0x0001757B
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x00019383 File Offset: 0x00017583
		[DataSourceProperty]
		public HintViewModel PerkRequirementHint
		{
			get
			{
				return this._perkRequirementHint;
			}
			set
			{
				if (value != this._perkRequirementHint)
				{
					this._perkRequirementHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PerkRequirementHint");
				}
			}
		}

		// Token: 0x040001F1 RID: 497
		private ItemCategory _category;

		// Token: 0x040001F2 RID: 498
		private PerkObject _perk;

		// Token: 0x040001F3 RID: 499
		private bool _isItemRequirementMet;

		// Token: 0x040001F4 RID: 500
		private bool _isPerkRequirementMet;

		// Token: 0x040001F5 RID: 501
		private bool _hasItemRequirement;

		// Token: 0x040001F6 RID: 502
		private bool _hasPerkRequirement;

		// Token: 0x040001F7 RID: 503
		private string _perkRequirement = "";

		// Token: 0x040001F8 RID: 504
		private string _itemRequirement = "";

		// Token: 0x040001F9 RID: 505
		private HintViewModel _itemRequirementHint;

		// Token: 0x040001FA RID: 506
		private HintViewModel _perkRequirementHint;
	}
}
