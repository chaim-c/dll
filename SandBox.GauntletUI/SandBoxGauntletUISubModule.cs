using System;
using SandBox.GauntletUI.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.GauntletUI;
using TaleWorlds.MountAndBlade.GauntletUI.SceneNotification;

namespace SandBox.GauntletUI
{
	// Token: 0x02000011 RID: 17
	public class SandBoxGauntletUISubModule : MBSubModuleBase
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00007445 File Offset: 0x00005645
		public override void OnCampaignStart(Game game, object starterObject)
		{
			base.OnCampaignStart(game, starterObject);
			if (!this._gameStarted && game.GameType is Campaign)
			{
				this._gameStarted = true;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000746B File Offset: 0x0000566B
		public override void OnGameEnd(Game game)
		{
			base.OnGameEnd(game);
			if (this._gameStarted && game.GameType is Campaign)
			{
				this._gameStarted = false;
				GauntletGameNotification.OnFinalize();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007495 File Offset: 0x00005695
		public override void BeginGameStart(Game game)
		{
			base.BeginGameStart(game);
			if (Campaign.Current != null)
			{
				Campaign.Current.VisualCreator.MapEventVisualCreator = new GauntletMapEventVisualCreator();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000074B9 File Offset: 0x000056B9
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			if (!this._initialized)
			{
				if (!Utilities.CommandLineArgumentExists("VisualTests"))
				{
					GauntletSceneNotification.Current.RegisterContextProvider(new SandboxSceneNotificationContextProvider());
				}
				this._initialized = true;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000074EB File Offset: 0x000056EB
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
			if (!this._gameStarted && game.GameType is Campaign)
			{
				this._gameStarted = true;
			}
		}

		// Token: 0x0400004F RID: 79
		private bool _gameStarted;

		// Token: 0x04000050 RID: 80
		private bool _initialized;
	}
}
