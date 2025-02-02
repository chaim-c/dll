using System;
using System.Linq;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000222 RID: 546
	public sealed class FaceGenHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001E80 RID: 7808 RVA: 0x0006CC7F File Offset: 0x0006AE7F
		public FaceGenHotkeyCategory() : base("FaceGenHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0006CCA4 File Offset: 0x0006AEA4
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("Ascend", "FaceGenHotkeyCategory", InputKey.MiddleMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Rotate", "FaceGenHotkeyCategory", InputKey.LeftMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Zoom", "FaceGenHotkeyCategory", InputKey.RightMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Copy", "FaceGenHotkeyCategory", InputKey.C, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Paste", "FaceGenHotkeyCategory", InputKey.V, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0006CD3C File Offset: 0x0006AF3C
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(55, "ControllerZoomIn", "FaceGenHotkeyCategory", InputKey.Invalid, InputKey.ControllerRTrigger, ""), true);
			base.RegisterGameKey(new GameKey(56, "ControllerZoomOut", "FaceGenHotkeyCategory", InputKey.Invalid, InputKey.ControllerLTrigger, ""), true);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0006CD90 File Offset: 0x0006AF90
		private void RegisterGameAxisKeys()
		{
			GameAxisKey gameKey = GenericGameKeyContext.Current.RegisteredGameAxisKeys.First((GameAxisKey g) => g.Id.Equals("CameraAxisX"));
			GameAxisKey gameKey2 = GenericGameKeyContext.Current.RegisteredGameAxisKeys.First((GameAxisKey g) => g.Id.Equals("CameraAxisY"));
			base.RegisterGameAxisKey(gameKey, true);
			base.RegisterGameAxisKey(gameKey2, true);
		}

		// Token: 0x04000A33 RID: 2611
		public const string CategoryId = "FaceGenHotkeyCategory";

		// Token: 0x04000A34 RID: 2612
		public const string Zoom = "Zoom";

		// Token: 0x04000A35 RID: 2613
		public const string ControllerRotationAxis = "CameraAxisX";

		// Token: 0x04000A36 RID: 2614
		public const string ControllerCameraUpDownAxis = "CameraAxisY";

		// Token: 0x04000A37 RID: 2615
		public const string Rotate = "Rotate";

		// Token: 0x04000A38 RID: 2616
		public const string Ascend = "Ascend";

		// Token: 0x04000A39 RID: 2617
		public const string Copy = "Copy";

		// Token: 0x04000A3A RID: 2618
		public const int ControllerZoomIn = 55;

		// Token: 0x04000A3B RID: 2619
		public const string Paste = "Paste";

		// Token: 0x04000A3C RID: 2620
		public const int ControllerZoomOut = 56;
	}
}
