using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000230 RID: 560
	public sealed class ScoreboardHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x0006E111 File Offset: 0x0006C311
		public ScoreboardHotKeyCategory() : base("ScoreboardHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0006E134 File Offset: 0x0006C334
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.F),
				new Key(InputKey.ControllerRUp)
			};
			base.RegisterHotKey(new HotKey("ToggleFastForward", "ScoreboardHotKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("MenuShowContextMenu", "ScoreboardHotKeyCategory", InputKey.RightMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("HoldShow", "ScoreboardHotKeyCategory", new List<Key>
			{
				new Key(InputKey.Tab),
				new Key(InputKey.ControllerRRight)
			}, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0006E1D6 File Offset: 0x0006C3D6
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(35, "ShowMouse", "ScoreboardHotKeyCategory", InputKey.MiddleMouseButton, InputKey.ControllerLThumb, GameKeyMainCategories.ActionCategory), true);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0006E1FF File Offset: 0x0006C3FF
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000B37 RID: 2871
		public const string CategoryId = "ScoreboardHotKeyCategory";

		// Token: 0x04000B38 RID: 2872
		public const int ShowMouse = 35;

		// Token: 0x04000B39 RID: 2873
		public const string HoldShow = "HoldShow";

		// Token: 0x04000B3A RID: 2874
		public const string ToggleFastForward = "ToggleFastForward";

		// Token: 0x04000B3B RID: 2875
		public const string MenuShowContextMenu = "MenuShowContextMenu";
	}
}
