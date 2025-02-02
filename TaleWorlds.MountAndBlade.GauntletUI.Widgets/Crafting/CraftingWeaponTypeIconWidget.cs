using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x0200015F RID: 351
	public class CraftingWeaponTypeIconWidget : Widget
	{
		// Token: 0x06001278 RID: 4728 RVA: 0x000329E7 File Offset: 0x00030BE7
		public CraftingWeaponTypeIconWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000329F0 File Offset: 0x00030BF0
		private void UpdateIconVisual()
		{
			base.Sprite = base.Context.SpriteData.GetSprite("Crafting\\WeaponTypes\\" + this.WeaponType);
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00032A18 File Offset: 0x00030C18
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00032A20 File Offset: 0x00030C20
		[Editor(false)]
		public string WeaponType
		{
			get
			{
				return this._weaponType;
			}
			set
			{
				if (value != this._weaponType)
				{
					this._weaponType = value;
					this.UpdateIconVisual();
					base.OnPropertyChanged<string>(value, "WeaponType");
				}
			}
		}

		// Token: 0x0400086D RID: 2157
		private string _weaponType;
	}
}
