using System;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Menu
{
	// Token: 0x0200001B RID: 27
	[OverrideView(typeof(MenuBaseView))]
	public class GauntletMenuBaseView : MenuView
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00009062 File Offset: 0x00007262
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000906A File Offset: 0x0000726A
		public GameMenuVM GameMenuDataSource { get; private set; }

		// Token: 0x06000119 RID: 281 RVA: 0x00009074 File Offset: 0x00007274
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.GameMenuDataSource = new GameMenuVM(base.MenuContext);
			GameKey gameKey = HotKeyManager.GetCategory("Generic").GetGameKey(4);
			this.GameMenuDataSource.AddHotKey(GameMenuOption.LeaveType.Leave, gameKey);
			base.Layer = base.MenuViewContext.FindLayer<GauntletLayer>("BasicLayer");
			if (base.Layer == null)
			{
				base.Layer = new GauntletLayer(100, "GauntletLayer", false)
				{
					Name = "BasicLayer"
				};
				base.MenuViewContext.AddLayer(base.Layer);
			}
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._movie = this._layerAsGauntletLayer.LoadMovie("GameMenu", this.GameMenuDataSource);
			ScreenManager.TrySetFocus(base.Layer);
			this._layerAsGauntletLayer.UIContext.ContextAlpha = 1f;
			MBInformationManager.HideInformations();
			this.GainGamepadNavigationAfterSeconds(0.25f);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009161 File Offset: 0x00007361
		protected override void OnActivate()
		{
			base.OnActivate();
			this.GameMenuDataSource.Refresh(true);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009175 File Offset: 0x00007375
		protected override void OnResume()
		{
			base.OnResume();
			this.GameMenuDataSource.Refresh(true);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000918C File Offset: 0x0000738C
		protected override void OnFinalize()
		{
			this.GameMenuDataSource.OnFinalize();
			this.GameMenuDataSource = null;
			ScreenManager.TryLoseFocus(base.Layer);
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			this._layerAsGauntletLayer = null;
			base.Layer = null;
			this._movie = null;
			base.OnFinalize();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000091E2 File Offset: 0x000073E2
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.GameMenuDataSource.OnFrameTick();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000091F6 File Offset: 0x000073F6
		protected override void OnMapConversationActivated()
		{
			base.OnMapConversationActivated();
			GauntletLayer layerAsGauntletLayer = this._layerAsGauntletLayer;
			if (((layerAsGauntletLayer != null) ? layerAsGauntletLayer.UIContext : null) != null)
			{
				this._layerAsGauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009227 File Offset: 0x00007427
		protected override void OnMapConversationDeactivated()
		{
			base.OnMapConversationDeactivated();
			GauntletLayer layerAsGauntletLayer = this._layerAsGauntletLayer;
			if (((layerAsGauntletLayer != null) ? layerAsGauntletLayer.UIContext : null) != null)
			{
				this._layerAsGauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009258 File Offset: 0x00007458
		protected override void OnMenuContextUpdated(MenuContext newMenuContext)
		{
			base.OnMenuContextUpdated(newMenuContext);
			this.GameMenuDataSource.UpdateMenuContext(newMenuContext);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000926D File Offset: 0x0000746D
		protected override void OnBackgroundMeshNameSet(string name)
		{
			base.OnBackgroundMeshNameSet(name);
			this.GameMenuDataSource.Background = name;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009282 File Offset: 0x00007482
		private void GainGamepadNavigationAfterSeconds(float seconds)
		{
			this._layerAsGauntletLayer.UIContext.GamepadNavigation.GainNavigationAfterTime(seconds, () => this.GameMenuDataSource.ItemList.Count > 0);
		}

		// Token: 0x04000077 RID: 119
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x04000078 RID: 120
		private IGauntletMovie _movie;
	}
}
