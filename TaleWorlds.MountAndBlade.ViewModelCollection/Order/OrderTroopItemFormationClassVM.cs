using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000028 RID: 40
	public class OrderTroopItemFormationClassVM : ViewModel
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000D825 File Offset: 0x0000BA25
		public OrderTroopItemFormationClassVM(Formation formation, FormationClass formationClass)
		{
			this._formation = formation;
			this.FormationClass = formationClass;
			this.FormationClassValue = (int)formationClass;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D844 File Offset: 0x0000BA44
		public void UpdateTroopCount()
		{
			switch (this.FormationClass)
			{
			case FormationClass.Infantry:
				this.TroopCount = this._formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.Infantry);
				return;
			case FormationClass.Ranged:
				this.TroopCount = this._formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.Ranged);
				return;
			case FormationClass.Cavalry:
				this.TroopCount = this._formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.Cavalry);
				return;
			case FormationClass.HorseArcher:
				this.TroopCount = this._formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.HorseArcher);
				return;
			default:
				return;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000D8BA File Offset: 0x0000BABA
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000D8C2 File Offset: 0x0000BAC2
		[DataSourceProperty]
		public int FormationClassValue
		{
			get
			{
				return this._formationClassValue;
			}
			set
			{
				if (value != this._formationClassValue)
				{
					this._formationClassValue = value;
					base.OnPropertyChangedWithValue(value, "FormationClassValue");
				}
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		[DataSourceProperty]
		public int TroopCount
		{
			get
			{
				return this._troopCount;
			}
			set
			{
				if (value != this._troopCount)
				{
					this._troopCount = value;
					base.OnPropertyChangedWithValue(value, "TroopCount");
				}
			}
		}

		// Token: 0x0400016B RID: 363
		public readonly FormationClass FormationClass;

		// Token: 0x0400016C RID: 364
		private readonly Formation _formation;

		// Token: 0x0400016D RID: 365
		private int _formationClassValue;

		// Token: 0x0400016E RID: 366
		private int _troopCount;
	}
}
