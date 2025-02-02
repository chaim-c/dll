using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.Order
{
	// Token: 0x020000DB RID: 219
	public class OrderFormationClassVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x0001FE57 File Offset: 0x0001E057
		public OrderFormationClassVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001FE68 File Offset: 0x0001E068
		private void UpdateVisual()
		{
			switch (this.FormationClassValue)
			{
			case 0:
				this.SetState("Infantry");
				return;
			case 1:
				this.SetState("Ranged");
				return;
			case 2:
				this.SetState("Cavalry");
				return;
			case 3:
				this.SetState("HorseArcher");
				return;
			default:
				this.SetState("Infantry");
				return;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0001FECF File Offset: 0x0001E0CF
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0001FED7 File Offset: 0x0001E0D7
		[Editor(false)]
		public int FormationClassValue
		{
			get
			{
				return this._formationClassValue;
			}
			set
			{
				if (this._formationClassValue != value)
				{
					this._formationClassValue = value;
					base.OnPropertyChanged(value, "FormationClassValue");
					this.UpdateVisual();
				}
			}
		}

		// Token: 0x04000536 RID: 1334
		private int _formationClassValue = -1;
	}
}
