using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection
{
	// Token: 0x0200000B RID: 11
	public class CraftingItemViewModel : ViewModel
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002AF5 File Offset: 0x00000CF5
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002AFD File Offset: 0x00000CFD
		[DataSourceProperty]
		public string UsedPieces
		{
			get
			{
				return this._usedPieces;
			}
			set
			{
				this._usedPieces = value;
				base.OnPropertyChangedWithValue<string>(value, "UsedPieces");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002B12 File Offset: 0x00000D12
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002B1A File Offset: 0x00000D1A
		[DataSourceProperty]
		public int WeaponClass
		{
			get
			{
				return this._weaponClass;
			}
			set
			{
				if (value != this._weaponClass)
				{
					this._weaponClass = value;
					base.OnPropertyChangedWithValue(value, "WeaponClass");
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002B38 File Offset: 0x00000D38
		public WeaponClass GetWeaponClass()
		{
			return (WeaponClass)this.WeaponClass;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002B40 File Offset: 0x00000D40
		public void SetCraftingData(WeaponClass weaponClass, WeaponDesignElement[] craftingPieces)
		{
			this.WeaponClass = (int)weaponClass;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B49 File Offset: 0x00000D49
		public CraftingItemViewModel()
		{
			this.WeaponClass = -1;
		}

		// Token: 0x04000022 RID: 34
		private string _usedPieces;

		// Token: 0x04000023 RID: 35
		private int _weaponClass;
	}
}
