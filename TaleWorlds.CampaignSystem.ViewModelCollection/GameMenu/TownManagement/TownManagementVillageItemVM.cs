using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x0200009A RID: 154
	public class TownManagementVillageItemVM : ViewModel
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003B2C4 File Offset: 0x000394C4
		public TownManagementVillageItemVM(Village village)
		{
			this._village = village;
			this.Background = village.Settlement.SettlementComponent.BackgroundMeshName + "_t";
			this.VillageType = (int)this.DetermineVillageType(village.VillageType);
			this.RefreshValues();
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003B316 File Offset: 0x00039516
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._village.Name.ToString();
			this.ProductionName = this._village.VillageType.PrimaryProduction.Name.ToString();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003B354 File Offset: 0x00039554
		public void ExecuteShowTooltip()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this._village.Settlement,
				true
			});
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003B382 File Offset: 0x00039582
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003B38C File Offset: 0x0003958C
		private TownManagementVillageItemVM.VillageTypes DetermineVillageType(VillageType village)
		{
			if (village == DefaultVillageTypes.EuropeHorseRanch)
			{
				return TownManagementVillageItemVM.VillageTypes.EuropeHorseRanch;
			}
			if (village == DefaultVillageTypes.BattanianHorseRanch)
			{
				return TownManagementVillageItemVM.VillageTypes.BattanianHorseRanch;
			}
			if (village == DefaultVillageTypes.SteppeHorseRanch)
			{
				return TownManagementVillageItemVM.VillageTypes.SteppeHorseRanch;
			}
			if (village == DefaultVillageTypes.DesertHorseRanch)
			{
				return TownManagementVillageItemVM.VillageTypes.DesertHorseRanch;
			}
			if (village == DefaultVillageTypes.WheatFarm)
			{
				return TownManagementVillageItemVM.VillageTypes.WheatFarm;
			}
			if (village == DefaultVillageTypes.Lumberjack)
			{
				return TownManagementVillageItemVM.VillageTypes.Lumberjack;
			}
			if (village == DefaultVillageTypes.ClayMine)
			{
				return TownManagementVillageItemVM.VillageTypes.ClayMine;
			}
			if (village == DefaultVillageTypes.SaltMine)
			{
				return TownManagementVillageItemVM.VillageTypes.SaltMine;
			}
			if (village == DefaultVillageTypes.IronMine)
			{
				return TownManagementVillageItemVM.VillageTypes.IronMine;
			}
			if (village == DefaultVillageTypes.Fisherman)
			{
				return TownManagementVillageItemVM.VillageTypes.Fisherman;
			}
			if (village == DefaultVillageTypes.CattleRange)
			{
				return TownManagementVillageItemVM.VillageTypes.CattleRange;
			}
			if (village == DefaultVillageTypes.SheepFarm)
			{
				return TownManagementVillageItemVM.VillageTypes.SheepFarm;
			}
			if (village == DefaultVillageTypes.VineYard)
			{
				return TownManagementVillageItemVM.VillageTypes.VineYard;
			}
			if (village == DefaultVillageTypes.FlaxPlant)
			{
				return TownManagementVillageItemVM.VillageTypes.FlaxPlant;
			}
			if (village == DefaultVillageTypes.DateFarm)
			{
				return TownManagementVillageItemVM.VillageTypes.DateFarm;
			}
			if (village == DefaultVillageTypes.OliveTrees)
			{
				return TownManagementVillageItemVM.VillageTypes.OliveTrees;
			}
			if (village == DefaultVillageTypes.SilkPlant)
			{
				return TownManagementVillageItemVM.VillageTypes.SilkPlant;
			}
			if (village == DefaultVillageTypes.SilverMine)
			{
				return TownManagementVillageItemVM.VillageTypes.SilverMine;
			}
			return TownManagementVillageItemVM.VillageTypes.None;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0003B458 File Offset: 0x00039658
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0003B460 File Offset: 0x00039660
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0003B483 File Offset: 0x00039683
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x0003B48B File Offset: 0x0003968B
		[DataSourceProperty]
		public string ProductionName
		{
			get
			{
				return this._productionName;
			}
			set
			{
				if (value != this._productionName)
				{
					this._productionName = value;
					base.OnPropertyChangedWithValue<string>(value, "ProductionName");
				}
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0003B4AE File Offset: 0x000396AE
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x0003B4B6 File Offset: 0x000396B6
		[DataSourceProperty]
		public string Background
		{
			get
			{
				return this._background;
			}
			set
			{
				if (value != this._background)
				{
					this._background = value;
					base.OnPropertyChangedWithValue<string>(value, "Background");
				}
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0003B4D9 File Offset: 0x000396D9
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x0003B4E1 File Offset: 0x000396E1
		[DataSourceProperty]
		public int VillageType
		{
			get
			{
				return this._villageType;
			}
			set
			{
				if (value != this._villageType)
				{
					this._villageType = value;
					base.OnPropertyChangedWithValue(value, "VillageType");
				}
			}
		}

		// Token: 0x040006EA RID: 1770
		private readonly Village _village;

		// Token: 0x040006EB RID: 1771
		private string _name;

		// Token: 0x040006EC RID: 1772
		private string _background;

		// Token: 0x040006ED RID: 1773
		private string _productionName;

		// Token: 0x040006EE RID: 1774
		private int _villageType;

		// Token: 0x020001DC RID: 476
		private enum VillageTypes
		{
			// Token: 0x04001063 RID: 4195
			None,
			// Token: 0x04001064 RID: 4196
			EuropeHorseRanch,
			// Token: 0x04001065 RID: 4197
			BattanianHorseRanch,
			// Token: 0x04001066 RID: 4198
			SteppeHorseRanch,
			// Token: 0x04001067 RID: 4199
			DesertHorseRanch,
			// Token: 0x04001068 RID: 4200
			WheatFarm,
			// Token: 0x04001069 RID: 4201
			Lumberjack,
			// Token: 0x0400106A RID: 4202
			ClayMine,
			// Token: 0x0400106B RID: 4203
			SaltMine,
			// Token: 0x0400106C RID: 4204
			IronMine,
			// Token: 0x0400106D RID: 4205
			Fisherman,
			// Token: 0x0400106E RID: 4206
			CattleRange,
			// Token: 0x0400106F RID: 4207
			SheepFarm,
			// Token: 0x04001070 RID: 4208
			VineYard,
			// Token: 0x04001071 RID: 4209
			FlaxPlant,
			// Token: 0x04001072 RID: 4210
			DateFarm,
			// Token: 0x04001073 RID: 4211
			OliveTrees,
			// Token: 0x04001074 RID: 4212
			SilkPlant,
			// Token: 0x04001075 RID: 4213
			SilverMine
		}
	}
}
