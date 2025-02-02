using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D7 RID: 471
	public static class MBSkeletonExtensions
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x0005CF61 File Offset: 0x0005B161
		public static Skeleton CreateWithActionSet(ref AnimationSystemData animationSystemData)
		{
			return MBAPI.IMBSkeletonExtensions.CreateWithActionSet(ref animationSystemData);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0005CF6E File Offset: 0x0005B16E
		public static float GetSkeletonFaceAnimationTime(Skeleton skeleton)
		{
			return MBAPI.IMBSkeletonExtensions.GetSkeletonFaceAnimationTime(skeleton.Pointer);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0005CF80 File Offset: 0x0005B180
		public static void SetSkeletonFaceAnimationTime(Skeleton skeleton, float time)
		{
			MBAPI.IMBSkeletonExtensions.SetSkeletonFaceAnimationTime(skeleton.Pointer, time);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0005CF93 File Offset: 0x0005B193
		public static string GetSkeletonFaceAnimationName(Skeleton skeleton)
		{
			return MBAPI.IMBSkeletonExtensions.GetSkeletonFaceAnimationName(skeleton.Pointer);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0005CFA8 File Offset: 0x0005B1A8
		public static MatrixFrame GetBoneEntitialFrameAtAnimationProgress(this Skeleton skeleton, sbyte boneIndex, int animationIndex, float progress)
		{
			MatrixFrame result = default(MatrixFrame);
			MBAPI.IMBSkeletonExtensions.GetBoneEntitialFrameAtAnimationProgress(skeleton.Pointer, boneIndex, animationIndex, progress, ref result);
			return result;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0005CFD4 File Offset: 0x0005B1D4
		public static MatrixFrame GetBoneEntitialFrame(this Skeleton skeleton, sbyte boneNumber, bool forceToUpdate = false)
		{
			MatrixFrame result = default(MatrixFrame);
			MBAPI.IMBSkeletonExtensions.GetBoneEntitialFrame(skeleton.Pointer, boneNumber, false, forceToUpdate, ref result);
			return result;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0005CFFF File Offset: 0x0005B1FF
		public static void SetFacialAnimation(this Skeleton skeleton, Agent.FacialAnimChannel channel, string faceAnimation, bool playSound, bool loop)
		{
			MBAPI.IMBSkeletonExtensions.SetFacialAnimationOfChannel(skeleton.Pointer, (int)channel, faceAnimation, playSound, loop);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0005D016 File Offset: 0x0005B216
		public static void SetAgentActionChannel(this Skeleton skeleton, int actionChannelNo, ActionIndexCache actionIndex, float channelParameter = 0f, float blendPeriodOverride = -0.2f, bool forceFaceMorphRestart = true)
		{
			MBAPI.IMBSkeletonExtensions.SetAgentActionChannel(skeleton.Pointer, actionChannelNo, actionIndex.Index, channelParameter, blendPeriodOverride, forceFaceMorphRestart);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0005D034 File Offset: 0x0005B234
		public static bool DoesActionContinueWithCurrentActionAtChannel(this Skeleton skeleton, int actionChannelNo, ActionIndexCache actionIndex)
		{
			return MBAPI.IMBSkeletonExtensions.DoesActionContinueWithCurrentActionAtChannel(skeleton.Pointer, actionChannelNo, actionIndex.Index);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0005D04D File Offset: 0x0005B24D
		public static void TickActionChannels(this Skeleton skeleton)
		{
			MBAPI.IMBSkeletonExtensions.TickActionChannels(skeleton.Pointer);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0005D060 File Offset: 0x0005B260
		public static void SetAnimationAtChannel(this Skeleton skeleton, string animationName, int channelNo, float animationSpeedMultiplier = 1f, float blendInPeriod = -1f, float startProgress = 0f)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			skeleton.SetAnimationAtChannel(indexWithID, channelNo, animationSpeedMultiplier, blendInPeriod, startProgress);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0005D086 File Offset: 0x0005B286
		public static void SetAnimationAtChannel(this Skeleton skeleton, int animationIndex, int channelNo, float animationSpeedMultiplier = 1f, float blendInPeriod = -1f, float startProgress = 0f)
		{
			MBAPI.IMBSkeletonExtensions.SetAnimationAtChannel(skeleton.Pointer, animationIndex, channelNo, animationSpeedMultiplier, blendInPeriod, startProgress);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0005D09F File Offset: 0x0005B29F
		public static ActionIndexCache GetActionAtChannel(this Skeleton skeleton, int channelNo)
		{
			return new ActionIndexCache(MBAPI.IMBSkeletonExtensions.GetActionAtChannel(skeleton.Pointer, channelNo));
		}
	}
}
