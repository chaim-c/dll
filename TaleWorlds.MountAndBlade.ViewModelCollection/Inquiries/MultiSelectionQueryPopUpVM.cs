using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries
{
	// Token: 0x02000039 RID: 57
	public class MultiSelectionQueryPopUpVM : PopUpBaseVM
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0001602C File Offset: 0x0001422C
		public MultiSelectionQueryPopUpVM(Action closeQuery) : base(closeQuery)
		{
			this.InquiryElements = new MBBindingList<InquiryElementVM>();
			this.MaxSelectableOptionCount = 0;
			this.MinSelectableOptionCount = 0;
			this._selectedOptionCount = 0;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00016058 File Offset: 0x00014258
		public void SetData(MultiSelectionInquiryData data)
		{
			this._data = data;
			this.InquiryElements.Clear();
			foreach (InquiryElement inquiryElement in this._data.InquiryElements)
			{
				TextObject hint = string.IsNullOrEmpty(inquiryElement.Hint) ? TextObject.Empty : new TextObject("{=!}" + inquiryElement.Hint, null);
				InquiryElementVM item = new InquiryElementVM(inquiryElement, hint, new Action<InquiryElementVM, bool>(this.OnInquiryElementSelected));
				this.InquiryElements.Add(item);
			}
			base.TitleText = this._data.TitleText;
			base.PopUpLabel = this._data.DescriptionText;
			this.MaxSelectableOptionCount = this._data.MaxSelectableOptionCount;
			this.MinSelectableOptionCount = this._data.MinSelectableOptionCount;
			base.ButtonOkLabel = this._data.AffirmativeText;
			base.ButtonCancelLabel = this._data.NegativeText;
			base.IsButtonOkShown = true;
			base.IsButtonCancelShown = this._data.IsExitShown;
			this.IsSearchAvailable = this._data.IsSeachAvailable;
			this.SearchPlaceholderText = new TextObject("{=tQOPRBFg}Search...", null).ToString();
			this.RefreshIsButtonOkEnabled();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000161B4 File Offset: 0x000143B4
		private void OnInquiryElementSelected(InquiryElementVM elementVM, bool isSelected)
		{
			if (isSelected)
			{
				this._selectedOptionCount++;
				if (this.MaxSelectableOptionCount != 1)
				{
					goto IL_5C;
				}
				using (IEnumerator<InquiryElementVM> enumerator = this.InquiryElements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						InquiryElementVM inquiryElementVM = enumerator.Current;
						if (inquiryElementVM != elementVM)
						{
							inquiryElementVM.IsSelected = false;
						}
					}
					goto IL_5C;
				}
			}
			this._selectedOptionCount--;
			IL_5C:
			this.RefreshIsButtonOkEnabled();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00016234 File Offset: 0x00014434
		public override void ExecuteAffirmativeAction()
		{
			if (this._data.AffirmativeAction != null)
			{
				List<InquiryElement> list = new List<InquiryElement>();
				foreach (InquiryElementVM inquiryElementVM in this.InquiryElements)
				{
					if (inquiryElementVM.IsSelected)
					{
						list.Add(inquiryElementVM.InquiryElement);
					}
				}
				this._data.AffirmativeAction(list);
			}
			base.CloseQuery();
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000162B8 File Offset: 0x000144B8
		public override void ExecuteNegativeAction()
		{
			Action<List<InquiryElement>> negativeAction = this._data.NegativeAction;
			if (negativeAction != null)
			{
				negativeAction(new List<InquiryElement>());
			}
			base.CloseQuery();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000162DB File Offset: 0x000144DB
		public override void OnClearData()
		{
			base.OnClearData();
			this._data = null;
			this.MaxSelectableOptionCount = 0;
			this.MinSelectableOptionCount = 0;
			this._selectedOptionCount = 0;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000162FF File Offset: 0x000144FF
		private void RefreshIsButtonOkEnabled()
		{
			base.IsButtonOkEnabled = ((this.MaxSelectableOptionCount <= 0 || this._selectedOptionCount <= this.MaxSelectableOptionCount) && this._selectedOptionCount >= this.MinSelectableOptionCount);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00016334 File Offset: 0x00014534
		private void UpdateInquiryFilter(string searchText, bool isAppending)
		{
			string value = searchText.ToLower();
			for (int i = 0; i < this.InquiryElements.Count; i++)
			{
				InquiryElementVM inquiryElementVM = this.InquiryElements[i];
				if (!isAppending || !inquiryElementVM.IsFilteredOut)
				{
					inquiryElementVM.IsFilteredOut = !inquiryElementVM.Text.ToLower().Contains(value);
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00016390 File Offset: 0x00014590
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00016398 File Offset: 0x00014598
		[DataSourceProperty]
		public MBBindingList<InquiryElementVM> InquiryElements
		{
			get
			{
				return this._inquiryElements;
			}
			set
			{
				if (value != this._inquiryElements)
				{
					this._inquiryElements = value;
					base.OnPropertyChangedWithValue<MBBindingList<InquiryElementVM>>(value, "InquiryElements");
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000163B6 File Offset: 0x000145B6
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x000163BE File Offset: 0x000145BE
		[DataSourceProperty]
		public int MaxSelectableOptionCount
		{
			get
			{
				return this._maxSelectableOptionCount;
			}
			set
			{
				if (value != this._maxSelectableOptionCount)
				{
					this._maxSelectableOptionCount = value;
					base.OnPropertyChangedWithValue(value, "MaxSelectableOptionCount");
				}
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000163DC File Offset: 0x000145DC
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x000163E4 File Offset: 0x000145E4
		[DataSourceProperty]
		public int MinSelectableOptionCount
		{
			get
			{
				return this._minSelectableOptionCount;
			}
			set
			{
				if (value != this._minSelectableOptionCount)
				{
					this._minSelectableOptionCount = value;
					base.OnPropertyChangedWithValue(value, "MinSelectableOptionCount");
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00016402 File Offset: 0x00014602
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0001640A File Offset: 0x0001460A
		[DataSourceProperty]
		public bool IsSearchAvailable
		{
			get
			{
				return this._isSearchAvailable;
			}
			set
			{
				if (value != this._isSearchAvailable)
				{
					this._isSearchAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsSearchAvailable");
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00016428 File Offset: 0x00014628
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00016430 File Offset: 0x00014630
		[DataSourceProperty]
		public string SearchText
		{
			get
			{
				return this._searchText;
			}
			set
			{
				if (value != this._searchText)
				{
					bool isAppending = value.IndexOf(this._searchText ?? "") >= 0;
					this._searchText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchText");
					this.UpdateInquiryFilter(this._searchText, isAppending);
				}
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00016487 File Offset: 0x00014687
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0001648F File Offset: 0x0001468F
		[DataSourceProperty]
		public string SearchPlaceholderText
		{
			get
			{
				return this._searchPlaceholderText;
			}
			set
			{
				if (value != this._searchPlaceholderText)
				{
					this._searchPlaceholderText = value;
					base.OnPropertyChangedWithValue<string>(value, "SearchPlaceholderText");
				}
			}
		}

		// Token: 0x04000274 RID: 628
		private MultiSelectionInquiryData _data;

		// Token: 0x04000275 RID: 629
		private int _selectedOptionCount;

		// Token: 0x04000276 RID: 630
		private MBBindingList<InquiryElementVM> _inquiryElements;

		// Token: 0x04000277 RID: 631
		private int _maxSelectableOptionCount;

		// Token: 0x04000278 RID: 632
		private int _minSelectableOptionCount;

		// Token: 0x04000279 RID: 633
		private bool _isSearchAvailable;

		// Token: 0x0400027A RID: 634
		private string _searchText;

		// Token: 0x0400027B RID: 635
		private string _searchPlaceholderText;
	}
}
