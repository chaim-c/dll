using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A0 RID: 416
	[ScriptingInterfaceBase]
	internal interface IMBActionSet
	{
		// Token: 0x06001572 RID: 5490
		[EngineMethod("get_index_with_id", false)]
		int GetIndexWithID(string id);

		// Token: 0x06001573 RID: 5491
		[EngineMethod("get_name_with_index", false)]
		string GetNameWithIndex(int index);

		// Token: 0x06001574 RID: 5492
		[EngineMethod("get_skeleton_name", false)]
		string GetSkeletonName(int index);

		// Token: 0x06001575 RID: 5493
		[EngineMethod("get_number_of_action_sets", false)]
		int GetNumberOfActionSets();

		// Token: 0x06001576 RID: 5494
		[EngineMethod("get_number_of_monster_usage_sets", false)]
		int GetNumberOfMonsterUsageSets();

		// Token: 0x06001577 RID: 5495
		[EngineMethod("get_animation_name", false)]
		string GetAnimationName(int index, int actionNo);

		// Token: 0x06001578 RID: 5496
		[EngineMethod("are_actions_alternatives", false)]
		bool AreActionsAlternatives(int index, int actionNo1, int actionNo2);

		// Token: 0x06001579 RID: 5497
		[EngineMethod("get_bone_index_with_id", false)]
		sbyte GetBoneIndexWithId(string actionSetId, string boneId);

		// Token: 0x0600157A RID: 5498
		[EngineMethod("get_bone_has_parent_bone", false)]
		bool GetBoneHasParentBone(string actionSetId, sbyte boneIndex);
	}
}
