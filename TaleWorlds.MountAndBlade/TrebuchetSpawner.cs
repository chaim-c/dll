using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034E RID: 846
	public class TrebuchetSpawner : SpawnerBase
	{
		// Token: 0x06002E49 RID: 11849 RVA: 0x000BD5E3 File Offset: 0x000BB7E3
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000BD608 File Offset: 0x000BB808
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			foreach (GameEntity gameEntity in _spawnerMissionHelper.SpawnedEntity.GetChildren())
			{
				if (gameEntity.GetFirstScriptOfType<Trebuchet>() != null)
				{
					gameEntity.GetFirstScriptOfType<Trebuchet>().AddOnDeployTag = this.AddOnDeployTag;
					gameEntity.GetFirstScriptOfType<Trebuchet>().RemoveOnDeployTag = this.RemoveOnDeployTag;
					break;
				}
			}
		}

		// Token: 0x0400136A RID: 4970
		[SpawnerBase.SpawnerPermissionField]
		public MatrixFrame projectile_pile = MatrixFrame.Zero;

		// Token: 0x0400136B RID: 4971
		[EditorVisibleScriptComponentVariable(true)]
		public string AddOnDeployTag = "";

		// Token: 0x0400136C RID: 4972
		[EditorVisibleScriptComponentVariable(true)]
		public string RemoveOnDeployTag = "";

		// Token: 0x0400136D RID: 4973
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_a_enabled = true;

		// Token: 0x0400136E RID: 4974
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_b_enabled = true;

		// Token: 0x0400136F RID: 4975
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_c_enabled = true;

		// Token: 0x04001370 RID: 4976
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_d_enabled = true;

		// Token: 0x04001371 RID: 4977
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_e_enabled = true;

		// Token: 0x04001372 RID: 4978
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_f_enabled = true;

		// Token: 0x04001373 RID: 4979
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_g_enabled = true;

		// Token: 0x04001374 RID: 4980
		[EditorVisibleScriptComponentVariable(true)]
		public bool ammo_pos_h_enabled = true;
	}
}
