using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000228 RID: 552
	public class GenericPanelGameKeyCategory : GameKeyContext
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0006D1F9 File Offset: 0x0006B3F9
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x0006D200 File Offset: 0x0006B400
		public static GenericPanelGameKeyCategory Current { get; private set; }

		// Token: 0x06001E96 RID: 7830 RVA: 0x0006D208 File Offset: 0x0006B408
		public GenericPanelGameKeyCategory(string categoryId = "GenericPanelGameKeyCategory") : base(categoryId, 108, GameKeyContext.GameKeyContextType.Default)
		{
			GenericPanelGameKeyCategory.Current = this;
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0006D22C File Offset: 0x0006B42C
		private void RegisterHotKeys()
		{
			List<Key> keys = new List<Key>
			{
				new Key(InputKey.Escape),
				new Key(InputKey.ControllerRRight)
			};
			List<Key> keys2 = new List<Key>
			{
				new Key(InputKey.Enter),
				new Key(InputKey.NumpadEnter),
				new Key(InputKey.ControllerRLeft)
			};
			List<Key> keys3 = new List<Key>
			{
				new Key(InputKey.ControllerRUp)
			};
			List<Key> keys4 = new List<Key>
			{
				new Key(InputKey.Escape),
				new Key(InputKey.ControllerROption)
			};
			List<Key> keys5 = new List<Key>
			{
				new Key(InputKey.Q),
				new Key(InputKey.ControllerLBumper)
			};
			List<Key> keys6 = new List<Key>
			{
				new Key(InputKey.E),
				new Key(InputKey.ControllerRBumper)
			};
			List<Key> keys7 = new List<Key>
			{
				new Key(InputKey.D),
				new Key(InputKey.ControllerRTrigger)
			};
			List<Key> keys8 = new List<Key>
			{
				new Key(InputKey.A),
				new Key(InputKey.ControllerLTrigger)
			};
			List<Key> keys9 = new List<Key>
			{
				new Key(InputKey.R),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> keys10 = new List<Key>
			{
				new Key(InputKey.ControllerROption)
			};
			List<Key> keys11 = new List<Key>
			{
				new Key(InputKey.Delete),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> keys12 = new List<Key>
			{
				new Key(InputKey.Escape),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> keys13 = new List<Key>
			{
				new Key(InputKey.Enter),
				new Key(InputKey.NumpadEnter),
				new Key(InputKey.ControllerRDown)
			};
			base.RegisterHotKey(new HotKey("Exit", "GenericPanelGameKeyCategory", keys, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Confirm", "GenericPanelGameKeyCategory", keys2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Reset", "GenericPanelGameKeyCategory", keys3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("ToggleEscapeMenu", "GenericPanelGameKeyCategory", keys4, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SwitchToPreviousTab", "GenericPanelGameKeyCategory", keys5, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SwitchToNextTab", "GenericPanelGameKeyCategory", keys6, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("GiveAll", "GenericPanelGameKeyCategory", keys7, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("TakeAll", "GenericPanelGameKeyCategory", keys8, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Randomize", "GenericPanelGameKeyCategory", keys9, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Start", "GenericPanelGameKeyCategory", keys10, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Delete", "GenericPanelGameKeyCategory", keys11, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SelectProfile", "GenericPanelGameKeyCategory", keys12, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("Play", "GenericPanelGameKeyCategory", keys13, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0006D55D File Offset: 0x0006B75D
		private void RegisterGameKeys()
		{
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0006D55F File Offset: 0x0006B75F
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000ACB RID: 2763
		public const string CategoryId = "GenericPanelGameKeyCategory";

		// Token: 0x04000ACC RID: 2764
		public const string Exit = "Exit";

		// Token: 0x04000ACD RID: 2765
		public const string Confirm = "Confirm";

		// Token: 0x04000ACE RID: 2766
		public const string ResetChanges = "Reset";

		// Token: 0x04000ACF RID: 2767
		public const string ToggleEscapeMenu = "ToggleEscapeMenu";

		// Token: 0x04000AD0 RID: 2768
		public const string SwitchToPreviousTab = "SwitchToPreviousTab";

		// Token: 0x04000AD1 RID: 2769
		public const string SwitchToNextTab = "SwitchToNextTab";

		// Token: 0x04000AD2 RID: 2770
		public const string GiveAll = "GiveAll";

		// Token: 0x04000AD3 RID: 2771
		public const string TakeAll = "TakeAll";

		// Token: 0x04000AD4 RID: 2772
		public const string Randomize = "Randomize";

		// Token: 0x04000AD5 RID: 2773
		public const string Start = "Start";

		// Token: 0x04000AD6 RID: 2774
		public const string Delete = "Delete";

		// Token: 0x04000AD7 RID: 2775
		public const string SelectProfile = "SelectProfile";

		// Token: 0x04000AD8 RID: 2776
		public const string Play = "Play";
	}
}
