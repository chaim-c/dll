using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Quests
{
	// Token: 0x02000021 RID: 33
	public class QuestStageTaskVM : ViewModel
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00010B73 File Offset: 0x0000ED73
		public QuestStageTaskVM(TextObject taskName, int currentProgress, int targetProgress, LogType type)
		{
			this._taskNameObj = taskName;
			this.CurrentProgress = currentProgress;
			this.TargetProgress = targetProgress;
			base.OnPropertyChanged("NegativeTargetProgress");
			this.ProgressType = (int)type;
			this.RefreshValues();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010BA9 File Offset: 0x0000EDA9
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TaskName = this._taskNameObj.ToString();
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00010BC2 File Offset: 0x0000EDC2
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00010BCA File Offset: 0x0000EDCA
		[DataSourceProperty]
		public string TaskName
		{
			get
			{
				return this._taskName;
			}
			set
			{
				if (value != this._taskName)
				{
					this._taskName = value;
					base.OnPropertyChangedWithValue<string>(value, "TaskName");
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00010BED File Offset: 0x0000EDED
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00010BF5 File Offset: 0x0000EDF5
		[DataSourceProperty]
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				if (value != this._isValid)
				{
					this._isValid = value;
					base.OnPropertyChangedWithValue(value, "IsValid");
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00010C13 File Offset: 0x0000EE13
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00010C1B File Offset: 0x0000EE1B
		[DataSourceProperty]
		public int CurrentProgress
		{
			get
			{
				return this._currentProgress;
			}
			set
			{
				if (value != this._currentProgress)
				{
					this._currentProgress = value;
					base.OnPropertyChangedWithValue(value, "CurrentProgress");
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00010C39 File Offset: 0x0000EE39
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00010C41 File Offset: 0x0000EE41
		[DataSourceProperty]
		public int TargetProgress
		{
			get
			{
				return this._targetProgress;
			}
			set
			{
				if (value != this._targetProgress)
				{
					this._targetProgress = value;
					base.OnPropertyChangedWithValue(value, "TargetProgress");
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00010C5F File Offset: 0x0000EE5F
		[DataSourceProperty]
		public int NegativeTargetProgress
		{
			get
			{
				return this._targetProgress * -1;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00010C69 File Offset: 0x0000EE69
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00010C71 File Offset: 0x0000EE71
		[DataSourceProperty]
		public int ProgressType
		{
			get
			{
				return this._progressType;
			}
			set
			{
				if (value != this._progressType)
				{
					this._progressType = value;
					base.OnPropertyChangedWithValue(value, "ProgressType");
				}
			}
		}

		// Token: 0x040000E7 RID: 231
		private readonly TextObject _taskNameObj;

		// Token: 0x040000E8 RID: 232
		private string _taskName;

		// Token: 0x040000E9 RID: 233
		private int _currentProgress;

		// Token: 0x040000EA RID: 234
		private int _targetProgress;

		// Token: 0x040000EB RID: 235
		private int _progressType;

		// Token: 0x040000EC RID: 236
		private bool _isValid;
	}
}
