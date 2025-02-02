using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace MCM.UI.Utils
{
	// Token: 0x02000011 RID: 17
	internal static class InquiryDataUtils
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002D80 File Offset: 0x00000F80
		[NullableContext(1)]
		[return: Nullable(2)]
		public static InquiryData Create(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action affirmativeAction, Action negativeAction)
		{
			bool flag = InquiryDataUtils.V1 != null;
			InquiryData result;
			if (flag)
			{
				result = InquiryDataUtils.V1(titleText, text, isAffirmativeOptionShown, isNegativeOptionShown, affirmativeText, negativeText, affirmativeAction, negativeAction, "", 0f, null);
			}
			else
			{
				bool flag2 = InquiryDataUtils.V2 != null;
				if (flag2)
				{
					result = InquiryDataUtils.V2(titleText, text, isAffirmativeOptionShown, isNegativeOptionShown, affirmativeText, negativeText, affirmativeAction, negativeAction, "", 0f, null, null, null);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DFB File Offset: 0x00000FFB
		[NullableContext(1)]
		[return: Nullable(2)]
		public static InquiryData CreateTranslatable(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action affirmativeAction, Action negativeAction)
		{
			return InquiryDataUtils.Create(new TextObject(titleText, null).ToString(), new TextObject(text, null).ToString(), isAffirmativeOptionShown, isNegativeOptionShown, new TextObject(affirmativeText, null).ToString(), new TextObject(negativeText, null).ToString(), affirmativeAction, negativeAction);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E3C File Offset: 0x0000103C
		[NullableContext(1)]
		[return: Nullable(2)]
		public static TextInquiryData CreateText(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action<string> affirmativeAction, Action negativeAction)
		{
			bool flag = InquiryDataUtils.V1Text != null;
			TextInquiryData result;
			if (flag)
			{
				result = InquiryDataUtils.V1Text(titleText, text, isAffirmativeOptionShown, isNegativeOptionShown, affirmativeText, negativeText, affirmativeAction, negativeAction, false, null, "", "");
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E83 File Offset: 0x00001083
		[NullableContext(1)]
		[return: Nullable(2)]
		public static TextInquiryData CreateTextTranslatable(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action<string> affirmativeAction, Action negativeAction)
		{
			return InquiryDataUtils.CreateText(new TextObject(titleText, null).ToString(), new TextObject(text, null).ToString(), isAffirmativeOptionShown, isNegativeOptionShown, new TextObject(affirmativeText, null).ToString(), new TextObject(negativeText, null).ToString(), affirmativeAction, negativeAction);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EC4 File Offset: 0x000010C4
		[NullableContext(1)]
		[return: Nullable(2)]
		public static MultiSelectionInquiryData CreateMulti(string titleText, string descriptionText, List<InquiryElement> inquiryElements, bool isExitShown, int minSelectableOptionCount, int maxSelectableOptionCount, string affirmativeText, string negativeText, Action<List<InquiryElement>> affirmativeAction, Action<List<InquiryElement>> negativeAction)
		{
			bool flag = InquiryDataUtils.V1Multi != null;
			MultiSelectionInquiryData result;
			if (flag)
			{
				result = InquiryDataUtils.V1Multi(titleText, descriptionText, inquiryElements, isExitShown, maxSelectableOptionCount, affirmativeText, negativeText, affirmativeAction, negativeAction, "");
			}
			else
			{
				bool flag2 = InquiryDataUtils.V2Multi != null;
				if (flag2)
				{
					result = InquiryDataUtils.V2Multi(titleText, descriptionText, inquiryElements, isExitShown, minSelectableOptionCount, maxSelectableOptionCount, affirmativeText, negativeText, affirmativeAction, negativeAction, "");
				}
				else
				{
					bool flag3 = InquiryDataUtils.V3Multi != null;
					if (flag3)
					{
						result = InquiryDataUtils.V3Multi(titleText, descriptionText, inquiryElements, isExitShown, minSelectableOptionCount, maxSelectableOptionCount, affirmativeText, negativeText, affirmativeAction, negativeAction, "", false);
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F6C File Offset: 0x0000116C
		[NullableContext(1)]
		[return: Nullable(2)]
		public static MultiSelectionInquiryData CreateMultiTranslatable(string titleText, string descriptionText, List<InquiryElement> inquiryElements, bool isExitShown, int minSelectableOptionCount, int maxSelectableOptionCount, string affirmativeText, string negativeText, Action<List<InquiryElement>> affirmativeAction, Action<List<InquiryElement>> negativeAction)
		{
			return InquiryDataUtils.CreateMulti(new TextObject(titleText, null).ToString(), new TextObject(descriptionText, null).ToString(), inquiryElements, isExitShown, minSelectableOptionCount, maxSelectableOptionCount, new TextObject(affirmativeText, null).ToString(), new TextObject(negativeText, null).ToString(), affirmativeAction, negativeAction);
		}

		// Token: 0x04000017 RID: 23
		[Nullable(2)]
		private static readonly InquiryDataUtils.V1Delegate V1 = AccessTools2.GetConstructorDelegate<InquiryDataUtils.V1Delegate>(typeof(InquiryData), (from x in typeof(InquiryDataUtils.V1Delegate).GetMethod("Invoke").GetParameters()
		select x.ParameterType).ToArray<Type>(), true);

		// Token: 0x04000018 RID: 24
		[Nullable(2)]
		private static readonly InquiryDataUtils.V2Delegate V2 = AccessTools2.GetConstructorDelegate<InquiryDataUtils.V2Delegate>(typeof(InquiryData), (from x in typeof(InquiryDataUtils.V2Delegate).GetMethod("Invoke").GetParameters()
		select x.ParameterType).ToArray<Type>(), true);

		// Token: 0x04000019 RID: 25
		[Nullable(2)]
		private static readonly InquiryDataUtils.V1TextDelegate V1Text = AccessTools2.GetConstructorDelegate<InquiryDataUtils.V1TextDelegate>(typeof(TextInquiryData), (from x in typeof(InquiryDataUtils.V1TextDelegate).GetMethod("Invoke").GetParameters()
		select x.ParameterType).ToArray<Type>(), true);

		// Token: 0x0400001A RID: 26
		[Nullable(2)]
		private static readonly InquiryDataUtils.V1MultiDelegate V1Multi = AccessTools2.GetConstructorDelegate<InquiryDataUtils.V1MultiDelegate>(typeof(MultiSelectionInquiryData), (from x in typeof(InquiryDataUtils.V1MultiDelegate).GetMethod("Invoke").GetParameters()
		select x.ParameterType).ToArray<Type>(), true);

		// Token: 0x0400001B RID: 27
		[Nullable(2)]
		private static readonly InquiryDataUtils.V2MultiDelegate V2Multi = AccessTools2.GetConstructorDelegate<InquiryDataUtils.V2MultiDelegate>(typeof(MultiSelectionInquiryData), (from x in typeof(InquiryDataUtils.V2MultiDelegate).GetMethod("Invoke").GetParameters()
		select x.ParameterType).ToArray<Type>(), true);

		// Token: 0x0400001C RID: 28
		[Nullable(2)]
		private static readonly InquiryDataUtils.V3MultiDelegate V3Multi = AccessTools2.GetConstructorDelegate<InquiryDataUtils.V3MultiDelegate>(typeof(MultiSelectionInquiryData), (from x in typeof(InquiryDataUtils.V3MultiDelegate).GetMethod("Invoke").GetParameters()
		select x.ParameterType).ToArray<Type>(), true);

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x060004AE RID: 1198
		private delegate InquiryData V1Delegate(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action affirmativeAction, Action negativeAction, string soundEventPath = "", float expireTime = 0f, [Nullable(2)] Action timeoutAction = null);

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x060004B2 RID: 1202
		private delegate InquiryData V2Delegate(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action affirmativeAction, Action negativeAction, string soundEventPath = "", float expireTime = 0f, [Nullable(2)] Action timeoutAction = null, [Nullable(new byte[]
		{
			2,
			0,
			1
		})] Func<ValueTuple<bool, string>> isAffirmativeOptionEnabled = null, [Nullable(new byte[]
		{
			2,
			0,
			1
		})] Func<ValueTuple<bool, string>> isNegativeOptionEnabled = null);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x060004B6 RID: 1206
		private delegate TextInquiryData V1TextDelegate(string titleText, string text, bool isAffirmativeOptionShown, bool isNegativeOptionShown, string affirmativeText, string negativeText, Action<string> affirmativeAction, Action negativeAction, bool shouldInputBeObfuscated = false, [Nullable(new byte[]
		{
			2,
			1,
			1,
			1
		})] Func<string, Tuple<bool, string>> textCondition = null, string soundEventPath = "", string defaultInputText = "");

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x060004BA RID: 1210
		private delegate MultiSelectionInquiryData V1MultiDelegate(string titleText, string descriptionText, List<InquiryElement> inquiryElements, bool isExitShown, int maxSelectableOptionCount, string affirmativeText, string negativeText, Action<List<InquiryElement>> affirmativeAction, Action<List<InquiryElement>> negativeAction, string soundEventPath = "");

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x060004BE RID: 1214
		private delegate MultiSelectionInquiryData V2MultiDelegate(string titleText, string descriptionText, List<InquiryElement> inquiryElements, bool isExitShown, int minSelectableOptionCount, int maxSelectableOptionCount, string affirmativeText, string negativeText, Action<List<InquiryElement>> affirmativeAction, Action<List<InquiryElement>> negativeAction, string soundEventPath = "");

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x060004C2 RID: 1218
		private delegate MultiSelectionInquiryData V3MultiDelegate(string titleText, string descriptionText, List<InquiryElement> inquiryElements, bool isExitShown, int minSelectableOptionCount, int maxSelectableOptionCount, string affirmativeText, string negativeText, Action<List<InquiryElement>> affirmativeAction, Action<List<InquiryElement>> negativeAction, string soundEventPath = "", bool isSeachAvailable = false);
	}
}
