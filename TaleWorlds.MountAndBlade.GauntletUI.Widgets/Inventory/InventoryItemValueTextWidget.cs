using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000134 RID: 308
	public class InventoryItemValueTextWidget : TextWidget
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x0002C71F File Offset: 0x0002A91F
		public InventoryItemValueTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002C728 File Offset: 0x0002A928
		private void HandleVisuals()
		{
			if (!this._firstHandled)
			{
				this.RegisterBrushStatesOfWidget();
				this._firstHandled = true;
			}
			switch (this.ProfitType)
			{
			case -2:
				this.SetState("VeryBad");
				return;
			case -1:
				this.SetState("Bad");
				return;
			case 0:
				this.SetState("Default");
				return;
			case 1:
				this.SetState("Good");
				return;
			case 2:
				this.SetState("VeryGood");
				return;
			default:
				return;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0002C7AA File Offset: 0x0002A9AA
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0002C7B2 File Offset: 0x0002A9B2
		[Editor(false)]
		public int ProfitType
		{
			get
			{
				return this._profitType;
			}
			set
			{
				if (this._profitType != value)
				{
					this._profitType = value;
					base.OnPropertyChanged(value, "ProfitType");
					this.HandleVisuals();
				}
			}
		}

		// Token: 0x04000755 RID: 1877
		private bool _firstHandled;

		// Token: 0x04000756 RID: 1878
		private int _profitType;
	}
}
