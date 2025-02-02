using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021C RID: 540
	public sealed class ArmyManagementHotkeyCategory : GameKeyContext
	{
		// Token: 0x06001E6A RID: 7786 RVA: 0x0006C127 File Offset: 0x0006A327
		public ArmyManagementHotkeyCategory() : base("ArmyManagementHotkeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0006C13D File Offset: 0x0006A33D
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("RemoveParty", "ArmyManagementHotkeyCategory", InputKey.ControllerRBumper, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x040009EB RID: 2539
		public const string CategoryId = "ArmyManagementHotkeyCategory";

		// Token: 0x040009EC RID: 2540
		public const string RemoveParty = "RemoveParty";
	}
}
