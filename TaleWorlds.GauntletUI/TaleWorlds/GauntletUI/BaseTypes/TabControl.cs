using System;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200006E RID: 110
	public class TabControl : Widget
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600073F RID: 1855 RVA: 0x0001F564 File Offset: 0x0001D764
		// (remove) Token: 0x06000740 RID: 1856 RVA: 0x0001F59C File Offset: 0x0001D79C
		public event OnActiveTabChangeEvent OnActiveTabChange;

		// Token: 0x06000741 RID: 1857 RVA: 0x0001F5D1 File Offset: 0x0001D7D1
		public TabControl(UIContext context) : base(context)
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001F5DA File Offset: 0x0001D7DA
		protected override void OnChildRemoved(Widget child)
		{
			base.OnChildRemoved(child);
			if (child == this.ActiveTab)
			{
				this.ActiveTab = null;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001F5F3 File Offset: 0x0001D7F3
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0001F5FB File Offset: 0x0001D7FB
		[Editor(false)]
		public Widget ActiveTab
		{
			get
			{
				return this._activeTab;
			}
			private set
			{
				if (this._activeTab != value)
				{
					this._activeTab = value;
					OnActiveTabChangeEvent onActiveTabChange = this.OnActiveTabChange;
					if (onActiveTabChange == null)
					{
						return;
					}
					onActiveTabChange();
				}
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001F620 File Offset: 0x0001D820
		private void SetActiveTab(int index)
		{
			Widget child = base.GetChild(index);
			this.SetActiveTab(child);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001F63C File Offset: 0x0001D83C
		public void SetActiveTab(string tabName)
		{
			Widget activeTab = base.FindChild(tabName);
			this.SetActiveTab(activeTab);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001F658 File Offset: 0x0001D858
		private void SetActiveTab(Widget newTab)
		{
			if (this.ActiveTab != newTab && newTab != null)
			{
				if (this.ActiveTab != null)
				{
					this.ActiveTab.IsVisible = false;
				}
				this.ActiveTab = newTab;
				this.ActiveTab.IsVisible = true;
				this.SelectedIndex = base.GetChildIndex(this.ActiveTab);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.ActiveTab != null && this.ActiveTab.ParentWidget == null)
			{
				this.ActiveTab = null;
			}
			if (this.ActiveTab == null || this.ActiveTab.IsDisabled)
			{
				for (int i = 0; i < base.ChildCount; i++)
				{
					Widget child = base.GetChild(i);
					if (child.IsEnabled && !string.IsNullOrEmpty(child.Id))
					{
						this.ActiveTab = child;
						break;
					}
				}
			}
			for (int j = 0; j < base.ChildCount; j++)
			{
				Widget child2 = base.GetChild(j);
				if (this.ActiveTab != child2 && (child2.IsEnabled || child2.IsVisible))
				{
					child2.IsVisible = false;
				}
				if (this.ActiveTab == child2)
				{
					child2.IsVisible = true;
				}
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001F771 File Offset: 0x0001D971
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001F779 File Offset: 0x0001D979
		[DataSourceProperty]
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				if (this._selectedIndex != value)
				{
					this._selectedIndex = value;
					this.SetActiveTab(this._selectedIndex);
					base.OnPropertyChanged(value, "SelectedIndex");
				}
			}
		}

		// Token: 0x04000360 RID: 864
		private Widget _activeTab;

		// Token: 0x04000361 RID: 865
		private int _selectedIndex;
	}
}
