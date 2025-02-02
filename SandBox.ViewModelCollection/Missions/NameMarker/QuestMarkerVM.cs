using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Missions.NameMarker
{
	// Token: 0x02000029 RID: 41
	public class QuestMarkerVM : ViewModel
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000FE2F File Offset: 0x0000E02F
		public SandBoxUIHelper.IssueQuestFlags IssueQuestFlag { get; }

		// Token: 0x0600033C RID: 828 RVA: 0x0000FE37 File Offset: 0x0000E037
		public QuestMarkerVM(SandBoxUIHelper.IssueQuestFlags issueQuestFlag)
		{
			this.QuestMarkerType = (int)issueQuestFlag;
			this.IssueQuestFlag = issueQuestFlag;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000FE4D File Offset: 0x0000E04D
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000FE55 File Offset: 0x0000E055
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

		// Token: 0x040001B0 RID: 432
		private int _questMarkerType;
	}
}
