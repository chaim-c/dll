using System;
using System.Runtime.CompilerServices;
using MCM.UI.GUI.ViewModels;
using Microsoft.Extensions.Logging;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace MCM.UI.GUI.GauntletUI
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModOptionsGauntletScreen : ScreenBase, IMCMOptionsScreen
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000077B0 File Offset: 0x000059B0
		public ModOptionsGauntletScreen(ILogger<ModOptionsGauntletScreen> logger)
		{
			this._logger = logger;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000077C8 File Offset: 0x000059C8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			SpriteCategory spriteCategoryMCMVal;
			this._spriteCategoryMCM = (spriteData.SpriteCategories.TryGetValue("ui_mcm", out spriteCategoryMCMVal) ? spriteCategoryMCMVal : null);
			SpriteCategory spriteCategoryMCM = this._spriteCategoryMCM;
			if (spriteCategoryMCM != null)
			{
				spriteCategoryMCM.Load(resourceContext, uiresourceDepot);
			}
			this._dataSource = new ModOptionsVM();
			this._gauntletLayer = new GauntletLayer(4000, "GauntletLayer", false);
			this._gauntletMovie = this._gauntletLayer.LoadMovie("ModOptionsView_MCM", this._dataSource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			base.AddLayer(this._gauntletLayer);
			ScreenManager.TrySetFocus(this._gauntletLayer);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000078B4 File Offset: 0x00005AB4
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			bool flag = this._gauntletLayer != null && this._gauntletLayer.Input.IsHotKeyReleased("Exit");
			if (flag)
			{
				this._dataSource.ExecuteClose();
				ScreenManager.TryLoseFocus(this._gauntletLayer);
				ScreenManager.PopScreen();
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007910 File Offset: 0x00005B10
		protected override void OnFinalize()
		{
			base.OnFinalize();
			bool flag = this._spriteCategoryMCM != null;
			if (flag)
			{
				this._spriteCategoryMCM.Unload();
			}
			bool flag2 = this._gauntletLayer != null;
			if (flag2)
			{
				base.RemoveLayer(this._gauntletLayer);
			}
			bool flag3 = this._gauntletLayer != null && this._gauntletMovie != null;
			if (flag3)
			{
				this._gauntletLayer.ReleaseMovie(this._gauntletMovie);
			}
			this._gauntletLayer = null;
			this._gauntletMovie = null;
			this._dataSource.ExecuteSelect(null);
			this._dataSource = null;
		}

		// Token: 0x04000061 RID: 97
		private readonly ILogger<ModOptionsGauntletScreen> _logger;

		// Token: 0x04000062 RID: 98
		[Nullable(2)]
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000063 RID: 99
		[Nullable(2)]
		private IGauntletMovie _gauntletMovie;

		// Token: 0x04000064 RID: 100
		private ModOptionsVM _dataSource = null;

		// Token: 0x04000065 RID: 101
		[Nullable(2)]
		private SpriteCategory _spriteCategoryMCM;
	}
}
