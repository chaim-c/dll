using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.Engine.InputSystem
{
	// Token: 0x020000A8 RID: 168
	public class EngineInputManager : IInputManager
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x0000EB90 File Offset: 0x0000CD90
		float IInputManager.GetMousePositionX()
		{
			return EngineApplicationInterface.IInput.GetMousePositionX();
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		float IInputManager.GetMousePositionY()
		{
			return EngineApplicationInterface.IInput.GetMousePositionY();
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		float IInputManager.GetMouseScrollValue()
		{
			return EngineApplicationInterface.IInput.GetMouseScrollValue();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		bool IInputManager.IsMouseActive()
		{
			return EngineApplicationInterface.IInput.IsMouseActive();
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		bool IInputManager.IsControllerConnected()
		{
			return EngineApplicationInterface.IInput.IsControllerConnected();
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		void IInputManager.PressKey(InputKey key)
		{
			EngineApplicationInterface.IInput.PressKey(key);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
		void IInputManager.ClearKeys()
		{
			EngineApplicationInterface.IInput.ClearKeys();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
		int IInputManager.GetVirtualKeyCode(InputKey key)
		{
			return EngineApplicationInterface.IInput.GetVirtualKeyCode(key);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0000EBF2 File Offset: 0x0000CDF2
		void IInputManager.SetClipboardText(string text)
		{
			EngineApplicationInterface.IInput.SetClipboardText(text);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		string IInputManager.GetClipboardText()
		{
			return EngineApplicationInterface.IInput.GetClipboardText();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0000EC0B File Offset: 0x0000CE0B
		float IInputManager.GetMouseMoveX()
		{
			return EngineApplicationInterface.IInput.GetMouseMoveX();
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0000EC17 File Offset: 0x0000CE17
		float IInputManager.GetMouseMoveY()
		{
			return EngineApplicationInterface.IInput.GetMouseMoveY();
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0000EC23 File Offset: 0x0000CE23
		float IInputManager.GetGyroX()
		{
			return EngineApplicationInterface.IInput.GetGyroX();
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0000EC2F File Offset: 0x0000CE2F
		float IInputManager.GetGyroY()
		{
			return EngineApplicationInterface.IInput.GetGyroY();
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0000EC3B File Offset: 0x0000CE3B
		float IInputManager.GetGyroZ()
		{
			return EngineApplicationInterface.IInput.GetGyroZ();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0000EC47 File Offset: 0x0000CE47
		float IInputManager.GetMouseSensitivity()
		{
			return EngineApplicationInterface.IInput.GetMouseSensitivity();
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0000EC53 File Offset: 0x0000CE53
		float IInputManager.GetMouseDeltaZ()
		{
			return EngineApplicationInterface.IInput.GetMouseDeltaZ();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		void IInputManager.UpdateKeyData(byte[] keyData)
		{
			EngineApplicationInterface.IInput.UpdateKeyData(keyData);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		Vec2 IInputManager.GetKeyState(InputKey key)
		{
			return EngineApplicationInterface.IInput.GetKeyState(key);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0000EC79 File Offset: 0x0000CE79
		bool IInputManager.IsKeyPressed(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyPressed(key);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0000EC86 File Offset: 0x0000CE86
		bool IInputManager.IsKeyDown(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyDown(key);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0000EC93 File Offset: 0x0000CE93
		bool IInputManager.IsKeyDownImmediate(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyDownImmediate(key);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		bool IInputManager.IsKeyReleased(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyReleased(key);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0000ECAD File Offset: 0x0000CEAD
		Vec2 IInputManager.GetResolution()
		{
			return Screen.RealScreenResolution;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		Vec2 IInputManager.GetDesktopResolution()
		{
			return Screen.DesktopResolution;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		void IInputManager.SetCursorPosition(int x, int y)
		{
			float num = 1f;
			float num2 = 1f;
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DisplayMode) != 0f)
			{
				num = Input.DesktopResolution.X / Input.Resolution.X;
				num2 = Input.DesktopResolution.Y / Input.Resolution.Y;
			}
			EngineApplicationInterface.IInput.SetCursorPosition((int)((float)x * num), (int)((float)y * num2));
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0000ED31 File Offset: 0x0000CF31
		void IInputManager.SetCursorFriction(float frictionValue)
		{
			EngineApplicationInterface.IInput.SetCursorFrictionValue(frictionValue);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0000ED40 File Offset: 0x0000CF40
		InputKey[] IInputManager.GetClickKeys()
		{
			InputKey inputKey = EngineApplicationInterface.IScreen.IsEnterButtonCross() ? InputKey.ControllerRDown : InputKey.ControllerRRight;
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableTouchpadMouse) != 0f)
			{
				return new InputKey[]
				{
					InputKey.LeftMouseButton,
					inputKey,
					InputKey.ControllerLOptionTap
				};
			}
			return new InputKey[]
			{
				InputKey.LeftMouseButton,
				inputKey
			};
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements)
		{
			EngineApplicationInterface.IInput.SetRumbleEffect(lowFrequencyLevels, lowFrequencyDurations, numLowFrequencyElements, highFrequencyLevels, highFrequencyDurations, numHighFrequencyElements);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0000EDB6 File Offset: 0x0000CFB6
		public void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength)
		{
			EngineApplicationInterface.IInput.SetTriggerFeedback(leftTriggerPosition, leftTriggerStrength, rightTriggerPosition, rightTriggerStrength);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0000EDC7 File Offset: 0x0000CFC7
		public void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength)
		{
			EngineApplicationInterface.IInput.SetTriggerWeaponEffect(leftStartPosition, leftEnd_position, leftStrength, rightStartPosition, rightEndPosition, rightStrength);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements)
		{
			EngineApplicationInterface.IInput.SetTriggerVibration(leftTriggerAmplitudes, leftTriggerFrequencies, leftTriggerDurations, numLeftTriggerElements, rightTriggerAmplitudes, rightTriggerFrequencies, rightTriggerDurations, numRightTriggerElements);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0000EE00 File Offset: 0x0000D000
		public void SetLightbarColor(float red, float green, float blue)
		{
			EngineApplicationInterface.IInput.SetLightbarColor(red, green, blue);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0000EE0F File Offset: 0x0000D00F
		Input.ControllerTypes IInputManager.GetControllerType()
		{
			return (Input.ControllerTypes)EngineApplicationInterface.IInput.GetControllerType();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0000EE1B File Offset: 0x0000D01B
		bool IInputManager.IsAnyTouchActive()
		{
			return EngineApplicationInterface.IInput.IsAnyTouchActive();
		}
	}
}
