using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x0200012B RID: 299
	public class InventoryBooleanRadioListPanel : ListPanel
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0002A7B6 File Offset: 0x000289B6
		public InventoryBooleanRadioListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002A7C0 File Offset: 0x000289C0
		private void UpdateChildSelectedState()
		{
			if (base.ChildCount < 2)
			{
				return;
			}
			ButtonWidget buttonWidget = base.GetChild(1) as ButtonWidget;
			ButtonWidget buttonWidget2 = base.GetChild(0) as ButtonWidget;
			if (buttonWidget == null || buttonWidget2 == null)
			{
				return;
			}
			buttonWidget.IsSelected = this.IsFirstSelected;
			buttonWidget2.IsSelected = !this.IsFirstSelected;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002A814 File Offset: 0x00028A14
		public override void OnChildSelected(Widget widget)
		{
			base.OnChildSelected(widget);
			int childIndex = base.GetChildIndex(widget);
			this.IsFirstSelected = (childIndex == 1);
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0002A83A File Offset: 0x00028A3A
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0002A842 File Offset: 0x00028A42
		[Editor(false)]
		public bool IsFirstSelected
		{
			get
			{
				return this._isFirstSelected;
			}
			set
			{
				if (this._isFirstSelected != value || !this._isSelectedStateSet)
				{
					this._isFirstSelected = value;
					base.OnPropertyChanged(value, "IsFirstSelected");
					this._isSelectedStateSet = true;
					this.UpdateChildSelectedState();
				}
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0002A875 File Offset: 0x00028A75
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0002A880 File Offset: 0x00028A80
		[Editor(false)]
		public bool IsSecondSelected
		{
			get
			{
				return !this._isFirstSelected;
			}
			set
			{
				if (this._isFirstSelected != !value)
				{
					this.IsFirstSelected = !value;
					base.OnPropertyChanged(!value, "IsSecondSelected");
				}
			}
		}

		// Token: 0x0400070D RID: 1805
		private bool _isSelectedStateSet;

		// Token: 0x0400070E RID: 1806
		private bool _isFirstSelected;
	}
}
