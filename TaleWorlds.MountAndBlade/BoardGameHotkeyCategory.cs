using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021D RID: 541
	public sealed class BoardGameHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001E6C RID: 7788 RVA: 0x0006C15C File Offset: 0x0006A35C
		public BoardGameHotkeyCategory() : base("BoardGameHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0006C180 File Offset: 0x0006A380
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRDown)
			};
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRDown)
			};
			List<Key> keys3 = new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRDown)
			};
			List<Key> keys4 = new List<Key>
			{
				new Key(InputKey.Space),
				new Key(InputKey.ControllerRBumper)
			};
			base.RegisterHotKey(new HotKey("BoardGamePawnSelect", "BoardGameHotkeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("BoardGamePawnDeselect", "BoardGameHotkeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("BoardGameDragPreview", "BoardGameHotkeyCategory", keys3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("BoardGameRollDice", "BoardGameHotkeyCategory", keys4, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0006C286 File Offset: 0x0006A486
		private void RegisterGameKeys()
		{
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0006C288 File Offset: 0x0006A488
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x040009ED RID: 2541
		public const string CategoryId = "BoardGameHotkeyCategory";

		// Token: 0x040009EE RID: 2542
		public const string BoardGamePawnSelect = "BoardGamePawnSelect";

		// Token: 0x040009EF RID: 2543
		public const string BoardGamePawnDeselect = "BoardGamePawnDeselect";

		// Token: 0x040009F0 RID: 2544
		public const string BoardGameDragPreview = "BoardGameDragPreview";

		// Token: 0x040009F1 RID: 2545
		public const string BoardGameRollDice = "BoardGameRollDice";
	}
}
