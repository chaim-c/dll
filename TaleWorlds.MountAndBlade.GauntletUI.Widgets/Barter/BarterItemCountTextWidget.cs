using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Barter
{
	// Token: 0x02000183 RID: 387
	public class BarterItemCountTextWidget : TextWidget
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x000365AE File Offset: 0x000347AE
		public BarterItemCountTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x000365B7 File Offset: 0x000347B7
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x000365BF File Offset: 0x000347BF
		[Editor(false)]
		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				if (this._count != value)
				{
					this._count = value;
					base.OnPropertyChanged(value, "Count");
					base.IntText = value;
					base.IsVisible = (value > 1);
				}
			}
		}

		// Token: 0x0400090F RID: 2319
		private int _count;
	}
}
