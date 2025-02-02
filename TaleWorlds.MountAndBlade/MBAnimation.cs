using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200019C RID: 412
	public struct MBAnimation
	{
		// Token: 0x0600154F RID: 5455 RVA: 0x0004FBB5 File Offset: 0x0004DDB5
		public MBAnimation(MBAnimation animation)
		{
			this._index = animation._index;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004FBC3 File Offset: 0x0004DDC3
		internal MBAnimation(int i)
		{
			this._index = i;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0004FBCC File Offset: 0x0004DDCC
		public bool Equals(MBAnimation a)
		{
			return this._index == a._index;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0004FBDC File Offset: 0x0004DDDC
		public override int GetHashCode()
		{
			return this._index;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0004FBE4 File Offset: 0x0004DDE4
		public static int GetAnimationIndexWithName(string animationName)
		{
			if (string.IsNullOrEmpty(animationName))
			{
				return -1;
			}
			return MBAPI.IMBAnimation.GetIndexWithID(animationName);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0004FBFB File Offset: 0x0004DDFB
		public static Agent.ActionCodeType GetActionType(ActionIndexCache actionIndex)
		{
			if (!(actionIndex == ActionIndexCache.act_none))
			{
				return MBAPI.IMBAnimation.GetActionType(actionIndex.Index);
			}
			return Agent.ActionCodeType.Other;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0004FC1C File Offset: 0x0004DE1C
		public static Agent.ActionCodeType GetActionType(ActionIndexValueCache actionIndex)
		{
			if (!(actionIndex == ActionIndexValueCache.act_none))
			{
				return MBAPI.IMBAnimation.GetActionType(actionIndex.Index);
			}
			return Agent.ActionCodeType.Other;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0004FC3E File Offset: 0x0004DE3E
		public static void PrefetchAnimationClip(MBActionSet actionSet, ActionIndexCache actionIndexCache)
		{
			MBAPI.IMBAnimation.PrefetchAnimationClip(actionSet.Index, actionIndexCache.Index);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0004FC58 File Offset: 0x0004DE58
		public static float GetAnimationDuration(string animationName)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			return MBAPI.IMBAnimation.GetAnimationDuration(indexWithID);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0004FC7C File Offset: 0x0004DE7C
		public static float GetAnimationDuration(int animationIndex)
		{
			return MBAPI.IMBAnimation.GetAnimationDuration(animationIndex);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0004FC8C File Offset: 0x0004DE8C
		public static float GetAnimationParameter1(string animationName)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			return MBAPI.IMBAnimation.GetAnimationParameter1(indexWithID);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0004FCB0 File Offset: 0x0004DEB0
		public static float GetAnimationParameter1(int animationIndex)
		{
			return MBAPI.IMBAnimation.GetAnimationParameter1(animationIndex);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0004FCC0 File Offset: 0x0004DEC0
		public static float GetAnimationParameter2(string animationName)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			return MBAPI.IMBAnimation.GetAnimationParameter2(indexWithID);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004FCE4 File Offset: 0x0004DEE4
		public static float GetAnimationParameter2(int animationIndex)
		{
			return MBAPI.IMBAnimation.GetAnimationParameter2(animationIndex);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
		public static float GetAnimationParameter3(string animationName)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			return MBAPI.IMBAnimation.GetAnimationParameter3(indexWithID);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0004FD18 File Offset: 0x0004DF18
		public static float GetAnimationBlendInPeriod(string animationName)
		{
			int indexWithID = MBAPI.IMBAnimation.GetIndexWithID(animationName);
			return MBAPI.IMBAnimation.GetAnimationBlendInPeriod(indexWithID);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0004FD3C File Offset: 0x0004DF3C
		public static float GetAnimationBlendInPeriod(int animationIndex)
		{
			return MBAPI.IMBAnimation.GetAnimationBlendInPeriod(animationIndex);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004FD4C File Offset: 0x0004DF4C
		public static int GetActionCodeWithName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return ActionIndexValueCache.act_none.Index;
			}
			return MBAPI.IMBAnimation.GetActionCodeWithName(name);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0004FD7A File Offset: 0x0004DF7A
		public static string GetActionNameWithCode(int actionIndex)
		{
			if (actionIndex == -1)
			{
				return "act_none";
			}
			return MBAPI.IMBAnimation.GetActionNameWithCode(actionIndex);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004FD91 File Offset: 0x0004DF91
		public static int GetNumActionCodes()
		{
			return MBAPI.IMBAnimation.GetNumActionCodes();
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0004FD9D File Offset: 0x0004DF9D
		public static int GetNumAnimations()
		{
			return MBAPI.IMBAnimation.GetNumAnimations();
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0004FDA9 File Offset: 0x0004DFA9
		public static bool IsAnyAnimationLoadingFromDisk()
		{
			return MBAPI.IMBAnimation.IsAnyAnimationLoadingFromDisk();
		}

		// Token: 0x04000754 RID: 1876
		private readonly int _index;
	}
}
