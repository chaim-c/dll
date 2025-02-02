using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000220 RID: 544
	public sealed class ConversationHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001E78 RID: 7800 RVA: 0x0006C957 File Offset: 0x0006AB57
		public ConversationHotKeyCategory() : base("ConversationHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0006C97C File Offset: 0x0006AB7C
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.Space),
				new Key(InputKey.Enter),
				new Key(InputKey.NumpadEnter),
				new Key(InputKey.ControllerRDown)
			};
			base.RegisterHotKey(new HotKey("ContinueKey", "ConversationHotKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ContinueClick", "ConversationHotKeyCategory", InputKey.LeftMouseButton, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0006C9FF File Offset: 0x0006ABFF
		private void RegisterGameKeys()
		{
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0006CA01 File Offset: 0x0006AC01
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000A21 RID: 2593
		public const string CategoryId = "ConversationHotKeyCategory";

		// Token: 0x04000A22 RID: 2594
		public const string ContinueKey = "ContinueKey";

		// Token: 0x04000A23 RID: 2595
		public const string ContinueClick = "ContinueClick";
	}
}
