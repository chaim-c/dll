using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x0200014B RID: 331
	public class EncyclopediaDividerButtonWidget : ButtonWidget
	{
		// Token: 0x0600119A RID: 4506 RVA: 0x00030E34 File Offset: 0x0002F034
		public EncyclopediaDividerButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00030E3D File Offset: 0x0002F03D
		protected override void OnClick()
		{
			base.OnClick();
			this.UpdateItemListVisibility();
			this.UpdateCollapseIndicator();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00030E51 File Offset: 0x0002F051
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			base.IsVisible = (this.ItemListWidget.ChildCount > 0);
			this.UpdateCollapseIndicator();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00030E74 File Offset: 0x0002F074
		private void UpdateItemListVisibility()
		{
			if (this.ItemListWidget != null && this.ItemListWidget != null)
			{
				this.ItemListWidget.IsVisible = !this.ItemListWidget.IsVisible;
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00030EA0 File Offset: 0x0002F0A0
		private void UpdateCollapseIndicator()
		{
			if (this.ItemListWidget != null && this.ItemListWidget != null && this.CollapseIndicator != null)
			{
				if (this.ItemListWidget.IsVisible)
				{
					this.CollapseIndicator.SetState("Expanded");
					return;
				}
				this.CollapseIndicator.SetState("Collapsed");
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00030EF3 File Offset: 0x0002F0F3
		private void CollapseIndicatorUpdated()
		{
			this.CollapseIndicator.AddState("Collapsed");
			this.CollapseIndicator.AddState("Expanded");
			this.UpdateCollapseIndicator();
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00030F1B File Offset: 0x0002F11B
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00030F23 File Offset: 0x0002F123
		public Widget ItemListWidget
		{
			get
			{
				return this._itemListWidget;
			}
			set
			{
				if (value != this._itemListWidget)
				{
					this._itemListWidget = value;
					base.OnPropertyChanged<Widget>(value, "ItemListWidget");
				}
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00030F41 File Offset: 0x0002F141
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00030F49 File Offset: 0x0002F149
		public Widget CollapseIndicator
		{
			get
			{
				return this._collapseIndicator;
			}
			set
			{
				if (value != this._collapseIndicator)
				{
					this._collapseIndicator = value;
					base.OnPropertyChanged<Widget>(value, "CollapseIndicator");
					this.CollapseIndicatorUpdated();
				}
			}
		}

		// Token: 0x0400080F RID: 2063
		private Widget _itemListWidget;

		// Token: 0x04000810 RID: 2064
		private Widget _collapseIndicator;
	}
}
