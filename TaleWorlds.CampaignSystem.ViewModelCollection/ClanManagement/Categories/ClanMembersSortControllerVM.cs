using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200011D RID: 285
	public class ClanMembersSortControllerVM : ViewModel
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x00063438 File Offset: 0x00061638
		public ClanMembersSortControllerVM(MBBindingList<MBBindingList<ClanLordItemVM>> listsToControl)
		{
			this._listsToControl = listsToControl;
			this._nameComparer = new ClanMembersSortControllerVM.ItemNameComparer();
			this._locationComparer = new ClanMembersSortControllerVM.ItemLocationComparer();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0006345D File Offset: 0x0006165D
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00063494 File Offset: 0x00061694
		public void ExecuteSortByName()
		{
			int nameState = this.NameState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.NameState = (nameState + 1) % 3;
			if (this.NameState == 0)
			{
				int nameState2 = this.NameState;
				this.NameState = nameState2 + 1;
			}
			this._nameComparer.SetSortMode(this.NameState == 1);
			foreach (MBBindingList<ClanLordItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._nameComparer);
			}
			this.IsNameSelected = true;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00063530 File Offset: 0x00061730
		public void ExecuteSortByLocation()
		{
			int locationState = this.LocationState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.LocationState = (locationState + 1) % 3;
			if (this.LocationState == 0)
			{
				int locationState2 = this.LocationState;
				this.LocationState = locationState2 + 1;
			}
			this._locationComparer.SetSortMode(this.LocationState == 1);
			foreach (MBBindingList<ClanLordItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._locationComparer);
			}
			this.IsLocationSelected = true;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000635CC File Offset: 0x000617CC
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.LocationState = (int)state;
			this.IsNameSelected = false;
			this.IsLocationSelected = false;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000635EA File Offset: 0x000617EA
		public void ResetAllStates()
		{
			this.SetAllStates(CampaignUIHelper.SortState.Default);
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x000635F3 File Offset: 0x000617F3
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x000635FB File Offset: 0x000617FB
		[DataSourceProperty]
		public int NameState
		{
			get
			{
				return this._nameState;
			}
			set
			{
				if (value != this._nameState)
				{
					this._nameState = value;
					base.OnPropertyChangedWithValue(value, "NameState");
				}
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00063619 File Offset: 0x00061819
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x00063621 File Offset: 0x00061821
		[DataSourceProperty]
		public int LocationState
		{
			get
			{
				return this._locationState;
			}
			set
			{
				if (value != this._locationState)
				{
					this._locationState = value;
					base.OnPropertyChangedWithValue(value, "LocationState");
				}
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0006363F File Offset: 0x0006183F
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x00063647 File Offset: 0x00061847
		[DataSourceProperty]
		public bool IsNameSelected
		{
			get
			{
				return this._isNameSelected;
			}
			set
			{
				if (value != this._isNameSelected)
				{
					this._isNameSelected = value;
					base.OnPropertyChangedWithValue(value, "IsNameSelected");
				}
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00063665 File Offset: 0x00061865
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0006366D File Offset: 0x0006186D
		[DataSourceProperty]
		public bool IsLocationSelected
		{
			get
			{
				return this._isLocationSelected;
			}
			set
			{
				if (value != this._isLocationSelected)
				{
					this._isLocationSelected = value;
					base.OnPropertyChangedWithValue(value, "IsLocationSelected");
				}
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0006368B File Offset: 0x0006188B
		// (set) Token: 0x06001B89 RID: 7049 RVA: 0x00063693 File Offset: 0x00061893
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x000636B6 File Offset: 0x000618B6
		// (set) Token: 0x06001B8B RID: 7051 RVA: 0x000636BE File Offset: 0x000618BE
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x04000CFF RID: 3327
		private readonly MBBindingList<MBBindingList<ClanLordItemVM>> _listsToControl;

		// Token: 0x04000D00 RID: 3328
		private readonly ClanMembersSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000D01 RID: 3329
		private readonly ClanMembersSortControllerVM.ItemLocationComparer _locationComparer;

		// Token: 0x04000D02 RID: 3330
		private int _nameState;

		// Token: 0x04000D03 RID: 3331
		private int _locationState;

		// Token: 0x04000D04 RID: 3332
		private bool _isNameSelected;

		// Token: 0x04000D05 RID: 3333
		private bool _isLocationSelected;

		// Token: 0x04000D06 RID: 3334
		private string _nameText;

		// Token: 0x04000D07 RID: 3335
		private string _locationText;

		// Token: 0x02000273 RID: 627
		public abstract class ItemComparerBase : IComparer<ClanLordItemVM>
		{
			// Token: 0x0600236E RID: 9070 RVA: 0x00077118 File Offset: 0x00075318
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x0600236F RID: 9071
			public abstract int Compare(ClanLordItemVM x, ClanLordItemVM y);

			// Token: 0x040011C4 RID: 4548
			protected bool _isAcending;
		}

		// Token: 0x02000274 RID: 628
		public class ItemNameComparer : ClanMembersSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002371 RID: 9073 RVA: 0x00077129 File Offset: 0x00075329
			public override int Compare(ClanLordItemVM x, ClanLordItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x02000275 RID: 629
		public class ItemLocationComparer : ClanMembersSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002373 RID: 9075 RVA: 0x00077160 File Offset: 0x00075360
			public override int Compare(ClanLordItemVM x, ClanLordItemVM y)
			{
				if (this._isAcending)
				{
					return y.GetHero().GetTrackDistanceToMainAgent().CompareTo(x.GetHero().GetTrackDistanceToMainAgent()) * -1;
				}
				return y.GetHero().GetTrackDistanceToMainAgent().CompareTo(x.GetHero().GetTrackDistanceToMainAgent());
			}
		}
	}
}
