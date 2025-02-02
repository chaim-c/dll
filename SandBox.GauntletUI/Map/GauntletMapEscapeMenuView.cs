using System;
using System.Collections.Generic;
using SandBox.View.Map;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000029 RID: 41
	[OverrideView(typeof(MapEscapeMenuView))]
	public class GauntletMapEscapeMenuView : MapView
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000BF77 File Offset: 0x0000A177
		public GauntletMapEscapeMenuView(List<EscapeMenuItemVM> items)
		{
			this._menuItems = items;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BF88 File Offset: 0x0000A188
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._escapeMenuDatasource = new EscapeMenuVM(this._menuItems, null);
			base.Layer = new GauntletLayer(4400, "GauntletLayer", false)
			{
				IsFocusLayer = true
			};
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._escapeMenuMovie = this._layerAsGauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.MapScreen.AddLayer(base.Layer);
			base.MapScreen.PauseAmbientSounds();
			ScreenManager.TrySetFocus(base.Layer);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C04C File Offset: 0x0000A24C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (base.Layer.Input.IsHotKeyReleased("ToggleEscapeMenu") || base.Layer.Input.IsHotKeyReleased("Exit"))
			{
				MapScreen.Instance.CloseEscapeMenu();
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000C098 File Offset: 0x0000A298
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			if (base.Layer.Input.IsHotKeyReleased("ToggleEscapeMenu") || base.Layer.Input.IsHotKeyReleased("Exit"))
			{
				MapScreen.Instance.CloseEscapeMenu();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		protected override bool IsEscaped()
		{
			return base.Layer.Input.IsHotKeyReleased("ToggleEscapeMenu");
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		protected override void OnFinalize()
		{
			base.OnFinalize();
			base.Layer.InputRestrictions.ResetInputRestrictions();
			base.MapScreen.RemoveLayer(base.Layer);
			base.MapScreen.RestartAmbientSounds();
			ScreenManager.TryLoseFocus(base.Layer);
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x040000BC RID: 188
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000BD RID: 189
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x040000BE RID: 190
		private IGauntletMovie _escapeMenuMovie;

		// Token: 0x040000BF RID: 191
		private readonly List<EscapeMenuItemVM> _menuItems;
	}
}
