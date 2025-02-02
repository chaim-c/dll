using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GatherArmy
{
	// Token: 0x02000141 RID: 321
	public class BoostItemButtonWidget : ButtonWidget
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0002FDB6 File Offset: 0x0002DFB6
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x0002FDBE File Offset: 0x0002DFBE
		public BoostCohesionPopupWidget ParentPopupWidget { get; private set; }

		// Token: 0x06001114 RID: 4372 RVA: 0x0002FDC7 File Offset: 0x0002DFC7
		public BoostItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0002FDD8 File Offset: 0x0002DFD8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.BoostCurrencyIconWidget != null)
			{
				int boostCurrencyType = this.BoostCurrencyType;
				if (boostCurrencyType != 0)
				{
					if (boostCurrencyType == 1)
					{
						this.BoostCurrencyIconWidget.SetState("Influence");
					}
				}
				else
				{
					this.BoostCurrencyIconWidget.SetState("Gold");
				}
			}
			if (this.ParentPopupWidget == null)
			{
				this.ParentPopupWidget = this.FindParentPopupWidget();
				if (this.ParentPopupWidget != null)
				{
					this.ClickEventHandlers.Add(new Action<Widget>(this.ParentPopupWidget.ClosePopup));
				}
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0002FE60 File Offset: 0x0002E060
		private BoostCohesionPopupWidget FindParentPopupWidget()
		{
			Widget widget = this;
			while (widget != base.EventManager.Root && this.ParentPopupWidget == null)
			{
				if (widget is BoostCohesionPopupWidget)
				{
					return widget as BoostCohesionPopupWidget;
				}
				widget = widget.ParentWidget;
			}
			return null;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0002FE9E File Offset: 0x0002E09E
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x0002FEA6 File Offset: 0x0002E0A6
		[Editor(false)]
		public int BoostCurrencyType
		{
			get
			{
				return this._boostCurrencyType;
			}
			set
			{
				if (this._boostCurrencyType != value)
				{
					this._boostCurrencyType = value;
					base.OnPropertyChanged(value, "BoostCurrencyType");
				}
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0002FEC4 File Offset: 0x0002E0C4
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x0002FECC File Offset: 0x0002E0CC
		[Editor(false)]
		public Widget BoostCurrencyIconWidget
		{
			get
			{
				return this._boostCurrencyIconWidget;
			}
			set
			{
				if (this._boostCurrencyIconWidget != value)
				{
					this._boostCurrencyIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "BoostCurrencyIconWidget");
				}
			}
		}

		// Token: 0x040007D9 RID: 2009
		private int _boostCurrencyType = -1;

		// Token: 0x040007DA RID: 2010
		private Widget _boostCurrencyIconWidget;
	}
}
