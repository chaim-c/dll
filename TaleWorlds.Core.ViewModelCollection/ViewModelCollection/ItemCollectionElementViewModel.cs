using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection
{
	// Token: 0x0200000C RID: 12
	public class ItemCollectionElementViewModel : ViewModel
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002B60 File Offset: 0x00000D60
		[DataSourceProperty]
		public string StringId
		{
			get
			{
				return this._stringId;
			}
			set
			{
				if (this._stringId != value)
				{
					this._stringId = value;
					base.OnPropertyChangedWithValue<string>(value, "StringId");
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002B83 File Offset: 0x00000D83
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002B8B File Offset: 0x00000D8B
		[DataSourceProperty]
		public int Ammo
		{
			get
			{
				return this._ammo;
			}
			set
			{
				if (this._ammo != value)
				{
					this._ammo = value;
					base.OnPropertyChangedWithValue(value, "Ammo");
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002BA9 File Offset: 0x00000DA9
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002BB1 File Offset: 0x00000DB1
		[DataSourceProperty]
		public int AverageUnitCost
		{
			get
			{
				return this._averageUnitCost;
			}
			set
			{
				if (this._averageUnitCost != value)
				{
					this._averageUnitCost = value;
					base.OnPropertyChangedWithValue(value, "AverageUnitCost");
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002BCF File Offset: 0x00000DCF
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002BD7 File Offset: 0x00000DD7
		[DataSourceProperty]
		public string ItemModifierId
		{
			get
			{
				return this._itemModifierId;
			}
			set
			{
				if (this._itemModifierId != value)
				{
					this._itemModifierId = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemModifierId");
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002BFA File Offset: 0x00000DFA
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002C02 File Offset: 0x00000E02
		[DataSourceProperty]
		public string BannerCode
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				if (value != this._bannerCode)
				{
					this._bannerCode = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerCode");
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002C2D File Offset: 0x00000E2D
		[DataSourceProperty]
		public float InitialPanRotation
		{
			get
			{
				return this._initialPanRotation;
			}
			set
			{
				if (value != this._initialPanRotation)
				{
					this._initialPanRotation = value;
					base.OnPropertyChangedWithValue(value, "InitialPanRotation");
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void FillFrom(EquipmentElement item, string bannerCode = "")
		{
			this.StringId = ((item.Item != null) ? item.Item.StringId : "");
			this.Ammo = (int)((!item.IsEmpty && item.Item.PrimaryWeapon != null && item.Item.PrimaryWeapon.IsConsumable) ? item.GetModifiedStackCountForUsage(0) : 0);
			this.AverageUnitCost = item.ItemValue;
			this.ItemModifierId = ((item.ItemModifier != null) ? item.ItemModifier.StringId : "");
			this.BannerCode = bannerCode;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002CEC File Offset: 0x00000EEC
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.StringId = "";
			this.ItemModifierId = "";
		}

		// Token: 0x04000024 RID: 36
		private string _stringId;

		// Token: 0x04000025 RID: 37
		private int _ammo;

		// Token: 0x04000026 RID: 38
		private int _averageUnitCost;

		// Token: 0x04000027 RID: 39
		private string _itemModifierId;

		// Token: 0x04000028 RID: 40
		private string _bannerCode;

		// Token: 0x04000029 RID: 41
		private float _initialPanRotation;
	}
}
