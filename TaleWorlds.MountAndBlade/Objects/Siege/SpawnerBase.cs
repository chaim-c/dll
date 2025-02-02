using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x0200039A RID: 922
	public class SpawnerBase : ScriptComponentBehavior
	{
		// Token: 0x060031D1 RID: 12753 RVA: 0x000CDB29 File Offset: 0x000CBD29
		protected internal override bool OnCheckForProblems()
		{
			return !this._spawnerEditorHelper.IsValid;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000CDB39 File Offset: 0x000CBD39
		public virtual void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			Debug.FailedAssert("Please override 'AssignParameters' function in the derived class.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SpawnerBase.cs", "AssignParameters", 40);
		}

		// Token: 0x04001584 RID: 5508
		[EditorVisibleScriptComponentVariable(true)]
		public string ToBeSpawnedOverrideName = "";

		// Token: 0x04001585 RID: 5509
		[EditorVisibleScriptComponentVariable(true)]
		public string ToBeSpawnedOverrideNameForFireVersion = "";

		// Token: 0x04001586 RID: 5510
		protected SpawnerEntityEditorHelper _spawnerEditorHelper;

		// Token: 0x04001587 RID: 5511
		protected SpawnerEntityMissionHelper _spawnerMissionHelper;

		// Token: 0x04001588 RID: 5512
		protected SpawnerEntityMissionHelper _spawnerMissionHelperFire;

		// Token: 0x0200064B RID: 1611
		public class SpawnerPermissionField : EditorVisibleScriptComponentVariable
		{
			// Token: 0x06003D2C RID: 15660 RVA: 0x000ECD5B File Offset: 0x000EAF5B
			public SpawnerPermissionField() : base(false)
			{
			}
		}
	}
}
