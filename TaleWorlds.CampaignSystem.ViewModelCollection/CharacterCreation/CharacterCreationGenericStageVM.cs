using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000134 RID: 308
	public class CharacterCreationGenericStageVM : CharacterCreationStageBaseVM
	{
		// Token: 0x06001DC4 RID: 7620 RVA: 0x0006ABE0 File Offset: 0x00068DE0
		public CharacterCreationGenericStageVM(CharacterCreation characterCreationMenu, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, int stageIndex, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex) : base(characterCreationMenu, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText, currentStageIndex, totalStagesCount, furthestIndex, goToIndex)
		{
			this._stageIndex = stageIndex;
			this.SelectionList = new MBBindingList<CharacterCreationOptionVM>();
			this._characterCreation.OnInit(stageIndex);
			base.Title = this._characterCreation.GetCurrentMenuTitle(stageIndex).ToString();
			base.Description = this._characterCreation.GetCurrentMenuText(stageIndex).ToString();
			GameTexts.SetVariable("SELECTION", base.Title);
			base.SelectionText = GameTexts.FindText("str_char_creation_generic_selection", null).ToString();
			foreach (CharacterCreationOption characterCreationOption in this._characterCreation.GetCurrentMenuOptions(stageIndex))
			{
				CharacterCreationOptionVM item = new CharacterCreationOptionVM(new Action<object>(this.ApplySelection), characterCreationOption.Text.ToString(), characterCreationOption);
				this.SelectionList.Add(item);
			}
			this.RefreshSelectedOptions();
			this.GainedPropertiesController = new CharacterCreationGainedPropertiesVM(this._characterCreation, this._stageIndex);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0006AD00 File Offset: 0x00068F00
		public void RefreshSelectedOptions()
		{
			this._isRefreshing = true;
			IEnumerable<int> selectedOptions = this._characterCreation.GetSelectedOptions(this._stageIndex);
			foreach (CharacterCreationOptionVM characterCreationOptionVM in this.SelectionList)
			{
				CharacterCreationOption characterCreationOption = (CharacterCreationOption)characterCreationOptionVM.Identifier;
				characterCreationOptionVM.IsSelected = selectedOptions.Contains(characterCreationOption.Id);
				if (characterCreationOptionVM.IsSelected)
				{
					this.PositiveEffectText = characterCreationOption.PositiveEffectText.ToString();
					this.DescriptionText = characterCreationOption.DescriptionText.ToString();
					base.AnyItemSelected = true;
					this._selectedOption = characterCreationOption;
					this._characterCreation.RunConsequence(this._selectedOption, this._stageIndex, false);
				}
			}
			this._isRefreshing = false;
			base.OnPropertyChanged("CanAdvance");
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0006ADE0 File Offset: 0x00068FE0
		public void ApplySelection(object optionObject)
		{
			CharacterCreationOption characterCreationOption = optionObject as CharacterCreationOption;
			if (characterCreationOption == null || this._isRefreshing || this._selectedOption == characterCreationOption)
			{
				return;
			}
			this._selectedOption = characterCreationOption;
			this._characterCreation.RunConsequence(this._selectedOption, this._stageIndex, false);
			this.RefreshSelectedOptions();
			Action onOptionSelection = this.OnOptionSelection;
			if (onOptionSelection != null)
			{
				onOptionSelection();
			}
			base.AnyItemSelected = true;
			base.OnPropertyChanged("CanAdvance");
			this.GainedPropertiesController.UpdateValues();
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0006AE5C File Offset: 0x0006905C
		public override void OnNextStage()
		{
			this._affirmativeAction();
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0006AE69 File Offset: 0x00069069
		public override void OnPreviousStage()
		{
			this._negativeAction();
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0006AE76 File Offset: 0x00069076
		public override bool CanAdvanceToNextStage()
		{
			if (this.SelectionList.Count != 0)
			{
				return this.SelectionList.Any((CharacterCreationOptionVM s) => s.IsSelected);
			}
			return true;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0006AEB1 File Offset: 0x000690B1
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

		// Token: 0x06001DCB RID: 7627 RVA: 0x0006AEDA File Offset: 0x000690DA
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0006AEE9 File Offset: 0x000690E9
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0006AEF8 File Offset: 0x000690F8
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0006AF00 File Offset: 0x00069100
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

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0006AF1E File Offset: 0x0006911E
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0006AF26 File Offset: 0x00069126
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

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0006AF44 File Offset: 0x00069144
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0006AF4C File Offset: 0x0006914C
		[DataSourceProperty]
		public MBBindingList<CharacterCreationOptionVM> SelectionList
		{
			get
			{
				return this._selectionList;
			}
			set
			{
				if (value != this._selectionList)
				{
					this._selectionList = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationOptionVM>>(value, "SelectionList");
				}
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0006AF6A File Offset: 0x0006916A
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0006AF72 File Offset: 0x00069172
		[DataSourceProperty]
		public CharacterCreationGainedPropertiesVM GainedPropertiesController
		{
			get
			{
				return this._gainedPropertiesController;
			}
			set
			{
				if (value != this._gainedPropertiesController)
				{
					this._gainedPropertiesController = value;
					base.OnPropertyChangedWithValue<CharacterCreationGainedPropertiesVM>(value, "GainedPropertiesController");
				}
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0006AF90 File Offset: 0x00069190
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0006AF98 File Offset: 0x00069198
		[DataSourceProperty]
		public string PositiveEffectText
		{
			get
			{
				return this._positiveEffectText;
			}
			set
			{
				if (value != this._positiveEffectText)
				{
					this._positiveEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "PositiveEffectText");
				}
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0006AFBB File Offset: 0x000691BB
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0006AFC3 File Offset: 0x000691C3
		[DataSourceProperty]
		public string NegativeEffectText
		{
			get
			{
				return this._negativeEffectText;
			}
			set
			{
				if (value != this._negativeEffectText)
				{
					this._negativeEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "NegativeEffectText");
				}
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0006AFE6 File Offset: 0x000691E6
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0006AFEE File Offset: 0x000691EE
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

		// Token: 0x04000E09 RID: 3593
		private readonly int _stageIndex;

		// Token: 0x04000E0A RID: 3594
		public Action OnOptionSelection;

		// Token: 0x04000E0B RID: 3595
		private CharacterCreationOption _selectedOption;

		// Token: 0x04000E0C RID: 3596
		private bool _isRefreshing;

		// Token: 0x04000E0D RID: 3597
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000E0E RID: 3598
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000E0F RID: 3599
		private MBBindingList<CharacterCreationOptionVM> _selectionList;

		// Token: 0x04000E10 RID: 3600
		private CharacterCreationGainedPropertiesVM _gainedPropertiesController;

		// Token: 0x04000E11 RID: 3601
		private string _positiveEffectText;

		// Token: 0x04000E12 RID: 3602
		private string _negativeEffectText;

		// Token: 0x04000E13 RID: 3603
		private string _descriptionText;
	}
}
