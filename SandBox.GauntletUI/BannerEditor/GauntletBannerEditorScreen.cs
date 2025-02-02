using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.BannerEditor
{
	// Token: 0x02000042 RID: 66
	[GameStateScreen(typeof(BannerEditorState))]
	public class GauntletBannerEditorScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x06000296 RID: 662 RVA: 0x000137F0 File Offset: 0x000119F0
		public GauntletBannerEditorScreen(BannerEditorState bannerEditorState)
		{
			LoadingWindow.EnableGlobalLoadingWindow();
			this._clan = bannerEditorState.GetClan();
			this._bannerEditorLayer = new BannerEditorView(bannerEditorState.GetCharacter(), bannerEditorState.GetClan().Banner, new ControlCharacterCreationStage(this.OnDone), new TextObject("{=WiNRdfsm}Done", null), new ControlCharacterCreationStage(this.OnCancel), new TextObject("{=3CpNUnVl}Cancel", null), null, null, null, null, null);
			this._bannerEditorLayer.DataSource.SetClanRelatedRules(bannerEditorState.GetClan().Kingdom == null);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00013881 File Offset: 0x00011A81
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this._bannerEditorLayer.OnTick(dt);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00013898 File Offset: 0x00011A98
		public void OnDone()
		{
			uint primaryColor = this._bannerEditorLayer.DataSource.BannerVM.GetPrimaryColor();
			uint sigilColor = this._bannerEditorLayer.DataSource.BannerVM.GetSigilColor();
			this._clan.Color = primaryColor;
			this._clan.Color2 = sigilColor;
			this._clan.UpdateBannerColor(primaryColor, sigilColor);
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00013906 File Offset: 0x00011B06
		public void OnCancel()
		{
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00013918 File Offset: 0x00011B18
		protected override void OnInitialize()
		{
			base.OnInitialize();
			Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00013930 File Offset: 0x00011B30
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._bannerEditorLayer.OnFinalize();
			if (LoadingWindow.GetGlobalLoadingWindowState())
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0001395F File Offset: 0x00011B5F
		protected override void OnActivate()
		{
			base.OnActivate();
			base.AddLayer(this._bannerEditorLayer.GauntletLayer);
			base.AddLayer(this._bannerEditorLayer.SceneLayer);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00013989 File Offset: 0x00011B89
		protected override void OnDeactivate()
		{
			this._bannerEditorLayer.OnDeactivate();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00013996 File Offset: 0x00011B96
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00013998 File Offset: 0x00011B98
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0001399A File Offset: 0x00011B9A
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0001399C File Offset: 0x00011B9C
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x04000188 RID: 392
		private const int ViewOrderPriority = 15;

		// Token: 0x04000189 RID: 393
		private readonly BannerEditorView _bannerEditorLayer;

		// Token: 0x0400018A RID: 394
		private readonly Clan _clan;
	}
}
