using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.Credits;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000005 RID: 5
	[OverrideView(typeof(CreditsScreen))]
	public class GauntletCreditsScreen : ScreenBase
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003B34 File Offset: 0x00001D34
		protected override void OnInitialize()
		{
			base.OnInitialize();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._creditsCategory = spriteData.SpriteCategories["ui_credits"];
			this._creditsCategory.Load(resourceContext, uiresourceDepot);
			this._datasource = new CreditsVM();
			string path = ModuleHelper.GetModuleFullPath("Native") + "ModuleData/" + "Credits.xml";
			this._datasource.FillFromFile(path);
			this._gauntletLayer = new GauntletLayer(100, "GauntletLayer", false);
			this._gauntletLayer.IsFocusLayer = true;
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			ScreenManager.TrySetFocus(this._gauntletLayer);
			this._movie = this._gauntletLayer.LoadMovie("CreditsScreen", this._datasource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003C30 File Offset: 0x00001E30
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._creditsCategory.Unload();
			this._datasource.OnFinalize();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._gauntletLayer.Input.IsHotKeyPressed("Exit"))
			{
				ScreenManager.PopScreen();
			}
		}

		// Token: 0x0400002C RID: 44
		private CreditsVM _datasource;

		// Token: 0x0400002D RID: 45
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400002E RID: 46
		private IGauntletMovie _movie;

		// Token: 0x0400002F RID: 47
		private SpriteCategory _creditsCategory;
	}
}
