using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;

namespace MCM.LightInject
{
	// Token: 0x020000EA RID: 234
	[ExcludeFromCodeCoverage]
	internal static class EmitterExtensions
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public static void UnboxOrCast(this IEmitter emitter, Type type)
		{
			bool flag = emitter.StackType == null;
			if (!flag)
			{
				bool flag2 = type == typeof(bool) && emitter.StackType == typeof(int);
				if (!flag2)
				{
					bool flag3 = type == typeof(byte) && emitter.StackType == typeof(int);
					if (!flag3)
					{
						bool flag4 = type == typeof(sbyte) && emitter.StackType == typeof(int);
						if (!flag4)
						{
							bool flag5 = type == typeof(short) && emitter.StackType == typeof(int);
							if (!flag5)
							{
								bool flag6 = type == typeof(ushort) && emitter.StackType == typeof(int);
								if (!flag6)
								{
									bool flag7 = type == typeof(uint) && emitter.StackType == typeof(int);
									if (!flag7)
									{
										bool flag8 = type == typeof(ulong) && emitter.StackType == typeof(long);
										if (!flag8)
										{
											bool isEnum = type.GetTypeInfo().IsEnum;
											if (!isEnum)
											{
												bool flag9 = !type.GetTypeInfo().IsAssignableFrom(emitter.StackType.GetTypeInfo());
												if (flag9)
												{
													emitter.Emit(type.GetTypeInfo().IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, type);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D7BF File Offset: 0x0000B9BF
		public static void PushConstant(this IEmitter emitter, int index, Type type)
		{
			emitter.PushConstant(index);
			emitter.UnboxOrCast(type);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D7D2 File Offset: 0x0000B9D2
		public static void PushConstant(this IEmitter emitter, int index)
		{
			emitter.PushArgument(0);
			emitter.Push(index);
			emitter.PushArrayElement();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public static void PushArrayElement(this IEmitter emitter)
		{
			emitter.Emit(OpCodes.Ldelem_Ref);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		public static void PushArguments(this IEmitter emitter, ParameterInfo[] parameters)
		{
			LocalBuilder argumentArray = emitter.DeclareLocal(typeof(object[]));
			emitter.Emit(OpCodes.Ldarg_0);
			emitter.Emit(OpCodes.Ldarg_0);
			emitter.Emit(OpCodes.Ldlen);
			emitter.Emit(OpCodes.Conv_I4);
			emitter.Emit(OpCodes.Ldc_I4_1);
			emitter.Emit(OpCodes.Sub);
			emitter.Emit(OpCodes.Ldelem_Ref);
			emitter.Emit(OpCodes.Castclass, typeof(object[]));
			emitter.Emit(OpCodes.Stloc, argumentArray);
			for (int i = 0; i < parameters.Length; i++)
			{
				emitter.Emit(OpCodes.Ldloc, argumentArray);
				emitter.Emit(OpCodes.Ldc_I4, i);
				emitter.Emit(OpCodes.Ldelem_Ref);
				emitter.Emit(parameters[i].ParameterType.GetTypeInfo().IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, parameters[i].ParameterType);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000D8FB File Offset: 0x0000BAFB
		public static void Call(this IEmitter emitter, MethodInfo methodInfo)
		{
			emitter.Emit(OpCodes.Callvirt, methodInfo);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D90A File Offset: 0x0000BB0A
		public static void New(this IEmitter emitter, ConstructorInfo constructorInfo)
		{
			emitter.Emit(OpCodes.Newobj, constructorInfo);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public static void Push(this IEmitter emitter, LocalBuilder localBuilder)
		{
			int index = localBuilder.LocalIndex;
			switch (index)
			{
			case 0:
				emitter.Emit(OpCodes.Ldloc_0);
				break;
			case 1:
				emitter.Emit(OpCodes.Ldloc_1);
				break;
			case 2:
				emitter.Emit(OpCodes.Ldloc_2);
				break;
			case 3:
				emitter.Emit(OpCodes.Ldloc_3);
				break;
			default:
			{
				bool flag = index <= 255;
				if (flag)
				{
					emitter.Emit(OpCodes.Ldloc_S, (byte)index);
				}
				else
				{
					emitter.Emit(OpCodes.Ldloc, index);
				}
				break;
			}
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public static void PushArgument(this IEmitter emitter, int index)
		{
			switch (index)
			{
			case 0:
				emitter.Emit(OpCodes.Ldarg_0);
				break;
			case 1:
				emitter.Emit(OpCodes.Ldarg_1);
				break;
			case 2:
				emitter.Emit(OpCodes.Ldarg_2);
				break;
			case 3:
				emitter.Emit(OpCodes.Ldarg_3);
				break;
			default:
			{
				bool flag = index <= 255;
				if (flag)
				{
					emitter.Emit(OpCodes.Ldarg_S, (byte)index);
				}
				else
				{
					emitter.Emit(OpCodes.Ldarg, index);
				}
				break;
			}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public static void Store(this IEmitter emitter, LocalBuilder localBuilder)
		{
			int index = localBuilder.LocalIndex;
			switch (index)
			{
			case 0:
				emitter.Emit(OpCodes.Stloc_0);
				break;
			case 1:
				emitter.Emit(OpCodes.Stloc_1);
				break;
			case 2:
				emitter.Emit(OpCodes.Stloc_2);
				break;
			case 3:
				emitter.Emit(OpCodes.Stloc_3);
				break;
			default:
			{
				bool flag = index <= 255;
				if (flag)
				{
					emitter.Emit(OpCodes.Stloc_S, (byte)index);
				}
				else
				{
					emitter.Emit(OpCodes.Stloc, index);
				}
				break;
			}
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000DAE5 File Offset: 0x0000BCE5
		public static void PushNewArray(this IEmitter emitter, Type elementType)
		{
			emitter.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public static void Push(this IEmitter emitter, int value)
		{
			switch (value)
			{
			case 0:
				emitter.Emit(OpCodes.Ldc_I4_0);
				break;
			case 1:
				emitter.Emit(OpCodes.Ldc_I4_1);
				break;
			case 2:
				emitter.Emit(OpCodes.Ldc_I4_2);
				break;
			case 3:
				emitter.Emit(OpCodes.Ldc_I4_3);
				break;
			case 4:
				emitter.Emit(OpCodes.Ldc_I4_4);
				break;
			case 5:
				emitter.Emit(OpCodes.Ldc_I4_5);
				break;
			case 6:
				emitter.Emit(OpCodes.Ldc_I4_6);
				break;
			case 7:
				emitter.Emit(OpCodes.Ldc_I4_7);
				break;
			case 8:
				emitter.Emit(OpCodes.Ldc_I4_8);
				break;
			default:
			{
				bool flag = value > -129 && value < 128;
				if (flag)
				{
					emitter.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
				}
				else
				{
					emitter.Emit(OpCodes.Ldc_I4, value);
				}
				break;
			}
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		public static void Cast(this IEmitter emitter, Type type)
		{
			emitter.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000DC03 File Offset: 0x0000BE03
		public static void Return(this IEmitter emitter)
		{
			emitter.Emit(OpCodes.Ret);
		}
	}
}
