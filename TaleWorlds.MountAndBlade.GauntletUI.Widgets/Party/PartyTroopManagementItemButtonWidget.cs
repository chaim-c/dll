using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000062 RID: 98
	public class PartyTroopManagementItemButtonWidget : ButtonWidget
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00010249 File Offset: 0x0000E449
		public PartyTroopManagementItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00010254 File Offset: 0x0000E454
		public Widget GetActionButtonAtIndex(int index)
		{
			if (this.ActionButtonsContainer != null)
			{
				int num = 0;
				foreach (Widget widget in this.ActionButtonsContainer.AllChildren)
				{
					if (widget.Id == "ActionButton")
					{
						if (num == index)
						{
							return widget;
						}
						num++;
					}
				}
			}
			return null;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000102CC File Offset: 0x0000E4CC
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x000102D4 File Offset: 0x0000E4D4
		public Widget ActionButtonsContainer
		{
			get
			{
				return this._actionButtonsContainer;
			}
			set
			{
				if (value != this._actionButtonsContainer)
				{
					this._actionButtonsContainer = value;
					base.OnPropertyChanged<Widget>(value, "ActionButtonsContainer");
				}
			}
		}

		// Token: 0x04000249 RID: 585
		private Widget _actionButtonsContainer;
	}
}
