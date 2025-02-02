using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000221 RID: 545
	public sealed class CraftingHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001E7C RID: 7804 RVA: 0x0006CA03 File Offset: 0x0006AC03
		public CraftingHotkeyCategory() : base("CraftingHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0006CA28 File Offset: 0x0006AC28
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.Escape),
				new Key(InputKey.ControllerRRight)
			};
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.Space),
				new Key(InputKey.ControllerRLeft)
			};
			List<Key> keys3 = new List<Key>
			{
				new Key(InputKey.Q),
				new Key(InputKey.ControllerLBumper)
			};
			List<Key> keys4 = new List<Key>
			{
				new Key(InputKey.E),
				new Key(InputKey.ControllerRBumper)
			};
			base.RegisterHotKey(new HotKey("Exit", "CraftingHotkeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Confirm", "CraftingHotkeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SwitchToPreviousTab", "CraftingHotkeyCategory", keys3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SwitchToNextTab", "CraftingHotkeyCategory", keys4, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Ascend", "CraftingHotkeyCategory", InputKey.MiddleMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Rotate", "CraftingHotkeyCategory", InputKey.LeftMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Zoom", "CraftingHotkeyCategory", InputKey.RightMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Copy", "CraftingHotkeyCategory", InputKey.C, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Paste", "CraftingHotkeyCategory", InputKey.V, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0006CBB0 File Offset: 0x0006ADB0
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(55, "ControllerZoomIn", "CraftingHotkeyCategory", InputKey.Invalid, InputKey.ControllerRTrigger, ""), true);
			base.RegisterGameKey(new GameKey(56, "ControllerZoomOut", "CraftingHotkeyCategory", InputKey.Invalid, InputKey.ControllerLTrigger, ""), true);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0006CC04 File Offset: 0x0006AE04
		private void RegisterGameAxisKeys()
		{
			GameAxisKey gameKey = GenericGameKeyContext.Current.RegisteredGameAxisKeys.First((GameAxisKey g) => g.Id.Equals("CameraAxisX"));
			GameAxisKey gameKey2 = GenericGameKeyContext.Current.RegisteredGameAxisKeys.First((GameAxisKey g) => g.Id.Equals("CameraAxisY"));
			base.RegisterGameAxisKey(gameKey, true);
			base.RegisterGameAxisKey(gameKey2, true);
		}

		// Token: 0x04000A24 RID: 2596
		public const string CategoryId = "CraftingHotkeyCategory";

		// Token: 0x04000A25 RID: 2597
		public const string Zoom = "Zoom";

		// Token: 0x04000A26 RID: 2598
		public const string Rotate = "Rotate";

		// Token: 0x04000A27 RID: 2599
		public const string Ascend = "Ascend";

		// Token: 0x04000A28 RID: 2600
		public const string ResetCamera = "ResetCamera";

		// Token: 0x04000A29 RID: 2601
		public const string Copy = "Copy";

		// Token: 0x04000A2A RID: 2602
		public const string Paste = "Paste";

		// Token: 0x04000A2B RID: 2603
		public const string Exit = "Exit";

		// Token: 0x04000A2C RID: 2604
		public const string Confirm = "Confirm";

		// Token: 0x04000A2D RID: 2605
		public const string SwitchToPreviousTab = "SwitchToPreviousTab";

		// Token: 0x04000A2E RID: 2606
		public const string SwitchToNextTab = "SwitchToNextTab";

		// Token: 0x04000A2F RID: 2607
		public const string ControllerRotationAxisX = "CameraAxisX";

		// Token: 0x04000A30 RID: 2608
		public const string ControllerRotationAxisY = "CameraAxisY";

		// Token: 0x04000A31 RID: 2609
		public const int ControllerZoomIn = 55;

		// Token: 0x04000A32 RID: 2610
		public const int ControllerZoomOut = 56;
	}
}
