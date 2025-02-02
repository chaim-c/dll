using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x0200038F RID: 911
	public class MangonelSpawner : SpawnerBase
	{
		// Token: 0x060031A9 RID: 12713 RVA: 0x000CCED7 File Offset: 0x000CB0D7
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000CCEFC File Offset: 0x000CB0FC
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			foreach (GameEntity gameEntity in _spawnerMissionHelper.SpawnedEntity.GetChildren())
			{
				if (gameEntity.GetFirstScriptOfType<Mangonel>() != null)
				{
					gameEntity.GetFirstScriptOfType<Mangonel>().AddOnDeployTag = this.AddOnDeployTag;
					gameEntity.GetFirstScriptOfType<Mangonel>().RemoveOnDeployTag = this.RemoveOnDeployTag;
					break;
				}
			}
		}

		// Token: 0x0400154D RID: 5453
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame projectile_pile = MatrixFrame.Zero;

		// Token: 0x0400154E RID: 5454
		[EditorVisibleScriptComponentVariable(true)]
		public string AddOnDeployTag = "";

		// Token: 0x0400154F RID: 5455
		[EditorVisibleScriptComponentVariable(true)]
		public string RemoveOnDeployTag = "";

		// Token: 0x04001550 RID: 5456
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_a_enabled = true;

		// Token: 0x04001551 RID: 5457
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_b_enabled = true;

		// Token: 0x04001552 RID: 5458
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_c_enabled = true;

		// Token: 0x04001553 RID: 5459
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_d_enabled = true;

		// Token: 0x04001554 RID: 5460
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_e_enabled = true;

		// Token: 0x04001555 RID: 5461
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_f_enabled = true;

		// Token: 0x04001556 RID: 5462
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_g_enabled = true;

		// Token: 0x04001557 RID: 5463
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_h_enabled = true;
	}
}
