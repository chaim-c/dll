using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CustomBattle
{
	// Token: 0x02000152 RID: 338
	public class CustomBattleSliderLockButtonWidget : ButtonWidget
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0003189F File Offset: 0x0002FA9F
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x000318A7 File Offset: 0x0002FAA7
		public Brush LockOpenedBrush { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000318B0 File Offset: 0x0002FAB0
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x000318B8 File Offset: 0x0002FAB8
		public Brush LockClosedBrush { get; set; }

		// Token: 0x060011E3 RID: 4579 RVA: 0x000318C1 File Offset: 0x0002FAC1
		public CustomBattleSliderLockButtonWidget(UIContext context) : base(context)
		{
			base.boolPropertyChanged += this.CustomBattleSliderLockButtonWidget_PropertyChanged;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000318DC File Offset: 0x0002FADC
		private void CustomBattleSliderLockButtonWidget_PropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsSelected")
			{
				base.Brush = (propertyValue ? this.LockClosedBrush : this.LockOpenedBrush);
			}
		}
	}
}
