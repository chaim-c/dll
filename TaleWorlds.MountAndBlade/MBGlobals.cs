using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C7 RID: 455
	public static class MBGlobals
	{
		// Token: 0x06001A1E RID: 6686 RVA: 0x0005C3F3 File Offset: 0x0005A5F3
		public static void InitializeReferences()
		{
			if (!MBGlobals._initialized)
			{
				MBGlobals._actionSets = new Dictionary<string, MBActionSet>();
				MBGlobals._initialized = true;
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0005C40C File Offset: 0x0005A60C
		public static MBActionSet GetActionSetWithSuffix(Monster monster, bool isFemale, string suffix)
		{
			return MBGlobals.GetActionSet(ActionSetCode.GenerateActionSetNameWithSuffix(monster, isFemale, suffix));
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0005C41C File Offset: 0x0005A61C
		public static MBActionSet GetActionSet(string actionSetCode)
		{
			MBActionSet actionSet;
			if (!MBGlobals._actionSets.TryGetValue(actionSetCode, out actionSet))
			{
				actionSet = MBActionSet.GetActionSet(actionSetCode);
				if (!actionSet.IsValid)
				{
					Debug.FailedAssert("No action set found with action set code: " + actionSetCode, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Base\\MBGlobals.cs", "GetActionSet", 40);
					throw new Exception("Invalid action set code");
				}
				MBGlobals._actionSets[actionSetCode] = actionSet;
			}
			return actionSet;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0005C47C File Offset: 0x0005A67C
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0005C493 File Offset: 0x0005A693
		public static string GetMethodName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MethodCallExpression)memberExpression.Body).Method.Name;
		}

		// Token: 0x040007F1 RID: 2033
		public const float Gravity = 9.806f;

		// Token: 0x040007F2 RID: 2034
		public static readonly Vec3 GravityVec3 = new Vec3(0f, 0f, -9.806f, -1f);

		// Token: 0x040007F3 RID: 2035
		private static bool _initialized;

		// Token: 0x040007F4 RID: 2036
		private static Dictionary<string, MBActionSet> _actionSets;
	}
}
