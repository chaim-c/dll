using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.InputSystem
{
	// Token: 0x02000006 RID: 6
	public class HotKey
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002C90 File Offset: 0x00000E90
		private bool _isDoublePressActive
		{
			get
			{
				int num = Environment.TickCount - this._doublePressTime;
				return num < 500 && num >= 0;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002CBB File Offset: 0x00000EBB
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002CC3 File Offset: 0x00000EC3
		public List<Key> Keys { get; internal set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002CCC File Offset: 0x00000ECC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public List<Key> DefaultKeys { get; private set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public HotKey(string id, string groupId, List<Key> keys, HotKey.Modifiers modifiers = HotKey.Modifiers.None, HotKey.Modifiers negativeModifiers = HotKey.Modifiers.None)
		{
			this.Id = id;
			this.GroupId = groupId;
			this.Keys = keys;
			this.DefaultKeys = new List<Key>();
			for (int i = 0; i < this.Keys.Count; i++)
			{
				this.DefaultKeys.Add(new Key(this.Keys[i].InputKey));
			}
			this._modifiers = modifiers;
			this._negativeModifiers = negativeModifiers;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002D5C File Offset: 0x00000F5C
		public HotKey(string id, string groupId, InputKey inputKey, HotKey.Modifiers modifiers = HotKey.Modifiers.None, HotKey.Modifiers negativeModifiers = HotKey.Modifiers.None)
		{
			this.Id = id;
			this.GroupId = groupId;
			this.Keys = new List<Key>
			{
				new Key(inputKey)
			};
			this.DefaultKeys = new List<Key>
			{
				new Key(inputKey)
			};
			this._modifiers = modifiers;
			this._negativeModifiers = negativeModifiers;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002DBB File Offset: 0x00000FBB
		private bool IsKeyAllowed(Key key, bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			return (isKeysAllowed || !key.IsKeyboardInput) && (isMouseButtonAllowed || !key.IsMouseButtonInput) && (isMouseWheelAllowed || !key.IsMouseWheelInput) && (isControllerAllowed || !key.IsControllerInput);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002DF0 File Offset: 0x00000FF0
		private bool CheckModifiers()
		{
			bool flag = Input.IsKeyDown(InputKey.LeftControl) || Input.IsKeyDown(InputKey.RightControl);
			bool flag2 = Input.IsKeyDown(InputKey.LeftAlt) || Input.IsKeyDown(InputKey.RightAlt);
			bool flag3 = Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift);
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;
			if (this._modifiers.HasAnyFlag(HotKey.Modifiers.Control))
			{
				flag4 = flag;
			}
			if (this._modifiers.HasAnyFlag(HotKey.Modifiers.Alt))
			{
				flag5 = flag2;
			}
			if (this._modifiers.HasAnyFlag(HotKey.Modifiers.Shift))
			{
				flag6 = flag3;
			}
			if (this._negativeModifiers.HasAnyFlag(HotKey.Modifiers.Control))
			{
				flag4 = !flag;
			}
			if (this._negativeModifiers.HasAnyFlag(HotKey.Modifiers.Alt))
			{
				flag5 = !flag2;
			}
			if (this._negativeModifiers.HasAnyFlag(HotKey.Modifiers.Shift))
			{
				flag6 = !flag3;
			}
			return flag4 && flag5 && flag6;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EBB File Offset: 0x000010BB
		private bool IsDown(Key key, bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			return this.IsKeyAllowed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed) && (this._modifiers == HotKey.Modifiers.None || this.CheckModifiers()) && key.IsDown();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002EE8 File Offset: 0x000010E8
		internal bool IsDown(bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			foreach (Key key in this.Keys)
			{
				if (this.IsDown(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F4C File Offset: 0x0000114C
		private bool IsDownImmediate(Key key, bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			return this.IsKeyAllowed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed) && (this._modifiers == HotKey.Modifiers.None || this.CheckModifiers()) && key.IsDownImmediate();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F78 File Offset: 0x00001178
		internal bool IsDownImmediate(bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			foreach (Key key in this.Keys)
			{
				if (this.IsDownImmediate(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FDC File Offset: 0x000011DC
		private bool IsDoublePressed(Key key, bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			if (!this.IsKeyAllowed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed))
			{
				return false;
			}
			if (this._modifiers != HotKey.Modifiers.None && !this.CheckModifiers())
			{
				return false;
			}
			if (key.IsPressed())
			{
				if (this._isDoublePressActive)
				{
					this._doublePressTime = 0;
					return true;
				}
				this._doublePressTime = Environment.TickCount;
			}
			return false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003034 File Offset: 0x00001234
		internal bool IsDoublePressed(bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			foreach (Key key in this.Keys)
			{
				if (this.IsDoublePressed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003098 File Offset: 0x00001298
		private bool IsPressed(Key key, bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			return this.IsKeyAllowed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed) && (this._modifiers == HotKey.Modifiers.None || this.CheckModifiers()) && key.IsPressed();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030C4 File Offset: 0x000012C4
		internal bool IsPressed(bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			foreach (Key key in this.Keys)
			{
				if (this.IsPressed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003128 File Offset: 0x00001328
		private bool IsReleased(Key key, bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			return this.IsKeyAllowed(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed) && (this._modifiers == HotKey.Modifiers.None || this.CheckModifiers()) && key.IsReleased();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003154 File Offset: 0x00001354
		internal bool IsReleased(bool isKeysAllowed, bool isMouseButtonAllowed, bool isMouseWheelAllowed, bool isControllerAllowed)
		{
			foreach (Key key in this.Keys)
			{
				if (this.IsReleased(key, isKeysAllowed, isMouseButtonAllowed, isMouseWheelAllowed, isControllerAllowed))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031B8 File Offset: 0x000013B8
		public bool HasModifier(HotKey.Modifiers modifier)
		{
			return this._modifiers.HasAnyFlag(modifier);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031C6 File Offset: 0x000013C6
		public bool HasSameModifiers(HotKey other)
		{
			return this._modifiers == other._modifiers;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000031D8 File Offset: 0x000013D8
		public override string ToString()
		{
			string result = "";
			bool flag = Input.IsControllerConnected && !Input.IsMouseActive;
			for (int i = 0; i < this.Keys.Count; i++)
			{
				if ((!flag && !this.Keys[i].IsControllerInput) || (flag && this.Keys[i].IsControllerInput))
				{
					return this.Keys[i].ToString();
				}
			}
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003254 File Offset: 0x00001454
		public override bool Equals(object obj)
		{
			HotKey hotKey = obj as HotKey;
			return hotKey != null && hotKey.Id.Equals(this.Id) && hotKey.GroupId.Equals(this.GroupId) && hotKey.Keys.SequenceEqual(this.Keys);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000032A4 File Offset: 0x000014A4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000016 RID: 22
		private const int DOUBLE_PRESS_TIME = 500;

		// Token: 0x04000017 RID: 23
		private int _doublePressTime;

		// Token: 0x04000018 RID: 24
		public string Id;

		// Token: 0x04000019 RID: 25
		public string GroupId;

		// Token: 0x0400001C RID: 28
		private HotKey.Modifiers _modifiers;

		// Token: 0x0400001D RID: 29
		private HotKey.Modifiers _negativeModifiers;

		// Token: 0x02000012 RID: 18
		[Flags]
		public enum Modifiers
		{
			// Token: 0x0400015A RID: 346
			None = 0,
			// Token: 0x0400015B RID: 347
			Shift = 1,
			// Token: 0x0400015C RID: 348
			Alt = 2,
			// Token: 0x0400015D RID: 349
			Control = 4
		}
	}
}
