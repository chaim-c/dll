using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000BF RID: 191
	[EncyclopediaViewModel(typeof(CharacterObject))]
	public class EncyclopediaUnitPageVM : EncyclopediaContentPageVM
	{
		// Token: 0x06001311 RID: 4881 RVA: 0x0004A0AC File Offset: 0x000482AC
		public EncyclopediaUnitPageVM(EncyclopediaPageArgs args) : base(args)
		{
			this._character = (base.Obj as CharacterObject);
			this.UnitCharacter = new CharacterViewModel(CharacterViewModel.StanceTypes.OnMount);
			this.UnitCharacter.FillFrom(this._character, -1);
			this.HasErrors = this.DoesCharacterHaveCircularUpgradePaths(this._character, null);
			if (!this.HasErrors)
			{
				CharacterObject rootCharacter = CharacterHelper.FindUpgradeRootOf(this._character);
				this.Tree = new EncyclopediaTroopTreeNodeVM(rootCharacter, this._character, false, null);
			}
			this.PropertiesList = new MBBindingList<StringItemWithHintVM>();
			this.EquipmentSetSelector = new SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>(0, new Action<SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>>(this.OnEquipmentSetChange));
			base.IsBookmarked = Campaign.Current.EncyclopediaManager.ViewDataTracker.IsEncyclopediaBookmarked(this._character);
			this.RefreshValues();
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004A174 File Offset: 0x00048374
		private bool DoesCharacterHaveCircularUpgradePaths(CharacterObject baseCharacter, CharacterObject character = null)
		{
			bool result = false;
			if (character == null)
			{
				character = baseCharacter;
			}
			for (int i = 0; i < character.UpgradeTargets.Length; i++)
			{
				if (character.UpgradeTargets[i] == baseCharacter)
				{
					Debug.FailedAssert(string.Format("Circular dependency on troop upgrade paths: {0} --> {1}", character.Name, baseCharacter.Name), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Pages\\EncyclopediaUnitPageVM.cs", "DoesCharacterHaveCircularUpgradePaths", 56);
					result = true;
					break;
				}
				result = this.DoesCharacterHaveCircularUpgradePaths(baseCharacter, character.UpgradeTargets[i]);
			}
			return result;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004A1E4 File Offset: 0x000483E4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._equipmentSetTextObj = new TextObject("{=vggt7exj}Set {CURINDEX}/{COUNT}", null);
			this.PropertiesList.Clear();
			this.PropertiesList.Add(CampaignUIHelper.GetCharacterTierData(this._character, true));
			this.PropertiesList.Add(CampaignUIHelper.GetCharacterTypeData(this._character, true));
			this.EquipmentSetSelector.ItemList.Clear();
			using (IEnumerator<Equipment> enumerator = this._character.BattleEquipments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Equipment equipment = enumerator.Current;
					if (!this.EquipmentSetSelector.ItemList.Any((EncyclopediaUnitEquipmentSetSelectorItemVM x) => x.EquipmentSet.IsEquipmentEqualTo(equipment)))
					{
						this.EquipmentSetSelector.AddItem(new EncyclopediaUnitEquipmentSetSelectorItemVM(equipment, ""));
					}
				}
			}
			if (this.EquipmentSetSelector.ItemList.Count > 0)
			{
				this.EquipmentSetSelector.SelectedIndex = 0;
			}
			this._equipmentSetTextObj.SetTextVariable("CURINDEX", this.EquipmentSetSelector.SelectedIndex + 1);
			this._equipmentSetTextObj.SetTextVariable("COUNT", this.EquipmentSetSelector.ItemList.Count);
			this.EquipmentSetText = this._equipmentSetTextObj.ToString();
			this.TreeDisplayErrorText = new TextObject("{=BkDycbdq}Error while displaying the troop tree", null).ToString();
			this.Skills = new MBBindingList<EncyclopediaSkillVM>();
			foreach (SkillObject skill in TaleWorlds.CampaignSystem.Extensions.Skills.All)
			{
				if (this._character.GetSkillValue(skill) > 0)
				{
					this.Skills.Add(new EncyclopediaSkillVM(skill, this._character.GetSkillValue(skill)));
				}
			}
			this.DescriptionText = GameTexts.FindText("str_encyclopedia_unit_description", this._character.StringId).ToString();
			this.NameText = this._character.Name.ToString();
			EncyclopediaTroopTreeNodeVM tree = this.Tree;
			if (tree != null)
			{
				tree.RefreshValues();
			}
			CharacterViewModel unitCharacter = this.UnitCharacter;
			if (unitCharacter != null)
			{
				unitCharacter.RefreshValues();
			}
			base.UpdateBookmarkHintText();
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004A428 File Offset: 0x00048628
		private void OnEquipmentSetChange(SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> selector)
		{
			this.CurrentSelectedEquipmentSet = selector.SelectedItem;
			this.UnitCharacter.SetEquipment(this.CurrentSelectedEquipmentSet.EquipmentSet);
			this._equipmentSetTextObj.SetTextVariable("CURINDEX", selector.SelectedIndex + 1);
			this._equipmentSetTextObj.SetTextVariable("COUNT", selector.ItemList.Count);
			this.EquipmentSetText = this._equipmentSetTextObj.ToString();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004A49D File Offset: 0x0004869D
		public override string GetName()
		{
			return this._character.Name.ToString();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004A4B0 File Offset: 0x000486B0
		public override string GetNavigationBarURL()
		{
			return HyperlinkTexts.GetGenericHyperlinkText("Home", GameTexts.FindText("str_encyclopedia_home", null).ToString()) + " \\ " + HyperlinkTexts.GetGenericHyperlinkText("ListPage-Units", GameTexts.FindText("str_encyclopedia_troops", null).ToString()) + " \\ " + this.GetName();
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004A518 File Offset: 0x00048718
		public override void ExecuteSwitchBookmarkedState()
		{
			base.ExecuteSwitchBookmarkedState();
			if (base.IsBookmarked)
			{
				Campaign.Current.EncyclopediaManager.ViewDataTracker.AddEncyclopediaBookmarkToItem(this._character);
				return;
			}
			Campaign.Current.EncyclopediaManager.ViewDataTracker.RemoveEncyclopediaBookmarkFromItem(this._character);
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0004A568 File Offset: 0x00048768
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x0004A570 File Offset: 0x00048770
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSkillVM> Skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				if (value != this._skills)
				{
					this._skills = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSkillVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0004A58E File Offset: 0x0004878E
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x0004A596 File Offset: 0x00048796
		[DataSourceProperty]
		public MBBindingList<StringItemWithHintVM> PropertiesList
		{
			get
			{
				return this._propertiesList;
			}
			set
			{
				if (value != this._propertiesList)
				{
					this._propertiesList = value;
					base.OnPropertyChangedWithValue<MBBindingList<StringItemWithHintVM>>(value, "PropertiesList");
				}
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0004A5B4 File Offset: 0x000487B4
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x0004A5BC File Offset: 0x000487BC
		[DataSourceProperty]
		public SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> EquipmentSetSelector
		{
			get
			{
				return this._equipmentSetSelector;
			}
			set
			{
				if (value != this._equipmentSetSelector)
				{
					this._equipmentSetSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM>>(value, "EquipmentSetSelector");
				}
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004A5DA File Offset: 0x000487DA
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x0004A5E2 File Offset: 0x000487E2
		[DataSourceProperty]
		public EncyclopediaUnitEquipmentSetSelectorItemVM CurrentSelectedEquipmentSet
		{
			get
			{
				return this._currentSelectedEquipmentSet;
			}
			set
			{
				if (value != this._currentSelectedEquipmentSet)
				{
					this._currentSelectedEquipmentSet = value;
					base.OnPropertyChangedWithValue<EncyclopediaUnitEquipmentSetSelectorItemVM>(value, "CurrentSelectedEquipmentSet");
				}
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004A600 File Offset: 0x00048800
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x0004A608 File Offset: 0x00048808
		[DataSourceProperty]
		public CharacterViewModel UnitCharacter
		{
			get
			{
				return this._unitCharacter;
			}
			set
			{
				if (value != this._unitCharacter)
				{
					this._unitCharacter = value;
					base.OnPropertyChangedWithValue<CharacterViewModel>(value, "UnitCharacter");
				}
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0004A626 File Offset: 0x00048826
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x0004A62E File Offset: 0x0004882E
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

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004A651 File Offset: 0x00048851
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0004A659 File Offset: 0x00048859
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004A67C File Offset: 0x0004887C
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0004A684 File Offset: 0x00048884
		[DataSourceProperty]
		public EncyclopediaTroopTreeNodeVM Tree
		{
			get
			{
				return this._tree;
			}
			set
			{
				if (value != this._tree)
				{
					this._tree = value;
					base.OnPropertyChangedWithValue<EncyclopediaTroopTreeNodeVM>(value, "Tree");
				}
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004A6A2 File Offset: 0x000488A2
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0004A6AA File Offset: 0x000488AA
		[DataSourceProperty]
		public string TreeDisplayErrorText
		{
			get
			{
				return this._treeDisplayErrorText;
			}
			set
			{
				if (value != this._treeDisplayErrorText)
				{
					this._treeDisplayErrorText = value;
					base.OnPropertyChangedWithValue<string>(value, "TreeDisplayErrorText");
				}
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004A6CD File Offset: 0x000488CD
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0004A6D5 File Offset: 0x000488D5
		[DataSourceProperty]
		public string EquipmentSetText
		{
			get
			{
				return this._equipmentSetText;
			}
			set
			{
				if (value != this._equipmentSetText)
				{
					this._equipmentSetText = value;
					base.OnPropertyChangedWithValue<string>(value, "EquipmentSetText");
				}
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0004A6F8 File Offset: 0x000488F8
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0004A700 File Offset: 0x00048900
		[DataSourceProperty]
		public bool HasErrors
		{
			get
			{
				return this._hasErrors;
			}
			set
			{
				if (value != this._hasErrors)
				{
					this._hasErrors = value;
					base.OnPropertyChangedWithValue(value, "HasErrors");
				}
			}
		}

		// Token: 0x040008D6 RID: 2262
		private readonly CharacterObject _character;

		// Token: 0x040008D7 RID: 2263
		private TextObject _equipmentSetTextObj;

		// Token: 0x040008D8 RID: 2264
		private MBBindingList<EncyclopediaSkillVM> _skills;

		// Token: 0x040008D9 RID: 2265
		private MBBindingList<StringItemWithHintVM> _propertiesList;

		// Token: 0x040008DA RID: 2266
		private SelectorVM<EncyclopediaUnitEquipmentSetSelectorItemVM> _equipmentSetSelector;

		// Token: 0x040008DB RID: 2267
		private EncyclopediaUnitEquipmentSetSelectorItemVM _currentSelectedEquipmentSet;

		// Token: 0x040008DC RID: 2268
		private EncyclopediaTroopTreeNodeVM _tree;

		// Token: 0x040008DD RID: 2269
		private string _descriptionText;

		// Token: 0x040008DE RID: 2270
		private CharacterViewModel _unitCharacter;

		// Token: 0x040008DF RID: 2271
		private string _nameText;

		// Token: 0x040008E0 RID: 2272
		private string _treeDisplayErrorText;

		// Token: 0x040008E1 RID: 2273
		private string _equipmentSetText;

		// Token: 0x040008E2 RID: 2274
		private bool _hasErrors;
	}
}
