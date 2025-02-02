using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Quests
{
	// Token: 0x02000022 RID: 34
	public class QuestStageVM : ViewModel
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00010C90 File Offset: 0x0000EE90
		public QuestStageVM(JournalLog log, string dateString, bool isLastStage, Action onLogNotified, QuestStageTaskVM stageTask = null)
		{
			this.StageTask = new QuestStageTaskVM(TextObject.Empty, 0, 0, LogType.None);
			this._onLogNotified = onLogNotified;
			string content = log.LogText.ToString();
			GameTexts.SetVariable("ENTRY", content);
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			this.DateText = dateString;
			this.DescriptionText = log.LogText.ToString();
			this.IsLastStage = isLastStage;
			this.Log = log;
			this.UpdateIsNew();
			if (stageTask != null)
			{
				this.StageTask = stageTask;
				this.StageTask.IsValid = true;
				this.HasATask = true;
				this.IsTaskCompleted = (this.StageTask.CurrentProgress == this.StageTask.TargetProgress);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00010D50 File Offset: 0x0000EF50
		public QuestStageVM(JournalLog log, string description, string dateString, bool isLastStage, Action onLogNotified)
		{
			this.Log = log;
			this.StageTask = new QuestStageTaskVM(TextObject.Empty, 0, 0, LogType.None);
			this._onLogNotified = onLogNotified;
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			this.DateText = dateString;
			this.DescriptionText = description;
			this.IsLastStage = isLastStage;
			this.UpdateIsNew();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00010DB1 File Offset: 0x0000EFB1
		public void ExecuteResetUpdated()
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00010DB3 File Offset: 0x0000EFB3
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00010DC5 File Offset: 0x0000EFC5
		public void UpdateIsNew()
		{
			if (this.Log != null)
			{
				this.IsNew = this._viewDataTracker.UnExaminedQuestLogs.Any((JournalLog l) => l == this.Log);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00010DF1 File Offset: 0x0000EFF1
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00010DF9 File Offset: 0x0000EFF9
		[DataSourceProperty]
		public string DateText
		{
			get
			{
				return this._dateText;
			}
			set
			{
				if (value != this._dateText)
				{
					this._dateText = value;
					base.OnPropertyChangedWithValue<string>(value, "DateText");
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00010E1C File Offset: 0x0000F01C
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00010E24 File Offset: 0x0000F024
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00010E47 File Offset: 0x0000F047
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00010E4F File Offset: 0x0000F04F
		[DataSourceProperty]
		public bool HasATask
		{
			get
			{
				return this._hasATask;
			}
			set
			{
				if (value != this._hasATask)
				{
					this._hasATask = value;
					base.OnPropertyChangedWithValue(value, "HasATask");
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00010E6D File Offset: 0x0000F06D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00010E75 File Offset: 0x0000F075
		[DataSourceProperty]
		public bool IsNew
		{
			get
			{
				return this._isNew;
			}
			set
			{
				if (value != this._isNew)
				{
					this._isNew = value;
					base.OnPropertyChangedWithValue(value, "IsNew");
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00010E93 File Offset: 0x0000F093
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00010E9B File Offset: 0x0000F09B
		[DataSourceProperty]
		public bool IsLastStage
		{
			get
			{
				return this._isLastStage;
			}
			set
			{
				if (value != this._isLastStage)
				{
					this._isLastStage = value;
					base.OnPropertyChangedWithValue(value, "IsLastStage");
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00010EB9 File Offset: 0x0000F0B9
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00010EC1 File Offset: 0x0000F0C1
		[DataSourceProperty]
		public bool IsTaskCompleted
		{
			get
			{
				return this._isTaskCompleted;
			}
			set
			{
				if (value != this._isTaskCompleted)
				{
					this._isTaskCompleted = value;
					base.OnPropertyChangedWithValue(value, "IsTaskCompleted");
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00010EDF File Offset: 0x0000F0DF
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00010EE7 File Offset: 0x0000F0E7
		[DataSourceProperty]
		public QuestStageTaskVM StageTask
		{
			get
			{
				return this._stageTask;
			}
			set
			{
				if (value != this._stageTask)
				{
					this._stageTask = value;
					base.OnPropertyChangedWithValue<QuestStageTaskVM>(value, "StageTask");
				}
			}
		}

		// Token: 0x040000ED RID: 237
		public readonly JournalLog Log;

		// Token: 0x040000EE RID: 238
		private readonly Action _onLogNotified;

		// Token: 0x040000EF RID: 239
		private readonly IViewDataTracker _viewDataTracker;

		// Token: 0x040000F0 RID: 240
		private string _descriptionText;

		// Token: 0x040000F1 RID: 241
		private string _dateText;

		// Token: 0x040000F2 RID: 242
		private bool _hasATask;

		// Token: 0x040000F3 RID: 243
		private bool _isNew;

		// Token: 0x040000F4 RID: 244
		private bool _isTaskCompleted;

		// Token: 0x040000F5 RID: 245
		private bool _isLastStage;

		// Token: 0x040000F6 RID: 246
		private QuestStageTaskVM _stageTask;
	}
}
