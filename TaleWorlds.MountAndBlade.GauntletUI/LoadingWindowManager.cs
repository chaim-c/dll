using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000014 RID: 20
	public class LoadingWindowManager : GlobalLayer, ILoadingWindowManager
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00006300 File Offset: 0x00004500
		public LoadingWindowManager()
		{
			this._sploadingCategory = UIResourceManager.SpriteData.SpriteCategories["ui_loading"];
			this._sploadingCategory.InitializePartialLoad();
			this._loadingWindowViewModel = new LoadingWindowViewModel(new Action<bool, int>(this.HandleSPPartialLoading));
			this._loadingWindowViewModel.Enabled = false;
			this._loadingWindowViewModel.SetTotalGenericImageCount(this._sploadingCategory.SpriteSheetCount);
			bool shouldClear = false;
			this._gauntletLayer = new GauntletLayer(100003, "GauntletLayer", shouldClear);
			this._gauntletLayer.LoadMovie("LoadingWindow", this._loadingWindowViewModel);
			base.Layer = this._gauntletLayer;
			ScreenManager.AddGlobalLayer(this, false);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000063B3 File Offset: 0x000045B3
		protected override void OnLateTick(float dt)
		{
			base.OnLateTick(dt);
			this._loadingWindowViewModel.Update();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000063C7 File Offset: 0x000045C7
		void ILoadingWindowManager.EnableLoadingWindow()
		{
			this._loadingWindowViewModel.Enabled = true;
			base.Layer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(base.Layer);
			base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000063FE File Offset: 0x000045FE
		void ILoadingWindowManager.DisableLoadingWindow()
		{
			this._loadingWindowViewModel.Enabled = false;
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			base.Layer.InputRestrictions.ResetInputRestrictions();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006434 File Offset: 0x00004634
		public void SetCurrentModeIsMultiplayer(bool isMultiplayer)
		{
			if (this._isMultiplayer != isMultiplayer)
			{
				this._isMultiplayer = isMultiplayer;
				this._loadingWindowViewModel.IsMultiplayer = isMultiplayer;
				if (isMultiplayer)
				{
					this._mpLoadingCategory = UIResourceManager.SpriteData.SpriteCategories["ui_mploading"];
					this._mpLoadingCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
					this._mpBackgroundCategory = UIResourceManager.SpriteData.SpriteCategories["ui_mpbackgrounds"];
					this._mpBackgroundCategory.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
					return;
				}
				this._mpLoadingCategory.Unload();
				this._mpBackgroundCategory.Unload();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000064D8 File Offset: 0x000046D8
		private void HandleSPPartialLoading(bool isLoading, int index)
		{
			if (isLoading)
			{
				SpriteCategory sploadingCategory = this._sploadingCategory;
				if (sploadingCategory == null)
				{
					return;
				}
				sploadingCategory.PartialLoadAtIndex(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot, index);
				return;
			}
			else
			{
				SpriteCategory sploadingCategory2 = this._sploadingCategory;
				if (sploadingCategory2 == null)
				{
					return;
				}
				sploadingCategory2.PartialUnloadAtIndex(index);
				return;
			}
		}

		// Token: 0x04000073 RID: 115
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000074 RID: 116
		private LoadingWindowViewModel _loadingWindowViewModel;

		// Token: 0x04000075 RID: 117
		private SpriteCategory _sploadingCategory;

		// Token: 0x04000076 RID: 118
		private SpriteCategory _mpLoadingCategory;

		// Token: 0x04000077 RID: 119
		private SpriteCategory _mpBackgroundCategory;

		// Token: 0x04000078 RID: 120
		private bool _isMultiplayer;
	}
}
