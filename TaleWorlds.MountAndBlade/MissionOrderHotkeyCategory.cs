using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022C RID: 556
	public sealed class MissionOrderHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0006DA04 File Offset: 0x0006BC04
		public MissionOrderHotkeyCategory() : base("MissionOrderHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0006DA26 File Offset: 0x0006BC26
		private void RegisterHotKeys()
		{
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0006DA28 File Offset: 0x0006BC28
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(67, "ViewOrders", "MissionOrderHotkeyCategory", InputKey.BackSpace, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(68, "SelectOrder1", "MissionOrderHotkeyCategory", InputKey.F1, InputKey.ControllerRLeft, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(69, "SelectOrder2", "MissionOrderHotkeyCategory", InputKey.F2, InputKey.ControllerRDown, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(70, "SelectOrder3", "MissionOrderHotkeyCategory", InputKey.F3, InputKey.ControllerRRight, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(71, "SelectOrder4", "MissionOrderHotkeyCategory", InputKey.F4, InputKey.ControllerRUp, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(72, "SelectOrder5", "MissionOrderHotkeyCategory", InputKey.F5, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(73, "SelectOrder6", "MissionOrderHotkeyCategory", InputKey.F6, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(74, "SelectOrder7", "MissionOrderHotkeyCategory", InputKey.F7, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(75, "SelectOrder8", "MissionOrderHotkeyCategory", InputKey.F8, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(76, "SelectOrderReturn", "MissionOrderHotkeyCategory", InputKey.F9, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(77, "EveryoneHear", "MissionOrderHotkeyCategory", InputKey.D0, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(78, "Group0Hear", "MissionOrderHotkeyCategory", InputKey.D1, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(79, "Group1Hear", "MissionOrderHotkeyCategory", InputKey.D2, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(80, "Group2Hear", "MissionOrderHotkeyCategory", InputKey.D3, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(81, "Group3Hear", "MissionOrderHotkeyCategory", InputKey.D4, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(82, "Group4Hear", "MissionOrderHotkeyCategory", InputKey.D5, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(83, "Group5Hear", "MissionOrderHotkeyCategory", InputKey.D6, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(84, "Group6Hear", "MissionOrderHotkeyCategory", InputKey.D7, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(85, "Group7Hear", "MissionOrderHotkeyCategory", InputKey.D8, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(86, "HoldOrder", "MissionOrderHotkeyCategory", InputKey.Invalid, InputKey.ControllerLBumper, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(87, "SelectNextGroup", "MissionOrderHotkeyCategory", InputKey.Invalid, InputKey.ControllerLRight, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(88, "SelectPreviousGroup", "MissionOrderHotkeyCategory", InputKey.Invalid, InputKey.ControllerLLeft, GameKeyMainCategories.OrderMenuCategory), true);
			base.RegisterGameKey(new GameKey(89, "ToggleGroupSelection", "MissionOrderHotkeyCategory", InputKey.Invalid, InputKey.ControllerLDown, GameKeyMainCategories.OrderMenuCategory), true);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0006DD1B File Offset: 0x0006BF1B
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000AFA RID: 2810
		public const string CategoryId = "MissionOrderHotkeyCategory";

		// Token: 0x04000AFB RID: 2811
		public const int ViewOrders = 67;

		// Token: 0x04000AFC RID: 2812
		public const int SelectOrder1 = 68;

		// Token: 0x04000AFD RID: 2813
		public const int SelectOrder2 = 69;

		// Token: 0x04000AFE RID: 2814
		public const int SelectOrder3 = 70;

		// Token: 0x04000AFF RID: 2815
		public const int SelectOrder4 = 71;

		// Token: 0x04000B00 RID: 2816
		public const int SelectOrder5 = 72;

		// Token: 0x04000B01 RID: 2817
		public const int SelectOrder6 = 73;

		// Token: 0x04000B02 RID: 2818
		public const int SelectOrder7 = 74;

		// Token: 0x04000B03 RID: 2819
		public const int SelectOrder8 = 75;

		// Token: 0x04000B04 RID: 2820
		public const int SelectOrderReturn = 76;

		// Token: 0x04000B05 RID: 2821
		public const int EveryoneHear = 77;

		// Token: 0x04000B06 RID: 2822
		public const int Group0Hear = 78;

		// Token: 0x04000B07 RID: 2823
		public const int Group1Hear = 79;

		// Token: 0x04000B08 RID: 2824
		public const int Group2Hear = 80;

		// Token: 0x04000B09 RID: 2825
		public const int Group3Hear = 81;

		// Token: 0x04000B0A RID: 2826
		public const int Group4Hear = 82;

		// Token: 0x04000B0B RID: 2827
		public const int Group5Hear = 83;

		// Token: 0x04000B0C RID: 2828
		public const int Group6Hear = 84;

		// Token: 0x04000B0D RID: 2829
		public const int Group7Hear = 85;

		// Token: 0x04000B0E RID: 2830
		public const int HoldOrder = 86;

		// Token: 0x04000B0F RID: 2831
		public const int SelectNextGroup = 87;

		// Token: 0x04000B10 RID: 2832
		public const int SelectPreviousGroup = 88;

		// Token: 0x04000B11 RID: 2833
		public const int ToggleGroupSelection = 89;
	}
}
