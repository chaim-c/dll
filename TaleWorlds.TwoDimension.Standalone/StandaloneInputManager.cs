using System;
using System.Drawing;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension.Standalone.Native.Windows;

namespace TaleWorlds.TwoDimension.Standalone
{
	// Token: 0x0200000A RID: 10
	public class StandaloneInputManager : IInputManager
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000040B5 File Offset: 0x000022B5
		public StandaloneInputManager(GraphicsForm graphicsForm)
		{
			this._graphicsForm = graphicsForm;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000040C4 File Offset: 0x000022C4
		float IInputManager.GetMousePositionX()
		{
			return this._graphicsForm.MousePosition().X / (float)this._graphicsForm.Width;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000040E3 File Offset: 0x000022E3
		float IInputManager.GetMousePositionY()
		{
			return this._graphicsForm.MousePosition().Y / (float)this._graphicsForm.Height;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004102 File Offset: 0x00002302
		float IInputManager.GetMouseScrollValue()
		{
			return 0f;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004109 File Offset: 0x00002309
		bool IInputManager.IsMouseActive()
		{
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000410C File Offset: 0x0000230C
		bool IInputManager.IsAnyTouchActive()
		{
			return false;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000410F File Offset: 0x0000230F
		bool IInputManager.IsControllerConnected()
		{
			return false;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004112 File Offset: 0x00002312
		void IInputManager.PressKey(InputKey key)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004114 File Offset: 0x00002314
		void IInputManager.ClearKeys()
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004116 File Offset: 0x00002316
		int IInputManager.GetVirtualKeyCode(InputKey key)
		{
			return -1;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004119 File Offset: 0x00002319
		void IInputManager.SetClipboardText(string text)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000411B File Offset: 0x0000231B
		string IInputManager.GetClipboardText()
		{
			return "";
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004122 File Offset: 0x00002322
		float IInputManager.GetMouseMoveX()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004129 File Offset: 0x00002329
		float IInputManager.GetMouseMoveY()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004130 File Offset: 0x00002330
		float IInputManager.GetGyroX()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004137 File Offset: 0x00002337
		float IInputManager.GetGyroY()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000413E File Offset: 0x0000233E
		float IInputManager.GetGyroZ()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004145 File Offset: 0x00002345
		float IInputManager.GetMouseSensitivity()
		{
			return 1f;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000414C File Offset: 0x0000234C
		float IInputManager.GetMouseDeltaZ()
		{
			return this._graphicsForm.GetMouseDeltaZ();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004159 File Offset: 0x00002359
		void IInputManager.UpdateKeyData(byte[] keyData)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000415B File Offset: 0x0000235B
		Vec2 IInputManager.GetKeyState(InputKey key)
		{
			if (!this._graphicsForm.GetKey(key))
			{
				return new Vec2(0f, 0f);
			}
			return new Vec2(1f, 0f);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000418A File Offset: 0x0000238A
		bool IInputManager.IsKeyPressed(InputKey key)
		{
			return this._graphicsForm.GetKeyDown(key);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004198 File Offset: 0x00002398
		bool IInputManager.IsKeyDown(InputKey key)
		{
			return this._graphicsForm.GetKey(key);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000041A6 File Offset: 0x000023A6
		bool IInputManager.IsKeyDownImmediate(InputKey key)
		{
			return this._graphicsForm.GetKey(key);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000041B4 File Offset: 0x000023B4
		bool IInputManager.IsKeyReleased(InputKey key)
		{
			return this._graphicsForm.GetKeyUp(key);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000041C2 File Offset: 0x000023C2
		Vec2 IInputManager.GetResolution()
		{
			return new Vec2((float)this._graphicsForm.Width, (float)this._graphicsForm.Height);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000041E4 File Offset: 0x000023E4
		Vec2 IInputManager.GetDesktopResolution()
		{
			Rectangle rectangle;
			User32.GetClientRect(User32.GetDesktopWindow(), out rectangle);
			return new Vec2((float)rectangle.Width, (float)rectangle.Height);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004213 File Offset: 0x00002413
		void IInputManager.SetCursorPosition(int x, int y)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004215 File Offset: 0x00002415
		void IInputManager.SetCursorFriction(float frictionValue)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004217 File Offset: 0x00002417
		InputKey[] IInputManager.GetClickKeys()
		{
			return new InputKey[]
			{
				InputKey.LeftMouseButton,
				InputKey.ControllerRDown
			};
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000422F File Offset: 0x0000242F
		public void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004231 File Offset: 0x00002431
		public void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004233 File Offset: 0x00002433
		public void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004235 File Offset: 0x00002435
		public void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004237 File Offset: 0x00002437
		public void SetLightbarColor(float red, float green, float blue)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004239 File Offset: 0x00002439
		Input.ControllerTypes IInputManager.GetControllerType()
		{
			return Input.ControllerTypes.Xbox;
		}

		// Token: 0x0400003D RID: 61
		private GraphicsForm _graphicsForm;
	}
}
