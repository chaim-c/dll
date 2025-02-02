using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000009 RID: 9
	public class GauntletGamepadCursor : GlobalLayer
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003ED0 File Offset: 0x000020D0
		public GauntletGamepadCursor()
		{
			this._dataSource = new GamepadCursorViewModel();
			this._layer = new GauntletLayer(100001, "GauntletLayer", false);
			this._layer.LoadMovie("GamepadCursor", this._dataSource);
			this._layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
			base.Layer = this._layer;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003F39 File Offset: 0x00002139
		public static void Initialize()
		{
			if (GauntletGamepadCursor._current == null)
			{
				GauntletGamepadCursor._current = new GauntletGamepadCursor();
				ScreenManager.AddGlobalLayer(GauntletGamepadCursor._current, false);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003F58 File Offset: 0x00002158
		protected override void OnLateTick(float dt)
		{
			base.OnLateTick(dt);
			if (ScreenManager.IsMouseCursorHidden())
			{
				this._dataSource.IsGamepadCursorVisible = true;
				this._dataSource.IsConsoleMouseVisible = false;
				Vec2 cursorPosition = GauntletGamepadCursor.GetCursorPosition();
				this._dataSource.CursorPositionX = cursorPosition.X;
				this._dataSource.CursorPositionY = cursorPosition.Y;
				return;
			}
			this._dataSource.IsGamepadCursorVisible = false;
			this._dataSource.IsConsoleMouseVisible = false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003FD0 File Offset: 0x000021D0
		private static Vec2 GetCursorPosition()
		{
			Vec2 mousePositionPixel = Input.MousePositionPixel;
			Vec2 vec = Vec2.One - ScreenManager.UsableArea;
			float num = vec.x * Screen.RealScreenResolution.x / 2f;
			float num2 = vec.y * Screen.RealScreenResolution.y / 2f;
			return new Vec2(mousePositionPixel.X - num, mousePositionPixel.Y - num2);
		}

		// Token: 0x04000037 RID: 55
		private GamepadCursorViewModel _dataSource;

		// Token: 0x04000038 RID: 56
		private GauntletLayer _layer;

		// Token: 0x04000039 RID: 57
		private static GauntletGamepadCursor _current;
	}
}
