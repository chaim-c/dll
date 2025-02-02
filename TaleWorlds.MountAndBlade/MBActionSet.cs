using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000195 RID: 405
	[EngineStruct("int", false)]
	public struct MBActionSet
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x0004EEFE File Offset: 0x0004D0FE
		internal MBActionSet(int i)
		{
			this.Index = i;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0004EF07 File Offset: 0x0004D107
		public bool IsValid
		{
			get
			{
				return this.Index >= 0;
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0004EF15 File Offset: 0x0004D115
		public bool Equals(MBActionSet a)
		{
			return this.Index == a.Index;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0004EF25 File Offset: 0x0004D125
		public bool Equals(int index)
		{
			return this.Index == index;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0004EF30 File Offset: 0x0004D130
		public override int GetHashCode()
		{
			return this.Index;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0004EF38 File Offset: 0x0004D138
		public string GetName()
		{
			if (!this.IsValid)
			{
				return "Invalid";
			}
			return MBAPI.IMBActionSet.GetNameWithIndex(this.Index);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004EF58 File Offset: 0x0004D158
		public string GetSkeletonName()
		{
			return MBAPI.IMBActionSet.GetSkeletonName(this.Index);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0004EF6A File Offset: 0x0004D16A
		public string GetAnimationName(ActionIndexCache actionCode)
		{
			return MBAPI.IMBActionSet.GetAnimationName(this.Index, actionCode.Index);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0004EF82 File Offset: 0x0004D182
		public bool AreActionsAlternatives(ActionIndexCache actionCode1, ActionIndexCache actionCode2)
		{
			return MBAPI.IMBActionSet.AreActionsAlternatives(this.Index, actionCode1.Index, actionCode2.Index);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0004EFA0 File Offset: 0x0004D1A0
		public bool AreActionsAlternatives(ActionIndexValueCache actionCode1, ActionIndexCache actionCode2)
		{
			return MBAPI.IMBActionSet.AreActionsAlternatives(this.Index, actionCode1.Index, actionCode2.Index);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0004EFBF File Offset: 0x0004D1BF
		public static int GetNumberOfActionSets()
		{
			return MBAPI.IMBActionSet.GetNumberOfActionSets();
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0004EFCB File Offset: 0x0004D1CB
		public static int GetNumberOfMonsterUsageSets()
		{
			return MBAPI.IMBActionSet.GetNumberOfMonsterUsageSets();
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0004EFD7 File Offset: 0x0004D1D7
		public static MBActionSet GetActionSet(string objectID)
		{
			return MBActionSet.GetActionSetWithIndex(MBAPI.IMBActionSet.GetIndexWithID(objectID));
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0004EFE9 File Offset: 0x0004D1E9
		public static MBActionSet GetActionSetWithIndex(int index)
		{
			return new MBActionSet(index);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004EFF1 File Offset: 0x0004D1F1
		public static sbyte GetBoneIndexWithId(string actionSetId, string boneId)
		{
			return MBAPI.IMBActionSet.GetBoneIndexWithId(actionSetId, boneId);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004EFFF File Offset: 0x0004D1FF
		public static bool GetBoneHasParentBone(string actionSetId, sbyte boneIndex)
		{
			return MBAPI.IMBActionSet.GetBoneHasParentBone(actionSetId, boneIndex);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004F00D File Offset: 0x0004D20D
		public static Vec3 GetActionDisplacementVector(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetDisplacementVector(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0004F025 File Offset: 0x0004D225
		public static AnimFlags GetActionAnimationFlags(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetAnimationFlags(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0004F03D File Offset: 0x0004D23D
		public static bool CheckActionAnimationClipExists(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.CheckAnimationClipExists(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0004F055 File Offset: 0x0004D255
		public static int GetAnimationIndexOfAction(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.AnimationIndexOfActionCode(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0004F06D File Offset: 0x0004D26D
		public static int GetAnimationIndexOfAction(MBActionSet actionSet, ActionIndexValueCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.AnimationIndexOfActionCode(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004F086 File Offset: 0x0004D286
		public static string GetActionAnimationName(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetAnimationName(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0004F09E File Offset: 0x0004D29E
		public static float GetActionAnimationDuration(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetActionAnimationDuration(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004F0B6 File Offset: 0x0004D2B6
		public static float GetActionAnimationDuration(MBActionSet actionSet, ActionIndexValueCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetActionAnimationDuration(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0004F0CF File Offset: 0x0004D2CF
		public static ActionIndexValueCache GetActionAnimationContinueToAction(MBActionSet actionSet, ActionIndexValueCache actionIndexCache)
		{
			return new ActionIndexValueCache(MBAPI.IMBAnimation.GetAnimationContinueToAction(actionSet.Index, actionIndexCache.Index));
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0004F0F0 File Offset: 0x0004D2F0
		public static float GetTotalAnimationDurationWithContinueToAction(MBActionSet actionSet, ActionIndexValueCache actionIndexCache)
		{
			float num = 0f;
			while (actionIndexCache != ActionIndexValueCache.act_none)
			{
				num += MBActionSet.GetActionAnimationDuration(actionSet, actionIndexCache);
				actionIndexCache = MBActionSet.GetActionAnimationContinueToAction(actionSet, actionIndexCache);
			}
			return num;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0004F126 File Offset: 0x0004D326
		public static float GetActionBlendOutStartProgress(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetActionBlendOutStartProgress(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0004F13E File Offset: 0x0004D33E
		public static float GetActionBlendOutStartProgress(MBActionSet actionSet, ActionIndexValueCache actionIndexCache)
		{
			return MBAPI.IMBAnimation.GetActionBlendOutStartProgress(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x040006F0 RID: 1776
		[CustomEngineStructMemberData("ignoredMember", true)]
		internal readonly int Index;

		// Token: 0x040006F1 RID: 1777
		public static readonly MBActionSet InvalidActionSet = new MBActionSet(-1);
	}
}
