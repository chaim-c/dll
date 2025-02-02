using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022B RID: 555
	public sealed class MapNotificationHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001EA2 RID: 7842 RVA: 0x0006D9CF File Offset: 0x0006BBCF
		public MapNotificationHotKeyCategory() : base("MapNotificationHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0006D9E5 File Offset: 0x0006BBE5
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("RemoveNotification", "MapNotificationHotKeyCategory", InputKey.ControllerRUp, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x04000AF8 RID: 2808
		public const string CategoryId = "MapNotificationHotKeyCategory";

		// Token: 0x04000AF9 RID: 2809
		public const string RemoveNotification = "RemoveNotification";
	}
}
