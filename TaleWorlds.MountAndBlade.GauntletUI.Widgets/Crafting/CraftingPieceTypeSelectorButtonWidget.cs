using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x0200015C RID: 348
	public class CraftingPieceTypeSelectorButtonWidget : ButtonWidget
	{
		// Token: 0x06001258 RID: 4696 RVA: 0x000325E0 File Offset: 0x000307E0
		public CraftingPieceTypeSelectorButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000325E9 File Offset: 0x000307E9
		public override void SetState(string stateName)
		{
			base.SetState(stateName);
			Widget visualsWidget = this.VisualsWidget;
			if (visualsWidget == null)
			{
				return;
			}
			visualsWidget.SetState(stateName);
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00032603 File Offset: 0x00030803
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x0003260B File Offset: 0x0003080B
		public Widget VisualsWidget
		{
			get
			{
				return this._visualsWidget;
			}
			set
			{
				if (value != this._visualsWidget)
				{
					this._visualsWidget = value;
				}
			}
		}

		// Token: 0x0400085E RID: 2142
		private Widget _visualsWidget;
	}
}
