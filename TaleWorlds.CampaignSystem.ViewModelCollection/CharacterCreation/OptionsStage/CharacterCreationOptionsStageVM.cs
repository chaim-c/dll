using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation.OptionsStage
{
	// Token: 0x02000139 RID: 313
	public class CharacterCreationOptionsStageVM : CharacterCreationStageBaseVM
	{
		// Token: 0x06001E1A RID: 7706 RVA: 0x0006B9D4 File Offset: 0x00069BD4
		public CharacterCreationOptionsStageVM(CharacterCreation characterCreation, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex) : base(characterCreation, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText, currentStageIndex, totalStagesCount, furthestIndex, goToIndex)
		{
			base.Title = GameTexts.FindText("str_difficulty", null).ToString();
			base.Description = GameTexts.FindText("str_determine_difficulty", null).ToString();
			MBBindingList<CampaignOptionItemVM> mbbindingList = new MBBindingList<CampaignOptionItemVM>();
			List<ICampaignOptionData> characterCreationCampaignOptions = CampaignOptionsManager.GetCharacterCreationCampaignOptions();
			for (int i = 0; i < characterCreationCampaignOptions.Count; i++)
			{
				mbbindingList.Add(new CampaignOptionItemVM(characterCreationCampaignOptions[i]));
			}
			this.OptionsController = new CampaignOptionsControllerVM(mbbindingList);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0006BA5D File Offset: 0x00069C5D
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.OptionsController.RefreshValues();
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0006BA70 File Offset: 0x00069C70
		private void OnOptionChange(string identifier)
		{
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0006BA72 File Offset: 0x00069C72
		public override bool CanAdvanceToNextStage()
		{
			return true;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0006BA75 File Offset: 0x00069C75
		public override void OnNextStage()
		{
			this._affirmativeAction();
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0006BA82 File Offset: 0x00069C82
		public override void OnPreviousStage()
		{
			this._negativeAction();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0006BA8F File Offset: 0x00069C8F
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

		// Token: 0x06001E21 RID: 7713 RVA: 0x0006BAB8 File Offset: 0x00069CB8
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0006BAC7 File Offset: 0x00069CC7
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0006BAD6 File Offset: 0x00069CD6
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0006BADE File Offset: 0x00069CDE
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0006BAFC File Offset: 0x00069CFC
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x0006BB04 File Offset: 0x00069D04
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

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x0006BB22 File Offset: 0x00069D22
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x0006BB2A File Offset: 0x00069D2A
		[DataSourceProperty]
		public CampaignOptionsControllerVM OptionsController
		{
			get
			{
				return this._optionsController;
			}
			set
			{
				if (value != this._optionsController)
				{
					this._optionsController = value;
					base.OnPropertyChangedWithValue<CampaignOptionsControllerVM>(value, "OptionsController");
				}
			}
		}

		// Token: 0x04000E31 RID: 3633
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000E32 RID: 3634
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000E33 RID: 3635
		private CampaignOptionsControllerVM _optionsController;
	}
}
