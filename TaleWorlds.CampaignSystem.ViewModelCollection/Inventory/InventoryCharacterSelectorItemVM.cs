using System;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Inventory
{
	// Token: 0x02000087 RID: 135
	public class InventoryCharacterSelectorItemVM : SelectorItemVM
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00036288 File Offset: 0x00034488
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00036290 File Offset: 0x00034490
		public string CharacterID { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00036299 File Offset: 0x00034499
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x000362A1 File Offset: 0x000344A1
		public Hero Hero { get; private set; }

		// Token: 0x06000D50 RID: 3408 RVA: 0x000362AA File Offset: 0x000344AA
		public InventoryCharacterSelectorItemVM(string characterID, Hero hero, TextObject characterName) : base(characterName)
		{
			this.Hero = hero;
			this.CharacterID = characterID;
		}
	}
}
