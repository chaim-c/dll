﻿using System;
using TaleWorlds.Library;

namespace TaleWorlds.InputSystem
{
	// Token: 0x0200000A RID: 10
	public static class Input
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004108 File Offset: 0x00002308
		public static bool IsPlaystation(this Input.ControllerTypes controllerType)
		{
			return controllerType.HasAnyFlag((Input.ControllerTypes)6);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004111 File Offset: 0x00002311
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004118 File Offset: 0x00002318
		public static InputState InputState { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004120 File Offset: 0x00002320
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004127 File Offset: 0x00002327
		public static IInputContext DebugInput { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000412F File Offset: 0x0000232F
		public static IInputManager InputManager
		{
			get
			{
				return Input._inputManager;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004136 File Offset: 0x00002336
		public static Vec2 Resolution
		{
			get
			{
				return Input._inputManager.GetResolution();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004142 File Offset: 0x00002342
		public static Vec2 DesktopResolution
		{
			get
			{
				return Input._inputManager.GetDesktopResolution();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000414E File Offset: 0x0000234E
		public static void Initialize(IInputManager inputManager, IInputContext debugInput)
		{
			Input._inputManager = inputManager;
			Input.InputState = new InputState();
			Input.keyData = new byte[256];
			Input.DebugInput = new EmptyInputContext();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004179 File Offset: 0x00002379
		public static void UpdateKeyData(byte[] keyData)
		{
			Input._inputManager.UpdateKeyData(keyData);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004186 File Offset: 0x00002386
		public static float GetMouseMoveX()
		{
			return Input._inputManager.GetMouseMoveX();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004192 File Offset: 0x00002392
		public static float GetMouseMoveY()
		{
			return Input._inputManager.GetMouseMoveY();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000419E File Offset: 0x0000239E
		public static float GetGyroX()
		{
			return Input._inputManager.GetGyroX();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000041AA File Offset: 0x000023AA
		public static float GetGyroY()
		{
			return Input._inputManager.GetGyroY();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000041B6 File Offset: 0x000023B6
		public static float GetGyroZ()
		{
			return Input._inputManager.GetGyroZ();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000041C2 File Offset: 0x000023C2
		public static Vec2 GetKeyState(InputKey key)
		{
			return Input._inputManager.GetKeyState(key);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000041CF File Offset: 0x000023CF
		public static bool IsKeyPressed(InputKey key)
		{
			return Input._inputManager.IsKeyPressed(key);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000041DC File Offset: 0x000023DC
		public static bool IsKeyDown(InputKey key)
		{
			return Input._inputManager.IsKeyDown(key);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000041E9 File Offset: 0x000023E9
		public static bool IsKeyDownImmediate(InputKey key)
		{
			return Input._inputManager.IsKeyDownImmediate(key);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041F6 File Offset: 0x000023F6
		public static bool IsKeyReleased(InputKey key)
		{
			return Input._inputManager.IsKeyReleased(key);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004203 File Offset: 0x00002403
		public static bool IsControlOrShiftNotDown()
		{
			return !InputKey.LeftControl.IsDown() && !InputKey.RightControl.IsDown() && !InputKey.LeftShift.IsDown() && !InputKey.RightShift.IsDown();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000422F File Offset: 0x0000242F
		public static bool IsMouseActive
		{
			get
			{
				return Input._inputManager.IsMouseActive();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000423B File Offset: 0x0000243B
		public static bool IsControllerConnected
		{
			get
			{
				return Input._inputManager.IsControllerConnected();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004247 File Offset: 0x00002447
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000424E File Offset: 0x0000244E
		public static bool IsGamepadActive
		{
			get
			{
				return Input._isGamepadActive;
			}
			private set
			{
				if (value != Input._isGamepadActive)
				{
					Input._isGamepadActive = value;
					Action onGamepadActiveStateChanged = Input.OnGamepadActiveStateChanged;
					if (onGamepadActiveStateChanged == null)
					{
						return;
					}
					onGamepadActiveStateChanged();
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000426D File Offset: 0x0000246D
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004274 File Offset: 0x00002474
		public static bool IsAnyTouchActive
		{
			get
			{
				return Input._isAnyTouchActive;
			}
			private set
			{
				if (value != Input._isAnyTouchActive)
				{
					Input._isAnyTouchActive = value;
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004284 File Offset: 0x00002484
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000428B File Offset: 0x0000248B
		public static Input.ControllerTypes ControllerType
		{
			get
			{
				return Input._controllerType;
			}
			private set
			{
				if (value != Input._controllerType)
				{
					Input._controllerType = value;
					Action<Input.ControllerTypes> onControllerTypeChanged = Input.OnControllerTypeChanged;
					if (onControllerTypeChanged == null)
					{
						return;
					}
					onControllerTypeChanged(value);
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000042AC File Offset: 0x000024AC
		public static int GetFirstKeyPressedInRange(int startKeyNo)
		{
			int result = -1;
			for (int i = startKeyNo; i < 256; i++)
			{
				if (Input.IsKeyPressed((InputKey)i))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000042D8 File Offset: 0x000024D8
		public static int GetFirstKeyDownInRange(int startKeyNo)
		{
			int result = -1;
			for (int i = startKeyNo; i < 256; i++)
			{
				if (Input.IsKeyDown((InputKey)i))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004304 File Offset: 0x00002504
		public static int GetFirstKeyReleasedInRange(int startKeyNo)
		{
			int result = -1;
			for (int i = startKeyNo; i < 256; i++)
			{
				if (Input.IsKeyReleased((InputKey)i))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004330 File Offset: 0x00002530
		public static void PressKey(InputKey key)
		{
			Input._inputManager.PressKey(key);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000433D File Offset: 0x0000253D
		public static void ClearKeys()
		{
			Input._inputManager.ClearKeys();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004349 File Offset: 0x00002549
		public static int GetVirtualKeyCode(InputKey key)
		{
			return Input._inputManager.GetVirtualKeyCode(key);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004356 File Offset: 0x00002556
		public static bool IsDown(this InputKey key)
		{
			return Input.IsKeyDown(key);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000435E File Offset: 0x0000255E
		public static bool IsPressed(this InputKey key)
		{
			return Input.IsKeyPressed(key);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004366 File Offset: 0x00002566
		public static bool IsReleased(this InputKey key)
		{
			return Input.IsKeyReleased(key);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000436E File Offset: 0x0000256E
		public static void SetClipboardText(string text)
		{
			Input._inputManager.SetClipboardText(text);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000437B File Offset: 0x0000257B
		public static string GetClipboardText()
		{
			return Input._inputManager.GetClipboardText();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004387 File Offset: 0x00002587
		public static float MouseMoveX
		{
			get
			{
				return Input._inputManager.GetMouseMoveX();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004393 File Offset: 0x00002593
		public static float MouseMoveY
		{
			get
			{
				return Input._inputManager.GetMouseMoveY();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000439F File Offset: 0x0000259F
		public static float GyroX
		{
			get
			{
				return Input._inputManager.GetGyroX();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000043AB File Offset: 0x000025AB
		public static float GyroY
		{
			get
			{
				return Input._inputManager.GetGyroY();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000043B7 File Offset: 0x000025B7
		public static float GyroZ
		{
			get
			{
				return Input._inputManager.GetGyroZ();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000043C3 File Offset: 0x000025C3
		public static float MouseSensitivity
		{
			get
			{
				return Input._inputManager.GetMouseSensitivity();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000043CF File Offset: 0x000025CF
		public static float DeltaMouseScroll
		{
			get
			{
				return Input._inputManager.GetMouseDeltaZ();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000043DB File Offset: 0x000025DB
		public static Vec2 MousePositionRanged
		{
			get
			{
				return Input.InputState.MousePositionRanged;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000043E7 File Offset: 0x000025E7
		public static Vec2 MousePositionPixel
		{
			get
			{
				return Input.InputState.MousePositionPixel;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000043F4 File Offset: 0x000025F4
		public static void Update()
		{
			float mousePositionX = Input._inputManager.GetMousePositionX();
			float mousePositionY = Input._inputManager.GetMousePositionY();
			float mouseScrollValue = Input._inputManager.GetMouseScrollValue();
			Input.IsMousePositionUpdated = Input.InputState.UpdateMousePosition(mousePositionX, mousePositionY);
			Input.IsMouseScrollChanged = Input.InputState.UpdateMouseScroll(mouseScrollValue);
			Input.IsGamepadActive = (Input.IsControllerConnected && !Input.IsMouseActive);
			Input.IsAnyTouchActive = Input._inputManager.IsAnyTouchActive();
			Input.ControllerType = Input._inputManager.GetControllerType();
			Input.UpdateKeyData(Input.keyData);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004482 File Offset: 0x00002682
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004489 File Offset: 0x00002689
		public static bool IsMousePositionUpdated { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004491 File Offset: 0x00002691
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004498 File Offset: 0x00002698
		public static bool IsMouseScrollChanged { get; private set; }

		// Token: 0x06000102 RID: 258 RVA: 0x000044A0 File Offset: 0x000026A0
		public static bool IsControllerKey(InputKey key)
		{
			switch (key)
			{
			case InputKey.Escape:
			case InputKey.D1:
			case InputKey.D2:
			case InputKey.D3:
			case InputKey.D4:
			case InputKey.D5:
			case InputKey.D6:
			case InputKey.D7:
			case InputKey.D8:
			case InputKey.D9:
			case InputKey.D0:
			case InputKey.Minus:
			case InputKey.Equals:
			case InputKey.BackSpace:
			case InputKey.Tab:
			case InputKey.Q:
			case InputKey.W:
			case InputKey.E:
			case InputKey.R:
			case InputKey.T:
			case InputKey.Y:
			case InputKey.U:
			case InputKey.I:
			case InputKey.O:
			case InputKey.P:
			case InputKey.OpenBraces:
			case InputKey.CloseBraces:
			case InputKey.Enter:
			case InputKey.LeftControl:
			case InputKey.A:
			case InputKey.S:
			case InputKey.D:
			case InputKey.F:
			case InputKey.G:
			case InputKey.H:
			case InputKey.J:
			case InputKey.K:
			case InputKey.L:
			case InputKey.SemiColon:
			case InputKey.Apostrophe:
			case InputKey.Tilde:
			case InputKey.LeftShift:
			case InputKey.BackSlash:
			case InputKey.Z:
			case InputKey.X:
			case InputKey.C:
			case InputKey.V:
			case InputKey.B:
			case InputKey.N:
			case InputKey.M:
			case InputKey.Comma:
			case InputKey.Period:
			case InputKey.Slash:
			case InputKey.RightShift:
			case InputKey.NumpadMultiply:
			case InputKey.LeftAlt:
			case InputKey.Space:
			case InputKey.CapsLock:
			case InputKey.F1:
			case InputKey.F2:
			case InputKey.F3:
			case InputKey.F4:
			case InputKey.F5:
			case InputKey.F6:
			case InputKey.F7:
			case InputKey.F8:
			case InputKey.F9:
			case InputKey.F10:
			case InputKey.Numpad7:
			case InputKey.Numpad8:
			case InputKey.Numpad9:
			case InputKey.NumpadMinus:
			case InputKey.Numpad4:
			case InputKey.Numpad5:
			case InputKey.Numpad6:
			case InputKey.NumpadPlus:
			case InputKey.Numpad1:
			case InputKey.Numpad2:
			case InputKey.Numpad3:
			case InputKey.Numpad0:
			case InputKey.NumpadPeriod:
			case InputKey.Extended:
			case InputKey.F11:
			case InputKey.F12:
			case InputKey.NumpadEnter:
			case InputKey.RightControl:
			case InputKey.NumpadSlash:
			case InputKey.RightAlt:
			case InputKey.NumLock:
			case InputKey.Home:
			case InputKey.Up:
			case InputKey.PageUp:
			case InputKey.Left:
			case InputKey.Right:
			case InputKey.End:
			case InputKey.Down:
			case InputKey.PageDown:
			case InputKey.Insert:
			case InputKey.Delete:
			case InputKey.LeftMouseButton:
			case InputKey.RightMouseButton:
			case InputKey.MiddleMouseButton:
			case InputKey.X1MouseButton:
			case InputKey.X2MouseButton:
			case InputKey.MouseScrollUp:
			case InputKey.MouseScrollDown:
				return false;
			}
			return true;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000048B6 File Offset: 0x00002AB6
		public static void SetMousePosition(int x, int y)
		{
			Input._inputManager.SetCursorPosition(x, y);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000048C4 File Offset: 0x00002AC4
		public static void SetCursorFriction(float frictionValue)
		{
			Input._inputManager.SetCursorFriction(frictionValue);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000048D1 File Offset: 0x00002AD1
		public static InputKey[] GetClickKeys()
		{
			return Input._inputManager.GetClickKeys();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000048DD File Offset: 0x00002ADD
		public static void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements)
		{
			Input._inputManager.SetRumbleEffect(lowFrequencyLevels, lowFrequencyDurations, numLowFrequencyElements, highFrequencyLevels, highFrequencyDurations, numHighFrequencyElements);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000048F1 File Offset: 0x00002AF1
		public static void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength)
		{
			Input._inputManager.SetTriggerFeedback(leftTriggerPosition, leftTriggerStrength, rightTriggerPosition, rightTriggerStrength);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004901 File Offset: 0x00002B01
		public static void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength)
		{
			Input._inputManager.SetTriggerWeaponEffect(leftStartPosition, leftEnd_position, leftStrength, rightStartPosition, rightEndPosition, rightStrength);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004918 File Offset: 0x00002B18
		public static void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements)
		{
			Input._inputManager.SetTriggerVibration(leftTriggerAmplitudes, leftTriggerFrequencies, leftTriggerDurations, numLeftTriggerElements, rightTriggerAmplitudes, rightTriggerFrequencies, rightTriggerDurations, numRightTriggerElements);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000493B File Offset: 0x00002B3B
		public static void SetLightbarColor(float red, float green, float blue)
		{
			Input._inputManager.SetLightbarColor(red, green, blue);
		}

		// Token: 0x04000027 RID: 39
		public const int NumberOfKeys = 256;

		// Token: 0x04000028 RID: 40
		private static byte[] keyData;

		// Token: 0x04000029 RID: 41
		private static IInputManager _inputManager;

		// Token: 0x0400002A RID: 42
		public static Action OnGamepadActiveStateChanged;

		// Token: 0x0400002B RID: 43
		private static bool _isGamepadActive;

		// Token: 0x0400002C RID: 44
		private static bool _isAnyTouchActive;

		// Token: 0x0400002D RID: 45
		public static Action<Input.ControllerTypes> OnControllerTypeChanged;

		// Token: 0x0400002E RID: 46
		private static Input.ControllerTypes _controllerType;

		// Token: 0x02000014 RID: 20
		public enum ControllerTypes
		{
			// Token: 0x0400015F RID: 351
			None,
			// Token: 0x04000160 RID: 352
			Xbox,
			// Token: 0x04000161 RID: 353
			PlayStationDualShock,
			// Token: 0x04000162 RID: 354
			PlayStationDualSense = 4
		}
	}
}
