using System;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200002A RID: 42
	public interface IInputService
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000321 RID: 801
		bool MouseEnabled { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000322 RID: 802
		bool KeyboardEnabled { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000323 RID: 803
		bool GamepadEnabled { get; }
	}
}
