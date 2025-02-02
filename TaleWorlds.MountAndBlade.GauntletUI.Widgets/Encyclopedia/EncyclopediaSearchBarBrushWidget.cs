using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x02000150 RID: 336
	public class EncyclopediaSearchBarBrushWidget : BrushWidget
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x000313F6 File Offset: 0x0002F5F6
		public EncyclopediaSearchBarBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00031400 File Offset: 0x0002F600
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			bool flag = base.EventManager.LatestMouseUpWidget == this || base.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget);
			bool flag2 = this.SearchResultPanel.VerticalScrollbar.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget);
			this.ShowResults = ((flag || flag2) && this.SearchInputWidget.Text.Length >= this.MinCharAmountToShowResults);
			this.SearchResultPanel.IsVisible = this.ShowResults;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003148D File Offset: 0x0002F68D
		protected override void OnMousePressed()
		{
			base.OnMousePressed();
			base.EventFired("SearchBarClick", Array.Empty<object>());
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000314A5 File Offset: 0x0002F6A5
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x000314AD File Offset: 0x0002F6AD
		public bool ShowResults
		{
			get
			{
				return this._showChat;
			}
			set
			{
				if (value != this._showChat)
				{
					this._showChat = value;
					base.OnPropertyChanged(value, "ShowResults");
				}
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x000314CB File Offset: 0x0002F6CB
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x000314D4 File Offset: 0x0002F6D4
		public EditableTextWidget SearchInputWidget
		{
			get
			{
				return this._searchInputWidget;
			}
			set
			{
				if (value != this._searchInputWidget)
				{
					if (this._searchInputWidget != null)
					{
						this._searchInputWidget.EventFire -= this.OnSearchInputClick;
					}
					this._searchInputWidget = value;
					base.OnPropertyChanged<EditableTextWidget>(value, "SearchInputWidget");
					if (this._searchInputWidget != null)
					{
						this._searchInputWidget.EventFire += this.OnSearchInputClick;
					}
				}
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003153B File Offset: 0x0002F73B
		private void OnSearchInputClick(Widget widget, string eventName, object[] arguments)
		{
			if (eventName == "MouseDown")
			{
				base.EventFired("SearchBarClick", Array.Empty<object>());
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003155A File Offset: 0x0002F75A
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00031562 File Offset: 0x0002F762
		public ScrollablePanel SearchResultPanel
		{
			get
			{
				return this._searchResultPanel;
			}
			set
			{
				if (value != this._searchResultPanel)
				{
					this._searchResultPanel = value;
					base.OnPropertyChanged<ScrollablePanel>(value, "SearchResultPanel");
				}
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00031580 File Offset: 0x0002F780
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00031588 File Offset: 0x0002F788
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
					base.OnPropertyChanged(value, "MinCharAmountToShowResults");
				}
			}
		}

		// Token: 0x0400081E RID: 2078
		private bool _showChat;

		// Token: 0x0400081F RID: 2079
		private ScrollablePanel _searchResultPanel;

		// Token: 0x04000820 RID: 2080
		private EditableTextWidget _searchInputWidget;

		// Token: 0x04000821 RID: 2081
		private int _minCharAmountToShowResults;
	}
}
