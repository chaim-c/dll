using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022E RID: 558
	public sealed class PartyHotKeyCategory : GameKeyContext
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x0006DEBE File Offset: 0x0006C0BE
		public PartyHotKeyCategory() : base("PartyHotKeyCategory", 108, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0006DEE0 File Offset: 0x0006C0E0
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.Q),
				new Key(InputKey.ControllerLTrigger)
			};
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.E),
				new Key(InputKey.ControllerRTrigger)
			};
			List<Key> keys3 = new List<Key>
			{
				new Key(InputKey.A),
				new Key(InputKey.ControllerLBumper)
			};
			List<Key> keys4 = new List<Key>
			{
				new Key(InputKey.D),
				new Key(InputKey.ControllerRBumper)
			};
			List<Key> keys5 = new List<Key>
			{
				new Key(InputKey.ControllerLBumper)
			};
			List<Key> keys6 = new List<Key>
			{
				new Key(InputKey.ControllerRBumper)
			};
			List<Key> keys7 = new List<Key>
			{
				new Key(InputKey.ControllerLThumb)
			};
			List<Key> keys8 = new List<Key>
			{
				new Key(InputKey.ControllerRThumb)
			};
			base.RegisterHotKey(new HotKey("TakeAllTroops", "PartyHotKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("GiveAllTroops", "PartyHotKeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("TakeAllPrisoners", "PartyHotKeyCategory", keys3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("GiveAllPrisoners", "PartyHotKeyCategory", keys4, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("OpenUpgradePopup", "PartyHotKeyCategory", keys7, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("OpenRecruitPopup", "PartyHotKeyCategory", keys8, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("PopupItemPrimaryAction", "PartyHotKeyCategory", keys5, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("PopupItemSecondaryAction", "PartyHotKeyCategory", keys6, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0006E0A1 File Offset: 0x0006C2A1
		private void RegisterGameKeys()
		{
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0006E0A3 File Offset: 0x0006C2A3
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000B2B RID: 2859
		public const string CategoryId = "PartyHotKeyCategory";

		// Token: 0x04000B2C RID: 2860
		public const string TakeAllTroops = "TakeAllTroops";

		// Token: 0x04000B2D RID: 2861
		public const string GiveAllTroops = "GiveAllTroops";

		// Token: 0x04000B2E RID: 2862
		public const string TakeAllPrisoners = "TakeAllPrisoners";

		// Token: 0x04000B2F RID: 2863
		public const string GiveAllPrisoners = "GiveAllPrisoners";

		// Token: 0x04000B30 RID: 2864
		public const string PopupItemPrimaryAction = "PopupItemPrimaryAction";

		// Token: 0x04000B31 RID: 2865
		public const string PopupItemSecondaryAction = "PopupItemSecondaryAction";

		// Token: 0x04000B32 RID: 2866
		public const string OpenUpgradePopup = "OpenUpgradePopup";

		// Token: 0x04000B33 RID: 2867
		public const string OpenRecruitPopup = "OpenRecruitPopup";
	}
}
