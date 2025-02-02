using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Quests
{
	// Token: 0x02000020 RID: 32
	public class QuestMarkerVM : ViewModel
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public TextObject QuestTitle { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00010A54 File Offset: 0x0000EC54
		public TextObject QuestHintText { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00010A5C File Offset: 0x0000EC5C
		public CampaignUIHelper.IssueQuestFlags IssueQuestFlag { get; }

		// Token: 0x060001EA RID: 490 RVA: 0x00010A64 File Offset: 0x0000EC64
		public QuestMarkerVM(CampaignUIHelper.IssueQuestFlags issueQuestFlag, TextObject questTitle = null, TextObject questHintText = null)
		{
			this.IssueQuestFlag = issueQuestFlag;
			this.QuestMarkerType = (int)issueQuestFlag;
			this.QuestTitle = (questTitle ?? TextObject.Empty);
			this.QuestHintText = questHintText;
			if (this.QuestHintText != null)
			{
				this.QuestHint = new HintViewModel(this.QuestHintText, null);
			}
			this.IsTrackMarker = (issueQuestFlag == CampaignUIHelper.IssueQuestFlags.TrackedIssue || issueQuestFlag == CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest);
			this.RefreshValues();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010ACE File Offset: 0x0000ECCE
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (!TextObject.IsNullOrEmpty(this.QuestHintText))
			{
				this.QuestHint = new HintViewModel(this.QuestHintText, null);
				return;
			}
			this.QuestHint = new HintViewModel();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00010B01 File Offset: 0x0000ED01
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00010B09 File Offset: 0x0000ED09
		[DataSourceProperty]
		public bool IsTrackMarker
		{
			get
			{
				return this._isTrackMarker;
			}
			set
			{
				if (value != this._isTrackMarker)
				{
					this._isTrackMarker = value;
					base.OnPropertyChangedWithValue(value, "IsTrackMarker");
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00010B27 File Offset: 0x0000ED27
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00010B2F File Offset: 0x0000ED2F
		[DataSourceProperty]
		public int QuestMarkerType
		{
			get
			{
				return this._questMarkerType;
			}
			set
			{
				if (value != this._questMarkerType)
				{
					this._questMarkerType = value;
					base.OnPropertyChangedWithValue(value, "QuestMarkerType");
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00010B4D File Offset: 0x0000ED4D
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00010B55 File Offset: 0x0000ED55
		[DataSourceProperty]
		public HintViewModel QuestHint
		{
			get
			{
				return this._questHint;
			}
			set
			{
				if (value != this._questHint)
				{
					this._questHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "QuestHint");
				}
			}
		}

		// Token: 0x040000E4 RID: 228
		private bool _isTrackMarker;

		// Token: 0x040000E5 RID: 229
		private int _questMarkerType;

		// Token: 0x040000E6 RID: 230
		private HintViewModel _questHint;
	}
}
