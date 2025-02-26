﻿using System;
using System.Numerics;
using TaleWorlds.Library;

namespace TaleWorlds.InputSystem
{
	// Token: 0x02000008 RID: 8
	public interface IInputContext
	{
		// Token: 0x06000089 RID: 137
		int GetPointerX();

		// Token: 0x0600008A RID: 138
		int GetPointerY();

		// Token: 0x0600008B RID: 139
		Vector2 GetPointerPosition();

		// Token: 0x0600008C RID: 140
		bool IsGameKeyDown(int gameKey);

		// Token: 0x0600008D RID: 141
		bool IsGameKeyDownImmediate(int gameKey);

		// Token: 0x0600008E RID: 142
		bool IsGameKeyReleased(int gameKey);

		// Token: 0x0600008F RID: 143
		bool IsGameKeyPressed(int gameKey);

		// Token: 0x06000090 RID: 144
		bool IsGameKeyDownAndReleased(int gameKey);

		// Token: 0x06000091 RID: 145
		float GetGameKeyAxis(string gameKey);

		// Token: 0x06000092 RID: 146
		bool IsHotKeyDown(string gameKey);

		// Token: 0x06000093 RID: 147
		bool IsHotKeyReleased(string gameKey);

		// Token: 0x06000094 RID: 148
		bool IsHotKeyPressed(string gameKey);

		// Token: 0x06000095 RID: 149
		bool IsHotKeyDownAndReleased(string gameKey);

		// Token: 0x06000096 RID: 150
		bool IsHotKeyDoublePressed(string gameKey);

		// Token: 0x06000097 RID: 151
		bool IsKeyDown(InputKey key);

		// Token: 0x06000098 RID: 152
		bool IsKeyPressed(InputKey key);

		// Token: 0x06000099 RID: 153
		bool IsKeyReleased(InputKey key);

		// Token: 0x0600009A RID: 154
		Vec2 GetKeyState(InputKey key);

		// Token: 0x0600009B RID: 155
		float GetMouseMoveX();

		// Token: 0x0600009C RID: 156
		float GetMouseMoveY();

		// Token: 0x0600009D RID: 157
		Vec2 GetControllerRightStickState();

		// Token: 0x0600009E RID: 158
		Vec2 GetControllerLeftStickState();

		// Token: 0x0600009F RID: 159
		float GetDeltaMouseScroll();

		// Token: 0x060000A0 RID: 160
		bool GetIsControllerConnected();

		// Token: 0x060000A1 RID: 161
		bool GetIsMouseActive();

		// Token: 0x060000A2 RID: 162
		Vec2 GetMousePositionRanged();

		// Token: 0x060000A3 RID: 163
		Vec2 GetMousePositionPixel();

		// Token: 0x060000A4 RID: 164
		float GetMouseSensitivity();

		// Token: 0x060000A5 RID: 165
		bool IsControlDown();

		// Token: 0x060000A6 RID: 166
		bool IsShiftDown();

		// Token: 0x060000A7 RID: 167
		bool IsAltDown();

		// Token: 0x060000A8 RID: 168
		InputKey[] GetClickKeys();
	}
}
