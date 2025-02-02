using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022D RID: 557
	public sealed class MultiplayerHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001EA8 RID: 7848 RVA: 0x0006DD1D File Offset: 0x0006BF1D
		public MultiplayerHotkeyCategory() : base("MultiplayerHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0006DD40 File Offset: 0x0006BF40
		private void RegisterHotKeys()
		{
			for (int i = 1; i <= 9; i++)
			{
				base.RegisterHotKey(new HotKey("StoreCameraPosition" + i.ToString(), "MultiplayerHotkeyCategory", InputKey.D0 + i, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			}
			for (int j = 1; j <= 9; j++)
			{
				base.RegisterHotKey(new HotKey("SpectateCameraPosition" + j.ToString(), "MultiplayerHotkeyCategory", InputKey.D0 + j, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			}
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.RightMouseButton),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRDown)
			};
			List<Key> keys3 = new List<Key>
			{
				new Key(InputKey.RightMouseButton),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> keys4 = new List<Key>
			{
				new Key(InputKey.F),
				new Key(InputKey.ControllerRLeft)
			};
			base.RegisterHotKey(new HotKey("PerformActionOnCosmeticItem", "MultiplayerHotkeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("PreviewCosmeticItem", "MultiplayerHotkeyCategory", keys3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("InspectBadgeProgression", "MultiplayerHotkeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ToggleFriendsList", "MultiplayerHotkeyCategory", keys4, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0006DEBA File Offset: 0x0006C0BA
		private void RegisterGameKeys()
		{
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0006DEBC File Offset: 0x0006C0BC
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000B12 RID: 2834
		public const string CategoryId = "MultiplayerHotkeyCategory";

		// Token: 0x04000B13 RID: 2835
		private const string _storeCameraPositionBase = "StoreCameraPosition";

		// Token: 0x04000B14 RID: 2836
		public const string StoreCameraPosition1 = "StoreCameraPosition1";

		// Token: 0x04000B15 RID: 2837
		public const string StoreCameraPosition2 = "StoreCameraPosition2";

		// Token: 0x04000B16 RID: 2838
		public const string StoreCameraPosition3 = "StoreCameraPosition3";

		// Token: 0x04000B17 RID: 2839
		public const string StoreCameraPosition4 = "StoreCameraPosition4";

		// Token: 0x04000B18 RID: 2840
		public const string StoreCameraPosition5 = "StoreCameraPosition5";

		// Token: 0x04000B19 RID: 2841
		public const string StoreCameraPosition6 = "StoreCameraPosition6";

		// Token: 0x04000B1A RID: 2842
		public const string StoreCameraPosition7 = "StoreCameraPosition7";

		// Token: 0x04000B1B RID: 2843
		public const string StoreCameraPosition8 = "StoreCameraPosition8";

		// Token: 0x04000B1C RID: 2844
		public const string StoreCameraPosition9 = "StoreCameraPosition9";

		// Token: 0x04000B1D RID: 2845
		private const string _spectateCameraPositionBase = "SpectateCameraPosition";

		// Token: 0x04000B1E RID: 2846
		public const string SpectateCameraPosition1 = "SpectateCameraPosition1";

		// Token: 0x04000B1F RID: 2847
		public const string SpectateCameraPosition2 = "SpectateCameraPosition2";

		// Token: 0x04000B20 RID: 2848
		public const string SpectateCameraPosition3 = "SpectateCameraPosition3";

		// Token: 0x04000B21 RID: 2849
		public const string SpectateCameraPosition4 = "SpectateCameraPosition4";

		// Token: 0x04000B22 RID: 2850
		public const string SpectateCameraPosition5 = "SpectateCameraPosition5";

		// Token: 0x04000B23 RID: 2851
		public const string SpectateCameraPosition6 = "SpectateCameraPosition6";

		// Token: 0x04000B24 RID: 2852
		public const string SpectateCameraPosition7 = "SpectateCameraPosition7";

		// Token: 0x04000B25 RID: 2853
		public const string SpectateCameraPosition8 = "SpectateCameraPosition8";

		// Token: 0x04000B26 RID: 2854
		public const string SpectateCameraPosition9 = "SpectateCameraPosition9";

		// Token: 0x04000B27 RID: 2855
		public const string InspectBadgeProgression = "InspectBadgeProgression";

		// Token: 0x04000B28 RID: 2856
		public const string PerformActionOnCosmeticItem = "PerformActionOnCosmeticItem";

		// Token: 0x04000B29 RID: 2857
		public const string PreviewCosmeticItem = "PreviewCosmeticItem";

		// Token: 0x04000B2A RID: 2858
		public const string ToggleFriendsList = "ToggleFriendsList";
	}
}
