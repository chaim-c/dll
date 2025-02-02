using System;

namespace HarmonyLib
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = true)]
	public class HarmonyDelegate : HarmonyPatch
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000941D File Offset: 0x0000761D
		public HarmonyDelegate(Type declaringType) : base(declaringType)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009426 File Offset: 0x00007626
		public HarmonyDelegate(Type declaringType, Type[] argumentTypes) : base(declaringType, argumentTypes)
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009430 File Offset: 0x00007630
		public HarmonyDelegate(Type declaringType, string methodName) : base(declaringType, methodName)
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000943A File Offset: 0x0000763A
		public HarmonyDelegate(Type declaringType, string methodName, params Type[] argumentTypes) : base(declaringType, methodName, argumentTypes)
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00009445 File Offset: 0x00007645
		public HarmonyDelegate(Type declaringType, string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations) : base(declaringType, methodName, argumentTypes, argumentVariations)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009452 File Offset: 0x00007652
		public HarmonyDelegate(Type declaringType, MethodDispatchType methodDispatchType) : base(declaringType, MethodType.Normal)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000946B File Offset: 0x0000766B
		public HarmonyDelegate(Type declaringType, MethodDispatchType methodDispatchType, params Type[] argumentTypes) : base(declaringType, MethodType.Normal, argumentTypes)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00009485 File Offset: 0x00007685
		public HarmonyDelegate(Type declaringType, MethodDispatchType methodDispatchType, Type[] argumentTypes, ArgumentType[] argumentVariations) : base(declaringType, MethodType.Normal, argumentTypes, argumentVariations)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000094A1 File Offset: 0x000076A1
		public HarmonyDelegate(Type declaringType, string methodName, MethodDispatchType methodDispatchType) : base(declaringType, methodName, MethodType.Normal)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000094BB File Offset: 0x000076BB
		public HarmonyDelegate(string methodName) : base(methodName)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000094C4 File Offset: 0x000076C4
		public HarmonyDelegate(string methodName, params Type[] argumentTypes) : base(methodName, argumentTypes)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000094CE File Offset: 0x000076CE
		public HarmonyDelegate(string methodName, Type[] argumentTypes, ArgumentType[] argumentVariations) : base(methodName, argumentTypes, argumentVariations)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000094D9 File Offset: 0x000076D9
		public HarmonyDelegate(string methodName, MethodDispatchType methodDispatchType) : base(methodName, MethodType.Normal)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000094F2 File Offset: 0x000076F2
		public HarmonyDelegate(MethodDispatchType methodDispatchType)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00009509 File Offset: 0x00007709
		public HarmonyDelegate(MethodDispatchType methodDispatchType, params Type[] argumentTypes) : base(MethodType.Normal, argumentTypes)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00009522 File Offset: 0x00007722
		public HarmonyDelegate(MethodDispatchType methodDispatchType, Type[] argumentTypes, ArgumentType[] argumentVariations) : base(MethodType.Normal, argumentTypes, argumentVariations)
		{
			this.info.nonVirtualDelegate = (methodDispatchType == MethodDispatchType.Call);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000953C File Offset: 0x0000773C
		public HarmonyDelegate(Type[] argumentTypes) : base(argumentTypes)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009545 File Offset: 0x00007745
		public HarmonyDelegate(Type[] argumentTypes, ArgumentType[] argumentVariations) : base(argumentTypes, argumentVariations)
		{
		}
	}
}
