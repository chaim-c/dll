using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000D9 RID: 217
	public class EducationVM : ViewModel
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x0004D19C File Offset: 0x0004B39C
		public EducationVM(Hero child, Action<bool> onDone, Action<EducationCampaignBehavior.EducationCharacterProperties[]> onOptionSelect, Action<List<BasicCharacterObject>, List<Equipment>> sendPossibleCharactersAndEquipment)
		{
			this._onDone = onDone;
			this._onOptionSelect = onOptionSelect;
			this._sendPossibleCharactersAndEquipment = sendPossibleCharactersAndEquipment;
			this._child = child;
			this._educationBehavior = Campaign.Current.GetCampaignBehavior<IEducationLogic>();
			int num;
			this._educationBehavior.GetStageProperties(this._child, out num);
			this._pageCount = num + 1;
			this.GainedPropertiesController = new EducationGainedPropertiesVM(this._child, this._pageCount);
			this.Options = new MBBindingList<EducationOptionVM>();
			this.Review = new EducationReviewVM(this._pageCount);
			this.CanGoBack = true;
			this.InitWithStageIndex(0);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004D268 File Offset: 0x0004B468
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject currentPageTitleTextObj = this._currentPageTitleTextObj;
			this.StageTitleText = (((currentPageTitleTextObj != null) ? currentPageTitleTextObj.ToString() : null) ?? "");
			TextObject currentPageDescriptionTextObj = this._currentPageDescriptionTextObj;
			this.PageDescriptionText = (((currentPageDescriptionTextObj != null) ? currentPageDescriptionTextObj.ToString() : null) ?? "");
			TextObject currentPageInstructionTextObj = this._currentPageInstructionTextObj;
			this.ChooseText = (((currentPageInstructionTextObj != null) ? currentPageInstructionTextObj.ToString() : null) ?? "");
			this.Options.ApplyActionOnAllItems(delegate(EducationOptionVM o)
			{
				o.RefreshValues();
			});
			foreach (EducationOptionVM educationOptionVM in this.Options)
			{
				if (educationOptionVM.IsSelected)
				{
					this.OptionEffectText = educationOptionVM.OptionEffect;
					this.OptionDescriptionText = educationOptionVM.OptionDescription;
				}
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004D364 File Offset: 0x0004B564
		private void InitWithStageIndex(int index)
		{
			this._latestOptionId = null;
			this.CanAdvance = false;
			this._currentPageIndex = index;
			this.OptionEffectText = "";
			this.OptionDescriptionText = "";
			this.Options.Clear();
			if (index < this._pageCount - 1)
			{
				List<BasicCharacterObject> list = new List<BasicCharacterObject>();
				List<Equipment> list2 = new List<Equipment>();
				TextObject currentPageTitleTextObj;
				TextObject currentPageDescriptionTextObj;
				TextObject currentPageInstructionTextObj;
				EducationCampaignBehavior.EducationCharacterProperties[] array;
				string[] array2;
				this._educationBehavior.GetPageProperties(this._child, this._selectedOptions.Take(index).ToList<string>(), out currentPageTitleTextObj, out currentPageDescriptionTextObj, out currentPageInstructionTextObj, out array, out array2);
				this._currentPageTitleTextObj = currentPageTitleTextObj;
				this._currentPageDescriptionTextObj = currentPageDescriptionTextObj;
				this._currentPageInstructionTextObj = currentPageInstructionTextObj;
				for (int i = 0; i < array2.Length; i++)
				{
					TextObject optionText;
					TextObject optionDescription;
					TextObject optionEffect;
					ValueTuple<CharacterAttribute, int>[] optionAttributes;
					ValueTuple<SkillObject, int>[] optionSkills;
					ValueTuple<SkillObject, int>[] optionFocusPoints;
					EducationCampaignBehavior.EducationCharacterProperties[] array3;
					this._educationBehavior.GetOptionProperties(this._child, array2[i], this._selectedOptions, out optionText, out optionDescription, out optionEffect, out optionAttributes, out optionSkills, out optionFocusPoints, out array3);
					this.Options.Add(new EducationOptionVM(new Action<object>(this.OnOptionSelect), array2[i], optionText, optionDescription, optionEffect, false, optionAttributes, optionSkills, optionFocusPoints, array3));
					foreach (EducationCampaignBehavior.EducationCharacterProperties educationCharacterProperties in array3)
					{
						if (educationCharacterProperties.Character != null && !list.Contains(educationCharacterProperties.Character))
						{
							list.Add(educationCharacterProperties.Character);
						}
						if (educationCharacterProperties.Equipment != null && !list2.Contains(educationCharacterProperties.Equipment))
						{
							list2.Add(educationCharacterProperties.Equipment);
						}
					}
				}
				this.OnlyHasOneOption = (this.Options.Count == 1);
				if (this._selectedOptions.Count > index)
				{
					string item = this._selectedOptions[index];
					int num = array2.IndexOf(item);
					if (num >= 0)
					{
						Action<EducationCampaignBehavior.EducationCharacterProperties[]> onOptionSelect = this._onOptionSelect;
						if (onOptionSelect != null)
						{
							onOptionSelect(this.Options[num].CharacterProperties);
						}
						if (index == this._currentPageIndex)
						{
							this.Options[num].ExecuteAction();
							this.CanAdvance = true;
						}
					}
				}
				else
				{
					EducationCampaignBehavior.EducationCharacterProperties[] array5 = new EducationCampaignBehavior.EducationCharacterProperties[(array != null) ? array.Length : 1];
					for (int k = 0; k < ((array != null) ? array.Length : 0); k++)
					{
						array5[k] = array[k];
						if (array5[k].Character != null && !list.Contains(array5[k].Character))
						{
							list.Add(array5[k].Character);
						}
						if (array5[k].Equipment != null && !list2.Contains(array5[k].Equipment))
						{
							list2.Add(array5[k].Equipment);
						}
					}
					Action<EducationCampaignBehavior.EducationCharacterProperties[]> onOptionSelect2 = this._onOptionSelect;
					if (onOptionSelect2 != null)
					{
						onOptionSelect2(array5);
					}
				}
				if (this.OnlyHasOneOption)
				{
					this.Options[0].ExecuteAction();
				}
				this._sendPossibleCharactersAndEquipment(list, list2);
			}
			else
			{
				this._currentPageTitleTextObj = new TextObject("{=Ck9HT8fQ}Summary", null);
				this._currentPageInstructionTextObj = null;
				this._currentPageDescriptionTextObj = null;
				this.OnlyHasOneOption = false;
				this.CanAdvance = true;
			}
			TextObject currentPageTitleTextObj2 = this._currentPageTitleTextObj;
			this.StageTitleText = (((currentPageTitleTextObj2 != null) ? currentPageTitleTextObj2.ToString() : null) ?? "");
			TextObject currentPageInstructionTextObj2 = this._currentPageInstructionTextObj;
			this.ChooseText = (((currentPageInstructionTextObj2 != null) ? currentPageInstructionTextObj2.ToString() : null) ?? "");
			TextObject currentPageDescriptionTextObj2 = this._currentPageDescriptionTextObj;
			this.PageDescriptionText = (((currentPageDescriptionTextObj2 != null) ? currentPageDescriptionTextObj2.ToString() : null) ?? "");
			if (this._currentPageIndex == 0)
			{
				this.NextText = this._nextPageTextObj.ToString();
				this.PreviousText = GameTexts.FindText("str_exit", null).ToString();
			}
			else if (this._currentPageIndex == this._pageCount - 1)
			{
				this.NextText = GameTexts.FindText("str_done", null).ToString();
				this.PreviousText = this._previousPageTextObj.ToString();
			}
			else
			{
				this.NextText = this._nextPageTextObj.ToString();
				this.PreviousText = this._previousPageTextObj.ToString();
			}
			this.UpdateGainedProperties();
			this.Review.SetCurrentPage(this._currentPageIndex);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004D798 File Offset: 0x0004B998
		private void OnOptionSelect(object optionIdAsObj)
		{
			if (optionIdAsObj != this._latestOptionId)
			{
				string optionId = (string)optionIdAsObj;
				EducationOptionVM educationOptionVM = this.Options.FirstOrDefault((EducationOptionVM o) => (string)o.Identifier == optionId);
				this.Options.ApplyActionOnAllItems(delegate(EducationOptionVM o)
				{
					o.IsSelected = false;
				});
				educationOptionVM.IsSelected = true;
				string actionText = educationOptionVM.ActionText;
				if (this._currentPageIndex == this._selectedOptions.Count)
				{
					this._selectedOptions.Add(optionId);
				}
				else if (this._currentPageIndex < this._selectedOptions.Count)
				{
					this._selectedOptions[this._currentPageIndex] = optionId;
				}
				else
				{
					Debug.FailedAssert("Skipped a stage for education!!!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Education\\EducationVM.cs", "OnOptionSelect", 210);
				}
				this.OptionEffectText = educationOptionVM.OptionEffect;
				this.OptionDescriptionText = educationOptionVM.OptionDescription;
				Action<EducationCampaignBehavior.EducationCharacterProperties[]> onOptionSelect = this._onOptionSelect;
				if (onOptionSelect != null)
				{
					onOptionSelect(educationOptionVM.CharacterProperties);
				}
				this.UpdateGainedProperties();
				this.CanAdvance = true;
				this._latestOptionId = optionIdAsObj;
				this.Review.SetGainForStage(this._currentPageIndex, this.OptionEffectText);
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0004D8D8 File Offset: 0x0004BAD8
		private void UpdateGainedProperties()
		{
			this.GainedPropertiesController.UpdateWithSelections(this._selectedOptions, this._currentPageIndex);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004D8F4 File Offset: 0x0004BAF4
		public void ExecuteNextStage()
		{
			if (this._currentPageIndex + 1 < this._pageCount)
			{
				this.InitWithStageIndex(this._currentPageIndex + 1);
				return;
			}
			this._educationBehavior.Finalize(this._child, this._selectedOptions);
			Action<bool> onDone = this._onDone;
			if (onDone == null)
			{
				return;
			}
			onDone(false);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004D948 File Offset: 0x0004BB48
		public void ExecutePreviousStage()
		{
			if (this._currentPageIndex > 0)
			{
				this.InitWithStageIndex(this._currentPageIndex - 1);
				return;
			}
			if (this._currentPageIndex == 0)
			{
				Action<bool> onDone = this._onDone;
				if (onDone == null)
				{
					return;
				}
				onDone(true);
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004D97B File Offset: 0x0004BB7B
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

		// Token: 0x06001430 RID: 5168 RVA: 0x0004D9A4 File Offset: 0x0004BBA4
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004D9B3 File Offset: 0x0004BBB3
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0004D9C2 File Offset: 0x0004BBC2
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0004D9CA File Offset: 0x0004BBCA
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

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0004D9E8 File Offset: 0x0004BBE8
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
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

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0004DA0E File Offset: 0x0004BC0E
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x0004DA16 File Offset: 0x0004BC16
		[DataSourceProperty]
		public string StageTitleText
		{
			get
			{
				return this._stageTitleText;
			}
			set
			{
				if (value != this._stageTitleText)
				{
					this._stageTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "StageTitleText");
				}
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004DA39 File Offset: 0x0004BC39
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0004DA41 File Offset: 0x0004BC41
		[DataSourceProperty]
		public string ChooseText
		{
			get
			{
				return this._chooseText;
			}
			set
			{
				if (value != this._chooseText)
				{
					this._chooseText = value;
					base.OnPropertyChangedWithValue<string>(value, "ChooseText");
				}
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0004DA64 File Offset: 0x0004BC64
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		[DataSourceProperty]
		public string PageDescriptionText
		{
			get
			{
				return this._pageDescriptionText;
			}
			set
			{
				if (value != this._pageDescriptionText)
				{
					this._pageDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "PageDescriptionText");
				}
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0004DA8F File Offset: 0x0004BC8F
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0004DA97 File Offset: 0x0004BC97
		[DataSourceProperty]
		public string OptionEffectText
		{
			get
			{
				return this._optionEffectText;
			}
			set
			{
				if (value != this._optionEffectText)
				{
					this._optionEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "OptionEffectText");
				}
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004DABA File Offset: 0x0004BCBA
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0004DAC2 File Offset: 0x0004BCC2
		[DataSourceProperty]
		public string OptionDescriptionText
		{
			get
			{
				return this._optionDescriptionText;
			}
			set
			{
				if (value != this._optionDescriptionText)
				{
					this._optionDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "OptionDescriptionText");
				}
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0004DAE5 File Offset: 0x0004BCE5
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x0004DAED File Offset: 0x0004BCED
		[DataSourceProperty]
		public string NextText
		{
			get
			{
				return this._nextText;
			}
			set
			{
				if (value != this._nextText)
				{
					this._nextText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextText");
				}
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0004DB10 File Offset: 0x0004BD10
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x0004DB18 File Offset: 0x0004BD18
		[DataSourceProperty]
		public string PreviousText
		{
			get
			{
				return this._previousText;
			}
			set
			{
				if (value != this._previousText)
				{
					this._previousText = value;
					base.OnPropertyChangedWithValue<string>(value, "PreviousText");
				}
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0004DB3B File Offset: 0x0004BD3B
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0004DB43 File Offset: 0x0004BD43
		[DataSourceProperty]
		public bool CanAdvance
		{
			get
			{
				return this._canAdvance;
			}
			set
			{
				if (value != this._canAdvance)
				{
					this._canAdvance = value;
					base.OnPropertyChangedWithValue(value, "CanAdvance");
				}
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004DB61 File Offset: 0x0004BD61
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0004DB69 File Offset: 0x0004BD69
		[DataSourceProperty]
		public bool CanGoBack
		{
			get
			{
				return this._canGoBack;
			}
			set
			{
				if (value != this._canGoBack)
				{
					this._canGoBack = value;
					base.OnPropertyChangedWithValue(value, "CanGoBack");
				}
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0004DB87 File Offset: 0x0004BD87
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0004DB8F File Offset: 0x0004BD8F
		[DataSourceProperty]
		public bool OnlyHasOneOption
		{
			get
			{
				return this._onlyHasOneOption;
			}
			set
			{
				if (value != this._onlyHasOneOption)
				{
					this._onlyHasOneOption = value;
					base.OnPropertyChangedWithValue(value, "OnlyHasOneOption");
				}
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0004DBAD File Offset: 0x0004BDAD
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x0004DBB5 File Offset: 0x0004BDB5
		[DataSourceProperty]
		public MBBindingList<EducationOptionVM> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != this._options)
				{
					this._options = value;
					base.OnPropertyChangedWithValue<MBBindingList<EducationOptionVM>>(value, "Options");
				}
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0004DBD3 File Offset: 0x0004BDD3
		// (set) Token: 0x0600144D RID: 5197 RVA: 0x0004DBDB File Offset: 0x0004BDDB
		[DataSourceProperty]
		public EducationGainedPropertiesVM GainedPropertiesController
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
					base.OnPropertyChangedWithValue<EducationGainedPropertiesVM>(value, "GainedPropertiesController");
				}
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0004DBF9 File Offset: 0x0004BDF9
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x0004DC01 File Offset: 0x0004BE01
		[DataSourceProperty]
		public EducationReviewVM Review
		{
			get
			{
				return this._review;
			}
			set
			{
				if (value != this._review)
				{
					this._review = value;
					base.OnPropertyChangedWithValue<EducationReviewVM>(value, "Review");
				}
			}
		}

		// Token: 0x04000951 RID: 2385
		private readonly Action<bool> _onDone;

		// Token: 0x04000952 RID: 2386
		private readonly Action<EducationCampaignBehavior.EducationCharacterProperties[]> _onOptionSelect;

		// Token: 0x04000953 RID: 2387
		private readonly Action<List<BasicCharacterObject>, List<Equipment>> _sendPossibleCharactersAndEquipment;

		// Token: 0x04000954 RID: 2388
		private readonly IEducationLogic _educationBehavior;

		// Token: 0x04000955 RID: 2389
		private readonly Hero _child;

		// Token: 0x04000956 RID: 2390
		private readonly TextObject _nextPageTextObj = new TextObject("{=Rvr1bcu8}Next", null);

		// Token: 0x04000957 RID: 2391
		private readonly TextObject _previousPageTextObj = new TextObject("{=WXAaWZVf}Previous", null);

		// Token: 0x04000958 RID: 2392
		private readonly int _pageCount;

		// Token: 0x04000959 RID: 2393
		private readonly List<string> _selectedOptions = new List<string>();

		// Token: 0x0400095A RID: 2394
		private TextObject _currentPageTitleTextObj;

		// Token: 0x0400095B RID: 2395
		private TextObject _currentPageDescriptionTextObj;

		// Token: 0x0400095C RID: 2396
		private TextObject _currentPageInstructionTextObj;

		// Token: 0x0400095D RID: 2397
		private object _latestOptionId;

		// Token: 0x0400095E RID: 2398
		private int _currentPageIndex;

		// Token: 0x0400095F RID: 2399
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000960 RID: 2400
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000961 RID: 2401
		private string _stageTitleText;

		// Token: 0x04000962 RID: 2402
		private string _chooseText;

		// Token: 0x04000963 RID: 2403
		private string _pageDescriptionText;

		// Token: 0x04000964 RID: 2404
		private string _optionEffectText;

		// Token: 0x04000965 RID: 2405
		private string _optionDescriptionText;

		// Token: 0x04000966 RID: 2406
		private string _nextText;

		// Token: 0x04000967 RID: 2407
		private string _previousText;

		// Token: 0x04000968 RID: 2408
		private bool _canAdvance;

		// Token: 0x04000969 RID: 2409
		private bool _canGoBack;

		// Token: 0x0400096A RID: 2410
		private bool _onlyHasOneOption;

		// Token: 0x0400096B RID: 2411
		private MBBindingList<EducationOptionVM> _options;

		// Token: 0x0400096C RID: 2412
		private EducationGainedPropertiesVM _gainedPropertiesController;

		// Token: 0x0400096D RID: 2413
		private EducationReviewVM _review;
	}
}
