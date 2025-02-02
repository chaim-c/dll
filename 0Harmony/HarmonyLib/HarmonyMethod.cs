using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x0200003A RID: 58
	public class HarmonyMethod
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000A874 File Offset: 0x00008A74
		public HarmonyMethod()
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A884 File Offset: 0x00008A84
		private void ImportMethod(MethodInfo theMethod)
		{
			this.method = theMethod;
			if (this.method != null)
			{
				List<HarmonyMethod> fromMethod = HarmonyMethodExtensions.GetFromMethod(this.method);
				if (fromMethod != null)
				{
					HarmonyMethod.Merge(fromMethod).CopyTo(this);
				}
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A8BB File Offset: 0x00008ABB
		public HarmonyMethod(MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.ImportMethod(method);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A8DF File Offset: 0x00008ADF
		public HarmonyMethod(Delegate @delegate) : this(@delegate.Method)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public HarmonyMethod(MethodInfo method, int priority = -1, string[] before = null, string[] after = null, bool? debug = null)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.ImportMethod(method);
			this.priority = priority;
			this.before = before;
			this.after = after;
			this.debug = debug;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A93D File Offset: 0x00008B3D
		public HarmonyMethod(Delegate @delegate, int priority = -1, string[] before = null, string[] after = null, bool? debug = null) : this(@delegate.Method, priority, before, after, debug)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A954 File Offset: 0x00008B54
		public HarmonyMethod(Type methodType, string methodName, Type[] argumentTypes = null)
		{
			MethodInfo methodInfo = AccessTools.Method(methodType, methodName, argumentTypes, null);
			if (methodInfo == null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Cannot not find method for type ");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(methodType);
				defaultInterpolatedStringHandler.AppendLiteral(" and name ");
				defaultInterpolatedStringHandler.AppendFormatted(methodName);
				defaultInterpolatedStringHandler.AppendLiteral(" and parameters ");
				defaultInterpolatedStringHandler.AppendFormatted((argumentTypes != null) ? argumentTypes.Description() : null);
				throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			this.ImportMethod(methodInfo);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public static List<string> HarmonyFields()
		{
			return (from s in AccessTools.GetFieldNames(typeof(HarmonyMethod))
			where s != "method"
			select s).ToList<string>();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public static HarmonyMethod Merge(List<HarmonyMethod> attributes)
		{
			HarmonyMethod harmonyMethod = new HarmonyMethod();
			if (attributes == null || attributes.Count == 0)
			{
				return harmonyMethod;
			}
			Traverse resultTrv = Traverse.Create(harmonyMethod);
			attributes.ForEach(delegate(HarmonyMethod attribute)
			{
				Traverse trv = Traverse.Create(attribute);
				HarmonyMethod.HarmonyFields().ForEach(delegate(string f)
				{
					object value = trv.Field(f).GetValue();
					if (value != null && (f != "priority" || (int)value != -1))
					{
						HarmonyMethodExtensions.SetValue(resultTrv, f, value);
					}
				});
			});
			return harmonyMethod;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AA64 File Offset: 0x00008C64
		public override string ToString()
		{
			string result = "";
			Traverse trv = Traverse.Create(this);
			HarmonyMethod.HarmonyFields().ForEach(delegate(string f)
			{
				string result;
				if (result.Length > 0)
				{
					result += ", ";
				}
				result = result;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(f);
				defaultInterpolatedStringHandler.AppendLiteral("=");
				defaultInterpolatedStringHandler.AppendFormatted<object>(trv.Field(f).GetValue());
				result += defaultInterpolatedStringHandler.ToStringAndClear();
			});
			return "HarmonyMethod[" + result + "]";
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AABC File Offset: 0x00008CBC
		internal string Description()
		{
			string value = (this.declaringType != null) ? this.declaringType.FullName : "undefined";
			string value2 = this.methodName ?? "undefined";
			string value3 = (this.methodType != null) ? this.methodType.Value.ToString() : "undefined";
			string value4 = (this.argumentTypes != null) ? this.argumentTypes.Description() : "undefined";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 4);
			defaultInterpolatedStringHandler.AppendLiteral("(class=");
			defaultInterpolatedStringHandler.AppendFormatted(value);
			defaultInterpolatedStringHandler.AppendLiteral(", methodname=");
			defaultInterpolatedStringHandler.AppendFormatted(value2);
			defaultInterpolatedStringHandler.AppendLiteral(", type=");
			defaultInterpolatedStringHandler.AppendFormatted(value3);
			defaultInterpolatedStringHandler.AppendLiteral(", args=");
			defaultInterpolatedStringHandler.AppendFormatted(value4);
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000ABAB File Offset: 0x00008DAB
		public static implicit operator HarmonyMethod(MethodInfo method)
		{
			return new HarmonyMethod(method);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		public static implicit operator HarmonyMethod(Delegate @delegate)
		{
			return new HarmonyMethod(@delegate);
		}

		// Token: 0x0400008B RID: 139
		public MethodInfo method;

		// Token: 0x0400008C RID: 140
		public string category;

		// Token: 0x0400008D RID: 141
		public Type declaringType;

		// Token: 0x0400008E RID: 142
		public string methodName;

		// Token: 0x0400008F RID: 143
		public MethodType? methodType;

		// Token: 0x04000090 RID: 144
		public Type[] argumentTypes;

		// Token: 0x04000091 RID: 145
		public int priority = -1;

		// Token: 0x04000092 RID: 146
		public string[] before;

		// Token: 0x04000093 RID: 147
		public string[] after;

		// Token: 0x04000094 RID: 148
		public HarmonyReversePatchType? reversePatchType;

		// Token: 0x04000095 RID: 149
		public bool? debug;

		// Token: 0x04000096 RID: 150
		public bool nonVirtualDelegate;
	}
}
