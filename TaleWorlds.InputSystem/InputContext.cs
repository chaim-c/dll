using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.Library;

namespace TaleWorlds.InputSystem
{
	// Token: 0x0200000B RID: 11
	public class InputContext : IInputContext
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000494A File Offset: 0x00002B4A
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00004952 File Offset: 0x00002B52
		public bool IsKeysAllowed { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000495B File Offset: 0x00002B5B
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004963 File Offset: 0x00002B63
		public bool IsMouseButtonAllowed { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000496C File Offset: 0x00002B6C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00004974 File Offset: 0x00002B74
		public bool IsMouseWheelAllowed { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000497D File Offset: 0x00002B7D
		public bool IsControllerAllowed
		{
			get
			{
				return this.IsKeysAllowed;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004985 File Offset: 0x00002B85
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000498D File Offset: 0x00002B8D
		public bool MouseOnMe { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00004998 File Offset: 0x00002B98
		public InputContext()
		{
			this._categories = new List<GameKeyContext>();
			this.MouseOnMe = false;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004A0C File Offset: 0x00002C0C
		public int GetPointerX()
		{
			float x = Input.Resolution.x;
			return (int)(this.GetMousePositionRanged().x * x);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004A34 File Offset: 0x00002C34
		public int GetPointerY()
		{
			float y = Input.Resolution.y;
			return (int)(this.GetMousePositionRanged().y * y);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004A5C File Offset: 0x00002C5C
		public Vector2 GetPointerPosition()
		{
			Vec2 resolution = Input.Resolution;
			float x = resolution.x;
			float y = resolution.y;
			float x2 = this.GetMousePositionRanged().x * x;
			float y2 = this.GetMousePositionRanged().y * y;
			return new Vector2(x2, y2);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004A9C File Offset: 0x00002C9C
		public Vec2 GetPointerPositionVec2()
		{
			Vec2 resolution = Input.Resolution;
			float x = resolution.x;
			float y = resolution.y;
			float a = this.GetMousePositionRanged().x * x;
			float b = this.GetMousePositionRanged().y * y;
			return new Vec2(a, b);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004ADC File Offset: 0x00002CDC
		public void RegisterHotKeyCategory(GameKeyContext category)
		{
			this._categories.Add(category);
			foreach (HotKey hotKey in category.RegisteredHotKeys)
			{
				if (!this._registeredHotKeys.ContainsKey(hotKey.Id))
				{
					this._registeredHotKeys.Add(hotKey.Id, hotKey);
				}
			}
			if (this._registeredGameKeys.Count == 0)
			{
				int count = category.RegisteredGameKeys.Count;
				for (int i = 0; i < count; i++)
				{
					this._registeredGameKeys.Add(null);
				}
			}
			foreach (GameKey gameKey in category.RegisteredGameKeys)
			{
				if (gameKey != null)
				{
					this._registeredGameKeys[gameKey.Id] = gameKey;
				}
			}
			foreach (GameAxisKey gameAxisKey in category.RegisteredGameAxisKeys)
			{
				if (!this._registeredGameAxisKeys.ContainsKey(gameAxisKey.Id))
				{
					this._registeredGameAxisKeys.Add(gameAxisKey.Id, gameAxisKey);
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004C44 File Offset: 0x00002E44
		public bool IsCategoryRegistered(GameKeyContext category)
		{
			List<GameKeyContext> categories = this._categories;
			return categories != null && categories.Contains(category);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004C58 File Offset: 0x00002E58
		public void UpdateLastDownKeys()
		{
			for (int i = this._gameKeysToCurrentlyIgnore.Count - 1; i >= 0; i--)
			{
				if (this.IsGameKeyReleased(this._gameKeysToCurrentlyIgnore[i]))
				{
					this._gameKeysToCurrentlyIgnore.RemoveAt(i);
				}
			}
			for (int j = this._hotKeysToCurrentlyIgnore.Count - 1; j >= 0; j--)
			{
				if (this.IsHotKeyReleased(this._hotKeysToCurrentlyIgnore[j]))
				{
					this._hotKeysToCurrentlyIgnore.RemoveAt(j);
				}
			}
			for (int k = 0; k < this._registeredGameKeys.Count; k++)
			{
				GameKey gameKey = this._registeredGameKeys[k];
				if (gameKey != null)
				{
					bool flag = this.IsGameKeyDown(gameKey);
					if (this._isDownMapsReset && flag)
					{
						this._gameKeysToCurrentlyIgnore.Add(gameKey);
					}
					else if (!this._lastFrameDownGameKeyIDs.Contains(gameKey.Id))
					{
						this._lastFrameDownGameKeyIDs.Add(gameKey.Id);
					}
				}
			}
			foreach (HotKey hotKey in this._registeredHotKeys.Values)
			{
				bool flag2 = this.IsHotKeyDown(hotKey);
				if (this._isDownMapsReset && flag2)
				{
					this._hotKeysToCurrentlyIgnore.Add(hotKey);
				}
				else if (flag2)
				{
					this._lastFrameHotKeyDownMap[hotKey] = true;
				}
			}
			this._isDownMapsReset = false;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public void ResetLastDownKeys()
		{
			if (!this._isDownMapsReset)
			{
				this._lastFrameDownGameKeyIDs.Clear();
				this._lastFrameHotKeyDownMap.Clear();
				this._hotKeysToCurrentlyIgnore.Clear();
				this._gameKeysToCurrentlyIgnore.Clear();
				this._isDownMapsReset = true;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004E01 File Offset: 0x00003001
		private bool IsHotKeyDown(HotKey hotKey)
		{
			return hotKey.IsDown(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E2C File Offset: 0x0000302C
		public bool IsHotKeyDown(string hotKey)
		{
			HotKey hotKey2;
			return this._registeredHotKeys.TryGetValue(hotKey, out hotKey2) && this.IsHotKeyDown(hotKey2);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004E52 File Offset: 0x00003052
		private bool IsGameKeyDown(GameKey gameKey)
		{
			return gameKey.IsDown(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed, true);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004E80 File Offset: 0x00003080
		public bool IsGameKeyDown(int gameKey)
		{
			GameKey gameKey2 = this._registeredGameKeys[gameKey];
			return this.IsGameKeyDown(gameKey2);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004EA1 File Offset: 0x000030A1
		private bool IsGameKeyDownImmediate(GameKey gameKey)
		{
			return gameKey.IsDownImmediate(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004ECC File Offset: 0x000030CC
		public bool IsGameKeyDownImmediate(int gameKey)
		{
			GameKey gameKey2 = this._registeredGameKeys[gameKey];
			return this.IsGameKeyDownImmediate(gameKey2);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004EED File Offset: 0x000030ED
		private bool IsHotKeyPressed(HotKey hotKey)
		{
			return hotKey.IsPressed(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004F18 File Offset: 0x00003118
		public bool IsHotKeyDownAndReleased(string hotkey)
		{
			HotKey hotKey;
			return this._registeredHotKeys.TryGetValue(hotkey, out hotKey) && this._lastFrameHotKeyDownMap.ContainsKey(hotKey) && !this._hotKeysToCurrentlyIgnore.Contains(hotKey) && hotKey.IsReleased(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004F7C File Offset: 0x0000317C
		public bool IsHotKeyPressed(string hotKey)
		{
			HotKey hotKey2;
			return this._registeredHotKeys.TryGetValue(hotKey, out hotKey2) && this.IsHotKeyPressed(hotKey2);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004FA2 File Offset: 0x000031A2
		private bool IsGameKeyPressed(GameKey gameKey)
		{
			return gameKey.IsPressed(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004FD0 File Offset: 0x000031D0
		public bool IsGameKeyDownAndReleased(int gameKey)
		{
			GameKey gameKey2 = this._registeredGameKeys[gameKey];
			return this._lastFrameDownGameKeyIDs.Contains(gameKey2.Id) && !this._gameKeysToCurrentlyIgnore.Contains(gameKey2) && gameKey2.IsReleased(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005038 File Offset: 0x00003238
		public bool IsGameKeyPressed(int gameKey)
		{
			GameKey gameKey2 = this._registeredGameKeys[gameKey];
			return this.IsGameKeyPressed(gameKey2);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005059 File Offset: 0x00003259
		private bool IsHotKeyReleased(HotKey hotKey)
		{
			return hotKey.IsReleased(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005084 File Offset: 0x00003284
		public bool IsHotKeyReleased(string hotKey)
		{
			HotKey hotKey2;
			return this._registeredHotKeys.TryGetValue(hotKey, out hotKey2) && this.IsHotKeyReleased(hotKey2);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000050AA File Offset: 0x000032AA
		private bool IsGameKeyReleased(GameKey gameKey)
		{
			return gameKey.IsReleased(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000050D8 File Offset: 0x000032D8
		public bool IsGameKeyReleased(int gameKey)
		{
			GameKey gameKey2 = this._registeredGameKeys[gameKey];
			return this.IsGameKeyReleased(gameKey2);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000050F9 File Offset: 0x000032F9
		private float GetGameKeyState(GameKey gameKey)
		{
			return gameKey.GetKeyState(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005124 File Offset: 0x00003324
		public float GetGameKeyState(int gameKey)
		{
			GameKey gameKey2 = this._registeredGameKeys[gameKey];
			return this.GetGameKeyState(gameKey2);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005145 File Offset: 0x00003345
		private bool IsHotKeyDoublePressed(HotKey hotKey)
		{
			return hotKey.IsDoublePressed(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005170 File Offset: 0x00003370
		public bool IsHotKeyDoublePressed(string hotKey)
		{
			HotKey hotKey2;
			return this._registeredHotKeys.TryGetValue(hotKey, out hotKey2) && this.IsHotKeyDoublePressed(hotKey2);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005196 File Offset: 0x00003396
		public float GetGameKeyAxis(GameAxisKey gameKey)
		{
			return gameKey.GetAxisState(this.IsKeysAllowed, this.IsMouseButtonAllowed && this.MouseOnMe, this.IsMouseWheelAllowed, this.IsControllerAllowed);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000051C4 File Offset: 0x000033C4
		public float GetGameKeyAxis(string gameKey)
		{
			GameAxisKey gameKey2;
			if (this._registeredGameAxisKeys.TryGetValue(gameKey, out gameKey2))
			{
				return this.GetGameKeyAxis(gameKey2);
			}
			return 0f;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000051F0 File Offset: 0x000033F0
		internal bool CanUse(InputKey key)
		{
			if (Input.GetClickKeys().Any((InputKey k) => k == key))
			{
				return this.IsMouseButtonAllowed || this.IsControllerAllowed;
			}
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
				return this.IsKeysAllowed;
			case InputKey.ControllerLStick:
			case InputKey.ControllerRStick:
			case InputKey.ControllerLStickUp:
			case InputKey.ControllerLStickDown:
			case InputKey.ControllerLStickLeft:
			case InputKey.ControllerLStickRight:
			case InputKey.ControllerRStickUp:
			case InputKey.ControllerRStickDown:
			case InputKey.ControllerRStickLeft:
			case InputKey.ControllerRStickRight:
			case InputKey.ControllerLUp:
			case InputKey.ControllerLDown:
			case InputKey.ControllerLLeft:
			case InputKey.ControllerLRight:
			case InputKey.ControllerRUp:
			case InputKey.ControllerRDown:
			case InputKey.ControllerRLeft:
			case InputKey.ControllerRRight:
			case InputKey.ControllerLBumper:
			case InputKey.ControllerRBumper:
			case InputKey.ControllerLOption:
			case InputKey.ControllerROption:
			case InputKey.ControllerLTrigger:
			case InputKey.ControllerRTrigger:
				return this.IsControllerAllowed;
			case InputKey.LeftMouseButton:
			case InputKey.RightMouseButton:
			case InputKey.MiddleMouseButton:
			case InputKey.X1MouseButton:
			case InputKey.X2MouseButton:
				return this.IsMouseButtonAllowed;
			case InputKey.MouseScrollUp:
			case InputKey.MouseScrollDown:
				return this.IsMouseWheelAllowed;
			}
			return false;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000565D File Offset: 0x0000385D
		public Vec2 GetKeyState(InputKey key)
		{
			if (!this.CanUse(key))
			{
				return new Vec2(0f, 0f);
			}
			return Input.GetKeyState(key);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000567E File Offset: 0x0000387E
		protected bool IsMouseButton(InputKey key)
		{
			return key == InputKey.LeftMouseButton || key == InputKey.RightMouseButton || key == InputKey.MiddleMouseButton;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000569A File Offset: 0x0000389A
		public bool IsKeyDown(InputKey key)
		{
			if (this.IsMouseButton(key))
			{
				if (!this.MouseOnMe)
				{
					return false;
				}
			}
			else if (!this.CanUse(key))
			{
				return false;
			}
			return Input.IsKeyDown(key);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000056C0 File Offset: 0x000038C0
		public bool IsKeyPressed(InputKey key)
		{
			return this.CanUse(key) && Input.IsKeyPressed(key);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000056D3 File Offset: 0x000038D3
		public bool IsKeyReleased(InputKey key)
		{
			if (this.IsMouseButton(key))
			{
				if (!this.MouseOnMe)
				{
					return false;
				}
			}
			else if (!this.CanUse(key))
			{
				return false;
			}
			return Input.IsKeyReleased(key);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000056F9 File Offset: 0x000038F9
		public float GetMouseMoveX()
		{
			return Input.GetMouseMoveX();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005700 File Offset: 0x00003900
		public float GetMouseMoveY()
		{
			return Input.GetMouseMoveY();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005707 File Offset: 0x00003907
		public Vec2 GetControllerRightStickState()
		{
			return Input.GetKeyState(InputKey.ControllerRStick);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005713 File Offset: 0x00003913
		public Vec2 GetControllerLeftStickState()
		{
			return Input.GetKeyState(InputKey.ControllerLStick);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000571F File Offset: 0x0000391F
		public bool GetIsMouseActive()
		{
			return Input.IsMouseActive;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005726 File Offset: 0x00003926
		public bool GetIsMouseDown()
		{
			return Input.IsKeyDown(InputKey.LeftMouseButton) || Input.IsKeyDown(InputKey.RightMouseButton);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005740 File Offset: 0x00003940
		public Vec2 GetMousePositionPixel()
		{
			return Input.MousePositionPixel;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005747 File Offset: 0x00003947
		public float GetDeltaMouseScroll()
		{
			if (!this.IsMouseWheelAllowed)
			{
				return 0f;
			}
			return Input.DeltaMouseScroll;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000575C File Offset: 0x0000395C
		public bool GetIsControllerConnected()
		{
			return Input.IsControllerConnected;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005763 File Offset: 0x00003963
		public Vec2 GetMousePositionRanged()
		{
			return Input.MousePositionRanged;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000576A File Offset: 0x0000396A
		public float GetMouseSensitivity()
		{
			return Input.MouseSensitivity;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005771 File Offset: 0x00003971
		public bool IsControlDown()
		{
			return this.IsKeysAllowed && (Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005792 File Offset: 0x00003992
		public bool IsShiftDown()
		{
			return this.IsKeysAllowed && (Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000057B0 File Offset: 0x000039B0
		public bool IsAltDown()
		{
			return this.IsKeysAllowed && (Input.IsKeyDown(InputKey.LeftAlt) || Input.IsKeyDown(InputKey.RightAlt));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000057D1 File Offset: 0x000039D1
		public InputKey[] GetClickKeys()
		{
			return Input.GetClickKeys();
		}

		// Token: 0x04000031 RID: 49
		private Dictionary<string, HotKey> _registeredHotKeys = new Dictionary<string, HotKey>();

		// Token: 0x04000032 RID: 50
		private List<GameKey> _registeredGameKeys = new List<GameKey>();

		// Token: 0x04000033 RID: 51
		private List<int> _lastFrameDownGameKeyIDs = new List<int>();

		// Token: 0x04000034 RID: 52
		private Dictionary<string, GameAxisKey> _registeredGameAxisKeys = new Dictionary<string, GameAxisKey>();

		// Token: 0x04000035 RID: 53
		private List<GameKey> _gameKeysToCurrentlyIgnore = new List<GameKey>();

		// Token: 0x04000036 RID: 54
		private List<HotKey> _hotKeysToCurrentlyIgnore = new List<HotKey>();

		// Token: 0x04000037 RID: 55
		private Dictionary<HotKey, bool> _lastFrameHotKeyDownMap = new Dictionary<HotKey, bool>();

		// Token: 0x04000038 RID: 56
		private bool _isDownMapsReset;

		// Token: 0x0400003D RID: 61
		private readonly List<GameKeyContext> _categories;
	}
}
