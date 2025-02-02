using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023E RID: 574
	public class ProfileSelectionState : GameState
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0006EF5F File Offset: 0x0006D15F
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x0006EF67 File Offset: 0x0006D167
		public bool IsDirectPlayPossible { get; private set; } = true;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001F38 RID: 7992 RVA: 0x0006EF70 File Offset: 0x0006D170
		// (remove) Token: 0x06001F39 RID: 7993 RVA: 0x0006EFA8 File Offset: 0x0006D1A8
		public event ProfileSelectionState.OnProfileSelectionEvent OnProfileSelection;

		// Token: 0x06001F3A RID: 7994 RVA: 0x0006EFDD File Offset: 0x0006D1DD
		public void OnProfileSelected()
		{
			NativeOptions.ReadRGLConfigFiles();
			BannerlordConfig.Initialize();
			ProfileSelectionState.OnProfileSelectionEvent onProfileSelection = this.OnProfileSelection;
			if (onProfileSelection != null)
			{
				onProfileSelection();
			}
			this.StartGame();
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0006F000 File Offset: 0x0006D200
		public void StartGame()
		{
			Module.CurrentModule.SetInitialModuleScreenAsRootScreen();
		}

		// Token: 0x02000504 RID: 1284
		// (Invoke) Token: 0x06003823 RID: 14371
		public delegate void OnProfileSelectionEvent();
	}
}
