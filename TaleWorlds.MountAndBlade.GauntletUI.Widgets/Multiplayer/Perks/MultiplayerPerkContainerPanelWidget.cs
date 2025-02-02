using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Perks
{
	// Token: 0x0200008F RID: 143
	public class MultiplayerPerkContainerPanelWidget : Widget
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x000167DD File Offset: 0x000149DD
		public MultiplayerPerkContainerPanelWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000167E8 File Offset: 0x000149E8
		protected override void OnUpdate(float dt)
		{
			Widget latestMouseUpWidget = base.EventManager.LatestMouseUpWidget;
			if (this.TroopTupleBodyWidget != null)
			{
				MultiplayerClassLoadoutTroopSubclassButtonWidget troopTupleBodyWidget = this.TroopTupleBodyWidget;
				if (troopTupleBodyWidget == null || !troopTupleBodyWidget.IsSelected)
				{
					goto IL_5E;
				}
			}
			if (!base.CheckIsMyChildRecursive(latestMouseUpWidget) && (this.PopupWidgetFirst.IsVisible || this.PopupWidgetSecond.IsVisible || this.PopupWidgetThird.IsVisible))
			{
				this.ClosePanel();
			}
			IL_5E:
			MultiplayerClassLoadoutTroopSubclassButtonWidget troopTupleBodyWidget2 = this.TroopTupleBodyWidget;
			if ((troopTupleBodyWidget2 == null || !troopTupleBodyWidget2.IsSelected) && this._currentSelectedItem != null)
			{
				this._currentSelectedItem.IsSelected = false;
				this._currentSelectedItem = null;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00016888 File Offset: 0x00014A88
		public void PerkSelected(MultiplayerPerkItemToggleWidget selectedItem)
		{
			if (selectedItem == this._currentSelectedItem || selectedItem == null)
			{
				this.ClosePanel();
				return;
			}
			if (selectedItem != null && selectedItem.ParentWidget != null)
			{
				if (this._currentSelectedItem != null)
				{
					this._currentSelectedItem.IsSelected = false;
				}
				int childIndex = selectedItem.ParentWidget.GetChildIndex(selectedItem);
				this.PopupWidgetFirst.IsVisible = (childIndex == 0);
				this.PopupWidgetFirst.IsEnabled = (childIndex == 0);
				this.PopupWidgetSecond.IsVisible = (childIndex == 1);
				this.PopupWidgetSecond.IsEnabled = (childIndex == 1);
				this.PopupWidgetThird.IsVisible = (childIndex == 2);
				this.PopupWidgetThird.IsEnabled = (childIndex == 2);
				this.PopupWidgetFirst.SetPopupPerksContainer(this);
				this.PopupWidgetSecond.SetPopupPerksContainer(this);
				this.PopupWidgetThird.SetPopupPerksContainer(this);
				this._currentSelectedItem = selectedItem;
				this._currentSelectedItem.IsSelected = true;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001696C File Offset: 0x00014B6C
		private void ClosePanel()
		{
			if (this._currentSelectedItem != null)
			{
				this._currentSelectedItem.IsSelected = false;
			}
			this._currentSelectedItem = null;
			this.PopupWidgetFirst.IsVisible = false;
			this.PopupWidgetSecond.IsVisible = false;
			this.PopupWidgetThird.IsVisible = false;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x000169B8 File Offset: 0x00014BB8
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x000169C0 File Offset: 0x00014BC0
		public MultiplayerPerkPopupWidget PopupWidgetFirst
		{
			get
			{
				return this._popupWidgetFirst;
			}
			set
			{
				if (value != this._popupWidgetFirst)
				{
					this._popupWidgetFirst = value;
					base.OnPropertyChanged<MultiplayerPerkPopupWidget>(value, "PopupWidgetFirst");
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x000169DE File Offset: 0x00014BDE
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x000169E6 File Offset: 0x00014BE6
		public MultiplayerPerkPopupWidget PopupWidgetSecond
		{
			get
			{
				return this._popupWidgetSecond;
			}
			set
			{
				if (value != this._popupWidgetSecond)
				{
					this._popupWidgetSecond = value;
					base.OnPropertyChanged<MultiplayerPerkPopupWidget>(value, "PopupWidgetSecond");
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00016A04 File Offset: 0x00014C04
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00016A0C File Offset: 0x00014C0C
		public MultiplayerPerkPopupWidget PopupWidgetThird
		{
			get
			{
				return this._popupWidgetThird;
			}
			set
			{
				if (value != this._popupWidgetThird)
				{
					this._popupWidgetThird = value;
					base.OnPropertyChanged<MultiplayerPerkPopupWidget>(value, "PopupWidgetThird");
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00016A2A File Offset: 0x00014C2A
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00016A32 File Offset: 0x00014C32
		public MultiplayerClassLoadoutTroopSubclassButtonWidget TroopTupleBodyWidget
		{
			get
			{
				return this._troopTupleBodyWidget;
			}
			set
			{
				if (value != this._troopTupleBodyWidget)
				{
					this._troopTupleBodyWidget = value;
					base.OnPropertyChanged<MultiplayerClassLoadoutTroopSubclassButtonWidget>(value, "TroopTupleBodyWidget");
				}
			}
		}

		// Token: 0x04000374 RID: 884
		private MultiplayerPerkItemToggleWidget _currentSelectedItem;

		// Token: 0x04000375 RID: 885
		private MultiplayerPerkPopupWidget _popupWidgetFirst;

		// Token: 0x04000376 RID: 886
		private MultiplayerPerkPopupWidget _popupWidgetSecond;

		// Token: 0x04000377 RID: 887
		private MultiplayerPerkPopupWidget _popupWidgetThird;

		// Token: 0x04000378 RID: 888
		private MultiplayerClassLoadoutTroopSubclassButtonWidget _troopTupleBodyWidget;
	}
}
