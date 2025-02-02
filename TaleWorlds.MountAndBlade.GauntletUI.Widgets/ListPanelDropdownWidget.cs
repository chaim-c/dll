using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200002A RID: 42
	public class ListPanelDropdownWidget : DropdownWidget
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00007F34 File Offset: 0x00006134
		public ListPanelDropdownWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007F3D File Offset: 0x0000613D
		protected override void OpenPanel()
		{
			base.OpenPanel();
			if (this.ListPanelContainer != null)
			{
				this.ListPanelContainer.IsVisible = true;
			}
			base.Button.IsSelected = true;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007F65 File Offset: 0x00006165
		protected override void ClosePanel()
		{
			if (this.ListPanelContainer != null)
			{
				this.ListPanelContainer.IsVisible = false;
			}
			base.Button.IsSelected = false;
			base.ClosePanel();
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00007F8D File Offset: 0x0000618D
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00007F95 File Offset: 0x00006195
		[Editor(false)]
		public Widget ListPanelContainer
		{
			get
			{
				return this._listPanelContainer;
			}
			set
			{
				if (this._listPanelContainer != value)
				{
					this._listPanelContainer = value;
					base.OnPropertyChanged<Widget>(value, "ListPanelContainer");
				}
			}
		}

		// Token: 0x04000102 RID: 258
		private Widget _listPanelContainer;
	}
}
