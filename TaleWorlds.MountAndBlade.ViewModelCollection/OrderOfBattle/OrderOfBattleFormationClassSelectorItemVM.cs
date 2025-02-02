using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x0200002B RID: 43
	public class OrderOfBattleFormationClassSelectorItemVM : SelectorItemVM
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000E410 File Offset: 0x0000C610
		public OrderOfBattleFormationClassSelectorItemVM(DeploymentFormationClass formationClass) : base(formationClass.ToString())
		{
			this.FormationClass = formationClass;
			this.FormationClassInt = (int)formationClass;
			this.RefreshValues();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E439 File Offset: 0x0000C639
		public override void RefreshValues()
		{
			base.Hint = new HintViewModel(this.FormationClass.GetClassName(), null);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000E452 File Offset: 0x0000C652
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000E45A File Offset: 0x0000C65A
		[DataSourceProperty]
		public int FormationClassInt
		{
			get
			{
				return this._formationClassInt;
			}
			set
			{
				if (value != this._formationClassInt)
				{
					this._formationClassInt = value;
					base.OnPropertyChangedWithValue(value, "FormationClassInt");
				}
			}
		}

		// Token: 0x04000186 RID: 390
		public readonly DeploymentFormationClass FormationClass;

		// Token: 0x04000187 RID: 391
		private int _formationClassInt;
	}
}
