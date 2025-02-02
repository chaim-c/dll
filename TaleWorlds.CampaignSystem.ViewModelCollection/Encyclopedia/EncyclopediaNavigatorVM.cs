using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000B3 RID: 179
	public class EncyclopediaNavigatorVM : ViewModel
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00045647 File Offset: 0x00043847
		public Tuple<string, object> LastActivePage
		{
			get
			{
				if (!this.History.IsEmpty<Tuple<string, object>>())
				{
					return this.History[this.HistoryIndex];
				}
				return null;
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0004566C File Offset: 0x0004386C
		public EncyclopediaNavigatorVM(Func<string, object, bool, EncyclopediaPageVM> goToLink, Action closeEncyclopedia)
		{
			this._closeEncyclopedia = closeEncyclopedia;
			this.History = new List<Tuple<string, object>>();
			this.HistoryIndex = 0;
			this.MinCharAmountToShowResults = 3;
			this.SearchResults = new MBBindingList<EncyclopediaSearchResultVM>();
			Campaign.Current.EncyclopediaManager.SetLinkCallback(new Action<string, object>(this.ExecuteLink));
			this._goToLink = goToLink;
			this._searchResultComparer = new EncyclopediaNavigatorVM.SearchResultComparer(string.Empty);
			this.AddHistory("Home", null);
			this.RefreshValues();
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00045714 File Offset: 0x00043914
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent evnt)
		{
			this.IsHighlightEnabled = (evnt.NewNotificationElementID == "EncyclopediaSearchButton");
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0004572C File Offset: 0x0004392C
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM previousPageInputKey = this.PreviousPageInputKey;
			if (previousPageInputKey != null)
			{
				previousPageInputKey.OnFinalize();
			}
			InputKeyItemVM nextPageInputKey = this.NextPageInputKey;
			if (nextPageInputKey == null)
			{
				return;
			}
			nextPageInputKey.OnFinalize();
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00045755 File Offset: 0x00043955
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.LeaderText = GameTexts.FindText("str_done", null).ToString();
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00045789 File Offset: 0x00043989
		public void ExecuteHome()
		{
			Campaign.Current.EncyclopediaManager.GoToLink("Home", "-1");
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000457A4 File Offset: 0x000439A4
		public void ExecuteBarLink(string targetID)
		{
			if (targetID.Contains("Home"))
			{
				this.ExecuteHome();
				return;
			}
			if (targetID.Contains("ListPage"))
			{
				string a = targetID.Split(new char[]
				{
					'-'
				})[1];
				if (a == "Clans")
				{
					Campaign.Current.EncyclopediaManager.GoToLink("ListPage", "Faction");
					return;
				}
				if (a == "Kingdoms")
				{
					Campaign.Current.EncyclopediaManager.GoToLink("ListPage", "Kingdom");
					return;
				}
				if (a == "Heroes")
				{
					Campaign.Current.EncyclopediaManager.GoToLink("ListPage", "Hero");
					return;
				}
				if (a == "Settlements")
				{
					Campaign.Current.EncyclopediaManager.GoToLink("ListPage", "Settlement");
					return;
				}
				if (a == "Units")
				{
					Campaign.Current.EncyclopediaManager.GoToLink("ListPage", "NPCCharacter");
					return;
				}
				if (a == "Concept")
				{
					Campaign.Current.EncyclopediaManager.GoToLink("ListPage", "Concept");
				}
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000458D2 File Offset: 0x00043AD2
		public void ExecuteCloseEncyclopedia()
		{
			this._closeEncyclopedia();
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000458E0 File Offset: 0x00043AE0
		private void ExecuteLink(string pageId, object target)
		{
			if (pageId != "LastPage" && target != this.LastActivePage.Item2)
			{
				if (!(pageId != "Home"))
				{
					pageId != this.LastActivePage.Item1;
				}
				this.AddHistory(pageId, target);
			}
			this._goToLink(pageId, target, true);
			this.PageName = GameTexts.FindText("str_encyclopedia_name", null).ToString();
			this.ResetSearch();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00045965 File Offset: 0x00043B65
		public void ResetHistory()
		{
			this.HistoryIndex = 0;
			this.History.Clear();
			this.AddHistory("Home", null);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00045988 File Offset: 0x00043B88
		public void ExecuteBack()
		{
			if (this.HistoryIndex == 0)
			{
				return;
			}
			int num = this.HistoryIndex - 1;
			Tuple<string, object> tuple = this.History[num];
			if (tuple.Item1 != "LastPage" && (tuple.Item1 != this.LastActivePage.Item1 || tuple.Item2 != this.LastActivePage.Item2))
			{
				if (!(tuple.Item1 != "Home"))
				{
					tuple.Item1 != this.LastActivePage.Item1;
				}
			}
			this.UpdateHistoryIndex(num);
			this._goToLink(tuple.Item1, tuple.Item2, true);
			this.PageName = GameTexts.FindText("str_encyclopedia_name", null).ToString();
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00045A58 File Offset: 0x00043C58
		public void ExecuteForward()
		{
			if (this.HistoryIndex == this.History.Count - 1)
			{
				return;
			}
			int num = this.HistoryIndex + 1;
			Tuple<string, object> tuple = this.History[num];
			if (tuple.Item1 != "LastPage" && (tuple.Item1 != this.LastActivePage.Item1 || tuple.Item2 != this.LastActivePage.Item2))
			{
				if (!(tuple.Item1 != "Home"))
				{
					tuple.Item1 != this.LastActivePage.Item1;
				}
			}
			this.UpdateHistoryIndex(num);
			this._goToLink(tuple.Item1, tuple.Item2, true);
			this.PageName = GameTexts.FindText("str_encyclopedia_name", null).ToString();
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00045B33 File Offset: 0x00043D33
		public Tuple<string, object> GetLastPage()
		{
			return this.History[this.HistoryIndex];
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00045B48 File Offset: 0x00043D48
		public void AddHistory(string pageId, object obj)
		{
			if (this.HistoryIndex < this.History.Count - 1)
			{
				Tuple<string, object> tuple = this.History[this.HistoryIndex];
				if (tuple.Item1 == pageId && tuple.Item2 == obj)
				{
					return;
				}
				this.History.RemoveRange(this.HistoryIndex + 1, this.History.Count - this.HistoryIndex - 1);
			}
			this.History.Add(new Tuple<string, object>(pageId, obj));
			this.UpdateHistoryIndex(this.History.Count - 1);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00045BE0 File Offset: 0x00043DE0
		private void UpdateHistoryIndex(int newIndex)
		{
			this.HistoryIndex = newIndex;
			this.IsBackEnabled = (newIndex > 0);
			this.IsForwardEnabled = (newIndex < this.History.Count - 1);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00045C09 File Offset: 0x00043E09
		public void UpdatePageName(string value)
		{
			this.PageName = GameTexts.FindText("str_encyclopedia_name", null).ToString();
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00045C24 File Offset: 0x00043E24
		private void RefreshSearch(bool isAppending, bool isPasted)
		{
			int firstAsianCharIndex = EncyclopediaNavigatorVM.GetFirstAsianCharIndex(this.SearchText);
			this.MinCharAmountToShowResults = ((firstAsianCharIndex > -1 && firstAsianCharIndex < 3) ? (firstAsianCharIndex + 1) : 3);
			if (this.SearchText.Length < this.MinCharAmountToShowResults)
			{
				this.SearchResults.Clear();
				return;
			}
			if (!isAppending || this.SearchText.Length == this.MinCharAmountToShowResults || isPasted)
			{
				this.SearchResults.Clear();
				foreach (EncyclopediaPage encyclopediaPage in Campaign.Current.EncyclopediaManager.GetEncyclopediaPages())
				{
					foreach (EncyclopediaListItem encyclopediaListItem in encyclopediaPage.GetListItems())
					{
						int num = encyclopediaListItem.Name.IndexOf(this._searchText, StringComparison.OrdinalIgnoreCase);
						if (num >= 0)
						{
							this.SearchResults.Add(new EncyclopediaSearchResultVM(encyclopediaListItem, this._searchText, num));
						}
					}
				}
				this._searchResultComparer.SearchText = this._searchText;
				this.SearchResults.Sort(this._searchResultComparer);
				return;
			}
			if (isAppending)
			{
				foreach (EncyclopediaSearchResultVM encyclopediaSearchResultVM in this.SearchResults.ToList<EncyclopediaSearchResultVM>())
				{
					if (encyclopediaSearchResultVM.OrgNameText.IndexOf(this._searchText, StringComparison.OrdinalIgnoreCase) < 0)
					{
						this.SearchResults.Remove(encyclopediaSearchResultVM);
					}
					else
					{
						encyclopediaSearchResultVM.UpdateSearchedText(this._searchText);
					}
				}
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00045DE0 File Offset: 0x00043FE0
		private static int GetFirstAsianCharIndex(string searchText)
		{
			for (int i = 0; i < searchText.Length; i++)
			{
				if (Common.IsCharAsian(searchText[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00045E0F File Offset: 0x0004400F
		public void ResetSearch()
		{
			this.SearchText = string.Empty;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00045E1C File Offset: 0x0004401C
		public void ExecuteOnSearchActivated()
		{
			Game.Current.EventManager.TriggerEvent<OnEncyclopediaSearchActivatedEvent>(new OnEncyclopediaSearchActivatedEvent());
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00045E32 File Offset: 0x00044032
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00045E3A File Offset: 0x0004403A
		[DataSourceProperty]
		public bool CanSwitchTabs
		{
			get
			{
				return this._canSwitchTabs;
			}
			set
			{
				if (value != this._canSwitchTabs)
				{
					this._canSwitchTabs = value;
					base.OnPropertyChangedWithValue(value, "CanSwitchTabs");
				}
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00045E58 File Offset: 0x00044058
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00045E60 File Offset: 0x00044060
		[DataSourceProperty]
		public bool IsBackEnabled
		{
			get
			{
				return this._isBackEnabled;
			}
			set
			{
				if (value != this._isBackEnabled)
				{
					this._isBackEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsBackEnabled");
				}
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00045E7E File Offset: 0x0004407E
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00045E86 File Offset: 0x00044086
		[DataSourceProperty]
		public bool IsForwardEnabled
		{
			get
			{
				return this._isForwardEnabled;
			}
			set
			{
				if (value != this._isForwardEnabled)
				{
					this._isForwardEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsForwardEnabled");
				}
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00045EA4 File Offset: 0x000440A4
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x00045EAC File Offset: 0x000440AC
		[DataSourceProperty]
		public bool IsHighlightEnabled
		{
			get
			{
				return this._isHighlightEnabled;
			}
			set
			{
				if (value != this._isHighlightEnabled)
				{
					this._isHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHighlightEnabled");
				}
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00045ECA File Offset: 0x000440CA
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x00045ED2 File Offset: 0x000440D2
		[DataSourceProperty]
		public bool IsSearchResultsShown
		{
			get
			{
				return this._isSearchResultsShown;
			}
			set
			{
				if (value != this._isSearchResultsShown)
				{
					this._isSearchResultsShown = value;
					base.OnPropertyChangedWithValue(value, "IsSearchResultsShown");
				}
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00045EF0 File Offset: 0x000440F0
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00045EF8 File Offset: 0x000440F8
		[DataSourceProperty]
		public string NavBarString
		{
			get
			{
				return this._navBarString;
			}
			set
			{
				if (value != this._navBarString)
				{
					this._navBarString = value;
					base.OnPropertyChangedWithValue<string>(value, "NavBarString");
				}
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00045F1B File Offset: 0x0004411B
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00045F23 File Offset: 0x00044123
		[DataSourceProperty]
		public string PageName
		{
			get
			{
				return this._pageName;
			}
			set
			{
				if (value != this._pageName)
				{
					this._pageName = value;
					base.OnPropertyChangedWithValue<string>(value, "PageName");
				}
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00045F46 File Offset: 0x00044146
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00045F4E File Offset: 0x0004414E
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00045F71 File Offset: 0x00044171
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00045F79 File Offset: 0x00044179
		[DataSourceProperty]
		public string LeaderText
		{
			get
			{
				return this._leaderText;
			}
			set
			{
				if (value != this._leaderText)
				{
					this._leaderText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaderText");
				}
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00045F9C File Offset: 0x0004419C
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00045FA4 File Offset: 0x000441A4
		[DataSourceProperty]
		public MBBindingList<EncyclopediaSearchResultVM> SearchResults
		{
			get
			{
				return this._searchResults;
			}
			set
			{
				if (value != this._searchResults)
				{
					this._searchResults = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaSearchResultVM>>(value, "SearchResults");
				}
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00045FC2 File Offset: 0x000441C2
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00045FCC File Offset: 0x000441CC
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
					bool isAppending = value.ToLower().Contains(this._searchText);
					bool isPasted = string.IsNullOrEmpty(this._searchText) && !string.IsNullOrEmpty(value);
					this._searchText = value.ToLower();
					Debug.Print("isAppending: " + isAppending.ToString() + " isPasted: " + isPasted.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
					this.RefreshSearch(isAppending, isPasted);
					base.OnPropertyChangedWithValue<string>(value, "SearchText");
				}
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00046061 File Offset: 0x00044261
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00046069 File Offset: 0x00044269
		[DataSourceProperty]
		public int MinCharAmountToShowResults
		{
			get
			{
				return this._minCharAmountToShowResults;
			}
			set
			{
				if (value != this._minCharAmountToShowResults)
				{
					this._minCharAmountToShowResults = value;
					base.OnPropertyChangedWithValue(value, "MinCharAmountToShowResults");
				}
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00046087 File Offset: 0x00044287
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x0004608F File Offset: 0x0004428F
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x000460AD File Offset: 0x000442AD
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x000460B5 File Offset: 0x000442B5
		[DataSourceProperty]
		public InputKeyItemVM PreviousPageInputKey
		{
			get
			{
				return this._previousPageInputKey;
			}
			set
			{
				if (value != this._previousPageInputKey)
				{
					this._previousPageInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousPageInputKey");
				}
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000460D3 File Offset: 0x000442D3
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x000460DB File Offset: 0x000442DB
		[DataSourceProperty]
		public InputKeyItemVM NextPageInputKey
		{
			get
			{
				return this._nextPageInputKey;
			}
			set
			{
				if (value != this._nextPageInputKey)
				{
					this._nextPageInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextPageInputKey");
				}
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000460F9 File Offset: 0x000442F9
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00046108 File Offset: 0x00044308
		public void SetPreviousPageInputKey(HotKey hotkey)
		{
			this.PreviousPageInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00046117 File Offset: 0x00044317
		public void SetNextPageInputKey(HotKey hotkey)
		{
			this.NextPageInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x04000825 RID: 2085
		private List<Tuple<string, object>> History;

		// Token: 0x04000826 RID: 2086
		private int HistoryIndex;

		// Token: 0x04000827 RID: 2087
		private readonly Func<string, object, bool, EncyclopediaPageVM> _goToLink;

		// Token: 0x04000828 RID: 2088
		private readonly Action _closeEncyclopedia;

		// Token: 0x04000829 RID: 2089
		private EncyclopediaNavigatorVM.SearchResultComparer _searchResultComparer;

		// Token: 0x0400082A RID: 2090
		private MBBindingList<EncyclopediaSearchResultVM> _searchResults;

		// Token: 0x0400082B RID: 2091
		private string _searchText = "";

		// Token: 0x0400082C RID: 2092
		private string _pageName;

		// Token: 0x0400082D RID: 2093
		private string _doneText;

		// Token: 0x0400082E RID: 2094
		private string _leaderText;

		// Token: 0x0400082F RID: 2095
		private bool _canSwitchTabs;

		// Token: 0x04000830 RID: 2096
		private bool _isBackEnabled;

		// Token: 0x04000831 RID: 2097
		private bool _isForwardEnabled;

		// Token: 0x04000832 RID: 2098
		private bool _isHighlightEnabled;

		// Token: 0x04000833 RID: 2099
		private bool _isSearchResultsShown;

		// Token: 0x04000834 RID: 2100
		private string _navBarString;

		// Token: 0x04000835 RID: 2101
		private int _minCharAmountToShowResults;

		// Token: 0x04000836 RID: 2102
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000837 RID: 2103
		private InputKeyItemVM _previousPageInputKey;

		// Token: 0x04000838 RID: 2104
		private InputKeyItemVM _nextPageInputKey;

		// Token: 0x020001FD RID: 509
		private class SearchResultComparer : IComparer<EncyclopediaSearchResultVM>
		{
			// Token: 0x17000AEC RID: 2796
			// (get) Token: 0x060021EE RID: 8686 RVA: 0x0007482E File Offset: 0x00072A2E
			// (set) Token: 0x060021EF RID: 8687 RVA: 0x00074836 File Offset: 0x00072A36
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
						this._searchText = value;
					}
				}
			}

			// Token: 0x060021F0 RID: 8688 RVA: 0x0007484D File Offset: 0x00072A4D
			public SearchResultComparer(string searchText)
			{
				this.SearchText = searchText;
			}

			// Token: 0x060021F1 RID: 8689 RVA: 0x0007485C File Offset: 0x00072A5C
			private int CompareBasedOnCapitalization(EncyclopediaSearchResultVM x, EncyclopediaSearchResultVM y)
			{
				int num = (x.NameText.Length > 0 && char.IsUpper(x.NameText[0])) ? 1 : -1;
				int value = (y.NameText.Length > 0 && char.IsUpper(y.NameText[0])) ? 1 : -1;
				return num.CompareTo(value);
			}

			// Token: 0x060021F2 RID: 8690 RVA: 0x000748C0 File Offset: 0x00072AC0
			public int Compare(EncyclopediaSearchResultVM x, EncyclopediaSearchResultVM y)
			{
				if (x.MatchStartIndex != y.MatchStartIndex)
				{
					return y.MatchStartIndex.CompareTo(x.MatchStartIndex);
				}
				int num = this.CompareBasedOnCapitalization(x, y);
				if (num == 0)
				{
					return y.NameText.Length.CompareTo(x.NameText.Length);
				}
				return num;
			}

			// Token: 0x040010BE RID: 4286
			private string _searchText;
		}
	}
}
