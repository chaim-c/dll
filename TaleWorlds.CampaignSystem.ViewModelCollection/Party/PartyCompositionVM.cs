using System;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x02000025 RID: 37
	public class PartyCompositionVM : ViewModel
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x000135E8 File Offset: 0x000117E8
		public PartyCompositionVM()
		{
			this.InfantryHint = new HintViewModel(new TextObject("{=1Bm1Wk1v}Infantry", null), null);
			this.RangedHint = new HintViewModel(new TextObject("{=bIiBytSB}Archers", null), null);
			this.CavalryHint = new HintViewModel(new TextObject("{=YVGtcLHF}Cavalry", null), null);
			this.HorseArcherHint = new HintViewModel(new TextObject("{=I1CMeL9R}Mounted Archers", null), null);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00013658 File Offset: 0x00011858
		public void OnTroopRemoved(FormationClass formationClass, int count)
		{
			if (this.IsInfantry(formationClass))
			{
				this.InfantryCount -= count;
			}
			if (this.IsRanged(formationClass))
			{
				this.RangedCount -= count;
			}
			if (this.IsCavalry(formationClass))
			{
				this.CavalryCount -= count;
			}
			if (this.IsHorseArcher(formationClass))
			{
				this.HorseArcherCount -= count;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000136C4 File Offset: 0x000118C4
		public void OnTroopAdded(FormationClass formationClass, int count)
		{
			if (this.IsInfantry(formationClass))
			{
				this.InfantryCount += count;
			}
			if (this.IsRanged(formationClass))
			{
				this.RangedCount += count;
			}
			if (this.IsCavalry(formationClass))
			{
				this.CavalryCount += count;
			}
			if (this.IsHorseArcher(formationClass))
			{
				this.HorseArcherCount += count;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00013730 File Offset: 0x00011930
		public void RefreshCounts(MBBindingList<PartyCharacterVM> list)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				TroopRosterElement troop = list[i].Troop;
				FormationClass defaultFormationClass = list[i].Troop.Character.DefaultFormationClass;
				if (this.IsInfantry(defaultFormationClass))
				{
					num += troop.Number;
				}
				if (this.IsRanged(defaultFormationClass))
				{
					num2 += troop.Number;
				}
				if (this.IsCavalry(defaultFormationClass))
				{
					num3 += troop.Number;
				}
				if (this.IsHorseArcher(defaultFormationClass))
				{
					num4 += troop.Number;
				}
			}
			this.InfantryCount = num;
			this.RangedCount = num2;
			this.CavalryCount = num3;
			this.HorseArcherCount = num4;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000137F1 File Offset: 0x000119F1
		private bool IsInfantry(FormationClass formationClass)
		{
			return formationClass == FormationClass.Infantry || formationClass == FormationClass.HeavyInfantry || formationClass == FormationClass.NumberOfDefaultFormations;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00013800 File Offset: 0x00011A00
		private bool IsRanged(FormationClass formationClass)
		{
			return formationClass == FormationClass.Ranged;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00013806 File Offset: 0x00011A06
		private bool IsCavalry(FormationClass formationClass)
		{
			return formationClass == FormationClass.Cavalry || formationClass == FormationClass.LightCavalry || formationClass == FormationClass.HeavyCavalry;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00013816 File Offset: 0x00011A16
		private bool IsHorseArcher(FormationClass formationClass)
		{
			return formationClass == FormationClass.HorseArcher;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0001381C File Offset: 0x00011A1C
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00013824 File Offset: 0x00011A24
		[DataSourceProperty]
		public int InfantryCount
		{
			get
			{
				return this._infantryCount;
			}
			set
			{
				if (value != this._infantryCount)
				{
					this._infantryCount = value;
					base.OnPropertyChangedWithValue(value, "InfantryCount");
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00013842 File Offset: 0x00011A42
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0001384A File Offset: 0x00011A4A
		[DataSourceProperty]
		public int RangedCount
		{
			get
			{
				return this._rangedCount;
			}
			set
			{
				if (value != this._rangedCount)
				{
					this._rangedCount = value;
					base.OnPropertyChangedWithValue(value, "RangedCount");
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00013868 File Offset: 0x00011A68
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00013870 File Offset: 0x00011A70
		[DataSourceProperty]
		public int CavalryCount
		{
			get
			{
				return this._cavalryCount;
			}
			set
			{
				if (value != this._cavalryCount)
				{
					this._cavalryCount = value;
					base.OnPropertyChangedWithValue(value, "CavalryCount");
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0001388E File Offset: 0x00011A8E
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00013896 File Offset: 0x00011A96
		[DataSourceProperty]
		public int HorseArcherCount
		{
			get
			{
				return this._horseArcherCount;
			}
			set
			{
				if (value != this._horseArcherCount)
				{
					this._horseArcherCount = value;
					base.OnPropertyChangedWithValue(value, "HorseArcherCount");
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000138B4 File Offset: 0x00011AB4
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000138BC File Offset: 0x00011ABC
		[DataSourceProperty]
		public HintViewModel InfantryHint
		{
			get
			{
				return this._infantryHint;
			}
			set
			{
				if (value != this._infantryHint)
				{
					this._infantryHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "InfantryHint");
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000138DA File Offset: 0x00011ADA
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000138E2 File Offset: 0x00011AE2
		[DataSourceProperty]
		public HintViewModel RangedHint
		{
			get
			{
				return this._rangedHint;
			}
			set
			{
				if (value != this._rangedHint)
				{
					this._rangedHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RangedHint");
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00013900 File Offset: 0x00011B00
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00013908 File Offset: 0x00011B08
		[DataSourceProperty]
		public HintViewModel CavalryHint
		{
			get
			{
				return this._cavalryHint;
			}
			set
			{
				if (value != this._cavalryHint)
				{
					this._cavalryHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CavalryHint");
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00013926 File Offset: 0x00011B26
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0001392E File Offset: 0x00011B2E
		[DataSourceProperty]
		public HintViewModel HorseArcherHint
		{
			get
			{
				return this._horseArcherHint;
			}
			set
			{
				if (value != this._horseArcherHint)
				{
					this._horseArcherHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "HorseArcherHint");
				}
			}
		}

		// Token: 0x0400014D RID: 333
		private int _infantryCount;

		// Token: 0x0400014E RID: 334
		private int _rangedCount;

		// Token: 0x0400014F RID: 335
		private int _cavalryCount;

		// Token: 0x04000150 RID: 336
		private int _horseArcherCount;

		// Token: 0x04000151 RID: 337
		private HintViewModel _infantryHint;

		// Token: 0x04000152 RID: 338
		private HintViewModel _rangedHint;

		// Token: 0x04000153 RID: 339
		private HintViewModel _cavalryHint;

		// Token: 0x04000154 RID: 340
		private HintViewModel _horseArcherHint;
	}
}
