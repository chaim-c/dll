using System;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x02000019 RID: 25
	public class MissionItemCalatogView : MissionView
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000098AC File Offset: 0x00007AAC
		public override void AfterStart()
		{
			base.AfterStart();
			this._itemCatalogController = base.Mission.GetMissionBehavior<ItemCatalogController>();
			this._itemCatalogController.BeforeCatalogTick += this.OnBeforeCatalogTick;
			this._itemCatalogController.AfterCatalogTick += this.OnAfterCatalogTick;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000098FE File Offset: 0x00007AFE
		private void OnBeforeCatalogTick(int currentItemIndex)
		{
			Utilities.TakeScreenshot("ItemCatalog/" + this._itemCatalogController.AllItems[currentItemIndex - 1].Name + ".bmp");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000992C File Offset: 0x00007B2C
		private void OnAfterCatalogTick()
		{
			MatrixFrame frame = default(MatrixFrame);
			Vec3 lookDirection = base.Mission.MainAgent.LookDirection;
			frame.origin = base.Mission.MainAgent.Position + lookDirection * 2f + new Vec3(0f, 0f, 1.273f, -1f);
			frame.rotation.u = lookDirection;
			frame.rotation.s = new Vec3(1f, 0f, 0f, -1f);
			frame.rotation.f = new Vec3(0f, 0f, 1f, -1f);
			frame.rotation.Orthonormalize();
			base.Mission.SetCameraFrame(ref frame, 1f);
			Camera camera = Camera.CreateCamera();
			camera.Frame = frame;
			base.MissionScreen.CustomCamera = camera;
		}

		// Token: 0x04000073 RID: 115
		private ItemCatalogController _itemCatalogController;
	}
}
