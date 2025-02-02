using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x0200002F RID: 47
	[GameStateScreen(typeof(GameLoadingState))]
	public class GameLoadingScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
		public GameLoadingScreen(GameLoadingState gameLoadingState)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000F9CE File Offset: 0x0000DBCE
		protected override void OnActivate()
		{
			base.OnActivate();
			LoadingWindow.EnableGlobalLoadingWindow();
			Utilities.SetScreenTextRenderingState(false);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000F9E1 File Offset: 0x0000DBE1
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			Utilities.SetScreenTextRenderingState(true);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000F9F2 File Offset: 0x0000DBF2
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		void IGameStateListener.OnFinalize()
		{
		}
	}
}
