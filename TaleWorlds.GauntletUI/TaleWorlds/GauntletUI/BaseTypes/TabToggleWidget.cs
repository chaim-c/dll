using System;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200006F RID: 111
	public class TabToggleWidget : ButtonWidget
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001F7A3 File Offset: 0x0001D9A3
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001F7AB File Offset: 0x0001D9AB
		public TabControl TabControlWidget { get; set; }

		// Token: 0x0600074D RID: 1869 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		public TabToggleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001F7BD File Offset: 0x0001D9BD
		protected override void OnClick()
		{
			base.OnClick();
			if (this.TabControlWidget != null && !string.IsNullOrEmpty(this.TabName))
			{
				this.TabControlWidget.SetActiveTab(this.TabName);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001F7EC File Offset: 0x0001D9EC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			bool isDisabled = false;
			if (this.TabControlWidget == null || string.IsNullOrEmpty(this.TabName))
			{
				isDisabled = true;
			}
			else
			{
				Widget widget = this.TabControlWidget.FindChild(this.TabName);
				if (widget == null || widget.IsDisabled)
				{
					isDisabled = true;
				}
			}
			base.IsDisabled = isDisabled;
			base.IsSelected = this.DetermineIfIsSelected();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001F850 File Offset: 0x0001DA50
		private bool DetermineIfIsSelected()
		{
			TabControl tabControlWidget = this.TabControlWidget;
			return ((tabControlWidget != null) ? tabControlWidget.ActiveTab : null) != null && !string.IsNullOrEmpty(this.TabName) && this.TabControlWidget.ActiveTab.Id == this.TabName && base.IsVisible;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001F8A3 File Offset: 0x0001DAA3
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001F8AB File Offset: 0x0001DAAB
		[Editor(false)]
		public string TabName
		{
			get
			{
				return this._tabName;
			}
			set
			{
				if (this._tabName != value)
				{
					this._tabName = value;
					base.OnPropertyChanged<string>(value, "TabName");
				}
			}
		}

		// Token: 0x04000363 RID: 867
		private string _tabName;
	}
}
