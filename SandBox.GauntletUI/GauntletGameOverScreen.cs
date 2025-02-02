using System;
using SandBox.ViewModelCollection.GameOver;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x02000009 RID: 9
	[GameStateScreen(typeof(GameOverState))]
	public class GauntletGameOverScreen : ScreenBase, IGameOverStateHandler, IGameStateListener
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00004D9A File Offset: 0x00002F9A
		public GauntletGameOverScreen(GameOverState gameOverState)
		{
			this._gameOverState = gameOverState;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004DA9 File Offset: 0x00002FA9
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this.CloseGameOverScreen();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004DDC File Offset: 0x00002FDC
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._gameOverCategory = spriteData.SpriteCategories["ui_gameover"];
			this._gameOverCategory.Load(resourceContext, uiresourceDepot);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", true);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			base.AddLayer(this._gauntletLayer);
			this._dataSource = new GameOverVM(this._gameOverState.Reason, new Action(this.CloseGameOverScreen));
			this._dataSource.SetCloseInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._gauntletLayer.LoadMovie("GameOverScreen", this._dataSource);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.GameOverScreen));
			switch (this._gameOverState.Reason)
			{
			case GameOverState.GameOverReason.Retirement:
				UISoundsHelper.PlayUISound("event:/ui/endgame/end_retirement");
				break;
			case GameOverState.GameOverReason.ClanDestroyed:
				UISoundsHelper.PlayUISound("event:/ui/endgame/end_clan_destroyed");
				break;
			case GameOverState.GameOverReason.Victory:
				UISoundsHelper.PlayUISound("event:/ui/endgame/end_victory");
				break;
			}
			LoadingWindow.DisableGlobalLoadingWindow();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004F3A File Offset: 0x0000313A
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
			base.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.None));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004F7A File Offset: 0x0000317A
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004F7C File Offset: 0x0000317C
		void IGameStateListener.OnFinalize()
		{
			this._gameOverCategory.Unload();
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer = null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004FA2 File Offset: 0x000031A2
		private void CloseGameOverScreen()
		{
			if (false || Game.Current.IsDevelopmentMode || this._gameOverState.Reason == GameOverState.GameOverReason.Victory)
			{
				Game.Current.GameStateManager.PopState(0);
				return;
			}
			MBGameManager.EndGame();
		}

		// Token: 0x0400002E RID: 46
		private SpriteCategory _gameOverCategory;

		// Token: 0x0400002F RID: 47
		private GameOverVM _dataSource;

		// Token: 0x04000030 RID: 48
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000031 RID: 49
		private readonly GameOverState _gameOverState;
	}
}
