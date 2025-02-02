using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace MCM.UI.Utils
{
	// Token: 0x02000010 RID: 16
	internal static class InitialStateOptionUtils
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002C90 File Offset: 0x00000E90
		static InitialStateOptionUtils()
		{
			foreach (ConstructorInfo constructorInfo in AccessTools.GetDeclaredConstructors(typeof(InitialStateOption), new bool?(false)))
			{
				ParameterInfo[] @params = constructorInfo.GetParameters();
				bool flag = @params.Length == 5;
				if (flag)
				{
					InitialStateOptionUtils.V1 = AccessTools2.GetDelegate<InitialStateOptionUtils.V1Delegate>(constructorInfo, true);
				}
				bool flag2 = @params.Length == 6;
				if (flag2)
				{
					InitialStateOptionUtils.V2 = AccessTools2.GetDelegate<InitialStateOptionUtils.V2Delegate>(constructorInfo, true);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D28 File Offset: 0x00000F28
		[NullableContext(1)]
		public static InitialStateOption Create(string id, TextObject name, int orderIndex, Action action, [Nullable(new byte[]
		{
			1,
			0,
			2
		})] Func<ValueTuple<bool, TextObject>> isDisabledAndReason)
		{
			bool flag = InitialStateOptionUtils.V1 != null;
			InitialStateOption result;
			if (flag)
			{
				result = InitialStateOptionUtils.V1(id, name, orderIndex, action, isDisabledAndReason);
			}
			else
			{
				bool flag2 = InitialStateOptionUtils.V2 != null;
				if (flag2)
				{
					result = InitialStateOptionUtils.V2(id, name, orderIndex, action, isDisabledAndReason, null);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000015 RID: 21
		[Nullable(2)]
		private static readonly InitialStateOptionUtils.V1Delegate V1;

		// Token: 0x04000016 RID: 22
		[Nullable(2)]
		private static readonly InitialStateOptionUtils.V2Delegate V2;

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x060004A6 RID: 1190
		private delegate InitialStateOption V1Delegate(string id, TextObject name, int orderIndex, Action action, [Nullable(new byte[]
		{
			1,
			0,
			2
		})] Func<ValueTuple<bool, TextObject>> isDisabledAndReason);

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x060004AA RID: 1194
		private delegate InitialStateOption V2Delegate(string id, TextObject name, int orderIndex, Action action, [Nullable(new byte[]
		{
			1,
			0,
			2
		})] Func<ValueTuple<bool, TextObject>> isDisabledAndReason, [Nullable(2)] TextObject enabledHint = null);
	}
}
