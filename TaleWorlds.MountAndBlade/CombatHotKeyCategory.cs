using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021F RID: 543
	public sealed class CombatHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001E74 RID: 7796 RVA: 0x0006C3D2 File Offset: 0x0006A5D2
		public CombatHotKeyCategory() : base("CombatHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0006C3F4 File Offset: 0x0006A5F4
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("DeploymentCameraIsActive", "CombatHotKeyCategory", InputKey.MiddleMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ToggleZoom", "CombatHotKeyCategory", InputKey.ControllerRThumb, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ControllerEquipDropWeapon1", "CombatHotKeyCategory", InputKey.ControllerRRight, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ControllerEquipDropWeapon2", "CombatHotKeyCategory", InputKey.ControllerRUp, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ControllerEquipDropWeapon3", "CombatHotKeyCategory", InputKey.ControllerRLeft, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ControllerEquipDropWeapon4", "CombatHotKeyCategory", InputKey.ControllerRDown, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkSelectFirstCategory", "CombatHotKeyCategory", new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRLeft)
			}, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkSelectSecondCategory", "CombatHotKeyCategory", new List<Key>
			{
				new Key(InputKey.RightMouseButton),
				new Key(InputKey.ControllerRRight)
			}, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkCloseMenu", "CombatHotKeyCategory", InputKey.ControllerRThumb, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkItem1", "CombatHotKeyCategory", InputKey.ControllerRUp, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkItem2", "CombatHotKeyCategory", InputKey.ControllerRRight, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkItem3", "CombatHotKeyCategory", InputKey.ControllerRDown, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("CheerBarkItem4", "CombatHotKeyCategory", InputKey.ControllerRLeft, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ForfeitSpawn", "CombatHotKeyCategory", new List<Key>
			{
				new Key(InputKey.X),
				new Key(InputKey.ControllerRLeft)
			}, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0006C5F4 File Offset: 0x0006A7F4
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(9, "Attack", "CombatHotKeyCategory", InputKey.LeftMouseButton, InputKey.ControllerRTrigger, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(10, "Defend", "CombatHotKeyCategory", InputKey.RightMouseButton, InputKey.ControllerLTrigger, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(11, "EquipPrimaryWeapon", "CombatHotKeyCategory", InputKey.MouseScrollUp, InputKey.Invalid, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(12, "EquipSecondaryWeapon", "CombatHotKeyCategory", InputKey.MouseScrollDown, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(13, "Action", "CombatHotKeyCategory", InputKey.F, InputKey.ControllerRUp, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(14, "Jump", "CombatHotKeyCategory", InputKey.Space, InputKey.ControllerRDown, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(15, "Crouch", "CombatHotKeyCategory", InputKey.Z, InputKey.ControllerLDown, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(16, "Kick", "CombatHotKeyCategory", InputKey.E, InputKey.ControllerRLeft, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(17, "ToggleWeaponMode", "CombatHotKeyCategory", InputKey.X, InputKey.Invalid, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(18, "EquipWeapon1", "CombatHotKeyCategory", InputKey.Numpad1, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(19, "EquipWeapon2", "CombatHotKeyCategory", InputKey.Numpad2, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(20, "EquipWeapon3", "CombatHotKeyCategory", InputKey.Numpad3, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(21, "EquipWeapon4", "CombatHotKeyCategory", InputKey.Numpad4, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(22, "DropWeapon", "CombatHotKeyCategory", InputKey.G, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(23, "SheathWeapon", "CombatHotKeyCategory", InputKey.BackSlash, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(24, "Zoom", "CombatHotKeyCategory", InputKey.LeftShift, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(25, "ViewCharacter", "CombatHotKeyCategory", InputKey.Tilde, InputKey.ControllerLLeft, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(26, "LockTarget", "CombatHotKeyCategory", InputKey.MiddleMouseButton, InputKey.ControllerRThumb, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(27, "CameraToggle", "CombatHotKeyCategory", InputKey.R, InputKey.ControllerLThumb, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(28, "MissionScreenHotkeyCameraZoomIn", "CombatHotKeyCategory", InputKey.NumpadPlus, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(29, "MissionScreenHotkeyCameraZoomOut", "CombatHotKeyCategory", InputKey.NumpadMinus, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(30, "ToggleWalkMode", "CombatHotKeyCategory", InputKey.CapsLock, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(31, "Cheer", "CombatHotKeyCategory", InputKey.O, InputKey.ControllerLUp, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(33, "PushToTalk", "CombatHotKeyCategory", InputKey.V, InputKey.ControllerLRight, GameKeyMainCategories.ActionCategory), true);
			base.RegisterGameKey(new GameKey(34, "EquipmentSwitch", "CombatHotKeyCategory", InputKey.U, InputKey.ControllerRBumper, GameKeyMainCategories.ActionCategory), true);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0006C955 File Offset: 0x0006AB55
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x040009F9 RID: 2553
		public const string CategoryId = "CombatHotKeyCategory";

		// Token: 0x040009FA RID: 2554
		public const int MissionScreenHotkeyCameraZoomIn = 28;

		// Token: 0x040009FB RID: 2555
		public const int MissionScreenHotkeyCameraZoomOut = 29;

		// Token: 0x040009FC RID: 2556
		public const int Action = 13;

		// Token: 0x040009FD RID: 2557
		public const int Jump = 14;

		// Token: 0x040009FE RID: 2558
		public const int Crouch = 15;

		// Token: 0x040009FF RID: 2559
		public const int Attack = 9;

		// Token: 0x04000A00 RID: 2560
		public const int Defend = 10;

		// Token: 0x04000A01 RID: 2561
		public const int Kick = 16;

		// Token: 0x04000A02 RID: 2562
		public const int ToggleWeaponMode = 17;

		// Token: 0x04000A03 RID: 2563
		public const int ToggleWalkMode = 30;

		// Token: 0x04000A04 RID: 2564
		public const int EquipWeapon1 = 18;

		// Token: 0x04000A05 RID: 2565
		public const int EquipWeapon2 = 19;

		// Token: 0x04000A06 RID: 2566
		public const int EquipWeapon3 = 20;

		// Token: 0x04000A07 RID: 2567
		public const int EquipWeapon4 = 21;

		// Token: 0x04000A08 RID: 2568
		public const int EquipPrimaryWeapon = 11;

		// Token: 0x04000A09 RID: 2569
		public const int EquipSecondaryWeapon = 12;

		// Token: 0x04000A0A RID: 2570
		public const int DropWeapon = 22;

		// Token: 0x04000A0B RID: 2571
		public const int SheathWeapon = 23;

		// Token: 0x04000A0C RID: 2572
		public const int Zoom = 24;

		// Token: 0x04000A0D RID: 2573
		public const int ViewCharacter = 25;

		// Token: 0x04000A0E RID: 2574
		public const int LockTarget = 26;

		// Token: 0x04000A0F RID: 2575
		public const int CameraToggle = 27;

		// Token: 0x04000A10 RID: 2576
		public const int Cheer = 31;

		// Token: 0x04000A11 RID: 2577
		public const int PushToTalk = 33;

		// Token: 0x04000A12 RID: 2578
		public const int EquipmentSwitch = 34;

		// Token: 0x04000A13 RID: 2579
		public const string DeploymentCameraIsActive = "DeploymentCameraIsActive";

		// Token: 0x04000A14 RID: 2580
		public const string ToggleZoom = "ToggleZoom";

		// Token: 0x04000A15 RID: 2581
		public const string ControllerEquipDropRRight = "ControllerEquipDropWeapon1";

		// Token: 0x04000A16 RID: 2582
		public const string ControllerEquipDropRUp = "ControllerEquipDropWeapon2";

		// Token: 0x04000A17 RID: 2583
		public const string ControllerEquipDropRLeft = "ControllerEquipDropWeapon3";

		// Token: 0x04000A18 RID: 2584
		public const string ControllerEquipDropRDown = "ControllerEquipDropWeapon4";

		// Token: 0x04000A19 RID: 2585
		public const string CheerBarkSelectFirstCategory = "CheerBarkSelectFirstCategory";

		// Token: 0x04000A1A RID: 2586
		public const string CheerBarkSelectSecondCategory = "CheerBarkSelectSecondCategory";

		// Token: 0x04000A1B RID: 2587
		public const string CheerBarkCloseMenu = "CheerBarkCloseMenu";

		// Token: 0x04000A1C RID: 2588
		public const string CheerBarkItem1 = "CheerBarkItem1";

		// Token: 0x04000A1D RID: 2589
		public const string CheerBarkItem2 = "CheerBarkItem2";

		// Token: 0x04000A1E RID: 2590
		public const string CheerBarkItem3 = "CheerBarkItem3";

		// Token: 0x04000A1F RID: 2591
		public const string CheerBarkItem4 = "CheerBarkItem4";

		// Token: 0x04000A20 RID: 2592
		public const string ForfeitSpawn = "ForfeitSpawn";
	}
}
