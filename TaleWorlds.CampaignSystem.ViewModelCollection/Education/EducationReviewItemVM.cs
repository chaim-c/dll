using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000D8 RID: 216
	public class EducationReviewItemVM : ViewModel
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x0004D13B File Offset: 0x0004B33B
		public void UpdateWith(string gainText)
		{
			this.GainText = gainText;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0004D144 File Offset: 0x0004B344
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x0004D14C File Offset: 0x0004B34C
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

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0004D16F File Offset: 0x0004B36F
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0004D177 File Offset: 0x0004B377
		[DataSourceProperty]
		public string GainText
		{
			get
			{
				return this._gainText;
			}
			set
			{
				if (value != this._gainText)
				{
					this._gainText = value;
					base.OnPropertyChangedWithValue<string>(value, "GainText");
				}
			}
		}

		// Token: 0x0400094F RID: 2383
		private string _title;

		// Token: 0x04000950 RID: 2384
		private string _gainText;
	}
}
