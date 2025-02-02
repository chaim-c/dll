using System;
using System.Collections.Generic;

namespace HarmonyLib
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true)]
	public class HarmonyPatch : HarmonyAttribute
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000090F4 File Offset: 0x000072F4
		public HarmonyPatch()
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000090FC File Offset: 0x000072FC
		public HarmonyPatch(Type declaringType)
		{
			this.info.declaringType = declaringType;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009110 File Offset: 0x00007310
		public HarmonyPatch(Type declaringType, Type[] argumentTypes)
		{
			this.info.declaringType = declaringType;
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009130 File Offset: 0x00007330
		public HarmonyPatch(Type declaringType, string methodName)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009150 File Offset: 0x00007350
		public HarmonyPatch(Type declaringType, string methodName, params Type[] argumentTypes)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000917C File Offset: 0x0000737C
		public HarmonyPatch(Type declaringType, string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000091A5 File Offset: 0x000073A5
		public HarmonyPatch(Type declaringType, MethodType methodType)
		{
			this.info.declaringType = declaringType;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000091CA File Offset: 0x000073CA
		public HarmonyPatch(Type declaringType, MethodType methodType, params Type[] argumentTypes)
		{
			this.info.declaringType = declaringType;
			this.info.methodType = new MethodType?(methodType);
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000091FB File Offset: 0x000073FB
		public HarmonyPatch(Type declaringType, MethodType methodType, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.declaringType = declaringType;
			this.info.methodType = new MethodType?(methodType);
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009229 File Offset: 0x00007429
		public HarmonyPatch(Type declaringType, string methodName, MethodType methodType)
		{
			this.info.declaringType = declaringType;
			this.info.methodName = methodName;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000925A File Offset: 0x0000745A
		public HarmonyPatch(string methodName)
		{
			this.info.methodName = methodName;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000926E File Offset: 0x0000746E
		public HarmonyPatch(string methodName, params Type[] argumentTypes)
		{
			this.info.methodName = methodName;
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000928E File Offset: 0x0000748E
		public HarmonyPatch(string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.methodName = methodName;
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000092AA File Offset: 0x000074AA
		public HarmonyPatch(string methodName, MethodType methodType)
		{
			this.info.methodName = methodName;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000092CF File Offset: 0x000074CF
		public HarmonyPatch(MethodType methodType)
		{
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000092E8 File Offset: 0x000074E8
		public HarmonyPatch(MethodType methodType, params Type[] argumentTypes)
		{
			this.info.methodType = new MethodType?(methodType);
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000930D File Offset: 0x0000750D
		public HarmonyPatch(MethodType methodType, Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.info.methodType = new MethodType?(methodType);
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000932E File Offset: 0x0000752E
		public HarmonyPatch(Type[] argumentTypes)
		{
			this.info.argumentTypes = argumentTypes;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009342 File Offset: 0x00007542
		public HarmonyPatch(Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			this.ParseSpecialArguments(argumentTypes, argumentVariations);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009352 File Offset: 0x00007552
		public HarmonyPatch(string typeName, string methodName, MethodType methodType = MethodType.Normal)
		{
			this.info.declaringType = AccessTools.TypeByName(typeName);
			this.info.methodName = methodName;
			this.info.methodType = new MethodType?(methodType);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00009388 File Offset: 0x00007588
		private void ParseSpecialArguments(Type[] argumentTypes, ArgumentType[] argumentVariations)
		{
			if (argumentVariations == null || argumentVariations.Length == 0)
			{
				this.info.argumentTypes = argumentTypes;
				return;
			}
			if (argumentTypes.Length < argumentVariations.Length)
			{
				throw new ArgumentException("argumentVariations contains more elements than argumentTypes", "argumentVariations");
			}
			List<Type> list = new List<Type>();
			for (int i = 0; i < argumentTypes.Length; i++)
			{
				Type type = argumentTypes[i];
				switch (argumentVariations[i])
				{
				case ArgumentType.Ref:
				case ArgumentType.Out:
					type = type.MakeByRefType();
					break;
				case ArgumentType.Pointer:
					type = type.MakePointerType();
					break;
				}
				list.Add(type);
			}
			this.info.argumentTypes = list.ToArray();
		}
	}
}
