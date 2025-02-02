using System;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000138 RID: 312
	public abstract class CharacterCreationStageBaseVM : ViewModel
	{
		// Token: 0x06001E04 RID: 7684 RVA: 0x0006B7FC File Offset: 0x000699FC
		protected CharacterCreationStageBaseVM(CharacterCreation characterCreation, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex)
		{
			this._characterCreation = characterCreation;
			this._goToIndex = goToIndex;
			this._affirmativeAction = affirmativeAction;
			this._negativeAction = negativeAction;
			this._affirmativeActionText = affirmativeActionText;
			this._negativeActionText = negativeActionText;
			this.TotalStageCount = totalStagesCount;
			this.CurrentStageIndex = currentStageIndex;
			this.FurthestIndex = furthestIndex;
		}

		// Token: 0x06001E05 RID: 7685
		public abstract void OnNextStage();

		// Token: 0x06001E06 RID: 7686
		public abstract void OnPreviousStage();

		// Token: 0x06001E07 RID: 7687
		public abstract bool CanAdvanceToNextStage();

		// Token: 0x06001E08 RID: 7688 RVA: 0x0006B88A File Offset: 0x00069A8A
		public virtual void ExecuteGoToIndex(int index)
		{
			this._goToIndex(index);
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0006B898 File Offset: 0x00069A98
		public bool CanAdvance
		{
			get
			{
				return this.CanAdvanceToNextStage();
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0006B8A0 File Offset: 0x00069AA0
		public string NextStageText
		{
			get
			{
				return this._affirmativeActionText.ToString();
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0006B8AD File Offset: 0x00069AAD
		public string PreviousStageText
		{
			get
			{
				return this._negativeActionText.ToString();
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0006B8BA File Offset: 0x00069ABA
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x0006B8C2 File Offset: 0x00069AC2
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0006B8E5 File Offset: 0x00069AE5
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0006B8ED File Offset: 0x00069AED
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0006B910 File Offset: 0x00069B10
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x0006B918 File Offset: 0x00069B18
		[DataSourceProperty]
		public string SelectionText
		{
			get
			{
				return this._selectionText;
			}
			set
			{
				if (value != this._selectionText)
				{
					this._selectionText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectionText");
				}
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0006B93B File Offset: 0x00069B3B
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x0006B943 File Offset: 0x00069B43
		[DataSourceProperty]
		public int TotalStageCount
		{
			get
			{
				return this._totalStageCount;
			}
			set
			{
				if (value != this._totalStageCount)
				{
					this._totalStageCount = value;
					base.OnPropertyChangedWithValue(value, "TotalStageCount");
				}
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x0006B961 File Offset: 0x00069B61
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x0006B969 File Offset: 0x00069B69
		[DataSourceProperty]
		public int FurthestIndex
		{
			get
			{
				return this._furthestIndex;
			}
			set
			{
				if (value != this._furthestIndex)
				{
					this._furthestIndex = value;
					base.OnPropertyChangedWithValue(value, "FurthestIndex");
				}
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0006B987 File Offset: 0x00069B87
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x0006B98F File Offset: 0x00069B8F
		[DataSourceProperty]
		public int CurrentStageIndex
		{
			get
			{
				return this._currentStageIndex;
			}
			set
			{
				if (value != this._currentStageIndex)
				{
					this._currentStageIndex = value;
					base.OnPropertyChangedWithValue(value, "CurrentStageIndex");
				}
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0006B9AD File Offset: 0x00069BAD
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0006B9B5 File Offset: 0x00069BB5
		[DataSourceProperty]
		public bool AnyItemSelected
		{
			get
			{
				return this._anyItemSelected;
			}
			set
			{
				if (value != this._anyItemSelected)
				{
					this._anyItemSelected = value;
					base.OnPropertyChangedWithValue(value, "AnyItemSelected");
				}
			}
		}

		// Token: 0x04000E24 RID: 3620
		protected readonly CharacterCreation _characterCreation;

		// Token: 0x04000E25 RID: 3621
		protected readonly Action _affirmativeAction;

		// Token: 0x04000E26 RID: 3622
		protected readonly Action _negativeAction;

		// Token: 0x04000E27 RID: 3623
		protected readonly TextObject _affirmativeActionText;

		// Token: 0x04000E28 RID: 3624
		protected readonly TextObject _negativeActionText;

		// Token: 0x04000E29 RID: 3625
		private readonly Action<int> _goToIndex;

		// Token: 0x04000E2A RID: 3626
		private string _title = "";

		// Token: 0x04000E2B RID: 3627
		private string _description = "";

		// Token: 0x04000E2C RID: 3628
		private string _selectionText = "";

		// Token: 0x04000E2D RID: 3629
		private int _totalStageCount = -1;

		// Token: 0x04000E2E RID: 3630
		private int _currentStageIndex = -1;

		// Token: 0x04000E2F RID: 3631
		private int _furthestIndex = -1;

		// Token: 0x04000E30 RID: 3632
		private bool _anyItemSelected;
	}
}
