using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection.SaveLoad;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000033 RID: 51
	[OverrideView(typeof(MapSaveView))]
	public class GauntletMapSaveView : MapView
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new MapSaveVM(new Action<bool>(this.OnStateChange));
			base.Layer = new GauntletLayer(10000, "GauntletLayer", false);
			(base.Layer as GauntletLayer).LoadMovie("MapSave", this._dataSource);
			base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.MouseButtons | InputUsageMask.Keyboardkeys);
			base.MapScreen.AddLayer(base.Layer);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D640 File Offset: 0x0000B840
		private void OnStateChange(bool isActive)
		{
			if (isActive)
			{
				base.Layer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(base.Layer);
				base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
				return;
			}
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			base.Layer.InputRestrictions.ResetInputRestrictions();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D6A1 File Offset: 0x0000B8A1
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._dataSource.OnFinalize();
			base.MapScreen.RemoveLayer(base.Layer);
			base.Layer = null;
			this._dataSource = null;
		}

		// Token: 0x040000EA RID: 234
		private MapSaveVM _dataSource;
	}
}
