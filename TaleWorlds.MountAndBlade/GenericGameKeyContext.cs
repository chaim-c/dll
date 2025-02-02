using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000227 RID: 551
	public sealed class GenericGameKeyContext : GameKeyContext
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0006D073 File Offset: 0x0006B273
		// (set) Token: 0x06001E8F RID: 7823 RVA: 0x0006D07A File Offset: 0x0006B27A
		public static GenericGameKeyContext Current { get; private set; }

		// Token: 0x06001E90 RID: 7824 RVA: 0x0006D082 File Offset: 0x0006B282
		public GenericGameKeyContext() : base("Generic", 108, GameKeyContext.GameKeyContextType.Default)
		{
			GenericGameKeyContext.Current = this;
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0006D0AA File Offset: 0x0006B2AA
		private void RegisterHotKeys()
		{
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0006D0AC File Offset: 0x0006B2AC
		private void RegisterGameKeys()
		{
			GameKey gameKey = new GameKey(0, "Up", "Generic", InputKey.W, InputKey.ControllerLStickUp, GameKeyMainCategories.ActionCategory);
			GameKey gameKey2 = new GameKey(1, "Down", "Generic", InputKey.S, InputKey.ControllerLStickDown, GameKeyMainCategories.ActionCategory);
			GameKey gameKey3 = new GameKey(2, "Left", "Generic", InputKey.A, InputKey.ControllerLStickLeft, GameKeyMainCategories.ActionCategory);
			GameKey gameKey4 = new GameKey(3, "Right", "Generic", InputKey.D, InputKey.ControllerLStickRight, GameKeyMainCategories.ActionCategory);
			base.RegisterGameKey(gameKey, true);
			base.RegisterGameKey(gameKey2, true);
			base.RegisterGameKey(gameKey3, true);
			base.RegisterGameKey(gameKey4, true);
			base.RegisterGameKey(new GameKey(4, "Leave", "Generic", InputKey.Tab, InputKey.ControllerRRight, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(5, "ShowIndicators", "Generic", InputKey.LeftAlt, InputKey.ControllerLBumper, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameAxisKey(new GameAxisKey("MovementAxisX", InputKey.ControllerLStick, gameKey4, gameKey3, GameAxisKey.AxisType.X), true);
			base.RegisterGameAxisKey(new GameAxisKey("MovementAxisY", InputKey.ControllerLStick, gameKey, gameKey2, GameAxisKey.AxisType.Y), true);
			base.RegisterGameAxisKey(new GameAxisKey("CameraAxisX", InputKey.ControllerRStick, null, null, GameAxisKey.AxisType.X), true);
			base.RegisterGameAxisKey(new GameAxisKey("CameraAxisY", InputKey.ControllerRStick, null, null, GameAxisKey.AxisType.Y), true);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0006D1F7 File Offset: 0x0006B3F7
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000ABF RID: 2751
		public const string CategoryId = "Generic";

		// Token: 0x04000AC0 RID: 2752
		public const int Up = 0;

		// Token: 0x04000AC1 RID: 2753
		public const int Down = 1;

		// Token: 0x04000AC2 RID: 2754
		public const int Right = 3;

		// Token: 0x04000AC3 RID: 2755
		public const int Left = 2;

		// Token: 0x04000AC4 RID: 2756
		public const string MovementAxisX = "MovementAxisX";

		// Token: 0x04000AC5 RID: 2757
		public const string MovementAxisY = "MovementAxisY";

		// Token: 0x04000AC6 RID: 2758
		public const string CameraAxisX = "CameraAxisX";

		// Token: 0x04000AC7 RID: 2759
		public const string CameraAxisY = "CameraAxisY";

		// Token: 0x04000AC8 RID: 2760
		public const int Leave = 4;

		// Token: 0x04000AC9 RID: 2761
		public const int ShowIndicators = 5;
	}
}
