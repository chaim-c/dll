using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x0200038B RID: 907
	public class BallistaSpawner : SpawnerBase
	{
		// Token: 0x0600319A RID: 12698 RVA: 0x000CCA26 File Offset: 0x000CAC26
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000CCA48 File Offset: 0x000CAC48
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<Ballista>().AddOnDeployTag = this.AddOnDeployTag;
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<Ballista>().RemoveOnDeployTag = this.RemoveOnDeployTag;
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<Ballista>().HorizontalDirectionRestriction = this.DirectionRestrictionDegree * 0.017453292f;
		}

		// Token: 0x0400153A RID: 5434
		[EditorVisibleScriptComponentVariable(true)]
		public string AddOnDeployTag = "";

		// Token: 0x0400153B RID: 5435
		[EditorVisibleScriptComponentVariable(true)]
		public string RemoveOnDeployTag = "";

		// Token: 0x0400153C RID: 5436
		[EditorVisibleScriptComponentVariable(true)]
		public float DirectionRestrictionDegree = 90f;
	}
}
