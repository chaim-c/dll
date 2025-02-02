using System;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000125 RID: 293
	public class PerkVM : ViewModel
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00067A92 File Offset: 0x00065C92
		private bool _hasAlternativeAndSelected
		{
			get
			{
				return this.AlternativeType != 0 && this._getIsPerkSelected(this.Perk.AlternativePerk);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00067AB4 File Offset: 0x00065CB4
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x00067ABC File Offset: 0x00065CBC
		public PerkVM.PerkStates CurrentState
		{
			get
			{
				return this._currentState;
			}
			private set
			{
				if (value != this._currentState)
				{
					this._currentState = value;
					this.PerkState = (int)value;
				}
			}
		}

		// Token: 0x170009DE RID: 2526
		// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x00067AD5 File Offset: 0x00065CD5
		public bool IsInSelection
		{
			set
			{
				if (value != this._isInSelection)
				{
					this._isInSelection = value;
					this.RefreshState();
					if (!this._isInSelection)
					{
						this._onSelectionOver(this);
					}
				}
			}
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00067B04 File Offset: 0x00065D04
		public PerkVM(PerkObject perk, bool isAvailable, PerkVM.PerkAlternativeType alternativeType, Action<PerkVM> onStartSelection, Action<PerkVM> onSelectionOver, Func<PerkObject, bool> getIsPerkSelected, Func<PerkObject, bool> getIsPreviousPerkSelected)
		{
			PerkVM <>4__this = this;
			this.AlternativeType = (int)alternativeType;
			this.Perk = perk;
			this._onStartSelection = onStartSelection;
			this._onSelectionOver = onSelectionOver;
			this._getIsPerkSelected = getIsPerkSelected;
			this._getIsPreviousPerkSelected = getIsPreviousPerkSelected;
			this._isAvailable = isAvailable;
			this.PerkId = "SPPerks\\" + perk.StringId;
			this.Level = (int)perk.RequiredSkillValue;
			this.LevelText = ((int)perk.RequiredSkillValue).ToString();
			this.Hint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPerkEffectText(perk, <>4__this._getIsPerkSelected(<>4__this.Perk)));
			this._perkConceptObj = Concept.All.SingleOrDefault((Concept c) => c.StringId == "str_game_objects_perks");
			this.RefreshState();
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00067C08 File Offset: 0x00065E08
		public void RefreshState()
		{
			bool flag = this._getIsPerkSelected(this.Perk);
			if (!this._isAvailable)
			{
				this.CurrentState = PerkVM.PerkStates.NotEarned;
				return;
			}
			if (this._isInSelection)
			{
				this.CurrentState = PerkVM.PerkStates.InSelection;
				return;
			}
			if (flag)
			{
				this.CurrentState = PerkVM.PerkStates.EarnedAndActive;
				return;
			}
			if (this.Perk.AlternativePerk != null && this._getIsPerkSelected(this.Perk.AlternativePerk))
			{
				this.CurrentState = PerkVM.PerkStates.EarnedAndNotActive;
				return;
			}
			if (this._getIsPreviousPerkSelected(this.Perk))
			{
				this.CurrentState = PerkVM.PerkStates.EarnedButNotSelected;
				return;
			}
			this.CurrentState = PerkVM.PerkStates.EarnedPreviousPerkNotSelected;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00067CA1 File Offset: 0x00065EA1
		public void ExecuteShowPerkConcept()
		{
			if (this._perkConceptObj != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._perkConceptObj.EncyclopediaLink);
				return;
			}
			Debug.FailedAssert("Couldn't find Perks encyclopedia page", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CharacterDeveloper\\PerkVM.cs", "ExecuteShowPerkConcept", 151);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00067CE0 File Offset: 0x00065EE0
		public void ExecuteStartSelection()
		{
			if (this._isAvailable && !this._getIsPerkSelected(this.Perk) && !this._hasAlternativeAndSelected && this._getIsPreviousPerkSelected(this.Perk))
			{
				this._onStartSelection(this);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00067D2F File Offset: 0x00065F2F
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x00067D37 File Offset: 0x00065F37
		[DataSourceProperty]
		public bool IsTutorialHighlightEnabled
		{
			get
			{
				return this._isTutorialHighlightEnabled;
			}
			set
			{
				if (value != this._isTutorialHighlightEnabled)
				{
					this._isTutorialHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTutorialHighlightEnabled");
				}
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00067D55 File Offset: 0x00065F55
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x00067D5D File Offset: 0x00065F5D
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00067D7B File Offset: 0x00065F7B
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x00067D83 File Offset: 0x00065F83
		[DataSourceProperty]
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value != this._level)
				{
					this._level = value;
					base.OnPropertyChangedWithValue(value, "Level");
				}
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00067DA1 File Offset: 0x00065FA1
		// (set) Token: 0x06001CCE RID: 7374 RVA: 0x00067DA9 File Offset: 0x00065FA9
		[DataSourceProperty]
		public int PerkState
		{
			get
			{
				return this._perkState;
			}
			set
			{
				if (value != this._perkState)
				{
					this._perkState = value;
					base.OnPropertyChangedWithValue(value, "PerkState");
				}
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00067DC7 File Offset: 0x00065FC7
		// (set) Token: 0x06001CD0 RID: 7376 RVA: 0x00067DCF File Offset: 0x00065FCF
		[DataSourceProperty]
		public int AlternativeType
		{
			get
			{
				return this._alternativeType;
			}
			set
			{
				if (value != this._alternativeType)
				{
					this._alternativeType = value;
					base.OnPropertyChangedWithValue(value, "AlternativeType");
				}
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00067DED File Offset: 0x00065FED
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x00067DF5 File Offset: 0x00065FF5
		[DataSourceProperty]
		public string LevelText
		{
			get
			{
				return this._levelText;
			}
			set
			{
				if (value != this._levelText)
				{
					this._levelText = value;
					base.OnPropertyChangedWithValue<string>(value, "LevelText");
				}
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00067E18 File Offset: 0x00066018
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x00067E20 File Offset: 0x00066020
		[DataSourceProperty]
		public string BackgroundImage
		{
			get
			{
				return this._backgroundImage;
			}
			set
			{
				if (value != this._backgroundImage)
				{
					this._backgroundImage = value;
					base.OnPropertyChangedWithValue<string>(value, "BackgroundImage");
				}
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00067E43 File Offset: 0x00066043
		// (set) Token: 0x06001CD6 RID: 7382 RVA: 0x00067E4B File Offset: 0x0006604B
		[DataSourceProperty]
		public string PerkId
		{
			get
			{
				return this._perkId;
			}
			set
			{
				if (value != this._perkId)
				{
					this._perkId = value;
					base.OnPropertyChangedWithValue<string>(value, "PerkId");
				}
			}
		}

		// Token: 0x04000D91 RID: 3473
		public readonly PerkObject Perk;

		// Token: 0x04000D92 RID: 3474
		private readonly Action<PerkVM> _onStartSelection;

		// Token: 0x04000D93 RID: 3475
		private readonly Action<PerkVM> _onSelectionOver;

		// Token: 0x04000D94 RID: 3476
		private readonly Func<PerkObject, bool> _getIsPerkSelected;

		// Token: 0x04000D95 RID: 3477
		private readonly Func<PerkObject, bool> _getIsPreviousPerkSelected;

		// Token: 0x04000D96 RID: 3478
		private readonly bool _isAvailable;

		// Token: 0x04000D97 RID: 3479
		private readonly Concept _perkConceptObj;

		// Token: 0x04000D98 RID: 3480
		private bool _isInSelection;

		// Token: 0x04000D99 RID: 3481
		private PerkVM.PerkStates _currentState = PerkVM.PerkStates.None;

		// Token: 0x04000D9A RID: 3482
		private string _levelText;

		// Token: 0x04000D9B RID: 3483
		private string _perkId;

		// Token: 0x04000D9C RID: 3484
		private string _backgroundImage;

		// Token: 0x04000D9D RID: 3485
		private BasicTooltipViewModel _hint;

		// Token: 0x04000D9E RID: 3486
		private int _level;

		// Token: 0x04000D9F RID: 3487
		private int _alternativeType;

		// Token: 0x04000DA0 RID: 3488
		private int _perkState = -1;

		// Token: 0x04000DA1 RID: 3489
		private bool _isTutorialHighlightEnabled;

		// Token: 0x02000288 RID: 648
		public enum PerkStates
		{
			// Token: 0x04001202 RID: 4610
			None = -1,
			// Token: 0x04001203 RID: 4611
			NotEarned,
			// Token: 0x04001204 RID: 4612
			EarnedButNotSelected,
			// Token: 0x04001205 RID: 4613
			InSelection,
			// Token: 0x04001206 RID: 4614
			EarnedAndActive,
			// Token: 0x04001207 RID: 4615
			EarnedAndNotActive,
			// Token: 0x04001208 RID: 4616
			EarnedPreviousPerkNotSelected
		}

		// Token: 0x02000289 RID: 649
		public enum PerkAlternativeType
		{
			// Token: 0x0400120A RID: 4618
			NoAlternative,
			// Token: 0x0400120B RID: 4619
			FirstAlternative,
			// Token: 0x0400120C RID: 4620
			SecondAlternative
		}
	}
}
