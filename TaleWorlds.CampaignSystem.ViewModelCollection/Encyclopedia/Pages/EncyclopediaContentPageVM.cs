using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000B7 RID: 183
	public class EncyclopediaContentPageVM : EncyclopediaPageVM
	{
		// Token: 0x06001206 RID: 4614 RVA: 0x00047152 File Offset: 0x00045352
		public EncyclopediaContentPageVM(EncyclopediaPageArgs args) : base(args)
		{
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004717D File Offset: 0x0004537D
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PreviousButtonLabel = this._previousButtonLabelText.ToString();
			this.NextButtonLabel = this._nextButtonLabelText.ToString();
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000471A8 File Offset: 0x000453A8
		public void InitializeQuickNavigation(EncyclopediaListVM list)
		{
			if (list != null && list.Items != null)
			{
				List<EncyclopediaListItemVM> list2 = (from x in list.Items
				where !x.IsFiltered
				select x).ToList<EncyclopediaListItemVM>();
				int count = list2.Count;
				int num = list2.FindIndex((EncyclopediaListItemVM x) => x.Object == base.Obj);
				if (count > 1 && num > -1)
				{
					if (num > 0)
					{
						this._previousItem = list2[num - 1];
						this.PreviousButtonHint = new HintViewModel(new TextObject(this._previousItem.Name, null), null);
						this.IsPreviousButtonEnabled = true;
					}
					if (num < count - 1)
					{
						this._nextItem = list2[num + 1];
						this.NextButtonHint = new HintViewModel(new TextObject(this._nextItem.Name, null), null);
						this.IsNextButtonEnabled = true;
					}
				}
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00047288 File Offset: 0x00045488
		public void ExecuteGoToNextItem()
		{
			if (this._nextItem != null)
			{
				this._nextItem.Execute();
				return;
			}
			Debug.FailedAssert("If the next button is enabled then next item should not be null.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Pages\\EncyclopediaContentPageVM.cs", "ExecuteGoToNextItem", 66);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000472B4 File Offset: 0x000454B4
		public void ExecuteGoToPreviousItem()
		{
			if (this._previousItem != null)
			{
				this._previousItem.Execute();
				return;
			}
			Debug.FailedAssert("If the previous button is enabled then previous item should not be null.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Pages\\EncyclopediaContentPageVM.cs", "ExecuteGoToPreviousItem", 78);
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x000472E0 File Offset: 0x000454E0
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x000472E8 File Offset: 0x000454E8
		[DataSourceProperty]
		public bool IsPreviousButtonEnabled
		{
			get
			{
				return this._isPreviousButtonEnabled;
			}
			set
			{
				if (value != this._isPreviousButtonEnabled)
				{
					this._isPreviousButtonEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPreviousButtonEnabled");
				}
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00047306 File Offset: 0x00045506
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x0004730E File Offset: 0x0004550E
		[DataSourceProperty]
		public bool IsNextButtonEnabled
		{
			get
			{
				return this._isNextButtonEnabled;
			}
			set
			{
				if (value != this._isNextButtonEnabled)
				{
					this._isNextButtonEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsNextButtonEnabled");
				}
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0004732C File Offset: 0x0004552C
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x00047334 File Offset: 0x00045534
		[DataSourceProperty]
		public string PreviousButtonLabel
		{
			get
			{
				return this._previousButtonLabel;
			}
			set
			{
				if (value != this._previousButtonLabel)
				{
					this._previousButtonLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "PreviousButtonLabel");
				}
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00047357 File Offset: 0x00045557
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x0004735F File Offset: 0x0004555F
		[DataSourceProperty]
		public string NextButtonLabel
		{
			get
			{
				return this._nextButtonLabel;
			}
			set
			{
				if (value != this._nextButtonLabel)
				{
					this._nextButtonLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "NextButtonLabel");
				}
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00047382 File Offset: 0x00045582
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x0004738A File Offset: 0x0004558A
		[DataSourceProperty]
		public HintViewModel PreviousButtonHint
		{
			get
			{
				return this._previousButtonHint;
			}
			set
			{
				if (value != this._previousButtonHint)
				{
					this._previousButtonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "PreviousButtonHint");
				}
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000473A8 File Offset: 0x000455A8
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x000473B0 File Offset: 0x000455B0
		[DataSourceProperty]
		public HintViewModel NextButtonHint
		{
			get
			{
				return this._nextButtonHint;
			}
			set
			{
				if (value != this._nextButtonHint)
				{
					this._nextButtonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "NextButtonHint");
				}
			}
		}

		// Token: 0x0400085F RID: 2143
		private EncyclopediaListItemVM _previousItem;

		// Token: 0x04000860 RID: 2144
		private EncyclopediaListItemVM _nextItem;

		// Token: 0x04000861 RID: 2145
		private TextObject _previousButtonLabelText = new TextObject("{=zlcMGAbn}Previous Page", null);

		// Token: 0x04000862 RID: 2146
		private TextObject _nextButtonLabelText = new TextObject("{=QFfMd5q3}Next Page", null);

		// Token: 0x04000863 RID: 2147
		private bool _isPreviousButtonEnabled;

		// Token: 0x04000864 RID: 2148
		private bool _isNextButtonEnabled;

		// Token: 0x04000865 RID: 2149
		private string _previousButtonLabel;

		// Token: 0x04000866 RID: 2150
		private string _nextButtonLabel;

		// Token: 0x04000867 RID: 2151
		private HintViewModel _previousButtonHint;

		// Token: 0x04000868 RID: 2152
		private HintViewModel _nextButtonHint;
	}
}
