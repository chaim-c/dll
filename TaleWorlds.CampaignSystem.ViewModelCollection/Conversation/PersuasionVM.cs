using System;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Conversation
{
	// Token: 0x020000FC RID: 252
	public class PersuasionVM : ViewModel
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x00058BF7 File Offset: 0x00056DF7
		public PersuasionVM(ConversationManager manager)
		{
			this.PersuasionProgress = new MBBindingList<BoolItemWithActionVM>();
			this._manager = manager;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00058C14 File Offset: 0x00056E14
		public void OnPersuasionProgress(Tuple<PersuasionOptionArgs, PersuasionOptionResult> selectedOption)
		{
			this.ProgressText = "";
			string newValue = null;
			string text = null;
			switch (selectedOption.Item2)
			{
			case PersuasionOptionResult.CriticalFailure:
				newValue = new TextObject("{=ocSW4WA2}Critical Fail!", null).ToString();
				text = "<a style=\"Conversation.Persuasion.Negative\"><b>{TEXT}</b></a>";
				break;
			case PersuasionOptionResult.Failure:
			case PersuasionOptionResult.Miss:
				newValue = new TextObject("{=JYOcl7Ox}Ineffective!", null).ToString();
				text = "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>";
				break;
			case PersuasionOptionResult.Success:
				newValue = new TextObject("{=3F0y3ugx}Success!", null).ToString();
				text = "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>";
				break;
			case PersuasionOptionResult.CriticalSuccess:
				newValue = new TextObject("{=4U9EnZt5}Critical Success!", null).ToString();
				text = "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>";
				break;
			}
			this.ProgressText = text.Replace("{TEXT}", newValue);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00058CC7 File Offset: 0x00056EC7
		public override void RefreshValues()
		{
			base.RefreshValues();
			PersuasionOptionVM currentPersuasionOption = this.CurrentPersuasionOption;
			if (currentPersuasionOption == null)
			{
				return;
			}
			currentPersuasionOption.RefreshValues();
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00058CDF File Offset: 0x00056EDF
		public void SetCurrentOption(PersuasionOptionVM option)
		{
			if (this.CurrentPersuasionOption != option)
			{
				this.CurrentPersuasionOption = option;
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00058CF4 File Offset: 0x00056EF4
		public void RefreshPersusasion()
		{
			this.CurrentCritFailChance = 0;
			this.CurrentFailChance = 0;
			this.CurrentCritSuccessChance = 0;
			this.CurrentSuccessChance = 0;
			this.IsPersuasionActive = ConversationManager.GetPersuasionIsActive();
			this.PersuasionProgress.Clear();
			this.PersuasionHint = new BasicTooltipViewModel();
			if (this.IsPersuasionActive)
			{
				int num = (int)ConversationManager.GetPersuasionProgress();
				int num2 = (int)ConversationManager.GetPersuasionGoalValue();
				for (int i = 1; i <= num2; i++)
				{
					bool isActive = i <= num;
					this.PersuasionProgress.Add(new BoolItemWithActionVM(null, isActive, null));
				}
				if (this.CurrentPersuasionOption != null)
				{
					this.CurrentCritFailChance = this._currentPersuasionOption.CritFailChance;
					this.CurrentFailChance = this._currentPersuasionOption.FailChance;
					this.CurrentCritSuccessChance = this._currentPersuasionOption.CritSuccessChance;
					this.CurrentSuccessChance = this._currentPersuasionOption.SuccessChance;
				}
				this.PersuasionHint = new BasicTooltipViewModel(() => this.GetPersuasionTooltip());
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00058DE1 File Offset: 0x00056FE1
		private string GetPersuasionTooltip()
		{
			if (ConversationManager.GetPersuasionIsActive())
			{
				GameTexts.SetVariable("CURRENT_PROGRESS", (int)ConversationManager.GetPersuasionProgress());
				GameTexts.SetVariable("TARGET_PROGRESS", (int)ConversationManager.GetPersuasionGoalValue());
				return GameTexts.FindText("str_persuasion_tooltip", null).ToString();
			}
			return "";
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00058E20 File Offset: 0x00057020
		private void RefreshChangeValues()
		{
			float num;
			float num2;
			float num3;
			this._manager.GetPersuasionChanceValues(out num, out num2, out num3);
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00058E3E File Offset: 0x0005703E
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x00058E46 File Offset: 0x00057046
		[DataSourceProperty]
		public BasicTooltipViewModel PersuasionHint
		{
			get
			{
				return this._persuasionHint;
			}
			set
			{
				if (this._persuasionHint != value)
				{
					this._persuasionHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "PersuasionHint");
				}
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00058E64 File Offset: 0x00057064
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x00058E6C File Offset: 0x0005706C
		[DataSourceProperty]
		public string ProgressText
		{
			get
			{
				return this._progressText;
			}
			set
			{
				if (this._progressText != value)
				{
					this._progressText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProgressText");
				}
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00058E8F File Offset: 0x0005708F
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x00058E97 File Offset: 0x00057097
		[DataSourceProperty]
		public MBBindingList<BoolItemWithActionVM> PersuasionProgress
		{
			get
			{
				return this._persuasionProgress;
			}
			set
			{
				if (value != this._persuasionProgress)
				{
					this._persuasionProgress = value;
					base.OnPropertyChangedWithValue<MBBindingList<BoolItemWithActionVM>>(value, "PersuasionProgress");
				}
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00058EB5 File Offset: 0x000570B5
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x00058EBD File Offset: 0x000570BD
		[DataSourceProperty]
		public bool IsPersuasionActive
		{
			get
			{
				return this._isPersuasionActive;
			}
			set
			{
				if (value != this._isPersuasionActive)
				{
					if (value)
					{
						this.RefreshChangeValues();
					}
					this._isPersuasionActive = value;
					base.OnPropertyChangedWithValue(value, "IsPersuasionActive");
				}
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00058EE4 File Offset: 0x000570E4
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x00058EEC File Offset: 0x000570EC
		[DataSourceProperty]
		public int CurrentSuccessChance
		{
			get
			{
				return this._currentSuccessChance;
			}
			set
			{
				if (this._currentSuccessChance != value)
				{
					this._currentSuccessChance = value;
					base.OnPropertyChangedWithValue(value, "CurrentSuccessChance");
				}
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00058F0A File Offset: 0x0005710A
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x00058F12 File Offset: 0x00057112
		[DataSourceProperty]
		public PersuasionOptionVM CurrentPersuasionOption
		{
			get
			{
				return this._currentPersuasionOption;
			}
			set
			{
				if (this._currentPersuasionOption != value)
				{
					this._currentPersuasionOption = value;
					base.OnPropertyChangedWithValue<PersuasionOptionVM>(value, "CurrentPersuasionOption");
				}
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x00058F30 File Offset: 0x00057130
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x00058F38 File Offset: 0x00057138
		[DataSourceProperty]
		public int CurrentFailChance
		{
			get
			{
				return this._currentFailChance;
			}
			set
			{
				if (this._currentFailChance != value)
				{
					this._currentFailChance = value;
					base.OnPropertyChangedWithValue(value, "CurrentFailChance");
				}
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00058F56 File Offset: 0x00057156
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x00058F5E File Offset: 0x0005715E
		[DataSourceProperty]
		public int CurrentCritSuccessChance
		{
			get
			{
				return this._currentCritSuccessChance;
			}
			set
			{
				if (this._currentCritSuccessChance != value)
				{
					this._currentCritSuccessChance = value;
					base.OnPropertyChangedWithValue(value, "CurrentCritSuccessChance");
				}
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00058F7C File Offset: 0x0005717C
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x00058F84 File Offset: 0x00057184
		[DataSourceProperty]
		public int CurrentCritFailChance
		{
			get
			{
				return this._currentCritFailChance;
			}
			set
			{
				if (this._currentCritFailChance != value)
				{
					this._currentCritFailChance = value;
					base.OnPropertyChangedWithValue(value, "CurrentCritFailChance");
				}
			}
		}

		// Token: 0x04000B32 RID: 2866
		internal const string PositiveText = "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>";

		// Token: 0x04000B33 RID: 2867
		internal const string NegativeText = "<a style=\"Conversation.Persuasion.Negative\"><b>{TEXT}</b></a>";

		// Token: 0x04000B34 RID: 2868
		internal const string NeutralText = "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>";

		// Token: 0x04000B35 RID: 2869
		private ConversationManager _manager;

		// Token: 0x04000B36 RID: 2870
		private MBBindingList<BoolItemWithActionVM> _persuasionProgress;

		// Token: 0x04000B37 RID: 2871
		private bool _isPersuasionActive;

		// Token: 0x04000B38 RID: 2872
		private int _currentCritFailChance;

		// Token: 0x04000B39 RID: 2873
		private int _currentFailChance;

		// Token: 0x04000B3A RID: 2874
		private int _currentSuccessChance;

		// Token: 0x04000B3B RID: 2875
		private int _currentCritSuccessChance;

		// Token: 0x04000B3C RID: 2876
		private string _progressText;

		// Token: 0x04000B3D RID: 2877
		private PersuasionOptionVM _currentPersuasionOption;

		// Token: 0x04000B3E RID: 2878
		private BasicTooltipViewModel _persuasionHint;
	}
}
