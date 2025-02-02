using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200003A RID: 58
	[ApplicationInterfaceBase]
	internal interface IInput
	{
		// Token: 0x0600051C RID: 1308
		[EngineMethod("clear_keys", false)]
		void ClearKeys();

		// Token: 0x0600051D RID: 1309
		[EngineMethod("get_mouse_sensitivity", false)]
		float GetMouseSensitivity();

		// Token: 0x0600051E RID: 1310
		[EngineMethod("get_mouse_delta_z", false)]
		float GetMouseDeltaZ();

		// Token: 0x0600051F RID: 1311
		[EngineMethod("is_mouse_active", false)]
		bool IsMouseActive();

		// Token: 0x06000520 RID: 1312
		[EngineMethod("is_controller_connected", false)]
		bool IsControllerConnected();

		// Token: 0x06000521 RID: 1313
		[EngineMethod("set_rumble_effect", false)]
		void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements);

		// Token: 0x06000522 RID: 1314
		[EngineMethod("set_trigger_feedback", false)]
		void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength);

		// Token: 0x06000523 RID: 1315
		[EngineMethod("set_trigger_weapon_effect", false)]
		void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength);

		// Token: 0x06000524 RID: 1316
		[EngineMethod("set_trigger_vibration", false)]
		void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements);

		// Token: 0x06000525 RID: 1317
		[EngineMethod("set_lightbar_color", false)]
		void SetLightbarColor(float red, float green, float blue);

		// Token: 0x06000526 RID: 1318
		[EngineMethod("press_key", false)]
		void PressKey(InputKey key);

		// Token: 0x06000527 RID: 1319
		[EngineMethod("get_virtual_key_code", false)]
		int GetVirtualKeyCode(InputKey key);

		// Token: 0x06000528 RID: 1320
		[EngineMethod("get_controller_type", false)]
		int GetControllerType();

		// Token: 0x06000529 RID: 1321
		[EngineMethod("set_clipboard_text", false)]
		void SetClipboardText(string text);

		// Token: 0x0600052A RID: 1322
		[EngineMethod("get_clipboard_text", false)]
		string GetClipboardText();

		// Token: 0x0600052B RID: 1323
		[EngineMethod("update_key_data", false)]
		void UpdateKeyData(byte[] keyData);

		// Token: 0x0600052C RID: 1324
		[EngineMethod("get_mouse_move_x", false)]
		float GetMouseMoveX();

		// Token: 0x0600052D RID: 1325
		[EngineMethod("get_mouse_move_y", false)]
		float GetMouseMoveY();

		// Token: 0x0600052E RID: 1326
		[EngineMethod("get_gyro_x", false)]
		float GetGyroX();

		// Token: 0x0600052F RID: 1327
		[EngineMethod("get_gyro_y", false)]
		float GetGyroY();

		// Token: 0x06000530 RID: 1328
		[EngineMethod("get_gyro_z", false)]
		float GetGyroZ();

		// Token: 0x06000531 RID: 1329
		[EngineMethod("get_mouse_position_x", false)]
		float GetMousePositionX();

		// Token: 0x06000532 RID: 1330
		[EngineMethod("get_mouse_position_y", false)]
		float GetMousePositionY();

		// Token: 0x06000533 RID: 1331
		[EngineMethod("get_mouse_scroll_value", false)]
		float GetMouseScrollValue();

		// Token: 0x06000534 RID: 1332
		[EngineMethod("get_key_state", false)]
		Vec2 GetKeyState(InputKey key);

		// Token: 0x06000535 RID: 1333
		[EngineMethod("is_key_down", false)]
		bool IsKeyDown(InputKey key);

		// Token: 0x06000536 RID: 1334
		[EngineMethod("is_key_down_immediate", false)]
		bool IsKeyDownImmediate(InputKey key);

		// Token: 0x06000537 RID: 1335
		[EngineMethod("is_key_pressed", false)]
		bool IsKeyPressed(InputKey key);

		// Token: 0x06000538 RID: 1336
		[EngineMethod("is_key_released", false)]
		bool IsKeyReleased(InputKey key);

		// Token: 0x06000539 RID: 1337
		[EngineMethod("is_any_touch_active", false)]
		bool IsAnyTouchActive();

		// Token: 0x0600053A RID: 1338
		[EngineMethod("set_cursor_position", false)]
		void SetCursorPosition(int x, int y);

		// Token: 0x0600053B RID: 1339
		[EngineMethod("set_cursor_friction_value", false)]
		void SetCursorFrictionValue(float frictionValue);
	}
}
