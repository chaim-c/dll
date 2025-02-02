using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.Encyclopedia
{
	// Token: 0x02000039 RID: 57
	[OverrideView(typeof(MapEncyclopediaView))]
	public class GauntletMapEncyclopediaView : MapEncyclopediaView
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		protected override void CreateLayout()
		{
			base.CreateLayout();
			SpriteData spriteData = UIResourceManager.SpriteData;
			this._spriteCategory = spriteData.SpriteCategories["ui_encyclopedia"];
			this._spriteCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			this._homeDatasource = new EncyclopediaHomeVM(new EncyclopediaPageArgs(null));
			this._navigatorDatasource = new EncyclopediaNavigatorVM(new Func<string, object, bool, EncyclopediaPageVM>(this.ExecuteLink), new Action(this.CloseEncyclopedia));
			this.ListViewDataController = new EncyclopediaListViewDataController();
			this._game = Game.Current;
			Game game = this._game;
			game.AfterTick = (Action<float>)Delegate.Combine(game.AfterTick, new Action<float>(this.OnTick));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000ECD2 File Offset: 0x0000CED2
		internal void OnTick(float dt)
		{
			EncyclopediaData encyclopediaData = this._encyclopediaData;
			if (encyclopediaData == null)
			{
				return;
			}
			encyclopediaData.OnTick();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private EncyclopediaPageVM ExecuteLink(string pageId, object obj, bool needsRefresh)
		{
			this._navigatorDatasource.NavBarString = string.Empty;
			if (this._encyclopediaData == null)
			{
				this._encyclopediaData = new EncyclopediaData(this, ScreenManager.TopScreen, this._homeDatasource, this._navigatorDatasource);
			}
			if (pageId == "LastPage")
			{
				Tuple<string, object> lastPage = this._navigatorDatasource.GetLastPage();
				pageId = lastPage.Item1;
				obj = lastPage.Item2;
			}
			base.IsEncyclopediaOpen = true;
			if (!this._spriteCategory.IsLoaded)
			{
				this._spriteCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			}
			return this._encyclopediaData.ExecuteLink(pageId, obj, needsRefresh);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000ED84 File Offset: 0x0000CF84
		protected override void OnFinalize()
		{
			Game game = this._game;
			game.AfterTick = (Action<float>)Delegate.Remove(game.AfterTick, new Action<float>(this.OnTick));
			this._game = null;
			this._homeDatasource = null;
			this._navigatorDatasource.OnFinalize();
			this._navigatorDatasource = null;
			this._encyclopediaData = null;
			base.OnFinalize();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000EDE5 File Offset: 0x0000CFE5
		public override void CloseEncyclopedia()
		{
			this._encyclopediaData.CloseEncyclopedia();
			this._encyclopediaData = null;
			base.IsEncyclopediaOpen = false;
		}

		// Token: 0x04000105 RID: 261
		private EncyclopediaHomeVM _homeDatasource;

		// Token: 0x04000106 RID: 262
		private EncyclopediaNavigatorVM _navigatorDatasource;

		// Token: 0x04000107 RID: 263
		private EncyclopediaData _encyclopediaData;

		// Token: 0x04000108 RID: 264
		public EncyclopediaListViewDataController ListViewDataController;

		// Token: 0x04000109 RID: 265
		private SpriteCategory _spriteCategory;

		// Token: 0x0400010A RID: 266
		private Game _game;
	}
}
