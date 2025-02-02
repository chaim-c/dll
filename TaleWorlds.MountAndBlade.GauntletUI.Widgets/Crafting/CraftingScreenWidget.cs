using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x0200015D RID: 349
	public class CraftingScreenWidget : Widget
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0003261D File Offset: 0x0003081D
		public CraftingScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00032628 File Offset: 0x00030828
		private void OnMainAction(Widget widget)
		{
			if (this.IsInCraftingMode)
			{
				base.Context.TwoDimensionContext.PlaySound("crafting/craft_success");
				return;
			}
			if (this.IsInRefinementMode)
			{
				base.Context.TwoDimensionContext.PlaySound("crafting/refine_success");
				return;
			}
			if (this.IsInSmeltingMode)
			{
				base.Context.TwoDimensionContext.PlaySound("crafting/smelt_success");
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003268E File Offset: 0x0003088E
		private void OnFinalAction(Widget widget)
		{
			if (this.NewCraftedWeaponPopupWidget != null && this.IsInCraftingMode)
			{
				bool isVisible = this.NewCraftedWeaponPopupWidget.IsVisible;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000326AC File Offset: 0x000308AC
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x000326B4 File Offset: 0x000308B4
		[Editor(false)]
		public bool IsInCraftingMode
		{
			get
			{
				return this._isInCraftingMode;
			}
			set
			{
				if (this._isInCraftingMode != value)
				{
					this._isInCraftingMode = value;
					base.OnPropertyChanged(value, "IsInCraftingMode");
				}
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x000326D2 File Offset: 0x000308D2
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x000326DA File Offset: 0x000308DA
		[Editor(false)]
		public bool IsInRefinementMode
		{
			get
			{
				return this._isInRefinementMode;
			}
			set
			{
				if (this._isInRefinementMode != value)
				{
					this._isInRefinementMode = value;
					base.OnPropertyChanged(value, "IsInRefinementMode");
				}
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x000326F8 File Offset: 0x000308F8
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00032700 File Offset: 0x00030900
		[Editor(false)]
		public bool IsInSmeltingMode
		{
			get
			{
				return this._isInSmeltingMode;
			}
			set
			{
				if (this._isInSmeltingMode != value)
				{
					this._isInSmeltingMode = value;
					base.OnPropertyChanged(value, "IsInSmeltingMode");
				}
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0003271E File Offset: 0x0003091E
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00032728 File Offset: 0x00030928
		[Editor(false)]
		public ButtonWidget MainActionButtonWidget
		{
			get
			{
				return this._mainActionButtonWidget;
			}
			set
			{
				if (this._mainActionButtonWidget != value)
				{
					this._mainActionButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "MainActionButtonWidget");
					if (!value.ClickEventHandlers.Contains(new Action<Widget>(this.OnMainAction)))
					{
						value.ClickEventHandlers.Add(new Action<Widget>(this.OnMainAction));
					}
				}
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00032781 File Offset: 0x00030981
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x0003278C File Offset: 0x0003098C
		[Editor(false)]
		public ButtonWidget FinalCraftButtonWidget
		{
			get
			{
				return this._mainActionButtonWidget;
			}
			set
			{
				if (this._finalCraftButtonWidget != value)
				{
					this._finalCraftButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "FinalCraftButtonWidget");
					if (!value.ClickEventHandlers.Contains(new Action<Widget>(this.OnFinalAction)))
					{
						value.ClickEventHandlers.Add(new Action<Widget>(this.OnFinalAction));
					}
				}
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x000327E5 File Offset: 0x000309E5
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x000327ED File Offset: 0x000309ED
		[Editor(false)]
		public Widget NewCraftedWeaponPopupWidget
		{
			get
			{
				return this._newCraftedWeaponPopupWidget;
			}
			set
			{
				if (this._newCraftedWeaponPopupWidget != value)
				{
					this._newCraftedWeaponPopupWidget = value;
					base.OnPropertyChanged<Widget>(value, "NewCraftedWeaponPopupWidget");
				}
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0003280B File Offset: 0x00030A0B
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00032813 File Offset: 0x00030A13
		[Editor(false)]
		public Widget CraftingOrderPopupWidget
		{
			get
			{
				return this._craftingOrdersPopupWidget;
			}
			set
			{
				if (this._craftingOrdersPopupWidget != value)
				{
					this._craftingOrdersPopupWidget = value;
					base.OnPropertyChanged<Widget>(value, "CraftingOrderPopupWidget");
				}
			}
		}

		// Token: 0x0400085F RID: 2143
		private ButtonWidget _mainActionButtonWidget;

		// Token: 0x04000860 RID: 2144
		private ButtonWidget _finalCraftButtonWidget;

		// Token: 0x04000861 RID: 2145
		private bool _isInCraftingMode;

		// Token: 0x04000862 RID: 2146
		private bool _isInRefinementMode;

		// Token: 0x04000863 RID: 2147
		private bool _isInSmeltingMode;

		// Token: 0x04000864 RID: 2148
		private Widget _newCraftedWeaponPopupWidget;

		// Token: 0x04000865 RID: 2149
		private Widget _craftingOrdersPopupWidget;
	}
}
