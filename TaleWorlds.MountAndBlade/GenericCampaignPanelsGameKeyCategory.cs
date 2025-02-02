using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000226 RID: 550
	public sealed class GenericCampaignPanelsGameKeyCategory : GameKeyContext
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0006CE90 File Offset: 0x0006B090
		// (set) Token: 0x06001E89 RID: 7817 RVA: 0x0006CE97 File Offset: 0x0006B097
		public static GenericCampaignPanelsGameKeyCategory Current { get; private set; }

		// Token: 0x06001E8A RID: 7818 RVA: 0x0006CE9F File Offset: 0x0006B09F
		public GenericCampaignPanelsGameKeyCategory(string categoryId = "GenericCampaignPanelsGameKeyCategory") : base(categoryId, 108, GameKeyContext.GameKeyContextType.Default)
		{
			GenericCampaignPanelsGameKeyCategory.Current = this;
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0006CEC4 File Offset: 0x0006B0C4
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.LeftShift),
				new Key(InputKey.RightShift)
			};
			base.RegisterHotKey(new HotKey("FiveStackModifier", "GenericCampaignPanelsGameKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.LeftControl),
				new Key(InputKey.RightControl)
			};
			base.RegisterHotKey(new HotKey("EntireStackModifier", "GenericCampaignPanelsGameKeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x0006CF48 File Offset: 0x0006B148
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(36, "BannerWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.B, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(37, "CharacterWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.C, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(38, "InventoryWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.I, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(39, "EncyclopediaWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.N, InputKey.ControllerLOption, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(40, "KingdomWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.K, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(41, "ClanWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.L, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(42, "QuestsWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.J, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(43, "PartyWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.P, GameKeyMainCategories.MenuShortcutCategory), true);
			base.RegisterGameKey(new GameKey(44, "FacegenWindow", "GenericCampaignPanelsGameKeyCategory", InputKey.V, GameKeyMainCategories.MenuShortcutCategory), true);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0006D071 File Offset: 0x0006B271
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000AB2 RID: 2738
		public const string CategoryId = "GenericCampaignPanelsGameKeyCategory";

		// Token: 0x04000AB3 RID: 2739
		public const string FiveStackModifier = "FiveStackModifier";

		// Token: 0x04000AB4 RID: 2740
		public const string EntireStackModifier = "EntireStackModifier";

		// Token: 0x04000AB5 RID: 2741
		public const int BannerWindow = 36;

		// Token: 0x04000AB6 RID: 2742
		public const int CharacterWindow = 37;

		// Token: 0x04000AB7 RID: 2743
		public const int InventoryWindow = 38;

		// Token: 0x04000AB8 RID: 2744
		public const int EncyclopediaWindow = 39;

		// Token: 0x04000AB9 RID: 2745
		public const int PartyWindow = 43;

		// Token: 0x04000ABA RID: 2746
		public const int KingdomWindow = 40;

		// Token: 0x04000ABB RID: 2747
		public const int ClanWindow = 41;

		// Token: 0x04000ABC RID: 2748
		public const int QuestsWindow = 42;

		// Token: 0x04000ABD RID: 2749
		public const int FacegenWindow = 44;
	}
}
