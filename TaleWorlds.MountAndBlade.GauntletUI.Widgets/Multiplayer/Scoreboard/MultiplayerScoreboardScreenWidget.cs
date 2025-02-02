using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x0200008A RID: 138
	public class MultiplayerScoreboardScreenWidget : Widget
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x000162AF File Offset: 0x000144AF
		public MultiplayerScoreboardScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000162B8 File Offset: 0x000144B8
		private void UpdateSidesList()
		{
			if (this.SidesList == null)
			{
				return;
			}
			base.SuggestedWidth = (float)(this.IsSingleSide ? this.SingleColumnedWidth : this.DoubleColumnedWidth);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000162E0 File Offset: 0x000144E0
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x000162E8 File Offset: 0x000144E8
		[DataSourceProperty]
		public bool IsSingleSide
		{
			get
			{
				return this._isSingleSide;
			}
			set
			{
				if (value != this._isSingleSide)
				{
					this._isSingleSide = value;
					base.OnPropertyChanged(value, "IsSingleSide");
					this.UpdateSidesList();
				}
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001630C File Offset: 0x0001450C
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x00016314 File Offset: 0x00014514
		[DataSourceProperty]
		public int SingleColumnedWidth
		{
			get
			{
				return this._singleColumnedWidth;
			}
			set
			{
				if (value != this._singleColumnedWidth)
				{
					this._singleColumnedWidth = value;
					base.OnPropertyChanged(value, "SingleColumnedWidth");
				}
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00016332 File Offset: 0x00014532
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0001633A File Offset: 0x0001453A
		[DataSourceProperty]
		public int DoubleColumnedWidth
		{
			get
			{
				return this._doubleColumnedWidth;
			}
			set
			{
				if (value != this._doubleColumnedWidth)
				{
					this._doubleColumnedWidth = value;
					base.OnPropertyChanged(value, "DoubleColumnedWidth");
				}
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00016358 File Offset: 0x00014558
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x00016360 File Offset: 0x00014560
		[DataSourceProperty]
		public ListPanel SidesList
		{
			get
			{
				return this._sidesList;
			}
			set
			{
				if (value != this._sidesList)
				{
					this._sidesList = value;
					base.OnPropertyChanged<ListPanel>(value, "SidesList");
					this.UpdateSidesList();
				}
			}
		}

		// Token: 0x0400035E RID: 862
		private bool _isSingleSide;

		// Token: 0x0400035F RID: 863
		private int _singleColumnedWidth;

		// Token: 0x04000360 RID: 864
		private int _doubleColumnedWidth;

		// Token: 0x04000361 RID: 865
		private ListPanel _sidesList;
	}
}
