using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A3 RID: 419
	[ScriptingInterfaceBase]
	internal interface IMBAnimation
	{
		// Token: 0x06001695 RID: 5781
		[EngineMethod("get_id_with_index", false)]
		string GetIDWithIndex(int index);

		// Token: 0x06001696 RID: 5782
		[EngineMethod("get_index_with_id", false)]
		int GetIndexWithID(string id);

		// Token: 0x06001697 RID: 5783
		[EngineMethod("get_displacement_vector", false)]
		Vec3 GetDisplacementVector(int actionSetNo, int actionIndex);

		// Token: 0x06001698 RID: 5784
		[EngineMethod("check_animation_clip_exists", false)]
		bool CheckAnimationClipExists(int actionSetNo, int actionIndex);

		// Token: 0x06001699 RID: 5785
		[EngineMethod("prefetch_animation_clip", false)]
		void PrefetchAnimationClip(int actionSetNo, int actionIndex);

		// Token: 0x0600169A RID: 5786
		[EngineMethod("get_animation_index_of_action_code", false)]
		int AnimationIndexOfActionCode(int actionSetNo, int actionIndex);

		// Token: 0x0600169B RID: 5787
		[EngineMethod("get_animation_flags", false)]
		AnimFlags GetAnimationFlags(int actionSetNo, int actionIndex);

		// Token: 0x0600169C RID: 5788
		[EngineMethod("get_action_type", false)]
		Agent.ActionCodeType GetActionType(int actionIndex);

		// Token: 0x0600169D RID: 5789
		[EngineMethod("get_animation_duration", false)]
		float GetAnimationDuration(int animationIndex);

		// Token: 0x0600169E RID: 5790
		[EngineMethod("get_animation_parameter1", false)]
		float GetAnimationParameter1(int animationIndex);

		// Token: 0x0600169F RID: 5791
		[EngineMethod("get_animation_parameter2", false)]
		float GetAnimationParameter2(int animationIndex);

		// Token: 0x060016A0 RID: 5792
		[EngineMethod("get_animation_parameter3", false)]
		float GetAnimationParameter3(int animationIndex);

		// Token: 0x060016A1 RID: 5793
		[EngineMethod("get_action_animation_duration", false)]
		float GetActionAnimationDuration(int actionSetNo, int actionIndex);

		// Token: 0x060016A2 RID: 5794
		[EngineMethod("get_animation_name", false)]
		string GetAnimationName(int actionSetNo, int actionIndex);

		// Token: 0x060016A3 RID: 5795
		[EngineMethod("get_animation_continue_to_action", false)]
		int GetAnimationContinueToAction(int actionSetNo, int actionIndex);

		// Token: 0x060016A4 RID: 5796
		[EngineMethod("get_animation_blend_in_period", false)]
		float GetAnimationBlendInPeriod(int animationIndex);

		// Token: 0x060016A5 RID: 5797
		[EngineMethod("get_action_blend_out_start_progress", false)]
		float GetActionBlendOutStartProgress(int actionSetNo, int actionIndex);

		// Token: 0x060016A6 RID: 5798
		[EngineMethod("get_action_code_with_name", false)]
		int GetActionCodeWithName(string name);

		// Token: 0x060016A7 RID: 5799
		[EngineMethod("get_action_name_with_code", false)]
		string GetActionNameWithCode(int index);

		// Token: 0x060016A8 RID: 5800
		[EngineMethod("get_num_action_codes", false)]
		int GetNumActionCodes();

		// Token: 0x060016A9 RID: 5801
		[EngineMethod("get_num_animations", false)]
		int GetNumAnimations();

		// Token: 0x060016AA RID: 5802
		[EngineMethod("is_any_animation_loading_from_disk", false)]
		bool IsAnyAnimationLoadingFromDisk();
	}
}
