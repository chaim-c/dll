using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001AB RID: 427
	[ScriptingInterfaceBase]
	internal interface IMBSkeletonExtensions
	{
		// Token: 0x06001774 RID: 6004
		[EngineMethod("create_agent_skeleton", false)]
		Skeleton CreateAgentSkeleton(string skeletonName, bool isHumanoid, int actionSetIndex, string monsterUsageSetName, ref AnimationSystemData animationSystemData);

		// Token: 0x06001775 RID: 6005
		[EngineMethod("create_simple_skeleton", false)]
		Skeleton CreateSimpleSkeleton(string skeletonName);

		// Token: 0x06001776 RID: 6006
		[EngineMethod("create_with_action_set", false)]
		Skeleton CreateWithActionSet(ref AnimationSystemData animationSystemData);

		// Token: 0x06001777 RID: 6007
		[EngineMethod("get_skeleton_face_animation_time", false)]
		float GetSkeletonFaceAnimationTime(UIntPtr entityId);

		// Token: 0x06001778 RID: 6008
		[EngineMethod("set_skeleton_face_animation_time", false)]
		void SetSkeletonFaceAnimationTime(UIntPtr entityId, float time);

		// Token: 0x06001779 RID: 6009
		[EngineMethod("get_skeleton_face_animation_name", false)]
		string GetSkeletonFaceAnimationName(UIntPtr entityId);

		// Token: 0x0600177A RID: 6010
		[EngineMethod("get_bone_entitial_frame_at_animation_progress", false)]
		void GetBoneEntitialFrameAtAnimationProgress(UIntPtr skeletonPointer, sbyte boneIndex, int animationIndex, float progress, ref MatrixFrame outFrame);

		// Token: 0x0600177B RID: 6011
		[EngineMethod("get_bone_entitial_frame", false)]
		void GetBoneEntitialFrame(UIntPtr skeletonPointer, sbyte bone, bool useBoneMapping, bool forceToUpdate, ref MatrixFrame outFrame);

		// Token: 0x0600177C RID: 6012
		[EngineMethod("set_animation_at_channel", false)]
		void SetAnimationAtChannel(UIntPtr skeletonPointer, int animationIndex, int channelNo, float animationSpeedMultiplier, float blendInPeriod, float startProgress);

		// Token: 0x0600177D RID: 6013
		[EngineMethod("get_action_at_channel", false)]
		int GetActionAtChannel(UIntPtr skeletonPointer, int channelNo);

		// Token: 0x0600177E RID: 6014
		[EngineMethod("set_facial_animation_of_channel", false)]
		void SetFacialAnimationOfChannel(UIntPtr skeletonPointer, int channel, string facialAnimationName, bool playSound, bool loop);

		// Token: 0x0600177F RID: 6015
		[EngineMethod("set_agent_action_channel", false)]
		void SetAgentActionChannel(UIntPtr skeletonPointer, int actionChannelNo, int actionIndex, float channelParameter, float blendPeriodOverride, bool forceFaceMorphRestart);

		// Token: 0x06001780 RID: 6016
		[EngineMethod("does_action_continue_with_current_action_at_channel", false)]
		bool DoesActionContinueWithCurrentActionAtChannel(UIntPtr skeletonPointer, int actionChannelNo, int actionIndex);

		// Token: 0x06001781 RID: 6017
		[EngineMethod("tick_action_channels", false)]
		void TickActionChannels(UIntPtr skeletonPointer);
	}
}
