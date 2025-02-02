using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022F RID: 559
	public sealed class PollHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001EB0 RID: 7856 RVA: 0x0006E0A5 File Offset: 0x0006C2A5
		public PollHotkeyCategory() : base("PollHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterGameKeys();
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0006E0BC File Offset: 0x0006C2BC
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(106, "AcceptPoll", "PollHotkeyCategory", InputKey.F10, InputKey.ControllerLBumper, GameKeyMainCategories.PollCategory), true);
			base.RegisterGameKey(new GameKey(107, "DeclinePoll", "PollHotkeyCategory", InputKey.F11, InputKey.ControllerRBumper, GameKeyMainCategories.PollCategory), true);
		}

		// Token: 0x04000B34 RID: 2868
		public const string CategoryId = "PollHotkeyCategory";

		// Token: 0x04000B35 RID: 2869
		public const int AcceptPoll = 106;

		// Token: 0x04000B36 RID: 2870
		public const int DeclinePoll = 107;
	}
}
