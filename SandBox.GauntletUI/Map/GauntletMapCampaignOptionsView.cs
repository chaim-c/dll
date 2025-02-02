using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000026 RID: 38
	[OverrideView(typeof(MapCampaignOptionsView))]
	public class GauntletMapCampaignOptionsView : MapCampaignOptionsView
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0000B03C File Offset: 0x0000923C
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new CampaignOptionsVM(new Action(this.OnClose));
			base.Layer = new GauntletLayer(4401, "GauntletLayer", false)
			{
				IsFocusLayer = true
			};
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._layerAsGauntletLayer.LoadMovie("CampaignOptions", this._dataSource);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.MapScreen.AddLayer(base.Layer);
			base.MapScreen.PauseAmbientSounds();
			ScreenManager.TrySetFocus(base.Layer);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B0FE File Offset: 0x000092FE
		private void OnClose()
		{
			MapScreen.Instance.CloseCampaignOptions();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B10A File Offset: 0x0000930A
		protected override void OnIdleTick(float dt)
		{
			base.OnFrameTick(dt);
			if (base.Layer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._dataSource.ExecuteDone();
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B140 File Offset: 0x00009340
		protected override void OnFinalize()
		{
			base.OnFinalize();
			base.Layer.InputRestrictions.ResetInputRestrictions();
			base.MapScreen.RemoveLayer(base.Layer);
			base.MapScreen.RestartAmbientSounds();
			ScreenManager.TryLoseFocus(base.Layer);
			base.Layer = null;
			this._dataSource = null;
			this._layerAsGauntletLayer = null;
		}

		// Token: 0x040000AB RID: 171
		private CampaignOptionsVM _dataSource;

		// Token: 0x040000AC RID: 172
		private GauntletLayer _layerAsGauntletLayer;
	}
}
