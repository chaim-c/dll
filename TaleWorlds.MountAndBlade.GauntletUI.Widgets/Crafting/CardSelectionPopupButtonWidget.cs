using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x02000155 RID: 341
	public class CardSelectionPopupButtonWidget : ButtonWidget
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00031BDA File Offset: 0x0002FDDA
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x00031BE2 File Offset: 0x0002FDE2
		public CircularAutoScrollablePanelWidget PropertiesContainer { get; set; }

		// Token: 0x060011FA RID: 4602 RVA: 0x00031BEB File Offset: 0x0002FDEB
		public CardSelectionPopupButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		public override void SetState(string stateName)
		{
			base.SetState(stateName);
			CircularAutoScrollablePanelWidget propertiesContainer = this.PropertiesContainer;
			if (propertiesContainer == null)
			{
				return;
			}
			propertiesContainer.SetState(stateName);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00031C0E File Offset: 0x0002FE0E
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			CircularAutoScrollablePanelWidget propertiesContainer = this.PropertiesContainer;
			if (propertiesContainer == null)
			{
				return;
			}
			propertiesContainer.SetHoverBegin();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00031C26 File Offset: 0x0002FE26
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			CircularAutoScrollablePanelWidget propertiesContainer = this.PropertiesContainer;
			if (propertiesContainer == null)
			{
				return;
			}
			propertiesContainer.SetHoverEnd();
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00031C3E File Offset: 0x0002FE3E
		protected override void OnMouseScroll()
		{
			base.OnMouseScroll();
			CircularAutoScrollablePanelWidget propertiesContainer = this.PropertiesContainer;
			if (propertiesContainer == null)
			{
				return;
			}
			propertiesContainer.SetScrollMouse();
		}
	}
}
