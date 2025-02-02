using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.Menu
{
	// Token: 0x02000032 RID: 50
	[GameStateScreen(typeof(TutorialState))]
	public class TutorialScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00012143 File Offset: 0x00010343
		public MenuViewContext MenuViewContext { get; }

		// Token: 0x060001B1 RID: 433 RVA: 0x0001214B File Offset: 0x0001034B
		public TutorialScreen(TutorialState tutorialState)
		{
			this.MenuViewContext = new MenuViewContext(this, tutorialState.MenuContext);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00012165 File Offset: 0x00010365
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.MenuViewContext.OnFrameTick(dt);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001217A File Offset: 0x0001037A
		protected override void OnActivate()
		{
			base.OnActivate();
			this.MenuViewContext.OnActivate();
			LoadingWindow.DisableGlobalLoadingWindow();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00012192 File Offset: 0x00010392
		protected override void OnDeactivate()
		{
			this.MenuViewContext.OnDeactivate();
			base.OnDeactivate();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000121A5 File Offset: 0x000103A5
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.MenuViewContext.OnInitialize();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000121B8 File Offset: 0x000103B8
		protected override void OnFinalize()
		{
			this.MenuViewContext.OnFinalize();
			base.OnFinalize();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000121CB File Offset: 0x000103CB
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000121CD File Offset: 0x000103CD
		void IGameStateListener.OnDeactivate()
		{
			this.MenuViewContext.OnGameStateDeactivate();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000121DA File Offset: 0x000103DA
		void IGameStateListener.OnInitialize()
		{
			this.MenuViewContext.OnGameStateInitialize();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000121E7 File Offset: 0x000103E7
		void IGameStateListener.OnFinalize()
		{
			this.MenuViewContext.OnGameStateFinalize();
		}
	}
}
