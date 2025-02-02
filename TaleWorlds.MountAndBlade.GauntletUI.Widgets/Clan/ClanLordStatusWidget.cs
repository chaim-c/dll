using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Clan
{
	// Token: 0x02000169 RID: 361
	public class ClanLordStatusWidget : Widget
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x000335E5 File Offset: 0x000317E5
		public ClanLordStatusWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000335F8 File Offset: 0x000317F8
		private void SetVisualState(int type)
		{
			switch (type)
			{
			case 0:
				this.SetState("Dead");
				return;
			case 1:
				this.SetState("Married");
				return;
			case 2:
				this.SetState("Pregnant");
				return;
			case 3:
				this.SetState("InBattle");
				return;
			case 4:
				this.SetState("InSiege");
				return;
			case 5:
				this.SetState("Child");
				return;
			case 6:
				this.SetState("Prisoner");
				return;
			case 7:
				this.SetState("Sick");
				return;
			default:
				return;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0003368B File Offset: 0x0003188B
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00033693 File Offset: 0x00031893
		[Editor(false)]
		public int StatusType
		{
			get
			{
				return this._statusType;
			}
			set
			{
				if (this._statusType != value)
				{
					this._statusType = value;
					base.OnPropertyChanged(value, "StatusType");
					this.SetVisualState(value);
				}
			}
		}

		// Token: 0x0400088F RID: 2191
		private int _statusType = -1;
	}
}
