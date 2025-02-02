using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.Engine.InputSystem
{
	// Token: 0x020000A6 RID: 166
	public class CheatsHotKeyCategory : GameKeyContext
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x0000DE68 File Offset: 0x0000C068
		public CheatsHotKeyCategory() : base("Cheats", 0, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterCheatHotkey("Pause", InputKey.F11, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyIncreaseCameraSpeed", InputKey.Up, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyDecreaseCameraSpeed", InputKey.Down, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("ResetCameraSpeed", InputKey.MiddleMouseButton, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyIncreaseSlowMotionFactor", InputKey.NumpadPlus, HotKey.Modifiers.Shift, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyDecreaseSlowMotionFactor", InputKey.NumpadMinus, HotKey.Modifiers.Shift, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("EnterSlowMotion", InputKey.F9, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeySwitchAgentToAi", InputKey.F5, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyControlFollowedAgent", InputKey.Numpad5, HotKey.Modifiers.Alt | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyHealYourSelf", InputKey.H, HotKey.Modifiers.Control, HotKey.Modifiers.Shift);
			this.RegisterCheatHotkey("MissionScreenHotkeyHealYourHorse", InputKey.H, HotKey.Modifiers.Shift | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillEnemyAgent", InputKey.F4, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillAllEnemyAgents", InputKey.F4, HotKey.Modifiers.Alt | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillEnemyHorse", InputKey.F4, HotKey.Modifiers.Shift | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillAllEnemyHorses", InputKey.F4, HotKey.Modifiers.Shift | HotKey.Modifiers.Alt | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillFriendlyAgent", InputKey.F2, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillAllFriendlyAgents", InputKey.F2, HotKey.Modifiers.Alt | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillFriendlyHorse", InputKey.F2, HotKey.Modifiers.Shift | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillAllFriendlyHorses", InputKey.F2, HotKey.Modifiers.Shift | HotKey.Modifiers.Alt | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillYourSelf", InputKey.F3, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyKillYourHorse", InputKey.F3, HotKey.Modifiers.Shift | HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyGhostCam", InputKey.K, HotKey.Modifiers.Control, HotKey.Modifiers.None);
			this.RegisterCheatHotkey("MissionScreenHotkeyTeleportMainAgent", InputKey.X, HotKey.Modifiers.Alt, HotKey.Modifiers.None);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		private void RegisterCheatHotkey(string id, InputKey hotkeyKey, HotKey.Modifiers modifiers, HotKey.Modifiers negativeModifiers = HotKey.Modifiers.None)
		{
			base.RegisterHotKey(new HotKey(id, "Cheats", hotkeyKey, modifiers, negativeModifiers), true);
		}

		// Token: 0x0400020A RID: 522
		public const string CategoryId = "Cheats";

		// Token: 0x0400020B RID: 523
		public const string MissionScreenHotkeyIncreaseCameraSpeed = "MissionScreenHotkeyIncreaseCameraSpeed";

		// Token: 0x0400020C RID: 524
		public const string MissionScreenHotkeyDecreaseCameraSpeed = "MissionScreenHotkeyDecreaseCameraSpeed";

		// Token: 0x0400020D RID: 525
		public const string ResetCameraSpeed = "ResetCameraSpeed";

		// Token: 0x0400020E RID: 526
		public const string MissionScreenHotkeyIncreaseSlowMotionFactor = "MissionScreenHotkeyIncreaseSlowMotionFactor";

		// Token: 0x0400020F RID: 527
		public const string MissionScreenHotkeyDecreaseSlowMotionFactor = "MissionScreenHotkeyDecreaseSlowMotionFactor";

		// Token: 0x04000210 RID: 528
		public const string EnterSlowMotion = "EnterSlowMotion";

		// Token: 0x04000211 RID: 529
		public const string Pause = "Pause";

		// Token: 0x04000212 RID: 530
		public const string MissionScreenHotkeyHealYourSelf = "MissionScreenHotkeyHealYourSelf";

		// Token: 0x04000213 RID: 531
		public const string MissionScreenHotkeyHealYourHorse = "MissionScreenHotkeyHealYourHorse";

		// Token: 0x04000214 RID: 532
		public const string MissionScreenHotkeyKillEnemyAgent = "MissionScreenHotkeyKillEnemyAgent";

		// Token: 0x04000215 RID: 533
		public const string MissionScreenHotkeyKillAllEnemyAgents = "MissionScreenHotkeyKillAllEnemyAgents";

		// Token: 0x04000216 RID: 534
		public const string MissionScreenHotkeyKillEnemyHorse = "MissionScreenHotkeyKillEnemyHorse";

		// Token: 0x04000217 RID: 535
		public const string MissionScreenHotkeyKillAllEnemyHorses = "MissionScreenHotkeyKillAllEnemyHorses";

		// Token: 0x04000218 RID: 536
		public const string MissionScreenHotkeyKillFriendlyAgent = "MissionScreenHotkeyKillFriendlyAgent";

		// Token: 0x04000219 RID: 537
		public const string MissionScreenHotkeyKillAllFriendlyAgents = "MissionScreenHotkeyKillAllFriendlyAgents";

		// Token: 0x0400021A RID: 538
		public const string MissionScreenHotkeyKillFriendlyHorse = "MissionScreenHotkeyKillFriendlyHorse";

		// Token: 0x0400021B RID: 539
		public const string MissionScreenHotkeyKillAllFriendlyHorses = "MissionScreenHotkeyKillAllFriendlyHorses";

		// Token: 0x0400021C RID: 540
		public const string MissionScreenHotkeyKillYourSelf = "MissionScreenHotkeyKillYourSelf";

		// Token: 0x0400021D RID: 541
		public const string MissionScreenHotkeyKillYourHorse = "MissionScreenHotkeyKillYourHorse";

		// Token: 0x0400021E RID: 542
		public const string MissionScreenHotkeyGhostCam = "MissionScreenHotkeyGhostCam";

		// Token: 0x0400021F RID: 543
		public const string MissionScreenHotkeySwitchAgentToAi = "MissionScreenHotkeySwitchAgentToAi";

		// Token: 0x04000220 RID: 544
		public const string MissionScreenHotkeyControlFollowedAgent = "MissionScreenHotkeyControlFollowedAgent";

		// Token: 0x04000221 RID: 545
		public const string MissionScreenHotkeyTeleportMainAgent = "MissionScreenHotkeyTeleportMainAgent";
	}
}
