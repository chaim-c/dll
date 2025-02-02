using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000D0 RID: 208
	public class EncyclopediaTroopTreeNodeVM : ViewModel
	{
		// Token: 0x060013CA RID: 5066 RVA: 0x0004BEAC File Offset: 0x0004A0AC
		public EncyclopediaTroopTreeNodeVM(CharacterObject rootCharacter, CharacterObject activeCharacter, bool isAlternativeUpgrade, PerkObject alternativeUpgradePerk = null)
		{
			this.Branch = new MBBindingList<EncyclopediaTroopTreeNodeVM>();
			this.IsActiveUnit = (rootCharacter == activeCharacter);
			this.IsAlternativeUpgrade = isAlternativeUpgrade;
			if (alternativeUpgradePerk != null && this.IsAlternativeUpgrade)
			{
				this.AlternativeUpgradeTooltip = new BasicTooltipViewModel(delegate()
				{
					TextObject textObject = new TextObject("{=LVJKy6a8}This troop requires {PERK_NAME} ({PERK_SKILL}) perk to upgrade.", null);
					textObject.SetTextVariable("PERK_NAME", alternativeUpgradePerk.Name);
					textObject.SetTextVariable("PERK_SKILL", alternativeUpgradePerk.Skill.Name);
					return textObject.ToString();
				});
			}
			this.Unit = new EncyclopediaUnitVM(rootCharacter, this.IsActiveUnit);
			foreach (CharacterObject characterObject in rootCharacter.UpgradeTargets)
			{
				if (characterObject == rootCharacter)
				{
					Debug.FailedAssert("A character cannot be it's own upgrade target!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Items\\EncyclopediaTroopTreeNodeVM.cs", ".ctor", 36);
				}
				else if (Campaign.Current.EncyclopediaManager.GetPageOf(typeof(CharacterObject)).IsValidEncyclopediaItem(characterObject))
				{
					bool isAlternativeUpgrade2 = rootCharacter.Culture.IsBandit && !characterObject.Culture.IsBandit;
					PerkObject alternativeUpgradePerk2;
					Campaign.Current.Models.PartyTroopUpgradeModel.DoesPartyHaveRequiredPerksForUpgrade(PartyBase.MainParty, rootCharacter, characterObject, out alternativeUpgradePerk2);
					this.Branch.Add(new EncyclopediaTroopTreeNodeVM(characterObject, activeCharacter, isAlternativeUpgrade2, alternativeUpgradePerk2));
				}
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004BFD2 File Offset: 0x0004A1D2
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Branch.ApplyActionOnAllItems(delegate(EncyclopediaTroopTreeNodeVM x)
			{
				x.RefreshValues();
			});
			this.Unit.RefreshValues();
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0004C00F File Offset: 0x0004A20F
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x0004C017 File Offset: 0x0004A217
		[DataSourceProperty]
		public bool IsActiveUnit
		{
			get
			{
				return this._isActiveUnit;
			}
			set
			{
				if (value != this._isActiveUnit)
				{
					this._isActiveUnit = value;
					base.OnPropertyChangedWithValue(value, "IsActiveUnit");
				}
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0004C035 File Offset: 0x0004A235
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x0004C03D File Offset: 0x0004A23D
		[DataSourceProperty]
		public bool IsAlternativeUpgrade
		{
			get
			{
				return this._isAlternativeUpgrade;
			}
			set
			{
				if (value != this._isAlternativeUpgrade)
				{
					this._isAlternativeUpgrade = value;
					base.OnPropertyChangedWithValue(value, "IsAlternativeUpgrade");
				}
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0004C05B File Offset: 0x0004A25B
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x0004C063 File Offset: 0x0004A263
		[DataSourceProperty]
		public MBBindingList<EncyclopediaTroopTreeNodeVM> Branch
		{
			get
			{
				return this._branch;
			}
			set
			{
				if (value != this._branch)
				{
					this._branch = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaTroopTreeNodeVM>>(value, "Branch");
				}
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0004C081 File Offset: 0x0004A281
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0004C089 File Offset: 0x0004A289
		[DataSourceProperty]
		public EncyclopediaUnitVM Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if (value != this._unit)
				{
					this._unit = value;
					base.OnPropertyChangedWithValue<EncyclopediaUnitVM>(value, "Unit");
				}
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0004C0A7 File Offset: 0x0004A2A7
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0004C0AF File Offset: 0x0004A2AF
		[DataSourceProperty]
		public BasicTooltipViewModel AlternativeUpgradeTooltip
		{
			get
			{
				return this._alternativeUpgradeTooltip;
			}
			set
			{
				if (value != this._alternativeUpgradeTooltip)
				{
					this._alternativeUpgradeTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "AlternativeUpgradeTooltip");
				}
			}
		}

		// Token: 0x04000926 RID: 2342
		private MBBindingList<EncyclopediaTroopTreeNodeVM> _branch;

		// Token: 0x04000927 RID: 2343
		private EncyclopediaUnitVM _unit;

		// Token: 0x04000928 RID: 2344
		private bool _isActiveUnit;

		// Token: 0x04000929 RID: 2345
		private bool _isAlternativeUpgrade;

		// Token: 0x0400092A RID: 2346
		private BasicTooltipViewModel _alternativeUpgradeTooltip;
	}
}
