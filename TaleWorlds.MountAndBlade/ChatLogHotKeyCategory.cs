using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021E RID: 542
	public sealed class ChatLogHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001E70 RID: 7792 RVA: 0x0006C28A File Offset: 0x0006A48A
		public ChatLogHotKeyCategory() : base("ChatLogHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0006C2AC File Offset: 0x0006A4AC
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.Tab),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> list = new List<Key>
			{
				new Key(InputKey.NumpadEnter)
			};
			list.Add(new Key(InputKey.ControllerLOption));
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.ControllerRLeft)
			};
			base.RegisterHotKey(new HotKey("CycleChatTypes", "ChatLogHotKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("FinalizeChatAlternative", "ChatLogHotKeyCategory", list, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SendMessage", "ChatLogHotKeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0006C364 File Offset: 0x0006A564
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(6, "InitiateAllChat", "ChatLogHotKeyCategory", InputKey.T, GameKeyMainCategories.ChatCategory), true);
			base.RegisterGameKey(new GameKey(7, "InitiateTeamChat", "ChatLogHotKeyCategory", InputKey.Y, GameKeyMainCategories.ChatCategory), true);
			base.RegisterGameKey(new GameKey(8, "FinalizeChat", "ChatLogHotKeyCategory", InputKey.Enter, InputKey.ControllerLOption, GameKeyMainCategories.ChatCategory), true);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0006C3D0 File Offset: 0x0006A5D0
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x040009F2 RID: 2546
		public const string CategoryId = "ChatLogHotKeyCategory";

		// Token: 0x040009F3 RID: 2547
		public const int InitiateAllChat = 6;

		// Token: 0x040009F4 RID: 2548
		public const int InitiateTeamChat = 7;

		// Token: 0x040009F5 RID: 2549
		public const int FinalizeChat = 8;

		// Token: 0x040009F6 RID: 2550
		public const string CycleChatTypes = "CycleChatTypes";

		// Token: 0x040009F7 RID: 2551
		public const string FinalizeChatAlternative = "FinalizeChatAlternative";

		// Token: 0x040009F8 RID: 2552
		public const string SendMessage = "SendMessage";
	}
}
