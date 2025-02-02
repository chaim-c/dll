using System;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x0200012E RID: 302
	public class CharacterCreationCultureStageVM : CharacterCreationStageBaseVM
	{
		// Token: 0x06001D75 RID: 7541 RVA: 0x00069834 File Offset: 0x00067A34
		public CharacterCreationCultureStageVM(CharacterCreation characterCreation, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex, Action<CultureObject> onCultureSelected) : base(characterCreation, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText, currentStageIndex, totalStagesCount, furthestIndex, goToIndex)
		{
			this._onCultureSelected = onCultureSelected;
			CharacterCreationContentBase currentContent = (GameStateManager.Current.ActiveState as CharacterCreationState).CurrentCharacterCreationContent;
			this.Cultures = new MBBindingList<CharacterCreationCultureVM>();
			base.Title = GameTexts.FindText("str_culture", null).ToString();
			base.Description = new TextObject("{=fz2kQjFS}Choose your character's culture:", null).ToString();
			base.SelectionText = new TextObject("{=MaHMOzL2}Character Culture", null).ToString();
			foreach (CultureObject culture in currentContent.GetCultures())
			{
				CharacterCreationCultureVM item = new CharacterCreationCultureVM(culture, new Action<CharacterCreationCultureVM>(this.OnCultureSelection));
				this.Cultures.Add(item);
			}
			this.SortCultureList(this.Cultures);
			if (currentContent.GetSelectedCulture() != null)
			{
				CharacterCreationCultureVM characterCreationCultureVM = this.Cultures.FirstOrDefault((CharacterCreationCultureVM c) => c.Culture == currentContent.GetSelectedCulture());
				if (characterCreationCultureVM != null)
				{
					this.OnCultureSelection(characterCreationCultureVM);
				}
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00069964 File Offset: 0x00067B64
		private void SortCultureList(MBBindingList<CharacterCreationCultureVM> listToWorkOn)
		{
			int swapFromIndex = listToWorkOn.IndexOf(listToWorkOn.Single((CharacterCreationCultureVM i) => i.CultureID.Contains("vlan")));
			this.Swap(listToWorkOn, swapFromIndex, 0);
			int swapFromIndex2 = listToWorkOn.IndexOf(listToWorkOn.Single((CharacterCreationCultureVM i) => i.CultureID.Contains("stur")));
			this.Swap(listToWorkOn, swapFromIndex2, 1);
			int swapFromIndex3 = listToWorkOn.IndexOf(listToWorkOn.Single((CharacterCreationCultureVM i) => i.CultureID.Contains("empi")));
			this.Swap(listToWorkOn, swapFromIndex3, 2);
			int swapFromIndex4 = listToWorkOn.IndexOf(listToWorkOn.Single((CharacterCreationCultureVM i) => i.CultureID.Contains("aser")));
			this.Swap(listToWorkOn, swapFromIndex4, 3);
			int swapFromIndex5 = listToWorkOn.IndexOf(listToWorkOn.Single((CharacterCreationCultureVM i) => i.CultureID.Contains("khuz")));
			this.Swap(listToWorkOn, swapFromIndex5, 4);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00069A7C File Offset: 0x00067C7C
		public void OnCultureSelection(CharacterCreationCultureVM selectedCulture)
		{
			this.InitializePlayersFaceKeyAccordingToCultureSelection(selectedCulture);
			foreach (CharacterCreationCultureVM characterCreationCultureVM in from c in this.Cultures
			where c.IsSelected
			select c)
			{
				characterCreationCultureVM.IsSelected = false;
			}
			selectedCulture.IsSelected = true;
			this.CurrentSelectedCulture = selectedCulture;
			base.AnyItemSelected = true;
			(GameStateManager.Current.ActiveState as CharacterCreationState).CurrentCharacterCreationContent.SetSelectedCulture(selectedCulture.Culture, this._characterCreation);
			base.OnPropertyChanged("CanAdvance");
			Action<CultureObject> onCultureSelected = this._onCultureSelected;
			if (onCultureSelected == null)
			{
				return;
			}
			onCultureSelected(selectedCulture.Culture);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00069B50 File Offset: 0x00067D50
		private void InitializePlayersFaceKeyAccordingToCultureSelection(CharacterCreationCultureVM selectedCulture)
		{
			string text = "<BodyProperties version='4' age='25.84' weight='0.5000' build='0.5000'  key='000BAC088000100DB976648E6774B835537D86629511323BDCB177278A84F667017776140748B49500000000000000000000000000000000000000003EFC5002'/>";
			string text2 = "<BodyProperties version='4' age='25.84' weight='0.5000' build='0.5000'  key='000500000000000D797664884754DCBAA35E866295A0967774414A498C8336860F7776F20BA7B7A500000000000000000000000000000000000000003CFC2002'/>";
			string text3 = "<BodyProperties version='4' age='25.84' weight='0.5000' build='0.5000'  key='001CB80CC000300D7C7664876753888A7577866254C69643C4B647398C95A0370077760307A7497300000000000000000000000000000000000000003AF47002'/>";
			string text4 = "<BodyProperties version='4' age='25.84' weight='0.5000' build='0.5000'  key='0028C80FC000100DBA756445533377873CD1833B3101B44A21C3C5347CA32C260F7776F20BBC35E8000000000000000000000000000000000000000042F41002'/>";
			string text5 = "<BodyProperties version='4' age='25.84' weight='0.5000' build='0.5000'   key='0016F80E4000200EB8708BD6CDC85229D3698B3ABDFE344CD22D3DD5388988680F7776F20B96723B00000000000000000000000000000000000000003EF41002'/>";
			string text6 = "<BodyProperties version='4' age='25.84' weight='0.5000' build='0.5000'  key='000000058000200D79766434475CDCBAC34E866255A096777441DA49838BF6A50F7776F20BA7B7A500000000000000000000000000000000000000003CFC0002'/>";
			string keyValue;
			if (selectedCulture.Culture.StringId == "aserai")
			{
				keyValue = text4;
			}
			else if (selectedCulture.Culture.StringId == "khuzait")
			{
				keyValue = text5;
			}
			else if (selectedCulture.Culture.StringId == "vlandia")
			{
				keyValue = text;
			}
			else if (selectedCulture.Culture.StringId == "sturgia")
			{
				keyValue = text2;
			}
			else if (selectedCulture.Culture.StringId == "battania")
			{
				keyValue = text6;
			}
			else if (selectedCulture.Culture.StringId == "empire")
			{
				keyValue = text3;
			}
			else
			{
				keyValue = text3;
			}
			BodyProperties properties;
			if (BodyProperties.FromString(keyValue, out properties))
			{
				CharacterObject.PlayerCharacter.UpdatePlayerCharacterBodyProperties(properties, CharacterObject.PlayerCharacter.Race, CharacterObject.PlayerCharacter.IsFemale);
			}
			CharacterObject.PlayerCharacter.Culture = selectedCulture.Culture;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00069C78 File Offset: 0x00067E78
		private void Swap(MBBindingList<CharacterCreationCultureVM> listToWorkOn, int swapFromIndex, int swapToIndex)
		{
			if (swapFromIndex != swapToIndex)
			{
				CharacterCreationCultureVM value = listToWorkOn[swapToIndex];
				listToWorkOn[swapToIndex] = listToWorkOn[swapFromIndex];
				listToWorkOn[swapFromIndex] = value;
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00069CA7 File Offset: 0x00067EA7
		public override void OnNextStage()
		{
			this._affirmativeAction();
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00069CB4 File Offset: 0x00067EB4
		public override void OnPreviousStage()
		{
			this._negativeAction();
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00069CC1 File Offset: 0x00067EC1
		public override bool CanAdvanceToNextStage()
		{
			return this.Cultures.Any((CharacterCreationCultureVM s) => s.IsSelected);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00069CED File Offset: 0x00067EED
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey == null)
			{
				return;
			}
			doneInputKey.OnFinalize();
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00069D16 File Offset: 0x00067F16
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00069D25 File Offset: 0x00067F25
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00069D34 File Offset: 0x00067F34
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x00069D3C File Offset: 0x00067F3C
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00069D5A File Offset: 0x00067F5A
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x00069D62 File Offset: 0x00067F62
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x00069D80 File Offset: 0x00067F80
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x00069D88 File Offset: 0x00067F88
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x00069DA6 File Offset: 0x00067FA6
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x00069DAE File Offset: 0x00067FAE
		[DataSourceProperty]
		public MBBindingList<CharacterCreationCultureVM> Cultures
		{
			get
			{
				return this._cultures;
			}
			set
			{
				if (value != this._cultures)
				{
					this._cultures = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationCultureVM>>(value, "Cultures");
				}
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00069DCC File Offset: 0x00067FCC
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x00069DD4 File Offset: 0x00067FD4
		[DataSourceProperty]
		public CharacterCreationCultureVM CurrentSelectedCulture
		{
			get
			{
				return this._currentSelectedCulture;
			}
			set
			{
				if (value != this._currentSelectedCulture)
				{
					this._currentSelectedCulture = value;
					base.OnPropertyChangedWithValue<CharacterCreationCultureVM>(value, "CurrentSelectedCulture");
				}
			}
		}

		// Token: 0x04000DE6 RID: 3558
		private Action<CultureObject> _onCultureSelected;

		// Token: 0x04000DE7 RID: 3559
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000DE8 RID: 3560
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000DE9 RID: 3561
		private bool _isActive;

		// Token: 0x04000DEA RID: 3562
		private MBBindingList<CharacterCreationCultureVM> _cultures;

		// Token: 0x04000DEB RID: 3563
		private CharacterCreationCultureVM _currentSelectedCulture;
	}
}
