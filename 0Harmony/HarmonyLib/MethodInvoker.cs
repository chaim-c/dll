using System;
using System.Reflection;
using System.Reflection.Emit;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x0200000A RID: 10
	public static class MethodInvoker
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024FC File Offset: 0x000006FC
		public static FastInvokeHandler GetHandler(MethodInfo methodInfo, bool directBoxValueAccess = false)
		{
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("FastInvoke_" + methodInfo.Name + "_" + (directBoxValueAccess ? "direct" : "indirect"), typeof(object), new Type[]
			{
				typeof(object),
				typeof(object[])
			});
			ILGenerator ilgenerator = dynamicMethodDefinition.GetILGenerator();
			if (!methodInfo.IsStatic)
			{
				MethodInvoker.Emit(ilgenerator, OpCodes.Ldarg_0);
				MethodInvoker.EmitUnboxIfNeeded(ilgenerator, methodInfo.DeclaringType);
			}
			bool flag = true;
			ParameterInfo[] parameters = methodInfo.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				Type type = parameters[i].ParameterType;
				bool isByRef = type.IsByRef;
				if (isByRef)
				{
					type = type.GetElementType();
				}
				bool isValueType = type.IsValueType;
				if (isByRef && isValueType && !directBoxValueAccess)
				{
					MethodInvoker.Emit(ilgenerator, OpCodes.Ldarg_1);
					MethodInvoker.EmitFastInt(ilgenerator, i);
				}
				MethodInvoker.Emit(ilgenerator, OpCodes.Ldarg_1);
				MethodInvoker.EmitFastInt(ilgenerator, i);
				if (isByRef && !isValueType)
				{
					MethodInvoker.Emit(ilgenerator, OpCodes.Ldelema, typeof(object));
				}
				else
				{
					MethodInvoker.Emit(ilgenerator, OpCodes.Ldelem_Ref);
					if (isValueType)
					{
						if (!isByRef || !directBoxValueAccess)
						{
							MethodInvoker.Emit(ilgenerator, OpCodes.Unbox_Any, type);
							if (isByRef)
							{
								MethodInvoker.Emit(ilgenerator, OpCodes.Box, type);
								MethodInvoker.Emit(ilgenerator, OpCodes.Dup);
								if (flag)
								{
									flag = false;
									ilgenerator.DeclareLocal(typeof(object), false);
								}
								MethodInvoker.Emit(ilgenerator, OpCodes.Stloc_0);
								MethodInvoker.Emit(ilgenerator, OpCodes.Stelem_Ref);
								MethodInvoker.Emit(ilgenerator, OpCodes.Ldloc_0);
								MethodInvoker.Emit(ilgenerator, OpCodes.Unbox, type);
							}
						}
						else
						{
							MethodInvoker.Emit(ilgenerator, OpCodes.Unbox, type);
						}
					}
				}
			}
			if (methodInfo.IsStatic)
			{
				MethodInvoker.EmitCall(ilgenerator, OpCodes.Call, methodInfo);
			}
			else
			{
				MethodInvoker.EmitCall(ilgenerator, OpCodes.Callvirt, methodInfo);
			}
			if (methodInfo.ReturnType == typeof(void))
			{
				MethodInvoker.Emit(ilgenerator, OpCodes.Ldnull);
			}
			else
			{
				MethodInvoker.EmitBoxIfNeeded(ilgenerator, methodInfo.ReturnType);
			}
			MethodInvoker.Emit(ilgenerator, OpCodes.Ret);
			return (FastInvokeHandler)dynamicMethodDefinition.Generate().CreateDelegate(typeof(FastInvokeHandler));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002731 File Offset: 0x00000931
		internal static void Emit(ILGenerator il, OpCode opcode)
		{
			il.Emit(opcode);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000273A File Offset: 0x0000093A
		internal static void Emit(ILGenerator il, OpCode opcode, Type type)
		{
			il.Emit(opcode, type);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002744 File Offset: 0x00000944
		internal static void EmitCall(ILGenerator il, OpCode opcode, MethodInfo methodInfo)
		{
			il.EmitCall(opcode, methodInfo, null);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000274F File Offset: 0x0000094F
		private static void EmitUnboxIfNeeded(ILGenerator il, Type type)
		{
			if (type.IsValueType)
			{
				MethodInvoker.Emit(il, OpCodes.Unbox_Any, type);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002765 File Offset: 0x00000965
		private static void EmitBoxIfNeeded(ILGenerator il, Type type)
		{
			if (type.IsValueType)
			{
				MethodInvoker.Emit(il, OpCodes.Box, type);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000277C File Offset: 0x0000097C
		internal static void EmitFastInt(ILGenerator il, int value)
		{
			switch (value)
			{
			case -1:
				il.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				il.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				il.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				il.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				il.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				il.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				il.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				il.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				il.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				il.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				if (value > -129 && value < 128)
				{
					il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
					return;
				}
				il.Emit(OpCodes.Ldc_I4, value);
				return;
			}
		}
	}
}
