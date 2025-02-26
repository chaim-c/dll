﻿using System;
using TaleWorlds.Library;

namespace TaleWorlds.InputSystem
{
	// Token: 0x02000009 RID: 9
	public interface IInputManager
	{
		// Token: 0x060000A9 RID: 169
		float GetMousePositionX();

		// Token: 0x060000AA RID: 170
		float GetMousePositionY();

		// Token: 0x060000AB RID: 171
		float GetMouseScrollValue();

		// Token: 0x060000AC RID: 172
		Input.ControllerTypes GetControllerType();

		// Token: 0x060000AD RID: 173
		bool IsMouseActive();

		// Token: 0x060000AE RID: 174
		bool IsControllerConnected();

		// Token: 0x060000AF RID: 175
		bool IsAnyTouchActive();

		// Token: 0x060000B0 RID: 176
		void PressKey(InputKey key);

		// Token: 0x060000B1 RID: 177
		void ClearKeys();

		// Token: 0x060000B2 RID: 178
		int GetVirtualKeyCode(InputKey key);

		// Token: 0x060000B3 RID: 179
		void SetClipboardText(string text);

		// Token: 0x060000B4 RID: 180
		string GetClipboardText();

		// Token: 0x060000B5 RID: 181
		float GetMouseMoveX();

		// Token: 0x060000B6 RID: 182
		float GetMouseMoveY();

		// Token: 0x060000B7 RID: 183
		float GetGyroX();

		// Token: 0x060000B8 RID: 184
		float GetGyroY();

		// Token: 0x060000B9 RID: 185
		float GetGyroZ();

		// Token: 0x060000BA RID: 186
		float GetMouseSensitivity();

		// Token: 0x060000BB RID: 187
		float GetMouseDeltaZ();

		// Token: 0x060000BC RID: 188
		void UpdateKeyData(byte[] keyData);

		// Token: 0x060000BD RID: 189
		Vec2 GetKeyState(InputKey key);

		// Token: 0x060000BE RID: 190
		bool IsKeyPressed(InputKey key);

		// Token: 0x060000BF RID: 191
		bool IsKeyDown(InputKey key);

		// Token: 0x060000C0 RID: 192
		bool IsKeyDownImmediate(InputKey key);

		// Token: 0x060000C1 RID: 193
		bool IsKeyReleased(InputKey key);

		// Token: 0x060000C2 RID: 194
		Vec2 GetResolution();

		// Token: 0x060000C3 RID: 195
		Vec2 GetDesktopResolution();

		// Token: 0x060000C4 RID: 196
		void SetCursorPosition(int x, int y);

		// Token: 0x060000C5 RID: 197
		void SetCursorFriction(float frictionValue);

		// Token: 0x060000C6 RID: 198
		InputKey[] GetClickKeys();

		// Token: 0x060000C7 RID: 199
		void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements);

		// Token: 0x060000C8 RID: 200
		void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength);

		// Token: 0x060000C9 RID: 201
		void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength);

		// Token: 0x060000CA RID: 202
		void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements);

		// Token: 0x060000CB RID: 203
		void SetLightbarColor(float red, float green, float blue);
	}
}
