using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200000A RID: 10
	public class GamepadCursorViewModel : ViewModel
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004038 File Offset: 0x00002238
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00004040 File Offset: 0x00002240
		[DataSourceProperty]
		public bool IsConsoleMouseVisible
		{
			get
			{
				return this._isConsoleMouseVisible;
			}
			set
			{
				if (this._isConsoleMouseVisible != value)
				{
					this._isConsoleMouseVisible = value;
					base.OnPropertyChangedWithValue(value, "IsConsoleMouseVisible");
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000405E File Offset: 0x0000225E
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00004066 File Offset: 0x00002266
		[DataSourceProperty]
		public bool IsGamepadCursorVisible
		{
			get
			{
				return this._isGamepadCursorVisible;
			}
			set
			{
				if (this._isGamepadCursorVisible != value)
				{
					this._isGamepadCursorVisible = value;
					base.OnPropertyChangedWithValue(value, "IsGamepadCursorVisible");
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004084 File Offset: 0x00002284
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000408C File Offset: 0x0000228C
		[DataSourceProperty]
		public float CursorPositionX
		{
			get
			{
				return this._cursorPositionX;
			}
			set
			{
				if (this._cursorPositionX != value)
				{
					this._cursorPositionX = value;
					base.OnPropertyChangedWithValue(value, "CursorPositionX");
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000040AA File Offset: 0x000022AA
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000040B2 File Offset: 0x000022B2
		[DataSourceProperty]
		public float CursorPositionY
		{
			get
			{
				return this._cursorPositionY;
			}
			set
			{
				if (this._cursorPositionY != value)
				{
					this._cursorPositionY = value;
					base.OnPropertyChangedWithValue(value, "CursorPositionY");
				}
			}
		}

		// Token: 0x0400003A RID: 58
		private float _cursorPositionX = 960f;

		// Token: 0x0400003B RID: 59
		private float _cursorPositionY = 540f;

		// Token: 0x0400003C RID: 60
		private bool _isConsoleMouseVisible;

		// Token: 0x0400003D RID: 61
		private bool _isGamepadCursorVisible;
	}
}
