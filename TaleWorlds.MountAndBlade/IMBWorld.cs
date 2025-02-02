using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B1 RID: 433
	[ScriptingInterfaceBase]
	internal interface IMBWorld
	{
		// Token: 0x06001794 RID: 6036
		[EngineMethod("get_global_time", false)]
		float GetGlobalTime(MBCommon.TimeType timeType);

		// Token: 0x06001795 RID: 6037
		[EngineMethod("get_last_messages", false)]
		string GetLastMessages();

		// Token: 0x06001796 RID: 6038
		[EngineMethod("get_game_type", false)]
		int GetGameType();

		// Token: 0x06001797 RID: 6039
		[EngineMethod("set_game_type", false)]
		void SetGameType(int gameType);

		// Token: 0x06001798 RID: 6040
		[EngineMethod("pause_game", false)]
		void PauseGame();

		// Token: 0x06001799 RID: 6041
		[EngineMethod("unpause_game", false)]
		void UnpauseGame();

		// Token: 0x0600179A RID: 6042
		[EngineMethod("set_mesh_used", false)]
		void SetMeshUsed(string meshName);

		// Token: 0x0600179B RID: 6043
		[EngineMethod("set_material_used", false)]
		void SetMaterialUsed(string materialName);

		// Token: 0x0600179C RID: 6044
		[EngineMethod("set_body_used", false)]
		void SetBodyUsed(string bodyName);

		// Token: 0x0600179D RID: 6045
		[EngineMethod("fix_skeletons", false)]
		void FixSkeletons();

		// Token: 0x0600179E RID: 6046
		[EngineMethod("check_resource_modifications", false)]
		void CheckResourceModifications();
	}
}
