using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MarriageOfferPopup;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000036 RID: 54
	[OverrideView(typeof(MarriageOfferPopupView))]
	public class GauntletMarriageOfferPopupView : MapView
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public GauntletMarriageOfferPopupView(Hero suitor, Hero maiden)
		{
			this._suitor = suitor;
			this._maiden = maiden;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DD10 File Offset: 0x0000BF10
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new MarriageOfferPopupVM(this._suitor, this._maiden);
			this.InitializeKeyVisuals();
			base.Layer = new GauntletLayer(201, "GauntletLayer", false);
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.Layer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(base.Layer);
			this._movie = this._layerAsGauntletLayer.LoadMovie("MarriageOfferPopup", this._dataSource);
			base.MapScreen.AddLayer(base.Layer);
			base.MapScreen.SetIsMarriageOfferPopupActive(true);
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			Campaign.Current.SetTimeControlModeLock(true);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DE13 File Offset: 0x0000C013
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.HandleInput();
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DE32 File Offset: 0x0000C032
		protected override void OnMenuModeTick(float dt)
		{
			base.OnMenuModeTick(dt);
			this.HandleInput();
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DE51 File Offset: 0x0000C051
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			this.HandleInput();
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000DE70 File Offset: 0x0000C070
		protected override void OnFinalize()
		{
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			base.MapScreen.RemoveLayer(base.Layer);
			this._movie = null;
			this._dataSource = null;
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			base.MapScreen.SetIsMarriageOfferPopupActive(false);
			Campaign.Current.SetTimeControlModeLock(false);
			base.OnFinalize();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		protected override bool IsEscaped()
		{
			MarriageOfferPopupVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.ExecuteDeclineOffer();
			}
			return true;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		protected override bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return false;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		private void HandleInput()
		{
			if (this._dataSource != null)
			{
				if (base.Layer.Input.IsGameKeyPressed(39))
				{
					base.MapScreen.OpenEncyclopedia();
					return;
				}
				if (base.Layer.Input.IsHotKeyReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					this._dataSource.ExecuteAcceptOffer();
					return;
				}
				if (base.Layer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					this._dataSource.ExecuteDeclineOffer();
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000DF7E File Offset: 0x0000C17E
		private void InitializeKeyVisuals()
		{
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
		}

		// Token: 0x040000F1 RID: 241
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000F2 RID: 242
		private MarriageOfferPopupVM _dataSource;

		// Token: 0x040000F3 RID: 243
		private IGauntletMovie _movie;

		// Token: 0x040000F4 RID: 244
		private Hero _suitor;

		// Token: 0x040000F5 RID: 245
		private Hero _maiden;
	}
}
