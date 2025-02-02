using System;
using System.Collections.Generic;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade.GameKeyCategory
{
	// Token: 0x020003C2 RID: 962
	public sealed class PhotoModeHotKeyCategory : GameKeyContext
	{
		// Token: 0x0600333A RID: 13114 RVA: 0x000D5575 File Offset: 0x000D3775
		public PhotoModeHotKeyCategory() : base("PhotoModeHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000D5598 File Offset: 0x000D3798
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.LeftShift),
				new Key(InputKey.ControllerRTrigger)
			};
			base.RegisterHotKey(new HotKey("FasterCamera", "PhotoModeHotKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000D55E4 File Offset: 0x000D37E4
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(90, "HideUI", "PhotoModeHotKeyCategory", InputKey.H, InputKey.ControllerRUp, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(91, "CameraRollLeft", "PhotoModeHotKeyCategory", InputKey.Q, InputKey.ControllerLBumper, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(92, "CameraRollRight", "PhotoModeHotKeyCategory", InputKey.E, InputKey.ControllerRBumper, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(95, "ToggleCameraFollowMode", "PhotoModeHotKeyCategory", InputKey.V, InputKey.ControllerRLeft, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(93, "TakePicture", "PhotoModeHotKeyCategory", InputKey.Enter, InputKey.ControllerRDown, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(94, "TakePictureWithAdditionalPasses", "PhotoModeHotKeyCategory", InputKey.BackSpace, InputKey.ControllerRBumper, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(96, "ToggleMouse", "PhotoModeHotKeyCategory", InputKey.C, InputKey.ControllerLThumb, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(97, "ToggleVignette", "PhotoModeHotKeyCategory", InputKey.X, InputKey.ControllerRThumb, GameKeyMainCategories.PhotoModeCategory), true);
			base.RegisterGameKey(new GameKey(98, "ToggleCharacters", "PhotoModeHotKeyCategory", InputKey.B, InputKey.ControllerRRight, GameKeyMainCategories.PhotoModeCategory), true);
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableTouchpadMouse) != 0f)
			{
				base.RegisterGameKey(new GameKey(105, "Reset", "PhotoModeHotKeyCategory", InputKey.T, InputKey.ControllerLOptionTap, GameKeyMainCategories.PhotoModeCategory), true);
				return;
			}
			base.RegisterGameKey(new GameKey(105, "Reset", "PhotoModeHotKeyCategory", InputKey.T, InputKey.ControllerLOption, GameKeyMainCategories.PhotoModeCategory), true);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000D578C File Offset: 0x000D398C
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04001636 RID: 5686
		public const string CategoryId = "PhotoModeHotKeyCategory";

		// Token: 0x04001637 RID: 5687
		public const int HideUI = 90;

		// Token: 0x04001638 RID: 5688
		public const int CameraRollLeft = 91;

		// Token: 0x04001639 RID: 5689
		public const int CameraRollRight = 92;

		// Token: 0x0400163A RID: 5690
		public const int ToggleCameraFollowMode = 95;

		// Token: 0x0400163B RID: 5691
		public const int TakePicture = 93;

		// Token: 0x0400163C RID: 5692
		public const int TakePictureWithAdditionalPasses = 94;

		// Token: 0x0400163D RID: 5693
		public const int ToggleMouse = 96;

		// Token: 0x0400163E RID: 5694
		public const int ToggleVignette = 97;

		// Token: 0x0400163F RID: 5695
		public const int ToggleCharacters = 98;

		// Token: 0x04001640 RID: 5696
		public const int Reset = 105;

		// Token: 0x04001641 RID: 5697
		public const string FasterCamera = "FasterCamera";
	}
}
