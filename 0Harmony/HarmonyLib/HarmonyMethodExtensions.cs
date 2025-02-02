using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x0200003B RID: 59
	public static class HarmonyMethodExtensions
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000ABBC File Offset: 0x00008DBC
		internal static void SetValue(Traverse trv, string name, object val)
		{
			if (val == null)
			{
				return;
			}
			Traverse traverse = trv.Field(name);
			if (name == "methodType" || name == "reversePatchType")
			{
				Type underlyingType = Nullable.GetUnderlyingType(traverse.GetValueType());
				val = Enum.ToObject(underlyingType, (int)val);
			}
			traverse.SetValue(val);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000AC14 File Offset: 0x00008E14
		public static void CopyTo(this HarmonyMethod from, HarmonyMethod to)
		{
			if (to == null)
			{
				return;
			}
			Traverse fromTrv = Traverse.Create(from);
			Traverse toTrv = Traverse.Create(to);
			HarmonyMethod.HarmonyFields().ForEach(delegate(string f)
			{
				object value = fromTrv.Field(f).GetValue();
				if (value != null)
				{
					HarmonyMethodExtensions.SetValue(toTrv, f, value);
				}
			});
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AC5C File Offset: 0x00008E5C
		public static HarmonyMethod Clone(this HarmonyMethod original)
		{
			HarmonyMethod harmonyMethod = new HarmonyMethod();
			original.CopyTo(harmonyMethod);
			return harmonyMethod;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AC78 File Offset: 0x00008E78
		public static HarmonyMethod Merge(this HarmonyMethod master, HarmonyMethod detail)
		{
			if (detail == null)
			{
				return master;
			}
			HarmonyMethod harmonyMethod = new HarmonyMethod();
			Traverse resultTrv = Traverse.Create(harmonyMethod);
			Traverse masterTrv = Traverse.Create(master);
			Traverse detailTrv = Traverse.Create(detail);
			HarmonyMethod.HarmonyFields().ForEach(delegate(string f)
			{
				object value = masterTrv.Field(f).GetValue();
				object value2 = detailTrv.Field(f).GetValue();
				if (f != "priority")
				{
					HarmonyMethodExtensions.SetValue(resultTrv, f, value2 ?? value);
					return;
				}
				int num = (int)value;
				int num2 = (int)value2;
				int num3 = Math.Max(num, num2);
				if (num == -1 && num2 != -1)
				{
					num3 = num2;
				}
				if (num != -1 && num2 == -1)
				{
					num3 = num;
				}
				HarmonyMethodExtensions.SetValue(resultTrv, f, num3);
			});
			return harmonyMethod;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		private static HarmonyMethod GetHarmonyMethodInfo(object attribute)
		{
			FieldInfo field = attribute.GetType().GetField("info", AccessTools.all);
			if (field == null)
			{
				return null;
			}
			if (field.FieldType.FullName != PatchTools.harmonyMethodFullName)
			{
				return null;
			}
			object value = field.GetValue(attribute);
			return AccessTools.MakeDeepCopy<HarmonyMethod>(value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000AD24 File Offset: 0x00008F24
		public static List<HarmonyMethod> GetFromType(Type type)
		{
			IEnumerable<object> customAttributes = type.GetCustomAttributes(true);
			Func<object, HarmonyMethod> selector;
			if ((selector = HarmonyMethodExtensions.<>O.<0>__GetHarmonyMethodInfo) == null)
			{
				selector = (HarmonyMethodExtensions.<>O.<0>__GetHarmonyMethodInfo = new Func<object, HarmonyMethod>(HarmonyMethodExtensions.GetHarmonyMethodInfo));
			}
			return (from info in customAttributes.Select(selector)
			where info != null
			select info).ToList<HarmonyMethod>();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000AD81 File Offset: 0x00008F81
		public static HarmonyMethod GetMergedFromType(Type type)
		{
			return HarmonyMethod.Merge(HarmonyMethodExtensions.GetFromType(type));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000AD90 File Offset: 0x00008F90
		public static List<HarmonyMethod> GetFromMethod(MethodBase method)
		{
			IEnumerable<object> customAttributes = method.GetCustomAttributes(true);
			Func<object, HarmonyMethod> selector;
			if ((selector = HarmonyMethodExtensions.<>O.<0>__GetHarmonyMethodInfo) == null)
			{
				selector = (HarmonyMethodExtensions.<>O.<0>__GetHarmonyMethodInfo = new Func<object, HarmonyMethod>(HarmonyMethodExtensions.GetHarmonyMethodInfo));
			}
			return (from info in customAttributes.Select(selector)
			where info != null
			select info).ToList<HarmonyMethod>();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000ADED File Offset: 0x00008FED
		public static HarmonyMethod GetMergedFromMethod(MethodBase method)
		{
			return HarmonyMethod.Merge(HarmonyMethodExtensions.GetFromMethod(method));
		}

		// Token: 0x0200008B RID: 139
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400019F RID: 415
			public static Func<object, HarmonyMethod> <0>__GetHarmonyMethodInfo;
		}
	}
}
