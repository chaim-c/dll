using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000DE RID: 222
	public class CraftingPerkVM : ViewModel
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x0004E48B File Offset: 0x0004C68B
		public CraftingPerkVM(PerkObject perk)
		{
			this.Perk = perk;
			this.Name = this.Perk.Name.ToString();
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x0004E4B8 File Offset: 0x0004C6B8
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

		// Token: 0x04000998 RID: 2456
		public readonly PerkObject Perk;

		// Token: 0x04000999 RID: 2457
		private string _name;
	}
}
