using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000333 RID: 819
	[Obsolete]
	public class DestructableMissionObject : MissionObject
	{
		// Token: 0x06002C45 RID: 11333 RVA: 0x000AD8AC File Offset: 0x000ABAAC
		protected internal override void OnEditorInit()
		{
			Debug.FailedAssert("This scene is using old prefabs with the old destruction system, please update!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\DestructableMissionObject.cs", "OnEditorInit", 18);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000AD8C4 File Offset: 0x000ABAC4
		protected internal override void OnInit()
		{
			Debug.FailedAssert("This scene is using old prefabs with the old destruction system, please update! The game will now close!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\DestructableMissionObject.cs", "OnInit", 23);
			Environment.Exit(0);
		}
	}
}
