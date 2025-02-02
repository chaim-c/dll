using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000229 RID: 553
	public sealed class InventoryHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001E9A RID: 7834 RVA: 0x0006D561 File Offset: 0x0006B761
		public InventoryHotKeyCategory() : base("InventoryHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x0006D583 File Offset: 0x0006B783
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("SwitchAlternative", "InventoryHotKeyCategory", InputKey.LeftAlt, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0006D59F File Offset: 0x0006B79F
		private void RegisterGameKeys()
		{
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x0006D5A1 File Offset: 0x0006B7A1
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000ADA RID: 2778
		public const string CategoryId = "InventoryHotKeyCategory";

		// Token: 0x04000ADB RID: 2779
		public const string SwitchAlternative = "SwitchAlternative";
	}
}
