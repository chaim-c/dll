using System;
using System.Collections.Generic;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022A RID: 554
	public sealed class MapHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001E9E RID: 7838 RVA: 0x0006D5A3 File Offset: 0x0006B7A3
		public MapHotKeyCategory() : base("MapHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0006D5C8 File Offset: 0x0006B7C8
		private void RegisterHotKeys()
		{
			List<Key> list = new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRDown)
			};
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableTouchpadMouse) != 0f)
			{
				list.Add(new Key(InputKey.ControllerLOptionTap));
			}
			base.RegisterHotKey(new HotKey("MapClick", "MapHotKeyCategory", list, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.LeftAlt),
				new Key(InputKey.ControllerLBumper)
			};
			base.RegisterHotKey(new HotKey("MapFollowModifier", "MapHotKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.ControllerRRight)
			};
			base.RegisterHotKey(new HotKey("MapChangeCursorMode", "MapHotKeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0006D6A0 File Offset: 0x0006B8A0
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(49, "PartyMoveUp", "MapHotKeyCategory", InputKey.Up, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(50, "PartyMoveDown", "MapHotKeyCategory", InputKey.Down, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(51, "PartyMoveRight", "MapHotKeyCategory", InputKey.Right, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(52, "PartyMoveLeft", "MapHotKeyCategory", InputKey.Left, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(53, "QuickSave", "MapHotKeyCategory", InputKey.F5, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(54, "MapFastMove", "MapHotKeyCategory", InputKey.LeftShift, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(55, "MapZoomIn", "MapHotKeyCategory", InputKey.MouseScrollUp, InputKey.ControllerRTrigger, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(56, "MapZoomOut", "MapHotKeyCategory", InputKey.MouseScrollDown, InputKey.ControllerLTrigger, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(57, "MapRotateLeft", "MapHotKeyCategory", InputKey.Q, InputKey.Invalid, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(58, "MapRotateRight", "MapHotKeyCategory", InputKey.E, InputKey.Invalid, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(59, "MapTimeStop", "MapHotKeyCategory", InputKey.D1, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(60, "MapTimeNormal", "MapHotKeyCategory", InputKey.D2, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(61, "MapTimeFastForward", "MapHotKeyCategory", InputKey.D3, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(62, "MapTimeTogglePause", "MapHotKeyCategory", InputKey.Space, InputKey.ControllerRLeft, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(63, "MapCameraFollowMode", "MapHotKeyCategory", InputKey.Invalid, InputKey.ControllerLThumb, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(64, "MapToggleFastForward", "MapHotKeyCategory", InputKey.Invalid, InputKey.ControllerRBumper, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(65, "MapTrackSettlement", "MapHotKeyCategory", InputKey.Invalid, InputKey.ControllerRThumb, GameKeyMainCategories.CampaignMapCategory), true);
			base.RegisterGameKey(new GameKey(66, "MapGoToEncylopedia", "MapHotKeyCategory", InputKey.Invalid, InputKey.ControllerLOption, GameKeyMainCategories.CampaignMapCategory), true);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0006D90C File Offset: 0x0006BB0C
		private void RegisterGameAxisKeys()
		{
			GameKey gameKey = new GameKey(45, "MapMoveUp", "MapHotKeyCategory", InputKey.W, GameKeyMainCategories.CampaignMapCategory);
			GameKey gameKey2 = new GameKey(46, "MapMoveDown", "MapHotKeyCategory", InputKey.S, GameKeyMainCategories.CampaignMapCategory);
			GameKey gameKey3 = new GameKey(47, "MapMoveRight", "MapHotKeyCategory", InputKey.D, GameKeyMainCategories.CampaignMapCategory);
			GameKey gameKey4 = new GameKey(48, "MapMoveLeft", "MapHotKeyCategory", InputKey.A, GameKeyMainCategories.CampaignMapCategory);
			base.RegisterGameKey(gameKey, true);
			base.RegisterGameKey(gameKey2, true);
			base.RegisterGameKey(gameKey4, true);
			base.RegisterGameKey(gameKey3, true);
			base.RegisterGameAxisKey(new GameAxisKey("MapMovementAxisX", InputKey.ControllerLStick, gameKey3, gameKey4, GameAxisKey.AxisType.X), true);
			base.RegisterGameAxisKey(new GameAxisKey("MapMovementAxisY", InputKey.ControllerLStick, gameKey, gameKey2, GameAxisKey.AxisType.Y), true);
		}

		// Token: 0x04000ADC RID: 2780
		public const string CategoryId = "MapHotKeyCategory";

		// Token: 0x04000ADD RID: 2781
		public const int QuickSave = 53;

		// Token: 0x04000ADE RID: 2782
		public const int PartyMoveUp = 49;

		// Token: 0x04000ADF RID: 2783
		public const int PartyMoveLeft = 52;

		// Token: 0x04000AE0 RID: 2784
		public const int PartyMoveDown = 50;

		// Token: 0x04000AE1 RID: 2785
		public const int PartyMoveRight = 51;

		// Token: 0x04000AE2 RID: 2786
		public const int MapMoveUp = 45;

		// Token: 0x04000AE3 RID: 2787
		public const int MapMoveDown = 46;

		// Token: 0x04000AE4 RID: 2788
		public const int MapMoveLeft = 48;

		// Token: 0x04000AE5 RID: 2789
		public const int MapMoveRight = 47;

		// Token: 0x04000AE6 RID: 2790
		public const string MovementAxisX = "MapMovementAxisX";

		// Token: 0x04000AE7 RID: 2791
		public const string MovementAxisY = "MapMovementAxisY";

		// Token: 0x04000AE8 RID: 2792
		public const int MapFastMove = 54;

		// Token: 0x04000AE9 RID: 2793
		public const int MapZoomIn = 55;

		// Token: 0x04000AEA RID: 2794
		public const int MapZoomOut = 56;

		// Token: 0x04000AEB RID: 2795
		public const int MapRotateLeft = 57;

		// Token: 0x04000AEC RID: 2796
		public const int MapRotateRight = 58;

		// Token: 0x04000AED RID: 2797
		public const int MapCameraFollowMode = 63;

		// Token: 0x04000AEE RID: 2798
		public const int MapToggleFastForward = 64;

		// Token: 0x04000AEF RID: 2799
		public const int MapTrackSettlement = 65;

		// Token: 0x04000AF0 RID: 2800
		public const int MapGoToEncylopedia = 66;

		// Token: 0x04000AF1 RID: 2801
		public const string MapClick = "MapClick";

		// Token: 0x04000AF2 RID: 2802
		public const string MapFollowModifier = "MapFollowModifier";

		// Token: 0x04000AF3 RID: 2803
		public const string MapChangeCursorMode = "MapChangeCursorMode";

		// Token: 0x04000AF4 RID: 2804
		public const int MapTimeStop = 59;

		// Token: 0x04000AF5 RID: 2805
		public const int MapTimeNormal = 60;

		// Token: 0x04000AF6 RID: 2806
		public const int MapTimeFastForward = 61;

		// Token: 0x04000AF7 RID: 2807
		public const int MapTimeTogglePause = 62;
	}
}
