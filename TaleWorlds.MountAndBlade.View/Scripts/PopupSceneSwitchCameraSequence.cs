using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x0200003C RID: 60
	public class PopupSceneSwitchCameraSequence : PopupSceneSequence
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x000189BC File Offset: 0x00016BBC
		protected override void OnInit()
		{
			this._switchEntity = base.GameEntity.Scene.GetFirstEntityWithName(this.EntityName);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000189DC File Offset: 0x00016BDC
		public override void OnInitialState()
		{
			if (this._switchEntity != null)
			{
				GameEntity gameEntity = base.GameEntity.Scene.FindEntityWithTag("customcamera");
				if (gameEntity != null)
				{
					gameEntity.RemoveTag("customcamera");
				}
				this._switchEntity.AddTag("customcamera");
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00018A2C File Offset: 0x00016C2C
		public override void OnPositiveState()
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00018A2E File Offset: 0x00016C2E
		public override void OnNegativeState()
		{
		}

		// Token: 0x040001E6 RID: 486
		public string EntityName = "";

		// Token: 0x040001E7 RID: 487
		private GameEntity _switchEntity;
	}
}
