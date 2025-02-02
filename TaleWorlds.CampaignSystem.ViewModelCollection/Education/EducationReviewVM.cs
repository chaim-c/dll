using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000D7 RID: 215
	public class EducationReviewVM : ViewModel
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x0004CFB4 File Offset: 0x0004B1B4
		public EducationReviewVM(int pageCount)
		{
			this._pageCount = pageCount;
			this.ReviewList = new MBBindingList<EducationReviewItemVM>();
			for (int i = 0; i < this._pageCount - 1; i++)
			{
				this.ReviewList.Add(new EducationReviewItemVM());
			}
			this.RefreshValues();
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0004D024 File Offset: 0x0004B224
		public override void RefreshValues()
		{
			for (int i = 0; i < this.ReviewList.Count; i++)
			{
				this._educationPageTitle.SetTextVariable("NUMBER", i + 1);
				this.ReviewList[i].Title = this._educationPageTitle.ToString();
			}
			this.StageCompleteText = this._stageCompleteTextObject.ToString();
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0004D088 File Offset: 0x0004B288
		public void SetGainForStage(int pageIndex, string gainText)
		{
			if (pageIndex >= 0 && pageIndex < this._pageCount)
			{
				this.ReviewList[pageIndex].UpdateWith(gainText);
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004D0A9 File Offset: 0x0004B2A9
		public void SetCurrentPage(int currentPageIndex)
		{
			this.IsEnabled = (currentPageIndex == this._pageCount - 1);
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0004D0BC File Offset: 0x0004B2BC
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0004D0C4 File Offset: 0x0004B2C4
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0004D0E2 File Offset: 0x0004B2E2
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x0004D0EA File Offset: 0x0004B2EA
		[DataSourceProperty]
		public string StageCompleteText
		{
			get
			{
				return this._stageCompleteText;
			}
			set
			{
				if (value != this._stageCompleteText)
				{
					this._stageCompleteText = value;
					base.OnPropertyChangedWithValue<string>(value, "StageCompleteText");
				}
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0004D10D File Offset: 0x0004B30D
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x0004D115 File Offset: 0x0004B315
		[DataSourceProperty]
		public MBBindingList<EducationReviewItemVM> ReviewList
		{
			get
			{
				return this._reviewList;
			}
			set
			{
				if (value != this._reviewList)
				{
					this._reviewList = value;
					base.OnPropertyChangedWithValue<MBBindingList<EducationReviewItemVM>>(value, "ReviewList");
				}
			}
		}

		// Token: 0x04000949 RID: 2377
		private readonly int _pageCount;

		// Token: 0x0400094A RID: 2378
		private readonly TextObject _educationPageTitle = new TextObject("{=m1Yynagz}Page {NUMBER}", null);

		// Token: 0x0400094B RID: 2379
		private readonly TextObject _stageCompleteTextObject = new TextObject("{=flxDkoMh}Stage Complete", null);

		// Token: 0x0400094C RID: 2380
		private MBBindingList<EducationReviewItemVM> _reviewList;

		// Token: 0x0400094D RID: 2381
		private bool _isEnabled;

		// Token: 0x0400094E RID: 2382
		private string _stageCompleteText;
	}
}
